using System;

namespace Zad2
{
    public class NullExpression : AbstractExpression
    {
        public override bool Interpret(Context context)
        {
            throw new NullReferenceException();
        }
    }
}