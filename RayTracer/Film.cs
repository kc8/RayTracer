using System; 

using RayTracer.Math;
using RayTracer.Colors;

namespace RayTracer
{
    ///<summary>
    /// This is the what holds the data regarding the film such as the 
    /// aspect ratiom 
    ///</summary>
    public class Film 
    {
        private float _AspectRatio;
        public float AspectRatio
        {
            get => _AspectRatio; 
        }
    }
}
