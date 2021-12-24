using System; 
using RayTracer.Math;
using RayTracer.Shapes;

namespace RayTracer
{
   public enum Hit 
   {
      Miss = 0,
      Hit = 1, 
   };

   ///<summary>
   /// Collection of intersections of objects
   /// Containing the hits of the objects or how the object has intersected...
   ///</summary>
   public class Intersections
   {
      public Intersection[] IntersectionsCollection { get; private set; }

      private int _Size;

      private int _Count;
      public int Length 
      {
         get => _Count;
      }

      public Intersection this[int index]
      {
         get => IntersectionsCollection[index];
      }

      private bool _HitUpToDate;
      private Intersection _Hit = null; 
      ///<summary>
      /// Get the currently update intersection or re-compute 
      /// if we need to
      /// If the returned hit is null, this means there is not intersection
      ///</summary>
      public Intersection Hit 
      {
         get 
         {
            if (_HitUpToDate)
            {
               return _Hit;
            }
            else 
            {
               ComputeIntersection();
               return _Hit;
            } 
         }
      }

      public Intersections()
      {
         _Size = 1;
         IntersectionsCollection = new Intersection[_Size]; 
         _Count = 0;
      }

      public Intersections(int size)
      {
         IntersectionsCollection = new Intersection[size]; 
         _Size = size;
         _Count = 0;
      }

      public Intersections(Intersection a, Intersection b)
      {
         _Size = 2;
         IntersectionsCollection = new Intersection[_Size]; 
         _Count = 0;
         Add(a);
         Add(b);
         ComputeIntersection();
      }

      public Intersections(Intersection[] intersections) 
      {
         IntersectionsCollection = intersections;
         ComputeIntersection();
      }

      public void ComputeIntersection()
      {
         for (int i = 0; i < IntersectionsCollection.Length; ++i)
         {
            if (IntersectionsCollection[i].Time > 0)
            {
               _Hit = IntersectionsCollection[i];
            }
         }
      }

      public void Add(Intersection i)
      {
         if (_Size > _Count) 
         {
            IntersectionsCollection[_Count++] = i;
         }
         else 
         {
            throw new NotImplementedException("The array needs to expand"); 
         }
      }
   }

   ///<summary>
   /// A single intersection containing the Time, the specific shape
   ///</summary>
   public class Intersection
   {
       public Shape Shape { get; private set; }
       public float Time  { get; set; }

       public Intersection(Shape s, float time)
       {
          Shape = s; 
          Time = time;
       }

       public Intersection(Shape s)
       {
          Shape = s; 
       }
   }
}
