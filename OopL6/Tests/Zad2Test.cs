using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad2;

namespace Tests
{
    [TestClass]
    public class Zad2Test
    {
        const string TRUE_TOKEN = "true";
        const string FALSE_TOKEN = "false";

        [TestMethod]
        public void TestUnaryVar()
        {
            const string VAR_NAME = "x";

            var ctx = new Context();
            ctx.SetValue(VAR_NAME, false);

            var unary = new UnaryExpression(VAR_NAME);
            Assert.AreEqual(true, unary.Interpret(ctx));
        }

        [TestMethod]
        public void TestConst()
        {
            var ctx = new Context();

            var constExp = new ConstExpression(TRUE_TOKEN);
            Assert.AreEqual(true, constExp.Interpret(ctx));

            constExp = new ConstExpression(FALSE_TOKEN);
            Assert.AreEqual(false, constExp.Interpret(ctx));
        }

        [TestMethod]
        public void TestBinaryAndVar()
        {
            const string VAR_X = "x";
            const string VAR_Y = "y";
            const string VAR_Z = "z";

            var ctx = new Context();
            ctx.SetValue(VAR_X, false);
            ctx.SetValue(VAR_Y, true);
            ctx.SetValue(VAR_Z, true);

            var binary = new BinaryExpression(VAR_X, BinaryOp.And, VAR_Y);
            Assert.AreEqual(false, binary.Interpret(ctx));

            binary = new BinaryExpression(VAR_Y, BinaryOp.And, VAR_Z);
            Assert.AreEqual(true, binary.Interpret(ctx));
        }

        [TestMethod]
        public void TestBinaryOrVar()
        {
            const string VAR_X = "x";
            const string VAR_Y = "y";
            const string VAR_Z = "z";

            var ctx = new Context();
            ctx.SetValue(VAR_X, false);
            ctx.SetValue(VAR_Y, true);
            ctx.SetValue(VAR_Z, false);

            var binary = new BinaryExpression(VAR_X, BinaryOp.Or, VAR_Y);
            Assert.AreEqual(true, binary.Interpret(ctx));

            binary = new BinaryExpression(VAR_X, BinaryOp.Or, VAR_Z);
            Assert.AreEqual(false, binary.Interpret(ctx));
        }

         [TestMethod]
        public void TestComplexExpression()
        {
             const string VAR_X = "x";
             const string VAR_Y = "y";
             const string VAR_Z = "z";

             var ctx = new Context();
             ctx.SetValue(VAR_X, true);
             ctx.SetValue(VAR_Y, true);
             ctx.SetValue(VAR_Z, false);

             var constExp = new ConstExpression(TRUE_TOKEN);
             var unaryExp = new UnaryExpression(constExp);
             Assert.AreEqual(false, unaryExp.Interpret(ctx));

             var binaryExp =
                 new BinaryExpression(
                     new BinaryExpression(VAR_X,
                                          BinaryOp.And,
                                          unaryExp),
                     BinaryOp.Or,
                     new BinaryExpression(new UnaryExpression(VAR_Y),
                                          BinaryOp.And,
                                          VAR_Z));

             Assert.AreEqual(false, binaryExp.Interpret(ctx));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestVarNotFoundExc()
        {
            const string VAR_NAME = "x";

            var ctx = new Context();            
            var unary = new UnaryExpression(VAR_NAME);
            unary.Interpret(ctx);
        }
    }
}
