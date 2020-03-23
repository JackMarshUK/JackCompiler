//  JackCompiler
//  Copyright © 2019 MJ Quinn Ltd. All rights reserved.

using System.Collections.Generic;

namespace JackCompiler.CodeAnalysis
{
    public abstract class SyntaxNode
    {
        public abstract IEnumerable<SyntaxNode> GetChildren();

        public abstract SyntaxKind Kind
        {
            get;
        }
    }
}