using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Simu16
{
    partial class TTYForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TxtOutput = new RichTextBox();
            BtnClear = new Button();
            BtnInvert = new Button();
            TimerUpdate = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // TxtOutput
            // 
            TxtOutput.BackColor = Color.Black;
            TxtOutput.BorderStyle = BorderStyle.None;
            TxtOutput.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            TxtOutput.ForeColor = Color.White;
            TxtOutput.Location = new Point(12, 41);
            TxtOutput.Name = "TxtOutput";
            TxtOutput.ReadOnly = true;
            TxtOutput.Size = new Size(776, 397);
            TxtOutput.TabIndex = 0;
            TxtOutput.Text = "";
            TxtOutput.TextChanged += TxtOutput_TextChanged;
            TxtOutput.KeyDown += TxtOutput_KeyDown;
            TxtOutput.KeyUp += TxtOutput_KeyUp;
            // 
            // BtnClear
            // 
            BtnClear.FlatAppearance.BorderColor = Color.Silver;
            BtnClear.FlatStyle = FlatStyle.Flat;
            BtnClear.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnClear.ForeColor = Color.White;
            BtnClear.Location = new Point(12, 12);
            BtnClear.Name = "BtnClear";
            BtnClear.Size = new Size(75, 23);
            BtnClear.TabIndex = 28;
            BtnClear.Text = "CLEAR";
            BtnClear.UseVisualStyleBackColor = true;
            BtnClear.Click += BtnClear_Click;
            // 
            // BtnInvert
            // 
            BtnInvert.FlatAppearance.BorderColor = Color.Silver;
            BtnInvert.FlatStyle = FlatStyle.Flat;
            BtnInvert.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnInvert.ForeColor = Color.White;
            BtnInvert.Location = new Point(93, 12);
            BtnInvert.Name = "BtnInvert";
            BtnInvert.Size = new Size(75, 23);
            BtnInvert.TabIndex = 29;
            BtnInvert.Text = "INVERT";
            BtnInvert.UseVisualStyleBackColor = true;
            BtnInvert.Click += BtnInvert_Click;
            // 
            // TimerUpdate
            // 
            TimerUpdate.Enabled = true;
            TimerUpdate.Interval = 1;
            TimerUpdate.Tick += TimerUpdate_Tick;
            // 
            // TTYForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(800, 450);
            Controls.Add(BtnInvert);
            Controls.Add(BtnClear);
            Controls.Add(TxtOutput);
            Name = "TTYForm";
            Text = "Simu16 TTY";
            FormClosing += TTYForm_FormClosing;
            Load += TTYForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox TxtOutput;
        private Button BtnClear;
        private Button BtnInvert;
        private System.Windows.Forms.Timer TimerUpdate;
    }
}