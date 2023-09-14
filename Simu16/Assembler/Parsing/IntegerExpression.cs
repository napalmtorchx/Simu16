using Simu16.Assembler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16.Assembler
{
    public static class IntegerExpression
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
                if (token.Type == TokenType.LiteralDec || token.Type == TokenType.Identifier) { output.Add(token); }
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

        public static uint EvaluateRPN(List<Token> rpnTokens)
        {
            Stack<uint> stack = new Stack<uint>();

            foreach (var token in rpnTokens)
            {
                if (token.Type == TokenType.LiteralDec)
                {
                    if (uint.TryParse(token.Value, out uint val)) { stack.Push(val); }
                    else { Debug.Error("Invalid decimal literal token: " + token.Value); return 0; }
                }
                else if (IsOperator(token.Type))
                {
                    if (stack.Count < 2) 
                    { 
                        Debug.Error("Insufficient operands for operator: " + token.Value); return 0; }

                    uint operand2 = stack.Pop();
                    uint operand1 = stack.Pop();
                    uint result = ApplyOperator(token.Type, operand1, operand2);
                    stack.Push(result);
                }
            }

            if (stack.Count != 1) { Debug.Error("Invalid RPN expression"); return 0; }
            return stack.Pop();
        }

        private static bool IsOperator(TokenType type)
        {
            if (type >= TokenType.Plus && type <= TokenType.BitwiseShiftR) { return true; }
            return false;
        }

        private static uint ApplyOperator(TokenType type, uint operand1, uint operand2)
        {
            switch (type)
            {
                default: { Debug.Error("Unsupported integer operator '" + type.ToString() + "'"); return 0; }
                case TokenType.Plus: { return operand1 + operand2; }
                case TokenType.Minus: { return operand1 - operand2; }
                case TokenType.Multiply: { return operand1 * operand2; }
                case TokenType.Divide: { return operand1 / operand2; }
                case TokenType.BitwiseOr: { return operand1 | operand2; }
                case TokenType.BitwiseXOr: { return operand1 ^ operand2; }
                case TokenType.BitwiseAnd: { return operand1 & operand2; }
                case TokenType.BitwiseShiftL: { return operand1 << (byte)operand2; }
                case TokenType.BitwiseShiftR: { return operand1 >> (byte)operand2; }
            }
        }
    }
}
