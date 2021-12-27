using System; 

using RayTracer; 

namespace RayTracer.Math
{
    ///<summary> 
    /// Matrix4x4 
    ///</summary> 
    public class M4 : Matrix
    {
        private static int _Column = 4; 
        private static int _Row = 4;

        private M4() 
        {
            Width = _Column; 
            Height = _Row;
            Values = new float[_Row, _Column]; 
        }

        ///<summary>
        /// Returns an empty M4 array with default values of 0.0f
        ///</summary>
        public static M4 CreateEmptyMatrix() 
        {
            M4 result = new M4(0.0f, 0.0f, 0.0f, 0.0f, 
                            0.0f, 0.0f, 0.0f, 0.0f, 
                            0.0f, 0.0f, 0.0f, 0.0f,
                            0.0f, 0.0f, 0.0f, 0.0f); 
            return result; 
        }

        ///<summary>
        /// Creates a scale matrix with the given vector
        /// x, .. ..  
        /// .. y, ..  
        /// .. .. z, 
        /// .. .. .. w
        ///</summary>
        public static M4 CreateScaleMatrix(V4 v) 
        {
            M4 result = M4.CreateIdentityMatrix();
            result.Scale(v);
            return result; 
        }

        ///<summary>
        /// Create a rotation matrix with the inital radians set
        ///</summary>
        public static M4 CreateRotationMatrixAboutXAxis(float radian)
        {
            M4 result = M4.CreateIdentityMatrix(); 
            result[0,0] = 1.0f;
            result[1,1] = MathF.Cos(radian);
            result[1,2] = -(MathF.Sin(radian));
            result[2,1] = MathF.Sin(radian);
            result[2,2] = MathF.Cos(radian);
            return result;

        }

        ///<summary>
        /// Given a radian rotate it about the Y axis
        /// This is done in place
        ///</summary>
        public static M4 CreateRotationMatrixAboutYAxis(float radian)
        {
            M4 result = M4.CreateIdentityMatrix(); 
            result[0,0] = MathF.Cos(radian);
            result[0,2] = MathF.Sin(radian);
            result[1,1] = 1.0f;
            result[2,0] = -(MathF.Sin(radian));
            result[2,2] = MathF.Cos(radian);
            return result;
        }

        ///<summary>
        /// Given a radian rotate it about the Y axis
        /// This is done in place
        ///</summary>
        public static M4 CreateRotationMatrixAboutZAxis(float radian)
        {
            M4 result = M4.CreateIdentityMatrix(); 
            result[0,0] = MathF.Cos(radian);
            result[0,1] = -(MathF.Sin(radian));
            result[1,0] = MathF.Sin(radian);
            result[1,1] = MathF.Cos(radian);
            result[2,2] = 1.0f; // TODO in the rotation matrixs, is this correct?
            return result;
        }


        ///<summary>
        /// Moves a point by adding or subtracting from that point
        /// by the matrix
        ///</summary>
        public static M4 CreateTranslationMatrix(V4 a)
        {
            M4 result = M4.CreateIdentityMatrix(); 
            result[0,3] = a.X; 
            result[1,3] = a.Y; 
            result[2,3] = a.Z;
            result[3,3] = 1.0f;
            return result; 
        }

        ///<summary>
        /// Creates the identity matrix 
        ///</summary>
        public static M4 CreateIdentityMatrix() 
        {
            M4 result = new M4(
                            1.0f, 0.0f, 0.0f, 0.0f, 
                            0.0f, 1.0f, 0.0f, 0.0f, 
                            0.0f, 0.0f, 1.0f, 0.0f,
                            0.0f, 0.0f, 0.0f, 1.0f); 
            return result; 
        }

        ///<summary>
        /// Create a 4x4 matrix with initilized values provided
        /// When passing parameterts, think of the following: 
        /// Y   X: 0, 1, 2, 3
        /// 0 [[0, 1, 2, 3 ]]
        /// 1 [[0, 1, 2, 3 ]]
        /// 2 [[0, 1, 2, 3 ]]
        /// 3 [[0, 1, 2, 3 ]]
        ///<summary>
        public M4( 
                float v1, float v2, float v3, 
                float v4, float v5, float v6, 
                float v7, float v8, float v9, 
                float v10,float v11, float v12, 
                float v13, float v14, float v15, 
                float v16) : this ()
        {
           Values[0,0] = v1;  Values[1,0] = v5;  
           Values[0,1] = v2;  Values[1,1] = v6;  
           Values[0,2] = v3;  Values[1,2] = v7;  
           Values[0,3] = v4;  Values[1,3] = v8;  
           Values[2,0] = v9;  Values[3,0] = v13;  
           Values[2,1] = v10;  Values[3,1] = v14;  
           Values[2,2] = v11;  Values[3,2] = v15;  
           Values[2,3] = v12;  Values[3,3] = v16;  
        }

