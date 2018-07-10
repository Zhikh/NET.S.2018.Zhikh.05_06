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
            for (int i = 0; i < coefficients.Length; i++)
            {
                _coefficients[i] = coefficients[i];
            }

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
                    throw new IndexOutOfRangeException();
                }

                return _coefficients[number];
            }

            // ? set : it's really strange to work with _coefficients inside class by this[]
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

            double[] a, b;
            if (left._coefficients.Length >= right._coefficients.Length)
            {
                a = left._coefficients;
                b = right._coefficients;
            }
            else
            {
                a = right._coefficients;
                b = left._coefficients;
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

            return new Polynomial(c);
        }

        /// <summary>
        /// Overload the "-" operation for Polynomial
        /// </summary>
        /// <param name="left"> First polynomial</param>
        /// <param name="right"> Second polynomial </param>
        /// <returns> New polynomial </returns>
        /// <exception cref="ArgumentNullException"> If one of objects is null </exception>
        public static Polynomial operator -(Polynomial left, Polynomial right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException("Object of polynomial can't be null!");
            }

            double[] a = left._coefficients;
            double[] b = right._coefficients;

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

            return new Polynomial(c);
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
        /// <param name="g"> Second polynomial </param>
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

            // vs
            // return this.GetHashCode() == ((Polynomial)obj).GetHashCode();
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
            //return new this.MemberwiseClone();
            //vs

            return new Polynomial(_coefficients);
        }
        #endregion
    }
}
