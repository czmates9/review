namespace Vyroba_Konzola.Ciselniky
{
    partial class FormUzivateleList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUzivateleList));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOGINDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fIRSTNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sECONDNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pASSWDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aDMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eANDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hASHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsUzivatel = new System.Windows.Forms.BindingSource(this.components);
            this.dsUzivatel = new Fask.Console.Interfaces.DataSets.Uzivatele();
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
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
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbUserLogin = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbUserID = new System.Windows.Forms.ComboBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenuVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.vybratToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemTiskEtiketaVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNovy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemUpravit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOdstranit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemTiskEtiketa = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.konecToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.výstupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDoCSVVseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDoCSVOznaceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.exportDoExcelVseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDoExcelOznaceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bwLoadUzivatele = new System.ComponentModel.BackgroundWorker();
            this.panelButtonsZobrazeniList = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonOdstranit = new System.Windows.Forms.Button();
            this.buttonUpravit = new System.Windows.Forms.Button();
            this.buttonNovy = new System.Windows.Forms.Button();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.exportDoXMLVseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDoXMLOznaceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUzivatel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsUzivatel)).BeginInit();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.panelButtonsZobrazeniList.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.lOGINDataGridViewTextBoxColumn,
            this.fIRSTNAMEDataGridViewTextBoxColumn,
            this.sECONDNAMEDataGridViewTextBoxColumn,
            this.pASSWDDataGridViewTextBoxColumn,
            this.aDMDataGridViewTextBoxColumn,
            this.eANDataGridViewTextBoxColumn,
            this.hASHDataGridViewTextBoxColumn,
            this.cODEDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.bsUzivatel;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 118);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(574, 350);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lOGINDataGridViewTextBoxColumn
            // 
            this.lOGINDataGridViewTextBoxColumn.DataPropertyName = "LOGIN";
            this.lOGINDataGridViewTextBoxColumn.HeaderText = "LOGIN";
            this.lOGINDataGridViewTextBoxColumn.Name = "lOGINDataGridViewTextBoxColumn";
            this.lOGINDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fIRSTNAMEDataGridViewTextBoxColumn
            // 
            this.fIRSTNAMEDataGridViewTextBoxColumn.DataPropertyName = "FIRSTNAME";
            this.fIRSTNAMEDataGridViewTextBoxColumn.HeaderText = "Jméno";
            this.fIRSTNAMEDataGridViewTextBoxColumn.Name = "fIRSTNAMEDataGridViewTextBoxColumn";
            this.fIRSTNAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sECONDNAMEDataGridViewTextBoxColumn
            // 
            this.sECONDNAMEDataGridViewTextBoxColumn.DataPropertyName = "SECONDNAME";
            this.sECONDNAMEDataGridViewTextBoxColumn.HeaderText = "Příjmení";
            this.sECONDNAMEDataGridViewTextBoxColumn.Name = "sECONDNAMEDataGridViewTextBoxColumn";
            this.sECONDNAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pASSWDDataGridViewTextBoxColumn
            // 
            this.pASSWDDataGridViewTextBoxColumn.DataPropertyName = "PASSWD";
            this.pASSWDDataGridViewTextBoxColumn.HeaderText = "Heslo";
            this.pASSWDDataGridViewTextBoxColumn.Name = "pASSWDDataGridViewTextBoxColumn";
            this.pASSWDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // aDMDataGridViewTextBoxColumn
            // 
            this.aDMDataGridViewTextBoxColumn.DataPropertyName = "ADM";
            this.aDMDataGridViewTextBoxColumn.HeaderText = "Administrátor";
            this.aDMDataGridViewTextBoxColumn.Name = "aDMDataGridViewTextBoxColumn";
            this.aDMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // eANDataGridViewTextBoxColumn
            // 
            this.eANDataGridViewTextBoxColumn.DataPropertyName = "EAN";
            this.eANDataGridViewTextBoxColumn.HeaderText = "Čár. kód";
            this.eANDataGridViewTextBoxColumn.Name = "eANDataGridViewTextBoxColumn";
            this.eANDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hASHDataGridViewTextBoxColumn
            // 
            this.hASHDataGridViewTextBoxColumn.DataPropertyName = "HASH";
            this.hASHDataGridViewTextBoxColumn.HeaderText = "HASH";
            this.hASHDataGridViewTextBoxColumn.Name = "hASHDataGridViewTextBoxColumn";
            this.hASHDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cODEDataGridViewTextBoxColumn
            // 
            this.cODEDataGridViewTextBoxColumn.DataPropertyName = "CODE";
            this.cODEDataGridViewTextBoxColumn.HeaderText = "CODE";
            this.cODEDataGridViewTextBoxColumn.Name = "cODEDataGridViewTextBoxColumn";
            this.cODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsUzivatel
            // 
            this.bsUzivatel.DataMember = "CZMSTPWD";
            this.bsUzivatel.DataSource = this.dsUzivatel;
            // 
            // dsUzivatel
            // 
            this.dsUzivatel.DataSetName = "Uzivatele";
            this.dsUzivatel.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.buttonVybratUzivatele.Text = "Vybrat uživatele";
            this.buttonVybratUzivatele.UseVisualStyleBackColor = true;
            this.buttonVybratUzivatele.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tsFiltry);
            this.panelMain.Controls.Add(this.progressIndicator1);
            this.panelMain.Controls.Add(this.buttonVyhledat);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.cbUserLogin);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.cbUserID);
            this.panelMain.Controls.Add(this.dataGridView1);
            this.panelMain.Controls.Add(this.menuStrip2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(574, 468);
            this.panelMain.TabIndex = 1;
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
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(204, 246);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 38;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Login:";
            // 
            // cbUserLogin
            // 
            this.cbUserLogin.FormattingEnabled = true;
            this.cbUserLogin.Location = new System.Drawing.Point(93, 79);
            this.cbUserLogin.Name = "cbUserLogin";
            this.cbUserLogin.Size = new System.Drawing.Size(135, 21);
            this.cbUserLogin.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "ID uživatele:";
            // 
            // cbUserID
            // 
            this.cbUserID.FormattingEnabled = true;
            this.cbUserID.Location = new System.Drawing.Point(93, 52);
            this.cbUserID.Name = "cbUserID";
            this.cbUserID.Size = new System.Drawing.Size(135, 21);
            this.cbUserID.TabIndex = 1;
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuVyber,
            this.tsmiMenuList,
            this.výstupToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(574, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // tsmiMenuVyber
            // 
            this.tsmiMenuVyber.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vybratToolStripMenuItem,
            this.toolStripSeparator10,
            this.toolStripMenuItemTiskEtiketaVyber,
            this.toolStripSeparator1,
            this.konecToolStripMenuItem});
            this.tsmiMenuVyber.Name = "tsmiMenuVyber";
            this.tsmiMenuVyber.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenuVyber.Text = "Menu";
            // 
            // vybratToolStripMenuItem
            // 
            this.vybratToolStripMenuItem.Name = "vybratToolStripMenuItem";
            this.vybratToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.vybratToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.vybratToolStripMenuItem.Text = "Vybrat";
            this.vybratToolStripMenuItem.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(170, 6);
            // 
            // toolStripMenuItemTiskEtiketaVyber
            // 
            this.toolStripMenuItemTiskEtiketaVyber.Name = "toolStripMenuItemTiskEtiketaVyber";
            this.toolStripMenuItemTiskEtiketaVyber.ShortcutKeyDisplayString = "";
            this.toolStripMenuItemTiskEtiketaVyber.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.toolStripMenuItemTiskEtiketaVyber.Size = new System.Drawing.Size(173, 22);
            this.toolStripMenuItemTiskEtiketaVyber.Text = "Tisk etiketa";
            this.toolStripMenuItemTiskEtiketaVyber.Click += new System.EventHandler(this.toolStripMenuItemTiskEtiketa_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.konecToolStripMenuItem.Text = "Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // tsmiMenuList
            // 
            this.tsmiMenuList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNovy,
            this.toolStripMenuItemUpravit,
            this.toolStripMenuItemOdstranit,
            this.toolStripSeparator9,
            this.toolStripMenuItemTiskEtiketa,
            this.toolStripSeparator7,
            this.konecToolStripMenuItem1});
            this.tsmiMenuList.Name = "tsmiMenuList";
            this.tsmiMenuList.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tsmiMenuList.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenuList.Text = "Menu";
            // 
            // toolStripMenuItemNovy
            // 
            this.toolStripMenuItemNovy.Name = "toolStripMenuItemNovy";
            this.toolStripMenuItemNovy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.toolStripMenuItemNovy.Size = new System.Drawing.Size(173, 22);
            this.toolStripMenuItemNovy.Text = "Nový";
            this.toolStripMenuItemNovy.Click += new System.EventHandler(this.buttonNovy_Click_1);
            // 
            // toolStripMenuItemUpravit
            // 
            this.toolStripMenuItemUpravit.Name = "toolStripMenuItemUpravit";
            this.toolStripMenuItemUpravit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.toolStripMenuItemUpravit.Size = new System.Drawing.Size(173, 22);
            this.toolStripMenuItemUpravit.Text = "Upravit";
            this.toolStripMenuItemUpravit.Click += new System.EventHandler(this.buttonUpravit_Click);
            // 
            // toolStripMenuItemOdstranit
            // 
            this.toolStripMenuItemOdstranit.Name = "toolStripMenuItemOdstranit";
            this.toolStripMenuItemOdstranit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItemOdstranit.Size = new System.Drawing.Size(173, 22);
            this.toolStripMenuItemOdstranit.Text = "Odstranit";
            this.toolStripMenuItemOdstranit.Click += new System.EventHandler(this.buttonOdstranit_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(170, 6);
            // 
            // toolStripMenuItemTiskEtiketa
            // 
            this.toolStripMenuItemTiskEtiketa.Name = "toolStripMenuItemTiskEtiketa";
            this.toolStripMenuItemTiskEtiketa.ShortcutKeyDisplayString = "";
            this.toolStripMenuItemTiskEtiketa.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.toolStripMenuItemTiskEtiketa.Size = new System.Drawing.Size(173, 22);
            this.toolStripMenuItemTiskEtiketa.Text = "Tisk etiketa";
            this.toolStripMenuItemTiskEtiketa.Click += new System.EventHandler(this.toolStripMenuItemTiskEtiketa_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(170, 6);
            // 
            // konecToolStripMenuItem1
            // 
            this.konecToolStripMenuItem1.Name = "konecToolStripMenuItem1";
            this.konecToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.konecToolStripMenuItem1.Size = new System.Drawing.Size(173, 22);
            this.konecToolStripMenuItem1.Text = "Konec";
            this.konecToolStripMenuItem1.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // výstupToolStripMenuItem
            // 
            this.výstupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportDoCSVVseToolStripMenuItem,
            this.exportDoCSVOznaceneToolStripMenuItem,
            this.toolStripSeparator6,
            this.exportDoExcelVseToolStripMenuItem,
            this.exportDoExcelOznaceneToolStripMenuItem,
            this.toolStripSeparator8,
            this.exportDoXMLVseToolStripMenuItem,
            this.exportDoXMLOznaceneToolStripMenuItem});
            this.výstupToolStripMenuItem.Name = "výstupToolStripMenuItem";
            this.výstupToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.výstupToolStripMenuItem.Text = "Výstup";
            // 
            // exportDoCSVVseToolStripMenuItem
            // 
            this.exportDoCSVVseToolStripMenuItem.Name = "exportDoCSVVseToolStripMenuItem";
            this.exportDoCSVVseToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportDoCSVVseToolStripMenuItem.Text = "Export do CSV vše";
            this.exportDoCSVVseToolStripMenuItem.Click += new System.EventHandler(this.exportDoCSVVseToolStripMenuItem_Click);
            // 
            // exportDoCSVOznaceneToolStripMenuItem
            // 
            this.exportDoCSVOznaceneToolStripMenuItem.Name = "exportDoCSVOznaceneToolStripMenuItem";
            this.exportDoCSVOznaceneToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportDoCSVOznaceneToolStripMenuItem.Text = "Export do CSV označené";
            this.exportDoCSVOznaceneToolStripMenuItem.Click += new System.EventHandler(this.exportDoCSVOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(203, 6);
            // 
            // exportDoExcelVseToolStripMenuItem
            // 
            this.exportDoExcelVseToolStripMenuItem.Name = "exportDoExcelVseToolStripMenuItem";
            this.exportDoExcelVseToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportDoExcelVseToolStripMenuItem.Text = "Export do Excel vše";
            this.exportDoExcelVseToolStripMenuItem.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_Click);
            // 
            // exportDoExcelOznaceneToolStripMenuItem
            // 
            this.exportDoExcelOznaceneToolStripMenuItem.Name = "exportDoExcelOznaceneToolStripMenuItem";
            this.exportDoExcelOznaceneToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportDoExcelOznaceneToolStripMenuItem.Text = "Export do Excel označené";
            this.exportDoExcelOznaceneToolStripMenuItem.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
            // 
            // bwLoadUzivatele
            // 
            this.bwLoadUzivatele.WorkerSupportsCancellation = true;
            this.bwLoadUzivatele.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadZbozi_DoWork);
            this.bwLoadUzivatele.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadZbozi_RunWorkerCompleted);
            // 
            // panelButtonsZobrazeniList
            // 
            this.panelButtonsZobrazeniList.Controls.Add(this.button2);
            this.panelButtonsZobrazeniList.Controls.Add(this.buttonOdstranit);
            this.panelButtonsZobrazeniList.Controls.Add(this.buttonUpravit);
            this.panelButtonsZobrazeniList.Controls.Add(this.buttonNovy);
            this.panelButtonsZobrazeniList.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniList.Location = new System.Drawing.Point(658, 0);
            this.panelButtonsZobrazeniList.Name = "panelButtonsZobrazeniList";
            this.panelButtonsZobrazeniList.Size = new System.Drawing.Size(84, 468);
            this.panelButtonsZobrazeniList.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(6, 393);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 63);
            this.button2.TabIndex = 4;
            this.button2.Text = "Konec";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // buttonOdstranit
            // 
            this.buttonOdstranit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdstranit.Location = new System.Drawing.Point(6, 162);
            this.buttonOdstranit.Name = "buttonOdstranit";
            this.buttonOdstranit.Size = new System.Drawing.Size(73, 63);
            this.buttonOdstranit.TabIndex = 2;
            this.buttonOdstranit.Text = "Odstranit";
            this.buttonOdstranit.UseVisualStyleBackColor = true;
            this.buttonOdstranit.Click += new System.EventHandler(this.buttonOdstranit_Click);
            // 
            // buttonUpravit
            // 
            this.buttonUpravit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpravit.Location = new System.Drawing.Point(6, 93);
            this.buttonUpravit.Name = "buttonUpravit";
            this.buttonUpravit.Size = new System.Drawing.Size(73, 63);
            this.buttonUpravit.TabIndex = 1;
            this.buttonUpravit.Text = "Upravit";
            this.buttonUpravit.UseVisualStyleBackColor = true;
            this.buttonUpravit.Click += new System.EventHandler(this.buttonUpravit_Click);
            // 
            // buttonNovy
            // 
            this.buttonNovy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNovy.Location = new System.Drawing.Point(6, 24);
            this.buttonNovy.Name = "buttonNovy";
            this.buttonNovy.Size = new System.Drawing.Size(73, 63);
            this.buttonNovy.TabIndex = 0;
            this.buttonNovy.Text = "Nový";
            this.buttonNovy.UseVisualStyleBackColor = true;
            this.buttonNovy.Click += new System.EventHandler(this.buttonNovy_Click_1);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(203, 6);
            // 
            // exportDoXMLVseToolStripMenuItem
            // 
            this.exportDoXMLVseToolStripMenuItem.Name = "exportDoXMLVseToolStripMenuItem";
            this.exportDoXMLVseToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportDoXMLVseToolStripMenuItem.Text = "Export do XML Vše";
            this.exportDoXMLVseToolStripMenuItem.Click += new System.EventHandler(this.exportDoXMLVseToolStripMenuItem_Click);
            // 
            // exportDoXMLOznaceneToolStripMenuItem
            // 
            this.exportDoXMLOznaceneToolStripMenuItem.Name = "exportDoXMLOznaceneToolStripMenuItem";
            this.exportDoXMLOznaceneToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportDoXMLOznaceneToolStripMenuItem.Text = "Export do XML označené";
            this.exportDoXMLOznaceneToolStripMenuItem.Click += new System.EventHandler(this.exportDoXMLOznaceneToolStripMenuItem_Click);
            // 
            // FormUzivateleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 468);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.KeyPreview = true;
            this.Name = "FormUzivateleList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výběr uživatele";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUzivateleList_FormClosing);
            this.Load += new System.EventHandler(this.FormUzivateleList_Load);
            this.Shown += new System.EventHandler(this.FormZboziSelect_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUzivateleList_KeyDown);
            this.Resize += new System.EventHandler(this.FormUzivateleList_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUzivatel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsUzivatel)).EndInit();
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panelButtonsZobrazeniList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panelButtonsZobrazeniVyber;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.BindingSource bsUzivatel;
        private System.Windows.Forms.Button buttonVybratUzivatele;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuVyber;
        private System.Windows.Forms.ToolStripMenuItem vybratToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbUserLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbUserID;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.ComponentModel.BackgroundWorker bwLoadUzivatele;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private System.Windows.Forms.Panel panelButtonsZobrazeniList;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonOdstranit;
        private System.Windows.Forms.Button buttonUpravit;
        private System.Windows.Forms.Button buttonNovy;
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
        private Fask.Console.Interfaces.DataSets.Uzivatele dsUzivatel;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOGINDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fIRSTNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sECONDNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pASSWDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aDMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eANDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hASHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuList;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTiskEtiketa;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNovy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUpravit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOdstranit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTiskEtiketaVyber;
        private System.Windows.Forms.ToolStripMenuItem výstupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDoCSVVseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDoCSVOznaceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem exportDoExcelVseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDoExcelOznaceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem exportDoXMLVseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDoXMLOznaceneToolStripMenuItem;
    }
}