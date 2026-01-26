namespace Konzola.SkladLokace
{
    partial class FormSkladLokaceStavList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSkladLokaceStavList));
            this.dgSkladLokace = new Zuby.ADGV.AdvancedDataGridView();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY_OWNER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRAC_ID_OWNER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDDEFDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOCNCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sERLTNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cZCarKodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDDEFDATEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eXPIRATIONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATECHANGEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsSkladLokace = new System.Windows.Forms.BindingSource(this.components);
            this.dsSkladLokace = new Fask.Interfaces.DataSets.SkladLokace();
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.cb_NenulovyStav = new System.Windows.Forms.CheckBox();
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
            this.label5 = new System.Windows.Forms.Label();
            this.cbMaterialBarcode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMaterialITEMCODE = new System.Windows.Forms.ComboBox();
            this.cbMaterialPracID = new System.Windows.Forms.ComboBox();
            this.cbMaterialOznaceni = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMaterialITEMNMBR = new System.Windows.Forms.ComboBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenuVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVybrat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiKonecVybrat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonecList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVystup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExcelOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTiskRadkuEtiketa = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProvestPohyb = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_FillLokMechFromINV = new System.Windows.Forms.ToolStripMenuItem();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.bwLoadSkladLokaceStav = new System.ComponentModel.BackgroundWorker();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgSkladLokace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSkladLokace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSkladLokace)).BeginInit();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgSkladLokace
            // 
            this.dgSkladLokace.AllowUserToAddRows = false;
            this.dgSkladLokace.AllowUserToDeleteRows = false;
            this.dgSkladLokace.AllowUserToOrderColumns = true;
            this.dgSkladLokace.AllowUserToResizeRows = false;
            this.dgSkladLokace.AutoGenerateColumns = false;
            this.dgSkladLokace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSkladLokace.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.QTY_OWNER,
            this.PRAC_ID_OWNER,
            this.iTEMCODEDataGridViewTextBoxColumn,
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.qTYSHPPDDataGridViewTextBoxColumn,
            this.qTYSHPPDDEFDataGridViewTextBoxColumn,
            this.lOCNCODEDataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.sERLTNUMDataGridViewTextBoxColumn,
            this.vNDITNUMDataGridViewTextBoxColumn,
            this.cZCarKodDataGridViewTextBoxColumn,
            this.qTYSHPPDDEFDATEDataGridViewTextBoxColumn,
            this.eXPIRATIONDataGridViewTextBoxColumn,
            this.dATECHANGEDataGridViewTextBoxColumn});
            this.dgSkladLokace.DataSource = this.bsSkladLokace;
            this.dgSkladLokace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSkladLokace.EnableHeadersVisualStyles = false;
            this.dgSkladLokace.FilterAndSortEnabled = true;
            this.dgSkladLokace.Location = new System.Drawing.Point(0, 170);
            this.dgSkladLokace.Name = "dgSkladLokace";
            this.dgSkladLokace.ReadOnly = true;
            this.dgSkladLokace.RowHeadersVisible = false;
            this.dgSkladLokace.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSkladLokace.Size = new System.Drawing.Size(574, 298);
            this.dgSkladLokace.TabIndex = 1;
            this.dgSkladLokace.TabStop = false;
            this.dgSkladLokace.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSkladLokace_CellDoubleClick);
            this.dgSkladLokace.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgSkladLokace_CellMouseDown);
            this.dgSkladLokace.Sorted += new System.EventHandler(this.dgSkladLokace_Sorted);
            this.dgSkladLokace.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgSkladLokace_MouseClick);
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "ID materiálu";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // QTY_OWNER
            // 
            this.QTY_OWNER.DataPropertyName = "QTY_OWNER";
            this.QTY_OWNER.HeaderText = "Počet u pracovnika";
            this.QTY_OWNER.Name = "QTY_OWNER";
            this.QTY_OWNER.ReadOnly = true;
            // 
            // PRAC_ID_OWNER
            // 
            this.PRAC_ID_OWNER.DataPropertyName = "PRAC_ID_OWNER";
            this.PRAC_ID_OWNER.HeaderText = "ID Pracovnika";
            this.PRAC_ID_OWNER.Name = "PRAC_ID_OWNER";
            this.PRAC_ID_OWNER.ReadOnly = true;
            // 
            // iTEMCODEDataGridViewTextBoxColumn
            // 
            this.iTEMCODEDataGridViewTextBoxColumn.DataPropertyName = "ITEMCODE";
            this.iTEMCODEDataGridViewTextBoxColumn.HeaderText = "Kód materiálu";
            this.iTEMCODEDataGridViewTextBoxColumn.Name = "iTEMCODEDataGridViewTextBoxColumn";
            this.iTEMCODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Označení materiálu";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYSHPPDDataGridViewTextBoxColumn
            // 
            this.qTYSHPPDDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPD";
            this.qTYSHPPDDataGridViewTextBoxColumn.HeaderText = "Množství";
            this.qTYSHPPDDataGridViewTextBoxColumn.Name = "qTYSHPPDDataGridViewTextBoxColumn";
            this.qTYSHPPDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYSHPPDDEFDataGridViewTextBoxColumn
            // 
            this.qTYSHPPDDEFDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPD_DEF";
            this.qTYSHPPDDEFDataGridViewTextBoxColumn.HeaderText = "Množství inventarizováno";
            this.qTYSHPPDDEFDataGridViewTextBoxColumn.Name = "qTYSHPPDDEFDataGridViewTextBoxColumn";
            this.qTYSHPPDDEFDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lOCNCODEDataGridViewTextBoxColumn
            // 
            this.lOCNCODEDataGridViewTextBoxColumn.DataPropertyName = "LOCNCODE";
            this.lOCNCODEDataGridViewTextBoxColumn.HeaderText = "Lokace";
            this.lOCNCODEDataGridViewTextBoxColumn.Name = "lOCNCODEDataGridViewTextBoxColumn";
            this.lOCNCODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sKLIDDataGridViewTextBoxColumn
            // 
            this.sKLIDDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.HeaderText = "Sklad ID";
            this.sKLIDDataGridViewTextBoxColumn.Name = "sKLIDDataGridViewTextBoxColumn";
            this.sKLIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sERLTNUMDataGridViewTextBoxColumn
            // 
            this.sERLTNUMDataGridViewTextBoxColumn.DataPropertyName = "SERLTNUM";
            this.sERLTNUMDataGridViewTextBoxColumn.HeaderText = "Šarže";
            this.sERLTNUMDataGridViewTextBoxColumn.Name = "sERLTNUMDataGridViewTextBoxColumn";
            this.sERLTNUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vNDITNUMDataGridViewTextBoxColumn
            // 
            this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "Čár. kód dodavatele";
            this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
            this.vNDITNUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cZCarKodDataGridViewTextBoxColumn
            // 
            this.cZCarKodDataGridViewTextBoxColumn.DataPropertyName = "CZ_CarKod";
            this.cZCarKodDataGridViewTextBoxColumn.HeaderText = "Čár. kód";
            this.cZCarKodDataGridViewTextBoxColumn.Name = "cZCarKodDataGridViewTextBoxColumn";
            this.cZCarKodDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYSHPPDDEFDATEDataGridViewTextBoxColumn
            // 
            this.qTYSHPPDDEFDATEDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPD_DEF_DATE";
            this.qTYSHPPDDEFDATEDataGridViewTextBoxColumn.HeaderText = "Datum inventarizace";
            this.qTYSHPPDDEFDATEDataGridViewTextBoxColumn.Name = "qTYSHPPDDEFDATEDataGridViewTextBoxColumn";
            this.qTYSHPPDDEFDATEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // eXPIRATIONDataGridViewTextBoxColumn
            // 
            this.eXPIRATIONDataGridViewTextBoxColumn.DataPropertyName = "EXPIRATION";
            this.eXPIRATIONDataGridViewTextBoxColumn.HeaderText = "Expirace";
            this.eXPIRATIONDataGridViewTextBoxColumn.Name = "eXPIRATIONDataGridViewTextBoxColumn";
            this.eXPIRATIONDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dATECHANGEDataGridViewTextBoxColumn
            // 
            this.dATECHANGEDataGridViewTextBoxColumn.DataPropertyName = "DATECHANGE";
            this.dATECHANGEDataGridViewTextBoxColumn.HeaderText = "Datum změny";
            this.dATECHANGEDataGridViewTextBoxColumn.Name = "dATECHANGEDataGridViewTextBoxColumn";
            this.dATECHANGEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsSkladLokace
            // 
            this.bsSkladLokace.DataMember = "CZMST_SkladLokace_Stav";
            this.bsSkladLokace.DataSource = this.dsSkladLokace;
            // 
            // dsSkladLokace
            // 
            this.dsSkladLokace.DataSetName = "SkladLokace";
            this.dsSkladLokace.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panelButtonsZobrazeniVyber
            // 
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonKonec);
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonVybratUzivatele);
            this.panelButtonsZobrazeniVyber.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniVyber.Location = new System.Drawing.Point(574, 0);
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
            this.panelMain.Controls.Add(this.cb_NenulovyStav);
            this.panelMain.Controls.Add(this.tsFiltry);
            this.panelMain.Controls.Add(this.buttonVyhledat);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.cbMaterialBarcode);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.cbMaterialITEMCODE);
            this.panelMain.Controls.Add(this.cbMaterialPracID);
            this.panelMain.Controls.Add(this.cbMaterialOznaceni);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.cbMaterialITEMNMBR);
            this.panelMain.Controls.Add(this.menuStrip2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(574, 143);
            this.panelMain.TabIndex = 1;
            // 
            // cb_NenulovyStav
            // 
            this.cb_NenulovyStav.AutoSize = true;
            this.cb_NenulovyStav.Location = new System.Drawing.Point(300, 109);
            this.cb_NenulovyStav.Name = "cb_NenulovyStav";
            this.cb_NenulovyStav.Size = new System.Drawing.Size(125, 17);
            this.cb_NenulovyStav.TabIndex = 41;
            this.cb_NenulovyStav.Text = "Pouze nenulový stav";
            this.cb_NenulovyStav.UseVisualStyleBackColor = true;
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
            this.tsFiltry.Location = new System.Drawing.Point(0, 24);
            this.tsFiltry.Name = "tsFiltry";
            this.tsFiltry.Size = new System.Drawing.Size(574, 25);
            this.tsFiltry.TabIndex = 40;
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
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(495, 49);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 20;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(244, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Čár. kód:";
            this.label5.DoubleClick += new System.EventHandler(this.label5_DoubleClick);
            // 
            // cbMaterialBarcode
            // 
            this.cbMaterialBarcode.FormattingEnabled = true;
            this.cbMaterialBarcode.Location = new System.Drawing.Point(300, 79);
            this.cbMaterialBarcode.Name = "cbMaterialBarcode";
            this.cbMaterialBarcode.Size = new System.Drawing.Size(135, 21);
            this.cbMaterialBarcode.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(242, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Kód mat.:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Pracovník ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Označení mat.:";
            // 
            // cbMaterialITEMCODE
            // 
            this.cbMaterialITEMCODE.FormattingEnabled = true;
            this.cbMaterialITEMCODE.Location = new System.Drawing.Point(300, 52);
            this.cbMaterialITEMCODE.Name = "cbMaterialITEMCODE";
            this.cbMaterialITEMCODE.Size = new System.Drawing.Size(135, 21);
            this.cbMaterialITEMCODE.TabIndex = 3;
            // 
            // cbMaterialPracID
            // 
            this.cbMaterialPracID.FormattingEnabled = true;
            this.cbMaterialPracID.Location = new System.Drawing.Point(93, 105);
            this.cbMaterialPracID.Name = "cbMaterialPracID";
            this.cbMaterialPracID.Size = new System.Drawing.Size(135, 21);
            this.cbMaterialPracID.TabIndex = 2;
            // 
            // cbMaterialOznaceni
            // 
            this.cbMaterialOznaceni.FormattingEnabled = true;
            this.cbMaterialOznaceni.Location = new System.Drawing.Point(93, 79);
            this.cbMaterialOznaceni.Name = "cbMaterialOznaceni";
            this.cbMaterialOznaceni.Size = new System.Drawing.Size(135, 21);
            this.cbMaterialOznaceni.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "ID materiálu:";
            // 
            // cbMaterialITEMNMBR
            // 
            this.cbMaterialITEMNMBR.FormattingEnabled = true;
            this.cbMaterialITEMNMBR.Location = new System.Drawing.Point(93, 52);
            this.cbMaterialITEMNMBR.Name = "cbMaterialITEMNMBR";
            this.cbMaterialITEMNMBR.Size = new System.Drawing.Size(135, 21);
            this.cbMaterialITEMNMBR.TabIndex = 1;
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuVyber,
            this.tsmiMenuList,
            this.tsmiVystup,
            this.tsmiAkce});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(574, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // tsmiMenuVyber
            // 
            this.tsmiMenuVyber.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiVybrat,
            this.toolStripSeparator1,
            this.tsmiKonecVybrat});
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
            // tsmiKonecVybrat
            // 
            this.tsmiKonecVybrat.Name = "tsmiKonecVybrat";
            this.tsmiKonecVybrat.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonecVybrat.Size = new System.Drawing.Size(151, 22);
            this.tsmiKonecVybrat.Text = "Konec";
            this.tsmiKonecVybrat.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
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
            this.tsmiKonecList.Size = new System.Drawing.Size(148, 22);
            this.tsmiKonecList.Text = "Konec";
            this.tsmiKonecList.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // tsmiVystup
            // 
            this.tsmiVystup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator6,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExcelOznacene,
            this.toolStripSeparator7,
            this.tsmiExportDoXMLVse,
            this.tsmiExportDoXMLOznacene,
            this.toolStripSeparator8,
            this.tsmiTiskRadkuEtiketa});
            this.tsmiVystup.Name = "tsmiVystup";
            this.tsmiVystup.Size = new System.Drawing.Size(55, 20);
            this.tsmiVystup.Text = "Výstup";
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
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiTiskRadkuEtiketa
            // 
            this.tsmiTiskRadkuEtiketa.Name = "tsmiTiskRadkuEtiketa";
            this.tsmiTiskRadkuEtiketa.Size = new System.Drawing.Size(208, 22);
            this.tsmiTiskRadkuEtiketa.Text = "Tisk etiket";
            this.tsmiTiskRadkuEtiketa.Click += new System.EventHandler(this.tsmiTiskRadkuEtiketa_Click);
            // 
            // tsmiAkce
            // 
            this.tsmiAkce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiProvestPohyb,
            this.tsmi_FillLokMechFromINV});
            this.tsmiAkce.Name = "tsmiAkce";
            this.tsmiAkce.Size = new System.Drawing.Size(45, 20);
            this.tsmiAkce.Text = "Akce";
            // 
            // tsmiProvestPohyb
            // 
            this.tsmiProvestPohyb.Name = "tsmiProvestPohyb";
            this.tsmiProvestPohyb.Size = new System.Drawing.Size(244, 22);
            this.tsmiProvestPohyb.Text = "Provést pohyb";
            this.tsmiProvestPohyb.Click += new System.EventHandler(this.tsmiProvestPohyb_Click);
            // 
            // tsmi_FillLokMechFromINV
            // 
            this.tsmi_FillLokMechFromINV.Name = "tsmi_FillLokMechFromINV";
            this.tsmi_FillLokMechFromINV.Size = new System.Drawing.Size(244, 22);
            this.tsmi_FillLokMechFromINV.Text = "Naplnění Lok. Mech. z inventury";
            this.tsmi_FillLokMechFromINV.Click += new System.EventHandler(this.tsmi_FillLokMechFromINV_Click);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(247, 266);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(98, 98);
            this.progressIndicator1.TabIndex = 38;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // bwLoadSkladLokaceStav
            // 
            this.bwLoadSkladLokaceStav.WorkerSupportsCancellation = true;
            this.bwLoadSkladLokaceStav.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadZbozi_DoWork);
            this.bwLoadSkladLokaceStav.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadZbozi_RunWorkerCompleted);
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
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 143);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(574, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 39;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // FormSkladLokaceStavList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 468);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dgSkladLokace);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "FormSkladLokaceStavList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stav skladu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSkladLokaceStavList_FormClosing);
            this.Load += new System.EventHandler(this.FormSkladLokaceStavList_Load);
            this.Shown += new System.EventHandler(this.FormSkladLokaceStavList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSkladLokaceStavList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgSkladLokace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSkladLokace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSkladLokace)).EndInit();
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgSkladLokace;
        private System.Windows.Forms.Panel panelButtonsZobrazeniVyber;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.BindingSource bsSkladLokace;
        private System.Windows.Forms.Button buttonVybratUzivatele;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuVyber;
        private System.Windows.Forms.ToolStripMenuItem tsmiVybrat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecVybrat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbMaterialBarcode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMaterialITEMCODE;
        private System.Windows.Forms.ComboBox cbMaterialOznaceni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMaterialITEMNMBR;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.ComponentModel.BackgroundWorker bwLoadSkladLokaceStav;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtonsZobrazeniList;
        private Fask.Interfaces.DataSets.SkladLokace dsSkladLokace;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuList;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecList;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY_OWNER;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRAC_ID_OWNER;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSHPPDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSHPPDDEFDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOCNCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sERLTNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDITNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cZCarKodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSHPPDDEFDATEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eXPIRATIONDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATECHANGEDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbMaterialPracID;
        private System.Windows.Forms.ToolStripMenuItem tsmiVystup;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private System.Windows.Forms.ToolStripMenuItem tsmiAkce;
        private System.Windows.Forms.ToolStripMenuItem tsmiProvestPohyb;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_FillLokMechFromINV;
        private System.Windows.Forms.ToolStripMenuItem tsmiTiskRadkuEtiketa;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.CheckBox cb_NenulovyStav;
    }
}