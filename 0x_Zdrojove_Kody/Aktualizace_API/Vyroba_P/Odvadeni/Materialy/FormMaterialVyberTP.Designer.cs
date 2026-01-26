namespace Fask.Aktualizace_API.Odvadeni.Materialy
{
    partial class FormMaterialVyberTP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btn_nezadavat = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOK = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStorno = new System.Windows.Forms.ToolStripMenuItem();
            this.fASKVyroba_TPBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vyrobaDataSet1 = new Fask.SQLiteDBs.DataSets.Vyroba();
            this.fASKCONS095BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CZ_CarKod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sernum_Treck = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRDefDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dESCDefDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mJDefDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mn_vyrobku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRfolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dESCFolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mJFolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.koefDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Predpis_Mn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Zadane_Mn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Zbyva_Mn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButtons.SuspendLayout();
            this.panelComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.MenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fASKVyroba_TPBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fASKCONS095BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btn_nezadavat);
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 474);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(727, 70);
            this.panelButtons.TabIndex = 0;
            // 
            // btn_nezadavat
            // 
            this.btn_nezadavat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_nezadavat.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.btn_nezadavat.Location = new System.Drawing.Point(249, 0);
            this.btn_nezadavat.Name = "btn_nezadavat";
            this.btn_nezadavat.Size = new System.Drawing.Size(233, 70);
            this.btn_nezadavat.TabIndex = 2;
            this.btn_nezadavat.Text = "Nezadávat";
            this.btn_nezadavat.UseVisualStyleBackColor = true;
            this.btn_nezadavat.Click += new System.EventHandler(this.btn_nezadavat_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(482, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(245, 70);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "Vyber";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(249, 70);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Zpět";
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
            this.CZ_CarKod,
            this.Sernum_Treck,
            this.iTEMNMBRDefDataGridViewTextBoxColumn,
            this.dESCDefDataGridViewTextBoxColumn,
            this.mJDefDataGridViewTextBoxColumn,
            this.Mn_vyrobku,
            this.iTEMNMBRfolDataGridViewTextBoxColumn,
            this.dESCFolDataGridViewTextBoxColumn,
            this.mJFolDataGridViewTextBoxColumn,
            this.koefDataGridViewTextBoxColumn,
            this.Predpis_Mn,
            this.Zadane_Mn,
            this.Zbyva_Mn});
            this.dataGridView1.DataSource = this.fASKVyroba_TPBindingSource;
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
            this.toolStripMenuItemOK.Size = new System.Drawing.Size(201, 52);
            this.toolStripMenuItemOK.Text = "Vyber";
            this.toolStripMenuItemOK.Click += new System.EventHandler(this.toolStripMenuItemOK_Click);
            // 
            // toolStripMenuItemStorno
            // 
            this.toolStripMenuItemStorno.Name = "toolStripMenuItemStorno";
            this.toolStripMenuItemStorno.Size = new System.Drawing.Size(201, 52);
            this.toolStripMenuItemStorno.Text = "Zpět";
            this.toolStripMenuItemStorno.Click += new System.EventHandler(this.toolStripMenuItemStorno_Click);
            // 
            // fASKVyroba_TPBindingSource
            // 
            this.fASKVyroba_TPBindingSource.DataMember = "FASK_Vyroba_TP";
            this.fASKVyroba_TPBindingSource.DataSource = this.vyrobaDataSet1;
            // 
            // vyrobaDataSet1
            // 
            this.vyrobaDataSet1.DataSetName = "VyrobaDataSet";
            this.vyrobaDataSet1.EnforceConstraints = false;
            this.vyrobaDataSet1.Locale = new System.Globalization.CultureInfo("");
            this.vyrobaDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fASKCONS095BindingSource
            // 
            this.fASKCONS095BindingSource.DataMember = "FASK_CONS_095";
            this.fASKCONS095BindingSource.DataSource = this.vyrobaDataSet1;
            // 
            // CZ_CarKod
            // 
            this.CZ_CarKod.DataPropertyName = "CZ_CarKod";
            this.CZ_CarKod.HeaderText = "Čárový kód";
            this.CZ_CarKod.Name = "CZ_CarKod";
            this.CZ_CarKod.ReadOnly = true;
            // 
            // Sernum_Treck
            // 
            this.Sernum_Treck.DataPropertyName = "CZ_SerNum_Track";
            this.Sernum_Treck.HeaderText = "Typ sledování";
            this.Sernum_Treck.Name = "Sernum_Treck";
            this.Sernum_Treck.ReadOnly = true;
            // 
            // iTEMNMBRDefDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDefDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR_Def";
            this.iTEMNMBRDefDataGridViewTextBoxColumn.HeaderText = "ID výrobku";
            this.iTEMNMBRDefDataGridViewTextBoxColumn.Name = "iTEMNMBRDefDataGridViewTextBoxColumn";
            this.iTEMNMBRDefDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dESCDefDataGridViewTextBoxColumn
            // 
            this.dESCDefDataGridViewTextBoxColumn.DataPropertyName = "DESC_Def";
            this.dESCDefDataGridViewTextBoxColumn.HeaderText = "Název výrobku";
            this.dESCDefDataGridViewTextBoxColumn.Name = "dESCDefDataGridViewTextBoxColumn";
            this.dESCDefDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mJDefDataGridViewTextBoxColumn
            // 
            this.mJDefDataGridViewTextBoxColumn.DataPropertyName = "MJ_Def";
            this.mJDefDataGridViewTextBoxColumn.HeaderText = "Měrná jednotka výrobku";
            this.mJDefDataGridViewTextBoxColumn.Name = "mJDefDataGridViewTextBoxColumn";
            this.mJDefDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Mn_vyrobku
            // 
            this.Mn_vyrobku.DataPropertyName = "Mn_vyrobku";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Mn_vyrobku.DefaultCellStyle = dataGridViewCellStyle2;
            this.Mn_vyrobku.HeaderText = "Množství výrobku";
            this.Mn_vyrobku.Name = "Mn_vyrobku";
            this.Mn_vyrobku.ReadOnly = true;
            // 
            // iTEMNMBRfolDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRfolDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR_fol";
            this.iTEMNMBRfolDataGridViewTextBoxColumn.HeaderText = "ID materiálu";
            this.iTEMNMBRfolDataGridViewTextBoxColumn.Name = "iTEMNMBRfolDataGridViewTextBoxColumn";
            this.iTEMNMBRfolDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dESCFolDataGridViewTextBoxColumn
            // 
            this.dESCFolDataGridViewTextBoxColumn.DataPropertyName = "DESC_Fol";
            this.dESCFolDataGridViewTextBoxColumn.HeaderText = "Název materiálu";
            this.dESCFolDataGridViewTextBoxColumn.Name = "dESCFolDataGridViewTextBoxColumn";
            this.dESCFolDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mJFolDataGridViewTextBoxColumn
            // 
            this.mJFolDataGridViewTextBoxColumn.DataPropertyName = "MJ_Fol";
            this.mJFolDataGridViewTextBoxColumn.HeaderText = "Měrná jednotka materiálu";
            this.mJFolDataGridViewTextBoxColumn.Name = "mJFolDataGridViewTextBoxColumn";
            this.mJFolDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // koefDataGridViewTextBoxColumn
            // 
            this.koefDataGridViewTextBoxColumn.DataPropertyName = "koef";
            this.koefDataGridViewTextBoxColumn.HeaderText = "koeficient";
            this.koefDataGridViewTextBoxColumn.Name = "koefDataGridViewTextBoxColumn";
            this.koefDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Predpis_Mn
            // 
            this.Predpis_Mn.DataPropertyName = "Predpis_Mn";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Predpis_Mn.DefaultCellStyle = dataGridViewCellStyle3;
            this.Predpis_Mn.HeaderText = "Předpis množství materiálu";
            this.Predpis_Mn.Name = "Predpis_Mn";
            this.Predpis_Mn.ReadOnly = true;
            // 
            // Zadane_Mn
            // 
            this.Zadane_Mn.DataPropertyName = "Zadane_Mn";
            this.Zadane_Mn.HeaderText = "Zadáno";
            this.Zadane_Mn.Name = "Zadane_Mn";
            this.Zadane_Mn.ReadOnly = true;
            // 
            // Zbyva_Mn
            // 
            this.Zbyva_Mn.DataPropertyName = "Zbyva_Mn";
            this.Zbyva_Mn.HeaderText = "Zbývá";
            this.Zbyva_Mn.Name = "Zbyva_Mn";
            this.Zbyva_Mn.ReadOnly = true;
            // 
            // FormMaterialVyberTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(727, 544);
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip1;
            this.Name = "FormMaterialVyberTP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Material Výber";
            this.Load += new System.EventHandler(this.FormMaterialVyber_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMaterialVyber_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelComponents.ResumeLayout(false);
            this.panelComponents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fASKVyroba_TPBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fASKCONS095BindingSource)).EndInit();
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
        private System.Windows.Forms.BindingSource fASKVyroba_TPBindingSource;
        private System.Windows.Forms.Button btn_nezadavat;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ_CarKod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sernum_Treck;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDefDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dESCDefDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mJDefDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mn_vyrobku;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRfolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dESCFolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mJFolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn koefDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Predpis_Mn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zadane_Mn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zbyva_Mn;
        // private System.Windows.Forms.BindingSource fASKVyroba_TP_new_BindingSource;
    }
}