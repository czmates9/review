namespace Fask.MST_W.ServisModule
{
    partial class ServisList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServisList));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemZobrazeni = new System.Windows.Forms.MenuItem();
            this.menuItemFiltrVseRozpracovane = new System.Windows.Forms.MenuItem();
            this.miAktualizace = new System.Windows.Forms.MenuItem();
            this.miAktualizaceCiselniky = new System.Windows.Forms.MenuItem();
            this.miAktualizaceZdrojeStavy = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miKonec = new System.Windows.Forms.MenuItem();
            this.bsZdrojeStav = new System.Windows.Forms.BindingSource(this.components);
            this.zdrojeStav = new Fask.MST_W.ServisModule.Data.ZdrojeStav();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dgZdrojID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgZdrojNazev = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgZdrojBarcode = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgZdrojModified = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgStavID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgStavNazev = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgZdrojMisto = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgCinnostID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgCinnostNazev = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgCinnostType = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgCinnostValue = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.txtSearchZdroj = new System.Windows.Forms.TextBox();
            
            ((System.ComponentModel.ISupportInitialize)(this.bsZdrojeStav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zdrojeStav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemZobrazeni);
            this.menuItem1.MenuItems.Add(this.miAktualizace);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.miKonec);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemZobrazeni
            // 
            this.menuItemZobrazeni.MenuItems.Add(this.menuItemFiltrVseRozpracovane);
            resources.ApplyResources(this.menuItemZobrazeni, "menuItemZobrazeni");
            // 
            // menuItemFiltrVseRozpracovane
            // 
            resources.ApplyResources(this.menuItemFiltrVseRozpracovane, "menuItemFiltrVseRozpracovane");
            this.menuItemFiltrVseRozpracovane.Click += new System.EventHandler(this.menuItemFiltrVseRozpracovane_Click);
            // 
            // miAktualizace
            // 
            this.miAktualizace.MenuItems.Add(this.miAktualizaceCiselniky);
            this.miAktualizace.MenuItems.Add(this.miAktualizaceZdrojeStavy);
            this.miAktualizace.MenuItems.Add(this.menuItem3);
            this.miAktualizace.MenuItems.Add(this.menuItem4);
            resources.ApplyResources(this.miAktualizace, "miAktualizace");
            // 
            // miAktualizaceCiselniky
            // 
            resources.ApplyResources(this.miAktualizaceCiselniky, "miAktualizaceCiselniky");
            this.miAktualizaceCiselniky.Click += new System.EventHandler(this.miAktualizaceCiselniky_Click);
            // 
            // miAktualizaceZdrojeStavy
            // 
            resources.ApplyResources(this.miAktualizaceZdrojeStavy, "miAktualizaceZdrojeStavy");
            this.miAktualizaceZdrojeStavy.Click += new System.EventHandler(this.miAktualizaceZdrojeStavy_Click);
            // 
            // menuItem3
            // 
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // menuItem4
            // 
            resources.ApplyResources(this.menuItem4, "menuItem4");
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem2
            // 
            resources.ApplyResources(this.menuItem2, "menuItem2");
            // 
            // miKonec
            // 
            resources.ApplyResources(this.miKonec, "miKonec");
            this.miKonec.Click += new System.EventHandler(this.miKonec_Click);
            // 
            // bsZdrojeStav
            // 
            this.bsZdrojeStav.AllowNew = false;
            this.bsZdrojeStav.DataMember = "ZdrojStav";
            this.bsZdrojeStav.DataSource = this.zdrojeStav;
            this.bsZdrojeStav.Sort = "";
            // 
            // zdrojeStav
            // 
            this.zdrojeStav.DataSetName = "ZdrojeStav";
            this.zdrojeStav.Locale = new System.Globalization.CultureInfo("");
            this.zdrojeStav.Prefix = "";
            this.zdrojeStav.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.DataSource = this.bsZdrojeStav;
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
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgZdrojID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgZdrojNazev);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgZdrojMisto);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgZdrojBarcode);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgZdrojModified);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgStavID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgStavNazev);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgCinnostID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgCinnostNazev);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgCinnostType);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgCinnostValue);
            this.dataGridTableStyle1.MappingName = "ZdrojStav";
            // 
            // dgZdrojID
            // 
            this.dgZdrojID.Alignment = System.Drawing.StringAlignment.Near;
            this.dgZdrojID.Format = "";
            this.dgZdrojID.FormatInfo = null;
            resources.ApplyResources(this.dgZdrojID, "dgZdrojID");
            this.dgZdrojID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgZdrojID.SelectionShow = false;
            this.dgZdrojID.Tag = "";
            // 
            // dgZdrojNazev
            // 
            this.dgZdrojNazev.Alignment = System.Drawing.StringAlignment.Near;
            this.dgZdrojNazev.Format = "";
            this.dgZdrojNazev.FormatInfo = null;
            resources.ApplyResources(this.dgZdrojNazev, "dgZdrojNazev");
            this.dgZdrojNazev.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgZdrojNazev.SelectionShow = false;
            this.dgZdrojNazev.Tag = "";
            // 
            // dgZdrojBarcode
            // 
            this.dgZdrojBarcode.Alignment = System.Drawing.StringAlignment.Near;
            this.dgZdrojBarcode.Format = "";
            this.dgZdrojBarcode.FormatInfo = null;
            resources.ApplyResources(this.dgZdrojBarcode, "dgZdrojBarcode");
            this.dgZdrojBarcode.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgZdrojBarcode.SelectionShow = false;
            this.dgZdrojBarcode.Tag = "";
            // 
            // dgZdrojModified
            // 
            this.dgZdrojModified.Alignment = System.Drawing.StringAlignment.Near;
            this.dgZdrojModified.Format = "";
            this.dgZdrojModified.FormatInfo = null;
            resources.ApplyResources(this.dgZdrojModified, "dgZdrojModified");
            this.dgZdrojModified.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgZdrojModified.SelectionShow = false;
            this.dgZdrojModified.Tag = "";
            // 
            // dgStavID
            // 
            this.dgStavID.Alignment = System.Drawing.StringAlignment.Near;
            this.dgStavID.Format = "";
            this.dgStavID.FormatInfo = null;
            resources.ApplyResources(this.dgStavID, "dgStavID");
            this.dgStavID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgStavID.SelectionShow = false;
            this.dgStavID.Tag = "";
            // 
            // dgStavNazev
            // 
            this.dgStavNazev.Alignment = System.Drawing.StringAlignment.Near;
            this.dgStavNazev.Format = "";
            this.dgStavNazev.FormatInfo = null;
            resources.ApplyResources(this.dgStavNazev, "dgStavNazev");
            this.dgStavNazev.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgStavNazev.SelectionShow = false;
            this.dgStavNazev.Tag = "";
            // 
            // dgCinnostID
            // 
            this.dgCinnostID.Alignment = System.Drawing.StringAlignment.Near;
            this.dgCinnostID.Format = "";
            this.dgCinnostID.FormatInfo = null;
            resources.ApplyResources(this.dgCinnostID, "dgCinnostID");
            this.dgCinnostID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgCinnostID.SelectionShow = false;
            this.dgCinnostID.Tag = "";
            // 
            // dgCinnostNazev
            // 
            this.dgCinnostNazev.Alignment = System.Drawing.StringAlignment.Near;
            this.dgCinnostNazev.Format = "";
            this.dgCinnostNazev.FormatInfo = null;
            resources.ApplyResources(this.dgCinnostNazev, "dgCinnostNazev");
            this.dgCinnostNazev.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgCinnostNazev.SelectionShow = false;
            this.dgCinnostNazev.Tag = "";
            // 
            // dgCinnostType
            // 
            this.dgCinnostType.Alignment = System.Drawing.StringAlignment.Near;
            this.dgCinnostType.Format = "";
            this.dgCinnostType.FormatInfo = null;
            resources.ApplyResources(this.dgCinnostType, "dgCinnostType");
            this.dgCinnostType.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgCinnostType.SelectionShow = false;
            this.dgCinnostType.Tag = "";
            // 
            // dgCinnostValue
            // 
            this.dgCinnostValue.Alignment = System.Drawing.StringAlignment.Near;
            this.dgCinnostValue.Format = "";
            this.dgCinnostValue.FormatInfo = null;
            resources.ApplyResources(this.dgCinnostValue, "dgCinnostValue");
            this.dgCinnostValue.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgCinnostValue.SelectionShow = false;
            this.dgCinnostValue.Tag = "";
            // 
            // statusBar1
            // 
            resources.ApplyResources(this.statusBar1, "statusBar1");
            this.statusBar1.Name = "statusBar1";
            // 
            // txtSearchZdroj
            // 
            resources.ApplyResources(this.txtSearchZdroj, "txtSearchZdroj");
            this.txtSearchZdroj.Name = "txtSearchZdroj";
            this.txtSearchZdroj.TextChanged += new System.EventHandler(this.txtSearchZdroj_TextChanged);
            // 
            // dgZdrojMisto
            // 
            this.dgZdrojMisto.Alignment = System.Drawing.StringAlignment.Near;
            this.dgZdrojMisto.Format = "";
            this.dgZdrojMisto.FormatInfo = null;
            resources.ApplyResources(this.dgZdrojMisto, "dgZdrojMisto");
            this.dgZdrojMisto.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgZdrojMisto.SelectionShow = false;
            this.dgZdrojMisto.Tag = "";
            // 
            // ServisList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.txtSearchZdroj);
            this.Controls.Add(this.statusBar1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ServisList";
            this.Load += new System.EventHandler(this.ServisMain_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ServisMain_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServisMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.bsZdrojeStav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zdrojeStav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miKonec;
        private System.Windows.Forms.MenuItem miAktualizace;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miAktualizaceCiselniky;
        private System.Windows.Forms.MenuItem miAktualizaceZdrojeStavy;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.BindingSource bsZdrojeStav;
        private Fask.MST_W.ServisModule.Data.ZdrojeStav zdrojeStav;
        private Fask.Graphic.DataGrid2TextBoxColumn dgZdrojID;
        private Fask.Graphic.DataGrid2TextBoxColumn dgZdrojNazev;
        private Fask.Graphic.DataGrid2TextBoxColumn dgZdrojMisto;
        private Fask.Graphic.DataGrid2TextBoxColumn dgZdrojBarcode;
        private Fask.Graphic.DataGrid2TextBoxColumn dgZdrojModified;
        private Fask.Graphic.DataGrid2TextBoxColumn dgStavID;
        private Fask.Graphic.DataGrid2TextBoxColumn dgStavNazev;
        private Fask.Graphic.DataGrid2TextBoxColumn dgCinnostID;
        private Fask.Graphic.DataGrid2TextBoxColumn dgCinnostNazev;
        private Fask.Graphic.DataGrid2TextBoxColumn dgCinnostType;
        private Fask.Graphic.DataGrid2TextBoxColumn dgCinnostValue;
        private System.Windows.Forms.TextBox txtSearchZdroj;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemZobrazeni;
        private System.Windows.Forms.MenuItem menuItemFiltrVseRozpracovane;
        
    }
}