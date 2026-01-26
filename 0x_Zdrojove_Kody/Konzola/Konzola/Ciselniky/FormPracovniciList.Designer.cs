namespace Konzola.Ciselniky
{
    partial class FormPracovniciList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPracovniciList));
            this.dgPracovnici = new Zuby.ADGV.AdvancedDataGridView();
            this.pracidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pracdescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.practypDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.praccarcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEXROWIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsPracovnici = new System.Windows.Forms.BindingSource(this.components);
            this.dsPracovnici = new Fask.Interfaces.DataSets.Pracovnici();
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbUserLogin = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbUserID = new System.Windows.Forms.ComboBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenuVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVybrat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiKonecVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonecList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskEtiketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExcelOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSynchronizovatsAD = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPolozka = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNovy = new System.Windows.Forms.ToolStripMenuItem();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.bwLoadPracovnici = new System.ComponentModel.BackgroundWorker();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.bw_SynchronizaceAD = new System.ComponentModel.BackgroundWorker();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.tsmiImportovatPracovniky = new System.Windows.Forms.ToolStripMenuItem();
            this.bwImportPracovnici = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgPracovnici)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPracovnici)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPracovnici)).BeginInit();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgPracovnici
            // 
            this.dgPracovnici.AllowUserToAddRows = false;
            this.dgPracovnici.AllowUserToDeleteRows = false;
            this.dgPracovnici.AllowUserToOrderColumns = true;
            this.dgPracovnici.AllowUserToResizeRows = false;
            this.dgPracovnici.AutoGenerateColumns = false;
            this.dgPracovnici.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPracovnici.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pracidDataGridViewTextBoxColumn,
            this.pracdescDataGridViewTextBoxColumn,
            this.practypDataGridViewTextBoxColumn,
            this.praccarcodeDataGridViewTextBoxColumn,
            this.dEXROWIDDataGridViewTextBoxColumn});
            this.dgPracovnici.DataSource = this.bsPracovnici;
            this.dgPracovnici.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPracovnici.EnableHeadersVisualStyles = false;
            this.dgPracovnici.FilterAndSortEnabled = true;
            this.dgPracovnici.Location = new System.Drawing.Point(0, 149);
            this.dgPracovnici.Name = "dgPracovnici";
            this.dgPracovnici.ReadOnly = true;
            this.dgPracovnici.RowHeadersVisible = false;
            this.dgPracovnici.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPracovnici.Size = new System.Drawing.Size(574, 319);
            this.dgPracovnici.TabIndex = 1;
            this.dgPracovnici.TabStop = false;
            this.dgPracovnici.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgPracovnici.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dgPracovnici.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // pracidDataGridViewTextBoxColumn
            // 
            this.pracidDataGridViewTextBoxColumn.DataPropertyName = "prac_id";
            this.pracidDataGridViewTextBoxColumn.HeaderText = "ID pracovnika";
            this.pracidDataGridViewTextBoxColumn.Name = "pracidDataGridViewTextBoxColumn";
            this.pracidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pracdescDataGridViewTextBoxColumn
            // 
            this.pracdescDataGridViewTextBoxColumn.DataPropertyName = "prac_desc";
            this.pracdescDataGridViewTextBoxColumn.HeaderText = "Popis pracovníka";
            this.pracdescDataGridViewTextBoxColumn.Name = "pracdescDataGridViewTextBoxColumn";
            this.pracdescDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // practypDataGridViewTextBoxColumn
            // 
            this.practypDataGridViewTextBoxColumn.DataPropertyName = "prac_typ";
            this.practypDataGridViewTextBoxColumn.HeaderText = "Typ pracovnka";
            this.practypDataGridViewTextBoxColumn.Name = "practypDataGridViewTextBoxColumn";
            this.practypDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // praccarcodeDataGridViewTextBoxColumn
            // 
            this.praccarcodeDataGridViewTextBoxColumn.DataPropertyName = "prac_carcode";
            this.praccarcodeDataGridViewTextBoxColumn.HeaderText = "čar. kod pracovnika";
            this.praccarcodeDataGridViewTextBoxColumn.Name = "praccarcodeDataGridViewTextBoxColumn";
            this.praccarcodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dEXROWIDDataGridViewTextBoxColumn
            // 
            this.dEXROWIDDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.dEXROWIDDataGridViewTextBoxColumn.Name = "dEXROWIDDataGridViewTextBoxColumn";
            this.dEXROWIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsPracovnici
            // 
            this.bsPracovnici.DataMember = "CZMST096";
            this.bsPracovnici.DataSource = this.dsPracovnici;
            // 
            // dsPracovnici
            // 
            this.dsPracovnici.DataSetName = "Pracovnici";
            this.dsPracovnici.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.buttonVybratUzivatele.Text = "Vybrat uživatele";
            this.buttonVybratUzivatele.UseVisualStyleBackColor = true;
            this.buttonVybratUzivatele.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tsFiltry);
            this.panelMain.Controls.Add(this.buttonVyhledat);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.cbUserLogin);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.cbUserID);
            this.panelMain.Controls.Add(this.menuStrip2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(574, 122);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Popis:";
            // 
            // cbUserLogin
            // 
            this.cbUserLogin.FormattingEnabled = true;
            this.cbUserLogin.Location = new System.Drawing.Point(93, 79);
            this.cbUserLogin.Name = "cbUserLogin";
            this.cbUserLogin.Size = new System.Drawing.Size(135, 21);
            this.cbUserLogin.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "ID pracovnika:";
            // 
            // cbUserID
            // 
            this.cbUserID.FormattingEnabled = true;
            this.cbUserID.Location = new System.Drawing.Point(93, 52);
            this.cbUserID.Name = "cbUserID";
            this.cbUserID.Size = new System.Drawing.Size(135, 21);
            this.cbUserID.TabIndex = 1;
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuVyber,
            this.tsmiMenuList,
            this.tsmiExport,
            this.tsmiAkce,
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
            this.toolStripSeparator10,
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
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(148, 6);
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
            // tsmiExport
            // 
            this.tsmiExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiskToolStripMenuItem,
            this.tiskEtiketToolStripMenuItem,
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator1,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExcelOznacene,
            this.toolStripSeparator6,
            this.tsmiExportDoXMLVse,
            this.tsmiExportDoXMLOznacene});
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(55, 20);
            this.tsmiExport.Text = "Výstup";
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
            // tiskEtiketToolStripMenuItem
            // 
            this.tiskEtiketToolStripMenuItem.Name = "tiskEtiketToolStripMenuItem";
            this.tiskEtiketToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+E, Ctrl+T";
            this.tiskEtiketToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tiskEtiketToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.tiskEtiketToolStripMenuItem.Text = "Tisk etiket";
            this.tiskEtiketToolStripMenuItem.Click += new System.EventHandler(this.tiskEtiketToolStripMenuItem_Click);
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
            // tsmiAkce
            // 
            this.tsmiAkce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSynchronizovatsAD,
            this.tsmiImportovatPracovniky});
            this.tsmiAkce.Name = "tsmiAkce";
            this.tsmiAkce.Size = new System.Drawing.Size(45, 20);
            this.tsmiAkce.Text = "Akce";
            // 
            // tsmiSynchronizovatsAD
            // 
            this.tsmiSynchronizovatsAD.Name = "tsmiSynchronizovatsAD";
            this.tsmiSynchronizovatsAD.Size = new System.Drawing.Size(182, 22);
            this.tsmiSynchronizovatsAD.Text = "Synchronizovat s AD";
            this.tsmiSynchronizovatsAD.Click += new System.EventHandler(this.tsmiSynchronizovatsAD_Click);
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
            this.progressIndicator1.Location = new System.Drawing.Point(252, 275);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(98, 98);
            this.progressIndicator1.TabIndex = 38;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // bwLoadPracovnici
            // 
            this.bwLoadPracovnici.WorkerSupportsCancellation = true;
            this.bwLoadPracovnici.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadZbozi_DoWork);
            this.bwLoadPracovnici.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadZbozi_RunWorkerCompleted);
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
            // bw_SynchronizaceAD
            // 
            this.bw_SynchronizaceAD.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_SynchronizaceAD_DoWork);
            this.bw_SynchronizaceAD.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_SynchronizaceAD_RunWorkerCompleted);
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 122);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(574, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 39;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // tsmiImportovatPracovniky
            // 
            this.tsmiImportovatPracovniky.Name = "tsmiImportovatPracovniky";
            this.tsmiImportovatPracovniky.Size = new System.Drawing.Size(182, 22);
            this.tsmiImportovatPracovniky.Text = "Importovat";
            this.tsmiImportovatPracovniky.Click += new System.EventHandler(this.tsmiImportovatPracovniky_Click);
            // 
            // bwImportPracovnici
            // 
            this.bwImportPracovnici.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwImportPracovnici_DoWork);
            this.bwImportPracovnici.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwImportPracovnici_RunWorkerCompleted);
            // 
            // FormPracovniciList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 468);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dgPracovnici);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "FormPracovniciList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výběr pracovnika";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPracovniciList_FormClosing);
            this.Load += new System.EventHandler(this.FormPracovniciList_Load);
            this.Shown += new System.EventHandler(this.FormPracovniciList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormPracovniciList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgPracovnici)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPracovnici)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPracovnici)).EndInit();
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
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

        private Zuby.ADGV.AdvancedDataGridView dgPracovnici;
        private System.Windows.Forms.Panel panelButtonsZobrazeniVyber;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.BindingSource bsPracovnici;
        private System.Windows.Forms.Button buttonVybratUzivatele;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuVyber;
        private System.Windows.Forms.ToolStripMenuItem tsmiVybrat;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecVyber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbUserLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbUserID;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.ComponentModel.BackgroundWorker bwLoadPracovnici;
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
        private Fask.Interfaces.DataSets.Pracovnici dsPracovnici;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuList;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.DataGridViewTextBoxColumn pracidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pracdescDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn practypDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn praccarcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDDataGridViewTextBoxColumn;
        private System.ComponentModel.BackgroundWorker bw_SynchronizaceAD;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private System.Windows.Forms.ToolStripMenuItem tsmiAkce;
        private System.Windows.Forms.ToolStripMenuItem tsmiSynchronizovatsAD;
        private System.Windows.Forms.ToolStripMenuItem tsmiPolozka;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravit;
        private System.Windows.Forms.ToolStripMenuItem tsmiNovy;
        private System.Windows.Forms.ToolStripMenuItem tiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tiskEtiketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportovatPracovniky;
        private System.ComponentModel.BackgroundWorker bwImportPracovnici;
    }
}