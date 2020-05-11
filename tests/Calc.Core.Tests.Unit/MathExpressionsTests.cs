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
            // 5·6:3
            var expr1 = new UnaryMathExpression<double>(5d)
                .Multiply(new UnaryMathExpression<double>(6d))
                .Divide(new UnaryMathExpression<double>(3d));

            // 4 / 2
            var expr2 = new UnaryMathExpression<double>(4d)
                .Divide(new UnaryMathExpression<double>(2d));

            // 17 - expr1 - 2 + expr2
            var expr3 = new UnaryMathExpression<double>(17d)
                .Subtract(expr1)
                .Subtract(new UnaryMathExpression<double>(2d))
                .Add(expr2);

            var r = expr3.Compile()();

            Assert.AreEqual(typeof(double), r.GetType());
            Assert.AreEqual(7d, r);
        }
    }
}
