using System;

namespace Calc.Core.MathExpressionsParser.Abstraction
{
    public interface IParser<out TOut>
    {
        TOut Parse(string input);
    }
}
