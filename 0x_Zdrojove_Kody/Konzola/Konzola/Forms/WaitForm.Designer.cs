namespace Konzola.Forms
{
    partial class WaitForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbEvent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(93, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(26, 66);
            this.progressBar1.Maximum = 250;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.progressBar1.Size = new System.Drawing.Size(215, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // lbEvent
            // 
            this.lbEvent.AutoSize = true;
            this.lbEvent.Location = new System.Drawing.Point(106, 18);
            this.lbEvent.Name = "lbEvent";
            this.lbEvent.Size = new System.Drawing.Size(48, 13);
            this.lbEvent.TabIndex = 2;
            this.lbEvent.Text = "Progress";
            // 
            // WaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 161);
            this.ControlBox = false;
            this.Controls.Add(this.lbEvent);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnCancel);
            this.Name = "WaitForm";
            this.Text = "WaitForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lbEvent;
    }
}