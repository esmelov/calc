using Calc.Core.Abstraction;
using NUnit.Framework;
using System;

namespace Calc.Core.Tests.Unit
{
    public class CalculateTests
    {
        protected ICalculate calculate;

        [OneTimeSetUp]
        public void SetUp()
        {
            calculate = new Calculate();
        }

        [Test]
        public void AdditionDoubleTest_ShouldBeOk()
        {
            Assert.AreEqual(8.0, calculate.Add(5.0, 3.0));
        }

        [Test]
        public void SubtractionDoubleTest_ShouldBeOk()
        {
            Assert.AreEqual(2.0, calculate.Subtract(5.0, 3.0));
        }

        [TestCase(4, 2, 2)]
        [TestCase(2, 4, 0)]
        public void DivideTest_ShouldBeOk(int a, int b, int expectedResult)
        {
            Assert.AreEqual(expectedResult, calculate.Divide(a, b));
        }

        [Test]
        public void DivideByZeroTest_ShouldBeOk()
        {
            Assert.Throws<DivideByZeroException>(() => calculate.Divide(1, 0));
        }

        [Test]
        public void MultiplyTest_ShouldBeOk()
        {
            Assert.AreEqual(-12m, calculate.Multiply(3m, -4m));
        }
    }
}