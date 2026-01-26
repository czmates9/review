namespace Fask.MST_W.Vydej_3
{
    partial class ListOdberateliForm3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListOdberateliForm3));
            this.ID_l = new System.Windows.Forms.Label();
            this.popis_l = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.zpet_but = new System.Windows.Forms.Button();
            this.ok_but = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_CarKod = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ID_l
            // 
            resources.ApplyResources(this.ID_l, "ID_l");
            this.ID_l.Name = "ID_l";
            // 
            // popis_l
            // 
            resources.ApplyResources(this.popis_l, "popis_l");
            this.popis_l.Name = "popis_l";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // zpet_but
            // 
            resources.ApplyResources(this.zpet_but, "zpet_but");
            this.zpet_but.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.zpet_but.Name = "zpet_but";
            // 
            // ok_but
            // 
            resources.ApplyResources(this.ok_but, "ok_but");
            this.ok_but.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok_but.Name = "ok_but";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lbl_CarKod
            // 
            resources.ApplyResources(this.lbl_CarKod, "lbl_CarKod");
            this.lbl_CarKod.Name = "lbl_CarKod";
            // 
            // ListOdberateliForm3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.lbl_CarKod);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.zpet_but);
            this.Controls.Add(this.ok_but);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.popis_l);
            this.Controls.Add(this.ID_l);
            this.KeyPreview = true;
            this.Name = "ListOdberateliForm3";
            this.Load += new System.EventHandler(this.ListOdberateliForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ListOdberateliForm_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListOdberateliForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ID_l;
        private System.Windows.Forms.Label popis_l;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button zpet_but;
        private System.Windows.Forms.Button ok_but;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_CarKod;
    }
}