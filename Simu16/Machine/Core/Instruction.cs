using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public class Instruction
    {
        public string                Name;
        public string                Arguments;
        public byte                  Opcode;
        public byte                  Bytes;
        public Action<BusController> Execute;

        public Instruction(byte op, byte bytes, string args, string name, Action<BusController> exec)
        {
            Name      = name;
            Arguments = args;
            Opcode    = op;
            Bytes     = bytes;
            Execute   = exec;
        }

        public override string ToString()
        {
            return "Op:" + Opcode.ToString("X2") + " Bytes:" + Bytes + " Arguments:" + Arguments.PadRight(5, ' ') + " Name:" + Name;
        }

        public static readonly Instruction NOP = new Instruction(0x00, 1, "", "NOP", InstructionSet.NOP);
        public static readonly Instruction HLT = new Instruction(0xFF, 1, "", "HLT", InstructionSet.HLT);

        public static readonly Instruction ADD  = new Instruction(0x01, 4, "RW", "ADD",  InstructionSet.ADD);
        public static readonly Instruction ADDR = new Instruction(0x02, 3, "RR", "ADDR", InstructionSet.ADDR);
        public static readonly Instruction SUB  = new Instruction(0x03, 4, "RW", "SUB",  InstructionSet.SUB);
        public static readonly Instruction SUBR = new Instruction(0x04, 3, "RR", "SUBR", InstructionSet.SUBR);
        public static readonly Instruction MUL  = new Instruction(0x05, 4, "RW", "MUL",  InstructionSet.MUL);
        public static readonly Instruction MULR = new Instruction(0x06, 3, "RR", "MULR", InstructionSet.MULR);
        public static readonly Instruction DIV  = new Instruction(0x07, 4, "RW", "DIV",  InstructionSet.DIV);
        public static readonly Instruction DIVR = new Instruction(0x08, 3, "RR", "DIVR", InstructionSet.DIVR);
        public static readonly Instruction OR   = new Instruction(0x09, 4, "RW", "OR",   InstructionSet.OR);
        public static readonly Instruction ORR  = new Instruction(0x0A, 3, "RR", "ORR",  InstructionSet.ORR);
        public static readonly Instruction XOR  = new Instruction(0x0B, 4, "RW", "XOR",  InstructionSet.XOR);
        public static readonly Instruction XORR = new Instruction(0x0C, 3, "RR", "XORR", InstructionSet.XORR);
        public static readonly Instruction AND  = new Instruction(0x0D, 4, "RW", "AND",  InstructionSet.AND);
        public static readonly Instruction ANDR = new Instruction(0x0E, 3, "RR", "ANDR", InstructionSet.ANDR);
        public static readonly Instruction NOT  = new Instruction(0x0F, 2, "R",  "NOT",  InstructionSet.NOT);

        public static readonly Instruction SHL  = new Instruction(0x10, 3, "RB", "SHL",  InstructionSet.SHL);
        public static readonly Instruction SHLR = new Instruction(0x11, 3, "RR", "SHLR", InstructionSet.SHLR);
        public static readonly Instruction SHR  = new Instruction(0x12, 3, "RB", "SHR",  InstructionSet.SHR);
        public static readonly Instruction SHRR = new Instruction(0x13, 3, "RR", "SHRR", InstructionSet.SHRR);

        public static readonly Instruction OUT  = new Instruction(0x14, 4, "WR", "OUT",  InstructionSet.OUT);
        public static readonly Instruction OUTR = new Instruction(0x15, 3, "RR", "OUTR", InstructionSet.OUTR);
        public static readonly Instruction IN   = new Instruction(0x16, 4, "WR", "IN",   InstructionSet.IN);
        public static readonly Instruction INR  = new Instruction(0x17, 3, "RR", "INR",  InstructionSet.INR);

        public static readonly Instruction PUSH  = new Instruction(0x18, 3, "W", "PUSH",  InstructionSet.PUSH);
        public static readonly Instruction PUSHR = new Instruction(0x19, 2, "R", "PUSHR", InstructionSet.PUSHR);
        public static readonly Instruction PUSHA = new Instruction(0x1A, 1, "",  "PUSHA", InstructionSet.PUSHA);
        public static readonly Instruction POP   = new Instruction(0x1B, 2, "R", "POP",   InstructionSet.POP);
        public static readonly Instruction POPA  = new Instruction(0x1C, 1, "",  "POPA",  InstructionSet.POPA);

        public static readonly Instruction JP   = new Instruction(0x1D, 3, "W", "JP",   InstructionSet.JP);
        public static readonly Instruction JPR  = new Instruction(0x1E, 2, "R", "JPR",  InstructionSet.JPR);
        public static readonly Instruction JPZ  = new Instruction(0x1F, 3, "W", "JPZ",  InstructionSet.JPZ);
        public static readonly Instruction JPZR = new Instruction(0x20, 2, "R", "JPZR", InstructionSet.JPZR);
        public static readonly Instruction JS   = new Instruction(0x21, 3, "W", "JS",   InstructionSet.JS);
        public static readonly Instruction JSR  = new Instruction(0x22, 2, "R", "JSR",  InstructionSet.JSR);
        public static readonly Instruction JSZ  = new Instruction(0x23, 3, "W", "JSZ",  InstructionSet.JSZ);
        public static readonly Instruction JSZR = new Instruction(0x24, 2, "R", "JSZR", InstructionSet.JSZR);
        public static readonly Instruction RET  = new Instruction(0x25, 1, "",  "RET",  InstructionSet.RET);

        public static readonly Instruction SE   = new Instruction(0x26, 4, "RW", "SE",   InstructionSet.SE);
        public static readonly Instruction SER  = new Instruction(0x27, 3, "RR", "SER",  InstructionSet.SER);
        public static readonly Instruction SNE  = new Instruction(0x28, 4, "RW", "SNE",  InstructionSet.SNE);
        public static readonly Instruction SNER = new Instruction(0x29, 3, "RR", "SNER", InstructionSet.SNER);
        public static readonly Instruction SGT  = new Instruction(0x2A, 4, "RW", "SGT",  InstructionSet.SGT);
        public static readonly Instruction SGTR = new Instruction(0x2B, 3, "RR", "SGTR", InstructionSet.SGTR);
        public static readonly Instruction SLT  = new Instruction(0x2C, 4, "RW", "SLT",  InstructionSet.SLT);
        public static readonly Instruction SLTR = new Instruction(0x2D, 3, "RR", "SLTR", InstructionSet.SLTR);
        public static readonly Instruction SC   = new Instruction(0x2E, 2, "B",  "SC",   InstructionSet.SC);
        public static readonly Instruction SZ   = new Instruction(0x2F, 2, "B",  "SZ",   InstructionSet.SZ);
        public static readonly Instruction SN   = new Instruction(0x30, 2, "B",  "SN",   InstructionSet.SN);

        public static readonly Instruction LD   = new Instruction(0x31, 4, "RW", "LD",   InstructionSet.LD);
        public static readonly Instruction LDR  = new Instruction(0x32, 3, "RR", "LDR",  InstructionSet.LDR);
        public static readonly Instruction LDB  = new Instruction(0x33, 4, "RW", "LDB",  InstructionSet.LDB);
        public static readonly Instruction LDW  = new Instruction(0x34, 4, "RW", "LDW",  InstructionSet.LDW);
        public static readonly Instruction LDRB = new Instruction(0x35, 3, "RR", "LDRB", InstructionSet.LDRB);
        public static readonly Instruction LDRW = new Instruction(0x36, 3, "RR", "LDRW", InstructionSet.LDRW);
        public static readonly Instruction LDSP = new Instruction(0x37, 4, "RW", "LDSP", InstructionSet.LDSP);

        public static readonly Instruction STB  = new Instruction(0x38, 4, "WB", "STB",  InstructionSet.STB);
        public static readonly Instruction STW  = new Instruction(0x39, 5, "WW", "STW",  InstructionSet.STW);
        public static readonly Instruction STRB = new Instruction(0x3A, 4, "WR", "STRB", InstructionSet.STRB);
        public static readonly Instruction STRW = new Instruction(0x3B, 4, "WR", "STRW", InstructionSet.STRW);

        public static readonly Instruction WRB  = new Instruction(0x3C, 3, "RB", "WRB",  InstructionSet.WRB);
        public static readonly Instruction WRW  = new Instruction(0x3D, 4, "RW", "WRW",  InstructionSet.WRW);
        public static readonly Instruction WRRB = new Instruction(0x3E, 3, "RR", "WRRB", InstructionSet.WRRB);
        public static readonly Instruction WRRW = new Instruction(0x3F, 3, "RR", "WRRW", InstructionSet.WRRW);

        public static readonly Instruction CPY  = new Instruction(0x40, 4, "RRR", "CPY",  InstructionSet.CPY);
        public static readonly Instruction FILB = new Instruction(0x41, 4, "RRR", "FILB", InstructionSet.FILB);
        public static readonly Instruction FILW = new Instruction(0x42, 4, "RRR", "FILW", InstructionSet.FILW);

        public static readonly Instruction INT  = new Instruction(0x43, 2, "B", "INT",  InstructionSet.INT);
        public static readonly Instruction INTR = new Instruction(0x44, 2, "R", "INTR", InstructionSet.INTR);
        public static readonly Instruction IRET = new Instruction(0x45, 1, "",  "IRET", InstructionSet.IRET);

        public static readonly Instruction[] List =
        {
            NOP, HLT,
            ADD, ADDR, SUB, SUBR, MUL, MULR, DIV, DIVR,
            OR, ORR, XOR, XORR, AND, ANDR, NOT,
            SHL, SHLR, SHR, SHRR,
            OUT, OUTR, IN, INR,
            PUSH, PUSHR, PUSHA, POP, POPA,
            JP, JPR, JPZ, JPZR, JS, JSR, JSZ, JSZR, RET,
            SE, SER, SNE, SNER, SGT, SGTR, SLT, SLTR, SC, SZ, SN,
            LD, LDR, LDB, LDW, LDRB, LDRW, LDSP,
            STB, STW, STRB, STRW, WRB, WRW, WRRB, WRRW,
            CPY, FILB, FILW,
            INT, INTR, IRET,
        };

        public static Instruction? FromOpcode(byte opcode)
        {
            foreach (Instruction instr in List)
            {
                if (instr.Opcode == opcode) { return instr; }
            }
            return null;
        }

        public static Instruction? FromName(string name)
        {
            foreach (Instruction instr in List)
            {
                if (instr.Name.ToUpper() == name.ToUpper()) { return instr; }
            }
            return null;
        }
    }
}
