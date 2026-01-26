using Fask.Graphic;
namespace Fask.MST_W.Forms
{
    partial class FormTiskarnyVyber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTiskarnyVyber));
            this.panelButtons = new System.Windows.Forms.Panel();
            this.bOK = new Fask.Graphic.GraphicButton();
            this.bStorno = new Fask.Graphic.GraphicButton();
            this.panelData = new System.Windows.Forms.Panel();
            this.cZMSTTiskarnaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.tiskarny = new Fask.SQLiteDBs.DataSets.Tiskarny();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumnID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnNAME = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnLocation = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnBarcode = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnDefault = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemActualize = new System.Windows.Forms.MenuItem();
            this.menuItemDefault = new System.Windows.Forms.MenuItem();
            this.menuItemFindBarcode = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemOK = new System.Windows.Forms.MenuItem();
            this.menuItemStorno = new System.Windows.Forms.MenuItem();
            this.panelButtons.SuspendLayout();
            this.panelData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cZMSTTiskarnaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tiskarny)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.bOK);
            this.panelButtons.Controls.Add(this.bStorno);
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.Name = "panelButtons";
            // 
            // bOK
            // 
            this.bOK.BitmapNormal = null;
            resources.ApplyResources(this.bOK, "bOK");
            this.bOK.FocusMargin = 5;
            this.bOK.Name = "bOK";
            this.bOK.Pressed = false;
            this.bOK.Transparent = System.Drawing.Color.White;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bStorno
            // 
            this.bStorno.BitmapNormal = null;
            resources.ApplyResources(this.bStorno, "bStorno");
            this.bStorno.FocusMargin = 5;
            this.bStorno.Name = "bStorno";
            this.bStorno.Pressed = false;
            this.bStorno.Transparent = System.Drawing.Color.White;
            this.bStorno.Click += new System.EventHandler(this.bStorno_Click);
            // 
            // panelData
            // 
            this.panelData.Controls.Add(this.dataGrid1);
            resources.ApplyResources(this.panelData, "panelData");
            this.panelData.Name = "panelData";
            // 
            // cZMSTTiskarnaBindingSource
            // 
            this.cZMSTTiskarnaBindingSource.DataMember = "CZMST_TISKARNA";
            this.cZMSTTiskarnaBindingSource.DataSource = this.tiskarny;
            this.cZMSTTiskarnaBindingSource.Sort = "";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.DataSource = this.cZMSTTiskarnaBindingSource;
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
            // tiskarny
            // 
            this.tiskarny.DataSetName = "Sklady";
            this.tiskarny.Locale = new System.Globalization.CultureInfo("");
            this.tiskarny.Prefix = "";
            this.tiskarny.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnNAME);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnLocation);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnBarcode);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnDefault);
            this.dataGridTableStyle1.MappingName = "CZMST_TISKARNA";
            // 
            // dataGridTextBoxColumnID
            // 
            this.dataGridTextBoxColumnID.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnID.Format = "";
            this.dataGridTextBoxColumnID.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnID, "dataGridTextBoxColumnID");
            this.dataGridTextBoxColumnID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnID.SelectionShow = false;
            this.dataGridTextBoxColumnID.Tag = "";
            // 
            // dataGridTextBoxColumnNAME
            // 
            this.dataGridTextBoxColumnNAME.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnNAME.Format = "";
            this.dataGridTextBoxColumnNAME.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnNAME, "dataGridTextBoxColumnNAME");
            this.dataGridTextBoxColumnNAME.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnNAME.SelectionShow = false;
            this.dataGridTextBoxColumnNAME.Tag = "";
            // 
            // dataGridTextBoxColumnLocation
            // 
            this.dataGridTextBoxColumnLocation.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnLocation.Format = "";
            this.dataGridTextBoxColumnLocation.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnLocation, "dataGridTextBoxColumnLocation");
            this.dataGridTextBoxColumnLocation.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnLocation.SelectionShow = false;
            this.dataGridTextBoxColumnLocation.Tag = "";
            // 
            // dataGridTextBoxColumnBarcode
            // 
            this.dataGridTextBoxColumnBarcode.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnBarcode.Format = "";
            this.dataGridTextBoxColumnBarcode.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnBarcode, "dataGridTextBoxColumnBarcode");
            this.dataGridTextBoxColumnBarcode.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnBarcode.SelectionShow = false;
            this.dataGridTextBoxColumnBarcode.Tag = "";
            // 
            // dataGridTextBoxColumnDefault
            // 
            this.dataGridTextBoxColumnDefault.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnDefault.Format = "";
            this.dataGridTextBoxColumnDefault.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnDefault, "dataGridTextBoxColumnDefault");
            this.dataGridTextBoxColumnDefault.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnDefault.SelectionShow = false;
            this.dataGridTextBoxColumnDefault.Tag = "";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemActualize);
            this.menuItem1.MenuItems.Add(this.menuItemDefault);
            this.menuItem1.MenuItems.Add(this.menuItemFindBarcode);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.menuItemOK);
            this.menuItem1.MenuItems.Add(this.menuItemStorno);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemActualize
            // 
            resources.ApplyResources(this.menuItemActualize, "menuItemActualize");
            this.menuItemActualize.Click += new System.EventHandler(this.menuItemActualize_Click);
            // 
            // menuItemDefault
            // 
            resources.ApplyResources(this.menuItemDefault, "menuItemDefault");
            this.menuItemDefault.Click += new System.EventHandler(this.menuItemDefault_Click);
            // 
            // menuItemFindBarcode
            // 
            resources.ApplyResources(this.menuItemFindBarcode, "menuItemFindBarcode");
            this.menuItemFindBarcode.Click += new System.EventHandler(this.menuItemFindBarcode_Click);
            // 
            // menuItem4
            // 
            resources.ApplyResources(this.menuItem4, "menuItem4");
            // 
            // menuItemOK
            // 
            resources.ApplyResources(this.menuItemOK, "menuItemOK");
            this.menuItemOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // menuItemStorno
            // 
            resources.ApplyResources(this.menuItemStorno, "menuItemStorno");
            this.menuItemStorno.Click += new System.EventHandler(this.bStorno_Click);
            // 
            // FormTiskarnyVyber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "FormTiskarnyVyber";
            this.Deactivate += new System.EventHandler(this.FormTiskarnyVyber_Deactivate);
            this.Load += new System.EventHandler(this.FormTiskarnaVyber_Load);
            this.Activated += new System.EventHandler(this.FormTiskarnyVyber_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormTiskarnaVyber_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cZMSTTiskarnaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tiskarny)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GraphicButton bOK;
        public GraphicButton bStorno;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Panel panelData;
        private DataGrid2 dataGrid1;
        private Fask.SQLiteDBs.DataSets.Tiskarny tiskarny;
        private System.Windows.Forms.BindingSource cZMSTTiskarnaBindingSource;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnID;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnNAME;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnLocation;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnBarcode;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnDefault;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemActualize;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemOK;
        private System.Windows.Forms.MenuItem menuItemStorno;
        private System.Windows.Forms.MenuItem menuItemDefault;
        private System.Windows.Forms.MenuItem menuItemFindBarcode;
    }
}