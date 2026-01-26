using Fask.Graphic;
namespace Fask.MST_W.Vydej_3.Dialogs
{
    partial class VydejDialogPocetVydavanych
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VydejDialogPocetVydavanych));
			this.panelButtons = new System.Windows.Forms.Panel();
			this.bOK = new Fask.Graphic.GraphicButton();
			this.bStorno = new Fask.Graphic.GraphicButton();
			this.panelData = new System.Windows.Forms.Panel();
			this.dfitemcode = new Fask.Graphic.DataField4();
			this.dfZadano = new Fask.Graphic.DataField4();
			this.dfPocetJednotek = new Fask.Graphic.DataField();
			this.dfItemdesc = new Fask.Graphic.DataField4();
			this.dfItemnmbr = new Fask.Graphic.DataField4();
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
			this.panelData.Controls.Add(this.dfitemcode);
			this.panelData.Controls.Add(this.dfZadano);
			this.panelData.Controls.Add(this.dfPocetJednotek);
			this.panelData.Controls.Add(this.dfItemdesc);
			this.panelData.Controls.Add(this.dfItemnmbr);
			resources.ApplyResources(this.panelData, "panelData");
			this.panelData.Name = "panelData";
			// 
			// dfitemcode
			// 
			resources.ApplyResources(this.dfitemcode, "dfitemcode");
			this.dfitemcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dfitemcode.Data = "";
			this.dfitemcode.Name = "dfitemcode";
			this.dfitemcode.Popis = "Kód:";
			this.dfitemcode.PopisDock = System.Windows.Forms.DockStyle.Left;
			this.dfitemcode.PopisHeight = 20;
			// 
			// dfZadano
			// 
			resources.ApplyResources(this.dfZadano, "dfZadano");
			this.dfZadano.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dfZadano.Data = "";
			this.dfZadano.Name = "dfZadano";
			this.dfZadano.Popis = "Zadano:";
			this.dfZadano.PopisDock = System.Windows.Forms.DockStyle.Left;
			this.dfZadano.PopisHeight = 20;
			// 
			// dfPocetJednotek
			// 
			resources.ApplyResources(this.dfPocetJednotek, "dfPocetJednotek");
			this.dfPocetJednotek.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dfPocetJednotek.Data = "-99999";
			this.dfPocetJednotek.DataBackColor = System.Drawing.SystemColors.Window;
			this.dfPocetJednotek.DataFont = new System.Drawing.Font("Arial", 35F, System.Drawing.FontStyle.Bold);
			this.dfPocetJednotek.DataMaxLength = 32767;
			this.dfPocetJednotek.DataTextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.dfPocetJednotek.MultiLine = false;
			this.dfPocetJednotek.Name = "dfPocetJednotek";
			this.dfPocetJednotek.Popis = "Poèet balíkù";
			this.dfPocetJednotek.PopisDock = System.Windows.Forms.DockStyle.Top;
			this.dfPocetJednotek.PopisFont = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
			this.dfPocetJednotek.PopisHeight = 27;
			this.dfPocetJednotek.PopisTextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.dfPocetJednotek.PopisWidth = 330;
			this.dfPocetJednotek.ReadOnly = false;
			this.dfPocetJednotek.DataChanged += new Fask.Graphic.DataField.DataChangedHandler(this.dfPocetJednotek_DataChanged);
			// 
			// dfItemdesc
			// 
			resources.ApplyResources(this.dfItemdesc, "dfItemdesc");
			this.dfItemdesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dfItemdesc.Data = "";
			this.dfItemdesc.Name = "dfItemdesc";
			this.dfItemdesc.Popis = "Název:";
			this.dfItemdesc.PopisDock = System.Windows.Forms.DockStyle.Left;
			this.dfItemdesc.PopisHeight = 51;
			// 
			// dfItemnmbr
			// 
			resources.ApplyResources(this.dfItemnmbr, "dfItemnmbr");
			this.dfItemnmbr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dfItemnmbr.Data = "";
			this.dfItemnmbr.Name = "dfItemnmbr";
			this.dfItemnmbr.Popis = "Pol. è.:";
			this.dfItemnmbr.PopisDock = System.Windows.Forms.DockStyle.Left;
			this.dfItemnmbr.PopisHeight = 20;
			// 
			// VydejDialogPocetVydavanych
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			resources.ApplyResources(this, "$this");
			this.ControlBox = false;
			this.Controls.Add(this.panelData);
			this.Controls.Add(this.panelButtons);
			this.KeyPreview = true;
			this.Name = "VydejDialogPocetVydavanych";
			this.Load += new System.EventHandler(this.VydejDialogPocetVydavanych_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VydejDialogPocetVydavanych_KeyDown);
			this.panelButtons.ResumeLayout(false);
			this.panelData.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private GraphicButton bOK;
        public GraphicButton bStorno;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Panel panelData;
        private DataField4 dfItemnmbr;
		private DataField4 dfItemdesc;
        private DataField dfPocetJednotek;
		private DataField4 dfZadano;
		private DataField4 dfitemcode;
    }
}