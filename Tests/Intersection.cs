using System;
using NUnit.Framework;

using RayTracer.Math; 
using RayTracer; 
using RayTracer.Colors;
using RayTracer.Shapes;

namespace RayTracerTests
{
    [TestFixture]
    public class IntersectionTests 
    {
        [Test] 
        public void Intersection_EncapsulateTandObject()
        {
            Sphere s = new Sphere();
            Intersection i = new Intersection(s, 3.5f);
            Assert.True(i.Time == 3.5);
            Assert.True(i.Shape == s);
        }
        
        [Test] 
        public void Intersection_IntersectionsHavePositiveT()
        {
            Sphere s = new Sphere();
            Intersection i1 = new Intersection(s, 1.0f);
            Intersection i2 = new Intersection(s, 2.0f);
            Intersections intersections = new Intersections(i2, i1);
            Intersection hit = intersections.Hit;
            Assert.True(hit == i1, $@"Hit was {hit.Time}");  
        }

        [Test] 
        public void Intersection_IntersectionsHaveNegativeT()
        {
            Sphere s = new Sphere();
            Intersection i1 = new Intersection(s, -1.0f);
            Intersection i2 = new Intersection(s, 2.0f);
            Intersections intersections = new Intersections(i1, i2);
            Intersection hit = intersections.Hit;
            Assert.True(hit == i2, $@"Hit was {hit?.Time}");  
        }

        [Test] 
        public void Intersection_IntersectionsHaveAllnegativeT()
        {
            Sphere s = new Sphere();
            Intersection i1 = new Intersection(s, -1.0f);
            Intersection i2 = new Intersection(s, -2.0f);
            Intersections intersections = new Intersections(i1, i2);
            Intersection hit = intersections.Hit;
            Assert.True(hit == null, $@"Hit was {hit?.Time}, when it should be null");  
        }

        [Test] 
        public void Intersection_IntersectionsAlwaysTheLowestNonNegativeIntersection()
        {
            Sphere s = new Sphere();
            Intersection i1 = new Intersection(s, 4.0f);
            Intersection i2 = new Intersection(s, 7.0f);
            Intersection i3 = new Intersection(s, -3.0f);
            Intersection i4 = new Intersection(s, 2.0f);
            Intersection[] list = new Intersection[4] {i1, i2, i3, i4};
            Intersections intersections = new Intersections(list);
            Intersection hit = intersections.Hit;
            Assert.True(hit == i4 , $@"Hit was {hit?.Time}");  
        }
    }
}
