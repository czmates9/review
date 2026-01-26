namespace Konzola.SkladLokace
{
    partial class FormSkladLokaceStavList_SelectCountEntries
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
            this.dgP = new Zuby.ADGV.AdvancedDataGridView();
            this.bsP = new System.Windows.Forms.BindingSource(this.components);
            this.dsI1H = new Fask.Interfaces.DataSets.Inventura();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.countEntriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsI1H)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgP
            // 
            this.dgP.AllowUserToAddRows = false;
            this.dgP.AllowUserToDeleteRows = false;
            this.dgP.AllowUserToOrderColumns = true;
            this.dgP.AllowUserToResizeRows = false;
            this.dgP.AutoGenerateColumns = false;
            this.dgP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.countEntriesDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn});
            this.dgP.DataSource = this.bsP;
            this.dgP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgP.FilterAndSortEnabled = true;
            this.dgP.Location = new System.Drawing.Point(0, 27);
            this.dgP.MultiSelect = false;
            this.dgP.Name = "dgP";
            this.dgP.ReadOnly = true;
            this.dgP.RowHeadersVisible = false;
            this.dgP.RowTemplate.Height = 24;
            this.dgP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgP.Size = new System.Drawing.Size(406, 315);
            this.dgP.TabIndex = 0;
            this.dgP.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgP.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // bsP
            // 
            this.bsP.DataMember = "CZMST_I1H";
            this.bsP.DataSource = this.dsI1H;
            // 
            // dsI1H
            // 
            this.dsI1H.DataSetName = "Inventura";
            this.dsI1H.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(300, 26);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 27);
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
            this.groupBox1.Location = new System.Drawing.Point(0, 342);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(406, 71);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dávka :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(80, 30);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(190, 20);
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
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(406, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 3;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // countEntriesDataGridViewTextBoxColumn
            // 
            this.countEntriesDataGridViewTextBoxColumn.DataPropertyName = "CountEntries";
            this.countEntriesDataGridViewTextBoxColumn.HeaderText = "Číslo dávky";
            this.countEntriesDataGridViewTextBoxColumn.Name = "countEntriesDataGridViewTextBoxColumn";
            this.countEntriesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Popis";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            this.descriptionDataGridViewTextBoxColumn.Width = 200;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.HeaderText = "Status";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FormSkladLokaceStavList_SelectCountEntries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 413);
            this.Controls.Add(this.dgP);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormSkladLokaceStavList_SelectCountEntries";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSkladLokaceStavList_SelectCountEntries_FormClosing);
            this.Load += new System.EventHandler(this.FormSkladLokaceStavList_SelectCountEntries_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSkladLokaceStavList_SelectCountEntries_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsI1H)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.BindingSource bsP;
        private Fask.Interfaces.DataSets.Inventura dsI1H;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
    }
}