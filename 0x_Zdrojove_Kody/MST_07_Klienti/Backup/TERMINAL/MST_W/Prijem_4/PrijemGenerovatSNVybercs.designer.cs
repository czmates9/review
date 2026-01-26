namespace Fask.MST_W.Prijem_4
{
    partial class PrijemGenerovatSNVybercs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrijemGenerovatSNVybercs));
            this.button1 = new Fask.Graphic.GraphicButton();
            this.button2 = new Fask.Graphic.GraphicButton();
            this.button3 = new Fask.Graphic.GraphicButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            // button2
            // 
            this.button2.BitmapNormal = null;
            resources.ApplyResources(this.button2, "button2");
            this.button2.FocusMargin = 5;
            this.button2.Name = "button2";
            this.button2.Pressed = false;
            this.button2.Transparent = System.Drawing.Color.White;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button2_KeyDown);
            // 
            // button3
            // 
            this.button3.BitmapNormal = null;
            resources.ApplyResources(this.button3, "button3");
            this.button3.FocusMargin = 5;
            this.button3.Name = "button3";
            this.button3.Pressed = false;
            this.button3.Transparent = System.Drawing.Color.White;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            this.mainMenu1.MenuItems.Add(this.menuItem3);
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
            // menuItem3
            // 
            resources.ApplyResources(this.menuItem3, "menuItem3");
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // PrijemGenerovatSNVybercs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "PrijemGenerovatSNVybercs";
            this.Load += new System.EventHandler(this.PrijemGenerovatSNVybercs_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrijemGenerovatSNVybercs_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton button1;
        private Fask.Graphic.GraphicButton button2;
        private Fask.Graphic.GraphicButton button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
    }
}