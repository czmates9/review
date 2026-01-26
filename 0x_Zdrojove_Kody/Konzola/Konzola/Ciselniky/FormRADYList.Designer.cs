namespace Konzola.Ciselniky
{
    partial class FormRADYList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRADYList));
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tb_ID = new System.Windows.Forms.TextBox();
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
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenuVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVyber = new System.Windows.Forms.ToolStripMenuItem();
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
            this.dgRady = new Zuby.ADGV.AdvancedDataGridView();
            this.bsRady = new System.Windows.Forms.BindingSource(this.components);
            this.dsRady = new Fask.Interfaces.DataSets.Rady();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.platnostOdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.platnostDoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modulDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modulIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modulID2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modulFunkceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radaIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radaNazevDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radaPrefixDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radaCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filtrSkladIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filtrUserIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vlozStrediskoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vlozCinnostDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vlozZakazkaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kontrola_Disponability = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Vyber_Typ_Prevodka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prodej_Prijemka_Tisk_Tiskarna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prodej_Prijemka_Tisk_ID_sablona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prodej_Vydejka_Tisk_Tiskarna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prodej_Vydejka_Tisk_ID_sablona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prodej_Prevodka_Tisk_Tiskarna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prodej_Prevodka_Tisk_ID_sablona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRady)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRady)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRady)).BeginInit();
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
            this.buttonVybratUzivatele.Text = "Vybrat řadu";
            this.buttonVybratUzivatele.UseVisualStyleBackColor = true;
            this.buttonVybratUzivatele.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tb_ID);
            this.panelMain.Controls.Add(this.tsFiltry);
            this.panelMain.Controls.Add(this.buttonVyhledat);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.menuStrip2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(574, 122);
            this.panelMain.TabIndex = 1;
            // 
            // tb_ID
            // 
            this.tb_ID.Location = new System.Drawing.Point(49, 67);
            this.tb_ID.Name = "tb_ID";
            this.tb_ID.Size = new System.Drawing.Size(135, 20);
            this.tb_ID.TabIndex = 42;
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
            this.label3.Location = new System.Drawing.Point(22, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "ID:";
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
            this.tsmiVyber,
            this.toolStripSeparator1,
            this.tsmiKonecVyber});
            this.tsmiMenuVyber.Name = "tsmiMenuVyber";
            this.tsmiMenuVyber.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenuVyber.Text = "Menu";
            // 
            // tsmiVyber
            // 
            this.tsmiVyber.Name = "tsmiVyber";
            this.tsmiVyber.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiVyber.Size = new System.Drawing.Size(151, 22);
            this.tsmiVyber.Text = "Vybrat";
            this.tsmiVyber.Click += new System.EventHandler(this.buttonNovy_Click);
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
            this.progressIndicator1.Location = new System.Drawing.Point(255, 262);
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
            // dgRady
            // 
            this.dgRady.AllowUserToAddRows = false;
            this.dgRady.AllowUserToDeleteRows = false;
            this.dgRady.AllowUserToOrderColumns = true;
            this.dgRady.AllowUserToResizeRows = false;
            this.dgRady.AutoGenerateColumns = false;
            this.dgRady.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRady.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.defaultDataGridViewCheckBoxColumn,
            this.platnostOdDataGridViewTextBoxColumn,
            this.platnostDoDataGridViewTextBoxColumn,
            this.modulDataGridViewTextBoxColumn,
            this.modulIDDataGridViewTextBoxColumn,
            this.modulID2DataGridViewTextBoxColumn,
            this.modulFunkceDataGridViewTextBoxColumn,
            this.radaIDDataGridViewTextBoxColumn,
            this.radaNazevDataGridViewTextBoxColumn,
            this.radaPrefixDataGridViewTextBoxColumn,
            this.radaCountDataGridViewTextBoxColumn,
            this.filtrSkladIDDataGridViewTextBoxColumn,
            this.filtrUserIDDataGridViewTextBoxColumn,
            this.vlozStrediskoDataGridViewTextBoxColumn,
            this.vlozCinnostDataGridViewTextBoxColumn,
            this.vlozZakazkaDataGridViewTextBoxColumn,
            this.Kontrola_Disponability,
            this.Vyber_Typ_Prevodka,
            this.Prodej_Prijemka_Tisk_Tiskarna,
            this.Prodej_Prijemka_Tisk_ID_sablona,
            this.Prodej_Vydejka_Tisk_Tiskarna,
            this.Prodej_Vydejka_Tisk_ID_sablona,
            this.Prodej_Prevodka_Tisk_Tiskarna,
            this.Prodej_Prevodka_Tisk_ID_sablona});
            this.dgRady.DataSource = this.bsRady;
            this.dgRady.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgRady.EnableHeadersVisualStyles = false;
            this.dgRady.FilterAndSortEnabled = true;
            this.dgRady.Location = new System.Drawing.Point(0, 149);
            this.dgRady.Name = "dgRady";
            this.dgRady.ReadOnly = true;
            this.dgRady.RowHeadersVisible = false;
            this.dgRady.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRady.Size = new System.Drawing.Size(574, 319);
            this.dgRady.TabIndex = 1;
            this.dgRady.TabStop = false;
            this.dgRady.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgRady.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dgRady.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // bsRady
            // 
            this.bsRady.DataMember = "FASK_RADY";
            this.bsRady.DataSource = this.dsRady;
            // 
            // dsRady
            // 
            this.dsRady.DataSetName = "Rady";
            this.dsRady.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // panelButtonsZobrazeniList
            // 
            this.panelButtonsZobrazeniList.AutoScroll = true;
            this.panelButtonsZobrazeniList.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniList.Location = new System.Drawing.Point(658, 0);
            this.panelButtonsZobrazeniList.Name = "panelButtonsZobrazeniList";
            this.panelButtonsZobrazeniList.Size = new System.Drawing.Size(84, 468);
            this.panelButtonsZobrazeniList.TabIndex = 3;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // defaultDataGridViewCheckBoxColumn
            // 
            this.defaultDataGridViewCheckBoxColumn.DataPropertyName = "Default";
            this.defaultDataGridViewCheckBoxColumn.HeaderText = "Default";
            this.defaultDataGridViewCheckBoxColumn.Name = "defaultDataGridViewCheckBoxColumn";
            this.defaultDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // platnostOdDataGridViewTextBoxColumn
            // 
            this.platnostOdDataGridViewTextBoxColumn.DataPropertyName = "PlatnostOd";
            this.platnostOdDataGridViewTextBoxColumn.HeaderText = "PlatnostOd";
            this.platnostOdDataGridViewTextBoxColumn.Name = "platnostOdDataGridViewTextBoxColumn";
            this.platnostOdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // platnostDoDataGridViewTextBoxColumn
            // 
            this.platnostDoDataGridViewTextBoxColumn.DataPropertyName = "PlatnostDo";
            this.platnostDoDataGridViewTextBoxColumn.HeaderText = "PlatnostDo";
            this.platnostDoDataGridViewTextBoxColumn.Name = "platnostDoDataGridViewTextBoxColumn";
            this.platnostDoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // modulDataGridViewTextBoxColumn
            // 
            this.modulDataGridViewTextBoxColumn.DataPropertyName = "Modul";
            this.modulDataGridViewTextBoxColumn.HeaderText = "Modul";
            this.modulDataGridViewTextBoxColumn.Name = "modulDataGridViewTextBoxColumn";
            this.modulDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // modulIDDataGridViewTextBoxColumn
            // 
            this.modulIDDataGridViewTextBoxColumn.DataPropertyName = "Modul_ID";
            this.modulIDDataGridViewTextBoxColumn.HeaderText = "Modul_ID";
            this.modulIDDataGridViewTextBoxColumn.Name = "modulIDDataGridViewTextBoxColumn";
            this.modulIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // modulID2DataGridViewTextBoxColumn
            // 
            this.modulID2DataGridViewTextBoxColumn.DataPropertyName = "Modul_ID2";
            this.modulID2DataGridViewTextBoxColumn.HeaderText = "Modul_ID2";
            this.modulID2DataGridViewTextBoxColumn.Name = "modulID2DataGridViewTextBoxColumn";
            this.modulID2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // modulFunkceDataGridViewTextBoxColumn
            // 
            this.modulFunkceDataGridViewTextBoxColumn.DataPropertyName = "Modul_Funkce";
            this.modulFunkceDataGridViewTextBoxColumn.HeaderText = "Modul_Funkce";
            this.modulFunkceDataGridViewTextBoxColumn.Name = "modulFunkceDataGridViewTextBoxColumn";
            this.modulFunkceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // radaIDDataGridViewTextBoxColumn
            // 
            this.radaIDDataGridViewTextBoxColumn.DataPropertyName = "Rada_ID";
            this.radaIDDataGridViewTextBoxColumn.HeaderText = "Rada_ID";
            this.radaIDDataGridViewTextBoxColumn.Name = "radaIDDataGridViewTextBoxColumn";
            this.radaIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // radaNazevDataGridViewTextBoxColumn
            // 
            this.radaNazevDataGridViewTextBoxColumn.DataPropertyName = "Rada_Nazev";
            this.radaNazevDataGridViewTextBoxColumn.HeaderText = "Rada_Nazev";
            this.radaNazevDataGridViewTextBoxColumn.Name = "radaNazevDataGridViewTextBoxColumn";
            this.radaNazevDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // radaPrefixDataGridViewTextBoxColumn
            // 
            this.radaPrefixDataGridViewTextBoxColumn.DataPropertyName = "Rada_Prefix";
            this.radaPrefixDataGridViewTextBoxColumn.HeaderText = "Rada_Prefix";
            this.radaPrefixDataGridViewTextBoxColumn.Name = "radaPrefixDataGridViewTextBoxColumn";
            this.radaPrefixDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // radaCountDataGridViewTextBoxColumn
            // 
            this.radaCountDataGridViewTextBoxColumn.DataPropertyName = "Rada_Count";
            this.radaCountDataGridViewTextBoxColumn.HeaderText = "Rada_Count";
            this.radaCountDataGridViewTextBoxColumn.Name = "radaCountDataGridViewTextBoxColumn";
            this.radaCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // filtrSkladIDDataGridViewTextBoxColumn
            // 
            this.filtrSkladIDDataGridViewTextBoxColumn.DataPropertyName = "Filtr_SkladID";
            this.filtrSkladIDDataGridViewTextBoxColumn.HeaderText = "Filtr_SkladID";
            this.filtrSkladIDDataGridViewTextBoxColumn.Name = "filtrSkladIDDataGridViewTextBoxColumn";
            this.filtrSkladIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // filtrUserIDDataGridViewTextBoxColumn
            // 
            this.filtrUserIDDataGridViewTextBoxColumn.DataPropertyName = "Filtr_UserID";
            this.filtrUserIDDataGridViewTextBoxColumn.HeaderText = "Filtr_UserID";
            this.filtrUserIDDataGridViewTextBoxColumn.Name = "filtrUserIDDataGridViewTextBoxColumn";
            this.filtrUserIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vlozStrediskoDataGridViewTextBoxColumn
            // 
            this.vlozStrediskoDataGridViewTextBoxColumn.DataPropertyName = "Vloz_Stredisko";
            this.vlozStrediskoDataGridViewTextBoxColumn.HeaderText = "Vloz_Stredisko";
            this.vlozStrediskoDataGridViewTextBoxColumn.Name = "vlozStrediskoDataGridViewTextBoxColumn";
            this.vlozStrediskoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vlozCinnostDataGridViewTextBoxColumn
            // 
            this.vlozCinnostDataGridViewTextBoxColumn.DataPropertyName = "Vloz_Cinnost";
            this.vlozCinnostDataGridViewTextBoxColumn.HeaderText = "Vloz_Cinnost";
            this.vlozCinnostDataGridViewTextBoxColumn.Name = "vlozCinnostDataGridViewTextBoxColumn";
            this.vlozCinnostDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vlozZakazkaDataGridViewTextBoxColumn
            // 
            this.vlozZakazkaDataGridViewTextBoxColumn.DataPropertyName = "Vloz_Zakazka";
            this.vlozZakazkaDataGridViewTextBoxColumn.HeaderText = "Vloz_Zakazka";
            this.vlozZakazkaDataGridViewTextBoxColumn.Name = "vlozZakazkaDataGridViewTextBoxColumn";
            this.vlozZakazkaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Kontrola_Disponability
            // 
            this.Kontrola_Disponability.DataPropertyName = "Kontrola_Disponability";
            this.Kontrola_Disponability.HeaderText = "Kontrola_Disponability";
            this.Kontrola_Disponability.Name = "Kontrola_Disponability";
            this.Kontrola_Disponability.ReadOnly = true;
            // 
            // Vyber_Typ_Prevodka
            // 
            this.Vyber_Typ_Prevodka.DataPropertyName = "Vyber_Typ_Prevodka";
            this.Vyber_Typ_Prevodka.HeaderText = "Vyber_Typ_Prevodka";
            this.Vyber_Typ_Prevodka.Name = "Vyber_Typ_Prevodka";
            this.Vyber_Typ_Prevodka.ReadOnly = true;
            // 
            // Prodej_Prijemka_Tisk_Tiskarna
            // 
            this.Prodej_Prijemka_Tisk_Tiskarna.DataPropertyName = "Prodej_Prijemka_Tisk_Tiskarna";
            this.Prodej_Prijemka_Tisk_Tiskarna.HeaderText = "Prodej_Prijemka_Tisk_Tiskarna";
            this.Prodej_Prijemka_Tisk_Tiskarna.Name = "Prodej_Prijemka_Tisk_Tiskarna";
            this.Prodej_Prijemka_Tisk_Tiskarna.ReadOnly = true;
            // 
            // Prodej_Prijemka_Tisk_ID_sablona
            // 
            this.Prodej_Prijemka_Tisk_ID_sablona.DataPropertyName = "Prodej_Prijemka_Tisk_ID_sablona";
            this.Prodej_Prijemka_Tisk_ID_sablona.HeaderText = "Prodej_Prijemka_Tisk_ID_sablona";
            this.Prodej_Prijemka_Tisk_ID_sablona.Name = "Prodej_Prijemka_Tisk_ID_sablona";
            this.Prodej_Prijemka_Tisk_ID_sablona.ReadOnly = true;
            // 
            // Prodej_Vydejka_Tisk_Tiskarna
            // 
            this.Prodej_Vydejka_Tisk_Tiskarna.DataPropertyName = "Prodej_Vydejka_Tisk_Tiskarna";
            this.Prodej_Vydejka_Tisk_Tiskarna.HeaderText = "Prodej_Vydejka_Tisk_Tiskarna";
            this.Prodej_Vydejka_Tisk_Tiskarna.Name = "Prodej_Vydejka_Tisk_Tiskarna";
            this.Prodej_Vydejka_Tisk_Tiskarna.ReadOnly = true;
            // 
            // Prodej_Vydejka_Tisk_ID_sablona
            // 
            this.Prodej_Vydejka_Tisk_ID_sablona.DataPropertyName = "Prodej_Vydejka_Tisk_ID_sablona";
            this.Prodej_Vydejka_Tisk_ID_sablona.HeaderText = "Prodej_Vydejka_Tisk_ID_sablona";
            this.Prodej_Vydejka_Tisk_ID_sablona.Name = "Prodej_Vydejka_Tisk_ID_sablona";
            this.Prodej_Vydejka_Tisk_ID_sablona.ReadOnly = true;
            // 
            // Prodej_Prevodka_Tisk_Tiskarna
            // 
            this.Prodej_Prevodka_Tisk_Tiskarna.DataPropertyName = "Prodej_Prevodka_Tisk_Tiskarna";
            this.Prodej_Prevodka_Tisk_Tiskarna.HeaderText = "Prodej_Prevodka_Tisk_Tiskarna";
            this.Prodej_Prevodka_Tisk_Tiskarna.Name = "Prodej_Prevodka_Tisk_Tiskarna";
            this.Prodej_Prevodka_Tisk_Tiskarna.ReadOnly = true;
            // 
            // Prodej_Prevodka_Tisk_ID_sablona
            // 
            this.Prodej_Prevodka_Tisk_ID_sablona.DataPropertyName = "Prodej_Prevodka_Tisk_ID_sablona";
            this.Prodej_Prevodka_Tisk_ID_sablona.HeaderText = "Prodej_Prevodka_Tisk_ID_sablona";
            this.Prodej_Prevodka_Tisk_ID_sablona.Name = "Prodej_Prevodka_Tisk_ID_sablona";
            this.Prodej_Prevodka_Tisk_ID_sablona.ReadOnly = true;
            // 
            // FormRADYList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 468);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dgRady);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "FormRADYList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výběr materiálu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRADYList_FormClosing);
            this.Load += new System.EventHandler(this.FormRADYList_Load);
            this.Shown += new System.EventHandler(this.FormRADYList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormRADYList_KeyDown);
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRady)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRady)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRady)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgRady;
        private System.Windows.Forms.Panel panelButtonsZobrazeniVyber;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.BindingSource bsRady;
        private System.Windows.Forms.Button buttonVybratUzivatele;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuVyber;
        private System.Windows.Forms.ToolStripMenuItem tsmiVyber;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecVyber;
        private Fask.Interfaces.DataSets.Rady dsRady;
        private System.Windows.Forms.Label label3;
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
        private System.Windows.Forms.TextBox tb_ID;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn defaultDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn platnostOdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn platnostDoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modulDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modulIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modulID2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modulFunkceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn radaIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn radaNazevDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn radaPrefixDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn radaCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filtrSkladIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filtrUserIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vlozStrediskoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vlozCinnostDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vlozZakazkaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Kontrola_Disponability;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vyber_Typ_Prevodka;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prodej_Prijemka_Tisk_Tiskarna;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prodej_Prijemka_Tisk_ID_sablona;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prodej_Vydejka_Tisk_Tiskarna;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prodej_Vydejka_Tisk_ID_sablona;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prodej_Prevodka_Tisk_Tiskarna;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prodej_Prevodka_Tisk_ID_sablona;
    }
}