namespace Fask.MST_W.Vydej_3.RFID
{
    partial class VydejRFIDZboziKPrirazeniList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VydejRFIDZboziKPrirazeniList));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemPriradit = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miPrerusit = new System.Windows.Forms.MenuItem();
            this.bsRFID = new System.Windows.Forms.BindingSource(this.components);
            this.dsRFID = new Fask.MST_W.Vydej_3.RFID.DsRFID();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn2 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn3 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn4 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn5 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn6 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn7 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn8 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemObnovitPrehled = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.bsRFID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRFID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemPriradit);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.miPrerusit);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemPriradit
            // 
            resources.ApplyResources(this.menuItemPriradit, "menuItemPriradit");
            // 
            // menuItem3
            // 
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // miPrerusit
            // 
            resources.ApplyResources(this.miPrerusit, "miPrerusit");
            this.miPrerusit.Click += new System.EventHandler(this.miKonec_Click);
            // 
            // bsRFID
            // 
            this.bsRFID.DataMember = "Predloha";
            this.bsRFID.DataSource = this.dsRFID;
            this.bsRFID.Sort = "";
            // 
            // dsRFID
            // 
            this.dsRFID.DataSetName = "DsRFID";
            this.dsRFID.Prefix = "";
            this.dsRFID.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.DataSource = this.bsRFID;
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
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn6);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn7);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn8);
            this.dataGridTableStyle1.MappingName = "Predloha";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn1, "dataGridTextBoxColumn1");
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn2, "dataGridTextBoxColumn2");
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn3, "dataGridTextBoxColumn3");
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn4, "dataGridTextBoxColumn4");
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn5, "dataGridTextBoxColumn5");
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn6, "dataGridTextBoxColumn6");
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn7, "dataGridTextBoxColumn7");
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn8, "dataGridTextBoxColumn8");
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.menuItemObnovitPrehled);
            resources.ApplyResources(this.menuItem2, "menuItem2");
            // 
            // menuItemObnovitPrehled
            // 
            resources.ApplyResources(this.menuItemObnovitPrehled, "menuItemObnovitPrehled");
            this.menuItemObnovitPrehled.Click += new System.EventHandler(this.menuItemObnovitPrehled_Click);
            // 
            // VydejRFIDZboziKPrirazeniList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "VydejRFIDZboziKPrirazeniList";
            this.Load += new System.EventHandler(this.VydejRFIDZboziKPrirazeniList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServisDynamickaTabulka_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.bsRFID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRFID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miPrerusit;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.BindingSource bsRFID;
        private DsRFID dsRFID;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn2;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn3;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn4;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn5;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn6;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn7;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn8;
        private System.Windows.Forms.MenuItem menuItemPriradit;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItemObnovitPrehled;
    }
}