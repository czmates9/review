namespace Module.DCDIdeal.InkJet
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxBarcodeOutput = new System.Windows.Forms.TextBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonReadParams = new System.Windows.Forms.Button();
            this.buttonSendParams = new System.Windows.Forms.Button();
            this.serialPortIN = new System.IO.Ports.SerialPort(this.components);
            this.serialPortOUT = new System.IO.Ports.SerialPort(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblData09 = new System.Windows.Forms.Label();
            this.lblData08 = new System.Windows.Forms.Label();
            this.lblData07 = new System.Windows.Forms.Label();
            this.lblData06 = new System.Windows.Forms.Label();
            this.lblData05 = new System.Windows.Forms.Label();
            this.lblData04 = new System.Windows.Forms.Label();
            this.lblData03 = new System.Windows.Forms.Label();
            this.lblData02 = new System.Windows.Forms.Label();
            this.lblData01 = new System.Windows.Forms.Label();
            this.lblDataValue = new System.Windows.Forms.Label();
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonBarcodeSaveOutput = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxBarcodeOutput
            // 
            this.textBoxBarcodeOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBarcodeOutput.Location = new System.Drawing.Point(9, 20);
            this.textBoxBarcodeOutput.Name = "textBoxBarcodeOutput";
            this.textBoxBarcodeOutput.Size = new System.Drawing.Size(500, 20);
            this.textBoxBarcodeOutput.TabIndex = 4;
            this.toolTip1.SetToolTip(this.textBoxBarcodeOutput, "n - n·zev programu\r\na - rozmÏr A\r\nb - rozmÏr B\r\nt - tlouöùka\r\nk - poËet kusu\r\nz -" +
        " zak·zka\r\np - pozice\r\no - po¯adÌ\r\n");
            // 
            // txtBarcode
            // 
            this.txtBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarcode.Location = new System.Drawing.Point(133, 3);
            this.txtBarcode.MaxLength = 0;
            this.txtBarcode.Multiline = true;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(407, 60);
            this.txtBarcode.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "»·rov˝ kÛd : ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonReadParams
            // 
            this.buttonReadParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReadParams.Location = new System.Drawing.Point(133, 69);
            this.buttonReadParams.Name = "buttonReadParams";
            this.buttonReadParams.Size = new System.Drawing.Size(407, 35);
            this.buttonReadParams.TabIndex = 2;
            this.buttonReadParams.Text = "NaËti parametry";
            this.buttonReadParams.UseVisualStyleBackColor = true;
            this.buttonReadParams.Click += new System.EventHandler(this.buttonReadParams_Click);
            // 
            // buttonSendParams
            // 
            this.buttonSendParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSendParams.Location = new System.Drawing.Point(133, 329);
            this.buttonSendParams.Name = "buttonSendParams";
            this.buttonSendParams.Size = new System.Drawing.Size(407, 32);
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
            this.tabControl1.Size = new System.Drawing.Size(602, 418);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblData09);
            this.tabPage1.Controls.Add(this.lblData08);
            this.tabPage1.Controls.Add(this.lblData07);
            this.tabPage1.Controls.Add(this.lblData06);
            this.tabPage1.Controls.Add(this.lblData05);
            this.tabPage1.Controls.Add(this.lblData04);
            this.tabPage1.Controls.Add(this.lblData03);
            this.tabPage1.Controls.Add(this.lblData02);
            this.tabPage1.Controls.Add(this.lblData01);
            this.tabPage1.Controls.Add(this.lblDataValue);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.buttonSendParams);
            this.tabPage1.Controls.Add(this.buttonReadParams);
            this.tabPage1.Controls.Add(this.txtBarcode);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(594, 392);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblData09
            // 
            this.lblData09.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblData09.BackColor = System.Drawing.Color.Azure;
            this.lblData09.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblData09.Location = new System.Drawing.Point(133, 292);
            this.lblData09.Name = "lblData09";
            this.lblData09.Size = new System.Drawing.Size(407, 19);
            this.lblData09.TabIndex = 22;
            // 
            // lblData08
            // 
            this.lblData08.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblData08.BackColor = System.Drawing.Color.Azure;
            this.lblData08.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblData08.Location = new System.Drawing.Point(133, 273);
            this.lblData08.Name = "lblData08";
            this.lblData08.Size = new System.Drawing.Size(407, 19);
            this.lblData08.TabIndex = 22;
            // 
            // lblData07
            // 
            this.lblData07.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblData07.BackColor = System.Drawing.Color.Azure;
            this.lblData07.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblData07.Location = new System.Drawing.Point(133, 254);
            this.lblData07.Name = "lblData07";
            this.lblData07.Size = new System.Drawing.Size(407, 19);
            this.lblData07.TabIndex = 22;
            // 
            // lblData06
            // 
            this.lblData06.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblData06.BackColor = System.Drawing.Color.Azure;
            this.lblData06.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblData06.Location = new System.Drawing.Point(133, 235);
            this.lblData06.Name = "lblData06";
            this.lblData06.Size = new System.Drawing.Size(407, 19);
            this.lblData06.TabIndex = 22;
            // 
            // lblData05
            // 
            this.lblData05.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblData05.BackColor = System.Drawing.Color.Azure;
            this.lblData05.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblData05.Location = new System.Drawing.Point(133, 216);
            this.lblData05.Name = "lblData05";
            this.lblData05.Size = new System.Drawing.Size(407, 19);
            this.lblData05.TabIndex = 22;
            // 
            // lblData04
            // 
            this.lblData04.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblData04.BackColor = System.Drawing.Color.Azure;
            this.lblData04.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblData04.Location = new System.Drawing.Point(133, 197);
            this.lblData04.Name = "lblData04";
            this.lblData04.Size = new System.Drawing.Size(407, 19);
            this.lblData04.TabIndex = 22;
            // 
            // lblData03
            // 
            this.lblData03.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblData03.BackColor = System.Drawing.Color.Azure;
            this.lblData03.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblData03.Location = new System.Drawing.Point(133, 178);
            this.lblData03.Name = "lblData03";
            this.lblData03.Size = new System.Drawing.Size(407, 19);
            this.lblData03.TabIndex = 22;
            // 
            // lblData02
            // 
            this.lblData02.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblData02.BackColor = System.Drawing.Color.Azure;
            this.lblData02.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblData02.Location = new System.Drawing.Point(133, 159);
            this.lblData02.Name = "lblData02";
            this.lblData02.Size = new System.Drawing.Size(407, 19);
            this.lblData02.TabIndex = 22;
            // 
            // lblData01
            // 
            this.lblData01.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblData01.BackColor = System.Drawing.Color.Azure;
            this.lblData01.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblData01.Location = new System.Drawing.Point(133, 140);
            this.lblData01.Name = "lblData01";
            this.lblData01.Size = new System.Drawing.Size(407, 19);
            this.lblData01.TabIndex = 22;
            // 
            // lblDataValue
            // 
            this.lblDataValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDataValue.BackColor = System.Drawing.Color.Azure;
            this.lblDataValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDataValue.Location = new System.Drawing.Point(133, 121);
            this.lblDataValue.Name = "lblDataValue";
            this.lblDataValue.Size = new System.Drawing.Size(407, 19);
            this.lblDataValue.TabIndex = 22;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(594, 392);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "NastavenÌ";
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
            this.groupBox2.Location = new System.Drawing.Point(399, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 343);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "V˝stup dat";
            // 
            // buttonSaveSPOUT
            // 
            this.buttonSaveSPOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveSPOUT.Location = new System.Drawing.Point(4, 314);
            this.buttonSaveSPOUT.Name = "buttonSaveSPOUT";
            this.buttonSaveSPOUT.Size = new System.Drawing.Size(68, 23);
            this.buttonSaveSPOUT.TabIndex = 10;
            this.buttonSaveSPOUT.Text = "Uloûit";
            this.buttonSaveSPOUT.UseVisualStyleBackColor = true;
            this.buttonSaveSPOUT.Click += new System.EventHandler(this.buttonSaveSPOUT_Click);
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
            this.buttonStartStopOUT.Location = new System.Drawing.Point(78, 314);
            this.buttonStartStopOUT.Name = "buttonStartStopOUT";
            this.buttonStartStopOUT.Size = new System.Drawing.Size(91, 23);
            this.buttonStartStopOUT.TabIndex = 11;
            this.buttonStartStopOUT.Text = "Serial port OUT";
            this.buttonStartStopOUT.UseVisualStyleBackColor = true;
            this.buttonStartStopOUT.Click += new System.EventHandler(this.buttonStartStopOUT_Click);
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
            this.groupBox1.Size = new System.Drawing.Size(184, 343);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vstup dat ";
            // 
            // buttonSaveSPIN
            // 
            this.buttonSaveSPIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveSPIN.Location = new System.Drawing.Point(6, 314);
            this.buttonSaveSPIN.Name = "buttonSaveSPIN";
            this.buttonSaveSPIN.Size = new System.Drawing.Size(68, 23);
            this.buttonSaveSPIN.TabIndex = 10;
            this.buttonSaveSPIN.Text = "Uloûit";
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
            this.buttonStartStopIN.Location = new System.Drawing.Point(80, 314);
            this.buttonStartStopIN.Name = "buttonStartStopIN";
            this.buttonStartStopIN.Size = new System.Drawing.Size(91, 23);
            this.buttonStartStopIN.TabIndex = 11;
            this.buttonStartStopIN.Text = "Serial port IN";
            this.buttonStartStopIN.UseVisualStyleBackColor = true;
            this.buttonStartStopIN.Click += new System.EventHandler(this.buttonStartStopIN_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonBarcodeSaveOutput);
            this.panel2.Controls.Add(this.textBoxBarcodeOutput);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 346);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(588, 43);
            this.panel2.TabIndex = 13;
            // 
            // buttonBarcodeSaveOutput
            // 
            this.buttonBarcodeSaveOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBarcodeSaveOutput.Location = new System.Drawing.Point(515, 11);
            this.buttonBarcodeSaveOutput.Name = "buttonBarcodeSaveOutput";
            this.buttonBarcodeSaveOutput.Size = new System.Drawing.Size(68, 23);
            this.buttonBarcodeSaveOutput.TabIndex = 12;
            this.buttonBarcodeSaveOutput.Text = "Uloûit";
            this.buttonBarcodeSaveOutput.UseVisualStyleBackColor = true;
            this.buttonBarcodeSaveOutput.Click += new System.EventHandler(this.buttonBarcodeSaveOutput_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(128, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "V˝stup - koncov˝ ¯etÏzec";
            // 
            // frmMain
            // 
            this.AcceptButton = this.buttonSendParams;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 418);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(408, 412);
            this.Name = "frmMain";
            this.Text = "InkJet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVrtacka_FormClosing);
            this.Load += new System.EventHandler(this.frmVrtacka_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonStartStopIN;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button buttonStartStopOUT;
        private System.Windows.Forms.TextBox txt_spoutParity;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_spoutPort;
        private System.Windows.Forms.TextBox txt_spoutStopBits;
        private System.Windows.Forms.TextBox txt_spoutBaudRate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_spinParity;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_spinStopBits;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_spinBaudRate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_spinPort;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_spoutDataBits;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_spinDataBits;
        private System.Windows.Forms.Button buttonSaveSPOUT;
        private System.Windows.Forms.Button buttonSaveSPIN;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonBarcodeSaveOutput;
        private System.Windows.Forms.TextBox textBoxBarcodeOutput;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblDataValue;
        private System.Windows.Forms.Label lblData07;
        private System.Windows.Forms.Label lblData06;
        private System.Windows.Forms.Label lblData05;
        private System.Windows.Forms.Label lblData04;
        private System.Windows.Forms.Label lblData03;
        private System.Windows.Forms.Label lblData02;
        private System.Windows.Forms.Label lblData01;
        private System.Windows.Forms.Label lblData08;
        private System.Windows.Forms.Label lblData09;
    }
}