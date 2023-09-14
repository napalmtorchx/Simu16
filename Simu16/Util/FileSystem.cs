using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simu16
{
    public class FileSystem
    {
        public const ushort SectorIndexMBR   = 0;
        public const ushort SectorIndexTable = 1;

        public const ushort FileEnt_Name      = 0;
        public const ushort FileEnt_NameSz    = 24;
        public const ushort FileEnt_Start     = (FileEnt_Name    + FileEnt_NameSz);
        public const ushort FileEnt_Sectors   = (FileEnt_Start   + 2);
        public const ushort FileEnt_Hidden    = (FileEnt_Sectors + 2);
        public const ushort FileEntSz         = 32;

        public const ushort MBR_TableStart  = 0;
        public const ushort MBR_TableCount  = (MBR_TableStart  + 2);
        public const ushort MBR_TableLength = (MBR_TableCount  + 2);
        public const ushort MBR_DataStart   = (MBR_TableLength + 2);
        public const ushort MBR_DataLength  = (MBR_DataStart   + 2);
        public const ushort MBR_DataPos     = (MBR_DataLength  + 2);

        public Disk Disk;

        private ushort _table_start;
        private ushort _table_count;
        private ushort _table_len;
        private ushort _data_start;
        private ushort _data_len;
        private ushort _data_pos;

        public FileSystem(Disk disk)
        {
            Disk = disk;
        }

        public void Format(int max_files = 256)
        {
            CreateBootRecord(max_files);
            WriteBootRecord();

            byte[] testdata =
            {
                (byte)'H', (byte)'e', (byte)'l', (byte)'l', (byte)'o', 0x20,
                (byte)'w', (byte)'o', (byte)'r', (byte)'l', (byte)'d', 0x0A, 0x00,
            };

            CreateFile("hello.txt", testdata, false);

            Debug.OK("Finished formatting disk");
        }

        public void CreateBootRecord(int max_files)
        {
            _table_start = SectorIndexTable;
            _table_count = 0;
            _table_len = (ushort)((FileEntSz * max_files) / Disk.SectorSize);
            _data_len = (ushort)((Disk.Size / Disk.SectorSize) - (_table_len + 1));
            _data_start = (ushort)(1 + _table_len);
            _data_pos = _data_start;
        }

        public void WriteBootRecord()
        {
            List<byte> sector = new List<byte>();

            // write properties
            sector.AddRange(BitConverter.GetBytes(_table_start));
            sector.AddRange(BitConverter.GetBytes(_table_count));
            sector.AddRange(BitConverter.GetBytes(_table_len));
            sector.AddRange(BitConverter.GetBytes(_data_start));
            sector.AddRange(BitConverter.GetBytes(_data_len));
            sector.AddRange(BitConverter.GetBytes(_data_pos));

            Disk.WriteSector(sector.ToArray(), SectorIndexMBR);
            Disk.Save();
            Debug.Log("Master boot record written to disk");
        }

        public void CreateFile(string name, byte[] data, bool hidden)
        {
            int start = SectorIndexTable * Disk.SectorSize, count = (_table_len * Disk.SectorSize) / FileEntSz;
            for (int i = 0; i < count; i++)
            {
                int    fp       = start + (i * FileEntSz);
                ushort sz       = BitConverter.ToUInt16(Disk.Data, fp + FileEnt_Sectors);
                uint   sectors  = Align((uint)data.Length, Disk.SectorSize) / Disk.SectorSize;

                if (sz == 0)
                {
                    for (int j = 0; j < 24; j++) { if (j < name.Length) { Disk.Data[fp + j] = (byte)name[j]; } else { Disk.Data[fp + j] = 0; } }
                    Array.Copy(BitConverter.GetBytes(_data_pos), 0, Disk.Data, fp + FileEnt_Start, 2);
                    Array.Copy(BitConverter.GetBytes((ushort)sectors), 0, Disk.Data, fp + FileEnt_Start, 2);
                    Array.Copy(BitConverter.GetBytes((byte)(hidden ? 1 : 0)), 0, Disk.Data, fp + FileEnt_Hidden, 1);

                    byte[] aligned_data = new byte[sectors * Disk.SectorSize];
                    Array.Copy(data, 0, aligned_data, 0, data.Length);

                    ushort data_offset = (ushort)(_data_pos * Disk.SectorSize);
                    Array.Copy(aligned_data, 0, Disk.Data, data_offset, sectors * Disk.SectorSize);
                    _data_pos += (ushort)sectors;
                    _table_count++;
                    Debug.Log("Created file - Start:" + (data_offset / Disk.SectorSize).ToString("X4") + " Sz:" + (sectors * Disk.SectorSize) + " bytes Hidden:" + (hidden ? "1" : "0") + " Name:" + name);
                    WriteBootRecord();
                    return;
                }
            }
        }

        public static uint Align(uint num, uint align)
        {
            uint output = num;
            output &= (0xFFFFFFFF - (align - 1));
            if (output < num) { output += align; }
            return output;
        }
    }
}
