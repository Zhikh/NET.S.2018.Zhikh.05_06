using System;

namespace Logic.Task1
{
    public static class StringExtension
    {
        private const int BITS_IN_BYTE = 8;

        /// <summary>
        /// Converts string value into decimal value
        /// </summary>
        /// <param name="value"> Decimal of written in the p-number system </param>
        /// <returns> Decimal value </returns>

        /// <exception cref="OverflowException"> If result of calculation gives overflow </exception>
        /// <exception cref="ArgumentException"> If string value or scale of notation aren't correct </exception>
        /// <exception cref="ArgumentException"> If scale isn't in range [2, 16] </exception>
        public static int ToDecimal(this string value, Notation notation)
        {
            if (value.Length > sizeof(int) * BITS_IN_BYTE)
            {
                throw new OverflowException(typeof(int).ToString());
            }

            return notation.Convert(value);
        }
    }
}
