namespace Fask.Vyroba_P.Tisk
{
    partial class FormSablonaVyber
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
            this.tiskSablony = new Fask.SQLiteDBs.DataSets.TiskSablony();
            this.sablonyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nazevDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.souborDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMTYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.printerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButtons.SuspendLayout();
            this.panelComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tiskSablony)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sablonyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 345);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(405, 71);
            this.panelButtons.TabIndex = 1;
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(285, 71);
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
            this.buttonStorno.Size = new System.Drawing.Size(120, 71);
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
            this.panelComponents.Size = new System.Drawing.Size(405, 345);
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
            this.nazevDataGridViewTextBoxColumn,
            this.souborDataGridViewTextBoxColumn,
            this.iTEMTYPEDataGridViewTextBoxColumn,
            this.printerNameDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.sablonyBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(405, 345);
            this.dataGridView1.TabIndex = 0;
            // 
            // tiskSablony
            // 
            this.tiskSablony.DataSetName = "TiskSablony";
            this.tiskSablony.Locale = new System.Globalization.CultureInfo("");
            this.tiskSablony.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sablonyBindingSource
            // 
            this.sablonyBindingSource.DataMember = "Sablony";
            this.sablonyBindingSource.DataSource = this.tiskSablony;
            // 
            // nazevDataGridViewTextBoxColumn
            // 
            this.nazevDataGridViewTextBoxColumn.DataPropertyName = "Nazev";
            this.nazevDataGridViewTextBoxColumn.HeaderText = "Název";
            this.nazevDataGridViewTextBoxColumn.Name = "nazevDataGridViewTextBoxColumn";
            this.nazevDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // souborDataGridViewTextBoxColumn
            // 
            this.souborDataGridViewTextBoxColumn.DataPropertyName = "Soubor";
            this.souborDataGridViewTextBoxColumn.HeaderText = "Soubor";
            this.souborDataGridViewTextBoxColumn.Name = "souborDataGridViewTextBoxColumn";
            this.souborDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMTYPEDataGridViewTextBoxColumn
            // 
            this.iTEMTYPEDataGridViewTextBoxColumn.DataPropertyName = "ITEMTYPE";
            this.iTEMTYPEDataGridViewTextBoxColumn.HeaderText = "Typ položky";
            this.iTEMTYPEDataGridViewTextBoxColumn.Name = "iTEMTYPEDataGridViewTextBoxColumn";
            this.iTEMTYPEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // printerNameDataGridViewTextBoxColumn
            // 
            this.printerNameDataGridViewTextBoxColumn.DataPropertyName = "PrinterName";
            this.printerNameDataGridViewTextBoxColumn.HeaderText = "Tiskárna";
            this.printerNameDataGridViewTextBoxColumn.Name = "printerNameDataGridViewTextBoxColumn";
            this.printerNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FormSablonaVyber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(405, 416);
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.KeyPreview = true;
            this.Name = "FormSablonaVyber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výbìr šablony k tisku etikety";
            this.Load += new System.EventHandler(this.FormBaseButtonOKStorno_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormBaseButtonOKStorno_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelComponents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tiskSablony)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sablonyBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        public System.Windows.Forms.Panel panelComponents;
        public System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource sablonyBindingSource;
        private Fask.SQLiteDBs.DataSets.TiskSablony tiskSablony;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazevDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn souborDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMTYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn printerNameDataGridViewTextBoxColumn;

    }
}
