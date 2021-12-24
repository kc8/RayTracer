using System;
using System.Collections.Generic;
using RayTracer.Math;

namespace RayTracer.Shapes
{
    public class Sphere : Shape
    {
        public float Radius { get; set; } = 1.0f;
        public V4 Center { get; set; } = new V4(0.0f);

        public M4 CurrentTransform { get; private set; } = M4.CreateIdentityMatrix();

        public Sphere()
        {
        }

        ///<summary>
        /// Given a ray determine at what points the ray intersects the sphere
        /// This will populate the time at which they are intersected using the 
        /// sphere and ray intersections on a single axis/plane
        ///</summary>
        /// <returns>
        /// A list of T values where the ray intersects there sphere
        /// </returns>
        public override Intersection[] Intersect(Ray r)
        {
           Intersection[] result = new Intersection[2]
           {
              new Intersection(this),
              new Intersection(this),
           };

           V4 sphereToRay = r.Origin - Center; 
           float a = V4.Dot(r.Direction, r.Direction); 
           float b = 2 * V4.Dot(r.Direction, sphereToRay); 
           float c = V4.Dot(sphereToRay, sphereToRay) - 1.0f; 

           // Think of this as the delta
           float discriminant = (b * b) - (4 * a * c);

           float t1; 
           float t2;
           // If negative then sphere misses Ray 
           // 
           if (discriminant < 0)
           {
              //TODO this means the ray misses our sphere, do we care 
              //about negative values indicating we missed the sphere?
               result[0].Time = -1.0f;
               result[1].Time = -1.0f;
           }
           
           // The ray is not tangent, and intersects only 1 part of the sphere
           if (discriminant == 0) 
           {
              // There is only 1 intersection point
              t1 = -(b / 2 *  a);
              t2 = t1;
              result[0].Time = t1;
              result[1].Time = t2;
           }
           // Perfect tangent w/ two intersections. A tangent will always return 
           // the same t values
           if (discriminant > 0) 
           {
              t1 = (-b - MathF.Sqrt(discriminant)) /(2 * a);
              t2 = (-b + MathF.Sqrt(discriminant)) /(2 * a);
              result[0].Time = t1;
              result[1].Time = t2;
           }
           return result;
        }

        ///<summary>
        /// Given a ray determine at what points the ray intersects the sphere 
        ///</summary>
        /// <returns>
        /// A list of T values where the ray intersects there sphere
        /// </returns>
        private float[] IntersectWithRay(Ray r)
        {
           float[] result = new float[2] {0.0f, 0.0f};

           V4 sphereToRay = r.Origin - Center; 
           float a = V4.Dot(r.Direction, r.Direction); 
           float b = 2 * V4.Dot(r.Direction, sphereToRay); 
           float c = V4.Dot(sphereToRay, sphereToRay) - 1.0f; 

           // Think of this as the delta
           float discriminant = (b * b) - (4 * a * c);

           float t1; 
           float t2;
           // If negative then sphere misseray 
           
           if (discriminant < 0)
           {
              //TODO this means the ray misses our sphere, do we care 
              //about negative values indicating we missed the sphere?
               result[0] = -1.0f;
               result[1] = -1.0f;
           }
           // The ray is not tangent, and intersects only 1 part of the sphere
           if (discriminant == 0) 
           {
              // There is only 1 intersection point
              t1 = -(b / 2 *  a);
              t2 = t1;
              result[0] = t1;
              result[1] = t2;
           }
           // Perfect tangent w/ two intersections. A tangent will always return 
           // the same t values
           if (discriminant > 0) 
           {
              t1 = (-b - MathF.Sqrt(discriminant)) /(2 * a);
              t2 = (-b + MathF.Sqrt(discriminant)) /(2 * a);
              result[0] = t1;
              result[1] = t2;
           }
           return result;
        }

        ///<summary>
        /// Apply the given matrix transform to the sphere
        ///</summary>
        public override void Translate(M4 m)
        {
           CurrentTransform = m * CurrentTransform;
        }

        ///<summary>
        /// Apply the given matrix transform to the sphere
        ///</summary>
        public override void Scale(M4 m)
        {
           CurrentTransform = m * CurrentTransform;
           Center = CurrentTransform * Center;
        }
    }
}
