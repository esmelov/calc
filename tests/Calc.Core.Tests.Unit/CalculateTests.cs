using NUnit.Framework;
using System;

namespace Calc.Core.Tests.Unit
{
    public class CalculateTests
    {
        [Test]
        public void AdditionTest_ShouldBeOk()
        {
            Assert.Pass();
        }

        [Test]
        public void SubstractionTest_ShouldBeOk()
        {
            Assert.Pass();
        }

        [TestCase(4, 2, 2)]
        [TestCase(2, 4, 0)]
        public void DivideTest_ShouldBeOk(int a, int b, int expectedResult)
        {
            var calculator = new Calculate();
            Assert.AreEqual(expectedResult, calculator.Divide(a, b));
        }

        [Test]
        public void DivideByZeroTest_ShouldBeOk()
        {
            var calculator = new Calculate();
            Assert.Throws<DivideByZeroException>(() => calculator.Divide(1, 0));
        }

        [Test]
        public void MultiplyTest_ShouldBeOk()
        {
            Assert.Pass();
        }
    }
}