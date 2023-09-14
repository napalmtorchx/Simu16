using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16.Assembler
{
    public class Parser
    {
        public List<Token>   Tokens;
        public List<ASTNode> Nodes;
        public ushort        Origin;

        private int _pos;

        public Parser(List<Token> toks)
        {
            Tokens = new List<Token>(toks);
            Nodes  = new List<ASTNode>();
        }

        public void Process()
        {
            _pos = 0;
            while (_pos < Tokens.Count)
            {
                Token tok = Next(Tokens, ref _pos);
                if (tok.Type == TokenType.DefineConstant)
                {
                    ASTConstant? node = ParseConstant();
                    if (node != null) { AddNode(node); }
                }
                else if (tok.Type == TokenType.Instruction)
                {
                    ASTInstruction? node = ParseInstruction();
                    if (node != null) { AddNode(node); }
                }
                else if (tok.Type == TokenType.Label)
                {
                    ASTLabel? node = ParseLabel();
                    if (node != null) { AddNode(node); }
                }
                else if (tok.Type >= TokenType.DefineByte && tok.Type <= TokenType.ReserveDWord)
                {
                    ASTData? node = ParseData();
                    if (node != null) { AddNode(node); }
                }
                else if (tok.Type == TokenType.DefineOrigin)
                {
                    Token tok_org = Next(Tokens, ref _pos);
                    if (tok_org.Type != TokenType.LiteralDec) { Debug.Error("Expected literal address for origin expression at line " + tok_org.Line); return; }
                    Origin = (ushort)Convert.ToUInt32(tok_org.Value, 10);
                    Debug.Log("Located origin address: " + Origin.ToString("X4"));
                }
                else if (tok.Type == TokenType.Identifier)
                {
                    Debug.Error("Unexpected identifier '" + tok.Value + "' at line " + tok.Line);
                    return;
                }
            }

            Evaluator.EvaluateConstants(Nodes);
            Evaluator.EvaluateData(Nodes);
            Evaluator.EvaluateLabels(Nodes, Origin);
            Evaluator.EvaluateDataOffsets(Nodes, Origin);
            Evaluator.EvaluateInstructions(Nodes);

            Debug.OK("Finished generating AST nodes");
            foreach (ASTNode node in Nodes)
            {
                Debug.Log("-   " + node.ToString());
            }
        }

        private void ASSERT_IDENT(Token tok, string err) { if (tok.Type != TokenType.Identifier) { Debug.Error(err + " - Expected identifier at line " + tok.Line); } }

        private ASTInstruction? ParseInstruction()
        {
            Token          tok_instr = Tokens[_pos - 1];
            ASTInstruction node      = new ASTInstruction(Instruction.FromName(tok_instr.Value), tok_instr.Line);

            List<Token> toks = new List<Token>();
            while (_pos < Tokens.Count)
            {
                Token tok = Next(Tokens, ref _pos);
                if (tok.Type == TokenType.Newline && toks.Count == 0) { break; }
                if ((tok.Type == TokenType.Comma || tok.Type == TokenType.Newline) && toks.Count > 0)
                {
                    node.Exprs.Add(new ASTExpression(tok_instr.Line));
                    node.Exprs[node.Exprs.Count - 1].Tokens.AddRange(toks);
                    toks.Clear();
                    if (tok.Type == TokenType.Newline) { break; } else { continue; }
                }
                else if (tok.Type == TokenType.LiteralRegister) { toks.Add(tok); continue; }
                else if (Token.IsExpressionType(tok.Type) || tok.Type == TokenType.LiteralString) { toks.Add(tok); continue; }
                Debug.Error("Invalid token '" + tok.Value + "' in expression at line " + tok.Line);
            }
            return node;
        }

        private ASTLabel? ParseLabel()
        {
            Token    tok_id = Tokens[_pos - 1];
            ASTLabel node   = new ASTLabel(tok_id, tok_id.Line);
            return node;
        }

        private ASTConstant? ParseConstant()
        {
            Token tok_id = Next(Tokens, ref _pos);
            ASSERT_IDENT(tok_id, "Failed to parse constant");

            ASTExpression? expr = ParseExpression(tok_id.Line);
            if (expr == null) { Debug.Error("Failed to parse expression for constant at line " + tok_id.Line); return null; }

            ASTConstant node = new ASTConstant(tok_id, expr, tok_id.Line);
            return node;
        }

        private ASTData? ParseData()
        {
            Token   tok_sz = Tokens[_pos - 1];
            ASTData node   = new ASTData(tok_sz.Value, tok_sz.Line);

            List<Token> toks = new List<Token>();
            while (_pos < Tokens.Count)
            {
                Token tok = Next(Tokens, ref _pos);
                if ((tok.Type == TokenType.Comma || tok.Type == TokenType.Newline) && toks.Count > 0) 
                { 
                    node.Exprs.Add(new ASTExpression(tok_sz.Line)); 
                    node.Exprs[node.Exprs.Count - 1].Tokens.AddRange(toks);
                    toks.Clear();
                    if (tok.Type == TokenType.Newline) { break; } else { continue; }
                }
                else if (Token.IsExpressionType(tok.Type) || tok.Type == TokenType.LiteralString) { toks.Add(tok); continue; }
                Debug.Error("Invalid token '" + tok.Value + "' in expression at line " + tok.Line);
            }

            if (node.Exprs.Count != 1 && node.SizeStr.StartsWith("RES")) { Debug.Error("Expected size for reseved data at line " + node.Line); return null; }
            return node;
        }

        private ASTExpression? ParseExpression(int line)
        {
            ASTExpression expr = new ASTExpression(line);
            while (_pos < Tokens.Count)
            {
                Token tok = Next(Tokens, ref _pos);
                if (tok.Type == TokenType.Newline) { break; }
                if (Token.IsExpressionType(tok.Type)) { expr.Tokens.Add(tok); continue; }
                Debug.Error("Invalid token '" + tok.Value + "' in expression at line " + line);
            }

            return expr;
        }

        private Token Next(List<Token> toks, ref int pos)
        {
            if (pos < 0 || pos >= toks.Count) { return new Token(); }
            Token tok = toks[pos++];
            return tok;
        }

        private void AddNode(ASTNode node)
        {
            Nodes.Add(node);
            Debug.Log("Parsed node - " + node.ToString());
        }
    }
}
