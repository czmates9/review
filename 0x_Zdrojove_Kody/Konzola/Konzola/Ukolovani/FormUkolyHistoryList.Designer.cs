namespace Konzola.Ukolovani
{
    partial class FormUkolyHistoryList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExceOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxStav = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePickerDatumDo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDatumOd = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxPriorita = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxUkol = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxUzivatel = new System.Windows.Forms.ComboBox();
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.buttonOznacitVse = new System.Windows.Forms.Button();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.dgUkolovani = new Zuby.ADGV.AdvancedDataGridView();
            this.iDHISTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukolIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserFirstname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserSecondname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UkolName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UkolDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UkolPriority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateChangedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UkolDateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UkolDateFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UkolDateTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userIDChangedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateNotifyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateFinishedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsUkolovani = new System.Windows.Forms.BindingSource(this.components);
            this.dsUkolovani = new Fask.Interfaces.DataSets.Ukolovani();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUkolovani)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUkolovani)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsUkolovani)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiExport});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(919, 24);
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
            // 
            // tsmiExport
            // 
            this.tsmiExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator6,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExceOznacene,
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
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxStav);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dateTimePickerDatumDo);
            this.panel1.Controls.Add(this.dateTimePickerDatumOd);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBoxPriorita);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBoxUkol);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBoxUzivatel);
            this.panel1.Controls.Add(this.buttonOdznacitVse);
            this.panel1.Controls.Add(this.buttonOznacitVse);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(835, 179);
            this.panel1.TabIndex = 1;
            // 
            // comboBoxStav
            // 
            this.comboBoxStav.FormattingEnabled = true;
            this.comboBoxStav.Location = new System.Drawing.Point(397, 6);
            this.comboBoxStav.Name = "comboBoxStav";
            this.comboBoxStav.Size = new System.Drawing.Size(249, 21);
            this.comboBoxStav.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(330, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Stav úkolu:";
            // 
            // dateTimePickerDatumDo
            // 
            this.dateTimePickerDatumDo.Checked = false;
            this.dateTimePickerDatumDo.CustomFormat = "HH:mm dd.MM.yyyy";
            this.dateTimePickerDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDatumDo.Location = new System.Drawing.Point(66, 113);
            this.dateTimePickerDatumDo.Name = "dateTimePickerDatumDo";
            this.dateTimePickerDatumDo.ShowCheckBox = true;
            this.dateTimePickerDatumDo.Size = new System.Drawing.Size(249, 20);
            this.dateTimePickerDatumDo.TabIndex = 5;
            // 
            // dateTimePickerDatumOd
            // 
            this.dateTimePickerDatumOd.Checked = false;
            this.dateTimePickerDatumOd.CustomFormat = "HH:mm dd.MM.yyyy";
            this.dateTimePickerDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDatumOd.Location = new System.Drawing.Point(66, 87);
            this.dateTimePickerDatumOd.Name = "dateTimePickerDatumOd";
            this.dateTimePickerDatumOd.ShowCheckBox = true;
            this.dateTimePickerDatumOd.Size = new System.Drawing.Size(249, 20);
            this.dateTimePickerDatumOd.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Datum do:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Datum od:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Priorita:";
            // 
            // comboBoxPriorita
            // 
            this.comboBoxPriorita.FormattingEnabled = true;
            this.comboBoxPriorita.Location = new System.Drawing.Point(66, 60);
            this.comboBoxPriorita.Name = "comboBoxPriorita";
            this.comboBoxPriorita.Size = new System.Drawing.Size(249, 21);
            this.comboBoxPriorita.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Úkol:";
            // 
            // comboBoxUkol
            // 
            this.comboBoxUkol.FormattingEnabled = true;
            this.comboBoxUkol.Location = new System.Drawing.Point(66, 33);
            this.comboBoxUkol.Name = "comboBoxUkol";
            this.comboBoxUkol.Size = new System.Drawing.Size(249, 21);
            this.comboBoxUkol.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Uživatel:";
            // 
            // comboBoxUzivatel
            // 
            this.comboBoxUzivatel.FormattingEnabled = true;
            this.comboBoxUzivatel.Location = new System.Drawing.Point(66, 6);
            this.comboBoxUzivatel.Name = "comboBoxUzivatel";
            this.comboBoxUzivatel.Size = new System.Drawing.Size(249, 21);
            this.comboBoxUzivatel.TabIndex = 1;
            // 
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdznacitVse.Location = new System.Drawing.Point(746, 146);
            this.buttonOdznacitVse.Name = "buttonOdznacitVse";
            this.buttonOdznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOdznacitVse.TabIndex = 9;
            this.buttonOdznacitVse.Text = "Odznačit vše";
            this.buttonOdznacitVse.UseVisualStyleBackColor = true;
            this.buttonOdznacitVse.Click += new System.EventHandler(this.buttonOdznacitVse_Click);
            // 
            // buttonOznacitVse
            // 
            this.buttonOznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOznacitVse.Location = new System.Drawing.Point(657, 146);
            this.buttonOznacitVse.Name = "buttonOznacitVse";
            this.buttonOznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOznacitVse.TabIndex = 8;
            this.buttonOznacitVse.Text = "Označit vše";
            this.buttonOznacitVse.UseVisualStyleBackColor = true;
            this.buttonOznacitVse.Click += new System.EventHandler(this.buttonOznacitVse_Click);
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(756, 77);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 7;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // dgUkolovani
            // 
            this.dgUkolovani.AllowUserToAddRows = false;
            this.dgUkolovani.AllowUserToDeleteRows = false;
            this.dgUkolovani.AllowUserToOrderColumns = true;
            this.dgUkolovani.AllowUserToResizeRows = false;
            this.dgUkolovani.AutoGenerateColumns = false;
            this.dgUkolovani.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUkolovani.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDHISTDataGridViewTextBoxColumn,
            this.iDDataGridViewTextBoxColumn,
            this.ukolIDDataGridViewTextBoxColumn,
            this.userIDDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.UserFirstname,
            this.UserSecondname,
            this.UserLogin,
            this.UkolName,
            this.UkolDescription,
            this.UkolPriority,
            this.dateChangedDataGridViewTextBoxColumn,
            this.UkolDateCreated,
            this.UkolDateFrom,
            this.UkolDateTo,
            this.userIDChangedDataGridViewTextBoxColumn,
            this.noteDataGridViewTextBoxColumn,
            this.dateNotifyDataGridViewTextBoxColumn,
            this.dateFinishedDataGridViewTextBoxColumn});
            this.dgUkolovani.DataSource = this.bsUkolovani;
            this.dgUkolovani.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgUkolovani.EnableHeadersVisualStyles = false;
            this.dgUkolovani.Location = new System.Drawing.Point(0, 230);
            this.dgUkolovani.Name = "dgUkolovani";
            this.dgUkolovani.ReadOnly = true;
            this.dgUkolovani.RowHeadersVisible = false;
            this.dgUkolovani.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUkolovani.Size = new System.Drawing.Size(835, 352);
            this.dgUkolovani.TabIndex = 0;
            this.dgUkolovani.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // iDHISTDataGridViewTextBoxColumn
            // 
            this.iDHISTDataGridViewTextBoxColumn.DataPropertyName = "ID_HIST";
            this.iDHISTDataGridViewTextBoxColumn.HeaderText = "ID historie";
            this.iDHISTDataGridViewTextBoxColumn.Name = "iDHISTDataGridViewTextBoxColumn";
            this.iDHISTDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ukolIDDataGridViewTextBoxColumn
            // 
            this.ukolIDDataGridViewTextBoxColumn.DataPropertyName = "UkolID";
            this.ukolIDDataGridViewTextBoxColumn.HeaderText = "ID úkol";
            this.ukolIDDataGridViewTextBoxColumn.Name = "ukolIDDataGridViewTextBoxColumn";
            this.ukolIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userIDDataGridViewTextBoxColumn
            // 
            this.userIDDataGridViewTextBoxColumn.DataPropertyName = "UserID";
            this.userIDDataGridViewTextBoxColumn.HeaderText = "ID uživatel";
            this.userIDDataGridViewTextBoxColumn.Name = "userIDDataGridViewTextBoxColumn";
            this.userIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.HeaderText = "Stav";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // UserFirstname
            // 
            this.UserFirstname.DataPropertyName = "UserFirstname";
            this.UserFirstname.HeaderText = "Jméno";
            this.UserFirstname.Name = "UserFirstname";
            this.UserFirstname.ReadOnly = true;
            // 
            // UserSecondname
            // 
            this.UserSecondname.DataPropertyName = "UserSecondname";
            this.UserSecondname.HeaderText = "Příjmení";
            this.UserSecondname.Name = "UserSecondname";
            this.UserSecondname.ReadOnly = true;
            // 
            // UserLogin
            // 
            this.UserLogin.DataPropertyName = "UserLogin";
            this.UserLogin.HeaderText = "Login";
            this.UserLogin.Name = "UserLogin";
            this.UserLogin.ReadOnly = true;
            // 
            // UkolName
            // 
            this.UkolName.DataPropertyName = "UkolName";
            this.UkolName.HeaderText = "Název úkolu";
            this.UkolName.Name = "UkolName";
            this.UkolName.ReadOnly = true;
            // 
            // UkolDescription
            // 
            this.UkolDescription.DataPropertyName = "UkolDescription";
            this.UkolDescription.HeaderText = "Popis úkolu";
            this.UkolDescription.Name = "UkolDescription";
            this.UkolDescription.ReadOnly = true;
            // 
            // UkolPriority
            // 
            this.UkolPriority.DataPropertyName = "UkolPriority";
            this.UkolPriority.HeaderText = "Priorita úkolu";
            this.UkolPriority.Name = "UkolPriority";
            this.UkolPriority.ReadOnly = true;
            // 
            // dateChangedDataGridViewTextBoxColumn
            // 
            this.dateChangedDataGridViewTextBoxColumn.DataPropertyName = "DateChanged";
            dataGridViewCellStyle4.Format = "G";
            dataGridViewCellStyle4.NullValue = null;
            this.dateChangedDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.dateChangedDataGridViewTextBoxColumn.HeaderText = "Datum změny";
            this.dateChangedDataGridViewTextBoxColumn.Name = "dateChangedDataGridViewTextBoxColumn";
            this.dateChangedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // UkolDateCreated
            // 
            this.UkolDateCreated.DataPropertyName = "UkolDateCreated";
            this.UkolDateCreated.HeaderText = "Datum vytvoření úkolu";
            this.UkolDateCreated.Name = "UkolDateCreated";
            this.UkolDateCreated.ReadOnly = true;
            // 
            // UkolDateFrom
            // 
            this.UkolDateFrom.DataPropertyName = "UkolDateFrom";
            this.UkolDateFrom.HeaderText = "Datum nabytí platn. úkolu";
            this.UkolDateFrom.Name = "UkolDateFrom";
            this.UkolDateFrom.ReadOnly = true;
            // 
            // UkolDateTo
            // 
            this.UkolDateTo.DataPropertyName = "UkolDateTo";
            this.UkolDateTo.HeaderText = "Datum pozbytí platn. úkolu";
            this.UkolDateTo.Name = "UkolDateTo";
            this.UkolDateTo.ReadOnly = true;
            // 
            // userIDChangedDataGridViewTextBoxColumn
            // 
            this.userIDChangedDataGridViewTextBoxColumn.DataPropertyName = "UserIDChanged";
            this.userIDChangedDataGridViewTextBoxColumn.HeaderText = "ID uživatel změna";
            this.userIDChangedDataGridViewTextBoxColumn.Name = "userIDChangedDataGridViewTextBoxColumn";
            this.userIDChangedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.HeaderText = "Poznámka";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateNotifyDataGridViewTextBoxColumn
            // 
            this.dateNotifyDataGridViewTextBoxColumn.DataPropertyName = "DateNotify";
            dataGridViewCellStyle5.Format = "G";
            dataGridViewCellStyle5.NullValue = null;
            this.dateNotifyDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.dateNotifyDataGridViewTextBoxColumn.HeaderText = "Čas upozornění";
            this.dateNotifyDataGridViewTextBoxColumn.Name = "dateNotifyDataGridViewTextBoxColumn";
            this.dateNotifyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateFinishedDataGridViewTextBoxColumn
            // 
            this.dateFinishedDataGridViewTextBoxColumn.DataPropertyName = "DateFinished";
            dataGridViewCellStyle6.Format = "G";
            dataGridViewCellStyle6.NullValue = null;
            this.dateFinishedDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.dateFinishedDataGridViewTextBoxColumn.HeaderText = "Čas dokončení";
            this.dateFinishedDataGridViewTextBoxColumn.Name = "dateFinishedDataGridViewTextBoxColumn";
            this.dateFinishedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsUkolovani
            // 
            this.bsUkolovani.DataMember = "CZ_UKOL_UZIV_HIST";
            this.bsUkolovani.DataSource = this.dsUkolovani;
            // 
            // dsUkolovani
            // 
            this.dsUkolovani.DataSetName = "UkolovaniDataset";
            this.dsUkolovani.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panelButtons
            // 
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(835, 24);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 558);
            this.panelButtons.TabIndex = 2;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 203);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(835, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 3;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // FormUkolyHistoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 582);
            this.Controls.Add(this.dgUkolovani);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormUkolyHistoryList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Přehled úkolů";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUkolyHistoryList_FormClosing);
            this.Load += new System.EventHandler(this.FormUkolyHistoryList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUkolyHistoryList_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUkolovani)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUkolovani)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsUkolovani)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.Panel panel1;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private Zuby.ADGV.AdvancedDataGridView dgUkolovani;
        private System.Windows.Forms.BindingSource bsUkolovani;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Button buttonOznacitVse;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxUzivatel;
        private Fask.Interfaces.DataSets.Ukolovani dsUkolovani;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxUkol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxPriorita;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumDo;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumOd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxStav;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDHISTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukolIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserFirstname;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserSecondname;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn UkolName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UkolDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn UkolPriority;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateChangedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UkolDateCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn UkolDateFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn UkolDateTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn userIDChangedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateNotifyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateFinishedDataGridViewTextBoxColumn;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExport;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoExceOznacene;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
    }
}