namespace Fask.MST_W.Forms
{
    partial class SplashScreen
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.imageControl1 = new Fask.Graphic.ImageControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 28);
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 28);
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(0, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 28);
            this.label3.Text = "label3";
            // 
            // imageControl1
            // 
            this.imageControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.imageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageControl1.Image = null;
            this.imageControl1.Location = new System.Drawing.Point(0, 0);
            this.imageControl1.Name = "imageControl1";
            this.imageControl1.Size = new System.Drawing.Size(176, 145);
            this.imageControl1.SizeMode = Fask.Graphic.ImageControl.ImageSizeMode.FitImage;
            this.imageControl1.TabIndex = 4;
            this.imageControl1.Transparent = System.Drawing.Color.Black;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(176, 229);
            this.ControlBox = false;
            this.Controls.Add(this.imageControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.Text = "MST_W";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Fask.Graphic.ImageControl imageControl1;
    }
}