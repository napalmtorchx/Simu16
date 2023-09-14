using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public enum RegisterID : byte
    {
        R0, R1, R2, R3, R4, R5, R6, R7, R8, R9, RA, RB, RC, RS, RD,
        SP, BP, IP, EF, CF, Count
    }

    public enum ExecutionFlag : ushort
    {
        Carry      = (1 << 0),
        Zero       = (1 << 1),
        Negative   = (1 << 2),
        BankEnable = (1 << 3),
        IrqEnable  = (1 << 4),
        Halted     = (1 << 5),
    }

    public enum ControlFlag : ushort
    {
        BankIndex = 0x00FF,
        IDTPage   = 0xFF00,

    }
    public class Registers
    {
        public BusController Bus;
        public ushort[]      Values;

        public ushort R0 { get { return Values[(int)RegisterID.R0]; } set { Values[(int)RegisterID.R0] = value; } }
        public ushort R1 { get { return Values[(int)RegisterID.R1]; } set { Values[(int)RegisterID.R1] = value; } }
        public ushort R2 { get { return Values[(int)RegisterID.R2]; } set { Values[(int)RegisterID.R2] = value; } }
        public ushort R3 { get { return Values[(int)RegisterID.R3]; } set { Values[(int)RegisterID.R3] = value; } }
        public ushort R4 { get { return Values[(int)RegisterID.R4]; } set { Values[(int)RegisterID.R4] = value; } }
        public ushort R5 { get { return Values[(int)RegisterID.R5]; } set { Values[(int)RegisterID.R5] = value; } }
        public ushort R6 { get { return Values[(int)RegisterID.R6]; } set { Values[(int)RegisterID.R6] = value; } }
        public ushort R7 { get { return Values[(int)RegisterID.R7]; } set { Values[(int)RegisterID.R7] = value; } }
        public ushort R8 { get { return Values[(int)RegisterID.R8]; } set { Values[(int)RegisterID.R8] = value; } }
        public ushort R9 { get { return Values[(int)RegisterID.R9]; } set { Values[(int)RegisterID.R9] = value; } }
        public ushort RA { get { return Values[(int)RegisterID.RA]; } set { Values[(int)RegisterID.RA] = value; } }
        public ushort RB { get { return Values[(int)RegisterID.RB]; } set { Values[(int)RegisterID.RB] = value; } }
        public ushort RC { get { return Values[(int)RegisterID.RC]; } set { Values[(int)RegisterID.RC] = value; } }
        public ushort RS { get { return Values[(int)RegisterID.RS]; } set { Values[(int)RegisterID.RS] = value; } }
        public ushort RD { get { return Values[(int)RegisterID.RD]; } set { Values[(int)RegisterID.RD] = value; } }

        public ushort SP { get { return Values[(int)RegisterID.SP]; } set { Values[(int)RegisterID.SP] = value; } }
        public ushort BP { get { return Values[(int)RegisterID.BP]; } set { Values[(int)RegisterID.BP] = value; } }
        public ushort IP { get { return Values[(int)RegisterID.IP]; } set { Values[(int)RegisterID.IP] = value; } }
        public ushort EF { get { return Values[(int)RegisterID.EF]; } set { Values[(int)RegisterID.EF] = value; } }
        public ushort CF { get { return Values[(int)RegisterID.CF]; } set { Values[(int)RegisterID.CF] = value; } }

        public Registers(BusController bus)
        {
            Bus = bus;
            Values = new ushort[(int)RegisterID.Count];
            Reset();
        }

        public void Reset() { Array.Fill<ushort>(Values, 0); }

        public void Write(RegisterID reg, ushort value)
        {
            if (reg >= RegisterID.Count) { Debug.Error("Register write exception - Invalid index " + ((byte)reg).ToString("X2")); return; }
            Values[(int)reg] = value;
        }

        public void Write(byte reg, ushort value) { Write((RegisterID)reg, value); }

        public ushort Read(RegisterID reg)
        {
            if (reg >= RegisterID.Count) { Debug.Error("Register read exception - Invalid index" + ((byte)reg).ToString("X2")); return 0; }
            return Values[(int)reg];
        }

        public ushort Read(byte reg) { return Read((RegisterID)reg); }

        public void SetExecutionFlag(ExecutionFlag flag, bool value)
        {
            if (value) { EF |= (ushort)flag; }
            else { EF &= (ushort)~flag; }
        }

        public bool GetExecutionFlag(ExecutionFlag flag)
        {
            ushort v = (ushort)((EF & (ushort)flag) != 0 ? 1 : 0);
            return v > 0;
        }

        public void SetControlFlag(ControlFlag flag, byte value)
        {
            if (flag == ControlFlag.BankIndex) { CF = (ushort)((CF & 0xFF00) | value); return; }
            if (flag == ControlFlag.IDTPage)   { CF = (ushort)((value << 8) | (CF & 0xFF));  }
        }

        public byte GetControlFlag(ControlFlag flag)
        {
            if (flag == ControlFlag.BankIndex) { return (byte)((CF & 0xFF00) >> 8); }
            return (byte)(CF & 0x00FF);
        }

        public void SetBankIndex(int index)
        {
            SetControlFlag(ControlFlag.BankIndex, (byte)index);
        }

        public int GetBankIndex() { return (int)GetControlFlag(ControlFlag.BankIndex); }
    }
}
