using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public static class Debug
    {
        public static void Write(char c) { Console.Write(c); }

        public static void Write(string str) { Console.Write(str); }

        public static void Write(string str, ConsoleColor fg)
        {
            ConsoleColor ofg = Console.ForegroundColor;
            Console.ForegroundColor = fg;
            Console.Write(str);
            Console.ForegroundColor = ofg;
        }

        public static void Log(string str)
        {
            Write("[");
            Write("  >>  ", ConsoleColor.Cyan);
            Write("] " + str + "\n");
        }

        public static void OK(string str)
        {
            Write("[");
            Write("  OK  ", ConsoleColor.Green);
            Write("] " + str + "\n");
        }

        public static void Warning(string str)
        {
            Write("[");
            Write("  ??  ", ConsoleColor.Yellow);
            Write("] " + str + "\n");
        }

        public static void Error(string str)
        {
            Write("[");
            Write("  !!  ", ConsoleColor.Red);
            Write("] " + str + "\n");
            Console.ReadLine();
            Environment.Exit(1);
        }

        public static void DumpRegisters(BusController bus)
        {
            Write("R0:"); Write(bus.CPU.Registers.R0.ToString("X4") + " ", ConsoleColor.DarkGray);
            Write("R1:"); Write(bus.CPU.Registers.R1.ToString("X4") + " ", ConsoleColor.DarkGray);
            Write("R2:"); Write(bus.CPU.Registers.R2.ToString("X4") + " ", ConsoleColor.DarkGray);
            Write("R3:"); Write(bus.CPU.Registers.R3.ToString("X4") + " ", ConsoleColor.DarkGray);
            Write('\n');

            Write("R4:"); Write(bus.CPU.Registers.R4.ToString("X4") + " ", ConsoleColor.DarkGray);
            Write("R5:"); Write(bus.CPU.Registers.R5.ToString("X4") + " ", ConsoleColor.DarkGray);
            Write("RS:"); Write(bus.CPU.Registers.RS.ToString("X4") + " ", ConsoleColor.DarkGray);
            Write("RD:"); Write(bus.CPU.Registers.RD.ToString("X4") + " ", ConsoleColor.DarkGray);
            Write('\n');

            Write("SP:"); Write(bus.CPU.Registers.SP.ToString("X4") + " ", ConsoleColor.Yellow);
            Write("BP:"); Write(bus.CPU.Registers.BP.ToString("X4") + " ", ConsoleColor.Cyan);
            Write("IP:"); Write(bus.CPU.Registers.IP.ToString("X4") + " ", ConsoleColor.Green);
            Write("EF:"); Write(bus.CPU.Registers.EF.ToString("X4") + " ", ConsoleColor.Magenta);
            Write("C ",  bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Carry) ? ConsoleColor.Green : ConsoleColor.Red);
            Write("Z ",  bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Zero) ? ConsoleColor.Green : ConsoleColor.Red);
            Write("N ",  bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Negative) ? ConsoleColor.Green : ConsoleColor.Red);
            Write("BE ", bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.BankEnable) ? ConsoleColor.Green : ConsoleColor.Red);
            Write("BI", bus.CPU.Registers.GetBankIndex() > 0 ? ConsoleColor.Green : ConsoleColor.Red);
            Write("(");
            Write(bus.CPU.Registers.GetBankIndex().ToString(), ConsoleColor.DarkGray);
            Write(")\n");

        }
    }
}
