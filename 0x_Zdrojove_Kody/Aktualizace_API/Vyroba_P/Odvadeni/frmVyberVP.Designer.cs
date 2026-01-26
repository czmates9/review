
namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku
{
    partial class frmVyberVP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bt_storno = new System.Windows.Forms.Button();
            this.bt_ok = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_nacteniVP = new System.Windows.Forms.Button();
            this.bs_archivace = new System.Windows.Forms.BindingSource(this.components);
            this.bw_archivace = new System.ComponentModel.BackgroundWorker();
            this.bw_deaktivace = new System.ComponentModel.BackgroundWorker();
            this.l_text_top = new System.Windows.Forms.Label();
            this.dg_vyberVP = new System.Windows.Forms.DataGridView();
            this.countEntriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_vyberVP = new System.Windows.Forms.BindingSource(this.components);
            this.vyroba = new Fask.SQLiteDBs.DataSets.Vyroba();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bs_archivace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_vyberVP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_vyberVP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyroba)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_storno
            // 
            this.bt_storno.Dock = System.Windows.Forms.DockStyle.Left;
            this.bt_storno.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bt_storno.Location = new System.Drawing.Point(0, 0);
            this.bt_storno.Name = "bt_storno";
            this.bt_storno.Size = new System.Drawing.Size(213, 126);
            this.bt_storno.TabIndex = 0;
            this.bt_storno.Text = "STORNO";
            this.bt_storno.UseVisualStyleBackColor = true;
            this.bt_storno.Click += new System.EventHandler(this.bt_storno_Click);
            // 
            // bt_ok
            // 
            this.bt_ok.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bt_ok.Location = new System.Drawing.Point(529, 0);
            this.bt_ok.Name = "bt_ok";
            this.bt_ok.Size = new System.Drawing.Size(271, 126);
            this.bt_ok.TabIndex = 2;
            this.bt_ok.Text = "OK";
            this.bt_ok.UseVisualStyleBackColor = true;
            this.bt_ok.Click += new System.EventHandler(this.bt_ok_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_nacteniVP);
            this.panel1.Controls.Add(this.bt_storno);
            this.panel1.Controls.Add(this.bt_ok);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 324);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 126);
            this.panel1.TabIndex = 3;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // bt_nacteniVP
            // 
            this.bt_nacteniVP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bt_nacteniVP.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bt_nacteniVP.Location = new System.Drawing.Point(213, 0);
            this.bt_nacteniVP.Name = "bt_nacteniVP";
            this.bt_nacteniVP.Size = new System.Drawing.Size(316, 126);
            this.bt_nacteniVP.TabIndex = 3;
            this.bt_nacteniVP.Text = "AKTUALIZOVAT";
            this.bt_nacteniVP.UseVisualStyleBackColor = true;
            this.bt_nacteniVP.Click += new System.EventHandler(this.bt_nacteniVP_Click);
            // 
            // bs_archivace
            // 
            this.bs_archivace.DataMember = "FASK_Events_archivace";
            // 
            // bw_archivace
            // 
            this.bw_archivace.WorkerSupportsCancellation = true;
            // 
            // bw_deaktivace
            // 
            this.bw_deaktivace.WorkerSupportsCancellation = true;
            // 
            // l_text_top
            // 
            this.l_text_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.l_text_top.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.l_text_top.Location = new System.Drawing.Point(0, 0);
            this.l_text_top.Name = "l_text_top";
            this.l_text_top.Size = new System.Drawing.Size(800, 47);
            this.l_text_top.TabIndex = 40;
            this.l_text_top.Text = "Výběr VP";
            this.l_text_top.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dg_vyberVP
            // 
            this.dg_vyberVP.AllowUserToAddRows = false;
            this.dg_vyberVP.AllowUserToDeleteRows = false;
            this.dg_vyberVP.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dg_vyberVP.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_vyberVP.AutoGenerateColumns = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_vyberVP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dg_vyberVP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_vyberVP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.countEntriesDataGridViewTextBoxColumn,
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMDESCDataGridViewTextBoxColumn});
            this.dg_vyberVP.DataSource = this.bs_vyberVP;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_vyberVP.DefaultCellStyle = dataGridViewCellStyle3;
            this.dg_vyberVP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_vyberVP.EnableHeadersVisualStyles = false;
            this.dg_vyberVP.Location = new System.Drawing.Point(0, 47);
            this.dg_vyberVP.MultiSelect = false;
            this.dg_vyberVP.Name = "dg_vyberVP";
            this.dg_vyberVP.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_vyberVP.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dg_vyberVP.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dg_vyberVP.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dg_vyberVP.RowTemplate.Height = 65;
            this.dg_vyberVP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_vyberVP.Size = new System.Drawing.Size(800, 277);
            this.dg_vyberVP.TabIndex = 41;
            this.dg_vyberVP.TabStop = false;
            // 
            // countEntriesDataGridViewTextBoxColumn
            // 
            this.countEntriesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.countEntriesDataGridViewTextBoxColumn.DataPropertyName = "CountEntries";
            this.countEntriesDataGridViewTextBoxColumn.HeaderText = "VP";
            this.countEntriesDataGridViewTextBoxColumn.MinimumWidth = 20;
            this.countEntriesDataGridViewTextBoxColumn.Name = "countEntriesDataGridViewTextBoxColumn";
            this.countEntriesDataGridViewTextBoxColumn.ReadOnly = true;
            this.countEntriesDataGridViewTextBoxColumn.Width = 67;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "Kód zboží";
            this.iTEMNMBRDataGridViewTextBoxColumn.MinimumWidth = 20;
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Název zboží";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bs_vyberVP
            // 
            this.bs_vyberVP.DataMember = "CZPRO_VPP";
            this.bs_vyberVP.DataSource = this.vyroba;
            // 
            // vyroba
            // 
            this.vyroba.DataSetName = "Vyroba";
            this.vyroba.EnforceConstraints = false;
            this.vyroba.Locale = new System.Globalization.CultureInfo("");
            this.vyroba.SchemaSerializationMode = System.Data.SchemaSerializationMode.ExcludeSchema;
            // 
            // frmVyberVP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dg_vyberVP);
            this.Controls.Add(this.l_text_top);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmVyberVP";
            this.Text = "frmArchivaceZaznamu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVyberVP_FormClosing);
            this.Load += new System.EventHandler(this.frmVyberVP_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bs_archivace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_vyberVP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_vyberVP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyroba)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_storno;
        private System.Windows.Forms.Button bt_ok;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource bs_archivace;
        private System.ComponentModel.BackgroundWorker bw_archivace;
       // private ProgressControls.ProgressIndicator progressIndicator1;
        private System.ComponentModel.BackgroundWorker bw_deaktivace;
        private System.Windows.Forms.Label l_text_top;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource bs_vyberVP;
        private System.Windows.Forms.DataGridView dg_vyberVP;
        private Fask.SQLiteDBs.DataSets.Vyroba vyroba;
        private System.Windows.Forms.Button bt_nacteniVP;
        private System.Windows.Forms.DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
    }
}