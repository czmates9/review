namespace Konzola.Terminal
{
    partial class TestDisp
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
            this.button1 = new System.Windows.Forms.Button();
            this.dgData = new Zuby.ADGV.AdvancedDataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Kontrola";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgData
            // 
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgData.Location = new System.Drawing.Point(0, 27);
            this.dgData.Name = "dgData";
            this.dgData.Size = new System.Drawing.Size(773, 411);
            this.dgData.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(773, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(78, 438);
            this.panel1.TabIndex = 2;
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
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(773, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 3;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // TestDisp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 438);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panel1);
            this.Name = "TestDisp";
            this.Text = "TestDisp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestDisp_FormClosing);
            this.Load += new System.EventHandler(this.TestDisp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private Zuby.ADGV.AdvancedDataGridView dgData;
        private System.Windows.Forms.Panel panel1;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
    }
}