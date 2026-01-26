namespace FASK.SledovaniVyroby.Module.ZZS
{
    partial class CiselnikServiceOperationsForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 303);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(342, 20);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 20);
            this.label1.TabIndex = 5;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CiselnikServiceOperationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(342, 323);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CiselnikServiceOperationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prodej";
            this.Load += new System.EventHandler(this.CiselnikServiceOperationsForm_Load);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
    }
}
