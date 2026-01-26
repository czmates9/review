namespace Konzola.Servis
{
    partial class FormReportSestava
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
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExceOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTisk = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTisknoutVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTisknoutOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.bwLoadData = new System.ComponentModel.BackgroundWorker();
            this.dgSestava = new Zuby.ADGV.AdvancedDataGridView();
            this.bsSestava = new System.Windows.Forms.BindingSource(this.components);
            this.dsSestava = new System.Data.DataSet();
            this.buttonOznacitVse = new System.Windows.Forms.Button();
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePickerDatumOd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDatumDo = new System.Windows.Forms.DateTimePicker();
            this.comboBoxODB_ID = new System.Windows.Forms.ComboBox();
            this.cbOkruh = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rb_poroce = new System.Windows.Forms.RadioButton();
            this.rb_pomesici = new System.Windows.Forms.RadioButton();
            this.rb_pomereni = new System.Windows.Forms.RadioButton();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSestava)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSestava)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSestava)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiExport,
            this.tsmiTisk});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(934, 24);
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
            this.tsmiKonec.Click += new System.EventHandler(this.buttonKonec_Click);
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
            // tsmiTisk
            // 
            this.tsmiTisk.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTisknoutVse,
            this.tsmiTisknoutOznacene});
            this.tsmiTisk.Name = "tsmiTisk";
            this.tsmiTisk.Size = new System.Drawing.Size(39, 20);
            this.tsmiTisk.Text = "Tisk";
            // 
            // tsmiTisknoutVse
            // 
            this.tsmiTisknoutVse.Name = "tsmiTisknoutVse";
            this.tsmiTisknoutVse.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsmiTisknoutVse.Size = new System.Drawing.Size(248, 22);
            this.tsmiTisknoutVse.Text = "Report tisknout vše";
            this.tsmiTisknoutVse.Click += new System.EventHandler(this.tsmiTisknoutVse_Click);
            // 
            // tsmiTisknoutOznacene
            // 
            this.tsmiTisknoutOznacene.Name = "tsmiTisknoutOznacene";
            this.tsmiTisknoutOznacene.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiTisknoutOznacene.Size = new System.Drawing.Size(248, 22);
            this.tsmiTisknoutOznacene.Text = "Report tisknout označené";
            this.tsmiTisknoutOznacene.Click += new System.EventHandler(this.tsmiTisknoutOznacene_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(850, 24);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 538);
            this.panelButtons.TabIndex = 2;
            // 
            // bwLoadData
            // 
            this.bwLoadData.WorkerSupportsCancellation = true;
            this.bwLoadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadData_DoWork);
            this.bwLoadData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadData_RunWorkerCompleted);
            // 
            // dgSestava
            // 
            this.dgSestava.AllowUserToAddRows = false;
            this.dgSestava.AllowUserToDeleteRows = false;
            this.dgSestava.AllowUserToOrderColumns = true;
            this.dgSestava.AllowUserToResizeRows = false;
            this.dgSestava.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSestava.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSestava.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSestava.DataSource = this.bsSestava;
            this.dgSestava.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSestava.EnableHeadersVisualStyles = false;
            this.dgSestava.FilterAndSortEnabled = true;
            this.dgSestava.Location = new System.Drawing.Point(0, 201);
            this.dgSestava.Name = "dgSestava";
            this.dgSestava.ReadOnly = true;
            this.dgSestava.RowHeadersVisible = false;
            this.dgSestava.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSestava.Size = new System.Drawing.Size(850, 361);
            this.dgSestava.TabIndex = 0;
            this.dgSestava.TabStop = false;
            this.dgSestava.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // bsSestava
            // 
            this.bsSestava.DataSource = this.dsSestava;
            this.bsSestava.Position = 0;
            // 
            // dsSestava
            // 
            this.dsSestava.DataSetName = "NewDataSet";
            // 
            // buttonOznacitVse
            // 
            this.buttonOznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOznacitVse.Location = new System.Drawing.Point(672, 114);
            this.buttonOznacitVse.Name = "buttonOznacitVse";
            this.buttonOznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOznacitVse.TabIndex = 15;
            this.buttonOznacitVse.Text = "Označit vše";
            this.buttonOznacitVse.UseVisualStyleBackColor = true;
            this.buttonOznacitVse.Click += new System.EventHandler(this.buttonOznacitVse_Click);
            // 
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdznacitVse.Location = new System.Drawing.Point(761, 114);
            this.buttonOdznacitVse.Name = "buttonOdznacitVse";
            this.buttonOdznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOdznacitVse.TabIndex = 16;
            this.buttonOdznacitVse.Text = "Odznačit vše";
            this.buttonOdznacitVse.UseVisualStyleBackColor = true;
            this.buttonOdznacitVse.Click += new System.EventHandler(this.buttonOdznacitVse_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Datum od:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(271, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Datum do:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePickerDatumOd
            // 
            this.dateTimePickerDatumOd.Checked = false;
            this.dateTimePickerDatumOd.CustomFormat = "HH:mm dd.MM.yyyy";
            this.dateTimePickerDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDatumOd.Location = new System.Drawing.Point(80, 17);
            this.dateTimePickerDatumOd.Name = "dateTimePickerDatumOd";
            this.dateTimePickerDatumOd.ShowCheckBox = true;
            this.dateTimePickerDatumOd.Size = new System.Drawing.Size(171, 20);
            this.dateTimePickerDatumOd.TabIndex = 6;
            // 
            // dateTimePickerDatumDo
            // 
            this.dateTimePickerDatumDo.Checked = false;
            this.dateTimePickerDatumDo.CustomFormat = "HH:mm dd.MM.yyyy";
            this.dateTimePickerDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDatumDo.Location = new System.Drawing.Point(330, 17);
            this.dateTimePickerDatumDo.Name = "dateTimePickerDatumDo";
            this.dateTimePickerDatumDo.ShowCheckBox = true;
            this.dateTimePickerDatumDo.Size = new System.Drawing.Size(171, 20);
            this.dateTimePickerDatumDo.TabIndex = 7;
            // 
            // comboBoxODB_ID
            // 
            this.comboBoxODB_ID.FormattingEnabled = true;
            this.comboBoxODB_ID.Location = new System.Drawing.Point(80, 47);
            this.comboBoxODB_ID.Name = "comboBoxODB_ID";
            this.comboBoxODB_ID.Size = new System.Drawing.Size(171, 21);
            this.comboBoxODB_ID.TabIndex = 4;
            this.comboBoxODB_ID.SelectedIndexChanged += new System.EventHandler(this.comboBoxODB_ID_SelectedIndexChanged);
            // 
            // cbOkruh
            // 
            this.cbOkruh.FormattingEnabled = true;
            this.cbOkruh.Location = new System.Drawing.Point(330, 47);
            this.cbOkruh.Name = "cbOkruh";
            this.cbOkruh.Size = new System.Drawing.Size(171, 21);
            this.cbOkruh.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Odběratel:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(288, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Okruh:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(771, 45);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 14;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(411, 328);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 30;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rb_poroce);
            this.panel1.Controls.Add(this.rb_pomesici);
            this.panel1.Controls.Add(this.rb_pomereni);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cbOkruh);
            this.panel1.Controls.Add(this.comboBoxODB_ID);
            this.panel1.Controls.Add(this.dateTimePickerDatumDo);
            this.panel1.Controls.Add(this.dateTimePickerDatumOd);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.buttonOdznacitVse);
            this.panel1.Controls.Add(this.buttonOznacitVse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(850, 150);
            this.panel1.TabIndex = 1;
            // 
            // rb_poroce
            // 
            this.rb_poroce.AutoSize = true;
            this.rb_poroce.Location = new System.Drawing.Point(549, 66);
            this.rb_poroce.Name = "rb_poroce";
            this.rb_poroce.Size = new System.Drawing.Size(67, 17);
            this.rb_poroce.TabIndex = 31;
            this.rb_poroce.Text = "Po Roce";
            this.rb_poroce.UseVisualStyleBackColor = true;
            // 
            // rb_pomesici
            // 
            this.rb_pomesici.AutoSize = true;
            this.rb_pomesici.Location = new System.Drawing.Point(549, 43);
            this.rb_pomesici.Name = "rb_pomesici";
            this.rb_pomesici.Size = new System.Drawing.Size(73, 17);
            this.rb_pomesici.TabIndex = 31;
            this.rb_pomesici.Text = "Po Měsíci";
            this.rb_pomesici.UseVisualStyleBackColor = true;
            // 
            // rb_pomereni
            // 
            this.rb_pomereni.AutoSize = true;
            this.rb_pomereni.Checked = true;
            this.rb_pomereni.Location = new System.Drawing.Point(549, 20);
            this.rb_pomereni.Name = "rb_pomereni";
            this.rb_pomereni.Size = new System.Drawing.Size(76, 17);
            this.rb_pomereni.TabIndex = 31;
            this.rb_pomereni.TabStop = true;
            this.rb_pomereni.Text = "Po Měření";
            this.rb_pomereni.UseVisualStyleBackColor = true;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 174);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(850, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 31;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // FormReportSestava
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 562);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dgSestava);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormReportSestava";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report Sestava";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormReportSestava_FormClosing);
            this.Load += new System.EventHandler(this.FormReportSestava_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormReportSestava_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSestava)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSestava)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSestava)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        //private System.Windows.Forms.DataGridViewTextBoxColumn cinnostOznaceniDataGridViewTextBoxColumn;
        private System.ComponentModel.BackgroundWorker bwLoadData;
        private System.Windows.Forms.ToolStripMenuItem tsmiTisk;
        private System.Windows.Forms.ToolStripMenuItem tsmiTisknoutOznacene;
        private System.Windows.Forms.ToolStripMenuItem tsmiTisknoutVse;
        private Zuby.ADGV.AdvancedDataGridView dgSestava;
        private System.Windows.Forms.Button buttonOznacitVse;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumOd;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumDo;
        private System.Windows.Forms.ComboBox comboBoxODB_ID;
        private System.Windows.Forms.ComboBox cbOkruh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonVyhledat;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource bsSestava;
        private System.Data.DataSet dsSestava;
        private System.Windows.Forms.RadioButton rb_poroce;
        private System.Windows.Forms.RadioButton rb_pomesici;
        private System.Windows.Forms.RadioButton rb_pomereni;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExport;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoExceOznacene;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        protected System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;


    }
}