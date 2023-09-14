using Simu16.Assembler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simu16
{
    public partial class DebugForm : Form
    {
        public DebugForm() { InitializeComponent(); }

        private void DebugForm_FormClosing(object sender, FormClosingEventArgs e) { Environment.Exit(0); }

        private void DebugForm_Load(object sender, EventArgs e)
        {
            if (App.Bus != null)
            {
                SetHexBox(App.Bus.BIOS.Address + ParseOffset(), ParseSize());

                ComboPorts.Items.Clear();
                foreach (IOPort port in App.Bus.Ports)
                {
                    ComboPorts.Items.Add(port.Component.Name + "_" + port.Port.ToString("X4"));
                }
                ComboPorts.SelectedIndex = 0;
            }

            UpdateAll();
        }

        private ushort ParseOffset()
        {
            string str = TxtOffset.Text;
            if (Grammar.IsDecimal(str)) { return (ushort)uint.Parse(str); }
            else if (Grammar.IsHex(str)) { return (ushort)uint.Parse(str.Substring(2), System.Globalization.NumberStyles.HexNumber); }
            else { MessageBox.Show("Invalid offset value '" + str + "'"); TxtOffset.Text = ""; return 0; }
        }

        private ushort ParseSize()
        {
            string str = TxtSize.Text;
            if (Grammar.IsDecimal(str)) { return (ushort)uint.Parse(str); }
            else if (Grammar.IsHex(str)) { return (ushort)uint.Parse(str.Substring(2), System.Globalization.NumberStyles.HexNumber); }
            else { MessageBox.Show("Invalid size value '" + str + "'"); TxtSize.Text = ""; return 0; }
        }

        private int ParseFreq()
        {
            string str = TxtFreq.Text;
            if (Grammar.IsDecimal(str)) { return int.Parse(str); }
            else { MessageBox.Show("Invalid frequency value '" + str + "'"); TxtFreq.Text = ""; return 0; }
        }

        private void AppendHexBox(string str, Color color)
        {
            TxtHex.SelectionStart = TxtHex.TextLength;
            TxtHex.SelectionLength = 0;
            TxtHex.SelectionColor = color;
            TxtHex.AppendText(str);
            TxtHex.SelectionColor = TxtHex.ForeColor;
        }

        private void SetHexBox(int addr, int size)
        {
            if (App.Bus == null) { return; }
            TxtHex.Clear();

            this.Text = "Simu16 Debugger - Generating hex data...";

            for (int i = 0; i < size; i += 16)
            {
                AppendHexBox((addr + i).ToString("X4"), Color.Cyan);
                TxtHex.AppendText(":");
                AppendHexBox((addr + i + 15).ToString("X4") + "  ", Color.Cyan);
                if (i % (size / 16) == 0) { this.Text += "."; }

                int j;
                for (j = 0; j < 16; j++)
                {
                    byte val = App.Bus.Data[addr + i + j];
                    AppendHexBox(val.ToString("X2") + " ", val == 0 ? Color.DimGray : Color.White);
                }

                for (j = 0; j < 16; j++)
                {
                    char c = (char)App.Bus.Data[addr + i + j];
                    bool valid = (c >= 32 && c <= 126);
                    AppendHexBox(valid ? c.ToString() : ".", valid ? Color.Yellow : Color.DimGray);
                }
                TxtHex.AppendText("\n");
            }

            this.Text = "Simu16 Debugger";
        }

        public void UpdateAll()
        {
            UpdateRegisters();
            UpdateFlags();
            UpdatePorts();
        }

        public void UpdateRegisters()
        {
            if (App.Bus == null) { return; }

            LabelR0.Text = App.Bus.CPU.Registers.R0.ToString("X4");
            LabelR1.Text = App.Bus.CPU.Registers.R1.ToString("X4");
            LabelR2.Text = App.Bus.CPU.Registers.R2.ToString("X4");
            LabelR3.Text = App.Bus.CPU.Registers.R3.ToString("X4");
            LabelR4.Text = App.Bus.CPU.Registers.R4.ToString("X4");
            LabelR5.Text = App.Bus.CPU.Registers.R5.ToString("X4");
            LabelR6.Text = App.Bus.CPU.Registers.R6.ToString("X4");
            LabelR7.Text = App.Bus.CPU.Registers.R7.ToString("X4");
            LabelR8.Text = App.Bus.CPU.Registers.R8.ToString("X4");
            LabelR9.Text = App.Bus.CPU.Registers.R9.ToString("X4");
            LabelRA.Text = App.Bus.CPU.Registers.RA.ToString("X4");
            LabelRB.Text = App.Bus.CPU.Registers.RB.ToString("X4");
            LabelRC.Text = App.Bus.CPU.Registers.RC.ToString("X4");
            LabelRS.Text = App.Bus.CPU.Registers.RS.ToString("X4");
            LabelRD.Text = App.Bus.CPU.Registers.RD.ToString("X4");
            LabelSP.Text = App.Bus.CPU.Registers.SP.ToString("X4");
            LabelBP.Text = App.Bus.CPU.Registers.BP.ToString("X4");
            LabelIP.Text = App.Bus.CPU.Registers.IP.ToString("X4");
            LabelEF.Text = App.Bus.CPU.Registers.EF.ToString("X4");
            LabelCF.Text = App.Bus.CPU.Registers.CF.ToString("X4");
        }

        public void UpdateFlags()
        {
            if (App.Bus == null) { return; }

            LabelCarry.Text = App.Bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Carry) ? "1" : "0";
            if (LabelCarry.Text == "1") { LabelCarry.ForeColor = Color.Lime; } else { LabelCarry.ForeColor = Color.Red; }

            LabelZero.Text = App.Bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Zero) ? "1" : "0";
            if (LabelZero.Text == "1") { LabelZero.ForeColor = Color.Lime; } else { LabelZero.ForeColor = Color.Red; }

            LabelNegative.Text = App.Bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Negative) ? "1" : "0";
            if (LabelNegative.Text == "1") { LabelNegative.ForeColor = Color.Lime; } else { LabelNegative.ForeColor = Color.Red; }

            LabelBankEnable.Text = App.Bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.BankEnable) ? "1" : "0";
            if (LabelBankEnable.Text == "1") { LabelBankEnable.ForeColor = Color.Lime; } else { LabelBankEnable.ForeColor = Color.Red; }

            LabelHalt.Text = App.Bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.Halted) ? "1" : "0";
            if (LabelHalt.Text == "1") { LabelHalt.ForeColor = Color.Lime; } else { LabelHalt.ForeColor = Color.Red; }

            LabelIE.Text = App.Bus.CPU.Registers.GetExecutionFlag(ExecutionFlag.IrqEnable) ? "1" : "0";
            if (LabelIE.Text == "1") { LabelIE.ForeColor = Color.Lime; } else { LabelIE.ForeColor = Color.Red; }

            LabelBankIndex.Text = App.Bus.CPU.Registers.GetControlFlag(ControlFlag.BankIndex).ToString("X2");
            if (LabelBankIndex.Text == "00") { LabelBankIndex.ForeColor = Color.Red; } else { LabelBankIndex.ForeColor = Color.Lime; }

            LabelIDTR.Text = App.Bus.CPU.Registers.GetControlFlag(ControlFlag.IDTPage).ToString("X2");
            if (LabelIDTR.Text == "00") { LabelIDTR.ForeColor = Color.Red; } else { LabelIDTR.ForeColor = Color.Lime; }
        }

        public void UpdatePorts()
        {
            if (App.Bus == null) { return; }

            foreach (IOPort port in App.Bus.Ports)
            {
                string sel = ((string)ComboPorts.Items[ComboPorts.SelectedIndex]);
                sel = sel.Substring(0, sel.IndexOf('_'));
                if (port.Component.Name == sel)
                {
                    LabelPort.Text = port.Port.ToString("X4");
                    LabelPortVal.Text = port.Data.ToString("X4");
                    LabelPortWr.Text = "1";
                }
            }
        }

        private void BtnStep_Click(object sender, EventArgs e)
        {
            if (App.Bus == null) { return; }
            App.Stepped = true;
            UpdateAll();
        }

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            UpdateRegisters();
            UpdateFlags();
            UpdatePorts();
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            if (!App.Started)
            {
                BtnContinue.Text = "STOP";
                BtnContinue.ForeColor = Color.Red;
                App.Started = true;
            }
            else
            {
                BtnContinue.Text = "CONTINUE";
                BtnContinue.ForeColor = Color.Lime;
                App.Started = false;
            }
        }

        private void BtnTTY_Click(object sender, EventArgs e)
        {
            App.ShowTTY();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            App.Started = false;
            App.Stepped = false;
            BtnContinue.Text = "CONTINUE";
            BtnContinue.ForeColor = Color.Lime;
            if (App.Bus != null) { App.Bus.CPU.Reset(); }
            UpdateAll();
        }

        private void BtnHexDump_Click(object sender, EventArgs e)
        {
            if (App.Bus == null) { return; }
            SetHexBox(ParseOffset(), ParseSize());
        }

        private void ComboPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePorts();
        }

        private void BtnSetFreq_Click(object sender, EventArgs e)
        {
            if (App.Bus == null) { return; }
            int freq = ParseFreq();
            App.Bus.Frequency = freq;
            Debug.Log("Set clock frequency to " + freq + "Hz");
        }
    }
}
