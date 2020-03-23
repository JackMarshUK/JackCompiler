//  JackCompiler
//  Copyright © 2019 MJ Quinn Ltd. All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace JackCompiler.CodeAnalysis
{
    public sealed class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            this.Kind = kind;
            this.Position = position;
            this.Text = text;
            this.Value = value;
        }

        /// <inheritdoc />
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        public  int Position
        {
            get;
        }

        public string Text
        {
            get;
        }

        public object Value
        {
            get;
        }

        public override SyntaxKind Kind
        {
            get;
        }
    }
}