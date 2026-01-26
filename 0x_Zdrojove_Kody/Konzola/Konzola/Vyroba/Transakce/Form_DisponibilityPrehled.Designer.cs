namespace Konzola.Vyroba.Transakce
{
    partial class Form_DisponibilityPrehled
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
            this.button1 = new System.Windows.Forms.Button();
            this.button_Konec = new System.Windows.Forms.Button();
            this.dg_validateData = new Zuby.ADGV.AdvancedDataGridView();
            this.bs_validateData = new System.Windows.Forms.BindingSource(this.components);
            this.ds_validateData = new Fask.POHODA.Disponibility.ValidateData();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.ITEMNMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKz_IDS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKz_EAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPNUMBE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STAV_SKLAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYSHPPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_validateData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_validateData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_validateData)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button_Konec);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(865, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(124, 434);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 60);
            this.button1.TabIndex = 1;
            this.button1.Text = "Zpracuj tak jak je!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_Konec
            // 
            this.button_Konec.Location = new System.Drawing.Point(9, 362);
            this.button_Konec.Name = "button_Konec";
            this.button_Konec.Size = new System.Drawing.Size(106, 60);
            this.button_Konec.TabIndex = 0;
            this.button_Konec.Text = "Konec";
            this.button_Konec.UseVisualStyleBackColor = true;
            this.button_Konec.Click += new System.EventHandler(this.button_Konec_Click);
            // 
            // dg_validateData
            // 
            this.dg_validateData.AllowUserToAddRows = false;
            this.dg_validateData.AllowUserToDeleteRows = false;
            this.dg_validateData.AllowUserToOrderColumns = true;
            this.dg_validateData.AllowUserToResizeRows = false;
            this.dg_validateData.AutoGenerateColumns = false;
            this.dg_validateData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_validateData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ITEMNMBR,
            this.SKz_IDS,
            this.ITEMDESC,
            this.SKz_EAN,
            this.SKL_ID,
            this.SOPNUMBE,
            this.STAV_SKLAD,
            this.QTYSHPPD});
            this.dg_validateData.DataSource = this.bs_validateData;
            this.dg_validateData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_validateData.FilterAndSortEnabled = true;
            this.dg_validateData.Location = new System.Drawing.Point(0, 51);
            this.dg_validateData.MultiSelect = false;
            this.dg_validateData.Name = "dg_validateData";
            this.dg_validateData.ReadOnly = true;
            this.dg_validateData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_validateData.Size = new System.Drawing.Size(865, 407);
            this.dg_validateData.TabIndex = 1;
            this.dg_validateData.TabStop = false;
            this.dg_validateData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_validateData_CellFormatting);
            this.dg_validateData.SelectionChanged += new System.EventHandler(this.dg_validateData_SelectionChanged);
            // 
            // bs_validateData
            // 
            this.bs_validateData.DataMember = "VydejKontrola";
            this.bs_validateData.DataSource = this.ds_validateData;
            // 
            // ds_validateData
            // 
            this.ds_validateData.DataSetName = "ValidateData";
            this.ds_validateData.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(989, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konecToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.konecToolStripMenuItem.Text = "Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.button_Konec_Click);
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 24);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(865, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 3;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // ITEMNMBR
            // 
            this.ITEMNMBR.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR.HeaderText = "ID materiálu";
            this.ITEMNMBR.Name = "ITEMNMBR";
            this.ITEMNMBR.ReadOnly = true;
            // 
            // SKz_IDS
            // 
            this.SKz_IDS.DataPropertyName = "SKz_IDS";
            this.SKz_IDS.HeaderText = "Kód materálu";
            this.SKz_IDS.Name = "SKz_IDS";
            this.SKz_IDS.ReadOnly = true;
            // 
            // ITEMDESC
            // 
            this.ITEMDESC.DataPropertyName = "ITEMDESC";
            this.ITEMDESC.HeaderText = "Název materiálu";
            this.ITEMDESC.Name = "ITEMDESC";
            this.ITEMDESC.ReadOnly = true;
            // 
            // SKz_EAN
            // 
            this.SKz_EAN.DataPropertyName = "SKz_EAN";
            this.SKz_EAN.HeaderText = "Čar. kód materálu";
            this.SKz_EAN.Name = "SKz_EAN";
            this.SKz_EAN.ReadOnly = true;
            // 
            // SKL_ID
            // 
            this.SKL_ID.DataPropertyName = "SKL_ID";
            this.SKL_ID.HeaderText = "ID Skladu";
            this.SKL_ID.Name = "SKL_ID";
            this.SKL_ID.ReadOnly = true;
            // 
            // SOPNUMBE
            // 
            this.SOPNUMBE.DataPropertyName = "SOPNUMBE";
            this.SOPNUMBE.HeaderText = "Číslo dokladu";
            this.SOPNUMBE.Name = "SOPNUMBE";
            this.SOPNUMBE.ReadOnly = true;
            // 
            // STAV_SKLAD
            // 
            this.STAV_SKLAD.DataPropertyName = "STAV_SKLAD";
            this.STAV_SKLAD.HeaderText = "Stav skladu";
            this.STAV_SKLAD.Name = "STAV_SKLAD";
            this.STAV_SKLAD.ReadOnly = true;
            // 
            // QTYSHPPD
            // 
            this.QTYSHPPD.DataPropertyName = "QTYSHPPD";
            this.QTYSHPPD.HeaderText = "Požadavek";
            this.QTYSHPPD.Name = "QTYSHPPD";
            this.QTYSHPPD.ReadOnly = true;
            // 
            // Form_DisponibilityPrehled
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 458);
            this.Controls.Add(this.dg_validateData);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_DisponibilityPrehled";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Přehled disponibility";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_DisponibilityPrehled_FormClosing);
            this.Load += new System.EventHandler(this.Form_DisponibilityPrehled_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_validateData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_validateData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_validateData)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource bs_validateData;
        private Zuby.ADGV.AdvancedDataGridView dg_validateData;
        private Fask.POHODA.Disponibility.ValidateData ds_validateData;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button button_Konec;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKz_IDS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKz_EAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPNUMBE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STAV_SKLAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYSHPPD;
    }
}