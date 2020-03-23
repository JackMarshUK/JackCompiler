//  JackCompiler
//  Copyright © 2019 MJ Quinn Ltd. All rights reserved.

using System.Collections.Generic;

namespace JackCompiler.CodeAnalysis
{
    public sealed class BinaryExpressionSyntax: ExpressionSyntax
    {
        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
        {
            this.Left = left;
            this.OperatorToken = operatorToken;
            this.Right = right;
        }

        /// <inheritdoc />
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return this.Left;
            yield return this.OperatorToken;
            yield return this.Right;
        }

        public ExpressionSyntax Left
        {
            get;
        }

        public SyntaxToken OperatorToken
        {
            get;
        }

        public ExpressionSyntax Right
        {
            get;
        }

        /// <inheritdoc />
        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
    }
}