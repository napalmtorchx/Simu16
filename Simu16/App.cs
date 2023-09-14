using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using Simu16.Assembler;

namespace Simu16
{
    public static class App
    {
        public static BusController? Bus;
        public static Thread?        Thread;
        public static bool           Started, Stepped;
        public static long           TPS { get; private set; } = 0;
        public static long           TotalTicks { get; private set; } = 0;

        public static DebugForm? DebugForm;
        public static TTYForm?   TTYForm;

        private static long _ticks = 0, _tm;
        private static double _secs;

        [STAThread]
        public static void Main()
        {
            Tokenizer tokenizer = new Tokenizer("ROMS/os/", "ROMS/os/kernel.s");
            tokenizer.Process();

            Parser parser = new Parser(tokenizer.Tokens);
            parser.Process();

            const string bios_file = "ROMS/bin/bios.bin";
            List<byte> binary = BytecodeGenerator.Generate(parser.Nodes, parser.Origin);
            File.WriteAllBytes(bios_file, binary.ToArray());
            Debug.OK("Saved BIOS file to '" + bios_file + "'");

            Bus = new BusController();
            Bus.BIOS.Load(File.ReadAllBytes(bios_file));

            Thread = new Thread(EmulationThread);
            Thread.Start();

            DisplayWindow.Init();

            ApplicationConfiguration.Initialize();

            DebugForm = new DebugForm();

            Application.Run(DebugForm);

            _secs = GetSeconds();
        }

        private static void EmulationThread()
        {
            if (Bus == null) { return; }

            while (true)
            {
                if (Stepped || Started) 
                {
                    double s = GetSeconds();
                    if ((s - _secs >= 1000.0 / (double)Bus.Frequency / 1000.0) || Bus.Frequency == 0)
                    {
                        _secs = s;
                        _ticks++;
                        TotalTicks++;

                        if (_tm != DateTime.Now.Second)
                        {
                            _tm = DateTime.Now.Second;
                            TPS = _ticks;
                            _ticks = 0;
                        }

                        Bus.CPU.Step(1);
                        Bus.SDC.SaveClock();
                        if (Stepped) { Stepped = false; }
                    }
                }
            }
        }

        public static void ShowTTY()
        {
            if (DebugForm == null) { return; }
            if (TTYForm   == null) { TTYForm = new TTYForm(); }

            TTYForm.Show();
            TTYForm.Location = new System.Drawing.Point(DebugForm.Left + DebugForm.Width + 8, DebugForm.Top);
        }

        public static double GetSeconds()
        {
            TimeSpan tt = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (double)tt.TotalSeconds;
        }
    }
}