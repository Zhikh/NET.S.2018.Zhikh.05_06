using System;
using NUnit.Framework;
using Logic.Task2;

namespace Logic.Tests.Task2
{
    [TestFixture]
    public class PolinomialTests
    {
        #region Exceptions
        [TestCase(13.14, 1.08, 0.2, 1.5, 3.2)]
        public void ArithmetocOperations_DifferentVariables_ThrowArgumentException(double firstValue, double secondValue, 
            params double[] coef)
        {
            var f = new Polynomial(firstValue, coef);
            var g = new Polynomial(secondValue, coef);

            Assert.Throws<ArgumentException>(delegate{ Polynomial polynomial = f + g; });
            Assert.Throws<ArgumentException>(delegate { Polynomial polynomial = f - g; });
            Assert.Throws<ArgumentException>(delegate { Polynomial polynomial = f * g; });
        }
        #endregion

        #region Methods of object
        [TestCase(13.14, 0.2, 1.5, 3.2)]
        [TestCase(0, 0, 1.5, 3.2, 18, 19.65)]
        public void Equals_EqualObjects_CorrectResult(double value, params double[] coef)
        {
            var f = new Polynomial(value, coef);
            var g = f;

            bool expected = true;

            bool actual = f.Equals(g);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(13.14, 13.14, 0.2, 1.5, 3.2)]
        [TestCase(0, 1.5, 3.2, 18, 19.65)]
        public void Equals_NotEqualObjects_CorrectResult(double firstValue, double secondValue, params double[] coef)
        {
            var f = new Polynomial(firstValue, coef);
            var g = new Polynomial(secondValue, coef);

            bool expected = false;

            bool actual = f.Equals(g);

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Comparison operations
        [TestCase(13.14, 0.2, 1.5, 3.2)]
        [TestCase(0, 1.5, 3.2, 18, 19.65)]
        [TestCase(Double.MaxValue, 1.5, 3.2, 18, 19.65, 10, 12)]
        public void ComparisonOperation_SamePolynomial_CorrectResult(double value, params double[] coef)
        {
            var f = new Polynomial(value, coef);
            var g = new Polynomial(value, coef);

            bool expected = true;

            bool actual = f == g;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(13.14, 0.2, 15, 4, 8, 56.7, 1.5, 3.2)]
        [TestCase(0, 1.5, 3.2, 18, 19.65)]
        [TestCase(Double.MaxValue, 1.5, 3.2, 18, 19.65, 10, 12)]
        public void ComparisonOperation_DifferentPolynomialWithDifferentVariables_CorrectResult(double firstValue, 
            double secondValue, params double[] coef)
        {
            var f = new Polynomial(firstValue, coef);
            var g = new Polynomial(secondValue, coef);

            bool expected = true;

            bool actual = f != g;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(13.14, 0.2, 15, 4, 8, 56.7)]
        [TestCase(0, 1.5, 3.2, 18, 19.65)]
        [TestCase(Double.MinValue, 1.5, 3.2, 18, 19.65)]
        public void ComparisonOperation_DifferentPolynomialWithDifferentCoef_CorrectResult(double value, params double[] coef)
        {
            var f = new Polynomial(value, coef[0], coef[1]);
            var g = new Polynomial(value, coef[2], coef[3]);

            bool expected = true;

            bool actual = f != g;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(13.14, 0.2, 15, 4, 8, 56.7)]
        [TestCase(0, 1.5, 3.2, 18, 19.65)]
        [TestCase(Double.MinValue, 1.5, 3.2, 18, 19.65)]
        public void ComparisonOperation_DifferentPolynomialWithDifferentCoefLength_CorrectResult(double value, params double[] coef)
        {
            var f = new Polynomial(value, coef[0], coef[1], coef[2]);
            var g = new Polynomial(value, coef[0], coef[1]);

            bool expected = true;

            bool actual = f != g;

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Value

        [TestCase(0, 0, 0.2, 15, 4, 8, 56.7)]
        [TestCase(1, 1, 1, 1, 1, 1)]
        [TestCase(2, 15, 1, 1, 1, 1)]
        public void Value_VariableCoef_CorrectResult(double value, double expected, params double[] coef)
        {
            var f = new Polynomial(value, coef);

            double actual = f.Value;

            Assert.AreEqual(expected, actual, 3);
        }
        #endregion

        #region Arithmetic operations
        
        #endregion
    }
}
