using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16.Assembler
{
    public class Tokenizer
    {
        public string       Path;
        public string       Input;
        public List<Token>  Tokens;
        public List<string> IncludedFiles;

        private string _buffer;
        private int _pos, _ln;

        public Tokenizer(string path, string filename)
        {
            IncludedFiles = new List<string>();
            Tokens        = new List<Token>();
            Input         = "";
            Path          = path;

            _buffer = "";
            _pos    = 0;
            _ln     = 0;

            if (!File.Exists(filename)) { Debug.Error("Unable to locate file '" + filename + "'"); Path = ""; Input = ""; return; }
            Input = File.ReadAllText(filename);
        }

        public void Process()
        {
            _pos = 0;
            _ln = 1;

            string input = "";
            for (int i = 0; i < Input.Length; i++) 
            {
                if (Input[i] == '\t') { input += "  "; }
                else if (Input[i] != '\r' && Input[i] != '\b') { input += Input[i]; } 
            }
            Input = input;

            while (_pos < Input.Length)
            {
                char c = Next();
                if (Grammar.IsSymbol(c)) { TokenizeSymbol(c); continue; }

                switch (c)
                {
                    case '\n': { TokenizeNewline(); break; }
                    case ' ': { TokenizeBuffer(); break; }
                    case '\'': { TokenizeQuotes(false); break; }
                    case '\"': { TokenizeQuotes(true); break; }
                    case ';': { TokenizeComment(); break; }
                    default: { _buffer += c; break; }
                }
            }
            TokenizeBuffer();

            for (int i = 0; i < Tokens.Count; i++)
            {
                if (Tokens[i].Type == TokenType.Include && Tokens[i].Value == "INCLUDE")
                {
                    if (i + 1 >= Tokens.Count) { Debug.Error("Expected filename for include at line " + Tokens[i].Line); return; }

                    Token tok_fname = Tokens[i + 1];
                    if (tok_fname.Type != TokenType.LiteralString) { Debug.Error("Expected filename for include at line " + Tokens[i].Line); return; }

                    string fname = Path + tok_fname.Value;
                    if (!File.Exists(fname)) { Debug.Error("Unable to locate include file '" + fname + "'"); return; }
                    if (IncludedFiles.Contains(fname)) { Debug.Warning("Ignoring multiple include of file '" + fname + "' at line " + Tokens[i].Line); continue; }
                    Tokenizer tokenizer = new Tokenizer(Path, fname);
                    tokenizer.Process();
                    Tokens.AddRange(tokenizer.Tokens);
                    IncludedFiles.Add(fname);
                    Debug.OK("Finished including file at '" + fname + "'");
                }
            }

            ConvertNumbersToDecimal();
            Debug.OK("Finished tokenization process");
        }

        private void TokenizeBuffer()
        {
            if (_buffer.Length == 0 || _buffer == " ") { _buffer = ""; return; }

            TokenType type = TokenType.Identifier;
            string value = _buffer;

            if (Grammar.IsDecimal(_buffer)) { type = TokenType.LiteralDec; }
            else if (Grammar.IsHex(_buffer)) { type = TokenType.LiteralHex; value = value.Substring(2); }
            else if (Grammar.IsBinary(_buffer)) { type = TokenType.LiteralBin; value = value.Substring(2); }
            else if (Grammar.IsRegister(_buffer)) { type = TokenType.LiteralRegister; value = value.ToUpper(); }
            else if (Grammar.IsKeyword(_buffer)) { value = value.ToUpper(); type = DetermineKeyword(value); }
            else if (Grammar.IsInstruction(_buffer)) { type = TokenType.Instruction; value = value.ToUpper(); }
            else if (_buffer.EndsWith(":")) { type = TokenType.Label; value = value.Remove(value.Length - 1, 1); }

            Add(type, _ln, value);
            _buffer = "";
        }

        private void TokenizeQuotes(bool isstr)
        {
            TokenizeBuffer();

            string value = "";
            while (_pos < Input.Length)
            {
                char c = Next();
                if ((c == '\'' && !isstr) || (c == '\"' && isstr)) { break; }
                value += c;
            }
            Add(isstr ? TokenType.LiteralString : TokenType.LiteralChar, _ln, value);
        }

        private void TokenizeComment()
        {
            TokenizeBuffer();

            while (_pos < Input.Length)
            {
                char c = Next();
                if (c == '\n' || c == '\0') { break; }
            }
            Add(TokenType.Newline, _ln++, "NL");
        }

        private void TokenizeNewline()
        {
            TokenizeBuffer();

            if (_pos - 2 >= 0)
            {
                char last = Input[_pos - 2];
                if (last == '\n') { _ln++; return; }
            }
            Add(TokenType.Newline, _ln++, "NL");
        }

        private void TokenizeSymbol(char c)
        {
            TokenizeBuffer();
            TokenType type = TokenType.Invalid;

            switch (c)
            {
                default: { Debug.Error("Invalid symbol '" + c + "' at line " + _ln); return; }
                case '=': { type = TokenType.Assign; break; }
                case '+': { type = TokenType.Plus; break; }
                case '-': { type = TokenType.Minus; break; }
                case '*': { type = TokenType.Multiply; break; }
                case '/': { type = TokenType.Divide; break; }
                case '|': { type = TokenType.BitwiseOr; break; }
                case '^': { type = TokenType.BitwiseXOr; break; }
                case '&': { type = TokenType.BitwiseAnd; break; }
                case '~': { type = TokenType.BitwiseNot; break; }
                case '(': { type = TokenType.LParen; break; }
                case ')': { type = TokenType.RParen; break; }
                case ',': { type = TokenType.Comma; break; }
                case '<':
                    {
                        char next = Input[_pos];
                        if (next == '<') { type = TokenType.BitwiseShiftL; Next(); break; }
                        else { Debug.Error("Invalid symbol '<' at line " + _ln); return; }
                    }
                case '>':
                    {
                        char next = Input[_pos];
                        if (next == '>') { type = TokenType.BitwiseShiftR; Next(); break; }
                        else { Debug.Error("Invalid symbol '>' at line " + _ln); return; }
                    }
            }

            Add(type, _ln, c.ToString());
        }

        private TokenType DetermineKeyword(string value)
        {
            switch (value)
            {
                case "INCLUDE": { return TokenType.Include; }
                case "DEFINE":  { return TokenType.DefineConstant; }
                case "ORG":     { return TokenType.DefineOrigin; }
                case "DB":      { return TokenType.DefineByte; }
                case "DW":      { return TokenType.DefineWord; }
                case "DD":      { return TokenType.DefineDWord; }
                case "RESB":    { return TokenType.ReserveByte; }
                case "RESW":    { return TokenType.ReserveWord; }
                case "RESD":    { return TokenType.ReserveDWord; }
                default:        { return TokenType.Identifier; }
            }
        }

        private void ConvertNumbersToDecimal()
        {
            int hex = 0, bin = 0, ch = 0;
            foreach (Token tok in Tokens)
            {
                if (tok.Type == TokenType.LiteralHex)
                {
                    tok.Type  = TokenType.LiteralDec;
                    tok.Value = Convert.ToInt32(tok.Value, 16).ToString();
                    hex++;
                }
                else if (tok.Type == TokenType.LiteralBin)
                {
                    tok.Type  = TokenType.LiteralDec;
                    tok.Value = Convert.ToInt32(tok.Value, 2).ToString();
                    bin++;
                }
                else if (tok.Type == TokenType.LiteralChar)
                {
                    tok.Type  = TokenType.LiteralDec;
                    tok.Value = ((byte)tok.Value[0]).ToString();
                    ch++;
                }
            }
            Debug.OK("Converted integers to decimal - Hex:" + hex + " Binary:" + bin + " Chars:" + ch);
        }

        private char Next()
        {
            if (_pos >= Input.Length) { return (char)0; }
            char c = Input[_pos++];
            return c;
        }

        private void Add(TokenType type, int line, string value)
        {
            Token tok = new Token(type, line, value);
            Tokens.Add(tok);
            Debug.Log("Located token - " + tok.ToString());
        }
    }
}