        ///<summary>
        /// Transpose a matrix. Useful when translating between 
        /// an object spacfe and world space
        /// Turns rows into columns and columns into rows
        ///</summary>
        public M4 Transpose()
        {
           M4 result = M4.CreateIdentityMatrix();  
           base.Transpose(result); 
           return result;
        }

        private void Swap(ref float a, ref float b)
        {
            float temp = a;
            a = b;
            b = temp;
        }

        ///<summary>
        /// Inverting matricies. Thhinkg of it as undoing something like 10 * 2 = 20
        /// You can do 20*1/2 to get 10 
        /// This method uses Co-Factor Expansion
        ///</summary>
        public M4 Invert()
        {
            bool  result = false;
            if (_Column != _Row)
            {
                //TODO I dont want to use exceptions but we need to ensure 
                //we have a NXN matrix. Maybe use assert?
                throw new Exception("Tried to invert a non invertable matrix");
            }
            M4 invertedMat = M4.CreateIdentityMatrix();  
            float det = Determinant(this);
            if (det > 0.0f || det < 0.0f)
            {
                result = ProtectedInvert(ref invertedMat);
            }
            return invertedMat;
        }

        //using row echelon reduction combined with the Gauss-Jordan elimination 
        //method
        // Credit to ScratchPixel for the help with figuring out the inverse
        private bool ProtectedInvert(ref M4 newMat)
        {
            float[,] tempValues = this.Values;
            bool result = false;
            // 1. Go through each column and find a pivot, 
            // the pivtot will be the diagnal that creates an identity matrix (our pivot is values[colum, column]
            // 2. Check this pivot is > 0, if not we need to find a row withh the abs > 0 in the same spot, we pick up the value that is the highest
            //  -> if this is not possible the matrix cannot be inverted
            // 3. We then use Gauss Jordan elimination method to transform orginal matrix to identity matrix
            for (int column = 0; column < _Column; ++column) 
            {
                // Step 2: Get into row-echelon form
                if (tempValues[column, column] == 0) // Our pivot is zero
                {
                    // We try to see if we can create our pivote to be > 0
                    int largerRow = column;
                    for (int row = 0; row < _Row; ++row)
                    {
                        if (MathF.Abs(tempValues[row,column]) > MathF.Abs(tempValues[largerRow, column]))
                        {
                            largerRow = row;
                        }
                    }
                    if (largerRow == column)
                    {
                        // Matrix is not invertable, meaning we could not replace the pivot
                        throw new Exception("Invalid matrix for invert");
                        return result;
                    }
                    else 
                    {
                        // Swap out the newl
                        for (int i = 0; i < _Row; ++i)
                        {
                            Swap(ref tempValues[column, i], ref tempValues[largerRow, i]);
                            Swap(ref newMat.Values[column, i], ref newMat.Values[largerRow, i]);
                        }
                    }
                }
                // Give us a row reducded echelon form 
                //1. If we have a valid pivot in the column we can contain
                //2. Reduce each coefficent of the column (excluding the pivot) to zero
                //. -> that is  multiple each coefficent 'm' multiplied by each coefficent of the row
                for (int row = 0; row < _Row; ++row)
                {
                    if (row != column)
                    {
                        // scale                                    //pivot
                        float coefficent = tempValues[row, column] / tempValues[column, column];
                        if (coefficent != 0)
                        {
                            for (int j = 0; j < _Row; ++j)
                            {
                                tempValues[row, j] -= (coefficent * tempValues[column, j]);
                                newMat[row, j] -= (coefficent * newMat[column, j]);
                            }
                            tempValues[row, column] = 0;
                        }
                    }
                }
            }
            // All the pivots should now be 0, which we need to set to 1
            for (int row = 0; row < _Row; ++row)
            {
                for (int column = 0; column < _Column; ++column) 
                {
                    newMat[row, column] /= tempValues[row, row];
                }
            }
            return result;
        }

