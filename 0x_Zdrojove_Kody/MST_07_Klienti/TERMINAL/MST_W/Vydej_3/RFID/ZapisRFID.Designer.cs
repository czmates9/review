namespace Fask.MST_W.Vydej_3.RFID
{
    partial class ZapisRFID
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
            this.menuItemZobrazeniRezim = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemKonec = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miRFIDZapsatInformace = new System.Windows.Forms.MenuItem();
            this.miKonecAUloz = new System.Windows.Forms.MenuItem();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.panelData = new System.Windows.Forms.Panel();
            this.panelListZapsano = new System.Windows.Forms.Panel();
            this.bsCZMSTSIRFIDZapsano = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridZapsano = new Fask.Graphic.DataGrid2();
            this.dsVydejZapsano = new Fask.SQLiteDBs.DataSets.Vydej();
            this.label2 = new System.Windows.Forms.Label();
            this.panelListZapsat = new System.Windows.Forms.Panel();
            this.bsCZMSTSIRFID = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridZapsat = new Fask.Graphic.DataGrid2();
            this.dsVydej = new Fask.SQLiteDBs.DataSets.Vydej();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn2 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn3 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn4 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTableStyle2 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn5 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn6 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn7 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn8 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.panelData.SuspendLayout();
            this.panelListZapsano.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsCZMSTSIRFIDZapsano)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridZapsano)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVydejZapsano)).BeginInit();
            this.panelListZapsat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsCZMSTSIRFID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridZapsat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVydej)).BeginInit();
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
            // menuItemKonec
            // 
            this.menuItemKonec.Text = "Konec (Esc)";
            this.menuItemKonec.Click += new System.EventHandler(this.menuItemKonec_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.miRFIDZapsatInformace);
            this.menuItem2.MenuItems.Add(this.miKonecAUloz);
            this.menuItem2.Text = "Akce";
            // 
            // miRFIDZapsatInformace
            // 
            this.miRFIDZapsatInformace.Text = "Zapsat informace (3)";
            this.miRFIDZapsatInformace.Click += new System.EventHandler(this.miRFIDZapsatInformace_Click);
            // 
            // miKonecAUloz
            // 
            this.miKonecAUloz.Text = "Ukončit a uložit (Ent)";
            this.miKonecAUloz.Click += new System.EventHandler(this.miKonecAUloz_Click);
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.statusBar1.Location = new System.Drawing.Point(0, 398);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(749, 19);
            // 
            // panelData
            // 
            this.panelData.Controls.Add(this.panelListZapsano);
            this.panelData.Controls.Add(this.panelListZapsat);
            this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelData.Location = new System.Drawing.Point(0, 0);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(749, 398);
            // 
            // panelListZapsano
            // 
            this.panelListZapsano.BackColor = System.Drawing.Color.Green;
            this.panelListZapsano.Controls.Add(this.dataGridZapsano);
            this.panelListZapsano.Controls.Add(this.label2);
            this.panelListZapsano.Location = new System.Drawing.Point(286, 30);
            this.panelListZapsano.Name = "panelListZapsano";
            this.panelListZapsano.Size = new System.Drawing.Size(218, 192);
            // 
            // bsCZMSTSIRFIDZapsano
            // 
            this.bsCZMSTSIRFIDZapsano.DataMember = "CZMST_SI_RFID";
            this.bsCZMSTSIRFIDZapsano.DataSource = this.dsVydejZapsano;
            this.bsCZMSTSIRFIDZapsano.Sort = "";
            // 
            // dataGridZapsano
            // 
            this.dataGridZapsano.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGridZapsano.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGridZapsano.CurrentRow = null;
            this.dataGridZapsano.DataSource = this.bsCZMSTSIRFIDZapsano;
            this.dataGridZapsano.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridZapsano.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dataGridZapsano.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dataGridZapsano.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dataGridZapsano.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dataGridZapsano.Location = new System.Drawing.Point(0, 13);
            this.dataGridZapsano.MultiSelect = false;
            this.dataGridZapsano.Name = "dataGridZapsano";
            this.dataGridZapsano.NumberFormat = "N";
            this.dataGridZapsano.RowHeightDefault = 23;
            this.dataGridZapsano.Size = new System.Drawing.Size(218, 179);
            this.dataGridZapsano.Sort = "";
            this.dataGridZapsano.SortByHeaderDoubleClick = true;
            this.dataGridZapsano.TabIndex = 1;
            this.dataGridZapsano.TableStyles.Add(this.dataGridTableStyle2);
            // 
            // dsVydejZapsano
            // 
            this.dsVydejZapsano.DataSetName = "Vydej";
            this.dsVydejZapsano.Locale = new System.Globalization.CultureInfo("");
            this.dsVydejZapsano.Prefix = "";
            this.dsVydejZapsano.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightGreen;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 13);
            this.label2.Text = "Zapsáno";
            // 
            // panelListZapsat
            // 
            this.panelListZapsat.BackColor = System.Drawing.Color.LightCoral;
            this.panelListZapsat.Controls.Add(this.dataGridZapsat);
            this.panelListZapsat.Controls.Add(this.label1);
            this.panelListZapsat.Location = new System.Drawing.Point(40, 30);
            this.panelListZapsat.Name = "panelListZapsat";
            this.panelListZapsat.Size = new System.Drawing.Size(207, 192);
            // 
            // bsCZMSTSIRFID
            // 
            this.bsCZMSTSIRFID.DataMember = "CZMST_SI_RFID";
            this.bsCZMSTSIRFID.DataSource = this.dsVydej;
            this.bsCZMSTSIRFID.Sort = "";
            // 
            // dataGridZapsat
            // 
            this.dataGridZapsat.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGridZapsat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGridZapsat.CurrentRow = null;
            this.dataGridZapsat.DataSource = this.bsCZMSTSIRFID;
            this.dataGridZapsat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridZapsat.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dataGridZapsat.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dataGridZapsat.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dataGridZapsat.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dataGridZapsat.Location = new System.Drawing.Point(0, 13);
            this.dataGridZapsat.MultiSelect = false;
            this.dataGridZapsat.Name = "dataGridZapsat";
            this.dataGridZapsat.NumberFormat = "N";
            this.dataGridZapsat.RowHeightDefault = 23;
            this.dataGridZapsat.Size = new System.Drawing.Size(207, 179);
            this.dataGridZapsat.Sort = "";
            this.dataGridZapsat.SortByHeaderDoubleClick = true;
            this.dataGridZapsat.TabIndex = 0;
            this.dataGridZapsat.TableStyles.Add(this.dataGridTableStyle1);
            this.dataGridZapsat.CurrentRowIndexChanged += new System.EventHandler(this.dataGrid2Polozky_CurrentRowIndexChanged);
            // 
            // dsVydej
            // 
            this.dsVydej.DataSetName = "Vydej";
            this.dsVydej.Locale = new System.Globalization.CultureInfo("");
            this.dsVydej.Prefix = "";
            this.dsVydej.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 13);
            this.label1.Text = "Zapsat";
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
            this.dataGridTableStyle1.MappingName = "CZMST_SI_RFID";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Pol.č.";
            this.dataGridTextBoxColumn1.MappingName = "ITEMNMBR";
            this.dataGridTextBoxColumn1.NullText = "?";
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Název";
            this.dataGridTextBoxColumn2.MappingName = "ITEMDESC";
            this.dataGridTextBoxColumn2.NullText = "?";
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Šarže";
            this.dataGridTextBoxColumn3.MappingName = "SERLNMBR";
            this.dataGridTextBoxColumn3.NullText = "?";
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "Seq.č.";
            this.dataGridTextBoxColumn4.MappingName = "SEQUENCENMBR";
            this.dataGridTextBoxColumn4.NullText = "?";
            // 
            // dataGridTableStyle2
            // 
            this.dataGridTableStyle2.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
            this.dataGridTableStyle2.GridColumnStyles.Add(this.dataGridTextBoxColumn6);
            this.dataGridTableStyle2.GridColumnStyles.Add(this.dataGridTextBoxColumn7);
            this.dataGridTableStyle2.GridColumnStyles.Add(this.dataGridTextBoxColumn8);
            this.dataGridTableStyle2.MappingName = "CZMST_SI_RFID";
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "Pol.č.";
            this.dataGridTextBoxColumn5.MappingName = "ITEMNMBR";
            this.dataGridTextBoxColumn5.NullText = "?";
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "Název";
            this.dataGridTextBoxColumn6.MappingName = "ITEMDESC";
            this.dataGridTextBoxColumn6.NullText = "?";
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "Šarže";
            this.dataGridTextBoxColumn7.MappingName = "SERLNMBR";
            this.dataGridTextBoxColumn7.NullText = "?";
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            this.dataGridTextBoxColumn8.HeaderText = "Seq.č.";
            this.dataGridTextBoxColumn8.MappingName = "SEQUENCENMBR";
            this.dataGridTextBoxColumn8.NullText = "?";
            // 
            // ZapisRFID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(749, 417);
            this.ControlBox = false;
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.statusBar1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ZapisRFID";
            this.Text = "Zápis RFID";
            this.Deactivate += new System.EventHandler(this.SnimatRFID_Deactivate);
            this.Load += new System.EventHandler(this.SnimatRFID_Load);
            this.Activated += new System.EventHandler(this.SnimatRFID_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SnimatRFID_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SnimatRFID_KeyDown);
            this.panelData.ResumeLayout(false);
            this.panelListZapsano.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsCZMSTSIRFIDZapsano)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridZapsano)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVydejZapsano)).EndInit();
            this.panelListZapsat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsCZMSTSIRFID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridZapsat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVydej)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.DataGrid2 dataGridZapsat;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemKonec;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.MenuItem menuItemZobrazeniRezim;
        private System.Windows.Forms.Panel panelListZapsat;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem miRFIDZapsatInformace;
        private Fask.SQLiteDBs.DataSets.Vydej dsVydej;
        private System.Windows.Forms.BindingSource bsCZMSTSIRFID;
        private System.Windows.Forms.Panel panelListZapsano;
        private Fask.Graphic.DataGrid2 dataGridZapsano;
        private System.Windows.Forms.BindingSource bsCZMSTSIRFIDZapsano;
        private Fask.SQLiteDBs.DataSets.Vydej dsVydejZapsano;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem miKonecAUloz;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle2;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn5;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn6;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn7;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn8;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn2;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn3;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn4;
    }
}