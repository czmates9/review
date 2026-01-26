namespace Konzola.Servis
{
    partial class FormZdrojeList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormZdrojeList));
            this.dgServis = new Zuby.ADGV.AdvancedDataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oznaceniDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Misto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StavID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StavOznaceni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OkruhID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OkruhOznaceni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsServis = new System.Windows.Forms.BindingSource(this.components);
            this.dsServis = new Fask.Interfaces.DataSets.Servis();
            this.panelButtonsZobrazeniVyber = new Fask.AdvancedButtonsPanel.ButtonsPanel();
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
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbZdrojType = new System.Windows.Forms.ComboBox();
            this.cbZdrojOznaceni = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbZdrojBarcode = new System.Windows.Forms.ComboBox();
            this.cbZdrojID = new System.Windows.Forms.ComboBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenuVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVybrat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiKonecVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonecList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTisk = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTiskReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTiskEtiketa = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPolozka = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDuplikovat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNovy = new System.Windows.Forms.ToolStripMenuItem();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.bwLoadData = new System.ComponentModel.BackgroundWorker();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExceOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgServis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsServis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsServis)).BeginInit();
            this.panelMain.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgServis
            // 
            this.dgServis.AllowUserToAddRows = false;
            this.dgServis.AllowUserToDeleteRows = false;
            this.dgServis.AllowUserToOrderColumns = true;
            this.dgServis.AllowUserToResizeRows = false;
            this.dgServis.AutoGenerateColumns = false;
            this.dgServis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgServis.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.oznaceniDataGridViewTextBoxColumn,
            this.Misto,
            this.barcodeDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.StavID,
            this.StavOznaceni,
            this.OkruhID,
            this.OkruhOznaceni});
            this.dgServis.DataSource = this.bsServis;
            this.dgServis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgServis.EnableHeadersVisualStyles = false;
            this.dgServis.FilterAndSortEnabled = true;
            this.dgServis.Location = new System.Drawing.Point(0, 153);
            this.dgServis.Name = "dgServis";
            this.dgServis.ReadOnly = true;
            this.dgServis.RowHeadersVisible = false;
            this.dgServis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgServis.Size = new System.Drawing.Size(574, 315);
            this.dgServis.TabIndex = 1;
            this.dgServis.TabStop = false;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            dataGridViewCellStyle1.NullValue = "-";
            this.iDDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // oznaceniDataGridViewTextBoxColumn
            // 
            this.oznaceniDataGridViewTextBoxColumn.DataPropertyName = "Oznaceni";
            dataGridViewCellStyle2.NullValue = "-";
            this.oznaceniDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.oznaceniDataGridViewTextBoxColumn.HeaderText = "Označení";
            this.oznaceniDataGridViewTextBoxColumn.Name = "oznaceniDataGridViewTextBoxColumn";
            this.oznaceniDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Misto
            // 
            this.Misto.DataPropertyName = "Misto";
            dataGridViewCellStyle3.NullValue = "-";
            this.Misto.DefaultCellStyle = dataGridViewCellStyle3;
            this.Misto.HeaderText = "Místo";
            this.Misto.Name = "Misto";
            this.Misto.ReadOnly = true;
            // 
            // barcodeDataGridViewTextBoxColumn
            // 
            this.barcodeDataGridViewTextBoxColumn.DataPropertyName = "Barcode";
            dataGridViewCellStyle4.NullValue = "-";
            this.barcodeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.barcodeDataGridViewTextBoxColumn.HeaderText = "Čár. kód";
            this.barcodeDataGridViewTextBoxColumn.Name = "barcodeDataGridViewTextBoxColumn";
            this.barcodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            dataGridViewCellStyle5.NullValue = "-";
            this.typeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.typeDataGridViewTextBoxColumn.HeaderText = "Typ";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // StavID
            // 
            this.StavID.DataPropertyName = "StavID";
            this.StavID.HeaderText = "StavID";
            this.StavID.Name = "StavID";
            this.StavID.ReadOnly = true;
            // 
            // StavOznaceni
            // 
            this.StavOznaceni.DataPropertyName = "StavOznaceni";
            this.StavOznaceni.HeaderText = "StavOznaceni";
            this.StavOznaceni.Name = "StavOznaceni";
            this.StavOznaceni.ReadOnly = true;
            // 
            // OkruhID
            // 
            this.OkruhID.DataPropertyName = "OkruhID";
            this.OkruhID.HeaderText = "OkruhID";
            this.OkruhID.Name = "OkruhID";
            this.OkruhID.ReadOnly = true;
            // 
            // OkruhOznaceni
            // 
            this.OkruhOznaceni.DataPropertyName = "OkruhOznaceni";
            this.OkruhOznaceni.HeaderText = "OkruhOznaceni";
            this.OkruhOznaceni.Name = "OkruhOznaceni";
            this.OkruhOznaceni.ReadOnly = true;
            // 
            // bsServis
            // 
            this.bsServis.DataMember = "CZMST_Servis_Zdroj";
            this.bsServis.DataSource = this.dsServis;
            // 
            // dsServis
            // 
            this.dsServis.DataSetName = "Servis";
            this.dsServis.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panelButtonsZobrazeniVyber
            // 
            this.panelButtonsZobrazeniVyber.AutoScroll = true;
            this.panelButtonsZobrazeniVyber.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniVyber.Location = new System.Drawing.Point(574, 0);
            this.panelButtonsZobrazeniVyber.Name = "panelButtonsZobrazeniVyber";
            this.panelButtonsZobrazeniVyber.Size = new System.Drawing.Size(84, 468);
            this.panelButtonsZobrazeniVyber.TabIndex = 2;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tsFiltry);
            this.panelMain.Controls.Add(this.buttonVyhledat);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.cbZdrojType);
            this.panelMain.Controls.Add(this.cbZdrojOznaceni);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.cbZdrojBarcode);
            this.panelMain.Controls.Add(this.cbZdrojID);
            this.panelMain.Controls.Add(this.menuStrip2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(574, 126);
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
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(241, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Typ:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Označení:";
            // 
            // cbZdrojType
            // 
            this.cbZdrojType.FormattingEnabled = true;
            this.cbZdrojType.Location = new System.Drawing.Point(275, 79);
            this.cbZdrojType.Name = "cbZdrojType";
            this.cbZdrojType.Size = new System.Drawing.Size(135, 21);
            this.cbZdrojType.TabIndex = 4;
            // 
            // cbZdrojOznaceni
            // 
            this.cbZdrojOznaceni.FormattingEnabled = true;
            this.cbZdrojOznaceni.Location = new System.Drawing.Point(77, 79);
            this.cbZdrojOznaceni.Name = "cbZdrojOznaceni";
            this.cbZdrojOznaceni.Size = new System.Drawing.Size(135, 21);
            this.cbZdrojOznaceni.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Čár. kód:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "ID:";
            // 
            // cbZdrojBarcode
            // 
            this.cbZdrojBarcode.FormattingEnabled = true;
            this.cbZdrojBarcode.Location = new System.Drawing.Point(275, 52);
            this.cbZdrojBarcode.Name = "cbZdrojBarcode";
            this.cbZdrojBarcode.Size = new System.Drawing.Size(135, 21);
            this.cbZdrojBarcode.TabIndex = 3;
            // 
            // cbZdrojID
            // 
            this.cbZdrojID.FormattingEnabled = true;
            this.cbZdrojID.Location = new System.Drawing.Point(77, 52);
            this.cbZdrojID.Name = "cbZdrojID";
            this.cbZdrojID.Size = new System.Drawing.Size(135, 21);
            this.cbZdrojID.TabIndex = 1;
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuVyber,
            this.tsmiMenuList,
            this.tsmiTisk,
            this.tsmiExport,
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
            this.tsmiVybrat.Size = new System.Drawing.Size(180, 22);
            this.tsmiVybrat.Text = "Vybrat zdroj";
            this.tsmiVybrat.Click += new System.EventHandler(this.tsmiVybrat_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiKonecVyber
            // 
            this.tsmiKonecVyber.Name = "tsmiKonecVyber";
            this.tsmiKonecVyber.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonecVyber.Size = new System.Drawing.Size(180, 22);
            this.tsmiKonecVyber.Text = "Konec";
            this.tsmiKonecVyber.Click += new System.EventHandler(this.tsmiKonec_Click);
            // 
            // tsmiMenuList
            // 
            this.tsmiMenuList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiKonecList});
            this.tsmiMenuList.Name = "tsmiMenuList";
            this.tsmiMenuList.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenuList.Text = "Menu";
            // 
            // tsmiKonecList
            // 
            this.tsmiKonecList.Name = "tsmiKonecList";
            this.tsmiKonecList.Size = new System.Drawing.Size(107, 22);
            this.tsmiKonecList.Text = "Konec";
            this.tsmiKonecList.Click += new System.EventHandler(this.tsmiKonec_Click);
            // 
            // tsmiTisk
            // 
            this.tsmiTisk.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTiskReport,
            this.tsmiTiskEtiketa});
            this.tsmiTisk.Name = "tsmiTisk";
            this.tsmiTisk.Size = new System.Drawing.Size(39, 20);
            this.tsmiTisk.Text = "Tisk";
            // 
            // tsmiTiskReport
            // 
            this.tsmiTiskReport.Enabled = false;
            this.tsmiTiskReport.Name = "tsmiTiskReport";
            this.tsmiTiskReport.Size = new System.Drawing.Size(132, 22);
            this.tsmiTiskReport.Text = "Tisk report";
            this.tsmiTiskReport.Click += new System.EventHandler(this.tsmiTiskReport_Click);
            // 
            // tsmiTiskEtiketa
            // 
            this.tsmiTiskEtiketa.Name = "tsmiTiskEtiketa";
            this.tsmiTiskEtiketa.Size = new System.Drawing.Size(132, 22);
            this.tsmiTiskEtiketa.Text = "Tisk etiketa";
            this.tsmiTiskEtiketa.Click += new System.EventHandler(this.tsmiTiskEtiketa_Click);
            // 
            // tsmiPolozka
            // 
            this.tsmiPolozka.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOdstranit,
            this.tsmiDuplikovat,
            this.tsmiUpravit,
            this.tsmiNovy});
            this.tsmiPolozka.Name = "tsmiPolozka";
            this.tsmiPolozka.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tsmiPolozka.Size = new System.Drawing.Size(60, 20);
            this.tsmiPolozka.Text = "Položka";
            // 
            // tsmiOdstranit
            // 
            this.tsmiOdstranit.Name = "tsmiOdstranit";
            this.tsmiOdstranit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiOdstranit.Size = new System.Drawing.Size(173, 22);
            this.tsmiOdstranit.Text = "&Odstranit";
            this.tsmiOdstranit.Click += new System.EventHandler(this.tsmiOdstranit_Click);
            // 
            // tsmiDuplikovat
            // 
            this.tsmiDuplikovat.Name = "tsmiDuplikovat";
            this.tsmiDuplikovat.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.tsmiDuplikovat.Size = new System.Drawing.Size(173, 22);
            this.tsmiDuplikovat.Text = "&Duplikovat";
            this.tsmiDuplikovat.Click += new System.EventHandler(this.tsmiDuplikovat_Click);
            // 
            // tsmiUpravit
            // 
            this.tsmiUpravit.Name = "tsmiUpravit";
            this.tsmiUpravit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.tsmiUpravit.Size = new System.Drawing.Size(173, 22);
            this.tsmiUpravit.Text = "&Upravit";
            this.tsmiUpravit.Click += new System.EventHandler(this.tsmiUpravit_Click);
            // 
            // tsmiNovy
            // 
            this.tsmiNovy.Name = "tsmiNovy";
            this.tsmiNovy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiNovy.Size = new System.Drawing.Size(173, 22);
            this.tsmiNovy.Text = "&Nový";
            this.tsmiNovy.Click += new System.EventHandler(this.tsmiNovy_Click);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(253, 266);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 38;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // bwLoadData
            // 
            this.bwLoadData.WorkerSupportsCancellation = true;
            this.bwLoadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadZbozi_DoWork);
            this.bwLoadData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadZbozi_RunWorkerCompleted);
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
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 126);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(574, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 39;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            // 
            // tsmiExport
            // 
            this.tsmiExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator6,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExceOznacene,
            this.toolStripSeparator7,
            this.tsmiExportDoXMLVse,
            this.tsmiExportDoXMLOznacene});
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(55, 20);
            this.tsmiExport.Text = "Výstup";
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
            // FormZdrojeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 468);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dgServis);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "FormZdrojeList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výběr zdroje";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormZdrojeList_FormClosing);
            this.Load += new System.EventHandler(this.FormZdrojeList_Load);
            this.Shown += new System.EventHandler(this.FormZboziSelect_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormZdrojeList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgServis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsServis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsServis)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgServis;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtonsZobrazeniVyber;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.BindingSource bsServis;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuVyber;
        private System.Windows.Forms.ToolStripMenuItem tsmiVybrat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecVyber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbZdrojOznaceni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbZdrojID;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.ComponentModel.BackgroundWorker bwLoadData;
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
        private System.Windows.Forms.ToolStripMenuItem tsmiPolozka;
        private Fask.Interfaces.DataSets.Servis dsServis;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbZdrojType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbZdrojBarcode;
        private System.Windows.Forms.ToolStripMenuItem tsmiDuplikovat;
        private System.Windows.Forms.ToolStripMenuItem tsmiNovy;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravit;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranit;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oznaceniDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Misto;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StavID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StavOznaceni;
        private System.Windows.Forms.DataGridViewTextBoxColumn OkruhID;
        private System.Windows.Forms.DataGridViewTextBoxColumn OkruhOznaceni;
        private System.Windows.Forms.ToolStripMenuItem tsmiTisk;
        private System.Windows.Forms.ToolStripMenuItem tsmiTiskReport;
        private System.Windows.Forms.ToolStripMenuItem tsmiTiskEtiketa;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuList;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecList;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExport;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoExceOznacene;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
    }
}