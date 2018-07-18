using System;
using NUnit.Framework;
using Logic.Task2;

namespace Logic.Task2.Tests
{
    [TestFixture]
    public class PolynomialTests
    {
        #region Exceptions
        public void Polynomial_NullCoefficients_ThrowArgumentNullException() 
            => Assert.Throws<ArgumentException>(delegate { Polynomial polynomial = new Polynomial(null); });

        public void ArithmeticOperations_NullCoefficients_ThrowArgumentNullException()
        {
            Polynomial f = null;
            Polynomial g = new Polynomial(1, 2, 3);
            Assert.Throws<ArgumentException>(delegate { Polynomial polynomial = f + g; });
            Assert.Throws<ArgumentException>(delegate { Polynomial polynomial = f - g; });
            Assert.Throws<ArgumentException>(delegate { Polynomial polynomial = f * g; });
        }

        [TestCase(0, 1.5, 3.2, 18, 19.65)]
        [TestCase(0, 1.5, 3.2, 18, 19.65, 15)]
        public void GetByIndex_Coefficients_ThrowIndexOutOfRangeException(params double[] array)
        {
            Polynomial f = new Polynomial(array);

            Assert.Throws<ArgumentOutOfRangeException>(delegate { double value = f[array.Length + 1]; });
            Assert.Throws<ArgumentOutOfRangeException>(delegate { double value = f[-1]; });
        }
        #endregion

        #region Methods of object
        [TestCase(0.2, 1.5, 3.2)]
        [TestCase(0, 1.5, 3.2, 18, 19.65)]
        public void Equals_EqualObjects_CorrectResult(params double[] coef)
        {
            var f = new Polynomial(coef);
            var g = f;

            bool expected = true;

            bool actual = f.Equals(g);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(13.14, 13.14, 0.2, 1.5, 3.2)]
        [TestCase(0, 1.5, 3.2, 18, 19.65)]
        public void Equals_NotEqualObjects_CorrectResult(params double[] coef)
        {
            var f = new Polynomial(coef);
            var g = new Polynomial(coef[0], coef[1], coef[2]);

            bool expected = false;

            bool actual = f.Equals(g);

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Comparison operations
        [TestCase(13.14, 0.2, 1.5, 3.2)]
        [TestCase(0, 1.5, 3.2, 18, 19.65)]
        [TestCase(Double.MaxValue, 1.5, 3.2, 18, 19.65, 10, 12)]
        public void ComparisonOperation_SamePolynomial_CorrectResult(params double[] coef)
        {
            var f = new Polynomial(coef);
            var g = new Polynomial(coef);

            bool expected = true;

            bool actual = f == g;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(13.14, 0.2, 15, 4, 8, 56.7)]
        [TestCase(0, 1.5, 3.2, 18, 19.65)]
        [TestCase(100.09, 1.5, 3.2, 18, 19.65)]
        public void ComparisonOperation_DifferentPolynomialWithDifferentCoef_CorrectResult(params double[] coef)
        {
            var f = new Polynomial(coef[0], coef[1]);
            var g = new Polynomial(coef[2], coef[3]);

            bool expected = true;

            bool actual = f != g;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(13.14, 0.2, 15, 4, 8, 56.7)]
        [TestCase(0, 1.5, 3.2, 18, 19.65)]
        [TestCase(Double.MinValue, 1.5, 3.2, 18, 19.65)]
        public void ComparisonOperation_DifferentPolynomialWithDifferentCoefLength_CorrectResult(params double[] coef)
        {
            var f = new Polynomial(coef[0], coef[1], coef[2]);
            var g = new Polynomial(coef[0], coef[1]);

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
            var f = new Polynomial(coef);

            double actual = f.GetValue(value);

            Assert.AreEqual(expected, actual, 3);
        }
        #endregion

        #region Arithmetic operations
        [TestCase(0, 0, 0.2, 15, 4, 8, 56.7)]
        [TestCase(1, 5, 1, 1, 1, 1)]
        [TestCase(2, 10, 1, 1, 1, 1)]
        [TestCase(5.5, 122.1, 0, 10, 0.4)]
        public void SumOperation_VariableCoefAnotherOrder_CorrectResult(double value, double expected, params double[] coef)
        {
            var g = new Polynomial(coef[0], coef[1], coef[2]);
            var f = new Polynomial(coef[0], coef[1]);

            double actual = (f + g).GetValue(value);

            Assert.AreEqual(expected, actual, 3);
        }

        [TestCase(0, 0, 0.2, 15, 4, 8, 56.7)]
        [TestCase(0.2, -0.016, 1, 1, 1, 1)]
        [TestCase(1, -1, 1, 1, 1, 1)]
        [TestCase(-0.1, 0.0001, 122.1, 0, 10, -1)]
        public void MinusOperation_VariableCoef_CorrectResult(double value, double expected, params double[] coef)
        {
            var f = new Polynomial(value, coef[0], coef[1], coef[2]);
            var g = new Polynomial(value, coef[0], coef[1], coef[2], coef[3]);

            double actual = (f - g).GetValue(value);

            Assert.AreEqual(expected, actual, 3);
        }

        [TestCase(0, 0, 0.2, 15, 4, 8)]
        [TestCase(0.2, 0.048, 1, 1, 1, 1)]
        [TestCase(1, 2, 1, 1, 1, 1)]
        [TestCase(50, -100000, 122.1, 0, 10, -1)]
        public void MinusOperation_AnotherCoefOrder_CorrectResult(double value, double expected, params double[] coef)
        {
            var f = new Polynomial(coef[0], coef[1], coef[2], coef[3]);
            var g = new Polynomial(coef[0], coef[1]);

            double actual = (f - g).GetValue(value);

            Assert.AreEqual(expected, actual, 3);
        }

        [TestCase(0, 0, 0.2, 15, 4, 8)]
        [TestCase(2.6, 975.52, 1, 2, 3, 4)]
        [TestCase(-2.6, 341.53, 1, 2, 3, 4)]
        [TestCase(-2.6, 341.53, -1, -2, -3, -4)]
        [TestCase(-2.6, -975.52, -1, 2, -3, 4)]
        [TestCase(50, -50244200, 122.1, 0, 10, -1)]
        public void MultiplicationOperation_VariableAnotherCoefOrder_CorrectResult(double value, double expected, params double[] coef)
        {
            var f = new Polynomial(coef[0], coef[1], coef[2]);
            var g = new Polynomial(coef[1], coef[2], coef[3]);

            double actual = (f * g).GetValue(value);

            Assert.AreEqual(expected, actual, 3);
        }
        #endregion
    }
}
