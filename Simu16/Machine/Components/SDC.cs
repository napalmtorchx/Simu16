using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public class Disk
    {
        public const ushort SectorSize = 512;
        public const int    Size       = 0x10000 * SectorSize;

        public string Filename;
        public byte[] Data;

        public Disk(string fname, bool create = false)
        {
            Filename = fname;
            Data     = new byte[Size];
            Array.Fill<byte>(Data, 0);

            if (!File.Exists(fname))
            {
                if (!create) { Debug.Error("Unable to locate disk image at '" + fname + "'"); Filename = ""; return; }
                else { File.WriteAllBytes(fname, Data); Debug.Log("Created new disk image at '" + fname + "'"); }
            }
            else
            {
                byte[] data = File.ReadAllBytes(fname);
                if (data.Length > Size) { Debug.Error("Disk image at '" + fname + "' is larger than 32MB"); Filename = ""; return; }
                Array.Copy(data, 0, Data, 0, data.Length);
            }

            Debug.OK("Loaded disk image at '" + fname + "'");
        }

        public byte[] ReadSector(int sector)
        {
            byte[] data = new byte[SectorSize];
            Array.Fill<byte>(data, 0x00);

            if (Data.Length == 0 || sector >= Data.Length / SectorSize) { Debug.Warning("Read attempt from invalid sector " + sector.ToString("X2") + " on disk"); return data; }
            Array.Copy(Data, sector * SectorSize, data, 0, SectorSize);
            return data;
        }

        public void WriteSector(byte[] data, int sector)
        {
            if (Data.Length == 0 || sector >= Data.Length / SectorSize) { Debug.Warning("Write attempt to invalid sector " + sector.ToString("X2") + " on disk"); return; }
            Array.Copy(data, 0, Data, sector * SectorSize, data.Length < SectorSize ? data.Length : SectorSize);
        }

        public void Save()
        {
            if (Filename.Length == 0) { return; }
            File.WriteAllBytes(Filename, Data);
            Debug.Log("Saved disk image at '" + Filename + "'");
        }
    }

    public class SDC : BusComponent
    {
        public const ushort PortBase = 0x0080;
        public const ushort CmdRead  = 0x0001;
        public const ushort CmdWrite = 0x0001;

        public Disk   Disk;
        public IOPort CommandPort;
        public IOPort BufferPort;
        public IOPort SectorPort;
        public IOPort LengthPort;

        private DateTime _tm;
        private FileSystem _fs;

        public SDC(BusController bus) : base(bus, "SDC", 0x0000, 0x0000, false, false)
        {
            CommandPort = new IOPort(this, PortBase, null, OnCommandPortWrite);
            Bus.RegisterPort(CommandPort);

            BufferPort = new IOPort(this, PortBase + 1, null, null);
            Bus.RegisterPort(BufferPort);

            SectorPort = new IOPort(this, PortBase + 2, null, null);
            Bus.RegisterPort(SectorPort);

            LengthPort = new IOPort(this, PortBase + 3, null, null);
            Bus.RegisterPort(LengthPort);

            Disk = new Disk("disk.img", true);
            _fs  = new FileSystem(Disk);
            _fs.Format(1024);

            _tm = DateTime.Now;
        }

        public void SaveClock()
        {
            DateTime now = DateTime.Now;
            if (now.Second - _tm.Second >= 15)
            {
                _tm = now;
                Disk.Save();
            }
        }

        private void OnCommandPortWrite(BusController bus, IOPort port)
        {
            if (App.TTYForm != null) { while (App.TTYForm.Writing) ; }
            bool flush = false;

            if (CommandPort.Data == CmdRead && Disk != null)
            {
                byte[] sector;
                for (int i = 0; i < LengthPort.Data; i++)
                {
                    sector = Disk.ReadSector(SectorPort.Data + i);
                    Bus.Write(BufferPort.Data + (i * Disk.SectorSize), 0, Disk.SectorSize, sector, false);
                }
                Debug.Log("Disk read - Address:" + BufferPort.Data.ToString("X4") + " Sector:" + SectorPort.Data.ToString("X4") + " Length:" + LengthPort.Data.ToString("X4"));
                flush = true;
            }
            else if (CommandPort.Data == CmdWrite && Disk != null)
            {
                byte[] sector;
                for (int i = 0; i < LengthPort.Data; i++)
                {
                    sector = Bus.Read(BufferPort.Data + (i * Disk.SectorSize), Disk.SectorSize);
                    Disk.WriteSector(sector, SectorPort.Data);
                }
                Debug.Log("Disk write - Address:" + BufferPort.Data.ToString("X4") + " Sector:" + SectorPort.Data.ToString("X4") + " Length:" + LengthPort.Data.ToString("X4"));
                if (LengthPort.Data > 8) { Disk.Save(); }
                flush = true;
            }

            if (flush)
            {
                CommandPort.Data = 0;
                BufferPort.Data  = 0;
                SectorPort.Data  = 0;
                LengthPort.Data  = 0;
            }
        }
    }
}
