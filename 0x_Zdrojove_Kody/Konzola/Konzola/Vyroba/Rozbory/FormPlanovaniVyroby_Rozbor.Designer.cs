namespace Konzola.Vyroba
{
    partial class FormPlanovaniVyroby_Rozbor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dg_PVH = new Zuby.ADGV.AdvancedDataGridView();
            this.sOPNUMBEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPTYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodeHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEXROWIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_PVH = new System.Windows.Forms.BindingSource(this.components);
            this.ds = new Fask.Interfaces.DataSets.Vyroba_Planovani();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.advancedDataGridViewSearchToolBar_PVH = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panel_Filtre_PVH = new System.Windows.Forms.Panel();
            this.buttonVyhledat_PVH = new System.Windows.Forms.Button();
            this.dg_PVP = new Zuby.ADGV.AdvancedDataGridView();
            this.oBJNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJTYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJCOMPANYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJDATEFROMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJDATETODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJORDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oBJITEMORDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATEZAPLANOVANIDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vPPRPSDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vPPRPSQTYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vPPRDCTQTYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uSERIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEXROWIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refPVHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_PVP = new System.Windows.Forms.BindingSource(this.components);
            this.advancedDataGridViewSearchToolBar_PVP = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panel_Filtre_PVP = new System.Windows.Forms.Panel();
            this.buttonVyhledat_PVP = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PVH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PVH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel_Filtre_PVH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PVP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PVP)).BeginInit();
            this.panel_Filtre_PVP.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg_PVH
            // 
            this.dg_PVH.AllowUserToAddRows = false;
            this.dg_PVH.AllowUserToDeleteRows = false;
            this.dg_PVH.AllowUserToOrderColumns = true;
            this.dg_PVH.AllowUserToResizeRows = false;
            this.dg_PVH.AutoGenerateColumns = false;
            this.dg_PVH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_PVH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sOPNUMBEDataGridViewTextBoxColumn,
            this.sOPTYPEDataGridViewTextBoxColumn,
            this.sOPDESCDataGridViewTextBoxColumn,
            this.barcodeHDataGridViewTextBoxColumn,
            this.activeDataGridViewTextBoxColumn,
            this.dEXROWIDDataGridViewTextBoxColumn});
            this.dg_PVH.DataSource = this.bs_PVH;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_PVH.DefaultCellStyle = dataGridViewCellStyle1;
            this.dg_PVH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_PVH.EnableHeadersVisualStyles = false;
            this.dg_PVH.FilterAndSortEnabled = true;
            this.dg_PVH.Location = new System.Drawing.Point(0, 158);
            this.dg_PVH.MultiSelect = false;
            this.dg_PVH.Name = "dg_PVH";
            this.dg_PVH.ReadOnly = true;
            this.dg_PVH.RowHeadersVisible = false;
            this.dg_PVH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_PVH.Size = new System.Drawing.Size(490, 538);
            this.dg_PVH.TabIndex = 1;
            this.dg_PVH.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // sOPNUMBEDataGridViewTextBoxColumn
            // 
            this.sOPNUMBEDataGridViewTextBoxColumn.DataPropertyName = "SOPNUMBE";
            this.sOPNUMBEDataGridViewTextBoxColumn.HeaderText = "Výrobní Zakázka";
            this.sOPNUMBEDataGridViewTextBoxColumn.Name = "sOPNUMBEDataGridViewTextBoxColumn";
            this.sOPNUMBEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sOPNUMBEDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // sOPTYPEDataGridViewTextBoxColumn
            // 
            this.sOPTYPEDataGridViewTextBoxColumn.DataPropertyName = "SOPTYPE";
            this.sOPTYPEDataGridViewTextBoxColumn.HeaderText = "Typ zakázky";
            this.sOPTYPEDataGridViewTextBoxColumn.Name = "sOPTYPEDataGridViewTextBoxColumn";
            this.sOPTYPEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sOPTYPEDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // sOPDESCDataGridViewTextBoxColumn
            // 
            this.sOPDESCDataGridViewTextBoxColumn.DataPropertyName = "SOPDESC";
            this.sOPDESCDataGridViewTextBoxColumn.HeaderText = "Popis zakázky";
            this.sOPDESCDataGridViewTextBoxColumn.Name = "sOPDESCDataGridViewTextBoxColumn";
            this.sOPDESCDataGridViewTextBoxColumn.ReadOnly = true;
            this.sOPDESCDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // barcodeHDataGridViewTextBoxColumn
            // 
            this.barcodeHDataGridViewTextBoxColumn.DataPropertyName = "BarcodeH";
            this.barcodeHDataGridViewTextBoxColumn.HeaderText = "Čarový kód";
            this.barcodeHDataGridViewTextBoxColumn.Name = "barcodeHDataGridViewTextBoxColumn";
            this.barcodeHDataGridViewTextBoxColumn.ReadOnly = true;
            this.barcodeHDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // activeDataGridViewTextBoxColumn
            // 
            this.activeDataGridViewTextBoxColumn.DataPropertyName = "Active";
            this.activeDataGridViewTextBoxColumn.HeaderText = "Aktivní";
            this.activeDataGridViewTextBoxColumn.Name = "activeDataGridViewTextBoxColumn";
            this.activeDataGridViewTextBoxColumn.ReadOnly = true;
            this.activeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dEXROWIDDataGridViewTextBoxColumn
            // 
            this.dEXROWIDDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn.HeaderText = "Index";
            this.dEXROWIDDataGridViewTextBoxColumn.Name = "dEXROWIDDataGridViewTextBoxColumn";
            this.dEXROWIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.dEXROWIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // bs_PVH
            // 
            this.bs_PVH.DataMember = "FASK_Vyroba_PVH";
            this.bs_PVH.DataSource = this.ds;
            // 
            // ds
            // 
            this.ds.DataSetName = "VyrobaDataSet";
            this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.menuStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(972, 720);
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
            this.splitContainer1.Panel1.Controls.Add(this.dg_PVH);
            this.splitContainer1.Panel1.Controls.Add(this.advancedDataGridViewSearchToolBar_PVH);
            this.splitContainer1.Panel1.Controls.Add(this.panel_Filtre_PVH);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dg_PVP);
            this.splitContainer1.Panel2.Controls.Add(this.advancedDataGridViewSearchToolBar_PVP);
            this.splitContainer1.Panel2.Controls.Add(this.panel_Filtre_PVP);
            this.splitContainer1.Size = new System.Drawing.Size(972, 696);
            this.splitContainer1.SplitterDistance = 490;
            this.splitContainer1.TabIndex = 3;
            // 
            // advancedDataGridViewSearchToolBar_PVH
            // 
            this.advancedDataGridViewSearchToolBar_PVH.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar_PVH.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar_PVH.Location = new System.Drawing.Point(0, 131);
            this.advancedDataGridViewSearchToolBar_PVH.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_PVH.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_PVH.Name = "advancedDataGridViewSearchToolBar_PVH";
            this.advancedDataGridViewSearchToolBar_PVH.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar_PVH.Size = new System.Drawing.Size(490, 27);
            this.advancedDataGridViewSearchToolBar_PVH.TabIndex = 2;
            this.advancedDataGridViewSearchToolBar_PVH.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar_PVH.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar_PVH_Search);
            // 
            // panel_Filtre_PVH
            // 
            this.panel_Filtre_PVH.Controls.Add(this.buttonVyhledat_PVH);
            this.panel_Filtre_PVH.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Filtre_PVH.Location = new System.Drawing.Point(0, 0);
            this.panel_Filtre_PVH.Name = "panel_Filtre_PVH";
            this.panel_Filtre_PVH.Size = new System.Drawing.Size(490, 131);
            this.panel_Filtre_PVH.TabIndex = 3;
            // 
            // buttonVyhledat_PVH
            // 
            this.buttonVyhledat_PVH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat_PVH.Location = new System.Drawing.Point(390, 13);
            this.buttonVyhledat_PVH.Name = "buttonVyhledat_PVH";
            this.buttonVyhledat_PVH.Size = new System.Drawing.Size(88, 67);
            this.buttonVyhledat_PVH.TabIndex = 0;
            this.buttonVyhledat_PVH.Text = "Vyhledat";
            this.buttonVyhledat_PVH.UseVisualStyleBackColor = true;
            this.buttonVyhledat_PVH.Click += new System.EventHandler(this.buttonVyhledat_PVH_Click);
            // 
            // dg_PVP
            // 
            this.dg_PVP.AllowUserToAddRows = false;
            this.dg_PVP.AllowUserToDeleteRows = false;
            this.dg_PVP.AllowUserToOrderColumns = true;
            this.dg_PVP.AllowUserToResizeRows = false;
            this.dg_PVP.AutoGenerateColumns = false;
            this.dg_PVP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_PVP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oBJNMBRDataGridViewTextBoxColumn,
            this.oBJDESCDataGridViewTextBoxColumn,
            this.oBJTYPEDataGridViewTextBoxColumn,
            this.oBJCOMPANYDataGridViewTextBoxColumn,
            this.oBJDATEFROMDataGridViewTextBoxColumn,
            this.oBJDATETODataGridViewTextBoxColumn,
            this.oBJORDDataGridViewTextBoxColumn,
            this.oBJITEMORDDataGridViewTextBoxColumn,
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.iTEMCODEDataGridViewTextBoxColumn,
            this.qTYDataGridViewTextBoxColumn,
            this.dATEZAPLANOVANIDataGridViewTextBoxColumn,
            this.vPPRPSDataGridViewCheckBoxColumn,
            this.vPPRPSQTYDataGridViewTextBoxColumn,
            this.vPPRDCTQTYDataGridViewTextBoxColumn,
            this.uSERIDDataGridViewTextBoxColumn,
            this.dEXROWIDDataGridViewTextBoxColumn1,
            this.refPVHDataGridViewTextBoxColumn});
            this.dg_PVP.DataSource = this.bs_PVP;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_PVP.DefaultCellStyle = dataGridViewCellStyle2;
            this.dg_PVP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_PVP.EnableHeadersVisualStyles = false;
            this.dg_PVP.FilterAndSortEnabled = true;
            this.dg_PVP.Location = new System.Drawing.Point(0, 158);
            this.dg_PVP.MultiSelect = false;
            this.dg_PVP.Name = "dg_PVP";
            this.dg_PVP.ReadOnly = true;
            this.dg_PVP.RowHeadersVisible = false;
            this.dg_PVP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_PVP.Size = new System.Drawing.Size(478, 538);
            this.dg_PVP.TabIndex = 0;
            // 
            // oBJNMBRDataGridViewTextBoxColumn
            // 
            this.oBJNMBRDataGridViewTextBoxColumn.DataPropertyName = "OBJ_NMBR";
            this.oBJNMBRDataGridViewTextBoxColumn.HeaderText = "Číslo objednávky";
            this.oBJNMBRDataGridViewTextBoxColumn.Name = "oBJNMBRDataGridViewTextBoxColumn";
            this.oBJNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            this.oBJNMBRDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // oBJDESCDataGridViewTextBoxColumn
            // 
            this.oBJDESCDataGridViewTextBoxColumn.DataPropertyName = "OBJ_DESC";
            this.oBJDESCDataGridViewTextBoxColumn.HeaderText = "Popis objednávky";
            this.oBJDESCDataGridViewTextBoxColumn.Name = "oBJDESCDataGridViewTextBoxColumn";
            this.oBJDESCDataGridViewTextBoxColumn.ReadOnly = true;
            this.oBJDESCDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // oBJTYPEDataGridViewTextBoxColumn
            // 
            this.oBJTYPEDataGridViewTextBoxColumn.DataPropertyName = "OBJ_TYPE";
            this.oBJTYPEDataGridViewTextBoxColumn.HeaderText = "Typ objednávky";
            this.oBJTYPEDataGridViewTextBoxColumn.Name = "oBJTYPEDataGridViewTextBoxColumn";
            this.oBJTYPEDataGridViewTextBoxColumn.ReadOnly = true;
            this.oBJTYPEDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // oBJCOMPANYDataGridViewTextBoxColumn
            // 
            this.oBJCOMPANYDataGridViewTextBoxColumn.DataPropertyName = "OBJ_COMPANY";
            this.oBJCOMPANYDataGridViewTextBoxColumn.HeaderText = "Firma";
            this.oBJCOMPANYDataGridViewTextBoxColumn.Name = "oBJCOMPANYDataGridViewTextBoxColumn";
            this.oBJCOMPANYDataGridViewTextBoxColumn.ReadOnly = true;
            this.oBJCOMPANYDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // oBJDATEFROMDataGridViewTextBoxColumn
            // 
            this.oBJDATEFROMDataGridViewTextBoxColumn.DataPropertyName = "OBJ_DATE_FROM";
            this.oBJDATEFROMDataGridViewTextBoxColumn.HeaderText = "Dátum od";
            this.oBJDATEFROMDataGridViewTextBoxColumn.Name = "oBJDATEFROMDataGridViewTextBoxColumn";
            this.oBJDATEFROMDataGridViewTextBoxColumn.ReadOnly = true;
            this.oBJDATEFROMDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // oBJDATETODataGridViewTextBoxColumn
            // 
            this.oBJDATETODataGridViewTextBoxColumn.DataPropertyName = "OBJ_DATE_TO";
            this.oBJDATETODataGridViewTextBoxColumn.HeaderText = "Dátum do";
            this.oBJDATETODataGridViewTextBoxColumn.Name = "oBJDATETODataGridViewTextBoxColumn";
            this.oBJDATETODataGridViewTextBoxColumn.ReadOnly = true;
            this.oBJDATETODataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // oBJORDDataGridViewTextBoxColumn
            // 
            this.oBJORDDataGridViewTextBoxColumn.DataPropertyName = "OBJ_ORD";
            this.oBJORDDataGridViewTextBoxColumn.HeaderText = "Číslo řádku dokladu";
            this.oBJORDDataGridViewTextBoxColumn.Name = "oBJORDDataGridViewTextBoxColumn";
            this.oBJORDDataGridViewTextBoxColumn.ReadOnly = true;
            this.oBJORDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // oBJITEMORDDataGridViewTextBoxColumn
            // 
            this.oBJITEMORDDataGridViewTextBoxColumn.DataPropertyName = "OBJ_ITEM_ORD";
            this.oBJITEMORDDataGridViewTextBoxColumn.HeaderText = "Číslo řádku položky";
            this.oBJITEMORDDataGridViewTextBoxColumn.Name = "oBJITEMORDDataGridViewTextBoxColumn";
            this.oBJITEMORDDataGridViewTextBoxColumn.ReadOnly = true;
            this.oBJITEMORDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "Číslo položky";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMNMBRDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Název položky";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMDESCDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // iTEMCODEDataGridViewTextBoxColumn
            // 
            this.iTEMCODEDataGridViewTextBoxColumn.DataPropertyName = "ITEMCODE";
            this.iTEMCODEDataGridViewTextBoxColumn.HeaderText = "Kód";
            this.iTEMCODEDataGridViewTextBoxColumn.Name = "iTEMCODEDataGridViewTextBoxColumn";
            this.iTEMCODEDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMCODEDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // qTYDataGridViewTextBoxColumn
            // 
            this.qTYDataGridViewTextBoxColumn.DataPropertyName = "QTY";
            this.qTYDataGridViewTextBoxColumn.HeaderText = "Množství objednané";
            this.qTYDataGridViewTextBoxColumn.Name = "qTYDataGridViewTextBoxColumn";
            this.qTYDataGridViewTextBoxColumn.ReadOnly = true;
            this.qTYDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dATEZAPLANOVANIDataGridViewTextBoxColumn
            // 
            this.dATEZAPLANOVANIDataGridViewTextBoxColumn.DataPropertyName = "DATE_ZAPLANOVANI";
            this.dATEZAPLANOVANIDataGridViewTextBoxColumn.HeaderText = "Dátum zaplánovaní";
            this.dATEZAPLANOVANIDataGridViewTextBoxColumn.Name = "dATEZAPLANOVANIDataGridViewTextBoxColumn";
            this.dATEZAPLANOVANIDataGridViewTextBoxColumn.ReadOnly = true;
            this.dATEZAPLANOVANIDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // vPPRPSDataGridViewCheckBoxColumn
            // 
            this.vPPRPSDataGridViewCheckBoxColumn.DataPropertyName = "VP_PRPS";
            this.vPPRPSDataGridViewCheckBoxColumn.HeaderText = "Návrh VP";
            this.vPPRPSDataGridViewCheckBoxColumn.Name = "vPPRPSDataGridViewCheckBoxColumn";
            this.vPPRPSDataGridViewCheckBoxColumn.ReadOnly = true;
            this.vPPRPSDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.vPPRPSDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // vPPRPSQTYDataGridViewTextBoxColumn
            // 
            this.vPPRPSQTYDataGridViewTextBoxColumn.DataPropertyName = "VP_PRPS_QTY";
            this.vPPRPSQTYDataGridViewTextBoxColumn.HeaderText = "Množství návrh VP";
            this.vPPRPSQTYDataGridViewTextBoxColumn.Name = "vPPRPSQTYDataGridViewTextBoxColumn";
            this.vPPRPSQTYDataGridViewTextBoxColumn.ReadOnly = true;
            this.vPPRPSQTYDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // vPPRDCTQTYDataGridViewTextBoxColumn
            // 
            this.vPPRDCTQTYDataGridViewTextBoxColumn.DataPropertyName = "VP_PRDCT_QTY";
            this.vPPRDCTQTYDataGridViewTextBoxColumn.HeaderText = "Množství do výroby";
            this.vPPRDCTQTYDataGridViewTextBoxColumn.Name = "vPPRDCTQTYDataGridViewTextBoxColumn";
            this.vPPRDCTQTYDataGridViewTextBoxColumn.ReadOnly = true;
            this.vPPRDCTQTYDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // uSERIDDataGridViewTextBoxColumn
            // 
            this.uSERIDDataGridViewTextBoxColumn.DataPropertyName = "USERID";
            this.uSERIDDataGridViewTextBoxColumn.HeaderText = "Pracovník ID";
            this.uSERIDDataGridViewTextBoxColumn.Name = "uSERIDDataGridViewTextBoxColumn";
            this.uSERIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.uSERIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dEXROWIDDataGridViewTextBoxColumn1
            // 
            this.dEXROWIDDataGridViewTextBoxColumn1.DataPropertyName = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn1.HeaderText = "Index";
            this.dEXROWIDDataGridViewTextBoxColumn1.Name = "dEXROWIDDataGridViewTextBoxColumn1";
            this.dEXROWIDDataGridViewTextBoxColumn1.ReadOnly = true;
            this.dEXROWIDDataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // refPVHDataGridViewTextBoxColumn
            // 
            this.refPVHDataGridViewTextBoxColumn.DataPropertyName = "Ref_PVH";
            this.refPVHDataGridViewTextBoxColumn.HeaderText = "Reference do hlavičky";
            this.refPVHDataGridViewTextBoxColumn.Name = "refPVHDataGridViewTextBoxColumn";
            this.refPVHDataGridViewTextBoxColumn.ReadOnly = true;
            this.refPVHDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // bs_PVP
            // 
            this.bs_PVP.DataMember = "FASK_Vyroba_PVP";
            this.bs_PVP.DataSource = this.ds;
            // 
            // advancedDataGridViewSearchToolBar_PVP
            // 
            this.advancedDataGridViewSearchToolBar_PVP.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar_PVP.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar_PVP.Location = new System.Drawing.Point(0, 131);
            this.advancedDataGridViewSearchToolBar_PVP.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_PVP.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_PVP.Name = "advancedDataGridViewSearchToolBar_PVP";
            this.advancedDataGridViewSearchToolBar_PVP.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar_PVP.Size = new System.Drawing.Size(478, 27);
            this.advancedDataGridViewSearchToolBar_PVP.TabIndex = 1;
            this.advancedDataGridViewSearchToolBar_PVP.Text = "advancedDataGridViewSearchToolBar2";
            this.advancedDataGridViewSearchToolBar_PVP.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar_PVP_Search);
            // 
            // panel_Filtre_PVP
            // 
            this.panel_Filtre_PVP.Controls.Add(this.buttonVyhledat_PVP);
            this.panel_Filtre_PVP.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Filtre_PVP.Location = new System.Drawing.Point(0, 0);
            this.panel_Filtre_PVP.Name = "panel_Filtre_PVP";
            this.panel_Filtre_PVP.Size = new System.Drawing.Size(478, 131);
            this.panel_Filtre_PVP.TabIndex = 2;
            // 
            // buttonVyhledat_PVP
            // 
            this.buttonVyhledat_PVP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat_PVP.Location = new System.Drawing.Point(384, 13);
            this.buttonVyhledat_PVP.Name = "buttonVyhledat_PVP";
            this.buttonVyhledat_PVP.Size = new System.Drawing.Size(88, 67);
            this.buttonVyhledat_PVP.TabIndex = 0;
            this.buttonVyhledat_PVP.Text = "Vyhledat";
            this.buttonVyhledat_PVP.UseVisualStyleBackColor = true;
            this.buttonVyhledat_PVP.Click += new System.EventHandler(this.buttonVyhledat_PVP_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(972, 24);
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
            this.tsmiKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(972, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 720);
            this.panelButtons.TabIndex = 4;
            // 
            // FormPlanovaniVyroby_Rozbor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 720);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormPlanovaniVyroby_Rozbor";
            this.ShowIcon = false;
            this.Text = "Plána výroby Rozbor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPlanovaniVyroby_Rozbor_FormClosing);
            this.Load += new System.EventHandler(this.FormPlanovaniVyroby_Rozbor_Load);
            this.Shown += new System.EventHandler(this.FormPlanovaniVyroby_Rozbor_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormPlanovaniVyroby_Rozbor_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dg_PVH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PVH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel_Filtre_PVH.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_PVP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PVP)).EndInit();
            this.panel_Filtre_PVP.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dg_PVH;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private Fask.Interfaces.DataSets.Vyroba_Planovani ds;
        private System.Windows.Forms.BindingSource bs_PVH;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Zuby.ADGV.AdvancedDataGridView dg_PVP;
        private System.Windows.Forms.BindingSource bs_PVP;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar_PVH;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar_PVP;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPNUMBEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPTYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodeHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn activeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJTYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJCOMPANYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJDATEFROMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJDATETODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJORDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJITEMORDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATEZAPLANOVANIDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vPPRPSDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vPPRPSQTYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vPPRDCTQTYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn refPVHDataGridViewTextBoxColumn;
        private System.Windows.Forms.Panel panel_Filtre_PVH;
        private System.Windows.Forms.Panel panel_Filtre_PVP;
        private System.Windows.Forms.Button buttonVyhledat_PVH;
        private System.Windows.Forms.Button buttonVyhledat_PVP;
    }
}