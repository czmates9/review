namespace Konzola.Vyroba
{
    partial class FormProduction_SN_List
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProduction_SN_List));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_SOPNUMBE = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_ITEMCODE = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_SERLNMBR = new System.Windows.Forms.ComboBox();
            this.cb_ITEMDESC = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
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
            this.dtp_DatumDo = new System.Windows.Forms.DateTimePicker();
            this.dtp_DatumOd = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.buttonOznacitVse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.cb_USERID = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExporty = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskEtiketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExceOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.progressIndicatorVyrobek = new ProgressControls.ProgressIndicator();
            this.bw__P_SN = new System.ComponentModel.BackgroundWorker();
            this.dg_P_SN = new Zuby.ADGV.AdvancedDataGridView();
            this.bs_P_SN = new System.Windows.Forms.BindingSource(this.components);
            this.ds_P_SN = new Fask.Interfaces.DataSets.Vyroba();
            this.adgvstb_P_SN = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.dateeveDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYPACKMJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countEntriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPNUMBEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.popiszakazkyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operationNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMTYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMTYPETextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodePDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serNumTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSERLTNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sERLNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expiraceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rEZ1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rEZ2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rEZ3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rEZ4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.termIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skldescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vetev1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vetev2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vetev3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vetev4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vetev5DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vetev6DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vetev7DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_P_SN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_P_SN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_P_SN)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Výrobní Zakázka:";
            // 
            // cb_SOPNUMBE
            // 
            this.cb_SOPNUMBE.FormattingEnabled = true;
            this.cb_SOPNUMBE.Location = new System.Drawing.Point(109, 35);
            this.cb_SOPNUMBE.Name = "cb_SOPNUMBE";
            this.cb_SOPNUMBE.Size = new System.Drawing.Size(249, 21);
            this.cb_SOPNUMBE.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kód položky:";
            // 
            // cb_ITEMCODE
            // 
            this.cb_ITEMCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cb_ITEMCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cb_ITEMCODE.FormattingEnabled = true;
            this.cb_ITEMCODE.Location = new System.Drawing.Point(109, 63);
            this.cb_ITEMCODE.Name = "cb_ITEMCODE";
            this.cb_ITEMCODE.Size = new System.Drawing.Size(249, 21);
            this.cb_ITEMCODE.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cb_SERLNMBR);
            this.panel1.Controls.Add(this.cb_ITEMDESC);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Controls.Add(this.dtp_DatumDo);
            this.panel1.Controls.Add(this.dtp_DatumOd);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.buttonOdznacitVse);
            this.panel1.Controls.Add(this.buttonOznacitVse);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cb_USERID);
            this.panel1.Controls.Add(this.cb_ITEMCODE);
            this.panel1.Controls.Add(this.cb_SOPNUMBE);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 235);
            this.panel1.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(371, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "SN/šarže:";
            // 
            // cb_SERLNMBR
            // 
            this.cb_SERLNMBR.FormattingEnabled = true;
            this.cb_SERLNMBR.Location = new System.Drawing.Point(443, 35);
            this.cb_SERLNMBR.Name = "cb_SERLNMBR";
            this.cb_SERLNMBR.Size = new System.Drawing.Size(249, 21);
            this.cb_SERLNMBR.TabIndex = 43;
            // 
            // cb_ITEMDESC
            // 
            this.cb_ITEMDESC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cb_ITEMDESC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cb_ITEMDESC.FormattingEnabled = true;
            this.cb_ITEMDESC.Location = new System.Drawing.Point(109, 89);
            this.cb_ITEMDESC.Name = "cb_ITEMDESC";
            this.cb_ITEMDESC.Size = new System.Drawing.Size(249, 21);
            this.cb_ITEMDESC.TabIndex = 41;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Název položky:";
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
            this.tsFiltry.Size = new System.Drawing.Size(818, 25);
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
            // 
            // dtp_DatumDo
            // 
            this.dtp_DatumDo.Checked = false;
            this.dtp_DatumDo.CustomFormat = "HH:mm dd.MM.yyyy";
            this.dtp_DatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_DatumDo.Location = new System.Drawing.Point(109, 182);
            this.dtp_DatumDo.Name = "dtp_DatumDo";
            this.dtp_DatumDo.ShowCheckBox = true;
            this.dtp_DatumDo.Size = new System.Drawing.Size(249, 20);
            this.dtp_DatumDo.TabIndex = 6;
            // 
            // dtp_DatumOd
            // 
            this.dtp_DatumOd.Checked = false;
            this.dtp_DatumOd.CustomFormat = "HH:mm dd.MM.yyyy";
            this.dtp_DatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_DatumOd.Location = new System.Drawing.Point(109, 156);
            this.dtp_DatumOd.Name = "dtp_DatumOd";
            this.dtp_DatumOd.ShowCheckBox = true;
            this.dtp_DatumOd.Size = new System.Drawing.Size(249, 20);
            this.dtp_DatumOd.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Datum do:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Datum od:";
            // 
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdznacitVse.Location = new System.Drawing.Point(714, 185);
            this.buttonOdznacitVse.Name = "buttonOdznacitVse";
            this.buttonOdznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOdznacitVse.TabIndex = 13;
            this.buttonOdznacitVse.Text = "Odznačit vše";
            this.buttonOdznacitVse.UseVisualStyleBackColor = true;
            this.buttonOdznacitVse.Click += new System.EventHandler(this.buttonOdznacitVse_Click);
            // 
            // buttonOznacitVse
            // 
            this.buttonOznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOznacitVse.Location = new System.Drawing.Point(625, 185);
            this.buttonOznacitVse.Name = "buttonOznacitVse";
            this.buttonOznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOznacitVse.TabIndex = 12;
            this.buttonOznacitVse.Text = "Označit vše";
            this.buttonOznacitVse.UseVisualStyleBackColor = true;
            this.buttonOznacitVse.Click += new System.EventHandler(this.buttonOznacitVse_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "ID uživatele:";
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(724, 116);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 11;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // cb_USERID
            // 
            this.cb_USERID.FormattingEnabled = true;
            this.cb_USERID.Location = new System.Drawing.Point(109, 115);
            this.cb_USERID.Name = "cb_USERID";
            this.cb_USERID.Size = new System.Drawing.Size(249, 21);
            this.cb_USERID.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiExporty});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(919, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
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
            this.tsmiKonec.Click += new System.EventHandler(this.tsmiKonec_Click);
            // 
            // tsmiExporty
            // 
            this.tsmiExporty.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiskToolStripMenuItem,
            this.tiskEtiketToolStripMenuItem,
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator6,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExceOznacene,
            this.toolStripSeparator7,
            this.tsmiExportDoXMLVse,
            this.tsmiExportDoXMLOznacene});
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
            // tsmiExportDoExceOznacene
            // 
            this.tsmiExportDoExceOznacene.Name = "tsmiExportDoExceOznacene";
            this.tsmiExportDoExceOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoExceOznacene.Text = "Export do Excel označené";
            this.tsmiExportDoExceOznacene.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
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
            // progressIndicatorVyrobek
            // 
            this.progressIndicatorVyrobek.Location = new System.Drawing.Point(357, 365);
            this.progressIndicatorVyrobek.Name = "progressIndicatorVyrobek";
            this.progressIndicatorVyrobek.Percentage = 0F;
            this.progressIndicatorVyrobek.Size = new System.Drawing.Size(98, 98);
            this.progressIndicatorVyrobek.TabIndex = 105;
            this.progressIndicatorVyrobek.Text = "progressIndicator1";
            this.progressIndicatorVyrobek.Visible = false;
            // 
            // bw__P_SN
            // 
            this.bw__P_SN.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw__P_SN_DoWork);
            this.bw__P_SN.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw__P_SN_RunWorkerCompleted);
            // 
            // dg_P_SN
            // 
            this.dg_P_SN.AllowUserToAddRows = false;
            this.dg_P_SN.AllowUserToDeleteRows = false;
            this.dg_P_SN.AllowUserToOrderColumns = true;
            this.dg_P_SN.AllowUserToResizeRows = false;
            this.dg_P_SN.AutoGenerateColumns = false;
            this.dg_P_SN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_P_SN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateeveDataGridViewTextBoxColumn,
            this.QTYPACKMJ,
            this.countEntriesDataGridViewTextBoxColumn,
            this.sOPNUMBEDataGridViewTextBoxColumn,
            this.popiszakazkyDataGridViewTextBoxColumn,
            this.operationNameDataGridViewTextBoxColumn,
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMCODEDataGridViewTextBoxColumn,
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.iTEMTYPEDataGridViewTextBoxColumn,
            this.iTEMTYPETextDataGridViewTextBoxColumn,
            this.barcodePDataGridViewTextBoxColumn,
            this.vNDITNUMDataGridViewTextBoxColumn,
            this.qtyDataGridViewTextBoxColumn,
            this.serNumTDataGridViewTextBoxColumn,
            this.qTYSERLTNUMDataGridViewTextBoxColumn,
            this.sERLNMBRDataGridViewTextBoxColumn,
            this.expiraceDataGridViewTextBoxColumn,
            this.rEZ1DataGridViewTextBoxColumn,
            this.rEZ2DataGridViewTextBoxColumn,
            this.rEZ3DataGridViewTextBoxColumn,
            this.rEZ4DataGridViewTextBoxColumn,
            this.machineidDataGridViewTextBoxColumn,
            this.machineNameDataGridViewTextBoxColumn,
            this.userIDDataGridViewTextBoxColumn,
            this.firstnameDataGridViewTextBoxColumn,
            this.surname,
            this.termIDDataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.skldescDataGridViewTextBoxColumn,
            this.vetev1DataGridViewTextBoxColumn,
            this.vetev2DataGridViewTextBoxColumn,
            this.vetev3DataGridViewTextBoxColumn,
            this.vetev4DataGridViewTextBoxColumn,
            this.vetev5DataGridViewTextBoxColumn,
            this.vetev6DataGridViewTextBoxColumn,
            this.vetev7DataGridViewTextBoxColumn});
            this.dg_P_SN.DataSource = this.bs_P_SN;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_P_SN.DefaultCellStyle = dataGridViewCellStyle1;
            this.dg_P_SN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_P_SN.EnableHeadersVisualStyles = false;
            this.dg_P_SN.FilterAndSortEnabled = true;
            this.dg_P_SN.Location = new System.Drawing.Point(0, 286);
            this.dg_P_SN.Name = "dg_P_SN";
            this.dg_P_SN.ReadOnly = true;
            this.dg_P_SN.RowHeadersVisible = false;
            this.dg_P_SN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_P_SN.Size = new System.Drawing.Size(818, 296);
            this.dg_P_SN.TabIndex = 30;
            this.dg_P_SN.TabStop = false;
            // 
            // bs_P_SN
            // 
            this.bs_P_SN.DataMember = "Production_SN_Pohled";
            this.bs_P_SN.DataSource = this.ds_P_SN;
            // 
            // ds_P_SN
            // 
            this.ds_P_SN.DataSetName = "VyrobaDataSet";
            this.ds_P_SN.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // adgvstb_P_SN
            // 
            this.adgvstb_P_SN.AllowMerge = false;
            this.adgvstb_P_SN.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.adgvstb_P_SN.Location = new System.Drawing.Point(0, 259);
            this.adgvstb_P_SN.MaximumSize = new System.Drawing.Size(0, 27);
            this.adgvstb_P_SN.MinimumSize = new System.Drawing.Size(0, 27);
            this.adgvstb_P_SN.Name = "adgvstb_P_SN";
            this.adgvstb_P_SN.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.adgvstb_P_SN.Size = new System.Drawing.Size(818, 27);
            this.adgvstb_P_SN.TabIndex = 106;
            this.adgvstb_P_SN.Text = "advancedDataGridViewSearchToolBar1";
            this.adgvstb_P_SN.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.adgvstb_P_SN_Search);
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(818, 24);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(101, 558);
            this.panelButtons.TabIndex = 8;
            // 
            // dateeveDataGridViewTextBoxColumn
            // 
            this.dateeveDataGridViewTextBoxColumn.DataPropertyName = "dateeve";
            this.dateeveDataGridViewTextBoxColumn.HeaderText = "Datum";
            this.dateeveDataGridViewTextBoxColumn.Name = "dateeveDataGridViewTextBoxColumn";
            this.dateeveDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // QTYPACKMJ
            // 
            this.QTYPACKMJ.DataPropertyName = "QTYPACKMJ";
            this.QTYPACKMJ.HeaderText = "Měrná jednotka bal.";
            this.QTYPACKMJ.Name = "QTYPACKMJ";
            this.QTYPACKMJ.ReadOnly = true;
            // 
            // countEntriesDataGridViewTextBoxColumn
            // 
            this.countEntriesDataGridViewTextBoxColumn.DataPropertyName = "CountEntries";
            this.countEntriesDataGridViewTextBoxColumn.HeaderText = "Číslo dávky";
            this.countEntriesDataGridViewTextBoxColumn.Name = "countEntriesDataGridViewTextBoxColumn";
            this.countEntriesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sOPNUMBEDataGridViewTextBoxColumn
            // 
            this.sOPNUMBEDataGridViewTextBoxColumn.DataPropertyName = "SOPNUMBE";
            this.sOPNUMBEDataGridViewTextBoxColumn.HeaderText = "Výrobní zakázka";
            this.sOPNUMBEDataGridViewTextBoxColumn.Name = "sOPNUMBEDataGridViewTextBoxColumn";
            this.sOPNUMBEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // popiszakazkyDataGridViewTextBoxColumn
            // 
            this.popiszakazkyDataGridViewTextBoxColumn.DataPropertyName = "popiszakazky";
            this.popiszakazkyDataGridViewTextBoxColumn.HeaderText = "Popis zakázky";
            this.popiszakazkyDataGridViewTextBoxColumn.Name = "popiszakazkyDataGridViewTextBoxColumn";
            this.popiszakazkyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // operationNameDataGridViewTextBoxColumn
            // 
            this.operationNameDataGridViewTextBoxColumn.DataPropertyName = "operationName";
            this.operationNameDataGridViewTextBoxColumn.HeaderText = "Název operace";
            this.operationNameDataGridViewTextBoxColumn.Name = "operationNameDataGridViewTextBoxColumn";
            this.operationNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "ID položky";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMCODEDataGridViewTextBoxColumn
            // 
            this.iTEMCODEDataGridViewTextBoxColumn.DataPropertyName = "ITEMCODE";
            this.iTEMCODEDataGridViewTextBoxColumn.HeaderText = "Kód položky";
            this.iTEMCODEDataGridViewTextBoxColumn.Name = "iTEMCODEDataGridViewTextBoxColumn";
            this.iTEMCODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Nazev položky";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMTYPEDataGridViewTextBoxColumn
            // 
            this.iTEMTYPEDataGridViewTextBoxColumn.DataPropertyName = "ITEMTYPE";
            this.iTEMTYPEDataGridViewTextBoxColumn.HeaderText = "Typ položky";
            this.iTEMTYPEDataGridViewTextBoxColumn.Name = "iTEMTYPEDataGridViewTextBoxColumn";
            this.iTEMTYPEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMTYPETextDataGridViewTextBoxColumn
            // 
            this.iTEMTYPETextDataGridViewTextBoxColumn.DataPropertyName = "ITEMTYPE_Text";
            this.iTEMTYPETextDataGridViewTextBoxColumn.HeaderText = "Typ položky Text";
            this.iTEMTYPETextDataGridViewTextBoxColumn.Name = "iTEMTYPETextDataGridViewTextBoxColumn";
            this.iTEMTYPETextDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // barcodePDataGridViewTextBoxColumn
            // 
            this.barcodePDataGridViewTextBoxColumn.DataPropertyName = "BarcodeP";
            this.barcodePDataGridViewTextBoxColumn.HeaderText = "Čár. kód položky vyr.";
            this.barcodePDataGridViewTextBoxColumn.Name = "barcodePDataGridViewTextBoxColumn";
            this.barcodePDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vNDITNUMDataGridViewTextBoxColumn
            // 
            this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "Čar. kód položky";
            this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
            this.vNDITNUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qtyDataGridViewTextBoxColumn
            // 
            this.qtyDataGridViewTextBoxColumn.DataPropertyName = "qty";
            this.qtyDataGridViewTextBoxColumn.HeaderText = "Množství";
            this.qtyDataGridViewTextBoxColumn.Name = "qtyDataGridViewTextBoxColumn";
            this.qtyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // serNumTDataGridViewTextBoxColumn
            // 
            this.serNumTDataGridViewTextBoxColumn.DataPropertyName = "SerNumT";
            this.serNumTDataGridViewTextBoxColumn.HeaderText = "Typ sledovaní";
            this.serNumTDataGridViewTextBoxColumn.Name = "serNumTDataGridViewTextBoxColumn";
            this.serNumTDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYSERLTNUMDataGridViewTextBoxColumn
            // 
            this.qTYSERLTNUMDataGridViewTextBoxColumn.DataPropertyName = "QTY_SERLTNUM";
            this.qTYSERLTNUMDataGridViewTextBoxColumn.HeaderText = "Množství u SN/šarže";
            this.qTYSERLTNUMDataGridViewTextBoxColumn.Name = "qTYSERLTNUMDataGridViewTextBoxColumn";
            this.qTYSERLTNUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sERLNMBRDataGridViewTextBoxColumn
            // 
            this.sERLNMBRDataGridViewTextBoxColumn.DataPropertyName = "SERLNMBR";
            this.sERLNMBRDataGridViewTextBoxColumn.HeaderText = "SN/Šarže";
            this.sERLNMBRDataGridViewTextBoxColumn.Name = "sERLNMBRDataGridViewTextBoxColumn";
            this.sERLNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // expiraceDataGridViewTextBoxColumn
            // 
            this.expiraceDataGridViewTextBoxColumn.DataPropertyName = "Expirace";
            this.expiraceDataGridViewTextBoxColumn.HeaderText = "Expirace";
            this.expiraceDataGridViewTextBoxColumn.Name = "expiraceDataGridViewTextBoxColumn";
            this.expiraceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rEZ1DataGridViewTextBoxColumn
            // 
            this.rEZ1DataGridViewTextBoxColumn.DataPropertyName = "REZ_1";
            this.rEZ1DataGridViewTextBoxColumn.HeaderText = "REZ_1";
            this.rEZ1DataGridViewTextBoxColumn.Name = "rEZ1DataGridViewTextBoxColumn";
            this.rEZ1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rEZ2DataGridViewTextBoxColumn
            // 
            this.rEZ2DataGridViewTextBoxColumn.DataPropertyName = "REZ_2";
            this.rEZ2DataGridViewTextBoxColumn.HeaderText = "REZ_2";
            this.rEZ2DataGridViewTextBoxColumn.Name = "rEZ2DataGridViewTextBoxColumn";
            this.rEZ2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rEZ3DataGridViewTextBoxColumn
            // 
            this.rEZ3DataGridViewTextBoxColumn.DataPropertyName = "REZ_3";
            this.rEZ3DataGridViewTextBoxColumn.HeaderText = "REZ_3";
            this.rEZ3DataGridViewTextBoxColumn.Name = "rEZ3DataGridViewTextBoxColumn";
            this.rEZ3DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rEZ4DataGridViewTextBoxColumn
            // 
            this.rEZ4DataGridViewTextBoxColumn.DataPropertyName = "REZ_4";
            this.rEZ4DataGridViewTextBoxColumn.HeaderText = "REZ_4";
            this.rEZ4DataGridViewTextBoxColumn.Name = "rEZ4DataGridViewTextBoxColumn";
            this.rEZ4DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // machineidDataGridViewTextBoxColumn
            // 
            this.machineidDataGridViewTextBoxColumn.DataPropertyName = "machineid";
            this.machineidDataGridViewTextBoxColumn.HeaderText = "ID stroje";
            this.machineidDataGridViewTextBoxColumn.Name = "machineidDataGridViewTextBoxColumn";
            this.machineidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // machineNameDataGridViewTextBoxColumn
            // 
            this.machineNameDataGridViewTextBoxColumn.DataPropertyName = "machineName";
            this.machineNameDataGridViewTextBoxColumn.HeaderText = "Název stroje";
            this.machineNameDataGridViewTextBoxColumn.Name = "machineNameDataGridViewTextBoxColumn";
            this.machineNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userIDDataGridViewTextBoxColumn
            // 
            this.userIDDataGridViewTextBoxColumn.DataPropertyName = "UserID";
            this.userIDDataGridViewTextBoxColumn.HeaderText = "ID uživatele";
            this.userIDDataGridViewTextBoxColumn.Name = "userIDDataGridViewTextBoxColumn";
            this.userIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firstnameDataGridViewTextBoxColumn
            // 
            this.firstnameDataGridViewTextBoxColumn.DataPropertyName = "firstname";
            this.firstnameDataGridViewTextBoxColumn.HeaderText = "Jméno";
            this.firstnameDataGridViewTextBoxColumn.Name = "firstnameDataGridViewTextBoxColumn";
            this.firstnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // surname
            // 
            this.surname.DataPropertyName = "surname";
            this.surname.HeaderText = "Příjmení";
            this.surname.Name = "surname";
            this.surname.ReadOnly = true;
            // 
            // termIDDataGridViewTextBoxColumn
            // 
            this.termIDDataGridViewTextBoxColumn.DataPropertyName = "TermID";
            this.termIDDataGridViewTextBoxColumn.HeaderText = "ID Terminálu";
            this.termIDDataGridViewTextBoxColumn.Name = "termIDDataGridViewTextBoxColumn";
            this.termIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sKLIDDataGridViewTextBoxColumn
            // 
            this.sKLIDDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.HeaderText = "Sklad ID";
            this.sKLIDDataGridViewTextBoxColumn.Name = "sKLIDDataGridViewTextBoxColumn";
            this.sKLIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // skldescDataGridViewTextBoxColumn
            // 
            this.skldescDataGridViewTextBoxColumn.DataPropertyName = "skl_desc";
            this.skldescDataGridViewTextBoxColumn.HeaderText = "Sklad Název";
            this.skldescDataGridViewTextBoxColumn.Name = "skldescDataGridViewTextBoxColumn";
            this.skldescDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vetev1DataGridViewTextBoxColumn
            // 
            this.vetev1DataGridViewTextBoxColumn.DataPropertyName = "Vetev1";
            this.vetev1DataGridViewTextBoxColumn.HeaderText = "Sklad větev 1";
            this.vetev1DataGridViewTextBoxColumn.Name = "vetev1DataGridViewTextBoxColumn";
            this.vetev1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vetev2DataGridViewTextBoxColumn
            // 
            this.vetev2DataGridViewTextBoxColumn.DataPropertyName = "Vetev2";
            this.vetev2DataGridViewTextBoxColumn.HeaderText = "Sklad větev 2";
            this.vetev2DataGridViewTextBoxColumn.Name = "vetev2DataGridViewTextBoxColumn";
            this.vetev2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vetev3DataGridViewTextBoxColumn
            // 
            this.vetev3DataGridViewTextBoxColumn.DataPropertyName = "Vetev3";
            this.vetev3DataGridViewTextBoxColumn.HeaderText = "Sklad větev 3";
            this.vetev3DataGridViewTextBoxColumn.Name = "vetev3DataGridViewTextBoxColumn";
            this.vetev3DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vetev4DataGridViewTextBoxColumn
            // 
            this.vetev4DataGridViewTextBoxColumn.DataPropertyName = "Vetev4";
            this.vetev4DataGridViewTextBoxColumn.HeaderText = "Sklad větev 4";
            this.vetev4DataGridViewTextBoxColumn.Name = "vetev4DataGridViewTextBoxColumn";
            this.vetev4DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vetev5DataGridViewTextBoxColumn
            // 
            this.vetev5DataGridViewTextBoxColumn.DataPropertyName = "Vetev5";
            this.vetev5DataGridViewTextBoxColumn.HeaderText = "Sklad větev 5";
            this.vetev5DataGridViewTextBoxColumn.Name = "vetev5DataGridViewTextBoxColumn";
            this.vetev5DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vetev6DataGridViewTextBoxColumn
            // 
            this.vetev6DataGridViewTextBoxColumn.DataPropertyName = "Vetev6";
            this.vetev6DataGridViewTextBoxColumn.HeaderText = "Sklad větev 6";
            this.vetev6DataGridViewTextBoxColumn.Name = "vetev6DataGridViewTextBoxColumn";
            this.vetev6DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vetev7DataGridViewTextBoxColumn
            // 
            this.vetev7DataGridViewTextBoxColumn.DataPropertyName = "Vetev7";
            this.vetev7DataGridViewTextBoxColumn.HeaderText = "Sklad větev 7";
            this.vetev7DataGridViewTextBoxColumn.Name = "vetev7DataGridViewTextBoxColumn";
            this.vetev7DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FormProduction_SN_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 582);
            this.Controls.Add(this.progressIndicatorVyrobek);
            this.Controls.Add(this.dg_P_SN);
            this.Controls.Add(this.adgvstb_P_SN);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormProduction_SN_List";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Přehled výrobky";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProduction_SN_List_FormClosing);
            this.Load += new System.EventHandler(this.FormProduction_SN_List_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormProduction_SN_List_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_P_SN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_P_SN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_P_SN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_SOPNUMBE;
        public Fask.Interfaces.DataSets.Vyroba ds_P_SN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_ITEMCODE;
        private System.Windows.Forms.Panel panel1;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.BindingSource bs_P_SN;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_USERID;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Button buttonOznacitVse;
        private System.Windows.Forms.DateTimePicker dtp_DatumDo;
        private System.Windows.Forms.DateTimePicker dtp_DatumOd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Zuby.ADGV.AdvancedDataGridView dg_P_SN;
        private ProgressControls.ProgressIndicator progressIndicatorVyrobek;
        private System.ComponentModel.BackgroundWorker bw__P_SN;
        private System.Windows.Forms.ToolStripMenuItem tsmiExporty;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExceOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar adgvstb_P_SN;
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
        private System.Windows.Forms.ComboBox cb_ITEMDESC;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_SERLNMBR;
        private System.Windows.Forms.ToolStripMenuItem tiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tiskEtiketToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateeveDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYPACKMJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPNUMBEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn popiszakazkyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operationNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMTYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMTYPETextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodePDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDITNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serNumTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSERLTNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sERLNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expiraceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rEZ1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rEZ2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rEZ3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rEZ4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn termIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn skldescDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vetev1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vetev2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vetev3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vetev4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vetev5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vetev6DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vetev7DataGridViewTextBoxColumn;
    }
}