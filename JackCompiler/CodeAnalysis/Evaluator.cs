//  JackCompiler
//  Copyright © 2019 MJ Quinn Ltd. All rights reserved.

using System;

namespace JackCompiler.CodeAnalysis
{
    public sealed class Evaluator
    {
        private readonly ExpressionSyntax m_root;

        public Evaluator(ExpressionSyntax root)
        {
            m_root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(m_root);
        }

        private int EvaluateExpression(ExpressionSyntax node)
        {
            switch(node)
            {
                case ParenthesizedExpressionSyntax p:
                    return EvaluateExpression(p.Expression);
                case NumberExpressionSyntax n:
                    return (int)n.NumberToken.Value;
                case BinaryExpressionSyntax b:
                {
                    var left = EvaluateExpression(b.Left);
                    var right = EvaluateExpression(b.Right);

                    return b.OperatorToken.Kind switch
                    {
                        SyntaxKind.PlusToken => (left + right),
                        SyntaxKind.MinusToken => (left - right),
                        SyntaxKind.StarToken => (left * right),
                        SyntaxKind.SlashToken => (left / right),
                        _ => throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}")
                    };
                }
                default:
                    throw new Exception($"Unexpected  node {node.Kind}");
            }
        }
    }
}