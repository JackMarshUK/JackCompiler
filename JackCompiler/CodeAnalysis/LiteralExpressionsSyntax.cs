//  JackCompiler
//  Copyright © 2019 MJ Quinn Ltd. All rights reserved.

using System.Collections.Generic;

namespace JackCompiler.CodeAnalysis
{
    public sealed class NumberExpressionSyntax : ExpressionSyntax
    {
        public NumberExpressionSyntax(SyntaxToken numberToken)
        {
            this.NumberToken = numberToken;
        }

        /// <inheritdoc />
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return this.NumberToken;
        }

        public SyntaxToken NumberToken
        {
            get;
        }

        /// <inheritdoc />
        public override SyntaxKind Kind => SyntaxKind.NumberExpression;
    }
}