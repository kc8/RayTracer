using System;
using NUnit.Framework;

using RayTracer.Math; 
using RayTracer; 
using RayTracer.Colors;
using RayTracer.Shapes;

namespace RayTracerTests
{
    [TestFixture]
    public class RaySphereIntersections 
    {
        [Test] 
        public void RaySphereIntersection_EnsureSetup()
        {
            V4 origin = new V4(1.0f, 2.0f, 3.0f, 1.0f);
            V4 direction = new V4(4.0f, 5.0f, 6.0f, 1.0f);
            Ray ray = new Ray(origin, direction); 
            Assert.True(origin == ray.Origin, "Ray Origin is not set properly");
            Assert.True(direction == ray.Direction, "Ray Origin is not set properly");
        }

        [Test] 
        public void RaySphereIntersection_PositionWithTimeT()
        {
            V4 origin = new V4(2.0f, 3.0f, 4.0f, 0.0f);
            V4 direction = new V4(1.0f, 0.0f, 0.0f, 0.0f);
            Ray ray = new Ray(origin, direction); 
            
            V4 current = ray.CurrentRayPosition(0.0f);
            V4 actualPosition = new V4(2.0f,3.0f,4.0f, 0.0f);
            Assert.True(current.Equals(actualPosition), 
                    $@"T0 {actualPosition.ToString()} != {current}");

            current = ray.CurrentRayPosition(1.0f);
            actualPosition = new V4(3.0f,3.0f,4.0f, 0.0f);
            Assert.True(current.Equals(actualPosition),
                    $@"T1 {actualPosition.ToString()} != {current}");

            current = ray.CurrentRayPosition(-1.0f);
            actualPosition = new V4(1.0f,3.0f,4.0f, 0.0f);
            Assert.True(current.Equals(actualPosition),
                    $@"T2 {actualPosition.ToString()} != {current}");

            current = ray.CurrentRayPosition(2.5f);
            actualPosition = new V4(4.5f,3.0f,4.0f, 0.0f);
            Assert.True(current.Equals(actualPosition),
                    $@"T3 {actualPosition.ToString()} != {current}");
        }

        ///<summary>
        /// Test that a ray intersects a sphere on a straight line from the 
        /// strating point of the ray
        ///</summary>
        [Test] 
        public void RaySphereIntersection_CenterRadiusOfCircle()
        {
            V4 origin = new V4(0.0f, 0.0f, -5.0f, 0.0f);
            V4 direction = new V4(0.0f, 0.0f, 1.0f, 0.0f);
            float[] answers = new float[2]{4.0f, 6.0f};

            Ray ray = new Ray(origin, direction); 
            Sphere s = new Sphere();

            Intersection[] intersections = s.Intersect(ray);
            Assert.True(intersections.Length == answers.Length, 
                    $@"Sphere intersections count was {intersections} when it should be {answers.Length}");
            Assert.True(intersections[0].Time == answers[0], 
                    $@"Sphere intersections failed {intersections[0]} when it should be {answers[0]}");
            Assert.True(intersections[1].Time == answers[1], 
                    $@"Sphere intersections failed {intersections[0]} when it should be {answers[1]}");
        }

        ///<summary>
        /// Test the ray as we move it up to the 'top' of the sphere
        ///</summary>
        [Test] 
        public void RaySphereIntersection_TopOfSphereAtCenter()
        {
            V4 origin = new V4(0.0f, 1.0f, -5.0f, 0.0f);
            V4 direction = new V4(0.0f, 0.0f, 1.0f, 0.0f);
            //NOTE answers represent at what 'time'
            float[] answers = new float[2]{ 5.0f, 5.0f };

            Ray ray = new Ray(origin, direction); 
            Sphere s = new Sphere();

            Intersection[] intersections = s.Intersect(ray);
            Assert.True(intersections.Length == answers.Length, 
                    $@"Sphere intersections count was {intersections} when it should be {answers.Length}");
            Assert.True(intersections[0].Time == answers[0], 
                    $@"Sphere intersections failed {intersections[0]} when it should be {answers[0]}");
            Assert.True(intersections[1].Time == answers[1], 
                    $@"Sphere intersections failed {intersections[0]} when it should be {answers[1]}");
        }

        ///<summary>
        /// Test the ray as we move it up to the 'top' of the sphere
        ///</summary>
        [Test] 
        public void RaySphereIntersection_StartInsideSphere()
        {
            V4 origin = new V4(0.0f, 0.0f, 0.0f, 0.0f);
            V4 direction = new V4(0.0f, 0.0f, 1.0f, 0.0f);
            //NOTE answers represent at what 'time'
            float[] answers = new float[2] { -1.0f, 1.0f };

            Ray ray = new Ray(origin, direction); 
            Sphere s = new Sphere();

            Intersection[] intersections = s.Intersect(ray);
            Assert.True(intersections.Length == answers.Length, 
                    $@"Sphere intersections count was {intersections} when it should be {answers.Length}");
            Assert.True(intersections[0].Time == answers[0], 
                    $@"Sphere intersections failed {intersections[0]} when it should be {answers[0]}");
            Assert.True(intersections[1].Time == answers[1], 
                    $@"Sphere intersections failed {intersections[0]} when it should be {answers[1]}");
        }

