namespace Konzola.Vydej
{
    partial class Form_Predloha_Rekapitulace
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
            this.dg_rekap = new Zuby.ADGV.AdvancedDataGridView();
            this.countEntriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_rekap = new System.Windows.Forms.BindingSource(this.components);
            this.ds_local = new Fask.Interfaces.DataSets.Vydej();
            this.label1 = new System.Windows.Forms.Label();
            this.ds_rekap = new Fask.Interfaces.DataSets.Vydej();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dg_rekap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_rekap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_local)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_rekap)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg_rekap
            // 
            this.dg_rekap.AllowUserToAddRows = false;
            this.dg_rekap.AllowUserToDeleteRows = false;
            this.dg_rekap.AllowUserToResizeColumns = false;
            this.dg_rekap.AllowUserToResizeRows = false;
            this.dg_rekap.AutoGenerateColumns = false;
            this.dg_rekap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.countEntriesDataGridViewTextBoxColumn});
            this.dg_rekap.DataSource = this.bs_rekap;
            this.dg_rekap.FilterAndSortEnabled = true;
            this.dg_rekap.Location = new System.Drawing.Point(12, 12);
            this.dg_rekap.MultiSelect = false;
            this.dg_rekap.Name = "dg_rekap";
            this.dg_rekap.ReadOnly = true;
            this.dg_rekap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_rekap.Size = new System.Drawing.Size(344, 221);
            this.dg_rekap.TabIndex = 0;
            this.dg_rekap.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // countEntriesDataGridViewTextBoxColumn
            // 
            this.countEntriesDataGridViewTextBoxColumn.DataPropertyName = "CountEntries";
            this.countEntriesDataGridViewTextBoxColumn.HeaderText = "Číslo dávky";
            this.countEntriesDataGridViewTextBoxColumn.Name = "countEntriesDataGridViewTextBoxColumn";
            this.countEntriesDataGridViewTextBoxColumn.ReadOnly = true;
            this.countEntriesDataGridViewTextBoxColumn.Width = 280;
            // 
            // bs_rekap
            // 
            this.bs_rekap.DataMember = "CZMST_SE_Rekap";
            this.bs_rekap.DataSource = this.ds_local;
            // 
            // ds_local
            // 
            this.ds_local.DataSetName = "Vydej";
            this.ds_local.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // ds_rekap
            // 
            this.ds_rekap.DataSetName = "Vydej";
            this.ds_rekap.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // Form_Predloha_Rekapitulace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 301);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dg_rekap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Predloha_Rekapitulace";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rekapitulace";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Predloha_Rekapitulace_FormClosing);
            this.Load += new System.EventHandler(this.Form_Predloha_Rekapitulace_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_Predloha_Rekapitulace_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dg_rekap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_rekap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_local)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_rekap)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bs_rekap;
        private Zuby.ADGV.AdvancedDataGridView dg_rekap;
        public Fask.Interfaces.DataSets.Vydej ds_rekap;
        private System.Windows.Forms.Label label1;
        private Fask.Interfaces.DataSets.Vydej ds_local;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn;
    }
}