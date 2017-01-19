﻿using NCalc.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elektor.SignalAnalyzer
{
    public class ParameterExtractionVisitor : LogicalExpressionVisitor
    {
        public HashSet<string> Parameters = new HashSet<string>();

        public override void Visit(NCalc.Domain.Identifier function)
        {
            //Parameter - add to list
            Parameters.Add(function.Name);
        }

        public override void Visit(NCalc.Domain.UnaryExpression expression)
        {
            expression.Expression.Accept(this);
        }

        public override void Visit(NCalc.Domain.BinaryExpression expression)
        {
            //Visit left and right
            expression.LeftExpression.Accept(this);
            expression.RightExpression.Accept(this);
        }

        public override void Visit(NCalc.Domain.TernaryExpression expression)
        {
            //Visit left, right and middle
            expression.LeftExpression.Accept(this);
            expression.RightExpression.Accept(this);
            expression.MiddleExpression.Accept(this);
        }

        public override void Visit(Function function)
        {
            if (function.Expressions != null)
            {
                foreach (var expression in function.Expressions)
                {
                    expression.Accept(this);
                }
            }
        }

        public override void Visit(LogicalExpression expression)
        {
            expression.Accept(this);
        }

        public override void Visit(ValueExpression expression)
        {

        }
    }
}
