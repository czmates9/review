namespace Fask.MST_W.Vydej_3
{
    partial class SejmiKodInfoForm4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SejmiKodInfoForm4));
            this.nacteno_l = new System.Windows.Forms.Label();
            this.nacist_l = new System.Windows.Forms.Label();
            this.baleni_l = new System.Windows.Forms.Label();
            this.CZ_CarKod_l = new System.Windows.Forms.Label();
            this.ItemDesc_l = new System.Windows.Forms.Label();
            this.ItemNmbr_l = new System.Windows.Forms.Label();
            this.toolBarExtended = new System.Windows.Forms.ToolBar();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonNeshodne = new System.Windows.Forms.ToolBarButton();
            this.imageListExtended = new System.Windows.Forms.ImageList();
            this.panelData = new System.Windows.Forms.Panel();
            this.Note_l = new System.Windows.Forms.Label();
            this.SERLTNUM_l = new System.Windows.Forms.Label();
            this.panelData.SuspendLayout();
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
            // toolBarExtended
            // 
            this.toolBarExtended.Buttons.Add(this.toolBarButton1);
            this.toolBarExtended.Buttons.Add(this.toolBarButtonNeshodne);
            this.toolBarExtended.ImageList = this.imageListExtended;
            this.toolBarExtended.Name = "toolBarExtended";
            this.toolBarExtended.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarExtended_ButtonClick);
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonNeshodne
            // 
            resources.ApplyResources(this.toolBarButtonNeshodne, "toolBarButtonNeshodne");
            this.toolBarButtonNeshodne.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // imageListExtended
            // 
            resources.ApplyResources(this.imageListExtended, "imageListExtended");
            this.imageListExtended.Images.Clear();
            this.imageListExtended.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource"))));
            this.imageListExtended.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource1"))));
            // 
            // panelData
            // 
            this.panelData.Controls.Add(this.Note_l);
            this.panelData.Controls.Add(this.SERLTNUM_l);
            this.panelData.Controls.Add(this.baleni_l);
            this.panelData.Controls.Add(this.nacteno_l);
            this.panelData.Controls.Add(this.ItemDesc_l);
            this.panelData.Controls.Add(this.nacist_l);
            this.panelData.Controls.Add(this.ItemNmbr_l);
            this.panelData.Controls.Add(this.CZ_CarKod_l);
            resources.ApplyResources(this.panelData, "panelData");
            this.panelData.Name = "panelData";
            // 
            // Note_l
            // 
            resources.ApplyResources(this.Note_l, "Note_l");
            this.Note_l.ForeColor = System.Drawing.Color.Red;
            this.Note_l.Name = "Note_l";
            // 
            // SERLTNUM_l
            // 
            resources.ApplyResources(this.SERLTNUM_l, "SERLTNUM_l");
            this.SERLTNUM_l.Name = "SERLTNUM_l";
            // 
            // SejmiKodInfoForm4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.toolBarExtended);
            this.Name = "SejmiKodInfoForm4";
            this.Load += new System.EventHandler(this.SejmiKodInfoForm4_Load);
            this.panelData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label nacteno_l;
        private System.Windows.Forms.Label nacist_l;
        private System.Windows.Forms.Label baleni_l;
        private System.Windows.Forms.Label CZ_CarKod_l;
        private System.Windows.Forms.Label ItemDesc_l;
        private System.Windows.Forms.Label ItemNmbr_l;
        private System.Windows.Forms.ToolBar toolBarExtended;
        private System.Windows.Forms.ImageList imageListExtended;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton toolBarButtonNeshodne;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.Label SERLTNUM_l;
        private System.Windows.Forms.Label Note_l;
    }
}