        ///<summary>
        /// Remove row and col from matrix to make it a sub-matrix
        /// NOTE that we return an M4 but with the non relevant cols/rows 
        /// zeroed out
        ///</summary>
        public static M4 SubMatrix(M4 a, int x, int y)
        {
            M4 result = M4.CreateIdentityMatrix();  
            a.CopyTo(result); 
            result[y, 0] = 0.0f; 
            result[y, 1] = 0.0f;
            result[y, 2] = 0.0f;
            result[y, 3] = 0.0f;
            result[0, x] = 0.0f;
            result[1, x] = 0.0f;
            result[2, x] = 0.0f;
            result[3, x] = 0.0f;
            return result; 
        }

        ///<summary>
        /// Shreading/ skewing transformation makes a 'straight' line slanted
        /// Changes each component of the given vector in proportion to the 
        /// other componenets. 
        ///</summary>
        public static M4 CreateShearMatirx(V4 v1, V4 v2) 
        {
            // Where v1 = Xi and v2 = Yi
            throw new NotImplementedException("Check page 153 of 3d math book, I think we want a better way to handle this");
            M4 result = M4.CreateIdentityMatrix();
            result[0,1] = v1.X;
            result[0,2] = v1.Z;
            result[1,1] = v2.X;
            result[1,2] = v2.Z;
            //result[2,0] = v2.W; 
            //result[0,1] = v.W; 
            return result;
        }

        ///<summary>
        /// Scales the point by the matrix
        /// This is done in place
        /// Preserves w
        ///</summary>
        public void Scale(V4 v)
        {
            this[0,0] *= v.X;
            this[1,1] *= v.Y;
            this[2,2] *= v.Z;
            if (v.W > 0)
            {
                this[3,3] *= v.W;
            }
        }

        ///<summary>
        /// Determine the determinant
        /// This is a hard coded solution to avoid things like Minor and sub matricies
        ///</summary>
        public static float Determinant(M4 m) 
        {
             float result = 
             m[0,3] * m[1,2] * m[2,1] * m[3,0] - m[0,2] * m[1,3] * m[2,1] * m[3,0] -
             m[0,3] * m[1,1] * m[2,2] * m[3,0] + m[0,1] * m[1,3] * m[2,2] * m[3,0] +
             m[0,2] * m[1,1] * m[2,3] * m[3,0] - m[0,1] * m[1,2] * m[2,3] * m[3,0] -
             m[0,3] * m[1,2] * m[2,0] * m[3,1] + m[0,2] * m[1,3] * m[2,0] * m[3,1] +
             m[0,3] * m[1,0] * m[2,2] * m[3,1] - m[0,0] * m[1,3] * m[2,2] * m[3,1] -
             m[0,2] * m[1,0] * m[2,3] * m[3,1] + m[0,0] * m[1,2] * m[2,3] * m[3,1] +
             m[0,3] * m[1,1] * m[2,0] * m[3,2] - m[0,1] * m[1,3] * m[2,0] * m[3,2] -
             m[0,3] * m[1,0] * m[2,1] * m[3,2] + m[0,0] * m[1,3] * m[2,1] * m[3,2] +
             m[0,1] * m[1,0] * m[2,3] * m[3,2] - m[0,0] * m[1,1] * m[2,3] * m[3,2] -
             m[0,2] * m[1,1] * m[2,0] * m[3,3] + m[0,1] * m[1,2] * m[2,0] * m[3,3] +
             m[0,2] * m[1,0] * m[2,1] * m[3,3] - m[0,0] * m[1,2] * m[2,1] * m[3,3] -
             m[0,1] * m[1,0] * m[2,2] * m[3,3] + m[0,0] * m[1,1] * m[2,2] * m[3,3];
             return result;
        }
        
        public static M4 Minor(M4 a, int x, int y)
        {
            M4 result = M4.CreateIdentityMatrix();  
            return result;
        }

        public static M4 Cofactor(M4 a, int x, int y)
        {
            M4 result = M4.CreateIdentityMatrix();  
            return result;
        }

