namespace Fask.MST_W.Inventura2
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
            this.menuItemRFIDON = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemKonec = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemZpracuj = new System.Windows.Forms.MenuItem();
            this.menuItemZpracujVse = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemSmazat = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItemNalezene = new System.Windows.Forms.MenuItem();
            this.menuItemNenalezene = new System.Windows.Forms.MenuItem();
            this.menuItemVice = new System.Windows.Forms.MenuItem();
            this.menuItemSplnene = new System.Windows.Forms.MenuItem();
            this.polozkyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listPolozkyDS = new Fask.MST_W.Inventura2.ListPolozkyDS();
            this.dataGrid2Polozky = new Fask.Graphic.DataGrid2();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumnID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnKategorie = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnICISLO = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnNazev = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnKusu = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnNacteno = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnZbyva = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnEAN = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnStrediskoID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnStrediskoNazev = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnOsobaID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnOsobaNazev = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnLokace1 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnLokace2 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnLokaceID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnLokaceNazev = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnKancelarID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnKanclNazev = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnCasNacteno = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButtonRFID = new System.Windows.Forms.ToolBarButton();
            this.imageListRFID = new System.Windows.Forms.ImageList();
            this.panelData = new System.Windows.Forms.Panel();
            this.textBoxS = new System.Windows.Forms.TextBox();
            this.textBoxV = new System.Windows.Forms.TextBox();
            this.textBoxN = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.polozkyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listPolozkyDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid2Polozky)).BeginInit();
            this.panelData.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemRFIDON);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItemKonec);
            this.menuItem1.Text = "Menu";
            // 
            // menuItemRFIDON
            // 
            this.menuItemRFIDON.Text = "RFID (F1)";
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
            this.menuItem2.MenuItems.Add(this.menuItemZpracuj);
            this.menuItem2.MenuItems.Add(this.menuItemZpracujVse);
            this.menuItem2.MenuItems.Add(this.menuItem4);
            this.menuItem2.MenuItems.Add(this.menuItemSmazat);
            this.menuItem2.MenuItems.Add(this.menuItem5);
            this.menuItem2.MenuItems.Add(this.menuItemNalezene);
            this.menuItem2.MenuItems.Add(this.menuItemNenalezene);
            this.menuItem2.MenuItems.Add(this.menuItemVice);
            this.menuItem2.MenuItems.Add(this.menuItemSplnene);
            this.menuItem2.Text = "Akce";
            // 
            // menuItemZpracuj
            // 
            this.menuItemZpracuj.Text = "Zpracuj položku (Ent)";
            this.menuItemZpracuj.Click += new System.EventHandler(this.menuItemZpracuj_Click);
            // 
            // menuItemZpracujVse
            // 
            this.menuItemZpracujVse.Text = "Zpracuj vše (F2)";
            this.menuItemZpracujVse.Click += new System.EventHandler(this.menuItemZpracujVse_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // menuItemSmazat
            // 
            this.menuItemSmazat.Text = "Smazat (Bksp)";
            this.menuItemSmazat.Click += new System.EventHandler(this.menuItemSmazat_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "-";
            // 
            // menuItemNalezene
            // 
            this.menuItemNalezene.Text = "Nalezené";
            this.menuItemNalezene.Click += new System.EventHandler(this.menuItemNalezene_Click);
            // 
            // menuItemNenalezene
            // 
            this.menuItemNenalezene.Text = "Nenalezené";
            this.menuItemNenalezene.Click += new System.EventHandler(this.menuItemNenalezene_Click);
            // 
            // menuItemVice
            // 
            this.menuItemVice.Text = "Více záznamů";
            this.menuItemVice.Click += new System.EventHandler(this.menuItemVice_Click);
            // 
            // menuItemSplnene
            // 
            this.menuItemSplnene.Text = "Splněné";
            this.menuItemSplnene.Click += new System.EventHandler(this.menuItemSplnene_Click);
            // 
            // polozkyBindingSource
            // 
            this.polozkyBindingSource.DataMember = "Polozky";
            this.polozkyBindingSource.DataSource = this.listPolozkyDS;
            this.polozkyBindingSource.Sort = "";
            // 
            // listPolozkyDS
            // 
            this.listPolozkyDS.DataSetName = "ListPolozkyDS";
            this.listPolozkyDS.Locale = new System.Globalization.CultureInfo("");
            this.listPolozkyDS.Prefix = "";
            this.listPolozkyDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGrid2Polozky
            // 
            this.dataGrid2Polozky.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid2Polozky.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid2Polozky.CurrentRow = null;
            this.dataGrid2Polozky.DataSource = this.polozkyBindingSource;
            this.dataGrid2Polozky.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dataGrid2Polozky.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dataGrid2Polozky.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dataGrid2Polozky.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dataGrid2Polozky.Location = new System.Drawing.Point(3, 6);
            this.dataGrid2Polozky.MultiSelect = false;
            this.dataGrid2Polozky.Name = "dataGrid2Polozky";
            this.dataGrid2Polozky.NumberFormat = "N";
            this.dataGrid2Polozky.RowHeightDefault = 23;
            this.dataGrid2Polozky.Size = new System.Drawing.Size(540, 259);
            this.dataGrid2Polozky.Sort = "";
            this.dataGrid2Polozky.SortByHeaderDoubleClick = true;
            this.dataGrid2Polozky.TabIndex = 0;
            this.dataGrid2Polozky.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnKategorie);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnICISLO);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnNazev);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnKusu);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnNacteno);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnZbyva);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnEAN);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnStrediskoID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnStrediskoNazev);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnOsobaID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnOsobaNazev);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnLokace1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnLokace2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnLokaceID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnLokaceNazev);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnKancelarID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnKanclNazev);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnCasNacteno);
            this.dataGridTableStyle1.MappingName = "Polozky";
            // 
            // dataGridTextBoxColumnID
            // 
            this.dataGridTextBoxColumnID.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnID.Format = "";
            this.dataGridTextBoxColumnID.FormatInfo = null;
            this.dataGridTextBoxColumnID.HeaderText = "ID";
            this.dataGridTextBoxColumnID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnID.MappingName = "ID";
            this.dataGridTextBoxColumnID.SelectionShow = false;
            this.dataGridTextBoxColumnID.Tag = "";
            // 
            // dataGridTextBoxColumnKategorie
            // 
            this.dataGridTextBoxColumnKategorie.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnKategorie.Format = "";
            this.dataGridTextBoxColumnKategorie.FormatInfo = null;
            this.dataGridTextBoxColumnKategorie.HeaderText = "Kategorie";
            this.dataGridTextBoxColumnKategorie.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnKategorie.MappingName = "KATEGORIE";
            this.dataGridTextBoxColumnKategorie.SelectionShow = false;
            this.dataGridTextBoxColumnKategorie.Tag = "";
            // 
            // dataGridTextBoxColumnICISLO
            // 
            this.dataGridTextBoxColumnICISLO.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnICISLO.Format = "";
            this.dataGridTextBoxColumnICISLO.FormatInfo = null;
            this.dataGridTextBoxColumnICISLO.HeaderText = "Interní číslo";
            this.dataGridTextBoxColumnICISLO.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnICISLO.MappingName = "I_CISLO";
            this.dataGridTextBoxColumnICISLO.SelectionShow = false;
            this.dataGridTextBoxColumnICISLO.Tag = "";
            // 
            // dataGridTextBoxColumnNazev
            // 
            this.dataGridTextBoxColumnNazev.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnNazev.Format = "";
            this.dataGridTextBoxColumnNazev.FormatInfo = null;
            this.dataGridTextBoxColumnNazev.HeaderText = "Název";
            this.dataGridTextBoxColumnNazev.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnNazev.MappingName = "NAZEV";
            this.dataGridTextBoxColumnNazev.SelectionShow = false;
            this.dataGridTextBoxColumnNazev.Tag = "";
            // 
            // dataGridTextBoxColumnKusu
            // 
            this.dataGridTextBoxColumnKusu.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnKusu.Format = "";
            this.dataGridTextBoxColumnKusu.FormatInfo = null;
            this.dataGridTextBoxColumnKusu.HeaderText = "Kusů";
            this.dataGridTextBoxColumnKusu.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnKusu.MappingName = "KUSU";
            this.dataGridTextBoxColumnKusu.SelectionShow = false;
            this.dataGridTextBoxColumnKusu.Tag = "";
            // 
            // dataGridTextBoxColumnNacteno
            // 
            this.dataGridTextBoxColumnNacteno.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnNacteno.Format = "";
            this.dataGridTextBoxColumnNacteno.FormatInfo = null;
            this.dataGridTextBoxColumnNacteno.HeaderText = "Načteno";
            this.dataGridTextBoxColumnNacteno.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnNacteno.MappingName = "NACTENO";
            this.dataGridTextBoxColumnNacteno.SelectionShow = false;
            this.dataGridTextBoxColumnNacteno.Tag = "";
            // 
            // dataGridTextBoxColumnZbyva
            // 
            this.dataGridTextBoxColumnZbyva.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnZbyva.Format = "";
            this.dataGridTextBoxColumnZbyva.FormatInfo = null;
            this.dataGridTextBoxColumnZbyva.HeaderText = "Zbývá";
            this.dataGridTextBoxColumnZbyva.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnZbyva.MappingName = "ZBYVA";
            this.dataGridTextBoxColumnZbyva.SelectionShow = false;
            this.dataGridTextBoxColumnZbyva.Tag = "";
            // 
            // dataGridTextBoxColumnEAN
            // 
            this.dataGridTextBoxColumnEAN.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnEAN.Format = "";
            this.dataGridTextBoxColumnEAN.FormatInfo = null;
            this.dataGridTextBoxColumnEAN.HeaderText = "EAN";
            this.dataGridTextBoxColumnEAN.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnEAN.MappingName = "EAN";
            this.dataGridTextBoxColumnEAN.SelectionShow = false;
            this.dataGridTextBoxColumnEAN.Tag = "";
            // 
            // dataGridTextBoxColumnStrediskoID
            // 
            this.dataGridTextBoxColumnStrediskoID.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnStrediskoID.Format = "";
            this.dataGridTextBoxColumnStrediskoID.FormatInfo = null;
            this.dataGridTextBoxColumnStrediskoID.HeaderText = "Středisko ID";
            this.dataGridTextBoxColumnStrediskoID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnStrediskoID.MappingName = "STRED";
            this.dataGridTextBoxColumnStrediskoID.SelectionShow = false;
            this.dataGridTextBoxColumnStrediskoID.Tag = "";
            // 
            // dataGridTextBoxColumnStrediskoNazev
            // 
            this.dataGridTextBoxColumnStrediskoNazev.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnStrediskoNazev.Format = "";
            this.dataGridTextBoxColumnStrediskoNazev.FormatInfo = null;
            this.dataGridTextBoxColumnStrediskoNazev.HeaderText = "Středisko název";
            this.dataGridTextBoxColumnStrediskoNazev.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnStrediskoNazev.MappingName = "STRED_NAZEV";
            this.dataGridTextBoxColumnStrediskoNazev.SelectionShow = false;
            this.dataGridTextBoxColumnStrediskoNazev.Tag = "";
            // 
            // dataGridTextBoxColumnOsobaID
            // 
            this.dataGridTextBoxColumnOsobaID.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnOsobaID.Format = "";
            this.dataGridTextBoxColumnOsobaID.FormatInfo = null;
            this.dataGridTextBoxColumnOsobaID.HeaderText = "Osoba ID";
            this.dataGridTextBoxColumnOsobaID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnOsobaID.MappingName = "OSOBA";
            this.dataGridTextBoxColumnOsobaID.SelectionShow = false;
            this.dataGridTextBoxColumnOsobaID.Tag = "";
            // 
            // dataGridTextBoxColumnOsobaNazev
            // 
            this.dataGridTextBoxColumnOsobaNazev.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnOsobaNazev.Format = "";
            this.dataGridTextBoxColumnOsobaNazev.FormatInfo = null;
            this.dataGridTextBoxColumnOsobaNazev.HeaderText = "Osoba název";
            this.dataGridTextBoxColumnOsobaNazev.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnOsobaNazev.MappingName = "OSOBA_NAZEV";
            this.dataGridTextBoxColumnOsobaNazev.SelectionShow = false;
            this.dataGridTextBoxColumnOsobaNazev.Tag = "";
            // 
            // dataGridTextBoxColumnLokace1
            // 
            this.dataGridTextBoxColumnLokace1.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnLokace1.Format = "";
            this.dataGridTextBoxColumnLokace1.FormatInfo = null;
            this.dataGridTextBoxColumnLokace1.HeaderText = "Lokace1";
            this.dataGridTextBoxColumnLokace1.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnLokace1.MappingName = "LOKACE1";
            this.dataGridTextBoxColumnLokace1.SelectionShow = false;
            this.dataGridTextBoxColumnLokace1.Tag = "";
            // 
            // dataGridTextBoxColumnLokace2
            // 
            this.dataGridTextBoxColumnLokace2.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnLokace2.Format = "";
            this.dataGridTextBoxColumnLokace2.FormatInfo = null;
            this.dataGridTextBoxColumnLokace2.HeaderText = "Lokace2";
            this.dataGridTextBoxColumnLokace2.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnLokace2.MappingName = "LOKACE2";
            this.dataGridTextBoxColumnLokace2.SelectionShow = false;
            this.dataGridTextBoxColumnLokace2.Tag = "";
            // 
            // dataGridTextBoxColumnLokaceID
            // 
            this.dataGridTextBoxColumnLokaceID.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnLokaceID.Format = "";
            this.dataGridTextBoxColumnLokaceID.FormatInfo = null;
            this.dataGridTextBoxColumnLokaceID.HeaderText = "Lokace ID";
            this.dataGridTextBoxColumnLokaceID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnLokaceID.MappingName = "KLIC_LOK";
            this.dataGridTextBoxColumnLokaceID.SelectionShow = false;
            this.dataGridTextBoxColumnLokaceID.Tag = "";
            // 
            // dataGridTextBoxColumnLokaceNazev
            // 
            this.dataGridTextBoxColumnLokaceNazev.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnLokaceNazev.Format = "";
            this.dataGridTextBoxColumnLokaceNazev.FormatInfo = null;
            this.dataGridTextBoxColumnLokaceNazev.HeaderText = "Lokace název";
            this.dataGridTextBoxColumnLokaceNazev.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnLokaceNazev.MappingName = "LOKACE_NAZEV";
            this.dataGridTextBoxColumnLokaceNazev.SelectionShow = false;
            this.dataGridTextBoxColumnLokaceNazev.Tag = "";
            // 
            // dataGridTextBoxColumnKancelarID
            // 
            this.dataGridTextBoxColumnKancelarID.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnKancelarID.Format = "";
            this.dataGridTextBoxColumnKancelarID.FormatInfo = null;
            this.dataGridTextBoxColumnKancelarID.HeaderText = "Kancelář ID";
            this.dataGridTextBoxColumnKancelarID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnKancelarID.MappingName = "KANCELAR";
            this.dataGridTextBoxColumnKancelarID.SelectionShow = false;
            this.dataGridTextBoxColumnKancelarID.Tag = "";
            // 
            // dataGridTextBoxColumnKanclNazev
            // 
            this.dataGridTextBoxColumnKanclNazev.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnKanclNazev.Format = "";
            this.dataGridTextBoxColumnKanclNazev.FormatInfo = null;
            this.dataGridTextBoxColumnKanclNazev.HeaderText = "Kancelář název";
            this.dataGridTextBoxColumnKanclNazev.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnKanclNazev.MappingName = "KANCELAR_NAZEV";
            this.dataGridTextBoxColumnKanclNazev.SelectionShow = false;
            this.dataGridTextBoxColumnKanclNazev.Tag = "";
            // 
            // dataGridTextBoxColumnCasNacteno
            // 
            this.dataGridTextBoxColumnCasNacteno.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnCasNacteno.Format = "HH:mm:ss";
            this.dataGridTextBoxColumnCasNacteno.FormatInfo = null;
            this.dataGridTextBoxColumnCasNacteno.HeaderText = "Čas načtení";
            this.dataGridTextBoxColumnCasNacteno.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnCasNacteno.MappingName = "CASNACTENO";
            this.dataGridTextBoxColumnCasNacteno.SelectionShow = false;
            this.dataGridTextBoxColumnCasNacteno.Tag = "";
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.statusBar1.Location = new System.Drawing.Point(0, 303);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(782, 19);
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
            this.panelData.Controls.Add(this.textBoxS);
            this.panelData.Controls.Add(this.textBoxV);
            this.panelData.Controls.Add(this.textBoxN);
            this.panelData.Controls.Add(this.dataGrid2Polozky);
            this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelData.Location = new System.Drawing.Point(0, 24);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(782, 279);
            // 
            // textBoxS
            // 
            this.textBoxS.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.textBoxS.Location = new System.Drawing.Point(576, 150);
            this.textBoxS.Multiline = true;
            this.textBoxS.Name = "textBoxS";
            this.textBoxS.ReadOnly = true;
            this.textBoxS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxS.Size = new System.Drawing.Size(194, 66);
            this.textBoxS.TabIndex = 1;
            // 
            // textBoxV
            // 
            this.textBoxV.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.textBoxV.Location = new System.Drawing.Point(576, 78);
            this.textBoxV.Multiline = true;
            this.textBoxV.Name = "textBoxV";
            this.textBoxV.ReadOnly = true;
            this.textBoxV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxV.Size = new System.Drawing.Size(194, 66);
            this.textBoxV.TabIndex = 1;
            // 
            // textBoxN
            // 
            this.textBoxN.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.textBoxN.Location = new System.Drawing.Point(576, 6);
            this.textBoxN.Multiline = true;
            this.textBoxN.Name = "textBoxN";
            this.textBoxN.ReadOnly = true;
            this.textBoxN.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxN.Size = new System.Drawing.Size(194, 66);
            this.textBoxN.TabIndex = 1;
            // 
            // SnimatRFID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(782, 322);
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
            ((System.ComponentModel.ISupportInitialize)(this.polozkyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listPolozkyDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid2Polozky)).EndInit();
            this.panelData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.DataGrid2 dataGrid2Polozky;
        private System.Windows.Forms.MenuItem menuItem1;
        private ListPolozkyDS listPolozkyDS;
        private System.Windows.Forms.BindingSource polozkyBindingSource;
        private System.Windows.Forms.MenuItem menuItemKonec;
        private System.Windows.Forms.MenuItem menuItemRFIDON;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItemZpracuj;
        private System.Windows.Forms.MenuItem menuItemZpracujVse;
        private System.Windows.Forms.MenuItem menuItemSmazat;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnID;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnKategorie;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnICISLO;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnNazev;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnStrediskoID;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnEAN;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnStrediskoNazev;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnOsobaID;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnOsobaNazev;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnLokace1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnLokace2;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnLokaceID;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnLokaceNazev;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnKancelarID;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnKanclNazev;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnKusu;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnNacteno;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnZbyva;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnCasNacteno;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton toolBarButtonRFID;
        private System.Windows.Forms.ImageList imageListRFID;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItemNalezene;
        private System.Windows.Forms.MenuItem menuItemNenalezene;
        private System.Windows.Forms.MenuItem menuItemVice;
        private System.Windows.Forms.MenuItem menuItemSplnene;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.TextBox textBoxS;
        private System.Windows.Forms.TextBox textBoxV;
        private System.Windows.Forms.TextBox textBoxN;
    }
}