
namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Kapalky_SV.Forms
{
    partial class frmArchivaceZaznamu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bt_vyhledat = new System.Windows.Forms.Button();
            this.bt_deaktivace = new System.Windows.Forms.Button();
            this.bt_zpet = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dg_archivace = new System.Windows.Forms.DataGridView();
            this.bs_archivace = new System.Windows.Forms.BindingSource(this.components);
            this.ds_archivace = new ICommDatabase.DSVyroba();
            this.bw_archivace = new System.ComponentModel.BackgroundWorker();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.bw_deaktivace = new System.ComponentModel.BackgroundWorker();
            this.l_text_top = new System.Windows.Forms.Label();
            this.dateeve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NMBRPAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_archivace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_archivace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_archivace)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_vyhledat
            // 
            this.bt_vyhledat.Dock = System.Windows.Forms.DockStyle.Left;
            this.bt_vyhledat.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bt_vyhledat.Location = new System.Drawing.Point(0, 0);
            this.bt_vyhledat.Name = "bt_vyhledat";
            this.bt_vyhledat.Size = new System.Drawing.Size(213, 126);
            this.bt_vyhledat.TabIndex = 0;
            this.bt_vyhledat.Text = "Vyhledat";
            this.bt_vyhledat.UseVisualStyleBackColor = true;
            this.bt_vyhledat.Click += new System.EventHandler(this.bt_vyhledat_Click);
            // 
            // bt_deaktivace
            // 
            this.bt_deaktivace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bt_deaktivace.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bt_deaktivace.Location = new System.Drawing.Point(213, 0);
            this.bt_deaktivace.Name = "bt_deaktivace";
            this.bt_deaktivace.Size = new System.Drawing.Size(316, 126);
            this.bt_deaktivace.TabIndex = 1;
            this.bt_deaktivace.Text = "Deaktivovat";
            this.bt_deaktivace.UseVisualStyleBackColor = true;
            this.bt_deaktivace.Click += new System.EventHandler(this.bt_deaktivace_Click);
            // 
            // bt_zpet
            // 
            this.bt_zpet.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_zpet.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bt_zpet.Location = new System.Drawing.Point(529, 0);
            this.bt_zpet.Name = "bt_zpet";
            this.bt_zpet.Size = new System.Drawing.Size(271, 126);
            this.bt_zpet.TabIndex = 2;
            this.bt_zpet.Text = "Zpět";
            this.bt_zpet.UseVisualStyleBackColor = true;
            this.bt_zpet.Click += new System.EventHandler(this.bt_zpet_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_deaktivace);
            this.panel1.Controls.Add(this.bt_vyhledat);
            this.panel1.Controls.Add(this.bt_zpet);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 324);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 126);
            this.panel1.TabIndex = 3;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // dg_archivace
            // 
            this.dg_archivace.AllowUserToAddRows = false;
            this.dg_archivace.AllowUserToDeleteRows = false;
            this.dg_archivace.AllowUserToOrderColumns = true;
            this.dg_archivace.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dg_archivace.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_archivace.AutoGenerateColumns = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_archivace.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dg_archivace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_archivace.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateeve,
            this.machineid,
            this.ITEMDESC,
            this.NMBRPAL,
            this.PackType,
            this.status,
            this.descriptionDataGridViewTextBoxColumn});
            this.dg_archivace.DataSource = this.bs_archivace;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_archivace.DefaultCellStyle = dataGridViewCellStyle4;
            this.dg_archivace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_archivace.Location = new System.Drawing.Point(0, 47);
            this.dg_archivace.MultiSelect = false;
            this.dg_archivace.Name = "dg_archivace";
            this.dg_archivace.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_archivace.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dg_archivace.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dg_archivace.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dg_archivace.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dg_archivace.RowTemplate.ReadOnly = true;
            this.dg_archivace.Size = new System.Drawing.Size(800, 277);
            this.dg_archivace.TabIndex = 4;
            // 
            // bs_archivace
            // 
            this.bs_archivace.DataMember = "FASK_Events_archivace";
            this.bs_archivace.DataSource = this.ds_archivace;
            // 
            // ds_archivace
            // 
            this.ds_archivace.DataSetName = "DSVyroba";
            this.ds_archivace.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bw_archivace
            // 
            this.bw_archivace.WorkerSupportsCancellation = true;
            this.bw_archivace.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadFE_DoWork);
            this.bw_archivace.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadFE_RunWorkerCompleted);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(355, 180);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 39;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // bw_deaktivace
            // 
            this.bw_deaktivace.WorkerSupportsCancellation = true;
            this.bw_deaktivace.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDeaktivaceFE_DoWork);
            this.bw_deaktivace.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwDeaktivaceFE_RunWorkerCompleted);
            // 
            // l_text_top
            // 
            this.l_text_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.l_text_top.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.l_text_top.Location = new System.Drawing.Point(0, 0);
            this.l_text_top.Name = "l_text_top";
            this.l_text_top.Size = new System.Drawing.Size(800, 47);
            this.l_text_top.TabIndex = 40;
            this.l_text_top.Text = "Linka 1";
            this.l_text_top.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dateeve
            // 
            this.dateeve.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dateeve.DataPropertyName = "dateeve";
            dataGridViewCellStyle3.Format = "G";
            dataGridViewCellStyle3.NullValue = null;
            this.dateeve.DefaultCellStyle = dataGridViewCellStyle3;
            this.dateeve.HeaderText = "čas";
            this.dateeve.Name = "dateeve";
            this.dateeve.ReadOnly = true;
            this.dateeve.Width = 74;
            // 
            // machineid
            // 
            this.machineid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.machineid.DataPropertyName = "machineid";
            this.machineid.HeaderText = "linka";
            this.machineid.Name = "machineid";
            this.machineid.ReadOnly = true;
            this.machineid.Width = 80;
            // 
            // ITEMDESC
            // 
            this.ITEMDESC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ITEMDESC.DataPropertyName = "ITEMDESC";
            this.ITEMDESC.HeaderText = "produkt";
            this.ITEMDESC.Name = "ITEMDESC";
            this.ITEMDESC.ReadOnly = true;
            this.ITEMDESC.Width = 116;
            // 
            // NMBRPAL
            // 
            this.NMBRPAL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.NMBRPAL.DataPropertyName = "NMBRPAL";
            this.NMBRPAL.HeaderText = "SSCC";
            this.NMBRPAL.Name = "NMBRPAL";
            this.NMBRPAL.ReadOnly = true;
            this.NMBRPAL.Width = 99;
            // 
            // PackType
            // 
            this.PackType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PackType.DataPropertyName = "PackType";
            this.PackType.HeaderText = "typ pal";
            this.PackType.Name = "PackType";
            this.PackType.ReadOnly = true;
            this.PackType.Width = 55;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 80;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "popis";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // frmArchivaceZaznamu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dg_archivace);
            this.Controls.Add(this.l_text_top);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmArchivaceZaznamu";
            this.Text = "frmArchivaceZaznamu";
            this.Load += new System.EventHandler(this.frmArchivaceZaznamu_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_archivace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_archivace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_archivace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_vyhledat;
        private System.Windows.Forms.Button bt_deaktivace;
        private System.Windows.Forms.Button bt_zpet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource bs_archivace;
        private ICommDatabase.DSVyroba ds_archivace;
        private System.Windows.Forms.DataGridView dg_archivace;
        private System.ComponentModel.BackgroundWorker bw_archivace;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private System.ComponentModel.BackgroundWorker bw_deaktivace;
        private System.Windows.Forms.Label l_text_top;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateeve;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn NMBRPAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackType;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
    }
}