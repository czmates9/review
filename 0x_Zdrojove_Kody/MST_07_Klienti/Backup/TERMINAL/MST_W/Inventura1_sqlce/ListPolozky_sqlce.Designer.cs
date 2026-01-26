using System.Windows.Forms;
namespace Fask.MST_W.Inventura1_sqlce
{
    partial class ListPolozky_sqlce
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListPolozky_sqlce));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemOnline = new System.Windows.Forms.MenuItem();
			this.menuItemOnlineNovyEAN = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItemHledatCarKod = new System.Windows.Forms.MenuItem();
			this.menuItemHledatPozice = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.miTisk = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItemPridat = new System.Windows.Forms.MenuItem();
			this.menuItemSmazat = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItemKonec = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemZobrazeniVse = new System.Windows.Forms.MenuItem();
			this.menuItemZobrazeniSwitchRezim = new System.Windows.Forms.MenuItem();
			this.menuItemNasnimane = new System.Windows.Forms.MenuItem();
			this.panelDetail = new System.Windows.Forms.Panel();
			this.dataFieldITEMCODE = new Fask.Graphic.DataField();
			this.dataFieldREZ2 = new Fask.Graphic.DataField();
			this.dataFieldREZ1 = new Fask.Graphic.DataField();
			this.dataFieldNasnimano = new Fask.Graphic.DataField();
			this.dataFieldSNFind = new Fask.Graphic.DataField();
			this.dataFieldSNTrack = new Fask.Graphic.DataField();
			this.dataFieldCarKodVlastni = new Fask.Graphic.DataField();
			this.dataFieldSklad = new Fask.Graphic.DataField();
			this.dataFieldLocnCode = new Fask.Graphic.DataField();
			this.dataFieldQUANTITY = new Fask.Graphic.DataField();
			this.dataFieldITEMDESC = new Fask.Graphic.DataField();
			this.dataFieldItemnmbr = new Fask.Graphic.DataField();
			this.sbInfo = new System.Windows.Forms.StatusBar();
			this.panelGrid = new System.Windows.Forms.Panel();
			this.dataGrid = new Fask.Graphic.DataGrid2();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.toolBarButtonFirst = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonPrev = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonNext = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonLast = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList();
			this.panelDetail.SuspendLayout();
			this.panelGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.menuItem2);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItemOnline);
			this.menuItem1.MenuItems.Add(this.menuItem7);
			this.menuItem1.MenuItems.Add(this.menuItem5);
			this.menuItem1.MenuItems.Add(this.menuItem3);
			this.menuItem1.MenuItems.Add(this.miTisk);
			this.menuItem1.MenuItems.Add(this.menuItem9);
			this.menuItem1.MenuItems.Add(this.menuItemPridat);
			this.menuItem1.MenuItems.Add(this.menuItemSmazat);
			this.menuItem1.MenuItems.Add(this.menuItem4);
			this.menuItem1.MenuItems.Add(this.menuItemKonec);
			resources.ApplyResources(this.menuItem1, "menuItem1");
			// 
			// menuItemOnline
			// 
			this.menuItemOnline.MenuItems.Add(this.menuItemOnlineNovyEAN);
			resources.ApplyResources(this.menuItemOnline, "menuItemOnline");
			// 
			// menuItemOnlineNovyEAN
			// 
			resources.ApplyResources(this.menuItemOnlineNovyEAN, "menuItemOnlineNovyEAN");
			this.menuItemOnlineNovyEAN.Click += new System.EventHandler(this.menuItemOnlineNovyEAN_Click);
			// 
			// menuItem7
			// 
			resources.ApplyResources(this.menuItem7, "menuItem7");
			// 
			// menuItem5
			// 
			this.menuItem5.MenuItems.Add(this.menuItem6);
			this.menuItem5.MenuItems.Add(this.menuItemHledatCarKod);
			this.menuItem5.MenuItems.Add(this.menuItemHledatPozice);
			this.menuItem5.MenuItems.Add(this.menuItem8);
			resources.ApplyResources(this.menuItem5, "menuItem5");
			// 
			// menuItem6
			// 
			resources.ApplyResources(this.menuItem6, "menuItem6");
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItemHledatCarKod
			// 
			resources.ApplyResources(this.menuItemHledatCarKod, "menuItemHledatCarKod");
			this.menuItemHledatCarKod.Click += new System.EventHandler(this.menuItemHledatCarKod_Click);
			// 
			// menuItemHledatPozice
			// 
			resources.ApplyResources(this.menuItemHledatPozice, "menuItemHledatPozice");
			this.menuItemHledatPozice.Click += new System.EventHandler(this.menuItemHledatPozice_Click);
			// 
			// menuItem8
			// 
			resources.ApplyResources(this.menuItem8, "menuItem8");
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem3
			// 
			resources.ApplyResources(this.menuItem3, "menuItem3");
			// 
			// miTisk
			// 
			resources.ApplyResources(this.miTisk, "miTisk");
			this.miTisk.Click += new System.EventHandler(this.menuItemTisk_Click);
			// 
			// menuItem9
			// 
			resources.ApplyResources(this.menuItem9, "menuItem9");
			// 
			// menuItemPridat
			// 
			resources.ApplyResources(this.menuItemPridat, "menuItemPridat");
			this.menuItemPridat.Click += new System.EventHandler(this.buttonZadat_Click);
			// 
			// menuItemSmazat
			// 
			resources.ApplyResources(this.menuItemSmazat, "menuItemSmazat");
			this.menuItemSmazat.Click += new System.EventHandler(this.menuItemSmazat_Click);
			// 
			// menuItem4
			// 
			resources.ApplyResources(this.menuItem4, "menuItem4");
			// 
			// menuItemKonec
			// 
			resources.ApplyResources(this.menuItemKonec, "menuItemKonec");
			this.menuItemKonec.Click += new System.EventHandler(this.buttonKonec_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.MenuItems.Add(this.menuItemZobrazeniVse);
			this.menuItem2.MenuItems.Add(this.menuItemZobrazeniSwitchRezim);
			this.menuItem2.MenuItems.Add(this.menuItemNasnimane);
			resources.ApplyResources(this.menuItem2, "menuItem2");
			// 
			// menuItemZobrazeniVse
			// 
			resources.ApplyResources(this.menuItemZobrazeniVse, "menuItemZobrazeniVse");
			this.menuItemZobrazeniVse.Click += new System.EventHandler(this.menuItemZobrazeniVse_Click);
			// 
			// menuItemZobrazeniSwitchRezim
			// 
			resources.ApplyResources(this.menuItemZobrazeniSwitchRezim, "menuItemZobrazeniSwitchRezim");
			this.menuItemZobrazeniSwitchRezim.Click += new System.EventHandler(this.menuItemZobrazeniSwitchRezim_Click);
			// 
			// menuItemNasnimane
			// 
			resources.ApplyResources(this.menuItemNasnimane, "menuItemNasnimane");
			this.menuItemNasnimane.Click += new System.EventHandler(this.menuItemNasnimane_Click);
			// 
			// panelDetail
			// 
			this.panelDetail.Controls.Add(this.dataFieldITEMCODE);
			this.panelDetail.Controls.Add(this.dataFieldREZ2);
			this.panelDetail.Controls.Add(this.dataFieldREZ1);
			this.panelDetail.Controls.Add(this.dataFieldNasnimano);
			this.panelDetail.Controls.Add(this.dataFieldSNFind);
			this.panelDetail.Controls.Add(this.dataFieldSNTrack);
			this.panelDetail.Controls.Add(this.dataFieldCarKodVlastni);
			this.panelDetail.Controls.Add(this.dataFieldSklad);
			this.panelDetail.Controls.Add(this.dataFieldLocnCode);
			this.panelDetail.Controls.Add(this.dataFieldQUANTITY);
			this.panelDetail.Controls.Add(this.dataFieldITEMDESC);
			this.panelDetail.Controls.Add(this.dataFieldItemnmbr);
			resources.ApplyResources(this.panelDetail, "panelDetail");
			this.panelDetail.Name = "panelDetail";
			// 
			// dataFieldITEMCODE
			// 
			this.dataFieldITEMCODE.Data = "";
			this.dataFieldITEMCODE.DataBackColor = System.Drawing.SystemColors.Window;
			this.dataFieldITEMCODE.DataMaxLength = 32767;
			this.dataFieldITEMCODE.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
			resources.ApplyResources(this.dataFieldITEMCODE, "dataFieldITEMCODE");
			this.dataFieldITEMCODE.MultiLine = false;
			this.dataFieldITEMCODE.Name = "dataFieldITEMCODE";
			this.dataFieldITEMCODE.Popis = "Kód položky";
			this.dataFieldITEMCODE.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
			this.dataFieldITEMCODE.PopisWidth = 100;
			this.dataFieldITEMCODE.ReadOnly = true;
			// 
			// dataFieldREZ2
			// 
			this.dataFieldREZ2.Data = "";
			this.dataFieldREZ2.DataBackColor = System.Drawing.SystemColors.Window;
			this.dataFieldREZ2.DataMaxLength = 32767;
			this.dataFieldREZ2.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
			resources.ApplyResources(this.dataFieldREZ2, "dataFieldREZ2");
			this.dataFieldREZ2.MultiLine = false;
			this.dataFieldREZ2.Name = "dataFieldREZ2";
			this.dataFieldREZ2.Popis = "REZ2";
			this.dataFieldREZ2.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
			this.dataFieldREZ2.PopisWidth = 100;
			this.dataFieldREZ2.ReadOnly = true;
			// 
			// dataFieldREZ1
			// 
			this.dataFieldREZ1.Data = "";
			this.dataFieldREZ1.DataBackColor = System.Drawing.SystemColors.Window;
			this.dataFieldREZ1.DataMaxLength = 32767;
			this.dataFieldREZ1.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
			resources.ApplyResources(this.dataFieldREZ1, "dataFieldREZ1");
			this.dataFieldREZ1.MultiLine = false;
			this.dataFieldREZ1.Name = "dataFieldREZ1";
			this.dataFieldREZ1.Popis = "REZ1";
			this.dataFieldREZ1.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
			this.dataFieldREZ1.PopisWidth = 100;
			this.dataFieldREZ1.ReadOnly = true;
			// 
			// dataFieldNasnimano
			// 
			this.dataFieldNasnimano.Data = "";
			this.dataFieldNasnimano.DataBackColor = System.Drawing.SystemColors.Window;
			this.dataFieldNasnimano.DataMaxLength = 32767;
			this.dataFieldNasnimano.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
			resources.ApplyResources(this.dataFieldNasnimano, "dataFieldNasnimano");
			this.dataFieldNasnimano.MultiLine = false;
			this.dataFieldNasnimano.Name = "dataFieldNasnimano";
			this.dataFieldNasnimano.Popis = "Nasnímáno";
			this.dataFieldNasnimano.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
			this.dataFieldNasnimano.PopisWidth = 100;
			this.dataFieldNasnimano.ReadOnly = true;
			// 
			// dataFieldSNFind
			// 
			this.dataFieldSNFind.Data = "";
			this.dataFieldSNFind.DataBackColor = System.Drawing.SystemColors.Window;
			this.dataFieldSNFind.DataMaxLength = 32767;
			this.dataFieldSNFind.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
			resources.ApplyResources(this.dataFieldSNFind, "dataFieldSNFind");
			this.dataFieldSNFind.MultiLine = false;
			this.dataFieldSNFind.Name = "dataFieldSNFind";
			this.dataFieldSNFind.Popis = "Dohledávat SN";
			this.dataFieldSNFind.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
			this.dataFieldSNFind.PopisWidth = 100;
			this.dataFieldSNFind.ReadOnly = true;
			// 
			// dataFieldSNTrack
			// 
			this.dataFieldSNTrack.Data = "";
			this.dataFieldSNTrack.DataBackColor = System.Drawing.SystemColors.Window;
			this.dataFieldSNTrack.DataMaxLength = 32767;
			this.dataFieldSNTrack.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
			resources.ApplyResources(this.dataFieldSNTrack, "dataFieldSNTrack");
			this.dataFieldSNTrack.MultiLine = false;
			this.dataFieldSNTrack.Name = "dataFieldSNTrack";
			this.dataFieldSNTrack.Popis = "Sledovat SN";
			this.dataFieldSNTrack.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
			this.dataFieldSNTrack.PopisWidth = 100;
			this.dataFieldSNTrack.ReadOnly = true;
			// 
			// dataFieldCarKodVlastni
			// 
			this.dataFieldCarKodVlastni.Data = "";
			this.dataFieldCarKodVlastni.DataBackColor = System.Drawing.SystemColors.Window;
			this.dataFieldCarKodVlastni.DataMaxLength = 32767;
			this.dataFieldCarKodVlastni.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
			resources.ApplyResources(this.dataFieldCarKodVlastni, "dataFieldCarKodVlastni");
			this.dataFieldCarKodVlastni.MultiLine = false;
			this.dataFieldCarKodVlastni.Name = "dataFieldCarKodVlastni";
			this.dataFieldCarKodVlastni.Popis = "Èár. kód vlastní";
			this.dataFieldCarKodVlastni.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
			this.dataFieldCarKodVlastni.PopisWidth = 100;
			this.dataFieldCarKodVlastni.ReadOnly = true;
			// 
			// dataFieldSklad
			// 
			this.dataFieldSklad.Data = "";
			this.dataFieldSklad.DataBackColor = System.Drawing.SystemColors.Window;
			this.dataFieldSklad.DataMaxLength = 32767;
			this.dataFieldSklad.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
			resources.ApplyResources(this.dataFieldSklad, "dataFieldSklad");
			this.dataFieldSklad.MultiLine = false;
			this.dataFieldSklad.Name = "dataFieldSklad";
			this.dataFieldSklad.Popis = "Sklad";
			this.dataFieldSklad.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
			this.dataFieldSklad.PopisWidth = 100;
			this.dataFieldSklad.ReadOnly = true;
			// 
			// dataFieldLocnCode
			// 
			this.dataFieldLocnCode.Data = "";
			this.dataFieldLocnCode.DataBackColor = System.Drawing.SystemColors.Window;
			this.dataFieldLocnCode.DataMaxLength = 32767;
			this.dataFieldLocnCode.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
			resources.ApplyResources(this.dataFieldLocnCode, "dataFieldLocnCode");
			this.dataFieldLocnCode.MultiLine = false;
			this.dataFieldLocnCode.Name = "dataFieldLocnCode";
			this.dataFieldLocnCode.Popis = "Lokace";
			this.dataFieldLocnCode.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
			this.dataFieldLocnCode.PopisWidth = 100;
			this.dataFieldLocnCode.ReadOnly = true;
			// 
			// dataFieldQUANTITY
			// 
			this.dataFieldQUANTITY.Data = "";
			this.dataFieldQUANTITY.DataBackColor = System.Drawing.SystemColors.Window;
			this.dataFieldQUANTITY.DataMaxLength = 32767;
			this.dataFieldQUANTITY.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
			resources.ApplyResources(this.dataFieldQUANTITY, "dataFieldQUANTITY");
			this.dataFieldQUANTITY.MultiLine = false;
			this.dataFieldQUANTITY.Name = "dataFieldQUANTITY";
			this.dataFieldQUANTITY.Popis = "Množství";
			this.dataFieldQUANTITY.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
			this.dataFieldQUANTITY.PopisWidth = 100;
			this.dataFieldQUANTITY.ReadOnly = true;
			// 
			// dataFieldITEMDESC
			// 
			this.dataFieldITEMDESC.Data = "";
			this.dataFieldITEMDESC.DataBackColor = System.Drawing.SystemColors.Window;
			this.dataFieldITEMDESC.DataMaxLength = 32767;
			this.dataFieldITEMDESC.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
			resources.ApplyResources(this.dataFieldITEMDESC, "dataFieldITEMDESC");
			this.dataFieldITEMDESC.MultiLine = true;
			this.dataFieldITEMDESC.Name = "dataFieldITEMDESC";
			this.dataFieldITEMDESC.Popis = "Název";
			this.dataFieldITEMDESC.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
			this.dataFieldITEMDESC.PopisWidth = 100;
			this.dataFieldITEMDESC.ReadOnly = true;
			// 
			// dataFieldItemnmbr
			// 
			this.dataFieldItemnmbr.Data = "";
			this.dataFieldItemnmbr.DataBackColor = System.Drawing.SystemColors.Window;
			this.dataFieldItemnmbr.DataMaxLength = 32767;
			this.dataFieldItemnmbr.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
			resources.ApplyResources(this.dataFieldItemnmbr, "dataFieldItemnmbr");
			this.dataFieldItemnmbr.MultiLine = false;
			this.dataFieldItemnmbr.Name = "dataFieldItemnmbr";
			this.dataFieldItemnmbr.Popis = "Èíslo";
			this.dataFieldItemnmbr.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
			this.dataFieldItemnmbr.PopisWidth = 100;
			this.dataFieldItemnmbr.ReadOnly = true;
			// 
			// sbInfo
			// 
			resources.ApplyResources(this.sbInfo, "sbInfo");
			this.sbInfo.Name = "sbInfo";
			// 
			// panelGrid
			// 
			this.panelGrid.Controls.Add(this.dataGrid);
			resources.ApplyResources(this.panelGrid, "panelGrid");
			this.panelGrid.Name = "panelGrid";
			// 
			// dataGrid
			// 
			this.dataGrid.BackColorAlternating = System.Drawing.Color.Gold;
			this.dataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.dataGrid.CurrentRow = null;
			resources.ApplyResources(this.dataGrid, "dataGrid");
			this.dataGrid.HeaderBackColorSelected = System.Drawing.Color.Green;
			this.dataGrid.HeaderForeColorSelected = System.Drawing.Color.Brown;
			this.dataGrid.KeyScrollDown = System.Windows.Forms.Keys.D5;
			this.dataGrid.KeyScrollUp = System.Windows.Forms.Keys.D2;
			this.dataGrid.MultiSelect = false;
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.NumberFormat = "N";
			this.dataGrid.RowHeightDefault = 23;
			this.dataGrid.Sort = "";
			this.dataGrid.SortByHeaderDoubleClick = true;
			this.dataGrid.CurrentCellChanged += new System.EventHandler(this.dataGrid_CurrentCellChanged);
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
			// ListPolozky_sqlce
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			resources.ApplyResources(this, "$this");
			this.ControlBox = false;
			this.Controls.Add(this.toolBar1);
			this.Controls.Add(this.panelGrid);
			this.Controls.Add(this.sbInfo);
			this.Controls.Add(this.panelDetail);
			this.KeyPreview = true;
			this.Menu = this.mainMenu1;
			this.Name = "ListPolozky_sqlce";
			this.Deactivate += new System.EventHandler(this.ListPolozky_sqlce_Deactivate);
			this.Load += new System.EventHandler(this.ListPolozky_Load);
			this.Activated += new System.EventHandler(this.ListPolozky_sqlce_Activated);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ListPolozky_Closing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListPolozky_KeyDown);
			this.panelDetail.ResumeLayout(false);
			this.panelGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private MainMenu mainMenu1;
        private MenuItem menuItem1;
        private MenuItem menuItemPridat;
        private MenuItem menuItem4;
        private MenuItem menuItemKonec;
        private Panel panelDetail;
        private Fask.Graphic.DataField dataFieldITEMDESC;
        private Fask.Graphic.DataField dataFieldItemnmbr;
        private Fask.Graphic.DataField dataFieldSNTrack;
        private Fask.Graphic.DataField dataFieldQUANTITY;
        private Fask.Graphic.DataField dataFieldLocnCode;
        private Fask.Graphic.DataField dataFieldSNFind;
        private StatusBar sbInfo;
        private Panel panelGrid;
        private MenuItem menuItem2;
        private MenuItem menuItemZobrazeniSwitchRezim;
        private Fask.Graphic.DataGrid2 dataGrid;
        private MenuItem menuItemZobrazeniVse;
        private Fask.Graphic.DataField dataFieldCarKodVlastni;
        private Fask.Graphic.DataField dataFieldNasnimano;
        private MenuItem menuItem5;
        private MenuItem menuItem6;
        private MenuItem menuItemHledatCarKod;
        private MenuItem menuItemNasnimane;
        private ToolBar toolBar1;
        private ToolBarButton toolBarButtonPrev;
        private ToolBarButton toolBarButtonNext;
        private ImageList imageList1;
        private MenuItem menuItemSmazat;
        private MenuItem menuItemHledatPozice;
        private MenuItem menuItem3;
        private ToolBarButton toolBarButtonFirst;
        private ToolBarButton toolBarButtonLast;
        private Fask.Graphic.DataField dataFieldSklad;
        private MenuItem menuItemOnline;
        private MenuItem menuItem7;
        private MenuItem menuItemOnlineNovyEAN;
        private Fask.Graphic.DataField dataFieldREZ2;
        private Fask.Graphic.DataField dataFieldREZ1;
        private Fask.Graphic.DataField dataFieldITEMCODE;
        private MenuItem menuItem8;
        private MenuItem miTisk;
        private MenuItem menuItem9;

    }
}