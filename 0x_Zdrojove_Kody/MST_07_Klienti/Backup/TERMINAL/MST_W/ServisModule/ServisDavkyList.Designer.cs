namespace Fask.MST_W.ServisModule
{
    partial class ServisDavkyList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServisDavkyList));
            this.buttonOK = new Fask.Graphic.GraphicButton();
            this.buttonStorno = new Fask.Graphic.GraphicButton();
            this.hlavickyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgI1 = new Fask.Graphic.DataGrid2();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumnDavka = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnSOPNUMBE = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnCntItems = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.dataGridTextBoxColumnOkruhID = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.dataGridTextBoxColumnOdbID = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.dataGridTextBoxColumnOdbOznaceni = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.dataGridTextBoxColumnOdberatel = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemGenerovat = new System.Windows.Forms.MenuItem();
            this.menuItemStornovat = new System.Windows.Forms.MenuItem();
            this.dataGridTextBoxColumnBarcode = new Fask.Graphic.DataGrid2NumberBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.hlavickyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgI1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            // hlavickyBindingSource
            // 
            this.hlavickyBindingSource.DataMember = "Hlavicky";
            this.hlavickyBindingSource.DataSource = typeof(Fask.MST_W.ServisModuleWService.ServisDavky);
            this.hlavickyBindingSource.Sort = "";
            // 
            // dgI1
            // 
            this.dgI1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dgI1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgI1.CurrentRow = null;
            this.dgI1.DataSource = this.hlavickyBindingSource;
            resources.ApplyResources(this.dgI1, "dgI1");
            this.dgI1.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dgI1.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dgI1.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dgI1.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dgI1.MultiSelect = false;
            this.dgI1.Name = "dgI1";
            this.dgI1.NumberFormat = "N";
            this.dgI1.RowHeightDefault = 23;
            this.dgI1.Sort = "";
            this.dgI1.SortByHeaderDoubleClick = true;
            this.dgI1.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnDavka);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnSOPNUMBE);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnCntItems);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnOkruhID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnOdbID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnOdbOznaceni);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnOdberatel);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnBarcode);
            this.dataGridTableStyle1.MappingName = "Hlavicky";
            // 
            // dataGridTextBoxColumnDavka
            // 
            this.dataGridTextBoxColumnDavka.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnDavka.Format = "";
            this.dataGridTextBoxColumnDavka.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnDavka, "dataGridTextBoxColumnDavka");
            this.dataGridTextBoxColumnDavka.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnDavka.SelectionShow = false;
            this.dataGridTextBoxColumnDavka.Tag = "";
            // 
            // dataGridTextBoxColumnSOPNUMBE
            // 
            this.dataGridTextBoxColumnSOPNUMBE.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnSOPNUMBE.Format = "";
            this.dataGridTextBoxColumnSOPNUMBE.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnSOPNUMBE, "dataGridTextBoxColumnSOPNUMBE");
            this.dataGridTextBoxColumnSOPNUMBE.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnSOPNUMBE.SelectionShow = false;
            this.dataGridTextBoxColumnSOPNUMBE.Tag = "";
            // 
            // dataGridTextBoxColumnCntItems
            // 
            this.dataGridTextBoxColumnCntItems.Alignment = System.Drawing.StringAlignment.Far;
            this.dataGridTextBoxColumnCntItems.Format = "N";
            this.dataGridTextBoxColumnCntItems.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnCntItems, "dataGridTextBoxColumnCntItems");
            this.dataGridTextBoxColumnCntItems.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnCntItems.SelectionShow = false;
            this.dataGridTextBoxColumnCntItems.Tag = "";
            // 
            // dataGridTextBoxColumnOkruhID
            // 
            this.dataGridTextBoxColumnOkruhID.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnOkruhID.Format = "";
            this.dataGridTextBoxColumnOkruhID.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnOkruhID, "dataGridTextBoxColumnOkruhID");
            this.dataGridTextBoxColumnOkruhID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnOkruhID.SelectionShow = false;
            this.dataGridTextBoxColumnOkruhID.Tag = "";
            // 
            // dataGridTextBoxColumnOdbID
            // 
            this.dataGridTextBoxColumnOdbID.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnOdbID.Format = "";
            this.dataGridTextBoxColumnOdbID.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnOdbID, "dataGridTextBoxColumnOdbID");
            this.dataGridTextBoxColumnOdbID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnOdbID.SelectionShow = false;
            this.dataGridTextBoxColumnOdbID.Tag = "";
            // 
            // dataGridTextBoxColumnOdbOznaceni
            // 
            this.dataGridTextBoxColumnOdbOznaceni.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnOdbOznaceni.Format = "";
            this.dataGridTextBoxColumnOdbOznaceni.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnOdbOznaceni, "dataGridTextBoxColumnOdbOznaceni");
            this.dataGridTextBoxColumnOdbOznaceni.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnOdbOznaceni.SelectionShow = false;
            this.dataGridTextBoxColumnOdbOznaceni.Tag = "";
            // 
            // dataGridTextBoxColumnOdberatel
            // 
            this.dataGridTextBoxColumnOdberatel.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnOdberatel.Format = "";
            this.dataGridTextBoxColumnOdberatel.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnOdberatel, "dataGridTextBoxColumnOdberatel");
            this.dataGridTextBoxColumnOdberatel.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnOdberatel.SelectionShow = false;
            this.dataGridTextBoxColumnOdberatel.Tag = "";
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
            this.menuItem1.MenuItems.Add(this.menuItemGenerovat);
            this.menuItem1.MenuItems.Add(this.menuItemStornovat);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemGenerovat
            // 
            resources.ApplyResources(this.menuItemGenerovat, "menuItemGenerovat");
            this.menuItemGenerovat.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItemStornovat
            // 
            resources.ApplyResources(this.menuItemStornovat, "menuItemStornovat");
            this.menuItemStornovat.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // dataGridTextBoxColumnBarcode
            // 
            this.dataGridTextBoxColumnBarcode.Format = "";
            this.dataGridTextBoxColumnBarcode.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnBarcode, "dataGridTextBoxColumnBarcode");
            // 
            // ServisDavkyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dgI1);
            this.Controls.Add(this.panel1);
            this.Menu = this.mainMenu1;
            this.Name = "ServisDavkyList";
            this.Deactivate += new System.EventHandler(this.PrijemDavkyList_Deactivate);
            this.Load += new System.EventHandler(this.PrijemDavkyList_Load);
            this.Activated += new System.EventHandler(this.PrijemDavkyList_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrijemDavkyList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.hlavickyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgI1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton buttonOK;
        private Fask.Graphic.GraphicButton buttonStorno;
        private Fask.Graphic.DataGrid2 dgI1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnDavka;
        private System.Windows.Forms.BindingSource hlavickyBindingSource;
        private System.Windows.Forms.Panel panel1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnSOPNUMBE;
        private Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumnCntItems;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemGenerovat;
        private System.Windows.Forms.MenuItem menuItemStornovat;
        private Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumnOkruhID;
        private Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumnOdbID;
        private Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumnOdbOznaceni;
        private Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumnOdberatel;
        private Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumnBarcode;
    }
}