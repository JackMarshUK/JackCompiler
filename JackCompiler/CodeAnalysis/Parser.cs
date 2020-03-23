//  JackCompiler
//  Copyright © 2019 MJ Quinn Ltd. All rights reserved.

using System.Collections.Generic;

namespace JackCompiler.CodeAnalysis
{
    internal sealed class Parser
    {
        private readonly SyntaxToken[] m_tokens;
        private int m_position;
        private readonly List< string> m_diagnostics = new List<string>();

        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.NextToken();
                if(token.Kind != SyntaxKind.WhitespaceToken &&
                   token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }
            } while(token.Kind != SyntaxKind.EndOfFileToken);

            m_tokens = tokens.ToArray();
            m_diagnostics.AddRange(lexer.Diagnostics);
        }

        public IEnumerable<string> Diagnostics => m_diagnostics;

        private SyntaxToken Match(SyntaxKind kind)
        {
            if(this.Current.Kind == kind)
                return NextToken();

            m_diagnostics.Add($"ERROR: Unexpected token <{this.Current.Kind}> , expected <{kind}>");

            return  new SyntaxToken(kind, this.Current.Position, null, null);
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseTerm();
        }

        public SyntaxTree Parse()
        {
            var expression = ParseTerm();
            var endOfFileToken = Match(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(m_diagnostics, expression, endOfFileToken);
        }

        private ExpressionSyntax ParseTerm()
        {
            var left = ParseFactor();

            while(this.Current.Kind == SyntaxKind.PlusToken ||
                  this.Current.Kind == SyntaxKind.MinusToken)
            {
                var operatorToken = NextToken();
                var right = ParseFactor();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParseFactor()
        {
            var left = ParsePrimaryExpression();

            while (
                   this.Current.Kind == SyntaxKind.StarToken ||
                   this.Current.Kind == SyntaxKind.SlashToken)
            {
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {

            if(this.Current.Kind == SyntaxKind.OpenParenthesisToken)
            {
                var left = NextToken();
                var expression = ParseExpression();
                var right = Match(SyntaxKind.CloseParenthesisToken);

                return new ParenthesizedExpressionSyntax(left, expression, right);
            }

            var numberToken = Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }

        private SyntaxToken Peek(int offset)
        {
            var index = m_position + offset;

            return index >= m_tokens.Length ? m_tokens[m_tokens.Length] : m_tokens[index];
        }

        private SyntaxToken NextToken()
        {
            var current = this.Current;
            m_position++;
            return current;
        }

        private SyntaxToken Current => Peek(0);
    }
}