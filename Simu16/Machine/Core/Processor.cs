using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public enum Interrupt : byte
    {
        Keyboard,
        Timer,
        Count,
    }

    public class Processor
    {
        public BusController         Bus;
        public Registers             Registers;
        public int[]                 InterruptRequests;
        public byte                  CurrentInterrupt;
        public int                   Cycles;

        public Processor(BusController bus)
        {
            Bus               = bus;
            Registers         = new Registers(bus);
            InterruptRequests = new int[256];
            Reset();
        }

        public void Reset()
        {
            Bus.SDC.Disk.Save();

            Cycles = 0;
            Registers.Reset();
            Registers.IP = (ushort)Bus.BIOS.Address;
            Registers.SP = 0xE000;
            Registers.BP = 0xE000;

            CurrentInterrupt = 0xFF;
            Array.Fill(InterruptRequests, 0);

            Bus.RAM.Reset();
            Bus.VGA.Reset();
        }

        public void Step(int cycles)
        {
            Cycles = cycles;
            while (Cycles > 0)
            {
                if (Registers.GetExecutionFlag(ExecutionFlag.Halted)) { continue; }
                byte op = FetchByte(), valid = 0;
                foreach (Instruction instr in Instruction.List)
                {
                    if (instr.Opcode == op)
                    {
                        instr.Execute(Bus);
                        valid = 1;
                        break;
                    }
                }

                bool ints = Registers.GetExecutionFlag(ExecutionFlag.IrqEnable);
                int  idt  = Registers.GetControlFlag(ControlFlag.IDTPage);

                if (ints && idt > 0)
                {
                    for (int i = 0; i < InterruptRequests.Length; i++)
                    {
                        if (InterruptRequests[i] < 0) { InterruptRequests[i] = 0; }
                        if (InterruptRequests[i] > 0)
                        {
                            if (CurrentInterrupt == 0xFF)
                            {
                                int offset   = (idt * 256) + (i * 2);
                                int callback = Bus.ReadWord(offset);
                                int ip       = Registers.IP;

                                StackPush(Registers.IP);
                                Registers.IP = (ushort)callback;
                                CurrentInterrupt = (byte)i;
                                Debug.Log("Calling routine for interrupt" + i.ToString("X2") + "(" + ((Interrupt)i).ToString() + ") - Callback:" + callback.ToString("X4") + " IP:" + ip.ToString("X4"));
                            }
                        }
                    }
                }
                    
                if (valid > 0) { continue; }
                Debug.Error("Invalid opcode " + op.ToString("X2"));
            }
        }

        public void InvokeInterrupt(Interrupt irq)
        {
            if (irq >= Interrupt.Count) { Debug.Warning("Interrupt routine for " + ((byte)irq).ToString("X2") + " is null"); return; }
            Bus.CPU.InterruptRequests[(int)irq]++;
        }

        public byte FetchByte()
        {
            byte value = Bus.ReadByte(Registers.IP);
            Registers.IP += 1;
            Cycles       -= 1;
            return value;
        }

        public ushort FetchWord()
        {
            ushort value = Bus.ReadWord(Registers.IP);
            Registers.IP += 2;
            Cycles       -= 2;
            return value;
        }

        public void StackPush(ushort value)
        {
            Bus.WriteWord(Registers.SP, value);
            Registers.SP += 2;
        }

        public ushort StackPop()
        {
            Registers.SP -= 2;
            return Bus.ReadWord(Registers.SP);
        }
    }
}
