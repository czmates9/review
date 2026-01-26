namespace Fask.MST_W.ServisModule
{
    partial class ServisDynamickaTabulka
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServisDynamickaTabulka));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miDynamickaTabulka = new System.Windows.Forms.MenuItem();
            this.miSeradit = new System.Windows.Forms.MenuItem();
            this.miSortByID = new System.Windows.Forms.MenuItem();
            this.miSortByOznaceni = new System.Windows.Forms.MenuItem();
            this.miSortByBarcode = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.miPrerusit = new System.Windows.Forms.MenuItem();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.bsServis = new System.Windows.Forms.BindingSource(this.components);
            this.ds_servis = new Fask.SQLiteDBs.DataSets.Servis();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.zpet_but = new Fask.Graphic.GraphicButton();
            this.ok_but = new Fask.Graphic.GraphicButton();
            this.statusBarInfo = new System.Windows.Forms.StatusBar();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miScannerReactivate = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.bsServis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_servis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem5);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.miSeradit);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.miPrerusit);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItem3
            // 
            resources.ApplyResources(this.menuItem3, "menuItem3");
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.miDynamickaTabulka);
            resources.ApplyResources(this.menuItem2, "menuItem2");
            // 
            // miDynamickaTabulka
            // 
            resources.ApplyResources(this.miDynamickaTabulka, "miDynamickaTabulka");
            this.miDynamickaTabulka.Click += new System.EventHandler(this.miDynamickaTabulka_Click);
            // 
            // miSeradit
            // 
            this.miSeradit.MenuItems.Add(this.miSortByID);
            this.miSeradit.MenuItems.Add(this.miSortByOznaceni);
            this.miSeradit.MenuItems.Add(this.miSortByBarcode);
            resources.ApplyResources(this.miSeradit, "miSeradit");
            // 
            // miSortByID
            // 
            resources.ApplyResources(this.miSortByID, "miSortByID");
            this.miSortByID.Click += new System.EventHandler(this.miSordByID_Click);
            // 
            // miSortByOznaceni
            // 
            resources.ApplyResources(this.miSortByOznaceni, "miSortByOznaceni");
            this.miSortByOznaceni.Click += new System.EventHandler(this.miSortByOznaceni_Click);
            // 
            // miSortByBarcode
            // 
            resources.ApplyResources(this.miSortByBarcode, "miSortByBarcode");
            this.miSortByBarcode.Click += new System.EventHandler(this.miSortByBarcode_Click);
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
            // txtSearch
            // 
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged_1);
            // 
            // bsServis
            // 
            this.bsServis.AllowNew = false;
            this.bsServis.DataMember = "CZMST_Servis_Dynamic_Table";
            this.bsServis.DataSource = this.ds_servis;
            this.bsServis.Sort = "";
            // 
            // ds_servis
            // 
            this.ds_servis.DataSetName = "Servis";
            this.ds_servis.Locale = new System.Globalization.CultureInfo("");
            this.ds_servis.Prefix = "";
            this.ds_servis.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.DataSource = this.bsServis;
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
            // statusBarInfo
            // 
            resources.ApplyResources(this.statusBarInfo, "statusBarInfo");
            this.statusBarInfo.Name = "statusBarInfo";
            // 
            // menuItem5
            // 
            this.menuItem5.MenuItems.Add(this.miScannerReactivate);
            resources.ApplyResources(this.menuItem5, "menuItem5");
            // 
            // miScannerReactivate
            // 
            resources.ApplyResources(this.miScannerReactivate, "miScannerReactivate");
            this.miScannerReactivate.Click += new System.EventHandler(this.miScannerReactivate_Click);
            // 
            // ServisDynamickaTabulka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.statusBarInfo);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.txtSearch);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ServisDynamickaTabulka";
            this.Load += new System.EventHandler(this.ServisDynamickaTabulka_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServisDynamickaTabulka_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.bsServis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_servis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.BindingSource bsServis;
        private Fask.SQLiteDBs.DataSets.Servis ds_servis;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miPrerusit;
        private System.Windows.Forms.MenuItem miDynamickaTabulka;
        private System.Windows.Forms.MenuItem miSeradit;
        private System.Windows.Forms.MenuItem miSortByID;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem miSortByOznaceni;
        private System.Windows.Forms.MenuItem miSortByBarcode;
        private Fask.Graphic.DataGrid2 dataGrid1;
        protected System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton zpet_but;
        private Fask.Graphic.GraphicButton ok_but;
        private System.Windows.Forms.StatusBar statusBarInfo;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem miScannerReactivate;
    }
}