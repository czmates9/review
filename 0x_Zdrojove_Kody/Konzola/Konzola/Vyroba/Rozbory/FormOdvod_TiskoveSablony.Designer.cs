namespace Konzola.Vyroba.Rozbory
{
    partial class FormOdvod_TiskoveSablony
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOdvod_TiskoveSablony));
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.dg_OdvodTiskoveSablony = new Zuby.ADGV.AdvancedDataGridView();
            this.nazevoknaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nazevDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formularDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_OdvodTiskoveSablony = new System.Windows.Forms.BindingSource(this.components);
            this.ds_OdvodTiskoveSablony = new Fask.Interfaces.DataSets.Vyroba();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_Eventu_Count = new System.Windows.Forms.ToolStripStatusLabel();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cB_typ = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_vytvorit = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_editace = new System.Windows.Forms.Button();
            this.btn_vyber = new System.Windows.Forms.Button();
            this.btn_zrusit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_machineid = new System.Windows.Forms.TextBox();
            this.tB_loginid = new System.Windows.Forms.TextBox();
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
            this.bw_OdvodTiskoveSablony = new System.ComponentModel.BackgroundWorker();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_OdvodTiskoveSablony)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_OdvodTiskoveSablony)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_OdvodTiskoveSablony)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
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
            this.panelMain.Controls.Add(this.dg_OdvodTiskoveSablony);
            this.panelMain.Controls.Add(this.statusStrip1);
            this.panelMain.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(702, 520);
            this.panelMain.TabIndex = 1;
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(337, 307);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 38;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // dg_OdvodTiskoveSablony
            // 
            this.dg_OdvodTiskoveSablony.AllowUserToAddRows = false;
            this.dg_OdvodTiskoveSablony.AllowUserToDeleteRows = false;
            this.dg_OdvodTiskoveSablony.AllowUserToOrderColumns = true;
            this.dg_OdvodTiskoveSablony.AllowUserToResizeRows = false;
            this.dg_OdvodTiskoveSablony.AutoGenerateColumns = false;
            this.dg_OdvodTiskoveSablony.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_OdvodTiskoveSablony.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nazevoknaDataGridViewTextBoxColumn,
            this.nazevDataGridViewTextBoxColumn,
            this.ordDataGridViewTextBoxColumn,
            this.typDataGridViewTextBoxColumn,
            this.loginidDataGridViewTextBoxColumn,
            this.machineidDataGridViewTextBoxColumn,
            this.formularDataGridViewTextBoxColumn});
            this.dg_OdvodTiskoveSablony.DataSource = this.bs_OdvodTiskoveSablony;
            this.dg_OdvodTiskoveSablony.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_OdvodTiskoveSablony.EnableHeadersVisualStyles = false;
            this.dg_OdvodTiskoveSablony.FilterAndSortEnabled = true;
            this.dg_OdvodTiskoveSablony.Location = new System.Drawing.Point(0, 203);
            this.dg_OdvodTiskoveSablony.Name = "dg_OdvodTiskoveSablony";
            this.dg_OdvodTiskoveSablony.ReadOnly = true;
            this.dg_OdvodTiskoveSablony.RowHeadersVisible = false;
            this.dg_OdvodTiskoveSablony.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_OdvodTiskoveSablony.Size = new System.Drawing.Size(702, 295);
            this.dg_OdvodTiskoveSablony.TabIndex = 1;
            this.dg_OdvodTiskoveSablony.TabStop = false;
            this.dg_OdvodTiskoveSablony.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dg_OdvodTiskoveSablony.SelectionChanged += new System.EventHandler(this.dg_OdvodTiskoveSablony_SelectionChanged);
            // 
            // nazevoknaDataGridViewTextBoxColumn
            // 
            this.nazevoknaDataGridViewTextBoxColumn.DataPropertyName = "nazev_okna";
            this.nazevoknaDataGridViewTextBoxColumn.HeaderText = "Název okna";
            this.nazevoknaDataGridViewTextBoxColumn.Name = "nazevoknaDataGridViewTextBoxColumn";
            this.nazevoknaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nazevDataGridViewTextBoxColumn
            // 
            this.nazevDataGridViewTextBoxColumn.DataPropertyName = "nazev";
            this.nazevDataGridViewTextBoxColumn.HeaderText = "Název";
            this.nazevDataGridViewTextBoxColumn.Name = "nazevDataGridViewTextBoxColumn";
            this.nazevDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ordDataGridViewTextBoxColumn
            // 
            this.ordDataGridViewTextBoxColumn.DataPropertyName = "ord";
            this.ordDataGridViewTextBoxColumn.HeaderText = "Pořadí";
            this.ordDataGridViewTextBoxColumn.Name = "ordDataGridViewTextBoxColumn";
            this.ordDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typDataGridViewTextBoxColumn
            // 
            this.typDataGridViewTextBoxColumn.DataPropertyName = "typ";
            this.typDataGridViewTextBoxColumn.HeaderText = "Typ";
            this.typDataGridViewTextBoxColumn.Name = "typDataGridViewTextBoxColumn";
            this.typDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // loginidDataGridViewTextBoxColumn
            // 
            this.loginidDataGridViewTextBoxColumn.DataPropertyName = "loginid";
            this.loginidDataGridViewTextBoxColumn.HeaderText = "Uživatel";
            this.loginidDataGridViewTextBoxColumn.Name = "loginidDataGridViewTextBoxColumn";
            this.loginidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // machineidDataGridViewTextBoxColumn
            // 
            this.machineidDataGridViewTextBoxColumn.DataPropertyName = "machineid";
            this.machineidDataGridViewTextBoxColumn.HeaderText = "Licence";
            this.machineidDataGridViewTextBoxColumn.Name = "machineidDataGridViewTextBoxColumn";
            this.machineidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // formularDataGridViewTextBoxColumn
            // 
            this.formularDataGridViewTextBoxColumn.DataPropertyName = "formular";
            this.formularDataGridViewTextBoxColumn.HeaderText = "Šablona";
            this.formularDataGridViewTextBoxColumn.Name = "formularDataGridViewTextBoxColumn";
            this.formularDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bs_OdvodTiskoveSablony
            // 
            this.bs_OdvodTiskoveSablony.DataMember = "FASK_FORMULARE";
            this.bs_OdvodTiskoveSablony.DataSource = this.ds_OdvodTiskoveSablony;
            // 
            // ds_OdvodTiskoveSablony
            // 
            this.ds_OdvodTiskoveSablony.DataSetName = "Vyroba";
            this.ds_OdvodTiskoveSablony.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_Eventu_Count});
            this.statusStrip1.Location = new System.Drawing.Point(0, 498);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(702, 22);
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
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 176);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(702, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 42;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cB_typ);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btn_vyber);
            this.panel1.Controls.Add(this.btn_zrusit);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tb_machineid);
            this.panel1.Controls.Add(this.tB_loginid);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(702, 176);
            this.panel1.TabIndex = 43;
            // 
            // cB_typ
            // 
            this.cB_typ.FormattingEnabled = true;
            this.cB_typ.Location = new System.Drawing.Point(98, 95);
            this.cB_typ.Name = "cB_typ";
            this.cB_typ.Size = new System.Drawing.Size(157, 21);
            this.cB_typ.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_vytvorit);
            this.groupBox1.Controls.Add(this.btn_delete);
            this.groupBox1.Controls.Add(this.btn_editace);
            this.groupBox1.Location = new System.Drawing.Point(369, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 130);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Práce se záznamy:";
            // 
            // btn_vytvorit
            // 
            this.btn_vytvorit.Location = new System.Drawing.Point(15, 22);
            this.btn_vytvorit.Name = "btn_vytvorit";
            this.btn_vytvorit.Size = new System.Drawing.Size(101, 39);
            this.btn_vytvorit.TabIndex = 7;
            this.btn_vytvorit.Text = "Vytvořit";
            this.btn_vytvorit.UseVisualStyleBackColor = true;
            this.btn_vytvorit.Click += new System.EventHandler(this.btn_vytvorit_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(15, 85);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(101, 39);
            this.btn_delete.TabIndex = 9;
            this.btn_delete.Text = "Odtranit";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_editace
            // 
            this.btn_editace.Location = new System.Drawing.Point(133, 22);
            this.btn_editace.Name = "btn_editace";
            this.btn_editace.Size = new System.Drawing.Size(101, 39);
            this.btn_editace.TabIndex = 8;
            this.btn_editace.Text = "Editace";
            this.btn_editace.UseVisualStyleBackColor = true;
            this.btn_editace.Click += new System.EventHandler(this.btn_editace_Click);
            // 
            // btn_vyber
            // 
            this.btn_vyber.Location = new System.Drawing.Point(256, 122);
            this.btn_vyber.Name = "btn_vyber";
            this.btn_vyber.Size = new System.Drawing.Size(97, 38);
            this.btn_vyber.TabIndex = 5;
            this.btn_vyber.Text = "Vybrat";
            this.btn_vyber.UseVisualStyleBackColor = true;
            this.btn_vyber.Click += new System.EventHandler(this.btn_vyber_Click);
            // 
            // btn_zrusit
            // 
            this.btn_zrusit.Location = new System.Drawing.Point(24, 122);
            this.btn_zrusit.Name = "btn_zrusit";
            this.btn_zrusit.Size = new System.Drawing.Size(101, 38);
            this.btn_zrusit.TabIndex = 6;
            this.btn_zrusit.Text = "Zrušit";
            this.btn_zrusit.UseVisualStyleBackColor = true;
            this.btn_zrusit.Click += new System.EventHandler(this.btn_zrusit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Typ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Licence";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Uživatel";
            // 
            // tb_machineid
            // 
            this.tb_machineid.Enabled = false;
            this.tb_machineid.Location = new System.Drawing.Point(98, 71);
            this.tb_machineid.Name = "tb_machineid";
            this.tb_machineid.Size = new System.Drawing.Size(157, 20);
            this.tb_machineid.TabIndex = 2;
            // 
            // tB_loginid
            // 
            this.tB_loginid.Enabled = false;
            this.tB_loginid.Location = new System.Drawing.Point(98, 46);
            this.tB_loginid.Name = "tB_loginid";
            this.tB_loginid.Size = new System.Drawing.Size(157, 20);
            this.tB_loginid.TabIndex = 1;
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Enabled = false;
            this.buttonVyhledat.Location = new System.Drawing.Point(280, 46);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 45);
            this.buttonVyhledat.TabIndex = 4;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
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
            this.tsFiltry.Size = new System.Drawing.Size(702, 27);
            this.tsFiltry.TabIndex = 41;
            this.tsFiltry.Text = "toolStrip1";
            this.tsFiltry.Visible = false;
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
            // bw_OdvodTiskoveSablony
            // 
            this.bw_OdvodTiskoveSablony.WorkerSupportsCancellation = true;
            this.bw_OdvodTiskoveSablony.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_OdvodTiskoveSablony_DoWork);
            this.bw_OdvodTiskoveSablony.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_OdvodTiskoveSablony_RunWorkerCompleted);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "nazev_okna";
            this.dataGridViewTextBoxColumn1.HeaderText = "Název okna";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "nazev";
            this.dataGridViewTextBoxColumn2.HeaderText = "Název";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ord";
            this.dataGridViewTextBoxColumn3.HeaderText = "Pořadí";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "typ";
            this.dataGridViewTextBoxColumn4.HeaderText = "Typ";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "loginid";
            this.dataGridViewTextBoxColumn5.HeaderText = "Uživatel";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "machineid";
            this.dataGridViewTextBoxColumn6.HeaderText = "Licence";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "formular";
            this.dataGridViewTextBoxColumn7.HeaderText = "Šablona";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // FormOdvod_TiskoveSablony
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 520);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.KeyPreview = true;
            this.Name = "FormOdvod_TiskoveSablony";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tiskové šablony";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUzivateleList_FormClosing);
            this.Load += new System.EventHandler(this.FormOdvod_TiskoveSablony_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOdvod_TiskoveSablony_KeyDown);
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_OdvodTiskoveSablony)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_OdvodTiskoveSablony)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_OdvodTiskoveSablony)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelButtonsZobrazeniVyber;
        protected System.Windows.Forms.Panel panelMain;
        protected System.Windows.Forms.Button buttonVybratUzivatele;
        protected System.Windows.Forms.Button buttonKonec;
        protected System.Windows.Forms.Button buttonVyhledat;
        protected ProgressControls.ProgressIndicator progressIndicator1;
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
        protected Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        protected System.Windows.Forms.Panel panel1;
        public Zuby.ADGV.AdvancedDataGridView dg_OdvodTiskoveSablony;
        private System.Windows.Forms.BindingSource bs_OdvodTiskoveSablony;
        private Fask.Interfaces.DataSets.Vyroba ds_OdvodTiskoveSablony;
        private System.ComponentModel.BackgroundWorker bw_OdvodTiskoveSablony;
        private System.Windows.Forms.TextBox tB_loginid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Eventu_Count;
        private System.Windows.Forms.Button btn_zrusit;
        private System.Windows.Forms.Button btn_vyber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_machineid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_editace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cB_typ;
        private System.Windows.Forms.Button btn_vytvorit;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazevoknaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazevDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loginidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn formularDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    }
}