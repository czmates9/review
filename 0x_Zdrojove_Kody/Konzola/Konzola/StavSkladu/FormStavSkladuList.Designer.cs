namespace Konzola.StavSkladu
{
    partial class FormStavSkladuList
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbMnozstvi = new System.Windows.Forms.TextBox();
            this.btnQtySmaller = new System.Windows.Forms.Button();
            this.btnQtyBigger = new System.Windows.Forms.Button();
            this.btnQtyEquals = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimePickerDatumDo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDatumOd = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxMaterialID = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxMaterialSERLTNUM = new System.Windows.Forms.ComboBox();
            this.comboBoxMaterialSKLID = new System.Windows.Forms.ComboBox();
            this.comboBoxMaterialLOCNCODE = new System.Windows.Forms.ComboBox();
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.buttonOznacitVse = new System.Windows.Forms.Button();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.dgStavSkladu = new Zuby.ADGV.AdvancedDataGridView();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOCNCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sERLTNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eXPIRATIONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skl_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsStavSkladu = new System.Windows.Forms.BindingSource(this.components);
            this.dsStavSkladu = new Fask.Interfaces.DataSets.StavSkladu();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStavSkladu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsStavSkladu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsStavSkladu)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu});
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
            this.tsmiKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbMnozstvi);
            this.panel1.Controls.Add(this.btnQtySmaller);
            this.panel1.Controls.Add(this.btnQtyBigger);
            this.panel1.Controls.Add(this.btnQtyEquals);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.dateTimePickerDatumDo);
            this.panel1.Controls.Add(this.dateTimePickerDatumOd);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBoxMaterialID);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBoxMaterialSERLTNUM);
            this.panel1.Controls.Add(this.comboBoxMaterialSKLID);
            this.panel1.Controls.Add(this.comboBoxMaterialLOCNCODE);
            this.panel1.Controls.Add(this.buttonOdznacitVse);
            this.panel1.Controls.Add(this.buttonOznacitVse);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(835, 173);
            this.panel1.TabIndex = 1;
            // 
            // tbMnozstvi
            // 
            this.tbMnozstvi.Location = new System.Drawing.Point(533, 62);
            this.tbMnozstvi.Name = "tbMnozstvi";
            this.tbMnozstvi.Size = new System.Drawing.Size(170, 20);
            this.tbMnozstvi.TabIndex = 9;
            // 
            // btnQtySmaller
            // 
            this.btnQtySmaller.Location = new System.Drawing.Point(455, 62);
            this.btnQtySmaller.Name = "btnQtySmaller";
            this.btnQtySmaller.Size = new System.Drawing.Size(20, 20);
            this.btnQtySmaller.TabIndex = 6;
            this.btnQtySmaller.Text = "<";
            this.btnQtySmaller.UseVisualStyleBackColor = true;
            this.btnQtySmaller.Click += new System.EventHandler(this.btnQtySmaller_Click);
            // 
            // btnQtyBigger
            // 
            this.btnQtyBigger.Location = new System.Drawing.Point(507, 62);
            this.btnQtyBigger.Name = "btnQtyBigger";
            this.btnQtyBigger.Size = new System.Drawing.Size(20, 20);
            this.btnQtyBigger.TabIndex = 8;
            this.btnQtyBigger.Text = ">";
            this.btnQtyBigger.UseVisualStyleBackColor = true;
            this.btnQtyBigger.Click += new System.EventHandler(this.btnQtyBigger_Click);
            // 
            // btnQtyEquals
            // 
            this.btnQtyEquals.Location = new System.Drawing.Point(481, 62);
            this.btnQtyEquals.Name = "btnQtyEquals";
            this.btnQtyEquals.Size = new System.Drawing.Size(20, 20);
            this.btnQtyEquals.TabIndex = 7;
            this.btnQtyEquals.Text = "=";
            this.btnQtyEquals.UseVisualStyleBackColor = true;
            this.btnQtyEquals.Click += new System.EventHandler(this.btnQtyEquals_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(394, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Množství:";
            // 
            // dateTimePickerDatumDo
            // 
            this.dateTimePickerDatumDo.Checked = false;
            this.dateTimePickerDatumDo.CustomFormat = "HH:mm dd.MM.yyyy";
            this.dateTimePickerDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDatumDo.Location = new System.Drawing.Point(120, 93);
            this.dateTimePickerDatumDo.Name = "dateTimePickerDatumDo";
            this.dateTimePickerDatumDo.ShowCheckBox = true;
            this.dateTimePickerDatumDo.Size = new System.Drawing.Size(249, 20);
            this.dateTimePickerDatumDo.TabIndex = 3;
            // 
            // dateTimePickerDatumOd
            // 
            this.dateTimePickerDatumOd.Checked = false;
            this.dateTimePickerDatumOd.CustomFormat = "HH:mm dd.MM.yyyy";
            this.dateTimePickerDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDatumOd.Location = new System.Drawing.Point(120, 67);
            this.dateTimePickerDatumOd.Name = "dateTimePickerDatumOd";
            this.dateTimePickerDatumOd.ShowCheckBox = true;
            this.dateTimePickerDatumOd.Size = new System.Drawing.Size(249, 20);
            this.dateTimePickerDatumOd.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Datum expirace do:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Datum expirace od:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Název mat.:";
            // 
            // comboBoxMaterialID
            // 
            this.comboBoxMaterialID.FormattingEnabled = true;
            this.comboBoxMaterialID.Location = new System.Drawing.Point(120, 11);
            this.comboBoxMaterialID.Name = "comboBoxMaterialID";
            this.comboBoxMaterialID.Size = new System.Drawing.Size(249, 21);
            this.comboBoxMaterialID.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Šarže:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(409, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Sklad:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(402, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Lokace:";
            // 
            // comboBoxMaterialSERLTNUM
            // 
            this.comboBoxMaterialSERLTNUM.FormattingEnabled = true;
            this.comboBoxMaterialSERLTNUM.Location = new System.Drawing.Point(120, 38);
            this.comboBoxMaterialSERLTNUM.Name = "comboBoxMaterialSERLTNUM";
            this.comboBoxMaterialSERLTNUM.Size = new System.Drawing.Size(249, 21);
            this.comboBoxMaterialSERLTNUM.TabIndex = 1;
            // 
            // comboBoxMaterialSKLID
            // 
            this.comboBoxMaterialSKLID.FormattingEnabled = true;
            this.comboBoxMaterialSKLID.Location = new System.Drawing.Point(454, 35);
            this.comboBoxMaterialSKLID.Name = "comboBoxMaterialSKLID";
            this.comboBoxMaterialSKLID.Size = new System.Drawing.Size(249, 21);
            this.comboBoxMaterialSKLID.TabIndex = 5;
            // 
            // comboBoxMaterialLOCNCODE
            // 
            this.comboBoxMaterialLOCNCODE.FormattingEnabled = true;
            this.comboBoxMaterialLOCNCODE.Location = new System.Drawing.Point(454, 8);
            this.comboBoxMaterialLOCNCODE.Name = "comboBoxMaterialLOCNCODE";
            this.comboBoxMaterialLOCNCODE.Size = new System.Drawing.Size(249, 21);
            this.comboBoxMaterialLOCNCODE.TabIndex = 4;
            // 
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdznacitVse.Location = new System.Drawing.Point(746, 136);
            this.buttonOdznacitVse.Name = "buttonOdznacitVse";
            this.buttonOdznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOdznacitVse.TabIndex = 12;
            this.buttonOdznacitVse.Text = "Odznačit vše";
            this.buttonOdznacitVse.UseVisualStyleBackColor = true;
            this.buttonOdznacitVse.Click += new System.EventHandler(this.buttonOdznacitVse_Click);
            // 
            // buttonOznacitVse
            // 
            this.buttonOznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOznacitVse.Location = new System.Drawing.Point(657, 136);
            this.buttonOznacitVse.Name = "buttonOznacitVse";
            this.buttonOznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOznacitVse.TabIndex = 11;
            this.buttonOznacitVse.Text = "Označit vše";
            this.buttonOznacitVse.UseVisualStyleBackColor = true;
            this.buttonOznacitVse.Click += new System.EventHandler(this.buttonOznacitVse_Click);
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(756, 67);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 10;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // dgStavSkladu
            // 
            this.dgStavSkladu.AllowUserToAddRows = false;
            this.dgStavSkladu.AllowUserToDeleteRows = false;
            this.dgStavSkladu.AllowUserToOrderColumns = true;
            this.dgStavSkladu.AllowUserToResizeRows = false;
            this.dgStavSkladu.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgStavSkladu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgStavSkladu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgStavSkladu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.ITEMCODE,
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.qTYSHPPDDataGridViewTextBoxColumn,
            this.lOCNCODEDataGridViewTextBoxColumn,
            this.sERLTNUMDataGridViewTextBoxColumn,
            this.eXPIRATIONDataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.skl_desc});
            this.dgStavSkladu.DataSource = this.bsStavSkladu;
            this.dgStavSkladu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgStavSkladu.EnableHeadersVisualStyles = false;
            this.dgStavSkladu.Location = new System.Drawing.Point(0, 224);
            this.dgStavSkladu.Name = "dgStavSkladu";
            this.dgStavSkladu.ReadOnly = true;
            this.dgStavSkladu.RowHeadersVisible = false;
            this.dgStavSkladu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgStavSkladu.Size = new System.Drawing.Size(835, 358);
            this.dgStavSkladu.TabIndex = 0;
            this.dgStavSkladu.TabStop = false;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "ID materiálu";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ITEMCODE
            // 
            this.ITEMCODE.DataPropertyName = "ITEMCODE";
            this.ITEMCODE.HeaderText = "Vlastní ID materiálu";
            this.ITEMCODE.Name = "ITEMCODE";
            this.ITEMCODE.ReadOnly = true;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Název materiálu";
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
            // lOCNCODEDataGridViewTextBoxColumn
            // 
            this.lOCNCODEDataGridViewTextBoxColumn.DataPropertyName = "LOCNCODE";
            this.lOCNCODEDataGridViewTextBoxColumn.HeaderText = "Lokace";
            this.lOCNCODEDataGridViewTextBoxColumn.Name = "lOCNCODEDataGridViewTextBoxColumn";
            this.lOCNCODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sERLTNUMDataGridViewTextBoxColumn
            // 
            this.sERLTNUMDataGridViewTextBoxColumn.DataPropertyName = "SERLTNUM";
            this.sERLTNUMDataGridViewTextBoxColumn.HeaderText = "Šarže";
            this.sERLTNUMDataGridViewTextBoxColumn.Name = "sERLTNUMDataGridViewTextBoxColumn";
            this.sERLTNUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // eXPIRATIONDataGridViewTextBoxColumn
            // 
            this.eXPIRATIONDataGridViewTextBoxColumn.DataPropertyName = "EXPIRATION";
            this.eXPIRATIONDataGridViewTextBoxColumn.HeaderText = "Expirace";
            this.eXPIRATIONDataGridViewTextBoxColumn.Name = "eXPIRATIONDataGridViewTextBoxColumn";
            this.eXPIRATIONDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sKLIDDataGridViewTextBoxColumn
            // 
            this.sKLIDDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.HeaderText = "ID skladu";
            this.sKLIDDataGridViewTextBoxColumn.Name = "sKLIDDataGridViewTextBoxColumn";
            this.sKLIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // skl_desc
            // 
            this.skl_desc.DataPropertyName = "skl_desc";
            this.skl_desc.HeaderText = "Název skladu";
            this.skl_desc.Name = "skl_desc";
            this.skl_desc.ReadOnly = true;
            // 
            // bsStavSkladu
            // 
            this.bsStavSkladu.DataMember = "get_os_ms";
            this.bsStavSkladu.DataSource = this.dsStavSkladu;
            // 
            // dsStavSkladu
            // 
            this.dsStavSkladu.DataSetName = "StavSkladu";
            this.dsStavSkladu.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 197);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(835, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 3;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // FormStavSkladuList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 582);
            this.Controls.Add(this.dgStavSkladu);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormStavSkladuList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Aktuální stav skladu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStavSkladuList_FormClosing);
            this.Load += new System.EventHandler(this.FormStavSkladuList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormStavSkladuList_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStavSkladu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsStavSkladu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsStavSkladu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumDo;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumOd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxMaterialID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxMaterialLOCNCODE;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Button buttonOznacitVse;
        private System.Windows.Forms.Button buttonVyhledat;
        private Zuby.ADGV.AdvancedDataGridView dgStavSkladu;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxMaterialSKLID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxMaterialSERLTNUM;
        private Fask.Interfaces.DataSets.StavSkladu dsStavSkladu;
        private System.Windows.Forms.BindingSource bsStavSkladu;
        private System.Windows.Forms.TextBox tbMnozstvi;
        private System.Windows.Forms.Button btnQtySmaller;
        private System.Windows.Forms.Button btnQtyBigger;
        private System.Windows.Forms.Button btnQtyEquals;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSHPPDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOCNCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sERLTNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eXPIRATIONDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn skl_desc;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;


    }
}