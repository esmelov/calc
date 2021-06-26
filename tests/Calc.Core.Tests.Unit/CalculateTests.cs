using Calc.Core.Abstraction;
using NUnit.Framework;
using System;

namespace Calc.Core.Tests.Unit
{
    public class CalculateTests
    {
        private ICalculate _calculate;

        [OneTimeSetUp]
        public void Setup()
        {
            _calculate = new Calculate();
        }

        [TestCase(5.0, 3.0, 8.0)]
        [TestCase(2.2, 4.3, 6.5)]
        public void AdditionDoubleTest_ShouldBeOk(double a, double b, double expectedResult)
        {
            Assert.AreEqual(expectedResult, _calculate.Add(a, b));
        }

        [Test]
        public void SubtractionTest_Double_ShouldBeOk()
        {
            Assert.AreEqual(2.0, _calculate.Subtract(5.0, 3.0));
        }

        [TestCase(4, 2, 2)]
        [TestCase(2, 4, 0)]
        public void DivideTest_Int32_ShouldBeOk(int a, int b, int expectedResult)
        {
            Assert.AreEqual(expectedResult, _calculate.Divide(a, b));
        }

        [Test]
        public void DivideByZeroTest_Int32_ShouldBeOk()
        {
            Assert.Throws<DivideByZeroException>(() => _calculate.Divide(1, 0));
        }

        [Test]
        public void MultiplyTest_Decimal_ShouldBeOk()
        {
            Assert.AreEqual(-12m, _calculate.Multiply(3m, -4m));
        }
    }
}