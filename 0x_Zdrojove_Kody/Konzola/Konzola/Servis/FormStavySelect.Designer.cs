namespace Konzola.Servis
{
    partial class FormStavySelect
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgServis = new Zuby.ADGV.AdvancedDataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oznaceniDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDCinnostDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsServis = new System.Windows.Forms.BindingSource(this.components);
            this.dsServis = new Fask.Interfaces.DataSets.Servis();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVybrat = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgServis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsServis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsServis)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgServis
            // 
            this.dgServis.AllowUserToAddRows = false;
            this.dgServis.AllowUserToDeleteRows = false;
            this.dgServis.AllowUserToOrderColumns = true;
            this.dgServis.AllowUserToResizeRows = false;
            this.dgServis.AutoGenerateColumns = false;
            this.dgServis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgServis.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.oznaceniDataGridViewTextBoxColumn,
            this.barcodeDataGridViewTextBoxColumn,
            this.iDCinnostDataGridViewTextBoxColumn});
            this.dgServis.DataSource = this.bsServis;
            this.dgServis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgServis.EnableHeadersVisualStyles = false;
            this.dgServis.Location = new System.Drawing.Point(0, 152);
            this.dgServis.Name = "dgServis";
            this.dgServis.ReadOnly = true;
            this.dgServis.RowHeadersVisible = false;
            this.dgServis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgServis.Size = new System.Drawing.Size(498, 316);
            this.dgServis.TabIndex = 1;
            this.dgServis.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgServis.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dgServis.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            dataGridViewCellStyle5.NullValue = "-";
            this.iDDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // oznaceniDataGridViewTextBoxColumn
            // 
            this.oznaceniDataGridViewTextBoxColumn.DataPropertyName = "Oznaceni";
            dataGridViewCellStyle6.NullValue = "-";
            this.oznaceniDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.oznaceniDataGridViewTextBoxColumn.HeaderText = "Označení";
            this.oznaceniDataGridViewTextBoxColumn.Name = "oznaceniDataGridViewTextBoxColumn";
            this.oznaceniDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // barcodeDataGridViewTextBoxColumn
            // 
            this.barcodeDataGridViewTextBoxColumn.DataPropertyName = "Barcode";
            dataGridViewCellStyle7.NullValue = "-";
            this.barcodeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.barcodeDataGridViewTextBoxColumn.HeaderText = "Čár. kód";
            this.barcodeDataGridViewTextBoxColumn.Name = "barcodeDataGridViewTextBoxColumn";
            this.barcodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDCinnostDataGridViewTextBoxColumn
            // 
            this.iDCinnostDataGridViewTextBoxColumn.DataPropertyName = "IDCinnost";
            dataGridViewCellStyle8.NullValue = "-";
            this.iDCinnostDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.iDCinnostDataGridViewTextBoxColumn.HeaderText = "ID činnosti";
            this.iDCinnostDataGridViewTextBoxColumn.Name = "iDCinnostDataGridViewTextBoxColumn";
            this.iDCinnostDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsServis
            // 
            this.bsServis.DataMember = "CZMST_Servis_Stav";
            this.bsServis.DataSource = this.dsServis;
            // 
            // dsServis
            // 
            this.dsServis.DataSetName = "Servis";
            this.dsServis.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panelButtons
            // 
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(498, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 468);
            this.panelButtons.TabIndex = 2;
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(407, 22);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(73, 63);
            this.buttonVyhledat.TabIndex = 3;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgServis);
            this.panel1.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.panel1.Controls.Add(this.panel2);
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
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 125);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(498, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 4;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonVyhledat);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(498, 101);
            this.panel2.TabIndex = 3;
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
            this.tsmiVybrat});
            this.tsmiMenu.Name = "tsmiMenu";
            this.tsmiMenu.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenu.Text = "Menu";
            // 
            // tsmiKonec
            // 
            this.tsmiKonec.Name = "tsmiKonec";
            this.tsmiKonec.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonec.Size = new System.Drawing.Size(175, 22);
            this.tsmiKonec.Text = "Konec";
            this.tsmiKonec.Click += new System.EventHandler(this.tsmiKonec_Click);
            // 
            // tsmiVybrat
            // 
            this.tsmiVybrat.Name = "tsmiVybrat";
            this.tsmiVybrat.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiVybrat.Size = new System.Drawing.Size(175, 22);
            this.tsmiVybrat.Text = "Vybrat stav";
            this.tsmiVybrat.Click += new System.EventHandler(this.tsmiVybrat_Click);
            // 
            // FormStavySelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 468);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormStavySelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výběr stavu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStavySelect_FormClosing);
            this.Load += new System.EventHandler(this.FormStavySelect_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormStavySelect_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgServis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsServis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsServis)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgServis;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource bsServis;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiVybrat;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private Fask.Interfaces.DataSets.Servis dsServis;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oznaceniDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDCinnostDataGridViewTextBoxColumn;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.Panel panel2;
    }
}