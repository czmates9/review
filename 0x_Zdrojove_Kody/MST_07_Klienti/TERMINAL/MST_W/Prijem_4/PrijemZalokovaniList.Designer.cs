namespace Fask.MST_W.Prijem_4
{
    partial class PrijemZalokovaniList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrijemZalokovaniList));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miZobrazitVse = new System.Windows.Forms.MenuItem();
            this.miNastavitLokaci = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.miPrerusit = new System.Windows.Forms.MenuItem();
            this.bs_prijem = new System.Windows.Forms.BindingSource(this.components);
            this.prijem = new Fask.SQLiteDBs.DataSets.Prijem();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.zpet_but = new Fask.Graphic.GraphicButton();
            this.ok_but = new Fask.Graphic.GraphicButton();
            this.sbInfo = new System.Windows.Forms.StatusBar();
            ((System.ComponentModel.ISupportInitialize)(this.bs_prijem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prijem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.miNastavitLokaci);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.miPrerusit);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.miZobrazitVse);
            resources.ApplyResources(this.menuItem2, "menuItem2");
            // 
            // miZobrazitVse
            // 
            resources.ApplyResources(this.miZobrazitVse, "miZobrazitVse");
            this.miZobrazitVse.Click += new System.EventHandler(this.miZobrazitVse_Click);
            // 
            // miNastavitLokaci
            // 
            resources.ApplyResources(this.miNastavitLokaci, "miNastavitLokaci");
            this.miNastavitLokaci.Click += new System.EventHandler(this.miNastavitLokaci_Click);
            // 
            // menuItem4
            // 
            resources.ApplyResources(this.menuItem4, "menuItem4");
            // 
            // miPrerusit
            // 
            resources.ApplyResources(this.miPrerusit, "miPrerusit");
            this.miPrerusit.Click += new System.EventHandler(this.miKonec_Click);
            // 
            // bs_prijem
            // 
            this.bs_prijem.DataMember = "CZMST_PI";
            this.bs_prijem.DataSource = this.prijem;
            this.bs_prijem.Sort = "";
            // 
            // prijem
            // 
            this.prijem.DataSetName = "Prijem";
            this.prijem.Prefix = "";
            this.prijem.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.DataSource = this.bs_prijem;
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
            this.dataGrid1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGrid1_KeyPress);
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
            // sbInfo
            // 
            resources.ApplyResources(this.sbInfo, "sbInfo");
            this.sbInfo.Name = "sbInfo";
            // 
            // PrijemZalokovaniList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.sbInfo);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "PrijemZalokovaniList";
            this.Load += new System.EventHandler(this.ServisDynamickaTabulka_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServisDynamickaTabulka_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.bs_prijem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prijem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miPrerusit;
        private Fask.Graphic.DataGrid2 dataGrid1;
        protected System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton zpet_but;
        private Fask.Graphic.GraphicButton ok_but;
        private Fask.SQLiteDBs.DataSets.Prijem prijem;
        private System.Windows.Forms.BindingSource bs_prijem;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miZobrazitVse;
        private System.Windows.Forms.StatusBar sbInfo;
        private System.Windows.Forms.MenuItem miNastavitLokaci;
        private System.Windows.Forms.MenuItem menuItem4;
    }
}