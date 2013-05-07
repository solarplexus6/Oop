using System;

namespace Zad2
{
    public class ConstExpression : AbstractExpression
    {
        #region Private fields

        private bool? _value;

        #endregion
        #region Ctors

        public ConstExpression(string token)
        {
            bool result;
            if (bool.TryParse(token, out result))
            {
                _value = result;
                return;
            }

            throw new ArgumentException();
        }

        #endregion
        #region Overrides

        public override bool Interpret(Context context)
        {
            if (_value.HasValue)
            {
                return (bool) _value;
            }

            throw new InvalidOperationException();
        }

        #endregion
    }
}