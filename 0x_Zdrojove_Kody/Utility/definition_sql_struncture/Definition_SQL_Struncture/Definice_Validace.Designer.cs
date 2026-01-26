namespace Definition_SQL_Struncture
{
    partial class Definice_Validace
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dg_SQL = new System.Windows.Forms.DataGridView();
            this.tABLENAMEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSNULLABLEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATATYPEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUMERICPRECISIONDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUMERICSCALEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOLUMNNAMEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_SQL = new System.Windows.Forms.BindingSource(this.components);
            this.ds_SQL = new Definition_SQL_Struncture.CreateStruncture.DS_Information();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dg_File = new System.Windows.Forms.DataGridView();
            this.tABLENAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSNULLABLEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATATYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUMERICPRECISIONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUMERICSCALEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOLUMNNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_File = new System.Windows.Forms.BindingSource(this.components);
            this.ds_File = new Definition_SQL_Struncture.CreateStruncture.DS_Information();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_SQL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_SQL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_SQL)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_File)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_File)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_File)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(722, 570);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(300, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.MinimumSize = new System.Drawing.Size(80, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(80, 570);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(14, 301);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(53, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Show";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 182);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 47);
            this.button2.TabIndex = 0;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 47);
            this.button1.TabIndex = 0;
            this.button1.Text = ">>";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dg_SQL);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(294, 564);
            this.panel2.TabIndex = 1;
            // 
            // dg_SQL
            // 
            this.dg_SQL.AllowUserToAddRows = false;
            this.dg_SQL.AllowUserToDeleteRows = false;
            this.dg_SQL.AllowUserToOrderColumns = true;
            this.dg_SQL.AllowUserToResizeRows = false;
            this.dg_SQL.AutoGenerateColumns = false;
            this.dg_SQL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_SQL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tABLENAMEDataGridViewTextBoxColumn1,
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn1,
            this.iSNULLABLEDataGridViewTextBoxColumn1,
            this.dATATYPEDataGridViewTextBoxColumn1,
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1,
            this.nUMERICPRECISIONDataGridViewTextBoxColumn1,
            this.nUMERICSCALEDataGridViewTextBoxColumn1,
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn1,
            this.cOLUMNNAMEDataGridViewTextBoxColumn1});
            this.dg_SQL.DataSource = this.bs_SQL;
            this.dg_SQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_SQL.Location = new System.Drawing.Point(0, 26);
            this.dg_SQL.Name = "dg_SQL";
            this.dg_SQL.ReadOnly = true;
            this.dg_SQL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_SQL.Size = new System.Drawing.Size(294, 538);
            this.dg_SQL.TabIndex = 1;
            this.dg_SQL.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dg_SQL_Scroll);
            // 
            // tABLENAMEDataGridViewTextBoxColumn1
            // 
            this.tABLENAMEDataGridViewTextBoxColumn1.DataPropertyName = "TABLE_NAME";
            this.tABLENAMEDataGridViewTextBoxColumn1.HeaderText = "Jmeno tabulky";
            this.tABLENAMEDataGridViewTextBoxColumn1.Name = "tABLENAMEDataGridViewTextBoxColumn1";
            this.tABLENAMEDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // cOLUMNDEFAULTDataGridViewTextBoxColumn1
            // 
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn1.DataPropertyName = "COLUMN_DEFAULT";
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn1.HeaderText = "Výchozí hodnota";
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn1.Name = "cOLUMNDEFAULTDataGridViewTextBoxColumn1";
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // iSNULLABLEDataGridViewTextBoxColumn1
            // 
            this.iSNULLABLEDataGridViewTextBoxColumn1.DataPropertyName = "IS_NULLABLE";
            this.iSNULLABLEDataGridViewTextBoxColumn1.HeaderText = "Akceptovat NULL";
            this.iSNULLABLEDataGridViewTextBoxColumn1.Name = "iSNULLABLEDataGridViewTextBoxColumn1";
            this.iSNULLABLEDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dATATYPEDataGridViewTextBoxColumn1
            // 
            this.dATATYPEDataGridViewTextBoxColumn1.DataPropertyName = "DATA_TYPE";
            this.dATATYPEDataGridViewTextBoxColumn1.HeaderText = "Datovy Typ";
            this.dATATYPEDataGridViewTextBoxColumn1.Name = "dATATYPEDataGridViewTextBoxColumn1";
            this.dATATYPEDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1
            // 
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1.DataPropertyName = "CHARACTER_MAXIMUM_LENGTH";
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1.HeaderText = "Maximalni delka";
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1.Name = "cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1";
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // nUMERICPRECISIONDataGridViewTextBoxColumn1
            // 
            this.nUMERICPRECISIONDataGridViewTextBoxColumn1.DataPropertyName = "NUMERIC_PRECISION";
            this.nUMERICPRECISIONDataGridViewTextBoxColumn1.HeaderText = "číslo přesnost";
            this.nUMERICPRECISIONDataGridViewTextBoxColumn1.Name = "nUMERICPRECISIONDataGridViewTextBoxColumn1";
            this.nUMERICPRECISIONDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // nUMERICSCALEDataGridViewTextBoxColumn1
            // 
            this.nUMERICSCALEDataGridViewTextBoxColumn1.DataPropertyName = "NUMERIC_SCALE";
            this.nUMERICSCALEDataGridViewTextBoxColumn1.HeaderText = "číslo rozsah";
            this.nUMERICSCALEDataGridViewTextBoxColumn1.Name = "nUMERICSCALEDataGridViewTextBoxColumn1";
            this.nUMERICSCALEDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dATETIMEPRECISIONDataGridViewTextBoxColumn1
            // 
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn1.DataPropertyName = "DATETIME_PRECISION";
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn1.HeaderText = "datum presnost";
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn1.Name = "dATETIMEPRECISIONDataGridViewTextBoxColumn1";
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // cOLUMNNAMEDataGridViewTextBoxColumn1
            // 
            this.cOLUMNNAMEDataGridViewTextBoxColumn1.DataPropertyName = "COLUMN_NAME";
            this.cOLUMNNAMEDataGridViewTextBoxColumn1.HeaderText = "Jmeno stloupce";
            this.cOLUMNNAMEDataGridViewTextBoxColumn1.Name = "cOLUMNNAMEDataGridViewTextBoxColumn1";
            this.cOLUMNNAMEDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // bs_SQL
            // 
            this.bs_SQL.DataMember = "SchemaColumns";
            this.bs_SQL.DataSource = this.ds_SQL;
            // 
            // ds_SQL
            // 
            this.ds_SQL.DataSetName = "DS_Information";
            this.ds_SQL.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "SQL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dg_File);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(383, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(336, 564);
            this.panel3.TabIndex = 2;
            // 
            // dg_File
            // 
            this.dg_File.AllowUserToAddRows = false;
            this.dg_File.AllowUserToDeleteRows = false;
            this.dg_File.AllowUserToOrderColumns = true;
            this.dg_File.AllowUserToResizeRows = false;
            this.dg_File.AutoGenerateColumns = false;
            this.dg_File.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_File.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tABLENAMEDataGridViewTextBoxColumn,
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn,
            this.iSNULLABLEDataGridViewTextBoxColumn,
            this.dATATYPEDataGridViewTextBoxColumn,
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn,
            this.nUMERICPRECISIONDataGridViewTextBoxColumn,
            this.nUMERICSCALEDataGridViewTextBoxColumn,
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn,
            this.cOLUMNNAMEDataGridViewTextBoxColumn});
            this.dg_File.DataSource = this.bs_File;
            this.dg_File.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_File.Location = new System.Drawing.Point(0, 23);
            this.dg_File.Name = "dg_File";
            this.dg_File.ReadOnly = true;
            this.dg_File.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_File.Size = new System.Drawing.Size(336, 541);
            this.dg_File.TabIndex = 1;
            this.dg_File.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dg_File_Scroll);
            // 
            // tABLENAMEDataGridViewTextBoxColumn
            // 
            this.tABLENAMEDataGridViewTextBoxColumn.DataPropertyName = "TABLE_NAME";
            this.tABLENAMEDataGridViewTextBoxColumn.HeaderText = "Jmeno tabulky";
            this.tABLENAMEDataGridViewTextBoxColumn.Name = "tABLENAMEDataGridViewTextBoxColumn";
            this.tABLENAMEDataGridViewTextBoxColumn.ReadOnly = true;
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
            // cOLUMNNAMEDataGridViewTextBoxColumn
            // 
            this.cOLUMNNAMEDataGridViewTextBoxColumn.DataPropertyName = "COLUMN_NAME";
            this.cOLUMNNAMEDataGridViewTextBoxColumn.HeaderText = "Jmeno stloupce";
            this.cOLUMNNAMEDataGridViewTextBoxColumn.Name = "cOLUMNNAMEDataGridViewTextBoxColumn";
            this.cOLUMNNAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bs_File
            // 
            this.bs_File.DataMember = "SchemaColumns";
            this.bs_File.DataSource = this.ds_File;
            // 
            // ds_File
            // 
            this.ds_File.DataSetName = "DS_Information";
            this.ds_File.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(336, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "XML soubor";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Definice_Validace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 570);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "Definice_Validace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Definice_Validace";
            this.Load += new System.EventHandler(this.Definice_Validace_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_SQL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_SQL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_SQL)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_File)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_File)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_File)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dg_SQL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dg_File;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource bs_SQL;
        private System.Windows.Forms.BindingSource bs_File;
        public CreateStruncture.DS_Information ds_SQL;
        public CreateStruncture.DS_Information ds_File;
        private System.Windows.Forms.DataGridViewTextBoxColumn tABLENAMEDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOLUMNDEFAULTDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iSNULLABLEDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATATYPEDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUMERICPRECISIONDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUMERICSCALEDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATETIMEPRECISIONDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOLUMNNAMEDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tABLENAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOLUMNDEFAULTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iSNULLABLEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATATYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUMERICPRECISIONDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUMERICSCALEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATETIMEPRECISIONDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOLUMNNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}