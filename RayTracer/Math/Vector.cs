using System; 
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace RayTracer.Math
{
    //TODO  for some of these calculations we might want to modify the V4 instead of creating a new one
    //TODO I kind of want a V3, as math might get trickt with the V4
    ///<summary> 
    /// This is SIMD vector with C#. This is a little tricky as it will not support
    /// all devices/platforms/cpus etc. There are some ways to handle this with checking 
    /// the platform but I did not implement this here. 
    /// https://devblogs.microsoft.com/dotnet/hardware-intrinsics-in-net-core/
    /// https://docs.microsoft.com/en-us/dotnet/api/system.runtime.intrinsics.x86?view=net-5.0
    ///<summary> 
    public class V4
    {
        ///<summary>
        /// This will be used to hold the results from any operations
        ///<summary>
        private Vector128<float> _Value; 
        public float X;
        public float Y;
        public float Z;
        public float W; // w = 1 is a point, w = 0 is a vector

        ///<summary>
        /// Init a Vector of length 4 that offers SIMD operations
        ///<summary>
        public V4(float x, float y, float z, float w)
        {
            X = x; 
            Y = y; 
            Z = z;
            W = w;
            _Value = Vector128<float>.Zero; 
        }

        ///<summary>
        /// Init a Vector of length 4 that offers SIMD operations
        /// Init x,y,z components as given W componenet defaults to 
        /// 1.0f
        ///<summary>
        public V4(float x, float y, float z)
        {
            X = x; 
            Y = y; 
            Z = z;
            W = 1.0f;
            _Value = Vector128<float>.Zero; 
        }

        ///<summary>
        /// Init a Vector of length 4, with the each componenet 
        /// initilized to the given value
        ///<summary>
        public V4(float c)
        {
            X = c; 
            Y = c; 
            Z = c;
            W = c;
            _Value = Vector128<float>.Zero; 
        }

        ///<summary>
        /// Return a new Empty Vec4 w/ values initilized to all 0.0f
        ///<summary>
        public static V4 CreateEmptyV4()
        {
            V4 result = new V4(0.0f, 0.0f, 0.0f, 0.0f);
            return result; 
        }

        ///<summary> 
        /// NOTE and TODO we only compute for three dimensions, right now we are not doing this, it 
        /// does not really matter for our case(s) anyway
        /// Cross product, the order mattesr here. The vector that is returned is perpendicular 
        /// to the original vectors. 
        /// Example: when figuring out axis (X Y Z) you can do X CROSS Y and get the Z axis
        ///<summary> 
        public V4 Cross3D(V4 b)
        {
            //TODO SIMD
            float newX = (this.Y * b.Z) - (this.Z * b.Y);
            float newY = (this.Z * b.X) - (this.X * b.Z);
            float newZ = (this.X * b.Y) - (this.Y * b.X);
            return new V4(newX, newY, newZ, 0.0f); 
        }

        ///<summary> 
        /// Multiple a vector by another vecotor. This does a per-position 
        /// multiple. This function kind of "blends" the two vectors togther often 
        /// used in mixing colors.
        /// We could use an operator here, but this is a unique operation which 
        /// I think should be made clear?
        ///<summary> 
        public V4 Hadamard(V4 b)
        {
            Vector128<float> vecA = Vector128.Create(X, Y, Z, W); 
            Vector128<float> vecB = Vector128.Create(b.X, b.Y, b.Z, b.W); 
            Vector128<float> hadResult = Sse.Multiply(vecA, vecB); 
            V4 result = CreateVectorFromSIMD(hadResult); 
            return result; 
        }

        ///<summary> 
        /// Calculator the Dot product (also the scalar or inner product) 
        /// Useful when axis or vectors cross 
        /// --- What does the Dot Product tell us? ---
        /// Smaller the dot, the larger thhe angle between the vectors, and larger whens a greater angle. 
        /// The dot product is the cosine of the angle between them 
        /// Example: dot product of 1 = angles pointsing in the same direction, dot of -1 means opposite direction 
        ///<summary> 
        public static float Dot(V4 a, V4 b)
        {
            //TODO SIMD
            float result = 0.0f; 
            result = ((a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z) + (a.W * b.W)); 
            return result;
        }

        ///<summary> 
        /// Calculator the Dot product (also the scalar or inner product) 
        /// Useful when axis or vectors cross 
        /// --- What does the Dot Product tell us? ---
        /// Smaller the dot, the larger thhe angle between the vectors, and larger whens a greater angle. 
        /// The dot product is the cosine of the angle between them 
        /// Example: dot product of 1 = angles pointsing in the same direction, dot of -1 means opposite direction 
        ///<summary> 
        public float Dot(V4 b)
        {
            //TODO SIMD
            float result = 0.0f; 
            result = ((this.X * b.X) + (this.Y * b.Y) + (this.Z * b.Z) + (this.W * b.W)); 
            return result;
        }

        ///<summary> 
        /// Puts the vector within a range from 0 - 1 to normalize the vector
        /// or its gets "unit vector"
        /// The normal is the V4/its magnitude
        ///<summary> 
        public V4 Normalize()
        {
            float magnitude = this.Magnitude();  
            if (magnitude == 0) 
            {
                return this; 
            } 
            Vector128<float> denom = Vector128.Create(magnitude); 
            Vector128<float> num = Vector128.Create(X, Y, Z, W); 
            Vector128<float> normal = Sse.Divide(num, denom); 
            V4  result = CreateVectorFromSIMD(normal); 
            return result; 
        }

        ///<summary> 
        /// Get the magnitude or length of a vector. 
        /// how far the vector travled in a line, or direction and distance
        ///<summary> 
        // TODO There is a way to do this with exp and logs, but I forget and 
        // don't see it in the C# intrinsics doc
        public float Magnitude()
        {
            //TODO There are different percision with the SSE operations and SQRT
            //TODO make this simd? Could not find the intrinsic I needed
            //Vector128<float> multiResult = Sse.Multiply(vec, vec); 
            float newX = X * X; 
            float newY = Y * Y; 
            float newZ = Z * Z; 
            float newW = W * W; 

            float added = newX + newY + newZ + newW; 

            Vector128<float> vec = Vector128.Create(added); 
            Vector128<float> sqrt = Sse.Sqrt(vec);  
            float result = Vector128.GetElement<float>(sqrt, 0); 
            float result1 = Vector128.GetElement<float>(sqrt, 1); 
            float result2 = Vector128.GetElement<float>(sqrt, 2); 
            float result3 = Vector128.GetElement<float>(sqrt, 3); 
            System.Console.WriteLine("{0}, {1}, {2}, {3}", result, result1, result2, result3);
            return result; 
        }

        ///<summary> 
        /// We can 'cast' our Vector4 (V4) into a Vector128<float>
        /// The following is an example of this use: 
        /// DoSomething(V4 a)
        /// {
        ///     Vector128<float> IntrinsicA = a; 
        /// }
        ///<summary> 
        public static implicit operator Vector128<float>(V4 vec)
        {
            Vector128<float> result = Vector128<float>.Zero; 
            result = Vector128.Create(vec.X, vec.Y, vec.Z, vec.W); 
            return result; 
        }

        ///<summary>
        ///Negate the vector
        ///<summary>
        //TODO I think we can SIMD this with a subtraction
        public static V4 operator -(V4 a) 
        {
            float newX = -a.X;
            float newY = -a.Y;
            float newZ = -a.Z; 
            float newW = -a.W;
            V4 result = new V4(newX, newY, newZ, newW); 
            return result; 
        }

        ///<summary>
        /// Negate a vector, can also use -V4 to negate
        ///</summary>
        public V4 Negate() 
        {
            return -this; 
        }

        ///<summary>
        /// Scalar multiple of a vector. 
        ///</summary>
        public static V4 operator *(V4 a, float b)
        {
            Vector128<float> vec = a;
            Vector128<float> scalar = Vector128.Create(b); 
            // This is __m128 _mm_mul_ps
            Vector128<float> multiResult = Sse.Multiply(vec, scalar); 
            V4  result = CreateVectorFromSIMD(multiResult); 
            return result; 
        }

        ///<summary>
        /// Divide the vector by a  scalar. 
        /// NOTE its best to multiple rather than divide, do this for /2 instead
        /// V4 result = 0.5 * V4;
        ///</summary>
        public static V4 operator /(V4 a, float b)
        {
            if (b == 0) 
            {
                //We could throw an exception here but since we have a perfmance 
                //focused goal, I don't want to be unwinding the stack 
                //or wasting CPU checking for them;
                return new V4(0.0f, 0.0f, 0.0f, 0.0f); 
            }

            Vector128<float> vec = a;
            Vector128<float> scalar = Vector128.Create(b); 
            // This is __m128 _mm_div_ps
            Vector128<float> multiResult = Sse.Divide(vec, scalar); 
            V4  result = CreateVectorFromSIMD(multiResult); 
            return result; 
        }

        public static V4 operator -(V4 a, V4 b)
        {
            if (Sse.IsSupported == true)
            {
                Vector128<float> IntrinsicA = a; 
                Vector128<float> IntrinsicB = b; 
                Vector128<float> subtractResult = Sse.Subtract(IntrinsicA, IntrinsicB); 

                V4 result = CreateVectorFromSIMD(subtractResult); 
                return result; 
            }
            else 
            {
                //TODO some kind of logger
                System.Console.WriteLine("WARNING, SSE not supported"); 
                float newX = a.X - b.X; 
                float newY = a.Y - b.Y; 
                float newZ = a.Z - b.Z; 
                float newW = a.W - b.W; 
                return new V4(newX, newY, newZ, newW); 
            }
        }

        public static V4 operator +(V4 a) => a; 
        public static V4 operator +(V4 a, V4 b)
        {
            if (Sse.IsSupported == true)
            {
                Vector128<float> IntrinsicA = a; 
                Vector128<float> IntrinsicB = b; 
                Vector128<float> additionResult = Sse.Add(IntrinsicA, IntrinsicB); 
                V4 result = CreateVectorFromSIMD(additionResult); 
                return result; 
            }
            else 
            {
                System.Console.WriteLine("WARNING, SSE not supported"); 
                float newX = a.X + b.X; 
                float newY = a.Y + b.Y; 
                float newZ = a.Z + b.Z; 
                float newW = a.W + b.W; 
                return new V4(newX, newY, newZ, newW); 
            }
        }

        /// OVERRIDES
        public override int GetHashCode() 
        {
            //TODO make this hash code better
            return (int)(X + Y + Z + W);
        }

        public override string ToString() 
        {
            string result = $@"[{X}][{Y}][{Z}][{W}]";
            return result; 
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            V4 objAsVec = (V4)obj; 
            bool xEquals = X.CompareFloatWithEpsilon(objAsVec.X) ? true : false;
            bool yEquals = Y.CompareFloatWithEpsilon(objAsVec.Y) ? true : false;
            bool zEquals = Z.CompareFloatWithEpsilon(objAsVec.Z) ? true : false;
            bool wEquals = W.CompareFloatWithEpsilon(objAsVec.W) ? true : false;
            if(xEquals == true && 
                yEquals == true && 
                zEquals == true && 
                wEquals == true)
            {
                return true; 
            }
            return false; 
        }

        public static bool operator ==(V4 a, V4 b) 
        {
            return a.Equals(b);
        }

        public static bool operator !=(V4 a, V4 b) 
        {
            return a.Equals(b);
        }

        // STATIC HELPER FUNCTIONS
        internal void UpdateValuesV4(Vector128<float> b)
        {
            X = Vector128.GetElement<float>(b, 0); 
            Y = Vector128.GetElement<float>(b, 1); 
            Z = Vector128.GetElement<float>(b, 2); 
            W = Vector128.GetElement<float>(b, 3); 
        }

        internal static V4 CreateVectorFromSIMD(Vector128<float> vec)
        {
            float X = Vector128.GetElement<float>(vec, 0); 
            float Y = Vector128.GetElement<float>(vec, 1); 
            float Z = Vector128.GetElement<float>(vec, 2); 
            float W = Vector128.GetElement<float>(vec, 3); 
            V4 result = new V4(X, Y, Z, W); 
            return result; 
        }
    }

    public static class VectorExtensions
    {
    }
}
