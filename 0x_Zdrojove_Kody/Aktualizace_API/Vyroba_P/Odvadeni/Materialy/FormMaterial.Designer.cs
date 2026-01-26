namespace Fask.Aktualizace_API.Odvadeni.Materialy
{
    partial class FormMaterial
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelbutton = new System.Windows.Forms.Panel();
            this.btn_vlozit = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iTEMNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productionSourcesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.VyrobaDatasetPS = new Fask.SQLiteDBs.DataSets.Vyroba();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHledat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCarKod = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSmazat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemOK = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStorno = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.předpisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_ostatni = new System.Windows.Forms.Button();
            this.panelbutton.SuspendLayout();
            this.panelComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productionSourcesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VyrobaDatasetPS)).BeginInit();
            this.MenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelbutton
            // 
            this.panelbutton.Controls.Add(this.btn_ostatni);
            this.panelbutton.Controls.Add(this.btn_vlozit);
            this.panelbutton.Controls.Add(this.buttonStorno);
            this.panelbutton.Controls.Add(this.buttonOK);
            this.panelbutton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelbutton.Location = new System.Drawing.Point(0, 307);
            this.panelbutton.Name = "panelbutton";
            this.panelbutton.Size = new System.Drawing.Size(587, 73);
            this.panelbutton.TabIndex = 0;
            // 
            // btn_vlozit
            // 
            this.btn_vlozit.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.btn_vlozit.Location = new System.Drawing.Point(200, 0);
            this.btn_vlozit.Name = "btn_vlozit";
            this.btn_vlozit.Size = new System.Drawing.Size(104, 73);
            this.btn_vlozit.TabIndex = 1;
            this.btn_vlozit.Text = "Předpis";
            this.btn_vlozit.UseVisualStyleBackColor = true;
            this.btn_vlozit.Click += new System.EventHandler(this.btn_vlozit_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(200, 73);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.UseVisualStyleBackColor = true;
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(407, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(180, 73);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.dataGridView1);
            this.panelComponents.Controls.Add(this.MenuStrip1);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(587, 307);
            this.panelComponents.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMNAMEDataGridViewTextBoxColumn,
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMCODEDataGridViewTextBoxColumn,
            this.mJDataGridViewTextBoxColumn,
            this.qTYSHPPDDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.productionSourcesBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 56);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dataGridView1.RowTemplate.Height = 38;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(587, 251);
            this.dataGridView1.TabIndex = 1;
            // 
            // iTEMNAMEDataGridViewTextBoxColumn
            // 
            this.iTEMNAMEDataGridViewTextBoxColumn.DataPropertyName = "ITEMNAME";
            this.iTEMNAMEDataGridViewTextBoxColumn.HeaderText = "Název";
            this.iTEMNAMEDataGridViewTextBoxColumn.Name = "iTEMNAMEDataGridViewTextBoxColumn";
            this.iTEMNAMEDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMNAMEDataGridViewTextBoxColumn.Width = 170;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "Pol.č.";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMNMBRDataGridViewTextBoxColumn.Width = 170;
            // 
            // iTEMCODEDataGridViewTextBoxColumn
            // 
            this.iTEMCODEDataGridViewTextBoxColumn.DataPropertyName = "ITEMCODE";
            this.iTEMCODEDataGridViewTextBoxColumn.HeaderText = "Ozn.";
            this.iTEMCODEDataGridViewTextBoxColumn.Name = "iTEMCODEDataGridViewTextBoxColumn";
            this.iTEMCODEDataGridViewTextBoxColumn.ReadOnly = true;
            this.iTEMCODEDataGridViewTextBoxColumn.Width = 170;
            // 
            // mJDataGridViewTextBoxColumn
            // 
            this.mJDataGridViewTextBoxColumn.DataPropertyName = "MJ";
            this.mJDataGridViewTextBoxColumn.HeaderText = "MJ";
            this.mJDataGridViewTextBoxColumn.Name = "mJDataGridViewTextBoxColumn";
            this.mJDataGridViewTextBoxColumn.ReadOnly = true;
            this.mJDataGridViewTextBoxColumn.Width = 170;
            // 
            // qTYSHPPDDataGridViewTextBoxColumn
            // 
            this.qTYSHPPDDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPD";
            this.qTYSHPPDDataGridViewTextBoxColumn.HeaderText = "Množství";
            this.qTYSHPPDDataGridViewTextBoxColumn.Name = "qTYSHPPDDataGridViewTextBoxColumn";
            this.qTYSHPPDDataGridViewTextBoxColumn.ReadOnly = true;
            this.qTYSHPPDDataGridViewTextBoxColumn.Width = 170;
            // 
            // productionSourcesBindingSource
            // 
            this.productionSourcesBindingSource.AllowNew = false;
            this.productionSourcesBindingSource.DataMember = "Production_Sources";
            this.productionSourcesBindingSource.DataSource = this.VyrobaDatasetPS;
            // 
            // VyrobaDatasetPS
            // 
            this.VyrobaDatasetPS.DataSetName = "VyrobaCEDataSet";
            this.VyrobaDatasetPS.EnforceConstraints = false;
            this.VyrobaDatasetPS.Locale = new System.Globalization.CultureInfo("");
            this.VyrobaDatasetPS.SchemaSerializationMode = System.Data.SchemaSerializationMode.ExcludeSchema;
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAkce});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(587, 56);
            this.MenuStrip1.TabIndex = 0;
            this.MenuStrip1.Text = "MenuStrip1";
            // 
            // toolStripMenuItemAkce
            // 
            this.toolStripMenuItemAkce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHledat,
            this.toolStripSeparator1,
            this.toolStripMenuItemSmazat,
            this.toolStripSeparator2,
            this.toolStripMenuItemOK,
            this.předpisToolStripMenuItem,
            this.toolStripMenuItemStorno});
            this.toolStripMenuItemAkce.Name = "toolStripMenuItemAkce";
            this.toolStripMenuItemAkce.Size = new System.Drawing.Size(115, 52);
            this.toolStripMenuItemAkce.Text = "Akce";
            // 
            // toolStripMenuItemHledat
            // 
            this.toolStripMenuItemHledat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCarKod});
            this.toolStripMenuItemHledat.Name = "toolStripMenuItemHledat";
            this.toolStripMenuItemHledat.Size = new System.Drawing.Size(229, 52);
            this.toolStripMenuItemHledat.Text = "Hledat";
            // 
            // toolStripMenuItemCarKod
            // 
            this.toolStripMenuItemCarKod.Name = "toolStripMenuItemCarKod";
            this.toolStripMenuItemCarKod.Size = new System.Drawing.Size(265, 52);
            this.toolStripMenuItemCarKod.Text = "Čár. Kód ";
            this.toolStripMenuItemCarKod.Click += new System.EventHandler(this.toolStripMenuItemCarKod_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(226, 6);
            // 
            // toolStripMenuItemSmazat
            // 
            this.toolStripMenuItemSmazat.Name = "toolStripMenuItemSmazat";
            this.toolStripMenuItemSmazat.Size = new System.Drawing.Size(229, 52);
            this.toolStripMenuItemSmazat.Text = "Smazat";
            this.toolStripMenuItemSmazat.Click += new System.EventHandler(this.toolStripMenuItemSmazat_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(226, 6);
            // 
            // toolStripMenuItemOK
            // 
            this.toolStripMenuItemOK.Name = "toolStripMenuItemOK";
            this.toolStripMenuItemOK.Size = new System.Drawing.Size(229, 52);
            this.toolStripMenuItemOK.Text = "OK";
            this.toolStripMenuItemOK.Click += new System.EventHandler(this.toolStripMenuItemOK_Click);
            // 
            // toolStripMenuItemStorno
            // 
            this.toolStripMenuItemStorno.Name = "toolStripMenuItemStorno";
            this.toolStripMenuItemStorno.Size = new System.Drawing.Size(229, 52);
            this.toolStripMenuItemStorno.Text = "Storno";
            this.toolStripMenuItemStorno.Click += new System.EventHandler(this.toolStripMenuItemStorno_Click);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = null;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "Production_Sources";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Název";
            this.dataGridTextBoxColumn1.MappingName = "ITEMNAME";
            this.dataGridTextBoxColumn1.Width = 75;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Pol.č.";
            this.dataGridTextBoxColumn2.MappingName = "ITEMNMBR";
            this.dataGridTextBoxColumn2.Width = 75;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Ozn.";
            this.dataGridTextBoxColumn3.MappingName = "ITEMCODE";
            this.dataGridTextBoxColumn3.Width = 75;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "MJ";
            this.dataGridTextBoxColumn4.MappingName = "MJ";
            this.dataGridTextBoxColumn4.Width = 75;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "Množství";
            this.dataGridTextBoxColumn5.MappingName = "QTYSHPPD";
            this.dataGridTextBoxColumn5.Width = 75;
            // 
            // předpisToolStripMenuItem
            // 
            this.předpisToolStripMenuItem.Name = "předpisToolStripMenuItem";
            this.předpisToolStripMenuItem.Size = new System.Drawing.Size(229, 52);
            this.předpisToolStripMenuItem.Text = "Předpis";
            this.předpisToolStripMenuItem.Click += new System.EventHandler(this.btn_vlozit_Click);
            // 
            // btn_ostatni
            // 
            this.btn_ostatni.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.btn_ostatni.Location = new System.Drawing.Point(302, 0);
            this.btn_ostatni.Name = "btn_ostatni";
            this.btn_ostatni.Size = new System.Drawing.Size(107, 73);
            this.btn_ostatni.TabIndex = 3;
            this.btn_ostatni.Text = "Ostatní";
            this.btn_ostatni.UseVisualStyleBackColor = true;
            this.btn_ostatni.Click += new System.EventHandler(this.toolStripMenuItemCarKod_Click);
            // 
            // FormMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(587, 380);
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelbutton);
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip1;
            this.Name = "FormMaterial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Materiál";
            this.Load += new System.EventHandler(this.FormMaterial_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMaterial_KeyDown);
            this.panelbutton.ResumeLayout(false);
            this.panelComponents.ResumeLayout(false);
            this.panelComponents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productionSourcesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VyrobaDatasetPS)).EndInit();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelbutton;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Panel panelComponents;
        private System.Windows.Forms.BindingSource productionSourcesBindingSource;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
        private System.Windows.Forms.MenuStrip MenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAkce;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHledat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSmazat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOK;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCarKod;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStorno;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Fask.SQLiteDBs.DataSets.Vyroba VyrobaDatasetPS;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mJDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSHPPDDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btn_vlozit;
        private System.Windows.Forms.Button btn_ostatni;
        private System.Windows.Forms.ToolStripMenuItem předpisToolStripMenuItem;
    }
}