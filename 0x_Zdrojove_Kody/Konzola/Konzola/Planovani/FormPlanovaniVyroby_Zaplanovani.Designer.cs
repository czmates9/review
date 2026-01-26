namespace Konzola.Planovani
{
    partial class FormPlanovaniVyroby_Zaplanovani
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPlanovaniVyroby_Zaplanovani));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Nacist = new System.Windows.Forms.Button();
            this.btn_Cancle = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.cbNezaplanovane = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dg_PV_Z = new Zuby.ADGV.AdvancedDataGridView();
            this.OBJ_NMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_COMPANY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_ForUh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_ForUh_IDS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_DATE_ZAPL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_DATE_FROM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_DATE_TO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY_Zaplanovano = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY_Zbyva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserParam_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserParam_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserParam_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserParam_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserParam_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_PV_Z = new System.Windows.Forms.BindingSource(this.components);
            this.ds_PV_Z = new Fask.Interfaces.DataSets.Vyroba_Planovani();
            this.ADGVSTB_Pol = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dg_PV_Z_H = new Zuby.ADGV.AdvancedDataGridView();
            this.OBJ_NMBR_H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_DESC_H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_COMPANY_H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_DATE_FROM_H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_DATE_TO_H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJ_ORD_H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY_H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY_Zaplanovano_H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY_Zbyva_H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_PV_Z_H = new System.Windows.Forms.BindingSource(this.components);
            this.ADGVSTB_H = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chb_Do = new System.Windows.Forms.CheckBox();
            this.chb_Od = new System.Windows.Forms.CheckBox();
            this.chb_Zapis = new System.Windows.Forms.CheckBox();
            this.rb_Do = new System.Windows.Forms.RadioButton();
            this.rb_Od = new System.Windows.Forms.RadioButton();
            this.rb_Zapis = new System.Windows.Forms.RadioButton();
            this.dtp_ZAP_DatumDo = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtp_ZAP_DatumOd = new System.Windows.Forms.DateTimePicker();
            this.dtp_DO_DatumDo = new System.Windows.Forms.DateTimePicker();
            this.dtp_OD_DatumDo = new System.Windows.Forms.DateTimePicker();
            this.tb_FormaUhrady = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_DO_DatumOd = new System.Windows.Forms.DateTimePicker();
            this.dtp_OD_DatumOd = new System.Windows.Forms.DateTimePicker();
            this.tb_Kod = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_Firma = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_SOPNUMBE = new System.Windows.Forms.TextBox();
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
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bwZaplanovani = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PV_Z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PV_Z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_PV_Z)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PV_Z_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PV_Z_H)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Nacist);
            this.panel1.Controls.Add(this.btn_Cancle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1026, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(122, 481);
            this.panel1.TabIndex = 1;
            // 
            // btn_Nacist
            // 
            this.btn_Nacist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Nacist.Location = new System.Drawing.Point(5, 12);
            this.btn_Nacist.Name = "btn_Nacist";
            this.btn_Nacist.Size = new System.Drawing.Size(111, 63);
            this.btn_Nacist.TabIndex = 20;
            this.btn_Nacist.Text = "Načíst";
            this.btn_Nacist.UseVisualStyleBackColor = true;
            this.btn_Nacist.Click += new System.EventHandler(this.btn_Nacist_Click);
            // 
            // btn_Cancle
            // 
            this.btn_Cancle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancle.Location = new System.Drawing.Point(5, 406);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Size = new System.Drawing.Size(111, 63);
            this.btn_Cancle.TabIndex = 18;
            this.btn_Cancle.Text = "Konec";
            this.btn_Cancle.UseVisualStyleBackColor = true;
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Refresh.Location = new System.Drawing.Point(904, 32);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(111, 47);
            this.btn_Refresh.TabIndex = 19;
            this.btn_Refresh.Text = "Aktualizovat";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // cbNezaplanovane
            // 
            this.cbNezaplanovane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbNezaplanovane.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbNezaplanovane.Checked = true;
            this.cbNezaplanovane.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNezaplanovane.Location = new System.Drawing.Point(102, 142);
            this.cbNezaplanovane.Name = "cbNezaplanovane";
            this.cbNezaplanovane.Size = new System.Drawing.Size(133, 18);
            this.cbNezaplanovane.TabIndex = 21;
            this.cbNezaplanovane.Text = "Jen nezaplánované";
            this.cbNezaplanovane.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbNezaplanovane.UseVisualStyleBackColor = true;
            this.cbNezaplanovane.CheckedChanged += new System.EventHandler(this.cbNezaplanovane_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 192);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1026, 289);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dg_PV_Z);
            this.tabPage1.Controls.Add(this.ADGVSTB_Pol);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1018, 263);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Přehled";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dg_PV_Z
            // 
            this.dg_PV_Z.AllowUserToAddRows = false;
            this.dg_PV_Z.AllowUserToDeleteRows = false;
            this.dg_PV_Z.AllowUserToResizeRows = false;
            this.dg_PV_Z.AutoGenerateColumns = false;
            this.dg_PV_Z.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_PV_Z.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OBJ_NMBR,
            this.OBJ_DESC,
            this.OBJ_COMPANY,
            this.OBJ_ForUh,
            this.OBJ_ForUh_IDS,
            this.OBJ_DATE_ZAPL,
            this.OBJ_DATE_FROM,
            this.OBJ_DATE_TO,
            this.ITEMCODE,
            this.ITEMDESC,
            this.QTY,
            this.QTY_Zaplanovano,
            this.QTY_Zbyva,
            this.UserParam_1,
            this.UserParam_2,
            this.UserParam_3,
            this.UserParam_4,
            this.UserParam_5});
            this.dg_PV_Z.DataSource = this.bs_PV_Z;
            this.dg_PV_Z.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_PV_Z.FilterAndSortEnabled = true;
            this.dg_PV_Z.Location = new System.Drawing.Point(3, 30);
            this.dg_PV_Z.Name = "dg_PV_Z";
            this.dg_PV_Z.ReadOnly = true;
            this.dg_PV_Z.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_PV_Z.Size = new System.Drawing.Size(1012, 230);
            this.dg_PV_Z.TabIndex = 0;
            // 
            // OBJ_NMBR
            // 
            this.OBJ_NMBR.DataPropertyName = "OBJ_NMBR";
            this.OBJ_NMBR.HeaderText = "Číslo objednávky";
            this.OBJ_NMBR.Name = "OBJ_NMBR";
            this.OBJ_NMBR.ReadOnly = true;
            // 
            // OBJ_DESC
            // 
            this.OBJ_DESC.DataPropertyName = "OBJ_DESC";
            this.OBJ_DESC.HeaderText = "Popis objednávky";
            this.OBJ_DESC.Name = "OBJ_DESC";
            this.OBJ_DESC.ReadOnly = true;
            // 
            // OBJ_COMPANY
            // 
            this.OBJ_COMPANY.DataPropertyName = "OBJ_COMPANY";
            this.OBJ_COMPANY.HeaderText = "Firma";
            this.OBJ_COMPANY.Name = "OBJ_COMPANY";
            this.OBJ_COMPANY.ReadOnly = true;
            // 
            // OBJ_ForUh
            // 
            this.OBJ_ForUh.DataPropertyName = "OBJ_ForUh";
            this.OBJ_ForUh.HeaderText = "ID Formy úhrady";
            this.OBJ_ForUh.Name = "OBJ_ForUh";
            this.OBJ_ForUh.ReadOnly = true;
            // 
            // OBJ_ForUh_IDS
            // 
            this.OBJ_ForUh_IDS.DataPropertyName = "OBJ_ForUh_IDS";
            this.OBJ_ForUh_IDS.HeaderText = "Forma úhrady";
            this.OBJ_ForUh_IDS.Name = "OBJ_ForUh_IDS";
            this.OBJ_ForUh_IDS.ReadOnly = true;
            // 
            // OBJ_DATE_ZAPL
            // 
            this.OBJ_DATE_ZAPL.DataPropertyName = "OBJ_DATE_ZAPL";
            this.OBJ_DATE_ZAPL.HeaderText = "Datum zápisu";
            this.OBJ_DATE_ZAPL.Name = "OBJ_DATE_ZAPL";
            this.OBJ_DATE_ZAPL.ReadOnly = true;
            // 
            // OBJ_DATE_FROM
            // 
            this.OBJ_DATE_FROM.DataPropertyName = "OBJ_DATE_FROM";
            this.OBJ_DATE_FROM.HeaderText = "Datum od";
            this.OBJ_DATE_FROM.Name = "OBJ_DATE_FROM";
            this.OBJ_DATE_FROM.ReadOnly = true;
            // 
            // OBJ_DATE_TO
            // 
            this.OBJ_DATE_TO.DataPropertyName = "OBJ_DATE_TO";
            this.OBJ_DATE_TO.HeaderText = "Datum do";
            this.OBJ_DATE_TO.Name = "OBJ_DATE_TO";
            this.OBJ_DATE_TO.ReadOnly = true;
            // 
            // ITEMCODE
            // 
            this.ITEMCODE.DataPropertyName = "ITEMCODE";
            this.ITEMCODE.HeaderText = "Kód";
            this.ITEMCODE.Name = "ITEMCODE";
            this.ITEMCODE.ReadOnly = true;
            // 
            // ITEMDESC
            // 
            this.ITEMDESC.DataPropertyName = "ITEMDESC";
            this.ITEMDESC.HeaderText = "Název položky";
            this.ITEMDESC.Name = "ITEMDESC";
            this.ITEMDESC.ReadOnly = true;
            // 
            // QTY
            // 
            this.QTY.DataPropertyName = "QTY";
            this.QTY.HeaderText = "Množství objednané";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            // 
            // QTY_Zaplanovano
            // 
            this.QTY_Zaplanovano.DataPropertyName = "QTY_Zaplanovano";
            this.QTY_Zaplanovano.HeaderText = "Množství zaplánováno";
            this.QTY_Zaplanovano.Name = "QTY_Zaplanovano";
            this.QTY_Zaplanovano.ReadOnly = true;
            // 
            // QTY_Zbyva
            // 
            this.QTY_Zbyva.DataPropertyName = "QTY_Zbyva";
            this.QTY_Zbyva.HeaderText = "Zbývá k zaplánování";
            this.QTY_Zbyva.Name = "QTY_Zbyva";
            this.QTY_Zbyva.ReadOnly = true;
            // 
            // UserParam_1
            // 
            this.UserParam_1.DataPropertyName = "UserParam_1";
            this.UserParam_1.HeaderText = "UserParam_1";
            this.UserParam_1.Name = "UserParam_1";
            this.UserParam_1.ReadOnly = true;
            // 
            // UserParam_2
            // 
            this.UserParam_2.DataPropertyName = "UserParam_2";
            this.UserParam_2.HeaderText = "UserParam_2";
            this.UserParam_2.Name = "UserParam_2";
            this.UserParam_2.ReadOnly = true;
            // 
            // UserParam_3
            // 
            this.UserParam_3.DataPropertyName = "UserParam_3";
            this.UserParam_3.HeaderText = "UserParam_3";
            this.UserParam_3.Name = "UserParam_3";
            this.UserParam_3.ReadOnly = true;
            // 
            // UserParam_4
            // 
            this.UserParam_4.DataPropertyName = "UserParam_4";
            this.UserParam_4.HeaderText = "UserParam_4";
            this.UserParam_4.Name = "UserParam_4";
            this.UserParam_4.ReadOnly = true;
            // 
            // UserParam_5
            // 
            this.UserParam_5.DataPropertyName = "UserParam_5";
            this.UserParam_5.HeaderText = "UserParam_5";
            this.UserParam_5.Name = "UserParam_5";
            this.UserParam_5.ReadOnly = true;
            // 
            // bs_PV_Z
            // 
            this.bs_PV_Z.DataMember = "PV_Zaplanovani";
            this.bs_PV_Z.DataSource = this.ds_PV_Z;
            // 
            // ds_PV_Z
            // 
            this.ds_PV_Z.DataSetName = "Vyroba";
            this.ds_PV_Z.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ADGVSTB_Pol
            // 
            this.ADGVSTB_Pol.AllowMerge = false;
            this.ADGVSTB_Pol.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ADGVSTB_Pol.Location = new System.Drawing.Point(3, 3);
            this.ADGVSTB_Pol.MaximumSize = new System.Drawing.Size(0, 27);
            this.ADGVSTB_Pol.MinimumSize = new System.Drawing.Size(0, 27);
            this.ADGVSTB_Pol.Name = "ADGVSTB_Pol";
            this.ADGVSTB_Pol.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ADGVSTB_Pol.Size = new System.Drawing.Size(1012, 27);
            this.ADGVSTB_Pol.TabIndex = 2;
            this.ADGVSTB_Pol.Text = "advancedDataGridViewSearchToolBar1";
            this.ADGVSTB_Pol.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.ADGVSTB_Pol_Search);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dg_PV_Z_H);
            this.tabPage2.Controls.Add(this.ADGVSTB_H);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1018, 263);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Jen součty";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dg_PV_Z_H
            // 
            this.dg_PV_Z_H.AllowUserToAddRows = false;
            this.dg_PV_Z_H.AllowUserToDeleteRows = false;
            this.dg_PV_Z_H.AllowUserToResizeRows = false;
            this.dg_PV_Z_H.AutoGenerateColumns = false;
            this.dg_PV_Z_H.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_PV_Z_H.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OBJ_NMBR_H,
            this.OBJ_DESC_H,
            this.Column1,
            this.dataGridViewTextBoxColumn1,
            this.OBJ_COMPANY_H,
            this.OBJ_DATE_FROM_H,
            this.OBJ_DATE_TO_H,
            this.OBJ_ORD_H,
            this.QTY_H,
            this.QTY_Zaplanovano_H,
            this.QTY_Zbyva_H,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dg_PV_Z_H.DataSource = this.bs_PV_Z_H;
            this.dg_PV_Z_H.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_PV_Z_H.FilterAndSortEnabled = true;
            this.dg_PV_Z_H.Location = new System.Drawing.Point(3, 30);
            this.dg_PV_Z_H.Name = "dg_PV_Z_H";
            this.dg_PV_Z_H.ReadOnly = true;
            this.dg_PV_Z_H.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_PV_Z_H.Size = new System.Drawing.Size(1012, 230);
            this.dg_PV_Z_H.TabIndex = 0;
            // 
            // OBJ_NMBR_H
            // 
            this.OBJ_NMBR_H.DataPropertyName = "OBJ_NMBR";
            this.OBJ_NMBR_H.HeaderText = "Číslo objednávky";
            this.OBJ_NMBR_H.Name = "OBJ_NMBR_H";
            this.OBJ_NMBR_H.ReadOnly = true;
            // 
            // OBJ_DESC_H
            // 
            this.OBJ_DESC_H.DataPropertyName = "OBJ_DESC";
            this.OBJ_DESC_H.HeaderText = "Popis objednávky";
            this.OBJ_DESC_H.Name = "OBJ_DESC_H";
            this.OBJ_DESC_H.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "OBJ_ForUh";
            this.Column1.HeaderText = "ID Formy úhrady";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "OBJ_ForUh_IDS";
            this.dataGridViewTextBoxColumn1.HeaderText = "Forma úhrady";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // OBJ_COMPANY_H
            // 
            this.OBJ_COMPANY_H.DataPropertyName = "OBJ_COMPANY";
            this.OBJ_COMPANY_H.HeaderText = "Firma";
            this.OBJ_COMPANY_H.Name = "OBJ_COMPANY_H";
            this.OBJ_COMPANY_H.ReadOnly = true;
            // 
            // OBJ_DATE_FROM_H
            // 
            this.OBJ_DATE_FROM_H.DataPropertyName = "OBJ_DATE_FROM";
            this.OBJ_DATE_FROM_H.HeaderText = "Dátum od";
            this.OBJ_DATE_FROM_H.Name = "OBJ_DATE_FROM_H";
            this.OBJ_DATE_FROM_H.ReadOnly = true;
            // 
            // OBJ_DATE_TO_H
            // 
            this.OBJ_DATE_TO_H.DataPropertyName = "OBJ_DATE_TO";
            this.OBJ_DATE_TO_H.HeaderText = "Dátum do";
            this.OBJ_DATE_TO_H.Name = "OBJ_DATE_TO_H";
            this.OBJ_DATE_TO_H.ReadOnly = true;
            // 
            // OBJ_ORD_H
            // 
            this.OBJ_ORD_H.DataPropertyName = "OBJ_ORD";
            this.OBJ_ORD_H.HeaderText = "ID řádku objednávky";
            this.OBJ_ORD_H.Name = "OBJ_ORD_H";
            this.OBJ_ORD_H.ReadOnly = true;
            // 
            // QTY_H
            // 
            this.QTY_H.DataPropertyName = "QTY";
            this.QTY_H.HeaderText = "Množství objednané";
            this.QTY_H.Name = "QTY_H";
            this.QTY_H.ReadOnly = true;
            // 
            // QTY_Zaplanovano_H
            // 
            this.QTY_Zaplanovano_H.DataPropertyName = "QTY_Zaplanovano";
            this.QTY_Zaplanovano_H.HeaderText = "Množství zaplánováno";
            this.QTY_Zaplanovano_H.Name = "QTY_Zaplanovano_H";
            this.QTY_Zaplanovano_H.ReadOnly = true;
            // 
            // QTY_Zbyva_H
            // 
            this.QTY_Zbyva_H.DataPropertyName = "QTY_Zbyva";
            this.QTY_Zbyva_H.HeaderText = "Zbývá k zaplánování";
            this.QTY_Zbyva_H.Name = "QTY_Zbyva_H";
            this.QTY_Zbyva_H.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "UserParam_1";
            this.dataGridViewTextBoxColumn2.HeaderText = "UserParam_1";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "UserParam_2";
            this.dataGridViewTextBoxColumn3.HeaderText = "UserParam_2";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // bs_PV_Z_H
            // 
            this.bs_PV_Z_H.DataMember = "PV_Zaplanovani_Hlavicky";
            this.bs_PV_Z_H.DataSource = this.ds_PV_Z;
            // 
            // ADGVSTB_H
            // 
            this.ADGVSTB_H.AllowMerge = false;
            this.ADGVSTB_H.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ADGVSTB_H.Location = new System.Drawing.Point(3, 3);
            this.ADGVSTB_H.MaximumSize = new System.Drawing.Size(0, 27);
            this.ADGVSTB_H.MinimumSize = new System.Drawing.Size(0, 27);
            this.ADGVSTB_H.Name = "ADGVSTB_H";
            this.ADGVSTB_H.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ADGVSTB_H.Size = new System.Drawing.Size(1012, 27);
            this.ADGVSTB_H.TabIndex = 0;
            this.ADGVSTB_H.Text = "advancedDataGridViewSearchToolBar2";
            this.ADGVSTB_H.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.ADGVSTB_H_Search);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.dtp_ZAP_DatumDo);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.dtp_ZAP_DatumOd);
            this.panel2.Controls.Add(this.dtp_DO_DatumDo);
            this.panel2.Controls.Add(this.dtp_OD_DatumDo);
            this.panel2.Controls.Add(this.tb_FormaUhrady);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cbNezaplanovane);
            this.panel2.Controls.Add(this.btn_Refresh);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.dtp_DO_DatumOd);
            this.panel2.Controls.Add(this.dtp_OD_DatumOd);
            this.panel2.Controls.Add(this.tb_Kod);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.tb_Firma);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tb_SOPNUMBE);
            this.panel2.Controls.Add(this.tsFiltry);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1026, 192);
            this.panel2.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chb_Do);
            this.groupBox1.Controls.Add(this.chb_Od);
            this.groupBox1.Controls.Add(this.chb_Zapis);
            this.groupBox1.Controls.Add(this.rb_Do);
            this.groupBox1.Controls.Add(this.rb_Od);
            this.groupBox1.Controls.Add(this.rb_Zapis);
            this.groupBox1.Location = new System.Drawing.Point(746, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 109);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Řadit do plánu dle";
            // 
            // chb_Do
            // 
            this.chb_Do.AutoSize = true;
            this.chb_Do.Location = new System.Drawing.Point(63, 69);
            this.chb_Do.Name = "chb_Do";
            this.chb_Do.Size = new System.Drawing.Size(71, 17);
            this.chb_Do.TabIndex = 5;
            this.chb_Do.Text = "Sestupně";
            this.chb_Do.UseVisualStyleBackColor = true;
            // 
            // chb_Od
            // 
            this.chb_Od.AutoSize = true;
            this.chb_Od.Location = new System.Drawing.Point(63, 46);
            this.chb_Od.Name = "chb_Od";
            this.chb_Od.Size = new System.Drawing.Size(71, 17);
            this.chb_Od.TabIndex = 4;
            this.chb_Od.Text = "Sestupně";
            this.chb_Od.UseVisualStyleBackColor = true;
            // 
            // chb_Zapis
            // 
            this.chb_Zapis.AutoSize = true;
            this.chb_Zapis.Location = new System.Drawing.Point(63, 23);
            this.chb_Zapis.Name = "chb_Zapis";
            this.chb_Zapis.Size = new System.Drawing.Size(71, 17);
            this.chb_Zapis.TabIndex = 3;
            this.chb_Zapis.Text = "Sestupně";
            this.chb_Zapis.UseVisualStyleBackColor = true;
            // 
            // rb_Do
            // 
            this.rb_Do.AutoSize = true;
            this.rb_Do.Location = new System.Drawing.Point(6, 68);
            this.rb_Do.Name = "rb_Do";
            this.rb_Do.Size = new System.Drawing.Size(39, 17);
            this.rb_Do.TabIndex = 2;
            this.rb_Do.TabStop = true;
            this.rb_Do.Text = "Do";
            this.rb_Do.UseVisualStyleBackColor = true;
            this.rb_Do.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rb_Od
            // 
            this.rb_Od.AutoSize = true;
            this.rb_Od.Location = new System.Drawing.Point(6, 45);
            this.rb_Od.Name = "rb_Od";
            this.rb_Od.Size = new System.Drawing.Size(39, 17);
            this.rb_Od.TabIndex = 1;
            this.rb_Od.TabStop = true;
            this.rb_Od.Text = "Od";
            this.rb_Od.UseVisualStyleBackColor = true;
            this.rb_Od.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rb_Zapis
            // 
            this.rb_Zapis.AutoSize = true;
            this.rb_Zapis.Checked = true;
            this.rb_Zapis.Location = new System.Drawing.Point(6, 22);
            this.rb_Zapis.Name = "rb_Zapis";
            this.rb_Zapis.Size = new System.Drawing.Size(51, 17);
            this.rb_Zapis.TabIndex = 0;
            this.rb_Zapis.TabStop = true;
            this.rb_Zapis.Text = "Zápis";
            this.rb_Zapis.UseVisualStyleBackColor = true;
            this.rb_Zapis.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // dtp_ZAP_DatumDo
            // 
            this.dtp_ZAP_DatumDo.Checked = false;
            this.dtp_ZAP_DatumDo.CustomFormat = "dd.MM.yy hh:mm:ss";
            this.dtp_ZAP_DatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_ZAP_DatumDo.Location = new System.Drawing.Point(516, 39);
            this.dtp_ZAP_DatumDo.Name = "dtp_ZAP_DatumDo";
            this.dtp_ZAP_DatumDo.ShowCheckBox = true;
            this.dtp_ZAP_DatumDo.Size = new System.Drawing.Size(144, 20);
            this.dtp_ZAP_DatumDo.TabIndex = 51;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(286, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "Datum zápisu:";
            // 
            // dtp_ZAP_DatumOd
            // 
            this.dtp_ZAP_DatumOd.Checked = false;
            this.dtp_ZAP_DatumOd.CustomFormat = "dd.MM.yy hh:mm:ss";
            this.dtp_ZAP_DatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_ZAP_DatumOd.Location = new System.Drawing.Point(366, 39);
            this.dtp_ZAP_DatumOd.Name = "dtp_ZAP_DatumOd";
            this.dtp_ZAP_DatumOd.ShowCheckBox = true;
            this.dtp_ZAP_DatumOd.Size = new System.Drawing.Size(144, 20);
            this.dtp_ZAP_DatumOd.TabIndex = 49;
            // 
            // dtp_DO_DatumDo
            // 
            this.dtp_DO_DatumDo.Checked = false;
            this.dtp_DO_DatumDo.CustomFormat = "dd.MM.yy hh:mm:ss";
            this.dtp_DO_DatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_DO_DatumDo.Location = new System.Drawing.Point(516, 90);
            this.dtp_DO_DatumDo.Name = "dtp_DO_DatumDo";
            this.dtp_DO_DatumDo.ShowCheckBox = true;
            this.dtp_DO_DatumDo.Size = new System.Drawing.Size(144, 20);
            this.dtp_DO_DatumDo.TabIndex = 48;
            // 
            // dtp_OD_DatumDo
            // 
            this.dtp_OD_DatumDo.Checked = false;
            this.dtp_OD_DatumDo.CustomFormat = "dd.MM.yy hh:mm:ss";
            this.dtp_OD_DatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_OD_DatumDo.Location = new System.Drawing.Point(516, 65);
            this.dtp_OD_DatumDo.Name = "dtp_OD_DatumDo";
            this.dtp_OD_DatumDo.ShowCheckBox = true;
            this.dtp_OD_DatumDo.Size = new System.Drawing.Size(144, 20);
            this.dtp_OD_DatumDo.TabIndex = 47;
            // 
            // tb_FormaUhrady
            // 
            this.tb_FormaUhrady.Location = new System.Drawing.Point(102, 116);
            this.tb_FormaUhrady.Name = "tb_FormaUhrady";
            this.tb_FormaUhrady.Size = new System.Drawing.Size(163, 20);
            this.tb_FormaUhrady.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "ID Forma úhrady:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(302, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "Datum Do:";
            // 
            // dtp_DO_DatumOd
            // 
            this.dtp_DO_DatumOd.Checked = false;
            this.dtp_DO_DatumOd.CustomFormat = "dd.MM.yy hh:mm:ss";
            this.dtp_DO_DatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_DO_DatumOd.Location = new System.Drawing.Point(366, 90);
            this.dtp_DO_DatumOd.Name = "dtp_DO_DatumOd";
            this.dtp_DO_DatumOd.ShowCheckBox = true;
            this.dtp_DO_DatumOd.Size = new System.Drawing.Size(144, 20);
            this.dtp_DO_DatumOd.TabIndex = 43;
            // 
            // dtp_OD_DatumOd
            // 
            this.dtp_OD_DatumOd.Checked = false;
            this.dtp_OD_DatumOd.CustomFormat = "dd.MM.yy hh:mm:ss";
            this.dtp_OD_DatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_OD_DatumOd.Location = new System.Drawing.Point(366, 65);
            this.dtp_OD_DatumOd.Name = "dtp_OD_DatumOd";
            this.dtp_OD_DatumOd.ShowCheckBox = true;
            this.dtp_OD_DatumOd.Size = new System.Drawing.Size(144, 20);
            this.dtp_OD_DatumOd.TabIndex = 43;
            // 
            // tb_Kod
            // 
            this.tb_Kod.Location = new System.Drawing.Point(102, 90);
            this.tb_Kod.Name = "tb_Kod";
            this.tb_Kod.Size = new System.Drawing.Size(163, 20);
            this.tb_Kod.TabIndex = 42;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(61, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Kód:";
            // 
            // tb_Firma
            // 
            this.tb_Firma.Location = new System.Drawing.Point(102, 65);
            this.tb_Firma.Name = "tb_Firma";
            this.tb_Firma.Size = new System.Drawing.Size(163, 20);
            this.tb_Firma.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Firma:";
            // 
            // tb_SOPNUMBE
            // 
            this.tb_SOPNUMBE.Location = new System.Drawing.Point(102, 39);
            this.tb_SOPNUMBE.Name = "tb_SOPNUMBE";
            this.tb_SOPNUMBE.Size = new System.Drawing.Size(163, 20);
            this.tb_SOPNUMBE.TabIndex = 40;
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
            this.tsFiltry.Size = new System.Drawing.Size(1026, 25);
            this.tsFiltry.TabIndex = 39;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(302, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Datum Od:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Objednávka č.:";
            // 
            // bwZaplanovani
            // 
            this.bwZaplanovani.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwZaplanovani_DoWork);
            this.bwZaplanovani.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwZaplanovani_RunWorkerCompleted);
            // 
            // FormPlanovaniVyroby_Zaplanovani
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 481);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormPlanovaniVyroby_Zaplanovani";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Načíst";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPlanovaniVyroby_Zaplanovani_FormClosing);
            this.Load += new System.EventHandler(this.FormPlanovaniVyroby_Zaplanovani_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PV_Z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PV_Z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_PV_Z)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_PV_Z_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_PV_Z_H)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bs_PV_Z;
        private Zuby.ADGV.AdvancedDataGridView dg_PV_Z;
        private System.Windows.Forms.Panel panel1;
        private Fask.Interfaces.DataSets.Vyroba_Planovani ds_PV_Z;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Button btn_Cancle;
        private System.Windows.Forms.Button btn_Nacist;
        private System.Windows.Forms.CheckBox cbNezaplanovane;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar ADGVSTB_Pol;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Zuby.ADGV.AdvancedDataGridView dg_PV_Z_H;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar ADGVSTB_H;
        private System.Windows.Forms.BindingSource bs_PV_Z_H;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtp_DO_DatumOd;
        private System.Windows.Forms.DateTimePicker dtp_OD_DatumOd;
        private System.Windows.Forms.TextBox tb_Kod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_Firma;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_SOPNUMBE;
        private System.Windows.Forms.ToolStrip tsFiltry;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbFiltry;
        private System.Windows.Forms.ToolStripButton tsbNastavit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbZmena;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbPridat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbOdebrat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbVycistit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_NMBR_H;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_DESC_H;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_COMPANY_H;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_DATE_FROM_H;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_DATE_TO_H;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_ORD_H;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY_H;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY_Zaplanovano_H;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY_Zbyva_H;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.TextBox tb_FormaUhrady;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp_DO_DatumDo;
        private System.Windows.Forms.DateTimePicker dtp_OD_DatumDo;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_NMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_COMPANY;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_ForUh;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_ForUh_IDS;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_DATE_ZAPL;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_DATE_FROM;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJ_DATE_TO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY_Zaplanovano;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY_Zbyva;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserParam_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserParam_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserParam_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserParam_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserParam_5;
        private System.Windows.Forms.DateTimePicker dtp_ZAP_DatumDo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtp_ZAP_DatumOd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chb_Do;
        private System.Windows.Forms.CheckBox chb_Od;
        private System.Windows.Forms.CheckBox chb_Zapis;
        private System.Windows.Forms.RadioButton rb_Do;
        private System.Windows.Forms.RadioButton rb_Od;
        private System.Windows.Forms.RadioButton rb_Zapis;
        private System.ComponentModel.BackgroundWorker bwZaplanovani;
    }
}