        ///<summary>
        /// Moves a point changing the coorindates of the point in 
        /// the negative or positive direction given a vector
        /// ***preserves w***
        ///<summary>
        public V4 Transform(V4 v)
        {
            float newX = v.X*this[0,3] + v.Y*this[2,1] + v.Z*this[3,1];
            float newY = v.X*this[1,2] + v.Y*this[2,2] + v.Z*this[3,2];
            float newZ = v.X*this[1,3] + v.Y*this[2,3] + v.Z*this[3,3];
            float newW = v.W;
            return new V4(newX, newY, newZ, newW); 
        }

        ///<summary>
        /// Given a radian rotate it about the X axis
        /// This is done in place
        ///</summary>
        public void RotateAboutXAxis(float radian)
        {
            this[0,0] *= 1.0f;
            this[1,1] *= MathF.Cos(radian);
            this[1,2] *= -(MathF.Sin(radian));
            this[2,1] *= MathF.Sin(radian);
            this[2,2] *= MathF.Cos(radian);
        }

        ///<summary>
        /// Given a radian rotate it about the Y axis
        /// This is done in place
        ///</summary>
        public void RotateAboutYAxis(float radian)
        {
            this[0,0] *= MathF.Cos(radian);
            this[0,2] *= MathF.Sin(radian);
            this[1,1] *= 1.0f;
            this[2,0] *= -(MathF.Sin(radian));
            this[2,2] *= MathF.Cos(radian);
        }

        ///<summary>
        /// Given a radian rotate it about the Y axis
        /// This is done in place
        ///</summary>
        public void RotateAboutZAxis(float radian)
        {
            this[0,0] *= MathF.Cos(radian);
            this[0,1] *= -(MathF.Sin(radian));
            this[1,0] *= MathF.Sin(radian);
            this[1,1] *= MathF.Cos(radian);
            this[2,2] *= 1.0f; // TODO in the rotation matrixs, is this correct?
        }

        ///<summary>
        /// Shreading/ skewing transformation makes a 'straight' line slanted
        /// Changes each component of the given vector in proportion to the 
        /// other componenets. 
        ///</summary>
        public void Shear(V4 v1, V4 v2) 
        {
            // Where v1 = Xi and v2 = Yi
            throw new NotImplementedException("This method needs more work");
            this[0,1] = v1.X;
            this[0,2] = v1.Z;
            this[1,1] = v2.X;
            this[1,2] = v2.Z;
            //this[2,0] = v.W; 
            //this[0,1] = v.W; 
        }

        ///<summary>
        /// A reflection moves the point to the other side of its axis 
        /// We can scale by a negative value in the axis position we want
        /// See Matrix_ReflectVectorThroughScaling for example
        ///</summary>
        public V4 ReflectVector(V4 axisVec, V4 pointToReflect) 
        {
            M4 scaleMatrix = M4.CreateScaleMatrix(axisVec);
            V4 result  = scaleMatrix * pointToReflect; 
            return result;
        }

        ///<summary>
        /// This is the 'dot' product of the matrix. 
        ///</summary>
        public static M4 operator *(M4 a, M4 b)
        {
            M4 result = M4.CreateEmptyMatrix(); 
            for (int y = 0; y < a.Height; ++y)
            {
                for (int x = 0; x < a.Width; ++x)
                {
                    //TODO SIMD Dot product as these are really vectors
                    result[y, x] = 
                        (a[y, 0] * b[0, x]) +
                        (a[y, 1] * b[1, x]) +
                        (a[y, 2] * b[2, x]) +
                        (a[y, 3] * b[3, x]);
                }
            }
            return result;
        }

        ///<summary>
        ///
        ///<summary>
        public static V4 operator *(M4 a, V4 b)
        {
            V4 result = V4.CreateEmptyV4(); 
            result.X = b.X*a[0,0] + b.Y*a[0,1] + b.Z*a[0,2] + b.W*a[0,3];
            result.Y = b.X*a[1,0] + b.Y*a[1,1] + b.Z*a[1,2] + b.W*a[1,3];
            result.Z = b.X*a[2,0] + b.Y*a[2,1] + b.Z*a[2,2] + b.W*a[2,3];
            result.W = b.X*a[3,0] + b.Y*a[3,1] + b.Z*a[3,2] + b.W*a[3,3];
            return result;
        }
    }
}
