namespace Fask.MST_W.Inventura2
{
    partial class ZmenaLokace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZmenaLokace));
            this.buttonStorno = new Fask.Graphic.GraphicButton();
            this.buttonOK = new Fask.Graphic.GraphicButton();
            this.timerLoad = new System.Windows.Forms.Timer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemNajit = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButtonFirst = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonPrev = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonNext = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonLast = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.sbInfo = new System.Windows.Forms.StatusBar();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStorno
            // 
            this.buttonStorno.BitmapNormal = null;
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.FocusMargin = 5;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Pressed = false;
            this.buttonStorno.Size = new System.Drawing.Size(123, 47);
            this.buttonStorno.TabIndex = 2;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Transparent = System.Drawing.Color.White;
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.BitmapNormal = null;
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.FocusMargin = 5;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(123, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Pressed = false;
            this.buttonOK.Size = new System.Drawing.Size(145, 47);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Transparent = System.Drawing.Color.White;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // timerLoad
            // 
            this.timerLoad.Tick += new System.EventHandler(this.ZmenaLokace_Shown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Controls.Add(this.buttonStorno);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 236);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 47);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem8);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.menuItem7);
            this.menuItem1.MenuItems.Add(this.menuItem5);
            this.menuItem1.MenuItems.Add(this.menuItem12);
            this.menuItem1.MenuItems.Add(this.menuItem6);
            this.menuItem1.Text = "Položka";
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.menuItemNajit);
            this.menuItem2.MenuItems.Add(this.menuItem3);
            this.menuItem2.Text = "Hledat";
            // 
            // menuItemNajit
            // 
            this.menuItemNajit.Text = "F1 - Èár. kód";
            this.menuItemNajit.Click += new System.EventHandler(this.menuItemNajit_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "F2 - Název";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.MenuItems.Add(this.menuItem11);
            this.menuItem8.MenuItems.Add(this.menuItem10);
            this.menuItem8.MenuItems.Add(this.menuItem9);
            this.menuItem8.Text = "Seøadit";
            // 
            // menuItem11
            // 
            this.menuItem11.Text = "Bez øazení";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Text = "Èár. kód";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Text = "Název";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.MenuItems.Add(this.menuItem13);
            this.menuItem4.MenuItems.Add(this.menuItem14);
            this.menuItem4.Text = "Filtr";
            // 
            // menuItem13
            // 
            this.menuItem13.Text = "F3 - Typ";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Text = "F4 - Vše";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Text = "-";
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "Ent - Ok";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Text = "Bez lokace";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Text = "Esc - Storno";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.dataGrid1.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dataGrid1.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dataGrid1.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dataGrid1.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dataGrid1.Location = new System.Drawing.Point(0, 43);
            this.dataGrid1.MultiSelect = false;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.NumberFormat = "N";
            this.dataGrid1.RowHeightDefault = 23;
            this.dataGrid1.Size = new System.Drawing.Size(268, 173);
            this.dataGrid1.Sort = "";
            this.dataGrid1.SortByHeaderDoubleClick = true;
            this.dataGrid1.TabIndex = 5;
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            this.dataGrid1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid1_KeyDown);
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
            this.toolBarButtonFirst.ImageIndex = 0;
            this.toolBarButtonFirst.ToolTipText = "Prvni";
            // 
            // toolBarButtonPrev
            // 
            this.toolBarButtonPrev.ImageIndex = 2;
            this.toolBarButtonPrev.ToolTipText = "Predchozi";
            // 
            // toolBarButtonNext
            // 
            this.toolBarButtonNext.ImageIndex = 3;
            this.toolBarButtonNext.ToolTipText = "Nasledujici";
            // 
            // toolBarButtonLast
            // 
            this.toolBarButtonLast.ImageIndex = 1;
            this.toolBarButtonLast.ToolTipText = "Posledni";
            this.imageList1.Images.Clear();
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            // 
            // sbInfo
            // 
            this.sbInfo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.sbInfo.Location = new System.Drawing.Point(0, 216);
            this.sbInfo.Name = "sbInfo";
            this.sbInfo.Size = new System.Drawing.Size(268, 20);
            this.sbInfo.Text = "Info";
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSearch.Location = new System.Drawing.Point(0, 24);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(268, 19);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.Visible = false;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.GotFocus += new System.EventHandler(this.txtSearch_GotFocus);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // ZmenaLokace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(268, 283);
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.sbInfo);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ZmenaLokace";
            this.Text = "Lokace";
            this.Deactivate += new System.EventHandler(this.ZmenaLokace_Deactivate);
            this.Load += new System.EventHandler(this.ZmenaLokace_Load);
            this.Activated += new System.EventHandler(this.ZmenaLokace_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ZmenaLokace_KeyDown);
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
        private System.Windows.Forms.MenuItem menuItem12;
    }
}