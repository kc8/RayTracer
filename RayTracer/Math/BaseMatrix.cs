using System; 
using RayTracer; 

namespace RayTracer.Math
{
    ///<summary>
    /// Consider these helper functions for our matrix, for example the 
    /// CopyTo works like this M4 v = new M4(...); v.CopyTo(AnotherM4); 
    ///</summary>
    public static class MatrixExtensions
    {
        ///<summary>
        /// The 'copy' operator. Copy a into b; 
        ///</summary>
        public static void CopyTo(this Matrix a, Matrix b) 
        {
            for (int y = 0; y < a.Height; ++y)
            {
                for (int x = 0; x < a.Width; ++x)
                {
                    b[y, x] = a[y , x];
                }
            }
        }
    }

    ///<summary>
    /// Base Matrix class that provides universal, overwritable
    /// functions for the extended methods
    /// NOTE a matrix is really an array of [][] for rows and columns 
    /// however, since we always know the size of our matrix 
    /// we can use an array and index in directly for example in a 
    /// 4x4 matrix matrix[15] will get us the [4][4] element
    ///</summary>
    //TODO we have lots here in the matrix, but I want to see how this works out first
    // I did not implement the M2 and M3
    // I want to SIMD the matrix
    // I kind of want each row to be a V4 and take advantage of SIMD
    public abstract class Matrix 
    {
        protected int _Width;
        public int Width
        {
            get => _Width; 
            protected set => _Width = value; 
        }

        protected int _Height;
        public int Height
        {
            get => _Height; 
            protected set => _Height = value; 
        }

        public int Count 
        {
            get => _Width * _Height;
        }

        ///<summary>
        /// The values within the matrix
        ///</summary>
        protected float[ , ] Values;

        public float this[int y, int x]
        {
            get 
            {
                return Values[y, x]; 
            }
            set 
            {
                Values[y, x] = value;
            }
        }

        ///<summary>
        /// Remove row and col from matrix to make it a sub-matrix
        ///</summary>
        public Matrix SubDivideMatrix(Matrix a, int x, int y)
        {
            throw new NotImplementedException();
            /*
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                }
            }
            */
        }

        protected Matrix Transpose(Matrix resultant) 
        {
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    resultant[y, x] = Values[x , y];
                }
            }
           return resultant;
        }

        //TODO I think we can do a better job formatting this
        public override string ToString()
        {
            string result = ""; 
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    float val = Values[y,x];  
                    result += $" [{val}] "; 
                }
            }
            return result; 
        }


        public override bool Equals(object obj) 
        {
            M4 m = (obj as M4); 
            if (m == null) 
            {
                return false; 
            }
            bool result = true; 
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    if (Values[y, x] != m[y, x])
                    {
                        result = false; 
                    }
                }
            }
            return result; 
        }
#if false
        public static bool operator !=(Matrix a, Matrix b) 
        {
            return a.Equals(b);
        }

        public static bool operator ==(Matrix a, Matrix b) 
        {
            return a.Equals(b);
        }
#endif
    }
}
