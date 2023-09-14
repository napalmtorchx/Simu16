using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public class BusComponent
    {
        public BusController Bus;
        public string        Name;
        public int           Address;
        public int           Size;
        public bool          CanRead;
        public bool          CanWrite;

        public BusComponent(BusController bus, string name, int addr, int sz, bool rd, bool wr)
        {
            Bus      = bus;
            Name     = name;
            Address  = addr;
            Size     = sz;
            CanRead  = rd;
            CanWrite = wr;
        }

        public override string ToString()
        {
            string mem = (Size > 0 ? Address.ToString("X4") + "-" + (Address + Size - 1).ToString("X4") : "(null)");

            return "Memory:" + mem + " Read:" + (CanRead ? "1" : "0") + " Write:" + (CanWrite ? "1" : "0") + " Name:" + Name;
        }
    }
}
