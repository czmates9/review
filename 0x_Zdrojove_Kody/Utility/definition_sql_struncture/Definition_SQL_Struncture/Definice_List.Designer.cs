namespace Definition_SQL_Struncture
{
    partial class Definice_List
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
            this.bs = new System.Windows.Forms.BindingSource(this.components);
            this.ds = new Definition_SQL_Struncture.CreateStruncture.DS_Information();
            this.dg = new System.Windows.Forms.DataGridView();
            this.tABLENAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOLUMNNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSNULLABLEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATATYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUMERICPRECISIONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUMERICSCALEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Edit = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_LoadPath = new System.Windows.Forms.Button();
            this.cb_Type = new System.Windows.Forms.ComboBox();
            this.tb_TableName = new System.Windows.Forms.TextBox();
            this.tb_ColumnName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_ClearFilter = new System.Windows.Forms.Button();
            this.panel_button = new System.Windows.Forms.Panel();
            this.btn_Verifikace = new System.Windows.Forms.Button();
            this.panel_Filtr = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_CreateFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_Konec = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.panel_button.SuspendLayout();
            this.panel_Filtr.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bs
            // 
            this.bs.DataSource = this.ds;
            this.bs.Position = 0;
            // 
            // ds
            // 
            this.ds.DataSetName = "DS_Information";
            this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AllowUserToOrderColumns = true;
            this.dg.AllowUserToResizeRows = false;
            this.dg.AutoGenerateColumns = false;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tABLENAMEDataGridViewTextBoxColumn,
            this.cOLUMNNAMEDataGridViewTextBoxColumn,
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn,
            this.iSNULLABLEDataGridViewTextBoxColumn,
            this.dATATYPEDataGridViewTextBoxColumn,
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn,
            this.nUMERICPRECISIONDataGridViewTextBoxColumn,
            this.nUMERICSCALEDataGridViewTextBoxColumn,
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn});
            this.dg.DataMember = "SchemaColumns";
            this.dg.DataSource = this.bs;
            this.dg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg.Location = new System.Drawing.Point(0, 136);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg.Size = new System.Drawing.Size(741, 379);
            this.dg.TabIndex = 11;
            // 
            // tABLENAMEDataGridViewTextBoxColumn
            // 
            this.tABLENAMEDataGridViewTextBoxColumn.DataPropertyName = "TABLE_NAME";
            this.tABLENAMEDataGridViewTextBoxColumn.HeaderText = "Jmeno tabulky";
            this.tABLENAMEDataGridViewTextBoxColumn.Name = "tABLENAMEDataGridViewTextBoxColumn";
            this.tABLENAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cOLUMNNAMEDataGridViewTextBoxColumn
            // 
            this.cOLUMNNAMEDataGridViewTextBoxColumn.DataPropertyName = "COLUMN_NAME";
            this.cOLUMNNAMEDataGridViewTextBoxColumn.HeaderText = "Jmeno stloupce";
            this.cOLUMNNAMEDataGridViewTextBoxColumn.Name = "cOLUMNNAMEDataGridViewTextBoxColumn";
            this.cOLUMNNAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cOLUMNDEFAULTDataGridViewTextBoxColumn
            // 
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn.DataPropertyName = "COLUMN_DEFAULT";
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn.HeaderText = "Výchozí hodnota";
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn.Name = "cOLUMNDEFAULTDataGridViewTextBoxColumn";
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iSNULLABLEDataGridViewTextBoxColumn
            // 
            this.iSNULLABLEDataGridViewTextBoxColumn.DataPropertyName = "IS_NULLABLE";
            this.iSNULLABLEDataGridViewTextBoxColumn.HeaderText = "Akceptovat NULL";
            this.iSNULLABLEDataGridViewTextBoxColumn.Name = "iSNULLABLEDataGridViewTextBoxColumn";
            this.iSNULLABLEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dATATYPEDataGridViewTextBoxColumn
            // 
            this.dATATYPEDataGridViewTextBoxColumn.DataPropertyName = "DATA_TYPE";
            this.dATATYPEDataGridViewTextBoxColumn.HeaderText = "Datovy Typ";
            this.dATATYPEDataGridViewTextBoxColumn.Name = "dATATYPEDataGridViewTextBoxColumn";
            this.dATATYPEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn
            // 
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn.DataPropertyName = "CHARACTER_MAXIMUM_LENGTH";
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn.HeaderText = "Maximalni delka";
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn.Name = "cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn";
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nUMERICPRECISIONDataGridViewTextBoxColumn
            // 
            this.nUMERICPRECISIONDataGridViewTextBoxColumn.DataPropertyName = "NUMERIC_PRECISION";
            this.nUMERICPRECISIONDataGridViewTextBoxColumn.HeaderText = "číslo přesnost";
            this.nUMERICPRECISIONDataGridViewTextBoxColumn.Name = "nUMERICPRECISIONDataGridViewTextBoxColumn";
            this.nUMERICPRECISIONDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nUMERICSCALEDataGridViewTextBoxColumn
            // 
            this.nUMERICSCALEDataGridViewTextBoxColumn.DataPropertyName = "NUMERIC_SCALE";
            this.nUMERICSCALEDataGridViewTextBoxColumn.HeaderText = "číslo rozsah";
            this.nUMERICSCALEDataGridViewTextBoxColumn.Name = "nUMERICSCALEDataGridViewTextBoxColumn";
            this.nUMERICSCALEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dATETIMEPRECISIONDataGridViewTextBoxColumn
            // 
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn.DataPropertyName = "DATETIME_PRECISION";
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn.HeaderText = "datum presnost";
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn.Name = "dATETIMEPRECISIONDataGridViewTextBoxColumn";
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.Location = new System.Drawing.Point(8, 163);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(90, 70);
            this.btn_Delete.TabIndex = 7;
            this.btn_Delete.Text = "Smazat";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Edit
            // 
            this.btn_Edit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Edit.Location = new System.Drawing.Point(8, 87);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(90, 70);
            this.btn_Edit.TabIndex = 6;
            this.btn_Edit.Text = "Upravit";
            this.btn_Edit.UseVisualStyleBackColor = true;
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // btn_add
            // 
            this.btn_add.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_add.Location = new System.Drawing.Point(8, 11);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(90, 70);
            this.btn_add.TabIndex = 5;
            this.btn_add.Text = "Pridat";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(3, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(607, 20);
            this.textBox1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cesta k definičnimu souboru";
            // 
            // btn_LoadPath
            // 
            this.btn_LoadPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LoadPath.Location = new System.Drawing.Point(616, 30);
            this.btn_LoadPath.Name = "btn_LoadPath";
            this.btn_LoadPath.Size = new System.Drawing.Size(108, 23);
            this.btn_LoadPath.TabIndex = 9;
            this.btn_LoadPath.Text = "Načist cestu";
            this.btn_LoadPath.UseVisualStyleBackColor = true;
            this.btn_LoadPath.Click += new System.EventHandler(this.btn_LoadPath_Click);
            // 
            // cb_Type
            // 
            this.cb_Type.FormattingEnabled = true;
            this.cb_Type.Location = new System.Drawing.Point(130, 71);
            this.cb_Type.Name = "cb_Type";
            this.cb_Type.Size = new System.Drawing.Size(172, 21);
            this.cb_Type.TabIndex = 2;
            // 
            // tb_TableName
            // 
            this.tb_TableName.Location = new System.Drawing.Point(130, 19);
            this.tb_TableName.Name = "tb_TableName";
            this.tb_TableName.Size = new System.Drawing.Size(172, 20);
            this.tb_TableName.TabIndex = 0;
            // 
            // tb_ColumnName
            // 
            this.tb_ColumnName.Location = new System.Drawing.Point(130, 45);
            this.tb_ColumnName.Name = "tb_ColumnName";
            this.tb_ColumnName.Size = new System.Drawing.Size(172, 20);
            this.tb_ColumnName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(53, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Datovy typ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(25, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Jmeno tabulky";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(16, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Jmeno stloupce";
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(308, 17);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(117, 75);
            this.btn_Search.TabIndex = 3;
            this.btn_Search.Text = "Hledej";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_ClearFilter
            // 
            this.btn_ClearFilter.Location = new System.Drawing.Point(431, 17);
            this.btn_ClearFilter.Name = "btn_ClearFilter";
            this.btn_ClearFilter.Size = new System.Drawing.Size(111, 75);
            this.btn_ClearFilter.TabIndex = 4;
            this.btn_ClearFilter.Text = "Vyčistit filtry";
            this.btn_ClearFilter.UseVisualStyleBackColor = true;
            this.btn_ClearFilter.Click += new System.EventHandler(this.btn_ClearFilter_Click);
            // 
            // panel_button
            // 
            this.panel_button.Controls.Add(this.btn_Verifikace);
            this.panel_button.Controls.Add(this.btn_add);
            this.panel_button.Controls.Add(this.btn_Delete);
            this.panel_button.Controls.Add(this.btn_Edit);
            this.panel_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_button.Enabled = false;
            this.panel_button.Location = new System.Drawing.Point(741, 24);
            this.panel_button.Name = "panel_button";
            this.panel_button.Size = new System.Drawing.Size(107, 555);
            this.panel_button.TabIndex = 12;
            // 
            // btn_Verifikace
            // 
            this.btn_Verifikace.Location = new System.Drawing.Point(8, 239);
            this.btn_Verifikace.Name = "btn_Verifikace";
            this.btn_Verifikace.Size = new System.Drawing.Size(90, 70);
            this.btn_Verifikace.TabIndex = 9;
            this.btn_Verifikace.Text = "Verifikace";
            this.btn_Verifikace.UseVisualStyleBackColor = true;
            this.btn_Verifikace.Click += new System.EventHandler(this.btn_Verifikace_Click);
            // 
            // panel_Filtr
            // 
            this.panel_Filtr.Controls.Add(this.label3);
            this.panel_Filtr.Controls.Add(this.cb_Type);
            this.panel_Filtr.Controls.Add(this.btn_ClearFilter);
            this.panel_Filtr.Controls.Add(this.tb_TableName);
            this.panel_Filtr.Controls.Add(this.btn_Search);
            this.panel_Filtr.Controls.Add(this.tb_ColumnName);
            this.panel_Filtr.Controls.Add(this.label4);
            this.panel_Filtr.Controls.Add(this.label2);
            this.panel_Filtr.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Filtr.Enabled = false;
            this.panel_Filtr.Location = new System.Drawing.Point(0, 24);
            this.panel_Filtr.Name = "panel_Filtr";
            this.panel_Filtr.Size = new System.Drawing.Size(741, 112);
            this.panel_Filtr.TabIndex = 13;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(848, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_CreateFile,
            this.toolStripSeparator1,
            this.tsmi_Konec});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // tsmi_CreateFile
            // 
            this.tsmi_CreateFile.Name = "tsmi_CreateFile";
            this.tsmi_CreateFile.Size = new System.Drawing.Size(186, 22);
            this.tsmi_CreateFile.Text = "Vytvořeni souboru";
            this.tsmi_CreateFile.Click += new System.EventHandler(this.tsmi_CreateFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // tsmi_Konec
            // 
            this.tsmi_Konec.Name = "tsmi_Konec";
            this.tsmi_Konec.Size = new System.Drawing.Size(186, 22);
            this.tsmi_Konec.Text = "Konec";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_LoadPath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 515);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(741, 64);
            this.panel1.TabIndex = 14;
            // 
            // Definice_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 579);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_Filtr);
            this.Controls.Add(this.panel_button);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Definice_List";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Definice_List_FormClosing);
            this.Load += new System.EventHandler(this.EditKonstanty_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.panel_button.ResumeLayout(false);
            this.panel_Filtr.ResumeLayout(false);
            this.panel_Filtr.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bs;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Edit;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private Definition_SQL_Struncture.CreateStruncture.DS_Information ds;
        private System.Windows.Forms.Button btn_LoadPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn tABLENAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOLUMNNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOLUMNDEFAULTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iSNULLABLEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATATYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUMERICPRECISIONDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUMERICSCALEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATETIMEPRECISIONDataGridViewTextBoxColumn;
        private System.Windows.Forms.ComboBox cb_Type;
        private System.Windows.Forms.TextBox tb_TableName;
        private System.Windows.Forms.TextBox tb_ColumnName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_ClearFilter;
        private System.Windows.Forms.Panel panel_button;
        private System.Windows.Forms.Panel panel_Filtr;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Verifikace;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_CreateFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Konec;
    }
}