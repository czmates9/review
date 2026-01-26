using System;
namespace Konzola.Expedice
{
    partial class FormBufferBaleniList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBufferBaleniList));
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.dgExpedice = new Zuby.ADGV.AdvancedDataGridView();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sERLTNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nMBRBALDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NMBRPAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cZCarKodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEXROWIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsExpedice = new System.Windows.Forms.BindingSource(this.components);
            this.dsExpedice = new Fask.Interfaces.DataSets.Expedice();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMaterialITEMNMBR = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMaterialNMBRPAL = new System.Windows.Forms.ComboBox();
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
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.cbMaterialRozpracovano = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenuVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVybrat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiKonecVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonecList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExcelOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPolozka = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNovy = new System.Windows.Forms.ToolStripMenuItem();
            this.bwLoadExpedice = new System.ComponentModel.BackgroundWorker();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExpedice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsExpedice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsExpedice)).BeginInit();
            this.panel1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtonsZobrazeniVyber
            // 
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonKonec);
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonVybratUzivatele);
            this.panelButtonsZobrazeniVyber.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniVyber.Location = new System.Drawing.Point(659, 0);
            this.panelButtonsZobrazeniVyber.Name = "panelButtonsZobrazeniVyber";
            this.panelButtonsZobrazeniVyber.Size = new System.Drawing.Size(84, 468);
            this.panelButtonsZobrazeniVyber.TabIndex = 2;
            // 
            // buttonKonec
            // 
            this.buttonKonec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKonec.Location = new System.Drawing.Point(6, 393);
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
            this.buttonVybratUzivatele.Text = "Vybrat záznam";
            this.buttonVybratUzivatele.UseVisualStyleBackColor = true;
            this.buttonVybratUzivatele.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.progressIndicator1);
            this.panelMain.Controls.Add(this.dgExpedice);
            this.panelMain.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Controls.Add(this.menuStrip2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(659, 468);
            this.panelMain.TabIndex = 1;
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(212, 220);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(98, 98);
            this.progressIndicator1.TabIndex = 38;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // dgExpedice
            // 
            this.dgExpedice.AllowUserToAddRows = false;
            this.dgExpedice.AllowUserToDeleteRows = false;
            this.dgExpedice.AllowUserToOrderColumns = true;
            this.dgExpedice.AllowUserToResizeRows = false;
            this.dgExpedice.AutoGenerateColumns = false;
            this.dgExpedice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExpedice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.qTYSHPPDDataGridViewTextBoxColumn,
            this.sERLTNUMDataGridViewTextBoxColumn,
            this.nMBRBALDataGridViewTextBoxColumn,
            this.NMBRPAL,
            this.vNDITNUMDataGridViewTextBoxColumn,
            this.cZCarKodDataGridViewTextBoxColumn,
            this.dEXROWIDDataGridViewTextBoxColumn,
            this.gUIDDataGridViewTextBoxColumn,
            this.iTEMDESCDataGridViewTextBoxColumn});
            this.dgExpedice.DataSource = this.bsExpedice;
            this.dgExpedice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgExpedice.EnableHeadersVisualStyles = false;
            this.dgExpedice.FilterAndSortEnabled = true;
            this.dgExpedice.Location = new System.Drawing.Point(0, 158);
            this.dgExpedice.Name = "dgExpedice";
            this.dgExpedice.ReadOnly = true;
            this.dgExpedice.RowHeadersVisible = false;
            this.dgExpedice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgExpedice.Size = new System.Drawing.Size(659, 310);
            this.dgExpedice.TabIndex = 1;
            this.dgExpedice.TabStop = false;
            this.dgExpedice.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgExpedice.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dgExpedice.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "ID materálu";
            this.iTEMNMBRDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMNMBRDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.iTEMNMBRDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // qTYSHPPDDataGridViewTextBoxColumn
            // 
            this.qTYSHPPDDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPD";
            this.qTYSHPPDDataGridViewTextBoxColumn.HeaderText = "Množství";
            this.qTYSHPPDDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.qTYSHPPDDataGridViewTextBoxColumn.Name = "qTYSHPPDDataGridViewTextBoxColumn";
            this.qTYSHPPDDataGridViewTextBoxColumn.ReadOnly = true;
            this.qTYSHPPDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.qTYSHPPDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // sERLTNUMDataGridViewTextBoxColumn
            // 
            this.sERLTNUMDataGridViewTextBoxColumn.DataPropertyName = "SERLTNUM";
            this.sERLTNUMDataGridViewTextBoxColumn.HeaderText = "SN";
            this.sERLTNUMDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.sERLTNUMDataGridViewTextBoxColumn.Name = "sERLTNUMDataGridViewTextBoxColumn";
            this.sERLTNUMDataGridViewTextBoxColumn.ReadOnly = true;
            this.sERLTNUMDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sERLTNUMDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // nMBRBALDataGridViewTextBoxColumn
            // 
            this.nMBRBALDataGridViewTextBoxColumn.DataPropertyName = "NMBRBAL";
            this.nMBRBALDataGridViewTextBoxColumn.HeaderText = "Číslo balení";
            this.nMBRBALDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.nMBRBALDataGridViewTextBoxColumn.Name = "nMBRBALDataGridViewTextBoxColumn";
            this.nMBRBALDataGridViewTextBoxColumn.ReadOnly = true;
            this.nMBRBALDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.nMBRBALDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // NMBRPAL
            // 
            this.NMBRPAL.DataPropertyName = "NMBRPAL";
            this.NMBRPAL.HeaderText = "číslo palety";
            this.NMBRPAL.MinimumWidth = 22;
            this.NMBRPAL.Name = "NMBRPAL";
            this.NMBRPAL.ReadOnly = true;
            this.NMBRPAL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NMBRPAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // vNDITNUMDataGridViewTextBoxColumn
            // 
            this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "Čar. kód dodavatele";
            this.vNDITNUMDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
            this.vNDITNUMDataGridViewTextBoxColumn.ReadOnly = true;
            this.vNDITNUMDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.vNDITNUMDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // cZCarKodDataGridViewTextBoxColumn
            // 
            this.cZCarKodDataGridViewTextBoxColumn.DataPropertyName = "CZ_CarKod";
            this.cZCarKodDataGridViewTextBoxColumn.HeaderText = "čar. kód";
            this.cZCarKodDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.cZCarKodDataGridViewTextBoxColumn.Name = "cZCarKodDataGridViewTextBoxColumn";
            this.cZCarKodDataGridViewTextBoxColumn.ReadOnly = true;
            this.cZCarKodDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cZCarKodDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dEXROWIDDataGridViewTextBoxColumn
            // 
            this.dEXROWIDDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn.HeaderText = "index";
            this.dEXROWIDDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.dEXROWIDDataGridViewTextBoxColumn.Name = "dEXROWIDDataGridViewTextBoxColumn";
            this.dEXROWIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.dEXROWIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dEXROWIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // gUIDDataGridViewTextBoxColumn
            // 
            this.gUIDDataGridViewTextBoxColumn.DataPropertyName = "GUID";
            this.gUIDDataGridViewTextBoxColumn.HeaderText = "GUID";
            this.gUIDDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.gUIDDataGridViewTextBoxColumn.Name = "gUIDDataGridViewTextBoxColumn";
            this.gUIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.gUIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gUIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Označení materálu";
            this.iTEMDESCDataGridViewTextBoxColumn.MinimumWidth = 22;
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMDESCDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.iTEMDESCDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // bsExpedice
            // 
            this.bsExpedice.DataMember = "CZMST_Expedice_Baleni_Buffer";
            this.bsExpedice.DataSource = this.dsExpedice;
            // 
            // dsExpedice
            // 
            this.dsExpedice.DataSetName = "Expedice";
            this.dsExpedice.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 131);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(659, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 44;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbMaterialITEMNMBR);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbMaterialNMBRPAL);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.cbMaterialRozpracovano);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(659, 107);
            this.panel1.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "ID materiálu:";
            this.label3.Visible = false;
            // 
            // cbMaterialITEMNMBR
            // 
            this.cbMaterialITEMNMBR.FormattingEnabled = true;
            this.cbMaterialITEMNMBR.Location = new System.Drawing.Point(81, 32);
            this.cbMaterialITEMNMBR.Name = "cbMaterialITEMNMBR";
            this.cbMaterialITEMNMBR.Size = new System.Drawing.Size(171, 21);
            this.cbMaterialITEMNMBR.TabIndex = 1;
            this.cbMaterialITEMNMBR.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Stav:";
            this.label2.Visible = false;
            // 
            // cbMaterialNMBRPAL
            // 
            this.cbMaterialNMBRPAL.FormattingEnabled = true;
            this.cbMaterialNMBRPAL.Location = new System.Drawing.Point(81, 59);
            this.cbMaterialNMBRPAL.Name = "cbMaterialNMBRPAL";
            this.cbMaterialNMBRPAL.Size = new System.Drawing.Size(171, 21);
            this.cbMaterialNMBRPAL.TabIndex = 2;
            this.cbMaterialNMBRPAL.Visible = false;
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
            this.tsFiltry.Size = new System.Drawing.Size(659, 25);
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
            this.tscbFiltry.Visible = false;
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
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(580, 28);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 20;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // cbMaterialRozpracovano
            // 
            this.cbMaterialRozpracovano.FormattingEnabled = true;
            this.cbMaterialRozpracovano.Location = new System.Drawing.Point(296, 35);
            this.cbMaterialRozpracovano.Name = "cbMaterialRozpracovano";
            this.cbMaterialRozpracovano.Size = new System.Drawing.Size(171, 21);
            this.cbMaterialRozpracovano.TabIndex = 3;
            this.cbMaterialRozpracovano.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Číslo palety:";
            this.label1.Visible = false;
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuVyber,
            this.tsmiMenuList,
            this.tsmiExport,
            this.tsmiPolozka});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(659, 24);
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
            this.tsmiVybrat.Size = new System.Drawing.Size(152, 22);
            this.tsmiVybrat.Text = "Vybrat";
            this.tsmiVybrat.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiKonecVyber
            // 
            this.tsmiKonecVyber.Name = "tsmiKonecVyber";
            this.tsmiKonecVyber.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonecVyber.Size = new System.Drawing.Size(152, 22);
            this.tsmiKonecVyber.Text = "Konec";
            this.tsmiKonecVyber.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // tsmiMenuList
            // 
            this.tsmiMenuList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiKonecList});
            this.tsmiMenuList.Name = "tsmiMenuList";
            this.tsmiMenuList.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenuList.Text = "Menu";
            // 
            // tsmiKonecList
            // 
            this.tsmiKonecList.Name = "tsmiKonecList";
            this.tsmiKonecList.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonecList.Size = new System.Drawing.Size(152, 22);
            this.tsmiKonecList.Text = "Konec";
            this.tsmiKonecList.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator6,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExcelOznacene,
            this.toolStripSeparator7,
            this.tsmiExportDoXMLVse,
            this.tsmiExportDoXMLOznacene});
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(55, 20);
            this.tsmiExport.Text = "Výstup";
            // 
            // tsmiExportDoCSVVse
            // 
            this.tsmiExportDoCSVVse.Name = "tsmiExportDoCSVVse";
            this.tsmiExportDoCSVVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoCSVVse.Text = "Export do CSV vše";
            this.tsmiExportDoCSVVse.Click += new System.EventHandler(this.exportDoCSVVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoCSVOznacene
            // 
            this.tsmiExportDoCSVOznacene.Name = "tsmiExportDoCSVOznacene";
            this.tsmiExportDoCSVOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoCSVOznacene.Text = "Export do CSV označené";
            this.tsmiExportDoCSVOznacene.Click += new System.EventHandler(this.exportDoCSVOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiExportDoExcelVse
            // 
            this.tsmiExportDoExcelVse.Name = "tsmiExportDoExcelVse";
            this.tsmiExportDoExcelVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoExcelVse.Text = "Export do Excel vše";
            this.tsmiExportDoExcelVse.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoExcelOznacene
            // 
            this.tsmiExportDoExcelOznacene.Name = "tsmiExportDoExcelOznacene";
            this.tsmiExportDoExcelOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoExcelOznacene.Text = "Export do Excel označené";
            this.tsmiExportDoExcelOznacene.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiExportDoXMLVse
            // 
            this.tsmiExportDoXMLVse.Name = "tsmiExportDoXMLVse";
            this.tsmiExportDoXMLVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoXMLVse.Text = "Export do XML Vše";
            this.tsmiExportDoXMLVse.Click += new System.EventHandler(this.exportDoXMLVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoXMLOznacene
            // 
            this.tsmiExportDoXMLOznacene.Name = "tsmiExportDoXMLOznacene";
            this.tsmiExportDoXMLOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoXMLOznacene.Text = "Export do XML označené";
            this.tsmiExportDoXMLOznacene.Click += new System.EventHandler(this.exportDoXMLOznaceneToolStripMenuItem_Click);
            // 
            // tsmiPolozka
            // 
            this.tsmiPolozka.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOdstranit,
            this.tsmiUpravit,
            this.tsmiNovy});
            this.tsmiPolozka.Name = "tsmiPolozka";
            this.tsmiPolozka.Size = new System.Drawing.Size(60, 20);
            this.tsmiPolozka.Text = "Položka";
            // 
            // tsmiOdstranit
            // 
            this.tsmiOdstranit.Name = "tsmiOdstranit";
            this.tsmiOdstranit.Size = new System.Drawing.Size(152, 22);
            this.tsmiOdstranit.Text = "Odstranit";
            this.tsmiOdstranit.Click += new System.EventHandler(this.tsmiOdstranit_Click);
            // 
            // tsmiUpravit
            // 
            this.tsmiUpravit.Name = "tsmiUpravit";
            this.tsmiUpravit.Size = new System.Drawing.Size(152, 22);
            this.tsmiUpravit.Text = "Upravit";
            this.tsmiUpravit.Click += new System.EventHandler(this.tsmiUpravit_Click);
            // 
            // tsmiNovy
            // 
            this.tsmiNovy.Name = "tsmiNovy";
            this.tsmiNovy.Size = new System.Drawing.Size(152, 22);
            this.tsmiNovy.Text = "Nový";
            this.tsmiNovy.Click += new System.EventHandler(this.tsmiNovy_Click);
            // 
            // bwLoadExpedice
            // 
            this.bwLoadExpedice.WorkerSupportsCancellation = true;
            this.bwLoadExpedice.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadZbozi_DoWork);
            this.bwLoadExpedice.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadZbozi_RunWorkerCompleted);
            // 
            // panelButtonsZobrazeniList
            // 
            this.panelButtonsZobrazeniList.AutoScroll = true;
            this.panelButtonsZobrazeniList.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniList.Location = new System.Drawing.Point(743, 0);
            this.panelButtonsZobrazeniList.Name = "panelButtonsZobrazeniList";
            this.panelButtonsZobrazeniList.Size = new System.Drawing.Size(84, 468);
            this.panelButtonsZobrazeniList.TabIndex = 3;
            // 
            // FormBufferBaleniList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 468);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "FormBufferBaleniList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Buffer Baleni";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBufferBaleniList_FormClosing);
            this.Load += new System.EventHandler(this.FormBufferBaleniList_Load);
            this.Shown += new System.EventHandler(this.FormBufferBaleniList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormBufferBaleniList_KeyDown);
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExpedice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsExpedice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsExpedice)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public Zuby.ADGV.AdvancedDataGridView dgExpedice;
        private System.Windows.Forms.Panel panelButtonsZobrazeniVyber;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.BindingSource bsExpedice;
        private System.Windows.Forms.Button buttonVybratUzivatele;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuVyber;
        private System.Windows.Forms.ToolStripMenuItem tsmiVybrat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecVyber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMaterialNMBRPAL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMaterialITEMNMBR;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.ComponentModel.BackgroundWorker bwLoadExpedice;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtonsZobrazeniList;
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
        private Fask.Interfaces.DataSets.Expedice dsExpedice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbMaterialRozpracovano;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuList;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecList;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private System.Windows.Forms.ToolStripMenuItem tsmiPolozka;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravit;
        private System.Windows.Forms.ToolStripMenuItem tsmiNovy;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSHPPDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sERLTNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nMBRBALDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NMBRPAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDITNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cZCarKodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
    }
}