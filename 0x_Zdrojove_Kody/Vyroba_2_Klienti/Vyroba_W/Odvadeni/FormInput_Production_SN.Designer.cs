namespace Fask.Vyroba_W.Odvadeni
{
	partial class FormInput_Production_SN
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
			this.components = new System.ComponentModel.Container();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.mi_Menu = new System.Windows.Forms.MenuItem();
			this.mi_Smazat = new System.Windows.Forms.MenuItem();
			this.panelButtons = new System.Windows.Forms.Panel();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonStorno = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonVlozSN = new System.Windows.Forms.Button();
			this.tb_SN = new System.Windows.Forms.TextBox();
			this.mainMenu2 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.bs_ProductionSN = new System.Windows.Forms.BindingSource(this.components);
			this.dg_ProductionSN = new Fask.Graphic.DataGrid2();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dGTBColumn_ITEMNMBR = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dGTBColumn_SERLNMBR = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dGTBColumn_QTY = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dGTBColumn_Expirace = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dGTBColumn_REZ_1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dGTBColumn_REZ_2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dGTBColumn_REZ_3 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dGTBColumn_REZ_4 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.panelButtons.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bs_ProductionSN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dg_ProductionSN)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.mi_Menu);
			// 
			// mi_Menu
			// 
			this.mi_Menu.MenuItems.Add(this.mi_Smazat);
			this.mi_Menu.Text = "Menu";
			// 
			// mi_Smazat
			// 
			this.mi_Smazat.Text = "Smazat";
			this.mi_Smazat.Click += new System.EventHandler(this.mi_Smazat_Click);
			// 
			// panelButtons
			// 
			this.panelButtons.Controls.Add(this.buttonOK);
			this.panelButtons.Controls.Add(this.buttonStorno);
			this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelButtons.Location = new System.Drawing.Point(0, 248);
			this.panelButtons.Name = "panelButtons";
			this.panelButtons.Size = new System.Drawing.Size(238, 47);
			// 
			// buttonOK
			// 
			this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.buttonOK.Location = new System.Drawing.Point(120, 0);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(118, 47);
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonStorno
			// 
			this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
			this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.buttonStorno.Location = new System.Drawing.Point(0, 0);
			this.buttonStorno.Name = "buttonStorno";
			this.buttonStorno.Size = new System.Drawing.Size(120, 47);
			this.buttonStorno.TabIndex = 0;
			this.buttonStorno.Text = "Storno";
			this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.buttonVlozSN);
			this.panel1.Controls.Add(this.tb_SN);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 196);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(238, 52);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(5, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 23);
			this.label1.Text = "SN:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// buttonVlozSN
			// 
			this.buttonVlozSN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonVlozSN.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.buttonVlozSN.Location = new System.Drawing.Point(188, 8);
			this.buttonVlozSN.Name = "buttonVlozSN";
			this.buttonVlozSN.Size = new System.Drawing.Size(47, 35);
			this.buttonVlozSN.TabIndex = 1;
			this.buttonVlozSN.Text = "Vlož";
			this.buttonVlozSN.Click += new System.EventHandler(this.buttonVlozSN_Click);
			// 
			// tb_SN
			// 
			this.tb_SN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tb_SN.Location = new System.Drawing.Point(40, 14);
			this.tb_SN.Name = "tb_SN";
			this.tb_SN.Size = new System.Drawing.Size(142, 23);
			this.tb_SN.TabIndex = 0;
			// 
			// mainMenu2
			// 
			this.mainMenu2.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.Text = "Menu";
			// 
			// bs_ProductionSN
			// 
			this.bs_ProductionSN.Sort = "";
			// 
			// dg_ProductionSN
			// 
			this.dg_ProductionSN.BackColorAlternating = System.Drawing.Color.Gold;
			this.dg_ProductionSN.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.dg_ProductionSN.CurrentRow = null;
			this.dg_ProductionSN.DataSource = this.bs_ProductionSN;
			this.dg_ProductionSN.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dg_ProductionSN.HeaderBackColorSelected = System.Drawing.Color.Green;
			this.dg_ProductionSN.HeaderForeColorSelected = System.Drawing.Color.Brown;
			this.dg_ProductionSN.KeyScrollDown = System.Windows.Forms.Keys.D5;
			this.dg_ProductionSN.KeyScrollUp = System.Windows.Forms.Keys.D2;
			this.dg_ProductionSN.Location = new System.Drawing.Point(0, 0);
			this.dg_ProductionSN.MultiSelect = false;
			this.dg_ProductionSN.Name = "dg_ProductionSN";
			this.dg_ProductionSN.NumberFormat = "N";
			this.dg_ProductionSN.RowHeightDefault = 23;
			this.dg_ProductionSN.Size = new System.Drawing.Size(238, 196);
			this.dg_ProductionSN.Sort = "";
			this.dg_ProductionSN.SortByHeaderDoubleClick = true;
			this.dg_ProductionSN.TabIndex = 2;
			this.dg_ProductionSN.TableStyles.Add(this.dataGridTableStyle1);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_ITEMNMBR);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_SERLNMBR);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_QTY);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_Expirace);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_REZ_1);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_REZ_2);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_REZ_3);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_REZ_4);
			this.dataGridTableStyle1.MappingName = "Production_SN";
			// 
			// dGTBColumn_ITEMNMBR
			// 
			this.dGTBColumn_ITEMNMBR.Format = "";
			this.dGTBColumn_ITEMNMBR.FormatInfo = null;
			this.dGTBColumn_ITEMNMBR.HeaderText = "ID Položky";
			this.dGTBColumn_ITEMNMBR.MappingName = "ITEMNMBR";
			// 
			// dGTBColumn_SERLNMBR
			// 
			this.dGTBColumn_SERLNMBR.Format = "";
			this.dGTBColumn_SERLNMBR.FormatInfo = null;
			this.dGTBColumn_SERLNMBR.HeaderText = "SN";
			this.dGTBColumn_SERLNMBR.MappingName = "SERLNMBR";
			// 
			// dGTBColumn_QTY
			// 
			this.dGTBColumn_QTY.Format = "";
			this.dGTBColumn_QTY.FormatInfo = null;
			this.dGTBColumn_QTY.HeaderText = "Množství";
			this.dGTBColumn_QTY.MappingName = "QTY";
			// 
			// dGTBColumn_Expirace
			// 
			this.dGTBColumn_Expirace.Format = "";
			this.dGTBColumn_Expirace.FormatInfo = null;
			this.dGTBColumn_Expirace.HeaderText = "Expirace";
			this.dGTBColumn_Expirace.MappingName = "Expirace";
			// 
			// dGTBColumn_REZ_1
			// 
			this.dGTBColumn_REZ_1.Format = "";
			this.dGTBColumn_REZ_1.FormatInfo = null;
			this.dGTBColumn_REZ_1.HeaderText = "REZ 1";
			this.dGTBColumn_REZ_1.MappingName = "REZ_1";
			// 
			// dGTBColumn_REZ_2
			// 
			this.dGTBColumn_REZ_2.Format = "";
			this.dGTBColumn_REZ_2.FormatInfo = null;
			this.dGTBColumn_REZ_2.HeaderText = "REZ 2";
			this.dGTBColumn_REZ_2.MappingName = "REZ_2";
			// 
			// dGTBColumn_REZ_3
			// 
			this.dGTBColumn_REZ_3.Format = "";
			this.dGTBColumn_REZ_3.FormatInfo = null;
			this.dGTBColumn_REZ_3.HeaderText = "REZ 3";
			this.dGTBColumn_REZ_3.MappingName = "REZ_3";
			// 
			// dGTBColumn_REZ_4
			// 
			this.dGTBColumn_REZ_4.Format = "";
			this.dGTBColumn_REZ_4.FormatInfo = null;
			this.dGTBColumn_REZ_4.HeaderText = "REZ 4";
			this.dGTBColumn_REZ_4.MappingName = "REZ_4";
			// 
			// FormInput_Production_SN
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(238, 295);
			this.Controls.Add(this.dg_ProductionSN);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panelButtons);
			this.KeyPreview = true;
			this.Menu = this.mainMenu1;
			this.Name = "FormInput_Production_SN";
			this.Text = "SN počet : XX";
			this.Load += new System.EventHandler(this.FormInput_Production_SN_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormInput_Production_SN_KeyDown);
			this.panelButtons.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bs_ProductionSN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dg_ProductionSN)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.Panel panelButtons;
		public System.Windows.Forms.Button buttonOK;
		public System.Windows.Forms.Button buttonStorno;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonVlozSN;
		private System.Windows.Forms.TextBox tb_SN;
		private System.Windows.Forms.MainMenu mainMenu2;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mi_Menu;
		private Fask.Graphic.DataGrid2 dg_ProductionSN;
		private System.Windows.Forms.BindingSource bs_ProductionSN;
		private System.Windows.Forms.MenuItem mi_Smazat;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_SERLNMBR;
		private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_ITEMNMBR;
		private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_QTY;
		private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_Expirace;
		private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_REZ_1;
		private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_REZ_2;
		private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_REZ_3;
		private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_REZ_4;
	}
}