using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using Key = SFML.Window.Keyboard.Key;

namespace Simu16
{
    public class Keyboard : BusComponent
    {
        public IOPort      CommandPort;
        public IOPort      DataPort;
        public Queue<Key> Buffer;

        public Keyboard(BusController bus) : base(bus, "KEYBOARD", 0x0000, 0x0000, false, false)
        {
            CommandPort = new IOPort(this, 0x0060, OnCommandPortRead, OnCommandPortWrite);
            Bus.RegisterPort(CommandPort);

            DataPort = new IOPort(this, 0x0061, OnDataPortRead, OnDataPortWrite);
            Bus.RegisterPort(DataPort);

            Buffer = new Queue<Key>();
        }

        private void OnCommandPortRead(BusController bus, IOPort port)
        {

        }

        private void OnCommandPortWrite(BusController bus, IOPort port)
        {

        }

        private void OnDataPortRead(BusController bus, IOPort port)
        {
            if (Buffer.Count == 0) { port.Data = 0; return; }
            port.Data = (ushort)Buffer.Dequeue();
        }

        private void OnDataPortWrite(BusController bus, IOPort port)
        {
            if (App.TTYForm != null) { while (App.TTYForm.Writing); }
            bool flush = false;

            if (flush)
            {
                CommandPort.Data = 0;
                DataPort.Data    = 0;
            }
        }
    }
}
