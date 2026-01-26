namespace Fask.MST_W.Prodej_3.RFID
{
    partial class ZobrazRFID
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemKonec = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miPotvrdit = new System.Windows.Forms.MenuItem();
            this.menuItemSmazat = new System.Windows.Forms.MenuItem();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.panelData = new System.Windows.Forms.Panel();
            this.panelList = new System.Windows.Forms.Panel();
            this.bsPolozkyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid2Polozky = new Fask.Graphic.DataGrid2();
            this.dsZobrazitRFID = new Fask.MST_W.Prodej_3.RFID.ZobrazitRFID();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn2 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn3 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn4 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn5 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.panelData.SuspendLayout();
            this.panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPolozkyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid2Polozky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsZobrazitRFID)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemKonec);
            this.menuItem1.Text = "Menu";
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
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.statusBar1.Location = new System.Drawing.Point(0, 319);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(726, 19);
            // 
            // panelData
            // 
            this.panelData.Controls.Add(this.panelList);
            this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelData.Location = new System.Drawing.Point(0, 0);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(726, 319);
            // 
            // panelList
            // 
            this.panelList.Controls.Add(this.dataGrid2Polozky);
            this.panelList.Location = new System.Drawing.Point(40, 30);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(286, 254);
            // 
            // bsPolozkyBindingSource
            // 
            this.bsPolozkyBindingSource.DataMember = "Polozky";
            this.bsPolozkyBindingSource.DataSource = this.dsZobrazitRFID;
            this.bsPolozkyBindingSource.Sort = "";
            // 
            // dataGrid2Polozky
            // 
            this.dataGrid2Polozky.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid2Polozky.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid2Polozky.CurrentRow = null;
            this.dataGrid2Polozky.DataSource = this.bsPolozkyBindingSource;
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
            this.dataGrid2Polozky.Size = new System.Drawing.Size(286, 254);
            this.dataGrid2Polozky.Sort = "";
            this.dataGrid2Polozky.SortByHeaderDoubleClick = true;
            this.dataGrid2Polozky.TabIndex = 0;
            this.dataGrid2Polozky.TableStyles.Add(this.dataGridTableStyle1);
            this.dataGrid2Polozky.CurrentRowIndexChanged += new System.EventHandler(this.dataGrid2Polozky_CurrentRowIndexChanged);
            // 
            // dsZobrazitRFID
            // 
            this.dsZobrazitRFID.DataSetName = "ZobrazitRFID";
            this.dsZobrazitRFID.Prefix = "";
            this.dsZobrazitRFID.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
            this.dataGridTableStyle1.MappingName = "Polozky";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Pol.č.";
            this.dataGridTextBoxColumn1.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn1.MappingName = "Itemnmbr";
            this.dataGridTextBoxColumn1.NullText = "?";
            this.dataGridTextBoxColumn1.SelectionShow = false;
            this.dataGridTextBoxColumn1.Tag = "";
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Název";
            this.dataGridTextBoxColumn2.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn2.MappingName = "ItemDesc";
            this.dataGridTextBoxColumn2.NullText = "?";
            this.dataGridTextBoxColumn2.SelectionShow = false;
            this.dataGridTextBoxColumn2.Tag = "";
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Čár. kód";
            this.dataGridTextBoxColumn3.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn3.MappingName = "Ean";
            this.dataGridTextBoxColumn3.NullText = "?";
            this.dataGridTextBoxColumn3.SelectionShow = false;
            this.dataGridTextBoxColumn3.Tag = "";
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "Šarže / SN";
            this.dataGridTextBoxColumn4.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn4.MappingName = "Serialnmbr";
            this.dataGridTextBoxColumn4.NullText = "?";
            this.dataGridTextBoxColumn4.SelectionShow = false;
            this.dataGridTextBoxColumn4.Tag = "";
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "Množství";
            this.dataGridTextBoxColumn5.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn5.MappingName = "mnozstvi";
            this.dataGridTextBoxColumn5.NullText = "?";
            this.dataGridTextBoxColumn5.SelectionShow = false;
            this.dataGridTextBoxColumn5.Tag = "";
            // 
            // ZobrazRFID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(726, 338);
            this.ControlBox = false;
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.statusBar1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ZobrazRFID";
            this.Text = "Nasnímané položky";
            this.Deactivate += new System.EventHandler(this.SnimatRFID_Deactivate);
            this.Load += new System.EventHandler(this.SnimatRFID_Load);
            this.Activated += new System.EventHandler(this.SnimatRFID_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SnimatRFID_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SnimatRFID_KeyDown);
            this.panelData.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsPolozkyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid2Polozky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsZobrazitRFID)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.DataGrid2 dataGrid2Polozky;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemKonec;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItemSmazat;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.MenuItem miPotvrdit;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.BindingSource bsPolozkyBindingSource;
        private ZobrazitRFID dsZobrazitRFID;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn2;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn3;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn4;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn5;
    }
}