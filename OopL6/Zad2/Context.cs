using System;
using System.Collections.Generic;

namespace Zad2
{
    public class Context
    {
        #region Constants

        private const string EXC_MSG_FORMAT = "Variable {0} was not initialized before use";

        #endregion
        #region Private fields

        private readonly Dictionary<string, bool> _values = new Dictionary<string, bool>();

        #endregion
        #region Public methods

        public bool GetValue(string variableName)
        {
            bool value;
            if (_values.TryGetValue(variableName, out value))
            {
                return value;
            }

            throw new InvalidOperationException(string.Format(EXC_MSG_FORMAT, variableName));
        }

        public void SetValue(string variableName, bool value)
        {
            if (string.IsNullOrEmpty(variableName))
            {
                throw new ArgumentNullException();
            }
            _values[variableName] = value;
        }

        #endregion
    }
}