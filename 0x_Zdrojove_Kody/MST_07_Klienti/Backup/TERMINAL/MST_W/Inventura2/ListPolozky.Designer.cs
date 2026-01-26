using System.Windows.Forms;
namespace Fask.MST_W.Inventura2
{
    partial class ListPolozky
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListPolozky));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemZmenaLokace = new System.Windows.Forms.MenuItem();
            this.menuItemZmenaKancl = new System.Windows.Forms.MenuItem();
            this.menuItemZmenaStrediska = new System.Windows.Forms.MenuItem();
            this.menuItemZmenaOsoby = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItemHledatCarKod = new System.Windows.Forms.MenuItem();
            this.menuItemHledatPozice = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemPridat = new System.Windows.Forms.MenuItem();
            this.menuItemSmazat = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemRFID = new System.Windows.Forms.MenuItem();
            this.menuItemKonec = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemZobrazeniVse = new System.Windows.Forms.MenuItem();
            this.menuItemZobrazeniSwitchRezim = new System.Windows.Forms.MenuItem();
            this.menuItemNasnimane = new System.Windows.Forms.MenuItem();
            this.menuItemZobrazeniZbyvajici = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.dfKancelarNazev = new Fask.Graphic.DataField();
            this.dataFieldZbyva = new Fask.Graphic.DataField();
            this.dataFieldNasnimano = new Fask.Graphic.DataField();
            this.dataFieldQUANTITY = new Fask.Graphic.DataField();
            this.dataFieldOsoba = new Fask.Graphic.DataField();
            this.dataFieldStredisko = new Fask.Graphic.DataField();
            this.dataFieldKancelar = new Fask.Graphic.DataField();
            this.dataFieldLokace = new Fask.Graphic.DataField();
            this.dataFieldEAN = new Fask.Graphic.DataField();
            this.dataFieldITEMDESC = new Fask.Graphic.DataField();
            this.dataFieldItemnmbr = new Fask.Graphic.DataField();
            this.sbInfo = new System.Windows.Forms.StatusBar();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dataGrid = new Fask.Graphic.DataGrid2();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButtonStart = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonLeft = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonRight = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonEnd = new System.Windows.Forms.ToolBarButton();
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
            this.menuItem1.MenuItems.Add(this.menuItemZmenaLokace);
            this.menuItem1.MenuItems.Add(this.menuItemZmenaKancl);
            this.menuItem1.MenuItems.Add(this.menuItemZmenaStrediska);
            this.menuItem1.MenuItems.Add(this.menuItemZmenaOsoby);
            this.menuItem1.MenuItems.Add(this.menuItem7);
            this.menuItem1.MenuItems.Add(this.menuItem5);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItemPridat);
            this.menuItem1.MenuItems.Add(this.menuItemSmazat);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.menuItemRFID);
            this.menuItem1.MenuItems.Add(this.menuItemKonec);
            this.menuItem1.Text = "Menu";
            // 
            // menuItemZmenaLokace
            // 
            this.menuItemZmenaLokace.Text = "5 - Zmìna lokace";
            this.menuItemZmenaLokace.Click += new System.EventHandler(this.menuItemZmenaLokace_Click);
            // 
            // menuItemZmenaKancl
            // 
            this.menuItemZmenaKancl.Text = "6 - Zmìna umístìní";
            this.menuItemZmenaKancl.Click += new System.EventHandler(this.menuItemZmenaKancl_Click);
            // 
            // menuItemZmenaStrediska
            // 
            this.menuItemZmenaStrediska.Text = "7 - Zmìna støediska";
            this.menuItemZmenaStrediska.Click += new System.EventHandler(this.menuItemZmenaStrediska_Click);
            // 
            // menuItemZmenaOsoby
            // 
            this.menuItemZmenaOsoby.Text = "8 -Zmìna osoby";
            this.menuItemZmenaOsoby.Click += new System.EventHandler(this.menuItemZmenaOsoby_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Text = "-";
            // 
            // menuItem5
            // 
            this.menuItem5.MenuItems.Add(this.menuItem6);
            this.menuItem5.MenuItems.Add(this.menuItemHledatCarKod);
            this.menuItem5.MenuItems.Add(this.menuItemHledatPozice);
            this.menuItem5.Text = "Hledat";
            // 
            // menuItem6
            // 
            this.menuItem6.Text = "F1 - Název";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItemHledatCarKod
            // 
            this.menuItemHledatCarKod.Text = "F2 - Èár. kód";
            this.menuItemHledatCarKod.Click += new System.EventHandler(this.menuItemHledatCarKod_Click);
            // 
            // menuItemHledatPozice
            // 
            this.menuItemHledatPozice.Text = "F3 - Pozice";
            this.menuItemHledatPozice.Click += new System.EventHandler(this.menuItemHledatPozice_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // menuItemPridat
            // 
            this.menuItemPridat.Text = "Ent - Pøidat";
            this.menuItemPridat.Click += new System.EventHandler(this.buttonZadat_Click);
            // 
            // menuItemSmazat
            // 
            this.menuItemSmazat.Text = "Bksp - Smazat";
            this.menuItemSmazat.Click += new System.EventHandler(this.menuItemSmazat_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // menuItemRFID
            // 
            this.menuItemRFID.Text = "F4 - Aktivace RFID";
            this.menuItemRFID.Click += new System.EventHandler(this.menuItemRFID_Click);
            // 
            // menuItemKonec
            // 
            this.menuItemKonec.Text = "Esc - Konec";
            this.menuItemKonec.Click += new System.EventHandler(this.buttonKonec_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.menuItemZobrazeniVse);
            this.menuItem2.MenuItems.Add(this.menuItemZobrazeniSwitchRezim);
            this.menuItem2.MenuItems.Add(this.menuItemNasnimane);
            this.menuItem2.MenuItems.Add(this.menuItemZobrazeniZbyvajici);
            this.menuItem2.MenuItems.Add(this.menuItem11);
            this.menuItem2.MenuItems.Add(this.menuItem8);
            this.menuItem2.Text = "Zobrazeni";
            // 
            // menuItemZobrazeniVse
            // 
            this.menuItemZobrazeniVse.Text = "1 - Vše";
            this.menuItemZobrazeniVse.Click += new System.EventHandler(this.menuItemZobrazeniVse_Click);
            // 
            // menuItemZobrazeniSwitchRezim
            // 
            this.menuItemZobrazeniSwitchRezim.Text = "2 - List / Detail";
            this.menuItemZobrazeniSwitchRezim.Click += new System.EventHandler(this.menuItemZobrazeniSwitchRezim_Click);
            // 
            // menuItemNasnimane
            // 
            this.menuItemNasnimane.Text = "3 - Nasnímané";
            this.menuItemNasnimane.Click += new System.EventHandler(this.menuItemNasnimane_Click);
            // 
            // menuItemZobrazeniZbyvajici
            // 
            this.menuItemZobrazeniZbyvajici.Text = "4 - Zbývající";
            this.menuItemZobrazeniZbyvajici.Click += new System.EventHandler(this.menuItemZobrazeniZbyvajici_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Text = "-";
            // 
            // menuItem8
            // 
            this.menuItem8.Text = "Aktuální filtry";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // panelDetail
            // 
            this.panelDetail.AutoScroll = true;
            this.panelDetail.Controls.Add(this.dfKancelarNazev);
            this.panelDetail.Controls.Add(this.dataFieldZbyva);
            this.panelDetail.Controls.Add(this.dataFieldNasnimano);
            this.panelDetail.Controls.Add(this.dataFieldQUANTITY);
            this.panelDetail.Controls.Add(this.dataFieldOsoba);
            this.panelDetail.Controls.Add(this.dataFieldStredisko);
            this.panelDetail.Controls.Add(this.dataFieldKancelar);
            this.panelDetail.Controls.Add(this.dataFieldLokace);
            this.panelDetail.Controls.Add(this.dataFieldEAN);
            this.panelDetail.Controls.Add(this.dataFieldITEMDESC);
            this.panelDetail.Controls.Add(this.dataFieldItemnmbr);
            this.panelDetail.Location = new System.Drawing.Point(10, 53);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(277, 242);
            // 
            // dfKancelarNazev
            // 
            this.dfKancelarNazev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dfKancelarNazev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dfKancelarNazev.Data = "";
            this.dfKancelarNazev.DataBackColor = System.Drawing.Color.White;
            this.dfKancelarNazev.DataMaxLength = 32767;
            this.dfKancelarNazev.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfKancelarNazev.Dock = System.Windows.Forms.DockStyle.Top;
            this.dfKancelarNazev.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dfKancelarNazev.Location = new System.Drawing.Point(0, 176);
            this.dfKancelarNazev.MultiLine = false;
            this.dfKancelarNazev.Name = "dfKancelarNazev";
            this.dfKancelarNazev.Popis = "Kanceláø název";
            this.dfKancelarNazev.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfKancelarNazev.PopisWidth = 100;
            this.dfKancelarNazev.ReadOnly = true;
            this.dfKancelarNazev.Size = new System.Drawing.Size(277, 16);
            this.dfKancelarNazev.TabIndex = 13;
            // 
            // dataFieldZbyva
            // 
            this.dataFieldZbyva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataFieldZbyva.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataFieldZbyva.Data = "";
            this.dataFieldZbyva.DataBackColor = System.Drawing.Color.White;
            this.dataFieldZbyva.DataMaxLength = 32767;
            this.dataFieldZbyva.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dataFieldZbyva.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataFieldZbyva.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.dataFieldZbyva.Location = new System.Drawing.Point(0, 158);
            this.dataFieldZbyva.MultiLine = false;
            this.dataFieldZbyva.Name = "dataFieldZbyva";
            this.dataFieldZbyva.Popis = "Zbývá";
            this.dataFieldZbyva.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dataFieldZbyva.PopisWidth = 100;
            this.dataFieldZbyva.ReadOnly = true;
            this.dataFieldZbyva.Size = new System.Drawing.Size(277, 18);
            this.dataFieldZbyva.TabIndex = 12;
            // 
            // dataFieldNasnimano
            // 
            this.dataFieldNasnimano.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataFieldNasnimano.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataFieldNasnimano.Data = "";
            this.dataFieldNasnimano.DataBackColor = System.Drawing.Color.White;
            this.dataFieldNasnimano.DataMaxLength = 32767;
            this.dataFieldNasnimano.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dataFieldNasnimano.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataFieldNasnimano.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.dataFieldNasnimano.Location = new System.Drawing.Point(0, 140);
            this.dataFieldNasnimano.MultiLine = false;
            this.dataFieldNasnimano.Name = "dataFieldNasnimano";
            this.dataFieldNasnimano.Popis = "Nasnímáno";
            this.dataFieldNasnimano.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dataFieldNasnimano.PopisWidth = 100;
            this.dataFieldNasnimano.ReadOnly = true;
            this.dataFieldNasnimano.Size = new System.Drawing.Size(277, 18);
            this.dataFieldNasnimano.TabIndex = 10;
            // 
            // dataFieldQUANTITY
            // 
            this.dataFieldQUANTITY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataFieldQUANTITY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataFieldQUANTITY.Data = "";
            this.dataFieldQUANTITY.DataBackColor = System.Drawing.Color.White;
            this.dataFieldQUANTITY.DataMaxLength = 32767;
            this.dataFieldQUANTITY.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dataFieldQUANTITY.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataFieldQUANTITY.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dataFieldQUANTITY.Location = new System.Drawing.Point(0, 124);
            this.dataFieldQUANTITY.MultiLine = false;
            this.dataFieldQUANTITY.Name = "dataFieldQUANTITY";
            this.dataFieldQUANTITY.Popis = "Množství";
            this.dataFieldQUANTITY.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dataFieldQUANTITY.PopisWidth = 100;
            this.dataFieldQUANTITY.ReadOnly = true;
            this.dataFieldQUANTITY.Size = new System.Drawing.Size(277, 16);
            this.dataFieldQUANTITY.TabIndex = 4;
            // 
            // dataFieldOsoba
            // 
            this.dataFieldOsoba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataFieldOsoba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataFieldOsoba.Data = "";
            this.dataFieldOsoba.DataBackColor = System.Drawing.Color.White;
            this.dataFieldOsoba.DataMaxLength = 32767;
            this.dataFieldOsoba.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dataFieldOsoba.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataFieldOsoba.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dataFieldOsoba.Location = new System.Drawing.Point(0, 108);
            this.dataFieldOsoba.MultiLine = false;
            this.dataFieldOsoba.Name = "dataFieldOsoba";
            this.dataFieldOsoba.Popis = "Osoba";
            this.dataFieldOsoba.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dataFieldOsoba.PopisWidth = 100;
            this.dataFieldOsoba.ReadOnly = true;
            this.dataFieldOsoba.Size = new System.Drawing.Size(277, 16);
            this.dataFieldOsoba.TabIndex = 5;
            // 
            // dataFieldStredisko
            // 
            this.dataFieldStredisko.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataFieldStredisko.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataFieldStredisko.Data = "";
            this.dataFieldStredisko.DataBackColor = System.Drawing.Color.White;
            this.dataFieldStredisko.DataMaxLength = 32767;
            this.dataFieldStredisko.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dataFieldStredisko.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataFieldStredisko.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dataFieldStredisko.Location = new System.Drawing.Point(0, 92);
            this.dataFieldStredisko.MultiLine = false;
            this.dataFieldStredisko.Name = "dataFieldStredisko";
            this.dataFieldStredisko.Popis = "Støedisko";
            this.dataFieldStredisko.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dataFieldStredisko.PopisWidth = 100;
            this.dataFieldStredisko.ReadOnly = true;
            this.dataFieldStredisko.Size = new System.Drawing.Size(277, 16);
            this.dataFieldStredisko.TabIndex = 9;
            // 
            // dataFieldKancelar
            // 
            this.dataFieldKancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataFieldKancelar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataFieldKancelar.Data = "";
            this.dataFieldKancelar.DataBackColor = System.Drawing.Color.White;
            this.dataFieldKancelar.DataMaxLength = 32767;
            this.dataFieldKancelar.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dataFieldKancelar.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataFieldKancelar.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dataFieldKancelar.Location = new System.Drawing.Point(0, 76);
            this.dataFieldKancelar.MultiLine = false;
            this.dataFieldKancelar.Name = "dataFieldKancelar";
            this.dataFieldKancelar.Popis = "Umístìní";
            this.dataFieldKancelar.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dataFieldKancelar.PopisWidth = 100;
            this.dataFieldKancelar.ReadOnly = true;
            this.dataFieldKancelar.Size = new System.Drawing.Size(277, 16);
            this.dataFieldKancelar.TabIndex = 11;
            // 
            // dataFieldLokace
            // 
            this.dataFieldLokace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataFieldLokace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataFieldLokace.Data = "";
            this.dataFieldLokace.DataBackColor = System.Drawing.Color.White;
            this.dataFieldLokace.DataMaxLength = 32767;
            this.dataFieldLokace.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dataFieldLokace.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataFieldLokace.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dataFieldLokace.Location = new System.Drawing.Point(0, 60);
            this.dataFieldLokace.MultiLine = false;
            this.dataFieldLokace.Name = "dataFieldLokace";
            this.dataFieldLokace.Popis = "Lokace";
            this.dataFieldLokace.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dataFieldLokace.PopisWidth = 100;
            this.dataFieldLokace.ReadOnly = true;
            this.dataFieldLokace.Size = new System.Drawing.Size(277, 16);
            this.dataFieldLokace.TabIndex = 3;
            // 
            // dataFieldEAN
            // 
            this.dataFieldEAN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataFieldEAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataFieldEAN.Data = "";
            this.dataFieldEAN.DataBackColor = System.Drawing.Color.White;
            this.dataFieldEAN.DataMaxLength = 32767;
            this.dataFieldEAN.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dataFieldEAN.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataFieldEAN.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dataFieldEAN.Location = new System.Drawing.Point(0, 44);
            this.dataFieldEAN.MultiLine = false;
            this.dataFieldEAN.Name = "dataFieldEAN";
            this.dataFieldEAN.Popis = "EAN";
            this.dataFieldEAN.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dataFieldEAN.PopisWidth = 100;
            this.dataFieldEAN.ReadOnly = true;
            this.dataFieldEAN.Size = new System.Drawing.Size(277, 16);
            this.dataFieldEAN.TabIndex = 6;
            // 
            // dataFieldITEMDESC
            // 
            this.dataFieldITEMDESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataFieldITEMDESC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataFieldITEMDESC.Data = "";
            this.dataFieldITEMDESC.DataBackColor = System.Drawing.Color.White;
            this.dataFieldITEMDESC.DataMaxLength = 32767;
            this.dataFieldITEMDESC.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dataFieldITEMDESC.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataFieldITEMDESC.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.dataFieldITEMDESC.Location = new System.Drawing.Point(0, 16);
            this.dataFieldITEMDESC.MultiLine = true;
            this.dataFieldITEMDESC.Name = "dataFieldITEMDESC";
            this.dataFieldITEMDESC.Popis = "Název";
            this.dataFieldITEMDESC.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dataFieldITEMDESC.PopisWidth = 100;
            this.dataFieldITEMDESC.ReadOnly = true;
            this.dataFieldITEMDESC.Size = new System.Drawing.Size(277, 28);
            this.dataFieldITEMDESC.TabIndex = 1;
            // 
            // dataFieldItemnmbr
            // 
            this.dataFieldItemnmbr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dataFieldItemnmbr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataFieldItemnmbr.Data = "";
            this.dataFieldItemnmbr.DataBackColor = System.Drawing.Color.White;
            this.dataFieldItemnmbr.DataMaxLength = 32767;
            this.dataFieldItemnmbr.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dataFieldItemnmbr.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataFieldItemnmbr.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dataFieldItemnmbr.Location = new System.Drawing.Point(0, 0);
            this.dataFieldItemnmbr.MultiLine = false;
            this.dataFieldItemnmbr.Name = "dataFieldItemnmbr";
            this.dataFieldItemnmbr.Popis = "Èíslo";
            this.dataFieldItemnmbr.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dataFieldItemnmbr.PopisWidth = 100;
            this.dataFieldItemnmbr.ReadOnly = true;
            this.dataFieldItemnmbr.Size = new System.Drawing.Size(277, 16);
            this.dataFieldItemnmbr.TabIndex = 0;
            // 
            // sbInfo
            // 
            this.sbInfo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.sbInfo.Location = new System.Drawing.Point(0, 546);
            this.sbInfo.Name = "sbInfo";
            this.sbInfo.Size = new System.Drawing.Size(846, 20);
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dataGrid);
            this.panelGrid.Location = new System.Drawing.Point(308, 53);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(230, 320);
            // 
            // dataGrid
            // 
            this.dataGrid.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid.CurrentRow = null;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dataGrid.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dataGrid.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dataGrid.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dataGrid.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dataGrid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.NumberFormat = "N";
            this.dataGrid.RowHeightDefault = 23;
            this.dataGrid.Size = new System.Drawing.Size(230, 320);
            this.dataGrid.Sort = "";
            this.dataGrid.SortByHeaderDoubleClick = true;
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CurrentCellChanged += new System.EventHandler(this.dataGrid_CurrentCellChanged);
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.Add(this.toolBarButtonStart);
            this.toolBar1.Buttons.Add(this.toolBarButtonLeft);
            this.toolBar1.Buttons.Add(this.toolBarButtonRight);
            this.toolBar1.Buttons.Add(this.toolBarButtonEnd);
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // toolBarButtonStart
            // 
            this.toolBarButtonStart.ImageIndex = 0;
            // 
            // toolBarButtonLeft
            // 
            this.toolBarButtonLeft.ImageIndex = 2;
            this.toolBarButtonLeft.ToolTipText = "1";
            // 
            // toolBarButtonRight
            // 
            this.toolBarButtonRight.ImageIndex = 3;
            this.toolBarButtonRight.ToolTipText = "2";
            // 
            // toolBarButtonEnd
            // 
            this.toolBarButtonEnd.ImageIndex = 1;
            this.imageList1.Images.Clear();
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            // 
            // ListPolozky
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(846, 566);
            this.ControlBox = false;
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.sbInfo);
            this.Controls.Add(this.panelDetail);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ListPolozky";
            this.Text = "Položky inventury";
            this.Deactivate += new System.EventHandler(this.ListPolozky_Deactivate);
            this.Load += new System.EventHandler(this.ListPolozky_Load);
            this.Activated += new System.EventHandler(this.ListPolozky_Activated);
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
        private Fask.Graphic.DataField dataFieldOsoba;
        private Fask.Graphic.DataField dataFieldQUANTITY;
        private Fask.Graphic.DataField dataFieldLokace;
        private Fask.Graphic.DataField dataFieldEAN;
        private StatusBar sbInfo;
        private Panel panelGrid;
        private MenuItem menuItem2;
        private MenuItem menuItemZobrazeniSwitchRezim;
        private Fask.Graphic.DataGrid2 dataGrid;
        private MenuItem menuItemZobrazeniVse;
        private Fask.Graphic.DataField dataFieldStredisko;
        private Fask.Graphic.DataField dataFieldNasnimano;
        private MenuItem menuItem5;
        private MenuItem menuItem6;
        private MenuItem menuItemHledatCarKod;
        private MenuItem menuItemNasnimane;
        private ToolBar toolBar1;
        private ToolBarButton toolBarButtonLeft;
        private ToolBarButton toolBarButtonRight;
        private ImageList imageList1;
        private MenuItem menuItemSmazat;
        private MenuItem menuItemHledatPozice;
        private MenuItem menuItem3;
        private ToolBarButton toolBarButtonStart;
        private ToolBarButton toolBarButtonEnd;
        private Fask.Graphic.DataField dataFieldKancelar;
        private MenuItem menuItemZmenaLokace;
        private MenuItem menuItemZmenaKancl;
        private MenuItem menuItemZmenaStrediska;
        private MenuItem menuItemZmenaOsoby;
        private MenuItem menuItem7;
        private MenuItem menuItem11;
        private MenuItem menuItem8;
        private MenuItem menuItemZobrazeniZbyvajici;
        private MenuItem menuItemRFID;
        private Fask.Graphic.DataField dataFieldZbyva;
        private Fask.Graphic.DataField dfKancelarNazev;

    }
}