using System;
using System.Linq;

namespace Logic.Task1
{
    public class Notation
    {
        #region Constants
        private const int A_POSITION = 65;
        private const int HEX_POSITION = 10;
        #endregion

        #region Fields
        private int _notationScale;
        #endregion

        #region Public and Internal methods
        /// <summary>
        /// Initialize NotationScale with value
        /// </summary>
        /// <param name="value"> Scale of notation </param>
        public Notation(int value = 2)
        {
            NotationScale = value;
        }

        /// <summary>
        /// Scale of natation
        /// </summary>
        /// <exception cref="ArgumentException"> If scale isn't in range [2, 16] </exception>
        public int NotationScale
        {
            get
            {
                return _notationScale;
            }

            set
            {
                if (value >= 2 && value <= 16)
                {
                    _notationScale = value;
                }
                else
                {
                    throw new ArgumentException("Scale of notation must be in range [2, 16].");
                }
            }
        }

        /// <summary>
        /// Converts string value into decimal value
        /// </summary>
        /// <param name="value"> Decimal of written in the p-number system </param>
        /// <returns> Decimal value </returns>
        /// <exception cref="OverflowException"> If result of calculation gives overflow </exception>
        /// <exception cref="ArgumentException"> If string value or scale of notation aren't correct </exception>
        internal int Convert(string value)
        {
            ToIntArray(value, out int[] array);

            CheckOnBase(array);

            try
            { 
                int result = 0;
                checked
                {
                    int i = 0;
                    for (var power = array.Count() - 1; power >= 0; power--)
                    {
                        result += (int)Math.Pow(NotationScale, power) * array[i++];
                    }
                }

                return result;
            }
            catch (OverflowException ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Private methods
        // Convert string value into array of interger values
        private void ToIntArray(string value, out int[] array)
        {
            array = new int[value.Length];

            int i = 0;
            foreach (var element in value)
            {
                if (char.IsNumber(element))
                {
                    array[i++] = (int)char.GetNumericValue(element);
                }
                else
                {
                    char temp = char.ToUpper(element);

                    array[i++] = (int)temp - A_POSITION + HEX_POSITION;
                }
            }
        }

        // Check array values for finding values that more than scale of notation
        private void CheckOnBase(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] >= NotationScale)
                {
                    throw new ArgumentException("Value can't consist means more thane scale of notation!");
                }
            }
        }
        #endregion
    }
}
