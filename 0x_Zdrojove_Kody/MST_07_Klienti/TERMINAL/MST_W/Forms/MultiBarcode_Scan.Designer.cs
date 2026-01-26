namespace Fask.MST_W.Forms
{
	partial class MultiBarcode_Scan
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MainMenu mainMenu1;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiBarcode_Scan));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mi_OK = new System.Windows.Forms.MenuItem();
			this.mi_Storno = new System.Windows.Forms.MenuItem();
			this.panelButtons = new System.Windows.Forms.Panel();
			this.bOK = new Fask.Graphic.GraphicButton();
			this.bStorno = new Fask.Graphic.GraphicButton();
			this.l30 = new System.Windows.Forms.Label();
			this.l21 = new System.Windows.Forms.Label();
			this.l10 = new System.Windows.Forms.Label();
			this.l01 = new System.Windows.Forms.Label();
			this.L_01 = new System.Windows.Forms.Label();
			this.tb_Actual = new System.Windows.Forms.TextBox();
			this.L_21 = new System.Windows.Forms.Label();
			this.L_10 = new System.Windows.Forms.Label();
			this.L_30 = new System.Windows.Forms.Label();
			this.l17 = new System.Windows.Forms.Label();
			this.L_17 = new System.Windows.Forms.Label();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mi_Delete = new System.Windows.Forms.MenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.panelButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.mi_Delete);
			this.menuItem1.MenuItems.Add(this.menuItem2);
			this.menuItem1.MenuItems.Add(this.mi_OK);
			this.menuItem1.MenuItems.Add(this.mi_Storno);
			resources.ApplyResources(this.menuItem1, "menuItem1");
			// 
			// mi_OK
			// 
			resources.ApplyResources(this.mi_OK, "mi_OK");
			this.mi_OK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// mi_Storno
			// 
			resources.ApplyResources(this.mi_Storno, "mi_Storno");
			this.mi_Storno.Click += new System.EventHandler(this.bStorno_Click);
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
			// l30
			// 
			this.l30.ForeColor = System.Drawing.Color.Red;
			resources.ApplyResources(this.l30, "l30");
			this.l30.Name = "l30";
			// 
			// l21
			// 
			this.l21.ForeColor = System.Drawing.Color.Red;
			resources.ApplyResources(this.l21, "l21");
			this.l21.Name = "l21";
			// 
			// l10
			// 
			this.l10.ForeColor = System.Drawing.Color.Red;
			resources.ApplyResources(this.l10, "l10");
			this.l10.Name = "l10";
			// 
			// l01
			// 
			this.l01.ForeColor = System.Drawing.Color.Red;
			resources.ApplyResources(this.l01, "l01");
			this.l01.Name = "l01";
			// 
			// L_01
			// 
			resources.ApplyResources(this.L_01, "L_01");
			this.L_01.ForeColor = System.Drawing.Color.Red;
			this.L_01.Name = "L_01";
			// 
			// tb_Actual
			// 
			resources.ApplyResources(this.tb_Actual, "tb_Actual");
			this.tb_Actual.Name = "tb_Actual";
			this.tb_Actual.ReadOnly = true;
			// 
			// L_21
			// 
			resources.ApplyResources(this.L_21, "L_21");
			this.L_21.ForeColor = System.Drawing.Color.Red;
			this.L_21.Name = "L_21";
			// 
			// L_10
			// 
			resources.ApplyResources(this.L_10, "L_10");
			this.L_10.ForeColor = System.Drawing.Color.Red;
			this.L_10.Name = "L_10";
			// 
			// L_30
			// 
			resources.ApplyResources(this.L_30, "L_30");
			this.L_30.ForeColor = System.Drawing.Color.Red;
			this.L_30.Name = "L_30";
			// 
			// l17
			// 
			this.l17.ForeColor = System.Drawing.Color.Red;
			resources.ApplyResources(this.l17, "l17");
			this.l17.Name = "l17";
			// 
			// L_17
			// 
			resources.ApplyResources(this.L_17, "L_17");
			this.L_17.ForeColor = System.Drawing.Color.Red;
			this.L_17.Name = "L_17";
			// 
			// menuItem2
			// 
			resources.ApplyResources(this.menuItem2, "menuItem2");
			// 
			// mi_Delete
			// 
			resources.ApplyResources(this.mi_Delete, "mi_Delete");
			this.mi_Delete.Click += new System.EventHandler(this.mi_Delete_Click);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// MultiBarcode_Scan
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			resources.ApplyResources(this, "$this");
			this.ControlBox = false;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.l17);
			this.Controls.Add(this.L_17);
			this.Controls.Add(this.l30);
			this.Controls.Add(this.l21);
			this.Controls.Add(this.l10);
			this.Controls.Add(this.l01);
			this.Controls.Add(this.L_01);
			this.Controls.Add(this.tb_Actual);
			this.Controls.Add(this.L_21);
			this.Controls.Add(this.L_10);
			this.Controls.Add(this.L_30);
			this.Controls.Add(this.panelButtons);
			this.KeyPreview = true;
			this.Menu = this.mainMenu1;
			this.Name = "MultiBarcode_Scan";
			this.Load += new System.EventHandler(this.MultiBarcode_Scan_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MultiBarcode_Scan_KeyDown);
			this.panelButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.Panel panelButtons;
		public Fask.Graphic.GraphicButton bStorno;
		private Fask.Graphic.GraphicButton bOK;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mi_OK;
		private System.Windows.Forms.MenuItem mi_Storno;
		private System.Windows.Forms.Label l30;
		private System.Windows.Forms.Label l21;
		private System.Windows.Forms.Label l10;
		private System.Windows.Forms.Label l01;
		private System.Windows.Forms.Label L_01;
		private System.Windows.Forms.TextBox tb_Actual;
		private System.Windows.Forms.Label L_21;
		private System.Windows.Forms.Label L_10;
		private System.Windows.Forms.Label L_30;
		private System.Windows.Forms.Label l17;
		private System.Windows.Forms.Label L_17;
		private System.Windows.Forms.MenuItem mi_Delete;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Label label1;
	}
}