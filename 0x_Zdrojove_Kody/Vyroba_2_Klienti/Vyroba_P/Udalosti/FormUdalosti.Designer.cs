namespace Fask.Vyroba_P.Udalosti
{
    partial class FormUdalosti
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
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusdescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusTypesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vyrobaCEDataSet = new Fask.SQLiteDBs.DataSets.Vyroba();
            this.panelButtons.SuspendLayout();
            this.panelComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusTypesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaCEDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 395);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(388, 71);
            this.panelButtons.TabIndex = 1;
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(174, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(214, 71);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(174, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.dataGridView1);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(388, 395);
            this.panelComponents.TabIndex = 0;
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
            this.statusidDataGridViewTextBoxColumn,
            this.statusdescDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.statusTypesBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 40;
            this.dataGridView1.Size = new System.Drawing.Size(388, 395);
            this.dataGridView1.TabIndex = 0;
            // 
            // statusidDataGridViewTextBoxColumn
            // 
            this.statusidDataGridViewTextBoxColumn.DataPropertyName = "statusid";
            this.statusidDataGridViewTextBoxColumn.HeaderText = "statusid";
            this.statusidDataGridViewTextBoxColumn.Name = "statusidDataGridViewTextBoxColumn";
            this.statusidDataGridViewTextBoxColumn.ReadOnly = true;
            this.statusidDataGridViewTextBoxColumn.Visible = false;
            // 
            // statusdescDataGridViewTextBoxColumn
            // 
            this.statusdescDataGridViewTextBoxColumn.DataPropertyName = "statusdesc";
            this.statusdescDataGridViewTextBoxColumn.HeaderText = "Událost";
            this.statusdescDataGridViewTextBoxColumn.Name = "statusdescDataGridViewTextBoxColumn";
            this.statusdescDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusTypesBindingSource
            // 
            this.statusTypesBindingSource.DataMember = "StatusTypes";
            this.statusTypesBindingSource.DataSource = this.vyrobaCEDataSet;
            // 
            // vyrobaCEDataSet
            // 
            this.vyrobaCEDataSet.DataSetName = "VyrobaCEDataSet";
            this.vyrobaCEDataSet.EnforceConstraints = false;
            this.vyrobaCEDataSet.Locale = new System.Globalization.CultureInfo("");
            this.vyrobaCEDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.ExcludeSchema;
            // 
            // FormUdalosti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(388, 466);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.KeyPreview = true;
            this.Name = "FormUdalosti";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Události obsluhy";
            this.Load += new System.EventHandler(this.FormUdalosti_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUdalosti_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelComponents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusTypesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaCEDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        public System.Windows.Forms.Panel panelComponents;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Fask.SQLiteDBs.DataSets.Vyroba vyrobaCEDataSet;
        private System.Windows.Forms.BindingSource statusTypesBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusdescDataGridViewTextBoxColumn;

    }
}
