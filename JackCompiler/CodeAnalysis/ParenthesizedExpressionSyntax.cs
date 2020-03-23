//  JackCompiler
//  Copyright © 2019 MJ Quinn Ltd. All rights reserved.

using System.Collections.Generic;

namespace JackCompiler.CodeAnalysis
{
    sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public ParenthesizedExpressionSyntax(SyntaxToken openParenthesisToken, ExpressionSyntax expression, SyntaxToken closeParenthesisToken)
        {
            this.OpenParenthesisToken = openParenthesisToken;
            this.Expression = expression;
            this.CloseParenthesisToken = closeParenthesisToken;
        }

        /// <inheritdoc />
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return this.OpenParenthesisToken;
            yield return this.Expression;
            yield return this.CloseParenthesisToken;
        }

        public SyntaxToken OpenParenthesisToken
        {
            get;
        }

        public ExpressionSyntax Expression
        {
            get;
        }

        public SyntaxToken CloseParenthesisToken
        {
            get;
        }

        /// <inheritdoc />
        public override SyntaxKind Kind
        {
            get;
        }
    }
}