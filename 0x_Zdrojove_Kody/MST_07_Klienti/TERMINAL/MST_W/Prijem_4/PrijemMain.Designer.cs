namespace Fask.MST_W.Prijem_4
{
    partial class PrijemMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrijemMain));
            this.buttonKonec = new Fask.Graphic.GraphicButton();
            this.buttonDavka = new Fask.Graphic.GraphicButton();
            this.vratitDavku_but = new Fask.Graphic.GraphicButton();
            this.odesliHotovouDavku_but = new Fask.Graphic.GraphicButton();
            this.stahniDavku_but = new Fask.Graphic.GraphicButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonKonec
            // 
            this.buttonKonec.BitmapNormal = null;
            resources.ApplyResources(this.buttonKonec, "buttonKonec");
            this.buttonKonec.FocusMargin = 5;
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Pressed = false;
            this.buttonKonec.Transparent = System.Drawing.Color.White;
            this.buttonKonec.Click += new System.EventHandler(this.buttonKonec_Click);
            this.buttonKonec.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonKonec_KeyDown);
            // 
            // buttonDavka
            // 
            this.buttonDavka.BitmapNormal = null;
            resources.ApplyResources(this.buttonDavka, "buttonDavka");
            this.buttonDavka.FocusMargin = 5;
            this.buttonDavka.Name = "buttonDavka";
            this.buttonDavka.Pressed = false;
            this.buttonDavka.Transparent = System.Drawing.Color.White;
            this.buttonDavka.Click += new System.EventHandler(this.buttonDavka_Click);
            this.buttonDavka.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonDavka_KeyDown);
            // 
            // vratitDavku_but
            // 
            this.vratitDavku_but.BitmapNormal = null;
            resources.ApplyResources(this.vratitDavku_but, "vratitDavku_but");
            this.vratitDavku_but.FocusMargin = 5;
            this.vratitDavku_but.Name = "vratitDavku_but";
            this.vratitDavku_but.Pressed = false;
            this.vratitDavku_but.Transparent = System.Drawing.Color.White;
            this.vratitDavku_but.Click += new System.EventHandler(this.vratitDavku_but_Click);
            this.vratitDavku_but.KeyDown += new System.Windows.Forms.KeyEventHandler(this.vratitDavku_but_KeyDown);
            // 
            // odesliHotovouDavku_but
            // 
            this.odesliHotovouDavku_but.BitmapNormal = null;
            resources.ApplyResources(this.odesliHotovouDavku_but, "odesliHotovouDavku_but");
            this.odesliHotovouDavku_but.FocusMargin = 5;
            this.odesliHotovouDavku_but.Name = "odesliHotovouDavku_but";
            this.odesliHotovouDavku_but.Pressed = false;
            this.odesliHotovouDavku_but.Transparent = System.Drawing.Color.White;
            this.odesliHotovouDavku_but.Click += new System.EventHandler(this.odesliHotovouDavku_but_Click);
            this.odesliHotovouDavku_but.KeyDown += new System.Windows.Forms.KeyEventHandler(this.odesliHotovouDavku_but_KeyDown);
            // 
            // stahniDavku_but
            // 
            this.stahniDavku_but.BitmapNormal = null;
            resources.ApplyResources(this.stahniDavku_but, "stahniDavku_but");
            this.stahniDavku_but.FocusMargin = 5;
            this.stahniDavku_but.Name = "stahniDavku_but";
            this.stahniDavku_but.Pressed = false;
            this.stahniDavku_but.Transparent = System.Drawing.Color.White;
            this.stahniDavku_but.Click += new System.EventHandler(this.stahniDavku_but_Click);
            this.stahniDavku_but.KeyDown += new System.Windows.Forms.KeyEventHandler(this.stahniDavku_but_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.vratitDavku_but);
            this.panel1.Controls.Add(this.odesliHotovouDavku_but);
            this.panel1.Controls.Add(this.stahniDavku_but);
            this.panel1.Controls.Add(this.buttonKonec);
            this.panel1.Controls.Add(this.buttonDavka);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // PrijemMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "PrijemMain";
            this.Deactivate += new System.EventHandler(this.PrijemMain_Deactivate);
            this.Load += new System.EventHandler(this.PrijemMain_Load);
            this.Activated += new System.EventHandler(this.PrijemMain_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.PrijemMain_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrijemMain_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton buttonKonec;
        private Fask.Graphic.GraphicButton buttonDavka;
        private Fask.Graphic.GraphicButton vratitDavku_but;
        private Fask.Graphic.GraphicButton odesliHotovouDavku_but;
        private Fask.Graphic.GraphicButton stahniDavku_but;
        private System.Windows.Forms.Panel panel1;
    }
}