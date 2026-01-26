namespace Fask.Vyroba_P.Odvadeni.Materialy
{
    partial class FormMaterialVyber
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cZCarKodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOCNCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fASKCONS095BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vyrobaDataSet1 = new Fask.SQLiteDBs.DataSets.Vyroba();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOK = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStorno = new System.Windows.Forms.ToolStripMenuItem();
            this.panelButtons.SuspendLayout();
            this.panelComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fASKCONS095BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaDataSet1)).BeginInit();
            this.MenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 474);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(727, 70);
            this.panelButtons.TabIndex = 0;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(351, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(376, 70);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(351, 70);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Cancel";
            this.buttonStorno.UseVisualStyleBackColor = true;
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.dataGridView1);
            this.panelComponents.Controls.Add(this.MenuStrip1);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(727, 474);
            this.panelComponents.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMCODEDataGridViewTextBoxColumn,
            this.mJDataGridViewTextBoxColumn,
            this.qTYDataGridViewTextBoxColumn,
            this.vNDITNUMDataGridViewTextBoxColumn,
            this.cZCarKodDataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.lOCNCODEDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.fASKCONS095BindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 56);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dataGridView1.RowTemplate.Height = 38;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(727, 418);
            this.dataGridView1.TabIndex = 0;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Název";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "Pol.č.";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMCODEDataGridViewTextBoxColumn
            // 
            this.iTEMCODEDataGridViewTextBoxColumn.DataPropertyName = "ITEMCODE";
            this.iTEMCODEDataGridViewTextBoxColumn.HeaderText = "Ozn.";
            this.iTEMCODEDataGridViewTextBoxColumn.Name = "iTEMCODEDataGridViewTextBoxColumn";
            this.iTEMCODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mJDataGridViewTextBoxColumn
            // 
            this.mJDataGridViewTextBoxColumn.DataPropertyName = "MJ";
            this.mJDataGridViewTextBoxColumn.HeaderText = "MJ";
            this.mJDataGridViewTextBoxColumn.Name = "mJDataGridViewTextBoxColumn";
            this.mJDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYDataGridViewTextBoxColumn
            // 
            this.qTYDataGridViewTextBoxColumn.DataPropertyName = "QTY";
            this.qTYDataGridViewTextBoxColumn.HeaderText = "Množství";
            this.qTYDataGridViewTextBoxColumn.Name = "qTYDataGridViewTextBoxColumn";
            this.qTYDataGridViewTextBoxColumn.ReadOnly = true;
            this.qTYDataGridViewTextBoxColumn.Width = 150;
            // 
            // vNDITNUMDataGridViewTextBoxColumn
            // 
            this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "Čár. kód";
            this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
            this.vNDITNUMDataGridViewTextBoxColumn.ReadOnly = true;
            this.vNDITNUMDataGridViewTextBoxColumn.Width = 150;
            // 
            // cZCarKodDataGridViewTextBoxColumn
            // 
            this.cZCarKodDataGridViewTextBoxColumn.DataPropertyName = "CZ_CarKod";
            this.cZCarKodDataGridViewTextBoxColumn.HeaderText = "Čár. kód. 2";
            this.cZCarKodDataGridViewTextBoxColumn.Name = "cZCarKodDataGridViewTextBoxColumn";
            this.cZCarKodDataGridViewTextBoxColumn.ReadOnly = true;
            this.cZCarKodDataGridViewTextBoxColumn.Width = 200;
            // 
            // sKLIDDataGridViewTextBoxColumn
            // 
            this.sKLIDDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.HeaderText = "Sklad ID";
            this.sKLIDDataGridViewTextBoxColumn.Name = "sKLIDDataGridViewTextBoxColumn";
            this.sKLIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.sKLIDDataGridViewTextBoxColumn.Width = 150;
            // 
            // lOCNCODEDataGridViewTextBoxColumn
            // 
            this.lOCNCODEDataGridViewTextBoxColumn.DataPropertyName = "LOCNCODE";
            this.lOCNCODEDataGridViewTextBoxColumn.HeaderText = "Lokace";
            this.lOCNCODEDataGridViewTextBoxColumn.Name = "lOCNCODEDataGridViewTextBoxColumn";
            this.lOCNCODEDataGridViewTextBoxColumn.ReadOnly = true;
            this.lOCNCODEDataGridViewTextBoxColumn.Width = 150;
            // 
            // fASKCONS095BindingSource
            // 
            this.fASKCONS095BindingSource.DataMember = "FASK_CONS_095";
            this.fASKCONS095BindingSource.DataSource = this.vyrobaDataSet1;
            // 
            // vyrobaDataSet1
            // 
            this.vyrobaDataSet1.DataSetName = "VyrobaDataSet";
            this.vyrobaDataSet1.EnforceConstraints = false;
            this.vyrobaDataSet1.Locale = new System.Globalization.CultureInfo("");
            this.vyrobaDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAkce});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(727, 56);
            this.MenuStrip1.TabIndex = 1;
            this.MenuStrip1.Text = "MenuStrip1";
            // 
            // toolStripMenuItemAkce
            // 
            this.toolStripMenuItemAkce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOK,
            this.toolStripMenuItemStorno});
            this.toolStripMenuItemAkce.Name = "toolStripMenuItemAkce";
            this.toolStripMenuItemAkce.Size = new System.Drawing.Size(115, 52);
            this.toolStripMenuItemAkce.Text = "Akce";
            // 
            // toolStripMenuItemOK
            // 
            this.toolStripMenuItemOK.Name = "toolStripMenuItemOK";
            this.toolStripMenuItemOK.Size = new System.Drawing.Size(215, 52);
            this.toolStripMenuItemOK.Text = "OK";
            this.toolStripMenuItemOK.Click += new System.EventHandler(this.toolStripMenuItemOK_Click);
            // 
            // toolStripMenuItemStorno
            // 
            this.toolStripMenuItemStorno.Name = "toolStripMenuItemStorno";
            this.toolStripMenuItemStorno.Size = new System.Drawing.Size(215, 52);
            this.toolStripMenuItemStorno.Text = "Storno";
            this.toolStripMenuItemStorno.Click += new System.EventHandler(this.toolStripMenuItemStorno_Click);
            // 
            // FormMaterialVyber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(727, 544);
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip1;
            this.Name = "FormMaterialVyber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Material Výber";
            this.Load += new System.EventHandler(this.FormMaterialVyber_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMaterialVyber_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelComponents.ResumeLayout(false);
            this.panelComponents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fASKCONS095BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaDataSet1)).EndInit();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelComponents;
        private System.Windows.Forms.MenuStrip MenuStrip1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Fask.SQLiteDBs.DataSets.Vyroba vyrobaDataSet1;
        
        public  System.Windows.Forms.BindingSource fASKCONS095BindingSource;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAkce;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOK;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStorno;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mJDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDITNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cZCarKodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOCNCODEDataGridViewTextBoxColumn;
    }
}