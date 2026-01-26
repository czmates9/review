namespace Konzola
{
    partial class FormPotrebaMaterialu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPotrebaMaterialu));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Prehled = new System.Windows.Forms.TabPage();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.dg_PotMat = new Zuby.ADGV.AdvancedDataGridView();
            this.bs_PotMat = new System.Windows.Forms.BindingSource(this.components);
            this.ds_Vyroba = new Fask.Interfaces.DataSets.Vyroba();
            this.advancedDataGridViewSearchToolBar_PotMat = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.tabPage_Soucty = new System.Windows.Forms.TabPage();
            this.progressIndicator2 = new ProgressControls.ProgressIndicator();
            this.dg_PotMat_JenSoucty = new Zuby.ADGV.AdvancedDataGridView();
            this.iTEMNMBRMATDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMCODEMATDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNAMEMATDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxValueTypeColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_PotMat_JenSoucet = new System.Windows.Forms.BindingSource(this.components);
            this.advancedDataGridViewSearchToolBar2 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
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
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.konecToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aktualizovatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDoCSVVseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDoCSVOznaceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.exportDoExcelVseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDoExceOznaceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.exportDoXMLVseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDoXMLOznaceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bw_PotMat = new System.ComponentModel.BackgroundWorker();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.ITEMNMBR_MAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMCODE_MAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNAME_MAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYSHPPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY_POHODA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY_ROZDIL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MJ_MAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR_VYR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VNDITNUM_VYR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC_VYR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountEntries_VP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPNUMBE_VP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPDESC_VP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPTYPE_VP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VNDDOCNMH_VP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_Prehled.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PotMat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PotMat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Vyroba)).BeginInit();
            this.tabPage_Soucty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PotMat_JenSoucty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PotMat_JenSoucet)).BeginInit();
            this.panel1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl1);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Controls.Add(this.menuStrip2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(658, 468);
            this.panelMain.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Prehled);
            this.tabControl1.Controls.Add(this.tabPage_Soucty);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 150);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(658, 318);
            this.tabControl1.TabIndex = 44;
            // 
            // tabPage_Prehled
            // 
            this.tabPage_Prehled.Controls.Add(this.progressIndicator1);
            this.tabPage_Prehled.Controls.Add(this.dg_PotMat);
            this.tabPage_Prehled.Controls.Add(this.advancedDataGridViewSearchToolBar_PotMat);
            this.tabPage_Prehled.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Prehled.Name = "tabPage_Prehled";
            this.tabPage_Prehled.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Prehled.Size = new System.Drawing.Size(650, 292);
            this.tabPage_Prehled.TabIndex = 0;
            this.tabPage_Prehled.Text = "Přehled";
            this.tabPage_Prehled.UseVisualStyleBackColor = true;
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(297, 115);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 38;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // dg_PotMat
            // 
            this.dg_PotMat.AllowUserToAddRows = false;
            this.dg_PotMat.AllowUserToDeleteRows = false;
            this.dg_PotMat.AllowUserToOrderColumns = true;
            this.dg_PotMat.AllowUserToResizeRows = false;
            this.dg_PotMat.AutoGenerateColumns = false;
            this.dg_PotMat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_PotMat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ITEMNMBR_MAT,
            this.ITEMCODE_MAT,
            this.ITEMNAME_MAT,
            this.QTYSHPPD,
            this.QTY_POHODA,
            this.QTY_ROZDIL,
            this.MJ_MAT,
            this.ITEMNMBR_VYR,
            this.VNDITNUM_VYR,
            this.ITEMDESC_VYR,
            this.CountEntries_VP,
            this.SOPNUMBE_VP,
            this.SOPDESC_VP,
            this.SOPTYPE_VP,
            this.VNDDOCNMH_VP});
            this.dg_PotMat.DataSource = this.bs_PotMat;
            this.dg_PotMat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_PotMat.FilterAndSortEnabled = true;
            this.dg_PotMat.Location = new System.Drawing.Point(3, 30);
            this.dg_PotMat.Name = "dg_PotMat";
            this.dg_PotMat.ReadOnly = true;
            this.dg_PotMat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_PotMat.Size = new System.Drawing.Size(644, 259);
            this.dg_PotMat.TabIndex = 1;
            this.dg_PotMat.TabStop = false;
            this.dg_PotMat.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_PotMat_CellFormatting);
            this.dg_PotMat.SelectionChanged += new System.EventHandler(this.dg_PotMat_SelectionChanged);
            // 
            // bs_PotMat
            // 
            this.bs_PotMat.DataMember = "VPH_PotrebaMaterialu";
            this.bs_PotMat.DataSource = this.ds_Vyroba;
            // 
            // ds_Vyroba
            // 
            this.ds_Vyroba.DataSetName = "Vyroba";
            this.ds_Vyroba.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar_PotMat
            // 
            this.advancedDataGridViewSearchToolBar_PotMat.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar_PotMat.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar_PotMat.Location = new System.Drawing.Point(3, 3);
            this.advancedDataGridViewSearchToolBar_PotMat.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_PotMat.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_PotMat.Name = "advancedDataGridViewSearchToolBar_PotMat";
            this.advancedDataGridViewSearchToolBar_PotMat.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar_PotMat.Size = new System.Drawing.Size(644, 27);
            this.advancedDataGridViewSearchToolBar_PotMat.TabIndex = 42;
            this.advancedDataGridViewSearchToolBar_PotMat.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar_PotMat.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // tabPage_Soucty
            // 
            this.tabPage_Soucty.Controls.Add(this.progressIndicator2);
            this.tabPage_Soucty.Controls.Add(this.dg_PotMat_JenSoucty);
            this.tabPage_Soucty.Controls.Add(this.advancedDataGridViewSearchToolBar2);
            this.tabPage_Soucty.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Soucty.Name = "tabPage_Soucty";
            this.tabPage_Soucty.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Soucty.Size = new System.Drawing.Size(650, 292);
            this.tabPage_Soucty.TabIndex = 1;
            this.tabPage_Soucty.Text = "Jen součty";
            this.tabPage_Soucty.UseVisualStyleBackColor = true;
            // 
            // progressIndicator2
            // 
            this.progressIndicator2.Location = new System.Drawing.Point(297, 115);
            this.progressIndicator2.Name = "progressIndicator2";
            this.progressIndicator2.Percentage = 0F;
            this.progressIndicator2.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator2.TabIndex = 44;
            this.progressIndicator2.Text = "progressIndicator2";
            this.progressIndicator2.Visible = false;
            // 
            // dg_PotMat_JenSoucty
            // 
            this.dg_PotMat_JenSoucty.AllowUserToAddRows = false;
            this.dg_PotMat_JenSoucty.AllowUserToDeleteRows = false;
            this.dg_PotMat_JenSoucty.AllowUserToOrderColumns = true;
            this.dg_PotMat_JenSoucty.AllowUserToResizeRows = false;
            this.dg_PotMat_JenSoucty.AutoGenerateColumns = false;
            this.dg_PotMat_JenSoucty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_PotMat_JenSoucty.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMNMBRMATDataGridViewTextBoxColumn,
            this.iTEMCODEMATDataGridViewTextBoxColumn,
            this.iTEMNAMEMATDataGridViewTextBoxColumn,
            this.qTYSHPPDDataGridViewTextBoxColumn,
            this.Column1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxValueTypeColumn1});
            this.dg_PotMat_JenSoucty.DataSource = this.bs_PotMat_JenSoucet;
            this.dg_PotMat_JenSoucty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_PotMat_JenSoucty.FilterAndSortEnabled = true;
            this.dg_PotMat_JenSoucty.Location = new System.Drawing.Point(3, 30);
            this.dg_PotMat_JenSoucty.Name = "dg_PotMat_JenSoucty";
            this.dg_PotMat_JenSoucty.ReadOnly = true;
            this.dg_PotMat_JenSoucty.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_PotMat_JenSoucty.Size = new System.Drawing.Size(644, 259);
            this.dg_PotMat_JenSoucty.TabIndex = 43;
            this.dg_PotMat_JenSoucty.TabStop = false;
            this.dg_PotMat_JenSoucty.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_PotMat_JenSoucty_CellFormatting);
            this.dg_PotMat_JenSoucty.SelectionChanged += new System.EventHandler(this.dg_PotMat_JenSoucty_SelectionChanged);
            // 
            // iTEMNMBRMATDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRMATDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR_MAT";
            this.iTEMNMBRMATDataGridViewTextBoxColumn.HeaderText = "Číslo materálu";
            this.iTEMNMBRMATDataGridViewTextBoxColumn.Name = "iTEMNMBRMATDataGridViewTextBoxColumn";
            this.iTEMNMBRMATDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMNMBRMATDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // iTEMCODEMATDataGridViewTextBoxColumn
            // 
            this.iTEMCODEMATDataGridViewTextBoxColumn.DataPropertyName = "ITEMCODE_MAT";
            this.iTEMCODEMATDataGridViewTextBoxColumn.HeaderText = "Kód materiálu";
            this.iTEMCODEMATDataGridViewTextBoxColumn.Name = "iTEMCODEMATDataGridViewTextBoxColumn";
            this.iTEMCODEMATDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMCODEMATDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // iTEMNAMEMATDataGridViewTextBoxColumn
            // 
            this.iTEMNAMEMATDataGridViewTextBoxColumn.DataPropertyName = "ITEMNAME_MAT";
            this.iTEMNAMEMATDataGridViewTextBoxColumn.HeaderText = "Název materálu";
            this.iTEMNAMEMATDataGridViewTextBoxColumn.Name = "iTEMNAMEMATDataGridViewTextBoxColumn";
            this.iTEMNAMEMATDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMNAMEMATDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // qTYSHPPDDataGridViewTextBoxColumn
            // 
            this.qTYSHPPDDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPD";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N5";
            dataGridViewCellStyle4.NullValue = null;
            this.qTYSHPPDDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.qTYSHPPDDataGridViewTextBoxColumn.HeaderText = "Požadované množství";
            this.qTYSHPPDDataGridViewTextBoxColumn.Name = "qTYSHPPDDataGridViewTextBoxColumn";
            this.qTYSHPPDDataGridViewTextBoxColumn.ReadOnly = true;
            this.qTYSHPPDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "QTY_POHODA";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N5";
            this.Column1.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column1.HeaderText = "Množství POHODA";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "QTY_ROZDIL";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N5";
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn1.HeaderText = "Množství rozdíl";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxValueTypeColumn1
            // 
            this.dataGridViewTextBoxValueTypeColumn1.DataPropertyName = "MJ_MAT";
            this.dataGridViewTextBoxValueTypeColumn1.HeaderText = "Měrná jednotka";
            this.dataGridViewTextBoxValueTypeColumn1.Name = "dataGridViewTextBoxValueTypeColumn1";
            this.dataGridViewTextBoxValueTypeColumn1.ReadOnly = true;
            this.dataGridViewTextBoxValueTypeColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // bs_PotMat_JenSoucet
            // 
            this.bs_PotMat_JenSoucet.DataMember = "VPH_PotrebaMaterialu_JenSoucet";
            this.bs_PotMat_JenSoucet.DataSource = this.ds_Vyroba;
            // 
            // advancedDataGridViewSearchToolBar2
            // 
            this.advancedDataGridViewSearchToolBar2.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar2.Location = new System.Drawing.Point(3, 3);
            this.advancedDataGridViewSearchToolBar2.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar2.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar2.Name = "advancedDataGridViewSearchToolBar2";
            this.advancedDataGridViewSearchToolBar2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar2.Size = new System.Drawing.Size(644, 27);
            this.advancedDataGridViewSearchToolBar2.TabIndex = 45;
            this.advancedDataGridViewSearchToolBar2.Text = "advancedDataGridViewSearchToolBar2";
            this.advancedDataGridViewSearchToolBar2.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar2_Search);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 126);
            this.panel1.TabIndex = 43;
            this.panel1.Visible = false;
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(559, 43);
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
            this.tsFiltry.Size = new System.Drawing.Size(658, 25);
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
            this.tsmiMenuList,
            this.exportyToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(658, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // tsmiMenuList
            // 
            this.tsmiMenuList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konecToolStripMenuItem1,
            this.toolStripSeparator1,
            this.aktualizovatToolStripMenuItem});
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // aktualizovatToolStripMenuItem
            // 
            this.aktualizovatToolStripMenuItem.Name = "aktualizovatToolStripMenuItem";
            this.aktualizovatToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.aktualizovatToolStripMenuItem.Text = "Aktualizovat";
            this.aktualizovatToolStripMenuItem.Click += new System.EventHandler(this.buttonVyhledat_Click);
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
            this.exportDoCSVVseToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exportDoCSVVseToolStripMenuItem.Text = "Export do CSV vše";
            this.exportDoCSVVseToolStripMenuItem.Click += new System.EventHandler(this.exportDoCSVVseToolStripMenuItem_Click);
            // 
            // exportDoCSVOznaceneToolStripMenuItem
            // 
            this.exportDoCSVOznaceneToolStripMenuItem.Name = "exportDoCSVOznaceneToolStripMenuItem";
            this.exportDoCSVOznaceneToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exportDoCSVOznaceneToolStripMenuItem.Text = "Export do CSV označené";
            this.exportDoCSVOznaceneToolStripMenuItem.Click += new System.EventHandler(this.exportDoCSVOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(205, 6);
            // 
            // exportDoExcelVseToolStripMenuItem
            // 
            this.exportDoExcelVseToolStripMenuItem.Name = "exportDoExcelVseToolStripMenuItem";
            this.exportDoExcelVseToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exportDoExcelVseToolStripMenuItem.Text = "Export do Excel vše";
            this.exportDoExcelVseToolStripMenuItem.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_Click);
            // 
            // exportDoExceOznaceneToolStripMenuItem
            // 
            this.exportDoExceOznaceneToolStripMenuItem.Name = "exportDoExceOznaceneToolStripMenuItem";
            this.exportDoExceOznaceneToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exportDoExceOznaceneToolStripMenuItem.Text = "Export do Excel označené";
            this.exportDoExceOznaceneToolStripMenuItem.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(205, 6);
            // 
            // exportDoXMLVseToolStripMenuItem
            // 
            this.exportDoXMLVseToolStripMenuItem.Name = "exportDoXMLVseToolStripMenuItem";
            this.exportDoXMLVseToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exportDoXMLVseToolStripMenuItem.Text = "Export do XML Vše";
            this.exportDoXMLVseToolStripMenuItem.Click += new System.EventHandler(this.exportDoXMLVseToolStripMenuItem_Click);
            // 
            // exportDoXMLOznaceneToolStripMenuItem
            // 
            this.exportDoXMLOznaceneToolStripMenuItem.Name = "exportDoXMLOznaceneToolStripMenuItem";
            this.exportDoXMLOznaceneToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exportDoXMLOznaceneToolStripMenuItem.Text = "Export do XML označené";
            this.exportDoXMLOznaceneToolStripMenuItem.Click += new System.EventHandler(this.exportDoXMLOznaceneToolStripMenuItem_Click);
            // 
            // bw_PotMat
            // 
            this.bw_PotMat.WorkerSupportsCancellation = true;
            this.bw_PotMat.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_PotMat_DoWork);
            this.bw_PotMat.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_PotMat_RunWorkerCompleted);
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
            // ITEMNMBR_MAT
            // 
            this.ITEMNMBR_MAT.DataPropertyName = "ITEMNMBR_MAT";
            this.ITEMNMBR_MAT.HeaderText = "Číslo materálu";
            this.ITEMNMBR_MAT.Name = "ITEMNMBR_MAT";
            this.ITEMNMBR_MAT.ReadOnly = true;
            this.ITEMNMBR_MAT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMCODE_MAT
            // 
            this.ITEMCODE_MAT.DataPropertyName = "ITEMCODE_MAT";
            this.ITEMCODE_MAT.HeaderText = "Kód materiálu";
            this.ITEMCODE_MAT.Name = "ITEMCODE_MAT";
            this.ITEMCODE_MAT.ReadOnly = true;
            this.ITEMCODE_MAT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMNAME_MAT
            // 
            this.ITEMNAME_MAT.DataPropertyName = "ITEMNAME_MAT";
            this.ITEMNAME_MAT.HeaderText = "Název materálu";
            this.ITEMNAME_MAT.Name = "ITEMNAME_MAT";
            this.ITEMNAME_MAT.ReadOnly = true;
            this.ITEMNAME_MAT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // QTYSHPPD
            // 
            this.QTYSHPPD.DataPropertyName = "QTYSHPPD";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N5";
            this.QTYSHPPD.DefaultCellStyle = dataGridViewCellStyle1;
            this.QTYSHPPD.HeaderText = "Požadované množství";
            this.QTYSHPPD.Name = "QTYSHPPD";
            this.QTYSHPPD.ReadOnly = true;
            this.QTYSHPPD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // QTY_POHODA
            // 
            this.QTY_POHODA.DataPropertyName = "QTY_POHODA";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N5";
            this.QTY_POHODA.DefaultCellStyle = dataGridViewCellStyle2;
            this.QTY_POHODA.HeaderText = "Množství POHODA";
            this.QTY_POHODA.Name = "QTY_POHODA";
            this.QTY_POHODA.ReadOnly = true;
            // 
            // QTY_ROZDIL
            // 
            this.QTY_ROZDIL.DataPropertyName = "QTY_ROZDIL";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N5";
            this.QTY_ROZDIL.DefaultCellStyle = dataGridViewCellStyle3;
            this.QTY_ROZDIL.HeaderText = "Množství rozdíl";
            this.QTY_ROZDIL.Name = "QTY_ROZDIL";
            this.QTY_ROZDIL.ReadOnly = true;
            // 
            // MJ_MAT
            // 
            this.MJ_MAT.DataPropertyName = "MJ_MAT";
            this.MJ_MAT.HeaderText = "Měrná jednotka";
            this.MJ_MAT.Name = "MJ_MAT";
            this.MJ_MAT.ReadOnly = true;
            this.MJ_MAT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMNMBR_VYR
            // 
            this.ITEMNMBR_VYR.DataPropertyName = "ITEMNMBR_VYR";
            this.ITEMNMBR_VYR.HeaderText = "Číslo výrobku";
            this.ITEMNMBR_VYR.Name = "ITEMNMBR_VYR";
            this.ITEMNMBR_VYR.ReadOnly = true;
            this.ITEMNMBR_VYR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // VNDITNUM_VYR
            // 
            this.VNDITNUM_VYR.DataPropertyName = "VNDITNUM_VYR";
            this.VNDITNUM_VYR.HeaderText = "Čár. Kód výrobku";
            this.VNDITNUM_VYR.Name = "VNDITNUM_VYR";
            this.VNDITNUM_VYR.ReadOnly = true;
            this.VNDITNUM_VYR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMDESC_VYR
            // 
            this.ITEMDESC_VYR.DataPropertyName = "ITEMDESC_VYR";
            this.ITEMDESC_VYR.HeaderText = "Název výrobku";
            this.ITEMDESC_VYR.Name = "ITEMDESC_VYR";
            this.ITEMDESC_VYR.ReadOnly = true;
            this.ITEMDESC_VYR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CountEntries_VP
            // 
            this.CountEntries_VP.DataPropertyName = "CountEntries_VP";
            this.CountEntries_VP.HeaderText = "Číslo vyr. přikazu";
            this.CountEntries_VP.Name = "CountEntries_VP";
            this.CountEntries_VP.ReadOnly = true;
            this.CountEntries_VP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SOPNUMBE_VP
            // 
            this.SOPNUMBE_VP.DataPropertyName = "SOPNUMBE_VP";
            this.SOPNUMBE_VP.HeaderText = "Číslo zakázky";
            this.SOPNUMBE_VP.Name = "SOPNUMBE_VP";
            this.SOPNUMBE_VP.ReadOnly = true;
            this.SOPNUMBE_VP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SOPDESC_VP
            // 
            this.SOPDESC_VP.DataPropertyName = "SOPDESC_VP";
            this.SOPDESC_VP.HeaderText = "Popis zakázky";
            this.SOPDESC_VP.Name = "SOPDESC_VP";
            this.SOPDESC_VP.ReadOnly = true;
            this.SOPDESC_VP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SOPTYPE_VP
            // 
            this.SOPTYPE_VP.DataPropertyName = "SOPTYPE_VP";
            this.SOPTYPE_VP.HeaderText = "Typ zakázky";
            this.SOPTYPE_VP.Name = "SOPTYPE_VP";
            this.SOPTYPE_VP.ReadOnly = true;
            this.SOPTYPE_VP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // VNDDOCNMH_VP
            // 
            this.VNDDOCNMH_VP.DataPropertyName = "VNDDOCNMH_VP";
            this.VNDDOCNMH_VP.HeaderText = "Objednatel číslo";
            this.VNDDOCNMH_VP.Name = "VNDDOCNMH_VP";
            this.VNDDOCNMH_VP.ReadOnly = true;
            this.VNDDOCNMH_VP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // FormPotrebaMaterialu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 468);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "FormPotrebaMaterialu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Potreba Materiálu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUzivateleList_FormClosing);
            this.Load += new System.EventHandler(this.FormUzivateleList_Load);
            this.Shown += new System.EventHandler(this.FormZboziSelect_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUzivateleList_KeyDown);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Prehled.ResumeLayout(false);
            this.tabPage_Prehled.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PotMat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PotMat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Vyroba)).EndInit();
            this.tabPage_Soucty.ResumeLayout(false);
            this.tabPage_Soucty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PotMat_JenSoucty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PotMat_JenSoucet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelMain;
        protected System.Windows.Forms.MenuStrip menuStrip2;
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
        protected Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar_PotMat;
        protected System.Windows.Forms.Panel panel1;
        public Zuby.ADGV.AdvancedDataGridView dg_PotMat;
        private System.ComponentModel.BackgroundWorker bw_PotMat;
        private System.Windows.Forms.BindingSource bs_PotMat;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Prehled;
        private System.Windows.Forms.TabPage tabPage_Soucty;
        protected Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar2;
        protected ProgressControls.ProgressIndicator progressIndicator2;
        public Zuby.ADGV.AdvancedDataGridView dg_PotMat_JenSoucty;
        private System.Windows.Forms.ToolStripMenuItem aktualizovatToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Fask.Interfaces.DataSets.Vyroba ds_Vyroba;
        private System.Windows.Forms.BindingSource bs_PotMat_JenSoucet;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRMATDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMCODEMATDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNAMEMATDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSHPPDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxValueTypeColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR_MAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMCODE_MAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNAME_MAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYSHPPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY_POHODA;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY_ROZDIL;
        private System.Windows.Forms.DataGridViewTextBoxColumn MJ_MAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR_VYR;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNDITNUM_VYR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC_VYR;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountEntries_VP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPNUMBE_VP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPDESC_VP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPTYPE_VP;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNDDOCNMH_VP;
    }
}