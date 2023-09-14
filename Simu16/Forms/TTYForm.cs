using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Simu16
{
    public partial class TTYForm : Form
    {
        public bool Inverted = true;
        public bool Reading = false;
        public bool Writing = false;

        public TTYForm()
        {
            InitializeComponent();
        }

        private void TTYForm_Load(object sender, EventArgs e)
        {
        }

        private void BtnInvert_Click(object sender, EventArgs e)
        {
            Inverted = !Inverted;
            if (Inverted) { TxtOutput.ForeColor = Color.White; TxtOutput.BackColor = Color.Black; }
            else { TxtOutput.ForeColor = Color.Black; TxtOutput.BackColor = Color.White; }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (App.Bus != null) { App.Bus.UART.Buffer.Clear(); }
            TxtOutput.Text = "";
        }

        private void TxtOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void TTYForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            App.TTYForm = null;
        }

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            if (App.Bus != null && App.Bus.UART.Buffer.Count > 0)
            {
                Reading = true;
                char c = App.Bus.UART.Buffer.Dequeue();
                if (c >= 0x20 && c < 0x7F) { TxtOutput.AppendText(c.ToString()); }
                else if (c == 0x0A) { TxtOutput.AppendText("\n"); }
                else if (c == 0xFE) { App.Bus.UART.Buffer.Clear(); TxtOutput.Text = ""; }
                else if (c == 0x08)
                {
                    if (TxtOutput.Text != null && TxtOutput.Text.Length > 0)
                    {
                        TxtOutput.Text = TxtOutput.Text.Remove(TxtOutput.Text.Length - 1, 1);
                        TxtOutput.SelectionStart = TxtOutput.TextLength;
                    }
                }

                TxtOutput.ScrollToCaret();
                Reading = false;
            }
        }

        private void TxtOutput_KeyDown(object sender, KeyEventArgs e)
        {
            if (App.Bus == null) { return; }
            e.SuppressKeyPress = true;
            e.Handled          = true;
        }

        private void TxtOutput_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
