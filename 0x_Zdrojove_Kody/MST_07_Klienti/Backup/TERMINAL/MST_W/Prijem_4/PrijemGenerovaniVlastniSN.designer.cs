namespace Fask.MST_W.Prijem_4
{
    partial class PrijemGenerovaniVlastniSN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrijemGenerovaniVlastniSN));
            this.LBL_volne_SN = new System.Windows.Forms.Label();
            this.LBL_pocet_SN = new System.Windows.Forms.Label();
            this.TXT_pocet_SN = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LBL_nalezene_volne_SN = new System.Windows.Forms.Label();
            this.BTN_generate_vlastni_SN = new Fask.Graphic.GraphicButton();
            this.button1 = new Fask.Graphic.GraphicButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LBL_volne_SN
            // 
            resources.ApplyResources(this.LBL_volne_SN, "LBL_volne_SN");
            this.LBL_volne_SN.Name = "LBL_volne_SN";
            // 
            // LBL_pocet_SN
            // 
            resources.ApplyResources(this.LBL_pocet_SN, "LBL_pocet_SN");
            this.LBL_pocet_SN.Name = "LBL_pocet_SN";
            // 
            // TXT_pocet_SN
            // 
            resources.ApplyResources(this.TXT_pocet_SN, "TXT_pocet_SN");
            this.TXT_pocet_SN.Name = "TXT_pocet_SN";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TXT_pocet_SN);
            this.panel2.Controls.Add(this.LBL_pocet_SN);
            this.panel2.Controls.Add(this.LBL_nalezene_volne_SN);
            this.panel2.Controls.Add(this.LBL_volne_SN);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // LBL_nalezene_volne_SN
            // 
            resources.ApplyResources(this.LBL_nalezene_volne_SN, "LBL_nalezene_volne_SN");
            this.LBL_nalezene_volne_SN.Name = "LBL_nalezene_volne_SN";
            // 
            // BTN_generate_vlastni_SN
            // 
            this.BTN_generate_vlastni_SN.BitmapNormal = null;
            resources.ApplyResources(this.BTN_generate_vlastni_SN, "BTN_generate_vlastni_SN");
            this.BTN_generate_vlastni_SN.FocusMargin = 5;
            this.BTN_generate_vlastni_SN.Name = "BTN_generate_vlastni_SN";
            this.BTN_generate_vlastni_SN.Pressed = false;
            this.BTN_generate_vlastni_SN.Transparent = System.Drawing.Color.White;
            this.BTN_generate_vlastni_SN.Click += new System.EventHandler(this.BTN_generate_vlastni_SN_Click);
            this.BTN_generate_vlastni_SN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BTN_generate_vlastni_SN_KeyDown);
            // 
            // button1
            // 
            this.button1.BitmapNormal = null;
            resources.ApplyResources(this.button1, "button1");
            this.button1.FocusMargin = 5;
            this.button1.Name = "button1";
            this.button1.Pressed = false;
            this.button1.Transparent = System.Drawing.Color.White;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BTN_generate_vlastni_SN);
            this.panel1.Controls.Add(this.button1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            resources.ApplyResources(this.menuItem1, "menuItem1");
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            resources.ApplyResources(this.menuItem2, "menuItem2");
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // PrijemGenerovaniVlastniSN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "PrijemGenerovaniVlastniSN";
            this.Load += new System.EventHandler(this.PrijemGenerovaniVlastniSN_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrijemGenerovaniVlastniSN_KeyDown);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LBL_volne_SN;
        private System.Windows.Forms.Label LBL_pocet_SN;
        private System.Windows.Forms.TextBox TXT_pocet_SN;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LBL_nalezene_volne_SN;
        private Fask.Graphic.GraphicButton BTN_generate_vlastni_SN;
        private Fask.Graphic.GraphicButton button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}