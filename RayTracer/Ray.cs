using System;

using RayTracer.Math;

namespace RayTracer
{
    public class Ray
    {
         public V4 Direction { get; set; } 
         public V4 Origin { get; set; } 

         public float Time { get; set; }

         public Ray(V4 origin, V4 direction) 
         {
            Direction = direction;
            Origin = origin;
         }

         ///<summary>
         /// Returns the current position of the ray when given a 
         /// time. 
         /// This indicates where the ray should be in the world
         ///</summary>
         public V4 CurrentRayPosition(float t)
         {
            return Origin + (Direction * t);
         }


         //TODO I am not sure that we are applying the transforms correctly here. 
         // Scaling, mighht but not always effect direction but translation does not 

         ///<summary>
         /// Applies the given matrix transform to the given Ray
         /// A new ray is created to preserve theh original rays information 
         /// (or T value) if its needed
         ///</summary>
         public static Ray Scale(Ray r, M4 m)
         {
            Ray result = new Ray(r.Origin, r.Direction);
            result.Origin =  m * r.Origin;
            result.Direction =  m * r.Direction;
            return result; 
         }

         ///<summary>
         /// Applies the given matrix transform to the given Ray
         /// A new ray is created to preserve theh original rays information 
         /// (or T value) if its needed
         /// Preserves the direction
         ///</summary>
         public static Ray Translate(Ray r, M4 m)
         {
            Ray result = new Ray(r.Origin, r.Direction);
            result.Origin =  m * r.Origin;
            result.Direction = r.Direction;
            return result; 
         }
    }
}
