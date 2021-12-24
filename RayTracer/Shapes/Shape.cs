using System;

using RayTracer.Math;

namespace RayTracer.Shapes
{
    public abstract class Shape
    {
        public int ID { get; protected set; }

        public abstract Intersection[] Intersect(Ray r);
        public abstract void Translate(M4 m);
        public abstract void Scale(M4 m);
    }
}
