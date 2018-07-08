using System;

namespace Logic.Task2
{
    public class Polynomial
    {
        #region Fields
        private double[] _coefficients;
        private double _variable;
        #endregion

        #region Public methods
        /// <summary>
        /// Initialize variable and coefficients
        /// </summary>
        /// <param name="variable"> variable of polynomial </param>
        /// <param name="coefficients"> Coefficients of polynomial (locate in order) </param>
        public Polynomial(double variable, params double[] coefficients)
        {
            _coefficients = new double[coefficients.Length];
            for (int i = 0; i < coefficients.Length; i++)
            {
                _coefficients[i] = coefficients[i];
            }

            _variable = variable;

            Value = CalculatePolinomial(_variable, _coefficients);
        }

        /// <summary>
        /// Return value of polynomial
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Overload the "+" operation for Polynomial
        /// </summary>
        /// <param name="f"> First polynomial</param>
        /// <param name="g"> Second polynomial </param>
        /// <returns> New polynomial </returns>
        public static Polynomial operator +(Polynomial f, Polynomial g)
        {
            checked
            {
                if (f._variable != g._variable)
                {
                    throw new ArgumentException("Variables of polinoms must be the same!");
                }

                double[] a, b;
                if (f._coefficients.Length >= g._coefficients.Length)
                {
                    a = f._coefficients;
                    b = g._coefficients;
                }
                else
                {
                    a = g._coefficients;
                    b = f._coefficients;
                }

                double[] c = new double[a.Length];

                int i = 0;
                for (; i < b.Length; i++)
                {
                    c[i] = a[i] + b[i];
                }

                for (; i < a.Length; i++)
                {
                    c[i] = a[i];
                }
            }

            return new Polynomial(f._variable, c);
        }

        /// <summary>
        /// Overload the "-" operation for Polynomial
        /// </summary>
        /// <param name="f"> First polynomial</param>
        /// <param name="g"> Second polynomial </param>
        /// <returns> New polynomial </returns>
        public static Polynomial operator -(Polynomial f, Polynomial g)
        {
            if (f._variable != g._variable)
            {
                throw new ArgumentException("Variables of polinoms must be the same!");
            }

            double[] a = f._coefficients;
            double[] b = g._coefficients;

            int n;
            if (a.Length > b.Length)
            {
                n = a.Length;
            }
            else
            {
                n = b.Length;
            }

            double[] c = new double[n];

            for (int i = 0; i < a.Length; i++)
            {
                c[i] = a[i];
            }

            for (int i = 0; i < b.Length; i++)
            {
                c[i] -= b[i];
            }

            return new Polynomial(f._variable, c);
        }

        /// <summary>
        /// Overload the "*" operation for Polynomial
        /// </summary>
        /// <param name="f"> First polynomial</param>
        /// <param name="g"> Second polynomial </param>
        /// <returns> New polynomial </returns>
        public static Polynomial operator *(Polynomial f, Polynomial g)
        {
            checked
            {
                if (f._variable != g._variable)
                {
                    throw new ArgumentException("Variables of polinoms must be the same!");
                }

                double[] a = f._coefficients;
                double[] b = g._coefficients;

                int n = a.Length + b.Length - 1;
                double[] c = new double[n];

                for (int i = 0; i < a.Length; i++)
                {
                    for (int j = 0; j < b.Length; j++)
                    {
                        c[i + j] += a[i] * b[j];
                    }
                }
            }
            return new Polynomial(f._variable, c);
        }

        /// <summary>
        /// Overload the "==" operation for Polynomial
        /// </summary>
        /// <param name="f"> First polynomial</param>
        /// <param name="g"> Second polynomial </param>
        /// <returns> True if polynomials are equal </returns>
        public static bool operator ==(Polynomial f, Polynomial g)
        {
            if (f._coefficients.Length != g._coefficients.Length)
            {
                return false;
            }

            if (f._variable != g._variable)
            {
                return false;
            }

            for (int i = 0; i < f._coefficients.Length; i++)
            {
                if (f._coefficients[i] != g._coefficients[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Overload the "!=" operation for Polynomial
        /// </summary>
        /// <param name="f"> First polynomial</param>
        /// <param name="g"> Second polynomial </param>
        /// <returns> True if polynomials are equal </returns>
        public static bool operator !=(Polynomial f, Polynomial g)
        {
            return !(f == g);
        }

        /// <summary>
        /// Checks this with object on equals
        /// </summary>
        /// <param name="obj"> Object for checking </param>
        /// <returns> True if objects the same </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.GetHashCode() == ((Polynomial)obj).GetHashCode();
        }

        /// <summary>
        /// Calculates hash-code for this
        /// </summary>
        /// <returns> Hash-code </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 21;

                hash = (hash * 7) + (!object.ReferenceEquals(null, _coefficients) ? _coefficients.GetHashCode() : 0);
                hash = (hash * 7) + (!object.ReferenceEquals(null, _variable) ? _variable.GetHashCode() : 0);

                return hash;
            }
        }

        /// <summary>
        /// Create string-view for polynomial
        /// </summary>
        /// <returns> Polynomial </returns>
        public override string ToString()
        {
            string result = "f(x) = " + _coefficients[0].ToString();

            for (int i = 1; i < _coefficients.Length; i++)
            {
                result += " + " + _coefficients[i] + "*x^" + i;
            }

            return Value.ToString();
        }
        #endregion

        #region Private methods
        private static double CalculatePolinomial(double variable, double[] coefficients)
        {
            double result = 0;

            checked
            {
                for (int i = 0; i < coefficients.Length; i++)
                {
                    result += coefficients[i] * Math.Pow(variable, i);
                }
            }

            return result;
        }
        #endregion
    }
}
