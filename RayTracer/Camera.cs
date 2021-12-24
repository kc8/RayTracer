using System; 

using RayTracer.Math;
using RayTracer.Colors;

namespace RayTracer
{
    // DECISION: This would be good for a singelton, however, I am 
    // thinking, multiple cameras in the future? 
    public class Camera
    {
        private V4 CameraPosition { get; set; }

        public Camera(IImage image)
        {
        }
    }
}
