namespace Konzola.Vyroba.Ciselnik
{
    partial class Form_Stroje
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Stroje));
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.dg_Stroj = new Zuby.ADGV.AdvancedDataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_Eventu_Count = new System.Windows.Forms.ToolStripStatusLabel();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cB_name = new System.Windows.Forms.ComboBox();
            this.tB_StrojLokace = new System.Windows.Forms.TextBox();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.tB_StrojSklad = new System.Windows.Forms.TextBox();
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
            this.tB_popis = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenuVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVybrat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiKonecVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonecList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExporty = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskEtiketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportyDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportyDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportyDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportyDoExceOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportyDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportyDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.položkaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNovy = new System.Windows.Forms.ToolStripMenuItem();
            this.bw_Stroje = new System.ComponentModel.BackgroundWorker();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.bs_Machines = new System.Windows.Forms.BindingSource(this.components);
            this.ds_Machines = new Fask.Interfaces.DataSets.Vyroba();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCNCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Stroj)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Machines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Machines)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButtonsZobrazeniVyber
            // 
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonKonec);
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonVybratUzivatele);
            this.panelButtonsZobrazeniVyber.Location = new System.Drawing.Point(1116, 0);
            this.panelButtonsZobrazeniVyber.Name = "panelButtonsZobrazeniVyber";
            this.panelButtonsZobrazeniVyber.Size = new System.Drawing.Size(84, 701);
            this.panelButtonsZobrazeniVyber.TabIndex = 2;
            // 
            // buttonKonec
            // 
            this.buttonKonec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKonec.Location = new System.Drawing.Point(6, 626);
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
            this.panelMain.Controls.Add(this.dg_Stroj);
            this.panelMain.Controls.Add(this.statusStrip1);
            this.panelMain.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Controls.Add(this.menuStrip2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1200, 701);
            this.panelMain.TabIndex = 1;
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(601, 467);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 38;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // dg_Stroj
            // 
            this.dg_Stroj.AllowUserToAddRows = false;
            this.dg_Stroj.AllowUserToDeleteRows = false;
            this.dg_Stroj.AllowUserToOrderColumns = true;
            this.dg_Stroj.AllowUserToResizeRows = false;
            this.dg_Stroj.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Stroj.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.name,
            this.description,
            this.SKL_ID,
            this.LOCNCODE});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_Stroj.DefaultCellStyle = dataGridViewCellStyle1;
            this.dg_Stroj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Stroj.EnableHeadersVisualStyles = false;
            this.dg_Stroj.FilterAndSortEnabled = true;
            this.dg_Stroj.Location = new System.Drawing.Point(0, 315);
            this.dg_Stroj.Name = "dg_Stroj";
            this.dg_Stroj.ReadOnly = true;
            this.dg_Stroj.RowHeadersVisible = false;
            this.dg_Stroj.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Stroj.Size = new System.Drawing.Size(1200, 364);
            this.dg_Stroj.TabIndex = 1;
            this.dg_Stroj.TabStop = false;
            this.dg_Stroj.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dg_Stroj.SelectionChanged += new System.EventHandler(this.dg_OdvodMachineStateSet_SelectionChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_Eventu_Count});
            this.statusStrip1.Location = new System.Drawing.Point(0, 679);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip1.TabIndex = 44;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_Eventu_Count
            // 
            this.tssl_Eventu_Count.Name = "tssl_Eventu_Count";
            this.tssl_Eventu_Count.Size = new System.Drawing.Size(118, 17);
            this.tssl_Eventu_Count.Text = "toolStripStatusLabel1";
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 288);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(1200, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 42;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cB_name);
            this.panel1.Controls.Add(this.tB_StrojLokace);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.tB_StrojSklad);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Controls.Add(this.tB_popis);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tb_id);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 264);
            this.panel1.TabIndex = 43;
            // 
            // cB_name
            // 
            this.cB_name.FormattingEnabled = true;
            this.cB_name.Location = new System.Drawing.Point(93, 108);
            this.cB_name.Name = "cB_name";
            this.cB_name.Size = new System.Drawing.Size(139, 21);
            this.cB_name.TabIndex = 60;
            // 
            // tB_StrojLokace
            // 
            this.tB_StrojLokace.Location = new System.Drawing.Point(93, 155);
            this.tB_StrojLokace.Name = "tB_StrojLokace";
            this.tB_StrojLokace.Size = new System.Drawing.Size(139, 20);
            this.tB_StrojLokace.TabIndex = 58;
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(1121, 42);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 20;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // tB_StrojSklad
            // 
            this.tB_StrojSklad.Location = new System.Drawing.Point(93, 132);
            this.tB_StrojSklad.Name = "tB_StrojSklad";
            this.tB_StrojSklad.Size = new System.Drawing.Size(139, 20);
            this.tB_StrojSklad.TabIndex = 58;
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
            this.tsFiltry.Location = new System.Drawing.Point(0, 0);
            this.tsFiltry.Name = "tsFiltry";
            this.tsFiltry.Size = new System.Drawing.Size(1200, 27);
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
            // tB_popis
            // 
            this.tB_popis.Location = new System.Drawing.Point(93, 85);
            this.tB_popis.Name = "tB_popis";
            this.tB_popis.Size = new System.Drawing.Size(139, 20);
            this.tB_popis.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 57;
            this.label2.Text = "Id";
            // 
            // tb_id
            // 
            this.tb_id.Location = new System.Drawing.Point(93, 63);
            this.tb_id.Name = "tb_id";
            this.tb_id.Size = new System.Drawing.Size(139, 20);
            this.tb_id.TabIndex = 58;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Popis";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 57;
            this.label5.Text = "Stroj";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(24, 136);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 57;
            this.label16.Text = "Stroj sklad";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(24, 159);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(63, 13);
            this.label19.TabIndex = 57;
            this.label19.Text = "Stroj lokace";
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuVyber,
            this.tsmiMenuList,
            this.tsmiExporty,
            this.položkaToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1200, 24);
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
            this.tiskToolStripMenuItem,
            this.tiskEtiketToolStripMenuItem,
            this.tsmiExportyDoCSVVse,
            this.tsmiExportyDoCSVOznacene,
            this.toolStripSeparator6,
            this.tsmiExportyDoExcelVse,
            this.tsmiExportyDoExceOznacene,
            this.toolStripSeparator7,
            this.tsmiExportyDoXMLVse,
            this.tsmiExportyDoXMLOznacene});
            this.tsmiExporty.Name = "tsmiExporty";
            this.tsmiExporty.Size = new System.Drawing.Size(55, 20);
            this.tsmiExporty.Text = "Výstup";
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
            // tsmiExportyDoCSVVse
            // 
            this.tsmiExportyDoCSVVse.Name = "tsmiExportyDoCSVVse";
            this.tsmiExportyDoCSVVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportyDoCSVVse.Text = "Export do CSV vše";
            this.tsmiExportyDoCSVVse.Click += new System.EventHandler(this.exportDoCSVVseToolStripMenuItem_Click);
            // 
            // tsmiExportyDoCSVOznacene
            // 
            this.tsmiExportyDoCSVOznacene.Name = "tsmiExportyDoCSVOznacene";
            this.tsmiExportyDoCSVOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportyDoCSVOznacene.Text = "Export do CSV označené";
            this.tsmiExportyDoCSVOznacene.Click += new System.EventHandler(this.exportDoCSVOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiExportyDoExcelVse
            // 
            this.tsmiExportyDoExcelVse.Name = "tsmiExportyDoExcelVse";
            this.tsmiExportyDoExcelVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportyDoExcelVse.Text = "Export do Excel vše";
            this.tsmiExportyDoExcelVse.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_Click);
            // 
            // tsmiExportyDoExceOznacene
            // 
            this.tsmiExportyDoExceOznacene.Name = "tsmiExportyDoExceOznacene";
            this.tsmiExportyDoExceOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportyDoExceOznacene.Text = "Export do Excel označené";
            this.tsmiExportyDoExceOznacene.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiExportyDoXMLVse
            // 
            this.tsmiExportyDoXMLVse.Name = "tsmiExportyDoXMLVse";
            this.tsmiExportyDoXMLVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportyDoXMLVse.Text = "Export do XML Vše";
            this.tsmiExportyDoXMLVse.Click += new System.EventHandler(this.exportDoXMLVseToolStripMenuItem_Click);
            // 
            // tsmiExportyDoXMLOznacene
            // 
            this.tsmiExportyDoXMLOznacene.Name = "tsmiExportyDoXMLOznacene";
            this.tsmiExportyDoXMLOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportyDoXMLOznacene.Text = "Export do XML označené";
            this.tsmiExportyDoXMLOznacene.Click += new System.EventHandler(this.exportDoXMLOznaceneToolStripMenuItem_Click);
            // 
            // položkaToolStripMenuItem
            // 
            this.položkaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOdstranit,
            this.tsmiUpravit,
            this.tsmiNovy});
            this.položkaToolStripMenuItem.Name = "položkaToolStripMenuItem";
            this.položkaToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.položkaToolStripMenuItem.Text = "Položka";
            // 
            // tsmiOdstranit
            // 
            this.tsmiOdstranit.Name = "tsmiOdstranit";
            this.tsmiOdstranit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiOdstranit.Size = new System.Drawing.Size(166, 22);
            this.tsmiOdstranit.Text = "Odstranit";
            this.tsmiOdstranit.Click += new System.EventHandler(this.tsmiOdstranit_Click);
            // 
            // tsmiUpravit
            // 
            this.tsmiUpravit.Name = "tsmiUpravit";
            this.tsmiUpravit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.tsmiUpravit.Size = new System.Drawing.Size(166, 22);
            this.tsmiUpravit.Text = "Upravit";
            this.tsmiUpravit.Click += new System.EventHandler(this.tsmiUpravit_Click);
            // 
            // tsmiNovy
            // 
            this.tsmiNovy.Name = "tsmiNovy";
            this.tsmiNovy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiNovy.Size = new System.Drawing.Size(166, 22);
            this.tsmiNovy.Text = "Nový";
            this.tsmiNovy.Click += new System.EventHandler(this.tsmiNovy_Click);
            // 
            // bw_Stroje
            // 
            this.bw_Stroje.WorkerSupportsCancellation = true;
            this.bw_Stroje.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_OdvodEvents_DoWork);
            this.bw_Stroje.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_OdvodEvents_RunWorkerCompleted);
            // 
            // panelButtonsZobrazeniList
            // 
            this.panelButtonsZobrazeniList.AutoScroll = true;
            this.panelButtonsZobrazeniList.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniList.Location = new System.Drawing.Point(1200, 0);
            this.panelButtonsZobrazeniList.Name = "panelButtonsZobrazeniList";
            this.panelButtonsZobrazeniList.Size = new System.Drawing.Size(84, 701);
            this.panelButtonsZobrazeniList.TabIndex = 3;
            // 
            // bs_Machines
            // 
            this.bs_Machines.DataMember = "Machines";
            this.bs_Machines.DataSource = this.ds_Machines;
            // 
            // ds_Machines
            // 
            this.ds_Machines.DataSetName = "Vyroba";
            this.ds_Machines.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "id";
            this.Column1.HeaderText = "Id";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "Stroj";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // description
            // 
            this.description.DataPropertyName = "description";
            this.description.HeaderText = "Popis";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            // 
            // SKL_ID
            // 
            this.SKL_ID.DataPropertyName = "SKL_ID";
            this.SKL_ID.HeaderText = "StrojSklad";
            this.SKL_ID.Name = "SKL_ID";
            this.SKL_ID.ReadOnly = true;
            // 
            // LOCNCODE
            // 
            this.LOCNCODE.DataPropertyName = "LOCNCODE";
            this.LOCNCODE.HeaderText = "StrojLokace";
            this.LOCNCODE.Name = "LOCNCODE";
            this.LOCNCODE.ReadOnly = true;
            // 
            // Form_Stroje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 701);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "Form_Stroje";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stroje";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUzivateleList_FormClosing);
            this.Load += new System.EventHandler(this.FormOdvod_MachineStateSetList_Load);
            this.Shown += new System.EventHandler(this.FormOdvod_MachineStateSetList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOdvod_MachineStateSetList_KeyDown);
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Stroj)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Machines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Machines)).EndInit();
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
        protected System.Windows.Forms.ToolStripMenuItem tsmiExporty;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportyDoCSVVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportyDoCSVOznacene;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportyDoExcelVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportyDoExceOznacene;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportyDoXMLVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportyDoXMLOznacene;
        protected Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        protected System.Windows.Forms.Panel panel1;
        public Zuby.ADGV.AdvancedDataGridView dg_Stroj;
        private System.Windows.Forms.BindingSource bs_Machines;
        private Fask.Interfaces.DataSets.Vyroba ds_Machines;
        private System.ComponentModel.BackgroundWorker bw_Stroje;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Eventu_Count;
        private System.Windows.Forms.TextBox tb_id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem tiskToolStripMenuItem;
        private System.Windows.Forms.ComboBox cB_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tB_StrojLokace;
        private System.Windows.Forms.TextBox tB_StrojSklad;
        private System.Windows.Forms.TextBox tB_popis;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem tiskEtiketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem položkaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravit;
        private System.Windows.Forms.ToolStripMenuItem tsmiNovy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCNCODE;
    }
}