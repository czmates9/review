namespace Production.Forms
{
    partial class FormZboziList
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.konzolaDataSet1 = new Production.DataServices.KonzolaDataSet();
            this.odstranitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upravitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonOdstranit = new System.Windows.Forms.Button();
            this.zaznamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.obnovitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonUpravit = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonObnovit = new System.Windows.Forms.Button();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonNovy = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iTEMNMBRDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.cZCarKodDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.lOCNCODEDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.qTYDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.qTYPACKDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.mJDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dMJDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.TIMEMODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tAXRATEDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pRICE0DataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pRICE1DataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pRICE2DataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pRICE3DataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pRICE4DataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pRICE5DataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.cZSerNumTrackDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.cZSerNumDelkaDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.cZRez1TrackDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.cZRez2TrackDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.cZRez3TrackDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.cZRez4TrackDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.rEZ1DataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dEXROWIDDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.iTEMCODEDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.oDBIDDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.tIMEPREPDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.tIMEUNITDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.tIMEFROMDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.tIMETODataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.lSTModDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.loginidDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.konzolaDataSet1)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.vNDITNUMDataGridViewTextBoxColumn,
            this.cZCarKodDataGridViewTextBoxColumn,
            this.lOCNCODEDataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.qTYDataGridViewTextBoxColumn,
            this.qTYPACKDataGridViewTextBoxColumn,
            this.mJDataGridViewTextBoxColumn,
            this.dMJDataGridViewTextBoxColumn,
            this.TIMEMODE,
            this.tAXRATEDataGridViewTextBoxColumn,
            this.pRICE0DataGridViewTextBoxColumn,
            this.pRICE1DataGridViewTextBoxColumn,
            this.pRICE2DataGridViewTextBoxColumn,
            this.pRICE3DataGridViewTextBoxColumn,
            this.pRICE4DataGridViewTextBoxColumn,
            this.pRICE5DataGridViewTextBoxColumn,
            this.cZSerNumTrackDataGridViewTextBoxColumn,
            this.cZSerNumDelkaDataGridViewTextBoxColumn,
            this.cZRez1TrackDataGridViewTextBoxColumn,
            this.cZRez2TrackDataGridViewTextBoxColumn,
            this.cZRez3TrackDataGridViewTextBoxColumn,
            this.cZRez4TrackDataGridViewTextBoxColumn,
            this.rEZ1DataGridViewTextBoxColumn,
            this.dEXROWIDDataGridViewTextBoxColumn,
            this.iTEMCODEDataGridViewTextBoxColumn,
            this.oDBIDDataGridViewTextBoxColumn,
            this.tIMEPREPDataGridViewTextBoxColumn,
            this.tIMEUNITDataGridViewTextBoxColumn,
            this.tIMEFROMDataGridViewTextBoxColumn,
            this.tIMETODataGridViewTextBoxColumn,
            this.lSTModDataGridViewTextBoxColumn,
            this.loginidDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(580, 446);
            this.dataGridView1.TabIndex = 1;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "FASK_CONS_095";
            this.bindingSource1.DataSource = this.konzolaDataSet1;
            // 
            // konzolaDataSet1
            // 
            this.konzolaDataSet1.DataSetName = "KonzolaDataSet";
            this.konzolaDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // odstranitToolStripMenuItem
            // 
            this.odstranitToolStripMenuItem.Name = "odstranitToolStripMenuItem";
            this.odstranitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.odstranitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.odstranitToolStripMenuItem.Text = "Odstranit";
            this.odstranitToolStripMenuItem.Click += new System.EventHandler(this.odstranitToolStripMenuItem_Click);
            // 
            // upravitToolStripMenuItem
            // 
            this.upravitToolStripMenuItem.Name = "upravitToolStripMenuItem";
            this.upravitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.upravitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.upravitToolStripMenuItem.Text = "Upravit";
            this.upravitToolStripMenuItem.Click += new System.EventHandler(this.upravitToolStripMenuItem_Click);
            // 
            // novýToolStripMenuItem
            // 
            this.novýToolStripMenuItem.Name = "novýToolStripMenuItem";
            this.novýToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.novýToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.novýToolStripMenuItem.Text = "Nový";
            this.novýToolStripMenuItem.Click += new System.EventHandler(this.novýToolStripMenuItem_Click);
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
            // zaznamToolStripMenuItem
            // 
            this.zaznamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novýToolStripMenuItem,
            this.upravitToolStripMenuItem,
            this.odstranitToolStripMenuItem,
            this.toolStripSeparator1,
            this.obnovitToolStripMenuItem,
            this.konecToolStripMenuItem});
            this.zaznamToolStripMenuItem.Name = "zaznamToolStripMenuItem";
            this.zaznamToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.zaznamToolStripMenuItem.Text = "Zboží";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(163, 6);
            // 
            // obnovitToolStripMenuItem
            // 
            this.obnovitToolStripMenuItem.Name = "obnovitToolStripMenuItem";
            this.obnovitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.obnovitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.obnovitToolStripMenuItem.Text = "Aktualizovat";
            this.obnovitToolStripMenuItem.Click += new System.EventHandler(this.obnovitToolStripMenuItem_Click);
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.konecToolStripMenuItem.Text = "Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
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
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonObnovit);
            this.panelButtons.Controls.Add(this.buttonKonec);
            this.panelButtons.Controls.Add(this.buttonOdstranit);
            this.panelButtons.Controls.Add(this.buttonUpravit);
            this.panelButtons.Controls.Add(this.buttonNovy);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(580, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 470);
            this.panelButtons.TabIndex = 2;
            // 
            // buttonObnovit
            // 
            this.buttonObnovit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonObnovit.Location = new System.Drawing.Point(6, 326);
            this.buttonObnovit.Name = "buttonObnovit";
            this.buttonObnovit.Size = new System.Drawing.Size(73, 63);
            this.buttonObnovit.TabIndex = 10;
            this.buttonObnovit.Text = "Aktualizovat";
            this.buttonObnovit.UseVisualStyleBackColor = true;
            this.buttonObnovit.Click += new System.EventHandler(this.obnovitToolStripMenuItem_Click);
            // 
            // buttonKonec
            // 
            this.buttonKonec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKonec.Location = new System.Drawing.Point(6, 395);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(73, 63);
            this.buttonKonec.TabIndex = 3;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
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
            this.buttonNovy.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zaznamToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(580, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.menuStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 470);
            this.panel1.TabIndex = 3;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "Pol. číslo";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMNMBRDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Popis";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMDESCDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // vNDITNUMDataGridViewTextBoxColumn
            // 
            this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "Č. k. dodavatele";
            this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
            this.vNDITNUMDataGridViewTextBoxColumn.ReadOnly = true;
            this.vNDITNUMDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // cZCarKodDataGridViewTextBoxColumn
            // 
            this.cZCarKodDataGridViewTextBoxColumn.DataPropertyName = "CZ_CarKod";
            this.cZCarKodDataGridViewTextBoxColumn.HeaderText = "Č. kód";
            this.cZCarKodDataGridViewTextBoxColumn.Name = "cZCarKodDataGridViewTextBoxColumn";
            this.cZCarKodDataGridViewTextBoxColumn.ReadOnly = true;
            this.cZCarKodDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // lOCNCODEDataGridViewTextBoxColumn
            // 
            this.lOCNCODEDataGridViewTextBoxColumn.DataPropertyName = "LOCNCODE";
            this.lOCNCODEDataGridViewTextBoxColumn.HeaderText = "Lokace";
            this.lOCNCODEDataGridViewTextBoxColumn.Name = "lOCNCODEDataGridViewTextBoxColumn";
            this.lOCNCODEDataGridViewTextBoxColumn.ReadOnly = true;
            this.lOCNCODEDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // sKLIDDataGridViewTextBoxColumn
            // 
            this.sKLIDDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.HeaderText = "Sklad";
            this.sKLIDDataGridViewTextBoxColumn.Name = "sKLIDDataGridViewTextBoxColumn";
            this.sKLIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.sKLIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // qTYDataGridViewTextBoxColumn
            // 
            this.qTYDataGridViewTextBoxColumn.DataPropertyName = "QTY";
            this.qTYDataGridViewTextBoxColumn.HeaderText = "Množství";
            this.qTYDataGridViewTextBoxColumn.Name = "qTYDataGridViewTextBoxColumn";
            this.qTYDataGridViewTextBoxColumn.ReadOnly = true;
            this.qTYDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // qTYPACKDataGridViewTextBoxColumn
            // 
            this.qTYPACKDataGridViewTextBoxColumn.DataPropertyName = "QTYPACK";
            this.qTYPACKDataGridViewTextBoxColumn.HeaderText = "Množství v balení";
            this.qTYPACKDataGridViewTextBoxColumn.Name = "qTYPACKDataGridViewTextBoxColumn";
            this.qTYPACKDataGridViewTextBoxColumn.ReadOnly = true;
            this.qTYPACKDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // mJDataGridViewTextBoxColumn
            // 
            this.mJDataGridViewTextBoxColumn.DataPropertyName = "MJ";
            this.mJDataGridViewTextBoxColumn.HeaderText = "Měrná jednotka";
            this.mJDataGridViewTextBoxColumn.Name = "mJDataGridViewTextBoxColumn";
            this.mJDataGridViewTextBoxColumn.ReadOnly = true;
            this.mJDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dMJDataGridViewTextBoxColumn
            // 
            this.dMJDataGridViewTextBoxColumn.DataPropertyName = "DMJ";
            this.dMJDataGridViewTextBoxColumn.HeaderText = "Dop. měrná jednotka";
            this.dMJDataGridViewTextBoxColumn.Name = "dMJDataGridViewTextBoxColumn";
            this.dMJDataGridViewTextBoxColumn.ReadOnly = true;
            this.dMJDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMEMODE
            // 
            this.TIMEMODE.DataPropertyName = "TIMEMODE";
            this.TIMEMODE.HeaderText = "Typ sledování času";
            this.TIMEMODE.Name = "TIMEMODE";
            this.TIMEMODE.ReadOnly = true;
            // 
            // tAXRATEDataGridViewTextBoxColumn
            // 
            this.tAXRATEDataGridViewTextBoxColumn.DataPropertyName = "TAXRATE";
            this.tAXRATEDataGridViewTextBoxColumn.HeaderText = "výše DPH";
            this.tAXRATEDataGridViewTextBoxColumn.Name = "tAXRATEDataGridViewTextBoxColumn";
            this.tAXRATEDataGridViewTextBoxColumn.ReadOnly = true;
            this.tAXRATEDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // pRICE0DataGridViewTextBoxColumn
            // 
            this.pRICE0DataGridViewTextBoxColumn.DataPropertyName = "PRICE0";
            this.pRICE0DataGridViewTextBoxColumn.HeaderText = "Základ. cena";
            this.pRICE0DataGridViewTextBoxColumn.Name = "pRICE0DataGridViewTextBoxColumn";
            this.pRICE0DataGridViewTextBoxColumn.ReadOnly = true;
            this.pRICE0DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // pRICE1DataGridViewTextBoxColumn
            // 
            this.pRICE1DataGridViewTextBoxColumn.DataPropertyName = "PRICE1";
            this.pRICE1DataGridViewTextBoxColumn.HeaderText = "Cen. hladina 1";
            this.pRICE1DataGridViewTextBoxColumn.Name = "pRICE1DataGridViewTextBoxColumn";
            this.pRICE1DataGridViewTextBoxColumn.ReadOnly = true;
            this.pRICE1DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // pRICE2DataGridViewTextBoxColumn
            // 
            this.pRICE2DataGridViewTextBoxColumn.DataPropertyName = "PRICE2";
            this.pRICE2DataGridViewTextBoxColumn.HeaderText = "Cen. hladina 2";
            this.pRICE2DataGridViewTextBoxColumn.Name = "pRICE2DataGridViewTextBoxColumn";
            this.pRICE2DataGridViewTextBoxColumn.ReadOnly = true;
            this.pRICE2DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // pRICE3DataGridViewTextBoxColumn
            // 
            this.pRICE3DataGridViewTextBoxColumn.DataPropertyName = "PRICE3";
            this.pRICE3DataGridViewTextBoxColumn.HeaderText = "Cen. hladina 3";
            this.pRICE3DataGridViewTextBoxColumn.Name = "pRICE3DataGridViewTextBoxColumn";
            this.pRICE3DataGridViewTextBoxColumn.ReadOnly = true;
            this.pRICE3DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // pRICE4DataGridViewTextBoxColumn
            // 
            this.pRICE4DataGridViewTextBoxColumn.DataPropertyName = "PRICE4";
            this.pRICE4DataGridViewTextBoxColumn.HeaderText = "Cen. hladina 4";
            this.pRICE4DataGridViewTextBoxColumn.Name = "pRICE4DataGridViewTextBoxColumn";
            this.pRICE4DataGridViewTextBoxColumn.ReadOnly = true;
            this.pRICE4DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // pRICE5DataGridViewTextBoxColumn
            // 
            this.pRICE5DataGridViewTextBoxColumn.DataPropertyName = "PRICE5";
            this.pRICE5DataGridViewTextBoxColumn.HeaderText = "Cen. hladina 5";
            this.pRICE5DataGridViewTextBoxColumn.Name = "pRICE5DataGridViewTextBoxColumn";
            this.pRICE5DataGridViewTextBoxColumn.ReadOnly = true;
            this.pRICE5DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // cZSerNumTrackDataGridViewTextBoxColumn
            // 
            this.cZSerNumTrackDataGridViewTextBoxColumn.DataPropertyName = "CZ_SerNum_Track";
            this.cZSerNumTrackDataGridViewTextBoxColumn.HeaderText = "Sledovat na sér. č.";
            this.cZSerNumTrackDataGridViewTextBoxColumn.Name = "cZSerNumTrackDataGridViewTextBoxColumn";
            this.cZSerNumTrackDataGridViewTextBoxColumn.ReadOnly = true;
            this.cZSerNumTrackDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // cZSerNumDelkaDataGridViewTextBoxColumn
            // 
            this.cZSerNumDelkaDataGridViewTextBoxColumn.DataPropertyName = "CZ_SerNum_Delka";
            this.cZSerNumDelkaDataGridViewTextBoxColumn.HeaderText = "Délka sér. č.";
            this.cZSerNumDelkaDataGridViewTextBoxColumn.Name = "cZSerNumDelkaDataGridViewTextBoxColumn";
            this.cZSerNumDelkaDataGridViewTextBoxColumn.ReadOnly = true;
            this.cZSerNumDelkaDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // cZRez1TrackDataGridViewTextBoxColumn
            // 
            this.cZRez1TrackDataGridViewTextBoxColumn.DataPropertyName = "CZ_Rez1_Track";
            this.cZRez1TrackDataGridViewTextBoxColumn.HeaderText = "CZ_Rez1_Track";
            this.cZRez1TrackDataGridViewTextBoxColumn.Name = "cZRez1TrackDataGridViewTextBoxColumn";
            this.cZRez1TrackDataGridViewTextBoxColumn.ReadOnly = true;
            this.cZRez1TrackDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cZRez1TrackDataGridViewTextBoxColumn.Visible = false;
            // 
            // cZRez2TrackDataGridViewTextBoxColumn
            // 
            this.cZRez2TrackDataGridViewTextBoxColumn.DataPropertyName = "CZ_Rez2_Track";
            this.cZRez2TrackDataGridViewTextBoxColumn.HeaderText = "CZ_Rez2_Track";
            this.cZRez2TrackDataGridViewTextBoxColumn.Name = "cZRez2TrackDataGridViewTextBoxColumn";
            this.cZRez2TrackDataGridViewTextBoxColumn.ReadOnly = true;
            this.cZRez2TrackDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cZRez2TrackDataGridViewTextBoxColumn.Visible = false;
            // 
            // cZRez3TrackDataGridViewTextBoxColumn
            // 
            this.cZRez3TrackDataGridViewTextBoxColumn.DataPropertyName = "CZ_Rez3_Track";
            this.cZRez3TrackDataGridViewTextBoxColumn.HeaderText = "CZ_Rez3_Track";
            this.cZRez3TrackDataGridViewTextBoxColumn.Name = "cZRez3TrackDataGridViewTextBoxColumn";
            this.cZRez3TrackDataGridViewTextBoxColumn.ReadOnly = true;
            this.cZRez3TrackDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cZRez3TrackDataGridViewTextBoxColumn.Visible = false;
            // 
            // cZRez4TrackDataGridViewTextBoxColumn
            // 
            this.cZRez4TrackDataGridViewTextBoxColumn.DataPropertyName = "CZ_Rez4_Track";
            this.cZRez4TrackDataGridViewTextBoxColumn.HeaderText = "CZ_Rez4_Track";
            this.cZRez4TrackDataGridViewTextBoxColumn.Name = "cZRez4TrackDataGridViewTextBoxColumn";
            this.cZRez4TrackDataGridViewTextBoxColumn.ReadOnly = true;
            this.cZRez4TrackDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cZRez4TrackDataGridViewTextBoxColumn.Visible = false;
            // 
            // rEZ1DataGridViewTextBoxColumn
            // 
            this.rEZ1DataGridViewTextBoxColumn.DataPropertyName = "REZ1";
            this.rEZ1DataGridViewTextBoxColumn.HeaderText = "REZ1";
            this.rEZ1DataGridViewTextBoxColumn.Name = "rEZ1DataGridViewTextBoxColumn";
            this.rEZ1DataGridViewTextBoxColumn.ReadOnly = true;
            this.rEZ1DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.rEZ1DataGridViewTextBoxColumn.Visible = false;
            // 
            // dEXROWIDDataGridViewTextBoxColumn
            // 
            this.dEXROWIDDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn.HeaderText = "Index";
            this.dEXROWIDDataGridViewTextBoxColumn.Name = "dEXROWIDDataGridViewTextBoxColumn";
            this.dEXROWIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.dEXROWIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // iTEMCODEDataGridViewTextBoxColumn
            // 
            this.iTEMCODEDataGridViewTextBoxColumn.DataPropertyName = "ITEMCODE";
            this.iTEMCODEDataGridViewTextBoxColumn.HeaderText = "ITEMCODE";
            this.iTEMCODEDataGridViewTextBoxColumn.Name = "iTEMCODEDataGridViewTextBoxColumn";
            this.iTEMCODEDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMCODEDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.iTEMCODEDataGridViewTextBoxColumn.Visible = false;
            // 
            // oDBIDDataGridViewTextBoxColumn
            // 
            this.oDBIDDataGridViewTextBoxColumn.DataPropertyName = "ODB_ID";
            this.oDBIDDataGridViewTextBoxColumn.HeaderText = "Odběratel";
            this.oDBIDDataGridViewTextBoxColumn.Name = "oDBIDDataGridViewTextBoxColumn";
            this.oDBIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.oDBIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // tIMEPREPDataGridViewTextBoxColumn
            // 
            this.tIMEPREPDataGridViewTextBoxColumn.DataPropertyName = "TIMEPREP";
            this.tIMEPREPDataGridViewTextBoxColumn.HeaderText = "Přípravný čas";
            this.tIMEPREPDataGridViewTextBoxColumn.Name = "tIMEPREPDataGridViewTextBoxColumn";
            this.tIMEPREPDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMEPREPDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // tIMEUNITDataGridViewTextBoxColumn
            // 
            this.tIMEUNITDataGridViewTextBoxColumn.DataPropertyName = "TIMEUNIT";
            this.tIMEUNITDataGridViewTextBoxColumn.HeaderText = "Jednotkový čas";
            this.tIMEUNITDataGridViewTextBoxColumn.Name = "tIMEUNITDataGridViewTextBoxColumn";
            this.tIMEUNITDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMEUNITDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // tIMEFROMDataGridViewTextBoxColumn
            // 
            this.tIMEFROMDataGridViewTextBoxColumn.DataPropertyName = "TIMEFROM";
            this.tIMEFROMDataGridViewTextBoxColumn.HeaderText = "Platnost od";
            this.tIMEFROMDataGridViewTextBoxColumn.Name = "tIMEFROMDataGridViewTextBoxColumn";
            this.tIMEFROMDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMEFROMDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // tIMETODataGridViewTextBoxColumn
            // 
            this.tIMETODataGridViewTextBoxColumn.DataPropertyName = "TIMETO";
            this.tIMETODataGridViewTextBoxColumn.HeaderText = "Platnost do";
            this.tIMETODataGridViewTextBoxColumn.Name = "tIMETODataGridViewTextBoxColumn";
            this.tIMETODataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMETODataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // lSTModDataGridViewTextBoxColumn
            // 
            this.lSTModDataGridViewTextBoxColumn.DataPropertyName = "LSTMod";
            this.lSTModDataGridViewTextBoxColumn.HeaderText = "Poslední úprava";
            this.lSTModDataGridViewTextBoxColumn.Name = "lSTModDataGridViewTextBoxColumn";
            this.lSTModDataGridViewTextBoxColumn.ReadOnly = true;
            this.lSTModDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // loginidDataGridViewTextBoxColumn
            // 
            this.loginidDataGridViewTextBoxColumn.DataPropertyName = "loginid";
            this.loginidDataGridViewTextBoxColumn.HeaderText = "ID uživatel";
            this.loginidDataGridViewTextBoxColumn.Name = "loginidDataGridViewTextBoxColumn";
            this.loginidDataGridViewTextBoxColumn.ReadOnly = true;
            this.loginidDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // FormZboziList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 470);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormZboziList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zboží";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormZboziList_FormClosing);
            this.Load += new System.EventHandler(this.FormZboziList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormZboziList_KeyDown);
            this.Resize += new System.EventHandler(this.FormZboziList_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.konzolaDataSet1)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem odstranitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upravitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novýToolStripMenuItem;
        private System.Windows.Forms.Button buttonOdstranit;
        private System.Windows.Forms.ToolStripMenuItem zaznamToolStripMenuItem;
        private System.Windows.Forms.Button buttonUpravit;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonNovy;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.Panel panel1;
        private DataServices.KonzolaDataSet konzolaDataSet1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem obnovitToolStripMenuItem;
        private System.Windows.Forms.Button buttonObnovit;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn vNDITNUMDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn cZCarKodDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn lOCNCODEDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn qTYDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn qTYPACKDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn mJDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dMJDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEMODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn tAXRATEDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn pRICE0DataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn pRICE1DataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn pRICE2DataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn pRICE3DataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn pRICE4DataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn pRICE5DataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn cZSerNumTrackDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn cZSerNumDelkaDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn cZRez1TrackDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn cZRez2TrackDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn cZRez3TrackDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn cZRez4TrackDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn rEZ1DataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dEXROWIDDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn iTEMCODEDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn oDBIDDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn tIMEPREPDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn tIMEUNITDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn tIMEFROMDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn tIMETODataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn lSTModDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn loginidDataGridViewTextBoxColumn;
    }
}