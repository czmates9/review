namespace Fask.Vyroba_W.Odvadeni
{
    partial class FormPrikazVyber
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
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.cZPROVPHBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
			this.vyrobaCEDataSet = new Fask.SQLiteDBs.DataSets.Vyroba();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItemAkce = new System.Windows.Forms.MenuItem();
            this.menuItemAkceStorno = new System.Windows.Forms.MenuItem();
            this.menuItemAkceOK = new System.Windows.Forms.MenuItem();
			//this.cZPRO_VPHTableAdapter = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter();
            this.panelButtons.SuspendLayout();
            this.panelComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cZPROVPHBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaCEDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 254);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(238, 41);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(118, 41);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 41);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.dataGrid1);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(238, 254);
            // 
            // cZPROVPHBindingSource
            // 
            this.cZPROVPHBindingSource.AllowNew = false;
            this.cZPROVPHBindingSource.DataMember = "CZPRO_VPH";
            this.cZPROVPHBindingSource.DataSource = this.vyrobaCEDataSet;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.DataSource = this.cZPROVPHBindingSource;
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
            this.dataGrid1.Size = new System.Drawing.Size(238, 254);
            this.dataGrid1.Sort = "";
            this.dataGrid1.SortByHeaderDoubleClick = true;
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // vyrobaCEDataSet
            // 
            this.vyrobaCEDataSet.DataSetName = "VyrobaCEDataSet";
            this.vyrobaCEDataSet.EnforceConstraints = false;
            this.vyrobaCEDataSet.Locale = new System.Globalization.CultureInfo("");
            this.vyrobaCEDataSet.Prefix = "";
            this.vyrobaCEDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.ExcludeSchema;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
            this.dataGridTableStyle1.MappingName = "CZPRO_VPH";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Dávka";
            this.dataGridTextBoxColumn1.MappingName = "CountEntries";
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Pøíkaz";
            this.dataGridTextBoxColumn2.MappingName = "SOPNUMBE";
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Popis";
            this.dataGridTextBoxColumn3.MappingName = "SOPDESC";
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "Èár. kód";
            this.dataGridTextBoxColumn4.MappingName = "BarcodeH";
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "Datum";
            this.dataGridTextBoxColumn5.MappingName = "DateProd";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemAkce);
            // 
            // menuItemAkce
            // 
            this.menuItemAkce.MenuItems.Add(this.menuItemAkceStorno);
            this.menuItemAkce.MenuItems.Add(this.menuItemAkceOK);
            this.menuItemAkce.Text = "Akce";
            // 
            // menuItemAkceStorno
            // 
            this.menuItemAkceStorno.Text = "Storno";
            this.menuItemAkceStorno.Click += new System.EventHandler(this.menuItemAkceStorno_Click);
            // 
            // menuItemAkceOK
            // 
            this.menuItemAkceOK.Text = "OK";
            this.menuItemAkceOK.Click += new System.EventHandler(this.menuItemAkceOK_Click);
			//// 
			//// cZPRO_VPHTableAdapter
			//// 
			//this.cZPRO_VPHTableAdapter.ClearBeforeFill = true;
            // 
            // FormPrikazVyber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FormPrikazVyber";
            this.Text = "Pøíkaz";
            this.Load += new System.EventHandler(this.FormPrikazVyber_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormPrikazVyber_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelComponents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cZPROVPHBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaCEDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        public System.Windows.Forms.Panel panelComponents;
        public System.Windows.Forms.Button buttonOK;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItemAkce;
        private System.Windows.Forms.MenuItem menuItemAkceStorno;
        private System.Windows.Forms.MenuItem menuItemAkceOK;
		private Fask.SQLiteDBs.DataSets.Vyroba vyrobaCEDataSet;
        private System.Windows.Forms.BindingSource cZPROVPHBindingSource;
		//private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter cZPRO_VPHTableAdapter;

    }
}
