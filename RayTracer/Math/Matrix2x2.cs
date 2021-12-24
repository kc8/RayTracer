using System; 
using RayTracer; 

namespace RayTracer.Math
{
#if true 
    ///<summary> 
    /// Matrix2x2
    ///</summary> 
    public class M2 : Matrix
    {
        private static int _Column = 2; 
        private static int _Row = 2; 

        private M2 () 
        {
            Width = _Column;
            Width = _Row; 
            Values = new float[_Row ,  _Column]; 
        }

        ///
        /// [ 0, 1, 2 ]
        /// [ 3, 4, 5 ]
        ///
        public M2( 
                float v1, float v2, float v3, 
                float v4) : this()
        {
           Values[0,0] = v1;  
           Values[0,1] = v2;  
           Values[1,0] = v3;  
           Values[1,1] = v4;  
        }

        ///<summary>
        /// Transpose a matrix. Useful when translating between 
        /// an object spacfe and world space
        /// Turns rows into columns and columns into rows
        ///</summary>
        public M2 Transpose()
        {
           M2 result = M2.CreateIdentityMatrix();  
           base.Transpose(result); 
           return result;
        }

        ///<summary>
        /// Creates the identity matrix 
        ///</summary>
        public static M2 CreateIdentityMatrix() 
        {
            M2 result = new M2(
                            1.0f, 0.0f, 
                            0.0f, 1.0f); 
            return result; 
        }
    }
#endif
}


