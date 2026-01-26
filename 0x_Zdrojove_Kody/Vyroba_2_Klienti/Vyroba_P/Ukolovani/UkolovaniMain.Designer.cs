namespace Fask.Vyroba_P.Ukolovani
{
    partial class UkolovaniMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_ServiceMan = new System.Windows.Forms.DataGridView();
            this.panelButton = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eMAILDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pHONEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_ServiceMan = new System.Windows.Forms.BindingSource(this.components);
            this.ds_ServiceMan = new Fask.Vyroba_P.UkolovaniService.Ukoly();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ServiceMan)).BeginInit();
            this.panelButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bs_ServiceMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_ServiceMan)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_ServiceMan
            // 
            this.dgv_ServiceMan.AllowUserToAddRows = false;
            this.dgv_ServiceMan.AllowUserToDeleteRows = false;
            this.dgv_ServiceMan.AllowUserToOrderColumns = true;
            this.dgv_ServiceMan.AutoGenerateColumns = false;
            this.dgv_ServiceMan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ServiceMan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ServiceMan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ServiceMan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.nAMEDataGridViewTextBoxColumn,
            this.eMAILDataGridViewTextBoxColumn,
            this.pHONEDataGridViewTextBoxColumn,
            this.dESCDataGridViewTextBoxColumn});
            this.dgv_ServiceMan.DataSource = this.bs_ServiceMan;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ServiceMan.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_ServiceMan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ServiceMan.Location = new System.Drawing.Point(0, 0);
            this.dgv_ServiceMan.MultiSelect = false;
            this.dgv_ServiceMan.Name = "dgv_ServiceMan";
            this.dgv_ServiceMan.ReadOnly = true;
            this.dgv_ServiceMan.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ServiceMan.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_ServiceMan.RowHeadersWidth = 70;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dgv_ServiceMan.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_ServiceMan.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dgv_ServiceMan.RowTemplate.Height = 70;
            this.dgv_ServiceMan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ServiceMan.Size = new System.Drawing.Size(727, 463);
            this.dgv_ServiceMan.TabIndex = 0;
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.buttonOK);
            this.panelButton.Controls.Add(this.buttonStorno);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 463);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(727, 71);
            this.panelButton.TabIndex = 1;
            this.panelButton.Resize += new System.EventHandler(this.panelButton_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(329, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(398, 71);
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
            this.buttonStorno.Size = new System.Drawing.Size(329, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.UseVisualStyleBackColor = true;
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 75;
            // 
            // nAMEDataGridViewTextBoxColumn
            // 
            this.nAMEDataGridViewTextBoxColumn.DataPropertyName = "NAME";
            this.nAMEDataGridViewTextBoxColumn.HeaderText = "Meno";
            this.nAMEDataGridViewTextBoxColumn.Name = "nAMEDataGridViewTextBoxColumn";
            this.nAMEDataGridViewTextBoxColumn.ReadOnly = true;
            this.nAMEDataGridViewTextBoxColumn.Width = 125;
            // 
            // eMAILDataGridViewTextBoxColumn
            // 
            this.eMAILDataGridViewTextBoxColumn.DataPropertyName = "EMAIL";
            this.eMAILDataGridViewTextBoxColumn.HeaderText = "Email";
            this.eMAILDataGridViewTextBoxColumn.Name = "eMAILDataGridViewTextBoxColumn";
            this.eMAILDataGridViewTextBoxColumn.ReadOnly = true;
            this.eMAILDataGridViewTextBoxColumn.Width = 127;
            // 
            // pHONEDataGridViewTextBoxColumn
            // 
            this.pHONEDataGridViewTextBoxColumn.DataPropertyName = "PHONE";
            this.pHONEDataGridViewTextBoxColumn.HeaderText = "Tel. číslo";
            this.pHONEDataGridViewTextBoxColumn.Name = "pHONEDataGridViewTextBoxColumn";
            this.pHONEDataGridViewTextBoxColumn.ReadOnly = true;
            this.pHONEDataGridViewTextBoxColumn.Width = 178;
            // 
            // dESCDataGridViewTextBoxColumn
            // 
            this.dESCDataGridViewTextBoxColumn.DataPropertyName = "DESC";
            this.dESCDataGridViewTextBoxColumn.HeaderText = "Popis";
            this.dESCDataGridViewTextBoxColumn.Name = "dESCDataGridViewTextBoxColumn";
            this.dESCDataGridViewTextBoxColumn.ReadOnly = true;
            this.dESCDataGridViewTextBoxColumn.Width = 127;
            // 
            // bs_ServiceMan
            // 
            this.bs_ServiceMan.DataMember = "CZ_UKOL_ServiceMan";
            this.bs_ServiceMan.DataSource = this.ds_ServiceMan;
            // 
            // ds_ServiceMan
            // 
            this.ds_ServiceMan.DataSetName = "Ukolovani";
            this.ds_ServiceMan.Locale = new System.Globalization.CultureInfo("");
            this.ds_ServiceMan.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // UkolovaniMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 534);
            this.Controls.Add(this.dgv_ServiceMan);
            this.Controls.Add(this.panelButton);
            this.Name = "UkolovaniMain";
            this.Text = "UkolovaniMain";
            this.Load += new System.EventHandler(this.UkolovaniMain_Load);
            this.Shown += new System.EventHandler(this.UkolovaniMain_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UkolovaniMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ServiceMan)).EndInit();
            this.panelButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bs_ServiceMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_ServiceMan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ServiceMan;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.BindingSource bs_ServiceMan;
        private Fask.Vyroba_P.UkolovaniService.Ukoly ds_ServiceMan;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eMAILDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pHONEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dESCDataGridViewTextBoxColumn;
    }
}