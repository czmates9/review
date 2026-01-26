using Fask.Graphic;
namespace Fask.MST_W.Forms
{
    partial class FormLokaceTypVyber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLokaceTypVyber));
            this.panelButtons = new System.Windows.Forms.Panel();
            this.bOK = new Fask.Graphic.GraphicButton();
            this.bStorno = new Fask.Graphic.GraphicButton();
            this.panelData = new System.Windows.Forms.Panel();
            this.bsLokace = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.dsLokace = new Fask.SQLiteDBs.DataSets.Lokace();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn2 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn3 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn4 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn5 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miAktualizovat = new System.Windows.Forms.MenuItem();
            this.panelButtons.SuspendLayout();
            this.panelData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsLokace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsLokace)).BeginInit();
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
            // bsLokace
            // 
            this.bsLokace.DataMember = "CZMST_SkladLokace_LokaceTypy";
            this.bsLokace.DataSource = this.dsLokace;
            this.bsLokace.Sort = "";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.DataSource = this.bsLokace;
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
            // dsLokace
            // 
            this.dsLokace.DataSetName = "Lokace";
            this.dsLokace.Locale = new System.Globalization.CultureInfo("");
            this.dsLokace.Prefix = "";
            this.dsLokace.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
            this.dataGridTableStyle1.MappingName = "CZMST_SkladLokace_LokaceTypy";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn1, "dataGridTextBoxColumn1");
            this.dataGridTextBoxColumn1.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn1.SelectionShow = false;
            this.dataGridTextBoxColumn1.Tag = "";
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn2, "dataGridTextBoxColumn2");
            this.dataGridTextBoxColumn2.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn2.SelectionShow = false;
            this.dataGridTextBoxColumn2.Tag = "";
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn3, "dataGridTextBoxColumn3");
            this.dataGridTextBoxColumn3.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn3.SelectionShow = false;
            this.dataGridTextBoxColumn3.Tag = "";
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn4, "dataGridTextBoxColumn4");
            this.dataGridTextBoxColumn4.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn4.SelectionShow = false;
            this.dataGridTextBoxColumn4.Tag = "";
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumn5, "dataGridTextBoxColumn5");
            this.dataGridTextBoxColumn5.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn5.SelectionShow = false;
            this.dataGridTextBoxColumn5.Tag = "";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.miAktualizovat);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // miAktualizovat
            // 
            resources.ApplyResources(this.miAktualizovat, "miAktualizovat");
            this.miAktualizovat.Click += new System.EventHandler(this.miAktualizovat_Click);
            // 
            // FormLokaceTypVyber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "FormLokaceTypVyber";
            this.Load += new System.EventHandler(this.FormLokaceTypVyber_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormLokaceTypVyber_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsLokace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsLokace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GraphicButton bOK;
        public GraphicButton bStorno;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Panel panelData;
        private DataGrid2 dataGrid1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miAktualizovat;
        private System.Windows.Forms.BindingSource bsLokace;
        private Fask.SQLiteDBs.DataSets.Lokace dsLokace;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn2;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn3;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn4;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn5;
    }
}