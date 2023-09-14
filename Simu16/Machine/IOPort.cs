using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public class IOPort
    {
        public BusComponent                  Component;
        public ushort                        Port;
        public ushort                        Data;
        public Action<BusController, IOPort>? OnRead;
        public Action<BusController, IOPort>? OnWrite;

        public IOPort(BusComponent component, int port, Action<BusController, IOPort>? on_rd, Action<BusController, IOPort>? on_wr)
        {
            Component = component;
            Port      = (ushort)port;
            Data      = 0x0000;
            OnRead    = on_rd;
            OnWrite   = on_wr;
        }

        public override string ToString()
        {
            return "Port:" + Port.ToString("X4") + " Data:" + Data.ToString("X4") + " Component:" + Component.Name;
        }
    }
}
