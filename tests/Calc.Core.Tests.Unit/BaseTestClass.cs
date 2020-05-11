using Calc.Core.Abstraction;
using NUnit.Framework;

namespace Calc.Core.Tests.Unit
{
    public abstract class BaseTestClass
    {
        protected ICalculate calculate;

        [OneTimeSetUp]
        public void SetUp()
        {
            calculate = new Calculate();
        }
    }
}
