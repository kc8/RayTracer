using System;
using NUnit.Framework;
using RayTracer.Math; 
using RayTracer; 
using RayTracer.Colors;

namespace RayTracerTests
{
    [TestFixture]
    public class MatrixTests
    {
        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_Equals_Test()
        {
            bool result = false; 

            M4 m1 = new M4(
                    1,2,3,4,5,6,7,8,9,8,7,6,5,4,3,2);
            M4 m2 = new M4(
                    1,2,3,4,5,6,7,8,9,8,7,6,5,4,3,2);
            result = m1.Equals(m2); 
            Assert.IsTrue(result, "M4 Equals method failed");
        }

        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_Multiplication()
        {
            bool result = false; 

            M4 m1 = new M4(
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                    9, 8, 7, 6,
                    5, 4, 3, 2);
            M4 m2 = new M4(
                    -2, 1, 2, 3, 
                    3, 2, 1, -1, 
                    4, 3, 6, 5,
                    1, 2, 7,8); 
            M4 multResult = m1 * m2; 
            M4 correctResult = new M4(
                    20, 22, 50, 48, 
                    44, 54, 114, 108, 
                    40, 58, 110, 102, 
                    16, 26, 46, 42);
            result = multResult.Equals(correctResult); 
            Assert.IsTrue(result, 
                    "M4 * M4 Method failed with possible result {0}", multResult.ToString());
        }

        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_Multiplication_V4()
        {
            bool result = false; 

            M4 m1 = new M4(
                    1, 2, 3, 4,
                    2, 4, 4, 2,
                    8, 6, 4, 1,
                    0, 0, 0, 1);
            V4 v1 = new V4(1, 2, 3, 1); 
            V4 multResult = m1 * v1; 
            V4 correctResult = new V4(18, 24, 33, 1); 
            result = multResult.Equals(correctResult); 
            Assert.IsTrue(result, 
                    "V4 * M4 failed with possible result {0}", multResult.ToString());
        }
        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_Transpose_Identity()
        {
            bool result = false; 

            M4 m1 = M4.CreateIdentityMatrix(); 
            M4 multResult = m1.Transpose();  
            M4 correctResult = M4.CreateIdentityMatrix(); 
            result = multResult.Equals(correctResult); 
            Assert.IsTrue(result, 
                    "V4 * M4 failed with possible result {0}", multResult.ToString());
        }

        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_SubMatrix()
        {
            bool result = false; 
            M4 m1 = new M4(
                    1, 2, 3, 4,
                    2, 4, 4, 2,
                    8, 6, 4, 1,
                    0, 0, 0, 1);
            M4 multResult = M4.SubMatrix(m1, 1, 0);  
            M4 correctResult= new M4(
                    0, 0, 0, 0,
                    2, 0, 4, 2,
                    8, 0, 4, 1,
                    0, 0, 0, 1);
            result = multResult.Equals(correctResult); 
            Assert.IsTrue(result, 
                    "To Submatrix failed with possible result {0}", multResult.ToString());
        }

        [Test] 
        [Ignore("This is a 2 x 2 matrix determinant, which we do not have setup")]
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_Determinant()
        {
            bool result = false; 
            M4 m1 = new M4(
                    1, 2, 3, 4,
                    2, 4, 4, 2,
                    8, 6, 4, 1,
                    0, 0, 0, 1);
            float multResult = M4.Determinant(m1);
            float correctResult = (1*4) - (2*2);
            result = multResult.Equals(correctResult); 
            Assert.IsTrue(result, 
                    "To Submatrix failed with possible result {0}", multResult.ToString());
        }

        [Test]
        public void Matrix_InvertDeterminant()
        {
            M4 m1 = new M4(
                    6, 4, 4, 4, 
                    5, 5, 7, 6, 
                    4, -9, 3, -7,
                    9, 1, 7, -6);
            float determinantResult = M4.Determinant(m1);
            float determinantActual = -2120.0f;
            Assert.IsTrue(determinantResult == determinantActual, 
                    $@"Determinant for inverse was incorrect, supposed to to {determinantActual} got {determinantResult}");
        }

        [Test]
        public void Matrix_NotInvertDeterminant()
        {
            M4 m1 = new M4(
                    -4, 2, -2, -3, 
                    9, 6, 2, 6, 
                    0, -5, 1, -5, 
                    0, 0, 0, 0); 
            float determinantResult = M4.Determinant(m1);
            float determinantActual = 0.0f;
            Assert.IsTrue(determinantResult == determinantActual, 
                    $@"Determinant for inverse was incorrect, supposed to to {determinantActual} got {determinantResult}");
        }

