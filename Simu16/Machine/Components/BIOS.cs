using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public class BIOS : BusComponent
    {
        public BIOS(BusController bus) : base(bus, "BIOS_ROM", 0x1000, 0x2000, true, false)
        {

        }

        public void Load(byte[] data)
        {
            if (data.Length > Size) { Debug.Error("Cannot load BIOS rom - Must be 8KB or less"); return; }
            Bus.Write(Address, 0, data.Length, data, true);
            Debug.Log("Loaded BIOS - Size:" + data.Length);
        }
    }
}
