using System;

namespace RayTracer.Math
{
    ///<summary>
    /// Consider these helper functions for our V4, Color functions etc
    ///<summary>
    public static class MathExtensions
    {
        ///<summary>
        /// Comparing floats leads to a rounding error, to be 
        /// cautious we can create an epsilon to help uuse 
        /// compare properly. If they are within that range we know they 
        /// are equal otherwise not
        ///<summary>
        public static bool CompareFloatWithEpsilon(this float a, float b)
        {
            bool result = false; 
            const float epsilon = 0.00001f; 
            if (MathF.Abs(a - b) < epsilon) 
            {
               result = true;  
            }
            return result; 
        }

       public static int ScaleToRange(this int num, 
                int min, 
                int max, 
                int scaleMin, 
                int scaleMax) 
        {
            int deno = max - min;
            if (deno == 0)
            {
                return scaleMin;  
            }
            float m = (num - min) / deno; 
            float result = m * (scaleMax - scaleMin) + scaleMin; 
            return (int)result; 
        }

        public static int ScaleToRangeAndCastToInt(this float num, 
                float min, 
                float max, 
                float scaleMin, 
                float scaleMax) 
        {
            float deno = max - min;
            if (deno == 0)
            {
                return (int)scaleMin;  
            }
            float m = (num - min) / deno; 
            float result = m * (scaleMax - scaleMin) + scaleMin; 
            return (int)result; 
        }

        public static int FloatToInt32(this float a) 
        {
            int result = (int)MathF.Round(a); 
            return result; 
        }

        ///<summary>
        /// Clamp the given value between a max and min 
        ///<summary>
        public static T Clamp<T>(this T num, T min, T max)
            where T : IComparable<T>
        {
            if (num.CompareTo(min) <= 0) 
            {
                return min; 
            }
            if (num.CompareTo(max) >= 0) 
            {
                return max; 
            }
            return num; 
        }
    }
}
