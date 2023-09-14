using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16.Assembler
{
    public enum AST
    { 
        Invalid,
        Instruction,
        Expression,
        Constant,
        Data,
        Label,
    }

    public class ASTNode
    {
        public AST  Type;
        public int  Line;
        public bool Evaluated;
        public ASTNode(AST type, int line) { Type = type; Line = line; Evaluated = false; }
    }

    public class ASTExpression : ASTNode
    {
        public List<Token> Tokens;
        public ASTExpression(int line) : base(AST.Expression, line) { Tokens = new List<Token>(); }
        public override string ToString() { return "Type:EXPR  Size:" + Tokens.Count.ToString() + " Expr:" + ToExpressionString(); }
        public string ToExpressionString()
        {
            string expr = "{ ";
            foreach (Token tok in Tokens) { expr += tok.Value + " "; }
            if (expr.EndsWith(" ")) { expr = expr.Remove(expr.Length - 1, 1); }
            return expr + " }";
        }
    }

    public class ASTLabel : ASTNode
    {
        public Token  ID;
        public ushort Offset;
        public ASTLabel(Token tok_id, int line) : base(AST.Label, line) { ID = tok_id; }
        public override string ToString() { return "Type:LABEL Name:" + ID.Value; }
    }

    public class ASTConstant : ASTNode
    {
        public Token          ID;
        public ASTExpression? Expr;
        public ushort         Value;
        public ASTConstant(Token tok_id, ASTExpression? expr, int line) : base(AST.Constant, line) { ID = tok_id; Expr = expr; }
        public override string ToString()
        {
            return "Type:CONST Name:" + ID.Value.PadRight(20, ' ') + " Expr:" + (Expr != null ? Expr.ToExpressionString() : "");
        }
    }

    public class ASTData : ASTNode
    {
        public string              SizeStr;
        public List<ASTExpression> Exprs;
        public List<byte>          Data;
        public ushort              Offset;
        public ASTData(string sizestr, int line) : base(AST.Data, line) { SizeStr = sizestr; Exprs = new List<ASTExpression>(); Data = new List<byte>(); }
        public override string ToString()
        {
            string exprs = "";
            foreach (ASTExpression expr in Exprs) { exprs += expr.ToExpressionString() + ", "; }
            if (exprs.EndsWith(",")) { exprs = exprs.Remove(exprs.Length - 1, 1); }

            string data = "";
            for (int i = 0; i < 16; i++) { if (i < Data.Count) { data += Data[i].ToString("X2") + " "; } }
            if (data.EndsWith(" ")) { data = data.Remove(data.Length - 1, 1); }

            return "Type:DATA  Size:" + SizeStr + (Evaluated ? (" Offset:" + Offset.ToString("X4") + " Bytes:" + Data.Count + " Data:{ " + data + " }") : " Exprs:" + exprs);
        }
    }

    public class ASTInstruction: ASTNode
    {
        public Instruction?        Instruction;
        public List<ASTExpression> Exprs;
        public List<Token>         Args;
        public ASTInstruction(Instruction? instr, int line) : base(AST.Instruction, line) { Instruction = instr; Exprs = new List<ASTExpression>(); Args = new List<Token>(); }
        public override string ToString()
        {
            string exprs = "";
            foreach (ASTExpression expr in Exprs) { exprs += expr.ToExpressionString() + ", "; }
            if (exprs.EndsWith(",")) { exprs = exprs.Remove(exprs.Length - 1, 1); }
            return "Type:INSTR Mnemonic:" + (Instruction != null ? Instruction.Name : "INVALID") + " Exprs:" + exprs;
        }
    }

}
