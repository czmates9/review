namespace Konzola.Vyroba.Ciselnik
{
    partial class FormModifikace_TP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormModifikace_TP));
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.dg_Modifikace_TP = new Zuby.ADGV.AdvancedDataGridView();
            this.bs_Modifikace_TP = new System.Windows.Forms.BindingSource(this.components);
            this.ds_Modifikace_TP = new Fask.Interfaces.DataSets.Vyroba();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rb_NEShodne = new System.Windows.Forms.CheckBox();
            this.rb_Shodne = new System.Windows.Forms.CheckBox();
            this.cb_Sklad = new System.Windows.Forms.ComboBox();
            this.cb_Vyrobek = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.tsmiVybrat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiKonecVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonecList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExceOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiModifikovat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ZamenZdrojCil = new System.Windows.Forms.ToolStripMenuItem();
            this.bw_Modifikace_TP = new System.ComponentModel.BackgroundWorker();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.bw_ZamenZdrojCil = new System.ComponentModel.BackgroundWorker();
            this.ITEMNMBR_Def = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESC_Def = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_ID_Def = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_DESC_Def = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR_fol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESC_Fol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMCODE_Fol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VNDITNUM_Fol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VNDITNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MJ_Fol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_ID_Fol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_DESC_Fol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PocetVyskytu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Modifikace_TP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Modifikace_TP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Modifikace_TP)).BeginInit();
            this.panel1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
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
            this.panelMain.Controls.Add(this.progressIndicator1);
            this.panelMain.Controls.Add(this.dg_Modifikace_TP);
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
            // dg_Modifikace_TP
            // 
            this.dg_Modifikace_TP.AllowUserToAddRows = false;
            this.dg_Modifikace_TP.AllowUserToDeleteRows = false;
            this.dg_Modifikace_TP.AllowUserToOrderColumns = true;
            this.dg_Modifikace_TP.AllowUserToResizeRows = false;
            this.dg_Modifikace_TP.AutoGenerateColumns = false;
            this.dg_Modifikace_TP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Modifikace_TP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ITEMNMBR_Def,
            this.DESC_Def,
            this.SKL_ID_Def,
            this.SKL_DESC_Def,
            this.ITEMNMBR_fol,
            this.ITEMNMBR,
            this.DESC_Fol,
            this.ITEMDESC,
            this.ITEMCODE_Fol,
            this.ITEMCODE,
            this.VNDITNUM_Fol,
            this.VNDITNUM,
            this.MJ_Fol,
            this.MJ,
            this.SKL_ID_Fol,
            this.SKL_ID,
            this.SKL_DESC_Fol,
            this.SKL_DESC,
            this.PocetVyskytu});
            this.dg_Modifikace_TP.DataSource = this.bs_Modifikace_TP;
            this.dg_Modifikace_TP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Modifikace_TP.EnableHeadersVisualStyles = false;
            this.dg_Modifikace_TP.FilterAndSortEnabled = true;
            this.dg_Modifikace_TP.Location = new System.Drawing.Point(0, 177);
            this.dg_Modifikace_TP.Name = "dg_Modifikace_TP";
            this.dg_Modifikace_TP.ReadOnly = true;
            this.dg_Modifikace_TP.RowHeadersVisible = false;
            this.dg_Modifikace_TP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Modifikace_TP.Size = new System.Drawing.Size(574, 291);
            this.dg_Modifikace_TP.TabIndex = 1;
            this.dg_Modifikace_TP.TabStop = false;
            this.dg_Modifikace_TP.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dg_Modifikace_TP.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_Modifikace_TP_CellFormatting);
            // 
            // bs_Modifikace_TP
            // 
            this.bs_Modifikace_TP.DataMember = "Modifikace_TP";
            this.bs_Modifikace_TP.DataSource = this.ds_Modifikace_TP;
            // 
            // ds_Modifikace_TP
            // 
            this.ds_Modifikace_TP.DataSetName = "Vyroba";
            this.ds_Modifikace_TP.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.panel1.Controls.Add(this.rb_NEShodne);
            this.panel1.Controls.Add(this.rb_Shodne);
            this.panel1.Controls.Add(this.cb_Sklad);
            this.panel1.Controls.Add(this.cb_Vyrobek);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(574, 126);
            this.panel1.TabIndex = 43;
            // 
            // rb_NEShodne
            // 
            this.rb_NEShodne.AutoSize = true;
            this.rb_NEShodne.Location = new System.Drawing.Point(295, 67);
            this.rb_NEShodne.Name = "rb_NEShodne";
            this.rb_NEShodne.Size = new System.Drawing.Size(78, 17);
            this.rb_NEShodne.TabIndex = 46;
            this.rb_NEShodne.Text = "NEShodné";
            this.rb_NEShodne.UseVisualStyleBackColor = true;
            this.rb_NEShodne.CheckedChanged += new System.EventHandler(this.rb_NEShodne_CheckedChanged);
            // 
            // rb_Shodne
            // 
            this.rb_Shodne.AutoSize = true;
            this.rb_Shodne.Location = new System.Drawing.Point(295, 48);
            this.rb_Shodne.Name = "rb_Shodne";
            this.rb_Shodne.Size = new System.Drawing.Size(63, 17);
            this.rb_Shodne.TabIndex = 46;
            this.rb_Shodne.Text = "Shodné";
            this.rb_Shodne.UseVisualStyleBackColor = true;
            this.rb_Shodne.CheckedChanged += new System.EventHandler(this.rb_Shodne_CheckedChanged);
            // 
            // cb_Sklad
            // 
            this.cb_Sklad.FormattingEnabled = true;
            this.cb_Sklad.Location = new System.Drawing.Point(103, 47);
            this.cb_Sklad.Name = "cb_Sklad";
            this.cb_Sklad.Size = new System.Drawing.Size(174, 21);
            this.cb_Sklad.TabIndex = 44;
            // 
            // cb_Vyrobek
            // 
            this.cb_Vyrobek.FormattingEnabled = true;
            this.cb_Vyrobek.Location = new System.Drawing.Point(103, 79);
            this.cb_Vyrobek.Name = "cb_Vyrobek";
            this.cb_Vyrobek.Size = new System.Drawing.Size(174, 21);
            this.cb_Vyrobek.TabIndex = 44;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Výrobek";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "ID Skladu";
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(468, 37);
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
            this.tsmiExport,
            this.tsmiModifikovat});
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
            // tsmiModifikovat
            // 
            this.tsmiModifikovat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ZamenZdrojCil});
            this.tsmiModifikovat.Name = "tsmiModifikovat";
            this.tsmiModifikovat.Size = new System.Drawing.Size(78, 20);
            this.tsmiModifikovat.Text = "Modifikace";
            // 
            // tsmi_ZamenZdrojCil
            // 
            this.tsmi_ZamenZdrojCil.Name = "tsmi_ZamenZdrojCil";
            this.tsmi_ZamenZdrojCil.Size = new System.Drawing.Size(159, 22);
            this.tsmi_ZamenZdrojCil.Text = "Zaměn zdroj-Cíl";
            this.tsmi_ZamenZdrojCil.Click += new System.EventHandler(this.tsmi_ZamenZdrojCil_Click);
            // 
            // bw_Modifikace_TP
            // 
            this.bw_Modifikace_TP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Modifikace_TP_DoWork);
            this.bw_Modifikace_TP.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Modifikace_TP_RunWorkerCompleted);
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
            // bw_ZamenZdrojCil
            // 
            this.bw_ZamenZdrojCil.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_ZamenZdrojCil_DoWork);
            this.bw_ZamenZdrojCil.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_ZamenZdrojCil_RunWorkerCompleted);
            // 
            // ITEMNMBR_Def
            // 
            this.ITEMNMBR_Def.DataPropertyName = "ITEMNMBR_Def";
            this.ITEMNMBR_Def.HeaderText = "Číslo položky výrobek";
            this.ITEMNMBR_Def.Name = "ITEMNMBR_Def";
            this.ITEMNMBR_Def.ReadOnly = true;
            this.ITEMNMBR_Def.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DESC_Def
            // 
            this.DESC_Def.DataPropertyName = "DESC_Def";
            this.DESC_Def.HeaderText = "Název položky výrobek";
            this.DESC_Def.Name = "DESC_Def";
            this.DESC_Def.ReadOnly = true;
            this.DESC_Def.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_ID_Def
            // 
            this.SKL_ID_Def.DataPropertyName = "SKL_ID_Def";
            this.SKL_ID_Def.HeaderText = "ID skladu výrobek";
            this.SKL_ID_Def.Name = "SKL_ID_Def";
            this.SKL_ID_Def.ReadOnly = true;
            this.SKL_ID_Def.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_DESC_Def
            // 
            this.SKL_DESC_Def.DataPropertyName = "SKL_DESC_Def";
            this.SKL_DESC_Def.HeaderText = "Název skladu výrobek";
            this.SKL_DESC_Def.Name = "SKL_DESC_Def";
            this.SKL_DESC_Def.ReadOnly = true;
            // 
            // ITEMNMBR_fol
            // 
            this.ITEMNMBR_fol.DataPropertyName = "ITEMNMBR_fol";
            this.ITEMNMBR_fol.HeaderText = "Číslo položky zdroj";
            this.ITEMNMBR_fol.Name = "ITEMNMBR_fol";
            this.ITEMNMBR_fol.ReadOnly = true;
            this.ITEMNMBR_fol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMNMBR
            // 
            this.ITEMNMBR.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR.HeaderText = "Číslo položky cíl";
            this.ITEMNMBR.Name = "ITEMNMBR";
            this.ITEMNMBR.ReadOnly = true;
            this.ITEMNMBR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DESC_Fol
            // 
            this.DESC_Fol.DataPropertyName = "DESC_Fol";
            this.DESC_Fol.HeaderText = "Název položky zdroj";
            this.DESC_Fol.Name = "DESC_Fol";
            this.DESC_Fol.ReadOnly = true;
            this.DESC_Fol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMDESC
            // 
            this.ITEMDESC.DataPropertyName = "ITEMDESC";
            this.ITEMDESC.HeaderText = "Název položky cíl";
            this.ITEMDESC.Name = "ITEMDESC";
            this.ITEMDESC.ReadOnly = true;
            this.ITEMDESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMCODE_Fol
            // 
            this.ITEMCODE_Fol.DataPropertyName = "ITEMCODE_Fol";
            this.ITEMCODE_Fol.HeaderText = "Kód zdroj";
            this.ITEMCODE_Fol.Name = "ITEMCODE_Fol";
            this.ITEMCODE_Fol.ReadOnly = true;
            this.ITEMCODE_Fol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMCODE
            // 
            this.ITEMCODE.DataPropertyName = "ITEMCODE";
            this.ITEMCODE.HeaderText = "Kód cíl";
            this.ITEMCODE.Name = "ITEMCODE";
            this.ITEMCODE.ReadOnly = true;
            this.ITEMCODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // VNDITNUM_Fol
            // 
            this.VNDITNUM_Fol.DataPropertyName = "VNDITNUM_Fol";
            this.VNDITNUM_Fol.HeaderText = "Čár. Kód zdroj";
            this.VNDITNUM_Fol.Name = "VNDITNUM_Fol";
            this.VNDITNUM_Fol.ReadOnly = true;
            this.VNDITNUM_Fol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // VNDITNUM
            // 
            this.VNDITNUM.DataPropertyName = "VNDITNUM";
            this.VNDITNUM.HeaderText = "Čár. Kód cíl";
            this.VNDITNUM.Name = "VNDITNUM";
            this.VNDITNUM.ReadOnly = true;
            this.VNDITNUM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MJ_Fol
            // 
            this.MJ_Fol.DataPropertyName = "MJ_Fol";
            this.MJ_Fol.HeaderText = "Měrná jednotka zdroj";
            this.MJ_Fol.Name = "MJ_Fol";
            this.MJ_Fol.ReadOnly = true;
            this.MJ_Fol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MJ
            // 
            this.MJ.DataPropertyName = "MJ";
            this.MJ.HeaderText = "Měrná jednotka cíl";
            this.MJ.Name = "MJ";
            this.MJ.ReadOnly = true;
            this.MJ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_ID_Fol
            // 
            this.SKL_ID_Fol.DataPropertyName = "SKL_ID_Fol";
            this.SKL_ID_Fol.HeaderText = "ID skladu zdroj";
            this.SKL_ID_Fol.Name = "SKL_ID_Fol";
            this.SKL_ID_Fol.ReadOnly = true;
            this.SKL_ID_Fol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_ID
            // 
            this.SKL_ID.DataPropertyName = "SKL_ID";
            this.SKL_ID.HeaderText = "ID skladu cíl";
            this.SKL_ID.Name = "SKL_ID";
            this.SKL_ID.ReadOnly = true;
            this.SKL_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_DESC_Fol
            // 
            this.SKL_DESC_Fol.DataPropertyName = "SKL_DESC_Fol";
            this.SKL_DESC_Fol.HeaderText = "Název skladu zdroj";
            this.SKL_DESC_Fol.Name = "SKL_DESC_Fol";
            this.SKL_DESC_Fol.ReadOnly = true;
            this.SKL_DESC_Fol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_DESC
            // 
            this.SKL_DESC.DataPropertyName = "SKL_DESC";
            this.SKL_DESC.HeaderText = "Název skladu cíl";
            this.SKL_DESC.Name = "SKL_DESC";
            this.SKL_DESC.ReadOnly = true;
            this.SKL_DESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PocetVyskytu
            // 
            this.PocetVyskytu.DataPropertyName = "PocetVyskytu";
            this.PocetVyskytu.HeaderText = "Počet Výskytů";
            this.PocetVyskytu.Name = "PocetVyskytu";
            this.PocetVyskytu.ReadOnly = true;
            this.PocetVyskytu.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // FormModifikace_TP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 468);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "FormModifikace_TP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modifikace TP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUzivateleList_FormClosing);
            this.Load += new System.EventHandler(this.FormUzivateleList_Load);
            this.Shown += new System.EventHandler(this.FormZboziSelect_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUzivateleList_KeyDown);
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Modifikace_TP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Modifikace_TP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Modifikace_TP)).EndInit();
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
        protected System.Windows.Forms.ToolStripMenuItem tsmiVybrat;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripMenuItem tsmiKonecVyber;
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
        protected System.Windows.Forms.ToolStripMenuItem tsmiKonecList;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExport;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoExceOznacene;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        protected Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        protected System.Windows.Forms.Panel panel1;
        public Zuby.ADGV.AdvancedDataGridView dg_Modifikace_TP;
        private System.Windows.Forms.ComboBox cb_Sklad;
        private System.Windows.Forms.ComboBox cb_Vyrobek;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource bs_Modifikace_TP;
        private Fask.Interfaces.DataSets.Vyroba ds_Modifikace_TP;
        private System.ComponentModel.BackgroundWorker bw_Modifikace_TP;
        private System.Windows.Forms.ToolStripMenuItem tsmiModifikovat;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ZamenZdrojCil;
        private System.Windows.Forms.CheckBox rb_Shodne;
        private System.Windows.Forms.CheckBox rb_NEShodne;
        private System.ComponentModel.BackgroundWorker bw_ZamenZdrojCil;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR_Def;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESC_Def;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_ID_Def;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_DESC_Def;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR_fol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESC_Fol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMCODE_Fol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNDITNUM_Fol;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNDITNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn MJ_Fol;
        private System.Windows.Forms.DataGridViewTextBoxColumn MJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_ID_Fol;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_DESC_Fol;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn PocetVyskytu;
    }
}