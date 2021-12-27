using System;
using NUnit.Framework;

using RayTracer.Math; 
using RayTracer; 
using RayTracer.Colors;
using RayTracer.Shapes;

namespace RayTracerTests
{
    [TestFixture]
    public class DrawTestSphere 
    {
        [Test]
        public void DrawTestSphere_DrawSimpleSphere()
        {
            // location of 'wall' or where the ray stops
            V4 wall_pos = new V4(0,0,10,0.0f); 
            V4 rayOrigin = new V4(0,0,-5,0.0f);
            V4 rayDirection = new V4(0,0,0,0.0f); 
            // how tall thhe wall is
            float wallSize = 7.0f;

            // Setup the image/canvas
            int canvasHeight = 100; 
            int canvasWidth = 100;
            float pixelSize = wallSize/canvasWidth;
            float halfCanvas = wallSize/2;
            // The following simple sphere should hit its target, if not we fail 
            // the test
            bool isHit = false; 

            Color sphereColor = new Color(1.0f, 0.0f, 0.0f, 0.0f); 
            Image canvas = new Image(canvasWidth, canvasHeight); 
            
            // Ray starting at 0,0-5 and point in the 0,0,0 direction
            Ray ray = new Ray(rayOrigin, rayDirection);
            Sphere sphere = new Sphere();

            for (int y = 0; y < canvasHeight; ++y)
            {
                // World cordinate
                float worldY = halfCanvas - pixelSize * y;
                for (int x = 0; x < canvasWidth; ++x)
                {
                    float worldX = -halfCanvas + pixelSize * x;
                    V4 canvasPoint = new V4(worldX, worldY, wallSize);
                    V4 newRayDirection = canvasPoint - ray.Origin; 
                    ray = new Ray(ray.Origin, newRayDirection.Normalize());

                    Intersection[] intersections =  sphere.Intersect(ray);
                    if (intersections[0].Time > 0 && intersections[1].Time > 0)
                    {
                        canvas.SetPixel(y, x, sphereColor);
                        isHit = true; 
                    }
                }
            }
            canvas.Commit("FirstSphere.bmp");
            Assert.True(isHit, "First Sphere did not get it a hit, when it should");
        }

        [Test]
        [Ignore("TODO need to work on rotatation")]
        public void DrawTestSphere_DrawScaledAndRotatedSphere()
        {
            // location of 'wall' or where the ray stops
            V4 wall_pos = new V4(0,0,10,0.0f); 
            V4 rayOrigin = new V4(0,0,-5,0.0f);
            V4 rayDirection = new V4(0,0,0,0.0f); 
            // how tall thhe wall is
            float wallSize = 7.0f;

            // Setup the image/canvas
            int canvasHeight = 100; 
            int canvasWidth = 100;
            float pixelSize = wallSize/canvasWidth;
            float halfCanvas = wallSize/2;
            // The following simple sphere should hit its target, if not we fail 
            // the test
            bool isHit = false; 

            Color sphereColor = new Color(1.0f, 0.0f, 0.0f, 0.0f); 
            Image canvas = new Image(canvasWidth, canvasHeight); 
            
            // Ray starting at 0,0-5 and point in the 0,0,0 direction
            Ray ray = new Ray(rayOrigin, rayDirection);
            Sphere sphere = new Sphere();
            V4 scalingVector = new V4(1.0f, 1.0f, 1.0f);
            float rotate = MathF.PI/4.0f;
            //M4 scalingMat = M4.CreateScaleMatrix(scalingVector);
            M4 rotateMat = M4.CreateIdentityMatrix();
            rotateMat.RotateAboutYAxis(rotate);
            //scalingMat = scalingMat * rotateMat;
            //sphere.Scale(scalingMat);

            for (int y = 0; y < canvasHeight; ++y)
            {
                // World cordinate
                float worldY = halfCanvas - pixelSize * y;
                for (int x = 0; x < canvasWidth; ++x)
                {
                    float worldX = -halfCanvas + pixelSize * x;
                    V4 canvasPoint = new V4(worldX, worldY, wallSize);
                    V4 newRayDirection = canvasPoint - ray.Origin; 
                    ray = new Ray(ray.Origin, newRayDirection.Normalize());
                    //M4 scalingInvertMat = scalingMat.Invert(); 
                    //ray = Ray.Scale(ray, scalingInvertMat);

                    Intersection[] intersections =  sphere.Intersect(ray);
                    if (intersections[0].Time > 0 && intersections[1].Time > 0)
                    {
                        canvas.SetPixel(y, x, sphereColor);
                        isHit = true; 
                    }
                }
            }
            canvas.Commit("ShrinkAndRotate.bmp");
            Assert.True(isHit, "First Sphere did not get it a hit, when it should");
        }

        [Test]
        public void DrawTestSphere_DrawScaledYAxisSphere()
        {
            // location of 'wall' or where the ray stops
            V4 wall_pos = new V4(0,0,10,0.0f); 
            V4 rayOrigin = new V4(0,0,-5,0.0f);
            V4 rayDirection = new V4(0,0,0,0.0f); 
            // how tall thhe wall is
            float wallSize = 7.0f;

            // Setup the image/canvas
            int canvasHeight = 100; 
            int canvasWidth = 100;
            float pixelSize = wallSize/canvasWidth;
            float halfCanvas = wallSize/2;
            // The following simple sphere should hit its target, if not we fail 
            // the test
            bool isHit = false; 

            Color sphereColor = new Color(1.0f, 0.0f, 0.0f, 0.0f); 
            Image canvas = new Image(canvasWidth, canvasHeight); 
            
            // Ray starting at 0,0-5 and point in the 0,0,0 direction
            Ray ray = new Ray(rayOrigin, rayDirection);
            Sphere sphere = new Sphere();
            V4 scalingVector = new V4(1.0f, 0.5f, 1f);
            M4 scalingMat = M4.CreateScaleMatrix(scalingVector);
            sphere.Scale(scalingMat);

            for (int y = 0; y < canvasHeight; ++y)
            {
                // World cordinate
                float worldY = halfCanvas - pixelSize * y;
                for (int x = 0; x < canvasWidth; ++x)
                {
                    float worldX = -halfCanvas + pixelSize * x;
                    V4 canvasPoint = new V4(worldX, worldY, wallSize);
                    V4 newRayDirection = canvasPoint - ray.Origin; 
                    ray = new Ray(ray.Origin, newRayDirection.Normalize());
                    ray = Ray.Scale(ray, scalingMat);

                    Intersection[] intersections =  sphere.Intersect(ray);
                    if (intersections[0].Time > 0 && intersections[1].Time > 0)
                    {
                        canvas.SetPixel(y, x, sphereColor);
                        isHit = true; 
                    }
                }
            }
            canvas.Commit("ScaledSphere.bmp");
            Assert.True(isHit, "First Sphere did not get it a hit, when it should");
        }
    }
}
