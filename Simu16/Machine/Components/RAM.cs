using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public class RAM : BusComponent
    {
        public RAM(BusController bus) : base(bus, "RAM", 0x3000, 0xF000 - 0x3000, true, true)
        {

        }
        
        public void Reset()
        {
            Array.Fill<byte>(Bus.Data, 0x00, Address, Size);
        }
    }
}
