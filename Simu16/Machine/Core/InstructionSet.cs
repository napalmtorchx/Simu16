using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public static class InstructionSet
    {
        private static RegisterID _REG(BusController bus)    { return (RegisterID)bus.CPU.FetchByte(); }
        private static byte       _IMM8(BusController bus)   { return bus.CPU.FetchByte(); }
        private static ushort     _IMM16(BusController bus)  { return bus.CPU.FetchWord(); }
        private static byte       _ADDR8(BusController bus)  { return bus.ReadByte(bus.CPU.FetchWord()); }
        private static ushort     _ADDR16(BusController bus) { return bus.ReadWord(bus.CPU.FetchWord()); }
        private static byte       _REL8(BusController bus)   { return bus.ReadByte(bus.CPU.Registers.Read(bus.CPU.FetchByte())); }
        private static ushort     _REL16(BusController bus)  { return bus.ReadWord(bus.CPU.Registers.Read(bus.CPU.FetchByte())); }

        private static ushort _FLAGS(BusController bus, uint sum) 
        { 
            bus.CPU.Registers.SetExecutionFlag(ExecutionFlag.Carry, sum > 0xFFFF);
            bus.CPU.Registers.SetExecutionFlag(ExecutionFlag.Zero,  sum == 0);
            return (ushort)sum;
        }

        public static void NOP(BusController bus) { }

        public static void HLT(BusController bus) { bus.CPU.Registers.SetExecutionFlag(ExecutionFlag.Halted, true); }
        
        public static void ADD(BusController bus)
        {
            RegisterID dest  = _REG(bus);
            ushort     value = _IMM16(bus);
            uint       sum   = (uint)bus.CPU.Registers.Read(dest) + value;
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void ADDR(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            uint       sum  = (uint)bus.CPU.Registers.Read(dest) + bus.CPU.Registers.Read(src);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void SUB(BusController bus)
        {
            RegisterID dest  = _REG(bus);
            ushort     value = _IMM16(bus);
            uint       sum   = (uint)bus.CPU.Registers.Read(dest) - value;
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void SUBR(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            uint       sum  = (uint)bus.CPU.Registers.Read(dest) - bus.CPU.Registers.Read(src);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void MUL(BusController bus)
        {
            RegisterID dest  = _REG(bus);
            ushort     value = _IMM16(bus);
            uint       sum   = (uint)bus.CPU.Registers.Read(dest) * value;
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void MULR(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            uint       sum  = (uint)bus.CPU.Registers.Read(dest) * bus.CPU.Registers.Read(src);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void DIV(BusController bus)
        {
            RegisterID dest  = _REG(bus);
            ushort     value = _IMM16(bus);
            uint       sum   = (uint)bus.CPU.Registers.Read(dest) / value;
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void DIVR(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            uint       sum  = (uint)bus.CPU.Registers.Read(dest) / bus.CPU.Registers.Read(src);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void OR(BusController bus)
        {
            RegisterID dest  = _REG(bus);
            ushort     value = _IMM16(bus);
            uint       sum   = (uint)bus.CPU.Registers.Read(dest) | value;
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void ORR(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            uint       sum  = (uint)bus.CPU.Registers.Read(dest) | bus.CPU.Registers.Read(src);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void XOR(BusController bus)
        {
            RegisterID dest  = _REG(bus);
            ushort     value = _IMM16(bus);
            uint       sum   = (uint)bus.CPU.Registers.Read(dest) ^ value;
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void XORR(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            uint       sum  = (uint)bus.CPU.Registers.Read(dest) ^ bus.CPU.Registers.Read(src);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void AND(BusController bus)
        {
            RegisterID dest  = _REG(bus);
            ushort     value = _IMM16(bus);
            uint       sum   = (uint)bus.CPU.Registers.Read(dest) & value;
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void ANDR(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            uint       sum  = (uint)bus.CPU.Registers.Read(dest) & bus.CPU.Registers.Read(src);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void NOT(BusController bus)
        {
            RegisterID dest = _REG(bus);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, (uint)~bus.CPU.Registers.Read(dest)));
        }

        public static void SHL(BusController bus)
        {
            RegisterID dest  = _REG(bus);
            byte       value = _IMM8(bus);
            uint       sum   = (uint)bus.CPU.Registers.Read(dest) << value;
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void SHLR(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            uint sum = (uint)((uint)bus.CPU.Registers.Read(dest) << (byte)bus.CPU.Registers.Read(src));
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void SHR(BusController bus)
        {
            RegisterID dest  = _REG(bus);
            byte       value = _IMM8(bus);
            uint       sum   = (uint)bus.CPU.Registers.Read(dest) >> value;
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void SHRR(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            uint sum = (uint)((uint)bus.CPU.Registers.Read(dest) >> (byte)bus.CPU.Registers.Read(src));
            bus.CPU.Registers.Write(dest, _FLAGS(bus, sum));
        }

        public static void OUT(BusController bus)
        {
            ushort     port = _IMM16(bus);
            RegisterID src  = _REG(bus);
            bus.WritePort(port, bus.CPU.Registers.Read(src));
        }

        public static void OUTR(BusController bus)
        {
            RegisterID port = _REG(bus);
            RegisterID src  = _REG(bus);
            bus.WritePort(bus.CPU.Registers.Read(port), bus.CPU.Registers.Read(src));
        }

        public static void IN(BusController bus)
        {
            ushort     port = _IMM16(bus);
            RegisterID dest = _REG(bus);
            bus.CPU.Registers.Write(dest, bus.ReadPort(port));
        }

        public static void INR(BusController bus)
        {
            RegisterID port = _REG(bus);
            RegisterID dest = _REG(bus);
            bus.CPU.Registers.Write(dest, bus.ReadPort(bus.CPU.Registers.Read(port)));
        }

        public static void PUSH(BusController bus)
        {
            ushort value = _IMM16(bus);
            bus.CPU.StackPush(value);
        }

        public static void PUSHR(BusController bus)
        {
            RegisterID src = _REG(bus);
            bus.CPU.StackPush(bus.CPU.Registers.Read(src));
        }

        public static void PUSHA(BusController bus)
        {
            bus.CPU.StackPush(bus.CPU.Registers.R0);
            bus.CPU.StackPush(bus.CPU.Registers.R1);
            bus.CPU.StackPush(bus.CPU.Registers.R2);
            bus.CPU.StackPush(bus.CPU.Registers.R3);
            bus.CPU.StackPush(bus.CPU.Registers.R4);
            bus.CPU.StackPush(bus.CPU.Registers.R5);
            bus.CPU.StackPush(bus.CPU.Registers.RS);
            bus.CPU.StackPush(bus.CPU.Registers.RD);
            bus.CPU.StackPush(bus.CPU.Registers.IP);
            bus.CPU.StackPush(bus.CPU.Registers.EF);
            bus.CPU.StackPush(bus.CPU.Registers.BP);
            bus.CPU.StackPush(bus.CPU.Registers.SP);
        }

        public static void POP(BusController bus)
        {
            RegisterID dest = _REG(bus);
            bus.CPU.Registers.Write(dest, bus.CPU.StackPop());
        }

        public static void POPA(BusController bus)
        {
            ushort sp = bus.CPU.StackPop();

            bus.CPU.Registers.BP = bus.CPU.StackPop();
            bus.CPU.Registers.EF = bus.CPU.StackPop();
            bus.CPU.Registers.IP = bus.CPU.StackPop();
            bus.CPU.Registers.RD = bus.CPU.StackPop();
            bus.CPU.Registers.RS = bus.CPU.StackPop();
            bus.CPU.Registers.R5 = bus.CPU.StackPop();
            bus.CPU.Registers.R4 = bus.CPU.StackPop();
            bus.CPU.Registers.R3 = bus.CPU.StackPop();
            bus.CPU.Registers.R2 = bus.CPU.StackPop();
            bus.CPU.Registers.R1 = bus.CPU.StackPop();
            bus.CPU.Registers.R0 = bus.CPU.StackPop();

            bus.CPU.Registers.SP = sp;
        }

        public static void JP(BusController bus)
        {
            ushort addr = _IMM16(bus);
            bus.CPU.Registers.IP = addr;
        }

        public static void JPR(BusController bus)
        {
            RegisterID src = _REG(bus);
            bus.CPU.Registers.IP = bus.CPU.Registers.Read(src);
        }

        public static void JPZ(BusController bus)
        {
            ushort addr = _IMM16(bus);
            if (bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Zero)) { bus.CPU.Registers.IP = addr; }
        }

        public static void JPZR(BusController bus)
        {
            RegisterID src = _REG(bus);
            if (bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Zero)) { bus.CPU.Registers.IP = bus.CPU.Registers.Read(src); }
        }

        public static void JS(BusController bus)
        {
            ushort addr = _IMM16(bus);
            bus.CPU.StackPush(bus.CPU.Registers.IP);
            bus.CPU.Registers.IP = addr;
        }

        public static void JSR(BusController bus)
        {
            RegisterID src = _REG(bus);
            bus.CPU.StackPush(bus.CPU.Registers.IP);
            bus.CPU.Registers.IP = bus.CPU.Registers.Read(src);
        }

        public static void JSZ(BusController bus)
        {
            ushort addr = _IMM16(bus);
            bus.CPU.StackPush(bus.CPU.Registers.IP);
            if (bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Zero)) { bus.CPU.Registers.IP = addr; }
        }

        public static void JSZR(BusController bus)
        {
            RegisterID src = _REG(bus);
            bus.CPU.StackPush(bus.CPU.Registers.IP);
            if (bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Zero)) { bus.CPU.Registers.IP = bus.CPU.Registers.Read(src); }
        }

        public static void RET(BusController bus)
        {
            bus.CPU.Registers.IP = bus.CPU.StackPop();
        }

        public static void SE(BusController bus)
        {
            RegisterID   reg    = _REG(bus);
            ushort       value  = _IMM16(bus);
            Instruction? instr = Instruction.FromOpcode(bus.ReadByte(bus.CPU.Registers.IP));
            if (instr == null) { Debug.Error("SE located invalid instruction"); return; }
            if (bus.CPU.Registers.Read(reg) == value) { bus.CPU.Registers.IP += instr.Bytes; }
        }

        public static void SER(BusController bus)
        {
            RegisterID   dest  = _REG(bus), src = _REG(bus);
            Instruction? instr = Instruction.FromOpcode(bus.ReadByte(bus.CPU.Registers.IP));
            if (instr == null) { Debug.Error("SER located invalid instruction"); return; }
            if (bus.CPU.Registers.Read(dest) == bus.CPU.Registers.Read(src)) { bus.CPU.Registers.IP += instr.Bytes; }
        }

        public static void SNE(BusController bus)
        {
            RegisterID   reg    = _REG(bus);
            ushort       value  = _IMM16(bus);
            Instruction? instr  = Instruction.FromOpcode(bus.ReadByte(bus.CPU.Registers.IP));
            if (instr == null) { Debug.Error("SNE located invalid instruction"); return; }
            if (bus.CPU.Registers.Read(reg) != value) { bus.CPU.Registers.IP += instr.Bytes; }
        }

        public static void SNER(BusController bus)
        {
            RegisterID   dest  = _REG(bus), src = _REG(bus);
            Instruction? instr = Instruction.FromOpcode(bus.ReadByte(bus.CPU.Registers.IP));
            if (instr == null) { Debug.Error("SNER located invalid instruction"); return; }
            if (bus.CPU.Registers.Read(dest) != bus.CPU.Registers.Read(src)) { bus.CPU.Registers.IP += instr.Bytes; }
        }

        public static void SGT(BusController bus)
        {
            RegisterID   reg    = _REG(bus);
            ushort       value  = _IMM16(bus);
            Instruction? instr  = Instruction.FromOpcode(bus.ReadByte(bus.CPU.Registers.IP));
            if (instr == null) { Debug.Error("SGT located invalid instruction"); return; }
            if (bus.CPU.Registers.Read(reg) > value) { bus.CPU.Registers.IP += instr.Bytes; }
        }

        public static void SGTR(BusController bus)
        {
            RegisterID   dest  = _REG(bus), src = _REG(bus);
            Instruction? instr = Instruction.FromOpcode(bus.ReadByte(bus.CPU.Registers.IP));
            if (instr == null) { Debug.Error("SGTR located invalid instruction"); return; }
            if (bus.CPU.Registers.Read(dest) > bus.CPU.Registers.Read(src)) { bus.CPU.Registers.IP += instr.Bytes; }
        }

        public static void SLT(BusController bus)
        {
            RegisterID   reg    = _REG(bus);
            ushort       value  = _IMM16(bus);
            Instruction? instr  = Instruction.FromOpcode(bus.ReadByte(bus.CPU.Registers.IP));
            if (instr == null) { Debug.Error("SLT located invalid instruction"); return; }
            if (bus.CPU.Registers.Read(reg) < value) { bus.CPU.Registers.IP += instr.Bytes; }
        }

        public static void SLTR(BusController bus)
        {
            RegisterID   dest  = _REG(bus), src = _REG(bus);
            Instruction? instr = Instruction.FromOpcode(bus.ReadByte(bus.CPU.Registers.IP));
            if (instr == null) { Debug.Error("SLTR located invalid instruction"); return; }
            if (bus.CPU.Registers.Read(dest) < bus.CPU.Registers.Read(src)) { bus.CPU.Registers.IP += instr.Bytes; }
        }

        public static void SC(BusController bus)
        {
            bool         set   = (_IMM8(bus) > 0 ? true : false);
            Instruction? instr = Instruction.FromOpcode(bus.ReadByte(bus.CPU.Registers.IP));
            if (instr == null) { Debug.Error("SLTR located invalid instruction"); return; }
            if (bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Carry) == set) { bus.CPU.Registers.IP += instr.Bytes; }
        }

        public static void SZ(BusController bus)
        {
            bool         set   = (_IMM8(bus) > 0 ? true : false);
            Instruction? instr = Instruction.FromOpcode(bus.ReadByte(bus.CPU.Registers.IP));
            if (instr == null) { Debug.Error("SLTR located invalid instruction"); return; }
            if (bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Zero) == set) { bus.CPU.Registers.IP += instr.Bytes; }
        }

        public static void SN(BusController bus)
        {
            bool         set   = (_IMM8(bus) > 0 ? true : false);
            Instruction? instr = Instruction.FromOpcode(bus.ReadByte(bus.CPU.Registers.IP));
            if (instr == null) { Debug.Error("SLTR located invalid instruction"); return; }
            if (bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Negative) == set) { bus.CPU.Registers.IP += instr.Bytes; }
        }

        public static void LD(BusController bus)
        {
            RegisterID dest  = _REG(bus);
            ushort     value = _IMM16(bus);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, value));
        }

        public static void LDR(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, bus.CPU.Registers.Read(src)));
        }

        public static void LDB(BusController bus)
        {
            RegisterID dest = _REG(bus);
            ushort     addr = _IMM16(bus);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, bus.ReadByte(addr)));
        }

        public static void LDW(BusController bus)
        {
            RegisterID dest = _REG(bus);
            ushort     addr = _IMM16(bus);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, bus.ReadWord(addr)));
        }

        public static void LDRB(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            ushort     addr = bus.CPU.Registers.Read(src);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, bus.ReadByte(addr)));
        }

        public static void LDRW(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            ushort     addr = bus.CPU.Registers.Read(src);
            bus.CPU.Registers.Write(dest, _FLAGS(bus, bus.ReadWord(addr)));
        }

        public static void LDSP(BusController bus)
        {
            RegisterID dest   = _REG(bus);
            int        offset = (int)_IMM16(bus);
            if (bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Negative)) { offset = -offset; }
            int addr = (int)bus.CPU.Registers.SP + offset;
            bus.CPU.Registers.Write(dest, bus.ReadWord(addr));
        }

        public static void STB(BusController bus)
        {
            ushort addr  = _IMM16(bus);
            byte   value = _IMM8(bus);
            bus.WriteByte(addr, value);
        }

        public static void STW(BusController bus)
        {
            ushort addr = _IMM16(bus), value = _IMM16(bus);
            bus.WriteWord(addr, value);
        }

        public static void STRB(BusController bus)
        {
            ushort     addr = _IMM16(bus);
            RegisterID src  = _REG(bus);
            bus.WriteByte(addr, (byte)bus.CPU.Registers.Read(src));
        }

        public static void STRW(BusController bus)
        {
            ushort     addr = _IMM16(bus);
            RegisterID src  = _REG(bus);
            bus.WriteWord(addr, bus.CPU.Registers.Read(src));
        }

        public static void WRB(BusController bus)
        {
            RegisterID dest  = _REG(bus);
            byte       value = _IMM8(bus);
            bus.WriteByte(bus.CPU.Registers.Read(dest), value);
        }

        public static void WRW(BusController bus)
        {
            RegisterID dest  = _REG(bus);
            ushort     value = _IMM16(bus);
            bus.WriteWord(bus.CPU.Registers.Read(dest), value);
        }

        public static void WRRB(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            bus.WriteByte(bus.CPU.Registers.Read(dest), (byte)bus.CPU.Registers.Read(src));
        }

        public static void WRRW(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus);
            bus.WriteWord(bus.CPU.Registers.Read(dest), bus.CPU.Registers.Read(src));
        }

        public static void CPY(BusController bus)
        {
            RegisterID dest = _REG(bus), src = _REG(bus), sz = _REG(bus);
            bus.Copy(bus.CPU.Registers.Read(dest), bus.CPU.Registers.Read(src), bus.CPU.Registers.Read(sz));
        }

        public static void FILB(BusController bus)
        {
            RegisterID dest = _REG(bus), value = _REG(bus), sz = _REG(bus);
            bus.FillByte(bus.CPU.Registers.Read(dest), (byte)bus.CPU.Registers.Read(value), bus.CPU.Registers.Read(sz));
        }

        public static void FILW(BusController bus)
        {
            RegisterID dest = _REG(bus), value = _REG(bus), sz = _REG(bus);
            bus.FillWord(bus.CPU.Registers.Read(dest), bus.CPU.Registers.Read(value), bus.CPU.Registers.Read(sz));
        }

        public static void INT(BusController bus)
        {
            if (!bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.IrqEnable) || bus.CPU.Registers.GetControlFlag(ControlFlag.IDTPage) == 0) { return; }

            byte irq = _IMM8(bus);
            bus.CPU.InvokeInterrupt((Interrupt)irq);
        }

        public static void INTR(BusController bus)
        {
            RegisterID irq = _REG(bus);
            bus.CPU.InvokeInterrupt((Interrupt)bus.CPU.Registers.Read(irq));
        }

        public static void IRET(BusController bus)
        {
            byte irq = bus.CPU.CurrentInterrupt;
            bus.CPU.InterruptRequests[bus.CPU.CurrentInterrupt]--;
            bus.CPU.CurrentInterrupt = 0xFF;
            bus.CPU.Registers.IP = bus.CPU.StackPop();
        }
    }
}
