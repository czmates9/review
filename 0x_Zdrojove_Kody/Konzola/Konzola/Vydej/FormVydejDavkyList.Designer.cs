namespace Konzola.Vydej
{
    partial class FormVydejDavkyList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVydejDavkyList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExcelOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZmenitPriorituDavky = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZmenitPriorituDavkyNaUzivatele = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbFiltrNestazeneDavky = new System.Windows.Forms.CheckBox();
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
            this.label2 = new System.Windows.Forms.Label();
            this.cbPriority = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSopnumbe = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCountEntries = new System.Windows.Forms.ComboBox();
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.buttonOznacitVse = new System.Windows.Forms.Button();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.dgVydej = new Zuby.ADGV.AdvancedDataGridView();
            this.bsVydej = new System.Windows.Forms.BindingSource(this.components);
            this.dsVydej = new Fask.Interfaces.DataSets.Vydej();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.bwLoadVydejHlavicky = new System.ComponentModel.BackgroundWorker();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.countEntriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPNUMBEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cntItemsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sumItemsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDDOCNMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.info1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pRIORITYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rOZPRACOVANODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_Doslo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USERID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOGIN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIRSTNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SECONDNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVydej)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVydej)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVydej)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
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
            this.tsmiKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.tsmiZmenitPriorituDavky,
            this.tsmiZmenitPriorituDavkyNaUzivatele});
            this.tsmiAkce.Name = "tsmiAkce";
            this.tsmiAkce.Size = new System.Drawing.Size(45, 20);
            this.tsmiAkce.Text = "Akce";
            // 
            // tsmiZmenitPriorituDavky
            // 
            this.tsmiZmenitPriorituDavky.Name = "tsmiZmenitPriorituDavky";
            this.tsmiZmenitPriorituDavky.Size = new System.Drawing.Size(253, 22);
            this.tsmiZmenitPriorituDavky.Text = "Změnit prioritu dávky";
            this.tsmiZmenitPriorituDavky.Click += new System.EventHandler(this.tsmiZmenitPriorituDavky_Click);
            // 
            // tsmiZmenitPriorituDavkyNaUzivatele
            // 
            this.tsmiZmenitPriorituDavkyNaUzivatele.Name = "tsmiZmenitPriorituDavkyNaUzivatele";
            this.tsmiZmenitPriorituDavkyNaUzivatele.Size = new System.Drawing.Size(253, 22);
            this.tsmiZmenitPriorituDavkyNaUzivatele.Text = "Změnit prioritu dávky na uživatele";
            this.tsmiZmenitPriorituDavkyNaUzivatele.Click += new System.EventHandler(this.tsmiZmenitPriorituDavkyNaUzivatele_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbFiltrNestazeneDavky);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbPriority);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbSopnumbe);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbCountEntries);
            this.panel1.Controls.Add(this.buttonOdznacitVse);
            this.panel1.Controls.Add(this.buttonOznacitVse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(850, 126);
            this.panel1.TabIndex = 1;
            // 
            // cbFiltrNestazeneDavky
            // 
            this.cbFiltrNestazeneDavky.AutoSize = true;
            this.cbFiltrNestazeneDavky.Location = new System.Drawing.Point(362, 59);
            this.cbFiltrNestazeneDavky.Name = "cbFiltrNestazeneDavky";
            this.cbFiltrNestazeneDavky.Size = new System.Drawing.Size(180, 17);
            this.cbFiltrNestazeneDavky.TabIndex = 3;
            this.cbFiltrNestazeneDavky.Text = "Zobrazit pouze nestažené dávky";
            this.cbFiltrNestazeneDavky.UseVisualStyleBackColor = true;
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
            this.buttonVyhledat.Location = new System.Drawing.Point(771, 28);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 14;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(331, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Priorita:";
            // 
            // cbPriority
            // 
            this.cbPriority.FormattingEnabled = true;
            this.cbPriority.Location = new System.Drawing.Point(374, 28);
            this.cbPriority.Name = "cbPriority";
            this.cbPriority.Size = new System.Drawing.Size(225, 21);
            this.cbPriority.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Objednávka č.:";
            // 
            // cbSopnumbe
            // 
            this.cbSopnumbe.FormattingEnabled = true;
            this.cbSopnumbe.Location = new System.Drawing.Point(92, 55);
            this.cbSopnumbe.Name = "cbSopnumbe";
            this.cbSopnumbe.Size = new System.Drawing.Size(225, 21);
            this.cbSopnumbe.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Číslo dávky:";
            // 
            // cbCountEntries
            // 
            this.cbCountEntries.FormattingEnabled = true;
            this.cbCountEntries.Location = new System.Drawing.Point(92, 28);
            this.cbCountEntries.Name = "cbCountEntries";
            this.cbCountEntries.Size = new System.Drawing.Size(225, 21);
            this.cbCountEntries.TabIndex = 0;
            // 
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdznacitVse.Location = new System.Drawing.Point(761, 96);
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
            this.buttonOznacitVse.Location = new System.Drawing.Point(672, 96);
            this.buttonOznacitVse.Name = "buttonOznacitVse";
            this.buttonOznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOznacitVse.TabIndex = 15;
            this.buttonOznacitVse.Text = "Označit vše";
            this.buttonOznacitVse.UseVisualStyleBackColor = true;
            this.buttonOznacitVse.Click += new System.EventHandler(this.buttonOznacitVse_Click);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(334, 325);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 30;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // dgVydej
            // 
            this.dgVydej.AllowUserToAddRows = false;
            this.dgVydej.AllowUserToDeleteRows = false;
            this.dgVydej.AllowUserToOrderColumns = true;
            this.dgVydej.AllowUserToResizeRows = false;
            this.dgVydej.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgVydej.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgVydej.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVydej.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.countEntriesDataGridViewTextBoxColumn,
            this.sOPNUMBEDataGridViewTextBoxColumn,
            this.cntItemsDataGridViewTextBoxColumn,
            this.sumItemsDataGridViewTextBoxColumn,
            this.vNDDOCNMDataGridViewTextBoxColumn,
            this.info1DataGridViewTextBoxColumn,
            this.pRIORITYDataGridViewTextBoxColumn,
            this.rOZPRACOVANODataGridViewTextBoxColumn,
            this.CZ_Doslo,
            this.USERID,
            this.LOGIN,
            this.FIRSTNAME,
            this.SECONDNAME});
            this.dgVydej.DataSource = this.bsVydej;
            this.dgVydej.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVydej.EnableHeadersVisualStyles = false;
            this.dgVydej.FilterAndSortEnabled = true;
            this.dgVydej.Location = new System.Drawing.Point(0, 177);
            this.dgVydej.Name = "dgVydej";
            this.dgVydej.ReadOnly = true;
            this.dgVydej.RowHeadersVisible = false;
            this.dgVydej.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVydej.Size = new System.Drawing.Size(850, 385);
            this.dgVydej.TabIndex = 0;
            this.dgVydej.TabStop = false;
            this.dgVydej.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgVydej.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dgVydej.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // bsVydej
            // 
            this.bsVydej.DataMember = "Hlavicky";
            this.bsVydej.DataSource = this.dsVydej;
            // 
            // dsVydej
            // 
            this.dsVydej.DataSetName = "Vydej";
            this.dsVydej.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // bwLoadVydejHlavicky
            // 
            this.bwLoadVydejHlavicky.WorkerSupportsCancellation = true;
            this.bwLoadVydejHlavicky.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSkladPohyb_DoWork);
            this.bwLoadVydejHlavicky.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSkladPohyb_RunWorkerCompleted);
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 150);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(850, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 31;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // countEntriesDataGridViewTextBoxColumn
            // 
            this.countEntriesDataGridViewTextBoxColumn.DataPropertyName = "CountEntries";
            dataGridViewCellStyle2.NullValue = "-";
            this.countEntriesDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.countEntriesDataGridViewTextBoxColumn.HeaderText = "Číslo dávky";
            this.countEntriesDataGridViewTextBoxColumn.Name = "countEntriesDataGridViewTextBoxColumn";
            this.countEntriesDataGridViewTextBoxColumn.ReadOnly = true;
            this.countEntriesDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // sOPNUMBEDataGridViewTextBoxColumn
            // 
            this.sOPNUMBEDataGridViewTextBoxColumn.DataPropertyName = "SOPNUMBE";
            dataGridViewCellStyle3.NullValue = "-";
            this.sOPNUMBEDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.sOPNUMBEDataGridViewTextBoxColumn.HeaderText = "Objednávka č.";
            this.sOPNUMBEDataGridViewTextBoxColumn.Name = "sOPNUMBEDataGridViewTextBoxColumn";
            this.sOPNUMBEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sOPNUMBEDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // cntItemsDataGridViewTextBoxColumn
            // 
            this.cntItemsDataGridViewTextBoxColumn.DataPropertyName = "CntItems";
            dataGridViewCellStyle4.NullValue = "-";
            this.cntItemsDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.cntItemsDataGridViewTextBoxColumn.HeaderText = "Počet položek";
            this.cntItemsDataGridViewTextBoxColumn.Name = "cntItemsDataGridViewTextBoxColumn";
            this.cntItemsDataGridViewTextBoxColumn.ReadOnly = true;
            this.cntItemsDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // sumItemsDataGridViewTextBoxColumn
            // 
            this.sumItemsDataGridViewTextBoxColumn.DataPropertyName = "SumItems";
            dataGridViewCellStyle5.NullValue = "-";
            this.sumItemsDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.sumItemsDataGridViewTextBoxColumn.HeaderText = "Množství požadováno";
            this.sumItemsDataGridViewTextBoxColumn.Name = "sumItemsDataGridViewTextBoxColumn";
            this.sumItemsDataGridViewTextBoxColumn.ReadOnly = true;
            this.sumItemsDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // vNDDOCNMDataGridViewTextBoxColumn
            // 
            this.vNDDOCNMDataGridViewTextBoxColumn.DataPropertyName = "VNDDOCNM";
            dataGridViewCellStyle6.NullValue = "-";
            this.vNDDOCNMDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.vNDDOCNMDataGridViewTextBoxColumn.HeaderText = "Dokument č.";
            this.vNDDOCNMDataGridViewTextBoxColumn.Name = "vNDDOCNMDataGridViewTextBoxColumn";
            this.vNDDOCNMDataGridViewTextBoxColumn.ReadOnly = true;
            this.vNDDOCNMDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // info1DataGridViewTextBoxColumn
            // 
            this.info1DataGridViewTextBoxColumn.DataPropertyName = "Info1";
            dataGridViewCellStyle7.NullValue = "-";
            this.info1DataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.info1DataGridViewTextBoxColumn.HeaderText = "Info";
            this.info1DataGridViewTextBoxColumn.Name = "info1DataGridViewTextBoxColumn";
            this.info1DataGridViewTextBoxColumn.ReadOnly = true;
            this.info1DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // pRIORITYDataGridViewTextBoxColumn
            // 
            this.pRIORITYDataGridViewTextBoxColumn.DataPropertyName = "PRIORITY";
            dataGridViewCellStyle8.NullValue = "-";
            this.pRIORITYDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.pRIORITYDataGridViewTextBoxColumn.HeaderText = "Priorita";
            this.pRIORITYDataGridViewTextBoxColumn.Name = "pRIORITYDataGridViewTextBoxColumn";
            this.pRIORITYDataGridViewTextBoxColumn.ReadOnly = true;
            this.pRIORITYDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // rOZPRACOVANODataGridViewTextBoxColumn
            // 
            this.rOZPRACOVANODataGridViewTextBoxColumn.DataPropertyName = "ROZPRACOVANO";
            dataGridViewCellStyle9.NullValue = "-";
            this.rOZPRACOVANODataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.rOZPRACOVANODataGridViewTextBoxColumn.HeaderText = "Rozpracováno";
            this.rOZPRACOVANODataGridViewTextBoxColumn.Name = "rOZPRACOVANODataGridViewTextBoxColumn";
            this.rOZPRACOVANODataGridViewTextBoxColumn.ReadOnly = true;
            this.rOZPRACOVANODataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CZ_Doslo
            // 
            this.CZ_Doslo.DataPropertyName = "CZ_Doslo";
            dataGridViewCellStyle10.NullValue = "-";
            this.CZ_Doslo.DefaultCellStyle = dataGridViewCellStyle10;
            this.CZ_Doslo.HeaderText = "Staženo";
            this.CZ_Doslo.Name = "CZ_Doslo";
            this.CZ_Doslo.ReadOnly = true;
            this.CZ_Doslo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // USERID
            // 
            this.USERID.DataPropertyName = "USERID";
            dataGridViewCellStyle11.NullValue = "-";
            this.USERID.DefaultCellStyle = dataGridViewCellStyle11;
            this.USERID.HeaderText = "ID uživatele";
            this.USERID.Name = "USERID";
            this.USERID.ReadOnly = true;
            this.USERID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // LOGIN
            // 
            this.LOGIN.DataPropertyName = "LOGIN";
            dataGridViewCellStyle12.NullValue = "-";
            this.LOGIN.DefaultCellStyle = dataGridViewCellStyle12;
            this.LOGIN.HeaderText = "Login uživatele";
            this.LOGIN.Name = "LOGIN";
            this.LOGIN.ReadOnly = true;
            this.LOGIN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // FIRSTNAME
            // 
            this.FIRSTNAME.DataPropertyName = "FIRSTNAME";
            dataGridViewCellStyle13.NullValue = "-";
            this.FIRSTNAME.DefaultCellStyle = dataGridViewCellStyle13;
            this.FIRSTNAME.HeaderText = "Jméno";
            this.FIRSTNAME.Name = "FIRSTNAME";
            this.FIRSTNAME.ReadOnly = true;
            this.FIRSTNAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SECONDNAME
            // 
            this.SECONDNAME.DataPropertyName = "SECONDNAME";
            dataGridViewCellStyle14.NullValue = "-";
            this.SECONDNAME.DefaultCellStyle = dataGridViewCellStyle14;
            this.SECONDNAME.HeaderText = "Příjmení";
            this.SECONDNAME.Name = "SECONDNAME";
            this.SECONDNAME.ReadOnly = true;
            this.SECONDNAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // FormVydejDavkyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 562);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dgVydej);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormVydejDavkyList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Řízení priorit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVydejDavkyList_FormClosing);
            this.Load += new System.EventHandler(this.FormVydejDavkyList_Load);
            this.Shown += new System.EventHandler(this.FormVydejDavkyList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormVydejDavkyList_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVydej)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVydej)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVydej)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSopnumbe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCountEntries;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Button buttonOznacitVse;
        private Zuby.ADGV.AdvancedDataGridView dgVydej;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbPriority;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private System.ComponentModel.BackgroundWorker bwLoadVydejHlavicky;
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
        private System.Windows.Forms.CheckBox cbFiltrNestazeneDavky;
        private Fask.Interfaces.DataSets.Vydej dsVydej;
        private System.Windows.Forms.BindingSource bsVydej;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbVycistit;
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
        private System.Windows.Forms.ToolStripMenuItem tsmiZmenitPriorituDavky;
        private System.Windows.Forms.ToolStripMenuItem tsmiZmenitPriorituDavkyNaUzivatele;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPNUMBEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cntItemsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sumItemsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDDOCNMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn info1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pRIORITYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rOZPRACOVANODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_Doslo;
        private System.Windows.Forms.DataGridViewTextBoxColumn USERID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOGIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIRSTNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn SECONDNAME;


    }
}