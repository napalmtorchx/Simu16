using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Simu16
{
    partial class DebugForm
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
            BtnReset = new Button();
            BtnStep = new Button();
            BtnContinue = new Button();
            LabelName = new Label();
            PanelRegisters = new Panel();
            LabelCF = new Label();
            LabelTagCF = new Label();
            DivRegisters3 = new PictureBox();
            LabelEF = new Label();
            LabelTagEF = new Label();
            LabelIP = new Label();
            LabelTagIP = new Label();
            LabelBP = new Label();
            LabelTagBP = new Label();
            LabelSP = new Label();
            LabelTagSP = new Label();
            LabelRD = new Label();
            LabelTagRD = new Label();
            LabelRS = new Label();
            LabelTagRS = new Label();
            LabelRC = new Label();
            DivRegisters2 = new PictureBox();
            LabelTagRC = new Label();
            LabelRegisters = new Label();
            LabelRB = new Label();
            LabelTagRB = new Label();
            LabelRA = new Label();
            LabelTagRA = new Label();
            LabelR9 = new Label();
            LabelTagR9 = new Label();
            LabelR8 = new Label();
            LabelTagR8 = new Label();
            LabelR7 = new Label();
            LabelTagR7 = new Label();
            LabelR6 = new Label();
            LabelTagR6 = new Label();
            LabelR5 = new Label();
            LabelTagR5 = new Label();
            LabelR4 = new Label();
            LabelTagR4 = new Label();
            LabelR3 = new Label();
            LabelTagR3 = new Label();
            LabelR2 = new Label();
            LabelTagR2 = new Label();
            LabelR1 = new Label();
            LabelTagR1 = new Label();
            LabelR0 = new Label();
            DivRegisters = new PictureBox();
            LabelTagR0 = new Label();
            PanelMemory = new Panel();
            LabelBytes = new Label();
            LabelSize = new Label();
            TxtSize = new TextBox();
            LabelOffset = new Label();
            TxtOffset = new TextBox();
            LabelMemory = new Label();
            PanelHex = new Panel();
            TxtHex = new RichTextBox();
            BtnHexDump = new Button();
            TimerUpdate = new Timer(components);
            PanelFlags = new Panel();
            LabelIE = new Label();
            LabelTagIE = new Label();
            LabelIDTR = new Label();
            LabelTagIDTR = new Label();
            DivFlags2 = new PictureBox();
            DivFlags3 = new PictureBox();
            LabelFlags = new Label();
            LabelBankIndex = new Label();
            LabelTagBankIndex = new Label();
            LabelHalt = new Label();
            LabelTagHalt = new Label();
            LabelBankEnable = new Label();
            LabelTagBankEnable = new Label();
            LabelNegative = new Label();
            LabelTagNegative = new Label();
            LabelZero = new Label();
            LabelTagZero = new Label();
            LabelCarry = new Label();
            DivFlags = new PictureBox();
            LabelTagCarry = new Label();
            PanelPorts = new Panel();
            LabelPortWr = new Label();
            LabelTagPortWr = new Label();
            ComboPorts = new ComboBox();
            LabelPortVal = new Label();
            LabelTagPortVal = new Label();
            LabelPort = new Label();
            DivPorts = new PictureBox();
            LabelTagPort = new Label();
            LabelPorts = new Label();
            PanelUtil = new Panel();
            BtnScreenshot = new Button();
            BtnSaveVRAM = new Button();
            BtnSaveRAM = new Button();
            BtnLoadROM = new Button();
            BtnTTY = new Button();
            LabelUtil = new Label();
            LabelFreq = new Label();
            TxtFreq = new TextBox();
            BtnSetFreq = new Button();
            LabelTPS = new Label();
            PanelRegisters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DivRegisters3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DivRegisters2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DivRegisters).BeginInit();
            PanelMemory.SuspendLayout();
            PanelHex.SuspendLayout();
            PanelFlags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DivFlags2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DivFlags3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DivFlags).BeginInit();
            PanelPorts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DivPorts).BeginInit();
            PanelUtil.SuspendLayout();
            SuspendLayout();
            // 
            // BtnReset
            // 
            BtnReset.FlatAppearance.BorderColor = Color.Silver;
            BtnReset.FlatStyle = FlatStyle.Flat;
            BtnReset.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnReset.ForeColor = Color.Gold;
            BtnReset.Location = new Point(130, 15);
            BtnReset.Name = "BtnReset";
            BtnReset.Size = new Size(75, 23);
            BtnReset.TabIndex = 0;
            BtnReset.Text = "RESET";
            BtnReset.UseVisualStyleBackColor = true;
            BtnReset.Click += BtnReset_Click;
            // 
            // BtnStep
            // 
            BtnStep.FlatAppearance.BorderColor = Color.Silver;
            BtnStep.FlatStyle = FlatStyle.Flat;
            BtnStep.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnStep.ForeColor = Color.Cyan;
            BtnStep.Location = new Point(211, 15);
            BtnStep.Name = "BtnStep";
            BtnStep.Size = new Size(75, 23);
            BtnStep.TabIndex = 1;
            BtnStep.Text = "STEP";
            BtnStep.UseVisualStyleBackColor = true;
            BtnStep.Click += BtnStep_Click;
            // 
            // BtnContinue
            // 
            BtnContinue.FlatAppearance.BorderColor = Color.Silver;
            BtnContinue.FlatStyle = FlatStyle.Flat;
            BtnContinue.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnContinue.ForeColor = Color.Lime;
            BtnContinue.Location = new Point(292, 15);
            BtnContinue.Name = "BtnContinue";
            BtnContinue.Size = new Size(75, 23);
            BtnContinue.TabIndex = 2;
            BtnContinue.Text = "CONTINUE";
            BtnContinue.UseVisualStyleBackColor = true;
            BtnContinue.Click += BtnContinue_Click;
            // 
            // LabelName
            // 
            LabelName.AutoSize = true;
            LabelName.Font = new Font("Consolas", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelName.ForeColor = Color.White;
            LabelName.Location = new Point(12, 9);
            LabelName.Name = "LabelName";
            LabelName.Size = new Size(104, 32);
            LabelName.TabIndex = 3;
            LabelName.Text = "SIMU16";
            // 
            // PanelRegisters
            // 
            PanelRegisters.BorderStyle = BorderStyle.FixedSingle;
            PanelRegisters.Controls.Add(LabelCF);
            PanelRegisters.Controls.Add(LabelTagCF);
            PanelRegisters.Controls.Add(DivRegisters3);
            PanelRegisters.Controls.Add(LabelEF);
            PanelRegisters.Controls.Add(LabelTagEF);
            PanelRegisters.Controls.Add(LabelIP);
            PanelRegisters.Controls.Add(LabelTagIP);
            PanelRegisters.Controls.Add(LabelBP);
            PanelRegisters.Controls.Add(LabelTagBP);
            PanelRegisters.Controls.Add(LabelSP);
            PanelRegisters.Controls.Add(LabelTagSP);
            PanelRegisters.Controls.Add(LabelRD);
            PanelRegisters.Controls.Add(LabelTagRD);
            PanelRegisters.Controls.Add(LabelRS);
            PanelRegisters.Controls.Add(LabelTagRS);
            PanelRegisters.Controls.Add(LabelRC);
            PanelRegisters.Controls.Add(DivRegisters2);
            PanelRegisters.Controls.Add(LabelTagRC);
            PanelRegisters.Controls.Add(LabelRegisters);
            PanelRegisters.Controls.Add(LabelRB);
            PanelRegisters.Controls.Add(LabelTagRB);
            PanelRegisters.Controls.Add(LabelRA);
            PanelRegisters.Controls.Add(LabelTagRA);
            PanelRegisters.Controls.Add(LabelR9);
            PanelRegisters.Controls.Add(LabelTagR9);
            PanelRegisters.Controls.Add(LabelR8);
            PanelRegisters.Controls.Add(LabelTagR8);
            PanelRegisters.Controls.Add(LabelR7);
            PanelRegisters.Controls.Add(LabelTagR7);
            PanelRegisters.Controls.Add(LabelR6);
            PanelRegisters.Controls.Add(LabelTagR6);
            PanelRegisters.Controls.Add(LabelR5);
            PanelRegisters.Controls.Add(LabelTagR5);
            PanelRegisters.Controls.Add(LabelR4);
            PanelRegisters.Controls.Add(LabelTagR4);
            PanelRegisters.Controls.Add(LabelR3);
            PanelRegisters.Controls.Add(LabelTagR3);
            PanelRegisters.Controls.Add(LabelR2);
            PanelRegisters.Controls.Add(LabelTagR2);
            PanelRegisters.Controls.Add(LabelR1);
            PanelRegisters.Controls.Add(LabelTagR1);
            PanelRegisters.Controls.Add(LabelR0);
            PanelRegisters.Controls.Add(DivRegisters);
            PanelRegisters.Controls.Add(LabelTagR0);
            PanelRegisters.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            PanelRegisters.ForeColor = Color.White;
            PanelRegisters.Location = new Point(12, 58);
            PanelRegisters.Name = "PanelRegisters";
            PanelRegisters.Size = new Size(168, 253);
            PanelRegisters.TabIndex = 4;
            // 
            // LabelCF
            // 
            LabelCF.AutoSize = true;
            LabelCF.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelCF.ForeColor = Color.Silver;
            LabelCF.Location = new Point(120, 157);
            LabelCF.Name = "LabelCF";
            LabelCF.Size = new Size(40, 18);
            LabelCF.TabIndex = 42;
            LabelCF.Text = "0000";
            // 
            // LabelTagCF
            // 
            LabelTagCF.AutoSize = true;
            LabelTagCF.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagCF.Location = new Point(85, 157);
            LabelTagCF.Name = "LabelTagCF";
            LabelTagCF.Size = new Size(24, 18);
            LabelTagCF.TabIndex = 41;
            LabelTagCF.Text = "CF";
            // 
            // DivRegisters3
            // 
            DivRegisters3.BorderStyle = BorderStyle.FixedSingle;
            DivRegisters3.Location = new Point(83, 32);
            DivRegisters3.Name = "DivRegisters3";
            DivRegisters3.Size = new Size(1, 300);
            DivRegisters3.TabIndex = 40;
            DivRegisters3.TabStop = false;
            // 
            // LabelEF
            // 
            LabelEF.AutoSize = true;
            LabelEF.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelEF.ForeColor = Color.Silver;
            LabelEF.Location = new Point(120, 139);
            LabelEF.Name = "LabelEF";
            LabelEF.Size = new Size(40, 18);
            LabelEF.TabIndex = 39;
            LabelEF.Text = "0000";
            // 
            // LabelTagEF
            // 
            LabelTagEF.AutoSize = true;
            LabelTagEF.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagEF.Location = new Point(85, 139);
            LabelTagEF.Name = "LabelTagEF";
            LabelTagEF.Size = new Size(24, 18);
            LabelTagEF.TabIndex = 38;
            LabelTagEF.Text = "EF";
            // 
            // LabelIP
            // 
            LabelIP.AutoSize = true;
            LabelIP.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelIP.ForeColor = Color.Silver;
            LabelIP.Location = new Point(120, 121);
            LabelIP.Name = "LabelIP";
            LabelIP.Size = new Size(40, 18);
            LabelIP.TabIndex = 37;
            LabelIP.Text = "0000";
            // 
            // LabelTagIP
            // 
            LabelTagIP.AutoSize = true;
            LabelTagIP.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagIP.Location = new Point(85, 121);
            LabelTagIP.Name = "LabelTagIP";
            LabelTagIP.Size = new Size(24, 18);
            LabelTagIP.TabIndex = 36;
            LabelTagIP.Text = "IP";
            // 
            // LabelBP
            // 
            LabelBP.AutoSize = true;
            LabelBP.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelBP.ForeColor = Color.Silver;
            LabelBP.Location = new Point(120, 103);
            LabelBP.Name = "LabelBP";
            LabelBP.Size = new Size(40, 18);
            LabelBP.TabIndex = 35;
            LabelBP.Text = "0000";
            // 
            // LabelTagBP
            // 
            LabelTagBP.AutoSize = true;
            LabelTagBP.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagBP.Location = new Point(85, 103);
            LabelTagBP.Name = "LabelTagBP";
            LabelTagBP.Size = new Size(24, 18);
            LabelTagBP.TabIndex = 34;
            LabelTagBP.Text = "BP";
            // 
            // LabelSP
            // 
            LabelSP.AutoSize = true;
            LabelSP.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelSP.ForeColor = Color.Silver;
            LabelSP.Location = new Point(120, 85);
            LabelSP.Name = "LabelSP";
            LabelSP.Size = new Size(40, 18);
            LabelSP.TabIndex = 33;
            LabelSP.Text = "0000";
            // 
            // LabelTagSP
            // 
            LabelTagSP.AutoSize = true;
            LabelTagSP.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagSP.Location = new Point(85, 85);
            LabelTagSP.Name = "LabelTagSP";
            LabelTagSP.Size = new Size(24, 18);
            LabelTagSP.TabIndex = 32;
            LabelTagSP.Text = "SP";
            // 
            // LabelRD
            // 
            LabelRD.AutoSize = true;
            LabelRD.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelRD.ForeColor = Color.Silver;
            LabelRD.Location = new Point(120, 67);
            LabelRD.Name = "LabelRD";
            LabelRD.Size = new Size(40, 18);
            LabelRD.TabIndex = 31;
            LabelRD.Text = "0000";
            // 
            // LabelTagRD
            // 
            LabelTagRD.AutoSize = true;
            LabelTagRD.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagRD.Location = new Point(85, 67);
            LabelTagRD.Name = "LabelTagRD";
            LabelTagRD.Size = new Size(24, 18);
            LabelTagRD.TabIndex = 30;
            LabelTagRD.Text = "RD";
            // 
            // LabelRS
            // 
            LabelRS.AutoSize = true;
            LabelRS.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelRS.ForeColor = Color.Silver;
            LabelRS.Location = new Point(120, 49);
            LabelRS.Name = "LabelRS";
            LabelRS.Size = new Size(40, 18);
            LabelRS.TabIndex = 29;
            LabelRS.Text = "0000";
            // 
            // LabelTagRS
            // 
            LabelTagRS.AutoSize = true;
            LabelTagRS.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagRS.Location = new Point(85, 49);
            LabelTagRS.Name = "LabelTagRS";
            LabelTagRS.Size = new Size(24, 18);
            LabelTagRS.TabIndex = 28;
            LabelTagRS.Text = "RS";
            // 
            // LabelRC
            // 
            LabelRC.AutoSize = true;
            LabelRC.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelRC.ForeColor = Color.Silver;
            LabelRC.Location = new Point(120, 31);
            LabelRC.Name = "LabelRC";
            LabelRC.Size = new Size(40, 18);
            LabelRC.TabIndex = 27;
            LabelRC.Text = "0000";
            // 
            // DivRegisters2
            // 
            DivRegisters2.BorderStyle = BorderStyle.FixedSingle;
            DivRegisters2.Location = new Point(111, 32);
            DivRegisters2.Name = "DivRegisters2";
            DivRegisters2.Size = new Size(1, 300);
            DivRegisters2.TabIndex = 26;
            DivRegisters2.TabStop = false;
            // 
            // LabelTagRC
            // 
            LabelTagRC.AutoSize = true;
            LabelTagRC.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagRC.Location = new Point(85, 31);
            LabelTagRC.Name = "LabelTagRC";
            LabelTagRC.Size = new Size(24, 18);
            LabelTagRC.TabIndex = 25;
            LabelTagRC.Text = "RC";
            // 
            // LabelRegisters
            // 
            LabelRegisters.AutoSize = true;
            LabelRegisters.Font = new Font("Consolas", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LabelRegisters.ForeColor = Color.Magenta;
            LabelRegisters.Location = new Point(3, 3);
            LabelRegisters.Name = "LabelRegisters";
            LabelRegisters.Size = new Size(80, 18);
            LabelRegisters.TabIndex = 5;
            LabelRegisters.Text = "REGISTERS";
            // 
            // LabelRB
            // 
            LabelRB.AutoSize = true;
            LabelRB.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelRB.ForeColor = Color.Silver;
            LabelRB.Location = new Point(39, 229);
            LabelRB.Name = "LabelRB";
            LabelRB.Size = new Size(40, 18);
            LabelRB.TabIndex = 24;
            LabelRB.Text = "0000";
            // 
            // LabelTagRB
            // 
            LabelTagRB.AutoSize = true;
            LabelTagRB.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagRB.Location = new Point(4, 229);
            LabelTagRB.Name = "LabelTagRB";
            LabelTagRB.Size = new Size(24, 18);
            LabelTagRB.TabIndex = 23;
            LabelTagRB.Text = "RB";
            // 
            // LabelRA
            // 
            LabelRA.AutoSize = true;
            LabelRA.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelRA.ForeColor = Color.Silver;
            LabelRA.Location = new Point(39, 211);
            LabelRA.Name = "LabelRA";
            LabelRA.Size = new Size(40, 18);
            LabelRA.TabIndex = 22;
            LabelRA.Text = "0000";
            // 
            // LabelTagRA
            // 
            LabelTagRA.AutoSize = true;
            LabelTagRA.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagRA.Location = new Point(4, 211);
            LabelTagRA.Name = "LabelTagRA";
            LabelTagRA.Size = new Size(24, 18);
            LabelTagRA.TabIndex = 21;
            LabelTagRA.Text = "RA";
            // 
            // LabelR9
            // 
            LabelR9.AutoSize = true;
            LabelR9.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelR9.ForeColor = Color.Silver;
            LabelR9.Location = new Point(39, 193);
            LabelR9.Name = "LabelR9";
            LabelR9.Size = new Size(40, 18);
            LabelR9.TabIndex = 20;
            LabelR9.Text = "0000";
            // 
            // LabelTagR9
            // 
            LabelTagR9.AutoSize = true;
            LabelTagR9.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagR9.Location = new Point(4, 193);
            LabelTagR9.Name = "LabelTagR9";
            LabelTagR9.Size = new Size(24, 18);
            LabelTagR9.TabIndex = 19;
            LabelTagR9.Text = "R9";
            // 
            // LabelR8
            // 
            LabelR8.AutoSize = true;
            LabelR8.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelR8.ForeColor = Color.Silver;
            LabelR8.Location = new Point(39, 175);
            LabelR8.Name = "LabelR8";
            LabelR8.Size = new Size(40, 18);
            LabelR8.TabIndex = 18;
            LabelR8.Text = "0000";
            // 
            // LabelTagR8
            // 
            LabelTagR8.AutoSize = true;
            LabelTagR8.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagR8.Location = new Point(4, 175);
            LabelTagR8.Name = "LabelTagR8";
            LabelTagR8.Size = new Size(24, 18);
            LabelTagR8.TabIndex = 17;
            LabelTagR8.Text = "R8";
            // 
            // LabelR7
            // 
            LabelR7.AutoSize = true;
            LabelR7.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelR7.ForeColor = Color.Silver;
            LabelR7.Location = new Point(39, 157);
            LabelR7.Name = "LabelR7";
            LabelR7.Size = new Size(40, 18);
            LabelR7.TabIndex = 16;
            LabelR7.Text = "0000";
            // 
            // LabelTagR7
            // 
            LabelTagR7.AutoSize = true;
            LabelTagR7.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagR7.Location = new Point(4, 157);
            LabelTagR7.Name = "LabelTagR7";
            LabelTagR7.Size = new Size(24, 18);
            LabelTagR7.TabIndex = 15;
            LabelTagR7.Text = "R7";
            // 
            // LabelR6
            // 
            LabelR6.AutoSize = true;
            LabelR6.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelR6.ForeColor = Color.Silver;
            LabelR6.Location = new Point(39, 139);
            LabelR6.Name = "LabelR6";
            LabelR6.Size = new Size(40, 18);
            LabelR6.TabIndex = 14;
            LabelR6.Text = "0000";
            // 
            // LabelTagR6
            // 
            LabelTagR6.AutoSize = true;
            LabelTagR6.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagR6.Location = new Point(4, 139);
            LabelTagR6.Name = "LabelTagR6";
            LabelTagR6.Size = new Size(24, 18);
            LabelTagR6.TabIndex = 13;
            LabelTagR6.Text = "R6";
            // 
            // LabelR5
            // 
            LabelR5.AutoSize = true;
            LabelR5.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelR5.ForeColor = Color.Silver;
            LabelR5.Location = new Point(39, 121);
            LabelR5.Name = "LabelR5";
            LabelR5.Size = new Size(40, 18);
            LabelR5.TabIndex = 12;
            LabelR5.Text = "0000";
            // 
            // LabelTagR5
            // 
            LabelTagR5.AutoSize = true;
            LabelTagR5.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagR5.Location = new Point(4, 121);
            LabelTagR5.Name = "LabelTagR5";
            LabelTagR5.Size = new Size(24, 18);
            LabelTagR5.TabIndex = 11;
            LabelTagR5.Text = "R5";
            // 
            // LabelR4
            // 
            LabelR4.AutoSize = true;
            LabelR4.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelR4.ForeColor = Color.Silver;
            LabelR4.Location = new Point(39, 103);
            LabelR4.Name = "LabelR4";
            LabelR4.Size = new Size(40, 18);
            LabelR4.TabIndex = 10;
            LabelR4.Text = "0000";
            // 
            // LabelTagR4
            // 
            LabelTagR4.AutoSize = true;
            LabelTagR4.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagR4.Location = new Point(4, 103);
            LabelTagR4.Name = "LabelTagR4";
            LabelTagR4.Size = new Size(24, 18);
            LabelTagR4.TabIndex = 9;
            LabelTagR4.Text = "R4";
            // 
            // LabelR3
            // 
            LabelR3.AutoSize = true;
            LabelR3.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelR3.ForeColor = Color.Silver;
            LabelR3.Location = new Point(39, 85);
            LabelR3.Name = "LabelR3";
            LabelR3.Size = new Size(40, 18);
            LabelR3.TabIndex = 8;
            LabelR3.Text = "0000";
            // 
            // LabelTagR3
            // 
            LabelTagR3.AutoSize = true;
            LabelTagR3.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagR3.Location = new Point(4, 85);
            LabelTagR3.Name = "LabelTagR3";
            LabelTagR3.Size = new Size(24, 18);
            LabelTagR3.TabIndex = 7;
            LabelTagR3.Text = "R3";
            // 
            // LabelR2
            // 
            LabelR2.AutoSize = true;
            LabelR2.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelR2.ForeColor = Color.Silver;
            LabelR2.Location = new Point(39, 67);
            LabelR2.Name = "LabelR2";
            LabelR2.Size = new Size(40, 18);
            LabelR2.TabIndex = 6;
            LabelR2.Text = "0000";
            // 
            // LabelTagR2
            // 
            LabelTagR2.AutoSize = true;
            LabelTagR2.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagR2.Location = new Point(4, 67);
            LabelTagR2.Name = "LabelTagR2";
            LabelTagR2.Size = new Size(24, 18);
            LabelTagR2.TabIndex = 5;
            LabelTagR2.Text = "R2";
            // 
            // LabelR1
            // 
            LabelR1.AutoSize = true;
            LabelR1.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelR1.ForeColor = Color.Silver;
            LabelR1.Location = new Point(39, 49);
            LabelR1.Name = "LabelR1";
            LabelR1.Size = new Size(40, 18);
            LabelR1.TabIndex = 4;
            LabelR1.Text = "0000";
            // 
            // LabelTagR1
            // 
            LabelTagR1.AutoSize = true;
            LabelTagR1.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagR1.Location = new Point(4, 49);
            LabelTagR1.Name = "LabelTagR1";
            LabelTagR1.Size = new Size(24, 18);
            LabelTagR1.TabIndex = 3;
            LabelTagR1.Text = "R1";
            // 
            // LabelR0
            // 
            LabelR0.AutoSize = true;
            LabelR0.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelR0.ForeColor = Color.Silver;
            LabelR0.Location = new Point(39, 31);
            LabelR0.Name = "LabelR0";
            LabelR0.Size = new Size(40, 18);
            LabelR0.TabIndex = 2;
            LabelR0.Text = "0000";
            // 
            // DivRegisters
            // 
            DivRegisters.BorderStyle = BorderStyle.FixedSingle;
            DivRegisters.Location = new Point(30, 32);
            DivRegisters.Name = "DivRegisters";
            DivRegisters.Size = new Size(1, 300);
            DivRegisters.TabIndex = 1;
            DivRegisters.TabStop = false;
            // 
            // LabelTagR0
            // 
            LabelTagR0.AutoSize = true;
            LabelTagR0.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagR0.Location = new Point(4, 31);
            LabelTagR0.Name = "LabelTagR0";
            LabelTagR0.Size = new Size(24, 18);
            LabelTagR0.TabIndex = 0;
            LabelTagR0.Text = "R0";
            // 
            // PanelMemory
            // 
            PanelMemory.BorderStyle = BorderStyle.FixedSingle;
            PanelMemory.Controls.Add(LabelBytes);
            PanelMemory.Controls.Add(LabelSize);
            PanelMemory.Controls.Add(TxtSize);
            PanelMemory.Controls.Add(LabelOffset);
            PanelMemory.Controls.Add(TxtOffset);
            PanelMemory.Controls.Add(LabelMemory);
            PanelMemory.Controls.Add(PanelHex);
            PanelMemory.Controls.Add(BtnHexDump);
            PanelMemory.Location = new Point(186, 58);
            PanelMemory.Name = "PanelMemory";
            PanelMemory.Size = new Size(576, 253);
            PanelMemory.TabIndex = 5;
            // 
            // LabelBytes
            // 
            LabelBytes.AutoSize = true;
            LabelBytes.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelBytes.ForeColor = Color.Gray;
            LabelBytes.Location = new Point(372, 224);
            LabelBytes.Name = "LabelBytes";
            LabelBytes.Size = new Size(37, 13);
            LabelBytes.TabIndex = 31;
            LabelBytes.Text = "BYTES";
            // 
            // LabelSize
            // 
            LabelSize.AutoSize = true;
            LabelSize.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelSize.ForeColor = Color.White;
            LabelSize.Location = new Point(250, 222);
            LabelSize.Name = "LabelSize";
            LabelSize.Size = new Size(40, 18);
            LabelSize.TabIndex = 29;
            LabelSize.Text = "SIZE";
            // 
            // TxtSize
            // 
            TxtSize.BackColor = Color.FromArgb(24, 24, 24);
            TxtSize.BorderStyle = BorderStyle.FixedSingle;
            TxtSize.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            TxtSize.ForeColor = Color.White;
            TxtSize.Location = new Point(296, 220);
            TxtSize.Name = "TxtSize";
            TxtSize.Size = new Size(70, 22);
            TxtSize.TabIndex = 30;
            TxtSize.Text = "256";
            // 
            // LabelOffset
            // 
            LabelOffset.AutoSize = true;
            LabelOffset.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelOffset.ForeColor = Color.White;
            LabelOffset.Location = new Point(11, 222);
            LabelOffset.Name = "LabelOffset";
            LabelOffset.Size = new Size(56, 18);
            LabelOffset.TabIndex = 15;
            LabelOffset.Text = "OFFSET";
            // 
            // TxtOffset
            // 
            TxtOffset.BackColor = Color.FromArgb(24, 24, 24);
            TxtOffset.BorderStyle = BorderStyle.FixedSingle;
            TxtOffset.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            TxtOffset.ForeColor = Color.White;
            TxtOffset.Location = new Point(73, 220);
            TxtOffset.Name = "TxtOffset";
            TxtOffset.Size = new Size(171, 22);
            TxtOffset.TabIndex = 28;
            TxtOffset.Text = "0x0000";
            // 
            // LabelMemory
            // 
            LabelMemory.AutoSize = true;
            LabelMemory.Font = new Font("Consolas", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LabelMemory.ForeColor = Color.Magenta;
            LabelMemory.Location = new Point(9, 3);
            LabelMemory.Name = "LabelMemory";
            LabelMemory.Size = new Size(56, 18);
            LabelMemory.TabIndex = 25;
            LabelMemory.Text = "MEMORY";
            // 
            // PanelHex
            // 
            PanelHex.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PanelHex.BorderStyle = BorderStyle.FixedSingle;
            PanelHex.Controls.Add(TxtHex);
            PanelHex.Location = new Point(13, 32);
            PanelHex.Name = "PanelHex";
            PanelHex.Size = new Size(549, 179);
            PanelHex.TabIndex = 6;
            // 
            // TxtHex
            // 
            TxtHex.BackColor = Color.FromArgb(24, 24, 24);
            TxtHex.BorderStyle = BorderStyle.None;
            TxtHex.Dock = DockStyle.Fill;
            TxtHex.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            TxtHex.ForeColor = Color.White;
            TxtHex.Location = new Point(0, 0);
            TxtHex.Name = "TxtHex";
            TxtHex.Size = new Size(547, 177);
            TxtHex.TabIndex = 0;
            TxtHex.Text = "0000:0000  00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00  ................";
            // 
            // BtnHexDump
            // 
            BtnHexDump.FlatAppearance.BorderColor = Color.Silver;
            BtnHexDump.FlatStyle = FlatStyle.Flat;
            BtnHexDump.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnHexDump.ForeColor = Color.White;
            BtnHexDump.Location = new Point(487, 218);
            BtnHexDump.Name = "BtnHexDump";
            BtnHexDump.Size = new Size(75, 23);
            BtnHexDump.TabIndex = 6;
            BtnHexDump.Text = "DUMP";
            BtnHexDump.UseVisualStyleBackColor = true;
            BtnHexDump.Click += BtnHexDump_Click;
            // 
            // TimerUpdate
            // 
            TimerUpdate.Enabled = true;
            TimerUpdate.Interval = 1;
            TimerUpdate.Tick += TimerUpdate_Tick;
            // 
            // PanelFlags
            // 
            PanelFlags.BorderStyle = BorderStyle.FixedSingle;
            PanelFlags.Controls.Add(LabelIE);
            PanelFlags.Controls.Add(LabelTagIE);
            PanelFlags.Controls.Add(LabelIDTR);
            PanelFlags.Controls.Add(LabelTagIDTR);
            PanelFlags.Controls.Add(DivFlags2);
            PanelFlags.Controls.Add(DivFlags3);
            PanelFlags.Controls.Add(LabelFlags);
            PanelFlags.Controls.Add(LabelBankIndex);
            PanelFlags.Controls.Add(LabelTagBankIndex);
            PanelFlags.Controls.Add(LabelHalt);
            PanelFlags.Controls.Add(LabelTagHalt);
            PanelFlags.Controls.Add(LabelBankEnable);
            PanelFlags.Controls.Add(LabelTagBankEnable);
            PanelFlags.Controls.Add(LabelNegative);
            PanelFlags.Controls.Add(LabelTagNegative);
            PanelFlags.Controls.Add(LabelZero);
            PanelFlags.Controls.Add(LabelTagZero);
            PanelFlags.Controls.Add(LabelCarry);
            PanelFlags.Controls.Add(DivFlags);
            PanelFlags.Controls.Add(LabelTagCarry);
            PanelFlags.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            PanelFlags.ForeColor = Color.White;
            PanelFlags.Location = new Point(12, 317);
            PanelFlags.Name = "PanelFlags";
            PanelFlags.Size = new Size(168, 145);
            PanelFlags.TabIndex = 25;
            // 
            // LabelIE
            // 
            LabelIE.AutoSize = true;
            LabelIE.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelIE.ForeColor = Color.Red;
            LabelIE.Location = new Point(39, 121);
            LabelIE.Name = "LabelIE";
            LabelIE.Size = new Size(16, 18);
            LabelIE.TabIndex = 48;
            LabelIE.Text = "0";
            // 
            // LabelTagIE
            // 
            LabelTagIE.AutoSize = true;
            LabelTagIE.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagIE.Location = new Point(4, 121);
            LabelTagIE.Name = "LabelTagIE";
            LabelTagIE.Size = new Size(24, 18);
            LabelTagIE.TabIndex = 47;
            LabelTagIE.Text = "IE";
            // 
            // LabelIDTR
            // 
            LabelIDTR.AutoSize = true;
            LabelIDTR.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelIDTR.ForeColor = Color.Silver;
            LabelIDTR.Location = new Point(138, 49);
            LabelIDTR.Name = "LabelIDTR";
            LabelIDTR.Size = new Size(24, 18);
            LabelIDTR.TabIndex = 46;
            LabelIDTR.Text = "00";
            // 
            // LabelTagIDTR
            // 
            LabelTagIDTR.AutoSize = true;
            LabelTagIDTR.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagIDTR.Location = new Point(85, 49);
            LabelTagIDTR.Name = "LabelTagIDTR";
            LabelTagIDTR.Size = new Size(40, 18);
            LabelTagIDTR.TabIndex = 45;
            LabelTagIDTR.Text = "IDTR";
            // 
            // DivFlags2
            // 
            DivFlags2.BorderStyle = BorderStyle.FixedSingle;
            DivFlags2.Location = new Point(130, 32);
            DivFlags2.Name = "DivFlags2";
            DivFlags2.Size = new Size(1, 300);
            DivFlags2.TabIndex = 44;
            DivFlags2.TabStop = false;
            // 
            // DivFlags3
            // 
            DivFlags3.BorderStyle = BorderStyle.FixedSingle;
            DivFlags3.Location = new Point(65, 32);
            DivFlags3.Name = "DivFlags3";
            DivFlags3.Size = new Size(1, 300);
            DivFlags3.TabIndex = 41;
            DivFlags3.TabStop = false;
            // 
            // LabelFlags
            // 
            LabelFlags.AutoSize = true;
            LabelFlags.Font = new Font("Consolas", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LabelFlags.ForeColor = Color.Magenta;
            LabelFlags.Location = new Point(3, 3);
            LabelFlags.Name = "LabelFlags";
            LabelFlags.Size = new Size(48, 18);
            LabelFlags.TabIndex = 5;
            LabelFlags.Text = "FLAGS";
            // 
            // LabelBankIndex
            // 
            LabelBankIndex.AutoSize = true;
            LabelBankIndex.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelBankIndex.ForeColor = Color.Silver;
            LabelBankIndex.Location = new Point(138, 31);
            LabelBankIndex.Name = "LabelBankIndex";
            LabelBankIndex.Size = new Size(24, 18);
            LabelBankIndex.TabIndex = 12;
            LabelBankIndex.Text = "00";
            // 
            // LabelTagBankIndex
            // 
            LabelTagBankIndex.AutoSize = true;
            LabelTagBankIndex.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagBankIndex.Location = new Point(100, 31);
            LabelTagBankIndex.Name = "LabelTagBankIndex";
            LabelTagBankIndex.Size = new Size(24, 18);
            LabelTagBankIndex.TabIndex = 11;
            LabelTagBankIndex.Text = "BI";
            // 
            // LabelHalt
            // 
            LabelHalt.AutoSize = true;
            LabelHalt.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelHalt.ForeColor = Color.Red;
            LabelHalt.Location = new Point(39, 103);
            LabelHalt.Name = "LabelHalt";
            LabelHalt.Size = new Size(16, 18);
            LabelHalt.TabIndex = 10;
            LabelHalt.Text = "0";
            // 
            // LabelTagHalt
            // 
            LabelTagHalt.AutoSize = true;
            LabelTagHalt.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagHalt.Location = new Point(4, 103);
            LabelTagHalt.Name = "LabelTagHalt";
            LabelTagHalt.Size = new Size(16, 18);
            LabelTagHalt.TabIndex = 9;
            LabelTagHalt.Text = "H";
            // 
            // LabelBankEnable
            // 
            LabelBankEnable.AutoSize = true;
            LabelBankEnable.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelBankEnable.ForeColor = Color.Red;
            LabelBankEnable.Location = new Point(39, 85);
            LabelBankEnable.Name = "LabelBankEnable";
            LabelBankEnable.Size = new Size(16, 18);
            LabelBankEnable.TabIndex = 8;
            LabelBankEnable.Text = "0";
            // 
            // LabelTagBankEnable
            // 
            LabelTagBankEnable.AutoSize = true;
            LabelTagBankEnable.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagBankEnable.Location = new Point(4, 85);
            LabelTagBankEnable.Name = "LabelTagBankEnable";
            LabelTagBankEnable.Size = new Size(24, 18);
            LabelTagBankEnable.TabIndex = 7;
            LabelTagBankEnable.Text = "BE";
            // 
            // LabelNegative
            // 
            LabelNegative.AutoSize = true;
            LabelNegative.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelNegative.ForeColor = Color.Red;
            LabelNegative.Location = new Point(39, 67);
            LabelNegative.Name = "LabelNegative";
            LabelNegative.Size = new Size(16, 18);
            LabelNegative.TabIndex = 6;
            LabelNegative.Text = "0";
            // 
            // LabelTagNegative
            // 
            LabelTagNegative.AutoSize = true;
            LabelTagNegative.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagNegative.Location = new Point(4, 67);
            LabelTagNegative.Name = "LabelTagNegative";
            LabelTagNegative.Size = new Size(16, 18);
            LabelTagNegative.TabIndex = 5;
            LabelTagNegative.Text = "N";
            // 
            // LabelZero
            // 
            LabelZero.AutoSize = true;
            LabelZero.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelZero.ForeColor = Color.Red;
            LabelZero.Location = new Point(39, 49);
            LabelZero.Name = "LabelZero";
            LabelZero.Size = new Size(16, 18);
            LabelZero.TabIndex = 4;
            LabelZero.Text = "0";
            // 
            // LabelTagZero
            // 
            LabelTagZero.AutoSize = true;
            LabelTagZero.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagZero.Location = new Point(4, 49);
            LabelTagZero.Name = "LabelTagZero";
            LabelTagZero.Size = new Size(16, 18);
            LabelTagZero.TabIndex = 3;
            LabelTagZero.Text = "Z";
            // 
            // LabelCarry
            // 
            LabelCarry.AutoSize = true;
            LabelCarry.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelCarry.ForeColor = Color.Red;
            LabelCarry.Location = new Point(39, 31);
            LabelCarry.Name = "LabelCarry";
            LabelCarry.Size = new Size(16, 18);
            LabelCarry.TabIndex = 2;
            LabelCarry.Text = "0";
            // 
            // DivFlags
            // 
            DivFlags.BorderStyle = BorderStyle.FixedSingle;
            DivFlags.Location = new Point(30, 32);
            DivFlags.Name = "DivFlags";
            DivFlags.Size = new Size(1, 300);
            DivFlags.TabIndex = 1;
            DivFlags.TabStop = false;
            // 
            // LabelTagCarry
            // 
            LabelTagCarry.AutoSize = true;
            LabelTagCarry.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagCarry.Location = new Point(4, 31);
            LabelTagCarry.Name = "LabelTagCarry";
            LabelTagCarry.Size = new Size(16, 18);
            LabelTagCarry.TabIndex = 0;
            LabelTagCarry.Text = "C";
            // 
            // PanelPorts
            // 
            PanelPorts.BorderStyle = BorderStyle.FixedSingle;
            PanelPorts.Controls.Add(LabelPortWr);
            PanelPorts.Controls.Add(LabelTagPortWr);
            PanelPorts.Controls.Add(ComboPorts);
            PanelPorts.Controls.Add(LabelPortVal);
            PanelPorts.Controls.Add(LabelTagPortVal);
            PanelPorts.Controls.Add(LabelPort);
            PanelPorts.Controls.Add(DivPorts);
            PanelPorts.Controls.Add(LabelTagPort);
            PanelPorts.Controls.Add(LabelPorts);
            PanelPorts.Location = new Point(186, 317);
            PanelPorts.Name = "PanelPorts";
            PanelPorts.Size = new Size(372, 145);
            PanelPorts.TabIndex = 26;
            // 
            // LabelPortWr
            // 
            LabelPortWr.AutoSize = true;
            LabelPortWr.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelPortWr.ForeColor = Color.Silver;
            LabelPortWr.Location = new Point(73, 103);
            LabelPortWr.Name = "LabelPortWr";
            LabelPortWr.Size = new Size(16, 18);
            LabelPortWr.TabIndex = 14;
            LabelPortWr.Text = "0";
            // 
            // LabelTagPortWr
            // 
            LabelTagPortWr.AutoSize = true;
            LabelTagPortWr.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagPortWr.ForeColor = Color.White;
            LabelTagPortWr.Location = new Point(12, 103);
            LabelTagPortWr.Name = "LabelTagPortWr";
            LabelTagPortWr.Size = new Size(48, 18);
            LabelTagPortWr.TabIndex = 13;
            LabelTagPortWr.Text = "WRITE";
            // 
            // ComboPorts
            // 
            ComboPorts.BackColor = Color.FromArgb(24, 24, 24);
            ComboPorts.FlatStyle = FlatStyle.Flat;
            ComboPorts.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ComboPorts.ForeColor = Color.White;
            ComboPorts.FormattingEnabled = true;
            ComboPorts.Location = new Point(9, 26);
            ComboPorts.Name = "ComboPorts";
            ComboPorts.Size = new Size(349, 22);
            ComboPorts.TabIndex = 12;
            ComboPorts.Text = "COM1_CMD";
            ComboPorts.SelectedIndexChanged += ComboPorts_SelectedIndexChanged;
            // 
            // LabelPortVal
            // 
            LabelPortVal.AutoSize = true;
            LabelPortVal.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelPortVal.ForeColor = Color.Silver;
            LabelPortVal.Location = new Point(73, 85);
            LabelPortVal.Name = "LabelPortVal";
            LabelPortVal.Size = new Size(40, 18);
            LabelPortVal.TabIndex = 11;
            LabelPortVal.Text = "0000";
            // 
            // LabelTagPortVal
            // 
            LabelTagPortVal.AutoSize = true;
            LabelTagPortVal.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagPortVal.ForeColor = Color.White;
            LabelTagPortVal.Location = new Point(12, 85);
            LabelTagPortVal.Name = "LabelTagPortVal";
            LabelTagPortVal.Size = new Size(48, 18);
            LabelTagPortVal.TabIndex = 10;
            LabelTagPortVal.Text = "VALUE";
            // 
            // LabelPort
            // 
            LabelPort.AutoSize = true;
            LabelPort.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelPort.ForeColor = Color.Silver;
            LabelPort.Location = new Point(73, 68);
            LabelPort.Name = "LabelPort";
            LabelPort.Size = new Size(40, 18);
            LabelPort.TabIndex = 9;
            LabelPort.Text = "0000";
            // 
            // DivPorts
            // 
            DivPorts.BorderStyle = BorderStyle.FixedSingle;
            DivPorts.Location = new Point(66, 69);
            DivPorts.Name = "DivPorts";
            DivPorts.Size = new Size(1, 300);
            DivPorts.TabIndex = 8;
            DivPorts.TabStop = false;
            // 
            // LabelTagPort
            // 
            LabelTagPort.AutoSize = true;
            LabelTagPort.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTagPort.ForeColor = Color.White;
            LabelTagPort.Location = new Point(20, 67);
            LabelTagPort.Name = "LabelTagPort";
            LabelTagPort.Size = new Size(40, 18);
            LabelTagPort.TabIndex = 7;
            LabelTagPort.Text = "PORT";
            // 
            // LabelPorts
            // 
            LabelPorts.AutoSize = true;
            LabelPorts.Font = new Font("Consolas", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LabelPorts.ForeColor = Color.Magenta;
            LabelPorts.Location = new Point(9, 3);
            LabelPorts.Name = "LabelPorts";
            LabelPorts.Size = new Size(80, 18);
            LabelPorts.TabIndex = 6;
            LabelPorts.Text = "I/O PORTS";
            // 
            // PanelUtil
            // 
            PanelUtil.BorderStyle = BorderStyle.FixedSingle;
            PanelUtil.Controls.Add(BtnScreenshot);
            PanelUtil.Controls.Add(BtnSaveVRAM);
            PanelUtil.Controls.Add(BtnSaveRAM);
            PanelUtil.Controls.Add(BtnLoadROM);
            PanelUtil.Controls.Add(BtnTTY);
            PanelUtil.Controls.Add(LabelUtil);
            PanelUtil.Location = new Point(564, 317);
            PanelUtil.Name = "PanelUtil";
            PanelUtil.Size = new Size(198, 145);
            PanelUtil.TabIndex = 27;
            // 
            // BtnScreenshot
            // 
            BtnScreenshot.FlatAppearance.BorderColor = Color.Silver;
            BtnScreenshot.FlatStyle = FlatStyle.Flat;
            BtnScreenshot.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnScreenshot.ForeColor = Color.White;
            BtnScreenshot.Location = new Point(6, 59);
            BtnScreenshot.Name = "BtnScreenshot";
            BtnScreenshot.Size = new Size(75, 23);
            BtnScreenshot.TabIndex = 31;
            BtnScreenshot.Text = "PRTSC";
            BtnScreenshot.UseVisualStyleBackColor = true;
            // 
            // BtnSaveVRAM
            // 
            BtnSaveVRAM.FlatAppearance.BorderColor = Color.Silver;
            BtnSaveVRAM.FlatStyle = FlatStyle.Flat;
            BtnSaveVRAM.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSaveVRAM.ForeColor = Color.White;
            BtnSaveVRAM.Location = new Point(87, 88);
            BtnSaveVRAM.Name = "BtnSaveVRAM";
            BtnSaveVRAM.Size = new Size(103, 23);
            BtnSaveVRAM.TabIndex = 30;
            BtnSaveVRAM.Text = "SAVE VRAM";
            BtnSaveVRAM.UseVisualStyleBackColor = true;
            // 
            // BtnSaveRAM
            // 
            BtnSaveRAM.FlatAppearance.BorderColor = Color.Silver;
            BtnSaveRAM.FlatStyle = FlatStyle.Flat;
            BtnSaveRAM.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSaveRAM.ForeColor = Color.White;
            BtnSaveRAM.Location = new Point(87, 59);
            BtnSaveRAM.Name = "BtnSaveRAM";
            BtnSaveRAM.Size = new Size(103, 23);
            BtnSaveRAM.TabIndex = 29;
            BtnSaveRAM.Text = "SAVE RAM";
            BtnSaveRAM.UseVisualStyleBackColor = true;
            // 
            // BtnLoadROM
            // 
            BtnLoadROM.FlatAppearance.BorderColor = Color.Silver;
            BtnLoadROM.FlatStyle = FlatStyle.Flat;
            BtnLoadROM.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnLoadROM.ForeColor = Color.White;
            BtnLoadROM.Location = new Point(87, 30);
            BtnLoadROM.Name = "BtnLoadROM";
            BtnLoadROM.Size = new Size(103, 23);
            BtnLoadROM.TabIndex = 28;
            BtnLoadROM.Text = "LOAD BIOS";
            BtnLoadROM.UseVisualStyleBackColor = true;
            // 
            // BtnTTY
            // 
            BtnTTY.FlatAppearance.BorderColor = Color.Silver;
            BtnTTY.FlatStyle = FlatStyle.Flat;
            BtnTTY.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnTTY.ForeColor = Color.White;
            BtnTTY.Location = new Point(6, 30);
            BtnTTY.Name = "BtnTTY";
            BtnTTY.Size = new Size(75, 23);
            BtnTTY.TabIndex = 27;
            BtnTTY.Text = "DEBUGGER";
            BtnTTY.UseVisualStyleBackColor = true;
            BtnTTY.Click += BtnTTY_Click;
            // 
            // LabelUtil
            // 
            LabelUtil.AutoSize = true;
            LabelUtil.Font = new Font("Consolas", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LabelUtil.ForeColor = Color.Magenta;
            LabelUtil.Location = new Point(9, 4);
            LabelUtil.Name = "LabelUtil";
            LabelUtil.Size = new Size(40, 18);
            LabelUtil.TabIndex = 26;
            LabelUtil.Text = "UTIL";
            // 
            // LabelFreq
            // 
            LabelFreq.AutoSize = true;
            LabelFreq.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelFreq.ForeColor = Color.White;
            LabelFreq.Location = new Point(386, 18);
            LabelFreq.Name = "LabelFreq";
            LabelFreq.Size = new Size(40, 18);
            LabelFreq.TabIndex = 29;
            LabelFreq.Text = "FREQ";
            // 
            // TxtFreq
            // 
            TxtFreq.BackColor = Color.FromArgb(24, 24, 24);
            TxtFreq.BorderStyle = BorderStyle.FixedSingle;
            TxtFreq.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            TxtFreq.ForeColor = Color.White;
            TxtFreq.Location = new Point(432, 16);
            TxtFreq.Name = "TxtFreq";
            TxtFreq.Size = new Size(73, 22);
            TxtFreq.TabIndex = 30;
            TxtFreq.Text = "0";
            // 
            // BtnSetFreq
            // 
            BtnSetFreq.FlatAppearance.BorderColor = Color.Silver;
            BtnSetFreq.FlatStyle = FlatStyle.Flat;
            BtnSetFreq.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSetFreq.ForeColor = Color.White;
            BtnSetFreq.Location = new Point(511, 16);
            BtnSetFreq.Name = "BtnSetFreq";
            BtnSetFreq.Size = new Size(45, 21);
            BtnSetFreq.TabIndex = 31;
            BtnSetFreq.Text = "SET";
            BtnSetFreq.UseVisualStyleBackColor = true;
            BtnSetFreq.Click += BtnSetFreq_Click;
            // 
            // LabelTPS
            // 
            LabelTPS.AutoSize = true;
            LabelTPS.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            LabelTPS.ForeColor = Color.White;
            LabelTPS.Location = new Point(564, 16);
            LabelTPS.Name = "LabelTPS";
            LabelTPS.Size = new Size(56, 18);
            LabelTPS.TabIndex = 32;
            LabelTPS.Text = "TPS: 0";
            // 
            // DebugForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(814, 475);
            Controls.Add(LabelTPS);
            Controls.Add(BtnSetFreq);
            Controls.Add(LabelFreq);
            Controls.Add(TxtFreq);
            Controls.Add(PanelUtil);
            Controls.Add(PanelPorts);
            Controls.Add(PanelFlags);
            Controls.Add(PanelMemory);
            Controls.Add(PanelRegisters);
            Controls.Add(LabelName);
            Controls.Add(BtnContinue);
            Controls.Add(BtnStep);
            Controls.Add(BtnReset);
            Name = "DebugForm";
            Text = "Simu16 Debugger";
            FormClosing += DebugForm_FormClosing;
            Load += DebugForm_Load;
            PanelRegisters.ResumeLayout(false);
            PanelRegisters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DivRegisters3).EndInit();
            ((System.ComponentModel.ISupportInitialize)DivRegisters2).EndInit();
            ((System.ComponentModel.ISupportInitialize)DivRegisters).EndInit();
            PanelMemory.ResumeLayout(false);
            PanelMemory.PerformLayout();
            PanelHex.ResumeLayout(false);
            PanelFlags.ResumeLayout(false);
            PanelFlags.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DivFlags2).EndInit();
            ((System.ComponentModel.ISupportInitialize)DivFlags3).EndInit();
            ((System.ComponentModel.ISupportInitialize)DivFlags).EndInit();
            PanelPorts.ResumeLayout(false);
            PanelPorts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DivPorts).EndInit();
            PanelUtil.ResumeLayout(false);
            PanelUtil.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnReset;
        private Button BtnStep;
        private Button BtnContinue;
        private Label LabelName;
        private Panel PanelRegisters;
        private PictureBox DivRegisters;
        private Label LabelTagR0;
        private Label LabelRB;
        private Label LabelTagRB;
        private Label LabelRA;
        private Label LabelTagRA;
        private Label LabelR9;
        private Label LabelTagR9;
        private Label LabelR8;
        private Label LabelTagR8;
        private Label LabelR7;
        private Label LabelTagR7;
        private Label LabelR6;
        private Label LabelTagR6;
        private Label LabelR5;
        private Label LabelTagR5;
        private Label LabelR4;
        private Label LabelTagR4;
        private Label LabelR3;
        private Label LabelTagR3;
        private Label LabelR2;
        private Label LabelTagR2;
        private Label LabelR1;
        private Label LabelTagR1;
        private Label LabelR0;
        private Label LabelRegisters;
        private Panel PanelMemory;
        private Panel PanelHex;
        private RichTextBox TxtHex;
        private Label LabelMemory;
        private Timer TimerUpdate;
        private Button BtnHexDump;
        private Panel PanelFlags;
        private Label LabelFlags;
        private Label LabelBankIndex;
        private Label LabelTagBankIndex;
        private Label LabelHalt;
        private Label LabelTagHalt;
        private Label LabelBankEnable;
        private Label LabelTagBankEnable;
        private Label LabelNegative;
        private Label LabelTagNegative;
        private Label LabelZero;
        private Label LabelTagZero;
        private Label LabelCarry;
        private PictureBox DivFlags;
        private Label LabelTagCarry;
        private Panel PanelPorts;
        private ComboBox ComboPorts;
        private Label LabelPortVal;
        private Label LabelTagPortVal;
        private Label LabelPort;
        private PictureBox DivPorts;
        private Label LabelTagPort;
        private Label LabelPorts;
        private Label LabelPortWr;
        private Label LabelTagPortWr;
        private Panel PanelUtil;
        private Button BtnScreenshot;
        private Button BtnSaveVRAM;
        private Button BtnSaveRAM;
        private Button BtnLoadROM;
        private Button BtnTTY;
        private Label LabelUtil;
        private Label LabelOffset;
        private TextBox TxtOffset;
        private Label LabelSize;
        private TextBox TxtSize;
        private Label LabelBytes;
        private Label LabelFreq;
        private TextBox TxtFreq;
        private Button BtnSetFreq;
        private PictureBox DivRegisters3;
        private Label LabelEF;
        private Label LabelTagEF;
        private Label LabelIP;
        private Label LabelTagIP;
        private Label LabelBP;
        private Label LabelTagBP;
        private Label LabelSP;
        private Label LabelTagSP;
        private Label LabelRD;
        private Label LabelTagRD;
        private Label LabelRS;
        private Label LabelTagRS;
        private Label LabelRC;
        private PictureBox DivRegisters2;
        private Label LabelTagRC;
        private Label LabelCF;
        private Label LabelTagCF;
        private Label LabelIDTR;
        private Label LabelTagIDTR;
        private PictureBox DivFlags2;
        private PictureBox DivFlags3;
        private Label LabelIE;
        private Label LabelTagIE;
        private Label LabelTPS;
    }
}