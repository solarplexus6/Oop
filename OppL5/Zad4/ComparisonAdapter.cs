using System;
using System.Collections;

namespace Zad4
{
    public class ComparisonAdapter<T> : IComparer
    {
        #region Private fields

        private readonly Comparison<T> _comparison;

        #endregion
        #region Ctors

        public ComparisonAdapter(Comparison<T> comparison)
        {
            _comparison = comparison;
        }

        #endregion
        #region IComparer Members

        public int Compare(object x, object y)
        {
            return _comparison((T) x, (T) y);
        }

        #endregion
    }
}