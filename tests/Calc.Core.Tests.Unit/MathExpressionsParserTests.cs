using Calc.Core.MathExpressionsParser;
using NUnit.Framework;

namespace Calc.Core.Tests.Unit
{
    public class MathExpressionsParserTests
    {
        [Test]
        public void Test()
        {
            var func = Parser.Default()
                .Parse("1+3*2");//("-3.5 + 2.5-1+9*1");

            var result = func();

            Assert.AreEqual(7d, result);
        }
    }
}
