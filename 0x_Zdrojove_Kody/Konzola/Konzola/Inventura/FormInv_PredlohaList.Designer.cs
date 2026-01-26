namespace Konzola.Inventura
{
    partial class FormInv_PredlohaList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInv_PredlohaList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVystup = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExcelOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportPOHODA = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportXML = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDBF = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.stornovatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rB_raz_1 = new System.Windows.Forms.RadioButton();
            this.rB_raz_2 = new System.Windows.Forms.RadioButton();
            this.rB_raz_3 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_Zakladni = new System.Windows.Forms.RadioButton();
            this.rb_Sarze = new System.Windows.Forms.RadioButton();
            this.rb_alterKody = new System.Windows.Forms.RadioButton();
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
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxLOCNCODE = new System.Windows.Forms.ComboBox();
            this.comboBoxSKLID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxITEMNMBR = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxCountEntries = new System.Windows.Forms.ComboBox();
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.buttonOznacitVse = new System.Windows.Forms.Button();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.bw_INV = new System.ComponentModel.BackgroundWorker();
            this.dg_INV = new Zuby.ADGV.AdvancedDataGridView();
            this.DEX_ROW_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountEntries = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_CarKod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCNCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DMJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATEDONE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IntegerValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMESPRT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_SerNum_Track = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_SerNum_Find = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TerminalID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.O_TID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REZ_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REZ_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CE_Orig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_REZ1_Track = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_REZ2_Track = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_Expirace_Track = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I2_DEX_ROW_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I2_CE_Orig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I2_SERLNMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I2_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I2_Expirace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I3_MJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I3_VNDITNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I3_WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I3_CE_Orig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I3_CZ_CarKod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I3_QTYPACK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I3_VENDORID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I3_VENDNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I3_DEX_ROW_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_INV = new System.Windows.Forms.BindingSource(this.components);
            this.ds_INV = new Fask.Interfaces.DataSets.Inventura();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.advancedDataGridViewSearchToolBar2 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.bw_Export_Pohoda = new System.ComponentModel.BackgroundWorker();
            this.bw_Export_XML = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_INV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_INV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_INV)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiVystup,
            this.tsmiExport,
            this.tsmiAkce});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(934, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiMenu
            // 
            this.tsmiMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiKonec});
            this.tsmiMenu.Name = "tsmiMenu";
            this.tsmiMenu.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenu.Text = "Menu";
            // 
            // tsmiKonec
            // 
            this.tsmiKonec.Name = "tsmiKonec";
            this.tsmiKonec.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonec.Size = new System.Drawing.Size(148, 22);
            this.tsmiKonec.Text = "Konec";
            this.tsmiKonec.Click += new System.EventHandler(this.tsmiKonec_Click);
            // 
            // tsmiVystup
            // 
            this.tsmiVystup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiskToolStripMenuItem,
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator1,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExcelOznacene,
            this.toolStripSeparator6,
            this.tsmiExportDoXMLVse,
            this.tsmiExportDoXMLOznacene});
            this.tsmiVystup.Name = "tsmiVystup";
            this.tsmiVystup.Size = new System.Drawing.Size(55, 20);
            this.tsmiVystup.Text = "Výstup";
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
            // tsmiExportDoCSVVse
            // 
            this.tsmiExportDoCSVVse.Name = "tsmiExportDoCSVVse";
            this.tsmiExportDoCSVVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoCSVVse.Text = "Export do CSV vše";
            this.tsmiExportDoCSVVse.Click += new System.EventHandler(this.exportDoCSVVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoCSVOznacene
            // 
            this.tsmiExportDoCSVOznacene.Name = "tsmiExportDoCSVOznacene";
            this.tsmiExportDoCSVOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoCSVOznacene.Text = "Export do CSV označené";
            this.tsmiExportDoCSVOznacene.Click += new System.EventHandler(this.exportDoCSVOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiExportDoExcelVse
            // 
            this.tsmiExportDoExcelVse.Name = "tsmiExportDoExcelVse";
            this.tsmiExportDoExcelVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoExcelVse.Text = "Export do Excel vše";
            this.tsmiExportDoExcelVse.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoExcelOznacene
            // 
            this.tsmiExportDoExcelOznacene.Name = "tsmiExportDoExcelOznacene";
            this.tsmiExportDoExcelOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoExcelOznacene.Text = "Export do Excel označené";
            this.tsmiExportDoExcelOznacene.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiExportDoXMLVse
            // 
            this.tsmiExportDoXMLVse.Name = "tsmiExportDoXMLVse";
            this.tsmiExportDoXMLVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoXMLVse.Text = "Export do XML Vše";
            this.tsmiExportDoXMLVse.Click += new System.EventHandler(this.exportDoXMLVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoXMLOznacene
            // 
            this.tsmiExportDoXMLOznacene.Name = "tsmiExportDoXMLOznacene";
            this.tsmiExportDoXMLOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoXMLOznacene.Text = "Export do XML označené";
            this.tsmiExportDoXMLOznacene.Click += new System.EventHandler(this.exportDoXMLOznaceneToolStripMenuItem_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportPOHODA,
            this.tsmiExportXML,
            this.tsmiExportDBF});
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(53, 20);
            this.tsmiExport.Text = "Export";
            // 
            // tsmiExportPOHODA
            // 
            this.tsmiExportPOHODA.Name = "tsmiExportPOHODA";
            this.tsmiExportPOHODA.Size = new System.Drawing.Size(219, 22);
            this.tsmiExportPOHODA.Text = "IS POHODA";
            this.tsmiExportPOHODA.Click += new System.EventHandler(this.tsmiExportPOHODA_Click);
            // 
            // tsmiExportXML
            // 
            this.tsmiExportXML.Name = "tsmiExportXML";
            this.tsmiExportXML.Size = new System.Drawing.Size(219, 22);
            this.tsmiExportXML.Text = "XML soubor (IS ABRA)";
            this.tsmiExportXML.Click += new System.EventHandler(this.tsmiExportXML_Click);
            // 
            // tsmiExportDBF
            // 
            this.tsmiExportDBF.Name = "tsmiExportDBF";
            this.tsmiExportDBF.Size = new System.Drawing.Size(219, 22);
            this.tsmiExportDBF.Text = "DBF soubor (IS SB Komplet)";
            this.tsmiExportDBF.Click += new System.EventHandler(this.tsmiExportDBF_Click);
            // 
            // tsmiAkce
            // 
            this.tsmiAkce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stornovatToolStripMenuItem});
            this.tsmiAkce.Name = "tsmiAkce";
            this.tsmiAkce.Size = new System.Drawing.Size(45, 20);
            this.tsmiAkce.Text = "Akce";
            // 
            // stornovatToolStripMenuItem
            // 
            this.stornovatToolStripMenuItem.Name = "stornovatToolStripMenuItem";
            this.stornovatToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.stornovatToolStripMenuItem.Text = "Storno";
            this.stornovatToolStripMenuItem.Click += new System.EventHandler(this.stornovatToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBoxLOCNCODE);
            this.panel1.Controls.Add(this.comboBoxSKLID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBoxITEMNMBR);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBoxCountEntries);
            this.panel1.Controls.Add(this.buttonOdznacitVse);
            this.panel1.Controls.Add(this.buttonOznacitVse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(850, 162);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rB_raz_1);
            this.groupBox2.Controls.Add(this.rB_raz_2);
            this.groupBox2.Controls.Add(this.rB_raz_3);
            this.groupBox2.Location = new System.Drawing.Point(446, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 104);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Řazení";
            // 
            // rB_raz_1
            // 
            this.rB_raz_1.AutoSize = true;
            this.rB_raz_1.Checked = true;
            this.rB_raz_1.Location = new System.Drawing.Point(15, 23);
            this.rB_raz_1.Name = "rB_raz_1";
            this.rB_raz_1.Size = new System.Drawing.Size(40, 17);
            this.rB_raz_1.TabIndex = 40;
            this.rB_raz_1.TabStop = true;
            this.rB_raz_1.Text = "xxx";
            this.rB_raz_1.UseVisualStyleBackColor = true;
            // 
            // rB_raz_2
            // 
            this.rB_raz_2.AutoSize = true;
            this.rB_raz_2.Location = new System.Drawing.Point(15, 46);
            this.rB_raz_2.Name = "rB_raz_2";
            this.rB_raz_2.Size = new System.Drawing.Size(40, 17);
            this.rB_raz_2.TabIndex = 40;
            this.rB_raz_2.Text = "xxx";
            this.rB_raz_2.UseVisualStyleBackColor = true;
            // 
            // rB_raz_3
            // 
            this.rB_raz_3.AutoSize = true;
            this.rB_raz_3.Location = new System.Drawing.Point(15, 67);
            this.rB_raz_3.Name = "rB_raz_3";
            this.rB_raz_3.Size = new System.Drawing.Size(40, 17);
            this.rB_raz_3.TabIndex = 40;
            this.rB_raz_3.Text = "xxx";
            this.rB_raz_3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_Zakladni);
            this.groupBox1.Controls.Add(this.rb_Sarze);
            this.groupBox1.Controls.Add(this.rb_alterKody);
            this.groupBox1.Location = new System.Drawing.Point(301, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(139, 104);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            // 
            // rb_Zakladni
            // 
            this.rb_Zakladni.AutoSize = true;
            this.rb_Zakladni.Checked = true;
            this.rb_Zakladni.Location = new System.Drawing.Point(15, 23);
            this.rb_Zakladni.Name = "rb_Zakladni";
            this.rb_Zakladni.Size = new System.Drawing.Size(68, 17);
            this.rb_Zakladni.TabIndex = 40;
            this.rb_Zakladni.TabStop = true;
            this.rb_Zakladni.Text = "Základní";
            this.rb_Zakladni.UseVisualStyleBackColor = true;
            this.rb_Zakladni.CheckedChanged += new System.EventHandler(this.rb_Zakladni_CheckedChanged);
            // 
            // rb_Sarze
            // 
            this.rb_Sarze.AutoSize = true;
            this.rb_Sarze.Location = new System.Drawing.Point(15, 46);
            this.rb_Sarze.Name = "rb_Sarze";
            this.rb_Sarze.Size = new System.Drawing.Size(78, 17);
            this.rb_Sarze.TabIndex = 40;
            this.rb_Sarze.Text = "SN / Šarže";
            this.rb_Sarze.UseVisualStyleBackColor = true;
            this.rb_Sarze.CheckedChanged += new System.EventHandler(this.rb_Sarze_CheckedChanged);
            // 
            // rb_alterKody
            // 
            this.rb_alterKody.AutoSize = true;
            this.rb_alterKody.Location = new System.Drawing.Point(15, 67);
            this.rb_alterKody.Name = "rb_alterKody";
            this.rb_alterKody.Size = new System.Drawing.Size(105, 17);
            this.rb_alterKody.TabIndex = 40;
            this.rb_alterKody.Text = "Alternativní kódy";
            this.rb_alterKody.UseVisualStyleBackColor = true;
            this.rb_alterKody.CheckedChanged += new System.EventHandler(this.rb_alterKody_CheckedChanged);
            // 
            // tsFiltry
            // 
            this.tsFiltry.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
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
            this.tsFiltry.Size = new System.Drawing.Size(850, 25);
            this.tsFiltry.TabIndex = 39;
            this.tsFiltry.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Filtry:";
            // 
            // tscbFiltry
            // 
            this.tscbFiltry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry.DropDownWidth = 170;
            this.tscbFiltry.Name = "tscbFiltry";
            this.tscbFiltry.Size = new System.Drawing.Size(170, 25);
            // 
            // tsbNastavit
            // 
            this.tsbNastavit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit.Image")));
            this.tsbNastavit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit.Name = "tsbNastavit";
            this.tsbNastavit.Size = new System.Drawing.Size(23, 22);
            this.tsbNastavit.ToolTipText = "Nastavit";
            this.tsbNastavit.Click += new System.EventHandler(this.tsbNastavit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbZmena
            // 
            this.tsbZmena.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena.Image")));
            this.tsbZmena.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena.Name = "tsbZmena";
            this.tsbZmena.Size = new System.Drawing.Size(23, 22);
            this.tsbZmena.Text = "Změna";
            this.tsbZmena.Click += new System.EventHandler(this.tsbZmena_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPridat
            // 
            this.tsbPridat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat.Image")));
            this.tsbPridat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat.Name = "tsbPridat";
            this.tsbPridat.Size = new System.Drawing.Size(23, 22);
            this.tsbPridat.Text = "Uložit";
            this.tsbPridat.Click += new System.EventHandler(this.tsbPridat_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOdebrat
            // 
            this.tsbOdebrat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat.Image")));
            this.tsbOdebrat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat.Name = "tsbOdebrat";
            this.tsbOdebrat.Size = new System.Drawing.Size(23, 22);
            this.tsbOdebrat.Text = "Odebrat";
            this.tsbOdebrat.Click += new System.EventHandler(this.tsbOdebrat_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbVycistit
            // 
            this.tsbVycistit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit.Image")));
            this.tsbVycistit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit.Name = "tsbVycistit";
            this.tsbVycistit.Size = new System.Drawing.Size(23, 22);
            this.tsbVycistit.Text = "Vyčistit";
            this.tsbVycistit.Click += new System.EventHandler(this.tsbVycistit_Click);
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(764, 35);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 14;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Lokace:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Sklad:";
            // 
            // comboBoxLOCNCODE
            // 
            this.comboBoxLOCNCODE.FormattingEnabled = true;
            this.comboBoxLOCNCODE.Location = new System.Drawing.Point(83, 116);
            this.comboBoxLOCNCODE.Name = "comboBoxLOCNCODE";
            this.comboBoxLOCNCODE.Size = new System.Drawing.Size(201, 21);
            this.comboBoxLOCNCODE.TabIndex = 3;
            // 
            // comboBoxSKLID
            // 
            this.comboBoxSKLID.FormattingEnabled = true;
            this.comboBoxSKLID.Location = new System.Drawing.Point(83, 89);
            this.comboBoxSKLID.Name = "comboBoxSKLID";
            this.comboBoxSKLID.Size = new System.Drawing.Size(201, 21);
            this.comboBoxSKLID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "ID Položky:";
            // 
            // comboBoxITEMNMBR
            // 
            this.comboBoxITEMNMBR.FormattingEnabled = true;
            this.comboBoxITEMNMBR.Location = new System.Drawing.Point(83, 62);
            this.comboBoxITEMNMBR.Name = "comboBoxITEMNMBR";
            this.comboBoxITEMNMBR.Size = new System.Drawing.Size(201, 21);
            this.comboBoxITEMNMBR.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Číslo dávky:";
            // 
            // comboBoxCountEntries
            // 
            this.comboBoxCountEntries.FormattingEnabled = true;
            this.comboBoxCountEntries.Location = new System.Drawing.Point(83, 35);
            this.comboBoxCountEntries.Name = "comboBoxCountEntries";
            this.comboBoxCountEntries.Size = new System.Drawing.Size(201, 21);
            this.comboBoxCountEntries.TabIndex = 0;
            // 
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdznacitVse.Location = new System.Drawing.Point(754, 120);
            this.buttonOdznacitVse.Name = "buttonOdznacitVse";
            this.buttonOdznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOdznacitVse.TabIndex = 16;
            this.buttonOdznacitVse.Text = "Odznačit vše";
            this.buttonOdznacitVse.UseVisualStyleBackColor = true;
            this.buttonOdznacitVse.Click += new System.EventHandler(this.buttonOdznacitVse_Click);
            // 
            // buttonOznacitVse
            // 
            this.buttonOznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOznacitVse.Location = new System.Drawing.Point(665, 120);
            this.buttonOznacitVse.Name = "buttonOznacitVse";
            this.buttonOznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOznacitVse.TabIndex = 15;
            this.buttonOznacitVse.Text = "Označit vše";
            this.buttonOznacitVse.UseVisualStyleBackColor = true;
            this.buttonOznacitVse.Click += new System.EventHandler(this.buttonOznacitVse_Click);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(397, 322);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 30;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // bw_INV
            // 
            this.bw_INV.WorkerSupportsCancellation = true;
            this.bw_INV.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_INV_DoWork);
            this.bw_INV.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_INV_RunWorkerCompleted);
            // 
            // dg_INV
            // 
            this.dg_INV.AllowUserToAddRows = false;
            this.dg_INV.AllowUserToDeleteRows = false;
            this.dg_INV.AllowUserToOrderColumns = true;
            this.dg_INV.AllowUserToResizeRows = false;
            this.dg_INV.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_INV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_INV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_INV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DEX_ROW_ID,
            this.CountEntries,
            this.ITEMNMBR,
            this.ITEMCODE,
            this.ITEMDESC,
            this.CZ_CarKod,
            this.SKL_ID,
            this.SKL_DESC,
            this.LOCNCODE,
            this.QUANTITY,
            this.DMJ,
            this.DATEDONE,
            this.IntegerValue,
            this.TIMESPRT,
            this.CZ_SerNum_Track,
            this.CZ_SerNum_Find,
            this.TerminalID,
            this.O_TID,
            this.REZ_1,
            this.REZ_2,
            this.CE_Orig,
            this.CZ_REZ1_Track,
            this.CZ_REZ2_Track,
            this.CZ_Expirace_Track,
            this.I2_DEX_ROW_ID,
            this.I2_CE_Orig,
            this.I2_SERLNMBR,
            this.I2_QTY,
            this.I2_Expirace,
            this.I3_MJ,
            this.I3_VNDITNUM,
            this.I3_WEIGHT,
            this.I3_CE_Orig,
            this.I3_CZ_CarKod,
            this.I3_QTYPACK,
            this.I3_VENDORID,
            this.I3_VENDNAME,
            this.I3_DEX_ROW_ID});
            this.dg_INV.DataSource = this.bs_INV;
            this.dg_INV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_INV.EnableHeadersVisualStyles = false;
            this.dg_INV.FilterAndSortEnabled = true;
            this.dg_INV.Location = new System.Drawing.Point(0, 213);
            this.dg_INV.Name = "dg_INV";
            this.dg_INV.ReadOnly = true;
            this.dg_INV.RowHeadersVisible = false;
            this.dg_INV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_INV.Size = new System.Drawing.Size(850, 349);
            this.dg_INV.TabIndex = 0;
            this.dg_INV.TabStop = false;
            this.dg_INV.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_INV_CellFormatting);
            this.dg_INV.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dg_INV.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // DEX_ROW_ID
            // 
            this.DEX_ROW_ID.DataPropertyName = "DEX_ROW_ID";
            this.DEX_ROW_ID.HeaderText = "Index";
            this.DEX_ROW_ID.Name = "DEX_ROW_ID";
            this.DEX_ROW_ID.ReadOnly = true;
            // 
            // CountEntries
            // 
            this.CountEntries.DataPropertyName = "CountEntries";
            this.CountEntries.HeaderText = "Číslo dávky";
            this.CountEntries.Name = "CountEntries";
            this.CountEntries.ReadOnly = true;
            // 
            // ITEMNMBR
            // 
            this.ITEMNMBR.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR.HeaderText = "ID položky";
            this.ITEMNMBR.Name = "ITEMNMBR";
            this.ITEMNMBR.ReadOnly = true;
            // 
            // ITEMCODE
            // 
            this.ITEMCODE.DataPropertyName = "ITEMCODE";
            this.ITEMCODE.HeaderText = "Kód položky";
            this.ITEMCODE.Name = "ITEMCODE";
            this.ITEMCODE.ReadOnly = true;
            // 
            // ITEMDESC
            // 
            this.ITEMDESC.DataPropertyName = "ITEMDESC";
            this.ITEMDESC.HeaderText = "Popis položky";
            this.ITEMDESC.Name = "ITEMDESC";
            this.ITEMDESC.ReadOnly = true;
            // 
            // CZ_CarKod
            // 
            this.CZ_CarKod.DataPropertyName = "CZ_CarKod";
            this.CZ_CarKod.HeaderText = "Čár. kód";
            this.CZ_CarKod.Name = "CZ_CarKod";
            this.CZ_CarKod.ReadOnly = true;
            // 
            // SKL_ID
            // 
            this.SKL_ID.DataPropertyName = "SKL_ID";
            this.SKL_ID.HeaderText = "ID skladu";
            this.SKL_ID.Name = "SKL_ID";
            this.SKL_ID.ReadOnly = true;
            // 
            // SKL_DESC
            // 
            this.SKL_DESC.DataPropertyName = "SKL_DESC";
            this.SKL_DESC.HeaderText = "Nazev skladu";
            this.SKL_DESC.Name = "SKL_DESC";
            this.SKL_DESC.ReadOnly = true;
            // 
            // LOCNCODE
            // 
            this.LOCNCODE.DataPropertyName = "LOCNCODE";
            this.LOCNCODE.HeaderText = "Lokace";
            this.LOCNCODE.Name = "LOCNCODE";
            this.LOCNCODE.ReadOnly = true;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            this.QUANTITY.HeaderText = "Množství";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            // 
            // DMJ
            // 
            this.DMJ.DataPropertyName = "DMJ";
            this.DMJ.HeaderText = "Měrná jednotka";
            this.DMJ.Name = "DMJ";
            this.DMJ.ReadOnly = true;
            // 
            // DATEDONE
            // 
            this.DATEDONE.DataPropertyName = "DATEDONE";
            this.DATEDONE.HeaderText = "Datum";
            this.DATEDONE.Name = "DATEDONE";
            this.DATEDONE.ReadOnly = true;
            // 
            // IntegerValue
            // 
            this.IntegerValue.DataPropertyName = "IntegerValue";
            this.IntegerValue.HeaderText = "IntegerValue";
            this.IntegerValue.Name = "IntegerValue";
            this.IntegerValue.ReadOnly = true;
            // 
            // TIMESPRT
            // 
            this.TIMESPRT.DataPropertyName = "TIMESPRT";
            this.TIMESPRT.HeaderText = "TIMESPRT";
            this.TIMESPRT.Name = "TIMESPRT";
            this.TIMESPRT.ReadOnly = true;
            // 
            // CZ_SerNum_Track
            // 
            this.CZ_SerNum_Track.DataPropertyName = "CZ_SerNum_Track";
            this.CZ_SerNum_Track.HeaderText = "Typ sledování";
            this.CZ_SerNum_Track.Name = "CZ_SerNum_Track";
            this.CZ_SerNum_Track.ReadOnly = true;
            // 
            // CZ_SerNum_Find
            // 
            this.CZ_SerNum_Find.DataPropertyName = "CZ_SerNum_Find";
            this.CZ_SerNum_Find.HeaderText = "Dohledávat SN";
            this.CZ_SerNum_Find.Name = "CZ_SerNum_Find";
            this.CZ_SerNum_Find.ReadOnly = true;
            // 
            // TerminalID
            // 
            this.TerminalID.DataPropertyName = "TerminalID";
            this.TerminalID.HeaderText = "ID terminálu";
            this.TerminalID.Name = "TerminalID";
            this.TerminalID.ReadOnly = true;
            // 
            // O_TID
            // 
            this.O_TID.DataPropertyName = "O_TID";
            this.O_TID.HeaderText = "Online ID terminálu";
            this.O_TID.Name = "O_TID";
            this.O_TID.ReadOnly = true;
            // 
            // REZ_1
            // 
            this.REZ_1.DataPropertyName = "REZ_1";
            this.REZ_1.HeaderText = "REZ 1";
            this.REZ_1.Name = "REZ_1";
            this.REZ_1.ReadOnly = true;
            // 
            // REZ_2
            // 
            this.REZ_2.DataPropertyName = "REZ_2";
            this.REZ_2.HeaderText = "REZ 2";
            this.REZ_2.Name = "REZ_2";
            this.REZ_2.ReadOnly = true;
            // 
            // CE_Orig
            // 
            this.CE_Orig.DataPropertyName = "CE_Orig";
            this.CE_Orig.HeaderText = "CE_Orig";
            this.CE_Orig.Name = "CE_Orig";
            this.CE_Orig.ReadOnly = true;
            // 
            // CZ_REZ1_Track
            // 
            this.CZ_REZ1_Track.DataPropertyName = "CZ_REZ1_Track";
            this.CZ_REZ1_Track.HeaderText = "Příznak REZ 1";
            this.CZ_REZ1_Track.Name = "CZ_REZ1_Track";
            this.CZ_REZ1_Track.ReadOnly = true;
            // 
            // CZ_REZ2_Track
            // 
            this.CZ_REZ2_Track.DataPropertyName = "CZ_REZ2_Track";
            this.CZ_REZ2_Track.HeaderText = "Příznak REZ 2";
            this.CZ_REZ2_Track.Name = "CZ_REZ2_Track";
            this.CZ_REZ2_Track.ReadOnly = true;
            // 
            // CZ_Expirace_Track
            // 
            this.CZ_Expirace_Track.DataPropertyName = "CZ_Expirace_Track";
            this.CZ_Expirace_Track.HeaderText = "Příznak expirace";
            this.CZ_Expirace_Track.Name = "CZ_Expirace_Track";
            this.CZ_Expirace_Track.ReadOnly = true;
            // 
            // I2_DEX_ROW_ID
            // 
            this.I2_DEX_ROW_ID.DataPropertyName = "I2_DEX_ROW_ID";
            this.I2_DEX_ROW_ID.HeaderText = "Index šarže";
            this.I2_DEX_ROW_ID.Name = "I2_DEX_ROW_ID";
            this.I2_DEX_ROW_ID.ReadOnly = true;
            // 
            // I2_CE_Orig
            // 
            this.I2_CE_Orig.DataPropertyName = "I2_CE_Orig";
            this.I2_CE_Orig.HeaderText = "CE_Orig u šarže";
            this.I2_CE_Orig.Name = "I2_CE_Orig";
            this.I2_CE_Orig.ReadOnly = true;
            // 
            // I2_SERLNMBR
            // 
            this.I2_SERLNMBR.DataPropertyName = "I2_SERLNMBR";
            this.I2_SERLNMBR.HeaderText = "Šarže / SN";
            this.I2_SERLNMBR.Name = "I2_SERLNMBR";
            this.I2_SERLNMBR.ReadOnly = true;
            // 
            // I2_QTY
            // 
            this.I2_QTY.DataPropertyName = "I2_QTY";
            this.I2_QTY.HeaderText = "Množství u šarže";
            this.I2_QTY.Name = "I2_QTY";
            this.I2_QTY.ReadOnly = true;
            // 
            // I2_Expirace
            // 
            this.I2_Expirace.DataPropertyName = "I2_Expirace";
            this.I2_Expirace.HeaderText = "Expirace u šarže";
            this.I2_Expirace.Name = "I2_Expirace";
            this.I2_Expirace.ReadOnly = true;
            // 
            // I3_MJ
            // 
            this.I3_MJ.DataPropertyName = "I3_MJ";
            this.I3_MJ.HeaderText = "Alter MJ";
            this.I3_MJ.Name = "I3_MJ";
            this.I3_MJ.ReadOnly = true;
            // 
            // I3_VNDITNUM
            // 
            this.I3_VNDITNUM.DataPropertyName = "I3_VNDITNUM";
            this.I3_VNDITNUM.HeaderText = "Alter Čár. kód";
            this.I3_VNDITNUM.Name = "I3_VNDITNUM";
            this.I3_VNDITNUM.ReadOnly = true;
            // 
            // I3_WEIGHT
            // 
            this.I3_WEIGHT.DataPropertyName = "I3_WEIGHT";
            this.I3_WEIGHT.HeaderText = "Alter Váha";
            this.I3_WEIGHT.Name = "I3_WEIGHT";
            this.I3_WEIGHT.ReadOnly = true;
            // 
            // I3_CE_Orig
            // 
            this.I3_CE_Orig.DataPropertyName = "I3_CE_Orig";
            this.I3_CE_Orig.HeaderText = "alter CE_Orig";
            this.I3_CE_Orig.Name = "I3_CE_Orig";
            this.I3_CE_Orig.ReadOnly = true;
            // 
            // I3_CZ_CarKod
            // 
            this.I3_CZ_CarKod.DataPropertyName = "I3_CZ_CarKod";
            this.I3_CZ_CarKod.HeaderText = "Alter Čár. kód 2";
            this.I3_CZ_CarKod.Name = "I3_CZ_CarKod";
            this.I3_CZ_CarKod.ReadOnly = true;
            // 
            // I3_QTYPACK
            // 
            this.I3_QTYPACK.DataPropertyName = "I3_QTYPACK";
            this.I3_QTYPACK.HeaderText = "alter QTYPACK";
            this.I3_QTYPACK.Name = "I3_QTYPACK";
            this.I3_QTYPACK.ReadOnly = true;
            // 
            // I3_VENDORID
            // 
            this.I3_VENDORID.DataPropertyName = "I3_VENDORID";
            this.I3_VENDORID.HeaderText = "alter VENDORID";
            this.I3_VENDORID.Name = "I3_VENDORID";
            this.I3_VENDORID.ReadOnly = true;
            // 
            // I3_VENDNAME
            // 
            this.I3_VENDNAME.DataPropertyName = "I3_VENDNAME";
            this.I3_VENDNAME.HeaderText = "alter VENDNAME";
            this.I3_VENDNAME.Name = "I3_VENDNAME";
            this.I3_VENDNAME.ReadOnly = true;
            // 
            // I3_DEX_ROW_ID
            // 
            this.I3_DEX_ROW_ID.DataPropertyName = "I3_DEX_ROW_ID";
            this.I3_DEX_ROW_ID.HeaderText = "alter index";
            this.I3_DEX_ROW_ID.Name = "I3_DEX_ROW_ID";
            this.I3_DEX_ROW_ID.ReadOnly = true;
            // 
            // bs_INV
            // 
            this.bs_INV.DataMember = "CZMST_I1_Predloha";
            this.bs_INV.DataSource = this.ds_INV;
            // 
            // ds_INV
            // 
            this.ds_INV.DataSetName = "Inventura";
            this.ds_INV.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(850, 24);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 538);
            this.panelButtons.TabIndex = 2;
            // 
            // advancedDataGridViewSearchToolBar2
            // 
            this.advancedDataGridViewSearchToolBar2.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar2.Location = new System.Drawing.Point(0, 186);
            this.advancedDataGridViewSearchToolBar2.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar2.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar2.Name = "advancedDataGridViewSearchToolBar2";
            this.advancedDataGridViewSearchToolBar2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar2.Size = new System.Drawing.Size(850, 27);
            this.advancedDataGridViewSearchToolBar2.TabIndex = 32;
            this.advancedDataGridViewSearchToolBar2.Text = "advancedDataGridViewSearchToolBar2";
            this.advancedDataGridViewSearchToolBar2.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // bw_Export_Pohoda
            // 
            this.bw_Export_Pohoda.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Export_Pohoda_DoWork);
            this.bw_Export_Pohoda.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Export_Pohoda_RunWorkerCompleted);
            // 
            // bw_Export_XML
            // 
            this.bw_Export_XML.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Export_XML_DoWork);
            this.bw_Export_XML.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Export_XML_RunWorkerCompleted);
            // 
            // FormInv_PredlohaList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 562);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dg_INV);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormInv_PredlohaList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stav inventury";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormInv_PredlohaList_FormClosing);
            this.Load += new System.EventHandler(this.FormInv_PredlohaList_Load);
            this.Shown += new System.EventHandler(this.FormInv_PredlohaList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormInv_PredlohaList_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_INV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_INV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_INV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxITEMNMBR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxCountEntries;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Button buttonOznacitVse;
        private Zuby.ADGV.AdvancedDataGridView dg_INV;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.BindingSource bs_INV;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxLOCNCODE;
        private System.Windows.Forms.ComboBox comboBoxSKLID;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private System.ComponentModel.BackgroundWorker bw_INV;
        private Fask.Interfaces.DataSets.Inventura ds_INV;
        private System.Windows.Forms.ToolStrip tsFiltry;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbFiltry;
        private System.Windows.Forms.ToolStripButton tsbNastavit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbPridat;
        private System.Windows.Forms.ToolStripButton tsbOdebrat;
        private System.Windows.Forms.ToolStripButton tsbZmena;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbVycistit;
        private System.Windows.Forms.ToolStripMenuItem tsmiVystup;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private System.Windows.Forms.RadioButton rb_alterKody;
        private System.Windows.Forms.RadioButton rb_Sarze;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEX_ROW_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountEntries;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_CarKod;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCNCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn DMJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATEDONE;
        private System.Windows.Forms.DataGridViewTextBoxColumn IntegerValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMESPRT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_SerNum_Track;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_SerNum_Find;
        private System.Windows.Forms.DataGridViewTextBoxColumn TerminalID;
        private System.Windows.Forms.DataGridViewTextBoxColumn O_TID;
        private System.Windows.Forms.DataGridViewTextBoxColumn REZ_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn REZ_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CE_Orig;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_REZ1_Track;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_REZ2_Track;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_Expirace_Track;
        private System.Windows.Forms.DataGridViewTextBoxColumn I2_DEX_ROW_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn I2_CE_Orig;
        private System.Windows.Forms.DataGridViewTextBoxColumn I2_SERLNMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn I2_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn I2_Expirace;
        private System.Windows.Forms.DataGridViewTextBoxColumn I3_MJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn I3_VNDITNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn I3_WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn I3_CE_Orig;
        private System.Windows.Forms.DataGridViewTextBoxColumn I3_CZ_CarKod;
        private System.Windows.Forms.DataGridViewTextBoxColumn I3_QTYPACK;
        private System.Windows.Forms.DataGridViewTextBoxColumn I3_VENDORID;
        private System.Windows.Forms.DataGridViewTextBoxColumn I3_VENDNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn I3_DEX_ROW_ID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_Zakladni;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportPOHODA;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportXML;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDBF;
        private System.ComponentModel.BackgroundWorker bw_Export_Pohoda;
        private System.ComponentModel.BackgroundWorker bw_Export_XML;
        private System.Windows.Forms.ToolStripMenuItem tsmiAkce;
        private System.Windows.Forms.ToolStripMenuItem stornovatToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rB_raz_1;
        private System.Windows.Forms.RadioButton rB_raz_2;
        private System.Windows.Forms.RadioButton rB_raz_3;
        private System.Windows.Forms.ToolStripMenuItem tiskToolStripMenuItem;
    }
}