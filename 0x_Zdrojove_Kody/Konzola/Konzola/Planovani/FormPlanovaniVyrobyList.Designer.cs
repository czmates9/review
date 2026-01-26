namespace Konzola.Planovani
{
    partial class FormPlanovaniVyrobyList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPlanovaniVyrobyList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTisk = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExcelOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZaplanovani_Vyroby = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZaplanovani_Vydeje = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdznacKZaplanovani = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOznacKZaplanovani = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiNavrh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiNacteniObchodnihoPozadavku = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPolozka = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNovy = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtp_Zaplanovano_OD = new System.Windows.Forms.DateTimePicker();
            this.cb_Zaplanovano_TimeVariant = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtp_Zaplanovano_DO = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtp_DatumOd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_DatumDo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.chb_Zap = new System.Windows.Forms.CheckBox();
            this.chb_NEzap = new System.Windows.Forms.CheckBox();
            this.tb_Kod = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_Firma = new System.Windows.Forms.TextBox();
            this.tb_VyrZak = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_ITEMNMBR = new System.Windows.Forms.TextBox();
            this.tb_SOPNUMBE = new System.Windows.Forms.TextBox();
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
            this.label6 = new System.Windows.Forms.Label();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.buttonOznacitVse = new System.Windows.Forms.Button();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.bw_PV = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dg_PV = new Zuby.ADGV.AdvancedDataGridView();
            this.OBJ_NMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_COMPANY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_DATE_FROM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_DATE_TO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_ORD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_ITEM_ORD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATE_ZAPLANOVANI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VP_PRPS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VP_PRPS_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VP_PRPS_SOPNUMBE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VP_PRDCT_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPNUMBE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypDok = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_ROW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USERID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEX_ROW_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_PV = new System.Windows.Forms.BindingSource(this.components);
            this.ds_PV = new Fask.Interfaces.DataSets.Vyroba_Planovani();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dg_PV_H = new Zuby.ADGV.AdvancedDataGridView();
            this.oBJNMBRDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJDESCDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJTYPEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJCOMPANYDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJDATEFROMDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJDATETODataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJORDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uSERIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._TYPE_ROW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_PV_H = new System.Windows.Forms.BindingSource(this.components);
            this.advancedDataGridViewSearchToolBar2 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.bwImportPlanovaniVyroby = new System.ComponentModel.BackgroundWorker();
            this.importovatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_PV)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PV_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PV_H)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiExport,
            this.tsmiAkce,
            this.tsmiPolozka});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1174, 24);
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
            // tsmiExport
            // 
            this.tsmiExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTisk,
            this.toolStripSeparator1,
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator6,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExcelOznacene,
            this.toolStripSeparator7,
            this.tsmiExportDoXMLVse,
            this.tsmiExportDoXMLOznacene});
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(55, 20);
            this.tsmiExport.Text = "Výstup";
            // 
            // tsmiTisk
            // 
            this.tsmiTisk.Name = "tsmiTisk";
            this.tsmiTisk.ShortcutKeyDisplayString = "Ctrl+P, Ctrl+R";
            this.tsmiTisk.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tsmiTisk.Size = new System.Drawing.Size(208, 22);
            this.tsmiTisk.Text = "Tisk";
            this.tsmiTisk.Click += new System.EventHandler(this.tiskToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
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
            // tsmiExportDoExcelOznacene
            // 
            this.tsmiExportDoExcelOznacene.Name = "tsmiExportDoExcelOznacene";
            this.tsmiExportDoExcelOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoExcelOznacene.Text = "Export do Excel označené";
            this.tsmiExportDoExcelOznacene.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
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
            // tsmiAkce
            // 
            this.tsmiAkce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiZaplanovani_Vyroby,
            this.tsmiZaplanovani_Vydeje,
            this.tsmiOdznacKZaplanovani,
            this.tsmiOznacKZaplanovani,
            this.toolStripSeparator9,
            this.tsmiNavrh,
            this.toolStripSeparator8,
            this.tsmiNacteniObchodnihoPozadavku,
            this.importovatToolStripMenuItem});
            this.tsmiAkce.Name = "tsmiAkce";
            this.tsmiAkce.Size = new System.Drawing.Size(45, 20);
            this.tsmiAkce.Text = "Akce";
            // 
            // tsmiZaplanovani_Vyroby
            // 
            this.tsmiZaplanovani_Vyroby.Name = "tsmiZaplanovani_Vyroby";
            this.tsmiZaplanovani_Vyroby.Size = new System.Drawing.Size(243, 22);
            this.tsmiZaplanovani_Vyroby.Text = "Zaplánovaní do výroby";
            this.tsmiZaplanovani_Vyroby.Click += new System.EventHandler(this.tsmiZaplanovani_Vyroby_Click);
            // 
            // tsmiZaplanovani_Vydeje
            // 
            this.tsmiZaplanovani_Vydeje.Name = "tsmiZaplanovani_Vydeje";
            this.tsmiZaplanovani_Vydeje.Size = new System.Drawing.Size(243, 22);
            this.tsmiZaplanovani_Vydeje.Text = "Zaplánovaní do výdeje";
            this.tsmiZaplanovani_Vydeje.Click += new System.EventHandler(this.tsmiZaplanovani_Vydeje_Click);
            // 
            // tsmiOdznacKZaplanovani
            // 
            this.tsmiOdznacKZaplanovani.Name = "tsmiOdznacKZaplanovani";
            this.tsmiOdznacKZaplanovani.Size = new System.Drawing.Size(243, 22);
            this.tsmiOdznacKZaplanovani.Text = "Odeber ze zaplánování";
            this.tsmiOdznacKZaplanovani.Click += new System.EventHandler(this.tsmiOdznacKZaplanovani_Click);
            // 
            // tsmiOznacKZaplanovani
            // 
            this.tsmiOznacKZaplanovani.Name = "tsmiOznacKZaplanovani";
            this.tsmiOznacKZaplanovani.Size = new System.Drawing.Size(243, 22);
            this.tsmiOznacKZaplanovani.Text = "Vyber k zaplánování";
            this.tsmiOznacKZaplanovani.Click += new System.EventHandler(this.tsmiOznacKZaplanovani_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(240, 6);
            // 
            // tsmiNavrh
            // 
            this.tsmiNavrh.Name = "tsmiNavrh";
            this.tsmiNavrh.Size = new System.Drawing.Size(243, 22);
            this.tsmiNavrh.Text = "Návrh";
            this.tsmiNavrh.Click += new System.EventHandler(this.tsmiNavrh_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(240, 6);
            // 
            // tsmiNacteniObchodnihoPozadavku
            // 
            this.tsmiNacteniObchodnihoPozadavku.Name = "tsmiNacteniObchodnihoPozadavku";
            this.tsmiNacteniObchodnihoPozadavku.Size = new System.Drawing.Size(243, 22);
            this.tsmiNacteniObchodnihoPozadavku.Text = "Načtení obchodního požadavku";
            this.tsmiNacteniObchodnihoPozadavku.Click += new System.EventHandler(this.tsmiNacteniObchodnihoPozadavku_Click);
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
            this.tsmiOdstranit.Size = new System.Drawing.Size(301, 22);
            this.tsmiOdstranit.Text = "Odstranit / Vrácení obchodního požadavku";
            this.tsmiOdstranit.Click += new System.EventHandler(this.tsmiOdstranit_Click);
            // 
            // tsmiUpravit
            // 
            this.tsmiUpravit.Name = "tsmiUpravit";
            this.tsmiUpravit.Size = new System.Drawing.Size(301, 22);
            this.tsmiUpravit.Text = "Upravit";
            this.tsmiUpravit.Click += new System.EventHandler(this.tsmiUpravit_Click);
            // 
            // tsmiNovy
            // 
            this.tsmiNovy.Name = "tsmiNovy";
            this.tsmiNovy.Size = new System.Drawing.Size(301, 22);
            this.tsmiNovy.Text = "Nový";
            this.tsmiNovy.Click += new System.EventHandler(this.tsmiNovy_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.chb_Zap);
            this.panel1.Controls.Add(this.chb_NEzap);
            this.panel1.Controls.Add(this.tb_Kod);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.tb_Firma);
            this.panel1.Controls.Add(this.tb_VyrZak);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tb_ITEMNMBR);
            this.panel1.Controls.Add(this.tb_SOPNUMBE);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonOdznacitVse);
            this.panel1.Controls.Add(this.buttonOznacitVse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1080, 213);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtp_Zaplanovano_OD);
            this.groupBox2.Controls.Add(this.cb_Zaplanovano_TimeVariant);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.dtp_Zaplanovano_DO);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(219, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(514, 83);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datum zaplánovaní";
            // 
            // dtp_Zaplanovano_OD
            // 
            this.dtp_Zaplanovano_OD.Checked = false;
            this.dtp_Zaplanovano_OD.Location = new System.Drawing.Point(72, 18);
            this.dtp_Zaplanovano_OD.Name = "dtp_Zaplanovano_OD";
            this.dtp_Zaplanovano_OD.ShowCheckBox = true;
            this.dtp_Zaplanovano_OD.Size = new System.Drawing.Size(200, 20);
            this.dtp_Zaplanovano_OD.TabIndex = 56;
            // 
            // cb_Zaplanovano_TimeVariant
            // 
            this.cb_Zaplanovano_TimeVariant.FormattingEnabled = true;
            this.cb_Zaplanovano_TimeVariant.Location = new System.Drawing.Point(368, 28);
            this.cb_Zaplanovano_TimeVariant.Name = "cb_Zaplanovano_TimeVariant";
            this.cb_Zaplanovano_TimeVariant.Size = new System.Drawing.Size(139, 21);
            this.cb_Zaplanovano_TimeVariant.TabIndex = 54;
            this.cb_Zaplanovano_TimeVariant.SelectedIndexChanged += new System.EventHandler(this.cb_Zaplanovano_TimeVariant_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(278, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 59;
            this.label8.Text = "Časová varianta";
            // 
            // dtp_Zaplanovano_DO
            // 
            this.dtp_Zaplanovano_DO.Checked = false;
            this.dtp_Zaplanovano_DO.Location = new System.Drawing.Point(72, 42);
            this.dtp_Zaplanovano_DO.Name = "dtp_Zaplanovano_DO";
            this.dtp_Zaplanovano_DO.ShowCheckBox = true;
            this.dtp_Zaplanovano_DO.Size = new System.Drawing.Size(200, 20);
            this.dtp_Zaplanovano_DO.TabIndex = 55;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 57;
            this.label9.Text = "Datum Do:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 58;
            this.label10.Text = "Datum Od:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtp_DatumOd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtp_DatumDo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(219, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 75);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datum od / Datum do";
            // 
            // dtp_DatumOd
            // 
            this.dtp_DatumOd.Checked = false;
            this.dtp_DatumOd.Location = new System.Drawing.Point(73, 19);
            this.dtp_DatumOd.Name = "dtp_DatumOd";
            this.dtp_DatumOd.ShowCheckBox = true;
            this.dtp_DatumOd.Size = new System.Drawing.Size(200, 20);
            this.dtp_DatumOd.TabIndex = 43;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Datum Od:";
            // 
            // dtp_DatumDo
            // 
            this.dtp_DatumDo.Checked = false;
            this.dtp_DatumDo.Location = new System.Drawing.Point(73, 45);
            this.dtp_DatumDo.Name = "dtp_DatumDo";
            this.dtp_DatumDo.ShowCheckBox = true;
            this.dtp_DatumDo.Size = new System.Drawing.Size(200, 20);
            this.dtp_DatumDo.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "Datum Do:";
            // 
            // chb_Zap
            // 
            this.chb_Zap.AutoSize = true;
            this.chb_Zap.Checked = true;
            this.chb_Zap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Zap.Location = new System.Drawing.Point(102, 176);
            this.chb_Zap.Name = "chb_Zap";
            this.chb_Zap.Size = new System.Drawing.Size(89, 17);
            this.chb_Zap.TabIndex = 45;
            this.chb_Zap.Text = "Zaplánované";
            this.chb_Zap.UseVisualStyleBackColor = true;
            // 
            // chb_NEzap
            // 
            this.chb_NEzap.AutoSize = true;
            this.chb_NEzap.Checked = true;
            this.chb_NEzap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_NEzap.Location = new System.Drawing.Point(102, 149);
            this.chb_NEzap.Name = "chb_NEzap";
            this.chb_NEzap.Size = new System.Drawing.Size(101, 17);
            this.chb_NEzap.TabIndex = 45;
            this.chb_NEzap.Text = "Nezaplánované";
            this.chb_NEzap.UseVisualStyleBackColor = true;
            // 
            // tb_Kod
            // 
            this.tb_Kod.Location = new System.Drawing.Point(102, 99);
            this.tb_Kod.Name = "tb_Kod";
            this.tb_Kod.Size = new System.Drawing.Size(111, 20);
            this.tb_Kod.TabIndex = 42;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(61, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Kód:";
            // 
            // tb_Firma
            // 
            this.tb_Firma.Location = new System.Drawing.Point(102, 76);
            this.tb_Firma.Name = "tb_Firma";
            this.tb_Firma.Size = new System.Drawing.Size(111, 20);
            this.tb_Firma.TabIndex = 42;
            // 
            // tb_VyrZak
            // 
            this.tb_VyrZak.Location = new System.Drawing.Point(102, 123);
            this.tb_VyrZak.Name = "tb_VyrZak";
            this.tb_VyrZak.Size = new System.Drawing.Size(111, 20);
            this.tb_VyrZak.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Firma:";
            // 
            // tb_ITEMNMBR
            // 
            this.tb_ITEMNMBR.Location = new System.Drawing.Point(102, 53);
            this.tb_ITEMNMBR.Name = "tb_ITEMNMBR";
            this.tb_ITEMNMBR.Size = new System.Drawing.Size(111, 20);
            this.tb_ITEMNMBR.TabIndex = 40;
            // 
            // tb_SOPNUMBE
            // 
            this.tb_SOPNUMBE.Location = new System.Drawing.Point(102, 30);
            this.tb_SOPNUMBE.Name = "tb_SOPNUMBE";
            this.tb_SOPNUMBE.Size = new System.Drawing.Size(111, 20);
            this.tb_SOPNUMBE.TabIndex = 40;
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
            this.tsFiltry.Size = new System.Drawing.Size(1080, 25);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Vyrobní zakázka:";
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(958, 43);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(105, 88);
            this.buttonVyhledat.TabIndex = 14;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Č. polžky:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Objednávka č.:";
            // 
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdznacitVse.Location = new System.Drawing.Point(980, 170);
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
            this.buttonOznacitVse.Location = new System.Drawing.Point(891, 170);
            this.buttonOznacitVse.Name = "buttonOznacitVse";
            this.buttonOznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOznacitVse.TabIndex = 15;
            this.buttonOznacitVse.Text = "Označit vše";
            this.buttonOznacitVse.UseVisualStyleBackColor = true;
            this.buttonOznacitVse.Click += new System.EventHandler(this.buttonOznacitVse_Click);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(329, 199);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 30;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // bw_PV
            // 
            this.bw_PV.WorkerSupportsCancellation = true;
            this.bw_PV.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_PV_DoWork);
            this.bw_PV.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_PV_RunWorkerCompleted);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 237);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1080, 474);
            this.tabControl1.TabIndex = 32;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.progressIndicator1);
            this.tabPage1.Controls.Add(this.dg_PV);
            this.tabPage1.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1072, 448);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Přehled";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dg_PV
            // 
            this.dg_PV.AllowUserToAddRows = false;
            this.dg_PV.AllowUserToDeleteRows = false;
            this.dg_PV.AllowUserToOrderColumns = true;
            this.dg_PV.AllowUserToResizeRows = false;
            this.dg_PV.AutoGenerateColumns = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_PV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dg_PV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_PV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OBJ_NMBR,
            this.OBJ_DESC,
            this.OBJ_TYPE,
            this.OBJ_COMPANY,
            this.OBJ_DATE_FROM,
            this.OBJ_DATE_TO,
            this.OBJ_ORD,
            this.OBJ_ITEM_ORD,
            this.ITEMNMBR,
            this.ITEMDESC,
            this.ITEMCODE,
            this.MJ,
            this.QTY,
            this.DATE_ZAPLANOVANI,
            this.VP_PRPS,
            this.VP_PRPS_QTY,
            this.VP_PRPS_SOPNUMBE,
            this.VP_PRDCT_QTY,
            this.SOPNUMBE,
            this.TypDok,
            this.ID_ROW,
            this.SOPDESC,
            this.USERID,
            this.DEX_ROW_ID});
            this.dg_PV.DataSource = this.bs_PV;
            this.dg_PV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_PV.EnableHeadersVisualStyles = false;
            this.dg_PV.FilterAndSortEnabled = true;
            this.dg_PV.Location = new System.Drawing.Point(3, 30);
            this.dg_PV.Name = "dg_PV";
            this.dg_PV.ReadOnly = true;
            this.dg_PV.RowHeadersVisible = false;
            this.dg_PV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_PV.Size = new System.Drawing.Size(1066, 415);
            this.dg_PV.TabIndex = 0;
            this.dg_PV.TabStop = false;
            this.dg_PV.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_PV_CellFormatting);
            this.dg_PV.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dg_PV_CellMouseDown);
            this.dg_PV.DoubleClick += new System.EventHandler(this.dg_PV_DoubleClick);
            this.dg_PV.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dg_PV_MouseClick);
            // 
            // OBJ_NMBR
            // 
            this.OBJ_NMBR.DataPropertyName = "OBJ_NMBR";
            this.OBJ_NMBR.HeaderText = "Číslo objednávky";
            this.OBJ_NMBR.Name = "OBJ_NMBR";
            this.OBJ_NMBR.ReadOnly = true;
            this.OBJ_NMBR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // OBJ_DESC
            // 
            this.OBJ_DESC.DataPropertyName = "OBJ_DESC";
            this.OBJ_DESC.HeaderText = "Popis objednávky";
            this.OBJ_DESC.Name = "OBJ_DESC";
            this.OBJ_DESC.ReadOnly = true;
            this.OBJ_DESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // OBJ_TYPE
            // 
            this.OBJ_TYPE.DataPropertyName = "OBJ_TYPE";
            this.OBJ_TYPE.HeaderText = "Typ objednávky";
            this.OBJ_TYPE.Name = "OBJ_TYPE";
            this.OBJ_TYPE.ReadOnly = true;
            this.OBJ_TYPE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // OBJ_COMPANY
            // 
            this.OBJ_COMPANY.DataPropertyName = "OBJ_COMPANY";
            this.OBJ_COMPANY.HeaderText = "Firma";
            this.OBJ_COMPANY.Name = "OBJ_COMPANY";
            this.OBJ_COMPANY.ReadOnly = true;
            this.OBJ_COMPANY.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // OBJ_DATE_FROM
            // 
            this.OBJ_DATE_FROM.DataPropertyName = "OBJ_DATE_FROM";
            this.OBJ_DATE_FROM.HeaderText = "Datum od";
            this.OBJ_DATE_FROM.Name = "OBJ_DATE_FROM";
            this.OBJ_DATE_FROM.ReadOnly = true;
            this.OBJ_DATE_FROM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // OBJ_DATE_TO
            // 
            this.OBJ_DATE_TO.DataPropertyName = "OBJ_DATE_TO";
            this.OBJ_DATE_TO.HeaderText = "Datum do";
            this.OBJ_DATE_TO.Name = "OBJ_DATE_TO";
            this.OBJ_DATE_TO.ReadOnly = true;
            this.OBJ_DATE_TO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // OBJ_ORD
            // 
            this.OBJ_ORD.DataPropertyName = "OBJ_ORD";
            this.OBJ_ORD.HeaderText = "Číslo řádku dokladu";
            this.OBJ_ORD.Name = "OBJ_ORD";
            this.OBJ_ORD.ReadOnly = true;
            this.OBJ_ORD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // OBJ_ITEM_ORD
            // 
            this.OBJ_ITEM_ORD.DataPropertyName = "OBJ_ITEM_ORD";
            this.OBJ_ITEM_ORD.HeaderText = "Číslo řádku položky";
            this.OBJ_ITEM_ORD.Name = "OBJ_ITEM_ORD";
            this.OBJ_ITEM_ORD.ReadOnly = true;
            this.OBJ_ITEM_ORD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMNMBR
            // 
            this.ITEMNMBR.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR.HeaderText = "Číslo položky";
            this.ITEMNMBR.Name = "ITEMNMBR";
            this.ITEMNMBR.ReadOnly = true;
            this.ITEMNMBR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMDESC
            // 
            this.ITEMDESC.DataPropertyName = "ITEMDESC";
            this.ITEMDESC.HeaderText = "Název položky";
            this.ITEMDESC.Name = "ITEMDESC";
            this.ITEMDESC.ReadOnly = true;
            this.ITEMDESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMCODE
            // 
            this.ITEMCODE.DataPropertyName = "ITEMCODE";
            this.ITEMCODE.HeaderText = "Kód";
            this.ITEMCODE.Name = "ITEMCODE";
            this.ITEMCODE.ReadOnly = true;
            this.ITEMCODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MJ
            // 
            this.MJ.DataPropertyName = "MJ";
            this.MJ.HeaderText = "Měrná jednotka";
            this.MJ.Name = "MJ";
            this.MJ.ReadOnly = true;
            // 
            // QTY
            // 
            this.QTY.DataPropertyName = "QTY";
            this.QTY.HeaderText = "Množství objednané";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DATE_ZAPLANOVANI
            // 
            this.DATE_ZAPLANOVANI.DataPropertyName = "DATE_ZAPLANOVANI";
            this.DATE_ZAPLANOVANI.HeaderText = "Datum zaplánovaní";
            this.DATE_ZAPLANOVANI.Name = "DATE_ZAPLANOVANI";
            this.DATE_ZAPLANOVANI.ReadOnly = true;
            this.DATE_ZAPLANOVANI.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // VP_PRPS
            // 
            this.VP_PRPS.DataPropertyName = "VP_PRPS";
            this.VP_PRPS.HeaderText = "Návrh";
            this.VP_PRPS.Name = "VP_PRPS";
            this.VP_PRPS.ReadOnly = true;
            this.VP_PRPS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VP_PRPS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // VP_PRPS_QTY
            // 
            this.VP_PRPS_QTY.DataPropertyName = "VP_PRPS_QTY";
            this.VP_PRPS_QTY.HeaderText = "Množství návrh";
            this.VP_PRPS_QTY.Name = "VP_PRPS_QTY";
            this.VP_PRPS_QTY.ReadOnly = true;
            this.VP_PRPS_QTY.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // VP_PRPS_SOPNUMBE
            // 
            this.VP_PRPS_SOPNUMBE.DataPropertyName = "VP_PRPS_SOPNUMBE";
            this.VP_PRPS_SOPNUMBE.HeaderText = "Návrh č. dávky";
            this.VP_PRPS_SOPNUMBE.Name = "VP_PRPS_SOPNUMBE";
            this.VP_PRPS_SOPNUMBE.ReadOnly = true;
            // 
            // VP_PRDCT_QTY
            // 
            this.VP_PRDCT_QTY.DataPropertyName = "VP_PRDCT_QTY";
            this.VP_PRDCT_QTY.HeaderText = "Množství zaplánováno";
            this.VP_PRDCT_QTY.Name = "VP_PRDCT_QTY";
            this.VP_PRDCT_QTY.ReadOnly = true;
            this.VP_PRDCT_QTY.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SOPNUMBE
            // 
            this.SOPNUMBE.DataPropertyName = "SOPNUMBE";
            this.SOPNUMBE.HeaderText = "ID zapl. Dokladu";
            this.SOPNUMBE.Name = "SOPNUMBE";
            this.SOPNUMBE.ReadOnly = true;
            this.SOPNUMBE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TypDok
            // 
            this.TypDok.DataPropertyName = "TypDok";
            this.TypDok.HeaderText = "Typ zapl. Dokladu";
            this.TypDok.Name = "TypDok";
            this.TypDok.ReadOnly = true;
            // 
            // ID_ROW
            // 
            this.ID_ROW.DataPropertyName = "ID_ROW";
            this.ID_ROW.HeaderText = "ID řádku pol. v zapl. Dokladu";
            this.ID_ROW.Name = "ID_ROW";
            this.ID_ROW.ReadOnly = true;
            // 
            // SOPDESC
            // 
            this.SOPDESC.DataPropertyName = "SOPDESC";
            this.SOPDESC.HeaderText = "Popis zakázky";
            this.SOPDESC.Name = "SOPDESC";
            this.SOPDESC.ReadOnly = true;
            this.SOPDESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // USERID
            // 
            this.USERID.DataPropertyName = "USERID";
            this.USERID.HeaderText = "Pracovník ID";
            this.USERID.Name = "USERID";
            this.USERID.ReadOnly = true;
            this.USERID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DEX_ROW_ID
            // 
            this.DEX_ROW_ID.DataPropertyName = "DEX_ROW_ID";
            this.DEX_ROW_ID.HeaderText = "Index";
            this.DEX_ROW_ID.Name = "DEX_ROW_ID";
            this.DEX_ROW_ID.ReadOnly = true;
            this.DEX_ROW_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // bs_PV
            // 
            this.bs_PV.DataMember = "FASK_Vyroba_PVP";
            this.bs_PV.DataSource = this.ds_PV;
            // 
            // ds_PV
            // 
            this.ds_PV.DataSetName = "Vyroba";
            this.ds_PV.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(3, 3);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(1066, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 31;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dg_PV_H);
            this.tabPage2.Controls.Add(this.advancedDataGridViewSearchToolBar2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1072, 448);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Jen součty";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dg_PV_H
            // 
            this.dg_PV_H.AllowUserToAddRows = false;
            this.dg_PV_H.AllowUserToDeleteRows = false;
            this.dg_PV_H.AllowUserToOrderColumns = true;
            this.dg_PV_H.AllowUserToResizeRows = false;
            this.dg_PV_H.AutoGenerateColumns = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_PV_H.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dg_PV_H.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_PV_H.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oBJNMBRDataGridViewTextBoxColumn1,
            this.oBJDESCDataGridViewTextBoxColumn1,
            this.oBJTYPEDataGridViewTextBoxColumn1,
            this.oBJCOMPANYDataGridViewTextBoxColumn1,
            this.oBJDATEFROMDataGridViewTextBoxColumn1,
            this.oBJDATETODataGridViewTextBoxColumn1,
            this.oBJORDDataGridViewTextBoxColumn1,
            this.qTYDataGridViewTextBoxColumn1,
            this.uSERIDDataGridViewTextBoxColumn1,
            this._TYPE_ROW});
            this.dg_PV_H.DataSource = this.bs_PV_H;
            this.dg_PV_H.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_PV_H.EnableHeadersVisualStyles = false;
            this.dg_PV_H.FilterAndSortEnabled = true;
            this.dg_PV_H.Location = new System.Drawing.Point(3, 30);
            this.dg_PV_H.Name = "dg_PV_H";
            this.dg_PV_H.ReadOnly = true;
            this.dg_PV_H.RowHeadersVisible = false;
            this.dg_PV_H.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_PV_H.Size = new System.Drawing.Size(1066, 415);
            this.dg_PV_H.TabIndex = 0;
            this.dg_PV_H.TabStop = false;
            this.dg_PV_H.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_PV_H_CellFormatting);
            this.dg_PV_H.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dg_PV_H_MouseClick);
            // 
            // oBJNMBRDataGridViewTextBoxColumn1
            // 
            this.oBJNMBRDataGridViewTextBoxColumn1.DataPropertyName = "OBJ_NMBR";
            this.oBJNMBRDataGridViewTextBoxColumn1.HeaderText = "Číslo objednávky";
            this.oBJNMBRDataGridViewTextBoxColumn1.Name = "oBJNMBRDataGridViewTextBoxColumn1";
            this.oBJNMBRDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // oBJDESCDataGridViewTextBoxColumn1
            // 
            this.oBJDESCDataGridViewTextBoxColumn1.DataPropertyName = "OBJ_DESC";
            this.oBJDESCDataGridViewTextBoxColumn1.HeaderText = "Popis objednávky";
            this.oBJDESCDataGridViewTextBoxColumn1.Name = "oBJDESCDataGridViewTextBoxColumn1";
            this.oBJDESCDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // oBJTYPEDataGridViewTextBoxColumn1
            // 
            this.oBJTYPEDataGridViewTextBoxColumn1.DataPropertyName = "OBJ_TYPE";
            this.oBJTYPEDataGridViewTextBoxColumn1.HeaderText = "Typ objednávky";
            this.oBJTYPEDataGridViewTextBoxColumn1.Name = "oBJTYPEDataGridViewTextBoxColumn1";
            this.oBJTYPEDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // oBJCOMPANYDataGridViewTextBoxColumn1
            // 
            this.oBJCOMPANYDataGridViewTextBoxColumn1.DataPropertyName = "OBJ_COMPANY";
            this.oBJCOMPANYDataGridViewTextBoxColumn1.HeaderText = "Firma";
            this.oBJCOMPANYDataGridViewTextBoxColumn1.Name = "oBJCOMPANYDataGridViewTextBoxColumn1";
            this.oBJCOMPANYDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // oBJDATEFROMDataGridViewTextBoxColumn1
            // 
            this.oBJDATEFROMDataGridViewTextBoxColumn1.DataPropertyName = "OBJ_DATE_FROM";
            this.oBJDATEFROMDataGridViewTextBoxColumn1.HeaderText = "Dátum od";
            this.oBJDATEFROMDataGridViewTextBoxColumn1.Name = "oBJDATEFROMDataGridViewTextBoxColumn1";
            this.oBJDATEFROMDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // oBJDATETODataGridViewTextBoxColumn1
            // 
            this.oBJDATETODataGridViewTextBoxColumn1.DataPropertyName = "OBJ_DATE_TO";
            this.oBJDATETODataGridViewTextBoxColumn1.HeaderText = "Dátum do";
            this.oBJDATETODataGridViewTextBoxColumn1.Name = "oBJDATETODataGridViewTextBoxColumn1";
            this.oBJDATETODataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // oBJORDDataGridViewTextBoxColumn1
            // 
            this.oBJORDDataGridViewTextBoxColumn1.DataPropertyName = "OBJ_ORD";
            this.oBJORDDataGridViewTextBoxColumn1.HeaderText = "Číslo řádku dokladu";
            this.oBJORDDataGridViewTextBoxColumn1.Name = "oBJORDDataGridViewTextBoxColumn1";
            this.oBJORDDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // qTYDataGridViewTextBoxColumn1
            // 
            this.qTYDataGridViewTextBoxColumn1.DataPropertyName = "QTY";
            this.qTYDataGridViewTextBoxColumn1.HeaderText = "Množství objednané";
            this.qTYDataGridViewTextBoxColumn1.Name = "qTYDataGridViewTextBoxColumn1";
            this.qTYDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // uSERIDDataGridViewTextBoxColumn1
            // 
            this.uSERIDDataGridViewTextBoxColumn1.DataPropertyName = "USERID";
            this.uSERIDDataGridViewTextBoxColumn1.HeaderText = "Pracovník ID";
            this.uSERIDDataGridViewTextBoxColumn1.Name = "uSERIDDataGridViewTextBoxColumn1";
            this.uSERIDDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // _TYPE_ROW
            // 
            this._TYPE_ROW.DataPropertyName = "_TYPE_ROW";
            this._TYPE_ROW.HeaderText = "Status";
            this._TYPE_ROW.Name = "_TYPE_ROW";
            this._TYPE_ROW.ReadOnly = true;
            // 
            // bs_PV_H
            // 
            this.bs_PV_H.DataMember = "FASK_Vyroba_PVP_Hlavicky";
            this.bs_PV_H.DataSource = this.ds_PV;
            // 
            // advancedDataGridViewSearchToolBar2
            // 
            this.advancedDataGridViewSearchToolBar2.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar2.Location = new System.Drawing.Point(3, 3);
            this.advancedDataGridViewSearchToolBar2.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar2.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar2.Name = "advancedDataGridViewSearchToolBar2";
            this.advancedDataGridViewSearchToolBar2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar2.Size = new System.Drawing.Size(1066, 27);
            this.advancedDataGridViewSearchToolBar2.TabIndex = 0;
            this.advancedDataGridViewSearchToolBar2.Text = "advancedDataGridViewSearchToolBar2";
            this.advancedDataGridViewSearchToolBar2.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar2_Search);
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(1080, 24);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(94, 687);
            this.panelButtons.TabIndex = 2;
            // 
            // bwImportPlanovaniVyroby
            // 
            this.bwImportPlanovaniVyroby.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwImportPlanovaniVyroby_DoWork);
            this.bwImportPlanovaniVyroby.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwImportPlanovaniVyroby_RunWorkerCompleted);
            // 
            // importovatToolStripMenuItem
            // 
            this.importovatToolStripMenuItem.Name = "importovatToolStripMenuItem";
            this.importovatToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.importovatToolStripMenuItem.Text = "Importovat";
            this.importovatToolStripMenuItem.Click += new System.EventHandler(this.importovatToolStripMenuItem_Click);
            // 
            // FormPlanovaniVyrobyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 711);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormPlanovaniVyrobyList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Plánování výroby";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPlanovaniVyrobyList_FormClosing);
            this.Load += new System.EventHandler(this.FormPlanovaniVyrobyList_Load);
            this.Shown += new System.EventHandler(this.FormPlanovaniVyrobyList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormPlanovaniVyrobyList_KeyDown);
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_PV)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PV_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PV_H)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Button buttonOznacitVse;
        private Zuby.ADGV.AdvancedDataGridView dg_PV;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.Button buttonVyhledat;
        private ProgressControls.ProgressIndicator progressIndicator1;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private System.Windows.Forms.BindingSource bs_PV;
        private Fask.Interfaces.DataSets.Vyroba_Planovani ds_PV;
        private System.ComponentModel.BackgroundWorker bw_PV;
        private System.Windows.Forms.TextBox tb_ITEMNMBR;
        private System.Windows.Forms.TextBox tb_SOPNUMBE;
        private System.Windows.Forms.CheckBox chb_Zap;
        private System.Windows.Forms.CheckBox chb_NEzap;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtp_DatumDo;
        private System.Windows.Forms.DateTimePicker dtp_DatumOd;
        private System.Windows.Forms.TextBox tb_Kod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_Firma;
        private System.Windows.Forms.TextBox tb_VyrZak;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.ToolStripMenuItem tsmiPolozka;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravit;
        private System.Windows.Forms.ToolStripMenuItem tsmiNovy;
        private System.Windows.Forms.ToolStripMenuItem tsmiAkce;
        private System.Windows.Forms.ToolStripMenuItem tsmiOznacKZaplanovani;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem tsmiNacteniObchodnihoPozadavku;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem tsmiNavrh;
        private System.Windows.Forms.ToolStripMenuItem tsmiZaplanovani_Vyroby;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdznacKZaplanovani;
        private System.Windows.Forms.ToolStripMenuItem tsmiTisk;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiZaplanovani_Vydeje;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Zuby.ADGV.AdvancedDataGridView dg_PV_H;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar2;
        private System.Windows.Forms.BindingSource bs_PV_H;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJNMBRDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJDESCDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJTYPEDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJCOMPANYDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJDATEFROMDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJDATETODataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJORDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn _TYPE_ROW;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_NMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_COMPANY;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_DATE_FROM;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_DATE_TO;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_ORD;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_ITEM_ORD;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATE_ZAPLANOVANI;
        private System.Windows.Forms.DataGridViewTextBoxColumn VP_PRPS;
        private System.Windows.Forms.DataGridViewTextBoxColumn VP_PRPS_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn VP_PRPS_SOPNUMBE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VP_PRDCT_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPNUMBE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypDok;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_ROW;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn USERID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEX_ROW_ID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtp_Zaplanovano_OD;
        private System.Windows.Forms.ComboBox cb_Zaplanovano_TimeVariant;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtp_Zaplanovano_DO;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.ComponentModel.BackgroundWorker bwImportPlanovaniVyroby;
        private System.Windows.Forms.ToolStripMenuItem importovatToolStripMenuItem;
    }
}