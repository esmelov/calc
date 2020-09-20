using Calc.Core.MathExpressions;
using Calc.Core.MathExpressions.Abstract;
using Calc.Core.MathExpressionsParser.Abstraction;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Calc.Core.MathExpressionsParser
{
    public class Parser : IParser<Func<double>>
    {
        private Parser()
        { }

        public static IParser<Func<double>> Default()
            => new Parser();

        public Func<double> Parse(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException(nameof(input));
            using var inputReader = new StringReader(input);
            return InternalParse(inputReader, new StringBuilder()).Compile();
        }

        private static Dictionary<char, int> _priority = new Dictionary<char, int>
        { ['+'] = 0, ['-'] = 0, ['*'] = 1, ['/'] = 1, [':'] = 1 };

        private static MathExpression<double> InternalParse(TextReader reader, StringBuilder accumulator)
        {
            MathExpression<double> expr = default;
            Operation operation = default;
            int peek;
            while ((peek = reader.Peek()) != -1)
            {
                switch (peek)
                {
                    case '*' when expr != default:
                        // похоже на скобки, то есть отдельное выродение
                        var tempExpr = InternalParse(reader, accumulator);
                        expr = new BinaryMathExpression<double>(expr, tempExpr, operation);
                        break;
                    case '*':
                        // похоже на скобки, то есть отдельное выродение
                        expr = Iteration(expr, accumulator, operation);
                        operation = Operation.Multiply;
                        reader.Read();
                        break;
                    case ':' when expr != default:
                    case '/' when expr != default:
                        var tempExpr1 = InternalParse(reader, accumulator);
                        expr = new BinaryMathExpression<double>(expr, tempExpr1, operation);
                        break;
                    case ':':
                    case '/':
                        expr = Iteration(expr, accumulator, operation);
                        operation = Operation.Divide;
                        reader.Read();
                        break;
                    case '+' when accumulator.Length > 0:
                        expr = Iteration(expr, accumulator, operation);
                        operation = Operation.Add;
                        reader.Read();
                        break;
                    case '-' when accumulator.Length > 0:
                        expr = Iteration(expr, accumulator, operation);
                        operation = Operation.Subtract;
                        reader.Read();
                        break;
                    case ' ':
                        reader.Read();
                        continue;
                    default:
                        accumulator.Append((char)reader.Read());
                        break;
                }
            }

            if (accumulator.Length > 0)
                expr = Iteration(expr, accumulator, operation);

            return expr;
        }

        private static MathExpression<double> Iteration(MathExpression<double> expr, StringBuilder accumulator, Operation operation)
        {
            var value = double.Parse(accumulator.ToString(), NumberStyles.Number, CultureInfo.InvariantCulture);
            accumulator.Clear();
            var tempExpr = new UnaryMathExpression<double>(value);

            return expr == default 
                ? (MathExpression<double>) tempExpr
                : new BinaryMathExpression<double>(expr, tempExpr, operation);
        }

        //private static MathExpression<double> IterationMultiply(MathExpression<double> expr, StringBuilder accumulator, Operation operation)
        //{

        //}
    }
}
