namespace Fask.Vyroba_P.Odvadeni
{
    partial class FormVyberPrikazOperace
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.countEntriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPNUMBEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMTYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oRDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMMJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYPACKDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYPACKMJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYODVEDENODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNTODVEDENODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodePDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDDOCNMPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOCNCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMEPREPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMEUNITDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtProdTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtProdLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serNumTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serNumLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.verTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.verLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.termIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lSTModDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEXROWIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cZPROVPPBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vyrobaCEDataSet = new Fask.SQLiteDBs.DataSets.Vyroba();
            this.label2 = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.ucDetailHlavicka = new Fask.Vyroba_P.Controls.ucDetail();
            this.ucDetailOperace = new Fask.Vyroba_P.Controls.ucDetail();
            this.panelComponents.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cZPROVPPBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaCEDataSet)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(221, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(661, 71);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.splitContainer1);
            this.panelComponents.Controls.Add(this.panelButtons);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(882, 575);
            this.panelComponents.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ucDetailHlavicka);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(882, 504);
            this.splitContainer1.SplitterDistance = 245;
            this.splitContainer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pøíkaz";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ucDetailOperace);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Size = new System.Drawing.Size(633, 504);
            this.splitContainer2.SplitterDistance = 442;
            this.splitContainer2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.countEntriesDataGridViewTextBoxColumn,
            this.sOPNUMBEDataGridViewTextBoxColumn,
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMTYPEDataGridViewTextBoxColumn,
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.oRDDataGridViewTextBoxColumn,
            this.qTYSHPPDDataGridViewTextBoxColumn,
            this.iTEMMJDataGridViewTextBoxColumn,
            this.qTYPACKDataGridViewTextBoxColumn,
            this.qTYPACKMJDataGridViewTextBoxColumn,
            this.qTYODVEDENODataGridViewTextBoxColumn,
            this.cNTODVEDENODataGridViewTextBoxColumn,
            this.barcodePDataGridViewTextBoxColumn,
            this.vNDDOCNMPDataGridViewTextBoxColumn,
            this.lOCNCODEDataGridViewTextBoxColumn,
            this.vNDITNUMDataGridViewTextBoxColumn,
            this.tIMEPREPDataGridViewTextBoxColumn,
            this.tIMEUNITDataGridViewTextBoxColumn,
            this.dtProdTDataGridViewTextBoxColumn,
            this.dtProdLDataGridViewTextBoxColumn,
            this.serNumTDataGridViewTextBoxColumn,
            this.serNumLDataGridViewTextBoxColumn,
            this.verTDataGridViewTextBoxColumn,
            this.verLDataGridViewTextBoxColumn,
            this.termIDDataGridViewTextBoxColumn,
            this.lSTModDataGridViewTextBoxColumn,
            this.dEXROWIDDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.cZPROVPPBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(442, 504);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // countEntriesDataGridViewTextBoxColumn
            // 
            this.countEntriesDataGridViewTextBoxColumn.DataPropertyName = "CountEntries";
            this.countEntriesDataGridViewTextBoxColumn.HeaderText = "Dávka";
            this.countEntriesDataGridViewTextBoxColumn.Name = "countEntriesDataGridViewTextBoxColumn";
            this.countEntriesDataGridViewTextBoxColumn.ReadOnly = true;
            this.countEntriesDataGridViewTextBoxColumn.Visible = false;
            this.countEntriesDataGridViewTextBoxColumn.Width = 64;
            // 
            // sOPNUMBEDataGridViewTextBoxColumn
            // 
            this.sOPNUMBEDataGridViewTextBoxColumn.DataPropertyName = "SOPNUMBE";
            this.sOPNUMBEDataGridViewTextBoxColumn.HeaderText = "Pøíkaz è.";
            this.sOPNUMBEDataGridViewTextBoxColumn.Name = "sOPNUMBEDataGridViewTextBoxColumn";
            this.sOPNUMBEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sOPNUMBEDataGridViewTextBoxColumn.Width = 93;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "Položka è.";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMNMBRDataGridViewTextBoxColumn.Width = 106;
            // 
            // iTEMTYPEDataGridViewTextBoxColumn
            // 
            this.iTEMTYPEDataGridViewTextBoxColumn.DataPropertyName = "ITEMTYPE";
            this.iTEMTYPEDataGridViewTextBoxColumn.HeaderText = "Typ";
            this.iTEMTYPEDataGridViewTextBoxColumn.Name = "iTEMTYPEDataGridViewTextBoxColumn";
            this.iTEMTYPEDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMTYPEDataGridViewTextBoxColumn.Width = 59;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Popis";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMDESCDataGridViewTextBoxColumn.Width = 73;
            // 
            // oRDDataGridViewTextBoxColumn
            // 
            this.oRDDataGridViewTextBoxColumn.DataPropertyName = "ORD";
            this.oRDDataGridViewTextBoxColumn.HeaderText = "Poøadí";
            this.oRDDataGridViewTextBoxColumn.Name = "oRDDataGridViewTextBoxColumn";
            this.oRDDataGridViewTextBoxColumn.ReadOnly = true;
            this.oRDDataGridViewTextBoxColumn.Width = 79;
            // 
            // qTYSHPPDDataGridViewTextBoxColumn
            // 
            this.qTYSHPPDDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPD";
            this.qTYSHPPDDataGridViewTextBoxColumn.HeaderText = "Množství";
            this.qTYSHPPDDataGridViewTextBoxColumn.Name = "qTYSHPPDDataGridViewTextBoxColumn";
            this.qTYSHPPDDataGridViewTextBoxColumn.ReadOnly = true;
            this.qTYSHPPDDataGridViewTextBoxColumn.Width = 96;
            // 
            // iTEMMJDataGridViewTextBoxColumn
            // 
            this.iTEMMJDataGridViewTextBoxColumn.DataPropertyName = "ITEMMJ";
            this.iTEMMJDataGridViewTextBoxColumn.HeaderText = "MJ";
            this.iTEMMJDataGridViewTextBoxColumn.Name = "iTEMMJDataGridViewTextBoxColumn";
            this.iTEMMJDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMMJDataGridViewTextBoxColumn.Width = 55;
            // 
            // qTYPACKDataGridViewTextBoxColumn
            // 
            this.qTYPACKDataGridViewTextBoxColumn.DataPropertyName = "QTYPACK";
            this.qTYPACKDataGridViewTextBoxColumn.HeaderText = "Balení";
            this.qTYPACKDataGridViewTextBoxColumn.Name = "qTYPACKDataGridViewTextBoxColumn";
            this.qTYPACKDataGridViewTextBoxColumn.ReadOnly = true;
            this.qTYPACKDataGridViewTextBoxColumn.Width = 78;
            // 
            // qTYPACKMJDataGridViewTextBoxColumn
            // 
            this.qTYPACKMJDataGridViewTextBoxColumn.DataPropertyName = "QTYPACKMJ";
            this.qTYPACKMJDataGridViewTextBoxColumn.HeaderText = "MJ Balení";
            this.qTYPACKMJDataGridViewTextBoxColumn.Name = "qTYPACKMJDataGridViewTextBoxColumn";
            this.qTYPACKMJDataGridViewTextBoxColumn.ReadOnly = true;
            this.qTYPACKMJDataGridViewTextBoxColumn.Width = 103;
            // 
            // qTYODVEDENODataGridViewTextBoxColumn
            // 
            this.qTYODVEDENODataGridViewTextBoxColumn.DataPropertyName = "QTYODVEDENO";
            this.qTYODVEDENODataGridViewTextBoxColumn.HeaderText = "Množství odv.";
            this.qTYODVEDENODataGridViewTextBoxColumn.Name = "qTYODVEDENODataGridViewTextBoxColumn";
            this.qTYODVEDENODataGridViewTextBoxColumn.ReadOnly = true;
            this.qTYODVEDENODataGridViewTextBoxColumn.Width = 129;
            // 
            // cNTODVEDENODataGridViewTextBoxColumn
            // 
            this.cNTODVEDENODataGridViewTextBoxColumn.DataPropertyName = "CNTODVEDENO";
            this.cNTODVEDENODataGridViewTextBoxColumn.HeaderText = "Poèet odv.";
            this.cNTODVEDENODataGridViewTextBoxColumn.Name = "cNTODVEDENODataGridViewTextBoxColumn";
            this.cNTODVEDENODataGridViewTextBoxColumn.ReadOnly = true;
            this.cNTODVEDENODataGridViewTextBoxColumn.Visible = false;
            this.cNTODVEDENODataGridViewTextBoxColumn.Width = 84;
            // 
            // barcodePDataGridViewTextBoxColumn
            // 
            this.barcodePDataGridViewTextBoxColumn.DataPropertyName = "BarcodeP";
            this.barcodePDataGridViewTextBoxColumn.HeaderText = "È.k.";
            this.barcodePDataGridViewTextBoxColumn.Name = "barcodePDataGridViewTextBoxColumn";
            this.barcodePDataGridViewTextBoxColumn.ReadOnly = true;
            this.barcodePDataGridViewTextBoxColumn.Visible = false;
            this.barcodePDataGridViewTextBoxColumn.Width = 51;
            // 
            // vNDDOCNMPDataGridViewTextBoxColumn
            // 
            this.vNDDOCNMPDataGridViewTextBoxColumn.DataPropertyName = "VNDDOCNMP";
            this.vNDDOCNMPDataGridViewTextBoxColumn.HeaderText = "Dod. è.";
            this.vNDDOCNMPDataGridViewTextBoxColumn.Name = "vNDDOCNMPDataGridViewTextBoxColumn";
            this.vNDDOCNMPDataGridViewTextBoxColumn.ReadOnly = true;
            this.vNDDOCNMPDataGridViewTextBoxColumn.Visible = false;
            this.vNDDOCNMPDataGridViewTextBoxColumn.Width = 67;
            // 
            // lOCNCODEDataGridViewTextBoxColumn
            // 
            this.lOCNCODEDataGridViewTextBoxColumn.DataPropertyName = "LOCNCODE";
            this.lOCNCODEDataGridViewTextBoxColumn.HeaderText = "Lokace";
            this.lOCNCODEDataGridViewTextBoxColumn.Name = "lOCNCODEDataGridViewTextBoxColumn";
            this.lOCNCODEDataGridViewTextBoxColumn.ReadOnly = true;
            this.lOCNCODEDataGridViewTextBoxColumn.Visible = false;
            this.lOCNCODEDataGridViewTextBoxColumn.Width = 68;
            // 
            // vNDITNUMDataGridViewTextBoxColumn
            // 
            this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "Dod. è.k.";
            this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
            this.vNDITNUMDataGridViewTextBoxColumn.ReadOnly = true;
            this.vNDITNUMDataGridViewTextBoxColumn.Visible = false;
            this.vNDITNUMDataGridViewTextBoxColumn.Width = 76;
            // 
            // tIMEPREPDataGridViewTextBoxColumn
            // 
            this.tIMEPREPDataGridViewTextBoxColumn.DataPropertyName = "TIMEPREP";
            this.tIMEPREPDataGridViewTextBoxColumn.HeaderText = "Èas pøípravy";
            this.tIMEPREPDataGridViewTextBoxColumn.Name = "tIMEPREPDataGridViewTextBoxColumn";
            this.tIMEPREPDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMEPREPDataGridViewTextBoxColumn.Visible = false;
            this.tIMEPREPDataGridViewTextBoxColumn.Width = 93;
            // 
            // tIMEUNITDataGridViewTextBoxColumn
            // 
            this.tIMEUNITDataGridViewTextBoxColumn.DataPropertyName = "TIMEUNIT";
            this.tIMEUNITDataGridViewTextBoxColumn.HeaderText = "Èas jednotkový";
            this.tIMEUNITDataGridViewTextBoxColumn.Name = "tIMEUNITDataGridViewTextBoxColumn";
            this.tIMEUNITDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMEUNITDataGridViewTextBoxColumn.Visible = false;
            this.tIMEUNITDataGridViewTextBoxColumn.Width = 105;
            // 
            // dtProdTDataGridViewTextBoxColumn
            // 
            this.dtProdTDataGridViewTextBoxColumn.DataPropertyName = "DtProdT";
            this.dtProdTDataGridViewTextBoxColumn.HeaderText = "DtProdT";
            this.dtProdTDataGridViewTextBoxColumn.Name = "dtProdTDataGridViewTextBoxColumn";
            this.dtProdTDataGridViewTextBoxColumn.ReadOnly = true;
            this.dtProdTDataGridViewTextBoxColumn.Visible = false;
            this.dtProdTDataGridViewTextBoxColumn.Width = 72;
            // 
            // dtProdLDataGridViewTextBoxColumn
            // 
            this.dtProdLDataGridViewTextBoxColumn.DataPropertyName = "DtProdL";
            this.dtProdLDataGridViewTextBoxColumn.HeaderText = "DtProdL";
            this.dtProdLDataGridViewTextBoxColumn.Name = "dtProdLDataGridViewTextBoxColumn";
            this.dtProdLDataGridViewTextBoxColumn.ReadOnly = true;
            this.dtProdLDataGridViewTextBoxColumn.Visible = false;
            this.dtProdLDataGridViewTextBoxColumn.Width = 71;
            // 
            // serNumTDataGridViewTextBoxColumn
            // 
            this.serNumTDataGridViewTextBoxColumn.DataPropertyName = "SerNumT";
            this.serNumTDataGridViewTextBoxColumn.HeaderText = "SerNumT";
            this.serNumTDataGridViewTextBoxColumn.Name = "serNumTDataGridViewTextBoxColumn";
            this.serNumTDataGridViewTextBoxColumn.ReadOnly = true;
            this.serNumTDataGridViewTextBoxColumn.Visible = false;
            this.serNumTDataGridViewTextBoxColumn.Width = 77;
            // 
            // serNumLDataGridViewTextBoxColumn
            // 
            this.serNumLDataGridViewTextBoxColumn.DataPropertyName = "SerNumL";
            this.serNumLDataGridViewTextBoxColumn.HeaderText = "SerNumL";
            this.serNumLDataGridViewTextBoxColumn.Name = "serNumLDataGridViewTextBoxColumn";
            this.serNumLDataGridViewTextBoxColumn.ReadOnly = true;
            this.serNumLDataGridViewTextBoxColumn.Visible = false;
            this.serNumLDataGridViewTextBoxColumn.Width = 76;
            // 
            // verTDataGridViewTextBoxColumn
            // 
            this.verTDataGridViewTextBoxColumn.DataPropertyName = "VerT";
            this.verTDataGridViewTextBoxColumn.HeaderText = "VerT";
            this.verTDataGridViewTextBoxColumn.Name = "verTDataGridViewTextBoxColumn";
            this.verTDataGridViewTextBoxColumn.ReadOnly = true;
            this.verTDataGridViewTextBoxColumn.Visible = false;
            this.verTDataGridViewTextBoxColumn.Width = 55;
            // 
            // verLDataGridViewTextBoxColumn
            // 
            this.verLDataGridViewTextBoxColumn.DataPropertyName = "VerL";
            this.verLDataGridViewTextBoxColumn.HeaderText = "VerL";
            this.verLDataGridViewTextBoxColumn.Name = "verLDataGridViewTextBoxColumn";
            this.verLDataGridViewTextBoxColumn.ReadOnly = true;
            this.verLDataGridViewTextBoxColumn.Visible = false;
            this.verLDataGridViewTextBoxColumn.Width = 54;
            // 
            // termIDDataGridViewTextBoxColumn
            // 
            this.termIDDataGridViewTextBoxColumn.DataPropertyName = "TermID";
            this.termIDDataGridViewTextBoxColumn.HeaderText = "Terminál ID";
            this.termIDDataGridViewTextBoxColumn.Name = "termIDDataGridViewTextBoxColumn";
            this.termIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.termIDDataGridViewTextBoxColumn.Visible = false;
            this.termIDDataGridViewTextBoxColumn.Width = 86;
            // 
            // lSTModDataGridViewTextBoxColumn
            // 
            this.lSTModDataGridViewTextBoxColumn.DataPropertyName = "LSTMod";
            this.lSTModDataGridViewTextBoxColumn.HeaderText = "Modifiováno";
            this.lSTModDataGridViewTextBoxColumn.Name = "lSTModDataGridViewTextBoxColumn";
            this.lSTModDataGridViewTextBoxColumn.ReadOnly = true;
            this.lSTModDataGridViewTextBoxColumn.Visible = false;
            this.lSTModDataGridViewTextBoxColumn.Width = 90;
            // 
            // dEXROWIDDataGridViewTextBoxColumn
            // 
            this.dEXROWIDDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn.HeaderText = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn.Name = "dEXROWIDDataGridViewTextBoxColumn";
            this.dEXROWIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.dEXROWIDDataGridViewTextBoxColumn.Visible = false;
            this.dEXROWIDDataGridViewTextBoxColumn.Width = 104;
            // 
            // cZPROVPPBindingSource
            // 
            this.cZPROVPPBindingSource.DataMember = "CZPRO_VPP";
            this.cZPROVPPBindingSource.DataSource = this.vyrobaCEDataSet;
            // 
            // vyrobaCEDataSet
            // 
            this.vyrobaCEDataSet.DataSetName = "VyrobaCEDataSet";
            this.vyrobaCEDataSet.EnforceConstraints = false;
            this.vyrobaCEDataSet.Locale = new System.Globalization.CultureInfo("");
            this.vyrobaCEDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.ExcludeSchema;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Operace";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 504);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(882, 71);
            this.panelButtons.TabIndex = 1;
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(221, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // ucDetailHlavicka
            // 
            this.ucDetailHlavicka.DetialObject = null;
            this.ucDetailHlavicka.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDetailHlavicka.Location = new System.Drawing.Point(0, 21);
            this.ucDetailHlavicka.Name = "ucDetailHlavicka";
            this.ucDetailHlavicka.Size = new System.Drawing.Size(245, 483);
            this.ucDetailHlavicka.TabIndex = 0;
            // 
            // ucDetailOperace
            // 
            this.ucDetailOperace.DetialObject = null;
            this.ucDetailOperace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDetailOperace.Location = new System.Drawing.Point(0, 20);
            this.ucDetailOperace.Name = "ucDetailOperace";
            this.ucDetailOperace.Size = new System.Drawing.Size(187, 484);
            this.ucDetailOperace.TabIndex = 0;
            // 
            // FormVyberPrikazOperace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(882, 575);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.KeyPreview = true;
            this.Name = "FormVyberPrikazOperace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výbìr výrobního pøíkazu";
            this.Load += new System.EventHandler(this.FormVyberPrikazOperace_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormInputKod_KeyDown);
            this.panelComponents.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cZPROVPPBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaCEDataSet)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Panel panelComponents;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Fask.SQLiteDBs.DataSets.Vyroba vyrobaCEDataSet;
        private System.Windows.Forms.BindingSource cZPROVPPBindingSource;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Fask.Vyroba_P.Controls.ucDetail ucDetailHlavicka;
        private Fask.Vyroba_P.Controls.ucDetail ucDetailOperace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPNUMBEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMTYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oRDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSHPPDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMMJDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYPACKDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYPACKMJDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYODVEDENODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNTODVEDENODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodePDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDDOCNMPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOCNCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDITNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMEPREPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMEUNITDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtProdTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtProdLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serNumTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serNumLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn verTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn verLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn termIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lSTModDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDDataGridViewTextBoxColumn;
    }
}
