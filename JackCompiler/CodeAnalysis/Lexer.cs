//  JackCompiler
//  Copyright © 2019 MJ Quinn Ltd. All rights reserved.

using System.Collections.Generic;

namespace JackCompiler.CodeAnalysis
{
    internal class Lexer
    {
        private readonly string m_text;
        private int m_position;
        private List< string> m_diagnostics = new List<string>();

        public Lexer(string text)
        {
            this.m_text = text;
        }

        private void Next()
        {
            m_position++;
        }

        public SyntaxToken NextToken()
        {

            if (m_position >= m_text.Length)
                return new SyntaxToken(SyntaxKind.EndOfFileToken, m_position, "\0", null);

            if (char.IsDigit(this.Current))
            {
                var start = m_position;

                while (char.IsDigit(this.Current))
                {
                    Next();
                }

                var length = m_position - start;
                var text = m_text.Substring(start, length);
                if(!int.TryParse(text, out var value))
                {
                    m_diagnostics.Add($"The number {m_text} cisn't valid Int32");
                }
                
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }
            if (char.IsWhiteSpace(this.Current))
            {
                var start = m_position;

                while (char.IsWhiteSpace(this.Current))
                {
                    Next();
                }

                var length = m_position - start;
                var text = m_text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }

            switch(this.Current)
            {
                case '+':
                    return new SyntaxToken(SyntaxKind.PlusToken, m_position++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxKind.MinusToken, m_position++, "-", null);
                case '*':
                    return new SyntaxToken(SyntaxKind.StarToken, m_position++, "*", null);
                case '/':
                    return new SyntaxToken(SyntaxKind.SlashToken, m_position++, "/", null);
                case '(':
                    return new SyntaxToken(SyntaxKind.OpenParenthesisToken, m_position++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxKind.CloseParenthesisToken, m_position++, ")", null);
                default:
                    m_diagnostics.Add($"ERROR; bad character input: '{this.Current}'");
                    return new SyntaxToken(SyntaxKind.BadToken, m_position++, m_text.Substring(m_position - 1, 1), null);

            }
        }

        public IEnumerable<string> Diagnostics => m_diagnostics;

        private char Current => m_position >= m_text.Length ? '\0' : m_text[m_position];
    }
}