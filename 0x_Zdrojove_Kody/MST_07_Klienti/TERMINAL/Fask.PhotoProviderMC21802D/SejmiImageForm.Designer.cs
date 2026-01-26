namespace Fask.PhotoProviderMC21802D
{
    partial class SejmiImageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SejmiImageForm));
            this.zpet_but = new Fask.Graphic.GraphicButton();
            this.ok_but = new Fask.Graphic.GraphicButton();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // zpet_but
            // 
            this.zpet_but.BitmapNormal = null;
            resources.ApplyResources(this.zpet_but, "zpet_but");
            this.zpet_but.FocusMargin = 5;
            this.zpet_but.Name = "zpet_but";
            this.zpet_but.Pressed = false;
            this.zpet_but.Transparent = System.Drawing.Color.White;
            this.zpet_but.Click += new System.EventHandler(this.zpet_but_Click);
            // 
            // ok_but
            // 
            this.ok_but.BitmapNormal = null;
            resources.ApplyResources(this.ok_but, "ok_but");
            this.ok_but.FocusMargin = 5;
            this.ok_but.Name = "ok_but";
            this.ok_but.Pressed = false;
            this.ok_but.Transparent = System.Drawing.Color.White;
            this.ok_but.Click += new System.EventHandler(this.ok_but_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.zpet_but);
            this.panelButtons.Controls.Add(this.ok_but);
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // statusBar1
            // 
            resources.ApplyResources(this.statusBar1, "statusBar1");
            this.statusBar1.Name = "statusBar1";
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.Name = "pictureBox";
            // 
            // SejmiImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.statusBar1);
            this.KeyPreview = true;
            this.Name = "SejmiImageForm";
            this.Load += new System.EventHandler(this.SejmiImageForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SejmiImageForm_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton zpet_but;
        private Fask.Graphic.GraphicButton ok_but;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}