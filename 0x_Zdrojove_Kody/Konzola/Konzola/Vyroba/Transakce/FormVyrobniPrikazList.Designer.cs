namespace Konzola.Vyroba
{
    partial class FormVyrobniPrikazList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVyrobniPrikazList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.progressIndicatorVyrobek_VPH = new ProgressControls.ProgressIndicator();
            this.dgVPH = new Zuby.ADGV.AdvancedDataGridView();
            this.Active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountEntries = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPNUMBE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VNDDOCNMH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarcodeH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCNCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateProd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rez1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rez2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TermID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USERID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LSTMod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEX_ROW_ID_P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsVPH = new System.Windows.Forms.BindingSource(this.components);
            this.ds = new Fask.Interfaces.DataSets.Vyroba();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsl_VPH = new System.Windows.Forms.ToolStripLabel();
            this.advancedDataGridViewSearchToolBarVPH = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panel_Filter_VPH = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_VyrZak = new System.Windows.Forms.TextBox();
            this.tsFiltry = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFiltry_VPH = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNastavit_VPH = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZmena_VPH = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPridat_VPH = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdebrat_VPH = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVycistit_VPH = new System.Windows.Forms.ToolStripButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_Active = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonVyhledatVPH = new System.Windows.Forms.Button();
            this.progressIndicatorVyrobek_VPP = new ProgressControls.ProgressIndicator();
            this.dgVPP = new Zuby.ADGV.AdvancedDataGridView();
            this.bsVPP = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tsl_VPP = new System.Windows.Forms.ToolStripLabel();
            this.advancedDataGridViewSearchToolBarVPP = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panel_Filter_VPP = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFiltry_VPP = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNastavit_VPP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZmena_VPP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPridat_VPP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdebrat_VPP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVycistit_VPP = new System.Windows.Forms.ToolStripButton();
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_OrderBy_VPP = new System.Windows.Forms.ComboBox();
            this.buttonVyhledatVPP = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVystup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTisky = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskEtiketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPotrebaMaterialu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPolozka = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranitPolozku = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravitPolozku = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPridatPolozkuHromadne = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPridatPolozku = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrikaz = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranitPrikaz = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravitPrikaz = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDuplikace = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNovyPrikaz = new System.Windows.Forms.ToolStripMenuItem();
            this.bw_VPH = new System.ComponentModel.BackgroundWorker();
            this.bw_VPP = new System.ComponentModel.BackgroundWorker();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.DEX_ROW_ID_VPP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountEntries_VPP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPNUMBE_VPP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMMJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VNDDOCNMP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VNDITNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarcodeP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarcodeT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCNCODE_VPP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYSHPPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYDOKON = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYPACK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYODVEDENO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNTODVEDENO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYPACKMJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEMODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEPREP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEUNIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DtProdT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DtProdL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerNumT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerNumL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VerT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VerL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TermID_VPP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LSTMod_VPP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_REZ1_Track = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_REZ2_Track = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_REZ3_Track = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_REZ4_Track = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_REZ5_Track = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT_TARA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT_NETTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT_TOL_PLUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT_TOL_MINUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVPH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVPH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.panel_Filter_VPH.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVPP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVPP)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.panel_Filter_VPP.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.menuStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 741);
            this.panel1.TabIndex = 5;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.progressIndicatorVyrobek_VPH);
            this.splitContainer1.Panel1.Controls.Add(this.dgVPH);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            this.splitContainer1.Panel1.Controls.Add(this.advancedDataGridViewSearchToolBarVPH);
            this.splitContainer1.Panel1.Controls.Add(this.panel_Filter_VPH);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.progressIndicatorVyrobek_VPP);
            this.splitContainer1.Panel2.Controls.Add(this.dgVPP);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip3);
            this.splitContainer1.Panel2.Controls.Add(this.advancedDataGridViewSearchToolBarVPP);
            this.splitContainer1.Panel2.Controls.Add(this.panel_Filter_VPP);
            this.splitContainer1.Size = new System.Drawing.Size(921, 717);
            this.splitContainer1.SplitterDistance = 515;
            this.splitContainer1.TabIndex = 3;
            // 
            // progressIndicatorVyrobek_VPH
            // 
            this.progressIndicatorVyrobek_VPH.Location = new System.Drawing.Point(202, 396);
            this.progressIndicatorVyrobek_VPH.Name = "progressIndicatorVyrobek_VPH";
            this.progressIndicatorVyrobek_VPH.Percentage = 0F;
            this.progressIndicatorVyrobek_VPH.Size = new System.Drawing.Size(98, 98);
            this.progressIndicatorVyrobek_VPH.TabIndex = 106;
            this.progressIndicatorVyrobek_VPH.Text = "progressIndicator1";
            this.progressIndicatorVyrobek_VPH.Visible = false;
            // 
            // dgVPH
            // 
            this.dgVPH.AllowUserToAddRows = false;
            this.dgVPH.AllowUserToDeleteRows = false;
            this.dgVPH.AllowUserToOrderColumns = true;
            this.dgVPH.AllowUserToResizeRows = false;
            this.dgVPH.AutoGenerateColumns = false;
            this.dgVPH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVPH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Active,
            this.CountEntries,
            this.SOPNUMBE,
            this.SOPTYPE,
            this.SOPDESC,
            this.VNDDOCNMH,
            this.BarcodeH,
            this.LOCNCODE,
            this.DateProd,
            this.Rez1,
            this.Rez2,
            this.TermID,
            this.USERID,
            this.LSTMod,
            this.DEX_ROW_ID_P});
            this.dgVPH.DataSource = this.bsVPH;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgVPH.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgVPH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVPH.EnableHeadersVisualStyles = false;
            this.dgVPH.FilterAndSortEnabled = true;
            this.dgVPH.Location = new System.Drawing.Point(0, 207);
            this.dgVPH.Name = "dgVPH";
            this.dgVPH.ReadOnly = true;
            this.dgVPH.RowHeadersVisible = false;
            this.dgVPH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVPH.Size = new System.Drawing.Size(515, 485);
            this.dgVPH.TabIndex = 1;
            this.dgVPH.SelectionChanged += new System.EventHandler(this.dgVPH_SelectionChanged);
            // 
            // Active
            // 
            this.Active.DataPropertyName = "Active";
            this.Active.HeaderText = "Aktivní";
            this.Active.Name = "Active";
            this.Active.ReadOnly = true;
            this.Active.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CountEntries
            // 
            this.CountEntries.DataPropertyName = "CountEntries";
            this.CountEntries.HeaderText = "Číslo dávky";
            this.CountEntries.Name = "CountEntries";
            this.CountEntries.ReadOnly = true;
            this.CountEntries.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CountEntries.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // SOPNUMBE
            // 
            this.SOPNUMBE.DataPropertyName = "SOPNUMBE";
            this.SOPNUMBE.HeaderText = "Výrobní Zakázka";
            this.SOPNUMBE.Name = "SOPNUMBE";
            this.SOPNUMBE.ReadOnly = true;
            this.SOPNUMBE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SOPNUMBE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // SOPTYPE
            // 
            this.SOPTYPE.DataPropertyName = "SOPTYPE";
            this.SOPTYPE.HeaderText = "Typ zakázky";
            this.SOPTYPE.Name = "SOPTYPE";
            this.SOPTYPE.ReadOnly = true;
            this.SOPTYPE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SOPTYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // SOPDESC
            // 
            this.SOPDESC.DataPropertyName = "SOPDESC";
            this.SOPDESC.HeaderText = "Popis zakázky";
            this.SOPDESC.Name = "SOPDESC";
            this.SOPDESC.ReadOnly = true;
            this.SOPDESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SOPDESC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // VNDDOCNMH
            // 
            this.VNDDOCNMH.DataPropertyName = "VNDDOCNMH";
            this.VNDDOCNMH.HeaderText = "Objednatel číslo";
            this.VNDDOCNMH.Name = "VNDDOCNMH";
            this.VNDDOCNMH.ReadOnly = true;
            this.VNDDOCNMH.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VNDDOCNMH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // BarcodeH
            // 
            this.BarcodeH.DataPropertyName = "BarcodeH";
            this.BarcodeH.HeaderText = "Č. kód";
            this.BarcodeH.Name = "BarcodeH";
            this.BarcodeH.ReadOnly = true;
            this.BarcodeH.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BarcodeH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // LOCNCODE
            // 
            this.LOCNCODE.DataPropertyName = "LOCNCODE";
            this.LOCNCODE.HeaderText = "Lokace";
            this.LOCNCODE.Name = "LOCNCODE";
            this.LOCNCODE.ReadOnly = true;
            this.LOCNCODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LOCNCODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // DateProd
            // 
            this.DateProd.DataPropertyName = "DateProd";
            this.DateProd.HeaderText = "Oček. směna výroby";
            this.DateProd.Name = "DateProd";
            this.DateProd.ReadOnly = true;
            this.DateProd.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DateProd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Rez1
            // 
            this.Rez1.DataPropertyName = "Rez1";
            this.Rez1.HeaderText = "Rezerva 1";
            this.Rez1.Name = "Rez1";
            this.Rez1.ReadOnly = true;
            this.Rez1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Rez1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Rez2
            // 
            this.Rez2.DataPropertyName = "Rez2";
            this.Rez2.HeaderText = "Rezerva 2";
            this.Rez2.Name = "Rez2";
            this.Rez2.ReadOnly = true;
            this.Rez2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Rez2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // TermID
            // 
            this.TermID.DataPropertyName = "TermID";
            this.TermID.HeaderText = "ID terminál";
            this.TermID.Name = "TermID";
            this.TermID.ReadOnly = true;
            this.TermID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TermID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // USERID
            // 
            this.USERID.DataPropertyName = "USERID";
            this.USERID.HeaderText = "ID uživatele";
            this.USERID.Name = "USERID";
            this.USERID.ReadOnly = true;
            this.USERID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // LSTMod
            // 
            this.LSTMod.DataPropertyName = "LSTMod";
            dataGridViewCellStyle1.Format = "G";
            this.LSTMod.DefaultCellStyle = dataGridViewCellStyle1;
            this.LSTMod.HeaderText = "Poslední úprava";
            this.LSTMod.Name = "LSTMod";
            this.LSTMod.ReadOnly = true;
            this.LSTMod.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LSTMod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.LSTMod.Width = 110;
            // 
            // DEX_ROW_ID_P
            // 
            this.DEX_ROW_ID_P.DataPropertyName = "DEX_ROW_ID";
            this.DEX_ROW_ID_P.HeaderText = "index";
            this.DEX_ROW_ID_P.Name = "DEX_ROW_ID_P";
            this.DEX_ROW_ID_P.ReadOnly = true;
            this.DEX_ROW_ID_P.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DEX_ROW_ID_P.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // bsVPH
            // 
            this.bsVPH.DataMember = "CZPRO_VPH";
            this.bsVPH.DataSource = this.ds;
            // 
            // ds
            // 
            this.ds.DataSetName = "VyrobaDataSet";
            this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsl_VPH});
            this.toolStrip2.Location = new System.Drawing.Point(0, 692);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(515, 25);
            this.toolStrip2.TabIndex = 107;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsl_VPH
            // 
            this.tsl_VPH.Name = "tsl_VPH";
            this.tsl_VPH.Size = new System.Drawing.Size(86, 22);
            this.tsl_VPH.Text = "toolStripLabel3";
            // 
            // advancedDataGridViewSearchToolBarVPH
            // 
            this.advancedDataGridViewSearchToolBarVPH.AllowMerge = false;
            this.advancedDataGridViewSearchToolBarVPH.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBarVPH.Location = new System.Drawing.Point(0, 180);
            this.advancedDataGridViewSearchToolBarVPH.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBarVPH.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBarVPH.Name = "advancedDataGridViewSearchToolBarVPH";
            this.advancedDataGridViewSearchToolBarVPH.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBarVPH.Size = new System.Drawing.Size(515, 27);
            this.advancedDataGridViewSearchToolBarVPH.TabIndex = 2;
            this.advancedDataGridViewSearchToolBarVPH.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBarVPH.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBarVPH_Search);
            // 
            // panel_Filter_VPH
            // 
            this.panel_Filter_VPH.Controls.Add(this.label2);
            this.panel_Filter_VPH.Controls.Add(this.tb_VyrZak);
            this.panel_Filter_VPH.Controls.Add(this.tsFiltry);
            this.panel_Filter_VPH.Controls.Add(this.label4);
            this.panel_Filter_VPH.Controls.Add(this.cb_Active);
            this.panel_Filter_VPH.Controls.Add(this.button1);
            this.panel_Filter_VPH.Controls.Add(this.button2);
            this.panel_Filter_VPH.Controls.Add(this.buttonVyhledatVPH);
            this.panel_Filter_VPH.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Filter_VPH.Location = new System.Drawing.Point(0, 0);
            this.panel_Filter_VPH.Name = "panel_Filter_VPH";
            this.panel_Filter_VPH.Size = new System.Drawing.Size(515, 180);
            this.panel_Filter_VPH.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "Výrobní Zakázka:";
            // 
            // tb_VyrZak
            // 
            this.tb_VyrZak.Location = new System.Drawing.Point(106, 64);
            this.tb_VyrZak.Name = "tb_VyrZak";
            this.tb_VyrZak.Size = new System.Drawing.Size(160, 20);
            this.tb_VyrZak.TabIndex = 61;
            // 
            // tsFiltry
            // 
            this.tsFiltry.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsFiltry.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsFiltry.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbFiltry_VPH,
            this.tsbNastavit_VPH,
            this.toolStripSeparator2,
            this.tsbZmena_VPH,
            this.toolStripSeparator4,
            this.tsbPridat_VPH,
            this.toolStripSeparator3,
            this.tsbOdebrat_VPH,
            this.toolStripSeparator5,
            this.tsbVycistit_VPH});
            this.tsFiltry.Location = new System.Drawing.Point(0, 0);
            this.tsFiltry.Name = "tsFiltry";
            this.tsFiltry.Size = new System.Drawing.Size(515, 27);
            this.tsFiltry.TabIndex = 60;
            this.tsFiltry.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 24);
            this.toolStripLabel1.Text = "Filtry:";
            // 
            // tscbFiltry_VPH
            // 
            this.tscbFiltry_VPH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry_VPH.DropDownWidth = 170;
            this.tscbFiltry_VPH.Name = "tscbFiltry_VPH";
            this.tscbFiltry_VPH.Size = new System.Drawing.Size(170, 27);
            // 
            // tsbNastavit_VPH
            // 
            this.tsbNastavit_VPH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit_VPH.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit_VPH.Image")));
            this.tsbNastavit_VPH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit_VPH.Name = "tsbNastavit_VPH";
            this.tsbNastavit_VPH.Size = new System.Drawing.Size(24, 24);
            this.tsbNastavit_VPH.ToolTipText = "Nastavit";
            this.tsbNastavit_VPH.Click += new System.EventHandler(this.tsbNastavit_VPH_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbZmena_VPH
            // 
            this.tsbZmena_VPH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena_VPH.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena_VPH.Image")));
            this.tsbZmena_VPH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena_VPH.Name = "tsbZmena_VPH";
            this.tsbZmena_VPH.Size = new System.Drawing.Size(24, 24);
            this.tsbZmena_VPH.Text = "Změna";
            this.tsbZmena_VPH.Click += new System.EventHandler(this.tsbZmena_VPH_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbPridat_VPH
            // 
            this.tsbPridat_VPH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat_VPH.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat_VPH.Image")));
            this.tsbPridat_VPH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat_VPH.Name = "tsbPridat_VPH";
            this.tsbPridat_VPH.Size = new System.Drawing.Size(24, 24);
            this.tsbPridat_VPH.Text = "Uložit";
            this.tsbPridat_VPH.Click += new System.EventHandler(this.tsbPridat_VPH_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbOdebrat_VPH
            // 
            this.tsbOdebrat_VPH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat_VPH.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat_VPH.Image")));
            this.tsbOdebrat_VPH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat_VPH.Name = "tsbOdebrat_VPH";
            this.tsbOdebrat_VPH.Size = new System.Drawing.Size(24, 24);
            this.tsbOdebrat_VPH.Text = "Odebrat";
            this.tsbOdebrat_VPH.Click += new System.EventHandler(this.tsbOdebrat_VPH_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbVycistit_VPH
            // 
            this.tsbVycistit_VPH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit_VPH.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit_VPH.Image")));
            this.tsbVycistit_VPH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit_VPH.Name = "tsbVycistit_VPH";
            this.tsbVycistit_VPH.Size = new System.Drawing.Size(24, 24);
            this.tsbVycistit_VPH.Text = "Vyčistit";
            this.tsbVycistit_VPH.Click += new System.EventHandler(this.tsbVycistit_VPH_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Aktivní:";
            // 
            // cb_Active
            // 
            this.cb_Active.FormattingEnabled = true;
            this.cb_Active.Location = new System.Drawing.Point(106, 37);
            this.cb_Active.Name = "cb_Active";
            this.cb_Active.Size = new System.Drawing.Size(160, 21);
            this.cb_Active.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(423, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Odznačit vše";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonOdznacitVse_VPH_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(334, 107);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Označit vše";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonOznacitVse_VPH_Click);
            // 
            // buttonVyhledatVPH
            // 
            this.buttonVyhledatVPH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledatVPH.Location = new System.Drawing.Point(430, 38);
            this.buttonVyhledatVPH.Name = "buttonVyhledatVPH";
            this.buttonVyhledatVPH.Size = new System.Drawing.Size(75, 62);
            this.buttonVyhledatVPH.TabIndex = 0;
            this.buttonVyhledatVPH.Text = "Vyhledat";
            this.buttonVyhledatVPH.UseVisualStyleBackColor = true;
            this.buttonVyhledatVPH.Click += new System.EventHandler(this.buttonVyhledatVPH_Click);
            // 
            // progressIndicatorVyrobek_VPP
            // 
            this.progressIndicatorVyrobek_VPP.Location = new System.Drawing.Point(180, 396);
            this.progressIndicatorVyrobek_VPP.Name = "progressIndicatorVyrobek_VPP";
            this.progressIndicatorVyrobek_VPP.Percentage = 0F;
            this.progressIndicatorVyrobek_VPP.Size = new System.Drawing.Size(98, 98);
            this.progressIndicatorVyrobek_VPP.TabIndex = 106;
            this.progressIndicatorVyrobek_VPP.Text = "progressIndicator1";
            this.progressIndicatorVyrobek_VPP.Visible = false;
            // 
            // dgVPP
            // 
            this.dgVPP.AllowUserToAddRows = false;
            this.dgVPP.AllowUserToDeleteRows = false;
            this.dgVPP.AllowUserToOrderColumns = true;
            this.dgVPP.AllowUserToResizeRows = false;
            this.dgVPP.AutoGenerateColumns = false;
            this.dgVPP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVPP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DEX_ROW_ID_VPP,
            this.CountEntries_VPP,
            this.SOPNUMBE_VPP,
            this.ITEMNMBR,
            this.ITEMCODE,
            this.ITEMTYPE,
            this.ITEMDESC,
            this.ITEMMJ,
            this.Column1,
            this.VNDDOCNMP,
            this.VNDITNUM,
            this.ORD,
            this.BarcodeP,
            this.BarcodeT,
            this.LOCNCODE_VPP,
            this.QTYSHPPD,
            this.QTYDOKON,
            this.QTYPACK,
            this.QTYODVEDENO,
            this.CNTODVEDENO,
            this.QTYPACKMJ,
            this.TIMEMODE,
            this.TIMEPREP,
            this.TIMEUNIT,
            this.DtProdT,
            this.DtProdL,
            this.SerNumT,
            this.SerNumL,
            this.VerT,
            this.VerL,
            this.TermID_VPP,
            this.LSTMod_VPP,
            this.CZ_REZ1_Track,
            this.CZ_REZ2_Track,
            this.CZ_REZ3_Track,
            this.CZ_REZ4_Track,
            this.CZ_REZ5_Track,
            this.WEIGHT_TARA,
            this.WEIGHT_NETTO,
            this.WEIGHT_TOL_PLUS,
            this.WEIGHT_TOL_MINUS});
            this.dgVPP.DataSource = this.bsVPP;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgVPP.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgVPP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVPP.EnableHeadersVisualStyles = false;
            this.dgVPP.FilterAndSortEnabled = true;
            this.dgVPP.Location = new System.Drawing.Point(0, 207);
            this.dgVPP.MultiSelect = false;
            this.dgVPP.Name = "dgVPP";
            this.dgVPP.ReadOnly = true;
            this.dgVPP.RowHeadersVisible = false;
            this.dgVPP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVPP.Size = new System.Drawing.Size(402, 485);
            this.dgVPP.TabIndex = 0;
            this.dgVPP.SelectionChanged += new System.EventHandler(this.dgVPP_SelectionChanged);
            // 
            // bsVPP
            // 
            this.bsVPP.DataMember = "CZPRO_VPP";
            this.bsVPP.DataSource = this.ds;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsl_VPP});
            this.toolStrip3.Location = new System.Drawing.Point(0, 692);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(402, 25);
            this.toolStrip3.TabIndex = 108;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // tsl_VPP
            // 
            this.tsl_VPP.Name = "tsl_VPP";
            this.tsl_VPP.Size = new System.Drawing.Size(86, 22);
            this.tsl_VPP.Text = "toolStripLabel3";
            // 
            // advancedDataGridViewSearchToolBarVPP
            // 
            this.advancedDataGridViewSearchToolBarVPP.AllowMerge = false;
            this.advancedDataGridViewSearchToolBarVPP.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBarVPP.Location = new System.Drawing.Point(0, 180);
            this.advancedDataGridViewSearchToolBarVPP.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBarVPP.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBarVPP.Name = "advancedDataGridViewSearchToolBarVPP";
            this.advancedDataGridViewSearchToolBarVPP.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBarVPP.Size = new System.Drawing.Size(402, 27);
            this.advancedDataGridViewSearchToolBarVPP.TabIndex = 1;
            this.advancedDataGridViewSearchToolBarVPP.Text = "advancedDataGridViewSearchToolBar2";
            this.advancedDataGridViewSearchToolBarVPP.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBarVPP_Search);
            // 
            // panel_Filter_VPP
            // 
            this.panel_Filter_VPP.Controls.Add(this.toolStrip1);
            this.panel_Filter_VPP.Controls.Add(this.buttonOdznacitVse);
            this.panel_Filter_VPP.Controls.Add(this.label1);
            this.panel_Filter_VPP.Controls.Add(this.cb_OrderBy_VPP);
            this.panel_Filter_VPP.Controls.Add(this.buttonVyhledatVPP);
            this.panel_Filter_VPP.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Filter_VPP.Location = new System.Drawing.Point(0, 0);
            this.panel_Filter_VPP.Name = "panel_Filter_VPP";
            this.panel_Filter_VPP.Size = new System.Drawing.Size(402, 180);
            this.panel_Filter_VPP.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tscbFiltry_VPP,
            this.tsbNastavit_VPP,
            this.toolStripSeparator1,
            this.tsbZmena_VPP,
            this.toolStripSeparator6,
            this.tsbPridat_VPP,
            this.toolStripSeparator7,
            this.tsbOdebrat_VPP,
            this.toolStripSeparator8,
            this.tsbVycistit_VPP});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(402, 27);
            this.toolStrip1.TabIndex = 60;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(36, 24);
            this.toolStripLabel2.Text = "Filtry:";
            // 
            // tscbFiltry_VPP
            // 
            this.tscbFiltry_VPP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry_VPP.DropDownWidth = 170;
            this.tscbFiltry_VPP.Name = "tscbFiltry_VPP";
            this.tscbFiltry_VPP.Size = new System.Drawing.Size(170, 27);
            // 
            // tsbNastavit_VPP
            // 
            this.tsbNastavit_VPP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit_VPP.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit_VPP.Image")));
            this.tsbNastavit_VPP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit_VPP.Name = "tsbNastavit_VPP";
            this.tsbNastavit_VPP.Size = new System.Drawing.Size(24, 24);
            this.tsbNastavit_VPP.ToolTipText = "Nastavit";
            this.tsbNastavit_VPP.Click += new System.EventHandler(this.tsbNastavit_VPP_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbZmena_VPP
            // 
            this.tsbZmena_VPP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena_VPP.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena_VPP.Image")));
            this.tsbZmena_VPP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena_VPP.Name = "tsbZmena_VPP";
            this.tsbZmena_VPP.Size = new System.Drawing.Size(24, 24);
            this.tsbZmena_VPP.Text = "Změna";
            this.tsbZmena_VPP.Click += new System.EventHandler(this.tsbZmena_VPP_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbPridat_VPP
            // 
            this.tsbPridat_VPP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat_VPP.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat_VPP.Image")));
            this.tsbPridat_VPP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat_VPP.Name = "tsbPridat_VPP";
            this.tsbPridat_VPP.Size = new System.Drawing.Size(24, 24);
            this.tsbPridat_VPP.Text = "Uložit";
            this.tsbPridat_VPP.Click += new System.EventHandler(this.tsbPridat_VPP_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbOdebrat_VPP
            // 
            this.tsbOdebrat_VPP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat_VPP.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat_VPP.Image")));
            this.tsbOdebrat_VPP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat_VPP.Name = "tsbOdebrat_VPP";
            this.tsbOdebrat_VPP.Size = new System.Drawing.Size(24, 24);
            this.tsbOdebrat_VPP.Text = "Odebrat";
            this.tsbOdebrat_VPP.Click += new System.EventHandler(this.tsbOdebrat_VPP_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbVycistit_VPP
            // 
            this.tsbVycistit_VPP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit_VPP.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit_VPP.Image")));
            this.tsbVycistit_VPP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit_VPP.Name = "tsbVycistit_VPP";
            this.tsbVycistit_VPP.Size = new System.Drawing.Size(24, 24);
            this.tsbVycistit_VPP.Text = "Vyčistit";
            this.tsbVycistit_VPP.Click += new System.EventHandler(this.tsbVycistit_VPP_Click);
            // 
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdznacitVse.Location = new System.Drawing.Point(314, 107);
            this.buttonOdznacitVse.Name = "buttonOdznacitVse";
            this.buttonOdznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOdznacitVse.TabIndex = 15;
            this.buttonOdznacitVse.Text = "Odznačit";
            this.buttonOdznacitVse.UseVisualStyleBackColor = true;
            this.buttonOdznacitVse.Click += new System.EventHandler(this.buttonOdznacitVse_VPP_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Order by :";
            // 
            // cb_OrderBy_VPP
            // 
            this.cb_OrderBy_VPP.FormattingEnabled = true;
            this.cb_OrderBy_VPP.Items.AddRange(new object[] {
            "vzestupně (asc)",
            "sestupně (desc)"});
            this.cb_OrderBy_VPP.Location = new System.Drawing.Point(70, 51);
            this.cb_OrderBy_VPP.Name = "cb_OrderBy_VPP";
            this.cb_OrderBy_VPP.Size = new System.Drawing.Size(99, 21);
            this.cb_OrderBy_VPP.TabIndex = 2;
            // 
            // buttonVyhledatVPP
            // 
            this.buttonVyhledatVPP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledatVPP.Location = new System.Drawing.Point(322, 38);
            this.buttonVyhledatVPP.Name = "buttonVyhledatVPP";
            this.buttonVyhledatVPP.Size = new System.Drawing.Size(75, 62);
            this.buttonVyhledatVPP.TabIndex = 1;
            this.buttonVyhledatVPP.Text = "Vyhledat";
            this.buttonVyhledatVPP.UseVisualStyleBackColor = true;
            this.buttonVyhledatVPP.Click += new System.EventHandler(this.buttonVyhledatVPP_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiVystup,
            this.tsmiAkce,
            this.tsmiPolozka,
            this.tsmiPrikaz});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(921, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
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
            this.tsmiKonec.Click += new System.EventHandler(this.tsmi_konec_Click);
            // 
            // tsmiVystup
            // 
            this.tsmiVystup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTisky,
            this.tiskEtiketToolStripMenuItem});
            this.tsmiVystup.Name = "tsmiVystup";
            this.tsmiVystup.Size = new System.Drawing.Size(55, 20);
            this.tsmiVystup.Text = "Výstup";
            // 
            // tsmiTisky
            // 
            this.tsmiTisky.Name = "tsmiTisky";
            this.tsmiTisky.ShortcutKeyDisplayString = "Ctrl+P, Ctrl+R";
            this.tsmiTisky.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tsmiTisky.Size = new System.Drawing.Size(205, 22);
            this.tsmiTisky.Text = "Tisk";
            this.tsmiTisky.Click += new System.EventHandler(this.tiskToolStripMenuItem_Click);
            // 
            // tiskEtiketToolStripMenuItem
            // 
            this.tiskEtiketToolStripMenuItem.Name = "tiskEtiketToolStripMenuItem";
            this.tiskEtiketToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+E, Ctrl+T";
            this.tiskEtiketToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tiskEtiketToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.tiskEtiketToolStripMenuItem.Text = "Tisk etiket";
            this.tiskEtiketToolStripMenuItem.Click += new System.EventHandler(this.tiskEtiketToolStripMenuItem_Click);
            // 
            // tsmiAkce
            // 
            this.tsmiAkce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPotrebaMaterialu});
            this.tsmiAkce.Name = "tsmiAkce";
            this.tsmiAkce.Size = new System.Drawing.Size(45, 20);
            this.tsmiAkce.Text = "Akce";
            // 
            // tsmiPotrebaMaterialu
            // 
            this.tsmiPotrebaMaterialu.Name = "tsmiPotrebaMaterialu";
            this.tsmiPotrebaMaterialu.Size = new System.Drawing.Size(165, 22);
            this.tsmiPotrebaMaterialu.Text = "Potřeba materálu";
            this.tsmiPotrebaMaterialu.Click += new System.EventHandler(this.tsmiPotrebaMaterialu_Click);
            // 
            // tsmiPolozka
            // 
            this.tsmiPolozka.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOdstranitPolozku,
            this.tsmiUpravitPolozku,
            this.tsmiPridatPolozkuHromadne,
            this.tsmiPridatPolozku});
            this.tsmiPolozka.Name = "tsmiPolozka";
            this.tsmiPolozka.Size = new System.Drawing.Size(60, 20);
            this.tsmiPolozka.Text = "Položka";
            // 
            // tsmiOdstranitPolozku
            // 
            this.tsmiOdstranitPolozku.Name = "tsmiOdstranitPolozku";
            this.tsmiOdstranitPolozku.Size = new System.Drawing.Size(207, 22);
            this.tsmiOdstranitPolozku.Text = "Odstranit položku";
            this.tsmiOdstranitPolozku.Click += new System.EventHandler(this.tsmiOdstranitPolozku_Click);
            // 
            // tsmiUpravitPolozku
            // 
            this.tsmiUpravitPolozku.Name = "tsmiUpravitPolozku";
            this.tsmiUpravitPolozku.Size = new System.Drawing.Size(207, 22);
            this.tsmiUpravitPolozku.Text = "Upravit položku";
            this.tsmiUpravitPolozku.Click += new System.EventHandler(this.tsmiUpravitPolozku_Click);
            // 
            // tsmiPridatPolozkuHromadne
            // 
            this.tsmiPridatPolozkuHromadne.Enabled = false;
            this.tsmiPridatPolozkuHromadne.Name = "tsmiPridatPolozkuHromadne";
            this.tsmiPridatPolozkuHromadne.Size = new System.Drawing.Size(207, 22);
            this.tsmiPridatPolozkuHromadne.Text = "Přidat hromadně položky";
            this.tsmiPridatPolozkuHromadne.Click += new System.EventHandler(this.tsmiPridatPolozkuHromadne_Click);
            // 
            // tsmiPridatPolozku
            // 
            this.tsmiPridatPolozku.Name = "tsmiPridatPolozku";
            this.tsmiPridatPolozku.Size = new System.Drawing.Size(207, 22);
            this.tsmiPridatPolozku.Text = "Přidat položku";
            this.tsmiPridatPolozku.Click += new System.EventHandler(this.tsmiPridatPolozku_Click);
            // 
            // tsmiPrikaz
            // 
            this.tsmiPrikaz.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOdstranitPrikaz,
            this.tsmiUpravitPrikaz,
            this.tsmiDuplikace,
            this.tsmiNovyPrikaz});
            this.tsmiPrikaz.Name = "tsmiPrikaz";
            this.tsmiPrikaz.Size = new System.Drawing.Size(50, 20);
            this.tsmiPrikaz.Text = "Příkaz";
            // 
            // tsmiOdstranitPrikaz
            // 
            this.tsmiOdstranitPrikaz.Name = "tsmiOdstranitPrikaz";
            this.tsmiOdstranitPrikaz.Size = new System.Drawing.Size(167, 22);
            this.tsmiOdstranitPrikaz.Text = "Odstranit příkaz";
            this.tsmiOdstranitPrikaz.Click += new System.EventHandler(this.tsmiOdstranitPrikaz_Click);
            // 
            // tsmiUpravitPrikaz
            // 
            this.tsmiUpravitPrikaz.Name = "tsmiUpravitPrikaz";
            this.tsmiUpravitPrikaz.Size = new System.Drawing.Size(167, 22);
            this.tsmiUpravitPrikaz.Text = "Upravit příkaz";
            this.tsmiUpravitPrikaz.Click += new System.EventHandler(this.tsmiUpravitPrikaz_Click);
            // 
            // tsmiDuplikace
            // 
            this.tsmiDuplikace.Name = "tsmiDuplikace";
            this.tsmiDuplikace.Size = new System.Drawing.Size(167, 22);
            this.tsmiDuplikace.Text = "Duplikace přikazu";
            this.tsmiDuplikace.Click += new System.EventHandler(this.tsmiDuplikace_Click);
            // 
            // tsmiNovyPrikaz
            // 
            this.tsmiNovyPrikaz.Name = "tsmiNovyPrikaz";
            this.tsmiNovyPrikaz.Size = new System.Drawing.Size(167, 22);
            this.tsmiNovyPrikaz.Text = "Nový příkaz";
            this.tsmiNovyPrikaz.Click += new System.EventHandler(this.tsmiNovyPrikaz_Click);
            // 
            // bw_VPH
            // 
            this.bw_VPH.WorkerSupportsCancellation = true;
            this.bw_VPH.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_VPH_DoWork);
            this.bw_VPH.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_VPH_RunWorkerCompleted);
            // 
            // bw_VPP
            // 
            this.bw_VPP.WorkerSupportsCancellation = true;
            this.bw_VPP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_VPP_DoWork);
            this.bw_VPP.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_VPP_RunWorkerCompleted);
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(921, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(100, 741);
            this.panelButtons.TabIndex = 4;
            // 
            // DEX_ROW_ID_VPP
            // 
            this.DEX_ROW_ID_VPP.DataPropertyName = "DEX_ROW_ID";
            this.DEX_ROW_ID_VPP.HeaderText = "Index";
            this.DEX_ROW_ID_VPP.Name = "DEX_ROW_ID_VPP";
            this.DEX_ROW_ID_VPP.ReadOnly = true;
            this.DEX_ROW_ID_VPP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DEX_ROW_ID_VPP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // CountEntries_VPP
            // 
            this.CountEntries_VPP.DataPropertyName = "CountEntries";
            this.CountEntries_VPP.HeaderText = "Pol. číslo";
            this.CountEntries_VPP.Name = "CountEntries_VPP";
            this.CountEntries_VPP.ReadOnly = true;
            this.CountEntries_VPP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CountEntries_VPP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // SOPNUMBE_VPP
            // 
            this.SOPNUMBE_VPP.DataPropertyName = "SOPNUMBE";
            this.SOPNUMBE_VPP.HeaderText = "Výrobní Zakázka";
            this.SOPNUMBE_VPP.Name = "SOPNUMBE_VPP";
            this.SOPNUMBE_VPP.ReadOnly = true;
            this.SOPNUMBE_VPP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SOPNUMBE_VPP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // ITEMNMBR
            // 
            this.ITEMNMBR.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR.HeaderText = "Pol. zboží číslo";
            this.ITEMNMBR.Name = "ITEMNMBR";
            this.ITEMNMBR.ReadOnly = true;
            this.ITEMNMBR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ITEMNMBR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // ITEMCODE
            // 
            this.ITEMCODE.DataPropertyName = "ITEMCODE";
            this.ITEMCODE.HeaderText = "Kód položky";
            this.ITEMCODE.Name = "ITEMCODE";
            this.ITEMCODE.ReadOnly = true;
            // 
            // ITEMTYPE
            // 
            this.ITEMTYPE.DataPropertyName = "ITEMTYPE";
            this.ITEMTYPE.HeaderText = "Typ položky";
            this.ITEMTYPE.Name = "ITEMTYPE";
            this.ITEMTYPE.ReadOnly = true;
            this.ITEMTYPE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ITEMTYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // ITEMDESC
            // 
            this.ITEMDESC.DataPropertyName = "ITEMDESC";
            this.ITEMDESC.HeaderText = "Popis položky";
            this.ITEMDESC.Name = "ITEMDESC";
            this.ITEMDESC.ReadOnly = true;
            this.ITEMDESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ITEMDESC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // ITEMMJ
            // 
            this.ITEMMJ.DataPropertyName = "ITEMMJ";
            this.ITEMMJ.HeaderText = "Měrná jednotka";
            this.ITEMMJ.Name = "ITEMMJ";
            this.ITEMMJ.ReadOnly = true;
            this.ITEMMJ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ITEMMJ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "QTYPACKMJ";
            this.Column1.HeaderText = "Měrná jednotka bal.";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // VNDDOCNMP
            // 
            this.VNDDOCNMP.DataPropertyName = "VNDDOCNMP";
            this.VNDDOCNMP.HeaderText = "Číslo dok. externí";
            this.VNDDOCNMP.Name = "VNDDOCNMP";
            this.VNDDOCNMP.ReadOnly = true;
            this.VNDDOCNMP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VNDDOCNMP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // VNDITNUM
            // 
            this.VNDITNUM.DataPropertyName = "VNDITNUM";
            this.VNDITNUM.HeaderText = "Číslo položky externí";
            this.VNDITNUM.Name = "VNDITNUM";
            this.VNDITNUM.ReadOnly = true;
            this.VNDITNUM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VNDITNUM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // ORD
            // 
            this.ORD.DataPropertyName = "ORD";
            this.ORD.HeaderText = "Pořadí položky";
            this.ORD.Name = "ORD";
            this.ORD.ReadOnly = true;
            this.ORD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ORD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // BarcodeP
            // 
            this.BarcodeP.DataPropertyName = "BarcodeP";
            this.BarcodeP.HeaderText = "Č. kód";
            this.BarcodeP.Name = "BarcodeP";
            this.BarcodeP.ReadOnly = true;
            this.BarcodeP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BarcodeP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // BarcodeT
            // 
            this.BarcodeT.DataPropertyName = "BarcodeT";
            this.BarcodeT.HeaderText = "Č. kód Track";
            this.BarcodeT.Name = "BarcodeT";
            this.BarcodeT.ReadOnly = true;
            this.BarcodeT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BarcodeT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // LOCNCODE_VPP
            // 
            this.LOCNCODE_VPP.DataPropertyName = "LOCNCODE";
            this.LOCNCODE_VPP.HeaderText = "Lokace";
            this.LOCNCODE_VPP.Name = "LOCNCODE_VPP";
            this.LOCNCODE_VPP.ReadOnly = true;
            this.LOCNCODE_VPP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LOCNCODE_VPP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // QTYSHPPD
            // 
            this.QTYSHPPD.DataPropertyName = "QTYSHPPD";
            this.QTYSHPPD.HeaderText = "Množství";
            this.QTYSHPPD.Name = "QTYSHPPD";
            this.QTYSHPPD.ReadOnly = true;
            this.QTYSHPPD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.QTYSHPPD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // QTYDOKON
            // 
            this.QTYDOKON.DataPropertyName = "QTYDOKON";
            this.QTYDOKON.HeaderText = "Množství dok. ruč. odvodem";
            this.QTYDOKON.Name = "QTYDOKON";
            this.QTYDOKON.ReadOnly = true;
            this.QTYDOKON.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.QTYDOKON.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // QTYPACK
            // 
            this.QTYPACK.DataPropertyName = "QTYPACK";
            this.QTYPACK.HeaderText = "Množství v balení";
            this.QTYPACK.Name = "QTYPACK";
            this.QTYPACK.ReadOnly = true;
            this.QTYPACK.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.QTYPACK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // QTYODVEDENO
            // 
            this.QTYODVEDENO.DataPropertyName = "QTYODVEDENO";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.QTYODVEDENO.DefaultCellStyle = dataGridViewCellStyle3;
            this.QTYODVEDENO.HeaderText = "Množství odvedeno";
            this.QTYODVEDENO.Name = "QTYODVEDENO";
            this.QTYODVEDENO.ReadOnly = true;
            this.QTYODVEDENO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.QTYODVEDENO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // CNTODVEDENO
            // 
            this.CNTODVEDENO.DataPropertyName = "CNTODVEDENO";
            this.CNTODVEDENO.HeaderText = "Počet záznamů";
            this.CNTODVEDENO.Name = "CNTODVEDENO";
            this.CNTODVEDENO.ReadOnly = true;
            this.CNTODVEDENO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CNTODVEDENO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // QTYPACKMJ
            // 
            this.QTYPACKMJ.DataPropertyName = "QTYPACKMJ";
            this.QTYPACKMJ.HeaderText = "Měrná jednotka balení";
            this.QTYPACKMJ.Name = "QTYPACKMJ";
            this.QTYPACKMJ.ReadOnly = true;
            this.QTYPACKMJ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.QTYPACKMJ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // TIMEMODE
            // 
            this.TIMEMODE.DataPropertyName = "TIMEMODE";
            this.TIMEMODE.HeaderText = "Typ sledování";
            this.TIMEMODE.Name = "TIMEMODE";
            this.TIMEMODE.ReadOnly = true;
            this.TIMEMODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TIMEMODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // TIMEPREP
            // 
            this.TIMEPREP.DataPropertyName = "TIMEPREP";
            this.TIMEPREP.HeaderText = "Přípravný čas";
            this.TIMEPREP.Name = "TIMEPREP";
            this.TIMEPREP.ReadOnly = true;
            this.TIMEPREP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TIMEPREP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // TIMEUNIT
            // 
            this.TIMEUNIT.DataPropertyName = "TIMEUNIT";
            this.TIMEUNIT.HeaderText = "Jednotkový čas";
            this.TIMEUNIT.Name = "TIMEUNIT";
            this.TIMEUNIT.ReadOnly = true;
            this.TIMEUNIT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TIMEUNIT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // DtProdT
            // 
            this.DtProdT.DataPropertyName = "DtProdT";
            this.DtProdT.HeaderText = "Sledovat dat. výroby";
            this.DtProdT.Name = "DtProdT";
            this.DtProdT.ReadOnly = true;
            this.DtProdT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DtProdT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.DtProdT.Visible = false;
            // 
            // DtProdL
            // 
            this.DtProdL.DataPropertyName = "DtProdL";
            this.DtProdL.HeaderText = "Požad. rozs. pole dat. výroby";
            this.DtProdL.Name = "DtProdL";
            this.DtProdL.ReadOnly = true;
            this.DtProdL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DtProdL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.DtProdL.Visible = false;
            // 
            // SerNumT
            // 
            this.SerNumT.DataPropertyName = "SerNumT";
            this.SerNumT.HeaderText = "Sledovat SN";
            this.SerNumT.Name = "SerNumT";
            this.SerNumT.ReadOnly = true;
            this.SerNumT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SerNumT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.SerNumT.Visible = false;
            // 
            // SerNumL
            // 
            this.SerNumL.DataPropertyName = "SerNumL";
            this.SerNumL.HeaderText = "Požad. rozs. pole SN";
            this.SerNumL.Name = "SerNumL";
            this.SerNumL.ReadOnly = true;
            this.SerNumL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SerNumL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.SerNumL.Visible = false;
            // 
            // VerT
            // 
            this.VerT.DataPropertyName = "VerT";
            this.VerT.HeaderText = "Sledovat verzi";
            this.VerT.Name = "VerT";
            this.VerT.ReadOnly = true;
            this.VerT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VerT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.VerT.Visible = false;
            // 
            // VerL
            // 
            this.VerL.DataPropertyName = "VerL";
            this.VerL.HeaderText = "Požad. rozs. pole verze";
            this.VerL.Name = "VerL";
            this.VerL.ReadOnly = true;
            this.VerL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VerL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.VerL.Visible = false;
            // 
            // TermID_VPP
            // 
            this.TermID_VPP.DataPropertyName = "TermID";
            this.TermID_VPP.HeaderText = "ID terminál";
            this.TermID_VPP.Name = "TermID_VPP";
            this.TermID_VPP.ReadOnly = true;
            this.TermID_VPP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TermID_VPP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // LSTMod_VPP
            // 
            this.LSTMod_VPP.DataPropertyName = "LSTMod";
            dataGridViewCellStyle4.Format = "G";
            this.LSTMod_VPP.DefaultCellStyle = dataGridViewCellStyle4;
            this.LSTMod_VPP.HeaderText = "Poslední úprava";
            this.LSTMod_VPP.Name = "LSTMod_VPP";
            this.LSTMod_VPP.ReadOnly = true;
            this.LSTMod_VPP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LSTMod_VPP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.LSTMod_VPP.Width = 110;
            // 
            // CZ_REZ1_Track
            // 
            this.CZ_REZ1_Track.DataPropertyName = "CZ_REZ1_Track";
            this.CZ_REZ1_Track.HeaderText = "REZ1 Příznak";
            this.CZ_REZ1_Track.Name = "CZ_REZ1_Track";
            this.CZ_REZ1_Track.ReadOnly = true;
            // 
            // CZ_REZ2_Track
            // 
            this.CZ_REZ2_Track.DataPropertyName = "CZ_REZ2_Track";
            this.CZ_REZ2_Track.HeaderText = "REZ2 Příznak";
            this.CZ_REZ2_Track.Name = "CZ_REZ2_Track";
            this.CZ_REZ2_Track.ReadOnly = true;
            // 
            // CZ_REZ3_Track
            // 
            this.CZ_REZ3_Track.DataPropertyName = "CZ_REZ3_Track";
            this.CZ_REZ3_Track.HeaderText = "REZ3 Příznak";
            this.CZ_REZ3_Track.Name = "CZ_REZ3_Track";
            this.CZ_REZ3_Track.ReadOnly = true;
            // 
            // CZ_REZ4_Track
            // 
            this.CZ_REZ4_Track.DataPropertyName = "CZ_REZ4_Track";
            this.CZ_REZ4_Track.HeaderText = "REZ4 Příznak";
            this.CZ_REZ4_Track.Name = "CZ_REZ4_Track";
            this.CZ_REZ4_Track.ReadOnly = true;
            // 
            // CZ_REZ5_Track
            // 
            this.CZ_REZ5_Track.DataPropertyName = "CZ_REZ5_Track";
            this.CZ_REZ5_Track.HeaderText = "REZ5 Příznak";
            this.CZ_REZ5_Track.Name = "CZ_REZ5_Track";
            this.CZ_REZ5_Track.ReadOnly = true;
            // 
            // WEIGHT_TARA
            // 
            this.WEIGHT_TARA.DataPropertyName = "WEIGHT_TARA";
            this.WEIGHT_TARA.HeaderText = "Váha obalu";
            this.WEIGHT_TARA.Name = "WEIGHT_TARA";
            this.WEIGHT_TARA.ReadOnly = true;
            // 
            // WEIGHT_NETTO
            // 
            this.WEIGHT_NETTO.DataPropertyName = "WEIGHT_NETTO";
            this.WEIGHT_NETTO.HeaderText = "Váha materialu";
            this.WEIGHT_NETTO.Name = "WEIGHT_NETTO";
            this.WEIGHT_NETTO.ReadOnly = true;
            // 
            // WEIGHT_TOL_PLUS
            // 
            this.WEIGHT_TOL_PLUS.DataPropertyName = "WEIGHT_TOL_PLUS";
            this.WEIGHT_TOL_PLUS.HeaderText = "Váha tolerance plus";
            this.WEIGHT_TOL_PLUS.Name = "WEIGHT_TOL_PLUS";
            this.WEIGHT_TOL_PLUS.ReadOnly = true;
            // 
            // WEIGHT_TOL_MINUS
            // 
            this.WEIGHT_TOL_MINUS.DataPropertyName = "WEIGHT_TOL_MINUS";
            this.WEIGHT_TOL_MINUS.HeaderText = "Váha tolerance minus";
            this.WEIGHT_TOL_MINUS.Name = "WEIGHT_TOL_MINUS";
            this.WEIGHT_TOL_MINUS.ReadOnly = true;
            // 
            // FormVyrobniPrikazList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 741);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormVyrobniPrikazList";
            this.ShowIcon = false;
            this.Text = "Výrobní příkazy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVyrobniPrikazList_FormClosing);
            this.Load += new System.EventHandler(this.FormVyrobniPrikazList_Load);
            this.Shown += new System.EventHandler(this.FormVyrobniPrikazList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormVyrobniPrikazList_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgVPH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVPH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel_Filter_VPH.ResumeLayout(false);
            this.panel_Filter_VPH.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVPP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVPP)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panel_Filter_VPP.ResumeLayout(false);
            this.panel_Filter_VPP.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgVPH;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private Fask.Interfaces.DataSets.Vyroba ds;
        private System.Windows.Forms.BindingSource bsVPH;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Zuby.ADGV.AdvancedDataGridView dgVPP;
        private System.Windows.Forms.BindingSource bsVPP;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.ToolStripMenuItem tsmiVystup;
        private System.Windows.Forms.ToolStripMenuItem tsmiTisky;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrikaz;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranitPrikaz;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravitPrikaz;
        private System.Windows.Forms.ToolStripMenuItem tsmiNovyPrikaz;
        private System.Windows.Forms.ToolStripMenuItem tsmiPolozka;
        private System.Windows.Forms.ToolStripMenuItem tsmiPridatPolozku;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravitPolozku;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranitPolozku;
        private System.Windows.Forms.ToolStripMenuItem tsmiPridatPolozkuHromadne;
        private System.Windows.Forms.ToolStripMenuItem tsmiDuplikace;
        private System.Windows.Forms.ToolStripMenuItem tsmiAkce;
        private System.Windows.Forms.ToolStripMenuItem tsmiPotrebaMaterialu;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBarVPH;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBarVPP;
        private System.Windows.Forms.Panel panel_Filter_VPH;
        private System.Windows.Forms.Panel panel_Filter_VPP;
        private System.Windows.Forms.Button buttonVyhledatVPH;
        private System.Windows.Forms.Button buttonVyhledatVPP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_OrderBy_VPP;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_Active;
        protected System.Windows.Forms.ToolStrip tsFiltry;
        protected System.Windows.Forms.ToolStripLabel toolStripLabel1;
        protected System.Windows.Forms.ToolStripComboBox tscbFiltry_VPH;
        protected System.Windows.Forms.ToolStripButton tsbNastavit_VPH;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        protected System.Windows.Forms.ToolStripButton tsbZmena_VPH;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        protected System.Windows.Forms.ToolStripButton tsbPridat_VPH;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        protected System.Windows.Forms.ToolStripButton tsbOdebrat_VPH;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        protected System.Windows.Forms.ToolStripButton tsbVycistit_VPH;
        protected System.Windows.Forms.ToolStrip toolStrip1;
        protected System.Windows.Forms.ToolStripLabel toolStripLabel2;
        protected System.Windows.Forms.ToolStripComboBox tscbFiltry_VPP;
        protected System.Windows.Forms.ToolStripButton tsbNastavit_VPP;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripButton tsbZmena_VPP;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        protected System.Windows.Forms.ToolStripButton tsbPridat_VPP;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        protected System.Windows.Forms.ToolStripButton tsbOdebrat_VPP;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        protected System.Windows.Forms.ToolStripButton tsbVycistit_VPP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_VyrZak;
        private System.ComponentModel.BackgroundWorker bw_VPH;
        private System.ComponentModel.BackgroundWorker bw_VPP;
        private ProgressControls.ProgressIndicator progressIndicatorVyrobek_VPH;
        private ProgressControls.ProgressIndicator progressIndicatorVyrobek_VPP;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel tsl_VPH;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel tsl_VPP;
        private System.Windows.Forms.ToolStripMenuItem tiskEtiketToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Active;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountEntries;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPNUMBE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNDDOCNMH;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarcodeH;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCNCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateProd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rez1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rez2;
        private System.Windows.Forms.DataGridViewTextBoxColumn TermID;
        private System.Windows.Forms.DataGridViewTextBoxColumn USERID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LSTMod;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEX_ROW_ID_P;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEX_ROW_ID_VPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountEntries_VPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPNUMBE_VPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMMJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNDDOCNMP;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNDITNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORD;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarcodeP;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarcodeT;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCNCODE_VPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYSHPPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYDOKON;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYPACK;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYODVEDENO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNTODVEDENO;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYPACKMJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEMODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEPREP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEUNIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DtProdT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DtProdL;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerNumT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerNumL;
        private System.Windows.Forms.DataGridViewTextBoxColumn VerT;
        private System.Windows.Forms.DataGridViewTextBoxColumn VerL;
        private System.Windows.Forms.DataGridViewTextBoxColumn TermID_VPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn LSTMod_VPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_REZ1_Track;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_REZ2_Track;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_REZ3_Track;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_REZ4_Track;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_REZ5_Track;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT_TARA;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT_NETTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT_TOL_PLUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT_TOL_MINUS;
    }
}