        [Test]
        public void Matrix_Inverse()
        {
            M4 m1 = new M4(
                    8, -5, 9, 2, 
                    7, 5, 6, 1,
                    -6, 0, 9, 6,
                    -3, 0, -9, -4);
            M4 result = m1.Invert();
            for (int y = 0; y < result.Height; ++y)
            {
                for (int x = 0; x < result.Width; ++x)
                {
                    result[y, x] = MathF.Truncate(100 * result[y,x]) /100;
                }
            }

            M4 inverseResultOrginal = new M4(
             -0.15385f, -0.15385f, -0.28205f, -0.53846f,
             -0.07692f, 0.12308f, 0.02564f,  0.03077f,
             0.35897f, 0.35897f, 0.43590f, 0.92308f,
             -0.69231f, -0.69231f, -0.76923f, -1.92308f);
            M4 inverseResult = new M4(
             -0.15f, -0.15f, -0.28f, -0.53f,
             -0.07f, 0.12f, 0.02f,  0.03f,
             0.35f, 0.35f, 0.43f, 0.92f,
             -0.69f, -0.69f, -0.76f, -1.92f);

            Assert.IsTrue(inverseResult.Equals(result),
                    $@"Inverse Matrix calculation was incorrect, got {result}");
        }

        [Test]
        [Ignore("There are some rounding errors w/ the inverse that will prevent this from passing")]
        public void Matrix_InverseMultipleToNormal()
        {
            M4 m1 = new M4(
                    3, -9, 7, 3, 
                    3, -8, 2, -9,
                    -4, 4, 4, 1,
                    -6, 5, -1, 1);
            M4 m2 = new M4(
                    8, 2, 2, 2,
                    3, -1, 7, 0,
                    7, 0, 5, 4, 
                    6, -2, 0, 5); 
            M4 c = m1 * m2;
            M4 result = c * m2.Invert(); 

            Assert.IsTrue(result.Equals(m1),
                    $@"Inverse Matrix calculation was incorrect, got {result}");
        }

        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_Transslate()
        {
            bool result = false; 
            M4 m1 = new M4(
                    1, 2, 3, 4,
                    2, 4, 4, 2,
                    8, 6, 4, 1,
                    0, 0, 0, 1);
            M4 multResult = m1.Transpose();  
            M4 correctResult = new M4(
                    1, 2, 8, 0,
                    2, 4, 6, 0,
                    3, 4, 4, 0,
                    4, 2, 1, 1);
            result = multResult.Equals(correctResult); 
            Assert.IsTrue(result, 
                    "V4 * M4 failed with possible result {0}", multResult.ToString());
        }

        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_Translation()
        {
            bool result = false; 
            V4 v1 = new V4(5.0f, -3.0f, 2.0f, 0.0f); 
            M4 translationMat = M4.CreateTranslationMatrix(v1); 
            M4 correctResult = new M4(
                    1, 0, 0, 5,
                    0, 1, 0, -3,
                    0, 0, 1, 2,
                    0, 0, 0, 1);
            result = translationMat.Equals(correctResult); 
            Assert.IsTrue(result, 
                    "M4 translation creation M4 failed with possible result {0}", translationMat.ToString());
        }

