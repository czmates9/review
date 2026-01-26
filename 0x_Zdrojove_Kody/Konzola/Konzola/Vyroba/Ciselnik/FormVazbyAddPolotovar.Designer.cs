namespace Konzola.Vyroba
{
    partial class FormVazbyAddPolotovar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVazbyAddPolotovar));
            this.bsProduct = new System.Windows.Forms.BindingSource(this.components);
            this.dsProduct = new Fask.Interfaces.DataSets.Vyroba();
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.buttonOznacitVse = new System.Windows.Forms.Button();
            this.dgProduct = new Zuby.ADGV.AdvancedDataGridView();
            this.Tab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelhlavni = new System.Windows.Forms.Panel();
            this.progressIndicatorAdd = new ProgressControls.ProgressIndicator();
            this.advancedDataGridViewSearchToolBar_Product = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox_AddMaterial_MJ = new System.Windows.Forms.ComboBox();
            this.comboBox_AddMaterial_ITEMDESC = new System.Windows.Forms.ComboBox();
            this.comboBox_AddMaterial_ITEMNMBR = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button_Filtr_AddMaterial = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFiltry_AddMaterial = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNastavit_AddMaterial = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZmena_AddMaterial = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPridat_AddMaterial = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdebrat_AddMaterial = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVycistit_AddMaterial = new System.Windows.Forms.ToolStripButton();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.buttonPridat = new System.Windows.Forms.Button();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.progressIndicatorPridane = new ProgressControls.ProgressIndicator();
            this.dgPridane = new Zuby.ADGV.AdvancedDataGridView();
            this.bsPridane = new System.Windows.Forms.BindingSource(this.components);
            this.dsPridane = new Fask.Interfaces.DataSets.Vyroba();
            this.advancedDataGridViewSearchToolBar_Pridane = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_P_Material_MJ = new System.Windows.Forms.ComboBox();
            this.comboBox_P_Material_ITEMDESC = new System.Windows.Forms.ComboBox();
            this.comboBox_P_Material_ITEMNMBR = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button_Filtr_P_Material = new System.Windows.Forms.Button();
            this.tscbFiltry = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFiltry_pridane_Materialy = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNastavit_pridane_Materialy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZmena_pridane_Materialy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPridat_pridane_Materialy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdebrat_pridane_Materialy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVycistit_pridane_Materialy = new System.Windows.Forms.ToolStripButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_main = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bw_Add_Material = new System.ComponentModel.BackgroundWorker();
            this.bw_P_Material = new System.ComponentModel.BackgroundWorker();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_L = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.koef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_USER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateedit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PUO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_H_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PUO_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_L_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MJ_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgProduct)).BeginInit();
            this.Tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelhlavni.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPridane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPridane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPridane)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tscbFiltry.SuspendLayout();
            this.panel_main.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bsProduct
            // 
            this.bsProduct.DataMember = "FASK_Vyroba_TP_Vyrobek";
            this.bsProduct.DataSource = this.dsProduct;
            // 
            // dsProduct
            // 
            this.dsProduct.DataSetName = "VyrobaDataSet";
            this.dsProduct.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdznacitVse.Location = new System.Drawing.Point(717, 104);
            this.buttonOdznacitVse.Name = "buttonOdznacitVse";
            this.buttonOdznacitVse.Size = new System.Drawing.Size(83, 29);
            this.buttonOdznacitVse.TabIndex = 13;
            this.buttonOdznacitVse.Text = "Odznačit vše";
            this.buttonOdznacitVse.UseVisualStyleBackColor = true;
            this.buttonOdznacitVse.Click += new System.EventHandler(this.buttonOdznacitVse_Click);
            // 
            // buttonOznacitVse
            // 
            this.buttonOznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOznacitVse.Location = new System.Drawing.Point(628, 104);
            this.buttonOznacitVse.Name = "buttonOznacitVse";
            this.buttonOznacitVse.Size = new System.Drawing.Size(83, 29);
            this.buttonOznacitVse.TabIndex = 12;
            this.buttonOznacitVse.Text = "Označit vše";
            this.buttonOznacitVse.UseVisualStyleBackColor = true;
            this.buttonOznacitVse.Click += new System.EventHandler(this.buttonOznacitVse_Click);
            // 
            // dgProduct
            // 
            this.dgProduct.AllowUserToAddRows = false;
            this.dgProduct.AllowUserToDeleteRows = false;
            this.dgProduct.AllowUserToOrderColumns = true;
            this.dgProduct.AllowUserToResizeRows = false;
            this.dgProduct.AutoGenerateColumns = false;
            this.dgProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_H_,
            this.PUO_,
            this.ID_L_,
            this.ID_,
            this.ITEMNMBR_,
            this.ITEMDESC_,
            this.MJ_});
            this.dgProduct.DataSource = this.bsProduct;
            this.dgProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgProduct.EnableHeadersVisualStyles = false;
            this.dgProduct.FilterAndSortEnabled = true;
            this.dgProduct.Location = new System.Drawing.Point(0, 172);
            this.dgProduct.Name = "dgProduct";
            this.dgProduct.ReadOnly = true;
            this.dgProduct.RowHeadersVisible = false;
            this.dgProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgProduct.Size = new System.Drawing.Size(806, 354);
            this.dgProduct.TabIndex = 30;
            // 
            // Tab
            // 
            this.Tab.Controls.Add(this.tabPage1);
            this.Tab.Controls.Add(this.tabPage2);
            this.Tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab.Location = new System.Drawing.Point(0, 0);
            this.Tab.Name = "Tab";
            this.Tab.SelectedIndex = 0;
            this.Tab.Size = new System.Drawing.Size(919, 558);
            this.Tab.TabIndex = 9;
            this.Tab.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelhlavni);
            this.tabPage1.Controls.Add(this.panelLeft);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(911, 532);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Přidat záznam";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelhlavni
            // 
            this.panelhlavni.Controls.Add(this.progressIndicatorAdd);
            this.panelhlavni.Controls.Add(this.dgProduct);
            this.panelhlavni.Controls.Add(this.advancedDataGridViewSearchToolBar_Product);
            this.panelhlavni.Controls.Add(this.groupBox2);
            this.panelhlavni.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelhlavni.Location = new System.Drawing.Point(3, 3);
            this.panelhlavni.Name = "panelhlavni";
            this.panelhlavni.Size = new System.Drawing.Size(806, 526);
            this.panelhlavni.TabIndex = 33;
            // 
            // progressIndicatorAdd
            // 
            this.progressIndicatorAdd.Location = new System.Drawing.Point(350, 308);
            this.progressIndicatorAdd.Name = "progressIndicatorAdd";
            this.progressIndicatorAdd.Percentage = 0F;
            this.progressIndicatorAdd.Size = new System.Drawing.Size(106, 106);
            this.progressIndicatorAdd.TabIndex = 41;
            this.progressIndicatorAdd.Text = "progressIndicator1";
            this.progressIndicatorAdd.Visible = false;
            // 
            // advancedDataGridViewSearchToolBar_Product
            // 
            this.advancedDataGridViewSearchToolBar_Product.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar_Product.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar_Product.Location = new System.Drawing.Point(0, 145);
            this.advancedDataGridViewSearchToolBar_Product.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_Product.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_Product.Name = "advancedDataGridViewSearchToolBar_Product";
            this.advancedDataGridViewSearchToolBar_Product.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar_Product.Size = new System.Drawing.Size(806, 27);
            this.advancedDataGridViewSearchToolBar_Product.TabIndex = 42;
            this.advancedDataGridViewSearchToolBar_Product.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar_Product.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar_Product_Search);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox_AddMaterial_MJ);
            this.groupBox2.Controls.Add(this.comboBox_AddMaterial_ITEMDESC);
            this.groupBox2.Controls.Add(this.comboBox_AddMaterial_ITEMNMBR);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.button_Filtr_AddMaterial);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Controls.Add(this.buttonOznacitVse);
            this.groupBox2.Controls.Add(this.buttonOdznacitVse);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(806, 145);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter";
            // 
            // comboBox_AddMaterial_MJ
            // 
            this.comboBox_AddMaterial_MJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_AddMaterial_MJ.FormattingEnabled = true;
            this.comboBox_AddMaterial_MJ.Location = new System.Drawing.Point(102, 108);
            this.comboBox_AddMaterial_MJ.Name = "comboBox_AddMaterial_MJ";
            this.comboBox_AddMaterial_MJ.Size = new System.Drawing.Size(127, 21);
            this.comboBox_AddMaterial_MJ.TabIndex = 18;
            // 
            // comboBox_AddMaterial_ITEMDESC
            // 
            this.comboBox_AddMaterial_ITEMDESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_AddMaterial_ITEMDESC.FormattingEnabled = true;
            this.comboBox_AddMaterial_ITEMDESC.Location = new System.Drawing.Point(102, 82);
            this.comboBox_AddMaterial_ITEMDESC.Name = "comboBox_AddMaterial_ITEMDESC";
            this.comboBox_AddMaterial_ITEMDESC.Size = new System.Drawing.Size(127, 21);
            this.comboBox_AddMaterial_ITEMDESC.TabIndex = 20;
            // 
            // comboBox_AddMaterial_ITEMNMBR
            // 
            this.comboBox_AddMaterial_ITEMNMBR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_AddMaterial_ITEMNMBR.FormattingEnabled = true;
            this.comboBox_AddMaterial_ITEMNMBR.Location = new System.Drawing.Point(102, 55);
            this.comboBox_AddMaterial_ITEMNMBR.Name = "comboBox_AddMaterial_ITEMNMBR";
            this.comboBox_AddMaterial_ITEMNMBR.Size = new System.Drawing.Size(127, 21);
            this.comboBox_AddMaterial_ITEMNMBR.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Měrná jednotka:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Pol. Číslo:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Popis :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_Filtr_AddMaterial
            // 
            this.button_Filtr_AddMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Filtr_AddMaterial.Location = new System.Drawing.Point(235, 55);
            this.button_Filtr_AddMaterial.Name = "button_Filtr_AddMaterial";
            this.button_Filtr_AddMaterial.Size = new System.Drawing.Size(85, 75);
            this.button_Filtr_AddMaterial.TabIndex = 16;
            this.button_Filtr_AddMaterial.Text = "Vyhledat";
            this.button_Filtr_AddMaterial.UseVisualStyleBackColor = true;
            this.button_Filtr_AddMaterial.Click += new System.EventHandler(this.button_filtr_ADD_Material_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbFiltry_AddMaterial,
            this.tsbNastavit_AddMaterial,
            this.toolStripSeparator1,
            this.tsbZmena_AddMaterial,
            this.toolStripSeparator2,
            this.tsbPridat_AddMaterial,
            this.toolStripSeparator3,
            this.tsbOdebrat_AddMaterial,
            this.toolStripSeparator4,
            this.tsbVycistit_AddMaterial});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Click += new System.EventHandler(this.tsbNastavit_ADD_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Filtry:";
            // 
            // tscbFiltry_AddMaterial
            // 
            this.tscbFiltry_AddMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry_AddMaterial.Name = "tscbFiltry_AddMaterial";
            this.tscbFiltry_AddMaterial.Size = new System.Drawing.Size(121, 25);
            // 
            // tsbNastavit_AddMaterial
            // 
            this.tsbNastavit_AddMaterial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit_AddMaterial.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit_AddMaterial.Image")));
            this.tsbNastavit_AddMaterial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit_AddMaterial.Name = "tsbNastavit_AddMaterial";
            this.tsbNastavit_AddMaterial.Size = new System.Drawing.Size(23, 22);
            this.tsbNastavit_AddMaterial.Text = "Nastavit";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbZmena_AddMaterial
            // 
            this.tsbZmena_AddMaterial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena_AddMaterial.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena_AddMaterial.Image")));
            this.tsbZmena_AddMaterial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena_AddMaterial.Name = "tsbZmena_AddMaterial";
            this.tsbZmena_AddMaterial.Size = new System.Drawing.Size(23, 22);
            this.tsbZmena_AddMaterial.Text = "Změna";
            this.tsbZmena_AddMaterial.Click += new System.EventHandler(this.tsbZmena_ADD_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPridat_AddMaterial
            // 
            this.tsbPridat_AddMaterial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat_AddMaterial.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat_AddMaterial.Image")));
            this.tsbPridat_AddMaterial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat_AddMaterial.Name = "tsbPridat_AddMaterial";
            this.tsbPridat_AddMaterial.Size = new System.Drawing.Size(23, 22);
            this.tsbPridat_AddMaterial.Text = "Uložit";
            this.tsbPridat_AddMaterial.Click += new System.EventHandler(this.tsbPridat_ADD_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOdebrat_AddMaterial
            // 
            this.tsbOdebrat_AddMaterial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat_AddMaterial.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat_AddMaterial.Image")));
            this.tsbOdebrat_AddMaterial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat_AddMaterial.Name = "tsbOdebrat_AddMaterial";
            this.tsbOdebrat_AddMaterial.Size = new System.Drawing.Size(23, 22);
            this.tsbOdebrat_AddMaterial.Text = "Odebrat";
            this.tsbOdebrat_AddMaterial.Click += new System.EventHandler(this.tsbOdebrat_ADD_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbVycistit_AddMaterial
            // 
            this.tsbVycistit_AddMaterial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit_AddMaterial.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit_AddMaterial.Image")));
            this.tsbVycistit_AddMaterial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit_AddMaterial.Name = "tsbVycistit_AddMaterial";
            this.tsbVycistit_AddMaterial.Size = new System.Drawing.Size(23, 22);
            this.tsbVycistit_AddMaterial.Text = "Vyčistit";
            this.tsbVycistit_AddMaterial.Click += new System.EventHandler(this.tsbVycistit_ADD_Click);
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.buttonPridat);
            this.panelLeft.Controls.Add(this.buttonKonec);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelLeft.Location = new System.Drawing.Point(809, 3);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(99, 526);
            this.panelLeft.TabIndex = 32;
            // 
            // buttonPridat
            // 
            this.buttonPridat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPridat.Location = new System.Drawing.Point(16, 16);
            this.buttonPridat.Name = "buttonPridat";
            this.buttonPridat.Size = new System.Drawing.Size(72, 63);
            this.buttonPridat.TabIndex = 12;
            this.buttonPridat.Text = "Přidat";
            this.buttonPridat.UseVisualStyleBackColor = true;
            this.buttonPridat.Click += new System.EventHandler(this.buttonPridat_Click);
            // 
            // buttonKonec
            // 
            this.buttonKonec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKonec.Location = new System.Drawing.Point(16, 449);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(72, 63);
            this.buttonKonec.TabIndex = 11;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Click += new System.EventHandler(this.buttonKonec_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(911, 532);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Přidané vazby";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.progressIndicatorPridane);
            this.panel4.Controls.Add(this.dgPridane);
            this.panel4.Controls.Add(this.advancedDataGridViewSearchToolBar_Pridane);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(905, 526);
            this.panel4.TabIndex = 2;
            // 
            // progressIndicatorPridane
            // 
            this.progressIndicatorPridane.Location = new System.Drawing.Point(337, 322);
            this.progressIndicatorPridane.Name = "progressIndicatorPridane";
            this.progressIndicatorPridane.Percentage = 0F;
            this.progressIndicatorPridane.Size = new System.Drawing.Size(103, 103);
            this.progressIndicatorPridane.TabIndex = 41;
            this.progressIndicatorPridane.Text = "progressIndicator1";
            this.progressIndicatorPridane.Visible = false;
            // 
            // dgPridane
            // 
            this.dgPridane.AllowUserToAddRows = false;
            this.dgPridane.AllowUserToDeleteRows = false;
            this.dgPridane.AllowUserToOrderColumns = true;
            this.dgPridane.AllowUserToResizeRows = false;
            this.dgPridane.AutoGenerateColumns = false;
            this.dgPridane.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPridane.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ID_H,
            this.ID_L,
            this.koef,
            this.ID_USER,
            this.dateedit,
            this.alter,
            this.PUO,
            this.ITEMNMBR,
            this.ITEMDESC,
            this.MJ});
            this.dgPridane.DataSource = this.bsPridane;
            this.dgPridane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPridane.EnableHeadersVisualStyles = false;
            this.dgPridane.FilterAndSortEnabled = true;
            this.dgPridane.Location = new System.Drawing.Point(0, 169);
            this.dgPridane.Name = "dgPridane";
            this.dgPridane.ReadOnly = true;
            this.dgPridane.RowHeadersVisible = false;
            this.dgPridane.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPridane.Size = new System.Drawing.Size(905, 357);
            this.dgPridane.TabIndex = 0;
            // 
            // bsPridane
            // 
            this.bsPridane.DataMember = "FASK_Vyroba_TP_Material";
            this.bsPridane.DataSource = this.dsPridane;
            // 
            // dsPridane
            // 
            this.dsPridane.DataSetName = "VyrobaDataSet";
            this.dsPridane.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar_Pridane
            // 
            this.advancedDataGridViewSearchToolBar_Pridane.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar_Pridane.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar_Pridane.Location = new System.Drawing.Point(0, 142);
            this.advancedDataGridViewSearchToolBar_Pridane.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_Pridane.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_Pridane.Name = "advancedDataGridViewSearchToolBar_Pridane";
            this.advancedDataGridViewSearchToolBar_Pridane.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar_Pridane.Size = new System.Drawing.Size(905, 27);
            this.advancedDataGridViewSearchToolBar_Pridane.TabIndex = 42;
            this.advancedDataGridViewSearchToolBar_Pridane.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar_Pridane.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar_Pridane_Search);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_P_Material_MJ);
            this.groupBox1.Controls.Add(this.comboBox_P_Material_ITEMDESC);
            this.groupBox1.Controls.Add(this.comboBox_P_Material_ITEMNMBR);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.button_Filtr_P_Material);
            this.groupBox1.Controls.Add(this.tscbFiltry);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(905, 142);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // comboBox_P_Material_MJ
            // 
            this.comboBox_P_Material_MJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_P_Material_MJ.FormattingEnabled = true;
            this.comboBox_P_Material_MJ.Location = new System.Drawing.Point(102, 108);
            this.comboBox_P_Material_MJ.Name = "comboBox_P_Material_MJ";
            this.comboBox_P_Material_MJ.Size = new System.Drawing.Size(127, 21);
            this.comboBox_P_Material_MJ.TabIndex = 31;
            // 
            // comboBox_P_Material_ITEMDESC
            // 
            this.comboBox_P_Material_ITEMDESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_P_Material_ITEMDESC.FormattingEnabled = true;
            this.comboBox_P_Material_ITEMDESC.Location = new System.Drawing.Point(102, 82);
            this.comboBox_P_Material_ITEMDESC.Name = "comboBox_P_Material_ITEMDESC";
            this.comboBox_P_Material_ITEMDESC.Size = new System.Drawing.Size(127, 21);
            this.comboBox_P_Material_ITEMDESC.TabIndex = 33;
            // 
            // comboBox_P_Material_ITEMNMBR
            // 
            this.comboBox_P_Material_ITEMNMBR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_P_Material_ITEMNMBR.FormattingEnabled = true;
            this.comboBox_P_Material_ITEMNMBR.Location = new System.Drawing.Point(102, 55);
            this.comboBox_P_Material_ITEMNMBR.Name = "comboBox_P_Material_ITEMNMBR";
            this.comboBox_P_Material_ITEMNMBR.Size = new System.Drawing.Size(127, 21);
            this.comboBox_P_Material_ITEMNMBR.TabIndex = 32;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 41;
            this.label10.Text = "Měrná jednotka:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(41, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 36;
            this.label11.Text = "Pol. Číslo:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(57, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 13);
            this.label12.TabIndex = 37;
            this.label12.Text = "Popis :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_Filtr_P_Material
            // 
            this.button_Filtr_P_Material.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Filtr_P_Material.Location = new System.Drawing.Point(235, 55);
            this.button_Filtr_P_Material.Name = "button_Filtr_P_Material";
            this.button_Filtr_P_Material.Size = new System.Drawing.Size(85, 75);
            this.button_Filtr_P_Material.TabIndex = 29;
            this.button_Filtr_P_Material.Text = "Vyhledat";
            this.button_Filtr_P_Material.UseVisualStyleBackColor = true;
            this.button_Filtr_P_Material.Click += new System.EventHandler(this.button_Filtr_P_Click);
            // 
            // tscbFiltry
            // 
            this.tscbFiltry.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tscbFiltry_pridane_Materialy,
            this.tsbNastavit_pridane_Materialy,
            this.toolStripSeparator5,
            this.tsbZmena_pridane_Materialy,
            this.toolStripSeparator8,
            this.tsbPridat_pridane_Materialy,
            this.toolStripSeparator7,
            this.tsbOdebrat_pridane_Materialy,
            this.toolStripSeparator6,
            this.tsbVycistit_pridane_Materialy});
            this.tscbFiltry.Location = new System.Drawing.Point(3, 16);
            this.tscbFiltry.Name = "tscbFiltry";
            this.tscbFiltry.Size = new System.Drawing.Size(899, 25);
            this.tscbFiltry.TabIndex = 0;
            this.tscbFiltry.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel2.Text = "Filtry:";
            // 
            // tscbFiltry_pridane_Materialy
            // 
            this.tscbFiltry_pridane_Materialy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry_pridane_Materialy.Name = "tscbFiltry_pridane_Materialy";
            this.tscbFiltry_pridane_Materialy.Size = new System.Drawing.Size(121, 25);
            // 
            // tsbNastavit_pridane_Materialy
            // 
            this.tsbNastavit_pridane_Materialy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit_pridane_Materialy.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit_pridane_Materialy.Image")));
            this.tsbNastavit_pridane_Materialy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit_pridane_Materialy.Name = "tsbNastavit_pridane_Materialy";
            this.tsbNastavit_pridane_Materialy.Size = new System.Drawing.Size(23, 22);
            this.tsbNastavit_pridane_Materialy.Text = "Nastavit";
            this.tsbNastavit_pridane_Materialy.Click += new System.EventHandler(this.tsbNastavit_P_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbZmena_pridane_Materialy
            // 
            this.tsbZmena_pridane_Materialy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena_pridane_Materialy.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena_pridane_Materialy.Image")));
            this.tsbZmena_pridane_Materialy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena_pridane_Materialy.Name = "tsbZmena_pridane_Materialy";
            this.tsbZmena_pridane_Materialy.Size = new System.Drawing.Size(23, 22);
            this.tsbZmena_pridane_Materialy.Text = "Změna";
            this.tsbZmena_pridane_Materialy.Click += new System.EventHandler(this.tsbZmena_P_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPridat_pridane_Materialy
            // 
            this.tsbPridat_pridane_Materialy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat_pridane_Materialy.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat_pridane_Materialy.Image")));
            this.tsbPridat_pridane_Materialy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat_pridane_Materialy.Name = "tsbPridat_pridane_Materialy";
            this.tsbPridat_pridane_Materialy.Size = new System.Drawing.Size(23, 22);
            this.tsbPridat_pridane_Materialy.Text = "Uložit";
            this.tsbPridat_pridane_Materialy.Click += new System.EventHandler(this.tsbPridat_P_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOdebrat_pridane_Materialy
            // 
            this.tsbOdebrat_pridane_Materialy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat_pridane_Materialy.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat_pridane_Materialy.Image")));
            this.tsbOdebrat_pridane_Materialy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat_pridane_Materialy.Name = "tsbOdebrat_pridane_Materialy";
            this.tsbOdebrat_pridane_Materialy.Size = new System.Drawing.Size(23, 22);
            this.tsbOdebrat_pridane_Materialy.Text = "Odebrat";
            this.tsbOdebrat_pridane_Materialy.Click += new System.EventHandler(this.tsbOdebrat_P_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbVycistit_pridane_Materialy
            // 
            this.tsbVycistit_pridane_Materialy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit_pridane_Materialy.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit_pridane_Materialy.Image")));
            this.tsbVycistit_pridane_Materialy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit_pridane_Materialy.Name = "tsbVycistit_pridane_Materialy";
            this.tsbVycistit_pridane_Materialy.Size = new System.Drawing.Size(23, 22);
            this.tsbVycistit_pridane_Materialy.Text = "Vyčistit";
            this.tsbVycistit_pridane_Materialy.Click += new System.EventHandler(this.tsbVycistit_P_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.Tab);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 24);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(919, 558);
            this.panel_main.TabIndex = 10;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(919, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konecToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.konecToolStripMenuItem.Text = "Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.buttonKonec_Click);
            // 
            // bw_Add_Material
            // 
            this.bw_Add_Material.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_MaterialyAdd_stav_DoWork);
            this.bw_Add_Material.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_MaterialyAdd_stav_RunWorkerCompleted);
            // 
            // bw_P_Material
            // 
            this.bw_P_Material.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Vyrobky_stav_DoWork);
            this.bw_P_Material.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Vyrobky_stav_RunWorkerCompleted);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "Index";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // ID_H
            // 
            this.ID_H.DataPropertyName = "ID_H";
            this.ID_H.HeaderText = "Index vyšší";
            this.ID_H.Name = "ID_H";
            this.ID_H.ReadOnly = true;
            // 
            // ID_L
            // 
            this.ID_L.DataPropertyName = "ID_L";
            this.ID_L.HeaderText = "Index nižší";
            this.ID_L.Name = "ID_L";
            this.ID_L.ReadOnly = true;
            // 
            // koef
            // 
            this.koef.DataPropertyName = "koef";
            this.koef.HeaderText = "Koeficient";
            this.koef.Name = "koef";
            this.koef.ReadOnly = true;
            // 
            // ID_USER
            // 
            this.ID_USER.DataPropertyName = "ID_USER";
            this.ID_USER.HeaderText = "ID uživatel";
            this.ID_USER.Name = "ID_USER";
            this.ID_USER.ReadOnly = true;
            // 
            // dateedit
            // 
            this.dateedit.DataPropertyName = "dateedit";
            this.dateedit.HeaderText = "Dátum Editace";
            this.dateedit.Name = "dateedit";
            this.dateedit.ReadOnly = true;
            // 
            // alter
            // 
            this.alter.DataPropertyName = "alter";
            this.alter.HeaderText = "Alternatíva";
            this.alter.Name = "alter";
            this.alter.ReadOnly = true;
            // 
            // PUO
            // 
            this.PUO.DataPropertyName = "PUO";
            this.PUO.HeaderText = "Příznak uzlového odvádění";
            this.PUO.Name = "PUO";
            this.PUO.ReadOnly = true;
            // 
            // ITEMNMBR
            // 
            this.ITEMNMBR.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR.HeaderText = "Pol. číslo";
            this.ITEMNMBR.Name = "ITEMNMBR";
            this.ITEMNMBR.ReadOnly = true;
            // 
            // ITEMDESC
            // 
            this.ITEMDESC.DataPropertyName = "ITEMDESC";
            this.ITEMDESC.HeaderText = "Popis";
            this.ITEMDESC.Name = "ITEMDESC";
            this.ITEMDESC.ReadOnly = true;
            // 
            // MJ
            // 
            this.MJ.DataPropertyName = "MJ";
            this.MJ.HeaderText = "Měrná jednotka";
            this.MJ.Name = "MJ";
            this.MJ.ReadOnly = true;
            // 
            // ID_H_
            // 
            this.ID_H_.DataPropertyName = "ID_H";
            this.ID_H_.HeaderText = "Index vyšší";
            this.ID_H_.Name = "ID_H_";
            this.ID_H_.ReadOnly = true;
            // 
            // PUO_
            // 
            this.PUO_.DataPropertyName = "PUO";
            this.PUO_.HeaderText = "Příznak uzlového odvádění";
            this.PUO_.Name = "PUO_";
            this.PUO_.ReadOnly = true;
            // 
            // ID_L_
            // 
            this.ID_L_.DataPropertyName = "ID_L";
            this.ID_L_.HeaderText = "Index nižší";
            this.ID_L_.Name = "ID_L_";
            this.ID_L_.ReadOnly = true;
            // 
            // ID_
            // 
            this.ID_.DataPropertyName = "ID";
            this.ID_.HeaderText = "Index";
            this.ID_.Name = "ID_";
            this.ID_.ReadOnly = true;
            // 
            // ITEMNMBR_
            // 
            this.ITEMNMBR_.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR_.HeaderText = "Pol. číslo";
            this.ITEMNMBR_.Name = "ITEMNMBR_";
            this.ITEMNMBR_.ReadOnly = true;
            // 
            // ITEMDESC_
            // 
            this.ITEMDESC_.DataPropertyName = "ITEMDESC";
            this.ITEMDESC_.HeaderText = "Popis";
            this.ITEMDESC_.Name = "ITEMDESC_";
            this.ITEMDESC_.ReadOnly = true;
            // 
            // MJ_
            // 
            this.MJ_.DataPropertyName = "MJ";
            this.MJ_.HeaderText = "Měrná jednotka";
            this.MJ_.Name = "MJ_";
            this.MJ_.ReadOnly = true;
            // 
            // FormVazbyAddPolotovar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 582);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.Name = "FormVazbyAddPolotovar";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Přehled polotovary";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVazbyAddPolotovar_FormClosing);
            this.Load += new System.EventHandler(this.FormVazbyAddPolotovar_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormVazbyAddPolotovar_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.bsProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgProduct)).EndInit();
            this.Tab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelhlavni.ResumeLayout(false);
            this.panelhlavni.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPridane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPridane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPridane)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tscbFiltry.ResumeLayout(false);
            this.tscbFiltry.PerformLayout();
            this.panel_main.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsProduct;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Button buttonOznacitVse;
        private Zuby.ADGV.AdvancedDataGridView dgProduct;
        private System.Windows.Forms.Button buttonPridat;
        private System.Windows.Forms.TabControl Tab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private Zuby.ADGV.AdvancedDataGridView dgPridane;
        private System.Windows.Forms.BindingSource bsPridane;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panelhlavni;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbFiltry_AddMaterial;
        private System.Windows.Forms.ToolStripButton tsbNastavit_AddMaterial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbZmena_AddMaterial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbPridat_AddMaterial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbOdebrat_AddMaterial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbVycistit_AddMaterial;
        private System.Windows.Forms.ToolStrip tscbFiltry;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tscbFiltry_pridane_Materialy;
        private System.Windows.Forms.ToolStripButton tsbNastavit_pridane_Materialy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbZmena_pridane_Materialy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbPridat_pridane_Materialy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbOdebrat_pridane_Materialy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbVycistit_pridane_Materialy;
        private System.Windows.Forms.ComboBox comboBox_AddMaterial_MJ;
        private System.Windows.Forms.ComboBox comboBox_AddMaterial_ITEMDESC;
        private System.Windows.Forms.ComboBox comboBox_AddMaterial_ITEMNMBR;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_Filtr_AddMaterial;
        private System.Windows.Forms.ComboBox comboBox_P_Material_MJ;
        private System.Windows.Forms.ComboBox comboBox_P_Material_ITEMDESC;
        private System.Windows.Forms.ComboBox comboBox_P_Material_ITEMNMBR;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button_Filtr_P_Material;
        private ProgressControls.ProgressIndicator progressIndicatorAdd;
        private ProgressControls.ProgressIndicator progressIndicatorPridane;
        private System.ComponentModel.BackgroundWorker bw_Add_Material;
        private System.ComponentModel.BackgroundWorker bw_P_Material;
        private Fask.Interfaces.DataSets.Vyroba dsPridane;
        private Fask.Interfaces.DataSets.Vyroba dsProduct;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar_Product;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar_Pridane;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_H;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_L;
        private System.Windows.Forms.DataGridViewTextBoxColumn koef;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_USER;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateedit;
        private System.Windows.Forms.DataGridViewTextBoxColumn alter;
        private System.Windows.Forms.DataGridViewTextBoxColumn PUO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_H_;
        private System.Windows.Forms.DataGridViewTextBoxColumn PUO_;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_L_;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR_;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC_;
        private System.Windows.Forms.DataGridViewTextBoxColumn MJ_;
    }
}