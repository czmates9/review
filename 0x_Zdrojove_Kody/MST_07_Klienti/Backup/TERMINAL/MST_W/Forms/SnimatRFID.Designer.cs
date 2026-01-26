namespace Fask.MST_W.Forms
{
    partial class SnimatRFID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SnimatRFID));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemZobrazeniRezim = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemAllMemories = new System.Windows.Forms.MenuItem();
            this.menuItemRFIDON = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemKonec = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miPotvrdit = new System.Windows.Forms.MenuItem();
            this.menuItemSmazat = new System.Windows.Forms.MenuItem();
            this.menuItemInfo = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItemRfidTIDRead = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItemRfidEpcWrite = new System.Windows.Forms.MenuItem();
            this.menuItemRFIDEpcRead = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItemRFIDUserWrite = new System.Windows.Forms.MenuItem();
            this.menuItemRfidUserRead = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItemRFIDReservedWrite = new System.Windows.Forms.MenuItem();
            this.menuItemRFIDReservedRead = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButtonRFID = new System.Windows.Forms.ToolBarButton();
            this.imageListRFID = new System.Windows.Forms.ImageList();
            this.panelData = new System.Windows.Forms.Panel();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.dfRESERVEDLen = new Fask.Graphic.DataField();
            this.dfUSERLen = new Fask.Graphic.DataField();
            this.dfEPCLen = new Fask.Graphic.DataField();
            this.dfTIDLen = new Fask.Graphic.DataField();
            this.dfCountSeen = new Fask.Graphic.DataField();
            this.dfEAN = new Fask.Graphic.DataField();
            this.dfRSSI = new Fask.Graphic.DataField();
            this.dfRFIDReserved = new Fask.Graphic.DataField();
            this.dfUSER = new Fask.Graphic.DataField();
            this.dfEPC = new Fask.Graphic.DataField();
            this.dfTID = new Fask.Graphic.DataField();
            this.tabPageTID = new System.Windows.Forms.TabPage();
            this.textBoxTID = new System.Windows.Forms.TextBox();
            this.tabPageEPC = new System.Windows.Forms.TabPage();
            this.textBoxEPC = new System.Windows.Forms.TextBox();
            this.tabPageUSER = new System.Windows.Forms.TabPage();
            this.textBoxUSER = new System.Windows.Forms.TextBox();
            this.panelList = new System.Windows.Forms.Panel();
            this.bsRFID = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid2Polozky = new Fask.Graphic.DataGrid2();
            this.dsObecne = new Fask.SQLiteDBs.DataSets.Obecne();
            this.panelData.SuspendLayout();
            this.panelDetail.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageData.SuspendLayout();
            this.tabPageTID.SuspendLayout();
            this.tabPageEPC.SuspendLayout();
            this.tabPageUSER.SuspendLayout();
            this.panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsRFID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid2Polozky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsObecne)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemZobrazeniRezim);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.menuItemAllMemories);
            this.menuItem1.MenuItems.Add(this.menuItemRFIDON);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItemKonec);
            this.menuItem1.Text = "Menu";
            // 
            // menuItemZobrazeniRezim
            // 
            this.menuItemZobrazeniRezim.Text = "List/Detail (1)";
            this.menuItemZobrazeniRezim.Click += new System.EventHandler(this.menuItemZobrazeniRezim_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // menuItemAllMemories
            // 
            this.menuItemAllMemories.Text = "All memories on Read";
            this.menuItemAllMemories.Click += new System.EventHandler(this.menuItemAllMemories_Click);
            // 
            // menuItemRFIDON
            // 
            this.menuItemRFIDON.Text = "RFID ON/OFF (C)";
            this.menuItemRFIDON.Click += new System.EventHandler(this.buttonRFIDONOFF_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // menuItemKonec
            // 
            this.menuItemKonec.Text = "Konec (Esc)";
            this.menuItemKonec.Click += new System.EventHandler(this.menuItemKonec_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.miPotvrdit);
            this.menuItem2.MenuItems.Add(this.menuItemSmazat);
            this.menuItem2.MenuItems.Add(this.menuItemInfo);
            this.menuItem2.MenuItems.Add(this.menuItem5);
            this.menuItem2.Text = "Akce";
            // 
            // miPotvrdit
            // 
            this.miPotvrdit.Text = "Potvrdit (Enter)";
            this.miPotvrdit.Click += new System.EventHandler(this.miPotvrdit_Click);
            // 
            // menuItemSmazat
            // 
            this.menuItemSmazat.Text = "Smazat (Bksp)";
            this.menuItemSmazat.Click += new System.EventHandler(this.menuItemSmazat_Click);
            // 
            // menuItemInfo
            // 
            this.menuItemInfo.Text = "Info o položce";
            this.menuItemInfo.Click += new System.EventHandler(this.menuItemInfo_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.MenuItems.Add(this.menuItem12);
            this.menuItem5.MenuItems.Add(this.menuItem11);
            this.menuItem5.MenuItems.Add(this.menuItemRfidTIDRead);
            this.menuItem5.MenuItems.Add(this.menuItem6);
            this.menuItem5.MenuItems.Add(this.menuItemRfidEpcWrite);
            this.menuItem5.MenuItems.Add(this.menuItemRFIDEpcRead);
            this.menuItem5.MenuItems.Add(this.menuItem7);
            this.menuItem5.MenuItems.Add(this.menuItemRFIDUserWrite);
            this.menuItem5.MenuItems.Add(this.menuItemRfidUserRead);
            this.menuItem5.MenuItems.Add(this.menuItem8);
            this.menuItem5.MenuItems.Add(this.menuItemRFIDReservedWrite);
            this.menuItem5.MenuItems.Add(this.menuItemRFIDReservedRead);
            this.menuItem5.MenuItems.Add(this.menuItem9);
            this.menuItem5.MenuItems.Add(this.menuItem10);
            this.menuItem5.Text = "RFID";
            // 
            // menuItem12
            // 
            this.menuItem12.Text = "Test";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Text = "-";
            // 
            // menuItemRfidTIDRead
            // 
            this.menuItemRfidTIDRead.Text = "TID Read (A)";
            this.menuItemRfidTIDRead.Click += new System.EventHandler(this.menuItemRfidTIDRead_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Text = "-";
            // 
            // menuItemRfidEpcWrite
            // 
            this.menuItemRfidEpcWrite.Text = "EPC Write (D)";
            this.menuItemRfidEpcWrite.Click += new System.EventHandler(this.menuItemRfidEpcWrite_Click);
            // 
            // menuItemRFIDEpcRead
            // 
            this.menuItemRFIDEpcRead.Text = "EPC Read (E)";
            this.menuItemRFIDEpcRead.Click += new System.EventHandler(this.menuItemRFIDEpcRead_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Text = "-";
            // 
            // menuItemRFIDUserWrite
            // 
            this.menuItemRFIDUserWrite.Text = "USER Write (I)";
            this.menuItemRFIDUserWrite.Click += new System.EventHandler(this.menuItemRFIDUserWrite_Click);
            // 
            // menuItemRfidUserRead
            // 
            this.menuItemRfidUserRead.Text = "USER Read (J)";
            this.menuItemRfidUserRead.Click += new System.EventHandler(this.menuItemRfidUserRead_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Text = "-";
            // 
            // menuItemRFIDReservedWrite
            // 
            this.menuItemRFIDReservedWrite.Text = "RESERVED Write (N)";
            this.menuItemRFIDReservedWrite.Click += new System.EventHandler(this.menuItemRFIDReservedWrite_Click);
            // 
            // menuItemRFIDReservedRead
            // 
            this.menuItemRFIDReservedRead.Text = "RESERVED Read (O)";
            this.menuItemRFIDReservedRead.Click += new System.EventHandler(this.menuItemRFIDReservedRead_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Text = "-";
            // 
            // menuItem10
            // 
            this.menuItem10.Text = "Write all tags with sgtin96 (S)";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.statusBar1.Location = new System.Drawing.Point(0, 398);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(749, 19);
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.Add(this.toolBarButtonRFID);
            this.toolBar1.ImageList = this.imageListRFID;
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // toolBarButtonRFID
            // 
            this.toolBarButtonRFID.ImageIndex = 0;
            this.toolBarButtonRFID.ToolTipText = "RFID deactivate";
            this.imageListRFID.Images.Clear();
            this.imageListRFID.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource"))));
            this.imageListRFID.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource1"))));
            // 
            // panelData
            // 
            this.panelData.Controls.Add(this.panelDetail);
            this.panelData.Controls.Add(this.panelList);
            this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelData.Location = new System.Drawing.Point(0, 24);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(749, 374);
            // 
            // panelDetail
            // 
            this.panelDetail.Controls.Add(this.tabControl1);
            this.panelDetail.Location = new System.Drawing.Point(274, 30);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(446, 315);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageData);
            this.tabControl1.Controls.Add(this.tabPageTID);
            this.tabControl1.Controls.Add(this.tabPageEPC);
            this.tabControl1.Controls.Add(this.tabPageUSER);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(446, 315);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPageData
            // 
            this.tabPageData.AutoScroll = true;
            this.tabPageData.Controls.Add(this.dfRESERVEDLen);
            this.tabPageData.Controls.Add(this.dfUSERLen);
            this.tabPageData.Controls.Add(this.dfEPCLen);
            this.tabPageData.Controls.Add(this.dfTIDLen);
            this.tabPageData.Controls.Add(this.dfCountSeen);
            this.tabPageData.Controls.Add(this.dfEAN);
            this.tabPageData.Controls.Add(this.dfRSSI);
            this.tabPageData.Controls.Add(this.dfRFIDReserved);
            this.tabPageData.Controls.Add(this.dfUSER);
            this.tabPageData.Controls.Add(this.dfEPC);
            this.tabPageData.Controls.Add(this.dfTID);
            this.tabPageData.Location = new System.Drawing.Point(4, 25);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Size = new System.Drawing.Size(438, 286);
            this.tabPageData.Text = "Data";
            // 
            // dfRESERVEDLen
            // 
            this.dfRESERVEDLen.Data = "";
            this.dfRESERVEDLen.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfRESERVEDLen.DataMaxLength = 32767;
            this.dfRESERVEDLen.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfRESERVEDLen.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfRESERVEDLen.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfRESERVEDLen.Location = new System.Drawing.Point(0, 156);
            this.dfRESERVEDLen.MultiLine = false;
            this.dfRESERVEDLen.Name = "dfRESERVEDLen";
            this.dfRESERVEDLen.Popis = "RES Len";
            this.dfRESERVEDLen.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfRESERVEDLen.PopisWidth = 50;
            this.dfRESERVEDLen.ReadOnly = true;
            this.dfRESERVEDLen.Size = new System.Drawing.Size(438, 15);
            this.dfRESERVEDLen.TabIndex = 7;
            // 
            // dfUSERLen
            // 
            this.dfUSERLen.Data = "";
            this.dfUSERLen.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfUSERLen.DataMaxLength = 32767;
            this.dfUSERLen.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfUSERLen.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfUSERLen.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfUSERLen.Location = new System.Drawing.Point(0, 141);
            this.dfUSERLen.MultiLine = false;
            this.dfUSERLen.Name = "dfUSERLen";
            this.dfUSERLen.Popis = "USR Len";
            this.dfUSERLen.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfUSERLen.PopisWidth = 50;
            this.dfUSERLen.ReadOnly = true;
            this.dfUSERLen.Size = new System.Drawing.Size(438, 15);
            this.dfUSERLen.TabIndex = 6;
            // 
            // dfEPCLen
            // 
            this.dfEPCLen.Data = "";
            this.dfEPCLen.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfEPCLen.DataMaxLength = 32767;
            this.dfEPCLen.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfEPCLen.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfEPCLen.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfEPCLen.Location = new System.Drawing.Point(0, 126);
            this.dfEPCLen.MultiLine = false;
            this.dfEPCLen.Name = "dfEPCLen";
            this.dfEPCLen.Popis = "EPC Len";
            this.dfEPCLen.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfEPCLen.PopisWidth = 50;
            this.dfEPCLen.ReadOnly = true;
            this.dfEPCLen.Size = new System.Drawing.Size(438, 15);
            this.dfEPCLen.TabIndex = 5;
            // 
            // dfTIDLen
            // 
            this.dfTIDLen.Data = "";
            this.dfTIDLen.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfTIDLen.DataMaxLength = 32767;
            this.dfTIDLen.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfTIDLen.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfTIDLen.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfTIDLen.Location = new System.Drawing.Point(0, 110);
            this.dfTIDLen.MultiLine = false;
            this.dfTIDLen.Name = "dfTIDLen";
            this.dfTIDLen.Popis = "TID Len";
            this.dfTIDLen.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfTIDLen.PopisWidth = 50;
            this.dfTIDLen.ReadOnly = true;
            this.dfTIDLen.Size = new System.Drawing.Size(438, 16);
            this.dfTIDLen.TabIndex = 4;
            // 
            // dfCountSeen
            // 
            this.dfCountSeen.Data = "";
            this.dfCountSeen.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfCountSeen.DataMaxLength = 32767;
            this.dfCountSeen.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfCountSeen.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfCountSeen.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfCountSeen.Location = new System.Drawing.Point(0, 95);
            this.dfCountSeen.MultiLine = false;
            this.dfCountSeen.Name = "dfCountSeen";
            this.dfCountSeen.Popis = "Seen";
            this.dfCountSeen.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfCountSeen.PopisWidth = 50;
            this.dfCountSeen.ReadOnly = true;
            this.dfCountSeen.Size = new System.Drawing.Size(438, 15);
            this.dfCountSeen.TabIndex = 8;
            // 
            // dfEAN
            // 
            this.dfEAN.Data = "";
            this.dfEAN.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfEAN.DataMaxLength = 32767;
            this.dfEAN.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfEAN.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfEAN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfEAN.Location = new System.Drawing.Point(0, 79);
            this.dfEAN.MultiLine = false;
            this.dfEAN.Name = "dfEAN";
            this.dfEAN.Popis = "ID";
            this.dfEAN.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfEAN.PopisWidth = 50;
            this.dfEAN.ReadOnly = true;
            this.dfEAN.Size = new System.Drawing.Size(438, 16);
            this.dfEAN.TabIndex = 0;
            this.dfEAN.ParentChanged += new System.EventHandler(this.label1_ParentChanged);
            // 
            // dfRSSI
            // 
            this.dfRSSI.Data = "";
            this.dfRSSI.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfRSSI.DataMaxLength = 32767;
            this.dfRSSI.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfRSSI.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfRSSI.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfRSSI.Location = new System.Drawing.Point(0, 63);
            this.dfRSSI.MultiLine = false;
            this.dfRSSI.Name = "dfRSSI";
            this.dfRSSI.Popis = "RSSI";
            this.dfRSSI.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfRSSI.PopisWidth = 50;
            this.dfRSSI.ReadOnly = true;
            this.dfRSSI.Size = new System.Drawing.Size(438, 16);
            this.dfRSSI.TabIndex = 10;
            // 
            // dfRFIDReserved
            // 
            this.dfRFIDReserved.Data = "";
            this.dfRFIDReserved.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfRFIDReserved.DataMaxLength = 32767;
            this.dfRFIDReserved.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfRFIDReserved.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfRFIDReserved.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfRFIDReserved.Location = new System.Drawing.Point(0, 47);
            this.dfRFIDReserved.MultiLine = false;
            this.dfRFIDReserved.Name = "dfRFIDReserved";
            this.dfRFIDReserved.Popis = "RESERVED";
            this.dfRFIDReserved.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfRFIDReserved.PopisWidth = 50;
            this.dfRFIDReserved.ReadOnly = true;
            this.dfRFIDReserved.Size = new System.Drawing.Size(438, 16);
            this.dfRFIDReserved.TabIndex = 9;
            // 
            // dfUSER
            // 
            this.dfUSER.Data = "";
            this.dfUSER.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfUSER.DataMaxLength = 32767;
            this.dfUSER.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfUSER.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfUSER.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfUSER.Location = new System.Drawing.Point(0, 32);
            this.dfUSER.MultiLine = true;
            this.dfUSER.Name = "dfUSER";
            this.dfUSER.Popis = "USER";
            this.dfUSER.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfUSER.PopisWidth = 50;
            this.dfUSER.ReadOnly = true;
            this.dfUSER.Size = new System.Drawing.Size(438, 15);
            this.dfUSER.TabIndex = 3;
            // 
            // dfEPC
            // 
            this.dfEPC.Data = "";
            this.dfEPC.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfEPC.DataMaxLength = 32767;
            this.dfEPC.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfEPC.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfEPC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfEPC.Location = new System.Drawing.Point(0, 16);
            this.dfEPC.MultiLine = false;
            this.dfEPC.Name = "dfEPC";
            this.dfEPC.Popis = "EPC";
            this.dfEPC.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfEPC.PopisWidth = 50;
            this.dfEPC.ReadOnly = true;
            this.dfEPC.Size = new System.Drawing.Size(438, 16);
            this.dfEPC.TabIndex = 2;
            // 
            // dfTID
            // 
            this.dfTID.Data = "";
            this.dfTID.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfTID.DataMaxLength = 32767;
            this.dfTID.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfTID.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfTID.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dfTID.Location = new System.Drawing.Point(0, 0);
            this.dfTID.MultiLine = false;
            this.dfTID.Name = "dfTID";
            this.dfTID.Popis = "TID";
            this.dfTID.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfTID.PopisWidth = 50;
            this.dfTID.ReadOnly = true;
            this.dfTID.Size = new System.Drawing.Size(438, 16);
            this.dfTID.TabIndex = 1;
            // 
            // tabPageTID
            // 
            this.tabPageTID.Controls.Add(this.textBoxTID);
            this.tabPageTID.Location = new System.Drawing.Point(4, 25);
            this.tabPageTID.Name = "tabPageTID";
            this.tabPageTID.Size = new System.Drawing.Size(438, 286);
            this.tabPageTID.Text = "TID";
            // 
            // textBoxTID
            // 
            this.textBoxTID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTID.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.textBoxTID.Location = new System.Drawing.Point(4, 4);
            this.textBoxTID.Multiline = true;
            this.textBoxTID.Name = "textBoxTID";
            this.textBoxTID.ReadOnly = true;
            this.textBoxTID.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxTID.Size = new System.Drawing.Size(431, 279);
            this.textBoxTID.TabIndex = 0;
            // 
            // tabPageEPC
            // 
            this.tabPageEPC.Controls.Add(this.textBoxEPC);
            this.tabPageEPC.Location = new System.Drawing.Point(4, 25);
            this.tabPageEPC.Name = "tabPageEPC";
            this.tabPageEPC.Size = new System.Drawing.Size(438, 286);
            this.tabPageEPC.Text = "EPC";
            // 
            // textBoxEPC
            // 
            this.textBoxEPC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEPC.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.textBoxEPC.Location = new System.Drawing.Point(4, 4);
            this.textBoxEPC.Multiline = true;
            this.textBoxEPC.Name = "textBoxEPC";
            this.textBoxEPC.ReadOnly = true;
            this.textBoxEPC.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxEPC.Size = new System.Drawing.Size(431, 279);
            this.textBoxEPC.TabIndex = 1;
            // 
            // tabPageUSER
            // 
            this.tabPageUSER.Controls.Add(this.textBoxUSER);
            this.tabPageUSER.Location = new System.Drawing.Point(4, 25);
            this.tabPageUSER.Name = "tabPageUSER";
            this.tabPageUSER.Size = new System.Drawing.Size(438, 286);
            this.tabPageUSER.Text = "USER";
            // 
            // textBoxUSER
            // 
            this.textBoxUSER.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUSER.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.textBoxUSER.Location = new System.Drawing.Point(4, 4);
            this.textBoxUSER.Multiline = true;
            this.textBoxUSER.Name = "textBoxUSER";
            this.textBoxUSER.ReadOnly = true;
            this.textBoxUSER.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxUSER.Size = new System.Drawing.Size(431, 279);
            this.textBoxUSER.TabIndex = 2;
            // 
            // panelList
            // 
            this.panelList.Controls.Add(this.dataGrid2Polozky);
            this.panelList.Location = new System.Drawing.Point(40, 30);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(213, 200);
            // 
            // bsRFID
            // 
            this.bsRFID.DataMember = "RFID";
            this.bsRFID.DataSource = this.dsObecne;
            this.bsRFID.Sort = "";
            // 
            // dataGrid2Polozky
            // 
            this.dataGrid2Polozky.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid2Polozky.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid2Polozky.CurrentRow = null;
            this.dataGrid2Polozky.DataSource = this.bsRFID;
            this.dataGrid2Polozky.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid2Polozky.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dataGrid2Polozky.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dataGrid2Polozky.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dataGrid2Polozky.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dataGrid2Polozky.Location = new System.Drawing.Point(0, 0);
            this.dataGrid2Polozky.MultiSelect = false;
            this.dataGrid2Polozky.Name = "dataGrid2Polozky";
            this.dataGrid2Polozky.NumberFormat = "N";
            this.dataGrid2Polozky.RowHeightDefault = 23;
            this.dataGrid2Polozky.Size = new System.Drawing.Size(213, 200);
            this.dataGrid2Polozky.Sort = "";
            this.dataGrid2Polozky.SortByHeaderDoubleClick = true;
            this.dataGrid2Polozky.TabIndex = 0;
            this.dataGrid2Polozky.CurrentRowIndexChanged += new System.EventHandler(this.dataGrid2Polozky_CurrentRowIndexChanged);
            // 
            // dsObecne
            // 
            this.dsObecne.DataSetName = "Obecne";
            this.dsObecne.Prefix = "";
            this.dsObecne.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SnimatRFID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(749, 417);
            this.ControlBox = false;
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.toolBar1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "SnimatRFID";
            this.Text = "Snímání RFID";
            this.Deactivate += new System.EventHandler(this.SnimatRFID_Deactivate);
            this.Load += new System.EventHandler(this.SnimatRFID_Load);
            this.Activated += new System.EventHandler(this.SnimatRFID_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SnimatRFID_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SnimatRFID_KeyDown);
            this.panelData.ResumeLayout(false);
            this.panelDetail.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageData.ResumeLayout(false);
            this.tabPageTID.ResumeLayout(false);
            this.tabPageEPC.ResumeLayout(false);
            this.tabPageUSER.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsRFID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid2Polozky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsObecne)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.DataGrid2 dataGrid2Polozky;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemKonec;
        private System.Windows.Forms.MenuItem menuItemRFIDON;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItemSmazat;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton toolBarButtonRFID;
        private System.Windows.Forms.ImageList imageListRFID;
        private System.Windows.Forms.Panel panelData;
        private Fask.SQLiteDBs.DataSets.Obecne dsObecne;
        private System.Windows.Forms.BindingSource bsRFID;
        private System.Windows.Forms.MenuItem menuItemInfo;
        private System.Windows.Forms.MenuItem miPotvrdit;
        private System.Windows.Forms.MenuItem menuItemZobrazeniRezim;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Panel panelList;
        private Fask.Graphic.DataField dfEAN;
        private Fask.Graphic.DataField dfEPCLen;
        private Fask.Graphic.DataField dfTIDLen;
        private Fask.Graphic.DataField dfUSER;
        private Fask.Graphic.DataField dfEPC;
        private Fask.Graphic.DataField dfTID;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemAllMemories;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItemRfidEpcWrite;
        private Fask.Graphic.DataField dfRESERVEDLen;
        private Fask.Graphic.DataField dfUSERLen;
        private System.Windows.Forms.MenuItem menuItemRFIDEpcRead;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItemRFIDUserWrite;
        private System.Windows.Forms.MenuItem menuItemRfidUserRead;
        private System.Windows.Forms.MenuItem menuItemRfidTIDRead;
        private System.Windows.Forms.MenuItem menuItem6;
        private Fask.Graphic.DataField dfCountSeen;
        private Fask.Graphic.DataField dfRFIDReserved;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItemRFIDReservedWrite;
        private System.Windows.Forms.MenuItem menuItemRFIDReservedRead;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageData;
        private System.Windows.Forms.TabPage tabPageTID;
        private System.Windows.Forms.TextBox textBoxTID;
        private System.Windows.Forms.TabPage tabPageEPC;
        private System.Windows.Forms.TextBox textBoxEPC;
        private System.Windows.Forms.TabPage tabPageUSER;
        private System.Windows.Forms.TextBox textBoxUSER;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem menuItem11;
        private Fask.Graphic.DataField dfRSSI;
    }
}