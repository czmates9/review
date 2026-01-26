namespace Konzola.ImportniMustky
{
    partial class FormImport_SKzNC_List
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImport_SKzNC_List));
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.buttonOznacitVse = new System.Windows.Forms.Button();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExporty = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExceOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_importZasobyDodavetele = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiimportDoFASKZEXCEL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPolozka = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNovy = new System.Windows.Forms.ToolStripMenuItem();
            this.progressIndicatorVyrobek = new ProgressControls.ProgressIndicator();
            this.dgImportPOHODA = new Zuby.ADGV.AdvancedDataGridView();
            this.DEX_ROW_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defDodDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.refAgDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDSSKzDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDsSkladDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refADDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firmaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nakupCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refCMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmKursDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eANDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mJEANDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mJkoefEANDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poznDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status_ErrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsImportPOHODA = new System.Windows.Forms.BindingSource(this.components);
            this.dsImportPOHODA = new Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.bw_Vyhledat = new System.ComponentModel.BackgroundWorker();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tssl_ImportPOHODA_Count = new System.Windows.Forms.ToolStripLabel();
            this.bw_import = new System.ComponentModel.BackgroundWorker();
            this.bw_ImportEXCEL = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgImportPOHODA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsImportPOHODA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsImportPOHODA)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Controls.Add(this.buttonOdznacitVse);
            this.panel1.Controls.Add(this.buttonOznacitVse);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 261);
            this.panel1.TabIndex = 7;
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
            this.tsFiltry.Size = new System.Drawing.Size(1045, 27);
            this.tsFiltry.TabIndex = 59;
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
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdznacitVse.Location = new System.Drawing.Point(956, 172);
            this.buttonOdznacitVse.Name = "buttonOdznacitVse";
            this.buttonOdznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOdznacitVse.TabIndex = 13;
            this.buttonOdznacitVse.Text = "Odznačit vše";
            this.buttonOdznacitVse.UseVisualStyleBackColor = true;
            this.buttonOdznacitVse.Click += new System.EventHandler(this.buttonOdznacitVse_Click);
            // 
            // buttonOznacitVse
            // 
            this.buttonOznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOznacitVse.Location = new System.Drawing.Point(867, 172);
            this.buttonOznacitVse.Name = "buttonOznacitVse";
            this.buttonOznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOznacitVse.TabIndex = 12;
            this.buttonOznacitVse.Text = "Označit vše";
            this.buttonOznacitVse.UseVisualStyleBackColor = true;
            this.buttonOznacitVse.Click += new System.EventHandler(this.buttonOznacitVse_Click);
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(956, 92);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(83, 74);
            this.buttonVyhledat.TabIndex = 11;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiExporty,
            this.tsmiImport,
            this.tsmiPolozka});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1045, 24);
            this.menuStrip1.TabIndex = 7;
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
            this.tsmiKonec.Click += new System.EventHandler(this.tsmikonec_Click);
            // 
            // tsmiExporty
            // 
            this.tsmiExporty.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator1,
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
            // tsmiImport
            // 
            this.tsmiImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_importZasobyDodavetele,
            this.tsmiimportDoFASKZEXCEL});
            this.tsmiImport.Name = "tsmiImport";
            this.tsmiImport.Size = new System.Drawing.Size(55, 20);
            this.tsmiImport.Text = "Import";
            // 
            // tsmi_importZasobyDodavetele
            // 
            this.tsmi_importZasobyDodavetele.Name = "tsmi_importZasobyDodavetele";
            this.tsmi_importZasobyDodavetele.Size = new System.Drawing.Size(279, 22);
            this.tsmi_importZasobyDodavetele.Text = "Import do POHODY zásoby dodavetele";
            this.tsmi_importZasobyDodavetele.Click += new System.EventHandler(this.tsmi_importZasobyDodavetele_Click);
            // 
            // tsmiimportDoFASKZEXCEL
            // 
            this.tsmiimportDoFASKZEXCEL.Name = "tsmiimportDoFASKZEXCEL";
            this.tsmiimportDoFASKZEXCEL.Size = new System.Drawing.Size(279, 22);
            this.tsmiimportDoFASKZEXCEL.Text = "Import do FASK z EXCEL";
            this.tsmiimportDoFASKZEXCEL.Click += new System.EventHandler(this.tsmiimportDoFASKZEXCEL_Click);
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
            this.tsmiOdstranit.Size = new System.Drawing.Size(180, 22);
            this.tsmiOdstranit.Text = "Odstranit";
            this.tsmiOdstranit.Click += new System.EventHandler(this.tsmiOdstranit_Click);
            // 
            // tsmiUpravit
            // 
            this.tsmiUpravit.Name = "tsmiUpravit";
            this.tsmiUpravit.Size = new System.Drawing.Size(180, 22);
            this.tsmiUpravit.Text = "Upravit";
            this.tsmiUpravit.Click += new System.EventHandler(this.tsmiUpravit_Click);
            // 
            // tsmiNovy
            // 
            this.tsmiNovy.Name = "tsmiNovy";
            this.tsmiNovy.Size = new System.Drawing.Size(180, 22);
            this.tsmiNovy.Text = "Nový";
            this.tsmiNovy.Click += new System.EventHandler(this.tsmiNovy_Click);
            // 
            // progressIndicatorVyrobek
            // 
            this.progressIndicatorVyrobek.Location = new System.Drawing.Point(395, 346);
            this.progressIndicatorVyrobek.Name = "progressIndicatorVyrobek";
            this.progressIndicatorVyrobek.Percentage = 0F;
            this.progressIndicatorVyrobek.Size = new System.Drawing.Size(98, 98);
            this.progressIndicatorVyrobek.TabIndex = 105;
            this.progressIndicatorVyrobek.Text = "progressIndicator1";
            this.progressIndicatorVyrobek.Visible = false;
            // 
            // dgImportPOHODA
            // 
            this.dgImportPOHODA.AllowUserToAddRows = false;
            this.dgImportPOHODA.AllowUserToDeleteRows = false;
            this.dgImportPOHODA.AllowUserToOrderColumns = true;
            this.dgImportPOHODA.AllowUserToResizeRows = false;
            this.dgImportPOHODA.AutoGenerateColumns = false;
            this.dgImportPOHODA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgImportPOHODA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DEX_ROW_ID,
            this.defDodDataGridViewCheckBoxColumn,
            this.refAgDataGridViewTextBoxColumn,
            this.iDSSKzDataGridViewTextBoxColumn,
            this.iDsSkladDataGridViewTextBoxColumn,
            this.refADDataGridViewTextBoxColumn,
            this.firmaDataGridViewTextBoxColumn,
            this.nakupCDataGridViewTextBoxColumn,
            this.refCMDataGridViewTextBoxColumn,
            this.cmKursDataGridViewTextBoxColumn,
            this.eANDataGridViewTextBoxColumn,
            this.mJEANDataGridViewTextBoxColumn,
            this.mJkoefEANDataGridViewTextBoxColumn,
            this.poznDataGridViewTextBoxColumn,
            this.Status_ErrDataGridViewTextBoxColumn});
            this.dgImportPOHODA.DataSource = this.bsImportPOHODA;
            this.dgImportPOHODA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgImportPOHODA.EnableHeadersVisualStyles = false;
            this.dgImportPOHODA.FilterAndSortEnabled = true;
            this.dgImportPOHODA.Location = new System.Drawing.Point(0, 288);
            this.dgImportPOHODA.Name = "dgImportPOHODA";
            this.dgImportPOHODA.ReadOnly = true;
            this.dgImportPOHODA.RowHeadersVisible = false;
            this.dgImportPOHODA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgImportPOHODA.Size = new System.Drawing.Size(1045, 319);
            this.dgImportPOHODA.TabIndex = 30;
            this.dgImportPOHODA.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgImportPOHODA_CellFormatting);
            this.dgImportPOHODA.SelectionChanged += new System.EventHandler(this.dgVyrobek_SelectionChanged);
            this.dgImportPOHODA.DoubleClick += new System.EventHandler(this.dgImportPOHODA_DoubleClick);
            // 
            // DEX_ROW_ID
            // 
            this.DEX_ROW_ID.DataPropertyName = "DEX_ROW_ID";
            this.DEX_ROW_ID.HeaderText = "Index";
            this.DEX_ROW_ID.Name = "DEX_ROW_ID";
            this.DEX_ROW_ID.ReadOnly = true;
            // 
            // defDodDataGridViewCheckBoxColumn
            // 
            this.defDodDataGridViewCheckBoxColumn.DataPropertyName = "DefDod";
            this.defDodDataGridViewCheckBoxColumn.HeaderText = "Výchozí EAN kód";
            this.defDodDataGridViewCheckBoxColumn.Name = "defDodDataGridViewCheckBoxColumn";
            this.defDodDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // refAgDataGridViewTextBoxColumn
            // 
            this.refAgDataGridViewTextBoxColumn.DataPropertyName = "RefAg";
            this.refAgDataGridViewTextBoxColumn.HeaderText = "Číslo položky";
            this.refAgDataGridViewTextBoxColumn.Name = "refAgDataGridViewTextBoxColumn";
            this.refAgDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDSSKzDataGridViewTextBoxColumn
            // 
            this.iDSSKzDataGridViewTextBoxColumn.DataPropertyName = "IDS_SKz";
            this.iDSSKzDataGridViewTextBoxColumn.HeaderText = "Kód položky";
            this.iDSSKzDataGridViewTextBoxColumn.Name = "iDSSKzDataGridViewTextBoxColumn";
            this.iDSSKzDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDsSkladDataGridViewTextBoxColumn
            // 
            this.iDsSkladDataGridViewTextBoxColumn.DataPropertyName = "ID_sSklad";
            this.iDsSkladDataGridViewTextBoxColumn.HeaderText = "ID Skladu";
            this.iDsSkladDataGridViewTextBoxColumn.Name = "iDsSkladDataGridViewTextBoxColumn";
            this.iDsSkladDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // refADDataGridViewTextBoxColumn
            // 
            this.refADDataGridViewTextBoxColumn.DataPropertyName = "RefAD";
            this.refADDataGridViewTextBoxColumn.HeaderText = "Odkaz do Adresáře";
            this.refADDataGridViewTextBoxColumn.Name = "refADDataGridViewTextBoxColumn";
            this.refADDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firmaDataGridViewTextBoxColumn
            // 
            this.firmaDataGridViewTextBoxColumn.DataPropertyName = "Firma";
            this.firmaDataGridViewTextBoxColumn.HeaderText = "Firma";
            this.firmaDataGridViewTextBoxColumn.Name = "firmaDataGridViewTextBoxColumn";
            this.firmaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nakupCDataGridViewTextBoxColumn
            // 
            this.nakupCDataGridViewTextBoxColumn.DataPropertyName = "NakupC";
            this.nakupCDataGridViewTextBoxColumn.HeaderText = "Nákupní cena";
            this.nakupCDataGridViewTextBoxColumn.Name = "nakupCDataGridViewTextBoxColumn";
            this.nakupCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // refCMDataGridViewTextBoxColumn
            // 
            this.refCMDataGridViewTextBoxColumn.DataPropertyName = "RefCM";
            this.refCMDataGridViewTextBoxColumn.HeaderText = "Odkaz na cizí měnu";
            this.refCMDataGridViewTextBoxColumn.Name = "refCMDataGridViewTextBoxColumn";
            this.refCMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cmKursDataGridViewTextBoxColumn
            // 
            this.cmKursDataGridViewTextBoxColumn.DataPropertyName = "CmKurs";
            this.cmKursDataGridViewTextBoxColumn.HeaderText = "Kurz cizí měny";
            this.cmKursDataGridViewTextBoxColumn.Name = "cmKursDataGridViewTextBoxColumn";
            this.cmKursDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // eANDataGridViewTextBoxColumn
            // 
            this.eANDataGridViewTextBoxColumn.DataPropertyName = "EAN";
            this.eANDataGridViewTextBoxColumn.HeaderText = "Alternativní EAN kód";
            this.eANDataGridViewTextBoxColumn.Name = "eANDataGridViewTextBoxColumn";
            this.eANDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mJEANDataGridViewTextBoxColumn
            // 
            this.mJEANDataGridViewTextBoxColumn.DataPropertyName = "MJEAN";
            this.mJEANDataGridViewTextBoxColumn.HeaderText = "Měrna jednotka";
            this.mJEANDataGridViewTextBoxColumn.Name = "mJEANDataGridViewTextBoxColumn";
            this.mJEANDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mJkoefEANDataGridViewTextBoxColumn
            // 
            this.mJkoefEANDataGridViewTextBoxColumn.DataPropertyName = "MJkoefEAN";
            this.mJkoefEANDataGridViewTextBoxColumn.HeaderText = "Přepočtový koeficient";
            this.mJkoefEANDataGridViewTextBoxColumn.Name = "mJkoefEANDataGridViewTextBoxColumn";
            this.mJkoefEANDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // poznDataGridViewTextBoxColumn
            // 
            this.poznDataGridViewTextBoxColumn.DataPropertyName = "Pozn";
            this.poznDataGridViewTextBoxColumn.HeaderText = "Poznámka";
            this.poznDataGridViewTextBoxColumn.Name = "poznDataGridViewTextBoxColumn";
            this.poznDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Status_ErrDataGridViewTextBoxColumn
            // 
            this.Status_ErrDataGridViewTextBoxColumn.DataPropertyName = "Status_Err";
            this.Status_ErrDataGridViewTextBoxColumn.HeaderText = "Status Chyby";
            this.Status_ErrDataGridViewTextBoxColumn.Name = "Status_ErrDataGridViewTextBoxColumn";
            this.Status_ErrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsImportPOHODA
            // 
            this.bsImportPOHODA.DataMember = "FASK_ZASOBY_IMPORT_POHODA_SKzNC";
            this.bsImportPOHODA.DataSource = this.dsImportPOHODA;
            // 
            // dsImportPOHODA
            // 
            this.dsImportPOHODA.DataSetName = "VyrobaDataSet";
            this.dsImportPOHODA.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(1045, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(101, 632);
            this.panelButtons.TabIndex = 8;
            // 
            // bw_Vyhledat
            // 
            this.bw_Vyhledat.WorkerSupportsCancellation = true;
            this.bw_Vyhledat.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Vyhledat_DoWork);
            this.bw_Vyhledat.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Vyhledat_RunWorkerCompleted);
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 261);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(1045, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 106;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_ImportPOHODA_Count});
            this.toolStrip1.Location = new System.Drawing.Point(0, 607);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1045, 25);
            this.toolStrip1.TabIndex = 107;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tssl_ImportPOHODA_Count
            // 
            this.tssl_ImportPOHODA_Count.Name = "tssl_ImportPOHODA_Count";
            this.tssl_ImportPOHODA_Count.Size = new System.Drawing.Size(86, 22);
            this.tssl_ImportPOHODA_Count.Text = "toolStripLabel1";
            // 
            // bw_import
            // 
            this.bw_import.WorkerSupportsCancellation = true;
            this.bw_import.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_import_DoWork);
            this.bw_import.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_import_RunWorkerCompleted);
            // 
            // bw_ImportEXCEL
            // 
            this.bw_ImportEXCEL.WorkerSupportsCancellation = true;
            this.bw_ImportEXCEL.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_ImportEXCEL_DoWork);
            this.bw_ImportEXCEL.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_ImportEXCEL_RunWorkerCompleted);
            // 
            // FormImport_SKzNC_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 632);
            this.Controls.Add(this.progressIndicatorVyrobek);
            this.Controls.Add(this.dgImportPOHODA);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormImport_SKzNC_List";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import POHODA Dodavatelé zásob";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormImport_SKzNC_List_FormClosing);
            this.Load += new System.EventHandler(this.FormImport_SKzNC_List_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormImport_SKzNC_List_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgImportPOHODA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsImportPOHODA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsImportPOHODA)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel dsImportPOHODA;
        private System.Windows.Forms.Panel panel1;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.BindingSource bsImportPOHODA;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Button buttonOznacitVse;
        private Zuby.ADGV.AdvancedDataGridView dgImportPOHODA;
        private ProgressControls.ProgressIndicator progressIndicatorVyrobek;
        private System.ComponentModel.BackgroundWorker bw_Vyhledat;
        private System.Windows.Forms.ToolStripMenuItem tsmiExporty;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExceOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private System.Windows.Forms.ToolStripMenuItem tsmiImport;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tssl_ImportPOHODA_Count;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.ComponentModel.BackgroundWorker bw_import;
        private System.Windows.Forms.ToolStripMenuItem tsmi_importZasobyDodavetele;
        private System.Windows.Forms.ToolStripMenuItem tsmiimportDoFASKZEXCEL;
        private System.ComponentModel.BackgroundWorker bw_ImportEXCEL;
        private System.Windows.Forms.ToolStripMenuItem tsmiPolozka;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravit;
        private System.Windows.Forms.ToolStripMenuItem tsmiNovy;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEX_ROW_ID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn defDodDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn refAgDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDSSKzDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDsSkladDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn refADDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firmaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nakupCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn refCMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmKursDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eANDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mJEANDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mJkoefEANDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn poznDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status_ErrDataGridViewTextBoxColumn;
    }
}