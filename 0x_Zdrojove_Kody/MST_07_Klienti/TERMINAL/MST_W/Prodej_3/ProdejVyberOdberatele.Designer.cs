namespace Fask.MST_W.Prodej_3
{
    partial class ProdejVyberOdberatele
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProdejVyberOdberatele));
			this.timerLoad = new System.Windows.Forms.Timer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemAktualize = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemNajit = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem_NajitCKodPol = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.mi_CarKodPartner = new System.Windows.Forms.MenuItem();
			this.mi_CarKodPolozky = new System.Windows.Forms.MenuItem();
			this.menuItem19 = new System.Windows.Forms.MenuItem();
			this.mi_SmazatVsechnyKody = new System.Windows.Forms.MenuItem();
			this.mi_SmazatPoslednyKod = new System.Windows.Forms.MenuItem();
			this.mi_KonScan = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.toolBarButtonFirst = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonPrev = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonNext = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonLast = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList();
			this.sbInfo = new System.Windows.Forms.StatusBar();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.dataGrid1 = new Fask.Graphic.DataGrid2();
			this.buttonStorno = new Fask.Graphic.GraphicButton();
			this.buttonOK = new Fask.Graphic.GraphicButton();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// timerLoad
			// 
			this.timerLoad.Tick += new System.EventHandler(this.ProdejVyberOdberatele_Shown);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonOK);
			this.panel1.Controls.Add(this.buttonStorno);
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItemAktualize);
			this.menuItem1.MenuItems.Add(this.menuItem15);
			this.menuItem1.MenuItems.Add(this.menuItem2);
			this.menuItem1.MenuItems.Add(this.menuItem8);
			this.menuItem1.MenuItems.Add(this.menuItem4);
			this.menuItem1.MenuItems.Add(this.menuItem18);
			this.menuItem1.MenuItems.Add(this.menuItem12);
			this.menuItem1.MenuItems.Add(this.menuItem19);
			this.menuItem1.MenuItems.Add(this.mi_KonScan);
			this.menuItem1.MenuItems.Add(this.menuItem7);
			this.menuItem1.MenuItems.Add(this.menuItem5);
			this.menuItem1.MenuItems.Add(this.menuItem6);
			resources.ApplyResources(this.menuItem1, "menuItem1");
			// 
			// menuItemAktualize
			// 
			resources.ApplyResources(this.menuItemAktualize, "menuItemAktualize");
			this.menuItemAktualize.Click += new System.EventHandler(this.menuItemAktualize_Click);
			// 
			// menuItem15
			// 
			resources.ApplyResources(this.menuItem15, "menuItem15");
			// 
			// menuItem2
			// 
			this.menuItem2.MenuItems.Add(this.menuItemNajit);
			this.menuItem2.MenuItems.Add(this.menuItem3);
			this.menuItem2.MenuItems.Add(this.menuItem_NajitCKodPol);
			this.menuItem2.MenuItems.Add(this.menuItem16);
			resources.ApplyResources(this.menuItem2, "menuItem2");
			// 
			// menuItemNajit
			// 
			resources.ApplyResources(this.menuItemNajit, "menuItemNajit");
			this.menuItemNajit.Click += new System.EventHandler(this.menuItemNajit_Click);
			// 
			// menuItem3
			// 
			resources.ApplyResources(this.menuItem3, "menuItem3");
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem_NajitCKodPol
			// 
			resources.ApplyResources(this.menuItem_NajitCKodPol, "menuItem_NajitCKodPol");
			// 
			// menuItem16
			// 
			resources.ApplyResources(this.menuItem16, "menuItem16");
			this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.MenuItems.Add(this.menuItem11);
			this.menuItem8.MenuItems.Add(this.menuItem10);
			this.menuItem8.MenuItems.Add(this.menuItem9);
			resources.ApplyResources(this.menuItem8, "menuItem8");
			// 
			// menuItem11
			// 
			resources.ApplyResources(this.menuItem11, "menuItem11");
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// menuItem10
			// 
			resources.ApplyResources(this.menuItem10, "menuItem10");
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// menuItem9
			// 
			resources.ApplyResources(this.menuItem9, "menuItem9");
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.MenuItems.Add(this.menuItem13);
			this.menuItem4.MenuItems.Add(this.menuItem14);
			resources.ApplyResources(this.menuItem4, "menuItem4");
			// 
			// menuItem13
			// 
			resources.ApplyResources(this.menuItem13, "menuItem13");
			this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
			// 
			// menuItem14
			// 
			resources.ApplyResources(this.menuItem14, "menuItem14");
			this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
			// 
			// menuItem18
			// 
			resources.ApplyResources(this.menuItem18, "menuItem18");
			// 
			// menuItem12
			// 
			this.menuItem12.MenuItems.Add(this.mi_CarKodPartner);
			this.menuItem12.MenuItems.Add(this.mi_CarKodPolozky);
			resources.ApplyResources(this.menuItem12, "menuItem12");
			// 
			// mi_CarKodPartner
			// 
			this.mi_CarKodPartner.Checked = true;
			resources.ApplyResources(this.mi_CarKodPartner, "mi_CarKodPartner");
			this.mi_CarKodPartner.Click += new System.EventHandler(this.mi_CarKodPartner_Click);
			// 
			// mi_CarKodPolozky
			// 
			resources.ApplyResources(this.mi_CarKodPolozky, "mi_CarKodPolozky");
			this.mi_CarKodPolozky.Click += new System.EventHandler(this.mi_CarKodPolozky_Click);
			// 
			// menuItem19
			// 
			this.menuItem19.MenuItems.Add(this.mi_SmazatVsechnyKody);
			this.menuItem19.MenuItems.Add(this.mi_SmazatPoslednyKod);
			resources.ApplyResources(this.menuItem19, "menuItem19");
			// 
			// mi_SmazatVsechnyKody
			// 
			resources.ApplyResources(this.mi_SmazatVsechnyKody, "mi_SmazatVsechnyKody");
			this.mi_SmazatVsechnyKody.Click += new System.EventHandler(this.mi_SmazatVsechnyKody_Click);
			// 
			// mi_SmazatPoslednyKod
			// 
			resources.ApplyResources(this.mi_SmazatPoslednyKod, "mi_SmazatPoslednyKod");
			this.mi_SmazatPoslednyKod.Click += new System.EventHandler(this.mi_SmazatPoslednyKod_Click);
			// 
			// mi_KonScan
			// 
			resources.ApplyResources(this.mi_KonScan, "mi_KonScan");
			this.mi_KonScan.Click += new System.EventHandler(this.mi_KonScan_Click);
			// 
			// menuItem7
			// 
			resources.ApplyResources(this.menuItem7, "menuItem7");
			// 
			// menuItem5
			// 
			resources.ApplyResources(this.menuItem5, "menuItem5");
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			resources.ApplyResources(this.menuItem6, "menuItem6");
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// toolBar1
			// 
			this.toolBar1.Buttons.Add(this.toolBarButtonFirst);
			this.toolBar1.Buttons.Add(this.toolBarButtonPrev);
			this.toolBar1.Buttons.Add(this.toolBarButtonNext);
			this.toolBar1.Buttons.Add(this.toolBarButtonLast);
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// toolBarButtonFirst
			// 
			resources.ApplyResources(this.toolBarButtonFirst, "toolBarButtonFirst");
			// 
			// toolBarButtonPrev
			// 
			resources.ApplyResources(this.toolBarButtonPrev, "toolBarButtonPrev");
			// 
			// toolBarButtonNext
			// 
			resources.ApplyResources(this.toolBarButtonNext, "toolBarButtonNext");
			// 
			// toolBarButtonLast
			// 
			resources.ApplyResources(this.toolBarButtonLast, "toolBarButtonLast");
			this.imageList1.Images.Clear();
			this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
			// 
			// sbInfo
			// 
			resources.ApplyResources(this.sbInfo, "sbInfo");
			this.sbInfo.Name = "sbInfo";
			// 
			// txtSearch
			// 
			resources.ApplyResources(this.txtSearch, "txtSearch");
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			this.txtSearch.GotFocus += new System.EventHandler(this.txtSearch_GotFocus);
			this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
			// 
			// dataGrid1
			// 
			this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
			this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.dataGrid1.CurrentRow = null;
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
			this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
			this.dataGrid1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid1_KeyDown);
			// 
			// buttonStorno
			// 
			this.buttonStorno.BitmapNormal = null;
			resources.ApplyResources(this.buttonStorno, "buttonStorno");
			this.buttonStorno.FocusMargin = 5;
			this.buttonStorno.Name = "buttonStorno";
			this.buttonStorno.Pressed = false;
			this.buttonStorno.Transparent = System.Drawing.Color.White;
			this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.BitmapNormal = null;
			resources.ApplyResources(this.buttonOK, "buttonOK");
			this.buttonOK.FocusMargin = 5;
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Pressed = false;
			this.buttonOK.Transparent = System.Drawing.Color.White;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// ProdejVyberOdberatele
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			resources.ApplyResources(this, "$this");
			this.ControlBox = false;
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.txtSearch);
			this.Controls.Add(this.sbInfo);
			this.Controls.Add(this.toolBar1);
			this.Controls.Add(this.panel1);
			this.KeyPreview = true;
			this.Menu = this.mainMenu1;
			this.Name = "ProdejVyberOdberatele";
			this.Deactivate += new System.EventHandler(this.ProdejVyberOdberatele_Deactivate);
			this.Load += new System.EventHandler(this.ProdejVyberOdberatele_Load);
			this.Activated += new System.EventHandler(this.ProdejVyberOdberatele_Activated);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ProdejVyberOdberatele_Closing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProdejVyberOdberatele_KeyDown);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton buttonStorno;
        private Fask.Graphic.GraphicButton buttonOK;
        private System.Windows.Forms.Timer timerLoad;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemNajit;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton toolBarButtonFirst;
        private System.Windows.Forms.ToolBarButton toolBarButtonPrev;
        private System.Windows.Forms.ToolBarButton toolBarButtonNext;
        private System.Windows.Forms.ToolBarButton toolBarButtonLast;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusBar sbInfo;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.MenuItem menuItemAktualize;
        private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem_NajitCKodPol;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem mi_CarKodPartner;
		private System.Windows.Forms.MenuItem mi_CarKodPolozky;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem mi_SmazatVsechnyKody;
		private System.Windows.Forms.MenuItem mi_SmazatPoslednyKod;
		private System.Windows.Forms.MenuItem mi_KonScan;
		private System.Windows.Forms.MenuItem menuItem16;
    }
}