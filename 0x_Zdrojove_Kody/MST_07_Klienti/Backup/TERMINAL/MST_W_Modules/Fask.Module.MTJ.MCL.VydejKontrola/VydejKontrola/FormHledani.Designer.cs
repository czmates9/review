using Fask.Graphic;
namespace Fask.Module.MTJ.MCL.VydejKontrola.VydejKontrola
{
    partial class FormHledani
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHledani));
			this.panelButtons = new System.Windows.Forms.Panel();
			this.bOK = new Fask.Graphic.GraphicButton();
			this.bStorno = new Fask.Graphic.GraphicButton();
			this.panelData = new System.Windows.Forms.Panel();
			this.listBoxFinded = new System.Windows.Forms.ListBox();
			this.labelFind = new System.Windows.Forms.Label();
			this.labelBarcode = new System.Windows.Forms.Label();
			this.panelButtons.SuspendLayout();
			this.panelData.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelButtons
			// 
			this.panelButtons.Controls.Add(this.bOK);
			this.panelButtons.Controls.Add(this.bStorno);
			resources.ApplyResources(this.panelButtons, "panelButtons");
			this.panelButtons.Name = "panelButtons";
			// 
			// bOK
			// 
			this.bOK.BitmapNormal = null;
			resources.ApplyResources(this.bOK, "bOK");
			this.bOK.FocusMargin = 5;
			this.bOK.Name = "bOK";
			this.bOK.Pressed = false;
			this.bOK.Transparent = System.Drawing.Color.White;
			this.bOK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// bStorno
			// 
			this.bStorno.BitmapNormal = null;
			resources.ApplyResources(this.bStorno, "bStorno");
			this.bStorno.FocusMargin = 5;
			this.bStorno.Name = "bStorno";
			this.bStorno.Pressed = false;
			this.bStorno.Transparent = System.Drawing.Color.White;
			this.bStorno.Click += new System.EventHandler(this.bStorno_Click);
			// 
			// panelData
			// 
			this.panelData.Controls.Add(this.listBoxFinded);
			this.panelData.Controls.Add(this.labelFind);
			this.panelData.Controls.Add(this.labelBarcode);
			resources.ApplyResources(this.panelData, "panelData");
			this.panelData.Name = "panelData";
			// 
			// listBoxFinded
			// 
			resources.ApplyResources(this.listBoxFinded, "listBoxFinded");
			this.listBoxFinded.Name = "listBoxFinded";
			// 
			// labelFind
			// 
			resources.ApplyResources(this.labelFind, "labelFind");
			this.labelFind.Name = "labelFind";
			// 
			// labelBarcode
			// 
			resources.ApplyResources(this.labelBarcode, "labelBarcode");
			this.labelBarcode.Name = "labelBarcode";
			// 
			// FormHledani
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			resources.ApplyResources(this, "$this");
			this.ControlBox = false;
			this.Controls.Add(this.panelData);
			this.Controls.Add(this.panelButtons);
			this.KeyPreview = true;
			this.Name = "FormHledani";
			this.Load += new System.EventHandler(this.FaskFormBase_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormHledani_KeyDown);
			this.panelButtons.ResumeLayout(false);
			this.panelData.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private GraphicButton bOK;
        public GraphicButton bStorno;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.ListBox listBoxFinded;
        private System.Windows.Forms.Label labelFind;
        private System.Windows.Forms.Label labelBarcode;
    }
}