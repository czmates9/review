namespace Fask.MST_W.Prijem_4
{
    partial class DetailItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailItem));
            this.buttonOK = new Fask.Graphic.GraphicButton();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.BitmapNormal = null;
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.FocusMargin = 5;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Pressed = false;
            this.buttonOK.Transparent = System.Drawing.Color.White;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // DetailItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.buttonOK);
            this.KeyPreview = true;
            this.Name = "DetailItem";
            this.Load += new System.EventHandler(this.Detail_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Detail_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Detail_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton buttonOK;
    }
}