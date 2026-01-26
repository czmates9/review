namespace FASK.SledovaniVyroby.Module.Pila
{
    partial class frmPila
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPila));
            this.label1 = new System.Windows.Forms.Label();
            this.txtProfileLength = new System.Windows.Forms.TextBox();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtItemNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonReadParams = new System.Windows.Forms.Button();
            this.buttonSendParams = new System.Windows.Forms.Button();
            this.serialPortIN = new System.IO.Ports.SerialPort(this.components);
            this.serialPortOUT = new System.IO.Ports.SerialPort(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonPrintParams = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSaveSPOUT = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.buttonStartStopOUT = new System.Windows.Forms.Button();
            this.txt_spoutParity = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_spoutDataBits = new System.Windows.Forms.TextBox();
            this.txt_spoutPort = new System.Windows.Forms.TextBox();
            this.txt_spoutStopBits = new System.Windows.Forms.TextBox();
            this.txt_spoutBaudRate = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonPrintServerSave = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.txt_template = new System.Windows.Forms.TextBox();
            this.num_timeout = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txt_printerName = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txt_printerAddress = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_timeout)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Délka profilu : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProfileLength
            // 
            this.txtProfileLength.Location = new System.Drawing.Point(133, 68);
            this.txtProfileLength.MaxLength = 10;
            this.txtProfileLength.Name = "txtProfileLength";
            this.txtProfileLength.Size = new System.Drawing.Size(91, 20);
            this.txtProfileLength.TabIndex = 4;
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Location = new System.Drawing.Point(133, 94);
            this.txtOrderNumber.MaxLength = 10;
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(91, 20);
            this.txtOrderNumber.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Èíslo zakázky : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtItemNumber
            // 
            this.txtItemNumber.Location = new System.Drawing.Point(133, 120);
            this.txtItemNumber.MaxLength = 10;
            this.txtItemNumber.Name = "txtItemNumber";
            this.txtItemNumber.Size = new System.Drawing.Size(91, 20);
            this.txtItemNumber.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Èíslo položky : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(133, 3);
            this.txtBarcode.MaxLength = 37;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(237, 20);
            this.txtBarcode.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "Èárový kód : ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonReadParams
            // 
            this.buttonReadParams.Location = new System.Drawing.Point(133, 27);
            this.buttonReadParams.Name = "buttonReadParams";
            this.buttonReadParams.Size = new System.Drawing.Size(237, 35);
            this.buttonReadParams.TabIndex = 2;
            this.buttonReadParams.Text = "Naèti parametry";
            this.buttonReadParams.UseVisualStyleBackColor = true;
            this.buttonReadParams.Click += new System.EventHandler(this.buttonReadParams_Click);
            // 
            // buttonSendParams
            // 
            this.buttonSendParams.Location = new System.Drawing.Point(133, 146);
            this.buttonSendParams.Name = "buttonSendParams";
            this.buttonSendParams.Size = new System.Drawing.Size(237, 32);
            this.buttonSendParams.TabIndex = 21;
            this.buttonSendParams.Text = "Odeslat parametry";
            this.buttonSendParams.UseVisualStyleBackColor = true;
            this.buttonSendParams.Click += new System.EventHandler(this.buttonSendParams_Click);
            // 
            // serialPortIN
            // 
            this.serialPortIN.ReadTimeout = 500;
            this.serialPortIN.WriteTimeout = 5000;
            this.serialPortIN.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortIN_DataReceived);
            // 
            // serialPortOUT
            // 
            this.serialPortOUT.ReadTimeout = 5000;
            this.serialPortOUT.WriteTimeout = 5000;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(392, 454);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonPrintParams);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.buttonSendParams);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.buttonReadParams);
            this.tabPage1.Controls.Add(this.txtProfileLength);
            this.tabPage1.Controls.Add(this.txtBarcode);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtOrderNumber);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtItemNumber);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(384, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonPrintParams
            // 
            this.buttonPrintParams.Location = new System.Drawing.Point(133, 184);
            this.buttonPrintParams.Name = "buttonPrintParams";
            this.buttonPrintParams.Size = new System.Drawing.Size(237, 32);
            this.buttonPrintParams.TabIndex = 22;
            this.buttonPrintParams.Text = "Tisknout parametry";
            this.buttonPrintParams.UseVisualStyleBackColor = true;
            this.buttonPrintParams.Click += new System.EventHandler(this.buttonPrintParams_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(384, 428);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Nastavení";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSaveSPOUT);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.buttonStartStopOUT);
            this.groupBox2.Controls.Add(this.txt_spoutParity);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txt_spoutDataBits);
            this.groupBox2.Controls.Add(this.txt_spoutPort);
            this.groupBox2.Controls.Add(this.txt_spoutStopBits);
            this.groupBox2.Controls.Add(this.txt_spoutBaudRate);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(189, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 286);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Výstup dat";
            // 
            // buttonSaveSPOUT
            // 
            this.buttonSaveSPOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveSPOUT.Location = new System.Drawing.Point(4, 257);
            this.buttonSaveSPOUT.Name = "buttonSaveSPOUT";
            this.buttonSaveSPOUT.Size = new System.Drawing.Size(68, 23);
            this.buttonSaveSPOUT.TabIndex = 10;
            this.buttonSaveSPOUT.Text = "Uložit";
            this.buttonSaveSPOUT.UseVisualStyleBackColor = true;
            this.buttonSaveSPOUT.Click += new System.EventHandler(this.buttonSaveSPOUT_Click_1);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(6, 116);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 20);
            this.label18.TabIndex = 8;
            this.label18.Text = "Parity : ";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonStartStopOUT
            // 
            this.buttonStartStopOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartStopOUT.Location = new System.Drawing.Point(78, 257);
            this.buttonStartStopOUT.Name = "buttonStartStopOUT";
            this.buttonStartStopOUT.Size = new System.Drawing.Size(91, 23);
            this.buttonStartStopOUT.TabIndex = 11;
            this.buttonStartStopOUT.Text = "Serial port OUT";
            this.buttonStartStopOUT.UseVisualStyleBackColor = true;
            this.buttonStartStopOUT.Click += new System.EventHandler(this.buttonStartStopOUT_Click_1);
            // 
            // txt_spoutParity
            // 
            this.txt_spoutParity.Location = new System.Drawing.Point(78, 117);
            this.txt_spoutParity.Name = "txt_spoutParity";
            this.txt_spoutParity.Size = new System.Drawing.Size(91, 20);
            this.txt_spoutParity.TabIndex = 9;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(6, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 20);
            this.label15.TabIndex = 0;
            this.label15.Text = "Serial port : ";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(4, 67);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(68, 20);
            this.label20.TabIndex = 4;
            this.label20.Text = "Data bits : ";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(6, 92);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 20);
            this.label17.TabIndex = 6;
            this.label17.Text = "Stop bits : ";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_spoutDataBits
            // 
            this.txt_spoutDataBits.Location = new System.Drawing.Point(78, 68);
            this.txt_spoutDataBits.Name = "txt_spoutDataBits";
            this.txt_spoutDataBits.Size = new System.Drawing.Size(91, 20);
            this.txt_spoutDataBits.TabIndex = 5;
            // 
            // txt_spoutPort
            // 
            this.txt_spoutPort.Location = new System.Drawing.Point(78, 18);
            this.txt_spoutPort.Name = "txt_spoutPort";
            this.txt_spoutPort.Size = new System.Drawing.Size(91, 20);
            this.txt_spoutPort.TabIndex = 1;
            // 
            // txt_spoutStopBits
            // 
            this.txt_spoutStopBits.Location = new System.Drawing.Point(78, 93);
            this.txt_spoutStopBits.Name = "txt_spoutStopBits";
            this.txt_spoutStopBits.Size = new System.Drawing.Size(91, 20);
            this.txt_spoutStopBits.TabIndex = 7;
            // 
            // txt_spoutBaudRate
            // 
            this.txt_spoutBaudRate.Location = new System.Drawing.Point(78, 44);
            this.txt_spoutBaudRate.Name = "txt_spoutBaudRate";
            this.txt_spoutBaudRate.Size = new System.Drawing.Size(91, 20);
            this.txt_spoutBaudRate.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(6, 43);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 20);
            this.label16.TabIndex = 2;
            this.label16.Text = "Baud rate : ";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.groupBox1.Size = new System.Drawing.Size(184, 286);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vstup dat ";
            // 
            // buttonSaveSPIN
            // 
            this.buttonSaveSPIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveSPIN.Location = new System.Drawing.Point(6, 257);
            this.buttonSaveSPIN.Name = "buttonSaveSPIN";
            this.buttonSaveSPIN.Size = new System.Drawing.Size(68, 23);
            this.buttonSaveSPIN.TabIndex = 10;
            this.buttonSaveSPIN.Text = "Uložit";
            this.buttonSaveSPIN.UseVisualStyleBackColor = true;
            this.buttonSaveSPIN.Click += new System.EventHandler(this.buttonSaveSPIN_Click_1);
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
            this.buttonStartStopIN.Location = new System.Drawing.Point(80, 257);
            this.buttonStartStopIN.Name = "buttonStartStopIN";
            this.buttonStartStopIN.Size = new System.Drawing.Size(91, 23);
            this.buttonStartStopIN.TabIndex = 11;
            this.buttonStartStopIN.Text = "Serial port IN";
            this.buttonStartStopIN.UseVisualStyleBackColor = true;
            this.buttonStartStopIN.Click += new System.EventHandler(this.buttonStartStopIN_Click_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonPrintServerSave);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.txt_template);
            this.groupBox3.Controls.Add(this.num_timeout);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.txt_printerName);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.txt_printerAddress);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(3, 289);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(378, 136);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tisk";
            // 
            // buttonPrintServerSave
            // 
            this.buttonPrintServerSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrintServerSave.Location = new System.Drawing.Point(303, 16);
            this.buttonPrintServerSave.Name = "buttonPrintServerSave";
            this.buttonPrintServerSave.Size = new System.Drawing.Size(68, 23);
            this.buttonPrintServerSave.TabIndex = 33;
            this.buttonPrintServerSave.Text = "Uložit";
            this.buttonPrintServerSave.UseVisualStyleBackColor = true;
            this.buttonPrintServerSave.Click += new System.EventHandler(this.buttonPrintServerSave_Click);
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(37, 20);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(68, 20);
            this.label27.TabIndex = 31;
            this.label27.Text = "Šablona : ";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_template
            // 
            this.txt_template.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_template.Location = new System.Drawing.Point(111, 19);
            this.txt_template.Name = "txt_template";
            this.txt_template.Size = new System.Drawing.Size(187, 20);
            this.txt_template.TabIndex = 32;
            // 
            // num_timeout
            // 
            this.num_timeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.num_timeout.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.num_timeout.Location = new System.Drawing.Point(112, 96);
            this.num_timeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.num_timeout.Name = "num_timeout";
            this.num_timeout.Size = new System.Drawing.Size(186, 20);
            this.num_timeout.TabIndex = 24;
            this.num_timeout.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.num_timeout.Visible = false;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(39, 97);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(68, 20);
            this.label24.TabIndex = 23;
            this.label24.Text = "Timeout : ";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label24.Visible = false;
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(7, 71);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(99, 20);
            this.label26.TabIndex = 16;
            this.label26.Text = "Jméno tiskárny : ";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label26.Visible = false;
            // 
            // txt_printerName
            // 
            this.txt_printerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_printerName.Location = new System.Drawing.Point(112, 72);
            this.txt_printerName.Name = "txt_printerName";
            this.txt_printerName.Size = new System.Drawing.Size(186, 20);
            this.txt_printerName.TabIndex = 17;
            this.txt_printerName.Visible = false;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(7, 47);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(99, 20);
            this.label23.TabIndex = 12;
            this.label23.Text = "Adresa tiskárny : ";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label23.Visible = false;
            // 
            // txt_printerAddress
            // 
            this.txt_printerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_printerAddress.Location = new System.Drawing.Point(112, 47);
            this.txt_printerAddress.Name = "txt_printerAddress";
            this.txt_printerAddress.Size = new System.Drawing.Size(186, 20);
            this.txt_printerAddress.TabIndex = 13;
            this.txt_printerAddress.Visible = false;
            // 
            // frmPila
            // 
            this.AcceptButton = this.buttonSendParams;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 454);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(408, 412);
            this.Name = "frmPila";
            this.Text = "Pila";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPila_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_timeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProfileLength;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtItemNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonReadParams;
        private System.Windows.Forms.Button buttonSendParams;
        private System.IO.Ports.SerialPort serialPortIN;
        private System.IO.Ports.SerialPort serialPortOUT;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonSaveSPOUT;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button buttonStartStopOUT;
        private System.Windows.Forms.TextBox txt_spoutParity;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_spoutDataBits;
        private System.Windows.Forms.TextBox txt_spoutPort;
        private System.Windows.Forms.TextBox txt_spoutStopBits;
        private System.Windows.Forms.TextBox txt_spoutBaudRate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSaveSPIN;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_spinParity;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_spinStopBits;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_spinDataBits;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_spinBaudRate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_spinPort;
        private System.Windows.Forms.Button buttonStartStopIN;
        private System.Windows.Forms.Button buttonPrintParams;
        private System.Windows.Forms.Button buttonPrintServerSave;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txt_template;
        private System.Windows.Forms.NumericUpDown num_timeout;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txt_printerName;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt_printerAddress;
    }
}