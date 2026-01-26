namespace Fask.MST_W.Forms
{
    partial class SejmiSNHromadneForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SejmiSNHromadneForm));
            this.zpet_but = new Fask.Graphic.GraphicButton();
            this.ok_but = new Fask.Graphic.GraphicButton();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelNazev = new System.Windows.Forms.Panel();
            this.lbl_Nazev = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Od = new System.Windows.Forms.TextBox();
            this.panelRight = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Do = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_Pocet = new System.Windows.Forms.TextBox();
            this.lbl_Pocet = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.panelNazev.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panel1.SuspendLayout();
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
            // panelNazev
            // 
            this.panelNazev.Controls.Add(this.lbl_Pocet);
            this.panelNazev.Controls.Add(this.lbl_Nazev);
            resources.ApplyResources(this.panelNazev, "panelNazev");
            this.panelNazev.Name = "panelNazev";
            // 
            // lbl_Nazev
            // 
            resources.ApplyResources(this.lbl_Nazev, "lbl_Nazev");
            this.lbl_Nazev.Name = "lbl_Nazev";
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.label2);
            this.panelLeft.Controls.Add(this.tb_Od);
            resources.ApplyResources(this.panelLeft, "panelLeft");
            this.panelLeft.Name = "panelLeft";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tb_Od
            // 
            resources.ApplyResources(this.tb_Od, "tb_Od");
            this.tb_Od.Name = "tb_Od";
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.label3);
            this.panelRight.Controls.Add(this.tb_Do);
            resources.ApplyResources(this.panelRight, "panelRight");
            this.panelRight.Name = "panelRight";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tb_Do
            // 
            resources.ApplyResources(this.tb_Do, "tb_Do");
            this.tb_Do.Name = "tb_Do";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tb_Pocet);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // tb_Pocet
            // 
            resources.ApplyResources(this.tb_Pocet, "tb_Pocet");
            this.tb_Pocet.Name = "tb_Pocet";
            // 
            // lbl_Pocet
            // 
            resources.ApplyResources(this.lbl_Pocet, "lbl_Pocet");
            this.lbl_Pocet.Name = "lbl_Pocet";
            // 
            // SejmiSNHromadneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelNazev);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "SejmiSNHromadneForm";
            this.Load += new System.EventHandler(this.SejmiKodForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SejmiKodForm_Closing);
            this.Resize += new System.EventHandler(this.SejmiSNHromadneForm_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SejmiKodForm_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelNazev.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton zpet_but;
        private Fask.Graphic.GraphicButton ok_but;
        protected System.Windows.Forms.Panel panelNazev;
        protected System.Windows.Forms.Label lbl_Nazev;
        private System.Windows.Forms.Panel panelLeft;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.TextBox tb_Od;
        private System.Windows.Forms.Panel panelRight;
        protected System.Windows.Forms.Label label3;
        protected System.Windows.Forms.TextBox tb_Do;
        private System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Label label4;
        protected System.Windows.Forms.TextBox tb_Pocet;
        private System.Windows.Forms.Label lbl_Pocet;
    }
}