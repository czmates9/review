namespace Konzola.Vyroba
{
    partial class FormVyrobniPrikaz_VPH_Seznam
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybrat = new System.Windows.Forms.Button();
            this.dg_VPH = new Zuby.ADGV.AdvancedDataGridView();
            this.bs_VPH = new System.Windows.Forms.BindingSource(this.components);
            this.ds_VPH = new Fask.Interfaces.DataSets.Vyroba();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.bw_VPH = new System.ComponentModel.BackgroundWorker();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.Active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountEntries = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPNUMBE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VNDDOCNMH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarcodeH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCNCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateProd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rez1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rez2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TermID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LSTMod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEX_ROW_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_VPH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_VPH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_VPH)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonKonec);
            this.panel1.Controls.Add(this.buttonVybrat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1037, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(111, 481);
            this.panel1.TabIndex = 1;
            // 
            // buttonKonec
            // 
            this.buttonKonec.Location = new System.Drawing.Point(6, 406);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(93, 63);
            this.buttonKonec.TabIndex = 1;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Click += new System.EventHandler(this.tsmiKonec_Click);
            // 
            // buttonVybrat
            // 
            this.buttonVybrat.Location = new System.Drawing.Point(6, 12);
            this.buttonVybrat.Name = "buttonVybrat";
            this.buttonVybrat.Size = new System.Drawing.Size(93, 63);
            this.buttonVybrat.TabIndex = 0;
            this.buttonVybrat.Text = "Vybrat";
            this.buttonVybrat.UseVisualStyleBackColor = true;
            this.buttonVybrat.Click += new System.EventHandler(this.buttonVybrat_Click);
            // 
            // dg_VPH
            // 
            this.dg_VPH.AllowUserToAddRows = false;
            this.dg_VPH.AllowUserToDeleteRows = false;
            this.dg_VPH.AllowUserToResizeRows = false;
            this.dg_VPH.AutoGenerateColumns = false;
            this.dg_VPH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_VPH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Active,
            this.CountEntries,
            this.SOPNUMBE,
            this.SOPTYPE,
            this.SOPDESC,
            this.VNDDOCNMH,
            this.BarcodeH,
            this.LOCNCODE,
            this.DateProd,
            this.Rez1,
            this.Rez2,
            this.TermID,
            this.LSTMod,
            this.DEX_ROW_ID});
            this.dg_VPH.DataSource = this.bs_VPH;
            this.dg_VPH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_VPH.FilterAndSortEnabled = true;
            this.dg_VPH.Location = new System.Drawing.Point(0, 177);
            this.dg_VPH.MultiSelect = false;
            this.dg_VPH.Name = "dg_VPH";
            this.dg_VPH.ReadOnly = true;
            this.dg_VPH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_VPH.Size = new System.Drawing.Size(1037, 304);
            this.dg_VPH.TabIndex = 0;
            this.dg_VPH.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_VPH_CellDoubleClick);
            // 
            // bs_VPH
            // 
            this.bs_VPH.DataMember = "CZPRO_VPH";
            this.bs_VPH.DataSource = this.ds_VPH;
            // 
            // ds_VPH
            // 
            this.ds_VPH.DataSetName = "Vyroba";
            this.ds_VPH.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 150);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(1037, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 2;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1037, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiMenu
            // 
            this.tsmiMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiKonec});
            this.tsmiMenu.Name = "tsmiMenu";
            this.tsmiMenu.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenu.Text = "Menu";
            // 
            // tsmiKonec
            // 
            this.tsmiKonec.Name = "tsmiKonec";
            this.tsmiKonec.Size = new System.Drawing.Size(107, 22);
            this.tsmiKonec.Text = "Konec";
            this.tsmiKonec.Click += new System.EventHandler(this.tsmiKonec_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonVyhledat);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1037, 126);
            this.panel2.TabIndex = 4;
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Location = new System.Drawing.Point(912, 20);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(119, 79);
            this.buttonVyhledat.TabIndex = 0;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // bw_VPH
            // 
            this.bw_VPH.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_VPH_DoWork);
            this.bw_VPH.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_VPH_RunWorkerCompleted);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(638, 285);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 39;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // Active
            // 
            this.Active.DataPropertyName = "Active";
            this.Active.HeaderText = "Aktivní";
            this.Active.Name = "Active";
            this.Active.ReadOnly = true;
            // 
            // CountEntries
            // 
            this.CountEntries.DataPropertyName = "CountEntries";
            this.CountEntries.HeaderText = "Pol. číslo";
            this.CountEntries.Name = "CountEntries";
            this.CountEntries.ReadOnly = true;
            // 
            // SOPNUMBE
            // 
            this.SOPNUMBE.DataPropertyName = "SOPNUMBE";
            this.SOPNUMBE.HeaderText = "Výrobní Zakázka";
            this.SOPNUMBE.Name = "SOPNUMBE";
            this.SOPNUMBE.ReadOnly = true;
            // 
            // SOPTYPE
            // 
            this.SOPTYPE.DataPropertyName = "SOPTYPE";
            this.SOPTYPE.HeaderText = "Typ zakázky";
            this.SOPTYPE.Name = "SOPTYPE";
            this.SOPTYPE.ReadOnly = true;
            // 
            // SOPDESC
            // 
            this.SOPDESC.DataPropertyName = "SOPDESC";
            this.SOPDESC.HeaderText = "Popis zakázky";
            this.SOPDESC.Name = "SOPDESC";
            this.SOPDESC.ReadOnly = true;
            // 
            // VNDDOCNMH
            // 
            this.VNDDOCNMH.DataPropertyName = "VNDDOCNMH";
            this.VNDDOCNMH.HeaderText = "Objednatel číslo";
            this.VNDDOCNMH.Name = "VNDDOCNMH";
            this.VNDDOCNMH.ReadOnly = true;
            // 
            // BarcodeH
            // 
            this.BarcodeH.DataPropertyName = "BarcodeH";
            this.BarcodeH.HeaderText = "Č. kód";
            this.BarcodeH.Name = "BarcodeH";
            this.BarcodeH.ReadOnly = true;
            // 
            // LOCNCODE
            // 
            this.LOCNCODE.DataPropertyName = "LOCNCODE";
            this.LOCNCODE.HeaderText = "Lokace";
            this.LOCNCODE.Name = "LOCNCODE";
            this.LOCNCODE.ReadOnly = true;
            // 
            // DateProd
            // 
            this.DateProd.DataPropertyName = "DateProd";
            this.DateProd.HeaderText = "Oček. datum výroby";
            this.DateProd.Name = "DateProd";
            this.DateProd.ReadOnly = true;
            // 
            // Rez1
            // 
            this.Rez1.DataPropertyName = "Rez1";
            this.Rez1.HeaderText = "Rezerva 1";
            this.Rez1.Name = "Rez1";
            this.Rez1.ReadOnly = true;
            // 
            // Rez2
            // 
            this.Rez2.DataPropertyName = "Rez2";
            this.Rez2.HeaderText = "Rezerva 2";
            this.Rez2.Name = "Rez2";
            this.Rez2.ReadOnly = true;
            // 
            // TermID
            // 
            this.TermID.DataPropertyName = "TermID";
            this.TermID.HeaderText = "ID terminál";
            this.TermID.Name = "TermID";
            this.TermID.ReadOnly = true;
            // 
            // LSTMod
            // 
            this.LSTMod.DataPropertyName = "LSTMod";
            this.LSTMod.HeaderText = "Poslední úprava";
            this.LSTMod.Name = "LSTMod";
            this.LSTMod.ReadOnly = true;
            // 
            // DEX_ROW_ID
            // 
            this.DEX_ROW_ID.DataPropertyName = "DEX_ROW_ID";
            this.DEX_ROW_ID.HeaderText = "index";
            this.DEX_ROW_ID.Name = "DEX_ROW_ID";
            this.DEX_ROW_ID.ReadOnly = true;
            // 
            // FormVyrobniPrikaz_VPH_Seznam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 481);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dg_VPH);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormVyrobniPrikaz_VPH_Seznam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Načíst";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVyrobniPrikaz_VPH_Seznam_FormClosing);
            this.Load += new System.EventHandler(this.FormVyrobniPrikaz_VPH_Seznam_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_VPH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_VPH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_VPH)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bs_VPH;
        private Zuby.ADGV.AdvancedDataGridView dg_VPH;
        private System.Windows.Forms.Panel panel1;
        private Fask.Interfaces.DataSets.Vyroba ds_VPH;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.ComponentModel.BackgroundWorker bw_VPH;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.Button buttonVybrat;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Active;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountEntries;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPNUMBE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNDDOCNMH;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarcodeH;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCNCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateProd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rez1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rez2;
        private System.Windows.Forms.DataGridViewTextBoxColumn TermID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LSTMod;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEX_ROW_ID;
    }
}