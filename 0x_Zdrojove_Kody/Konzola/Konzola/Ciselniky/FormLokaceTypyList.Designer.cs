namespace Konzola.Ciselniky
{
    partial class FormLokaceTypyList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLokaceTypyList));
            this.dgSkladLokace = new Zuby.ADGV.AdvancedDataGridView();
            this.tYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSRECEIVEDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSDEFAULTDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSNORMALDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsSkladLokace = new System.Windows.Forms.BindingSource(this.components);
            this.dsSkladLokace = new Fask.Interfaces.DataSets.SkladLokace();
            this.panelButtonsZobrazeniList = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox_Bezna = new System.Windows.Forms.CheckBox();
            this.checkBox_vychoz = new System.Windows.Forms.CheckBox();
            this.checkBox_Prijm = new System.Windows.Forms.CheckBox();
            this.tb_Typ = new System.Windows.Forms.TextBox();
            this.tsFiltry = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFiltry = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNastavit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZmena = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPridat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdebrat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVycistit = new System.Windows.Forms.ToolStripButton();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonecList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVybrat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiKonecVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVystup = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskEtiketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExcelOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPolozka = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNovy = new System.Windows.Forms.ToolStripMenuItem();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonVybratTyp = new System.Windows.Forms.Button();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgSkladLokace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSkladLokace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSkladLokace)).BeginInit();
            this.panel1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgSkladLokace
            // 
            this.dgSkladLokace.AllowUserToAddRows = false;
            this.dgSkladLokace.AllowUserToDeleteRows = false;
            this.dgSkladLokace.AllowUserToOrderColumns = true;
            this.dgSkladLokace.AllowUserToResizeRows = false;
            this.dgSkladLokace.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSkladLokace.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSkladLokace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSkladLokace.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tYPEDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.iSRECEIVEDataGridViewCheckBoxColumn,
            this.iSDEFAULTDataGridViewCheckBoxColumn,
            this.iSNORMALDataGridViewCheckBoxColumn});
            this.dgSkladLokace.DataSource = this.bsSkladLokace;
            this.dgSkladLokace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSkladLokace.EnableHeadersVisualStyles = false;
            this.dgSkladLokace.FilterAndSortEnabled = true;
            this.dgSkladLokace.Location = new System.Drawing.Point(0, 176);
            this.dgSkladLokace.MultiSelect = false;
            this.dgSkladLokace.Name = "dgSkladLokace";
            this.dgSkladLokace.ReadOnly = true;
            this.dgSkladLokace.RowHeadersVisible = false;
            this.dgSkladLokace.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSkladLokace.Size = new System.Drawing.Size(530, 337);
            this.dgSkladLokace.TabIndex = 1;
            this.dgSkladLokace.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgSkladLokace.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dgSkladLokace.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // tYPEDataGridViewTextBoxColumn
            // 
            this.tYPEDataGridViewTextBoxColumn.DataPropertyName = "TYPE";
            this.tYPEDataGridViewTextBoxColumn.HeaderText = "Typ lokace";
            this.tYPEDataGridViewTextBoxColumn.Name = "tYPEDataGridViewTextBoxColumn";
            this.tYPEDataGridViewTextBoxColumn.ReadOnly = true;
            this.tYPEDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Označení";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            this.descriptionDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // iSRECEIVEDataGridViewCheckBoxColumn
            // 
            this.iSRECEIVEDataGridViewCheckBoxColumn.DataPropertyName = "IS_RECEIVE";
            this.iSRECEIVEDataGridViewCheckBoxColumn.HeaderText = "Příjmová lokace";
            this.iSRECEIVEDataGridViewCheckBoxColumn.Name = "iSRECEIVEDataGridViewCheckBoxColumn";
            this.iSRECEIVEDataGridViewCheckBoxColumn.ReadOnly = true;
            this.iSRECEIVEDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.iSRECEIVEDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // iSDEFAULTDataGridViewCheckBoxColumn
            // 
            this.iSDEFAULTDataGridViewCheckBoxColumn.DataPropertyName = "IS_DEFAULT";
            this.iSDEFAULTDataGridViewCheckBoxColumn.HeaderText = "Výchozí lokace";
            this.iSDEFAULTDataGridViewCheckBoxColumn.Name = "iSDEFAULTDataGridViewCheckBoxColumn";
            this.iSDEFAULTDataGridViewCheckBoxColumn.ReadOnly = true;
            this.iSDEFAULTDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.iSDEFAULTDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // iSNORMALDataGridViewCheckBoxColumn
            // 
            this.iSNORMALDataGridViewCheckBoxColumn.DataPropertyName = "IS_NORMAL";
            this.iSNORMALDataGridViewCheckBoxColumn.HeaderText = "Běžná lokace";
            this.iSNORMALDataGridViewCheckBoxColumn.Name = "iSNORMALDataGridViewCheckBoxColumn";
            this.iSNORMALDataGridViewCheckBoxColumn.ReadOnly = true;
            this.iSNORMALDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.iSNORMALDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bsSkladLokace
            // 
            this.bsSkladLokace.DataMember = "CZMST_SkladLokace_LokaceTypy";
            this.bsSkladLokace.DataSource = this.dsSkladLokace;
            // 
            // dsSkladLokace
            // 
            this.dsSkladLokace.DataSetName = "SkladLokace";
            this.dsSkladLokace.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panelButtonsZobrazeniList
            // 
            this.panelButtonsZobrazeniList.AutoScroll = true;
            this.panelButtonsZobrazeniList.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniList.Location = new System.Drawing.Point(530, 0);
            this.panelButtonsZobrazeniList.Name = "panelButtonsZobrazeniList";
            this.panelButtonsZobrazeniList.Size = new System.Drawing.Size(84, 513);
            this.panelButtonsZobrazeniList.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox_Bezna);
            this.panel1.Controls.Add(this.checkBox_vychoz);
            this.panel1.Controls.Add(this.checkBox_Prijm);
            this.panel1.Controls.Add(this.tb_Typ);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.menuStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(530, 149);
            this.panel1.TabIndex = 1;
            // 
            // checkBox_Bezna
            // 
            this.checkBox_Bezna.AutoSize = true;
            this.checkBox_Bezna.Checked = true;
            this.checkBox_Bezna.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Bezna.Location = new System.Drawing.Point(196, 122);
            this.checkBox_Bezna.Name = "checkBox_Bezna";
            this.checkBox_Bezna.Size = new System.Drawing.Size(91, 17);
            this.checkBox_Bezna.TabIndex = 72;
            this.checkBox_Bezna.Text = "Běžná lokace";
            this.checkBox_Bezna.UseVisualStyleBackColor = true;
            // 
            // checkBox_vychoz
            // 
            this.checkBox_vychoz.AutoSize = true;
            this.checkBox_vychoz.Checked = true;
            this.checkBox_vychoz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_vychoz.Location = new System.Drawing.Point(196, 99);
            this.checkBox_vychoz.Name = "checkBox_vychoz";
            this.checkBox_vychoz.Size = new System.Drawing.Size(100, 17);
            this.checkBox_vychoz.TabIndex = 71;
            this.checkBox_vychoz.Text = "Výchozí lokace";
            this.checkBox_vychoz.UseVisualStyleBackColor = true;
            // 
            // checkBox_Prijm
            // 
            this.checkBox_Prijm.AutoSize = true;
            this.checkBox_Prijm.Checked = true;
            this.checkBox_Prijm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Prijm.Location = new System.Drawing.Point(196, 76);
            this.checkBox_Prijm.Name = "checkBox_Prijm";
            this.checkBox_Prijm.Size = new System.Drawing.Size(104, 17);
            this.checkBox_Prijm.TabIndex = 70;
            this.checkBox_Prijm.Text = "Příjmová lokace";
            this.checkBox_Prijm.UseVisualStyleBackColor = true;
            // 
            // tb_Typ
            // 
            this.tb_Typ.Location = new System.Drawing.Point(64, 76);
            this.tb_Typ.Name = "tb_Typ";
            this.tb_Typ.Size = new System.Drawing.Size(100, 20);
            this.tb_Typ.TabIndex = 67;
            // 
            // tsFiltry
            // 
            this.tsFiltry.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsFiltry.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbFiltry,
            this.tsbNastavit,
            this.toolStripSeparator3,
            this.tsbZmena,
            this.toolStripSeparator4,
            this.tsbPridat,
            this.toolStripSeparator5,
            this.tsbOdebrat,
            this.toolStripSeparator6,
            this.tsbVycistit});
            this.tsFiltry.Location = new System.Drawing.Point(0, 24);
            this.tsFiltry.Name = "tsFiltry";
            this.tsFiltry.Size = new System.Drawing.Size(530, 25);
            this.tsFiltry.TabIndex = 65;
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
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
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(451, 59);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 60;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 62;
            this.label3.Text = "Typ :";
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuList,
            this.tsmiMenuVyber,
            this.tsmiVystup,
            this.tsmiPolozka});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(530, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
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
            // tsmiMenuVyber
            // 
            this.tsmiMenuVyber.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiVybrat,
            this.toolStripSeparator8,
            this.tsmiKonecVyber});
            this.tsmiMenuVyber.Name = "tsmiMenuVyber";
            this.tsmiMenuVyber.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenuVyber.Text = "Menu";
            // 
            // tsmiVybrat
            // 
            this.tsmiVybrat.Name = "tsmiVybrat";
            this.tsmiVybrat.Size = new System.Drawing.Size(148, 22);
            this.tsmiVybrat.Text = "Vybrat";
            this.tsmiVybrat.Click += new System.EventHandler(this.buttonVybratTyp_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(145, 6);
            // 
            // tsmiKonecVyber
            // 
            this.tsmiKonecVyber.Name = "tsmiKonecVyber";
            this.tsmiKonecVyber.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonecVyber.Size = new System.Drawing.Size(148, 22);
            this.tsmiKonecVyber.Text = "Konec";
            // 
            // tsmiVystup
            // 
            this.tsmiVystup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiskToolStripMenuItem,
            this.tiskEtiketToolStripMenuItem,
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator1,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExcelOznacene,
            this.toolStripSeparator7,
            this.tsmiExportDoXMLVse,
            this.tsmiExportDoXMLOznacene});
            this.tsmiVystup.Name = "tsmiVystup";
            this.tsmiVystup.Size = new System.Drawing.Size(55, 20);
            this.tsmiVystup.Text = "Výstup";
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
            // tsmiNovy
            // 
            this.tsmiNovy.Name = "tsmiNovy";
            this.tsmiNovy.Size = new System.Drawing.Size(123, 22);
            this.tsmiNovy.Text = "Nový";
            this.tsmiNovy.Click += new System.EventHandler(this.tsmiNovy_Click);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(250, 283);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(76, 76);
            this.progressIndicator1.TabIndex = 9;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // panelButtonsZobrazeniVyber
            // 
            this.panelButtonsZobrazeniVyber.Controls.Add(this.button1);
            this.panelButtonsZobrazeniVyber.Controls.Add(this.buttonVybratTyp);
            this.panelButtonsZobrazeniVyber.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniVyber.Location = new System.Drawing.Point(614, 0);
            this.panelButtonsZobrazeniVyber.Name = "panelButtonsZobrazeniVyber";
            this.panelButtonsZobrazeniVyber.Size = new System.Drawing.Size(84, 513);
            this.panelButtonsZobrazeniVyber.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(6, 438);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 63);
            this.button1.TabIndex = 4;
            this.button1.Text = "Konec";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // buttonVybratTyp
            // 
            this.buttonVybratTyp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVybratTyp.Location = new System.Drawing.Point(6, 24);
            this.buttonVybratTyp.Name = "buttonVybratTyp";
            this.buttonVybratTyp.Size = new System.Drawing.Size(73, 63);
            this.buttonVybratTyp.TabIndex = 2;
            this.buttonVybratTyp.Text = "Vybrat Typ";
            this.buttonVybratTyp.UseVisualStyleBackColor = true;
            this.buttonVybratTyp.Click += new System.EventHandler(this.buttonVybratTyp_Click);
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 149);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(530, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 10;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // FormLokaceTypyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 513);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dgSkladLokace);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.KeyPreview = true;
            this.Name = "FormLokaceTypyList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Typy lokací";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLokaceTypyList_FormClosing);
            this.Load += new System.EventHandler(this.FormLokaceTypyList_Load);
            this.Shown += new System.EventHandler(this.FormLokaceTypyList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormLokaceTypyList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgSkladLokace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSkladLokace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSkladLokace)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgSkladLokace;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtonsZobrazeniList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuList;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecList;
        private System.Windows.Forms.BindingSource bsSkladLokace;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private Fask.Interfaces.DataSets.SkladLokace dsSkladLokace;
        private System.Windows.Forms.CheckBox checkBox_Bezna;
        private System.Windows.Forms.CheckBox checkBox_vychoz;
        private System.Windows.Forms.CheckBox checkBox_Prijm;
        private System.Windows.Forms.TextBox tb_Typ;
        private System.Windows.Forms.ToolStrip tsFiltry;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbFiltry;
        private System.Windows.Forms.ToolStripButton tsbNastavit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbZmena;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbPridat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbOdebrat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbVycistit;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem tsmiVystup;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private System.Windows.Forms.Panel panelButtonsZobrazeniVyber;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonVybratTyp;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuVyber;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonecVyber;
        private System.Windows.Forms.ToolStripMenuItem tsmiVybrat;
        private System.Windows.Forms.ToolStripMenuItem tsmiPolozka;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravit;
        private System.Windows.Forms.ToolStripMenuItem tsmiNovy;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iSRECEIVEDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iSDEFAULTDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iSNORMALDataGridViewCheckBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem tiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tiskEtiketToolStripMenuItem;
    }
}