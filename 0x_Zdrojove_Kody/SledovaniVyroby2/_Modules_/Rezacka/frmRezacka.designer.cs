namespace FASK.SledovaniVyroby.Module.Rezacka
{
    partial class frmRezacka
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRezacka));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.serialPortIN = new System.IO.Ports.SerialPort(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabData = new System.Windows.Forms.TabPage();
            this.pnlOperations = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLoginOperations = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numBMDecimalPlaces = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numHistoryHeight = new System.Windows.Forms.NumericUpDown();
            this.btnSaveOther = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numNumberObHistooryRecords = new System.Windows.Forms.NumericUpDown();
            this.chbHistory = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSaveSPIN = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_spinParity = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_spinStopBits = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_spinDataBits = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_spinBaudRate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_spinPort = new System.Windows.Forms.TextBox();
            this.buttonStartStopIN = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonSaveSPOUT = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_spoutParity = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_spoutStopBits = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_spoutDataBits = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_spoutBaudRate = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_spoutPort = new System.Windows.Forms.TextBox();
            this.buttonStartStopOUT = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAssembly = new System.Windows.Forms.TextBox();
            this.btnLoadCounter = new System.Windows.Forms.Button();
            this.txtObject = new System.Windows.Forms.TextBox();
            this.btnSaveCounter = new System.Windows.Forms.Button();
            this.serialPortOUT = new System.IO.Ports.SerialPort(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.tabData.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBMDecimalPlaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHistoryHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNumberObHistooryRecords)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPortIN
            // 
            this.serialPortIN.ReadTimeout = 500;
            this.serialPortIN.WriteTimeout = 5000;
            this.serialPortIN.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortIN_DataReceived);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabData);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(955, 490);
            this.tabControl1.TabIndex = 0;
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.pnlOperations);
            this.tabData.Location = new System.Drawing.Point(4, 22);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(3);
            this.tabData.Size = new System.Drawing.Size(947, 464);
            this.tabData.TabIndex = 0;
            this.tabData.Text = "Data";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // pnlOperations
            // 
            this.pnlOperations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOperations.Location = new System.Drawing.Point(3, 3);
            this.pnlOperations.Name = "pnlOperations";
            this.pnlOperations.Size = new System.Drawing.Size(941, 458);
            this.pnlOperations.TabIndex = 70;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(947, 464);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Nastavení";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtLoginOperations);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.numBMDecimalPlaces);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.numHistoryHeight);
            this.groupBox3.Controls.Add(this.btnSaveOther);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numNumberObHistooryRecords);
            this.groupBox3.Controls.Add(this.chbHistory);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(181, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(585, 357);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            // 
            // txtLoginOperations
            // 
            this.txtLoginOperations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoginOperations.Location = new System.Drawing.Point(248, 115);
            this.txtLoginOperations.Name = "txtLoginOperations";
            this.txtLoginOperations.Size = new System.Drawing.Size(331, 20);
            this.txtLoginOperations.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(186, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Operace vyžadující zmìnu uživatele :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(236, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Poèet zobrazovaných desetinných míst pro BM :";
            // 
            // numBMDecimalPlaces
            // 
            this.numBMDecimalPlaces.Location = new System.Drawing.Point(248, 89);
            this.numBMDecimalPlaces.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numBMDecimalPlaces.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBMDecimalPlaces.Name = "numBMDecimalPlaces";
            this.numBMDecimalPlaces.Size = new System.Drawing.Size(52, 20);
            this.numBMDecimalPlaces.TabIndex = 14;
            this.numBMDecimalPlaces.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Šíøka historie operací :";
            // 
            // numHistoryHeight
            // 
            this.numHistoryHeight.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numHistoryHeight.Location = new System.Drawing.Point(248, 63);
            this.numHistoryHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numHistoryHeight.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numHistoryHeight.Name = "numHistoryHeight";
            this.numHistoryHeight.Size = new System.Drawing.Size(52, 20);
            this.numHistoryHeight.TabIndex = 12;
            this.numHistoryHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnSaveOther
            // 
            this.btnSaveOther.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveOther.Location = new System.Drawing.Point(511, 328);
            this.btnSaveOther.Name = "btnSaveOther";
            this.btnSaveOther.Size = new System.Drawing.Size(68, 23);
            this.btnSaveOther.TabIndex = 11;
            this.btnSaveOther.Text = "Uložit";
            this.btnSaveOther.UseVisualStyleBackColor = true;
            this.btnSaveOther.Click += new System.EventHandler(this.btnSaveOther_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Poèet zobrazovaných operací v historii :";
            // 
            // numNumberObHistooryRecords
            // 
            this.numNumberObHistooryRecords.Location = new System.Drawing.Point(248, 37);
            this.numNumberObHistooryRecords.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numNumberObHistooryRecords.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNumberObHistooryRecords.Name = "numNumberObHistooryRecords";
            this.numNumberObHistooryRecords.Size = new System.Drawing.Size(52, 20);
            this.numNumberObHistooryRecords.TabIndex = 1;
            this.numNumberObHistooryRecords.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chbHistory
            // 
            this.chbHistory.AutoSize = true;
            this.chbHistory.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbHistory.Location = new System.Drawing.Point(101, 17);
            this.chbHistory.Name = "chbHistory";
            this.chbHistory.Size = new System.Drawing.Size(161, 17);
            this.chbHistory.TabIndex = 0;
            this.chbHistory.Text = "Zobrazovat historii operací : ";
            this.chbHistory.UseVisualStyleBackColor = true;
            this.chbHistory.CheckedChanged += new System.EventHandler(this.chbHistory_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSaveSPIN);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txt_spinParity);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txt_spinStopBits);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txt_spinDataBits);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txt_spinBaudRate);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txt_spinPort);
            this.groupBox1.Controls.Add(this.buttonStartStopIN);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 357);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pøipojení scaneru";
            // 
            // buttonSaveSPIN
            // 
            this.buttonSaveSPIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveSPIN.Location = new System.Drawing.Point(8, 328);
            this.buttonSaveSPIN.Name = "buttonSaveSPIN";
            this.buttonSaveSPIN.Size = new System.Drawing.Size(68, 23);
            this.buttonSaveSPIN.TabIndex = 10;
            this.buttonSaveSPIN.Text = "Uložit";
            this.buttonSaveSPIN.UseVisualStyleBackColor = true;
            this.buttonSaveSPIN.Click += new System.EventHandler(this.buttonSaveSPIN_Click);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(6, 118);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 20);
            this.label14.TabIndex = 8;
            this.label14.Text = "Parity : ";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_spinParity
            // 
            this.txt_spinParity.Location = new System.Drawing.Point(80, 119);
            this.txt_spinParity.Name = "txt_spinParity";
            this.txt_spinParity.Size = new System.Drawing.Size(91, 20);
            this.txt_spinParity.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(6, 93);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 20);
            this.label13.TabIndex = 6;
            this.label13.Text = "Stop bits : ";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_spinStopBits
            // 
            this.txt_spinStopBits.Location = new System.Drawing.Point(80, 94);
            this.txt_spinStopBits.Name = "txt_spinStopBits";
            this.txt_spinStopBits.Size = new System.Drawing.Size(91, 20);
            this.txt_spinStopBits.TabIndex = 7;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(6, 67);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(68, 20);
            this.label19.TabIndex = 4;
            this.label19.Text = "Data bits : ";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_spinDataBits
            // 
            this.txt_spinDataBits.Location = new System.Drawing.Point(80, 68);
            this.txt_spinDataBits.Name = "txt_spinDataBits";
            this.txt_spinDataBits.Size = new System.Drawing.Size(91, 20);
            this.txt_spinDataBits.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(6, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "Baud rate : ";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_spinBaudRate
            // 
            this.txt_spinBaudRate.Location = new System.Drawing.Point(80, 44);
            this.txt_spinBaudRate.Name = "txt_spinBaudRate";
            this.txt_spinBaudRate.Size = new System.Drawing.Size(91, 20);
            this.txt_spinBaudRate.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(6, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "Serial port : ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_spinPort
            // 
            this.txt_spinPort.Location = new System.Drawing.Point(80, 18);
            this.txt_spinPort.Name = "txt_spinPort";
            this.txt_spinPort.Size = new System.Drawing.Size(91, 20);
            this.txt_spinPort.TabIndex = 1;
            // 
            // buttonStartStopIN
            // 
            this.buttonStartStopIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartStopIN.Location = new System.Drawing.Point(82, 328);
            this.buttonStartStopIN.Name = "buttonStartStopIN";
            this.buttonStartStopIN.Size = new System.Drawing.Size(91, 23);
            this.buttonStartStopIN.TabIndex = 11;
            this.buttonStartStopIN.Text = "Serial port";
            this.buttonStartStopIN.UseVisualStyleBackColor = true;
            this.buttonStartStopIN.Click += new System.EventHandler(this.buttonStartStopIN_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonSaveSPOUT);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.txt_spoutParity);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.txt_spoutStopBits);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.txt_spoutDataBits);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.txt_spoutBaudRate);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.txt_spoutPort);
            this.groupBox4.Controls.Add(this.buttonStartStopOUT);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox4.Location = new System.Drawing.Point(766, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(178, 357);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Pøipojení èítaèe";
            // 
            // buttonSaveSPOUT
            // 
            this.buttonSaveSPOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveSPOUT.Location = new System.Drawing.Point(8, 328);
            this.buttonSaveSPOUT.Name = "buttonSaveSPOUT";
            this.buttonSaveSPOUT.Size = new System.Drawing.Size(68, 23);
            this.buttonSaveSPOUT.TabIndex = 10;
            this.buttonSaveSPOUT.Text = "Uložit";
            this.buttonSaveSPOUT.UseVisualStyleBackColor = true;
            this.buttonSaveSPOUT.Click += new System.EventHandler(this.buttonSaveSPOUT_Click);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 20);
            this.label9.TabIndex = 8;
            this.label9.Text = "Parity : ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_spoutParity
            // 
            this.txt_spoutParity.Location = new System.Drawing.Point(80, 119);
            this.txt_spoutParity.Name = "txt_spoutParity";
            this.txt_spoutParity.Size = new System.Drawing.Size(91, 20);
            this.txt_spoutParity.TabIndex = 9;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(6, 93);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 20);
            this.label15.TabIndex = 6;
            this.label15.Text = "Stop bits : ";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_spoutStopBits
            // 
            this.txt_spoutStopBits.Location = new System.Drawing.Point(80, 94);
            this.txt_spoutStopBits.Name = "txt_spoutStopBits";
            this.txt_spoutStopBits.Size = new System.Drawing.Size(91, 20);
            this.txt_spoutStopBits.TabIndex = 7;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(6, 67);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 20);
            this.label16.TabIndex = 4;
            this.label16.Text = "Data bits : ";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_spoutDataBits
            // 
            this.txt_spoutDataBits.Location = new System.Drawing.Point(80, 68);
            this.txt_spoutDataBits.Name = "txt_spoutDataBits";
            this.txt_spoutDataBits.Size = new System.Drawing.Size(91, 20);
            this.txt_spoutDataBits.TabIndex = 5;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(6, 43);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(68, 20);
            this.label17.TabIndex = 2;
            this.label17.Text = "Baud rate : ";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_spoutBaudRate
            // 
            this.txt_spoutBaudRate.Location = new System.Drawing.Point(80, 44);
            this.txt_spoutBaudRate.Name = "txt_spoutBaudRate";
            this.txt_spoutBaudRate.Size = new System.Drawing.Size(91, 20);
            this.txt_spoutBaudRate.TabIndex = 3;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(6, 17);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 20);
            this.label18.TabIndex = 0;
            this.label18.Text = "Serial port : ";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_spoutPort
            // 
            this.txt_spoutPort.Location = new System.Drawing.Point(80, 18);
            this.txt_spoutPort.Name = "txt_spoutPort";
            this.txt_spoutPort.Size = new System.Drawing.Size(91, 20);
            this.txt_spoutPort.TabIndex = 1;
            // 
            // buttonStartStopOUT
            // 
            this.buttonStartStopOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartStopOUT.Location = new System.Drawing.Point(82, 328);
            this.buttonStartStopOUT.Name = "buttonStartStopOUT";
            this.buttonStartStopOUT.Size = new System.Drawing.Size(91, 23);
            this.buttonStartStopOUT.TabIndex = 11;
            this.buttonStartStopOUT.Text = "Serial port";
            this.buttonStartStopOUT.UseVisualStyleBackColor = true;
            this.buttonStartStopOUT.Click += new System.EventHandler(this.buttonStartStopOUT_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtAssembly);
            this.groupBox2.Controls.Add(this.btnLoadCounter);
            this.groupBox2.Controls.Add(this.txtObject);
            this.groupBox2.Controls.Add(this.btnSaveCounter);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 360);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(941, 101);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nastavení èítaèe";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "Objekt : ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Knihovna : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAssembly
            // 
            this.txtAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAssembly.Location = new System.Drawing.Point(80, 19);
            this.txtAssembly.Name = "txtAssembly";
            this.txtAssembly.Size = new System.Drawing.Size(853, 20);
            this.txtAssembly.TabIndex = 0;
            // 
            // btnLoadCounter
            // 
            this.btnLoadCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadCounter.Location = new System.Drawing.Point(845, 71);
            this.btnLoadCounter.Name = "btnLoadCounter";
            this.btnLoadCounter.Size = new System.Drawing.Size(88, 23);
            this.btnLoadCounter.TabIndex = 3;
            this.btnLoadCounter.Text = "Naèíst";
            this.btnLoadCounter.UseVisualStyleBackColor = true;
            this.btnLoadCounter.Click += new System.EventHandler(this.btnLoadCounter_Click_1);
            // 
            // txtObject
            // 
            this.txtObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObject.Location = new System.Drawing.Point(80, 45);
            this.txtObject.Name = "txtObject";
            this.txtObject.Size = new System.Drawing.Size(853, 20);
            this.txtObject.TabIndex = 1;
            // 
            // btnSaveCounter
            // 
            this.btnSaveCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveCounter.Location = new System.Drawing.Point(771, 71);
            this.btnSaveCounter.Name = "btnSaveCounter";
            this.btnSaveCounter.Size = new System.Drawing.Size(66, 23);
            this.btnSaveCounter.TabIndex = 2;
            this.btnSaveCounter.Text = "Uložit";
            this.btnSaveCounter.UseVisualStyleBackColor = true;
            this.btnSaveCounter.Click += new System.EventHandler(this.btnSaveCounter_Click_1);
            // 
            // serialPortOUT
            // 
            this.serialPortOUT.Handshake = System.IO.Ports.Handshake.RequestToSend;
            this.serialPortOUT.ReadTimeout = 500;
            this.serialPortOUT.WriteTimeout = 5000;
            this.serialPortOUT.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortOUT_DataReceived);
            // 
            // frmRezacka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 490);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRezacka";
            this.Text = "Øezaèka";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRezacka_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBMDecimalPlaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHistoryHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNumberObHistooryRecords)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPortIN;
        private System.IO.Ports.SerialPort serialPortOUT;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonStartStopIN;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_spinParity;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_spinStopBits;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_spinBaudRate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_spinPort;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_spinDataBits;
        private System.Windows.Forms.Button buttonSaveSPIN;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAssembly;
        private System.Windows.Forms.Button btnLoadCounter;
        private System.Windows.Forms.TextBox txtObject;
        private System.Windows.Forms.Button btnSaveCounter;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonSaveSPOUT;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_spoutParity;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_spoutStopBits;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_spoutDataBits;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_spoutBaudRate;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_spoutPort;
        private System.Windows.Forms.Button buttonStartStopOUT;
        private System.Windows.Forms.Panel pnlOperations;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numNumberObHistooryRecords;
        private System.Windows.Forms.CheckBox chbHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveOther;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numHistoryHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numBMDecimalPlaces;
        private System.Windows.Forms.TextBox txtLoginOperations;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}