namespace Konzola.Vyroba.Wizard.VyrobniPrikazList_VPP
{
    partial class Form_01_Vyber_Vyrobku
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_01_Vyber_Vyrobku));
            this.dgVyrobkyProPridani = new Zuby.ADGV.AdvancedDataGridView();
            this.vyrobitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMEMODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dsVyrobkyProPridani = new Konzola.Vyroba.Wizard.VyrobniPrikazList_VPP.dsVyrobkyProPridani();
            this.tIMEPREPDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.tIMEUNITDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.iTEMCODEDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.iTEMNMBRDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.mJDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.VNDITNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ_CarKod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOCNCODEDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.qTYDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.qTYPACKDataGridViewTextBoxColumn = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.bsVyrobkyProPridani = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmi_Zrusit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_PotvrditVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.fASK_ZASOBYTableAdapter = new Konzola.Vyroba.Wizard.VyrobniPrikazList_VPP.dsVyrobkyProPridaniTableAdapters.FASK_ZASOBYTableAdapter();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripLabel();
            this.tstb_CarkodFilter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel_VyrobitUpdate = new System.Windows.Forms.ToolStripLabel();
            this.tstb_Mnozstvi = new System.Windows.Forms.ToolStripTextBox();
            this.tsb_VyrobitUpdate = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Error = new System.Windows.Forms.ToolStripStatusLabel();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgVyrobkyProPridani)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVyrobkyProPridani)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVyrobkyProPridani)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgVyrobkyProPridani
            // 
            this.dgVyrobkyProPridani.AllowUserToAddRows = false;
            this.dgVyrobkyProPridani.AllowUserToDeleteRows = false;
            this.dgVyrobkyProPridani.AllowUserToOrderColumns = true;
            this.dgVyrobkyProPridani.AutoGenerateColumns = false;
            this.dgVyrobkyProPridani.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVyrobkyProPridani.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.vyrobitDataGridViewTextBoxColumn,
            this.tIMEMODEDataGridViewTextBoxColumn,
            this.tIMEPREPDataGridViewTextBoxColumn,
            this.tIMEUNITDataGridViewTextBoxColumn,
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.iTEMCODEDataGridViewTextBoxColumn,
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.mJDataGridViewTextBoxColumn,
            this.vNDITNUMDataGridViewTextBoxColumn,
            this.VNDITNUM,
            this.CZ_CarKod,
            this.lOCNCODEDataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.qTYDataGridViewTextBoxColumn,
            this.qTYPACKDataGridViewTextBoxColumn});
            this.dgVyrobkyProPridani.DataSource = this.bsVyrobkyProPridani;
            this.dgVyrobkyProPridani.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVyrobkyProPridani.EnableHeadersVisualStyles = false;
            this.dgVyrobkyProPridani.FilterAndSortEnabled = true;
            this.dgVyrobkyProPridani.Location = new System.Drawing.Point(0, 76);
            this.dgVyrobkyProPridani.Name = "dgVyrobkyProPridani";
            this.dgVyrobkyProPridani.Size = new System.Drawing.Size(783, 358);
            this.dgVyrobkyProPridani.TabIndex = 0;
            // 
            // vyrobitDataGridViewTextBoxColumn
            // 
            this.vyrobitDataGridViewTextBoxColumn.DataPropertyName = "Vyrobit";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.Format = ".#######";
            dataGridViewCellStyle1.NullValue = null;
            this.vyrobitDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.vyrobitDataGridViewTextBoxColumn.Frozen = true;
            this.vyrobitDataGridViewTextBoxColumn.HeaderText = "Vyrobit";
            this.vyrobitDataGridViewTextBoxColumn.Name = "vyrobitDataGridViewTextBoxColumn";
            // 
            // tIMEMODEDataGridViewTextBoxColumn
            // 
            this.tIMEMODEDataGridViewTextBoxColumn.DataPropertyName = "TIMEMODE";
            this.tIMEMODEDataGridViewTextBoxColumn.DataSource = this.dsVyrobkyProPridani;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tIMEMODEDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.tIMEMODEDataGridViewTextBoxColumn.DisplayMember = "TypSledovaniCasu.Nazev";
            this.tIMEMODEDataGridViewTextBoxColumn.DisplayStyleForCurrentCellOnly = true;
            this.tIMEMODEDataGridViewTextBoxColumn.Frozen = true;
            this.tIMEMODEDataGridViewTextBoxColumn.HeaderText = "Mód operace";
            this.tIMEMODEDataGridViewTextBoxColumn.Name = "tIMEMODEDataGridViewTextBoxColumn";
            this.tIMEMODEDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMEMODEDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tIMEMODEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.tIMEMODEDataGridViewTextBoxColumn.ValueMember = "TypSledovaniCasu.ID";
            // 
            // dsVyrobkyProPridani
            // 
            this.dsVyrobkyProPridani.DataSetName = "dsVyrobkyProPridani";
            this.dsVyrobkyProPridani.Locale = new System.Globalization.CultureInfo("");
            this.dsVyrobkyProPridani.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tIMEPREPDataGridViewTextBoxColumn
            // 
            this.tIMEPREPDataGridViewTextBoxColumn.DataPropertyName = "TIMEPREP";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tIMEPREPDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.tIMEPREPDataGridViewTextBoxColumn.Frozen = true;
            this.tIMEPREPDataGridViewTextBoxColumn.HeaderText = "Přípravný čas";
            this.tIMEPREPDataGridViewTextBoxColumn.Name = "tIMEPREPDataGridViewTextBoxColumn";
            this.tIMEPREPDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMEPREPDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // tIMEUNITDataGridViewTextBoxColumn
            // 
            this.tIMEUNITDataGridViewTextBoxColumn.DataPropertyName = "TIMEUNIT";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tIMEUNITDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.tIMEUNITDataGridViewTextBoxColumn.Frozen = true;
            this.tIMEUNITDataGridViewTextBoxColumn.HeaderText = "Jednotkový čas";
            this.tIMEUNITDataGridViewTextBoxColumn.Name = "tIMEUNITDataGridViewTextBoxColumn";
            this.tIMEUNITDataGridViewTextBoxColumn.ReadOnly = true;
            this.tIMEUNITDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.Frozen = true;
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Název";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMDESCDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // iTEMCODEDataGridViewTextBoxColumn
            // 
            this.iTEMCODEDataGridViewTextBoxColumn.DataPropertyName = "ITEMCODE";
            this.iTEMCODEDataGridViewTextBoxColumn.HeaderText = "Označení";
            this.iTEMCODEDataGridViewTextBoxColumn.Name = "iTEMCODEDataGridViewTextBoxColumn";
            this.iTEMCODEDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMCODEDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "Pol. číslo";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMNMBRDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // mJDataGridViewTextBoxColumn
            // 
            this.mJDataGridViewTextBoxColumn.DataPropertyName = "MJ";
            this.mJDataGridViewTextBoxColumn.HeaderText = "MJ";
            this.mJDataGridViewTextBoxColumn.Name = "mJDataGridViewTextBoxColumn";
            this.mJDataGridViewTextBoxColumn.ReadOnly = true;
            this.mJDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // vNDITNUMDataGridViewTextBoxColumn
            // 
            this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "BarcodeP";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.vNDITNUMDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "Čár. kód operace";
            this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
            this.vNDITNUMDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // VNDITNUM
            // 
            this.VNDITNUM.DataPropertyName = "VNDITNUM";
            this.VNDITNUM.HeaderText = "Čár. kód zboží";
            this.VNDITNUM.Name = "VNDITNUM";
            this.VNDITNUM.ReadOnly = true;
            // 
            // CZ_CarKod
            // 
            this.CZ_CarKod.DataPropertyName = "CZ_CarKod";
            this.CZ_CarKod.HeaderText = "Čár. kód zboží vlastní";
            this.CZ_CarKod.Name = "CZ_CarKod";
            this.CZ_CarKod.ReadOnly = true;
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
            this.qTYPACKDataGridViewTextBoxColumn.HeaderText = "Balení";
            this.qTYPACKDataGridViewTextBoxColumn.Name = "qTYPACKDataGridViewTextBoxColumn";
            this.qTYPACKDataGridViewTextBoxColumn.ReadOnly = true;
            this.qTYPACKDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // bsVyrobkyProPridani
            // 
            this.bsVyrobkyProPridani.DataMember = "FASK_ZASOBY_KONZOLA";
            this.bsVyrobkyProPridani.DataSource = this.dsVyrobkyProPridani;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Zrusit,
            this.tsmi_PotvrditVyber});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(783, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmi_Zrusit
            // 
            this.tsmi_Zrusit.Name = "tsmi_Zrusit";
            this.tsmi_Zrusit.Size = new System.Drawing.Size(49, 20);
            this.tsmi_Zrusit.Text = "Zrušit";
            this.tsmi_Zrusit.Click += new System.EventHandler(this.tsmi_Zrusit_Click);
            // 
            // tsmi_PotvrditVyber
            // 
            this.tsmi_PotvrditVyber.Name = "tsmi_PotvrditVyber";
            this.tsmi_PotvrditVyber.Size = new System.Drawing.Size(93, 20);
            this.tsmi_PotvrditVyber.Text = "Potvrdit výběr";
            this.tsmi_PotvrditVyber.Click += new System.EventHandler(this.tsmi_PotvrditVyber_Click);
            // 
            // fASK_ZASOBYTableAdapter
            // 
            this.fASK_ZASOBYTableAdapter.ClearBeforeFill = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tstb_CarkodFilter,
            this.toolStripLabel_VyrobitUpdate,
            this.tstb_Mnozstvi,
            this.tsb_VyrobitUpdate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(783, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(70, 22);
            this.toolStripButton1.Text = "Čárový kód:";
            // 
            // tstb_CarkodFilter
            // 
            this.tstb_CarkodFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstb_CarkodFilter.Name = "tstb_CarkodFilter";
            this.tstb_CarkodFilter.Size = new System.Drawing.Size(150, 25);
            this.tstb_CarkodFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tstb_CarkodFilter_KeyDown);
            this.tstb_CarkodFilter.TextChanged += new System.EventHandler(this.tstb_CarkodFilter_TextChanged);
            // 
            // toolStripLabel_VyrobitUpdate
            // 
            this.toolStripLabel_VyrobitUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel_VyrobitUpdate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel_VyrobitUpdate.Image")));
            this.toolStripLabel_VyrobitUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLabel_VyrobitUpdate.Name = "toolStripLabel_VyrobitUpdate";
            this.toolStripLabel_VyrobitUpdate.Size = new System.Drawing.Size(92, 22);
            this.toolStripLabel_VyrobitUpdate.Text = "Přidat množství:";
            // 
            // tstb_Mnozstvi
            // 
            this.tstb_Mnozstvi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstb_Mnozstvi.Name = "tstb_Mnozstvi";
            this.tstb_Mnozstvi.Size = new System.Drawing.Size(35, 25);
            this.tstb_Mnozstvi.Text = "1";
            this.tstb_Mnozstvi.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tstb_Mnozstvi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tstb_Mnozstvi_KeyDown);
            // 
            // tsb_VyrobitUpdate
            // 
            this.tsb_VyrobitUpdate.BackColor = System.Drawing.Color.LightGreen;
            this.tsb_VyrobitUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_VyrobitUpdate.Name = "tsb_VyrobitUpdate";
            this.tsb_VyrobitUpdate.Size = new System.Drawing.Size(42, 22);
            this.tsb_VyrobitUpdate.Text = "Přidat";
            this.tsb_VyrobitUpdate.ToolTipText = "Navýší množství k výrobě";
            this.tsb_VyrobitUpdate.Click += new System.EventHandler(this.tsb_VyrobitUpdate_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Error});
            this.statusStrip1.Location = new System.Drawing.Point(0, 434);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(783, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Error
            // 
            this.toolStripStatusLabel_Error.Name = "toolStripStatusLabel_Error";
            this.toolStripStatusLabel_Error.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel_Error.Text = "_____";
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 49);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(783, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 4;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // Form_01_Vyber_Vyrobku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 456);
            this.Controls.Add(this.dgVyrobkyProPridani);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_01_Vyber_Vyrobku";
            this.Text = "Výběr výrobků";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_01_Vyber_Vyrobku_FormClosing);
            this.Load += new System.EventHandler(this.Form_01_Vyber_Vyrobku_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_01_Vyber_Vyrobku_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgVyrobkyProPridani)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVyrobkyProPridani)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVyrobkyProPridani)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgVyrobkyProPridani;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private dsVyrobkyProPridani dsVyrobkyProPridani;
        private System.Windows.Forms.BindingSource bsVyrobkyProPridani;
        private dsVyrobkyProPridaniTableAdapters.FASK_ZASOBYTableAdapter fASK_ZASOBYTableAdapter;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Zrusit;
        private System.Windows.Forms.ToolStripMenuItem tsmi_PotvrditVyber;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox tstb_CarkodFilter;
        private System.Windows.Forms.ToolStripTextBox tstb_Mnozstvi;
        private System.Windows.Forms.ToolStripLabel toolStripButton1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Error;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_VyrobitUpdate;
        private System.Windows.Forms.ToolStripButton tsb_VyrobitUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn vyrobitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn tIMEMODEDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn tIMEPREPDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn tIMEUNITDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn iTEMCODEDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn mJDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn vNDITNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNDITNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_CarKod;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn lOCNCODEDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn qTYDataGridViewTextBoxColumn;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn qTYPACKDataGridViewTextBoxColumn;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
    }
}