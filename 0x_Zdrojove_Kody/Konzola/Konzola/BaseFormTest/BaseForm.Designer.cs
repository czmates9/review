namespace Konzola
{
    partial class BaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.dataGridView1 = new Zuby.ADGV.AdvancedDataGridView();
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.vybratToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.konecToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDoCSVVseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDoCSVOznaceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.exportDoExcelVseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDoExceOznaceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.exportDoXMLVseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDoXMLOznaceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.položkaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nováToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upravitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odstranitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.FilterAndSortEnabled = true;
            this.dataGridView1.Location = new System.Drawing.Point(0, 177);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(574, 291);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
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
            this.panelMain.Controls.Add(this.progressIndicator1);
            this.panelMain.Controls.Add(this.dataGridView1);
            this.panelMain.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Controls.Add(this.menuStrip2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(574, 468);
            this.panelMain.TabIndex = 1;
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(223, 254);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 38;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
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
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(574, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 42;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(574, 126);
            this.panel1.TabIndex = 43;
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(498, 2);
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
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuVyber,
            this.tsmiMenuList,
            this.exportyToolStripMenuItem,
            this.položkaToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(574, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // tsmiMenuVyber
            // 
            this.tsmiMenuVyber.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vybratToolStripMenuItem,
            this.toolStripSeparator1,
            this.konecToolStripMenuItem});
            this.tsmiMenuVyber.Name = "tsmiMenuVyber";
            this.tsmiMenuVyber.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenuVyber.Text = "Menu";
            // 
            // vybratToolStripMenuItem
            // 
            this.vybratToolStripMenuItem.Name = "vybratToolStripMenuItem";
            this.vybratToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.vybratToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.vybratToolStripMenuItem.Text = "Vybrat";
            this.vybratToolStripMenuItem.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.konecToolStripMenuItem.Text = "Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // tsmiMenuList
            // 
            this.tsmiMenuList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konecToolStripMenuItem1});
            this.tsmiMenuList.Name = "tsmiMenuList";
            this.tsmiMenuList.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tsmiMenuList.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenuList.Text = "Menu";
            // 
            // konecToolStripMenuItem1
            // 
            this.konecToolStripMenuItem1.Name = "konecToolStripMenuItem1";
            this.konecToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.konecToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.konecToolStripMenuItem1.Text = "Konec";
            this.konecToolStripMenuItem1.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // exportyToolStripMenuItem
            // 
            this.exportyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportDoCSVVseToolStripMenuItem,
            this.exportDoCSVOznaceneToolStripMenuItem,
            this.toolStripSeparator6,
            this.exportDoExcelVseToolStripMenuItem,
            this.exportDoExceOznaceneToolStripMenuItem,
            this.toolStripSeparator7,
            this.exportDoXMLVseToolStripMenuItem,
            this.exportDoXMLOznaceneToolStripMenuItem});
            this.exportyToolStripMenuItem.Name = "exportyToolStripMenuItem";
            this.exportyToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.exportyToolStripMenuItem.Text = "Výstup";
            // 
            // exportDoCSVVseToolStripMenuItem
            // 
            this.exportDoCSVVseToolStripMenuItem.Name = "exportDoCSVVseToolStripMenuItem";
            this.exportDoCSVVseToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportDoCSVVseToolStripMenuItem.Text = "Export do CSV vše";
            this.exportDoCSVVseToolStripMenuItem.Click += new System.EventHandler(this.exportDoCSVVseToolStripMenuItem_Click);
            // 
            // exportDoCSVOznaceneToolStripMenuItem
            // 
            this.exportDoCSVOznaceneToolStripMenuItem.Name = "exportDoCSVOznaceneToolStripMenuItem";
            this.exportDoCSVOznaceneToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportDoCSVOznaceneToolStripMenuItem.Text = "Export do CSV označené";
            this.exportDoCSVOznaceneToolStripMenuItem.Click += new System.EventHandler(this.exportDoCSVOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(203, 6);
            // 
            // exportDoExcelVseToolStripMenuItem
            // 
            this.exportDoExcelVseToolStripMenuItem.Name = "exportDoExcelVseToolStripMenuItem";
            this.exportDoExcelVseToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportDoExcelVseToolStripMenuItem.Text = "Export do Excel vše";
            this.exportDoExcelVseToolStripMenuItem.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_Click);
            // 
            // exportDoExceOznaceneToolStripMenuItem
            // 
            this.exportDoExceOznaceneToolStripMenuItem.Name = "exportDoExceOznaceneToolStripMenuItem";
            this.exportDoExceOznaceneToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportDoExceOznaceneToolStripMenuItem.Text = "Export do Excel označené";
            this.exportDoExceOznaceneToolStripMenuItem.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(203, 6);
            // 
            // exportDoXMLVseToolStripMenuItem
            // 
            this.exportDoXMLVseToolStripMenuItem.Name = "exportDoXMLVseToolStripMenuItem";
            this.exportDoXMLVseToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportDoXMLVseToolStripMenuItem.Text = "Export do XML Vše";
            this.exportDoXMLVseToolStripMenuItem.Click += new System.EventHandler(this.exportDoXMLVseToolStripMenuItem_Click);
            // 
            // exportDoXMLOznaceneToolStripMenuItem
            // 
            this.exportDoXMLOznaceneToolStripMenuItem.Name = "exportDoXMLOznaceneToolStripMenuItem";
            this.exportDoXMLOznaceneToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportDoXMLOznaceneToolStripMenuItem.Text = "Export do XML označené";
            this.exportDoXMLOznaceneToolStripMenuItem.Click += new System.EventHandler(this.exportDoXMLOznaceneToolStripMenuItem_Click);
            // 
            // položkaToolStripMenuItem
            // 
            this.položkaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nováToolStripMenuItem,
            this.upravitToolStripMenuItem,
            this.odstranitToolStripMenuItem});
            this.položkaToolStripMenuItem.Name = "položkaToolStripMenuItem";
            this.položkaToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.položkaToolStripMenuItem.Text = "Položka";
            // 
            // nováToolStripMenuItem
            // 
            this.nováToolStripMenuItem.Name = "nováToolStripMenuItem";
            this.nováToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.nováToolStripMenuItem.Text = "Nová";
            this.nováToolStripMenuItem.Click += new System.EventHandler(this.buttonNovy_Click_1);
            // 
            // upravitToolStripMenuItem
            // 
            this.upravitToolStripMenuItem.Name = "upravitToolStripMenuItem";
            this.upravitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.upravitToolStripMenuItem.Text = "Upravit";
            this.upravitToolStripMenuItem.Click += new System.EventHandler(this.buttonUpravit_Click);
            // 
            // odstranitToolStripMenuItem
            // 
            this.odstranitToolStripMenuItem.Name = "odstranitToolStripMenuItem";
            this.odstranitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.odstranitToolStripMenuItem.Text = "Odstranit";
            this.odstranitToolStripMenuItem.Click += new System.EventHandler(this.buttonOdstranit_Click);
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
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 468);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BaseForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUzivateleList_FormClosing);
            this.Load += new System.EventHandler(this.FormUzivateleList_Load);
            this.Shown += new System.EventHandler(this.FormZboziSelect_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUzivateleList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelButtonsZobrazeniVyber;
        protected System.Windows.Forms.Panel panelMain;
        protected System.Windows.Forms.Button buttonVybratUzivatele;
        protected System.Windows.Forms.Button buttonKonec;
        protected System.Windows.Forms.MenuStrip menuStrip2;
        protected System.Windows.Forms.ToolStripMenuItem tsmiMenuVyber;
        protected System.Windows.Forms.ToolStripMenuItem vybratToolStripMenuItem;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
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
        protected System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem1;
        protected System.Windows.Forms.ToolStripMenuItem exportyToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem exportDoCSVVseToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem exportDoCSVOznaceneToolStripMenuItem;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        protected System.Windows.Forms.ToolStripMenuItem exportDoExcelVseToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem exportDoExceOznaceneToolStripMenuItem;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        protected System.Windows.Forms.ToolStripMenuItem exportDoXMLVseToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem exportDoXMLOznaceneToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem položkaToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem nováToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem upravitToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem odstranitToolStripMenuItem;
        protected Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        protected System.Windows.Forms.Panel panel1;
        public Zuby.ADGV.AdvancedDataGridView dataGridView1;
    }
}