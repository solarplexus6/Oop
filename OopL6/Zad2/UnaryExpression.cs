namespace Zad2
{
    public class UnaryExpression : AbstractExpression
    {
        #region Private fields

        private readonly string _varName;
        private readonly AbstractExpression _exp = new NullExpression();

        #endregion
        #region Ctors

        public UnaryExpression(string varName)
        {
            _varName = varName;            
        }

        public UnaryExpression(AbstractExpression exp)
        {
            _exp = exp;
        }

        #endregion
        #region Overrides

        public override bool Interpret(Context context)
        {
            if (!string.IsNullOrEmpty(_varName))
            {
                return !context.GetValue(_varName);
            }

            return !_exp.Interpret(context);
        }

        #endregion
    }
}