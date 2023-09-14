using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16.Assembler
{
    public static class Grammar
    {
        public static readonly string   Symbols   = "=+-*/|^&<>()~,";
        public static readonly string[] Registers = { "R0", "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9", "RA", "RB", "RC", "RS", "RD", "SP", "BP", "IP", "EF", "CF" };

        public static bool IsSymbol(char c)
        {
            if (c == 0) { return false; }
            foreach (char sym in Symbols) { if (c == sym) { return true; } }
            return false;
        }

        public static bool IsDecimal(string str)
        {
            uint num = 0;
            if (!uint.TryParse(str, out num)) { return false; }
            return true;
        }

        public static bool IsHex(string str)
        {
            if (!str.ToUpper().StartsWith("0X")) { return false; }

            uint num = 0;
            if (!uint.TryParse(str.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out num)) { return false; }
            return true;
        }

        public static bool IsBinary(string str)
        {
            if (!str.ToUpper().StartsWith("0B")) { return false; }
            str = str.Substring(2);

            foreach (char c in str) { if (c != '0' && c != '1') { return false; } }
            return true;
        }

        public static bool IsRegister(string str)
        {
            foreach (string reg in Registers) { if (str.ToUpper() == reg) { return true; } }
            return false;
        }

        public static bool IsKeyword(string str)
        {
            str = str.ToUpper();
            if (str == "DEFINE" || str == "INCLUDE" || str == "ORG") { return true; }
            if (str == "DB" || str == "DW" || str == "DD" || str == "RESB" || str == "RESW" || str == "RESD") { return true; }
            return false;
        }

        public static bool IsInstruction(string str)
        {
            foreach (Instruction instr in Instruction.List)
            {
                if (instr.Name.ToUpper() == str.ToUpper()) { return true; }
            }
            return false;
        }

        public static byte ConvertRegister(string str)
        {
            str = str.ToUpper();
            switch (str)
            {
                default: { return 0xFF; }
                case "R0": { return (byte)RegisterID.R0; }
                case "R1": { return (byte)RegisterID.R1; }
                case "R2": { return (byte)RegisterID.R2; }
                case "R3": { return (byte)RegisterID.R3; }
                case "R4": { return (byte)RegisterID.R4; }
                case "R5": { return (byte)RegisterID.R5; }
                case "R6": { return (byte)RegisterID.R6; }
                case "R7": { return (byte)RegisterID.R7; }
                case "R8": { return (byte)RegisterID.R8; }
                case "R9": { return (byte)RegisterID.R9; }
                case "RA": { return (byte)RegisterID.RA; }
                case "RB": { return (byte)RegisterID.RB; }
                case "RC": { return (byte)RegisterID.RC; }
                case "RS": { return (byte)RegisterID.RS; }
                case "RD": { return (byte)RegisterID.RD; }
                case "SP": { return (byte)RegisterID.SP; }
                case "BP": { return (byte)RegisterID.BP; }
                case "IP": { return (byte)RegisterID.IP; }
                case "EF": { return (byte)RegisterID.EF; }
                case "CF": { return (byte)RegisterID.CF; }
            }
        }
    }
}
