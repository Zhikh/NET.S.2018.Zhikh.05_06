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
        [TestCase("7FFFFFFF", 16, int.MaxValue)]
        public void ToDecimal_String_CorrectResult(string value, int scale, int expected)
        {
            int actual = value.ToDecimal(scale);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("1AeF101", 2)]
        [TestCase("SA123", 2)]
        [TestCase("764241", 2)]
        [TestCase("123", 3)]
        [TestCase("H123A", 16)]
        [TestCase("111111100000000000000001111111111", 2)]
        [TestCase("11111111111111111111111111111111", 2)]
        [TestCase("764241", 1)]
        [TestCase("764241", 17)]
        public void ToDecimal_UncorrectParams_ThrowArgumentException(string value, int scale)
        {
            Assert.Throws<ArgumentException>(() => value.ToDecimal(scale));
        }
    }
}
