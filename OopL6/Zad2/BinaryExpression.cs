using System;

namespace Zad2
{
    public class BinaryExpression : AbstractExpression
    {
        #region Enums

        private enum Scenario
        {
            VarVar,
            ExpVar,
            VarExp,
            ExpExp
        }

        #endregion
        #region Private fields

        private readonly BinaryOp? _op;
        private readonly Scenario? _scenario;

        private readonly AbstractExpression _xExp = new NullExpression();
        private readonly string _xVarName;
        private readonly AbstractExpression _yExp = new NullExpression();
        private readonly string _yVarName;

        #endregion
        #region Ctors

        public BinaryExpression(string xVarName, BinaryOp op, string yVarName)
        {
            if (string.IsNullOrEmpty(xVarName) || string.IsNullOrEmpty(yVarName))
            {
                throw new ArgumentNullException();
            }

            _xVarName = xVarName;
            _yVarName = yVarName;

            _op = op;
            _scenario = Scenario.VarVar;
        }

        public BinaryExpression(string xVarName, BinaryOp op, AbstractExpression exp)
        {
            if (string.IsNullOrEmpty(xVarName) || exp == null)
            {
                throw new ArgumentNullException();
            }

            _xVarName = xVarName;
            _yExp = exp;

            _op = op;
            _scenario = Scenario.VarExp;
        }

        public BinaryExpression(AbstractExpression exp, BinaryOp op, string yVarName)
        {
            _xExp = exp;
            _yVarName = yVarName;

            _op = op;
            _scenario = Scenario.ExpVar;
        }

        public BinaryExpression(AbstractExpression xExp, BinaryOp op, AbstractExpression yExp)
        {
            _xExp = xExp;
            _yExp = xExp;

            _op = op;
            _scenario = Scenario.ExpExp;
        }

        #endregion
        #region Overrides

        public override bool Interpret(Context context)
        {
            bool xVar;
            bool yVar;

            switch (_scenario)
            {
                case Scenario.VarVar:
                    xVar = context.GetValue(_xVarName);
                    yVar = context.GetValue(_yVarName);
                    break;
                case Scenario.VarExp:
                    xVar = context.GetValue(_xVarName);
                    yVar = _yExp.Interpret(context);
                    break;
                case Scenario.ExpVar:
                    xVar = _xExp.Interpret(context);
                    yVar = context.GetValue(_yVarName);
                    break;
                case Scenario.ExpExp:
                    xVar = _xExp.Interpret(context);
                    yVar = _yExp.Interpret(context);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            switch (_op)
            {
                case BinaryOp.And:
                    return xVar && yVar;
                case BinaryOp.Or:
                    return xVar || yVar;
                default:
                    throw new InvalidOperationException();
            }
        }

        #endregion
    }
}