        ///<summary>
        /// Test the ray as we move it up to the 'top' of the sphere
        ///</summary>
        [Test] 
        public void RaySphereIntersection_MissedIntersection()
        {
            V4 origin = new V4(0.0f, 5.0f, -5.0f, 0.0f);
            V4 direction = new V4(0.0f, 0.0f, 1.0f, 0.0f);
            //NOTE answers represent at what 'time'
            float[] answers = new float[2] { -1.0f, -1.0f };

            Ray ray = new Ray(origin, direction); 
            Sphere s = new Sphere();
            Intersection[] intersections = s.Intersect(ray);
            Assert.True(intersections.Length == answers.Length, 
                    $@"Sphere intersections count was {intersections} when it should be {answers.Length}");
            Assert.True(intersections[0].Time == answers[0], 
                    $@"Sphere intersections failed {intersections[0]} when it should be {answers[0]}");
            Assert.True(intersections[1].Time == answers[1], 
                    $@"Sphere intersections failed {intersections[0]} when it should be {answers[1]}");
                
        }

        ///<summary>
        ///</summary>
        [Test] 
        public void RaySphereIntersection_BehindSphere()
        {
            V4 origin = new V4(0.0f, 0.0f, 5.0f, 0.0f);
            V4 direction = new V4(0.0f, 0.0f, 1.0f, 0.0f);
            //NOTE answers represent at what 'time'
            float[] answers = new float[2] { -6.0f, -4.0f };

            Ray ray = new Ray(origin, direction); 
            Sphere s = new Sphere();
            Intersection[] intersections = s.Intersect(ray);
            Assert.True(intersections.Length == answers.Length, 
                    $@"Sphere intersections count was {intersections} when it should be {answers.Length}");
            Assert.True(intersections[0].Time == answers[0], 
                    $@"Sphere intersections failed {intersections[0]} when it should be {answers[0]}");
            Assert.True(intersections[1].Time == answers[1], 
                    $@"Sphere intersections failed {intersections[0]} when it should be {answers[1]}");
                
        }

        [Test] 
        public void RayInteractionTests_TransformRay()
        {
            Ray r = new Ray(new V4(1,2,3), new(0,1,0));
            V4 translate = new V4(3.0f, 4.0f, 5.0f);
            M4 m = M4.CreateTranslationMatrix(translate);
            Ray transformedR = Ray.Translate(r, m);
            V4 answerOrigin = new V4(4,6,8);
            Assert.True(transformedR.Origin == answerOrigin,
                    $@"Ray translation failed with {transformedR.Origin} not {answerOrigin}"); 
            V4 answerDirection = new V4(0,1,0);
            Assert.True(transformedR.Direction == answerDirection,
                    $@"Ray translation failed with {transformedR.Direction} not {answerDirection}"); 
            
        }

        [Test] 
        public void RayInteractionTests_ScalingRay()
        {
            Ray r = new Ray(new V4(1,2,3), new(0,1,0));
            V4 translate = new V4(2.0f, 3.0f, 4.0f);
            M4 m = M4.CreateScaleMatrix(translate);
            Ray transformedR = Ray.Scale(r, m);
            V4 answerOrigin = new V4(2,6,12);
            Assert.True(transformedR.Origin == answerOrigin,
                    $@"Ray translation failed with {transformedR.Origin} not {answerOrigin}"); 
            V4 answerDirection = new V4(0,3,0);
            Assert.True(transformedR.Direction == answerDirection,
                    $@"Ray translation failed with {transformedR.Direction} not {answerDirection}"); 
            
        }

        [Test] 
        public void RayInteractionTests_SphereDefaultTransform()
        {
           Sphere s = new Sphere(); 
           M4 transformAnswer = M4.CreateIdentityMatrix();
           Assert.True(s.CurrentTransform.Equals(transformAnswer), 
                   "sphere transform was not the identity matrix");
        }

        [Test] 
        public void RayInteractionTests_SphereTransformation()
        {
            Sphere s = new Sphere(); 
            M4 translation = M4.CreateTranslationMatrix(new V4(2,3,4));
            s.Translate(translation); 
            Assert.True(s.CurrentTransform.Equals(translation), 
                    $@"Current transform {s.CurrentTransform} does not equal {translation}"); 
        }

        [Test] 
        // pg 69
        /// Scale a ray by the inverse of how you want to scale the sphere
        public void RayInteractionTests_SphereTransformationWithRay()
        {
            Sphere s = new Sphere(); 
            Ray r = new Ray(new V4(0,0,-5, 0.0f), new V4(0,0,1,0.0f));
            M4 scaling = M4.CreateScaleMatrix(new V4(2,2,2,2));
            s.Scale(scaling);

            M4 scalingInvert = scaling.Invert();
            Ray scaledRay = Ray.Scale(r, scalingInvert);
            Intersection[] intersections = s.Intersect(scaledRay);
            Assert.True(intersections[0].Time == 3.0f, 
                   $@"Intersection was incorrect with {intersections[0].Time} not being 3.0f"); 
            Assert.True(intersections[1].Time == 7.0I, 
                   $@"Intersection was incorrect with {intersections[1].Time} not being 7.0f"); 
        }

        [Test] 
        // pg 69
        /// Scale a ray by the inverse of how you want to scale the sphere
        public void RayInteractionTests_SphereTranslatedWithRay()
        {
            Sphere s = new Sphere(); 
            Ray r = new Ray(new V4(0, 0, -5, 1.0f), new V4(0, 0, 1, 1.0f));
            M4 scaling = M4.CreateTranslationMatrix(new V4(5, 0, 0, 0));
            s.Translate(scaling);

            //M4 scalingInvert = scaling.Invert();
            Ray scaledRay = Ray.Translate(r, scaling);
            Intersection[] intersections = s.Intersect(scaledRay);
            Assert.True(intersections[0].Time == -1.0f, 
                   $@"Intersection was incorrect with {intersections[0].Time} not being -1.0f"); 
            Assert.True(intersections[1].Time == -1.0f, 
                   $@"Intersection was incorrect with {intersections[1].Time} not being -1.0f"); 
        }
    }
}

