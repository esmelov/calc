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
    }
}
