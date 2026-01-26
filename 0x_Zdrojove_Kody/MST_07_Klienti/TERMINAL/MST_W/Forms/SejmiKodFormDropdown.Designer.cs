namespace Fask.MST_W.Forms
{
    partial class SejmiKodFormDropdown
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SejmiKodFormDropdown));
            this.popis_l = new System.Windows.Forms.Label();
            this.zpet_but = new Fask.Graphic.GraphicButton();
            this.ok_but = new Fask.Graphic.GraphicButton();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelKod = new System.Windows.Forms.Panel();
            this.kod_tb = new System.Windows.Forms.ComboBox();
            this.panelButtons.SuspendLayout();
            this.panelKod.SuspendLayout();
            this.SuspendLayout();
            // 
            // popis_l
            // 
            resources.ApplyResources(this.popis_l, "popis_l");
            this.popis_l.Name = "popis_l";
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
            // panelKod
            // 
            this.panelKod.Controls.Add(this.kod_tb);
            this.panelKod.Controls.Add(this.popis_l);
            resources.ApplyResources(this.panelKod, "panelKod");
            this.panelKod.Name = "panelKod";
            // 
            // kod_tb
            // 
            resources.ApplyResources(this.kod_tb, "kod_tb");
            this.kod_tb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.kod_tb.Name = "kod_tb";
            // 
            // SejmiKodFormDropdown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panelKod);
            this.Controls.Add(this.panelButtons);
            this.Name = "SejmiKodFormDropdown";
            this.Load += new System.EventHandler(this.SejmiKodFormDropdown_Load);
            //this.Closing += new System.ComponentModel.CancelEventHandler(this.SejmiKodFormDropdown_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SejmiKodFormDropdown_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelKod.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton zpet_but;
        private Fask.Graphic.GraphicButton ok_but;
        protected System.Windows.Forms.Panel panelKod;
        protected System.Windows.Forms.Label popis_l;
        internal System.Windows.Forms.ComboBox kod_tb;
    }
}