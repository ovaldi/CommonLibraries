#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Dynamic.Calculator.Evaluate;
using Kooboo.Dynamic.Calculator.Parser;

namespace Kooboo.Dynamic.Calculator
{
    /// <summary>
    /// 
    /// </summary>
    public static class Calculator
    {
        /// <summary>
        /// Calculates the specified expression.
        /// 计算器，计算数学表达式的值
        /// </summary>
        /// <param name="expression"> 1+2+（1*3)</param>
        /// <returns></returns>
        public static string Calculate(string expression)
        {
            Token token = new Kooboo.Dynamic.Calculator.Parser.Token(expression);
            Evaluator evaluator = new Evaluator(token);
            string value;
            string errorMsg;
            if (!evaluator.Evaluate(out value, out errorMsg))
            {
                throw new CalculateExpression(errorMsg);
            }
            return value;
        }
    }
}
