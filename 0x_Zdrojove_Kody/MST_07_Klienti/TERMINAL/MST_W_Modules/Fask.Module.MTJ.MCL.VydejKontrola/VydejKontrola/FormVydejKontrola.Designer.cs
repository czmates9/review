namespace Fask.Module.MTJ.MCL.VydejKontrola.VydejKontrola
{
    partial class FormVydejKontrola
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
            this.miAkce = new System.Windows.Forms.MenuItem();
            this.menuItemFaktura = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miKonec = new System.Windows.Forms.MenuItem();
            this.menuItemData = new System.Windows.Forms.MenuItem();
            this.menuItemDataOdeslat = new System.Windows.Forms.MenuItem();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.vydejKontrolaDSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vydejKontrolaDS = new Fask.Module.MTJ.MCL.VydejKontrola.DataSets.VydejKontrolaDS();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumnFaktura = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumnZboziFaktura = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumnZboziVydej = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumnUserID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumnTerminalID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumnGUID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumnZboziFakturaDatetime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumnZboziVydejDatetime = new System.Windows.Forms.DataGridTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.vydejKontrolaDSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vydejKontrolaDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miAkce);
            this.mainMenu1.MenuItems.Add(this.menuItemData);
            // 
            // miAkce
            // 
            this.miAkce.MenuItems.Add(this.menuItemFaktura);
            this.miAkce.MenuItems.Add(this.menuItem1);
            this.miAkce.MenuItems.Add(this.miKonec);
            this.miAkce.Text = "Akce";
            // 
            // menuItemFaktura
            // 
            this.menuItemFaktura.Text = "Doklad (F1)";
            this.menuItemFaktura.Click += new System.EventHandler(this.menuItemFaktura_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "-";
            // 
            // miKonec
            // 
            this.miKonec.Text = "Konec (Esc)";
            this.miKonec.Click += new System.EventHandler(this.miKonec_Click);
            // 
            // menuItemData
            // 
            this.menuItemData.MenuItems.Add(this.menuItemDataOdeslat);
            this.menuItemData.Text = "Data";
            // 
            // menuItemDataOdeslat
            // 
            this.menuItemDataOdeslat.Text = "Odeslat";
            this.menuItemDataOdeslat.Click += new System.EventHandler(this.menuItemDataOdeslat_Click);
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.statusBar1.Location = new System.Drawing.Point(0, 284);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(445, 20);
            // 
            // vydejKontrolaDSBindingSource
            // 
            this.vydejKontrolaDSBindingSource.DataMember = "fask_Vydej_Kontrola";
            this.vydejKontrolaDSBindingSource.DataSource = this.vydejKontrolaDS;
            this.vydejKontrolaDSBindingSource.Sort = "";
            // 
            // vydejKontrolaDS
            // 
            this.vydejKontrolaDS.DataSetName = "VydejKontrolaDS";
            this.vydejKontrolaDS.Prefix = "";
            this.vydejKontrolaDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.DataSource = this.vydejKontrolaDSBindingSource;
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
            this.dataGrid1.Size = new System.Drawing.Size(445, 284);
            this.dataGrid1.Sort = "";
            this.dataGrid1.SortByHeaderDoubleClick = true;
            this.dataGrid1.TabIndex = 1;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnFaktura);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnZboziFaktura);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnZboziVydej);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnUserID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnTerminalID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnGUID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnZboziFakturaDatetime);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnZboziVydejDatetime);
            this.dataGridTableStyle1.MappingName = "fask_Vydej_Kontrola";
            // 
            // dataGridTextBoxColumnFaktura
            // 
            this.dataGridTextBoxColumnFaktura.Format = "";
            this.dataGridTextBoxColumnFaktura.FormatInfo = null;
            this.dataGridTextBoxColumnFaktura.HeaderText = "Doklad";
            this.dataGridTextBoxColumnFaktura.MappingName = "Faktura";
            // 
            // dataGridTextBoxColumnZboziFaktura
            // 
            this.dataGridTextBoxColumnZboziFaktura.Format = "";
            this.dataGridTextBoxColumnZboziFaktura.FormatInfo = null;
            this.dataGridTextBoxColumnZboziFaktura.HeaderText = "D. položka";
            this.dataGridTextBoxColumnZboziFaktura.MappingName = "BarcodeZboziFaktura";
            // 
            // dataGridTextBoxColumnZboziVydej
            // 
            this.dataGridTextBoxColumnZboziVydej.Format = "";
            this.dataGridTextBoxColumnZboziVydej.FormatInfo = null;
            this.dataGridTextBoxColumnZboziVydej.HeaderText = "Položka";
            this.dataGridTextBoxColumnZboziVydej.MappingName = "BarcodeZboziVydej";
            // 
            // dataGridTextBoxColumnUserID
            // 
            this.dataGridTextBoxColumnUserID.Format = "";
            this.dataGridTextBoxColumnUserID.FormatInfo = null;
            this.dataGridTextBoxColumnUserID.HeaderText = "Uživatel";
            this.dataGridTextBoxColumnUserID.MappingName = "UserID";
            // 
            // dataGridTextBoxColumnTerminalID
            // 
            this.dataGridTextBoxColumnTerminalID.Format = "";
            this.dataGridTextBoxColumnTerminalID.FormatInfo = null;
            this.dataGridTextBoxColumnTerminalID.HeaderText = "Terminál";
            this.dataGridTextBoxColumnTerminalID.MappingName = "TerminalID";
            // 
            // dataGridTextBoxColumnGUID
            // 
            this.dataGridTextBoxColumnGUID.Format = "";
            this.dataGridTextBoxColumnGUID.FormatInfo = null;
            this.dataGridTextBoxColumnGUID.HeaderText = "Identifikátor";
            this.dataGridTextBoxColumnGUID.MappingName = "GUID";
            // 
            // dataGridTextBoxColumnZboziFakturaDatetime
            // 
            this.dataGridTextBoxColumnZboziFakturaDatetime.Format = "";
            this.dataGridTextBoxColumnZboziFakturaDatetime.FormatInfo = null;
            this.dataGridTextBoxColumnZboziFakturaDatetime.HeaderText = "D. čas";
            this.dataGridTextBoxColumnZboziFakturaDatetime.MappingName = "BarcodeZboziFakturaDatetime";
            // 
            // dataGridTextBoxColumnZboziVydejDatetime
            // 
            this.dataGridTextBoxColumnZboziVydejDatetime.Format = "";
            this.dataGridTextBoxColumnZboziVydejDatetime.FormatInfo = null;
            this.dataGridTextBoxColumnZboziVydejDatetime.HeaderText = "P. čas";
            this.dataGridTextBoxColumnZboziVydejDatetime.MappingName = "BarcodeZboziVydejDatetime";
            // 
            // FormVydejKontrola
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(445, 304);
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.statusBar1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "FormVydejKontrola";
            this.Text = "Výdej Kontrola";
            this.Load += new System.EventHandler(this.FormVydejKontrola_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormVydejKontrola_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.vydejKontrolaDSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vydejKontrolaDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miAkce;
        private System.Windows.Forms.MenuItem miKonec;
        private System.Windows.Forms.StatusBar statusBar1;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Module.MTJ.MCL.VydejKontrola.DataSets.VydejKontrolaDS vydejKontrolaDS;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemFaktura;
        private System.Windows.Forms.BindingSource vydejKontrolaDSBindingSource;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnFaktura;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnZboziFaktura;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnZboziVydej;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnUserID;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnTerminalID;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnGUID;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnZboziFakturaDatetime;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnZboziVydejDatetime;
        private System.Windows.Forms.MenuItem menuItemData;
        private System.Windows.Forms.MenuItem menuItemDataOdeslat;
    }
}