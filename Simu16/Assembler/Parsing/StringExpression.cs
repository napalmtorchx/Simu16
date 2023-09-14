using Simu16.Assembler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16.Assembler
{
    public static class StringExpression
    {
        public static readonly Dictionary<TokenType, int> Precedence = new Dictionary<TokenType, int>
        {
            { TokenType.Plus, 2 },
            { TokenType.Minus, 2 },
            { TokenType.Multiply, 3 },
            { TokenType.Divide, 3 },
            { TokenType.BitwiseOr, 4 },
            { TokenType.BitwiseXOr, 5 },
            { TokenType.BitwiseAnd, 6 },
        };

        public static List<Token> ConvertToRPN(List<Token> inputTokens)
        {
            List<Token> output = new List<Token>();
            Stack<Token> op_stack = new Stack<Token>();

            foreach (Token token in inputTokens)
            {
                if (token.Type == TokenType.LiteralString || token.Type == TokenType.Identifier) { output.Add(token); }
                else if (token.Type == TokenType.LiteralDec) { output.Add(new Token(TokenType.LiteralString, token.Line, token.Value)); }
                else if (Precedence.ContainsKey(token.Type))
                {
                    while (op_stack.Count > 0 && (Precedence.ContainsKey(op_stack.Peek().Type) && Precedence[token.Type] <= Precedence[op_stack.Peek().Type]))
                    {
                        output.Add(op_stack.Pop());
                    }
                    op_stack.Push(token);
                }
                else if (token.Type == TokenType.LParen) { op_stack.Push(token); }
                else if (token.Type == TokenType.RParen)
                {
                    while (op_stack.Count > 0 && op_stack.Peek().Type != TokenType.LParen) { output.Add(op_stack.Pop()); }
                    op_stack.Pop();
                }
            }

            while (op_stack.Count > 0) { output.Add(op_stack.Pop()); }
            return output;
        }

        public static string EvaluateRPN(List<Token> rpnTokens)
        {
            Stack<string> stack = new Stack<string>();

            foreach (var token in rpnTokens)
            {
                if (token.Type == TokenType.LiteralString) { stack.Push(token.Value); }
                else if (IsOperator(token.Type))
                {
                    if (stack.Count < 2) { Debug.Error("Insufficient operands for operator: " + token.Value); return ""; }

                    string operand2 = stack.Pop();
                    string operand1 = stack.Pop();
                    string result = ApplyOperator(token.Type, operand1, operand2);
                    stack.Push(result);
                }
            }

            if (stack.Count != 1) { Debug.Error("Invalid RPN expression"); return ""; }
            return stack.Pop();
        }

        private static bool IsOperator(TokenType type)
        {
            if (type == TokenType.Plus) { return true; }
            return false;
        }

        private static string ApplyOperator(TokenType type, string operand1, string operand2)
        {
            switch (type)
            {
                default: { Debug.Error("Unsupported string operator '" + type.ToString() + "'"); return ""; }
                case TokenType.Plus: { return operand1 + operand2; }
            }
        }
    }
}
