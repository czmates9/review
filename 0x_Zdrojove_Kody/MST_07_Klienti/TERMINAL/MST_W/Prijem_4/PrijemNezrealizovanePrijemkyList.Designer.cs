namespace Fask.MST_W.Prijem_4
{
    partial class PrijemNezrealizovanePrijemkyList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrijemNezrealizovanePrijemkyList));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.miNajitPonumber = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.miPrerusit = new System.Windows.Forms.MenuItem();
			this.miMenuZobrazitSeznam = new System.Windows.Forms.MenuItem();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.ds = new Fask.MST_W.PrijemService.Obecne();
			this.panelButtons = new System.Windows.Forms.Panel();
			this.zpet_but = new Fask.Graphic.GraphicButton();
			this.ok_but = new Fask.Graphic.GraphicButton();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.dataGrid1 = new Fask.Graphic.DataGrid2();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumn_Name = new Fask.Graphic.DataGrid2TextBoxColumn();
			this.dataGridTextBoxColumn_PONUMBER = new Fask.Graphic.DataGrid2TextBoxColumn();
			this.dataGridTextBoxColumn_CZ_CarKod = new Fask.Graphic.DataGrid2TextBoxColumn();
			this.dataGridTextBoxColumn_DateTime = new Fask.Graphic.DataGrid2TextBoxColumn();
			this.dataGridTextBoxColumn_Desc = new Fask.Graphic.DataGrid2TextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
			this.panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem2);
			this.menuItem1.MenuItems.Add(this.menuItem3);
			this.menuItem1.MenuItems.Add(this.menuItem4);
			this.menuItem1.MenuItems.Add(this.miPrerusit);
			this.menuItem1.MenuItems.Add(this.miMenuZobrazitSeznam);
			resources.ApplyResources(this.menuItem1, "menuItem1");
			// 
			// menuItem2
			// 
			this.menuItem2.MenuItems.Add(this.miNajitPonumber);
			resources.ApplyResources(this.menuItem2, "menuItem2");
			// 
			// miNajitPonumber
			// 
			resources.ApplyResources(this.miNajitPonumber, "miNajitPonumber");
			this.miNajitPonumber.Click += new System.EventHandler(this.miNajitPonumber_Click);
			// 
			// menuItem3
			// 
			resources.ApplyResources(this.menuItem3, "menuItem3");
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			resources.ApplyResources(this.menuItem4, "menuItem4");
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// miPrerusit
			// 
			resources.ApplyResources(this.miPrerusit, "miPrerusit");
			this.miPrerusit.Click += new System.EventHandler(this.miKonec_Click);
			// 
			// miMenuZobrazitSeznam
			// 
			resources.ApplyResources(this.miMenuZobrazitSeznam, "miMenuZobrazitSeznam");
			this.miMenuZobrazitSeznam.Click += new System.EventHandler(this.miMenuZobrazitSeznam_Click);
			// 
			// bindingSource1
			// 
			this.bindingSource1.DataMember = "Prijemky";
			this.bindingSource1.DataSource = this.ds;
			this.bindingSource1.Sort = "";
			// 
			// ds
			// 
			this.ds.DataSetName = "Obecne";
			this.ds.Prefix = "";
			this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// panelButtons
			// 
			this.panelButtons.Controls.Add(this.zpet_but);
			this.panelButtons.Controls.Add(this.ok_but);
			resources.ApplyResources(this.panelButtons, "panelButtons");
			this.panelButtons.Name = "panelButtons";
			this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
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
			// statusBar1
			// 
			resources.ApplyResources(this.statusBar1, "statusBar1");
			this.statusBar1.Name = "statusBar1";
			// 
			// dataGrid1
			// 
			this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
			this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.dataGrid1.CurrentRow = null;
			this.dataGrid1.DataSource = this.bindingSource1;
			resources.ApplyResources(this.dataGrid1, "dataGrid1");
			this.dataGrid1.HeaderBackColorSelected = System.Drawing.Color.Green;
			this.dataGrid1.HeaderForeColorSelected = System.Drawing.Color.Brown;
			this.dataGrid1.KeyScrollDown = System.Windows.Forms.Keys.D5;
			this.dataGrid1.KeyScrollUp = System.Windows.Forms.Keys.D2;
			this.dataGrid1.MultiSelect = false;
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.NumberFormat = "N";
			this.dataGrid1.RowHeightDefault = 23;
			this.dataGrid1.Sort = "";
			this.dataGrid1.SortByHeaderDoubleClick = true;
			this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
			this.dataGrid1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGrid1_KeyPress);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn_Name);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn_PONUMBER);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn_CZ_CarKod);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn_DateTime);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn_Desc);
			this.dataGridTableStyle1.MappingName = "Prijemky";
			// 
			// dataGridTextBoxColumn_Name
			// 
			this.dataGridTextBoxColumn_Name.Alignment = System.Drawing.StringAlignment.Near;
			this.dataGridTextBoxColumn_Name.Format = "";
			this.dataGridTextBoxColumn_Name.FormatInfo = null;
			resources.ApplyResources(this.dataGridTextBoxColumn_Name, "dataGridTextBoxColumn_Name");
			this.dataGridTextBoxColumn_Name.LineAlignment = System.Drawing.StringAlignment.Near;
			this.dataGridTextBoxColumn_Name.SelectionShow = false;
			this.dataGridTextBoxColumn_Name.Tag = "";
			// 
			// dataGridTextBoxColumn_PONUMBER
			// 
			this.dataGridTextBoxColumn_PONUMBER.Alignment = System.Drawing.StringAlignment.Near;
			this.dataGridTextBoxColumn_PONUMBER.Format = "";
			this.dataGridTextBoxColumn_PONUMBER.FormatInfo = null;
			resources.ApplyResources(this.dataGridTextBoxColumn_PONUMBER, "dataGridTextBoxColumn_PONUMBER");
			this.dataGridTextBoxColumn_PONUMBER.LineAlignment = System.Drawing.StringAlignment.Near;
			this.dataGridTextBoxColumn_PONUMBER.SelectionShow = false;
			this.dataGridTextBoxColumn_PONUMBER.Tag = "";
			// 
			// dataGridTextBoxColumn_CZ_CarKod
			// 
			this.dataGridTextBoxColumn_CZ_CarKod.Alignment = System.Drawing.StringAlignment.Near;
			this.dataGridTextBoxColumn_CZ_CarKod.Format = "";
			this.dataGridTextBoxColumn_CZ_CarKod.FormatInfo = null;
			resources.ApplyResources(this.dataGridTextBoxColumn_CZ_CarKod, "dataGridTextBoxColumn_CZ_CarKod");
			this.dataGridTextBoxColumn_CZ_CarKod.LineAlignment = System.Drawing.StringAlignment.Near;
			this.dataGridTextBoxColumn_CZ_CarKod.SelectionShow = false;
			this.dataGridTextBoxColumn_CZ_CarKod.Tag = "";
			// 
			// dataGridTextBoxColumn_DateTime
			// 
			this.dataGridTextBoxColumn_DateTime.Alignment = System.Drawing.StringAlignment.Near;
			this.dataGridTextBoxColumn_DateTime.Format = "";
			this.dataGridTextBoxColumn_DateTime.FormatInfo = null;
			resources.ApplyResources(this.dataGridTextBoxColumn_DateTime, "dataGridTextBoxColumn_DateTime");
			this.dataGridTextBoxColumn_DateTime.LineAlignment = System.Drawing.StringAlignment.Near;
			this.dataGridTextBoxColumn_DateTime.SelectionShow = false;
			this.dataGridTextBoxColumn_DateTime.Tag = "";
			// 
			// dataGridTextBoxColumn_Desc
			// 
			this.dataGridTextBoxColumn_Desc.Alignment = System.Drawing.StringAlignment.Near;
			this.dataGridTextBoxColumn_Desc.Format = "";
			this.dataGridTextBoxColumn_Desc.FormatInfo = null;
			resources.ApplyResources(this.dataGridTextBoxColumn_Desc, "dataGridTextBoxColumn_Desc");
			this.dataGridTextBoxColumn_Desc.LineAlignment = System.Drawing.StringAlignment.Near;
			this.dataGridTextBoxColumn_Desc.SelectionShow = false;
			this.dataGridTextBoxColumn_Desc.Tag = "";
			// 
			// PrijemNezrealizovanePrijemkyList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			resources.ApplyResources(this, "$this");
			this.ControlBox = false;
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.panelButtons);
			this.KeyPreview = true;
			this.Menu = this.mainMenu1;
			this.Name = "PrijemNezrealizovanePrijemkyList";
			this.Load += new System.EventHandler(this.ServisDynamickaTabulka_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServisDynamickaTabulka_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
			this.panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miPrerusit;
        private Fask.Graphic.DataGrid2 dataGrid1;
        protected System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton zpet_but;
        private Fask.Graphic.GraphicButton ok_but;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miNajitPonumber;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Fask.MST_W.PrijemService.Obecne ds;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn_Name;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn_PONUMBER;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn_CZ_CarKod;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn_DateTime;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn_Desc;
        private System.Windows.Forms.MenuItem miMenuZobrazitSeznam;
    }
}