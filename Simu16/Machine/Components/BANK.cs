using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public class BANK : BusComponent
    {
        public const int Count = 256;

        private byte[] _data;

        public BANK(BusController bus) : base(bus, "BANK", 0xF000, 0x1000, true, true)
        {
            _data = new byte[Size * Count];
        }

        public byte ReadByte(int bank, int addr)
        {
            Debug.Log("Bank file read - Bank:" + bank + " Offset:" + addr.ToString("X4"));
            return _data[(bank * Size) + addr];
        }

        public void WriteByte(int bank, int addr, byte value)
        {
            Debug.Log("Bank file write - Bank:" + bank + " Offset:" + addr.ToString("X4") + "Value:" + value.ToString("X2"));
            _data[(bank * Size) + addr] = value;
        }
    }
}
