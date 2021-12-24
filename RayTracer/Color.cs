using System; 
using RayTracer.Math;

namespace RayTracer.Colors
{
    ///<summary>
    /// A color is really just a V4 with some extra features
    ///</summary>
    // TODO we could use a generic Color because <int> and <float> will 
    // help us with either 0 - 255 or 0f - 1f values
    public class Color : V4
    {
        public float R
        {
            get => base.X; 
            set => base.X = value; 
        }

        public float G
        {
            get => base.Y; 
            set => base.Y = value; 
        }

        public float B
        {
            get => base.Z; 
            set => base.Y = value; 
        }

        public float A
        {
            get => base.W; 
            set => base.W = value; 
        }

        ///<summary>
        /// r = red, g = green, b = blue, a = alpha (1.0f/ 255 is solid)
        ///</summary>
        public Color(float r, float g, float b, float a) 
            : base(r, g, b, a) 
        {
        }
    }

    public static class ColorExtensions
    {
        ///<summary> 
        /// Convert a normalized color to a 0 thru to 255 color value
        ///</summary> 
        public static Color Linear1ToSRGB255(this Color c)
        {
            float one255 = 255.0f; 
            int newX = (one255*c.X).FloatToInt32().Clamp(0, 255); 
            int newY = (one255*c.Y).FloatToInt32().Clamp(0, 255);
            int newZ = (one255*c.Z).FloatToInt32().Clamp(0, 255);
            int newA = (one255*c.Z).FloatToInt32().Clamp(0, 255);

            return new Color(newX, newY, newZ, newA);  
        }
    }
}
