namespace Konzola.SkladLokace
{
    partial class FormPohybyIS_List
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPohybyIS_List));
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tb_ITEMCODE = new System.Windows.Forms.TextBox();
            this.tb_ITEMDESC = new System.Windows.Forms.TextBox();
            this.tb_ITEMNMBR = new System.Windows.Forms.TextBox();
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
            this.btn_Vyhledat = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenuVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVybrat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiKonecVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonecList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExporty = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExceOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.bw_PP = new System.ComponentModel.BackgroundWorker();
            this.dg_PP = new Zuby.ADGV.AdvancedDataGridView();
            this.bs_PP = new System.Windows.Forms.BindingSource(this.components);
            this.ds_PP = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtp_Upraveno_OD = new System.Windows.Forms.DateTimePicker();
            this.cb_Upraveno_TimeVariant = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtp_Upraveno_DO = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtp_Vytvoreno_OD = new System.Windows.Forms.DateTimePicker();
            this.cb_Vytvoreno_TimeVariant = new System.Windows.Forms.ComboBox();
            this.dtp_Vytvoreno_DO = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skldescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typPohybuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zdrojPohybuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datumPohybuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pocetNaPohybuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stavPoPohybuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cisloDokladuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kdoVytvorilDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ucetniDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datumVytvoreniDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datumUlozeniDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_PP)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtonsZobrazeniVyber
            // 
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonKonec);
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonVybratUzivatele);
            this.panelButtonsZobrazeniVyber.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniVyber.Location = new System.Drawing.Point(925, 0);
            this.panelButtonsZobrazeniVyber.Name = "panelButtonsZobrazeniVyber";
            this.panelButtonsZobrazeniVyber.Size = new System.Drawing.Size(104, 708);
            this.panelButtonsZobrazeniVyber.TabIndex = 2;
            // 
            // buttonKonec
            // 
            this.buttonKonec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKonec.Location = new System.Drawing.Point(6, 633);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(93, 63);
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
            this.buttonVybratUzivatele.Size = new System.Drawing.Size(93, 63);
            this.buttonVybratUzivatele.TabIndex = 2;
            this.buttonVybratUzivatele.Text = "Vybrat materiál";
            this.buttonVybratUzivatele.UseVisualStyleBackColor = true;
            this.buttonVybratUzivatele.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBox2);
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Controls.Add(this.tb_ITEMCODE);
            this.panelMain.Controls.Add(this.tb_ITEMDESC);
            this.panelMain.Controls.Add(this.tb_ITEMNMBR);
            this.panelMain.Controls.Add(this.tsFiltry);
            this.panelMain.Controls.Add(this.btn_Vyhledat);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.menuStrip2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(925, 322);
            this.panelMain.TabIndex = 1;
            // 
            // tb_ITEMCODE
            // 
            this.tb_ITEMCODE.Location = new System.Drawing.Point(358, 67);
            this.tb_ITEMCODE.Name = "tb_ITEMCODE";
            this.tb_ITEMCODE.Size = new System.Drawing.Size(139, 20);
            this.tb_ITEMCODE.TabIndex = 46;
            // 
            // tb_ITEMDESC
            // 
            this.tb_ITEMDESC.Location = new System.Drawing.Point(91, 93);
            this.tb_ITEMDESC.Name = "tb_ITEMDESC";
            this.tb_ITEMDESC.Size = new System.Drawing.Size(168, 20);
            this.tb_ITEMDESC.TabIndex = 45;
            // 
            // tb_ITEMNMBR
            // 
            this.tb_ITEMNMBR.Location = new System.Drawing.Point(91, 67);
            this.tb_ITEMNMBR.Name = "tb_ITEMNMBR";
            this.tb_ITEMNMBR.Size = new System.Drawing.Size(168, 20);
            this.tb_ITEMNMBR.TabIndex = 44;
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
            this.tsFiltry.Location = new System.Drawing.Point(0, 24);
            this.tsFiltry.Name = "tsFiltry";
            this.tsFiltry.Size = new System.Drawing.Size(925, 27);
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
            // btn_Vyhledat
            // 
            this.btn_Vyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Vyhledat.Location = new System.Drawing.Point(837, 67);
            this.btn_Vyhledat.Name = "btn_Vyhledat";
            this.btn_Vyhledat.Size = new System.Drawing.Size(73, 63);
            this.btn_Vyhledat.TabIndex = 20;
            this.btn_Vyhledat.Text = "Vyhledat";
            this.btn_Vyhledat.UseVisualStyleBackColor = true;
            this.btn_Vyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Kód položky:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Popis položky:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Číslo položky:";
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
            this.menuStrip2.Size = new System.Drawing.Size(925, 24);
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
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator6,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExceOznacene,
            this.toolStripSeparator7,
            this.tsmiExportDoXMLVse,
            this.tsmiExportDoXMLOznacene});
            this.tsmiExporty.Name = "tsmiExporty";
            this.tsmiExporty.Size = new System.Drawing.Size(55, 20);
            this.tsmiExporty.Text = "Výstup";
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
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiExportDoExcelVse
            // 
            this.tsmiExportDoExcelVse.Name = "tsmiExportDoExcelVse";
            this.tsmiExportDoExcelVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoExcelVse.Text = "Export do Excel vše";
            this.tsmiExportDoExcelVse.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoExceOznacene
            // 
            this.tsmiExportDoExceOznacene.Name = "tsmiExportDoExceOznacene";
            this.tsmiExportDoExceOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoExceOznacene.Text = "Export do Excel označené";
            this.tsmiExportDoExceOznacene.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(205, 6);
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
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(366, 459);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 38;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // bw_PP
            // 
            this.bw_PP.WorkerSupportsCancellation = true;
            this.bw_PP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_PP_DoWork);
            this.bw_PP.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_PP_RunWorkerCompleted);
            // 
            // dg_PP
            // 
            this.dg_PP.AllowUserToAddRows = false;
            this.dg_PP.AllowUserToDeleteRows = false;
            this.dg_PP.AllowUserToOrderColumns = true;
            this.dg_PP.AllowUserToResizeRows = false;
            this.dg_PP.AutoGenerateColumns = false;
            this.dg_PP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_PP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMCODEDataGridViewTextBoxColumn,
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.vNDITNUMDataGridViewTextBoxColumn,
            this.mJDataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.skldescDataGridViewTextBoxColumn,
            this.typPohybuDataGridViewTextBoxColumn,
            this.zdrojPohybuDataGridViewTextBoxColumn,
            this.datumPohybuDataGridViewTextBoxColumn,
            this.pocetNaPohybuDataGridViewTextBoxColumn,
            this.stavPoPohybuDataGridViewTextBoxColumn,
            this.cisloDokladuDataGridViewTextBoxColumn,
            this.kdoVytvorilDataGridViewTextBoxColumn,
            this.ucetniDataGridViewTextBoxColumn,
            this.datumVytvoreniDataGridViewTextBoxColumn,
            this.datumUlozeniDataGridViewTextBoxColumn});
            this.dg_PP.DataSource = this.bs_PP;
            this.dg_PP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_PP.EnableHeadersVisualStyles = false;
            this.dg_PP.FilterAndSortEnabled = true;
            this.dg_PP.Location = new System.Drawing.Point(0, 349);
            this.dg_PP.Name = "dg_PP";
            this.dg_PP.ReadOnly = true;
            this.dg_PP.RowHeadersVisible = false;
            this.dg_PP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_PP.Size = new System.Drawing.Size(925, 359);
            this.dg_PP.TabIndex = 1;
            this.dg_PP.TabStop = false;
            this.dg_PP.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dg_PP.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dg_PP.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // bs_PP
            // 
            this.bs_PP.DataMember = "POHODA_Pohyby";
            this.bs_PP.DataSource = this.ds_PP;
            // 
            // ds_PP
            // 
            this.ds_PP.DataSetName = "SkladLokace_CompareToIS";
            this.ds_PP.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 322);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(925, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 42;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // panelButtonsZobrazeniList
            // 
            this.panelButtonsZobrazeniList.AutoScroll = true;
            this.panelButtonsZobrazeniList.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniList.Location = new System.Drawing.Point(1029, 0);
            this.panelButtonsZobrazeniList.Name = "panelButtonsZobrazeniList";
            this.panelButtonsZobrazeniList.Size = new System.Drawing.Size(113, 708);
            this.panelButtonsZobrazeniList.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtp_Upraveno_OD);
            this.groupBox2.Controls.Add(this.cb_Upraveno_TimeVariant);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.dtp_Upraveno_DO);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(15, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(523, 74);
            this.groupBox2.TabIndex = 57;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Upravováno";
            // 
            // dtp_Upraveno_OD
            // 
            this.dtp_Upraveno_OD.Checked = false;
            this.dtp_Upraveno_OD.Location = new System.Drawing.Point(47, 25);
            this.dtp_Upraveno_OD.Name = "dtp_Upraveno_OD";
            this.dtp_Upraveno_OD.ShowCheckBox = true;
            this.dtp_Upraveno_OD.Size = new System.Drawing.Size(200, 20);
            this.dtp_Upraveno_OD.TabIndex = 49;
            // 
            // cb_Upraveno_TimeVariant
            // 
            this.cb_Upraveno_TimeVariant.FormattingEnabled = true;
            this.cb_Upraveno_TimeVariant.Location = new System.Drawing.Point(346, 33);
            this.cb_Upraveno_TimeVariant.Name = "cb_Upraveno_TimeVariant";
            this.cb_Upraveno_TimeVariant.Size = new System.Drawing.Size(139, 21);
            this.cb_Upraveno_TimeVariant.TabIndex = 47;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(256, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Časová varianta";
            // 
            // dtp_Upraveno_DO
            // 
            this.dtp_Upraveno_DO.Checked = false;
            this.dtp_Upraveno_DO.Location = new System.Drawing.Point(47, 48);
            this.dtp_Upraveno_DO.Name = "dtp_Upraveno_DO";
            this.dtp_Upraveno_DO.ShowCheckBox = true;
            this.dtp_Upraveno_DO.Size = new System.Drawing.Size(200, 20);
            this.dtp_Upraveno_DO.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 51;
            this.label5.Text = "DO";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 52;
            this.label6.Text = "OD";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.dtp_Vytvoreno_OD);
            this.groupBox1.Controls.Add(this.cb_Vytvoreno_TimeVariant);
            this.groupBox1.Controls.Add(this.dtp_Vytvoreno_DO);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(12, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 74);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vytvořeno";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(256, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 56;
            this.label9.Text = "Časová varianta";
            // 
            // dtp_Vytvoreno_OD
            // 
            this.dtp_Vytvoreno_OD.Checked = false;
            this.dtp_Vytvoreno_OD.Location = new System.Drawing.Point(47, 25);
            this.dtp_Vytvoreno_OD.Name = "dtp_Vytvoreno_OD";
            this.dtp_Vytvoreno_OD.ShowCheckBox = true;
            this.dtp_Vytvoreno_OD.Size = new System.Drawing.Size(200, 20);
            this.dtp_Vytvoreno_OD.TabIndex = 43;
            // 
            // cb_Vytvoreno_TimeVariant
            // 
            this.cb_Vytvoreno_TimeVariant.FormattingEnabled = true;
            this.cb_Vytvoreno_TimeVariant.Location = new System.Drawing.Point(346, 33);
            this.cb_Vytvoreno_TimeVariant.Name = "cb_Vytvoreno_TimeVariant";
            this.cb_Vytvoreno_TimeVariant.Size = new System.Drawing.Size(139, 21);
            this.cb_Vytvoreno_TimeVariant.TabIndex = 55;
            // 
            // dtp_Vytvoreno_DO
            // 
            this.dtp_Vytvoreno_DO.Checked = false;
            this.dtp_Vytvoreno_DO.Location = new System.Drawing.Point(47, 48);
            this.dtp_Vytvoreno_DO.Name = "dtp_Vytvoreno_DO";
            this.dtp_Vytvoreno_DO.ShowCheckBox = true;
            this.dtp_Vytvoreno_DO.Size = new System.Drawing.Size(200, 20);
            this.dtp_Vytvoreno_DO.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "OD";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 45;
            this.label7.Text = "DO";
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "Číslo položky";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMCODEDataGridViewTextBoxColumn
            // 
            this.iTEMCODEDataGridViewTextBoxColumn.DataPropertyName = "ITEMCODE";
            this.iTEMCODEDataGridViewTextBoxColumn.HeaderText = "Kód položky";
            this.iTEMCODEDataGridViewTextBoxColumn.Name = "iTEMCODEDataGridViewTextBoxColumn";
            this.iTEMCODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Označení položky";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vNDITNUMDataGridViewTextBoxColumn
            // 
            this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "Čar. kód";
            this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
            this.vNDITNUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mJDataGridViewTextBoxColumn
            // 
            this.mJDataGridViewTextBoxColumn.DataPropertyName = "MJ";
            this.mJDataGridViewTextBoxColumn.HeaderText = "Měrná jednotka";
            this.mJDataGridViewTextBoxColumn.Name = "mJDataGridViewTextBoxColumn";
            this.mJDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sKLIDDataGridViewTextBoxColumn
            // 
            this.sKLIDDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.HeaderText = "ID Skladu";
            this.sKLIDDataGridViewTextBoxColumn.Name = "sKLIDDataGridViewTextBoxColumn";
            this.sKLIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // skldescDataGridViewTextBoxColumn
            // 
            this.skldescDataGridViewTextBoxColumn.DataPropertyName = "skl_desc";
            this.skldescDataGridViewTextBoxColumn.HeaderText = "Název skladu";
            this.skldescDataGridViewTextBoxColumn.Name = "skldescDataGridViewTextBoxColumn";
            this.skldescDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typPohybuDataGridViewTextBoxColumn
            // 
            this.typPohybuDataGridViewTextBoxColumn.DataPropertyName = "TypPohybu";
            this.typPohybuDataGridViewTextBoxColumn.HeaderText = "Typ pohybu";
            this.typPohybuDataGridViewTextBoxColumn.Name = "typPohybuDataGridViewTextBoxColumn";
            this.typPohybuDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // zdrojPohybuDataGridViewTextBoxColumn
            // 
            this.zdrojPohybuDataGridViewTextBoxColumn.DataPropertyName = "ZdrojPohybu";
            this.zdrojPohybuDataGridViewTextBoxColumn.HeaderText = "Zdroj pohybu";
            this.zdrojPohybuDataGridViewTextBoxColumn.Name = "zdrojPohybuDataGridViewTextBoxColumn";
            this.zdrojPohybuDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // datumPohybuDataGridViewTextBoxColumn
            // 
            this.datumPohybuDataGridViewTextBoxColumn.DataPropertyName = "DatumPohybu";
            this.datumPohybuDataGridViewTextBoxColumn.HeaderText = "Datum pohybu";
            this.datumPohybuDataGridViewTextBoxColumn.Name = "datumPohybuDataGridViewTextBoxColumn";
            this.datumPohybuDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pocetNaPohybuDataGridViewTextBoxColumn
            // 
            this.pocetNaPohybuDataGridViewTextBoxColumn.DataPropertyName = "PocetNaPohybu";
            this.pocetNaPohybuDataGridViewTextBoxColumn.HeaderText = "Množství na pohybu";
            this.pocetNaPohybuDataGridViewTextBoxColumn.Name = "pocetNaPohybuDataGridViewTextBoxColumn";
            this.pocetNaPohybuDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stavPoPohybuDataGridViewTextBoxColumn
            // 
            this.stavPoPohybuDataGridViewTextBoxColumn.DataPropertyName = "StavPoPohybu";
            this.stavPoPohybuDataGridViewTextBoxColumn.HeaderText = "Stav zásoby po pohybu";
            this.stavPoPohybuDataGridViewTextBoxColumn.Name = "stavPoPohybuDataGridViewTextBoxColumn";
            this.stavPoPohybuDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cisloDokladuDataGridViewTextBoxColumn
            // 
            this.cisloDokladuDataGridViewTextBoxColumn.DataPropertyName = "CisloDokladu";
            this.cisloDokladuDataGridViewTextBoxColumn.HeaderText = "Číslo dokladu";
            this.cisloDokladuDataGridViewTextBoxColumn.Name = "cisloDokladuDataGridViewTextBoxColumn";
            this.cisloDokladuDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // kdoVytvorilDataGridViewTextBoxColumn
            // 
            this.kdoVytvorilDataGridViewTextBoxColumn.DataPropertyName = "KdoVytvoril";
            this.kdoVytvorilDataGridViewTextBoxColumn.HeaderText = "Vytvořil";
            this.kdoVytvorilDataGridViewTextBoxColumn.Name = "kdoVytvorilDataGridViewTextBoxColumn";
            this.kdoVytvorilDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ucetniDataGridViewTextBoxColumn
            // 
            this.ucetniDataGridViewTextBoxColumn.DataPropertyName = "Ucetni";
            this.ucetniDataGridViewTextBoxColumn.HeaderText = "Editoval";
            this.ucetniDataGridViewTextBoxColumn.Name = "ucetniDataGridViewTextBoxColumn";
            this.ucetniDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // datumVytvoreniDataGridViewTextBoxColumn
            // 
            this.datumVytvoreniDataGridViewTextBoxColumn.DataPropertyName = "DatumVytvoreni";
            this.datumVytvoreniDataGridViewTextBoxColumn.HeaderText = "Datum vytvoření";
            this.datumVytvoreniDataGridViewTextBoxColumn.Name = "datumVytvoreniDataGridViewTextBoxColumn";
            this.datumVytvoreniDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // datumUlozeniDataGridViewTextBoxColumn
            // 
            this.datumUlozeniDataGridViewTextBoxColumn.DataPropertyName = "DatumUlozeni";
            this.datumUlozeniDataGridViewTextBoxColumn.HeaderText = "Datum uložení";
            this.datumUlozeniDataGridViewTextBoxColumn.Name = "datumUlozeniDataGridViewTextBoxColumn";
            this.datumUlozeniDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FormPohybyIS_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 708);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dg_PP);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "FormPohybyIS_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výběr materiálu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPohybyIS_List_FormClosing);
            this.Load += new System.EventHandler(this.FormPohybyIS_List_Load);
            this.Shown += new System.EventHandler(this.FormPohybyIS_List_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormPohybyIS_List_KeyDown);
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_PP)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dg_PP;
        private System.Windows.Forms.Panel panelButtonsZobrazeniVyber;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.BindingSource bs_PP;
        private System.Windows.Forms.Button buttonVybratUzivatele;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuVyber;
        private System.Windows.Forms.ToolStripMenuItem tsmiVybrat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecVyber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Vyhledat;
        private System.ComponentModel.BackgroundWorker bw_PP;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtonsZobrazeniList;
        private System.Windows.Forms.ToolStrip tsFiltry;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbFiltry;
        private System.Windows.Forms.ToolStripButton tsbNastavit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbZmena;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbPridat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbOdebrat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbVycistit;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuList;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecList;
        private System.Windows.Forms.ToolStripMenuItem tsmiExporty;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExceOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private Fask.Interfaces.DataSets.SkladLokace_CompareToIS ds_PP;
        private System.Windows.Forms.TextBox tb_ITEMCODE;
        private System.Windows.Forms.TextBox tb_ITEMDESC;
        private System.Windows.Forms.TextBox tb_ITEMNMBR;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtp_Upraveno_OD;
        private System.Windows.Forms.ComboBox cb_Upraveno_TimeVariant;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtp_Upraveno_DO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtp_Vytvoreno_OD;
        private System.Windows.Forms.ComboBox cb_Vytvoreno_TimeVariant;
        private System.Windows.Forms.DateTimePicker dtp_Vytvoreno_DO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDITNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mJDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn skldescDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typPohybuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zdrojPohybuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datumPohybuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pocetNaPohybuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stavPoPohybuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cisloDokladuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kdoVytvorilDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ucetniDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datumVytvoreniDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datumUlozeniDataGridViewTextBoxColumn;
    }
}