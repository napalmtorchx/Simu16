using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public class BusController
    {
        public List<BusComponent> Components;
        public List<IOPort>       Ports;
        public Processor          CPU;
        public VGAController      VGA;
        public BIOS               BIOS;
        public RAM                RAM;
        public UART               UART;
        public BANK               BANK;
        public Keyboard           KBD;
        public SDC                SDC;
        public int                Frequency;

        public byte[] Data;

        public BusController()
        {
            Data      = new byte[0x10000];
            Frequency = 0;

            Ports      = new List<IOPort>();
            Components = new List<BusComponent>();

            RegisterComponent(VGA  = new VGAController(this));
            RegisterComponent(BIOS = new BIOS(this));
            RegisterComponent(RAM  = new RAM(this));
            RegisterComponent(BANK = new BANK(this));
            RegisterComponent(UART = new UART(this));
            RegisterComponent(KBD  = new Keyboard(this));
            RegisterComponent(SDC  = new SDC(this));

            CPU = new Processor(this);
        }

        public void RegisterComponent(BusComponent component)
        {
            Components.Add(component);
            Debug.OK("Registered component - " + component.ToString());
        }

        public void RegisterPort(IOPort port) 
        { 
            Ports.Add(port);
            Debug.OK("Registered IO port - " + port.ToString());
        }

        public ushort ReadPort(ushort port)
        {
            foreach (IOPort p in Ports)
            {
                if (p.Port == port)
                {
                    if (p.OnRead != null) { p.OnRead(this, p); }
                    return p.Data;
                }
            }
            Debug.Error("Port read violation - Port:" + port.ToString("X4"));
            return 0;
        }

        public void WritePort(ushort port, ushort value)
        {
            foreach (IOPort p in Ports)
            {
                if (p.Port == port)
                {
                    p.Data = value;
                    if (p.OnWrite != null) { p.OnWrite(this, p); }
                    return;
                }
            }
            Debug.Error("Port write violation - Port:" + port.ToString("X4") + " Value:" + value.ToString("X4"));
        }

        public byte[] Read(int addr, int sz)
        {
            byte[] data = new byte[sz];
            for (int i = 0; i < sz; i++) { data[i] = ReadByte(addr + i); }
            return data;
        }

        public byte ReadByte(int addr)
        {
            BusComponent? comp = GetComponentAtAddress(addr, 1);
            if (comp == null) { Debug.Error("Attempt to read from unmapped memory - Address:" + addr.ToString("X4") + " Size:1"); return 0; }
            if (comp.Size == 0) { return 0; }
            if (!comp.CanRead) { Debug.Error("Memory read privilege violation - Component:" + comp.Name + " Address:" + addr.ToString("X4") + " Size:1"); return 0; }

            if ((addr >= BANK.Address && addr < BANK.Address + BANK.Size) && CPU.Registers.GetExecutionFlag(ExecutionFlag.BankEnable))
            {
                return BANK.ReadByte(CPU.Registers.GetBankIndex(), addr - BANK.Address);
            }
            return Data[addr];
        }

        public ushort ReadWord(int addr)
        {
            return (ushort)((ReadByte(addr + 1) << 8) | ReadByte(addr));
        }

        public void Write(int addr, int src_offset, int sz, byte[] src_data, bool wr_override = false)
        {
            for (int i = 0; i < sz; i++) { WriteByte(addr + i, src_data[src_offset + i], wr_override); }
        }

        public void WriteByte(int addr, byte value, bool wr_override = false)
        {
            BusComponent? comp = GetComponentAtAddress(addr, 1);
            if (comp == null) { Debug.Error("Attempt to write to unmapped memory - Address:" + addr.ToString("X4") + " Size:1"); return; }
            if (comp.Size == 0) { return; }
            if (!comp.CanWrite && !wr_override) { Debug.Error("Memory write privilege violation - Component:" + comp.Name + " Address:" + addr.ToString("X4") + " Size:1 Value:" + value.ToString("X2")); return; }

            if ((addr >= BANK.Address && addr < BANK.Address + BANK.Size) && CPU.Registers.GetExecutionFlag(ExecutionFlag.BankEnable))
            {
                BANK.WriteByte(CPU.Registers.GetBankIndex(), addr - BANK.Address, value);
            }
            else { Data[addr] = value; }
        }

        public void WriteWord(int addr, ushort value, bool wr_override = false)
        {
            WriteByte(addr + 0, (byte)((value & 0x00FF)), wr_override);
            WriteByte(addr + 1, (byte)((value & 0xFF00) >> 8), wr_override);
        }

        public void WriteLong(int addr, uint value, bool wr_override = false)
        {
            WriteByte(addr + 0, (byte)((value & 0x000000FF)),       wr_override);
            WriteByte(addr + 1, (byte)((value & 0x0000FF00) >> 8),  wr_override);
            WriteByte(addr + 3, (byte)((value & 0x00FF0000) >> 16), wr_override);
            WriteByte(addr + 4, (byte)((value & 0xFF000000) >> 32), wr_override);
        }

        public void Copy(int dest_addr, int src_addr, int sz, bool wr_override = false)
        {
            for (int i = 0; i < sz; i++)
            {
                WriteByte(dest_addr + i, ReadByte(src_addr + i), wr_override);
            }
        }

        public void FillByte(int addr, byte value, int sz, bool wr_override = false)
        {
            for (int i = 0; i < sz; i++) { WriteByte(addr + i, value, wr_override); }
        }

        public void FillWord(int addr, ushort value, int sz, bool wr_override = false)
        {
            for (int i = 0; i < sz; i++) { WriteWord(addr + (i * 2), value, wr_override); }
        }

        public void FillLong(int addr, uint value, int sz, bool wr_override = false)
        {
            for (int i = 0; i < sz; i++) { WriteLong(addr + (i * 4), value, wr_override); }
        }

        public BusComponent? GetComponentAtAddress(int addr, int sz)
        {
            foreach (BusComponent comp in Components)
            {
                if (addr >= comp.Address && addr + sz <= comp.Address + comp.Size) { return comp; }
            }
            return null;
        }
    }
}
