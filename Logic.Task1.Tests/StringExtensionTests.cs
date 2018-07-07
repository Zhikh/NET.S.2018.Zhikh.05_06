using System;
using NUnit.Framework;

namespace Logic.Task1.Tests
{
    [TestFixture]
    public class StringExtensionTests
    {
        [TestCase("0110111101100001100001010111111", 2, 934331071)]
        [TestCase("01101111011001100001010111111", 2, 233620159)]
        [TestCase("11101101111011001100001010", 2, 62370570)]
        [TestCase("764241", 8, 256161)]
        [TestCase("1AeF101", 16, 28242177)]
        [TestCase("1ACB67", 16, 1756007)]
        public void ToDecimal_String_CorrectResult(string value, int scale, int expected)
        {
            var notation = new Notation(scale);     //need singleton

            int actual = value.ToDecimal(notation);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("1AeF101", 2)]
        [TestCase("SA123", 2)]
        [TestCase("764241", 2)]
        public void ToDecimal_UncorrectParams_ThrowArgumentException(string value, int scale)
        {
            var notation = new Notation(scale);

            Assert.Throws<ArgumentException>(() => value.ToDecimal(notation));
        }

        [TestCase("11111111111111111111111111111111", 2)]
        public void ToDecimal_UncorrectParams_ThrowOverflowException(string value, int scale)
        {
            var notation = new Notation(scale);

            Assert.Throws<OverflowException>(() => value.ToDecimal(notation));
        }
    }
}
