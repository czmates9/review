namespace Fask.MST_W.Prodej_3
{
    partial class ProdejSumar
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProdejSumar));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.sbPal = new System.Windows.Forms.StatusBar();
			this.cZMSTSOUHRNBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.prodej = new Fask.SQLiteDBs.DataSets.Prodej();
			this.dataGrid1 = new Fask.Graphic.DataGrid2();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumn1 = new Fask.Graphic.DataGrid2TextBoxColumn();
			this.dataGridTextBoxColumn2 = new Fask.Graphic.DataGrid2TextBoxColumn();
			this.dataGridTextBoxColumn3 = new Fask.Graphic.DataGrid2NumberBoxColumn();
			this.dataGridTextBoxColumn4 = new Fask.Graphic.DataGrid2NumberBoxColumn();
			this.dataGridTextBoxColumn5 = new Fask.Graphic.DataGrid2NumberBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.cZMSTSOUHRNBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.prodej)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem2);
			resources.ApplyResources(this.menuItem1, "menuItem1");
			// 
			// menuItem2
			// 
			resources.ApplyResources(this.menuItem2, "menuItem2");
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// sbPal
			// 
			resources.ApplyResources(this.sbPal, "sbPal");
			this.sbPal.Name = "sbPal";
			// 
			// cZMSTSOUHRNBindingSource
			// 
			this.cZMSTSOUHRNBindingSource.DataMember = "CZMST_SOUHRN";
			this.cZMSTSOUHRNBindingSource.DataSource = this.prodej;
			this.cZMSTSOUHRNBindingSource.Sort = "";
			// 
			// prodej
			// 
			this.prodej.DataSetName = "Prodej";
			this.prodej.Locale = new System.Globalization.CultureInfo("");
			this.prodej.Prefix = "";
			this.prodej.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// dataGrid1
			// 
			this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
			this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.dataGrid1.CurrentRow = null;
			this.dataGrid1.DataSource = this.cZMSTSOUHRNBindingSource;
			resources.ApplyResources(this.dataGrid1, "dataGrid1");
			this.dataGrid1.HeaderBackColorSelected = System.Drawing.Color.GreenYellow;
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
			this.dataGridTableStyle1.MappingName = "CZMST_SOUHRN";
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
			// ProdejSumar
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			resources.ApplyResources(this, "$this");
			this.ControlBox = false;
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.sbPal);
			this.KeyPreview = true;
			this.Menu = this.mainMenu1;
			this.Name = "ProdejSumar";
			this.Load += new System.EventHandler(this.ProdejSumar_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProdejSumar_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.cZMSTSOUHRNBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.prodej)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.StatusBar sbPal;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private Fask.SQLiteDBs.DataSets.Prodej prodej;
		private System.Windows.Forms.BindingSource cZMSTSOUHRNBindingSource;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn2;
        private Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumn3;
        private Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumn4;
        private Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumn5;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}