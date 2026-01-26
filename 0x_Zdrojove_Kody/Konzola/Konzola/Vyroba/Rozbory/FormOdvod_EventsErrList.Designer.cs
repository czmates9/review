namespace Konzola.Vyroba.Rozbory
{
    partial class FormOdvod_EventsErrList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOdvod_EventsErrList));
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.dg_OdvodEvents = new Zuby.ADGV.AdvancedDataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateeveDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyRealDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodeReadedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodeSendedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zakazkaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.popisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.faskGUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reportTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isProcessedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scan1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scan2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scan3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sensorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_OdvodEventsErr = new System.Windows.Forms.BindingSource(this.components);
            this.ds_OdvodEventsErr = new Fask.Interfaces.DataSets.Vyroba();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_Eventu_Count = new System.Windows.Forms.ToolStripStatusLabel();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cb_Razeni = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chb_Razeni = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cb_PackType = new System.Windows.Forms.ComboBox();
            this.tb_status = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_description = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_MachineID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtp_porizeno_OD = new System.Windows.Forms.DateTimePicker();
            this.cb_Porizeno_TimeVariant = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtp_porizeno_DO = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtp_OD = new System.Windows.Forms.DateTimePicker();
            this.cb_TimeVariant = new System.Windows.Forms.ComboBox();
            this.dtp_DO = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chb_Nezpracovane = new System.Windows.Forms.CheckBox();
            this.chb_Zpravovane = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
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
            this.bw_OdvodEventsErr = new System.ComponentModel.BackgroundWorker();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_OdvodEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_OdvodEventsErr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_OdvodEventsErr)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
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
            this.panelMain.Controls.Add(this.dg_OdvodEvents);
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
            // dg_OdvodEvents
            // 
            this.dg_OdvodEvents.AllowUserToAddRows = false;
            this.dg_OdvodEvents.AllowUserToDeleteRows = false;
            this.dg_OdvodEvents.AllowUserToOrderColumns = true;
            this.dg_OdvodEvents.AllowUserToResizeRows = false;
            this.dg_OdvodEvents.AutoGenerateColumns = false;
            this.dg_OdvodEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_OdvodEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.loginidDataGridViewTextBoxColumn,
            this.machineidDataGridViewTextBoxColumn,
            this.dateeveDataGridViewTextBoxColumn,
            this.qtyDataGridViewTextBoxColumn,
            this.qtyRealDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.barcodeReadedDataGridViewTextBoxColumn,
            this.barcodeSendedDataGridViewTextBoxColumn,
            this.zakazkaDataGridViewTextBoxColumn,
            this.popisDataGridViewTextBoxColumn,
            this.faskGUIDDataGridViewTextBoxColumn,
            this.reportTypeDataGridViewTextBoxColumn,
            this.isProcessedDataGridViewTextBoxColumn,
            this.iDODataGridViewTextBoxColumn,
            this.scan1DataGridViewTextBoxColumn,
            this.scan2DataGridViewTextBoxColumn,
            this.scan3DataGridViewTextBoxColumn,
            this.sensorDataGridViewTextBoxColumn});
            this.dg_OdvodEvents.DataSource = this.bs_OdvodEventsErr;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_OdvodEvents.DefaultCellStyle = dataGridViewCellStyle22;
            this.dg_OdvodEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_OdvodEvents.EnableHeadersVisualStyles = false;
            this.dg_OdvodEvents.FilterAndSortEnabled = true;
            this.dg_OdvodEvents.Location = new System.Drawing.Point(0, 315);
            this.dg_OdvodEvents.Name = "dg_OdvodEvents";
            this.dg_OdvodEvents.ReadOnly = true;
            this.dg_OdvodEvents.RowHeadersVisible = false;
            this.dg_OdvodEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_OdvodEvents.Size = new System.Drawing.Size(1200, 364);
            this.dg_OdvodEvents.TabIndex = 1;
            this.dg_OdvodEvents.TabStop = false;
            this.dg_OdvodEvents.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dg_OdvodEvents.SelectionChanged += new System.EventHandler(this.dg_OdvodEvents_SelectionChanged);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "ID";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.idDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // loginidDataGridViewTextBoxColumn
            // 
            this.loginidDataGridViewTextBoxColumn.DataPropertyName = "loginid";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.loginidDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.loginidDataGridViewTextBoxColumn.HeaderText = "Číslo směny";
            this.loginidDataGridViewTextBoxColumn.Name = "loginidDataGridViewTextBoxColumn";
            this.loginidDataGridViewTextBoxColumn.ReadOnly = true;
            this.loginidDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.loginidDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // machineidDataGridViewTextBoxColumn
            // 
            this.machineidDataGridViewTextBoxColumn.DataPropertyName = "machineid";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.machineidDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.machineidDataGridViewTextBoxColumn.HeaderText = "ID stroje";
            this.machineidDataGridViewTextBoxColumn.Name = "machineidDataGridViewTextBoxColumn";
            this.machineidDataGridViewTextBoxColumn.ReadOnly = true;
            this.machineidDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.machineidDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dateeveDataGridViewTextBoxColumn
            // 
            this.dateeveDataGridViewTextBoxColumn.DataPropertyName = "dateeve";
            dataGridViewCellStyle14.Format = "G";
            dataGridViewCellStyle14.NullValue = null;
            this.dateeveDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle14;
            this.dateeveDataGridViewTextBoxColumn.HeaderText = "Pořízeno";
            this.dateeveDataGridViewTextBoxColumn.Name = "dateeveDataGridViewTextBoxColumn";
            this.dateeveDataGridViewTextBoxColumn.ReadOnly = true;
            this.dateeveDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dateeveDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // qtyDataGridViewTextBoxColumn
            // 
            this.qtyDataGridViewTextBoxColumn.DataPropertyName = "qty";
            this.qtyDataGridViewTextBoxColumn.HeaderText = "Počet 1";
            this.qtyDataGridViewTextBoxColumn.Name = "qtyDataGridViewTextBoxColumn";
            this.qtyDataGridViewTextBoxColumn.ReadOnly = true;
            this.qtyDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.qtyDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // qtyRealDataGridViewTextBoxColumn
            // 
            this.qtyRealDataGridViewTextBoxColumn.DataPropertyName = "qtyReal";
            this.qtyRealDataGridViewTextBoxColumn.HeaderText = "Počet 2";
            this.qtyRealDataGridViewTextBoxColumn.Name = "qtyRealDataGridViewTextBoxColumn";
            this.qtyRealDataGridViewTextBoxColumn.ReadOnly = true;
            this.qtyRealDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.qtyRealDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Vlastnost";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            this.descriptionDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.descriptionDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // barcodeReadedDataGridViewTextBoxColumn
            // 
            this.barcodeReadedDataGridViewTextBoxColumn.DataPropertyName = "barcodeReaded";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.barcodeReadedDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle15;
            this.barcodeReadedDataGridViewTextBoxColumn.HeaderText = "Načtený čár. kód";
            this.barcodeReadedDataGridViewTextBoxColumn.Name = "barcodeReadedDataGridViewTextBoxColumn";
            this.barcodeReadedDataGridViewTextBoxColumn.ReadOnly = true;
            this.barcodeReadedDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.barcodeReadedDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // barcodeSendedDataGridViewTextBoxColumn
            // 
            this.barcodeSendedDataGridViewTextBoxColumn.DataPropertyName = "barcodeSended";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.barcodeSendedDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle16;
            this.barcodeSendedDataGridViewTextBoxColumn.HeaderText = "Odeslaný Kód";
            this.barcodeSendedDataGridViewTextBoxColumn.Name = "barcodeSendedDataGridViewTextBoxColumn";
            this.barcodeSendedDataGridViewTextBoxColumn.ReadOnly = true;
            this.barcodeSendedDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.barcodeSendedDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // zakazkaDataGridViewTextBoxColumn
            // 
            this.zakazkaDataGridViewTextBoxColumn.DataPropertyName = "zakazka";
            this.zakazkaDataGridViewTextBoxColumn.HeaderText = "Zakázka";
            this.zakazkaDataGridViewTextBoxColumn.Name = "zakazkaDataGridViewTextBoxColumn";
            this.zakazkaDataGridViewTextBoxColumn.ReadOnly = true;
            this.zakazkaDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.zakazkaDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // popisDataGridViewTextBoxColumn
            // 
            this.popisDataGridViewTextBoxColumn.DataPropertyName = "popis";
            this.popisDataGridViewTextBoxColumn.HeaderText = "Popis";
            this.popisDataGridViewTextBoxColumn.Name = "popisDataGridViewTextBoxColumn";
            this.popisDataGridViewTextBoxColumn.ReadOnly = true;
            this.popisDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.popisDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // faskGUIDDataGridViewTextBoxColumn
            // 
            this.faskGUIDDataGridViewTextBoxColumn.DataPropertyName = "faskGUID";
            this.faskGUIDDataGridViewTextBoxColumn.HeaderText = "GUID záznamu";
            this.faskGUIDDataGridViewTextBoxColumn.Name = "faskGUIDDataGridViewTextBoxColumn";
            this.faskGUIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.faskGUIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.faskGUIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // reportTypeDataGridViewTextBoxColumn
            // 
            this.reportTypeDataGridViewTextBoxColumn.DataPropertyName = "reportType";
            this.reportTypeDataGridViewTextBoxColumn.HeaderText = "Typ záznamu";
            this.reportTypeDataGridViewTextBoxColumn.Name = "reportTypeDataGridViewTextBoxColumn";
            this.reportTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.reportTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.reportTypeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // isProcessedDataGridViewTextBoxColumn
            // 
            this.isProcessedDataGridViewTextBoxColumn.DataPropertyName = "isProcessed";
            this.isProcessedDataGridViewTextBoxColumn.HeaderText = "Zpracováno";
            this.isProcessedDataGridViewTextBoxColumn.Name = "isProcessedDataGridViewTextBoxColumn";
            this.isProcessedDataGridViewTextBoxColumn.ReadOnly = true;
            this.isProcessedDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isProcessedDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // iDODataGridViewTextBoxColumn
            // 
            this.iDODataGridViewTextBoxColumn.DataPropertyName = "IDO";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.iDODataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle17;
            this.iDODataGridViewTextBoxColumn.HeaderText = "ID operace";
            this.iDODataGridViewTextBoxColumn.Name = "iDODataGridViewTextBoxColumn";
            this.iDODataGridViewTextBoxColumn.ReadOnly = true;
            this.iDODataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.iDODataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // scan1DataGridViewTextBoxColumn
            // 
            this.scan1DataGridViewTextBoxColumn.DataPropertyName = "scan1";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.scan1DataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle18;
            this.scan1DataGridViewTextBoxColumn.HeaderText = "Hodnota načteno 1";
            this.scan1DataGridViewTextBoxColumn.Name = "scan1DataGridViewTextBoxColumn";
            this.scan1DataGridViewTextBoxColumn.ReadOnly = true;
            this.scan1DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.scan1DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // scan2DataGridViewTextBoxColumn
            // 
            this.scan2DataGridViewTextBoxColumn.DataPropertyName = "scan2";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.scan2DataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle19;
            this.scan2DataGridViewTextBoxColumn.HeaderText = "Hodnota načteno 2";
            this.scan2DataGridViewTextBoxColumn.Name = "scan2DataGridViewTextBoxColumn";
            this.scan2DataGridViewTextBoxColumn.ReadOnly = true;
            this.scan2DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.scan2DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // scan3DataGridViewTextBoxColumn
            // 
            this.scan3DataGridViewTextBoxColumn.DataPropertyName = "scan3";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.scan3DataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle20;
            this.scan3DataGridViewTextBoxColumn.HeaderText = "Hodnota načteno 3";
            this.scan3DataGridViewTextBoxColumn.Name = "scan3DataGridViewTextBoxColumn";
            this.scan3DataGridViewTextBoxColumn.ReadOnly = true;
            this.scan3DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.scan3DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // sensorDataGridViewTextBoxColumn
            // 
            this.sensorDataGridViewTextBoxColumn.DataPropertyName = "sensor";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.sensorDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle21;
            this.sensorDataGridViewTextBoxColumn.HeaderText = "Hodnota sensor 1";
            this.sensorDataGridViewTextBoxColumn.Name = "sensorDataGridViewTextBoxColumn";
            this.sensorDataGridViewTextBoxColumn.ReadOnly = true;
            this.sensorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sensorDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // bs_OdvodEventsErr
            // 
            this.bs_OdvodEventsErr.DataMember = "FASK_EventsErr";
            this.bs_OdvodEventsErr.DataSource = this.ds_OdvodEventsErr;
            // 
            // ds_OdvodEventsErr
            // 
            this.ds_OdvodEventsErr.DataSetName = "Vyroba";
            this.ds_OdvodEventsErr.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 264);
            this.panel1.TabIndex = 43;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cb_PackType);
            this.groupBox3.Controls.Add(this.tb_status);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.tb_description);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tb_MachineID);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(697, 45);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 186);
            this.groupBox3.TabIndex = 56;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cb_Razeni);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.chb_Razeni);
            this.groupBox4.Location = new System.Drawing.Point(6, 121);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(308, 59);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "řazení";
            // 
            // cb_Razeni
            // 
            this.cb_Razeni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Razeni.FormattingEnabled = true;
            this.cb_Razeni.Items.AddRange(new object[] {
            "",
            "ID",
            "Pořízeno",
            "Odeslaný kód"});
            this.cb_Razeni.Location = new System.Drawing.Point(63, 9);
            this.cb_Razeni.Name = "cb_Razeni";
            this.cb_Razeni.Size = new System.Drawing.Size(141, 21);
            this.cb_Razeni.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Sloupec:";
            // 
            // chb_Razeni
            // 
            this.chb_Razeni.AutoSize = true;
            this.chb_Razeni.Location = new System.Drawing.Point(63, 36);
            this.chb_Razeni.Name = "chb_Razeni";
            this.chb_Razeni.Size = new System.Drawing.Size(76, 17);
            this.chb_Razeni.TabIndex = 8;
            this.chb_Razeni.Text = "Vzestupně";
            this.chb_Razeni.UseVisualStyleBackColor = true;
            this.chb_Razeni.CheckedChanged += new System.EventHandler(this.chb_Razeni_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Typ balení:";
            // 
            // cb_PackType
            // 
            this.cb_PackType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_PackType.Enabled = false;
            this.cb_PackType.FormattingEnabled = true;
            this.cb_PackType.Items.AddRange(new object[] {
            "",
            "UP",
            "NP",
            "ZZ",
            "FP"});
            this.cb_PackType.Location = new System.Drawing.Point(69, 94);
            this.cb_PackType.Name = "cb_PackType";
            this.cb_PackType.Size = new System.Drawing.Size(150, 21);
            this.cb_PackType.TabIndex = 6;
            // 
            // tb_status
            // 
            this.tb_status.Enabled = false;
            this.tb_status.Location = new System.Drawing.Point(69, 69);
            this.tb_status.Name = "tb_status";
            this.tb_status.Size = new System.Drawing.Size(150, 20);
            this.tb_status.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(26, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Status:";
            // 
            // tb_description
            // 
            this.tb_description.Location = new System.Drawing.Point(69, 44);
            this.tb_description.Name = "tb_description";
            this.tb_description.Size = new System.Drawing.Size(150, 20);
            this.tb_description.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Vlastnost:";
            // 
            // tb_MachineID
            // 
            this.tb_MachineID.Location = new System.Drawing.Point(69, 19);
            this.tb_MachineID.Name = "tb_MachineID";
            this.tb_MachineID.Size = new System.Drawing.Size(150, 20);
            this.tb_MachineID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "ID stroje:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtp_porizeno_OD);
            this.groupBox2.Controls.Add(this.cb_Porizeno_TimeVariant);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.dtp_porizeno_DO);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(25, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(666, 74);
            this.groupBox2.TabIndex = 55;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pořízeno";
            // 
            // dtp_porizeno_OD
            // 
            this.dtp_porizeno_OD.Checked = false;
            this.dtp_porizeno_OD.Location = new System.Drawing.Point(47, 25);
            this.dtp_porizeno_OD.Name = "dtp_porizeno_OD";
            this.dtp_porizeno_OD.ShowCheckBox = true;
            this.dtp_porizeno_OD.Size = new System.Drawing.Size(200, 20);
            this.dtp_porizeno_OD.TabIndex = 49;
            // 
            // cb_Porizeno_TimeVariant
            // 
            this.cb_Porizeno_TimeVariant.FormattingEnabled = true;
            this.cb_Porizeno_TimeVariant.Location = new System.Drawing.Point(346, 33);
            this.cb_Porizeno_TimeVariant.Name = "cb_Porizeno_TimeVariant";
            this.cb_Porizeno_TimeVariant.Size = new System.Drawing.Size(139, 21);
            this.cb_Porizeno_TimeVariant.TabIndex = 47;
            this.cb_Porizeno_TimeVariant.SelectedIndexChanged += new System.EventHandler(this.cb_Porizeno_TimeVariant_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(256, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Časová varianta";
            // 
            // dtp_porizeno_DO
            // 
            this.dtp_porizeno_DO.Checked = false;
            this.dtp_porizeno_DO.Location = new System.Drawing.Point(47, 48);
            this.dtp_porizeno_DO.Name = "dtp_porizeno_DO";
            this.dtp_porizeno_DO.ShowCheckBox = true;
            this.dtp_porizeno_DO.Size = new System.Drawing.Size(200, 20);
            this.dtp_porizeno_DO.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 51;
            this.label5.Text = "DO";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 52;
            this.label6.Text = "OD";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.dtp_OD);
            this.groupBox1.Controls.Add(this.cb_TimeVariant);
            this.groupBox1.Controls.Add(this.dtp_DO);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chb_Nezpracovane);
            this.groupBox1.Controls.Add(this.chb_Zpravovane);
            this.groupBox1.Location = new System.Drawing.Point(25, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(666, 74);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zpracováno";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(256, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 56;
            this.label9.Text = "Časová varianta";
            // 
            // dtp_OD
            // 
            this.dtp_OD.Checked = false;
            this.dtp_OD.Location = new System.Drawing.Point(47, 25);
            this.dtp_OD.Name = "dtp_OD";
            this.dtp_OD.ShowCheckBox = true;
            this.dtp_OD.Size = new System.Drawing.Size(200, 20);
            this.dtp_OD.TabIndex = 43;
            // 
            // cb_TimeVariant
            // 
            this.cb_TimeVariant.FormattingEnabled = true;
            this.cb_TimeVariant.Location = new System.Drawing.Point(346, 33);
            this.cb_TimeVariant.Name = "cb_TimeVariant";
            this.cb_TimeVariant.Size = new System.Drawing.Size(139, 21);
            this.cb_TimeVariant.TabIndex = 55;
            this.cb_TimeVariant.SelectedIndexChanged += new System.EventHandler(this.cb_TimeVariant_SelectedIndexChanged);
            // 
            // dtp_DO
            // 
            this.dtp_DO.Checked = false;
            this.dtp_DO.Location = new System.Drawing.Point(47, 48);
            this.dtp_DO.Name = "dtp_DO";
            this.dtp_DO.ShowCheckBox = true;
            this.dtp_DO.Size = new System.Drawing.Size(200, 20);
            this.dtp_DO.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "OD";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "DO";
            // 
            // chb_Nezpracovane
            // 
            this.chb_Nezpracovane.AutoSize = true;
            this.chb_Nezpracovane.Checked = true;
            this.chb_Nezpracovane.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Nezpracovane.Location = new System.Drawing.Point(509, 43);
            this.chb_Nezpracovane.Name = "chb_Nezpracovane";
            this.chb_Nezpracovane.Size = new System.Drawing.Size(96, 17);
            this.chb_Nezpracovane.TabIndex = 46;
            this.chb_Nezpracovane.Text = "Nezpracované";
            this.chb_Nezpracovane.UseVisualStyleBackColor = true;
            // 
            // chb_Zpravovane
            // 
            this.chb_Zpravovane.AutoSize = true;
            this.chb_Zpravovane.Checked = true;
            this.chb_Zpravovane.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Zpravovane.Location = new System.Drawing.Point(509, 25);
            this.chb_Zpravovane.Name = "chb_Zpravovane";
            this.chb_Zpravovane.Size = new System.Drawing.Size(84, 17);
            this.chb_Zpravovane.TabIndex = 46;
            this.chb_Zpravovane.Text = "Zpracované";
            this.chb_Zpravovane.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "GUID Production záznamu:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(171, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(520, 20);
            this.textBox1.TabIndex = 42;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
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
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuVyber,
            this.tsmiMenuList,
            this.tsmiExporty});
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
            // bw_OdvodEventsErr
            // 
            this.bw_OdvodEventsErr.WorkerSupportsCancellation = true;
            this.bw_OdvodEventsErr.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_OdvodEvents_DoWork);
            this.bw_OdvodEventsErr.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_OdvodEvents_RunWorkerCompleted);
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
            // FormOdvod_EventsErrList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 701);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "FormOdvod_EventsErrList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Odvod Events chyby";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUzivateleList_FormClosing);
            this.Load += new System.EventHandler(this.FormOdvod_EventsErrList_Load);
            this.Shown += new System.EventHandler(this.FormOdvod_EventsErrList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOdvod_EventsErrList_KeyDown);
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_OdvodEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_OdvodEventsErr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_OdvodEventsErr)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        public Zuby.ADGV.AdvancedDataGridView dg_OdvodEvents;
        private System.Windows.Forms.BindingSource bs_OdvodEventsErr;
        private Fask.Interfaces.DataSets.Vyroba ds_OdvodEventsErr;
        private System.ComponentModel.BackgroundWorker bw_OdvodEventsErr;
        private System.Windows.Forms.DateTimePicker dtp_OD;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_DO;
        private System.Windows.Forms.CheckBox chb_Nezpracovane;
        private System.Windows.Forms.CheckBox chb_Zpravovane;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtp_porizeno_DO;
        private System.Windows.Forms.DateTimePicker dtp_porizeno_OD;
        private System.Windows.Forms.ComboBox cb_Porizeno_TimeVariant;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_TimeVariant;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cb_PackType;
        private System.Windows.Forms.TextBox tb_status;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_description;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_MachineID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cb_Razeni;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chb_Razeni;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Eventu_Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loginidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateeveDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyRealDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodeReadedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodeSendedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zakazkaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn popisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn faskGUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn reportTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isProcessedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scan1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scan2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scan3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sensorDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem tiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tiskEtiketToolStripMenuItem;
    }
}