using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public class UART : BusComponent
    {
        public IOPort      CommandPort;
        public IOPort      DataPort;
        public Queue<char> Buffer;
        public bool        Modified;

        public UART(BusController bus) : base(bus, "UART", 0, 0, false, false)
        {
            CommandPort = new IOPort(this, 0x3A0, OnCommandPortRead, OnCommandPortWrite);
            Bus.RegisterPort(CommandPort);

            DataPort = new IOPort(this, 0x03A1, OnDataPortRead, OnDataPortWrite);
            Bus.RegisterPort(DataPort);

            Buffer = new Queue<char>();
        }

        private void OnCommandPortRead(BusController bus, IOPort port)
        {

        }

        private void OnCommandPortWrite(BusController bus, IOPort port)
        {

        }

        private void OnDataPortRead(BusController bus, IOPort port)
        {

        }

        private void OnDataPortWrite(BusController bus, IOPort port)
        {
            if (App.TTYForm != null) { while (App.TTYForm.Reading); }
            bool flush = false;

            if (CommandPort.Data == 0x01)
            {
                char c = (char)port.Data;
                Buffer.Enqueue(c);
            }
            else if (CommandPort.Data == 0x02)
            {
                int addr = (int)port.Data, i = 0;
                while (true)
                {
                    char c = (char)bus.ReadByte(addr + i);
                    if (c == '\0') { break; }
                    Buffer.Enqueue(c);
                    i++;
                    if (Buffer.Count >= 8192) { Debug.Error("UART buffer overflow"); return; }
                }
            }

            if (flush)
            {
                CommandPort.Data = 0;
                DataPort.Data = 0;
            }
        }
    }
}
