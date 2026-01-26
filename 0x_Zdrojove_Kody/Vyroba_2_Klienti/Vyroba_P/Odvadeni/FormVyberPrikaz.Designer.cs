namespace Fask.Vyroba_P.Odvadeni
{
    partial class FormVyberPrikaz
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonZmenaStavu = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonTisk = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.cZPROVPHBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vyrobaCEDataSet = new Fask.SQLiteDBs.DataSets.Vyroba();
            this.ucDetail1 = new Fask.Vyroba_P.Controls.ucDetail();
            this.countEntriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPTYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPNUMBEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDDOCNMHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodeHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOCNCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateProdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rez1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rez2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.termIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stav = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lSTModDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEXROWIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelComponents.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cZPROVPHBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaCEDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(602, 71);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.splitContainer1);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(722, 509);
            this.panelComponents.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucDetail1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(722, 509);
            this.splitContainer1.SplitterDistance = 490;
            this.splitContainer1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonZmenaStavu,
            this.toolStripButtonTisk});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(490, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonZmenaStavu
            // 
            this.toolStripButtonZmenaStavu.Image = global::Fask.Vyroba_P.Properties.Resources.ProtectForm;
            this.toolStripButtonZmenaStavu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZmenaStavu.Name = "toolStripButtonZmenaStavu";
            this.toolStripButtonZmenaStavu.Size = new System.Drawing.Size(89, 22);
            this.toolStripButtonZmenaStavu.Text = "Zmìna stavu";
            this.toolStripButtonZmenaStavu.ToolTipText = "Zmìna stavu výrobního pøíkazu (F2)";
            this.toolStripButtonZmenaStavu.Click += new System.EventHandler(this.toolStripButtonZmenaStavu_Click);
            // 
            // toolStripButtonTisk
            // 
            this.toolStripButtonTisk.Image = global::Fask.Vyroba_P.Properties.Resources.Print;
            this.toolStripButtonTisk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTisk.Name = "toolStripButtonTisk";
            this.toolStripButtonTisk.Size = new System.Drawing.Size(45, 22);
            this.toolStripButtonTisk.Text = "Tisk";
            this.toolStripButtonTisk.ToolTipText = "Tisk etikety výrobního pøíkazu (F3)";
            this.toolStripButtonTisk.Click += new System.EventHandler(this.toolStripButtonTisk_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.countEntriesDataGridViewTextBoxColumn,
            this.sOPTYPEDataGridViewTextBoxColumn,
            this.sOPNUMBEDataGridViewTextBoxColumn,
            this.sOPDESCDataGridViewTextBoxColumn,
            this.vNDDOCNMHDataGridViewTextBoxColumn,
            this.barcodeHDataGridViewTextBoxColumn,
            this.lOCNCODEDataGridViewTextBoxColumn,
            this.dateProdDataGridViewTextBoxColumn,
            this.rez1DataGridViewTextBoxColumn,
            this.rez2DataGridViewTextBoxColumn,
            this.termIDDataGridViewTextBoxColumn,
            this.Stav,
            this.lSTModDataGridViewTextBoxColumn,
            this.dEXROWIDDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.cZPROVPHBindingSource;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.Format = "g";
            dataGridViewCellStyle2.NullValue = null;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(490, 484);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pøíkaz";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 509);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(722, 71);
            this.panelButtons.TabIndex = 1;
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // cZPROVPHBindingSource
            // 
            this.cZPROVPHBindingSource.DataMember = "CZPRO_VPH";
            this.cZPROVPHBindingSource.DataSource = this.vyrobaCEDataSet;
            this.cZPROVPHBindingSource.Filter = "";
            this.cZPROVPHBindingSource.Sort = "";
            // 
            // vyrobaCEDataSet
            // 
            this.vyrobaCEDataSet.DataSetName = "VyrobaCEDataSet";
            this.vyrobaCEDataSet.EnforceConstraints = false;
            this.vyrobaCEDataSet.Locale = new System.Globalization.CultureInfo("");
            this.vyrobaCEDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.ExcludeSchema;
            // 
            // ucDetail1
            // 
            this.ucDetail1.DetialObject = null;
            this.ucDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDetail1.Location = new System.Drawing.Point(0, 21);
            this.ucDetail1.Name = "ucDetail1";
            this.ucDetail1.Size = new System.Drawing.Size(228, 488);
            this.ucDetail1.TabIndex = 1;
            // 
            // countEntriesDataGridViewTextBoxColumn
            // 
            this.countEntriesDataGridViewTextBoxColumn.DataPropertyName = "CountEntries";
            this.countEntriesDataGridViewTextBoxColumn.HeaderText = "Dávka";
            this.countEntriesDataGridViewTextBoxColumn.Name = "countEntriesDataGridViewTextBoxColumn";
            this.countEntriesDataGridViewTextBoxColumn.ReadOnly = true;
            this.countEntriesDataGridViewTextBoxColumn.Visible = false;
            // 
            // sOPTYPEDataGridViewTextBoxColumn
            // 
            this.sOPTYPEDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sOPTYPEDataGridViewTextBoxColumn.DataPropertyName = "SOPTYPE";
            this.sOPTYPEDataGridViewTextBoxColumn.HeaderText = "Typ";
            this.sOPTYPEDataGridViewTextBoxColumn.Name = "sOPTYPEDataGridViewTextBoxColumn";
            this.sOPTYPEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sOPTYPEDataGridViewTextBoxColumn.Width = 59;
            // 
            // sOPNUMBEDataGridViewTextBoxColumn
            // 
            this.sOPNUMBEDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sOPNUMBEDataGridViewTextBoxColumn.DataPropertyName = "SOPNUMBE";
            this.sOPNUMBEDataGridViewTextBoxColumn.HeaderText = "Pøíkaz è.";
            this.sOPNUMBEDataGridViewTextBoxColumn.Name = "sOPNUMBEDataGridViewTextBoxColumn";
            this.sOPNUMBEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sOPNUMBEDataGridViewTextBoxColumn.Width = 93;
            // 
            // sOPDESCDataGridViewTextBoxColumn
            // 
            this.sOPDESCDataGridViewTextBoxColumn.DataPropertyName = "SOPDESC";
            this.sOPDESCDataGridViewTextBoxColumn.HeaderText = "Popis";
            this.sOPDESCDataGridViewTextBoxColumn.Name = "sOPDESCDataGridViewTextBoxColumn";
            this.sOPDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vNDDOCNMHDataGridViewTextBoxColumn
            // 
            this.vNDDOCNMHDataGridViewTextBoxColumn.DataPropertyName = "VNDDOCNMH";
            this.vNDDOCNMHDataGridViewTextBoxColumn.HeaderText = "Dod. è.";
            this.vNDDOCNMHDataGridViewTextBoxColumn.Name = "vNDDOCNMHDataGridViewTextBoxColumn";
            this.vNDDOCNMHDataGridViewTextBoxColumn.ReadOnly = true;
            this.vNDDOCNMHDataGridViewTextBoxColumn.Visible = false;
            // 
            // barcodeHDataGridViewTextBoxColumn
            // 
            this.barcodeHDataGridViewTextBoxColumn.DataPropertyName = "BarcodeH";
            this.barcodeHDataGridViewTextBoxColumn.HeaderText = "È.k.";
            this.barcodeHDataGridViewTextBoxColumn.Name = "barcodeHDataGridViewTextBoxColumn";
            this.barcodeHDataGridViewTextBoxColumn.ReadOnly = true;
            this.barcodeHDataGridViewTextBoxColumn.Visible = false;
            // 
            // lOCNCODEDataGridViewTextBoxColumn
            // 
            this.lOCNCODEDataGridViewTextBoxColumn.DataPropertyName = "LOCNCODE";
            this.lOCNCODEDataGridViewTextBoxColumn.HeaderText = "Lokace";
            this.lOCNCODEDataGridViewTextBoxColumn.Name = "lOCNCODEDataGridViewTextBoxColumn";
            this.lOCNCODEDataGridViewTextBoxColumn.ReadOnly = true;
            this.lOCNCODEDataGridViewTextBoxColumn.Visible = false;
            // 
            // dateProdDataGridViewTextBoxColumn
            // 
            this.dateProdDataGridViewTextBoxColumn.DataPropertyName = "DateProd";
            this.dateProdDataGridViewTextBoxColumn.HeaderText = "Datum";
            this.dateProdDataGridViewTextBoxColumn.Name = "dateProdDataGridViewTextBoxColumn";
            this.dateProdDataGridViewTextBoxColumn.ReadOnly = true;
            this.dateProdDataGridViewTextBoxColumn.Visible = false;
            // 
            // rez1DataGridViewTextBoxColumn
            // 
            this.rez1DataGridViewTextBoxColumn.DataPropertyName = "Rez1";
            this.rez1DataGridViewTextBoxColumn.HeaderText = "Rez1";
            this.rez1DataGridViewTextBoxColumn.Name = "rez1DataGridViewTextBoxColumn";
            this.rez1DataGridViewTextBoxColumn.ReadOnly = true;
            this.rez1DataGridViewTextBoxColumn.Visible = false;
            // 
            // rez2DataGridViewTextBoxColumn
            // 
            this.rez2DataGridViewTextBoxColumn.DataPropertyName = "Rez2";
            this.rez2DataGridViewTextBoxColumn.HeaderText = "Rez2";
            this.rez2DataGridViewTextBoxColumn.Name = "rez2DataGridViewTextBoxColumn";
            this.rez2DataGridViewTextBoxColumn.ReadOnly = true;
            this.rez2DataGridViewTextBoxColumn.Visible = false;
            // 
            // termIDDataGridViewTextBoxColumn
            // 
            this.termIDDataGridViewTextBoxColumn.DataPropertyName = "TermID";
            this.termIDDataGridViewTextBoxColumn.HeaderText = "Terminál ID";
            this.termIDDataGridViewTextBoxColumn.Name = "termIDDataGridViewTextBoxColumn";
            this.termIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.termIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // Stav
            // 
            this.Stav.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Stav.DataPropertyName = "Stav";
            this.Stav.HeaderText = "Stav";
            this.Stav.Name = "Stav";
            this.Stav.ReadOnly = true;
            this.Stav.Width = 66;
            // 
            // lSTModDataGridViewTextBoxColumn
            // 
            this.lSTModDataGridViewTextBoxColumn.DataPropertyName = "LSTMod";
            this.lSTModDataGridViewTextBoxColumn.HeaderText = "Modifikováno";
            this.lSTModDataGridViewTextBoxColumn.Name = "lSTModDataGridViewTextBoxColumn";
            this.lSTModDataGridViewTextBoxColumn.ReadOnly = true;
            this.lSTModDataGridViewTextBoxColumn.Visible = false;
            // 
            // dEXROWIDDataGridViewTextBoxColumn
            // 
            this.dEXROWIDDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn.HeaderText = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn.Name = "dEXROWIDDataGridViewTextBoxColumn";
            this.dEXROWIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.dEXROWIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // FormVyberPrikaz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(722, 580);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.KeyPreview = true;
            this.Name = "FormVyberPrikaz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výbìr výrobního pøíkazu";
            this.Load += new System.EventHandler(this.FormPrikazVyber_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormInputKod_KeyDown);
            this.panelComponents.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cZPROVPHBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaCEDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Panel panelComponents;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Fask.SQLiteDBs.DataSets.Vyroba vyrobaCEDataSet;
        private System.Windows.Forms.BindingSource cZPROVPHBindingSource;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Fask.Vyroba_P.Controls.ucDetail ucDetail1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonZmenaStavu;
        private System.Windows.Forms.ToolStripButton toolStripButtonTisk;
        private System.Windows.Forms.DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPTYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPNUMBEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDDOCNMHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodeHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOCNCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateProdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rez1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rez2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn termIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stav;
        private System.Windows.Forms.DataGridViewTextBoxColumn lSTModDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDDataGridViewTextBoxColumn;
    }
}
