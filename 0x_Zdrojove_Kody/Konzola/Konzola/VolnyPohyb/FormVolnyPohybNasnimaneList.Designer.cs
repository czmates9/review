namespace Konzola.VolnyPohyb
{
    partial class FormVolnyPohybNasnimaneList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVolnyPohybNasnimaneList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskEtiketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExcelOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImportovatDavku = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_KlavesovyVystup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPolozka = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravit = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tB_ID_terminalu = new System.Windows.Forms.TextBox();
            this.tB_ID_uzivatele = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tB_kod_polozky = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tB_Typ_pohybu2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tB_Typ_pohybu = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tB_Nazev_polozky = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtp_porizeno_OD = new System.Windows.Forms.DateTimePicker();
            this.cb_Porizeno_TimeVariant = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtp_porizeno_DO = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
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
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbITEMNMBR = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCountEntries = new System.Windows.Forms.ComboBox();
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.buttonOznacitVse = new System.Windows.Forms.Button();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.bwLoadVolnyPohyb = new System.ComponentModel.BackgroundWorker();
            this.bwExport = new System.ComponentModel.BackgroundWorker();
            this.dgProdej = new Zuby.ADGV.AdvancedDataGridView();
            this.bsProdej = new System.Windows.Forms.BindingSource(this.components);
            this.dsProdej = new Fask.Interfaces.DataSets.Prodej();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.countEntriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cZCarKodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oDBIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sTRIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dOCIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dOCID2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pRACIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOCNCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDMJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYPACKDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sERLTNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tAXAMPIEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aMOUNPIEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wITHTAXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pRICEXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menaIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tAXAMPIEMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aMOUNPIEMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menaIDMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rEZ1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rEZ2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rEZ3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rEZ4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uSERIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATEDONEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMEDONEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEXROWIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iNPUTMODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDTERMINALDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOCNCODEDESTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDESTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXPIRACE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NMBRPAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TYPEPAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRINTED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttributeToSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProdej)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProdej)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProdej)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiExport,
            this.tsmiAkce,
            this.tsmiPolozka});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(997, 24);
            this.menuStrip1.TabIndex = 0;
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
            this.tsmiKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiskToolStripMenuItem,
            this.tiskEtiketToolStripMenuItem,
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator1,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExcelOznacene,
            this.toolStripSeparator6,
            this.tsmiExportDoXMLVse,
            this.tsmiExportDoXMLOznacene,
            this.toolStripSeparator7});
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(55, 20);
            this.tsmiExport.Text = "Výstup";
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
            this.tiskEtiketToolStripMenuItem.Text = "Tisk Etiket";
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
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
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(205, 6);
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
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiAkce
            // 
            this.tsmiAkce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiImportovatDavku,
            this.tsmi_KlavesovyVystup});
            this.tsmiAkce.Name = "tsmiAkce";
            this.tsmiAkce.Size = new System.Drawing.Size(45, 20);
            this.tsmiAkce.Text = "Akce";
            // 
            // tsmiImportovatDavku
            // 
            this.tsmiImportovatDavku.Name = "tsmiImportovatDavku";
            this.tsmiImportovatDavku.Size = new System.Drawing.Size(164, 22);
            this.tsmiImportovatDavku.Text = "Exportovat";
            this.tsmiImportovatDavku.Click += new System.EventHandler(this.tsmiImportovatDavku_Click);
            // 
            // tsmi_KlavesovyVystup
            // 
            this.tsmi_KlavesovyVystup.Name = "tsmi_KlavesovyVystup";
            this.tsmi_KlavesovyVystup.Size = new System.Drawing.Size(164, 22);
            this.tsmi_KlavesovyVystup.Text = "Klávesový výstup";
            this.tsmi_KlavesovyVystup.Click += new System.EventHandler(this.tsmi_KlavesovyVystup_Click);
            // 
            // tsmiPolozka
            // 
            this.tsmiPolozka.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOdstranit,
            this.tsmiUpravit});
            this.tsmiPolozka.Name = "tsmiPolozka";
            this.tsmiPolozka.Size = new System.Drawing.Size(60, 20);
            this.tsmiPolozka.Text = "Položka";
            // 
            // tsmiOdstranit
            // 
            this.tsmiOdstranit.Name = "tsmiOdstranit";
            this.tsmiOdstranit.Size = new System.Drawing.Size(123, 22);
            this.tsmiOdstranit.Text = "Odstranit";
            this.tsmiOdstranit.Click += new System.EventHandler(this.tsmiOdstranit_Click);
            // 
            // tsmiUpravit
            // 
            this.tsmiUpravit.Name = "tsmiUpravit";
            this.tsmiUpravit.Size = new System.Drawing.Size(123, 22);
            this.tsmiUpravit.Text = "Upravit";
            this.tsmiUpravit.Click += new System.EventHandler(this.tsmiUpravit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tB_ID_terminalu);
            this.panel1.Controls.Add(this.tB_ID_uzivatele);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.tB_kod_polozky);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.tB_Typ_pohybu2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.tB_Typ_pohybu);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tB_Nazev_polozky);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbITEMNMBR);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbCountEntries);
            this.panel1.Controls.Add(this.buttonOdznacitVse);
            this.panel1.Controls.Add(this.buttonOznacitVse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(913, 263);
            this.panel1.TabIndex = 1;
            // 
            // tB_ID_terminalu
            // 
            this.tB_ID_terminalu.Location = new System.Drawing.Point(363, 142);
            this.tB_ID_terminalu.Name = "tB_ID_terminalu";
            this.tB_ID_terminalu.Size = new System.Drawing.Size(178, 20);
            this.tB_ID_terminalu.TabIndex = 60;
            // 
            // tB_ID_uzivatele
            // 
            this.tB_ID_uzivatele.Location = new System.Drawing.Point(362, 110);
            this.tB_ID_uzivatele.Name = "tB_ID_uzivatele";
            this.tB_ID_uzivatele.Size = new System.Drawing.Size(178, 20);
            this.tB_ID_uzivatele.TabIndex = 60;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(290, 145);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 59;
            this.label11.Text = "ID terminálu:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(290, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 59;
            this.label10.Text = "ID uživatele:";
            // 
            // tB_kod_polozky
            // 
            this.tB_kod_polozky.Location = new System.Drawing.Point(362, 84);
            this.tB_kod_polozky.Name = "tB_kod_polozky";
            this.tB_kod_polozky.Size = new System.Drawing.Size(178, 20);
            this.tB_kod_polozky.TabIndex = 60;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(288, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 59;
            this.label9.Text = "Kód položky:";
            // 
            // tB_Typ_pohybu2
            // 
            this.tB_Typ_pohybu2.Location = new System.Drawing.Point(362, 58);
            this.tB_Typ_pohybu2.Name = "tB_Typ_pohybu2";
            this.tB_Typ_pohybu2.Size = new System.Drawing.Size(178, 20);
            this.tB_Typ_pohybu2.TabIndex = 60;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(280, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "Typ pohybu 2:";
            // 
            // tB_Typ_pohybu
            // 
            this.tB_Typ_pohybu.Location = new System.Drawing.Point(362, 28);
            this.tB_Typ_pohybu.Name = "tB_Typ_pohybu";
            this.tB_Typ_pohybu.Size = new System.Drawing.Size(178, 20);
            this.tB_Typ_pohybu.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(289, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 59;
            this.label4.Text = "Typ pohybu:";
            // 
            // tB_Nazev_polozky
            // 
            this.tB_Nazev_polozky.Location = new System.Drawing.Point(92, 87);
            this.tB_Nazev_polozky.Name = "tB_Nazev_polozky";
            this.tB_Nazev_polozky.Size = new System.Drawing.Size(178, 20);
            this.tB_Nazev_polozky.TabIndex = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Název položky:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtp_porizeno_OD);
            this.groupBox2.Controls.Add(this.cb_Porizeno_TimeVariant);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.dtp_porizeno_DO);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(23, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(517, 74);
            this.groupBox2.TabIndex = 58;
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
            this.tsFiltry.Size = new System.Drawing.Size(913, 25);
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
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(834, 28);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 14;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Č. položky:";
            // 
            // cbITEMNMBR
            // 
            this.cbITEMNMBR.FormattingEnabled = true;
            this.cbITEMNMBR.Location = new System.Drawing.Point(92, 59);
            this.cbITEMNMBR.Name = "cbITEMNMBR";
            this.cbITEMNMBR.Size = new System.Drawing.Size(178, 21);
            this.cbITEMNMBR.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Číslo dávky:";
            // 
            // cbCountEntries
            // 
            this.cbCountEntries.FormattingEnabled = true;
            this.cbCountEntries.Location = new System.Drawing.Point(92, 28);
            this.cbCountEntries.Name = "cbCountEntries";
            this.cbCountEntries.Size = new System.Drawing.Size(178, 21);
            this.cbCountEntries.TabIndex = 0;
            // 
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdznacitVse.Location = new System.Drawing.Point(824, 96);
            this.buttonOdznacitVse.Name = "buttonOdznacitVse";
            this.buttonOdznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOdznacitVse.TabIndex = 16;
            this.buttonOdznacitVse.Text = "Odznačit vše";
            this.buttonOdznacitVse.UseVisualStyleBackColor = true;
            this.buttonOdznacitVse.Click += new System.EventHandler(this.buttonOdznacitVse_Click);
            // 
            // buttonOznacitVse
            // 
            this.buttonOznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOznacitVse.Location = new System.Drawing.Point(735, 96);
            this.buttonOznacitVse.Name = "buttonOznacitVse";
            this.buttonOznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOznacitVse.TabIndex = 15;
            this.buttonOznacitVse.Text = "Označit vše";
            this.buttonOznacitVse.UseVisualStyleBackColor = true;
            this.buttonOznacitVse.Click += new System.EventHandler(this.buttonOznacitVse_Click);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(380, 333);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 30;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // bwLoadVolnyPohyb
            // 
            this.bwLoadVolnyPohyb.WorkerSupportsCancellation = true;
            this.bwLoadVolnyPohyb.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSkladPohyb_DoWork);
            this.bwLoadVolnyPohyb.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSkladPohyb_RunWorkerCompleted);
            // 
            // bwExport
            // 
            this.bwExport.WorkerSupportsCancellation = true;
            this.bwExport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwExport_DoWork);
            this.bwExport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwExport_RunWorkerCompleted);
            // 
            // dgProdej
            // 
            this.dgProdej.AllowUserToAddRows = false;
            this.dgProdej.AllowUserToDeleteRows = false;
            this.dgProdej.AllowUserToOrderColumns = true;
            this.dgProdej.AllowUserToResizeRows = false;
            this.dgProdej.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgProdej.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgProdej.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProdej.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.countEntriesDataGridViewTextBoxColumn,
            this.vNDITNUMDataGridViewTextBoxColumn,
            this.cZCarKodDataGridViewTextBoxColumn,
            this.oDBIDDataGridViewTextBoxColumn,
            this.sTRIDDataGridViewTextBoxColumn,
            this.dOCIDDataGridViewTextBoxColumn,
            this.dOCID2DataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.pRACIDDataGridViewTextBoxColumn,
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMCODEDataGridViewTextBoxColumn,
            this.lOCNCODEDataGridViewTextBoxColumn,
            this.mJDataGridViewTextBoxColumn,
            this.qTYSHPPDDataGridViewTextBoxColumn,
            this.qTYSHPPDMJDataGridViewTextBoxColumn,
            this.qTYPACKDataGridViewTextBoxColumn,
            this.sERLTNUMDataGridViewTextBoxColumn,
            this.tAXAMPIEDataGridViewTextBoxColumn,
            this.aMOUNPIEDataGridViewTextBoxColumn,
            this.wITHTAXDataGridViewTextBoxColumn,
            this.pRICEXDataGridViewTextBoxColumn,
            this.menaIDDataGridViewTextBoxColumn,
            this.tAXAMPIEMDataGridViewTextBoxColumn,
            this.aMOUNPIEMDataGridViewTextBoxColumn,
            this.menaIDMDataGridViewTextBoxColumn,
            this.rEZ1DataGridViewTextBoxColumn,
            this.rEZ2DataGridViewTextBoxColumn,
            this.rEZ3DataGridViewTextBoxColumn,
            this.rEZ4DataGridViewTextBoxColumn,
            this.uSERIDDataGridViewTextBoxColumn,
            this.dATEDONEDataGridViewTextBoxColumn,
            this.tIMEDONEDataGridViewTextBoxColumn,
            this.dEXROWIDDataGridViewTextBoxColumn,
            this.gUIDDataGridViewTextBoxColumn,
            this.iNPUTMODEDataGridViewTextBoxColumn,
            this.iDTERMINALDataGridViewTextBoxColumn,
            this.lOCNCODEDESTDataGridViewTextBoxColumn,
            this.sKLIDDESTDataGridViewTextBoxColumn,
            this.EXPIRACE,
            this.ITEMDESC,
            this.WEIGHT,
            this.NMBRPAL,
            this.TYPEPAL,
            this.PRINTED,
            this.AttributeToSN});
            this.dgProdej.DataSource = this.bsProdej;
            this.dgProdej.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgProdej.EnableHeadersVisualStyles = false;
            this.dgProdej.FilterAndSortEnabled = true;
            this.dgProdej.Location = new System.Drawing.Point(0, 314);
            this.dgProdej.Name = "dgProdej";
            this.dgProdej.ReadOnly = true;
            this.dgProdej.RowHeadersVisible = false;
            this.dgProdej.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgProdej.Size = new System.Drawing.Size(913, 363);
            this.dgProdej.TabIndex = 0;
            this.dgProdej.TabStop = false;
            this.dgProdej.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dgProdej.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // bsProdej
            // 
            this.bsProdej.DataMember = "CZMST_DI";
            this.bsProdej.DataSource = this.dsProdej;
            // 
            // dsProdej
            // 
            this.dsProdej.DataSetName = "Prodej";
            this.dsProdej.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 287);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(913, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 31;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(913, 24);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 653);
            this.panelButtons.TabIndex = 2;
            // 
            // countEntriesDataGridViewTextBoxColumn
            // 
            this.countEntriesDataGridViewTextBoxColumn.DataPropertyName = "CountEntries";
            this.countEntriesDataGridViewTextBoxColumn.HeaderText = "Číslo dávky";
            this.countEntriesDataGridViewTextBoxColumn.Name = "countEntriesDataGridViewTextBoxColumn";
            this.countEntriesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vNDITNUMDataGridViewTextBoxColumn
            // 
            this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "Čárový kód položky";
            this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
            this.vNDITNUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cZCarKodDataGridViewTextBoxColumn
            // 
            this.cZCarKodDataGridViewTextBoxColumn.DataPropertyName = "CZ_CarKod";
            this.cZCarKodDataGridViewTextBoxColumn.HeaderText = "Vlastní čárový kód";
            this.cZCarKodDataGridViewTextBoxColumn.Name = "cZCarKodDataGridViewTextBoxColumn";
            this.cZCarKodDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // oDBIDDataGridViewTextBoxColumn
            // 
            this.oDBIDDataGridViewTextBoxColumn.DataPropertyName = "ODB_ID";
            this.oDBIDDataGridViewTextBoxColumn.HeaderText = "ID odběratele";
            this.oDBIDDataGridViewTextBoxColumn.Name = "oDBIDDataGridViewTextBoxColumn";
            this.oDBIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sTRIDDataGridViewTextBoxColumn
            // 
            this.sTRIDDataGridViewTextBoxColumn.DataPropertyName = "STR_ID";
            this.sTRIDDataGridViewTextBoxColumn.HeaderText = "ID střediska";
            this.sTRIDDataGridViewTextBoxColumn.Name = "sTRIDDataGridViewTextBoxColumn";
            this.sTRIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dOCIDDataGridViewTextBoxColumn
            // 
            this.dOCIDDataGridViewTextBoxColumn.DataPropertyName = "DOC_ID";
            this.dOCIDDataGridViewTextBoxColumn.HeaderText = "Typ pohybu";
            this.dOCIDDataGridViewTextBoxColumn.Name = "dOCIDDataGridViewTextBoxColumn";
            this.dOCIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dOCID2DataGridViewTextBoxColumn
            // 
            this.dOCID2DataGridViewTextBoxColumn.DataPropertyName = "DOC_ID2";
            this.dOCID2DataGridViewTextBoxColumn.HeaderText = "Typ pohybu 2";
            this.dOCID2DataGridViewTextBoxColumn.Name = "dOCID2DataGridViewTextBoxColumn";
            this.dOCID2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sKLIDDataGridViewTextBoxColumn
            // 
            this.sKLIDDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.HeaderText = "Číslo skladu";
            this.sKLIDDataGridViewTextBoxColumn.Name = "sKLIDDataGridViewTextBoxColumn";
            this.sKLIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pRACIDDataGridViewTextBoxColumn
            // 
            this.pRACIDDataGridViewTextBoxColumn.DataPropertyName = "PRAC_ID";
            this.pRACIDDataGridViewTextBoxColumn.HeaderText = "Číslo pracovníka";
            this.pRACIDDataGridViewTextBoxColumn.Name = "pRACIDDataGridViewTextBoxColumn";
            this.pRACIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "Číslo položky";
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
            // lOCNCODEDataGridViewTextBoxColumn
            // 
            this.lOCNCODEDataGridViewTextBoxColumn.DataPropertyName = "LOCNCODE";
            this.lOCNCODEDataGridViewTextBoxColumn.HeaderText = "Lokace";
            this.lOCNCODEDataGridViewTextBoxColumn.Name = "lOCNCODEDataGridViewTextBoxColumn";
            this.lOCNCODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mJDataGridViewTextBoxColumn
            // 
            this.mJDataGridViewTextBoxColumn.DataPropertyName = "MJ";
            this.mJDataGridViewTextBoxColumn.HeaderText = "Měrná jednotka";
            this.mJDataGridViewTextBoxColumn.Name = "mJDataGridViewTextBoxColumn";
            this.mJDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYSHPPDDataGridViewTextBoxColumn
            // 
            this.qTYSHPPDDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPD";
            this.qTYSHPPDDataGridViewTextBoxColumn.HeaderText = "Množství";
            this.qTYSHPPDDataGridViewTextBoxColumn.Name = "qTYSHPPDDataGridViewTextBoxColumn";
            this.qTYSHPPDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYSHPPDMJDataGridViewTextBoxColumn
            // 
            this.qTYSHPPDMJDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPDMJ";
            this.qTYSHPPDMJDataGridViewTextBoxColumn.HeaderText = "Množství jednotek";
            this.qTYSHPPDMJDataGridViewTextBoxColumn.Name = "qTYSHPPDMJDataGridViewTextBoxColumn";
            this.qTYSHPPDMJDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYPACKDataGridViewTextBoxColumn
            // 
            this.qTYPACKDataGridViewTextBoxColumn.DataPropertyName = "QTYPACK";
            this.qTYPACKDataGridViewTextBoxColumn.HeaderText = "Množství v balení";
            this.qTYPACKDataGridViewTextBoxColumn.Name = "qTYPACKDataGridViewTextBoxColumn";
            this.qTYPACKDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sERLTNUMDataGridViewTextBoxColumn
            // 
            this.sERLTNUMDataGridViewTextBoxColumn.DataPropertyName = "SERLTNUM";
            this.sERLTNUMDataGridViewTextBoxColumn.HeaderText = "SN/výr.číslo";
            this.sERLTNUMDataGridViewTextBoxColumn.Name = "sERLTNUMDataGridViewTextBoxColumn";
            this.sERLTNUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tAXAMPIEDataGridViewTextBoxColumn
            // 
            this.tAXAMPIEDataGridViewTextBoxColumn.DataPropertyName = "TAXAMPIE";
            this.tAXAMPIEDataGridViewTextBoxColumn.HeaderText = "Daň za položku";
            this.tAXAMPIEDataGridViewTextBoxColumn.Name = "tAXAMPIEDataGridViewTextBoxColumn";
            this.tAXAMPIEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // aMOUNPIEDataGridViewTextBoxColumn
            // 
            this.aMOUNPIEDataGridViewTextBoxColumn.DataPropertyName = "AMOUNPIE";
            this.aMOUNPIEDataGridViewTextBoxColumn.HeaderText = "Celkem za položku";
            this.aMOUNPIEDataGridViewTextBoxColumn.Name = "aMOUNPIEDataGridViewTextBoxColumn";
            this.aMOUNPIEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // wITHTAXDataGridViewTextBoxColumn
            // 
            this.wITHTAXDataGridViewTextBoxColumn.DataPropertyName = "WITHTAX";
            this.wITHTAXDataGridViewTextBoxColumn.HeaderText = "Cena celkem";
            this.wITHTAXDataGridViewTextBoxColumn.Name = "wITHTAXDataGridViewTextBoxColumn";
            this.wITHTAXDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pRICEXDataGridViewTextBoxColumn
            // 
            this.pRICEXDataGridViewTextBoxColumn.DataPropertyName = "PRICEX";
            this.pRICEXDataGridViewTextBoxColumn.HeaderText = "Cenová hladina";
            this.pRICEXDataGridViewTextBoxColumn.Name = "pRICEXDataGridViewTextBoxColumn";
            this.pRICEXDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // menaIDDataGridViewTextBoxColumn
            // 
            this.menaIDDataGridViewTextBoxColumn.DataPropertyName = "mena_ID";
            this.menaIDDataGridViewTextBoxColumn.HeaderText = "ID základní měny";
            this.menaIDDataGridViewTextBoxColumn.Name = "menaIDDataGridViewTextBoxColumn";
            this.menaIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tAXAMPIEMDataGridViewTextBoxColumn
            // 
            this.tAXAMPIEMDataGridViewTextBoxColumn.DataPropertyName = "TAXAMPIEM";
            this.tAXAMPIEMDataGridViewTextBoxColumn.HeaderText = "Daň v měně";
            this.tAXAMPIEMDataGridViewTextBoxColumn.Name = "tAXAMPIEMDataGridViewTextBoxColumn";
            this.tAXAMPIEMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // aMOUNPIEMDataGridViewTextBoxColumn
            // 
            this.aMOUNPIEMDataGridViewTextBoxColumn.DataPropertyName = "AMOUNPIEM";
            this.aMOUNPIEMDataGridViewTextBoxColumn.HeaderText = "Celkem v měně";
            this.aMOUNPIEMDataGridViewTextBoxColumn.Name = "aMOUNPIEMDataGridViewTextBoxColumn";
            this.aMOUNPIEMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // menaIDMDataGridViewTextBoxColumn
            // 
            this.menaIDMDataGridViewTextBoxColumn.DataPropertyName = "mena_IDM";
            this.menaIDMDataGridViewTextBoxColumn.HeaderText = "ID cizí měny";
            this.menaIDMDataGridViewTextBoxColumn.Name = "menaIDMDataGridViewTextBoxColumn";
            this.menaIDMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rEZ1DataGridViewTextBoxColumn
            // 
            this.rEZ1DataGridViewTextBoxColumn.DataPropertyName = "REZ_1";
            this.rEZ1DataGridViewTextBoxColumn.HeaderText = "Rezerva 1";
            this.rEZ1DataGridViewTextBoxColumn.Name = "rEZ1DataGridViewTextBoxColumn";
            this.rEZ1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rEZ2DataGridViewTextBoxColumn
            // 
            this.rEZ2DataGridViewTextBoxColumn.DataPropertyName = "REZ_2";
            this.rEZ2DataGridViewTextBoxColumn.HeaderText = "Rezerva 2";
            this.rEZ2DataGridViewTextBoxColumn.Name = "rEZ2DataGridViewTextBoxColumn";
            this.rEZ2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rEZ3DataGridViewTextBoxColumn
            // 
            this.rEZ3DataGridViewTextBoxColumn.DataPropertyName = "REZ_3";
            this.rEZ3DataGridViewTextBoxColumn.HeaderText = "Rezerva 3";
            this.rEZ3DataGridViewTextBoxColumn.Name = "rEZ3DataGridViewTextBoxColumn";
            this.rEZ3DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rEZ4DataGridViewTextBoxColumn
            // 
            this.rEZ4DataGridViewTextBoxColumn.DataPropertyName = "REZ_4";
            this.rEZ4DataGridViewTextBoxColumn.HeaderText = "Rezerva 4";
            this.rEZ4DataGridViewTextBoxColumn.Name = "rEZ4DataGridViewTextBoxColumn";
            this.rEZ4DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // uSERIDDataGridViewTextBoxColumn
            // 
            this.uSERIDDataGridViewTextBoxColumn.DataPropertyName = "USER_ID";
            this.uSERIDDataGridViewTextBoxColumn.HeaderText = "ID uživatele";
            this.uSERIDDataGridViewTextBoxColumn.Name = "uSERIDDataGridViewTextBoxColumn";
            this.uSERIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dATEDONEDataGridViewTextBoxColumn
            // 
            this.dATEDONEDataGridViewTextBoxColumn.DataPropertyName = "DATEDONE";
            this.dATEDONEDataGridViewTextBoxColumn.HeaderText = "Datum";
            this.dATEDONEDataGridViewTextBoxColumn.Name = "dATEDONEDataGridViewTextBoxColumn";
            this.dATEDONEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tIMEDONEDataGridViewTextBoxColumn
            // 
            this.tIMEDONEDataGridViewTextBoxColumn.DataPropertyName = "TIMEDONE";
            this.tIMEDONEDataGridViewTextBoxColumn.HeaderText = "Čas";
            this.tIMEDONEDataGridViewTextBoxColumn.Name = "tIMEDONEDataGridViewTextBoxColumn";
            this.tIMEDONEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dEXROWIDDataGridViewTextBoxColumn
            // 
            this.dEXROWIDDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn.HeaderText = "Pořadí záznamu v databázi";
            this.dEXROWIDDataGridViewTextBoxColumn.Name = "dEXROWIDDataGridViewTextBoxColumn";
            this.dEXROWIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gUIDDataGridViewTextBoxColumn
            // 
            this.gUIDDataGridViewTextBoxColumn.DataPropertyName = "GUID";
            this.gUIDDataGridViewTextBoxColumn.HeaderText = "Jedinečný klíč";
            this.gUIDDataGridViewTextBoxColumn.Name = "gUIDDataGridViewTextBoxColumn";
            this.gUIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iNPUTMODEDataGridViewTextBoxColumn
            // 
            this.iNPUTMODEDataGridViewTextBoxColumn.DataPropertyName = "INPUT_MODE";
            this.iNPUTMODEDataGridViewTextBoxColumn.HeaderText = "Způsob zápisu";
            this.iNPUTMODEDataGridViewTextBoxColumn.Name = "iNPUTMODEDataGridViewTextBoxColumn";
            this.iNPUTMODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDTERMINALDataGridViewTextBoxColumn
            // 
            this.iDTERMINALDataGridViewTextBoxColumn.DataPropertyName = "ID_TERMINAL";
            this.iDTERMINALDataGridViewTextBoxColumn.HeaderText = "Číslo terminálu";
            this.iDTERMINALDataGridViewTextBoxColumn.Name = "iDTERMINALDataGridViewTextBoxColumn";
            this.iDTERMINALDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lOCNCODEDESTDataGridViewTextBoxColumn
            // 
            this.lOCNCODEDESTDataGridViewTextBoxColumn.DataPropertyName = "LOCNCODEDEST";
            this.lOCNCODEDESTDataGridViewTextBoxColumn.HeaderText = "Cílová lokace";
            this.lOCNCODEDESTDataGridViewTextBoxColumn.Name = "lOCNCODEDESTDataGridViewTextBoxColumn";
            this.lOCNCODEDESTDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sKLIDDESTDataGridViewTextBoxColumn
            // 
            this.sKLIDDESTDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID_DEST";
            this.sKLIDDESTDataGridViewTextBoxColumn.HeaderText = "Cílový sklad";
            this.sKLIDDESTDataGridViewTextBoxColumn.Name = "sKLIDDESTDataGridViewTextBoxColumn";
            this.sKLIDDESTDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // EXPIRACE
            // 
            this.EXPIRACE.DataPropertyName = "EXPIRACE";
            this.EXPIRACE.HeaderText = "Expirace";
            this.EXPIRACE.Name = "EXPIRACE";
            this.EXPIRACE.ReadOnly = true;
            // 
            // ITEMDESC
            // 
            this.ITEMDESC.DataPropertyName = "ITEMDESC";
            this.ITEMDESC.HeaderText = "Název položky";
            this.ITEMDESC.Name = "ITEMDESC";
            this.ITEMDESC.ReadOnly = true;
            // 
            // WEIGHT
            // 
            this.WEIGHT.DataPropertyName = "WEIGHT";
            this.WEIGHT.HeaderText = "Váha";
            this.WEIGHT.Name = "WEIGHT";
            this.WEIGHT.ReadOnly = true;
            // 
            // NMBRPAL
            // 
            this.NMBRPAL.DataPropertyName = "NMBRPAL";
            this.NMBRPAL.HeaderText = "Číslo palety SSCC";
            this.NMBRPAL.Name = "NMBRPAL";
            this.NMBRPAL.ReadOnly = true;
            // 
            // TYPEPAL
            // 
            this.TYPEPAL.DataPropertyName = "TYPEPAL";
            this.TYPEPAL.HeaderText = "Typ palety";
            this.TYPEPAL.Name = "TYPEPAL";
            this.TYPEPAL.ReadOnly = true;
            // 
            // PRINTED
            // 
            this.PRINTED.DataPropertyName = "PRINTED";
            this.PRINTED.HeaderText = "Příznak vytisknuto";
            this.PRINTED.Name = "PRINTED";
            this.PRINTED.ReadOnly = true;
            // 
            // AttributeToSN
            // 
            this.AttributeToSN.DataPropertyName = "AttributeToSN";
            this.AttributeToSN.HeaderText = "Atribut k SN";
            this.AttributeToSN.Name = "AttributeToSN";
            this.AttributeToSN.ReadOnly = true;
            // 
            // FormVolnyPohybNasnimaneList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 677);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dgProdej);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormVolnyPohybNasnimaneList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Volný pohyb - Nasnimané";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVolnyPohybNasnimaneList_FormClosing);
            this.Load += new System.EventHandler(this.FormVolnyPohybNasnimaneList_Load);
            this.Shown += new System.EventHandler(this.FormVolnyPohybNasnimaneList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormVolnyPohybNasnimaneList_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProdej)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProdej)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProdej)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCountEntries;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Button buttonOznacitVse;
        private Zuby.ADGV.AdvancedDataGridView dgProdej;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.Button buttonVyhledat;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private System.ComponentModel.BackgroundWorker bwLoadVolnyPohyb;
        private System.Windows.Forms.ToolStrip tsFiltry;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbFiltry;
        private System.Windows.Forms.ToolStripButton tsbNastavit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbPridat;
        private System.Windows.Forms.ToolStripButton tsbOdebrat;
        private System.Windows.Forms.ToolStripButton tsbZmena;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private Fask.Interfaces.DataSets.Prodej dsProdej;
        private System.Windows.Forms.BindingSource bsProdej;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbVycistit;
        private System.ComponentModel.BackgroundWorker bwExport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbITEMNMBR;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private System.Windows.Forms.ToolStripMenuItem tsmiAkce;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportovatDavku;
        private System.Windows.Forms.ToolStripMenuItem tsmiPolozka;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravit;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tiskEtiketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_KlavesovyVystup;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtp_porizeno_OD;
        private System.Windows.Forms.ComboBox cb_Porizeno_TimeVariant;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtp_porizeno_DO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem tiskToolStripMenuItem;
        private System.Windows.Forms.TextBox tB_ID_terminalu;
        private System.Windows.Forms.TextBox tB_ID_uzivatele;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tB_kod_polozky;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tB_Typ_pohybu2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tB_Typ_pohybu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tB_Nazev_polozky;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDITNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cZCarKodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oDBIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sTRIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dOCIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dOCID2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pRACIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOCNCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mJDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSHPPDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSHPPDMJDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYPACKDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sERLTNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tAXAMPIEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aMOUNPIEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wITHTAXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pRICEXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn menaIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tAXAMPIEMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aMOUNPIEMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn menaIDMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rEZ1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rEZ2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rEZ3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rEZ4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATEDONEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMEDONEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iNPUTMODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDTERMINALDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOCNCODEDESTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sKLIDDESTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXPIRACE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn NMBRPAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn TYPEPAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRINTED;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttributeToSN;
    }
}