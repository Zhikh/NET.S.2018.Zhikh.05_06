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
            int result = 0;
            int[] array = new int[value.Length];

            value.ToIntArray(array);
            
            int temp = 1;
            try
            {
                for (int i = array.Length - 1; i >= 0; i--)
                {
                    result += checked(temp * array[i]);

                    temp *= _notation.Base;
                }
            }
            catch (OverflowException)
            {
                throw new ArgumentException("Value is too big!");
            }

            return result;
        }

        private static void ToIntArray(this string value, int[] array)
        {
            string upperValue = value.ToUpper();

            int i = 0;
            int position;
            foreach (var element in upperValue)
            {
                position = _notation.Alphabet.IndexOf(element);

                if (position == -1)
                {
                    throw new ArgumentException("Value can't consist means more thane scale of notation!");
                }

                array[i++] = position;
            }
        }
        #endregion
    }
}
