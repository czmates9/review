namespace Konzola.Ciselniky
{
    partial class FormTypyDokladuList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTypyDokladuList));
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbDocID = new System.Windows.Forms.ComboBox();
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
            this.tsmiPolozka = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNovy = new System.Windows.Forms.ToolStripMenuItem();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.bwLoadTypyDokladu = new System.ComponentModel.BackgroundWorker();
            this.dgTypDokladu = new Zuby.ADGV.AdvancedDataGridView();
            this.bsTypDokladu = new System.Windows.Forms.BindingSource(this.components);
            this.dsTypDokladu = new Fask.Interfaces.DataSets.TypyDokladu();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.docidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfg_disp_dest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfg_Navrh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfg_FIFO_FEFO_check = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfg_sarze_ONOFF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfg_sn_ONOFF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfg_expirace_ONOFF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfg_AttributeToSN_ONOFF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docid2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docdescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doctypDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doccarcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEXROWIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOCNCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgodbDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgstrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgpracDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgmn2snDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgdispDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgpaletyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgpaletaidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgzakazkaidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgmenaidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgtiskDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgprevodskladDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgtisksoupisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfglokaceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfglokaceciselnikDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfglokacedestDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgonldoppalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgonloverlokaceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgonloverlokacedestDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgmnozstvizezboziDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgpredvyplnitmnozstviDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgskliddestDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.predvyplnitskliddestDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfglokmechDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfglokmechpohybtypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgskliddestprevzitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfglokacedestciselnikDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.predvyplnitlocncodedestDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgskladyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgonldoplokacedestDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfgdelkaSNDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTypDokladu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTypDokladu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTypDokladu)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButtonsZobrazeniVyber
            // 
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonKonec);
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonVybratUzivatele);
            this.panelButtonsZobrazeniVyber.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniVyber.Location = new System.Drawing.Point(574, 0);
            this.panelButtonsZobrazeniVyber.Name = "panelButtonsZobrazeniVyber";
            this.panelButtonsZobrazeniVyber.Size = new System.Drawing.Size(84, 468);
            this.panelButtonsZobrazeniVyber.TabIndex = 2;
            // 
            // buttonKonec
            // 
            this.buttonKonec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKonec.Location = new System.Drawing.Point(6, 393);
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
            this.panelMain.Controls.Add(this.tsFiltry);
            this.panelMain.Controls.Add(this.buttonVyhledat);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.cbDocID);
            this.panelMain.Controls.Add(this.menuStrip2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(574, 125);
            this.panelMain.TabIndex = 1;
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
            this.tsFiltry.Location = new System.Drawing.Point(0, 24);
            this.tsFiltry.Name = "tsFiltry";
            this.tsFiltry.Size = new System.Drawing.Size(574, 25);
            this.tsFiltry.TabIndex = 41;
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
            this.buttonVyhledat.Location = new System.Drawing.Point(495, 49);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 20;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "ID Typu dokladu:";
            // 
            // cbDocID
            // 
            this.cbDocID.FormattingEnabled = true;
            this.cbDocID.Location = new System.Drawing.Point(116, 52);
            this.cbDocID.Name = "cbDocID";
            this.cbDocID.Size = new System.Drawing.Size(135, 21);
            this.cbDocID.TabIndex = 1;
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuVyber,
            this.tsmiMenuList,
            this.tsmiExporty,
            this.tsmiPolozka});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(574, 24);
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
            // tsmiPolozka
            // 
            this.tsmiPolozka.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOdstranit,
            this.tsmiUpravit,
            this.tsmiNovy});
            this.tsmiPolozka.Name = "tsmiPolozka";
            this.tsmiPolozka.Size = new System.Drawing.Size(60, 20);
            this.tsmiPolozka.Text = "Položka";
            // 
            // tsmiOdstranit
            // 
            this.tsmiOdstranit.Name = "tsmiOdstranit";
            this.tsmiOdstranit.Size = new System.Drawing.Size(123, 22);
            this.tsmiOdstranit.Text = "Odstranit";
            this.tsmiOdstranit.Click += new System.EventHandler(this.tsmiOdstranit_Click);
            // 
            // tsmiUpravit
            // 
            this.tsmiUpravit.Name = "tsmiUpravit";
            this.tsmiUpravit.Size = new System.Drawing.Size(123, 22);
            this.tsmiUpravit.Text = "Upravit";
            this.tsmiUpravit.Click += new System.EventHandler(this.tsmiUpravit_Click);
            // 
            // tsmiNovy
            // 
            this.tsmiNovy.Name = "tsmiNovy";
            this.tsmiNovy.Size = new System.Drawing.Size(123, 22);
            this.tsmiNovy.Text = "Nový";
            this.tsmiNovy.Click += new System.EventHandler(this.tsmiNovy_Click);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(242, 270);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 38;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // bwLoadTypyDokladu
            // 
            this.bwLoadTypyDokladu.WorkerSupportsCancellation = true;
            this.bwLoadTypyDokladu.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadTypyDokladu_DoWork);
            this.bwLoadTypyDokladu.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadTypyDokladu_RunWorkerCompleted);
            // 
            // dgTypDokladu
            // 
            this.dgTypDokladu.AllowUserToAddRows = false;
            this.dgTypDokladu.AllowUserToDeleteRows = false;
            this.dgTypDokladu.AllowUserToOrderColumns = true;
            this.dgTypDokladu.AllowUserToResizeRows = false;
            this.dgTypDokladu.AutoGenerateColumns = false;
            this.dgTypDokladu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTypDokladu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.docidDataGridViewTextBoxColumn,
            this.cfg_disp_dest,
            this.cfg_Navrh,
            this.cfg_FIFO_FEFO_check,
            this.cfg_sarze_ONOFF,
            this.cfg_sn_ONOFF,
            this.cfg_expirace_ONOFF,
            this.cfg_AttributeToSN_ONOFF,
            this.docid2DataGridViewTextBoxColumn,
            this.docdescDataGridViewTextBoxColumn,
            this.doctypDataGridViewTextBoxColumn,
            this.doccarcodeDataGridViewTextBoxColumn,
            this.dEXROWIDDataGridViewTextBoxColumn,
            this.lOCNCODEDataGridViewTextBoxColumn,
            this.cfgodbDataGridViewTextBoxColumn,
            this.cfgstrDataGridViewTextBoxColumn,
            this.cfgpracDataGridViewTextBoxColumn,
            this.cfgmn2snDataGridViewTextBoxColumn,
            this.cfgdispDataGridViewTextBoxColumn,
            this.cfgpaletyDataGridViewTextBoxColumn,
            this.cfgpaletaidDataGridViewTextBoxColumn,
            this.cfgzakazkaidDataGridViewTextBoxColumn,
            this.cfgmenaidDataGridViewTextBoxColumn,
            this.cfgtiskDataGridViewTextBoxColumn,
            this.cfgprevodskladDataGridViewTextBoxColumn,
            this.cfgtisksoupisDataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.cfglokaceDataGridViewTextBoxColumn,
            this.cfglokaceciselnikDataGridViewTextBoxColumn,
            this.cfglokacedestDataGridViewTextBoxColumn,
            this.cfgonldoppalDataGridViewTextBoxColumn,
            this.cfgonloverlokaceDataGridViewTextBoxColumn,
            this.cfgonloverlokacedestDataGridViewTextBoxColumn,
            this.cfgmnozstvizezboziDataGridViewTextBoxColumn,
            this.cfgpredvyplnitmnozstviDataGridViewTextBoxColumn,
            this.cfgskliddestDataGridViewTextBoxColumn,
            this.predvyplnitskliddestDataGridViewTextBoxColumn,
            this.cfglokmechDataGridViewTextBoxColumn,
            this.cfglokmechpohybtypeDataGridViewTextBoxColumn,
            this.cfgskliddestprevzitDataGridViewTextBoxColumn,
            this.cfglokacedestciselnikDataGridViewTextBoxColumn,
            this.predvyplnitlocncodedestDataGridViewTextBoxColumn,
            this.cfgskladyDataGridViewTextBoxColumn,
            this.cfgonldoplokacedestDataGridViewTextBoxColumn,
            this.cfgdelkaSNDataGridViewTextBoxColumn});
            this.dgTypDokladu.DataSource = this.bsTypDokladu;
            this.dgTypDokladu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTypDokladu.EnableHeadersVisualStyles = false;
            this.dgTypDokladu.FilterAndSortEnabled = true;
            this.dgTypDokladu.Location = new System.Drawing.Point(0, 152);
            this.dgTypDokladu.Name = "dgTypDokladu";
            this.dgTypDokladu.ReadOnly = true;
            this.dgTypDokladu.RowHeadersVisible = false;
            this.dgTypDokladu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTypDokladu.Size = new System.Drawing.Size(574, 316);
            this.dgTypDokladu.TabIndex = 1;
            this.dgTypDokladu.TabStop = false;
            this.dgTypDokladu.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgTypDokladu.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dgTypDokladu.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // bsTypDokladu
            // 
            this.bsTypDokladu.DataMember = "CZMST092";
            this.bsTypDokladu.DataSource = this.dsTypDokladu;
            // 
            // dsTypDokladu
            // 
            this.dsTypDokladu.DataSetName = "TypyDokladu";
            this.dsTypDokladu.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 125);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(574, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 39;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // panelButtonsZobrazeniList
            // 
            this.panelButtonsZobrazeniList.AutoScroll = true;
            this.panelButtonsZobrazeniList.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniList.Location = new System.Drawing.Point(658, 0);
            this.panelButtonsZobrazeniList.Name = "panelButtonsZobrazeniList";
            this.panelButtonsZobrazeniList.Size = new System.Drawing.Size(84, 468);
            this.panelButtonsZobrazeniList.TabIndex = 3;
            // 
            // docidDataGridViewTextBoxColumn
            // 
            this.docidDataGridViewTextBoxColumn.DataPropertyName = "doc_id";
            this.docidDataGridViewTextBoxColumn.HeaderText = "doc_id";
            this.docidDataGridViewTextBoxColumn.Name = "docidDataGridViewTextBoxColumn";
            this.docidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfg_disp_dest
            // 
            this.cfg_disp_dest.DataPropertyName = "cfg_disp_dest";
            this.cfg_disp_dest.HeaderText = "cfg_disp_dest";
            this.cfg_disp_dest.Name = "cfg_disp_dest";
            this.cfg_disp_dest.ReadOnly = true;
            // 
            // cfg_Navrh
            // 
            this.cfg_Navrh.DataPropertyName = "cfg_Navrh";
            this.cfg_Navrh.HeaderText = "cfg_Navrh";
            this.cfg_Navrh.Name = "cfg_Navrh";
            this.cfg_Navrh.ReadOnly = true;
            // 
            // cfg_FIFO_FEFO_check
            // 
            this.cfg_FIFO_FEFO_check.DataPropertyName = "cfg_FIFO_FEFO_check";
            this.cfg_FIFO_FEFO_check.HeaderText = "cfg_FIFO_FEFO_check";
            this.cfg_FIFO_FEFO_check.Name = "cfg_FIFO_FEFO_check";
            this.cfg_FIFO_FEFO_check.ReadOnly = true;
            // 
            // cfg_sarze_ONOFF
            // 
            this.cfg_sarze_ONOFF.DataPropertyName = "cfg_sarze_ONOFF";
            this.cfg_sarze_ONOFF.HeaderText = "cfg_sarze_ONOFF";
            this.cfg_sarze_ONOFF.Name = "cfg_sarze_ONOFF";
            this.cfg_sarze_ONOFF.ReadOnly = true;
            // 
            // cfg_sn_ONOFF
            // 
            this.cfg_sn_ONOFF.DataPropertyName = "cfg_sn_ONOFF";
            this.cfg_sn_ONOFF.HeaderText = "cfg_sn_ONOFF";
            this.cfg_sn_ONOFF.Name = "cfg_sn_ONOFF";
            this.cfg_sn_ONOFF.ReadOnly = true;
            // 
            // cfg_expirace_ONOFF
            // 
            this.cfg_expirace_ONOFF.DataPropertyName = "cfg_expirace_ONOFF";
            this.cfg_expirace_ONOFF.HeaderText = "cfg_expirace_ONOFF";
            this.cfg_expirace_ONOFF.Name = "cfg_expirace_ONOFF";
            this.cfg_expirace_ONOFF.ReadOnly = true;
            // 
            // cfg_AttributeToSN_ONOFF
            // 
            this.cfg_AttributeToSN_ONOFF.DataPropertyName = "cfg_AttributeToSN_ONOFF";
            this.cfg_AttributeToSN_ONOFF.HeaderText = "cfg_AttributeToSN_ONOFF";
            this.cfg_AttributeToSN_ONOFF.Name = "cfg_AttributeToSN_ONOFF";
            this.cfg_AttributeToSN_ONOFF.ReadOnly = true;
            // 
            // docid2DataGridViewTextBoxColumn
            // 
            this.docid2DataGridViewTextBoxColumn.DataPropertyName = "doc_id2";
            this.docid2DataGridViewTextBoxColumn.HeaderText = "doc_id2";
            this.docid2DataGridViewTextBoxColumn.Name = "docid2DataGridViewTextBoxColumn";
            this.docid2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // docdescDataGridViewTextBoxColumn
            // 
            this.docdescDataGridViewTextBoxColumn.DataPropertyName = "doc_desc";
            this.docdescDataGridViewTextBoxColumn.HeaderText = "doc_desc";
            this.docdescDataGridViewTextBoxColumn.Name = "docdescDataGridViewTextBoxColumn";
            this.docdescDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // doctypDataGridViewTextBoxColumn
            // 
            this.doctypDataGridViewTextBoxColumn.DataPropertyName = "doc_typ";
            this.doctypDataGridViewTextBoxColumn.HeaderText = "doc_typ";
            this.doctypDataGridViewTextBoxColumn.Name = "doctypDataGridViewTextBoxColumn";
            this.doctypDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // doccarcodeDataGridViewTextBoxColumn
            // 
            this.doccarcodeDataGridViewTextBoxColumn.DataPropertyName = "doc_carcode";
            this.doccarcodeDataGridViewTextBoxColumn.HeaderText = "doc_carcode";
            this.doccarcodeDataGridViewTextBoxColumn.Name = "doccarcodeDataGridViewTextBoxColumn";
            this.doccarcodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dEXROWIDDataGridViewTextBoxColumn
            // 
            this.dEXROWIDDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn.HeaderText = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn.Name = "dEXROWIDDataGridViewTextBoxColumn";
            this.dEXROWIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lOCNCODEDataGridViewTextBoxColumn
            // 
            this.lOCNCODEDataGridViewTextBoxColumn.DataPropertyName = "LOCNCODE";
            this.lOCNCODEDataGridViewTextBoxColumn.HeaderText = "LOCNCODE";
            this.lOCNCODEDataGridViewTextBoxColumn.Name = "lOCNCODEDataGridViewTextBoxColumn";
            this.lOCNCODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgodbDataGridViewTextBoxColumn
            // 
            this.cfgodbDataGridViewTextBoxColumn.DataPropertyName = "cfg_odb";
            this.cfgodbDataGridViewTextBoxColumn.HeaderText = "cfg_odb";
            this.cfgodbDataGridViewTextBoxColumn.Name = "cfgodbDataGridViewTextBoxColumn";
            this.cfgodbDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgstrDataGridViewTextBoxColumn
            // 
            this.cfgstrDataGridViewTextBoxColumn.DataPropertyName = "cfg_str";
            this.cfgstrDataGridViewTextBoxColumn.HeaderText = "cfg_str";
            this.cfgstrDataGridViewTextBoxColumn.Name = "cfgstrDataGridViewTextBoxColumn";
            this.cfgstrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgpracDataGridViewTextBoxColumn
            // 
            this.cfgpracDataGridViewTextBoxColumn.DataPropertyName = "cfg_prac";
            this.cfgpracDataGridViewTextBoxColumn.HeaderText = "cfg_prac";
            this.cfgpracDataGridViewTextBoxColumn.Name = "cfgpracDataGridViewTextBoxColumn";
            this.cfgpracDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgmn2snDataGridViewTextBoxColumn
            // 
            this.cfgmn2snDataGridViewTextBoxColumn.DataPropertyName = "cfg_mn2sn";
            this.cfgmn2snDataGridViewTextBoxColumn.HeaderText = "cfg_mn2sn";
            this.cfgmn2snDataGridViewTextBoxColumn.Name = "cfgmn2snDataGridViewTextBoxColumn";
            this.cfgmn2snDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgdispDataGridViewTextBoxColumn
            // 
            this.cfgdispDataGridViewTextBoxColumn.DataPropertyName = "cfg_disp";
            this.cfgdispDataGridViewTextBoxColumn.HeaderText = "cfg_disp";
            this.cfgdispDataGridViewTextBoxColumn.Name = "cfgdispDataGridViewTextBoxColumn";
            this.cfgdispDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgpaletyDataGridViewTextBoxColumn
            // 
            this.cfgpaletyDataGridViewTextBoxColumn.DataPropertyName = "cfg_palety";
            this.cfgpaletyDataGridViewTextBoxColumn.HeaderText = "cfg_palety";
            this.cfgpaletyDataGridViewTextBoxColumn.Name = "cfgpaletyDataGridViewTextBoxColumn";
            this.cfgpaletyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgpaletaidDataGridViewTextBoxColumn
            // 
            this.cfgpaletaidDataGridViewTextBoxColumn.DataPropertyName = "cfg_paleta_id";
            this.cfgpaletaidDataGridViewTextBoxColumn.HeaderText = "cfg_paleta_id";
            this.cfgpaletaidDataGridViewTextBoxColumn.Name = "cfgpaletaidDataGridViewTextBoxColumn";
            this.cfgpaletaidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgzakazkaidDataGridViewTextBoxColumn
            // 
            this.cfgzakazkaidDataGridViewTextBoxColumn.DataPropertyName = "cfg_zakazka_id";
            this.cfgzakazkaidDataGridViewTextBoxColumn.HeaderText = "cfg_zakazka_id";
            this.cfgzakazkaidDataGridViewTextBoxColumn.Name = "cfgzakazkaidDataGridViewTextBoxColumn";
            this.cfgzakazkaidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgmenaidDataGridViewTextBoxColumn
            // 
            this.cfgmenaidDataGridViewTextBoxColumn.DataPropertyName = "cfg_mena_id";
            this.cfgmenaidDataGridViewTextBoxColumn.HeaderText = "cfg_mena_id";
            this.cfgmenaidDataGridViewTextBoxColumn.Name = "cfgmenaidDataGridViewTextBoxColumn";
            this.cfgmenaidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgtiskDataGridViewTextBoxColumn
            // 
            this.cfgtiskDataGridViewTextBoxColumn.DataPropertyName = "cfg_tisk";
            this.cfgtiskDataGridViewTextBoxColumn.HeaderText = "cfg_tisk";
            this.cfgtiskDataGridViewTextBoxColumn.Name = "cfgtiskDataGridViewTextBoxColumn";
            this.cfgtiskDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgprevodskladDataGridViewTextBoxColumn
            // 
            this.cfgprevodskladDataGridViewTextBoxColumn.DataPropertyName = "cfg_prevod_sklad";
            this.cfgprevodskladDataGridViewTextBoxColumn.HeaderText = "cfg_prevod_sklad";
            this.cfgprevodskladDataGridViewTextBoxColumn.Name = "cfgprevodskladDataGridViewTextBoxColumn";
            this.cfgprevodskladDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgtisksoupisDataGridViewTextBoxColumn
            // 
            this.cfgtisksoupisDataGridViewTextBoxColumn.DataPropertyName = "cfg_tisk_soupis";
            this.cfgtisksoupisDataGridViewTextBoxColumn.HeaderText = "cfg_tisk_soupis";
            this.cfgtisksoupisDataGridViewTextBoxColumn.Name = "cfgtisksoupisDataGridViewTextBoxColumn";
            this.cfgtisksoupisDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sKLIDDataGridViewTextBoxColumn
            // 
            this.sKLIDDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.HeaderText = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.Name = "sKLIDDataGridViewTextBoxColumn";
            this.sKLIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfglokaceDataGridViewTextBoxColumn
            // 
            this.cfglokaceDataGridViewTextBoxColumn.DataPropertyName = "cfg_lokace";
            this.cfglokaceDataGridViewTextBoxColumn.HeaderText = "cfg_lokace";
            this.cfglokaceDataGridViewTextBoxColumn.Name = "cfglokaceDataGridViewTextBoxColumn";
            this.cfglokaceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfglokaceciselnikDataGridViewTextBoxColumn
            // 
            this.cfglokaceciselnikDataGridViewTextBoxColumn.DataPropertyName = "cfg_lokace_ciselnik";
            this.cfglokaceciselnikDataGridViewTextBoxColumn.HeaderText = "cfg_lokace_ciselnik";
            this.cfglokaceciselnikDataGridViewTextBoxColumn.Name = "cfglokaceciselnikDataGridViewTextBoxColumn";
            this.cfglokaceciselnikDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfglokacedestDataGridViewTextBoxColumn
            // 
            this.cfglokacedestDataGridViewTextBoxColumn.DataPropertyName = "cfg_lokace_dest";
            this.cfglokacedestDataGridViewTextBoxColumn.HeaderText = "cfg_lokace_dest";
            this.cfglokacedestDataGridViewTextBoxColumn.Name = "cfglokacedestDataGridViewTextBoxColumn";
            this.cfglokacedestDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgonldoppalDataGridViewTextBoxColumn
            // 
            this.cfgonldoppalDataGridViewTextBoxColumn.DataPropertyName = "cfg_onl_dop_pal";
            this.cfgonldoppalDataGridViewTextBoxColumn.HeaderText = "cfg_onl_dop_pal";
            this.cfgonldoppalDataGridViewTextBoxColumn.Name = "cfgonldoppalDataGridViewTextBoxColumn";
            this.cfgonldoppalDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgonloverlokaceDataGridViewTextBoxColumn
            // 
            this.cfgonloverlokaceDataGridViewTextBoxColumn.DataPropertyName = "cfg_onl_over_lokace";
            this.cfgonloverlokaceDataGridViewTextBoxColumn.HeaderText = "cfg_onl_over_lokace";
            this.cfgonloverlokaceDataGridViewTextBoxColumn.Name = "cfgonloverlokaceDataGridViewTextBoxColumn";
            this.cfgonloverlokaceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgonloverlokacedestDataGridViewTextBoxColumn
            // 
            this.cfgonloverlokacedestDataGridViewTextBoxColumn.DataPropertyName = "cfg_onl_over_lokace_dest";
            this.cfgonloverlokacedestDataGridViewTextBoxColumn.HeaderText = "cfg_onl_over_lokace_dest";
            this.cfgonloverlokacedestDataGridViewTextBoxColumn.Name = "cfgonloverlokacedestDataGridViewTextBoxColumn";
            this.cfgonloverlokacedestDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgmnozstvizezboziDataGridViewTextBoxColumn
            // 
            this.cfgmnozstvizezboziDataGridViewTextBoxColumn.DataPropertyName = "cfg_mnozstvi_ze_zbozi";
            this.cfgmnozstvizezboziDataGridViewTextBoxColumn.HeaderText = "cfg_mnozstvi_ze_zbozi";
            this.cfgmnozstvizezboziDataGridViewTextBoxColumn.Name = "cfgmnozstvizezboziDataGridViewTextBoxColumn";
            this.cfgmnozstvizezboziDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgpredvyplnitmnozstviDataGridViewTextBoxColumn
            // 
            this.cfgpredvyplnitmnozstviDataGridViewTextBoxColumn.DataPropertyName = "cfg_predvyplnit_mnozstvi";
            this.cfgpredvyplnitmnozstviDataGridViewTextBoxColumn.HeaderText = "cfg_predvyplnit_mnozstvi";
            this.cfgpredvyplnitmnozstviDataGridViewTextBoxColumn.Name = "cfgpredvyplnitmnozstviDataGridViewTextBoxColumn";
            this.cfgpredvyplnitmnozstviDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgskliddestDataGridViewTextBoxColumn
            // 
            this.cfgskliddestDataGridViewTextBoxColumn.DataPropertyName = "cfg_skl_id_dest";
            this.cfgskliddestDataGridViewTextBoxColumn.HeaderText = "cfg_skl_id_dest";
            this.cfgskliddestDataGridViewTextBoxColumn.Name = "cfgskliddestDataGridViewTextBoxColumn";
            this.cfgskliddestDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // predvyplnitskliddestDataGridViewTextBoxColumn
            // 
            this.predvyplnitskliddestDataGridViewTextBoxColumn.DataPropertyName = "predvyplnit_skl_id_dest";
            this.predvyplnitskliddestDataGridViewTextBoxColumn.HeaderText = "predvyplnit_skl_id_dest";
            this.predvyplnitskliddestDataGridViewTextBoxColumn.Name = "predvyplnitskliddestDataGridViewTextBoxColumn";
            this.predvyplnitskliddestDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfglokmechDataGridViewTextBoxColumn
            // 
            this.cfglokmechDataGridViewTextBoxColumn.DataPropertyName = "cfg_lok_mech";
            this.cfglokmechDataGridViewTextBoxColumn.HeaderText = "cfg_lok_mech";
            this.cfglokmechDataGridViewTextBoxColumn.Name = "cfglokmechDataGridViewTextBoxColumn";
            this.cfglokmechDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfglokmechpohybtypeDataGridViewTextBoxColumn
            // 
            this.cfglokmechpohybtypeDataGridViewTextBoxColumn.DataPropertyName = "cfg_lok_mech_pohyb_type";
            this.cfglokmechpohybtypeDataGridViewTextBoxColumn.HeaderText = "cfg_lok_mech_pohyb_type";
            this.cfglokmechpohybtypeDataGridViewTextBoxColumn.Name = "cfglokmechpohybtypeDataGridViewTextBoxColumn";
            this.cfglokmechpohybtypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgskliddestprevzitDataGridViewTextBoxColumn
            // 
            this.cfgskliddestprevzitDataGridViewTextBoxColumn.DataPropertyName = "cfg_skl_id_dest_prevzit";
            this.cfgskliddestprevzitDataGridViewTextBoxColumn.HeaderText = "cfg_skl_id_dest_prevzit";
            this.cfgskliddestprevzitDataGridViewTextBoxColumn.Name = "cfgskliddestprevzitDataGridViewTextBoxColumn";
            this.cfgskliddestprevzitDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfglokacedestciselnikDataGridViewTextBoxColumn
            // 
            this.cfglokacedestciselnikDataGridViewTextBoxColumn.DataPropertyName = "cfg_lokace_dest_ciselnik";
            this.cfglokacedestciselnikDataGridViewTextBoxColumn.HeaderText = "cfg_lokace_dest_ciselnik";
            this.cfglokacedestciselnikDataGridViewTextBoxColumn.Name = "cfglokacedestciselnikDataGridViewTextBoxColumn";
            this.cfglokacedestciselnikDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // predvyplnitlocncodedestDataGridViewTextBoxColumn
            // 
            this.predvyplnitlocncodedestDataGridViewTextBoxColumn.DataPropertyName = "predvyplnit_locncodedest";
            this.predvyplnitlocncodedestDataGridViewTextBoxColumn.HeaderText = "predvyplnit_locncodedest";
            this.predvyplnitlocncodedestDataGridViewTextBoxColumn.Name = "predvyplnitlocncodedestDataGridViewTextBoxColumn";
            this.predvyplnitlocncodedestDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgskladyDataGridViewTextBoxColumn
            // 
            this.cfgskladyDataGridViewTextBoxColumn.DataPropertyName = "cfg_sklady";
            this.cfgskladyDataGridViewTextBoxColumn.HeaderText = "cfg_sklady";
            this.cfgskladyDataGridViewTextBoxColumn.Name = "cfgskladyDataGridViewTextBoxColumn";
            this.cfgskladyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgonldoplokacedestDataGridViewTextBoxColumn
            // 
            this.cfgonldoplokacedestDataGridViewTextBoxColumn.DataPropertyName = "cfg_onl_dop_lokace_dest";
            this.cfgonldoplokacedestDataGridViewTextBoxColumn.HeaderText = "cfg_onl_dop_lokace_dest";
            this.cfgonldoplokacedestDataGridViewTextBoxColumn.Name = "cfgonldoplokacedestDataGridViewTextBoxColumn";
            this.cfgonldoplokacedestDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cfgdelkaSNDataGridViewTextBoxColumn
            // 
            this.cfgdelkaSNDataGridViewTextBoxColumn.DataPropertyName = "cfg_delka_SN";
            this.cfgdelkaSNDataGridViewTextBoxColumn.HeaderText = "cfg_delka_SN";
            this.cfgdelkaSNDataGridViewTextBoxColumn.Name = "cfgdelkaSNDataGridViewTextBoxColumn";
            this.cfgdelkaSNDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FormTypyDokladuList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 468);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dgTypDokladu);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "FormTypyDokladuList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Typ dokladu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTypDokladuList_FormClosing);
            this.Load += new System.EventHandler(this.FormTypDokladuList_Load);
            this.Shown += new System.EventHandler(this.FormTypDokladuList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormTypDokladuList_KeyDown);
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTypDokladu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTypDokladu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTypDokladu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgTypDokladu;
        private System.Windows.Forms.Panel panelButtonsZobrazeniVyber;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.BindingSource bsTypDokladu;
        private System.Windows.Forms.Button buttonVybratUzivatele;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuVyber;
        private System.Windows.Forms.ToolStripMenuItem tsmiVybrat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecVyber;
        private Fask.Interfaces.DataSets.TypyDokladu dsTypDokladu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDocID;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.ComponentModel.BackgroundWorker bwLoadTypyDokladu;
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
        private System.Windows.Forms.ToolStripMenuItem tsmiPolozka;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravit;
        private System.Windows.Forms.ToolStripMenuItem tsmiNovy;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn docidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfg_disp_dest;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfg_Navrh;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfg_FIFO_FEFO_check;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfg_sarze_ONOFF;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfg_sn_ONOFF;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfg_expirace_ONOFF;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfg_AttributeToSN_ONOFF;
        private System.Windows.Forms.DataGridViewTextBoxColumn docid2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn docdescDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn doctypDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn doccarcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOCNCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgodbDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgstrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgpracDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgmn2snDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgdispDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgpaletyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgpaletaidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgzakazkaidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgmenaidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgtiskDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgprevodskladDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgtisksoupisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfglokaceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfglokaceciselnikDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfglokacedestDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgonldoppalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgonloverlokaceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgonloverlokacedestDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgmnozstvizezboziDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgpredvyplnitmnozstviDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgskliddestDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn predvyplnitskliddestDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfglokmechDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfglokmechpohybtypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgskliddestprevzitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfglokacedestciselnikDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn predvyplnitlocncodedestDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgskladyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgonldoplokacedestDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cfgdelkaSNDataGridViewTextBoxColumn;
    }
}