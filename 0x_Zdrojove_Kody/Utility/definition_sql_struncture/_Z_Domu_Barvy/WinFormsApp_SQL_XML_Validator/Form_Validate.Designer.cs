using WinFormsApp_SQL_XML_Validator.CreateStruncture;

namespace WinFormsApp_SQL_XML_Validator
{
    partial class Form_Validate
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
            this.dg_SQL = new System.Windows.Forms.DataGridView();
            this.tABLENAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSNULLABLEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATATYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUMERICPRECISIONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUMERICSCALEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOLUMNNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_SQL = new System.Windows.Forms.BindingSource(this.components);
            this.ds_SQL = new WinFormsApp_SQL_XML_Validator.CreateStruncture.DS_Information();
            this.dg_File = new System.Windows.Forms.DataGridView();
            this.tABLENAMEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSNULLABLEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATATYPEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUMERICPRECISIONDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUMERICSCALEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOLUMNNAMEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_File = new System.Windows.Forms.BindingSource(this.components);
            this.ds_File = new WinFormsApp_SQL_XML_Validator.CreateStruncture.DS_Information();
            ((System.ComponentModel.ISupportInitialize)(this.dg_SQL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_SQL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_SQL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_File)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_File)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_File)).BeginInit();
            this.SuspendLayout();
            // 
            // dg_SQL
            // 
            this.dg_SQL.AllowUserToAddRows = false;
            this.dg_SQL.AutoGenerateColumns = false;
            this.dg_SQL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_SQL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tABLENAMEDataGridViewTextBoxColumn,
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn,
            this.iSNULLABLEDataGridViewTextBoxColumn,
            this.dATATYPEDataGridViewTextBoxColumn,
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn,
            this.nUMERICPRECISIONDataGridViewTextBoxColumn,
            this.nUMERICSCALEDataGridViewTextBoxColumn,
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn,
            this.cOLUMNNAMEDataGridViewTextBoxColumn});
            this.dg_SQL.DataSource = this.bs_SQL;
            this.dg_SQL.Dock = System.Windows.Forms.DockStyle.Left;
            this.dg_SQL.Location = new System.Drawing.Point(0, 0);
            this.dg_SQL.Name = "dg_SQL";
            this.dg_SQL.Size = new System.Drawing.Size(496, 582);
            this.dg_SQL.TabIndex = 0;
            // 
            // tABLENAMEDataGridViewTextBoxColumn
            // 
            this.tABLENAMEDataGridViewTextBoxColumn.DataPropertyName = "TABLE_NAME";
            this.tABLENAMEDataGridViewTextBoxColumn.HeaderText = "TABLE_NAME";
            this.tABLENAMEDataGridViewTextBoxColumn.Name = "tABLENAMEDataGridViewTextBoxColumn";
            // 
            // cOLUMNDEFAULTDataGridViewTextBoxColumn
            // 
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn.DataPropertyName = "COLUMN_DEFAULT";
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn.HeaderText = "COLUMN_DEFAULT";
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn.Name = "cOLUMNDEFAULTDataGridViewTextBoxColumn";
            // 
            // iSNULLABLEDataGridViewTextBoxColumn
            // 
            this.iSNULLABLEDataGridViewTextBoxColumn.DataPropertyName = "IS_NULLABLE";
            this.iSNULLABLEDataGridViewTextBoxColumn.HeaderText = "IS_NULLABLE";
            this.iSNULLABLEDataGridViewTextBoxColumn.Name = "iSNULLABLEDataGridViewTextBoxColumn";
            // 
            // dATATYPEDataGridViewTextBoxColumn
            // 
            this.dATATYPEDataGridViewTextBoxColumn.DataPropertyName = "DATA_TYPE";
            this.dATATYPEDataGridViewTextBoxColumn.HeaderText = "DATA_TYPE";
            this.dATATYPEDataGridViewTextBoxColumn.Name = "dATATYPEDataGridViewTextBoxColumn";
            // 
            // cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn
            // 
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn.DataPropertyName = "CHARACTER_MAXIMUM_LENGTH";
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn.HeaderText = "CHARACTER_MAXIMUM_LENGTH";
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn.Name = "cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn";
            // 
            // nUMERICPRECISIONDataGridViewTextBoxColumn
            // 
            this.nUMERICPRECISIONDataGridViewTextBoxColumn.DataPropertyName = "NUMERIC_PRECISION";
            this.nUMERICPRECISIONDataGridViewTextBoxColumn.HeaderText = "NUMERIC_PRECISION";
            this.nUMERICPRECISIONDataGridViewTextBoxColumn.Name = "nUMERICPRECISIONDataGridViewTextBoxColumn";
            // 
            // nUMERICSCALEDataGridViewTextBoxColumn
            // 
            this.nUMERICSCALEDataGridViewTextBoxColumn.DataPropertyName = "NUMERIC_SCALE";
            this.nUMERICSCALEDataGridViewTextBoxColumn.HeaderText = "NUMERIC_SCALE";
            this.nUMERICSCALEDataGridViewTextBoxColumn.Name = "nUMERICSCALEDataGridViewTextBoxColumn";
            // 
            // dATETIMEPRECISIONDataGridViewTextBoxColumn
            // 
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn.DataPropertyName = "DATETIME_PRECISION";
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn.HeaderText = "DATETIME_PRECISION";
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn.Name = "dATETIMEPRECISIONDataGridViewTextBoxColumn";
            // 
            // cOLUMNNAMEDataGridViewTextBoxColumn
            // 
            this.cOLUMNNAMEDataGridViewTextBoxColumn.DataPropertyName = "COLUMN_NAME";
            this.cOLUMNNAMEDataGridViewTextBoxColumn.HeaderText = "COLUMN_NAME";
            this.cOLUMNNAMEDataGridViewTextBoxColumn.Name = "cOLUMNNAMEDataGridViewTextBoxColumn";
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
            // dg_File
            // 
            this.dg_File.AllowUserToAddRows = false;
            this.dg_File.AutoGenerateColumns = false;
            this.dg_File.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_File.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tABLENAMEDataGridViewTextBoxColumn1,
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn1,
            this.iSNULLABLEDataGridViewTextBoxColumn1,
            this.dATATYPEDataGridViewTextBoxColumn1,
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1,
            this.nUMERICPRECISIONDataGridViewTextBoxColumn1,
            this.nUMERICSCALEDataGridViewTextBoxColumn1,
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn1,
            this.cOLUMNNAMEDataGridViewTextBoxColumn1});
            this.dg_File.DataSource = this.bs_File;
            this.dg_File.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_File.Location = new System.Drawing.Point(496, 0);
            this.dg_File.Name = "dg_File";
            this.dg_File.Size = new System.Drawing.Size(443, 582);
            this.dg_File.TabIndex = 1;
            // 
            // tABLENAMEDataGridViewTextBoxColumn1
            // 
            this.tABLENAMEDataGridViewTextBoxColumn1.DataPropertyName = "TABLE_NAME";
            this.tABLENAMEDataGridViewTextBoxColumn1.HeaderText = "TABLE_NAME";
            this.tABLENAMEDataGridViewTextBoxColumn1.Name = "tABLENAMEDataGridViewTextBoxColumn1";
            // 
            // cOLUMNDEFAULTDataGridViewTextBoxColumn1
            // 
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn1.DataPropertyName = "COLUMN_DEFAULT";
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn1.HeaderText = "COLUMN_DEFAULT";
            this.cOLUMNDEFAULTDataGridViewTextBoxColumn1.Name = "cOLUMNDEFAULTDataGridViewTextBoxColumn1";
            // 
            // iSNULLABLEDataGridViewTextBoxColumn1
            // 
            this.iSNULLABLEDataGridViewTextBoxColumn1.DataPropertyName = "IS_NULLABLE";
            this.iSNULLABLEDataGridViewTextBoxColumn1.HeaderText = "IS_NULLABLE";
            this.iSNULLABLEDataGridViewTextBoxColumn1.Name = "iSNULLABLEDataGridViewTextBoxColumn1";
            // 
            // dATATYPEDataGridViewTextBoxColumn1
            // 
            this.dATATYPEDataGridViewTextBoxColumn1.DataPropertyName = "DATA_TYPE";
            this.dATATYPEDataGridViewTextBoxColumn1.HeaderText = "DATA_TYPE";
            this.dATATYPEDataGridViewTextBoxColumn1.Name = "dATATYPEDataGridViewTextBoxColumn1";
            // 
            // cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1
            // 
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1.DataPropertyName = "CHARACTER_MAXIMUM_LENGTH";
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1.HeaderText = "CHARACTER_MAXIMUM_LENGTH";
            this.cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1.Name = "cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1";
            // 
            // nUMERICPRECISIONDataGridViewTextBoxColumn1
            // 
            this.nUMERICPRECISIONDataGridViewTextBoxColumn1.DataPropertyName = "NUMERIC_PRECISION";
            this.nUMERICPRECISIONDataGridViewTextBoxColumn1.HeaderText = "NUMERIC_PRECISION";
            this.nUMERICPRECISIONDataGridViewTextBoxColumn1.Name = "nUMERICPRECISIONDataGridViewTextBoxColumn1";
            // 
            // nUMERICSCALEDataGridViewTextBoxColumn1
            // 
            this.nUMERICSCALEDataGridViewTextBoxColumn1.DataPropertyName = "NUMERIC_SCALE";
            this.nUMERICSCALEDataGridViewTextBoxColumn1.HeaderText = "NUMERIC_SCALE";
            this.nUMERICSCALEDataGridViewTextBoxColumn1.Name = "nUMERICSCALEDataGridViewTextBoxColumn1";
            // 
            // dATETIMEPRECISIONDataGridViewTextBoxColumn1
            // 
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn1.DataPropertyName = "DATETIME_PRECISION";
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn1.HeaderText = "DATETIME_PRECISION";
            this.dATETIMEPRECISIONDataGridViewTextBoxColumn1.Name = "dATETIMEPRECISIONDataGridViewTextBoxColumn1";
            // 
            // cOLUMNNAMEDataGridViewTextBoxColumn1
            // 
            this.cOLUMNNAMEDataGridViewTextBoxColumn1.DataPropertyName = "COLUMN_NAME";
            this.cOLUMNNAMEDataGridViewTextBoxColumn1.HeaderText = "COLUMN_NAME";
            this.cOLUMNNAMEDataGridViewTextBoxColumn1.Name = "cOLUMNNAMEDataGridViewTextBoxColumn1";
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
            // Form_Validate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 582);
            this.Controls.Add(this.dg_File);
            this.Controls.Add(this.dg_SQL);
            this.Name = "Form_Validate";
            this.Text = "Form_Validate";
            this.Load += new System.EventHandler(this.Form_Validate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_SQL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_SQL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_SQL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_File)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_File)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_File)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_SQL;
        private System.Windows.Forms.DataGridView dg_File;
        private System.Windows.Forms.DataGridViewTextBoxColumn tABLENAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOLUMNDEFAULTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iSNULLABLEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATATYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUMERICPRECISIONDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUMERICSCALEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATETIMEPRECISIONDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOLUMNNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource bs_SQL;
        public DS_Information ds_SQL;
        private System.Windows.Forms.DataGridViewTextBoxColumn tABLENAMEDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOLUMNDEFAULTDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iSNULLABLEDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATATYPEDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cHARACTERMAXIMUMLENGTHDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUMERICPRECISIONDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUMERICSCALEDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATETIMEPRECISIONDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOLUMNNAMEDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource bs_File;
        public DS_Information ds_File;
    }
}