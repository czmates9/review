namespace Fask.Module.Zbozi
{
    partial class ListPolozek
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItemMenu = new System.Windows.Forms.MenuItem();
            this.miHledatDleCK = new System.Windows.Forms.MenuItem();
            this.miHledatDleNazvu = new System.Windows.Forms.MenuItem();
            this.menuItemHledatDleKodu = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miList = new System.Windows.Forms.MenuItem();
            this.miDetail = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.miKonec = new System.Windows.Forms.MenuItem();
            this.menuItemFunkce = new System.Windows.Forms.MenuItem();
            this.menuItemFunkceHmotnost = new System.Windows.Forms.MenuItem();
            this.menuItemFunkceLokace = new System.Windows.Forms.MenuItem();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.labelMJ = new System.Windows.Forms.Label();
            this.labelCZCarKod = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSNTrack = new System.Windows.Forms.Label();
            this.labelSklad = new System.Windows.Forms.Label();
            this.labelTAXRATE = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.labelQTY = new System.Windows.Forms.Label();
            this.labelVNDITNUM = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelQTYPACK = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelItemnmbr = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lP5 = new System.Windows.Forms.Label();
            this.labelPRICE5 = new System.Windows.Forms.Label();
            this.lP4 = new System.Windows.Forms.Label();
            this.labelPRICE4 = new System.Windows.Forms.Label();
            this.lP3 = new System.Windows.Forms.Label();
            this.labelPRICE3 = new System.Windows.Forms.Label();
            this.lP2 = new System.Windows.Forms.Label();
            this.labelPRICE2 = new System.Windows.Forms.Label();
            this.lP1 = new System.Windows.Forms.Label();
            this.labelPRICE1 = new System.Windows.Forms.Label();
            this.lP0 = new System.Windows.Forms.Label();
            this.labelPRICE0 = new System.Windows.Forms.Label();
            this.labelNazev = new System.Windows.Forms.Label();
            this.panelList = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.panelDetail.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelList.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemMenu);
            this.mainMenu1.MenuItems.Add(this.menuItemFunkce);
            // 
            // menuItemMenu
            // 
            this.menuItemMenu.MenuItems.Add(this.miHledatDleCK);
            this.menuItemMenu.MenuItems.Add(this.miHledatDleNazvu);
            this.menuItemMenu.MenuItems.Add(this.menuItemHledatDleKodu);
            this.menuItemMenu.MenuItems.Add(this.menuItem3);
            this.menuItemMenu.MenuItems.Add(this.miList);
            this.menuItemMenu.MenuItems.Add(this.miDetail);
            this.menuItemMenu.MenuItems.Add(this.menuItem7);
            this.menuItemMenu.MenuItems.Add(this.miKonec);
            this.menuItemMenu.Text = "Menu";
            // 
            // miHledatDleCK
            // 
            this.miHledatDleCK.Text = "Hledat dle ČK (F1)";
            this.miHledatDleCK.Click += new System.EventHandler(this.miHledatDleCK_Click);
            // 
            // miHledatDleNazvu
            // 
            this.miHledatDleNazvu.Text = "Hledat dle názvu (F2)";
            this.miHledatDleNazvu.Click += new System.EventHandler(this.miHledatDleNazvu_Click);
            // 
            // menuItemHledatDleKodu
            // 
            this.menuItemHledatDleKodu.Text = "Hledat dle kódu (F3)";
            this.menuItemHledatDleKodu.Click += new System.EventHandler(this.menuItemHledatDleKodu_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // miList
            // 
            this.miList.Text = "List (F5)";
            this.miList.Click += new System.EventHandler(this.miList_Click);
            // 
            // miDetail
            // 
            this.miDetail.Text = "Detail (F6)";
            this.miDetail.Click += new System.EventHandler(this.miDetail_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Text = "-";
            // 
            // miKonec
            // 
            this.miKonec.Text = "Konec (ESC)";
            this.miKonec.Click += new System.EventHandler(this.miKonec_Click);
            // 
            // menuItemFunkce
            // 
            this.menuItemFunkce.MenuItems.Add(this.menuItemFunkceHmotnost);
            this.menuItemFunkce.MenuItems.Add(this.menuItemFunkceLokace);
            this.menuItemFunkce.Text = "Funkce";
            // 
            // menuItemFunkceHmotnost
            // 
            this.menuItemFunkceHmotnost.Text = "Hmotnost";
            this.menuItemFunkceHmotnost.Click += new System.EventHandler(this.menuItemFunkceHmotnost_Click);
            // 
            // menuItemFunkceLokace
            // 
            this.menuItemFunkceLokace.Text = "Lokace";
            this.menuItemFunkceLokace.Click += new System.EventHandler(this.menuItemFunkceLokace_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dataGrid1.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dataGrid1.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dataGrid1.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.MultiSelect = false;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.NumberFormat = "N";
            this.dataGrid1.RowHeightDefault = 23;
            this.dataGrid1.Size = new System.Drawing.Size(273, 270);
            this.dataGrid1.Sort = "";
            this.dataGrid1.SortByHeaderDoubleClick = true;
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            this.dataGrid1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid1_KeyDown);
            // 
            // panelDetail
            // 
            this.panelDetail.Controls.Add(this.tabControl1);
            this.panelDetail.Controls.Add(this.labelNazev);
            this.panelDetail.Location = new System.Drawing.Point(131, 3);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(240, 299);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tabControl1.Location = new System.Drawing.Point(0, 52);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(237, 244);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.labelMJ);
            this.tabPage1.Controls.Add(this.labelCZCarKod);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.labelSNTrack);
            this.tabPage1.Controls.Add(this.labelSklad);
            this.tabPage1.Controls.Add(this.labelTAXRATE);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.labelQTY);
            this.tabPage1.Controls.Add(this.labelVNDITNUM);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.labelQTYPACK);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.labelItemnmbr);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(229, 218);
            this.tabPage1.Text = "Hlavní";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(0, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 15);
            this.label9.Text = "Pol.č. :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelMJ
            // 
            this.labelMJ.Location = new System.Drawing.Point(77, 161);
            this.labelMJ.Name = "labelMJ";
            this.labelMJ.Size = new System.Drawing.Size(51, 19);
            this.labelMJ.Text = "-";
            // 
            // labelCZCarKod
            // 
            this.labelCZCarKod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCZCarKod.Location = new System.Drawing.Point(77, 74);
            this.labelCZCarKod.Name = "labelCZCarKod";
            this.labelCZCarKod.Size = new System.Drawing.Size(149, 19);
            this.labelCZCarKod.Text = "-";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.Text = "Sklad :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSNTrack
            // 
            this.labelSNTrack.Location = new System.Drawing.Point(77, 181);
            this.labelSNTrack.Name = "labelSNTrack";
            this.labelSNTrack.Size = new System.Drawing.Size(51, 19);
            this.labelSNTrack.Text = "-";
            // 
            // labelSklad
            // 
            this.labelSklad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSklad.Location = new System.Drawing.Point(77, 28);
            this.labelSklad.Name = "labelSklad";
            this.labelSklad.Size = new System.Drawing.Size(149, 19);
            this.labelSklad.Text = "-";
            // 
            // labelTAXRATE
            // 
            this.labelTAXRATE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTAXRATE.Location = new System.Drawing.Point(77, 141);
            this.labelTAXRATE.Name = "labelTAXRATE";
            this.labelTAXRATE.Size = new System.Drawing.Size(149, 19);
            this.labelTAXRATE.Text = "-";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.Text = "CZ kód :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(24, 163);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 15);
            this.label20.Text = "MJ : ";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(0, 122);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 15);
            this.label12.Text = "Balení :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelQTY
            // 
            this.labelQTY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelQTY.Location = new System.Drawing.Point(77, 97);
            this.labelQTY.Name = "labelQTY";
            this.labelQTY.Size = new System.Drawing.Size(149, 19);
            this.labelQTY.Text = "-";
            // 
            // labelVNDITNUM
            // 
            this.labelVNDITNUM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVNDITNUM.Location = new System.Drawing.Point(77, 51);
            this.labelVNDITNUM.Name = "labelVNDITNUM";
            this.labelVNDITNUM.Size = new System.Drawing.Size(149, 19);
            this.labelVNDITNUM.Text = "-";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(0, 183);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 15);
            this.label15.Text = "Ser. čísla : ";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(0, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 15);
            this.label11.Text = "Kusů :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(0, 143);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 15);
            this.label13.Text = "Daň :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelQTYPACK
            // 
            this.labelQTYPACK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelQTYPACK.Location = new System.Drawing.Point(77, 120);
            this.labelQTYPACK.Name = "labelQTYPACK";
            this.labelQTYPACK.Size = new System.Drawing.Size(149, 19);
            this.labelQTYPACK.Text = "-";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(0, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 15);
            this.label10.Text = "Čár. kód :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelItemnmbr
            // 
            this.labelItemnmbr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemnmbr.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelItemnmbr.Location = new System.Drawing.Point(77, 5);
            this.labelItemnmbr.Name = "labelItemnmbr";
            this.labelItemnmbr.Size = new System.Drawing.Size(149, 19);
            this.labelItemnmbr.Text = "-";
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.lP5);
            this.tabPage2.Controls.Add(this.labelPRICE5);
            this.tabPage2.Controls.Add(this.lP4);
            this.tabPage2.Controls.Add(this.labelPRICE4);
            this.tabPage2.Controls.Add(this.lP3);
            this.tabPage2.Controls.Add(this.labelPRICE3);
            this.tabPage2.Controls.Add(this.lP2);
            this.tabPage2.Controls.Add(this.labelPRICE2);
            this.tabPage2.Controls.Add(this.lP1);
            this.tabPage2.Controls.Add(this.labelPRICE1);
            this.tabPage2.Controls.Add(this.lP0);
            this.tabPage2.Controls.Add(this.labelPRICE0);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(229, 218);
            this.tabPage2.Text = "Ceny";
            // 
            // lP5
            // 
            this.lP5.Location = new System.Drawing.Point(0, 119);
            this.lP5.Name = "lP5";
            this.lP5.Size = new System.Drawing.Size(73, 22);
            this.lP5.Text = "Cena 5 :";
            this.lP5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPRICE5
            // 
            this.labelPRICE5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPRICE5.Location = new System.Drawing.Point(79, 119);
            this.labelPRICE5.Name = "labelPRICE5";
            this.labelPRICE5.Size = new System.Drawing.Size(150, 22);
            this.labelPRICE5.Text = "-";
            // 
            // lP4
            // 
            this.lP4.Location = new System.Drawing.Point(0, 97);
            this.lP4.Name = "lP4";
            this.lP4.Size = new System.Drawing.Size(73, 22);
            this.lP4.Text = "Cena 4 :";
            this.lP4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPRICE4
            // 
            this.labelPRICE4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPRICE4.Location = new System.Drawing.Point(79, 97);
            this.labelPRICE4.Name = "labelPRICE4";
            this.labelPRICE4.Size = new System.Drawing.Size(150, 22);
            this.labelPRICE4.Text = "-";
            // 
            // lP3
            // 
            this.lP3.Location = new System.Drawing.Point(-1, 75);
            this.lP3.Name = "lP3";
            this.lP3.Size = new System.Drawing.Size(73, 22);
            this.lP3.Text = "Cena 3 :";
            this.lP3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPRICE3
            // 
            this.labelPRICE3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPRICE3.Location = new System.Drawing.Point(78, 75);
            this.labelPRICE3.Name = "labelPRICE3";
            this.labelPRICE3.Size = new System.Drawing.Size(150, 22);
            this.labelPRICE3.Text = "-";
            // 
            // lP2
            // 
            this.lP2.Location = new System.Drawing.Point(0, 53);
            this.lP2.Name = "lP2";
            this.lP2.Size = new System.Drawing.Size(73, 22);
            this.lP2.Text = "Cena 2 :";
            this.lP2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPRICE2
            // 
            this.labelPRICE2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPRICE2.Location = new System.Drawing.Point(79, 53);
            this.labelPRICE2.Name = "labelPRICE2";
            this.labelPRICE2.Size = new System.Drawing.Size(150, 22);
            this.labelPRICE2.Text = "-";
            // 
            // lP1
            // 
            this.lP1.Location = new System.Drawing.Point(0, 31);
            this.lP1.Name = "lP1";
            this.lP1.Size = new System.Drawing.Size(73, 22);
            this.lP1.Text = "Cena 1 :";
            this.lP1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPRICE1
            // 
            this.labelPRICE1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPRICE1.Location = new System.Drawing.Point(79, 31);
            this.labelPRICE1.Name = "labelPRICE1";
            this.labelPRICE1.Size = new System.Drawing.Size(150, 22);
            this.labelPRICE1.Text = "-";
            // 
            // lP0
            // 
            this.lP0.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lP0.Location = new System.Drawing.Point(0, 9);
            this.lP0.Name = "lP0";
            this.lP0.Size = new System.Drawing.Size(73, 22);
            this.lP0.Text = "Cena 0 :";
            this.lP0.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelPRICE0
            // 
            this.labelPRICE0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPRICE0.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelPRICE0.Location = new System.Drawing.Point(79, 9);
            this.labelPRICE0.Name = "labelPRICE0";
            this.labelPRICE0.Size = new System.Drawing.Size(150, 22);
            this.labelPRICE0.Text = "-";
            // 
            // labelNazev
            // 
            this.labelNazev.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.labelNazev.Location = new System.Drawing.Point(3, 0);
            this.labelNazev.Name = "labelNazev";
            this.labelNazev.Size = new System.Drawing.Size(234, 49);
            this.labelNazev.Text = "-";
            // 
            // panelList
            // 
            this.panelList.Controls.Add(this.dataGrid1);
            this.panelList.Location = new System.Drawing.Point(451, 32);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(273, 270);
            // 
            // ListPolozek
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(757, 345);
            this.ControlBox = false;
            this.Controls.Add(this.panelList);
            this.Controls.Add(this.panelDetail);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "ListPolozek";
            this.Text = "Info";
            this.Load += new System.EventHandler(this.ListPolozek_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListPolozek_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.MenuItem menuItemMenu;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Label labelNazev;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelPRICE0;
        private System.Windows.Forms.Label labelItemnmbr;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelVNDITNUM;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelTAXRATE;
        private System.Windows.Forms.Label labelCZCarKod;
        private System.Windows.Forms.Label labelSNTrack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lP0;
        private System.Windows.Forms.Label labelQTY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelQTYPACK;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MenuItem miHledatDleNazvu;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem miKonec;
        private System.Windows.Forms.MenuItem miList;
        private System.Windows.Forms.MenuItem miDetail;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem miHledatDleCK;
        private System.Windows.Forms.Label labelMJ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSklad;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lP5;
        private System.Windows.Forms.Label labelPRICE5;
        private System.Windows.Forms.Label lP4;
        private System.Windows.Forms.Label labelPRICE4;
        private System.Windows.Forms.Label lP3;
        private System.Windows.Forms.Label labelPRICE3;
        private System.Windows.Forms.Label lP2;
        private System.Windows.Forms.Label labelPRICE2;
        private System.Windows.Forms.Label lP1;
        private System.Windows.Forms.Label labelPRICE1;
        private System.Windows.Forms.MenuItem menuItemHledatDleKodu;
        private System.Windows.Forms.MenuItem menuItemFunkce;
        private System.Windows.Forms.MenuItem menuItemFunkceHmotnost;
        private System.Windows.Forms.MenuItem menuItemFunkceLokace;
    }
}