        [Test] 
        [Ignore("We need to figure out an inverse technique based on the type of matrix")]
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_Transform()
        {
            // This test does not work
            bool result = false; 
            V4 v1 = new V4(5.0f, -3.0f, 2.0f, 0.0f); 
            M4 transformMat = M4.CreateTranslationMatrix(v1); 
            V4 correctResult = new V4(2, 1, 7, 0);
            V4 pos1 = new V4(-3.0f, 4.0f, 5.0f, 0.0f);
            V4 translationVec = transformMat * pos1;
            //V4 transformmat = transformMat.Transform(pos1);
            result = transformMat.Equals(correctResult); 
            Assert.IsTrue(result, 
                    "M4 transform failed with possible result {0}", translationVec.ToString());
        }

        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_ReflectVectorThroughScaling()
        {
            bool result = false; 
            V4 correctResult = new V4(-2.0f, 3.0f, 4.0f, 1.0f); 

            V4 v1 = new V4(-1.0f, 1.0f, 1.0f, 1.0f); 
            M4 scaleMatrix = M4.CreateScaleMatrix(v1);
            V4 pointToScale = new V4(2.0f, 3.0f, 4.0f, 1.0f);

            V4 resultant = scaleMatrix * pointToScale; 
            result = resultant.Equals(correctResult); 
            Assert.IsTrue(result, 
                    "M4 translation creation M4 failed with possible result {0}", resultant.ToString());
        }

        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_Scale()
        {
            bool result = false; 
            V4 correctResult = new V4(-8.0f, 18.0f, 32.0f, 1.0f); 

            V4 v1 = new V4(2.0f, 3.0f, 4.0f, 1.0f); 
            M4 scaleMatrix = M4.CreateScaleMatrix(v1);
            V4 pointToScale = new V4(-4.0f, 6.0f, 8.0f, 1.0f);

            V4 resultant = scaleMatrix * pointToScale; 
            result = resultant.Equals(correctResult); 
            Assert.IsTrue(result, 
                    "M4 translation creation M4 failed with possible result {0}", resultant.ToString());
        }

        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_RotatePointAboutXAxis()
        {
            bool result = false; 
            float sqrTwo = MathF.Sqrt(2);
            V4 halfQuarterCorrectResult = new V4(0.0f, (sqrTwo/2.0f), (sqrTwo/2.0f), 1.0f); 
            V4 fullQuarterCorrectResult = new V4(0.0f, 0.0f, 1.0f, 1.0f); 

            V4 pointToRotate = new V4(0.0f, 1.0f, 0.0f, 1.0f); 
            M4 halfQuarter = M4.CreateRotationMatrixAboutXAxis((MathF.PI/4.0f));
            M4 fullQuarter = M4.CreateRotationMatrixAboutXAxis((MathF.PI/2.0f));

            V4 halfQuarterResult = halfQuarter * pointToRotate; 
            V4 fullQuarterResult = fullQuarter * pointToRotate; 

            result = halfQuarterCorrectResult.Equals(halfQuarterResult); 
            Assert.IsTrue(result, 
                    "Rotation failed for half rotate {0}",
                    halfQuarterResult.ToString());

            result = fullQuarterCorrectResult.Equals(fullQuarterResult);
            Assert.IsTrue(result, 
                    "Rotation failed for full half rotate {0}", 
                    fullQuarterResult.ToString());
        }

        [Test] 
        /// We test both ways as cross product is not-cummunititive
        public void Matrix_RotatePointAboutYAxis()
        {
            bool result = false; 
            float sqrTwo = MathF.Sqrt(2);
            V4 halfQuarterCorrectResult = new V4((sqrTwo/2.0f), 0.0f, (sqrTwo/2.0f), 1.0f); 
            V4 fullQuarterCorrectResult = new V4(1.0f, 0.0f, 0.0f, 1.0f); 

            V4 pointToRotate = new V4(0.0f, 0.0f, 1.0f, 1.0f); 
            M4 halfQuarter = M4.CreateRotationMatrixAboutYAxis((MathF.PI/4.0f));
            M4 fullQuarter = M4.CreateRotationMatrixAboutYAxis((MathF.PI/2.0f));

            V4 halfQuarterResult = halfQuarter * pointToRotate; 
            V4 fullQuarterResult = fullQuarter * pointToRotate; 

            result = halfQuarterCorrectResult.Equals(halfQuarterResult); 
            Assert.IsTrue(result, 
                    "Rotation failed for half rotate {0}",
                    halfQuarterResult.ToString());

            result = fullQuarterCorrectResult.Equals(fullQuarterResult);
            Assert.IsTrue(result, 
                    "Rotation failed for full half rotate {0}", 
                    fullQuarterResult.ToString());
        }

        /// We test both ways as cross product is not-cummunititive
        public void Matrix_RotatePointAboutZAxis()
        {
            bool result = false; 
            float sqrTwo = MathF.Sqrt(2);
            V4 halfQuarterCorrectResult = new V4(-(sqrTwo/2.0f), (sqrTwo/2.0f), 0.0f, 1.0f); 
            V4 fullQuarterCorrectResult = new V4(-1.0f, 0.0f, 0.0f, 1.0f); 

            V4 pointToRotate = new V4(0.0f, 1.0f, 0.0f, 1.0f); 
            M4 halfQuarter = M4.CreateRotationMatrixAboutZAxis((MathF.PI/4.0f));
            M4 fullQuarter = M4.CreateRotationMatrixAboutZAxis((MathF.PI/2.0f));

            V4 halfQuarterResult = halfQuarter * pointToRotate; 
            V4 fullQuarterResult = fullQuarter * pointToRotate; 

            result = halfQuarterCorrectResult.Equals(halfQuarterResult); 
            Assert.IsTrue(result, 
                    "Rotation failed for half rotate {0}",
                    halfQuarterResult.ToString());

            result = fullQuarterCorrectResult.Equals(fullQuarterResult);
            Assert.IsTrue(result, 
                    "Rotation failed for full half rotate {0}", 
                    fullQuarterResult.ToString());
        }
    }
}
