using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16.Assembler
{
    public enum TokenType
    {
        Invalid,
        Label,
        Identifier,
        Instruction,
        Include,
        DefineOrigin,
        DefineConstant,
        DefineByte,
        DefineWord,
        DefineDWord,
        ReserveByte,
        ReserveWord,
        ReserveDWord,
        LiteralDec,
        LiteralHex,
        LiteralBin,
        LiteralChar,
        LiteralString,
        LiteralRegister,
        Assign,
        Plus,
        Minus,
        Multiply,
        Divide,
        BitwiseOr,
        BitwiseXOr,
        BitwiseAnd,
        BitwiseNot,
        BitwiseShiftL,
        BitwiseShiftR,
        LParen,
        RParen,
        Comma,
        Newline,
    }

    public class Token
    {
        public TokenType Type;
        public int Line;
        public string Value;

        public Token() { Type = TokenType.Invalid; Line = 0; Value = ""; }
        public Token(TokenType type, int line, string value) { Type = type; Line = line; Value = value; }

        public override string ToString()
        {
            return Line.ToString().PadLeft(5, '0') + " Type:" + Type.ToString().PadRight(16, ' ') + " Value:" + Value;
        }

        public static bool IsExpressionType(TokenType type)
        {
            if (type == TokenType.Identifier) { return true; }
            if (type >= TokenType.LiteralDec && type <= TokenType.LiteralChar) { return true; }
            if (type >= TokenType.Plus && type <= TokenType.RParen) { return true; }
            return false;
        }
    }
}
