using Calc.Core.MathExpressions;
using Calc.Core.MathExpressions.Extensions;
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
            var expr = new UnaryMathExpression<double>(5d).Add(
                new UnaryMathExpression<double>(3d)
                    .Multiply(new UnaryMathExpression<double>(4d)));

            var r = expr.Compile()();

            Assert.AreEqual(typeof(double), r.GetType());
            Assert.AreEqual(17d, r);
        }

        //17−5·6:3−2+4:2
        [Test]
        public void BinaryMathExpression2_Double_ShouldBeOk()
        {
            // 5·6
            var expr1 = new UnaryMathExpression<double>(5d)
                .Multiply(new UnaryMathExpression<double>(6d));

            // expr1 / 3
            var expr2 = expr1.Divide(new UnaryMathExpression<double>(3d));

            // 4 / 2
            var expr3 = new UnaryMathExpression<double>(4d)
                .Divide(new UnaryMathExpression<double>(2d));

            // 17 - expr2
            var expr4 = new UnaryMathExpression<double>(17d).Subtract(expr2);

            // expr4 - 2
            var expr5 = expr4.Subtract(new UnaryMathExpression<double>(2d));

            // expr5 - expr3
            var expr6 = expr5.Add(expr3);

            var r = expr6.Compile()();

            Assert.AreEqual(typeof(double), r.GetType());
            Assert.AreEqual(7d, r);
        }
    }
}
