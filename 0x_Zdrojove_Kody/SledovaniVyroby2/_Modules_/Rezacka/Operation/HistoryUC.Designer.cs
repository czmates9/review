namespace FASK.SledovaniVyroby.Module.Rezacka
{
    partial class HistoryUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgHistory = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgHistory);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 122);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Historie operací";
            // 
            // dgHistory
            // 
            this.dgHistory.AllowUserToAddRows = false;
            this.dgHistory.AllowUserToDeleteRows = false;
            this.dgHistory.AllowUserToResizeRows = false;
            this.dgHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgHistory.Location = new System.Drawing.Point(3, 17);
            this.dgHistory.MultiSelect = false;
            this.dgHistory.Name = "dgHistory";
            this.dgHistory.ReadOnly = true;
            this.dgHistory.RowHeadersVisible = false;
            this.dgHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHistory.Size = new System.Drawing.Size(317, 102);
            this.dgHistory.TabIndex = 110;
            this.dgHistory.SelectionChanged += new System.EventHandler(this.dgHistory_SelectionChanged);
            // 
            // HistoryUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "HistoryUC";
            this.Size = new System.Drawing.Size(323, 122);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgHistory;
    }
}
