namespace Konzola.Ciselniky
{
    partial class FormLokaceTypySelect
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
            this.dgSkladLokace = new Zuby.ADGV.AdvancedDataGridView();
            this.tYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSRECEIVEDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.iSDEFAULTDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.iSNORMALDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bsSkladLokace = new System.Windows.Forms.BindingSource(this.components);
            this.dsSkladLokace = new Fask.Interfaces.DataSets.SkladLokace();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAktualizovat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiVybrat = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgSkladLokace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSkladLokace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSkladLokace)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgSkladLokace
            // 
            this.dgSkladLokace.AllowUserToAddRows = false;
            this.dgSkladLokace.AllowUserToDeleteRows = false;
            this.dgSkladLokace.AllowUserToOrderColumns = true;
            this.dgSkladLokace.AllowUserToResizeRows = false;
            this.dgSkladLokace.AutoGenerateColumns = false;
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
            this.dgSkladLokace.Location = new System.Drawing.Point(0, 51);
            this.dgSkladLokace.Name = "dgSkladLokace";
            this.dgSkladLokace.ReadOnly = true;
            this.dgSkladLokace.RowHeadersVisible = false;
            this.dgSkladLokace.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSkladLokace.Size = new System.Drawing.Size(498, 417);
            this.dgSkladLokace.TabIndex = 1;
            this.dgSkladLokace.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgSkladLokace.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dgSkladLokace.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // tYPEDataGridViewTextBoxColumn
            // 
            this.tYPEDataGridViewTextBoxColumn.DataPropertyName = "TYPE";
            dataGridViewCellStyle1.NullValue = "-";
            this.tYPEDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.tYPEDataGridViewTextBoxColumn.HeaderText = "Typ lokace";
            this.tYPEDataGridViewTextBoxColumn.Name = "tYPEDataGridViewTextBoxColumn";
            this.tYPEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            dataGridViewCellStyle2.NullValue = "-";
            this.descriptionDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Označení";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iSRECEIVEDataGridViewCheckBoxColumn
            // 
            this.iSRECEIVEDataGridViewCheckBoxColumn.DataPropertyName = "IS_RECEIVE";
            this.iSRECEIVEDataGridViewCheckBoxColumn.HeaderText = "Příjmová lokace";
            this.iSRECEIVEDataGridViewCheckBoxColumn.Name = "iSRECEIVEDataGridViewCheckBoxColumn";
            this.iSRECEIVEDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // iSDEFAULTDataGridViewCheckBoxColumn
            // 
            this.iSDEFAULTDataGridViewCheckBoxColumn.DataPropertyName = "IS_DEFAULT";
            this.iSDEFAULTDataGridViewCheckBoxColumn.HeaderText = "Výchozí lokace";
            this.iSDEFAULTDataGridViewCheckBoxColumn.Name = "iSDEFAULTDataGridViewCheckBoxColumn";
            this.iSDEFAULTDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // iSNORMALDataGridViewCheckBoxColumn
            // 
            this.iSNORMALDataGridViewCheckBoxColumn.DataPropertyName = "IS_NORMAL";
            this.iSNORMALDataGridViewCheckBoxColumn.HeaderText = "Běžná lokace";
            this.iSNORMALDataGridViewCheckBoxColumn.Name = "iSNORMALDataGridViewCheckBoxColumn";
            this.iSNORMALDataGridViewCheckBoxColumn.ReadOnly = true;
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
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(498, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 468);
            this.panelButtons.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgSkladLokace);
            this.panel1.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.panel1.Controls.Add(this.menuStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 468);
            this.panel1.TabIndex = 1;
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
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(498, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 3;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(498, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // tsmiMenu
            // 
            this.tsmiMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiKonec,
            this.toolStripSeparator2,
            this.tsmiAktualizovat,
            this.toolStripSeparator1,
            this.tsmiVybrat});
            this.tsmiMenu.Name = "tsmiMenu";
            this.tsmiMenu.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenu.Text = "Menu";
            // 
            // tsmiKonec
            // 
            this.tsmiKonec.Name = "tsmiKonec";
            this.tsmiKonec.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonec.Size = new System.Drawing.Size(158, 22);
            this.tsmiKonec.Text = "Konec";
            this.tsmiKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(155, 6);
            // 
            // tsmiAktualizovat
            // 
            this.tsmiAktualizovat.Name = "tsmiAktualizovat";
            this.tsmiAktualizovat.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tsmiAktualizovat.Size = new System.Drawing.Size(158, 22);
            this.tsmiAktualizovat.Text = "Aktualizovat";
            this.tsmiAktualizovat.Click += new System.EventHandler(this.obnovitToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // tsmiVybrat
            // 
            this.tsmiVybrat.Name = "tsmiVybrat";
            this.tsmiVybrat.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiVybrat.Size = new System.Drawing.Size(158, 22);
            this.tsmiVybrat.Text = "Vybrat";
            this.tsmiVybrat.Click += new System.EventHandler(this.vybratToolStripMenuItem_Click);
            // 
            // FormLokaceTypySelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 468);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormLokaceTypySelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výběr typu lokace";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLokaceTypySelect_FormClosing);
            this.Load += new System.EventHandler(this.FormLokaceTypySelect_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormLokaceTypySelect_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgSkladLokace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSkladLokace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSkladLokace)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgSkladLokace;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource bsSkladLokace;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiVybrat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAktualizovat;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private Fask.Interfaces.DataSets.SkladLokace dsSkladLokace;
        private System.Windows.Forms.DataGridViewTextBoxColumn tYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn iSRECEIVEDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn iSDEFAULTDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn iSNORMALDataGridViewCheckBoxColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
    }
}