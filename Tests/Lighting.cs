using System;
using NUnit.Framework;

using RayTracer.Math; 
using RayTracer; 
using RayTracer.Colors;
using RayTracer.Shapes;

namespace RayTracerTests
{
    [TestFixture]
    public class LightingTest 
    {
        [Test]
        public void LightingTest_SphereNormalsXAxis()
        {
            Sphere s = new Sphere();
            V4 normal = s.NormalAt(new V4(1,0,0));
            V4 normalResult = new V4(1,0,0); 
            Assert.True(normal == normalResult, 
                    $@"Lighting for sphere normal calculation failed with {normal} and not {normalResult}");
        }

        [Test]
        public void LightingTest_SphereNormalsYAxis()
        {
            Sphere s = new Sphere();
            V4 normal = s.NormalAt(new V4(0,1,0));
            V4 normalResult = new V4(0,1,0); 
            Assert.True(normal == normalResult, 
                    $@"Lighting for sphere normal calculation failed with {normal} and not {normalResult}");
        }

        [Test]
        public void LightingTest_SphereNormalsZAxis()
        {
            Sphere s = new Sphere();
            V4 normal = s.NormalAt(new V4(0,0,1));
            V4 normalResult = new V4(0,0,1); 
            Assert.True(normal == normalResult, 
                    $@"Lighting for sphere normal calculation failed with {normal} and not {normalResult}");
        }

        [Test]
        public void LightingTest_SphereNonaxialNormalized()
        {
            Sphere s = new Sphere();
            float sqrt3  = MathF.Sqrt(3.0f)/3.0f;
            V4 normal = s.NormalizedNormalAt(new V4(sqrt3));
            V4 normalResult = new V4(sqrt3); 
            normalResult = normalResult.Normalize();
            Assert.True(normal == normalResult, 
                    $@"Lighting for sphere normal calculation failed with {normal} and not {normalResult}");
        }

        [Test]
        public void LightingTest_SphereNonaxial()
        {
            Sphere s = new Sphere();
            float sqrt3  = MathF.Sqrt(3.0f)/3.0f;
            V4 normal = s.NormalAt(new V4(sqrt3));
            V4 normalResult = new V4(sqrt3); 
            Assert.True(normal == normalResult, 
                    $@"Lighting for sphere normal calculation failed with {normal} and not {normalResult}");
        }
    }
}
