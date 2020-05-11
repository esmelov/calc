using Calc.Core.MathExpressions;
using NUnit.Framework;

namespace Calc.Core.Tests.Unit
{
    public class MathExpressionsTests : BaseTestClass
    {
        [Test]
        public void UnaryMathExpressionTest_Double_ShouldBeOk()
        {
            var uExpr = new UnaryMathExpression<double>(5.0);
            var r = uExpr.Compile()();

            Assert.AreEqual(typeof(double), r.GetType());
            Assert.AreEqual(5.0, r);
        }

        [Test]
        public void BinaryMathExpression_Double_ShouldBeOk()
        {
            var expr = new BinaryMathExpression<double>(
                new UnaryMathExpression<double>(5d),
                new BinaryMathExpression<double>(
                    new UnaryMathExpression<double>(3d), new UnaryMathExpression<double>(4d), Operation.Multiply),
                Operation.Add);

            var r = expr.Compile()();

            Assert.AreEqual(typeof(double), r.GetType());
            Assert.AreEqual(17d, r);
        }

        //17−5·6:3−2+4:2
        [Test]
        public void BinaryMathExpression2_Double_ShouldBeOk()
        {
            // 5·6
            var expr1 = new BinaryMathExpression<double>(
                new UnaryMathExpression<double>(5d),
                new UnaryMathExpression<double>(6d),
                Operation.Multiply);

            // expr1 / 3
            var expr2 = new BinaryMathExpression<double>(
                expr1,
                new UnaryMathExpression<double>(3d),
                Operation.Divide);

            // 4 / 2
            var expr3 = new BinaryMathExpression<double>(
                new UnaryMathExpression<double>(4d),
                new UnaryMathExpression<double>(2d),
                Operation.Divide);

            // 17 - expr2
            var expr4 = new BinaryMathExpression<double>(
                new UnaryMathExpression<double>(17d),
                expr2,
                Operation.Subtract);

            // expr4 - 2
            var expr5 = new BinaryMathExpression<double>(
                expr4,
                new UnaryMathExpression<double>(2d),
                Operation.Subtract);

            // expr5 - expr3
            var expr6 = new BinaryMathExpression<double>(expr5, expr3, Operation.Add);

            var r = expr6.Compile()();

            Assert.AreEqual(typeof(double), r.GetType());
            Assert.AreEqual(7d, r);
        }
    }
}
