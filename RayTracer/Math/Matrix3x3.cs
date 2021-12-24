using System; 
using RayTracer; 

namespace RayTracer.Math
{
    ///<summary> 
    /// Matrix4x4 
    ///</summary> 
    public class M3 : Matrix
    {
        private static int _Column = 3; 
        private static int _Row = 3; 

        private M3 () 
        {
            Width = _Row; 
            Height = _Column; 
            Values = new float[_Row, _Column]; 
        }

        /// 
        /// [ 0, 1, 2 ]
        /// [ 3, 4, 5 ]
        ///
        public M3( 
                float v1, float v2, float v3, 
                float v4, float v5, float v6, 
                float v7, float v8, float v9) : this()
        {
            Values[0,0] = v1;  
            Values[0,1] = v2;  
            Values[0,2] = v3;  
            Values[1,0] = v4;  
            Values[1,1] = v5;  
            Values[1,2] = v6;  
            Values[2,0] = v7;  
            Values[2,1] = v8;  
            Values[2,2] = v9;  
        }

        ///<summary>
        /// Creates the identity matrix 
        ///</summary>
        public static M3 CreateIdentityMatrix() 
        {
            M3 result = new M3(
                            1.0f, 0.0f, 0.0f, 
                            0.0f, 1.0f, 0.0f, 
                            0.0f, 0.0f, 1.0f); 
            return result; 
        }
    }
}
