using System;
using System.Text;

namespace Logic.Task1
{
    internal sealed class Notation
    {
        #region Pablic methods
        /// <summary>
        /// Initialize Base and Alphabet
        /// </summary>
        /// <param name="base"> Scale of notation </param>
        public Notation(int @base = 2)
        {
            if (@base < 2 || @base > 16)
            {
                throw new ArgumentException("Scale of notation must be in range [2, 16].");
            }

            Base = @base;

            Alphabet = GenerateAlphabet();
        }

        /// <summary>
        /// Scale of notation
        /// </summary>
        public int Base { get; }

        /// <summary>
        /// Alphabet for current notation
        /// </summary>
        public string Alphabet { get; }
        #endregion

        #region Private methods
        private string GenerateAlphabet()
        {
            var stringBuilder = new StringBuilder(Base);

            for (int i = 0; i < Base; i++)
            {
                stringBuilder.Append(i.ToString("X"));
            }

            return stringBuilder.ToString();
        }
        #endregion
    }
}
