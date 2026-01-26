namespace Konzola.Vyroba
{
    partial class FormVazbyMaterialy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVazbyMaterialy));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.progressIndicatorVyrobky = new ProgressControls.ProgressIndicator();
            this.dgVyrobky = new Zuby.ADGV.AdvancedDataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_L = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VNDITNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PUO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vetev1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vetev2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vetev3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vetev4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vetev5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vetev6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vetev7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsVyrobky = new System.Windows.Forms.BindingSource(this.components);
            this.dsVyrobky = new Fask.Interfaces.DataSets.Vyroba();
            this.advancedDataGridViewSearchToolBarVyrobky = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel_FiltryVyrobky = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFiltry_Vyrobek = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNastavit_Vyrobek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZmena_Vyrobek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPridat_Vyrobek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdebrat_Vyrobek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVycistit_Vyrobek = new System.Windows.Forms.ToolStripButton();
            this.comboBox_Vyrobek_MJ = new System.Windows.Forms.ComboBox();
            this.comboBox_Vyrobek_ITEMDESC = new System.Windows.Forms.ComboBox();
            this.comboBox_Vyrobek_ITEMNMBR = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_Filtr_Vyrobky = new System.Windows.Forms.Button();
            this.progressIndicatorMaterialy = new ProgressControls.ProgressIndicator();
            this.dgMaterialy = new Zuby.ADGV.AdvancedDataGridView();
            this.ID_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_H_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_L_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.koef_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_USER_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateedit_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alter_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MJ_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PUO_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_ID_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_DESC_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsMaterialy = new System.Windows.Forms.BindingSource(this.components);
            this.dsMaterialy = new Fask.Interfaces.DataSets.Vyroba();
            this.advancedDataGridViewSearchToolBarMaterialy = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_MJ = new System.Windows.Forms.ComboBox();
            this.comboBox_ITEMDESC = new System.Windows.Forms.ComboBox();
            this.button_filtr_Material = new System.Windows.Forms.Button();
            this.comboBox_ITEMNMBR = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFiltry_Material = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNastavit_Material = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZmena_Material = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPridat_Material = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdebrat_Material = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVycistit_Material = new System.Windows.Forms.ToolStripButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmi_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_konec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Modifikace_TP = new System.Windows.Forms.ToolStripMenuItem();
            this.výstupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Material = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_odstranitMaterial = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_upravitMaterial = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pridatMaterial = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Polotovar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_PridatPolotovar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_vyrobek = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_duplikovatVyrobek = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_odstranitVyrobek = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pridatVyrobek = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImportovatMaterialy = new System.Windows.Forms.ToolStripMenuItem();
            this.bw_Materialy_stav = new System.ComponentModel.BackgroundWorker();
            this.bw_Vyrobky_stav = new System.ComponentModel.BackgroundWorker();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.bwImportVyrobky = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVyrobky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVyrobky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVyrobky)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel_FiltryVyrobky.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMaterialy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMaterialy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMaterialy)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.progressIndicatorVyrobky);
            this.splitContainer1.Panel1.Controls.Add(this.dgVyrobky);
            this.splitContainer1.Panel1.Controls.Add(this.advancedDataGridViewSearchToolBarVyrobky);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.progressIndicatorMaterialy);
            this.splitContainer1.Panel2.Controls.Add(this.dgMaterialy);
            this.splitContainer1.Panel2.Controls.Add(this.advancedDataGridViewSearchToolBarMaterialy);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(735, 650);
            this.splitContainer1.SplitterDistance = 409;
            this.splitContainer1.TabIndex = 4;
            // 
            // progressIndicatorVyrobky
            // 
            this.progressIndicatorVyrobky.Location = new System.Drawing.Point(135, 359);
            this.progressIndicatorVyrobky.Name = "progressIndicatorVyrobky";
            this.progressIndicatorVyrobky.Percentage = 0F;
            this.progressIndicatorVyrobky.Size = new System.Drawing.Size(98, 98);
            this.progressIndicatorVyrobky.TabIndex = 39;
            this.progressIndicatorVyrobky.Text = "progressIndicator1";
            this.progressIndicatorVyrobky.Visible = false;
            // 
            // dgVyrobky
            // 
            this.dgVyrobky.AllowUserToAddRows = false;
            this.dgVyrobky.AllowUserToDeleteRows = false;
            this.dgVyrobky.AllowUserToOrderColumns = true;
            this.dgVyrobky.AllowUserToResizeRows = false;
            this.dgVyrobky.AutoGenerateColumns = false;
            this.dgVyrobky.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVyrobky.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ID_H,
            this.ID_L,
            this.ITEMNMBR,
            this.ITEMCODE,
            this.VNDITNUM,
            this.ITEMDESC,
            this.MJ,
            this.PUO,
            this.SKL_ID,
            this.SKL_DESC,
            this.Vetev1,
            this.Vetev2,
            this.Vetev3,
            this.Vetev4,
            this.Vetev5,
            this.Vetev6,
            this.Vetev7});
            this.dgVyrobky.DataSource = this.bsVyrobky;
            this.dgVyrobky.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVyrobky.EnableHeadersVisualStyles = false;
            this.dgVyrobky.FilterAndSortEnabled = true;
            this.dgVyrobky.Location = new System.Drawing.Point(0, 169);
            this.dgVyrobky.MultiSelect = false;
            this.dgVyrobky.Name = "dgVyrobky";
            this.dgVyrobky.ReadOnly = true;
            this.dgVyrobky.RowHeadersVisible = false;
            this.dgVyrobky.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVyrobky.Size = new System.Drawing.Size(409, 481);
            this.dgVyrobky.TabIndex = 1;
            this.dgVyrobky.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgVyrobky_DataError);
            this.dgVyrobky.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "Index";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ID_H
            // 
            this.ID_H.DataPropertyName = "ID_H";
            this.ID_H.HeaderText = "Index vyšší";
            this.ID_H.Name = "ID_H";
            this.ID_H.ReadOnly = true;
            this.ID_H.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ID_L
            // 
            this.ID_L.DataPropertyName = "ID_L";
            this.ID_L.HeaderText = "Index nižší";
            this.ID_L.Name = "ID_L";
            this.ID_L.ReadOnly = true;
            this.ID_L.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMNMBR
            // 
            this.ITEMNMBR.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR.HeaderText = "Pol. číslo";
            this.ITEMNMBR.Name = "ITEMNMBR";
            this.ITEMNMBR.ReadOnly = true;
            this.ITEMNMBR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMCODE
            // 
            this.ITEMCODE.DataPropertyName = "ITEMCODE";
            this.ITEMCODE.HeaderText = "Kód položky";
            this.ITEMCODE.Name = "ITEMCODE";
            this.ITEMCODE.ReadOnly = true;
            // 
            // VNDITNUM
            // 
            this.VNDITNUM.DataPropertyName = "VNDITNUM";
            this.VNDITNUM.HeaderText = "Čár. kód";
            this.VNDITNUM.Name = "VNDITNUM";
            this.VNDITNUM.ReadOnly = true;
            // 
            // ITEMDESC
            // 
            this.ITEMDESC.DataPropertyName = "ITEMDESC";
            this.ITEMDESC.HeaderText = "Popis";
            this.ITEMDESC.Name = "ITEMDESC";
            this.ITEMDESC.ReadOnly = true;
            this.ITEMDESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MJ
            // 
            this.MJ.DataPropertyName = "MJ";
            this.MJ.HeaderText = "Měrná jednotka";
            this.MJ.Name = "MJ";
            this.MJ.ReadOnly = true;
            this.MJ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PUO
            // 
            this.PUO.DataPropertyName = "PUO";
            this.PUO.HeaderText = "Příznak uzlového odvádění";
            this.PUO.Name = "PUO";
            this.PUO.ReadOnly = true;
            this.PUO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_ID
            // 
            this.SKL_ID.DataPropertyName = "SKL_ID";
            this.SKL_ID.HeaderText = "ID skladu";
            this.SKL_ID.Name = "SKL_ID";
            this.SKL_ID.ReadOnly = true;
            this.SKL_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_DESC
            // 
            this.SKL_DESC.DataPropertyName = "SKL_DESC";
            this.SKL_DESC.HeaderText = "Název skladu";
            this.SKL_DESC.Name = "SKL_DESC";
            this.SKL_DESC.ReadOnly = true;
            this.SKL_DESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Vetev1
            // 
            this.Vetev1.DataPropertyName = "Vetev1";
            this.Vetev1.HeaderText = "Větev 1";
            this.Vetev1.Name = "Vetev1";
            this.Vetev1.ReadOnly = true;
            // 
            // Vetev2
            // 
            this.Vetev2.DataPropertyName = "Vetev2";
            this.Vetev2.HeaderText = "Větev 2";
            this.Vetev2.Name = "Vetev2";
            this.Vetev2.ReadOnly = true;
            // 
            // Vetev3
            // 
            this.Vetev3.DataPropertyName = "Vetev3";
            this.Vetev3.HeaderText = "Větev 3";
            this.Vetev3.Name = "Vetev3";
            this.Vetev3.ReadOnly = true;
            // 
            // Vetev4
            // 
            this.Vetev4.DataPropertyName = "Vetev4";
            this.Vetev4.HeaderText = "Větev 4";
            this.Vetev4.Name = "Vetev4";
            this.Vetev4.ReadOnly = true;
            // 
            // Vetev5
            // 
            this.Vetev5.DataPropertyName = "Vetev5";
            this.Vetev5.HeaderText = "Větev 5";
            this.Vetev5.Name = "Vetev5";
            this.Vetev5.ReadOnly = true;
            // 
            // Vetev6
            // 
            this.Vetev6.DataPropertyName = "Vetev6";
            this.Vetev6.HeaderText = "Větev 6";
            this.Vetev6.Name = "Vetev6";
            this.Vetev6.ReadOnly = true;
            // 
            // Vetev7
            // 
            this.Vetev7.DataPropertyName = "Vetev7";
            this.Vetev7.HeaderText = "Větev 7";
            this.Vetev7.Name = "Vetev7";
            this.Vetev7.ReadOnly = true;
            // 
            // bsVyrobky
            // 
            this.bsVyrobky.DataMember = "FASK_Vyroba_TP_Vyrobek";
            this.bsVyrobky.DataSource = this.dsVyrobky;
            // 
            // dsVyrobky
            // 
            this.dsVyrobky.DataSetName = "VyrobaDataSet";
            this.dsVyrobky.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBarVyrobky
            // 
            this.advancedDataGridViewSearchToolBarVyrobky.AllowMerge = false;
            this.advancedDataGridViewSearchToolBarVyrobky.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBarVyrobky.Location = new System.Drawing.Point(0, 142);
            this.advancedDataGridViewSearchToolBarVyrobky.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBarVyrobky.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBarVyrobky.Name = "advancedDataGridViewSearchToolBarVyrobky";
            this.advancedDataGridViewSearchToolBarVyrobky.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBarVyrobky.Size = new System.Drawing.Size(409, 27);
            this.advancedDataGridViewSearchToolBarVyrobky.TabIndex = 40;
            this.advancedDataGridViewSearchToolBarVyrobky.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBarVyrobky.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBarVyrobky_Search);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel_FiltryVyrobky);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 142);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtr Výrobky";
            // 
            // panel_FiltryVyrobky
            // 
            this.panel_FiltryVyrobky.AutoScroll = true;
            this.panel_FiltryVyrobky.Controls.Add(this.toolStrip2);
            this.panel_FiltryVyrobky.Controls.Add(this.comboBox_Vyrobek_MJ);
            this.panel_FiltryVyrobky.Controls.Add(this.comboBox_Vyrobek_ITEMDESC);
            this.panel_FiltryVyrobky.Controls.Add(this.comboBox_Vyrobek_ITEMNMBR);
            this.panel_FiltryVyrobky.Controls.Add(this.label3);
            this.panel_FiltryVyrobky.Controls.Add(this.label4);
            this.panel_FiltryVyrobky.Controls.Add(this.label5);
            this.panel_FiltryVyrobky.Controls.Add(this.button_Filtr_Vyrobky);
            this.panel_FiltryVyrobky.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_FiltryVyrobky.Location = new System.Drawing.Point(3, 16);
            this.panel_FiltryVyrobky.Name = "panel_FiltryVyrobky";
            this.panel_FiltryVyrobky.Size = new System.Drawing.Size(403, 123);
            this.panel_FiltryVyrobky.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tscbFiltry_Vyrobek,
            this.tsbNastavit_Vyrobek,
            this.toolStripSeparator7,
            this.tsbZmena_Vyrobek,
            this.toolStripSeparator8,
            this.tsbPridat_Vyrobek,
            this.toolStripSeparator9,
            this.tsbOdebrat_Vyrobek,
            this.toolStripSeparator10,
            this.tsbVycistit_Vyrobek});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(403, 25);
            this.toolStrip2.TabIndex = 16;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(30, 22);
            this.toolStripLabel2.Text = "Filtr:";
            // 
            // tscbFiltry_Vyrobek
            // 
            this.tscbFiltry_Vyrobek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry_Vyrobek.Name = "tscbFiltry_Vyrobek";
            this.tscbFiltry_Vyrobek.Size = new System.Drawing.Size(121, 25);
            // 
            // tsbNastavit_Vyrobek
            // 
            this.tsbNastavit_Vyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit_Vyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit_Vyrobek.Image")));
            this.tsbNastavit_Vyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit_Vyrobek.Name = "tsbNastavit_Vyrobek";
            this.tsbNastavit_Vyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbNastavit_Vyrobek.Text = "Nastavit";
            this.tsbNastavit_Vyrobek.ToolTipText = "Nastavit";
            this.tsbNastavit_Vyrobek.Click += new System.EventHandler(this.tsbNastavit_Material_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbZmena_Vyrobek
            // 
            this.tsbZmena_Vyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena_Vyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena_Vyrobek.Image")));
            this.tsbZmena_Vyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena_Vyrobek.Name = "tsbZmena_Vyrobek";
            this.tsbZmena_Vyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbZmena_Vyrobek.Text = "Změna";
            this.tsbZmena_Vyrobek.Click += new System.EventHandler(this.tsbZmena_Vyrobek_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPridat_Vyrobek
            // 
            this.tsbPridat_Vyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat_Vyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat_Vyrobek.Image")));
            this.tsbPridat_Vyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat_Vyrobek.Name = "tsbPridat_Vyrobek";
            this.tsbPridat_Vyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbPridat_Vyrobek.Text = "Uložit";
            this.tsbPridat_Vyrobek.Click += new System.EventHandler(this.tsbPridat_Vyrobek_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOdebrat_Vyrobek
            // 
            this.tsbOdebrat_Vyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat_Vyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat_Vyrobek.Image")));
            this.tsbOdebrat_Vyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat_Vyrobek.Name = "tsbOdebrat_Vyrobek";
            this.tsbOdebrat_Vyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbOdebrat_Vyrobek.Text = "Odebrat";
            this.tsbOdebrat_Vyrobek.Click += new System.EventHandler(this.tsbOdebrat_Vyrobek_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbVycistit_Vyrobek
            // 
            this.tsbVycistit_Vyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit_Vyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit_Vyrobek.Image")));
            this.tsbVycistit_Vyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit_Vyrobek.Name = "tsbVycistit_Vyrobek";
            this.tsbVycistit_Vyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbVycistit_Vyrobek.Text = "Vyčistit";
            this.tsbVycistit_Vyrobek.Click += new System.EventHandler(this.tsbVycistit_Vyrobek_Click);
            // 
            // comboBox_Vyrobek_MJ
            // 
            this.comboBox_Vyrobek_MJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Vyrobek_MJ.FormattingEnabled = true;
            this.comboBox_Vyrobek_MJ.Location = new System.Drawing.Point(97, 93);
            this.comboBox_Vyrobek_MJ.Name = "comboBox_Vyrobek_MJ";
            this.comboBox_Vyrobek_MJ.Size = new System.Drawing.Size(103, 21);
            this.comboBox_Vyrobek_MJ.TabIndex = 5;
            // 
            // comboBox_Vyrobek_ITEMDESC
            // 
            this.comboBox_Vyrobek_ITEMDESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Vyrobek_ITEMDESC.FormattingEnabled = true;
            this.comboBox_Vyrobek_ITEMDESC.Location = new System.Drawing.Point(97, 66);
            this.comboBox_Vyrobek_ITEMDESC.Name = "comboBox_Vyrobek_ITEMDESC";
            this.comboBox_Vyrobek_ITEMDESC.Size = new System.Drawing.Size(103, 21);
            this.comboBox_Vyrobek_ITEMDESC.TabIndex = 7;
            // 
            // comboBox_Vyrobek_ITEMNMBR
            // 
            this.comboBox_Vyrobek_ITEMNMBR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Vyrobek_ITEMNMBR.FormattingEnabled = true;
            this.comboBox_Vyrobek_ITEMNMBR.Location = new System.Drawing.Point(97, 39);
            this.comboBox_Vyrobek_ITEMNMBR.Name = "comboBox_Vyrobek_ITEMNMBR";
            this.comboBox_Vyrobek_ITEMNMBR.Size = new System.Drawing.Size(103, 21);
            this.comboBox_Vyrobek_ITEMNMBR.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Měrná jednotka:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Pol. Číslo:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Popis :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_Filtr_Vyrobky
            // 
            this.button_Filtr_Vyrobky.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Filtr_Vyrobky.Location = new System.Drawing.Point(206, 36);
            this.button_Filtr_Vyrobky.Name = "button_Filtr_Vyrobky";
            this.button_Filtr_Vyrobky.Size = new System.Drawing.Size(82, 78);
            this.button_Filtr_Vyrobky.TabIndex = 0;
            this.button_Filtr_Vyrobky.Text = "Vyhledat";
            this.button_Filtr_Vyrobky.UseVisualStyleBackColor = true;
            this.button_Filtr_Vyrobky.Click += new System.EventHandler(this.button_Filtr_Vyrobky_Click);
            // 
            // progressIndicatorMaterialy
            // 
            this.progressIndicatorMaterialy.Location = new System.Drawing.Point(117, 371);
            this.progressIndicatorMaterialy.Name = "progressIndicatorMaterialy";
            this.progressIndicatorMaterialy.Percentage = 0F;
            this.progressIndicatorMaterialy.Size = new System.Drawing.Size(98, 98);
            this.progressIndicatorMaterialy.TabIndex = 39;
            this.progressIndicatorMaterialy.Text = "progressIndicator1";
            this.progressIndicatorMaterialy.Visible = false;
            // 
            // dgMaterialy
            // 
            this.dgMaterialy.AllowUserToAddRows = false;
            this.dgMaterialy.AllowUserToDeleteRows = false;
            this.dgMaterialy.AllowUserToOrderColumns = true;
            this.dgMaterialy.AllowUserToResizeRows = false;
            this.dgMaterialy.AutoGenerateColumns = false;
            this.dgMaterialy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMaterialy.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_,
            this.ID_H_,
            this.ID_L_,
            this.koef_,
            this.ID_USER_,
            this.dateedit_,
            this.alter_,
            this.ITEMNMBR_,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.ITEMDESC_,
            this.MJ_,
            this.PUO_,
            this.SKL_ID_,
            this.SKL_DESC_,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9});
            this.dgMaterialy.DataSource = this.bsMaterialy;
            this.dgMaterialy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMaterialy.EnableHeadersVisualStyles = false;
            this.dgMaterialy.FilterAndSortEnabled = true;
            this.dgMaterialy.Location = new System.Drawing.Point(0, 169);
            this.dgMaterialy.Name = "dgMaterialy";
            this.dgMaterialy.ReadOnly = true;
            this.dgMaterialy.RowHeadersVisible = false;
            this.dgMaterialy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMaterialy.Size = new System.Drawing.Size(322, 481);
            this.dgMaterialy.TabIndex = 2;
            this.dgMaterialy.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgMaterialy_DataError);
            // 
            // ID_
            // 
            this.ID_.DataPropertyName = "ID";
            this.ID_.HeaderText = "Index";
            this.ID_.Name = "ID_";
            this.ID_.ReadOnly = true;
            this.ID_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ID_H_
            // 
            this.ID_H_.DataPropertyName = "ID_H";
            this.ID_H_.HeaderText = "Index vyšší";
            this.ID_H_.Name = "ID_H_";
            this.ID_H_.ReadOnly = true;
            this.ID_H_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ID_L_
            // 
            this.ID_L_.DataPropertyName = "ID_L";
            this.ID_L_.HeaderText = "Index nižší";
            this.ID_L_.Name = "ID_L_";
            this.ID_L_.ReadOnly = true;
            this.ID_L_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // koef_
            // 
            this.koef_.DataPropertyName = "koef";
            this.koef_.HeaderText = "Koeficient";
            this.koef_.Name = "koef_";
            this.koef_.ReadOnly = true;
            this.koef_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ID_USER_
            // 
            this.ID_USER_.DataPropertyName = "ID_USER";
            this.ID_USER_.HeaderText = "ID uživatel";
            this.ID_USER_.Name = "ID_USER_";
            this.ID_USER_.ReadOnly = true;
            this.ID_USER_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dateedit_
            // 
            this.dateedit_.DataPropertyName = "dateedit";
            this.dateedit_.HeaderText = "Dátum Editace";
            this.dateedit_.Name = "dateedit_";
            this.dateedit_.ReadOnly = true;
            this.dateedit_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // alter_
            // 
            this.alter_.DataPropertyName = "alter";
            this.alter_.HeaderText = "Alternatíva";
            this.alter_.Name = "alter_";
            this.alter_.ReadOnly = true;
            this.alter_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMNMBR_
            // 
            this.ITEMNMBR_.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR_.HeaderText = "Pol. číslo";
            this.ITEMNMBR_.Name = "ITEMNMBR_";
            this.ITEMNMBR_.ReadOnly = true;
            this.ITEMNMBR_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ITEMCODE";
            this.dataGridViewTextBoxColumn1.HeaderText = "Kód položky";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "VNDITNUM";
            this.dataGridViewTextBoxColumn2.HeaderText = "Čár. kód";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // ITEMDESC_
            // 
            this.ITEMDESC_.DataPropertyName = "ITEMDESC";
            this.ITEMDESC_.HeaderText = "Popis";
            this.ITEMDESC_.Name = "ITEMDESC_";
            this.ITEMDESC_.ReadOnly = true;
            this.ITEMDESC_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MJ_
            // 
            this.MJ_.DataPropertyName = "MJ";
            this.MJ_.HeaderText = "Měrná jednotka";
            this.MJ_.Name = "MJ_";
            this.MJ_.ReadOnly = true;
            this.MJ_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PUO_
            // 
            this.PUO_.DataPropertyName = "PUO";
            this.PUO_.HeaderText = "Příznak uzlového odvádění";
            this.PUO_.Name = "PUO_";
            this.PUO_.ReadOnly = true;
            this.PUO_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_ID_
            // 
            this.SKL_ID_.DataPropertyName = "SKL_ID";
            this.SKL_ID_.HeaderText = "ID skladu";
            this.SKL_ID_.Name = "SKL_ID_";
            this.SKL_ID_.ReadOnly = true;
            this.SKL_ID_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_DESC_
            // 
            this.SKL_DESC_.DataPropertyName = "SKL_DESC";
            this.SKL_DESC_.HeaderText = "Název skladu";
            this.SKL_DESC_.Name = "SKL_DESC_";
            this.SKL_DESC_.ReadOnly = true;
            this.SKL_DESC_.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Vetev1";
            this.dataGridViewTextBoxColumn3.HeaderText = "Větev 1";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Vetev2";
            this.dataGridViewTextBoxColumn4.HeaderText = "Větev 2";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Vetev3";
            this.dataGridViewTextBoxColumn5.HeaderText = "Větev 3";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Vetev4";
            this.dataGridViewTextBoxColumn6.HeaderText = "Větev 4";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Vetev5";
            this.dataGridViewTextBoxColumn7.HeaderText = "Větev 5";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Vetev6";
            this.dataGridViewTextBoxColumn8.HeaderText = "Větev 6";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Vetev7";
            this.dataGridViewTextBoxColumn9.HeaderText = "Větev 7";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // bsMaterialy
            // 
            this.bsMaterialy.DataMember = "FASK_Vyroba_TP_Material";
            this.bsMaterialy.DataSource = this.dsMaterialy;
            // 
            // dsMaterialy
            // 
            this.dsMaterialy.DataSetName = "VyrobaDataSet";
            this.dsMaterialy.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBarMaterialy
            // 
            this.advancedDataGridViewSearchToolBarMaterialy.AllowMerge = false;
            this.advancedDataGridViewSearchToolBarMaterialy.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBarMaterialy.Location = new System.Drawing.Point(0, 142);
            this.advancedDataGridViewSearchToolBarMaterialy.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBarMaterialy.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBarMaterialy.Name = "advancedDataGridViewSearchToolBarMaterialy";
            this.advancedDataGridViewSearchToolBarMaterialy.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBarMaterialy.Size = new System.Drawing.Size(322, 27);
            this.advancedDataGridViewSearchToolBarMaterialy.TabIndex = 40;
            this.advancedDataGridViewSearchToolBarMaterialy.Text = "advancedDataGridViewSearchToolBar2";
            this.advancedDataGridViewSearchToolBarMaterialy.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBarMaterialy_Search);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_MJ);
            this.groupBox1.Controls.Add(this.comboBox_ITEMDESC);
            this.groupBox1.Controls.Add(this.button_filtr_Material);
            this.groupBox1.Controls.Add(this.comboBox_ITEMNMBR);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 142);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtr Materiály";
            // 
            // comboBox_MJ
            // 
            this.comboBox_MJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_MJ.FormattingEnabled = true;
            this.comboBox_MJ.Location = new System.Drawing.Point(96, 113);
            this.comboBox_MJ.Name = "comboBox_MJ";
            this.comboBox_MJ.Size = new System.Drawing.Size(103, 21);
            this.comboBox_MJ.TabIndex = 1;
            // 
            // comboBox_ITEMDESC
            // 
            this.comboBox_ITEMDESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_ITEMDESC.FormattingEnabled = true;
            this.comboBox_ITEMDESC.Location = new System.Drawing.Point(96, 86);
            this.comboBox_ITEMDESC.Name = "comboBox_ITEMDESC";
            this.comboBox_ITEMDESC.Size = new System.Drawing.Size(103, 21);
            this.comboBox_ITEMDESC.TabIndex = 1;
            // 
            // button_filtr_Material
            // 
            this.button_filtr_Material.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_filtr_Material.Location = new System.Drawing.Point(205, 56);
            this.button_filtr_Material.Name = "button_filtr_Material";
            this.button_filtr_Material.Size = new System.Drawing.Size(82, 78);
            this.button_filtr_Material.TabIndex = 0;
            this.button_filtr_Material.Text = "Vyhledat";
            this.button_filtr_Material.UseVisualStyleBackColor = true;
            this.button_filtr_Material.Click += new System.EventHandler(this.button_filtr_Material_Click);
            // 
            // comboBox_ITEMNMBR
            // 
            this.comboBox_ITEMNMBR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_ITEMNMBR.FormattingEnabled = true;
            this.comboBox_ITEMNMBR.Location = new System.Drawing.Point(96, 59);
            this.comboBox_ITEMNMBR.Name = "comboBox_ITEMNMBR";
            this.comboBox_ITEMNMBR.Size = new System.Drawing.Size(103, 21);
            this.comboBox_ITEMNMBR.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbFiltry_Material,
            this.tsbNastavit_Material,
            this.toolStripSeparator3,
            this.tsbZmena_Material,
            this.toolStripSeparator4,
            this.tsbPridat_Material,
            this.toolStripSeparator5,
            this.tsbOdebrat_Material,
            this.toolStripSeparator6,
            this.tsbVycistit_Material});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(316, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(30, 22);
            this.toolStripLabel1.Text = "Filtr:";
            // 
            // tscbFiltry_Material
            // 
            this.tscbFiltry_Material.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry_Material.Name = "tscbFiltry_Material";
            this.tscbFiltry_Material.Size = new System.Drawing.Size(121, 25);
            // 
            // tsbNastavit_Material
            // 
            this.tsbNastavit_Material.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit_Material.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit_Material.Image")));
            this.tsbNastavit_Material.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit_Material.Name = "tsbNastavit_Material";
            this.tsbNastavit_Material.Size = new System.Drawing.Size(23, 22);
            this.tsbNastavit_Material.Text = "Nastavit";
            this.tsbNastavit_Material.ToolTipText = "Nastavit";
            this.tsbNastavit_Material.Click += new System.EventHandler(this.tsbNastavit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbZmena_Material
            // 
            this.tsbZmena_Material.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena_Material.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena_Material.Image")));
            this.tsbZmena_Material.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena_Material.Name = "tsbZmena_Material";
            this.tsbZmena_Material.Size = new System.Drawing.Size(23, 22);
            this.tsbZmena_Material.Text = "Změna";
            this.tsbZmena_Material.Click += new System.EventHandler(this.tsbZmena_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPridat_Material
            // 
            this.tsbPridat_Material.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat_Material.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat_Material.Image")));
            this.tsbPridat_Material.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat_Material.Name = "tsbPridat_Material";
            this.tsbPridat_Material.Size = new System.Drawing.Size(23, 22);
            this.tsbPridat_Material.Text = "Uložit";
            this.tsbPridat_Material.Click += new System.EventHandler(this.tsbPridat_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOdebrat_Material
            // 
            this.tsbOdebrat_Material.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat_Material.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat_Material.Image")));
            this.tsbOdebrat_Material.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat_Material.Name = "tsbOdebrat_Material";
            this.tsbOdebrat_Material.Size = new System.Drawing.Size(23, 22);
            this.tsbOdebrat_Material.Text = "Odebrat";
            this.tsbOdebrat_Material.Click += new System.EventHandler(this.tsbOdebrat_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbVycistit_Material
            // 
            this.tsbVycistit_Material.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit_Material.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit_Material.Image")));
            this.tsbVycistit_Material.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit_Material.Name = "tsbVycistit_Material";
            this.tsbVycistit_Material.Size = new System.Drawing.Size(23, 22);
            this.tsbVycistit_Material.Text = "Vyčistit";
            this.tsbVycistit_Material.Click += new System.EventHandler(this.tsbVycistit_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Měrná jednotka:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Pol. Číslo:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(51, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Popis :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Menu,
            this.výstupToolStripMenuItem,
            this.tsmi_Material,
            this.tsmi_Polotovar,
            this.tsmi_vyrobek,
            this.tsmiAkce});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(735, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // tsmi_Menu
            // 
            this.tsmi_Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_konec,
            this.tsmi_Modifikace_TP});
            this.tsmi_Menu.Name = "tsmi_Menu";
            this.tsmi_Menu.Size = new System.Drawing.Size(50, 20);
            this.tsmi_Menu.Text = "Menu";
            // 
            // tsmi_konec
            // 
            this.tsmi_konec.Name = "tsmi_konec";
            this.tsmi_konec.Size = new System.Drawing.Size(149, 22);
            this.tsmi_konec.Text = "Konec";
            this.tsmi_konec.Click += new System.EventHandler(this.PerformCancel_Click);
            // 
            // tsmi_Modifikace_TP
            // 
            this.tsmi_Modifikace_TP.Name = "tsmi_Modifikace_TP";
            this.tsmi_Modifikace_TP.Size = new System.Drawing.Size(149, 22);
            this.tsmi_Modifikace_TP.Text = "Modifikace TP";
            this.tsmi_Modifikace_TP.Click += new System.EventHandler(this.PerformModifikace_TP_Click);
            // 
            // výstupToolStripMenuItem
            // 
            this.výstupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiskToolStripMenuItem});
            this.výstupToolStripMenuItem.Name = "výstupToolStripMenuItem";
            this.výstupToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.výstupToolStripMenuItem.Text = "Výstup";
            // 
            // tiskToolStripMenuItem
            // 
            this.tiskToolStripMenuItem.Name = "tiskToolStripMenuItem";
            this.tiskToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+P, Ctrl+R";
            this.tiskToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tiskToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.tiskToolStripMenuItem.Text = "Tisk";
            this.tiskToolStripMenuItem.Click += new System.EventHandler(this.tiskToolStripMenuItem_Click);
            // 
            // tsmi_Material
            // 
            this.tsmi_Material.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_odstranitMaterial,
            this.tsmi_upravitMaterial,
            this.tsmi_pridatMaterial});
            this.tsmi_Material.Name = "tsmi_Material";
            this.tsmi_Material.Size = new System.Drawing.Size(59, 20);
            this.tsmi_Material.Text = "Materál";
            // 
            // tsmi_odstranitMaterial
            // 
            this.tsmi_odstranitMaterial.Name = "tsmi_odstranitMaterial";
            this.tsmi_odstranitMaterial.Size = new System.Drawing.Size(169, 22);
            this.tsmi_odstranitMaterial.Text = "Odstranit materiál";
            this.tsmi_odstranitMaterial.Click += new System.EventHandler(this.PerformOdstranitMaterial_Click);
            // 
            // tsmi_upravitMaterial
            // 
            this.tsmi_upravitMaterial.Name = "tsmi_upravitMaterial";
            this.tsmi_upravitMaterial.Size = new System.Drawing.Size(169, 22);
            this.tsmi_upravitMaterial.Text = "Upravit materiál";
            this.tsmi_upravitMaterial.Click += new System.EventHandler(this.PerformEditMaterial_Click);
            // 
            // tsmi_pridatMaterial
            // 
            this.tsmi_pridatMaterial.Name = "tsmi_pridatMaterial";
            this.tsmi_pridatMaterial.Size = new System.Drawing.Size(169, 22);
            this.tsmi_pridatMaterial.Text = "Přidat materiál";
            this.tsmi_pridatMaterial.Click += new System.EventHandler(this.PerformPridatMaterial_Click);
            // 
            // tsmi_Polotovar
            // 
            this.tsmi_Polotovar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_PridatPolotovar});
            this.tsmi_Polotovar.Name = "tsmi_Polotovar";
            this.tsmi_Polotovar.Size = new System.Drawing.Size(70, 20);
            this.tsmi_Polotovar.Text = "Polotovar";
            // 
            // tsmi_PridatPolotovar
            // 
            this.tsmi_PridatPolotovar.Name = "tsmi_PridatPolotovar";
            this.tsmi_PridatPolotovar.Size = new System.Drawing.Size(159, 22);
            this.tsmi_PridatPolotovar.Text = "Přidat polotovar";
            this.tsmi_PridatPolotovar.Click += new System.EventHandler(this.PerformAddPolotovar_Click);
            // 
            // tsmi_vyrobek
            // 
            this.tsmi_vyrobek.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_duplikovatVyrobek,
            this.tsmi_odstranitVyrobek,
            this.tsmi_pridatVyrobek});
            this.tsmi_vyrobek.Name = "tsmi_vyrobek";
            this.tsmi_vyrobek.Size = new System.Drawing.Size(62, 20);
            this.tsmi_vyrobek.Text = "Vyrobek";
            // 
            // tsmi_duplikovatVyrobek
            // 
            this.tsmi_duplikovatVyrobek.Name = "tsmi_duplikovatVyrobek";
            this.tsmi_duplikovatVyrobek.Size = new System.Drawing.Size(168, 22);
            this.tsmi_duplikovatVyrobek.Text = "Duplikovat";
            this.tsmi_duplikovatVyrobek.Click += new System.EventHandler(this.PerformDuplikace_Click);
            // 
            // tsmi_odstranitVyrobek
            // 
            this.tsmi_odstranitVyrobek.Name = "tsmi_odstranitVyrobek";
            this.tsmi_odstranitVyrobek.Size = new System.Drawing.Size(168, 22);
            this.tsmi_odstranitVyrobek.Text = "Odstranit výrobek";
            this.tsmi_odstranitVyrobek.Click += new System.EventHandler(this.PerformOdstranitVyrobek_Click);
            // 
            // tsmi_pridatVyrobek
            // 
            this.tsmi_pridatVyrobek.Name = "tsmi_pridatVyrobek";
            this.tsmi_pridatVyrobek.Size = new System.Drawing.Size(168, 22);
            this.tsmi_pridatVyrobek.Text = "Přidat výrobek";
            this.tsmi_pridatVyrobek.Click += new System.EventHandler(this.PerformPridatVyrobek_Click);
            // 
            // tsmiAkce
            // 
            this.tsmiAkce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiImportovatMaterialy});
            this.tsmiAkce.Name = "tsmiAkce";
            this.tsmiAkce.Size = new System.Drawing.Size(45, 20);
            this.tsmiAkce.Text = "Akce";
            // 
            // tsmiImportovatMaterialy
            // 
            this.tsmiImportovatMaterialy.Name = "tsmiImportovatMaterialy";
            this.tsmiImportovatMaterialy.Size = new System.Drawing.Size(133, 22);
            this.tsmiImportovatMaterialy.Text = "Importovat";
            this.tsmiImportovatMaterialy.Click += new System.EventHandler(this.tsmiImport_Vyrobky_Click);
            // 
            // bw_Materialy_stav
            // 
            this.bw_Materialy_stav.WorkerSupportsCancellation = true;
            this.bw_Materialy_stav.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Materialy_stav_DoWork);
            this.bw_Materialy_stav.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Materialy_stav_RunWorkerCompleted);
            // 
            // bw_Vyrobky_stav
            // 
            this.bw_Vyrobky_stav.WorkerSupportsCancellation = true;
            this.bw_Vyrobky_stav.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Vyrobky_stav_DoWork);
            this.bw_Vyrobky_stav.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Vyrobky_stav_RunWorkerCompleted);
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(735, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 674);
            this.panelButtons.TabIndex = 2;
            // 
            // bwImportVyrobky
            // 
            this.bwImportVyrobky.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwImportVyrobky_DoWork);
            this.bwImportVyrobky.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwImportVyrobky_RunWorkerCompleted);
            // 
            // FormVazbyMaterialy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 674);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormVazbyMaterialy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vazby Materialy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVazbyMaterialy_FormClosing);
            this.Load += new System.EventHandler(this.FormVazbyMaterialy_Load);
            this.Shown += new System.EventHandler(this.FormVazbyMaterialy_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormVazbyMaterialy_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgVyrobky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVyrobky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVyrobky)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel_FiltryVyrobky.ResumeLayout(false);
            this.panel_FiltryVyrobky.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMaterialy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMaterialy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMaterialy)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgVyrobky;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Menu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_konec;
        private Zuby.ADGV.AdvancedDataGridView dgMaterialy;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.BindingSource bsVyrobky;
        private System.Windows.Forms.BindingSource bsMaterialy;
        private System.Windows.Forms.Button button_Filtr_Vyrobky;
        private System.Windows.Forms.Panel panel_FiltryVyrobky;
        private ProgressControls.ProgressIndicator progressIndicatorMaterialy;
        private System.ComponentModel.BackgroundWorker bw_Materialy_stav;
        private System.ComponentModel.BackgroundWorker bw_Vyrobky_stav;
        private System.Windows.Forms.ComboBox comboBox_Vyrobek_MJ;
        private System.Windows.Forms.ComboBox comboBox_Vyrobek_ITEMDESC;
        private System.Windows.Forms.ComboBox comboBox_Vyrobek_ITEMNMBR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tscbFiltry_Vyrobek;
        private System.Windows.Forms.ToolStripButton tsbNastavit_Vyrobek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbZmena_Vyrobek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbPridat_Vyrobek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton tsbOdebrat_Vyrobek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton tsbVycistit_Vyrobek;
        private Fask.Interfaces.DataSets.Vyroba dsVyrobky;
        private Fask.Interfaces.DataSets.Vyroba dsMaterialy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox_MJ;
        private System.Windows.Forms.ComboBox comboBox_ITEMDESC;
        private System.Windows.Forms.Button button_filtr_Material;
        private System.Windows.Forms.ComboBox comboBox_ITEMNMBR;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbFiltry_Material;
        private System.Windows.Forms.ToolStripButton tsbNastavit_Material;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbZmena_Material;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbPridat_Material;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbOdebrat_Material;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbVycistit_Material;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private ProgressControls.ProgressIndicator progressIndicatorVyrobky;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Material;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pridatMaterial;
        private System.Windows.Forms.ToolStripMenuItem tsmi_upravitMaterial;
        private System.Windows.Forms.ToolStripMenuItem tsmi_odstranitMaterial;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Polotovar;
        private System.Windows.Forms.ToolStripMenuItem tsmi_PridatPolotovar;
        private System.Windows.Forms.ToolStripMenuItem tsmi_vyrobek;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pridatVyrobek;
        private System.Windows.Forms.ToolStripMenuItem tsmi_odstranitVyrobek;
        private System.Windows.Forms.ToolStripMenuItem tsmi_duplikovatVyrobek;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Modifikace_TP;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBarVyrobky;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBarMaterialy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_H;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_L;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNDITNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn PUO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vetev1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vetev2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vetev3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vetev4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vetev5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vetev6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vetev7;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_H_;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_L_;
        private System.Windows.Forms.DataGridViewTextBoxColumn koef_;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_USER_;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateedit_;
        private System.Windows.Forms.DataGridViewTextBoxColumn alter_;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR_;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC_;
        private System.Windows.Forms.DataGridViewTextBoxColumn MJ_;
        private System.Windows.Forms.DataGridViewTextBoxColumn PUO_;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_ID_;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_DESC_;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.ToolStripMenuItem výstupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAkce;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportovatMaterialy;
        private System.ComponentModel.BackgroundWorker bwImportVyrobky;
    }
}