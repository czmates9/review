namespace Konzola.Vyroba
{
    partial class FormProductionSourcesList_SelectCountEntries
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
            this.dgPS = new Zuby.ADGV.AdvancedDataGridView();
            this.bsPS = new System.Windows.Forms.BindingSource(this.components);
            this.dsPS = new Fask.Interfaces.DataSets.Vyroba();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.countEntriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPNUMBEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPS)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgPS
            // 
            this.dgPS.AllowUserToAddRows = false;
            this.dgPS.AllowUserToDeleteRows = false;
            this.dgPS.AllowUserToOrderColumns = true;
            this.dgPS.AllowUserToResizeRows = false;
            this.dgPS.AutoGenerateColumns = false;
            this.dgPS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.countEntriesDataGridViewTextBoxColumn,
            this.sOPNUMBEDataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.sOPDESCDataGridViewTextBoxColumn});
            this.dgPS.DataSource = this.bsPS;
            this.dgPS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPS.Location = new System.Drawing.Point(0, 27);
            this.dgPS.MultiSelect = false;
            this.dgPS.Name = "dgPS";
            this.dgPS.ReadOnly = true;
            this.dgPS.RowHeadersVisible = false;
            this.dgPS.RowTemplate.Height = 24;
            this.dgPS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPS.Size = new System.Drawing.Size(485, 308);
            this.dgPS.TabIndex = 0;
            this.dgPS.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // bsPS
            // 
            this.bsPS.DataMember = "Production_SourcesImport";
            this.bsPS.DataSource = this.dsPS;
            // 
            // dsPS
            // 
            this.dsPS.DataSetName = "VyrobaDataSet";
            this.dsPS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(369, 33);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Vybrat";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 335);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(485, 78);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dávka :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(75, 33);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(255, 20);
            this.textBox1.TabIndex = 2;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 0);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(485, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 3;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // countEntriesDataGridViewTextBoxColumn
            // 
            this.countEntriesDataGridViewTextBoxColumn.DataPropertyName = "CountEntries";
            this.countEntriesDataGridViewTextBoxColumn.HeaderText = "Pol. Číslo";
            this.countEntriesDataGridViewTextBoxColumn.Name = "countEntriesDataGridViewTextBoxColumn";
            this.countEntriesDataGridViewTextBoxColumn.ReadOnly = true;
            this.countEntriesDataGridViewTextBoxColumn.Width = 120;
            // 
            // sOPNUMBEDataGridViewTextBoxColumn
            // 
            this.sOPNUMBEDataGridViewTextBoxColumn.DataPropertyName = "SOPNUMBE";
            this.sOPNUMBEDataGridViewTextBoxColumn.HeaderText = "Výrobní Zakazka";
            this.sOPNUMBEDataGridViewTextBoxColumn.Name = "sOPNUMBEDataGridViewTextBoxColumn";
            this.sOPNUMBEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sOPNUMBEDataGridViewTextBoxColumn.Width = 120;
            // 
            // sKLIDDataGridViewTextBoxColumn
            // 
            this.sKLIDDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.HeaderText = "Sklad ID";
            this.sKLIDDataGridViewTextBoxColumn.Name = "sKLIDDataGridViewTextBoxColumn";
            this.sKLIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.sKLIDDataGridViewTextBoxColumn.Width = 120;
            // 
            // sOPDESCDataGridViewTextBoxColumn
            // 
            this.sOPDESCDataGridViewTextBoxColumn.DataPropertyName = "SOPDESC";
            this.sOPDESCDataGridViewTextBoxColumn.HeaderText = "Popis";
            this.sOPDESCDataGridViewTextBoxColumn.Name = "sOPDESCDataGridViewTextBoxColumn";
            this.sOPDESCDataGridViewTextBoxColumn.ReadOnly = true;
            this.sOPDESCDataGridViewTextBoxColumn.Width = 120;
            // 
            // FormProductionSourcesList_SelectCountEntries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 413);
            this.Controls.Add(this.dgPS);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormProductionSourcesList_SelectCountEntries";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProductionSourcesList_SelectCountEntries_FormClosing);
            this.Load += new System.EventHandler(this.FormProductionSourcesList_SelectCountEntries_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormProductionSourcesList_SelectCountEntries_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPS)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgPS;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.BindingSource bsPS;
        public Fask.Interfaces.DataSets.Vyroba dsPS;
        private System.Windows.Forms.DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPNUMBEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPDESCDataGridViewTextBoxColumn;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
    }
}