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

        public static DebugForm? DebugForm;
        public static TTYForm?   TTYForm;

        [STAThread]
        public static void Main()
        {
            Tokenizer tokenizer = new Tokenizer("ROMS/src/", "ROMS/src/bios.s");
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
        }

        private static void EmulationThread()
        {
            if (Bus == null) { return; }

            while (true)
            {
                if (Stepped) { Bus.CPU.Step(1); Stepped = false; continue; }
                if (Started) { Bus.CPU.Step(1); }

                if (Bus.Frequency > 0)
                {
                    float freq = 1000.0f / Bus.Frequency;
                    if (freq > 1) { Thread.Sleep(1000 / Bus.Frequency); }
                }

                if (Bus.Frequency > 0) { Thread.Sleep(1000 / Bus.Frequency); }

                Bus.SDC.SaveClock();
            }
        }

        public static void ShowTTY()
        {
            if (DebugForm == null) { return; }
            if (TTYForm   == null) { TTYForm = new TTYForm(); }

            TTYForm.Show();
            TTYForm.Location = new System.Drawing.Point(DebugForm.Left + DebugForm.Width + 8, DebugForm.Top);
        }
    }
}