﻿//  JackCompiler
//  Copyright © 2019 MJ Quinn Ltd. All rights reserved.

namespace JackCompiler.CodeAnalysis
{
    public enum SyntaxKind
    {
        NumberToken,
        WhitespaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        BadToken,
        EndOfFileToken,
        NumberExpression,
        BinaryExpression
    }
}