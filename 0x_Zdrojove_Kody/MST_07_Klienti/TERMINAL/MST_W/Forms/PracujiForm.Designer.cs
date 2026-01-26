namespace Fask.MST_W.Forms
{
    partial class PracujiForm
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
            if (disposing)
            {
                if (waitTimer != null)
                {
                    waitTimer.Enabled = false;
                    waitTimer.Dispose();
                    waitTimer = null;
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PracujiForm));
            this.zprava_l = new System.Windows.Forms.Label();
            this.waitTimer = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // zprava_l
            // 
            resources.ApplyResources(this.zprava_l, "zprava_l");
            this.zprava_l.Name = "zprava_l";
            this.zprava_l.ParentChanged += new System.EventHandler(this.zprava_l_ParentChanged);
            // 
            // waitTimer
            // 
            this.waitTimer.Enabled = true;
            this.waitTimer.Interval = 1200;
            this.waitTimer.Tick += new System.EventHandler(this.waitTimer_Tick);
            // 
            // PracujiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.zprava_l);
            this.Name = "PracujiForm";
            this.Load += new System.EventHandler(this.PracujiForm_Load);
            this.Closed += new System.EventHandler(this.PracujiForm_Closed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label zprava_l;
        private System.Windows.Forms.Timer waitTimer;
    }
}