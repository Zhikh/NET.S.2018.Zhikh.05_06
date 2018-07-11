using System;

namespace Logic.Task1
{
    public static class StringExtension
    {
        private const int BITS_IN_BYTE = 8;

        private static Notation _notation;

        #region Public methods
        /// <summary>
        /// Converts string value into decimal value
        /// </summary>
        /// <param name="value"> Decimal of written in the p-number system </param>
        /// <returns> Decimal value </returns>
        /// <exception cref="OverflowException"> If result of calculation gives overflow </exception>
        /// <exception cref="ArgumentException"> If string value or scale of notation aren't correct </exception>
        /// <exception cref="ArgumentException"> If scale isn't in range [2, 16] </exception>
        public static int ToDecimal(this string value, int notationScale)
        {
            if (value.Length > sizeof(int) * BITS_IN_BYTE)
            {
                throw new ArgumentException(typeof(int).ToString());
            }

            _notation = new Notation(notationScale);

            return Convert(value);
        }
        #endregion

        #region Private methods
        private static int Convert(string value)
        {
            string upperValue = value.ToUpper();
            int n = upperValue.Length;
            int result = GetIndex(upperValue[n - 1]);
            int power = 1;
            try
            {
                checked
                {
                    for (int i = n - 2; i >= 0; i--)
                    {
                        power *= _notation.Base;
                        result += power * GetIndex(upperValue[i]);
                    }
                }
            }
            catch (OverflowException)
            {
                throw new ArgumentException("Value is too big!");
            }

            return result;
        }

        private static int GetIndex(char value)
        {
            int temp = _notation.Alphabet.IndexOf(value);

            if (temp == -1)
            {
                throw new ArgumentException("Value isn't correct!");
            }

            return temp;
        }
        #endregion
    }
}
