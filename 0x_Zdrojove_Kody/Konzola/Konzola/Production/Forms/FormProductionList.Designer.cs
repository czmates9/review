namespace Production.Forms
{
    partial class FormProductionList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxVyrobniPrikaz = new System.Windows.Forms.ComboBox();
            this.vyrobaDataSet1 = new Production.DataServices.VyrobaDataSet();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxZbozi = new System.Windows.Forms.ComboBox();
            this.bindingSourceZbozi = new System.Windows.Forms.BindingSource(this.components);
            this.konzolaDataSet1 = new Production.DataServices.KonzolaDataSet();
            this.bindingSourceProduct = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxSkupina = new System.Windows.Forms.ComboBox();
            this.checkBoxKorekceVse = new System.Windows.Forms.CheckBox();
            this.checkBoxOdvadeniVse = new System.Windows.Forms.CheckBox();
            this.checkBoxKorekceNedokoncene = new System.Windows.Forms.CheckBox();
            this.dateTimePickerDatumDo = new System.Windows.Forms.DateTimePicker();
            this.checkBoxOdvadeniNedokoncene = new System.Windows.Forms.CheckBox();
            this.dateTimePickerDatumOd = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.buttonOznacitVse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dateeveDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPNUMBEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMESTARTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMESTOPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMECORSTARTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMECORSTOPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countEntriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMTYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMMJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oRDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMEMODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMEPREPSTARTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMEPREPSTOPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMEPREPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMEUNITDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMECORDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMECRIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operationidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyRealDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYPACKDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYPACKMJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodePDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.termIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSOKDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOUBEHGUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CORRGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyOldDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idVSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateeditDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBoxUzivatel = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.výrobaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upravitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ukoncitZakazkuKorekciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schvalitVybraneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSourceUzivatel = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonUkoncitZakazku = new System.Windows.Forms.Button();
            this.buttonUpravit2 = new System.Windows.Forms.Button();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonSchvalitVybrane = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxOperace = new System.Windows.Forms.ComboBox();
            this.comboBoxStroj = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceZbozi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.konzolaDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceProduct)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUzivatel)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Výrobní příkaz:";
            // 
            // comboBoxVyrobniPrikaz
            // 
            this.comboBoxVyrobniPrikaz.FormattingEnabled = true;
            this.comboBoxVyrobniPrikaz.Location = new System.Drawing.Point(97, 27);
            this.comboBoxVyrobniPrikaz.Name = "comboBoxVyrobniPrikaz";
            this.comboBoxVyrobniPrikaz.Size = new System.Drawing.Size(249, 21);
            this.comboBoxVyrobniPrikaz.TabIndex = 1;
            this.comboBoxVyrobniPrikaz.SelectedIndexChanged += new System.EventHandler(this.comboBoxVyrobniPrikaz_SelectedIndexChanged);
            // 
            // vyrobaDataSet1
            // 
            this.vyrobaDataSet1.DataSetName = "VyrobaDataSet";
            this.vyrobaDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Zboží:";
            // 
            // comboBoxZbozi
            // 
            this.comboBoxZbozi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxZbozi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBoxZbozi.FormattingEnabled = true;
            this.comboBoxZbozi.Location = new System.Drawing.Point(97, 54);
            this.comboBoxZbozi.Name = "comboBoxZbozi";
            this.comboBoxZbozi.Size = new System.Drawing.Size(249, 21);
            this.comboBoxZbozi.TabIndex = 2;
            this.comboBoxZbozi.SelectedIndexChanged += new System.EventHandler(this.comboBoxZbozi_SelectedIndexChanged);
            // 
            // bindingSourceZbozi
            // 
            this.bindingSourceZbozi.DataMember = "FASK_CONS_095";
            this.bindingSourceZbozi.DataSource = this.konzolaDataSet1;
            this.bindingSourceZbozi.CurrentChanged += new System.EventHandler(this.bindingSourceZbozi_CurrentChanged);
            // 
            // konzolaDataSet1
            // 
            this.konzolaDataSet1.DataSetName = "KonzolaDataSet";
            this.konzolaDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSourceProduct
            // 
            this.bindingSourceProduct.DataMember = "Production";
            this.bindingSourceProduct.DataSource = this.vyrobaDataSet1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.comboBoxOperace);
            this.panel1.Controls.Add(this.comboBoxStroj);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.comboBoxSkupina);
            this.panel1.Controls.Add(this.checkBoxKorekceVse);
            this.panel1.Controls.Add(this.checkBoxOdvadeniVse);
            this.panel1.Controls.Add(this.checkBoxKorekceNedokoncene);
            this.panel1.Controls.Add(this.dateTimePickerDatumDo);
            this.panel1.Controls.Add(this.checkBoxOdvadeniNedokoncene);
            this.panel1.Controls.Add(this.dateTimePickerDatumOd);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.buttonOdznacitVse);
            this.panel1.Controls.Add(this.buttonOznacitVse);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBoxUzivatel);
            this.panel1.Controls.Add(this.comboBoxZbozi);
            this.panel1.Controls.Add(this.comboBoxVyrobniPrikaz);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(919, 582);
            this.panel1.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Skupina:";
            // 
            // comboBoxSkupina
            // 
            this.comboBoxSkupina.FormattingEnabled = true;
            this.comboBoxSkupina.Location = new System.Drawing.Point(97, 107);
            this.comboBoxSkupina.Name = "comboBoxSkupina";
            this.comboBoxSkupina.Size = new System.Drawing.Size(249, 21);
            this.comboBoxSkupina.TabIndex = 4;
            // 
            // checkBoxKorekceVse
            // 
            this.checkBoxKorekceVse.AutoSize = true;
            this.checkBoxKorekceVse.Location = new System.Drawing.Point(410, 127);
            this.checkBoxKorekceVse.Name = "checkBoxKorekceVse";
            this.checkBoxKorekceVse.Size = new System.Drawing.Size(138, 17);
            this.checkBoxKorekceVse.TabIndex = 9;
            this.checkBoxKorekceVse.Text = "Zobrazit pouze korekce";
            this.checkBoxKorekceVse.UseVisualStyleBackColor = true;
            // 
            // checkBoxOdvadeniVse
            // 
            this.checkBoxOdvadeniVse.AutoSize = true;
            this.checkBoxOdvadeniVse.Location = new System.Drawing.Point(410, 82);
            this.checkBoxOdvadeniVse.Name = "checkBoxOdvadeniVse";
            this.checkBoxOdvadeniVse.Size = new System.Drawing.Size(179, 17);
            this.checkBoxOdvadeniVse.TabIndex = 7;
            this.checkBoxOdvadeniVse.Text = "Zobrazit pouze odvádění výroby";
            this.checkBoxOdvadeniVse.UseVisualStyleBackColor = true;
            this.checkBoxOdvadeniVse.CheckedChanged += new System.EventHandler(this.checkBoxZakazky_CheckedChanged);
            // 
            // checkBoxKorekceNedokoncene
            // 
            this.checkBoxKorekceNedokoncene.AutoSize = true;
            this.checkBoxKorekceNedokoncene.Location = new System.Drawing.Point(410, 150);
            this.checkBoxKorekceNedokoncene.Name = "checkBoxKorekceNedokoncene";
            this.checkBoxKorekceNedokoncene.Size = new System.Drawing.Size(175, 17);
            this.checkBoxKorekceNedokoncene.TabIndex = 10;
            this.checkBoxKorekceNedokoncene.Text = "Zobrazit nedokončené korekce";
            this.checkBoxKorekceNedokoncene.UseVisualStyleBackColor = true;
            this.checkBoxKorekceNedokoncene.CheckedChanged += new System.EventHandler(this.checkBoxNedokonceneKorekce_CheckedChanged);
            // 
            // dateTimePickerDatumDo
            // 
            this.dateTimePickerDatumDo.Checked = false;
            this.dateTimePickerDatumDo.CustomFormat = "HH:mm dd.MM.yyyy";
            this.dateTimePickerDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDatumDo.Location = new System.Drawing.Point(97, 160);
            this.dateTimePickerDatumDo.Name = "dateTimePickerDatumDo";
            this.dateTimePickerDatumDo.ShowCheckBox = true;
            this.dateTimePickerDatumDo.Size = new System.Drawing.Size(249, 20);
            this.dateTimePickerDatumDo.TabIndex = 6;
            // 
            // checkBoxOdvadeniNedokoncene
            // 
            this.checkBoxOdvadeniNedokoncene.AutoSize = true;
            this.checkBoxOdvadeniNedokoncene.Location = new System.Drawing.Point(410, 104);
            this.checkBoxOdvadeniNedokoncene.Name = "checkBoxOdvadeniNedokoncene";
            this.checkBoxOdvadeniNedokoncene.Size = new System.Drawing.Size(205, 17);
            this.checkBoxOdvadeniNedokoncene.TabIndex = 8;
            this.checkBoxOdvadeniNedokoncene.Text = "Zobrazit nedokončené odvody výroby";
            this.checkBoxOdvadeniNedokoncene.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerDatumOd
            // 
            this.dateTimePickerDatumOd.Checked = false;
            this.dateTimePickerDatumOd.CustomFormat = "HH:mm dd.MM.yyyy";
            this.dateTimePickerDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDatumOd.Location = new System.Drawing.Point(97, 134);
            this.dateTimePickerDatumOd.Name = "dateTimePickerDatumOd";
            this.dateTimePickerDatumOd.ShowCheckBox = true;
            this.dateTimePickerDatumOd.Size = new System.Drawing.Size(249, 20);
            this.dateTimePickerDatumOd.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Datum do:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Datum od:";
            // 
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Location = new System.Drawing.Point(746, 152);
            this.buttonOdznacitVse.Name = "buttonOdznacitVse";
            this.buttonOdznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOdznacitVse.TabIndex = 13;
            this.buttonOdznacitVse.Text = "Odznačit vše";
            this.buttonOdznacitVse.UseVisualStyleBackColor = true;
            this.buttonOdznacitVse.Click += new System.EventHandler(this.buttonOdznacitVse_Click);
            // 
            // buttonOznacitVse
            // 
            this.buttonOznacitVse.Location = new System.Drawing.Point(657, 152);
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
            this.label3.Location = new System.Drawing.Point(43, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Uživatel:";
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Location = new System.Drawing.Point(756, 83);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 11;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
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
            this.dateeveDataGridViewTextBoxColumn,
            this.groupName,
            this.firstname,
            this.surname,
            this.sOPNUMBEDataGridViewTextBoxColumn,
            this.tIMESTARTDataGridViewTextBoxColumn,
            this.tIMESTOPDataGridViewTextBoxColumn,
            this.tIMECORSTARTDataGridViewTextBoxColumn,
            this.tIMECORSTOPDataGridViewTextBoxColumn,
            this.operationName,
            this.machineName,
            this.dataGridViewTextBoxColumn1,
            this.countEntriesDataGridViewTextBoxColumn,
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMTYPEDataGridViewTextBoxColumn,
            this.iTEMMJDataGridViewTextBoxColumn,
            this.oRDDataGridViewTextBoxColumn,
            this.tIMEMODEDataGridViewTextBoxColumn,
            this.tIMEPREPSTARTDataGridViewTextBoxColumn,
            this.tIMEPREPSTOPDataGridViewTextBoxColumn,
            this.tIMEPREPDataGridViewTextBoxColumn,
            this.tIMEUNITDataGridViewTextBoxColumn,
            this.tIMECORDataGridViewTextBoxColumn,
            this.tIMECRIDDataGridViewTextBoxColumn,
            this.idDataGridViewTextBoxColumn,
            this.loginidDataGridViewTextBoxColumn,
            this.machineidDataGridViewTextBoxColumn,
            this.operationidDataGridViewTextBoxColumn,
            this.qtyDataGridViewTextBoxColumn,
            this.qtyRealDataGridViewTextBoxColumn,
            this.qTYPACKDataGridViewTextBoxColumn,
            this.qTYPACKMJDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.barcodePDataGridViewTextBoxColumn,
            this.userIDDataGridViewTextBoxColumn,
            this.termIDDataGridViewTextBoxColumn,
            this.iSOKDataGridViewTextBoxColumn,
            this.gUIDDataGridViewTextBoxColumn,
            this.sOUBEHGUIDDataGridViewTextBoxColumn,
            this.CORRGUID,
            this.qtyOldDataGridViewTextBoxColumn,
            this.idVSDataGridViewTextBoxColumn,
            this.dateeditDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.bindingSourceProduct;
            this.dataGridView1.Location = new System.Drawing.Point(3, 198);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(832, 381);
            this.dataGridView1.TabIndex = 30;
            // 
            // dateeveDataGridViewTextBoxColumn
            // 
            this.dateeveDataGridViewTextBoxColumn.DataPropertyName = "dateeve";
            dataGridViewCellStyle1.Format = "G";
            dataGridViewCellStyle1.NullValue = null;
            this.dateeveDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.dateeveDataGridViewTextBoxColumn.HeaderText = "Datum";
            this.dateeveDataGridViewTextBoxColumn.Name = "dateeveDataGridViewTextBoxColumn";
            this.dateeveDataGridViewTextBoxColumn.ReadOnly = true;
            this.dateeveDataGridViewTextBoxColumn.Width = 110;
            // 
            // groupName
            // 
            this.groupName.DataPropertyName = "groupName";
            this.groupName.HeaderText = "Název skupiny";
            this.groupName.Name = "groupName";
            this.groupName.ReadOnly = true;
            // 
            // firstname
            // 
            this.firstname.DataPropertyName = "firstname";
            this.firstname.HeaderText = "Jméno";
            this.firstname.Name = "firstname";
            this.firstname.ReadOnly = true;
            // 
            // surname
            // 
            this.surname.DataPropertyName = "surname";
            this.surname.HeaderText = "Příjmení";
            this.surname.Name = "surname";
            this.surname.ReadOnly = true;
            // 
            // sOPNUMBEDataGridViewTextBoxColumn
            // 
            this.sOPNUMBEDataGridViewTextBoxColumn.DataPropertyName = "SOPNUMBE";
            this.sOPNUMBEDataGridViewTextBoxColumn.HeaderText = "Číslo výrobní zakázky";
            this.sOPNUMBEDataGridViewTextBoxColumn.Name = "sOPNUMBEDataGridViewTextBoxColumn";
            this.sOPNUMBEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tIMESTARTDataGridViewTextBoxColumn
            // 
            this.tIMESTARTDataGridViewTextBoxColumn.DataPropertyName = "TIMESTART";
            dataGridViewCellStyle2.Format = "G";
            dataGridViewCellStyle2.NullValue = "-";
            this.tIMESTARTDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.tIMESTARTDataGridViewTextBoxColumn.HeaderText = "Čas zahájení";
            this.tIMESTARTDataGridViewTextBoxColumn.Name = "tIMESTARTDataGridViewTextBoxColumn";
            this.tIMESTARTDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMESTARTDataGridViewTextBoxColumn.Width = 110;
            // 
            // tIMESTOPDataGridViewTextBoxColumn
            // 
            this.tIMESTOPDataGridViewTextBoxColumn.DataPropertyName = "TIMESTOP";
            dataGridViewCellStyle3.Format = "G";
            dataGridViewCellStyle3.NullValue = null;
            this.tIMESTOPDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.tIMESTOPDataGridViewTextBoxColumn.HeaderText = "Čas ukončení";
            this.tIMESTOPDataGridViewTextBoxColumn.Name = "tIMESTOPDataGridViewTextBoxColumn";
            this.tIMESTOPDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMESTOPDataGridViewTextBoxColumn.Width = 110;
            // 
            // tIMECORSTARTDataGridViewTextBoxColumn
            // 
            this.tIMECORSTARTDataGridViewTextBoxColumn.DataPropertyName = "TIMECORSTART";
            dataGridViewCellStyle4.Format = "G";
            dataGridViewCellStyle4.NullValue = null;
            this.tIMECORSTARTDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.tIMECORSTARTDataGridViewTextBoxColumn.HeaderText = "Čas zahájení korekce";
            this.tIMECORSTARTDataGridViewTextBoxColumn.Name = "tIMECORSTARTDataGridViewTextBoxColumn";
            this.tIMECORSTARTDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMECORSTARTDataGridViewTextBoxColumn.Width = 110;
            // 
            // tIMECORSTOPDataGridViewTextBoxColumn
            // 
            this.tIMECORSTOPDataGridViewTextBoxColumn.DataPropertyName = "TIMECORSTOP";
            dataGridViewCellStyle5.Format = "G";
            dataGridViewCellStyle5.NullValue = null;
            this.tIMECORSTOPDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.tIMECORSTOPDataGridViewTextBoxColumn.HeaderText = "Čas ukončení korekce";
            this.tIMECORSTOPDataGridViewTextBoxColumn.Name = "tIMECORSTOPDataGridViewTextBoxColumn";
            this.tIMECORSTOPDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMECORSTOPDataGridViewTextBoxColumn.Width = 110;
            // 
            // operationName
            // 
            this.operationName.DataPropertyName = "operationName";
            this.operationName.HeaderText = "Název operace";
            this.operationName.Name = "operationName";
            this.operationName.ReadOnly = true;
            // 
            // machineName
            // 
            this.machineName.DataPropertyName = "machineName";
            this.machineName.HeaderText = "Název stroje";
            this.machineName.Name = "machineName";
            this.machineName.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ITEMDESC";
            this.dataGridViewTextBoxColumn1.HeaderText = "Popis zboží";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // countEntriesDataGridViewTextBoxColumn
            // 
            this.countEntriesDataGridViewTextBoxColumn.DataPropertyName = "CountEntries";
            this.countEntriesDataGridViewTextBoxColumn.HeaderText = "Číslo dávky";
            this.countEntriesDataGridViewTextBoxColumn.Name = "countEntriesDataGridViewTextBoxColumn";
            this.countEntriesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "Pol. číslo";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMTYPEDataGridViewTextBoxColumn
            // 
            this.iTEMTYPEDataGridViewTextBoxColumn.DataPropertyName = "ITEMTYPE";
            this.iTEMTYPEDataGridViewTextBoxColumn.HeaderText = "Typ položky";
            this.iTEMTYPEDataGridViewTextBoxColumn.Name = "iTEMTYPEDataGridViewTextBoxColumn";
            this.iTEMTYPEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMMJDataGridViewTextBoxColumn
            // 
            this.iTEMMJDataGridViewTextBoxColumn.DataPropertyName = "ITEMMJ";
            this.iTEMMJDataGridViewTextBoxColumn.HeaderText = "Měrná jednotka";
            this.iTEMMJDataGridViewTextBoxColumn.Name = "iTEMMJDataGridViewTextBoxColumn";
            this.iTEMMJDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // oRDDataGridViewTextBoxColumn
            // 
            this.oRDDataGridViewTextBoxColumn.DataPropertyName = "ORD";
            this.oRDDataGridViewTextBoxColumn.HeaderText = "Pořadí položky";
            this.oRDDataGridViewTextBoxColumn.Name = "oRDDataGridViewTextBoxColumn";
            this.oRDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tIMEMODEDataGridViewTextBoxColumn
            // 
            this.tIMEMODEDataGridViewTextBoxColumn.DataPropertyName = "TIMEMODE";
            this.tIMEMODEDataGridViewTextBoxColumn.HeaderText = "Typ sledování času";
            this.tIMEMODEDataGridViewTextBoxColumn.Name = "tIMEMODEDataGridViewTextBoxColumn";
            this.tIMEMODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tIMEPREPSTARTDataGridViewTextBoxColumn
            // 
            this.tIMEPREPSTARTDataGridViewTextBoxColumn.DataPropertyName = "TIMEPREPSTART";
            dataGridViewCellStyle6.Format = "G";
            dataGridViewCellStyle6.NullValue = null;
            this.tIMEPREPSTARTDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.tIMEPREPSTARTDataGridViewTextBoxColumn.HeaderText = "Start čas přípravy";
            this.tIMEPREPSTARTDataGridViewTextBoxColumn.Name = "tIMEPREPSTARTDataGridViewTextBoxColumn";
            this.tIMEPREPSTARTDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tIMEPREPSTOPDataGridViewTextBoxColumn
            // 
            this.tIMEPREPSTOPDataGridViewTextBoxColumn.DataPropertyName = "TIMEPREPSTOP";
            dataGridViewCellStyle7.Format = "G";
            dataGridViewCellStyle7.NullValue = null;
            this.tIMEPREPSTOPDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.tIMEPREPSTOPDataGridViewTextBoxColumn.HeaderText = "Stop čas přípravy";
            this.tIMEPREPSTOPDataGridViewTextBoxColumn.Name = "tIMEPREPSTOPDataGridViewTextBoxColumn";
            this.tIMEPREPSTOPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tIMEPREPDataGridViewTextBoxColumn
            // 
            this.tIMEPREPDataGridViewTextBoxColumn.DataPropertyName = "TIMEPREP";
            this.tIMEPREPDataGridViewTextBoxColumn.HeaderText = "Přípravný čas v minutách";
            this.tIMEPREPDataGridViewTextBoxColumn.Name = "tIMEPREPDataGridViewTextBoxColumn";
            this.tIMEPREPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tIMEUNITDataGridViewTextBoxColumn
            // 
            this.tIMEUNITDataGridViewTextBoxColumn.DataPropertyName = "TIMEUNIT";
            this.tIMEUNITDataGridViewTextBoxColumn.HeaderText = "Jednotkový čas";
            this.tIMEUNITDataGridViewTextBoxColumn.Name = "tIMEUNITDataGridViewTextBoxColumn";
            this.tIMEUNITDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tIMECORDataGridViewTextBoxColumn
            // 
            this.tIMECORDataGridViewTextBoxColumn.DataPropertyName = "TIMECOR";
            this.tIMECORDataGridViewTextBoxColumn.HeaderText = "Korekce času v minutách";
            this.tIMECORDataGridViewTextBoxColumn.Name = "tIMECORDataGridViewTextBoxColumn";
            this.tIMECORDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tIMECRIDDataGridViewTextBoxColumn
            // 
            this.tIMECRIDDataGridViewTextBoxColumn.DataPropertyName = "TIMECRID";
            this.tIMECRIDDataGridViewTextBoxColumn.HeaderText = "id korekce";
            this.tIMECRIDDataGridViewTextBoxColumn.Name = "tIMECRIDDataGridViewTextBoxColumn";
            this.tIMECRIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id události";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // loginidDataGridViewTextBoxColumn
            // 
            this.loginidDataGridViewTextBoxColumn.DataPropertyName = "loginid";
            this.loginidDataGridViewTextBoxColumn.HeaderText = "id vedoucího směny";
            this.loginidDataGridViewTextBoxColumn.Name = "loginidDataGridViewTextBoxColumn";
            this.loginidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // machineidDataGridViewTextBoxColumn
            // 
            this.machineidDataGridViewTextBoxColumn.DataPropertyName = "machineid";
            this.machineidDataGridViewTextBoxColumn.HeaderText = "id stroje";
            this.machineidDataGridViewTextBoxColumn.Name = "machineidDataGridViewTextBoxColumn";
            this.machineidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // operationidDataGridViewTextBoxColumn
            // 
            this.operationidDataGridViewTextBoxColumn.DataPropertyName = "operationid";
            this.operationidDataGridViewTextBoxColumn.HeaderText = "id operace";
            this.operationidDataGridViewTextBoxColumn.Name = "operationidDataGridViewTextBoxColumn";
            this.operationidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qtyDataGridViewTextBoxColumn
            // 
            this.qtyDataGridViewTextBoxColumn.DataPropertyName = "qty";
            this.qtyDataGridViewTextBoxColumn.HeaderText = "Počet kusů";
            this.qtyDataGridViewTextBoxColumn.Name = "qtyDataGridViewTextBoxColumn";
            this.qtyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qtyRealDataGridViewTextBoxColumn
            // 
            this.qtyRealDataGridViewTextBoxColumn.DataPropertyName = "qtyReal";
            this.qtyRealDataGridViewTextBoxColumn.HeaderText = "Počet kusů sejmuto";
            this.qtyRealDataGridViewTextBoxColumn.Name = "qtyRealDataGridViewTextBoxColumn";
            this.qtyRealDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYPACKDataGridViewTextBoxColumn
            // 
            this.qTYPACKDataGridViewTextBoxColumn.DataPropertyName = "QTYPACK";
            this.qTYPACKDataGridViewTextBoxColumn.HeaderText = "Množství v balení";
            this.qTYPACKDataGridViewTextBoxColumn.Name = "qTYPACKDataGridViewTextBoxColumn";
            this.qTYPACKDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYPACKMJDataGridViewTextBoxColumn
            // 
            this.qTYPACKMJDataGridViewTextBoxColumn.DataPropertyName = "QTYPACKMJ";
            this.qTYPACKMJDataGridViewTextBoxColumn.HeaderText = "Měrná jednotka balení";
            this.qTYPACKMJDataGridViewTextBoxColumn.Name = "qTYPACKMJDataGridViewTextBoxColumn";
            this.qTYPACKMJDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Popis";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // barcodePDataGridViewTextBoxColumn
            // 
            this.barcodePDataGridViewTextBoxColumn.DataPropertyName = "BarcodeP";
            this.barcodePDataGridViewTextBoxColumn.HeaderText = "Čár. kód položky";
            this.barcodePDataGridViewTextBoxColumn.Name = "barcodePDataGridViewTextBoxColumn";
            this.barcodePDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userIDDataGridViewTextBoxColumn
            // 
            this.userIDDataGridViewTextBoxColumn.DataPropertyName = "UserID";
            this.userIDDataGridViewTextBoxColumn.HeaderText = "id pracovníka";
            this.userIDDataGridViewTextBoxColumn.Name = "userIDDataGridViewTextBoxColumn";
            this.userIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // termIDDataGridViewTextBoxColumn
            // 
            this.termIDDataGridViewTextBoxColumn.DataPropertyName = "TermID";
            this.termIDDataGridViewTextBoxColumn.HeaderText = "id terminálu";
            this.termIDDataGridViewTextBoxColumn.Name = "termIDDataGridViewTextBoxColumn";
            this.termIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iSOKDataGridViewTextBoxColumn
            // 
            this.iSOKDataGridViewTextBoxColumn.DataPropertyName = "ISOK";
            dataGridViewCellStyle8.Format = "G";
            dataGridViewCellStyle8.NullValue = null;
            this.iSOKDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.iSOKDataGridViewTextBoxColumn.HeaderText = "Čas převzetí do IS";
            this.iSOKDataGridViewTextBoxColumn.Name = "iSOKDataGridViewTextBoxColumn";
            this.iSOKDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gUIDDataGridViewTextBoxColumn
            // 
            this.gUIDDataGridViewTextBoxColumn.DataPropertyName = "GUID";
            this.gUIDDataGridViewTextBoxColumn.HeaderText = "GUID";
            this.gUIDDataGridViewTextBoxColumn.Name = "gUIDDataGridViewTextBoxColumn";
            this.gUIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sOUBEHGUIDDataGridViewTextBoxColumn
            // 
            this.sOUBEHGUIDDataGridViewTextBoxColumn.DataPropertyName = "SOUBEHGUID";
            this.sOUBEHGUIDDataGridViewTextBoxColumn.HeaderText = "Souběh GUID";
            this.sOUBEHGUIDDataGridViewTextBoxColumn.Name = "sOUBEHGUIDDataGridViewTextBoxColumn";
            this.sOUBEHGUIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // CORRGUID
            // 
            this.CORRGUID.DataPropertyName = "CORRGUID";
            this.CORRGUID.HeaderText = "Korekce GUID";
            this.CORRGUID.Name = "CORRGUID";
            this.CORRGUID.ReadOnly = true;
            // 
            // qtyOldDataGridViewTextBoxColumn
            // 
            this.qtyOldDataGridViewTextBoxColumn.DataPropertyName = "qtyOld";
            this.qtyOldDataGridViewTextBoxColumn.HeaderText = "Původní množství";
            this.qtyOldDataGridViewTextBoxColumn.Name = "qtyOldDataGridViewTextBoxColumn";
            this.qtyOldDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idVSDataGridViewTextBoxColumn
            // 
            this.idVSDataGridViewTextBoxColumn.DataPropertyName = "idVS";
            this.idVSDataGridViewTextBoxColumn.HeaderText = "id vedoucího směny";
            this.idVSDataGridViewTextBoxColumn.Name = "idVSDataGridViewTextBoxColumn";
            this.idVSDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateeditDataGridViewTextBoxColumn
            // 
            this.dateeditDataGridViewTextBoxColumn.DataPropertyName = "dateedit";
            dataGridViewCellStyle9.Format = "G";
            dataGridViewCellStyle9.NullValue = null;
            this.dateeditDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.dateeditDataGridViewTextBoxColumn.HeaderText = "Datum a čas editace";
            this.dateeditDataGridViewTextBoxColumn.Name = "dateeditDataGridViewTextBoxColumn";
            this.dateeditDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // comboBoxUzivatel
            // 
            this.comboBoxUzivatel.FormattingEnabled = true;
            this.comboBoxUzivatel.Location = new System.Drawing.Point(97, 80);
            this.comboBoxUzivatel.Name = "comboBoxUzivatel";
            this.comboBoxUzivatel.Size = new System.Drawing.Size(249, 21);
            this.comboBoxUzivatel.TabIndex = 3;
            this.comboBoxUzivatel.SelectedIndexChanged += new System.EventHandler(this.comboBoxUzivatel_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.výrobaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(919, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // výrobaToolStripMenuItem
            // 
            this.výrobaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upravitToolStripMenuItem,
            this.ukoncitZakazkuKorekciToolStripMenuItem,
            this.schvalitVybraneToolStripMenuItem,
            this.toolStripSeparator1,
            this.konecToolStripMenuItem});
            this.výrobaToolStripMenuItem.Name = "výrobaToolStripMenuItem";
            this.výrobaToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.výrobaToolStripMenuItem.Text = "Přehled výroby";
            // 
            // upravitToolStripMenuItem
            // 
            this.upravitToolStripMenuItem.Name = "upravitToolStripMenuItem";
            this.upravitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.upravitToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.upravitToolStripMenuItem.Text = "Upravit";
            this.upravitToolStripMenuItem.Click += new System.EventHandler(this.buttonUpravit2_Click);
            // 
            // ukoncitZakazkuKorekciToolStripMenuItem
            // 
            this.ukoncitZakazkuKorekciToolStripMenuItem.Name = "ukoncitZakazkuKorekciToolStripMenuItem";
            this.ukoncitZakazkuKorekciToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.ukoncitZakazkuKorekciToolStripMenuItem.Text = "Ukončit zakázku / korekci";
            this.ukoncitZakazkuKorekciToolStripMenuItem.Click += new System.EventHandler(this.buttonUkoncitZakazky_Click);
            // 
            // schvalitVybraneToolStripMenuItem
            // 
            this.schvalitVybraneToolStripMenuItem.Name = "schvalitVybraneToolStripMenuItem";
            this.schvalitVybraneToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.schvalitVybraneToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.schvalitVybraneToolStripMenuItem.Text = "Schválit vybrané";
            this.schvalitVybraneToolStripMenuItem.Click += new System.EventHandler(this.schvalitVybraneToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.konecToolStripMenuItem.Text = "Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // bindingSourceUzivatel
            // 
            this.bindingSourceUzivatel.DataMember = "Logins";
            this.bindingSourceUzivatel.DataSource = this.vyrobaDataSet1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonUkoncitZakazku);
            this.panel2.Controls.Add(this.buttonUpravit2);
            this.panel2.Controls.Add(this.buttonKonec);
            this.panel2.Controls.Add(this.buttonSchvalitVybrane);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(835, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(84, 582);
            this.panel2.TabIndex = 8;
            // 
            // buttonUkoncitZakazku
            // 
            this.buttonUkoncitZakazku.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUkoncitZakazku.Location = new System.Drawing.Point(8, 198);
            this.buttonUkoncitZakazku.Name = "buttonUkoncitZakazku";
            this.buttonUkoncitZakazku.Size = new System.Drawing.Size(73, 63);
            this.buttonUkoncitZakazku.TabIndex = 13;
            this.buttonUkoncitZakazku.Text = "Ukončit zakázku / korekci";
            this.buttonUkoncitZakazku.UseVisualStyleBackColor = true;
            this.buttonUkoncitZakazku.Click += new System.EventHandler(this.buttonUkoncitZakazky_Click);
            // 
            // buttonUpravit2
            // 
            this.buttonUpravit2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpravit2.Location = new System.Drawing.Point(6, 129);
            this.buttonUpravit2.Name = "buttonUpravit2";
            this.buttonUpravit2.Size = new System.Drawing.Size(73, 63);
            this.buttonUpravit2.TabIndex = 12;
            this.buttonUpravit2.Text = "Upravit";
            this.buttonUpravit2.UseVisualStyleBackColor = true;
            this.buttonUpravit2.Click += new System.EventHandler(this.buttonUpravit2_Click);
            // 
            // buttonKonec
            // 
            this.buttonKonec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKonec.Location = new System.Drawing.Point(6, 507);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(73, 63);
            this.buttonKonec.TabIndex = 11;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // buttonSchvalitVybrane
            // 
            this.buttonSchvalitVybrane.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSchvalitVybrane.Location = new System.Drawing.Point(8, 267);
            this.buttonSchvalitVybrane.Name = "buttonSchvalitVybrane";
            this.buttonSchvalitVybrane.Size = new System.Drawing.Size(73, 63);
            this.buttonSchvalitVybrane.TabIndex = 10;
            this.buttonSchvalitVybrane.Text = "Schválit vybrané";
            this.buttonSchvalitVybrane.UseVisualStyleBackColor = true;
            this.buttonSchvalitVybrane.Click += new System.EventHandler(this.schvalitVybraneToolStripMenuItem_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(373, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Stroj:";
            // 
            // comboBoxOperace
            // 
            this.comboBoxOperace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxOperace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBoxOperace.FormattingEnabled = true;
            this.comboBoxOperace.Location = new System.Drawing.Point(410, 54);
            this.comboBoxOperace.Name = "comboBoxOperace";
            this.comboBoxOperace.Size = new System.Drawing.Size(249, 21);
            this.comboBoxOperace.TabIndex = 33;
            // 
            // comboBoxStroj
            // 
            this.comboBoxStroj.FormattingEnabled = true;
            this.comboBoxStroj.Location = new System.Drawing.Point(410, 27);
            this.comboBoxStroj.Name = "comboBoxStroj";
            this.comboBoxStroj.Size = new System.Drawing.Size(249, 21);
            this.comboBoxStroj.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(353, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Operace:";
            // 
            // FormProductionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 582);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormProductionList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Přehled výroby";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProductionList_FormClosing);
            this.Load += new System.EventHandler(this.FormProductionList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormProductionList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceZbozi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.konzolaDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceProduct)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUzivatel)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxVyrobniPrikaz;
        private DataServices.VyrobaDataSet vyrobaDataSet1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxZbozi;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem výrobaToolStripMenuItem;
        private System.Windows.Forms.Button buttonSchvalitVybrane;
        private System.Windows.Forms.ToolStripMenuItem upravitToolStripMenuItem;
        private System.Windows.Forms.BindingSource bindingSourceProduct;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxUzivatel;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.Windows.Forms.BindingSource bindingSourceZbozi;
        private DataServices.KonzolaDataSet konzolaDataSet1;
        private System.Windows.Forms.BindingSource bindingSourceUzivatel;
        private System.Windows.Forms.ToolStripMenuItem schvalitVybraneToolStripMenuItem;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Button buttonOznacitVse;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumDo;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumOd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox checkBoxOdvadeniVse;
        private System.Windows.Forms.CheckBox checkBoxOdvadeniNedokoncene;
        private System.Windows.Forms.CheckBox checkBoxKorekceNedokoncene;
        private System.Windows.Forms.CheckBox checkBoxKorekceVse;
        private System.Windows.Forms.Button buttonUpravit2;
        private System.Windows.Forms.Button buttonUkoncitZakazku;
        private System.Windows.Forms.ToolStripMenuItem ukoncitZakazkuKorekciToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxSkupina;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateeveDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstname;
        private System.Windows.Forms.DataGridViewTextBoxColumn surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPNUMBEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMESTARTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMESTOPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMECORSTARTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMECORSTOPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMTYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMMJDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oRDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMEMODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMEPREPSTARTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMEPREPSTOPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMEPREPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMEUNITDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMECORDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMECRIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loginidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operationidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyRealDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYPACKDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYPACKMJDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodePDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn termIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iSOKDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOUBEHGUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CORRGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyOldDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idVSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateeditDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxOperace;
        private System.Windows.Forms.ComboBox comboBoxStroj;
        private System.Windows.Forms.Label label8;
    }
}