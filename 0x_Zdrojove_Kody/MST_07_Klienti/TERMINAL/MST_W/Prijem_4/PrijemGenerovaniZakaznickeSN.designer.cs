namespace Fask.MST_W.Prijem_4
{
    partial class PrijemGenerovaniZakaznickeSN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrijemGenerovaniZakaznickeSN));
            this.LBL_zadejte_SN = new System.Windows.Forms.Label();
            this.TXT_zadane_SN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TXT_pocet_SN2 = new System.Windows.Forms.TextBox();
            this.BTN_generate_zazkaznicke_SN = new Fask.Graphic.GraphicButton();
            this.button1 = new Fask.Graphic.GraphicButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LBL_zadejte_SN
            // 
            resources.ApplyResources(this.LBL_zadejte_SN, "LBL_zadejte_SN");
            this.LBL_zadejte_SN.Name = "LBL_zadejte_SN";
            // 
            // TXT_zadane_SN
            // 
            resources.ApplyResources(this.TXT_zadane_SN, "TXT_zadane_SN");
            this.TXT_zadane_SN.Name = "TXT_zadane_SN";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // TXT_pocet_SN2
            // 
            resources.ApplyResources(this.TXT_pocet_SN2, "TXT_pocet_SN2");
            this.TXT_pocet_SN2.Name = "TXT_pocet_SN2";
            // 
            // BTN_generate_zazkaznicke_SN
            // 
            this.BTN_generate_zazkaznicke_SN.BitmapNormal = null;
            resources.ApplyResources(this.BTN_generate_zazkaznicke_SN, "BTN_generate_zazkaznicke_SN");
            this.BTN_generate_zazkaznicke_SN.FocusMargin = 5;
            this.BTN_generate_zazkaznicke_SN.Name = "BTN_generate_zazkaznicke_SN";
            this.BTN_generate_zazkaznicke_SN.Pressed = false;
            this.BTN_generate_zazkaznicke_SN.Transparent = System.Drawing.Color.White;
            this.BTN_generate_zazkaznicke_SN.Click += new System.EventHandler(this.BTN_generate_zazkaznicke_SN_Click);
            this.BTN_generate_zazkaznicke_SN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BTN_generate_zazkaznicke_SN_KeyDown);
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
            this.panel1.Controls.Add(this.BTN_generate_zazkaznicke_SN);
            this.panel1.Controls.Add(this.button1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TXT_pocet_SN2);
            this.panel2.Controls.Add(this.TXT_zadane_SN);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.LBL_zadejte_SN);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
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
            // PrijemGenerovaniZakaznickeSN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "PrijemGenerovaniZakaznickeSN";
            this.Load += new System.EventHandler(this.PrijemGenerovaniZakaznickeSN_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrijemGenerovaniZakaznickeSN_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LBL_zadejte_SN;
        private System.Windows.Forms.TextBox TXT_zadane_SN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TXT_pocet_SN2;
        private Fask.Graphic.GraphicButton BTN_generate_zazkaznicke_SN;
        private Fask.Graphic.GraphicButton button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}