using NUnit.Framework;
using RayTracer.Math; 
using RayTracer; 
using RayTracer.Colors;

namespace RayTracerTests
{
    [TestFixture] //denotes a class that contains unit tests
    public class MainTests
    {
        [SetUp] // "Constructor" for our services
        public void Setup()
        {
        }

        ///<summary>
        /// Dump a bmp to the \RayTracer\Tests\bin\Debug\net5.0 file containg 
        /// a 255 x 255 grid of red pixels
        ///</summary>
        [Test] 
        public void Test_Image()
        {
            bool result = false; 
            int width = 255; 
            int height = 255; 
            IImage image = new Image(height, 255); 
            Color c = new Color(1.0f, 0.0f, 0.0f, 1.0f); 
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < height; ++x)
                {
                    image.SetPixel(x, y, c);
                }
            }
            image.Commit(); 
            result = true; //TODO test that file was created? 
            Assert.IsTrue(result, "Check that the file was written"); 
        }
    }

    [TestFixture] //denotes a class that contains unit tests
    public class Vector 
    {

        ///<summary>
        /// We test to ensure that we init a vector properly
        /// IDEA: We will being playing with SIMD values and want to ensure 
        /// values are true through creation
        ///<summary>
        [Test] 
        public void Is_Vector_Working()
        {
            bool result = false; 
            V4 v = new V4(1.0f, 2.0f, 3.0f, 4.0f); 
            result = v.X == 1.0f ? true : false; 
            result = v.Y == 2.0f ? true : false; 
            result = v.Z == 3.0f ? true : false; 
            result = v.W == 4.0f ? true : false; 
            Assert.IsTrue(result);
        }

        [Test] 
        public void Is_Vector_Equals()
        {
            bool result = false; 
            V4 a = new V4(1.0f, 2.0f, 3.0f, 4.0f); 
            V4 b = new V4(1.0f, 2.0f, 3.0f, 4.0f); 
            result = a.Equals(b); 
            Assert.IsTrue(result, "Vector4 was not equal");
        }

        [Test] 
        public void Is_Vector_Not_Equals()
        {
            bool result = false; 
            //TODO this test does not cover all code paths, we should do that
            V4 a = new V4(1.0f, 2.0f, 3.0f, 5.0f); 
            V4 b = new V4(1.0f, 2.0f, 3.0f, 4.0f); 
            result = a.Equals(b); 
            Assert.IsFalse(result, "Vector4 was equal");
        }

        [Test] 
        public void Vector_Subtract()
        {
            bool result = false; 
            //TODO this test does not cover all code paths, we should do that
            V4 a = new V4(3.0f, 2.0f, 1.0f, 1.0f); 
            V4 b = new V4(5.0f, 6.0f, 7.0f, 1.0f); 
            V4 accurateResult = new V4(-2.0f, -4.0f, -6.0f, 0.0f);
            V4 possibleResult = a - b; 
            result = possibleResult.Equals(accurateResult); 
            Assert.IsTrue(result, "Vector4 subtraction failed with result {0}", possibleResult);
        }

        [Test] 
        public void Vector_Add()
        {
            bool result = false; 
            V4 a = new V4(3.0f, -2.0f, 5.0f, 1.0f); 
            V4 b = new V4(-2.0f, 3.0f, 1.0f, 0.0f); 
            V4 accurateResult = new V4(1, 1, 6, 1);
            V4 possibleResult = a + b; 
            result = possibleResult.Equals(accurateResult); 
            Assert.IsTrue(result, "Vector4 addition failed with result {0}", possibleResult);
        }

        [Test] 
        public void Vector_Negate()
        {
            bool result = false; 
            V4 a = new V4(1.0f, -2.0f, 3.0f, -4.0f); 
            V4 possibleResult = -a; 
            V4 accurateResult = new V4(-1.0f, 2.0f, -3.0f, 4.0f);
            result = possibleResult.Equals(accurateResult); 
            Assert.IsTrue(result, "Vector4 negate failed with result {0}", possibleResult);
        }

        [Test] 
        public void Vector_Mutliply_Scalar()
        {
            bool result = false; 
            V4 a = new V4(1.0f, -2.0f, 3.0f, -4.0f); 
            float scalar = 3.5f; 
            V4 possibleResult = a * scalar;  
            V4 accurateResult = new V4(3.5f, -7.0f, 10.5f, -14.0f);
            result = possibleResult.Equals(accurateResult); 
            Assert.IsTrue(result, "Vector4 multiple scalar a failed with result {0}", possibleResult);
        }

        [Test] 
        public void Vector_Division_Scalar()
        {
            bool result = false; 
            V4 a = new V4(1.0f, -2.0f, 3.0f, -4.0f); 
            float scalar = 2.0f; 
            V4 possibleResult = a / scalar;  
            V4 accurateResult = new V4(0.5f, -1.0f, 1.5f, -2.0f);
            result = possibleResult.Equals(accurateResult); 
            Assert.IsTrue(result, "Vector4 divide scalar a failed with result {0}", possibleResult);
        }

        // TODO we need to test other magnitudes as well at least something with a valid 
        // X, Y Z, and W
        [Test] 
        public void Vector_Magnitude()
        {
            bool result = false; 
            V4 a = new V4(0.0f, 0.0f, 0.0f, 1.0f); 
            float possibleResult = a.Magnitude();
            float accurateResult =  1.0f; 
            result = (possibleResult == accurateResult);
            Assert.IsTrue(result, "Vector4 magnitiude failed with result {0}", possibleResult);
        }

        [Test] 
        public void Vector_Normalize()
        {
            bool result = false; 
            V4 a = new V4(1.0f, 2.0f, 3.0f, 0.0f); 
            V4 possibleResult = a.Normalize(); 
            V4 accurateResult = new V4(0.26726f, 0.53452f, 0.80178f, 0.0f);
            result = possibleResult.Equals(accurateResult); 
            Assert.IsTrue(result, "Vector4 normalize a failed with result {0}", possibleResult);
        }

        [Test] 
        public void Vector_Dot_With_W_As_Zero()
        {
            bool result = false; 
            V4 a = new V4(1.0f, 2.0f, 3.0f, 0.0f); 
            V4 b = new V4(2.0f, 3.0f, 4.0f, 0.0f); 
            float  possibleResult = a.Dot(b); 
            float accurateResult = 20; 
            result = possibleResult.Equals(accurateResult); 
            Assert.IsTrue(result, "Vector4 dot (with w = 0) a failed with result {0}", possibleResult);
        }

        [Test] 
        public void Vector_Cross_3D()
        {
            bool result = false; 
            V4 a = new V4(1.0f, 2.0f, 3.0f, 0.0f); 
            V4 b = new V4(2.0f, 3.0f, 4.0f, 0.0f); 
            V4 possibleResult = a.Cross3D(b); 
            V4 accurateResult =  new V4(-1.0f, 2.0f, -1.0f, 0.0f); 
            result = possibleResult.Equals(accurateResult); 
            Assert.IsTrue(result, "Vector4 dot (with w = 0) a failed with result {0}", possibleResult);
        }

        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Vector_Cross_3D_Backwards()
        {
            bool result = false; 
            V4 a = new V4(1.0f, 2.0f, 3.0f, 0.0f); 
            V4 b = new V4(2.0f, 3.0f, 4.0f, 0.0f); 
            V4 possibleResult = b.Cross3D(a); 
            V4 accurateResult =  new V4(1.0f, -2.0f, 1.0f, 0.0f); 
            result = possibleResult.Equals(accurateResult); 
            Assert.IsTrue(result, "Vector4 dot (with w = 0) a failed with result {0}", possibleResult);
        }

        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Vector_Hadamard()
        {
            bool result = false; 
            V4 a = new V4(1.0f, 0.2f, 0.4f, 0.0f); 
            V4 b = new V4(0.9f, 1.0f, 0.1f, 0.0f); 
            V4 possibleResult = a.Hadamard(b); 
            V4 accurateResult =  new V4(0.9f, 0.2f, 0.04f, 0.0f); 
            result = possibleResult.Equals(accurateResult); 
            Assert.IsTrue(result, "Vector4 dot (with w = 0) a failed with result {0}", possibleResult);
        }
    }
}
