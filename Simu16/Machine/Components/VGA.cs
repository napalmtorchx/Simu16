using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Simu16
{
    public enum VGAColor : byte
    { 
        Black,
        DarkBlue,
        DarkGreen,
        DarkCyan,
        DarkRed,
        DarkMagenta,
        DarkYellow,
        Gray,
        DarkGray,
        Blue,
        Green,
        Cyan,
        Red,
        Magenta,
        Yellow,
        White,
    }

    public enum VGAModeID : byte
    {
        Text80x25,
        Graphics256x144,
        Graphics320x200,
        Graphics320x240,
        Graphics640x360,
        Graphics640x400,
        Graphics640x480,
        Graphics800x450,
        Graphics800x600,
    }

    public struct VGAMode
    {
        public VGAModeID ID;
        public int       Width;
        public int       Height;
        public bool      TextMode;

        public VGAMode(VGAModeID id, int w, int h, bool txt) { ID = id; Width = w; Height = h; TextMode = txt; }
    }

    public class VGAController : BusComponent
    {
        public static readonly Color[] Palette16 = new Color[16]
        {
            new Color(0x000000FF),
            new Color(0x00007FFF),
            new Color(0x007F00FF),
            new Color(0x007F7FFF),
            new Color(0x7F0000FF),
            new Color(0x7F007FFF),
            new Color(0x7F7F00FF),
            new Color(0xAFAFAFFF),
            new Color(0x7F7F7FFF),
            Color.Blue,
            Color.Green,
            Color.Cyan,
            Color.Red,
            Color.Magenta,
            Color.Yellow,
            Color.White,
        };

        public static readonly VGAMode[] Modes = new VGAMode[]
        {
            new VGAMode(VGAModeID.Text80x25,       80,  25,  true),
            new VGAMode(VGAModeID.Graphics256x144, 256, 144, false),
            new VGAMode(VGAModeID.Graphics320x200, 320, 200, false),
            new VGAMode(VGAModeID.Graphics320x240, 320, 240, false),
            new VGAMode(VGAModeID.Graphics640x360, 640, 360, false),
            new VGAMode(VGAModeID.Graphics640x400, 640, 400, false),
            new VGAMode(VGAModeID.Graphics640x480, 640, 480, false),
            new VGAMode(VGAModeID.Graphics800x450, 800, 450, false),
            new VGAMode(VGAModeID.Graphics800x600, 800, 600, false),
        };

        public const int CmdClear     = 0x01;
        public const int CmdDrawPixel = 0x02;
        public const int CmdFillRect  = 0x03;
        public const int CmdDrawRect  = 0x04;
        public const int CmdDrawChar  = 0x05;
        public const int CmdDrawStr   = 0x06;
        public const int CmdPrintChar = 0x07;
        public const int CmdPrintStr  = 0x08;
        public const int CmdPrintDec  = 0x09;
        public const int CmdPrintHex  = 0x0A;
        public const int CmdNewline   = 0x0B;
        public const int CmdScroll    = 0x0C;
        public const int CmdSetCursor = 0x0D;
        public const int CmdSetMode   = 0x0E;
        public const int CmdGetMode   = 0x0F;

        public const int PortBase = 0x00D0;

        public VGAMode Mode;

        public IOPort  CommandPort;
        public IOPort  DataPort;
        public IOPort  ColorPort;
        public IOPort  CursorPort;
        public IOPort  SizePort;

        public VGAController(BusController bus) : base(bus, "VGA", 0x0000, 0x1000, true, true)
        {
            CommandPort = new IOPort(this, PortBase, OnCommandPortRead, OnCommandPortWrite);
            Bus.RegisterPort(CommandPort);

            DataPort = new IOPort(this, PortBase + 1, OnDataPortRead, OnDataPortWrite);
            Bus.RegisterPort(DataPort);

            ColorPort = new IOPort(this, PortBase + 2, null, null);
            Bus.RegisterPort(ColorPort);

            CursorPort = new IOPort(this, PortBase + 3, null, null);
            Bus.RegisterPort(CursorPort);

            SizePort = new IOPort(this, PortBase + 4, null, null);
            Bus.RegisterPort(SizePort);

            Mode = new VGAMode(VGAModeID.Text80x25, 80, 25, true);
        }

        public void Reset()
        {
            Array.Fill<byte>(Bus.Data, 0x00, Address, Size);
        }

        public void SetMode(VGAModeID id)
        {
            if (DisplayWindow.Window == null || DisplayWindow.Target == null) { return; }

            foreach (VGAMode mode in Modes)
            {
                if (mode.ID == id)
                {
                    int w = mode.Width, h = mode.Height;
                    if (mode.TextMode) { w *= 8; h *= 16; }
                    Mode = mode;

                    DisplayWindow.Window.Size = new SFML.System.Vector2u((uint)w, (uint)h);
                    DisplayWindow.Target      = new RenderTexture((uint)w, (uint)h);
                    Debug.Log("Set video mode to " + ((byte)mode.ID).ToString("X2") + "(" + mode.ID.ToString() + ")");
                    return;
                }
            }
        }

        private void OnCommandPortRead(BusController bus, IOPort port)
        {
            
        }

        private void OnCommandPortWrite(BusController bus, IOPort port)
        {
            bool flush = false;

            if (port.Data == CmdClear)
            {
                VGAColor bg = (VGAColor)((ColorPort.Data & 0x00FF));
                Clear(bg);
            }
            else if (port.Data == CmdNewline) { Newline(); }
            else if (port.Data == CmdSetMode)
            {
                VGAModeID mode = (VGAModeID)DataPort.Data;
                SetMode(mode);
            }
            else if (port.Data == CmdPrintChar)
            {
                char c = (char)DataPort.Data;
                Write(c);
            }
            else if (port.Data == CmdPrintStr)
            {
                ushort addr = DataPort.Data;
                while (Bus.Data[addr] != 0)
                {
                    char c = (char)Bus.Data[addr];
                    if (c == '\n') { Newline(); }
                    else { Write((char)Bus.Data[addr]); }
                    addr++;
                }
            }
            else if (port.Data == CmdPrintDec) { Write(DataPort.Data.ToString()); }
            else if (port.Data == CmdPrintHex) { Write(DataPort.Data.ToString("X4")); }
            else if (port.Data == CmdGetMode) { DataPort.Data = (ushort)Mode.ID; }

            if (flush)
            {
                CommandPort.Data = 0;
                DataPort.Data = 0;
            }
        }

        private void OnDataPortRead(BusController bus, IOPort port)
        {
            
        }

        private void OnDataPortWrite(BusController bus, IOPort port)
        {
            
        }

        public void Clear(VGAColor bg)
        {
            if (Mode.TextMode)
            {
                for (int i = 0; i < Mode.Width * Mode.Height * 2; i += 2)
                {
                    Bus.Data[i + 0] = 0x20;
                    Bus.Data[i + 1] = (byte)(((byte)GetFG() << 8) | (byte)bg);
                }
            }
            else 
            {
                Array.Fill<byte>(Bus.Data, (byte)bg, 0, Mode.Width * Mode.Height); }
        }

        public void Write(char c) { Write(c, GetFG(), GetBG()); }

        public void Write(char c, VGAColor fg) { Write(c, fg, GetBG()); }

        public void Write(char c, VGAColor fg, VGAColor bg)
        {
            if (c == '\n') { Newline(); }
            else
            {
                int xx = GetCursorX(), yy = GetCursorY();

                int offset = (yy * Mode.Width + xx) * 2;
                Bus.Data[offset + 0] = (byte)c;
                Bus.Data[offset + 1] = (byte)(((byte)fg << 4) | (byte)bg);
                SetCursorX(xx + 1);
                if (xx + 1 >= Mode.Width) { Newline(); }
            }
        }

        public void Write(string str)
        {
            foreach (char c in str) { Write(c); }
        }

        public void Write(string str, VGAColor fg)
        {
            foreach (char c in str) { Write(c, fg); }
        }

        public void Write(string str, VGAColor fg, VGAColor bg)
        {
            foreach (char c in str) { Write(c, fg, bg); }
        }

        public void WriteLine(string str) { Write(str); Newline(); }

        public void WriteLine(string str, VGAColor fg) { Write(str, fg); Newline(); }

        public void WriteLine(string str, VGAColor fg, VGAColor bg)
        {
            Write(str, fg, bg);
            Newline();
        }

        public void Newline()
        {
            int y = GetCursorY();

            SetCursorX(0);
            SetCursorY(y + 1);
            if (y >= Mode.Height) { Scroll(); }
        }

        public void Scroll()
        {
            int line = Mode.Width * 2;
            int size = Mode.Width * Mode.Height * 2;
            Array.Copy(Bus.Data, line, Bus.Data, 0, size - line);
            for (int i = 0; i < Mode.Width * 2; i += 2)
            {
                Bus.Data[(size - line) + i] = 0x20;
                Bus.Data[(size - line) + i + 1] = (byte)((byte)GetFG() << 4 | (byte)GetBG());
            }
            SetCursor(0, Mode.Height - 1);
        }

        public void SetCursorX(int x)
        {
            CursorPort.Data = (ushort)((x << 8) | (CursorPort.Data & 0x00FF));
        }

        public void SetCursorY(int y)
        {
            ushort x = (ushort)((CursorPort.Data & 0xFF00) >> 8);
            CursorPort.Data = (ushort)((x << 8) | (byte)y);
        }
        
        public void SetCursor(int x, int y) { CursorPort.Data = (ushort)((x << 8) | y); }

        public ushort GetCursorX() { return (ushort)((CursorPort.Data & 0xFF00) >> 8); }

        public ushort GetCursorY() { return (ushort)(CursorPort.Data & 0x00FF); }

        public Vector2i GetCursor() { return new Vector2i(GetCursorX(), GetCursorY()); }

        public void SetColors(VGAColor fg, VGAColor bg) { ColorPort.Data = (ushort)(((byte)fg << 8) | (byte)bg); }

        public void SetFG(VGAColor fg) { ColorPort.Data = (ushort)(((byte)fg << 8) | (byte)(ColorPort.Data & 0x00FF)); }

        public void SetBG(VGAColor bg)
        {
            ushort fg = (ushort)((ColorPort.Data & 0xFF00) >> 8);
            ColorPort.Data = (ushort)((fg << 8) | (byte)bg);
        }

        public VGAColor GetFG() { return (VGAColor)((ColorPort.Data & 0xFF00) >> 8); }

        public VGAColor GetBG() { return (VGAColor)(ColorPort.Data & 0x00FF); }
    }
}
