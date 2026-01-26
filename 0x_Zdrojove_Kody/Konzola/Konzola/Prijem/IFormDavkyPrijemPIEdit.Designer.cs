namespace Konzola.Prijem
{
    partial class FormDavkyPrijemPIEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_PONUMBER = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelNapoveda = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbNapoveda = new System.Windows.Forms.RichTextBox();
            this.textBox_SKL_ID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_QTYPACK = new System.Windows.Forms.TextBox();
            this.textBox_TIMEDONE = new System.Windows.Forms.TextBox();
            this.textBox_QTYSHPPDMJ = new System.Windows.Forms.TextBox();
            this.textBox_CZ_CarKod = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_LOCNCODE = new System.Windows.Forms.TextBox();
            this.textBox_DATEDONE = new System.Windows.Forms.TextBox();
            this.textBox_INPUT_MODE = new System.Windows.Forms.TextBox();
            this.textBox_VNDDOCNM = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_SERLTNUM = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox_CountEntries = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_MJ = new System.Windows.Forms.TextBox();
            this.textBox_DEX_ROW_ID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_REZ_1 = new System.Windows.Forms.TextBox();
            this.textBox_QTYSHPPD = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_VNDITNUM = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_USER_ID = new System.Windows.Forms.TextBox();
            this.textBox_ID_TERMINAL = new System.Windows.Forms.TextBox();
            this.textBox_REZ_2 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox_KOD_SW = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ORD = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_DAT_VYROBY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_ITEMNMBR = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelNapoveda.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Číslo dokladu:";
            // 
            // textBox_PONUMBER
            // 
            this.textBox_PONUMBER.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_PONUMBER.Location = new System.Drawing.Point(172, 58);
            this.textBox_PONUMBER.Name = "textBox_PONUMBER";
            this.textBox_PONUMBER.Size = new System.Drawing.Size(215, 20);
            this.textBox_PONUMBER.TabIndex = 1;
            this.textBox_PONUMBER.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 495);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(987, 71);
            this.panelButtons.TabIndex = 2;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(867, 71);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 71);
            this.buttonStorno.TabIndex = 1;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelNapoveda);
            this.panel2.Controls.Add(this.textBox_SKL_ID);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.textBox_QTYPACK);
            this.panel2.Controls.Add(this.textBox_TIMEDONE);
            this.panel2.Controls.Add(this.textBox_QTYSHPPDMJ);
            this.panel2.Controls.Add(this.textBox_CZ_CarKod);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textBox_LOCNCODE);
            this.panel2.Controls.Add(this.textBox_DATEDONE);
            this.panel2.Controls.Add(this.textBox_INPUT_MODE);
            this.panel2.Controls.Add(this.textBox_VNDDOCNM);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label23);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.textBox_SERLTNUM);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.textBox_CountEntries);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.textBox_MJ);
            this.panel2.Controls.Add(this.textBox_DEX_ROW_ID);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.textBox_REZ_1);
            this.panel2.Controls.Add(this.textBox_QTYSHPPD);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.textBox_VNDITNUM);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBox_USER_ID);
            this.panel2.Controls.Add(this.textBox_ID_TERMINAL);
            this.panel2.Controls.Add(this.textBox_REZ_2);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.textBox_KOD_SW);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textBox_ORD);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.textBox_PONUMBER);
            this.panel2.Controls.Add(this.textBox_DAT_VYROBY);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.textBox_ITEMNMBR);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(987, 495);
            this.panel2.TabIndex = 1;
            // 
            // panelNapoveda
            // 
            this.panelNapoveda.Controls.Add(this.groupBox1);
            this.panelNapoveda.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelNapoveda.Location = new System.Drawing.Point(0, 420);
            this.panelNapoveda.Name = "panelNapoveda";
            this.panelNapoveda.Size = new System.Drawing.Size(987, 75);
            this.panelNapoveda.TabIndex = 39;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNapoveda);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(987, 75);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nápověda";
            // 
            // tbNapoveda
            // 
            this.tbNapoveda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNapoveda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbNapoveda.Location = new System.Drawing.Point(3, 16);
            this.tbNapoveda.Name = "tbNapoveda";
            this.tbNapoveda.ReadOnly = true;
            this.tbNapoveda.Size = new System.Drawing.Size(981, 56);
            this.tbNapoveda.TabIndex = 0;
            this.tbNapoveda.TabStop = false;
            this.tbNapoveda.Text = "";
            // 
            // textBox_SKL_ID
            // 
            this.textBox_SKL_ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SKL_ID.Location = new System.Drawing.Point(171, 240);
            this.textBox_SKL_ID.Name = "textBox_SKL_ID";
            this.textBox_SKL_ID.Size = new System.Drawing.Size(215, 20);
            this.textBox_SKL_ID.TabIndex = 9;
            this.textBox_SKL_ID.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(99, 243);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "Číslo skladu:";
            // 
            // textBox_QTYPACK
            // 
            this.textBox_QTYPACK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_QTYPACK.Location = new System.Drawing.Point(171, 342);
            this.textBox_QTYPACK.Name = "textBox_QTYPACK";
            this.textBox_QTYPACK.Size = new System.Drawing.Size(215, 20);
            this.textBox_QTYPACK.TabIndex = 13;
            this.textBox_QTYPACK.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // textBox_TIMEDONE
            // 
            this.textBox_TIMEDONE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_TIMEDONE.Location = new System.Drawing.Point(605, 161);
            this.textBox_TIMEDONE.Name = "textBox_TIMEDONE";
            this.textBox_TIMEDONE.Size = new System.Drawing.Size(310, 20);
            this.textBox_TIMEDONE.TabIndex = 19;
            this.textBox_TIMEDONE.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // textBox_QTYSHPPDMJ
            // 
            this.textBox_QTYSHPPDMJ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_QTYSHPPDMJ.Location = new System.Drawing.Point(172, 110);
            this.textBox_QTYSHPPDMJ.Name = "textBox_QTYSHPPDMJ";
            this.textBox_QTYSHPPDMJ.Size = new System.Drawing.Size(215, 20);
            this.textBox_QTYSHPPDMJ.TabIndex = 4;
            this.textBox_QTYSHPPDMJ.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // textBox_CZ_CarKod
            // 
            this.textBox_CZ_CarKod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_CZ_CarKod.Location = new System.Drawing.Point(171, 214);
            this.textBox_CZ_CarKod.Name = "textBox_CZ_CarKod";
            this.textBox_CZ_CarKod.Size = new System.Drawing.Size(215, 20);
            this.textBox_CZ_CarKod.TabIndex = 8;
            this.textBox_CZ_CarKod.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(68, 345);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(99, 13);
            this.label19.TabIndex = 36;
            this.label19.Text = "Množství v balení :";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(570, 164);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(28, 13);
            this.label27.TabIndex = 38;
            this.label27.Text = "Čas:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(69, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Množství jednotek:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(68, 217);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Vlastní čárový kód:";
            // 
            // textBox_LOCNCODE
            // 
            this.textBox_LOCNCODE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_LOCNCODE.Location = new System.Drawing.Point(170, 264);
            this.textBox_LOCNCODE.Name = "textBox_LOCNCODE";
            this.textBox_LOCNCODE.Size = new System.Drawing.Size(216, 20);
            this.textBox_LOCNCODE.TabIndex = 10;
            this.textBox_LOCNCODE.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // textBox_DATEDONE
            // 
            this.textBox_DATEDONE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_DATEDONE.Location = new System.Drawing.Point(605, 135);
            this.textBox_DATEDONE.Name = "textBox_DATEDONE";
            this.textBox_DATEDONE.Size = new System.Drawing.Size(310, 20);
            this.textBox_DATEDONE.TabIndex = 18;
            this.textBox_DATEDONE.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // textBox_INPUT_MODE
            // 
            this.textBox_INPUT_MODE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_INPUT_MODE.Location = new System.Drawing.Point(605, 187);
            this.textBox_INPUT_MODE.Name = "textBox_INPUT_MODE";
            this.textBox_INPUT_MODE.Size = new System.Drawing.Size(310, 20);
            this.textBox_INPUT_MODE.TabIndex = 20;
            this.textBox_INPUT_MODE.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // textBox_VNDDOCNM
            // 
            this.textBox_VNDDOCNM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_VNDDOCNM.Location = new System.Drawing.Point(170, 136);
            this.textBox_VNDDOCNM.Name = "textBox_VNDDOCNM";
            this.textBox_VNDDOCNM.Size = new System.Drawing.Size(216, 20);
            this.textBox_VNDDOCNM.TabIndex = 5;
            this.textBox_VNDDOCNM.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(557, 139);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(41, 13);
            this.label24.TabIndex = 36;
            this.label24.Text = "Datum:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(121, 267);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(46, 13);
            this.label18.TabIndex = 4;
            this.label18.Text = "Lokace:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(523, 191);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(79, 13);
            this.label23.TabIndex = 4;
            this.label23.Text = "Způsob zápisu:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Číslo dokladu dodavatele:";
            // 
            // textBox_SERLTNUM
            // 
            this.textBox_SERLTNUM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SERLTNUM.Location = new System.Drawing.Point(604, 57);
            this.textBox_SERLTNUM.Name = "textBox_SERLTNUM";
            this.textBox_SERLTNUM.Size = new System.Drawing.Size(310, 20);
            this.textBox_SERLTNUM.TabIndex = 15;
            this.textBox_SERLTNUM.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(83, 293);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Měrná jednotka:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(542, 217);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 13);
            this.label21.TabIndex = 0;
            this.label21.Text = "Rezerva 1:";
            // 
            // textBox_CountEntries
            // 
            this.textBox_CountEntries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_CountEntries.Location = new System.Drawing.Point(171, 32);
            this.textBox_CountEntries.Name = "textBox_CountEntries";
            this.textBox_CountEntries.ReadOnly = true;
            this.textBox_CountEntries.Size = new System.Drawing.Size(216, 20);
            this.textBox_CountEntries.TabIndex = 0;
            this.textBox_CountEntries.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(64, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Čárový kód položky:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(525, 60);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "SN/výr. číslo:";
            // 
            // textBox_MJ
            // 
            this.textBox_MJ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_MJ.Location = new System.Drawing.Point(171, 290);
            this.textBox_MJ.Name = "textBox_MJ";
            this.textBox_MJ.Size = new System.Drawing.Size(215, 20);
            this.textBox_MJ.TabIndex = 11;
            this.textBox_MJ.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // textBox_DEX_ROW_ID
            // 
            this.textBox_DEX_ROW_ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_DEX_ROW_ID.Location = new System.Drawing.Point(604, 316);
            this.textBox_DEX_ROW_ID.Name = "textBox_DEX_ROW_ID";
            this.textBox_DEX_ROW_ID.ReadOnly = true;
            this.textBox_DEX_ROW_ID.Size = new System.Drawing.Size(310, 20);
            this.textBox_DEX_ROW_ID.TabIndex = 27;
            this.textBox_DEX_ROW_ID.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Číslo dávky:";
            // 
            // textBox_REZ_1
            // 
            this.textBox_REZ_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_REZ_1.Location = new System.Drawing.Point(605, 213);
            this.textBox_REZ_1.Name = "textBox_REZ_1";
            this.textBox_REZ_1.Size = new System.Drawing.Size(310, 20);
            this.textBox_REZ_1.TabIndex = 21;
            this.textBox_REZ_1.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // textBox_QTYSHPPD
            // 
            this.textBox_QTYSHPPD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_QTYSHPPD.Location = new System.Drawing.Point(171, 316);
            this.textBox_QTYSHPPD.Name = "textBox_QTYSHPPD";
            this.textBox_QTYSHPPD.Size = new System.Drawing.Size(215, 20);
            this.textBox_QTYSHPPD.TabIndex = 12;
            this.textBox_QTYSHPPD.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(520, 87);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Provedení SN:";
            // 
            // textBox_VNDITNUM
            // 
            this.textBox_VNDITNUM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_VNDITNUM.Location = new System.Drawing.Point(171, 162);
            this.textBox_VNDITNUM.Name = "textBox_VNDITNUM";
            this.textBox_VNDITNUM.Size = new System.Drawing.Size(215, 20);
            this.textBox_VNDITNUM.TabIndex = 6;
            this.textBox_VNDITNUM.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(462, 320);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(140, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Pořadí záznamu v databázi:";
            // 
            // textBox_USER_ID
            // 
            this.textBox_USER_ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_USER_ID.Location = new System.Drawing.Point(605, 290);
            this.textBox_USER_ID.Name = "textBox_USER_ID";
            this.textBox_USER_ID.Size = new System.Drawing.Size(310, 20);
            this.textBox_USER_ID.TabIndex = 22;
            this.textBox_USER_ID.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // textBox_ID_TERMINAL
            // 
            this.textBox_ID_TERMINAL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ID_TERMINAL.Location = new System.Drawing.Point(605, 264);
            this.textBox_ID_TERMINAL.Name = "textBox_ID_TERMINAL";
            this.textBox_ID_TERMINAL.Size = new System.Drawing.Size(310, 20);
            this.textBox_ID_TERMINAL.TabIndex = 22;
            this.textBox_ID_TERMINAL.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // textBox_REZ_2
            // 
            this.textBox_REZ_2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_REZ_2.Location = new System.Drawing.Point(605, 239);
            this.textBox_REZ_2.Name = "textBox_REZ_2";
            this.textBox_REZ_2.Size = new System.Drawing.Size(310, 20);
            this.textBox_REZ_2.TabIndex = 22;
            this.textBox_REZ_2.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(113, 319);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Množství:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(532, 293);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(66, 13);
            this.label22.TabIndex = 2;
            this.label22.Text = "ID uživatele:";
            // 
            // textBox_KOD_SW
            // 
            this.textBox_KOD_SW.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_KOD_SW.Location = new System.Drawing.Point(605, 83);
            this.textBox_KOD_SW.Name = "textBox_KOD_SW";
            this.textBox_KOD_SW.Size = new System.Drawing.Size(310, 20);
            this.textBox_KOD_SW.TabIndex = 16;
            this.textBox_KOD_SW.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(532, 267);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "ID terminálu:";
            // 
            // textBox_ORD
            // 
            this.textBox_ORD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ORD.Location = new System.Drawing.Point(171, 188);
            this.textBox_ORD.Name = "textBox_ORD";
            this.textBox_ORD.Size = new System.Drawing.Size(215, 20);
            this.textBox_ORD.TabIndex = 7;
            this.textBox_ORD.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(543, 243);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Rezerva 2:";
            // 
            // textBox_DAT_VYROBY
            // 
            this.textBox_DAT_VYROBY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_DAT_VYROBY.Location = new System.Drawing.Point(605, 109);
            this.textBox_DAT_VYROBY.Name = "textBox_DAT_VYROBY";
            this.textBox_DAT_VYROBY.Size = new System.Drawing.Size(310, 20);
            this.textBox_DAT_VYROBY.TabIndex = 17;
            this.textBox_DAT_VYROBY.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Číslo řádku dokladu:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(520, 113);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "¨Datum výroby:";
            // 
            // textBox_ITEMNMBR
            // 
            this.textBox_ITEMNMBR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ITEMNMBR.Location = new System.Drawing.Point(172, 84);
            this.textBox_ITEMNMBR.Name = "textBox_ITEMNMBR";
            this.textBox_ITEMNMBR.Size = new System.Drawing.Size(215, 20);
            this.textBox_ITEMNMBR.TabIndex = 2;
            this.textBox_ITEMNMBR.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Číslo položky:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormDavkyPrijemPIEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 566);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "FormDavkyPrijemPIEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nový uživatel";
            this.Load += new System.EventHandler(this.FormDavkyVydejeEdit_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormDavkyVydejeEdit_KeyDown_1);
            this.Resize += new System.EventHandler(this.FormDavkyVydejeEdit_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelNapoveda.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_PONUMBER;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.TextBox textBox_ITEMNMBR;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox textBox_CountEntries;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_QTYSHPPDMJ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_SKL_ID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_QTYPACK;
        private System.Windows.Forms.TextBox textBox_TIMEDONE;
        private System.Windows.Forms.TextBox textBox_CZ_CarKod;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_LOCNCODE;
        private System.Windows.Forms.TextBox textBox_DATEDONE;
        private System.Windows.Forms.TextBox textBox_INPUT_MODE;
        private System.Windows.Forms.TextBox textBox_VNDDOCNM;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_SERLTNUM;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox_MJ;
        private System.Windows.Forms.TextBox textBox_DEX_ROW_ID;
        private System.Windows.Forms.TextBox textBox_REZ_1;
        private System.Windows.Forms.TextBox textBox_QTYSHPPD;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox_VNDITNUM;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_REZ_2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_KOD_SW;
        private System.Windows.Forms.TextBox textBox_ORD;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_DAT_VYROBY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panelNapoveda;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tbNapoveda;
        private System.Windows.Forms.TextBox textBox_USER_ID;
        private System.Windows.Forms.TextBox textBox_ID_TERMINAL;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label4;
    }
}