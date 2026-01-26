namespace Konzola.Servis
{
    partial class FormVazbyDynTabDefDynTabList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgStav = new Zuby.ADGV.AdvancedDataGridView();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsStav = new System.Windows.Forms.BindingSource(this.components);
            this.dsStav = new Fask.Interfaces.DataSets.Servis();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.advancedDataGridViewSearchToolBar_Stav = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDynTabDefTypeName = new System.Windows.Forms.Label();
            this.lblDynTabDefFullName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgStavNext = new Zuby.ADGV.AdvancedDataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oznaceniDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsStavNext = new System.Windows.Forms.BindingSource(this.components);
            this.dsStavNext = new Fask.Interfaces.DataSets.Servis();
            this.advancedDataGridViewSearchToolBar_StavNext = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblStavNextID = new System.Windows.Forms.Label();
            this.lblStavNextOznaceni = new System.Windows.Forms.Label();
            this.lblStavNextBarcode = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAktualizovat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTabulky = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPridatDataTabulky = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravitDataTabulky = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranitDataTabulky = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPridatDefiniciTabulky = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranitVazbu = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgStav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsStav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsStav)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStavNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsStavNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsStavNext)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgStav
            // 
            this.dgStav.AllowUserToAddRows = false;
            this.dgStav.AllowUserToDeleteRows = false;
            this.dgStav.AllowUserToOrderColumns = true;
            this.dgStav.AllowUserToResizeRows = false;
            this.dgStav.AutoGenerateColumns = false;
            this.dgStav.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgStav.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fullNameDataGridViewTextBoxColumn,
            this.typeNameDataGridViewTextBoxColumn});
            this.dgStav.DataSource = this.bsStav;
            this.dgStav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgStav.EnableHeadersVisualStyles = false;
            this.dgStav.Location = new System.Drawing.Point(0, 148);
            this.dgStav.MultiSelect = false;
            this.dgStav.Name = "dgStav";
            this.dgStav.ReadOnly = true;
            this.dgStav.RowHeadersVisible = false;
            this.dgStav.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgStav.Size = new System.Drawing.Size(302, 502);
            this.dgStav.TabIndex = 1;
            this.dgStav.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dgStav.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dgStav.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            dataGridViewCellStyle6.NullValue = "-";
            this.fullNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Název tabulky";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typeNameDataGridViewTextBoxColumn
            // 
            this.typeNameDataGridViewTextBoxColumn.DataPropertyName = "TypeName";
            dataGridViewCellStyle7.NullValue = "-";
            this.typeNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.typeNameDataGridViewTextBoxColumn.HeaderText = "Zkratka";
            this.typeNameDataGridViewTextBoxColumn.Name = "typeNameDataGridViewTextBoxColumn";
            this.typeNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsStav
            // 
            this.bsStav.DataMember = "CZMST_Servis_Dynamic_Table_Definition";
            this.bsStav.DataSource = this.dsStav;
            // 
            // dsStav
            // 
            this.dsStav.DataSetName = "Servis";
            this.dsStav.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panelButtons
            // 
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(735, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 674);
            this.panelButtons.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.menuStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 674);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgStav);
            this.splitContainer1.Panel1.Controls.Add(this.advancedDataGridViewSearchToolBar_Stav);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgStavNext);
            this.splitContainer1.Panel2.Controls.Add(this.advancedDataGridViewSearchToolBar_StavNext);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(735, 650);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 4;
            // 
            // advancedDataGridViewSearchToolBar_Stav
            // 
            this.advancedDataGridViewSearchToolBar_Stav.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar_Stav.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar_Stav.Location = new System.Drawing.Point(0, 121);
            this.advancedDataGridViewSearchToolBar_Stav.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_Stav.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_Stav.Name = "advancedDataGridViewSearchToolBar_Stav";
            this.advancedDataGridViewSearchToolBar_Stav.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar_Stav.Size = new System.Drawing.Size(302, 27);
            this.advancedDataGridViewSearchToolBar_Stav.TabIndex = 3;
            this.advancedDataGridViewSearchToolBar_Stav.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar_Stav.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar_Stav_Search);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblDynTabDefTypeName);
            this.groupBox2.Controls.Add(this.lblDynTabDefFullName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 121);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Definice tabulky";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Název tabulky:";
            // 
            // lblDynTabDefTypeName
            // 
            this.lblDynTabDefTypeName.AutoSize = true;
            this.lblDynTabDefTypeName.Location = new System.Drawing.Point(97, 16);
            this.lblDynTabDefTypeName.Name = "lblDynTabDefTypeName";
            this.lblDynTabDefTypeName.Size = new System.Drawing.Size(28, 13);
            this.lblDynTabDefTypeName.TabIndex = 5;
            this.lblDynTabDefTypeName.Text = "###";
            // 
            // lblDynTabDefFullName
            // 
            this.lblDynTabDefFullName.AutoSize = true;
            this.lblDynTabDefFullName.Location = new System.Drawing.Point(97, 29);
            this.lblDynTabDefFullName.Name = "lblDynTabDefFullName";
            this.lblDynTabDefFullName.Size = new System.Drawing.Size(28, 13);
            this.lblDynTabDefFullName.TabIndex = 3;
            this.lblDynTabDefFullName.Text = "###";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Zkratka:";
            // 
            // dgStavNext
            // 
            this.dgStavNext.AllowUserToAddRows = false;
            this.dgStavNext.AllowUserToDeleteRows = false;
            this.dgStavNext.AllowUserToOrderColumns = true;
            this.dgStavNext.AllowUserToResizeRows = false;
            this.dgStavNext.AutoGenerateColumns = false;
            this.dgStavNext.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgStavNext.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.oznaceniDataGridViewTextBoxColumn,
            this.barcodeDataGridViewTextBoxColumn});
            this.dgStavNext.DataSource = this.bsStavNext;
            this.dgStavNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgStavNext.EnableHeadersVisualStyles = false;
            this.dgStavNext.Location = new System.Drawing.Point(0, 148);
            this.dgStavNext.MultiSelect = false;
            this.dgStavNext.Name = "dgStavNext";
            this.dgStavNext.ReadOnly = true;
            this.dgStavNext.RowHeadersVisible = false;
            this.dgStavNext.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgStavNext.Size = new System.Drawing.Size(429, 502);
            this.dgStavNext.TabIndex = 2;
            this.dgStavNext.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseDown);
            this.dgStavNext.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            this.dgStavNext.Sorted += new System.EventHandler(this.dataGridView2_Sorted);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            dataGridViewCellStyle8.NullValue = "-";
            this.iDDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // oznaceniDataGridViewTextBoxColumn
            // 
            this.oznaceniDataGridViewTextBoxColumn.DataPropertyName = "Oznaceni";
            dataGridViewCellStyle9.NullValue = "-";
            this.oznaceniDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.oznaceniDataGridViewTextBoxColumn.HeaderText = "Označení";
            this.oznaceniDataGridViewTextBoxColumn.Name = "oznaceniDataGridViewTextBoxColumn";
            this.oznaceniDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // barcodeDataGridViewTextBoxColumn
            // 
            this.barcodeDataGridViewTextBoxColumn.DataPropertyName = "Barcode";
            dataGridViewCellStyle10.NullValue = "-";
            this.barcodeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.barcodeDataGridViewTextBoxColumn.HeaderText = "Čár. kód";
            this.barcodeDataGridViewTextBoxColumn.Name = "barcodeDataGridViewTextBoxColumn";
            this.barcodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsStavNext
            // 
            this.bsStavNext.DataMember = "CZMST_Servis_Dynamic_Table";
            this.bsStavNext.DataSource = this.dsStavNext;
            // 
            // dsStavNext
            // 
            this.dsStavNext.DataSetName = "Servis";
            this.dsStavNext.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar_StavNext
            // 
            this.advancedDataGridViewSearchToolBar_StavNext.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar_StavNext.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar_StavNext.Location = new System.Drawing.Point(0, 121);
            this.advancedDataGridViewSearchToolBar_StavNext.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_StavNext.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_StavNext.Name = "advancedDataGridViewSearchToolBar_StavNext";
            this.advancedDataGridViewSearchToolBar_StavNext.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar_StavNext.Size = new System.Drawing.Size(429, 27);
            this.advancedDataGridViewSearchToolBar_StavNext.TabIndex = 6;
            this.advancedDataGridViewSearchToolBar_StavNext.Text = "advancedDataGridViewSearchToolBar2";
            this.advancedDataGridViewSearchToolBar_StavNext.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar_StavNext_Search);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblStavNextID);
            this.groupBox1.Controls.Add(this.lblStavNextOznaceni);
            this.groupBox1.Controls.Add(this.lblStavNextBarcode);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 121);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data tabulky";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Označení:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(60, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "id:";
            // 
            // lblStavNextID
            // 
            this.lblStavNextID.AutoSize = true;
            this.lblStavNextID.Location = new System.Drawing.Point(84, 15);
            this.lblStavNextID.Name = "lblStavNextID";
            this.lblStavNextID.Size = new System.Drawing.Size(28, 13);
            this.lblStavNextID.TabIndex = 3;
            this.lblStavNextID.Text = "###";
            // 
            // lblStavNextOznaceni
            // 
            this.lblStavNextOznaceni.AutoSize = true;
            this.lblStavNextOznaceni.Location = new System.Drawing.Point(84, 28);
            this.lblStavNextOznaceni.Name = "lblStavNextOznaceni";
            this.lblStavNextOznaceni.Size = new System.Drawing.Size(28, 13);
            this.lblStavNextOznaceni.TabIndex = 5;
            this.lblStavNextOznaceni.Text = "###";
            // 
            // lblStavNextBarcode
            // 
            this.lblStavNextBarcode.AutoSize = true;
            this.lblStavNextBarcode.Location = new System.Drawing.Point(84, 41);
            this.lblStavNextBarcode.Name = "lblStavNextBarcode";
            this.lblStavNextBarcode.Size = new System.Drawing.Size(28, 13);
            this.lblStavNextBarcode.TabIndex = 7;
            this.lblStavNextBarcode.Text = "###";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Čár. kód:";
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiTabulky,
            this.tsmiData});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(735, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // tsmiMenu
            // 
            this.tsmiMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAktualizovat,
            this.tsmiKonec});
            this.tsmiMenu.Name = "tsmiMenu";
            this.tsmiMenu.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenu.Text = "Menu";
            // 
            // tsmiAktualizovat
            // 
            this.tsmiAktualizovat.Name = "tsmiAktualizovat";
            this.tsmiAktualizovat.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tsmiAktualizovat.Size = new System.Drawing.Size(158, 22);
            this.tsmiAktualizovat.Text = "Aktualizovat";
            this.tsmiAktualizovat.Click += new System.EventHandler(this.tsmiAktualizovat_Click);
            // 
            // tsmiKonec
            // 
            this.tsmiKonec.Name = "tsmiKonec";
            this.tsmiKonec.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonec.Size = new System.Drawing.Size(158, 22);
            this.tsmiKonec.Text = "Konec";
            this.tsmiKonec.Click += new System.EventHandler(this.tsmiKonec_Click);
            // 
            // tsmiTabulky
            // 
            this.tsmiTabulky.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPridatDataTabulky,
            this.tsmiUpravitDataTabulky,
            this.tsmiOdstranitDataTabulky});
            this.tsmiTabulky.Name = "tsmiTabulky";
            this.tsmiTabulky.Size = new System.Drawing.Size(59, 20);
            this.tsmiTabulky.Text = "Tabulky";
            // 
            // tsmiPridatDataTabulky
            // 
            this.tsmiPridatDataTabulky.Name = "tsmiPridatDataTabulky";
            this.tsmiPridatDataTabulky.Size = new System.Drawing.Size(191, 22);
            this.tsmiPridatDataTabulky.Text = "Přidat data tabulky";
            this.tsmiPridatDataTabulky.Click += new System.EventHandler(this.tsmiPridatDataTabulky_Click);
            // 
            // tsmiUpravitDataTabulky
            // 
            this.tsmiUpravitDataTabulky.Name = "tsmiUpravitDataTabulky";
            this.tsmiUpravitDataTabulky.Size = new System.Drawing.Size(191, 22);
            this.tsmiUpravitDataTabulky.Text = "Upravit data tabulky";
            this.tsmiUpravitDataTabulky.Click += new System.EventHandler(this.tsmiUpravitDataTabulky_Click);
            // 
            // tsmiOdstranitDataTabulky
            // 
            this.tsmiOdstranitDataTabulky.Name = "tsmiOdstranitDataTabulky";
            this.tsmiOdstranitDataTabulky.Size = new System.Drawing.Size(191, 22);
            this.tsmiOdstranitDataTabulky.Text = "Odstranit data tabulky";
            this.tsmiOdstranitDataTabulky.Click += new System.EventHandler(this.tsmiOdstranitDataTabulky_Click);
            // 
            // tsmiData
            // 
            this.tsmiData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPridatDefiniciTabulky,
            this.tsmiOdstranitVazbu});
            this.tsmiData.Name = "tsmiData";
            this.tsmiData.Size = new System.Drawing.Size(43, 20);
            this.tsmiData.Text = "Data";
            // 
            // tsmiPridatDefiniciTabulky
            // 
            this.tsmiPridatDefiniciTabulky.Name = "tsmiPridatDefiniciTabulky";
            this.tsmiPridatDefiniciTabulky.Size = new System.Drawing.Size(189, 22);
            this.tsmiPridatDefiniciTabulky.Text = "Přidat definici tabulky";
            this.tsmiPridatDefiniciTabulky.Click += new System.EventHandler(this.tsmiPridatDefiniciTabulky_Click);
            // 
            // tsmiOdstranitVazbu
            // 
            this.tsmiOdstranitVazbu.Name = "tsmiOdstranitVazbu";
            this.tsmiOdstranitVazbu.Size = new System.Drawing.Size(189, 22);
            this.tsmiOdstranitVazbu.Text = "Odstranit vazbu";
            this.tsmiOdstranitVazbu.Click += new System.EventHandler(this.tsmiOdstranitVazbu_Click);
            // 
            // FormVazbyDynTabDefDynTabList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 674);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormVazbyDynTabDefDynTabList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vazby dynamických tabulek";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVazbyDynTabDefDynTabList_FormClosing);
            this.Load += new System.EventHandler(this.FormVazbyDynTabDefDynTabList_Load);
            this.Shown += new System.EventHandler(this.FormVazbyDynTabDefDynTabList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormVazbyDynTabDefDynTabList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgStav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsStav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsStav)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStavNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsStavNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsStavNext)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgStav;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.ToolStripMenuItem tsmiAktualizovat;
        private Zuby.ADGV.AdvancedDataGridView dgStavNext;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblDynTabDefTypeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDynTabDefFullName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblStavNextID;
        private System.Windows.Forms.Label lblStavNextOznaceni;
        private System.Windows.Forms.Label lblStavNextBarcode;
        private System.Windows.Forms.Label label6;
        private Fask.Interfaces.DataSets.Servis dsStav;
        private Fask.Interfaces.DataSets.Servis dsStavNext;
        private System.Windows.Forms.BindingSource bsStav;
        private System.Windows.Forms.BindingSource bsStavNext;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oznaceniDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem tsmiTabulky;
        private System.Windows.Forms.ToolStripMenuItem tsmiPridatDataTabulky;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravitDataTabulky;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranitDataTabulky;
        private System.Windows.Forms.ToolStripMenuItem tsmiData;
        private System.Windows.Forms.ToolStripMenuItem tsmiPridatDefiniciTabulky;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranitVazbu;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar_Stav;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar_StavNext;
    }
}