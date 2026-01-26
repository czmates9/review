
namespace FASK.SledovaniVyroby.Module.Vyroba_SV.UC
{
    partial class UC_TISK
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
            this.GB_name = new System.Windows.Forms.GroupBox();
            this.BT_tisk = new System.Windows.Forms.Button();
            this.TB = new System.Windows.Forms.TextBox();
            this.GB_name.SuspendLayout();
            this.SuspendLayout();
            // 
            // GB_name
            // 
            this.GB_name.Controls.Add(this.TB);
            this.GB_name.Controls.Add(this.BT_tisk);
            this.GB_name.Location = new System.Drawing.Point(3, 12);
            this.GB_name.Name = "GB_name";
            this.GB_name.Size = new System.Drawing.Size(349, 90);
            this.GB_name.TabIndex = 0;
            this.GB_name.TabStop = false;
            this.GB_name.Text = "Tisk - test";
            // 
            // BT_tisk
            // 
            this.BT_tisk.Location = new System.Drawing.Point(238, 58);
            this.BT_tisk.Name = "BT_tisk";
            this.BT_tisk.Size = new System.Drawing.Size(105, 23);
            this.BT_tisk.TabIndex = 0;
            this.BT_tisk.Text = "Poslat na tisk";
            this.BT_tisk.UseVisualStyleBackColor = true;
            this.BT_tisk.Click += new System.EventHandler(this.BT_tisk_Click);
            // 
            // TB
            // 
            this.TB.Location = new System.Drawing.Point(7, 19);
            this.TB.Multiline = true;
            this.TB.Name = "TB";
            this.TB.Size = new System.Drawing.Size(336, 33);
            this.TB.TabIndex = 1;
            // 
            // UC_TISK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GB_name);
            this.Name = "UC_TISK";
            this.Size = new System.Drawing.Size(364, 115);
            this.GB_name.ResumeLayout(false);
            this.GB_name.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GB_name;
        private System.Windows.Forms.TextBox TB;
        private System.Windows.Forms.Button BT_tisk;
    }
}
