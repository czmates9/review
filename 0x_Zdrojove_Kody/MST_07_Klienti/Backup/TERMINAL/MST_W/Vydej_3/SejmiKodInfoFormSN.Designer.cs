namespace Fask.MST_W.Vydej_3
{
    partial class SejmiKodInfoFormSN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SejmiKodInfoFormSN));
            this.nacteno_l = new System.Windows.Forms.Label();
            this.nacist_l = new System.Windows.Forms.Label();
            this.baleni_l = new System.Windows.Forms.Label();
            this.CZ_CarKod_l = new System.Windows.Forms.Label();
            this.ItemDesc_l = new System.Windows.Forms.Label();
            this.ItemNmbr_l = new System.Windows.Forms.Label();
            this.btnZobrazitAlternativy = new Fask.Graphic.GraphicButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SERLTNUM_l = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // nacteno_l
            // 
            resources.ApplyResources(this.nacteno_l, "nacteno_l");
            this.nacteno_l.Name = "nacteno_l";
            // 
            // nacist_l
            // 
            resources.ApplyResources(this.nacist_l, "nacist_l");
            this.nacist_l.Name = "nacist_l";
            // 
            // baleni_l
            // 
            resources.ApplyResources(this.baleni_l, "baleni_l");
            this.baleni_l.Name = "baleni_l";
            // 
            // CZ_CarKod_l
            // 
            resources.ApplyResources(this.CZ_CarKod_l, "CZ_CarKod_l");
            this.CZ_CarKod_l.Name = "CZ_CarKod_l";
            // 
            // ItemDesc_l
            // 
            resources.ApplyResources(this.ItemDesc_l, "ItemDesc_l");
            this.ItemDesc_l.Name = "ItemDesc_l";
            // 
            // ItemNmbr_l
            // 
            resources.ApplyResources(this.ItemNmbr_l, "ItemNmbr_l");
            this.ItemNmbr_l.Name = "ItemNmbr_l";
            // 
            // btnZobrazitAlternativy
            // 
            resources.ApplyResources(this.btnZobrazitAlternativy, "btnZobrazitAlternativy");
            this.btnZobrazitAlternativy.FocusMargin = 6;
            this.btnZobrazitAlternativy.Name = "btnZobrazitAlternativy";
            this.btnZobrazitAlternativy.Pressed = false;
            this.btnZobrazitAlternativy.Transparent = System.Drawing.Color.White;
            this.btnZobrazitAlternativy.Click += new System.EventHandler(this.btnZobrazitAlternativy_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.CZ_CarKod_l);
            this.panel1.Controls.Add(this.ItemDesc_l);
            this.panel1.Controls.Add(this.ItemNmbr_l);
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnZobrazitAlternativy);
            this.panel2.Controls.Add(this.baleni_l);
            this.panel2.Controls.Add(this.SERLTNUM_l);
            this.panel2.Controls.Add(this.nacist_l);
            this.panel2.Controls.Add(this.nacteno_l);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // SERLTNUM_l
            // 
            resources.ApplyResources(this.SERLTNUM_l, "SERLTNUM_l");
            this.SERLTNUM_l.Name = "SERLTNUM_l";
            // 
            // SejmiKodInfoFormSN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "SejmiKodInfoFormSN";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SejmiKodInfoForm3_KeyDown);
            this.Load += new System.EventHandler(this.SejmiKodInfoFormSN_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label nacteno_l;
        private System.Windows.Forms.Label nacist_l;
        private System.Windows.Forms.Label baleni_l;
        private System.Windows.Forms.Label CZ_CarKod_l;
        private System.Windows.Forms.Label ItemDesc_l;
        private System.Windows.Forms.Label ItemNmbr_l;
        private Fask.Graphic.GraphicButton btnZobrazitAlternativy;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label SERLTNUM_l;
    }
}
