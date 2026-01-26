namespace Fask.MST_W.ServisModule
{
    partial class ServisStavyZmena
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
            this.bsServis = new System.Windows.Forms.BindingSource(this.components);
            this.ds_servis = new Fask.SQLiteDBs.DataSets.Servis();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dgID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgOznaceni = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgBarcode = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgCinnostID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgCinnostNazev = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemOK = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemCancel = new System.Windows.Forms.MenuItem();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.zpet_but = new Fask.Graphic.GraphicButton();
            this.ok_but = new Fask.Graphic.GraphicButton();
            this.statusBarInfo = new System.Windows.Forms.StatusBar();
            ((System.ComponentModel.ISupportInitialize)(this.bsServis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_servis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // bsServis
            // 
            this.bsServis.AllowNew = false;
            this.bsServis.DataMember = "CZMST_Servis_Stav";
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
            this.dataGrid1.Size = new System.Drawing.Size(429, 361);
            this.dataGrid1.Sort = "";
            this.dataGrid1.SortByHeaderDoubleClick = true;
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            this.dataGrid1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGrid1_KeyPress);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgOznaceni);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgBarcode);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgCinnostID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgCinnostNazev);
            this.dataGridTableStyle1.MappingName = "CZMST_Servis_Stav";
            // 
            // dgID
            // 
            this.dgID.Alignment = System.Drawing.StringAlignment.Near;
            this.dgID.Format = "";
            this.dgID.FormatInfo = null;
            this.dgID.HeaderText = "ID";
            this.dgID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgID.MappingName = "ID";
            this.dgID.NullText = "-";
            this.dgID.SelectionShow = false;
            this.dgID.Tag = "";
            // 
            // dgOznaceni
            // 
            this.dgOznaceni.Alignment = System.Drawing.StringAlignment.Near;
            this.dgOznaceni.Format = "";
            this.dgOznaceni.FormatInfo = null;
            this.dgOznaceni.HeaderText = "Název";
            this.dgOznaceni.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgOznaceni.MappingName = "Oznaceni";
            this.dgOznaceni.NullText = "-";
            this.dgOznaceni.SelectionShow = false;
            this.dgOznaceni.Tag = "";
            // 
            // dgBarcode
            // 
            this.dgBarcode.Alignment = System.Drawing.StringAlignment.Near;
            this.dgBarcode.Format = "";
            this.dgBarcode.FormatInfo = null;
            this.dgBarcode.HeaderText = "Čár. kód";
            this.dgBarcode.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgBarcode.MappingName = "Barcode";
            this.dgBarcode.NullText = "-";
            this.dgBarcode.SelectionShow = false;
            this.dgBarcode.Tag = "";
            // 
            // dgCinnostID
            // 
            this.dgCinnostID.Alignment = System.Drawing.StringAlignment.Near;
            this.dgCinnostID.Format = "";
            this.dgCinnostID.FormatInfo = null;
            this.dgCinnostID.HeaderText = "Činnost ID";
            this.dgCinnostID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgCinnostID.MappingName = "IDCinnost";
            this.dgCinnostID.NullText = "-";
            this.dgCinnostID.SelectionShow = false;
            this.dgCinnostID.Tag = "";
            // 
            // dgCinnostNazev
            // 
            this.dgCinnostNazev.Alignment = System.Drawing.StringAlignment.Near;
            this.dgCinnostNazev.Format = "";
            this.dgCinnostNazev.FormatInfo = null;
            this.dgCinnostNazev.HeaderText = "Název činnosti";
            this.dgCinnostNazev.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgCinnostNazev.MappingName = "CinnostNazev";
            this.dgCinnostNazev.NullText = "-";
            this.dgCinnostNazev.SelectionShow = false;
            this.dgCinnostNazev.Tag = "";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemOK);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItemCancel);
            this.menuItem1.Text = "Menu";
            // 
            // menuItemOK
            // 
            this.menuItemOK.Text = "OK";
            this.menuItemOK.Click += new System.EventHandler(this.menuItemOK_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Krok zpět";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // menuItemCancel
            // 
            this.menuItemCancel.Text = "Storno";
            this.menuItemCancel.Click += new System.EventHandler(this.menuItemCancel_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.zpet_but);
            this.panelButtons.Controls.Add(this.ok_but);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 385);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(429, 38);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // zpet_but
            // 
            this.zpet_but.BitmapNormal = null;
            this.zpet_but.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zpet_but.FocusMargin = 5;
            this.zpet_but.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.zpet_but.Location = new System.Drawing.Point(0, 0);
            this.zpet_but.Name = "zpet_but";
            this.zpet_but.Pressed = false;
            this.zpet_but.Size = new System.Drawing.Size(303, 38);
            this.zpet_but.TabIndex = 17;
            this.zpet_but.Text = "Storno";
            this.zpet_but.Transparent = System.Drawing.Color.White;
            this.zpet_but.Click += new System.EventHandler(this.zpet_but_Click_1);
            // 
            // ok_but
            // 
            this.ok_but.BitmapNormal = null;
            this.ok_but.Dock = System.Windows.Forms.DockStyle.Right;
            this.ok_but.FocusMargin = 5;
            this.ok_but.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.ok_but.Location = new System.Drawing.Point(303, 0);
            this.ok_but.Name = "ok_but";
            this.ok_but.Pressed = false;
            this.ok_but.Size = new System.Drawing.Size(126, 38);
            this.ok_but.TabIndex = 16;
            this.ok_but.Text = "OK";
            this.ok_but.Transparent = System.Drawing.Color.White;
            this.ok_but.Click += new System.EventHandler(this.ok_but_Click_1);
            // 
            // statusBarInfo
            // 
            this.statusBarInfo.Location = new System.Drawing.Point(0, 361);
            this.statusBarInfo.Name = "statusBarInfo";
            this.statusBarInfo.Size = new System.Drawing.Size(429, 24);
            // 
            // ServisStavyZmena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(429, 423);
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.statusBarInfo);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ServisStavyZmena";
            this.Text = "Stavy - možnosti";
            this.Load += new System.EventHandler(this.ServisStavyZmena_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServisStavyZmena_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.bsServis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_servis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemOK;
        private System.Windows.Forms.MenuItem menuItemCancel;
        private System.Windows.Forms.BindingSource bsServis;
        private Fask.SQLiteDBs.DataSets.Servis ds_servis;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dgID;
        private Fask.Graphic.DataGrid2TextBoxColumn dgOznaceni;
        private Fask.Graphic.DataGrid2TextBoxColumn dgBarcode;
        private Fask.Graphic.DataGrid2TextBoxColumn dgCinnostID;
        private Fask.Graphic.DataGrid2TextBoxColumn dgCinnostNazev;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        protected System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton zpet_but;
        private Fask.Graphic.GraphicButton ok_but;
        private System.Windows.Forms.StatusBar statusBarInfo;
    }
}