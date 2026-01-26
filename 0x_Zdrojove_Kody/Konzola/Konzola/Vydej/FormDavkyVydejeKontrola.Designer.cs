namespace Konzola.Vydej
{
    partial class FormDavkyVydejeKontrola
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.dg_Vydej = new Zuby.ADGV.AdvancedDataGridView();
            this.ITEMNMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKz_IDS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKz_EAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPNUMBE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY_OBJ_Pohoda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zADATDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rEZJADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rEZOSTATNIDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zBUDEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STAV_SKLAD_PRED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sTAVSKLADDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_Vydej = new System.Windows.Forms.BindingSource(this.components);
            this.ds_Vydej = new Fask.Interfaces.DataSets.Vydej();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAktualizovat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExcelOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOdstranit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravitMnozstvi = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Vydej)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Vydej)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Vydej)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(852, 24);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(94, 436);
            this.panelButtons.TabIndex = 0;
            // 
            // dg_Vydej
            // 
            this.dg_Vydej.AllowUserToAddRows = false;
            this.dg_Vydej.AllowUserToDeleteRows = false;
            this.dg_Vydej.AllowUserToOrderColumns = true;
            this.dg_Vydej.AllowUserToResizeRows = false;
            this.dg_Vydej.AutoGenerateColumns = false;
            this.dg_Vydej.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Vydej.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ITEMNMBR,
            this.SKz_IDS,
            this.SKz_EAN,
            this.SOPNUMBE,
            this.ORD,
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.QTY_OBJ_Pohoda,
            this.qTYSHPPDDataGridViewTextBoxColumn,
            this.zADATDataGridViewTextBoxColumn,
            this.rEZJADataGridViewTextBoxColumn,
            this.rEZOSTATNIDataGridViewTextBoxColumn,
            this.zBUDEDataGridViewTextBoxColumn,
            this.STAV_SKLAD_PRED,
            this.sTAVSKLADDataGridViewTextBoxColumn});
            this.dg_Vydej.DataSource = this.bs_Vydej;
            this.dg_Vydej.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Vydej.EnableHeadersVisualStyles = false;
            this.dg_Vydej.Location = new System.Drawing.Point(0, 51);
            this.dg_Vydej.MultiSelect = false;
            this.dg_Vydej.Name = "dg_Vydej";
            this.dg_Vydej.ReadOnly = true;
            this.dg_Vydej.RowTemplate.DefaultCellStyle.Format = "N2";
            this.dg_Vydej.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dg_Vydej.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Vydej.Size = new System.Drawing.Size(852, 409);
            this.dg_Vydej.TabIndex = 0;
            this.dg_Vydej.TabStop = false;
            this.dg_Vydej.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_Vydej_CellFormatting);
            this.dg_Vydej.SelectionChanged += new System.EventHandler(this.dg_Vydej_SelectionChanged);
            // 
            // ITEMNMBR
            // 
            this.ITEMNMBR.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR.HeaderText = "ID položky";
            this.ITEMNMBR.Name = "ITEMNMBR";
            this.ITEMNMBR.ReadOnly = true;
            // 
            // SKz_IDS
            // 
            this.SKz_IDS.DataPropertyName = "SKz_IDS";
            this.SKz_IDS.HeaderText = "Kód";
            this.SKz_IDS.Name = "SKz_IDS";
            this.SKz_IDS.ReadOnly = true;
            // 
            // SKz_EAN
            // 
            this.SKz_EAN.DataPropertyName = "SKz_EAN";
            this.SKz_EAN.HeaderText = "Vlastní čárový kód";
            this.SKz_EAN.Name = "SKz_EAN";
            this.SKz_EAN.ReadOnly = true;
            // 
            // SOPNUMBE
            // 
            this.SOPNUMBE.DataPropertyName = "SOPNUMBE";
            this.SOPNUMBE.HeaderText = "Číslo objednávky";
            this.SOPNUMBE.Name = "SOPNUMBE";
            this.SOPNUMBE.ReadOnly = true;
            // 
            // ORD
            // 
            this.ORD.DataPropertyName = "ORD";
            this.ORD.HeaderText = "Pořadí položky";
            this.ORD.Name = "ORD";
            this.ORD.ReadOnly = true;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Název položky";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // QTY_OBJ_Pohoda
            // 
            this.QTY_OBJ_Pohoda.DataPropertyName = "QTY_OBJ_Pohoda";
            this.QTY_OBJ_Pohoda.HeaderText = "Počet objednáno";
            this.QTY_OBJ_Pohoda.Name = "QTY_OBJ_Pohoda";
            this.QTY_OBJ_Pohoda.ReadOnly = true;
            // 
            // qTYSHPPDDataGridViewTextBoxColumn
            // 
            this.qTYSHPPDDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPD";
            dataGridViewCellStyle13.Format = "N2";
            this.qTYSHPPDDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.qTYSHPPDDataGridViewTextBoxColumn.HeaderText = "Počet předloha";
            this.qTYSHPPDDataGridViewTextBoxColumn.Name = "qTYSHPPDDataGridViewTextBoxColumn";
            this.qTYSHPPDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // zADATDataGridViewTextBoxColumn
            // 
            this.zADATDataGridViewTextBoxColumn.DataPropertyName = "ZADAT";
            dataGridViewCellStyle14.Format = "N2";
            this.zADATDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle14;
            this.zADATDataGridViewTextBoxColumn.HeaderText = "Zadat";
            this.zADATDataGridViewTextBoxColumn.Name = "zADATDataGridViewTextBoxColumn";
            this.zADATDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rEZJADataGridViewTextBoxColumn
            // 
            this.rEZJADataGridViewTextBoxColumn.DataPropertyName = "REZ_JA";
            dataGridViewCellStyle15.Format = "N2";
            this.rEZJADataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle15;
            this.rEZJADataGridViewTextBoxColumn.HeaderText = "Rezervováno obj.";
            this.rEZJADataGridViewTextBoxColumn.Name = "rEZJADataGridViewTextBoxColumn";
            this.rEZJADataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rEZOSTATNIDataGridViewTextBoxColumn
            // 
            this.rEZOSTATNIDataGridViewTextBoxColumn.DataPropertyName = "REZ_OSTATNI";
            dataGridViewCellStyle16.Format = "N2";
            this.rEZOSTATNIDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle16;
            this.rEZOSTATNIDataGridViewTextBoxColumn.HeaderText = "Rezervováno ostatní";
            this.rEZOSTATNIDataGridViewTextBoxColumn.Name = "rEZOSTATNIDataGridViewTextBoxColumn";
            this.rEZOSTATNIDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // zBUDEDataGridViewTextBoxColumn
            // 
            this.zBUDEDataGridViewTextBoxColumn.DataPropertyName = "ZBUDE";
            dataGridViewCellStyle17.Format = "N2";
            this.zBUDEDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle17;
            this.zBUDEDataGridViewTextBoxColumn.HeaderText = "Zbude volných";
            this.zBUDEDataGridViewTextBoxColumn.Name = "zBUDEDataGridViewTextBoxColumn";
            this.zBUDEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // STAV_SKLAD_PRED
            // 
            this.STAV_SKLAD_PRED.DataPropertyName = "STAV_SKLAD_PRED";
            this.STAV_SKLAD_PRED.HeaderText = "Stav před pohybem";
            this.STAV_SKLAD_PRED.Name = "STAV_SKLAD_PRED";
            this.STAV_SKLAD_PRED.ReadOnly = true;
            // 
            // sTAVSKLADDataGridViewTextBoxColumn
            // 
            this.sTAVSKLADDataGridViewTextBoxColumn.DataPropertyName = "STAV_SKLAD";
            dataGridViewCellStyle18.Format = "N2";
            this.sTAVSKLADDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle18;
            this.sTAVSKLADDataGridViewTextBoxColumn.HeaderText = "Stav po pohybu";
            this.sTAVSKLADDataGridViewTextBoxColumn.Name = "sTAVSKLADDataGridViewTextBoxColumn";
            this.sTAVSKLADDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bs_Vydej
            // 
            this.bs_Vydej.DataMember = "VydejKontrola";
            this.bs_Vydej.DataSource = this.ds_Vydej;
            // 
            // ds_Vydej
            // 
            this.ds_Vydej.DataSetName = "Vydej";
            this.ds_Vydej.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiExport,
            this.tsmiAkce});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(946, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiMenu
            // 
            this.tsmiMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiKonec,
            this.tsmiAktualizovat});
            this.tsmiMenu.Name = "tsmiMenu";
            this.tsmiMenu.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenu.Text = "Menu";
            // 
            // tsmiKonec
            // 
            this.tsmiKonec.Name = "tsmiKonec";
            this.tsmiKonec.Size = new System.Drawing.Size(139, 22);
            this.tsmiKonec.Text = "Konec";
            this.tsmiKonec.Click += new System.EventHandler(this.tsmiKonec_Click);
            // 
            // tsmiAktualizovat
            // 
            this.tsmiAktualizovat.Name = "tsmiAktualizovat";
            this.tsmiAktualizovat.Size = new System.Drawing.Size(139, 22);
            this.tsmiAktualizovat.Text = "Aktualizovat";
            this.tsmiAktualizovat.Click += new System.EventHandler(this.tsmiAktualizovat_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator1,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExcelOznacene,
            this.toolStripSeparator6,
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
            // 
            // tsmiExportDoCSVOznacene
            // 
            this.tsmiExportDoCSVOznacene.Name = "tsmiExportDoCSVOznacene";
            this.tsmiExportDoCSVOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoCSVOznacene.Text = "Export do CSV označené";
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
            // 
            // tsmiExportDoExcelOznacene
            // 
            this.tsmiExportDoExcelOznacene.Name = "tsmiExportDoExcelOznacene";
            this.tsmiExportDoExcelOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoExcelOznacene.Text = "Export do Excel označené";
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
            // 
            // tsmiExportDoXMLOznacene
            // 
            this.tsmiExportDoXMLOznacene.Name = "tsmiExportDoXMLOznacene";
            this.tsmiExportDoXMLOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoXMLOznacene.Text = "Export do XML označené";
            // 
            // tsmiAkce
            // 
            this.tsmiAkce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOdstranit,
            this.tsmiUpravitMnozstvi});
            this.tsmiAkce.Name = "tsmiAkce";
            this.tsmiAkce.Size = new System.Drawing.Size(45, 20);
            this.tsmiAkce.Text = "Akce";
            // 
            // tsmiOdstranit
            // 
            this.tsmiOdstranit.Name = "tsmiOdstranit";
            this.tsmiOdstranit.Size = new System.Drawing.Size(163, 22);
            this.tsmiOdstranit.Text = "Odstranit";
            this.tsmiOdstranit.Click += new System.EventHandler(this.tsmiOdstranit_Click);
            // 
            // tsmiUpravitMnozstvi
            // 
            this.tsmiUpravitMnozstvi.Name = "tsmiUpravitMnozstvi";
            this.tsmiUpravitMnozstvi.Size = new System.Drawing.Size(163, 22);
            this.tsmiUpravitMnozstvi.Text = "Upravit množství";
            this.tsmiUpravitMnozstvi.Click += new System.EventHandler(this.tsmiUpravitMnozstvi_Click);
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 24);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(852, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 2;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // FormDavkyVydejeKontrola
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 460);
            this.Controls.Add(this.dg_Vydej);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormDavkyVydejeKontrola";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormDavkyVydejeKontrola";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDavkyVydejeKontrola_FormClosing);
            this.Load += new System.EventHandler(this.FormDavkyVydejeKontrola_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Vydej)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Vydej)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Vydej)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.BindingSource bs_Vydej;
        private Fask.Interfaces.DataSets.Vydej ds_Vydej;
        private Zuby.ADGV.AdvancedDataGridView dg_Vydej;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKz_IDS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKz_EAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPNUMBE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORD;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY_OBJ_Pohoda;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSHPPDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zADATDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rEZJADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rEZOSTATNIDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zBUDEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn STAV_SKLAD_PRED;
        private System.Windows.Forms.DataGridViewTextBoxColumn sTAVSKLADDataGridViewTextBoxColumn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private System.Windows.Forms.ToolStripMenuItem tsmiAktualizovat;
        private System.Windows.Forms.ToolStripMenuItem tsmiAkce;
        private System.Windows.Forms.ToolStripMenuItem tsmiOdstranit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravitMnozstvi;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
    }
}