using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16.Assembler
{
    public enum ExpressionType
    {
        Invalid,
        Register,
        Integer,
        String,
    }

    public static class Evaluator
    {
        public static void EvaluateLabels(List<ASTNode> nodes, uint origin)
        {
            uint ip = origin;

            foreach (ASTNode basenode in nodes)
            {
                if (basenode.Type == AST.Label)
                {
                    ASTLabel node = (ASTLabel)basenode;
                    node.Offset    = (ushort)ip;
                    node.Evaluated = true;
                    Debug.Log("Evaluated label '" + node.ID.Value + "' - Offset:" + node.Offset.ToString("X4"));
                }
                else if (basenode.Type == AST.Data)
                {
                    ASTData node = (ASTData)basenode;
                    ip += (uint)node.Data.Count;
                }
                else if (basenode.Type == AST.Instruction)
                {
                    ASTInstruction node = (ASTInstruction)basenode;
                    if (node.Instruction == null) { Debug.Error("Error evaluating instruction at line " + node.Line); return; }
                    ip += node.Instruction.Bytes;
                }
            }
        }

        public static void EvaluateConstants(List<ASTNode> nodes)
        {
            foreach (ASTNode basenode in nodes)
            {
                if (basenode.Type == AST.Constant)
                {
                    ASTConstant node = (ASTConstant)basenode;
                    if (node.Expr == null) { continue; }

                    ExpressionType type = EvaluateExpressionType(nodes, node.Expr);
                    bool           eval = false;

                    if (type == ExpressionType.Integer)
                    {
                        List<Token> expr_rpn = IntegerExpression.ConvertToRPN(node.Expr.Tokens);
                        node.Value = (ushort)IntegerExpression.EvaluateRPN(expr_rpn);
                        eval = true;
                    }
                    else if (type == ExpressionType.String)   { Debug.Error("Constant '" + node.ID.Value + "' at line " + node.Line + " cannot be a string"); return; }
                    else if (type == ExpressionType.Register) { Debug.Error("Constant '" + node.ID.Value + "' at line " + node.Line + " cannot be a register"); return; }
                    else { Debug.Error("Failed to parse expression for constant '" + node.ID.Value + "' at line " + node.Line); return; }

                    if (eval)
                    {
                        node.Evaluated = true;
                        Debug.Log("Evaluated constant '" + node.ID.Value + "' - Value:" + node.Value.ToString("X4"));
                    }
                }
            }
        }

        public static void EvaluateData(List<ASTNode> nodes)
        {
            foreach (ASTNode basenode in nodes)
            {
                if (basenode.Type == AST.Data)
                {
                    ASTData node = (ASTData)basenode;
                    if (node.SizeStr.StartsWith("RES"))
                    {
                        ExpressionType type = EvaluateExpressionType(nodes, node.Exprs[0]);
                        if (type == ExpressionType.Integer)
                        {
                            List<Token> expr_rpn = IntegerExpression.ConvertToRPN(node.Exprs[0].Tokens);
                            ushort value = (ushort)IntegerExpression.EvaluateRPN(expr_rpn);

                            if      (node.SizeStr == "RESB") { for (int i = 0; i < value; i++) { node.Data.Add(0x00); } }
                            else if (node.SizeStr == "RESW") { for (int i = 0; i < value; i++) { node.Data.AddRange(BitConverter.GetBytes((ushort)0x00)); } }
                            else if (node.SizeStr == "RESD") { for (int i = 0; i < value; i++) { node.Data.AddRange(BitConverter.GetBytes((uint)0x00)); } }
                            node.Evaluated = true;
                            Debug.Log("Evaluated reserved data - Line:" + node.Line + " Size:" + node.Data.Count);
                        }
                        else { Debug.Error("Failed to parse expression for reserved data at line " + node.Line); return; }
                    }
                    else
                    {
                        foreach (ASTExpression expr in node.Exprs)
                        {
                            if (expr == null) { continue; }

                            ExpressionType type = EvaluateExpressionType(nodes, expr);
                            bool eval = false;

                            if (type == ExpressionType.Integer)
                            {
                                List<Token> expr_rpn = IntegerExpression.ConvertToRPN(expr.Tokens);
                                ushort value = (ushort)IntegerExpression.EvaluateRPN(expr_rpn);

                                if (node.SizeStr == "DB") { node.Data.Add((byte)value); }
                                else if (node.SizeStr == "DW") { node.Data.AddRange(BitConverter.GetBytes(value)); }
                                else if (node.SizeStr == "DD") { node.Data.AddRange(BitConverter.GetBytes((uint)value)); }
                                eval = true;
                            }
                            else if (type == ExpressionType.String)
                            {
                                List<Token> expr_rpn = StringExpression.ConvertToRPN(expr.Tokens);
                                string value = StringExpression.EvaluateRPN(expr_rpn);
                                List<byte> data = new List<byte>();
                                foreach (char c in value)
                                {
                                    if (node.SizeStr == "DB") { data.Add((byte)c); }
                                    else if (node.SizeStr == "DW") { data.AddRange(BitConverter.GetBytes((ushort)c)); }
                                    else if (node.SizeStr == "DD") { data.AddRange(BitConverter.GetBytes((uint)c)); }
                                }
                                node.Data.AddRange(data);
                            }
                            else if (type == ExpressionType.Register) { Debug.Error("Data at line " + node.Line + " cannot be a register"); return; }
                            else { Debug.Error("Failed to parse expression for data at line " + node.Line); return; }

                            if (eval)
                            {
                                node.Evaluated = true;
                                Debug.Log("Evaluated data - Line:" + node.Line + " Size:" + node.Data.Count);
                            }
                        }
                    }
                }
            }
        }

        public static void EvaluateInstructions(List<ASTNode> nodes)
        {
            foreach (ASTNode basenode in nodes)
            {
                if (basenode.Type == AST.Instruction)
                {
                    ASTInstruction node = (ASTInstruction)basenode;
                    foreach (ASTExpression expr in node.Exprs)
                    {
                        if (expr == null) { continue; }

                        ExpressionType type = EvaluateExpressionType(nodes, expr);
                        bool           eval = false;

                        if (type == ExpressionType.Integer)
                        {
                            List<Token> expr_rpn = IntegerExpression.ConvertToRPN(expr.Tokens);
                            ushort value = (ushort)IntegerExpression.EvaluateRPN(expr_rpn);
                            node.Args.Add(new Token(TokenType.LiteralDec, node.Line, value.ToString()));
                            eval = true;
                        }
                        else if (type == ExpressionType.Register)
                        {
                            byte reg = Grammar.ConvertRegister(expr.Tokens[0].Value);
                            node.Args.Add(new Token(TokenType.LiteralRegister, node.Line, reg.ToString()));
                            eval = true;
                        }
                        else if (type == ExpressionType.String) { Debug.Error("Instruction argument at line " + node.Line + " cannot be a string"); return; }
                        else 
                        { 
                            Debug.Error("Failed to parse instruction argument expression at line " + node.Line); return; }

                        if (eval)
                        {
                            node.Evaluated = true;
                            Debug.Log("Evaluated instruction - Line:" + node.Line + " Mnemonic:" + (node.Instruction != null ? node.Instruction.Name : "INVALID") + " Args:" + node.Args.Count);
                        }
                    }
                }
            }
        }

        public static void EvaluateDataOffsets(List<ASTNode> nodes, uint origin)
        {
            uint offset = origin;

            foreach (ASTNode basenode in nodes)
            {
                if (basenode.Type == AST.Data)
                {
                    ASTData node = (ASTData)basenode;
                    node.Offset = (ushort)offset;
                }
                else if (basenode.Type == AST.Data)
                {
                    ASTData node = (ASTData)basenode;
                    offset += (uint)node.Data.Count;
                }
                else if (basenode.Type == AST.Instruction)
                {
                    ASTInstruction node = (ASTInstruction)basenode;
                    if (node.Instruction == null) { Debug.Error("Error evaluating instruction at line " + node.Line); return; }
                    offset += node.Instruction.Bytes;
                }
            }
        }

        public static ExpressionType EvaluateExpressionType(List<ASTNode> nodes, ASTExpression? expr)
        {
            ExpressionType type = ExpressionType.Invalid;
            if (expr == null) { return type; }

            for (int i = 0; i < expr.Tokens.Count; i++)
            {
                Token tok = expr.Tokens[i];

                if (type == ExpressionType.Invalid)
                {
                    if      (tok.Type == TokenType.LiteralDec)      { type = ExpressionType.Integer; }
                    else if (tok.Type == TokenType.LiteralString)   { type = ExpressionType.String; }
                    else if (tok.Type == TokenType.LiteralRegister) { type = ExpressionType.Register; }             
                }

                if (tok.Type == TokenType.Identifier)
                {
                    ASTNode? node = ResolveIdentifier(nodes, tok.Value);
                    if (node == null) { Debug.Error("Unknown identifier '" + tok.Value + "' at line " + tok.Line); return ExpressionType.Invalid; }

                    if (node.Type == AST.Constant)
                    {
                        expr.Tokens[i] = new Token(TokenType.LiteralDec, tok.Line, ((ASTConstant)node).Value.ToString());
                        if (type == ExpressionType.Invalid) { type = ExpressionType.Integer; }
                    }
                    else if (node.Type == AST.Label)
                    {
                        expr.Tokens[i] = new Token(TokenType.LiteralDec, tok.Line, ((ASTLabel)node).Offset.ToString());
                        if (type == ExpressionType.Invalid) { type = ExpressionType.Integer; }
                    }
                }
            }
            return type;
        }

        public static ASTNode? ResolveIdentifier(List<ASTNode> nodes, string id)
        {
            foreach (ASTNode basenode in nodes)
            {
                if (basenode.Type == AST.Constant)
                {
                    ASTConstant node = (ASTConstant)basenode;
                    if (node.ID.Value == id)
                    {
                        if (!node.Evaluated) { Debug.Error("Attempt to access constant '" + id + "' at line " + node.Line + " which has not yet been evaluated"); return null; }
                        return node;
                    }
                }
                else if (basenode.Type == AST.Label)
                {
                    ASTLabel node = (ASTLabel)basenode;
                    if (node.ID.Value == id)
                    {
                        if (!node.Evaluated) { Debug.Error("Attempt to access label '" + id + "' at line " + node.Line + " which has not yet been evaluated"); return null; }
                        return node;
                    }
                }
            }
            return null;
        }
    }
}
