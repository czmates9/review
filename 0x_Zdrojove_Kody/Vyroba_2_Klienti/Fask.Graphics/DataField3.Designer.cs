namespace Fask.Graphic
{
    partial class DataField3
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
            this.lPopis = new System.Windows.Forms.Label();
            this.tDataEdit = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lPopis
            // 
            this.lPopis.Dock = System.Windows.Forms.DockStyle.Left;
            this.lPopis.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.lPopis.Location = new System.Drawing.Point(0, 0);
            this.lPopis.Name = "lPopis";
            this.lPopis.Size = new System.Drawing.Size(96, 23);
            this.lPopis.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tDataEdit
            // 
            this.tDataEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tDataEdit.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.tDataEdit.Location = new System.Drawing.Point(96, 0);
            this.tDataEdit.Name = "tDataEdit";
            this.tDataEdit.Size = new System.Drawing.Size(122, 22);
            this.tDataEdit.TabIndex = 2;
            // 
            // DataField3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tDataEdit);
            this.Controls.Add(this.lPopis);
            this.Name = "DataField3";
            this.Size = new System.Drawing.Size(218, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lPopis;
        private System.Windows.Forms.TextBox tDataEdit;
    }
}
