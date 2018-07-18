using System;

namespace Logic.Task2
{
    public sealed class Polynomial: ICloneable, IEquatable<Polynomial>
    {
        #region Fields
        private readonly double[] _coefficients = { };
        #endregion

        #region Public methods
        /// <summary>
        /// Initialize variable and coefficients
        /// </summary>
        /// <param name="variable"> variable of polynomial </param>
        /// <param name="coefficients"> Coefficients of polynomial (locate in order) </param>
        public Polynomial(params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException("Array can't be null!");
            }

            _coefficients = new double[coefficients.Length];
            coefficients.CopyTo(_coefficients, 0);

            Degree = _coefficients.Length - 1;
        }

        /// <summary>
        /// Return value of polynomial
        /// </summary>
        public int Degree { get; }

        /// <summary>
        /// Gets coefficient by index
        /// </summary>
        /// <param name="number"> Position of coefficient </param>
        /// <returns> Coefficient </returns>
        /// <exception cref="IndexOutOfRangeException"> If index is more than number of coefficients </exception>
        public double this[int number]
        {
            get
            {
                if (number > _coefficients.Length || number < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return _coefficients[number];
            }
            private set
            {
                if (number >= 0 || number < _coefficients.Length)
                {
                    _coefficients[number] = value;
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Overload the "+" operation for Polynomial
        /// </summary>
        /// <param name="left"> First polynomial</param>
        /// <param name="right"> Second polynomial </param>
        /// <returns> New polynomial </returns>
        /// <exception cref="ArgumentNullException"> If one of objects is null </exception>
        public static Polynomial operator +(Polynomial left, Polynomial right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException("Object of polynomial can't be null!");
            }

            int leftLength = left._coefficients.Length;
            int rightLength = right._coefficients.Length;

            int n = leftLength < rightLength ? rightLength : leftLength;
            double[] c = new double[n];

            left._coefficients.CopyTo(c, 0);

            for (int i = 0; i < rightLength; i++)
            {
                c[i] += right._coefficients[i];
            }

            return new Polynomial(c);
        }

        /// <summary>
        /// Add operation
        /// </summary>
        /// <param name="left"> First polynomial</param>
        /// <param name="right"> Second polynomial </param>
        /// <returns> New polynomial </returns>
        public static Polynomial Add(Polynomial left, Polynomial right) => left + right;

        /// <summary>
        /// Substruct operation
        /// </summary>
        /// <param name="left"> First polynomial</param>
        /// <param name="right"> Second polynomial </param>
        /// <returns> New polynomial </returns>
        public static Polynomial Substruct(Polynomial left, Polynomial right) => left - right;

        /// <summary>
        /// Overload the "-" operation for Polynomial
        /// </summary>
        /// <param name="left"> First polynomial</param>
        /// <param name="right"> Second polynomial </param>
        /// <returns> New polynomial </returns>
        /// <exception cref="ArgumentNullException"> If one of objects is null </exception>
        public static Polynomial operator -(Polynomial left, Polynomial right)
        {
            for (int i = 0; i < right._coefficients.Length; i++)
            {
                right._coefficients[i] *= -1; 
            }

            return left + right;
        }

        /// <summary>
        /// Overload the "*" operation for Polynomial
        /// </summary>
        /// <param name="left"> First polynomial</param>
        /// <param name="g"> Second polynomial </param>
        /// <returns> New polynomial </returns>
        /// <exception cref="ArgumentNullException"> If one of objects is null </exception>
        public static Polynomial operator *(Polynomial left, Polynomial right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException("Object of polynomial can't be null!");
            }

            double[] a = left._coefficients;
            double[] b = right._coefficients;

            int n = a.Length + b.Length - 1;
            double[] c = new double[n];
           
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    c[i + j] += a[i] * b[j];
                }
            }

            return new Polynomial(c);
        }

        /// <summary>
        /// Overload the "==" operation for Polynomial
        /// </summary>
        /// <param name="left"> First polynomial</param>
        /// <param name="right"> Second polynomial </param>
        /// <returns> True if polynomials are equal </returns>
        public static bool operator ==(Polynomial left, Polynomial right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null))
            {
                return false;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Overload the "!=" operation for Polynomial
        /// </summary>
        /// <param name="left"> First polynomial</param>
        /// <param name="right"> Second polynomial </param>
        /// <returns> True if polynomials are equal </returns>
        public static bool operator !=(Polynomial left, Polynomial right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Checks this with polynomial object on equals
        /// </summary>
        /// <param name="obj"> Object for checking </param>
        /// <returns> True if objects the same </returns>
        public bool Equals(Polynomial obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this._coefficients.Length != obj._coefficients.Length)
            {
                return false;
            }

            for (var i = 0; i < this._coefficients.Length; i++)
            {
                if (!this._coefficients[i].Equals(obj._coefficients[i]))
                {
                    return false;
                }
            }

            return true;
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

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((Polynomial)obj);
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

                foreach (var element in _coefficients)
                {
                    hash = (hash * 7) + element.GetHashCode();
                }

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

            return result.ToString();
        }

        /// <summary>
        /// Calculates value of polynomial in the point = variable
        /// </summary>
        /// <param name="variable"> Point </param>
        /// <returns> Value of polynomial in the point </returns>
        public double GetValue(double variable)
        {
            double result = 0;
                
            for (int i = 0; i < _coefficients.Length; i++)
            {
                result += _coefficients[i] * Math.Pow(variable, i);
            }

            return result;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion
    }
}
