using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Task1
{
    public static class StringExtension
    {
        private const int BITS_IN_BYTE = 8;

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
