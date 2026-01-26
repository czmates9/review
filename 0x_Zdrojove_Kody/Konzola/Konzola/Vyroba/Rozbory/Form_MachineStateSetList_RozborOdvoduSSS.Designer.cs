namespace Konzola.Vyroba.Rozbory
{
    partial class Form_MachineStateSetList_RozborOdvoduSSS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MachineStateSetList_RozborOdvoduSSS));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_Eventu_Count = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tB_S7 = new System.Windows.Forms.TextBox();
            this.tB_S11 = new System.Windows.Forms.TextBox();
            this.tB_S3 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tB_S5 = new System.Windows.Forms.TextBox();
            this.tB_S9 = new System.Windows.Forms.TextBox();
            this.tB_S1 = new System.Windows.Forms.TextBox();
            this.tB_S6 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tB_S10 = new System.Windows.Forms.TextBox();
            this.tB_S2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tB_S4 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tB_S8 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tB_S0 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cB_description = new System.Windows.Forms.ComboBox();
            this.tb_CisloSluzby = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtp_OD = new System.Windows.Forms.DateTimePicker();
            this.cb_TimeVariant = new System.Windows.Forms.ComboBox();
            this.dtp_DO = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.tsFiltry = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFiltry = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNastavit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZmena = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPridat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdebrat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVycistit = new System.Windows.Forms.ToolStripButton();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenuVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVybrat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiKonecVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonecList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExporty = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportyDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportyDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportyDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportyDoExceOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportyDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportyDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.bw_OdvodMachineStateSet = new System.ComponentModel.BackgroundWorker();
            this.dg_OdvodMachineStateSet = new Zuby.ADGV.AdvancedDataGridView();
            this.bs_OdvodMachineStateSet = new System.Windows.Forms.BindingSource(this.components);
            this.ds_OdvodMachineStateSet = new Fask.Interfaces.DataSets.Vyroba();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.ZDROJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STROJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s0DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s5DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s6DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s7DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s8DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s9DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s10DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s11DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counter_0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counter_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counter_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counter_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counter_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counter_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counter6DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counter7DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counter8DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counter9DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counter10DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.counter11DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Popis_pol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mnozstvi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountEntries = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPNUMBE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMMJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEMODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEPREPSTART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEPREPSTOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEPREP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEUNIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMESTART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMESTOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMECORSTART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMECORSTOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMECOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMECRID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMECRIDTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operationid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyReal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYPACK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYPACKMJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarcodeP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TermID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISOK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOUBEHGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CORRGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyOld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idVS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateedit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCNCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERLTNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXPIRATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NMBRPAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TYPEPAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STORNOGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REZ_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REZ_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REZ_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REZ_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REZ_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT_OLD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StrojSklad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StrojLokace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.tB_Zdroj = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tB_StrojSklad = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tB_StrojLokace = new System.Windows.Forms.TextBox();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_OdvodMachineStateSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_OdvodMachineStateSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_OdvodMachineStateSet)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButtonsZobrazeniVyber
            // 
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonKonec);
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonVybratUzivatele);
            this.panelButtonsZobrazeniVyber.Location = new System.Drawing.Point(1116, 0);
            this.panelButtonsZobrazeniVyber.Name = "panelButtonsZobrazeniVyber";
            this.panelButtonsZobrazeniVyber.Size = new System.Drawing.Size(84, 701);
            this.panelButtonsZobrazeniVyber.TabIndex = 2;
            // 
            // buttonKonec
            // 
            this.buttonKonec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKonec.Location = new System.Drawing.Point(6, 626);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(73, 63);
            this.buttonKonec.TabIndex = 4;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // buttonVybratUzivatele
            // 
            this.buttonVybratUzivatele.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVybratUzivatele.Location = new System.Drawing.Point(6, 24);
            this.buttonVybratUzivatele.Name = "buttonVybratUzivatele";
            this.buttonVybratUzivatele.Size = new System.Drawing.Size(73, 63);
            this.buttonVybratUzivatele.TabIndex = 2;
            this.buttonVybratUzivatele.Text = "Vybrat materiál";
            this.buttonVybratUzivatele.UseVisualStyleBackColor = true;
            this.buttonVybratUzivatele.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.progressIndicator1);
            this.panelMain.Controls.Add(this.dg_OdvodMachineStateSet);
            this.panelMain.Controls.Add(this.statusStrip1);
            this.panelMain.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Controls.Add(this.menuStrip2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1200, 701);
            this.panelMain.TabIndex = 1;
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(601, 467);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 38;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_Eventu_Count});
            this.statusStrip1.Location = new System.Drawing.Point(0, 679);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip1.TabIndex = 44;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_Eventu_Count
            // 
            this.tssl_Eventu_Count.Name = "tssl_Eventu_Count";
            this.tssl_Eventu_Count.Size = new System.Drawing.Size(118, 17);
            this.tssl_Eventu_Count.Text = "toolStripStatusLabel1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 264);
            this.panel1.TabIndex = 43;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tB_S7);
            this.groupBox2.Controls.Add(this.tB_S11);
            this.groupBox2.Controls.Add(this.tB_S3);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tB_S5);
            this.groupBox2.Controls.Add(this.tB_S9);
            this.groupBox2.Controls.Add(this.tB_S1);
            this.groupBox2.Controls.Add(this.tB_S6);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.tB_S10);
            this.groupBox2.Controls.Add(this.tB_S2);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.tB_S4);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tB_S8);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.tB_S0);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(536, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 131);
            this.groupBox2.TabIndex = 55;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vstupy";
            // 
            // tB_S7
            // 
            this.tB_S7.Location = new System.Drawing.Point(110, 94);
            this.tB_S7.Name = "tB_S7";
            this.tB_S7.Size = new System.Drawing.Size(32, 20);
            this.tB_S7.TabIndex = 58;
            // 
            // tB_S11
            // 
            this.tB_S11.Location = new System.Drawing.Point(183, 93);
            this.tB_S11.Name = "tB_S11";
            this.tB_S11.Size = new System.Drawing.Size(32, 20);
            this.tB_S11.TabIndex = 58;
            // 
            // tB_S3
            // 
            this.tB_S3.Location = new System.Drawing.Point(39, 94);
            this.tB_S3.Name = "tB_S3";
            this.tB_S3.Size = new System.Drawing.Size(32, 20);
            this.tB_S3.TabIndex = 58;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(84, 97);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 13);
            this.label14.TabIndex = 57;
            this.label14.Text = "S7";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(157, 96);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(26, 13);
            this.label21.TabIndex = 57;
            this.label21.Text = "S11";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 57;
            this.label10.Text = "S3";
            // 
            // tB_S5
            // 
            this.tB_S5.Location = new System.Drawing.Point(110, 43);
            this.tB_S5.Name = "tB_S5";
            this.tB_S5.Size = new System.Drawing.Size(32, 20);
            this.tB_S5.TabIndex = 58;
            // 
            // tB_S9
            // 
            this.tB_S9.Location = new System.Drawing.Point(183, 42);
            this.tB_S9.Name = "tB_S9";
            this.tB_S9.Size = new System.Drawing.Size(32, 20);
            this.tB_S9.TabIndex = 58;
            // 
            // tB_S1
            // 
            this.tB_S1.Location = new System.Drawing.Point(39, 43);
            this.tB_S1.Name = "tB_S1";
            this.tB_S1.Size = new System.Drawing.Size(32, 20);
            this.tB_S1.TabIndex = 58;
            // 
            // tB_S6
            // 
            this.tB_S6.Location = new System.Drawing.Point(110, 69);
            this.tB_S6.Name = "tB_S6";
            this.tB_S6.Size = new System.Drawing.Size(32, 20);
            this.tB_S6.TabIndex = 58;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(84, 46);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 13);
            this.label13.TabIndex = 57;
            this.label13.Text = "S5";
            // 
            // tB_S10
            // 
            this.tB_S10.Location = new System.Drawing.Point(183, 68);
            this.tB_S10.Name = "tB_S10";
            this.tB_S10.Size = new System.Drawing.Size(32, 20);
            this.tB_S10.TabIndex = 58;
            // 
            // tB_S2
            // 
            this.tB_S2.Location = new System.Drawing.Point(39, 69);
            this.tB_S2.Name = "tB_S2";
            this.tB_S2.Size = new System.Drawing.Size(32, 20);
            this.tB_S2.TabIndex = 58;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(84, 72);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 13);
            this.label12.TabIndex = 57;
            this.label12.Text = "S6";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(157, 45);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(20, 13);
            this.label18.TabIndex = 57;
            this.label18.Text = "S9";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 57;
            this.label7.Text = "S1";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(157, 71);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(26, 13);
            this.label17.TabIndex = 57;
            this.label17.Text = "S10";
            // 
            // tB_S4
            // 
            this.tB_S4.Location = new System.Drawing.Point(110, 18);
            this.tB_S4.Name = "tB_S4";
            this.tB_S4.Size = new System.Drawing.Size(32, 20);
            this.tB_S4.TabIndex = 58;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 57;
            this.label8.Text = "S2";
            // 
            // tB_S8
            // 
            this.tB_S8.Location = new System.Drawing.Point(183, 17);
            this.tB_S8.Name = "tB_S8";
            this.tB_S8.Size = new System.Drawing.Size(32, 20);
            this.tB_S8.TabIndex = 58;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(84, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 57;
            this.label11.Text = "S4";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(157, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(20, 13);
            this.label15.TabIndex = 57;
            this.label15.Text = "S8";
            // 
            // tB_S0
            // 
            this.tB_S0.Location = new System.Drawing.Point(39, 18);
            this.tB_S0.Name = "tB_S0";
            this.tB_S0.Size = new System.Drawing.Size(32, 20);
            this.tB_S0.TabIndex = 58;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "S0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cB_description);
            this.groupBox1.Controls.Add(this.tB_StrojLokace);
            this.groupBox1.Controls.Add(this.tB_StrojSklad);
            this.groupBox1.Controls.Add(this.tB_Zdroj);
            this.groupBox1.Controls.Add(this.tb_CisloSluzby);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.dtp_OD);
            this.groupBox1.Controls.Add(this.cb_TimeVariant);
            this.groupBox1.Controls.Add(this.dtp_DO);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(25, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 161);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zpracováno";
            // 
            // cB_description
            // 
            this.cB_description.FormattingEnabled = true;
            this.cB_description.Location = new System.Drawing.Point(332, 70);
            this.cB_description.Name = "cB_description";
            this.cB_description.Size = new System.Drawing.Size(139, 21);
            this.cB_description.TabIndex = 60;
            // 
            // tb_CisloSluzby
            // 
            this.tb_CisloSluzby.Location = new System.Drawing.Point(332, 25);
            this.tb_CisloSluzby.Name = "tb_CisloSluzby";
            this.tb_CisloSluzby.Size = new System.Drawing.Size(139, 20);
            this.tb_CisloSluzby.TabIndex = 58;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(263, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 57;
            this.label5.Text = "Stroj";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 57;
            this.label2.Text = "Číslo služby";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 56;
            this.label9.Text = "Časová varianta";
            // 
            // dtp_OD
            // 
            this.dtp_OD.Checked = false;
            this.dtp_OD.Location = new System.Drawing.Point(47, 25);
            this.dtp_OD.Name = "dtp_OD";
            this.dtp_OD.ShowCheckBox = true;
            this.dtp_OD.Size = new System.Drawing.Size(200, 20);
            this.dtp_OD.TabIndex = 43;
            // 
            // cb_TimeVariant
            // 
            this.cb_TimeVariant.FormattingEnabled = true;
            this.cb_TimeVariant.Location = new System.Drawing.Point(108, 78);
            this.cb_TimeVariant.Name = "cb_TimeVariant";
            this.cb_TimeVariant.Size = new System.Drawing.Size(139, 21);
            this.cb_TimeVariant.TabIndex = 55;
            this.cb_TimeVariant.SelectedIndexChanged += new System.EventHandler(this.cb_TimeVariant_SelectedIndexChanged);
            // 
            // dtp_DO
            // 
            this.dtp_DO.Checked = false;
            this.dtp_DO.Location = new System.Drawing.Point(47, 48);
            this.dtp_DO.Name = "dtp_DO";
            this.dtp_DO.ShowCheckBox = true;
            this.dtp_DO.Size = new System.Drawing.Size(200, 20);
            this.dtp_DO.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "OD";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "DO";
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(1121, 42);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 20;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // tsFiltry
            // 
            this.tsFiltry.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsFiltry.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsFiltry.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbFiltry,
            this.tsbNastavit,
            this.toolStripSeparator2,
            this.tsbZmena,
            this.toolStripSeparator4,
            this.tsbPridat,
            this.toolStripSeparator3,
            this.tsbOdebrat,
            this.toolStripSeparator5,
            this.tsbVycistit});
            this.tsFiltry.Location = new System.Drawing.Point(0, 0);
            this.tsFiltry.Name = "tsFiltry";
            this.tsFiltry.Size = new System.Drawing.Size(1200, 27);
            this.tsFiltry.TabIndex = 41;
            this.tsFiltry.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 24);
            this.toolStripLabel1.Text = "Filtry:";
            // 
            // tscbFiltry
            // 
            this.tscbFiltry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry.DropDownWidth = 170;
            this.tscbFiltry.Name = "tscbFiltry";
            this.tscbFiltry.Size = new System.Drawing.Size(170, 27);
            // 
            // tsbNastavit
            // 
            this.tsbNastavit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit.Image")));
            this.tsbNastavit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit.Name = "tsbNastavit";
            this.tsbNastavit.Size = new System.Drawing.Size(24, 24);
            this.tsbNastavit.ToolTipText = "Nastavit";
            this.tsbNastavit.Click += new System.EventHandler(this.tsbNastavit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbZmena
            // 
            this.tsbZmena.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena.Image")));
            this.tsbZmena.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena.Name = "tsbZmena";
            this.tsbZmena.Size = new System.Drawing.Size(24, 24);
            this.tsbZmena.Text = "Změna";
            this.tsbZmena.Click += new System.EventHandler(this.tsbZmena_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbPridat
            // 
            this.tsbPridat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat.Image")));
            this.tsbPridat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat.Name = "tsbPridat";
            this.tsbPridat.Size = new System.Drawing.Size(24, 24);
            this.tsbPridat.Text = "Uložit";
            this.tsbPridat.Click += new System.EventHandler(this.tsbPridat_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbOdebrat
            // 
            this.tsbOdebrat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat.Image")));
            this.tsbOdebrat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat.Name = "tsbOdebrat";
            this.tsbOdebrat.Size = new System.Drawing.Size(24, 24);
            this.tsbOdebrat.Text = "Odebrat";
            this.tsbOdebrat.Click += new System.EventHandler(this.tsbOdebrat_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbVycistit
            // 
            this.tsbVycistit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit.Image")));
            this.tsbVycistit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit.Name = "tsbVycistit";
            this.tsbVycistit.Size = new System.Drawing.Size(24, 24);
            this.tsbVycistit.Text = "Vyčistit";
            this.tsbVycistit.Click += new System.EventHandler(this.tsbVycistit_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuVyber,
            this.tsmiMenuList,
            this.tsmiExporty});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // tsmiMenuVyber
            // 
            this.tsmiMenuVyber.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiVybrat,
            this.toolStripSeparator1,
            this.tsmiKonecVyber});
            this.tsmiMenuVyber.Name = "tsmiMenuVyber";
            this.tsmiMenuVyber.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenuVyber.Text = "Menu";
            // 
            // tsmiVybrat
            // 
            this.tsmiVybrat.Name = "tsmiVybrat";
            this.tsmiVybrat.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiVybrat.Size = new System.Drawing.Size(151, 22);
            this.tsmiVybrat.Text = "Vybrat";
            this.tsmiVybrat.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
            // 
            // tsmiKonecVyber
            // 
            this.tsmiKonecVyber.Name = "tsmiKonecVyber";
            this.tsmiKonecVyber.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonecVyber.Size = new System.Drawing.Size(151, 22);
            this.tsmiKonecVyber.Text = "Konec";
            this.tsmiKonecVyber.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // tsmiMenuList
            // 
            this.tsmiMenuList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiKonecList});
            this.tsmiMenuList.Name = "tsmiMenuList";
            this.tsmiMenuList.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tsmiMenuList.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenuList.Text = "Menu";
            // 
            // tsmiKonecList
            // 
            this.tsmiKonecList.Name = "tsmiKonecList";
            this.tsmiKonecList.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonecList.Size = new System.Drawing.Size(148, 22);
            this.tsmiKonecList.Text = "Konec";
            this.tsmiKonecList.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // tsmiExporty
            // 
            this.tsmiExporty.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiskToolStripMenuItem,
            this.tsmiExportyDoCSVVse,
            this.tsmiExportyDoCSVOznacene,
            this.toolStripSeparator6,
            this.tsmiExportyDoExcelVse,
            this.tsmiExportyDoExceOznacene,
            this.toolStripSeparator7,
            this.tsmiExportyDoXMLVse,
            this.tsmiExportyDoXMLOznacene});
            this.tsmiExporty.Name = "tsmiExporty";
            this.tsmiExporty.Size = new System.Drawing.Size(55, 20);
            this.tsmiExporty.Text = "Výstup";
            // 
            // tiskToolStripMenuItem
            // 
            this.tiskToolStripMenuItem.Name = "tiskToolStripMenuItem";
            this.tiskToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+P, Ctrl+R";
            this.tiskToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tiskToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.tiskToolStripMenuItem.Text = "Tisk";
            this.tiskToolStripMenuItem.Click += new System.EventHandler(this.tiskToolStripMenuItem_Click);
            // 
            // tsmiExportyDoCSVVse
            // 
            this.tsmiExportyDoCSVVse.Name = "tsmiExportyDoCSVVse";
            this.tsmiExportyDoCSVVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportyDoCSVVse.Text = "Export do CSV vše";
            this.tsmiExportyDoCSVVse.Click += new System.EventHandler(this.exportDoCSVVseToolStripMenuItem_Click);
            // 
            // tsmiExportyDoCSVOznacene
            // 
            this.tsmiExportyDoCSVOznacene.Name = "tsmiExportyDoCSVOznacene";
            this.tsmiExportyDoCSVOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportyDoCSVOznacene.Text = "Export do CSV označené";
            this.tsmiExportyDoCSVOznacene.Click += new System.EventHandler(this.exportDoCSVOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiExportyDoExcelVse
            // 
            this.tsmiExportyDoExcelVse.Name = "tsmiExportyDoExcelVse";
            this.tsmiExportyDoExcelVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportyDoExcelVse.Text = "Export do Excel vše";
            this.tsmiExportyDoExcelVse.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_Click);
            // 
            // tsmiExportyDoExceOznacene
            // 
            this.tsmiExportyDoExceOznacene.Name = "tsmiExportyDoExceOznacene";
            this.tsmiExportyDoExceOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportyDoExceOznacene.Text = "Export do Excel označené";
            this.tsmiExportyDoExceOznacene.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiExportyDoXMLVse
            // 
            this.tsmiExportyDoXMLVse.Name = "tsmiExportyDoXMLVse";
            this.tsmiExportyDoXMLVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportyDoXMLVse.Text = "Export do XML Vše";
            this.tsmiExportyDoXMLVse.Click += new System.EventHandler(this.exportDoXMLVseToolStripMenuItem_Click);
            // 
            // tsmiExportyDoXMLOznacene
            // 
            this.tsmiExportyDoXMLOznacene.Name = "tsmiExportyDoXMLOznacene";
            this.tsmiExportyDoXMLOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportyDoXMLOznacene.Text = "Export do XML označené";
            this.tsmiExportyDoXMLOznacene.Click += new System.EventHandler(this.exportDoXMLOznaceneToolStripMenuItem_Click);
            // 
            // bw_OdvodMachineStateSet
            // 
            this.bw_OdvodMachineStateSet.WorkerSupportsCancellation = true;
            this.bw_OdvodMachineStateSet.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_OdvodEvents_DoWork);
            this.bw_OdvodMachineStateSet.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_OdvodEvents_RunWorkerCompleted);
            // 
            // dg_OdvodMachineStateSet
            // 
            this.dg_OdvodMachineStateSet.AllowUserToAddRows = false;
            this.dg_OdvodMachineStateSet.AllowUserToDeleteRows = false;
            this.dg_OdvodMachineStateSet.AllowUserToOrderColumns = true;
            this.dg_OdvodMachineStateSet.AllowUserToResizeRows = false;
            this.dg_OdvodMachineStateSet.AutoGenerateColumns = false;
            this.dg_OdvodMachineStateSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_OdvodMachineStateSet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ZDROJ,
            this.STROJ,
            this.DATUM,
            this.ID_group,
            this.s0DataGridViewTextBoxColumn,
            this.s1DataGridViewTextBoxColumn,
            this.s2DataGridViewTextBoxColumn,
            this.s3DataGridViewTextBoxColumn,
            this.s4DataGridViewTextBoxColumn,
            this.s5DataGridViewTextBoxColumn,
            this.s6DataGridViewTextBoxColumn,
            this.s7DataGridViewTextBoxColumn,
            this.s8DataGridViewTextBoxColumn,
            this.s9DataGridViewTextBoxColumn,
            this.s10DataGridViewTextBoxColumn,
            this.s11DataGridViewTextBoxColumn,
            this.counter_0,
            this.counter_1,
            this.counter_2,
            this.counter_3,
            this.counter_4,
            this.counter_5,
            this.counter6DataGridViewTextBoxColumn,
            this.counter7DataGridViewTextBoxColumn,
            this.counter8DataGridViewTextBoxColumn,
            this.counter9DataGridViewTextBoxColumn,
            this.counter10DataGridViewTextBoxColumn,
            this.counter11DataGridViewTextBoxColumn,
            this.Popis_pol,
            this.Mnozstvi,
            this.CountEntries,
            this.SOPNUMBE,
            this.ITEMNMBR,
            this.ITEMTYPE,
            this.ITEMMJ,
            this.ORD,
            this.TIMEMODE,
            this.TIMEPREPSTART,
            this.TIMEPREPSTOP,
            this.TIMEPREP,
            this.TIMEUNIT,
            this.TIMESTART,
            this.TIMESTOP,
            this.TIMECORSTART,
            this.TIMECORSTOP,
            this.TIMECOR,
            this.TIMECRID,
            this.TIMECRIDTYPE,
            this.id,
            this.loginid,
            this.machineid,
            this.operationid,
            this.qtyReal,
            this.QTYPACK,
            this.QTYPACKMJ,
            this.description,
            this.BarcodeP,
            this.UserID,
            this.TermID,
            this.ISOK,
            this.GUID,
            this.SOUBEHGUID,
            this.CORRGUID,
            this.qtyOld,
            this.idVS,
            this.dateedit,
            this.SKL_ID,
            this.LOCNCODE,
            this.SERLTNUM,
            this.EXPIRATION,
            this.NMBRPAL,
            this.TYPEPAL,
            this.PackType,
            this.status,
            this.WEIGHT,
            this.STORNOGUID,
            this.REZ_1,
            this.REZ_2,
            this.REZ_3,
            this.REZ_4,
            this.REZ_5,
            this.WEIGHT_OLD,
            this.StrojSklad,
            this.StrojLokace});
            this.dg_OdvodMachineStateSet.DataSource = this.bs_OdvodMachineStateSet;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_OdvodMachineStateSet.DefaultCellStyle = dataGridViewCellStyle1;
            this.dg_OdvodMachineStateSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_OdvodMachineStateSet.EnableHeadersVisualStyles = false;
            this.dg_OdvodMachineStateSet.FilterAndSortEnabled = true;
            this.dg_OdvodMachineStateSet.Location = new System.Drawing.Point(0, 315);
            this.dg_OdvodMachineStateSet.Name = "dg_OdvodMachineStateSet";
            this.dg_OdvodMachineStateSet.ReadOnly = true;
            this.dg_OdvodMachineStateSet.RowHeadersVisible = false;
            this.dg_OdvodMachineStateSet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_OdvodMachineStateSet.Size = new System.Drawing.Size(1200, 364);
            this.dg_OdvodMachineStateSet.TabIndex = 1;
            this.dg_OdvodMachineStateSet.TabStop = false;
            this.dg_OdvodMachineStateSet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dg_OdvodMachineStateSet.SelectionChanged += new System.EventHandler(this.dg_OdvodMachineStateSet_SelectionChanged);
            // 
            // bs_OdvodMachineStateSet
            // 
            this.bs_OdvodMachineStateSet.DataMember = "MachineStateSetHistory_Analyza_Odvodu";
            this.bs_OdvodMachineStateSet.DataSource = this.ds_OdvodMachineStateSet;
            // 
            // ds_OdvodMachineStateSet
            // 
            this.ds_OdvodMachineStateSet.DataSetName = "Vyroba";
            this.ds_OdvodMachineStateSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 288);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(1200, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 42;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // panelButtonsZobrazeniList
            // 
            this.panelButtonsZobrazeniList.AutoScroll = true;
            this.panelButtonsZobrazeniList.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniList.Location = new System.Drawing.Point(1200, 0);
            this.panelButtonsZobrazeniList.Name = "panelButtonsZobrazeniList";
            this.panelButtonsZobrazeniList.Size = new System.Drawing.Size(84, 701);
            this.panelButtonsZobrazeniList.TabIndex = 3;
            // 
            // ZDROJ
            // 
            this.ZDROJ.DataPropertyName = "ZDROJ";
            this.ZDROJ.HeaderText = "ZDROJ";
            this.ZDROJ.Name = "ZDROJ";
            this.ZDROJ.ReadOnly = true;
            // 
            // STROJ
            // 
            this.STROJ.DataPropertyName = "STROJ";
            this.STROJ.HeaderText = "STROJ";
            this.STROJ.Name = "STROJ";
            this.STROJ.ReadOnly = true;
            // 
            // DATUM
            // 
            this.DATUM.DataPropertyName = "DATUM";
            this.DATUM.HeaderText = "DATUM";
            this.DATUM.Name = "DATUM";
            this.DATUM.ReadOnly = true;
            // 
            // ID_group
            // 
            this.ID_group.DataPropertyName = "ID_group";
            this.ID_group.HeaderText = "Číslo služby";
            this.ID_group.Name = "ID_group";
            this.ID_group.ReadOnly = true;
            // 
            // s0DataGridViewTextBoxColumn
            // 
            this.s0DataGridViewTextBoxColumn.DataPropertyName = "S0";
            this.s0DataGridViewTextBoxColumn.HeaderText = "S0";
            this.s0DataGridViewTextBoxColumn.Name = "s0DataGridViewTextBoxColumn";
            this.s0DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // s1DataGridViewTextBoxColumn
            // 
            this.s1DataGridViewTextBoxColumn.DataPropertyName = "S1";
            this.s1DataGridViewTextBoxColumn.HeaderText = "S1";
            this.s1DataGridViewTextBoxColumn.Name = "s1DataGridViewTextBoxColumn";
            this.s1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // s2DataGridViewTextBoxColumn
            // 
            this.s2DataGridViewTextBoxColumn.DataPropertyName = "S2";
            this.s2DataGridViewTextBoxColumn.HeaderText = "S2";
            this.s2DataGridViewTextBoxColumn.Name = "s2DataGridViewTextBoxColumn";
            this.s2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // s3DataGridViewTextBoxColumn
            // 
            this.s3DataGridViewTextBoxColumn.DataPropertyName = "S3";
            this.s3DataGridViewTextBoxColumn.HeaderText = "S3";
            this.s3DataGridViewTextBoxColumn.Name = "s3DataGridViewTextBoxColumn";
            this.s3DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // s4DataGridViewTextBoxColumn
            // 
            this.s4DataGridViewTextBoxColumn.DataPropertyName = "S4";
            this.s4DataGridViewTextBoxColumn.HeaderText = "S4";
            this.s4DataGridViewTextBoxColumn.Name = "s4DataGridViewTextBoxColumn";
            this.s4DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // s5DataGridViewTextBoxColumn
            // 
            this.s5DataGridViewTextBoxColumn.DataPropertyName = "S5";
            this.s5DataGridViewTextBoxColumn.HeaderText = "S5";
            this.s5DataGridViewTextBoxColumn.Name = "s5DataGridViewTextBoxColumn";
            this.s5DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // s6DataGridViewTextBoxColumn
            // 
            this.s6DataGridViewTextBoxColumn.DataPropertyName = "S6";
            this.s6DataGridViewTextBoxColumn.HeaderText = "S6";
            this.s6DataGridViewTextBoxColumn.Name = "s6DataGridViewTextBoxColumn";
            this.s6DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // s7DataGridViewTextBoxColumn
            // 
            this.s7DataGridViewTextBoxColumn.DataPropertyName = "S7";
            this.s7DataGridViewTextBoxColumn.HeaderText = "S7";
            this.s7DataGridViewTextBoxColumn.Name = "s7DataGridViewTextBoxColumn";
            this.s7DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // s8DataGridViewTextBoxColumn
            // 
            this.s8DataGridViewTextBoxColumn.DataPropertyName = "S8";
            this.s8DataGridViewTextBoxColumn.HeaderText = "S8";
            this.s8DataGridViewTextBoxColumn.Name = "s8DataGridViewTextBoxColumn";
            this.s8DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // s9DataGridViewTextBoxColumn
            // 
            this.s9DataGridViewTextBoxColumn.DataPropertyName = "S9";
            this.s9DataGridViewTextBoxColumn.HeaderText = "S9";
            this.s9DataGridViewTextBoxColumn.Name = "s9DataGridViewTextBoxColumn";
            this.s9DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // s10DataGridViewTextBoxColumn
            // 
            this.s10DataGridViewTextBoxColumn.DataPropertyName = "S10";
            this.s10DataGridViewTextBoxColumn.HeaderText = "S10";
            this.s10DataGridViewTextBoxColumn.Name = "s10DataGridViewTextBoxColumn";
            this.s10DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // s11DataGridViewTextBoxColumn
            // 
            this.s11DataGridViewTextBoxColumn.DataPropertyName = "S11";
            this.s11DataGridViewTextBoxColumn.HeaderText = "S11";
            this.s11DataGridViewTextBoxColumn.Name = "s11DataGridViewTextBoxColumn";
            this.s11DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // counter_0
            // 
            this.counter_0.DataPropertyName = "counter_0";
            this.counter_0.HeaderText = "counter_0";
            this.counter_0.Name = "counter_0";
            this.counter_0.ReadOnly = true;
            // 
            // counter_1
            // 
            this.counter_1.DataPropertyName = "counter_1";
            this.counter_1.HeaderText = "counter_1";
            this.counter_1.Name = "counter_1";
            this.counter_1.ReadOnly = true;
            // 
            // counter_2
            // 
            this.counter_2.DataPropertyName = "counter_2";
            this.counter_2.HeaderText = "counter_2";
            this.counter_2.Name = "counter_2";
            this.counter_2.ReadOnly = true;
            // 
            // counter_3
            // 
            this.counter_3.DataPropertyName = "counter_3";
            this.counter_3.HeaderText = "counter_3";
            this.counter_3.Name = "counter_3";
            this.counter_3.ReadOnly = true;
            // 
            // counter_4
            // 
            this.counter_4.DataPropertyName = "counter_4";
            this.counter_4.HeaderText = "counter_4";
            this.counter_4.Name = "counter_4";
            this.counter_4.ReadOnly = true;
            // 
            // counter_5
            // 
            this.counter_5.DataPropertyName = "counter_5";
            this.counter_5.HeaderText = "counter_5";
            this.counter_5.Name = "counter_5";
            this.counter_5.ReadOnly = true;
            // 
            // counter6DataGridViewTextBoxColumn
            // 
            this.counter6DataGridViewTextBoxColumn.DataPropertyName = "counter_6";
            this.counter6DataGridViewTextBoxColumn.HeaderText = "counter_6";
            this.counter6DataGridViewTextBoxColumn.Name = "counter6DataGridViewTextBoxColumn";
            this.counter6DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // counter7DataGridViewTextBoxColumn
            // 
            this.counter7DataGridViewTextBoxColumn.DataPropertyName = "counter_7";
            this.counter7DataGridViewTextBoxColumn.HeaderText = "counter_7";
            this.counter7DataGridViewTextBoxColumn.Name = "counter7DataGridViewTextBoxColumn";
            this.counter7DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // counter8DataGridViewTextBoxColumn
            // 
            this.counter8DataGridViewTextBoxColumn.DataPropertyName = "counter_8";
            this.counter8DataGridViewTextBoxColumn.HeaderText = "counter_8";
            this.counter8DataGridViewTextBoxColumn.Name = "counter8DataGridViewTextBoxColumn";
            this.counter8DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // counter9DataGridViewTextBoxColumn
            // 
            this.counter9DataGridViewTextBoxColumn.DataPropertyName = "counter_9";
            this.counter9DataGridViewTextBoxColumn.HeaderText = "counter_9";
            this.counter9DataGridViewTextBoxColumn.Name = "counter9DataGridViewTextBoxColumn";
            this.counter9DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // counter10DataGridViewTextBoxColumn
            // 
            this.counter10DataGridViewTextBoxColumn.DataPropertyName = "counter_10";
            this.counter10DataGridViewTextBoxColumn.HeaderText = "counter_10";
            this.counter10DataGridViewTextBoxColumn.Name = "counter10DataGridViewTextBoxColumn";
            this.counter10DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // counter11DataGridViewTextBoxColumn
            // 
            this.counter11DataGridViewTextBoxColumn.DataPropertyName = "counter_11";
            this.counter11DataGridViewTextBoxColumn.HeaderText = "counter_11";
            this.counter11DataGridViewTextBoxColumn.Name = "counter11DataGridViewTextBoxColumn";
            this.counter11DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Popis_pol
            // 
            this.Popis_pol.DataPropertyName = "Popis_pol";
            this.Popis_pol.HeaderText = "Popis_pol";
            this.Popis_pol.Name = "Popis_pol";
            this.Popis_pol.ReadOnly = true;
            // 
            // Mnozstvi
            // 
            this.Mnozstvi.DataPropertyName = "Mnozstvi";
            this.Mnozstvi.HeaderText = "Mnozstvi";
            this.Mnozstvi.Name = "Mnozstvi";
            this.Mnozstvi.ReadOnly = true;
            // 
            // CountEntries
            // 
            this.CountEntries.DataPropertyName = "CountEntries";
            this.CountEntries.HeaderText = "Číslo dávky";
            this.CountEntries.Name = "CountEntries";
            this.CountEntries.ReadOnly = true;
            // 
            // SOPNUMBE
            // 
            this.SOPNUMBE.DataPropertyName = "SOPNUMBE";
            this.SOPNUMBE.HeaderText = "Výrobní Zakázka";
            this.SOPNUMBE.Name = "SOPNUMBE";
            this.SOPNUMBE.ReadOnly = true;
            // 
            // ITEMNMBR
            // 
            this.ITEMNMBR.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR.HeaderText = "Číslo položky";
            this.ITEMNMBR.Name = "ITEMNMBR";
            this.ITEMNMBR.ReadOnly = true;
            // 
            // ITEMTYPE
            // 
            this.ITEMTYPE.DataPropertyName = "ITEMTYPE";
            this.ITEMTYPE.HeaderText = "Typ položky";
            this.ITEMTYPE.Name = "ITEMTYPE";
            this.ITEMTYPE.ReadOnly = true;
            // 
            // ITEMMJ
            // 
            this.ITEMMJ.DataPropertyName = "ITEMMJ";
            this.ITEMMJ.HeaderText = "Měrná jednotka";
            this.ITEMMJ.Name = "ITEMMJ";
            this.ITEMMJ.ReadOnly = true;
            // 
            // ORD
            // 
            this.ORD.DataPropertyName = "ORD";
            this.ORD.HeaderText = "Pořadí položky";
            this.ORD.Name = "ORD";
            this.ORD.ReadOnly = true;
            // 
            // TIMEMODE
            // 
            this.TIMEMODE.DataPropertyName = "TIMEMODE";
            this.TIMEMODE.HeaderText = "Typ sledování času";
            this.TIMEMODE.Name = "TIMEMODE";
            this.TIMEMODE.ReadOnly = true;
            // 
            // TIMEPREPSTART
            // 
            this.TIMEPREPSTART.DataPropertyName = "TIMEPREPSTART";
            this.TIMEPREPSTART.HeaderText = "Start čas přípravy";
            this.TIMEPREPSTART.Name = "TIMEPREPSTART";
            this.TIMEPREPSTART.ReadOnly = true;
            // 
            // TIMEPREPSTOP
            // 
            this.TIMEPREPSTOP.DataPropertyName = "TIMEPREPSTOP";
            this.TIMEPREPSTOP.HeaderText = "Stop čas přípravy";
            this.TIMEPREPSTOP.Name = "TIMEPREPSTOP";
            this.TIMEPREPSTOP.ReadOnly = true;
            // 
            // TIMEPREP
            // 
            this.TIMEPREP.DataPropertyName = "TIMEPREP";
            this.TIMEPREP.HeaderText = "Přípravný čas v minutách";
            this.TIMEPREP.Name = "TIMEPREP";
            this.TIMEPREP.ReadOnly = true;
            // 
            // TIMEUNIT
            // 
            this.TIMEUNIT.DataPropertyName = "TIMEUNIT";
            this.TIMEUNIT.HeaderText = "Jednotkový čas";
            this.TIMEUNIT.Name = "TIMEUNIT";
            this.TIMEUNIT.ReadOnly = true;
            // 
            // TIMESTART
            // 
            this.TIMESTART.DataPropertyName = "TIMESTART";
            this.TIMESTART.HeaderText = "Čas zahájení";
            this.TIMESTART.Name = "TIMESTART";
            this.TIMESTART.ReadOnly = true;
            // 
            // TIMESTOP
            // 
            this.TIMESTOP.DataPropertyName = "TIMESTOP";
            this.TIMESTOP.HeaderText = "Čas ukončení";
            this.TIMESTOP.Name = "TIMESTOP";
            this.TIMESTOP.ReadOnly = true;
            // 
            // TIMECORSTART
            // 
            this.TIMECORSTART.DataPropertyName = "TIMECORSTART";
            this.TIMECORSTART.HeaderText = "Čas zahájení korekce";
            this.TIMECORSTART.Name = "TIMECORSTART";
            this.TIMECORSTART.ReadOnly = true;
            // 
            // TIMECORSTOP
            // 
            this.TIMECORSTOP.DataPropertyName = "TIMECORSTOP";
            this.TIMECORSTOP.HeaderText = "Čas ukončení korekce";
            this.TIMECORSTOP.Name = "TIMECORSTOP";
            this.TIMECORSTOP.ReadOnly = true;
            // 
            // TIMECOR
            // 
            this.TIMECOR.DataPropertyName = "TIMECOR";
            this.TIMECOR.HeaderText = "Korekce času v minutách";
            this.TIMECOR.Name = "TIMECOR";
            this.TIMECOR.ReadOnly = true;
            // 
            // TIMECRID
            // 
            this.TIMECRID.DataPropertyName = "TIMECRID";
            this.TIMECRID.HeaderText = "id korekce";
            this.TIMECRID.Name = "TIMECRID";
            this.TIMECRID.ReadOnly = true;
            // 
            // TIMECRIDTYPE
            // 
            this.TIMECRIDTYPE.DataPropertyName = "TIMECRIDTYPE";
            this.TIMECRIDTYPE.HeaderText = "TIMECRIDTYPE";
            this.TIMECRIDTYPE.Name = "TIMECRIDTYPE";
            this.TIMECRIDTYPE.ReadOnly = true;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id události";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // loginid
            // 
            this.loginid.DataPropertyName = "loginid";
            this.loginid.HeaderText = "id uživatele";
            this.loginid.Name = "loginid";
            this.loginid.ReadOnly = true;
            // 
            // machineid
            // 
            this.machineid.DataPropertyName = "machineid";
            this.machineid.HeaderText = "id stroje";
            this.machineid.Name = "machineid";
            this.machineid.ReadOnly = true;
            // 
            // operationid
            // 
            this.operationid.DataPropertyName = "operationid";
            this.operationid.HeaderText = "id operace";
            this.operationid.Name = "operationid";
            this.operationid.ReadOnly = true;
            // 
            // qtyReal
            // 
            this.qtyReal.DataPropertyName = "qtyReal";
            this.qtyReal.HeaderText = "Počet kusů sejmuto";
            this.qtyReal.Name = "qtyReal";
            this.qtyReal.ReadOnly = true;
            // 
            // QTYPACK
            // 
            this.QTYPACK.DataPropertyName = "QTYPACK";
            this.QTYPACK.HeaderText = "Množství v balení";
            this.QTYPACK.Name = "QTYPACK";
            this.QTYPACK.ReadOnly = true;
            // 
            // QTYPACKMJ
            // 
            this.QTYPACKMJ.DataPropertyName = "QTYPACKMJ";
            this.QTYPACKMJ.HeaderText = "Měrná jednotka balení";
            this.QTYPACKMJ.Name = "QTYPACKMJ";
            this.QTYPACKMJ.ReadOnly = true;
            // 
            // description
            // 
            this.description.DataPropertyName = "description";
            this.description.HeaderText = "Popis";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            // 
            // BarcodeP
            // 
            this.BarcodeP.DataPropertyName = "BarcodeP";
            this.BarcodeP.HeaderText = "Čár. kód položky";
            this.BarcodeP.Name = "BarcodeP";
            this.BarcodeP.ReadOnly = true;
            // 
            // UserID
            // 
            this.UserID.DataPropertyName = "UserID";
            this.UserID.HeaderText = "id pracovníka";
            this.UserID.Name = "UserID";
            this.UserID.ReadOnly = true;
            // 
            // TermID
            // 
            this.TermID.DataPropertyName = "TermID";
            this.TermID.HeaderText = "id terminálu";
            this.TermID.Name = "TermID";
            this.TermID.ReadOnly = true;
            // 
            // ISOK
            // 
            this.ISOK.DataPropertyName = "ISOK";
            this.ISOK.HeaderText = "Čas převzetí do IS";
            this.ISOK.Name = "ISOK";
            this.ISOK.ReadOnly = true;
            // 
            // GUID
            // 
            this.GUID.DataPropertyName = "GUID";
            this.GUID.HeaderText = "GUID";
            this.GUID.Name = "GUID";
            this.GUID.ReadOnly = true;
            // 
            // SOUBEHGUID
            // 
            this.SOUBEHGUID.DataPropertyName = "SOUBEHGUID";
            this.SOUBEHGUID.HeaderText = "Souběh GUID";
            this.SOUBEHGUID.Name = "SOUBEHGUID";
            this.SOUBEHGUID.ReadOnly = true;
            // 
            // CORRGUID
            // 
            this.CORRGUID.DataPropertyName = "CORRGUID";
            this.CORRGUID.HeaderText = "Korekce GUID";
            this.CORRGUID.Name = "CORRGUID";
            this.CORRGUID.ReadOnly = true;
            // 
            // qtyOld
            // 
            this.qtyOld.DataPropertyName = "qtyOld";
            this.qtyOld.HeaderText = "Původní množství";
            this.qtyOld.Name = "qtyOld";
            this.qtyOld.ReadOnly = true;
            // 
            // idVS
            // 
            this.idVS.DataPropertyName = "idVS";
            this.idVS.HeaderText = "schválil vedoucí směny";
            this.idVS.Name = "idVS";
            this.idVS.ReadOnly = true;
            // 
            // dateedit
            // 
            this.dateedit.DataPropertyName = "dateedit";
            this.dateedit.HeaderText = "Datum a čas editace";
            this.dateedit.Name = "dateedit";
            this.dateedit.ReadOnly = true;
            // 
            // SKL_ID
            // 
            this.SKL_ID.DataPropertyName = "SKL_ID";
            this.SKL_ID.HeaderText = "Sklad ID";
            this.SKL_ID.Name = "SKL_ID";
            this.SKL_ID.ReadOnly = true;
            // 
            // LOCNCODE
            // 
            this.LOCNCODE.DataPropertyName = "LOCNCODE";
            this.LOCNCODE.HeaderText = "Lokace";
            this.LOCNCODE.Name = "LOCNCODE";
            this.LOCNCODE.ReadOnly = true;
            // 
            // SERLTNUM
            // 
            this.SERLTNUM.DataPropertyName = "SERLTNUM";
            this.SERLTNUM.HeaderText = "SN/Šarže";
            this.SERLTNUM.Name = "SERLTNUM";
            this.SERLTNUM.ReadOnly = true;
            // 
            // EXPIRATION
            // 
            this.EXPIRATION.DataPropertyName = "EXPIRATION";
            this.EXPIRATION.HeaderText = "Expirace";
            this.EXPIRATION.Name = "EXPIRATION";
            this.EXPIRATION.ReadOnly = true;
            // 
            // NMBRPAL
            // 
            this.NMBRPAL.DataPropertyName = "NMBRPAL";
            this.NMBRPAL.HeaderText = "SSCC";
            this.NMBRPAL.Name = "NMBRPAL";
            this.NMBRPAL.ReadOnly = true;
            // 
            // TYPEPAL
            // 
            this.TYPEPAL.DataPropertyName = "TYPEPAL";
            this.TYPEPAL.HeaderText = "typ palety";
            this.TYPEPAL.Name = "TYPEPAL";
            this.TYPEPAL.ReadOnly = true;
            // 
            // PackType
            // 
            this.PackType.DataPropertyName = "PackType";
            this.PackType.HeaderText = "typ balení";
            this.PackType.Name = "PackType";
            this.PackType.ReadOnly = true;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // WEIGHT
            // 
            this.WEIGHT.DataPropertyName = "WEIGHT";
            this.WEIGHT.HeaderText = "váha";
            this.WEIGHT.Name = "WEIGHT";
            this.WEIGHT.ReadOnly = true;
            // 
            // STORNOGUID
            // 
            this.STORNOGUID.DataPropertyName = "STORNOGUID";
            this.STORNOGUID.HeaderText = "STORNO - GUID";
            this.STORNOGUID.Name = "STORNOGUID";
            this.STORNOGUID.ReadOnly = true;
            // 
            // REZ_1
            // 
            this.REZ_1.DataPropertyName = "REZ_1";
            this.REZ_1.HeaderText = "REZ_1";
            this.REZ_1.Name = "REZ_1";
            this.REZ_1.ReadOnly = true;
            // 
            // REZ_2
            // 
            this.REZ_2.DataPropertyName = "REZ_2";
            this.REZ_2.HeaderText = "REZ_2";
            this.REZ_2.Name = "REZ_2";
            this.REZ_2.ReadOnly = true;
            // 
            // REZ_3
            // 
            this.REZ_3.DataPropertyName = "REZ_3";
            this.REZ_3.HeaderText = "REZ_3";
            this.REZ_3.Name = "REZ_3";
            this.REZ_3.ReadOnly = true;
            // 
            // REZ_4
            // 
            this.REZ_4.DataPropertyName = "REZ_4";
            this.REZ_4.HeaderText = "REZ_4";
            this.REZ_4.Name = "REZ_4";
            this.REZ_4.ReadOnly = true;
            // 
            // REZ_5
            // 
            this.REZ_5.DataPropertyName = "REZ_5";
            this.REZ_5.HeaderText = "REZ_5";
            this.REZ_5.Name = "REZ_5";
            this.REZ_5.ReadOnly = true;
            // 
            // WEIGHT_OLD
            // 
            this.WEIGHT_OLD.DataPropertyName = "WEIGHT_OLD";
            this.WEIGHT_OLD.HeaderText = "Původní váha";
            this.WEIGHT_OLD.Name = "WEIGHT_OLD";
            this.WEIGHT_OLD.ReadOnly = true;
            // 
            // StrojSklad
            // 
            this.StrojSklad.DataPropertyName = "StrojSklad";
            this.StrojSklad.HeaderText = "StrojSklad";
            this.StrojSklad.Name = "StrojSklad";
            this.StrojSklad.ReadOnly = true;
            // 
            // StrojLokace
            // 
            this.StrojLokace.DataPropertyName = "StrojLokace";
            this.StrojLokace.HeaderText = "StrojLokace";
            this.StrojLokace.Name = "StrojLokace";
            this.StrojLokace.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(263, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Zdroj";
            // 
            // tB_Zdroj
            // 
            this.tB_Zdroj.Location = new System.Drawing.Point(332, 47);
            this.tB_Zdroj.Name = "tB_Zdroj";
            this.tB_Zdroj.Size = new System.Drawing.Size(139, 20);
            this.tB_Zdroj.TabIndex = 58;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(263, 98);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 57;
            this.label16.Text = "Stroj sklad";
            // 
            // tB_StrojSklad
            // 
            this.tB_StrojSklad.Location = new System.Drawing.Point(332, 94);
            this.tB_StrojSklad.Name = "tB_StrojSklad";
            this.tB_StrojSklad.Size = new System.Drawing.Size(139, 20);
            this.tB_StrojSklad.TabIndex = 58;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(263, 121);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(63, 13);
            this.label19.TabIndex = 57;
            this.label19.Text = "Stroj lokace";
            // 
            // tB_StrojLokace
            // 
            this.tB_StrojLokace.Location = new System.Drawing.Point(332, 117);
            this.tB_StrojLokace.Name = "tB_StrojLokace";
            this.tB_StrojLokace.Size = new System.Drawing.Size(139, 20);
            this.tB_StrojLokace.TabIndex = 58;
            // 
            // Form_MachineStateSetList_RozborOdvoduSSS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 701);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "Form_MachineStateSetList_RozborOdvoduSSS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Odvod Events stavy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUzivateleList_FormClosing);
            this.Load += new System.EventHandler(this.FormOdvod_MachineStateSetList_Load);
            this.Shown += new System.EventHandler(this.FormOdvod_MachineStateSetList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOdvod_MachineStateSetList_KeyDown);
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_OdvodMachineStateSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_OdvodMachineStateSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_OdvodMachineStateSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelButtonsZobrazeniVyber;
        protected System.Windows.Forms.Panel panelMain;
        protected System.Windows.Forms.Button buttonVybratUzivatele;
        protected System.Windows.Forms.Button buttonKonec;
        protected System.Windows.Forms.MenuStrip menuStrip2;
        protected System.Windows.Forms.ToolStripMenuItem tsmiMenuVyber;
        protected System.Windows.Forms.ToolStripMenuItem tsmiVybrat;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripMenuItem tsmiKonecVyber;
        protected System.Windows.Forms.Button buttonVyhledat;
        protected ProgressControls.ProgressIndicator progressIndicator1;
        protected Fask.AdvancedButtonsPanel.ButtonsPanel panelButtonsZobrazeniList;
        protected System.Windows.Forms.ToolStrip tsFiltry;
        protected System.Windows.Forms.ToolStripLabel toolStripLabel1;
        protected System.Windows.Forms.ToolStripComboBox tscbFiltry;
        protected System.Windows.Forms.ToolStripButton tsbNastavit;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        protected System.Windows.Forms.ToolStripButton tsbZmena;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        protected System.Windows.Forms.ToolStripButton tsbPridat;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        protected System.Windows.Forms.ToolStripButton tsbOdebrat;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        protected System.Windows.Forms.ToolStripButton tsbVycistit;
        protected System.Windows.Forms.ToolStripMenuItem tsmiMenuList;
        protected System.Windows.Forms.ToolStripMenuItem tsmiKonecList;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExporty;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportyDoCSVVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportyDoCSVOznacene;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportyDoExcelVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportyDoExceOznacene;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportyDoXMLVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportyDoXMLOznacene;
        protected Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        protected System.Windows.Forms.Panel panel1;
        public Zuby.ADGV.AdvancedDataGridView dg_OdvodMachineStateSet;
        private System.Windows.Forms.BindingSource bs_OdvodMachineStateSet;
        private Fask.Interfaces.DataSets.Vyroba ds_OdvodMachineStateSet;
        private System.ComponentModel.BackgroundWorker bw_OdvodMachineStateSet;
        private System.Windows.Forms.DateTimePicker dtp_OD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_DO;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_TimeVariant;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Eventu_Count;
        private System.Windows.Forms.TextBox tb_CisloSluzby;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem tiskToolStripMenuItem;
        private System.Windows.Forms.ComboBox cB_description;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tB_S7;
        private System.Windows.Forms.TextBox tB_S11;
        private System.Windows.Forms.TextBox tB_S3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tB_S5;
        private System.Windows.Forms.TextBox tB_S9;
        private System.Windows.Forms.TextBox tB_S1;
        private System.Windows.Forms.TextBox tB_S6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tB_S10;
        private System.Windows.Forms.TextBox tB_S2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tB_S4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tB_S8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tB_S0;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZDROJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn STROJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_group;
        private System.Windows.Forms.DataGridViewTextBoxColumn s0DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn s1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn s2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn s3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn s4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn s5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn s6DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn s7DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn s8DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn s9DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn s10DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn s11DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn counter_0;
        private System.Windows.Forms.DataGridViewTextBoxColumn counter_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn counter_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn counter_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn counter_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn counter_5;
        private System.Windows.Forms.DataGridViewTextBoxColumn counter6DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn counter7DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn counter8DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn counter9DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn counter10DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn counter11DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Popis_pol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mnozstvi;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountEntries;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPNUMBE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMMJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEMODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEPREPSTART;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEPREPSTOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEPREP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEUNIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMESTART;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMESTOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMECORSTART;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMECORSTOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMECOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMECRID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMECRIDTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn loginid;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineid;
        private System.Windows.Forms.DataGridViewTextBoxColumn operationid;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyReal;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYPACK;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYPACKMJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarcodeP;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TermID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOUBEHGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CORRGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyOld;
        private System.Windows.Forms.DataGridViewTextBoxColumn idVS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateedit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCNCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERLTNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXPIRATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn NMBRPAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn TYPEPAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackType;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn STORNOGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn REZ_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn REZ_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn REZ_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn REZ_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn REZ_5;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT_OLD;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrojSklad;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrojLokace;
        private System.Windows.Forms.TextBox tB_StrojLokace;
        private System.Windows.Forms.TextBox tB_StrojSklad;
        private System.Windows.Forms.TextBox tB_Zdroj;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label1;
    }
}