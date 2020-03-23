//  JackCompiler
//  Copyright © 2019 MJ Quinn Ltd. All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace JackCompiler.CodeAnalysis
{
    public sealed class SyntaxTree
    {
        public IReadOnlyList<string> Diagnostics
        {
            get;
        }

        public ExpressionSyntax Root
        {
            get;
        }

        public SyntaxToken EndOfFileToken
        {
            get;
        }

        public SyntaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, SyntaxToken endOfFileToken)
        {
            this.Diagnostics = diagnostics.ToArray();
            this.Root = root;
            this.EndOfFileToken = endOfFileToken;
        }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}