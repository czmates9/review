namespace Konzola.Terminal
{
    partial class FormTerminalSeznam
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
            this.PanelButton = new System.Windows.Forms.Panel();
            this.btn_konf_andr = new System.Windows.Forms.Button();
            this.btn_VNC = new System.Windows.Forms.Button();
            this.btn_aktualizovat = new System.Windows.Forms.Button();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.progressIndicatorUpdate = new ProgressControls.ProgressIndicator();
            this.dg_Terminaly = new Zuby.ADGV.AdvancedDataGridView();
            this.iDTERMINALDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATEREQDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dBTYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_Terminaly = new System.Windows.Forms.BindingSource(this.components);
            this.dsTerminaly = new Fask.Interfaces.DataSets.Terminal();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.bw_Load = new System.ComponentModel.BackgroundWorker();
            this.btn_konf_validace = new System.Windows.Forms.Button();
            this.PanelButton.SuspendLayout();
            this.PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Terminaly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Terminaly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTerminaly)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelButton
            // 
            this.PanelButton.Controls.Add(this.btn_konf_validace);
            this.PanelButton.Controls.Add(this.btn_konf_andr);
            this.PanelButton.Controls.Add(this.btn_VNC);
            this.PanelButton.Controls.Add(this.btn_aktualizovat);
            this.PanelButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.PanelButton.Location = new System.Drawing.Point(845, 0);
            this.PanelButton.Name = "PanelButton";
            this.PanelButton.Size = new System.Drawing.Size(84, 592);
            this.PanelButton.TabIndex = 0;
            // 
            // btn_konf_andr
            // 
            this.btn_konf_andr.Location = new System.Drawing.Point(4, 82);
            this.btn_konf_andr.Name = "btn_konf_andr";
            this.btn_konf_andr.Size = new System.Drawing.Size(75, 64);
            this.btn_konf_andr.TabIndex = 1;
            this.btn_konf_andr.Text = "konfigurace android";
            this.btn_konf_andr.UseVisualStyleBackColor = true;
            this.btn_konf_andr.Click += new System.EventHandler(this.btn_konf_andr_Click);
            // 
            // btn_VNC
            // 
            this.btn_VNC.Location = new System.Drawing.Point(4, 12);
            this.btn_VNC.Name = "btn_VNC";
            this.btn_VNC.Size = new System.Drawing.Size(75, 64);
            this.btn_VNC.TabIndex = 1;
            this.btn_VNC.Text = "VNC";
            this.btn_VNC.UseVisualStyleBackColor = true;
            this.btn_VNC.Click += new System.EventHandler(this.btn_VNC_Click);
            // 
            // btn_aktualizovat
            // 
            this.btn_aktualizovat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_aktualizovat.Location = new System.Drawing.Point(6, 517);
            this.btn_aktualizovat.Name = "btn_aktualizovat";
            this.btn_aktualizovat.Size = new System.Drawing.Size(73, 63);
            this.btn_aktualizovat.TabIndex = 0;
            this.btn_aktualizovat.Text = "Aktualizovat";
            this.btn_aktualizovat.UseVisualStyleBackColor = true;
            this.btn_aktualizovat.Click += new System.EventHandler(this.btn_aktualizovat_Click);
            // 
            // PanelMain
            // 
            this.PanelMain.Controls.Add(this.progressIndicatorUpdate);
            this.PanelMain.Controls.Add(this.dg_Terminaly);
            this.PanelMain.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(845, 592);
            this.PanelMain.TabIndex = 1;
            // 
            // progressIndicatorUpdate
            // 
            this.progressIndicatorUpdate.Location = new System.Drawing.Point(411, 261);
            this.progressIndicatorUpdate.Name = "progressIndicatorUpdate";
            this.progressIndicatorUpdate.Percentage = 0F;
            this.progressIndicatorUpdate.Size = new System.Drawing.Size(98, 98);
            this.progressIndicatorUpdate.TabIndex = 106;
            this.progressIndicatorUpdate.Text = "progressIndicator1";
            this.progressIndicatorUpdate.Visible = false;
            // 
            // dg_Terminaly
            // 
            this.dg_Terminaly.AllowUserToAddRows = false;
            this.dg_Terminaly.AllowUserToDeleteRows = false;
            this.dg_Terminaly.AllowUserToOrderColumns = true;
            this.dg_Terminaly.AllowUserToResizeRows = false;
            this.dg_Terminaly.AutoGenerateColumns = false;
            this.dg_Terminaly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Terminaly.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDTERMINALDataGridViewTextBoxColumn,
            this.iPDataGridViewTextBoxColumn,
            this.dATEREQDataGridViewTextBoxColumn,
            this.dBTYPEDataGridViewTextBoxColumn});
            this.dg_Terminaly.DataSource = this.bs_Terminaly;
            this.dg_Terminaly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Terminaly.FilterAndSortEnabled = true;
            this.dg_Terminaly.Location = new System.Drawing.Point(0, 27);
            this.dg_Terminaly.Name = "dg_Terminaly";
            this.dg_Terminaly.ReadOnly = true;
            this.dg_Terminaly.RowHeadersVisible = false;
            this.dg_Terminaly.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Terminaly.Size = new System.Drawing.Size(845, 565);
            this.dg_Terminaly.TabIndex = 0;
            // 
            // iDTERMINALDataGridViewTextBoxColumn
            // 
            this.iDTERMINALDataGridViewTextBoxColumn.DataPropertyName = "ID_TERMINAL";
            this.iDTERMINALDataGridViewTextBoxColumn.HeaderText = "ID Terminál";
            this.iDTERMINALDataGridViewTextBoxColumn.Name = "iDTERMINALDataGridViewTextBoxColumn";
            this.iDTERMINALDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iPDataGridViewTextBoxColumn
            // 
            this.iPDataGridViewTextBoxColumn.DataPropertyName = "IP";
            this.iPDataGridViewTextBoxColumn.HeaderText = "IP adresa";
            this.iPDataGridViewTextBoxColumn.Name = "iPDataGridViewTextBoxColumn";
            this.iPDataGridViewTextBoxColumn.ReadOnly = true;
            this.iPDataGridViewTextBoxColumn.Width = 150;
            // 
            // dATEREQDataGridViewTextBoxColumn
            // 
            this.dATEREQDataGridViewTextBoxColumn.DataPropertyName = "DATEREQ";
            this.dATEREQDataGridViewTextBoxColumn.HeaderText = "Dátum aktualizace";
            this.dATEREQDataGridViewTextBoxColumn.Name = "dATEREQDataGridViewTextBoxColumn";
            this.dATEREQDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dBTYPEDataGridViewTextBoxColumn
            // 
            this.dBTYPEDataGridViewTextBoxColumn.DataPropertyName = "DB_TYPE";
            this.dBTYPEDataGridViewTextBoxColumn.HeaderText = "Typ terminálu";
            this.dBTYPEDataGridViewTextBoxColumn.Name = "dBTYPEDataGridViewTextBoxColumn";
            this.dBTYPEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bs_Terminaly
            // 
            this.bs_Terminaly.DataMember = "CZMST_TERMINAL_ALL";
            this.bs_Terminaly.DataSource = this.dsTerminaly;
            // 
            // dsTerminaly
            // 
            this.dsTerminaly.DataSetName = "Terminal";
            this.dsTerminaly.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 0);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(845, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 107;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // bw_Load
            // 
            this.bw_Load.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Load_DoWork);
            this.bw_Load.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Load_RunWorkerCompleted);
            // 
            // btn_konf_validace
            // 
            this.btn_konf_validace.Location = new System.Drawing.Point(4, 152);
            this.btn_konf_validace.Name = "btn_konf_validace";
            this.btn_konf_validace.Size = new System.Drawing.Size(75, 64);
            this.btn_konf_validace.TabIndex = 1;
            this.btn_konf_validace.Text = "validace konfigurace";
            this.btn_konf_validace.UseVisualStyleBackColor = true;
            this.btn_konf_validace.Click += new System.EventHandler(this.btn_konf_validace_Click);
            // 
            // FormTerminalSeznam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 592);
            this.Controls.Add(this.PanelMain);
            this.Controls.Add(this.PanelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormTerminalSeznam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormTerminalSeznam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTerminalSeznam_FormClosing);
            this.Load += new System.EventHandler(this.FormTerminalSeznam_Load);
            this.PanelButton.ResumeLayout(false);
            this.PanelMain.ResumeLayout(false);
            this.PanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Terminaly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Terminaly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTerminaly)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelButton;
        private System.Windows.Forms.Panel PanelMain;
        private Zuby.ADGV.AdvancedDataGridView dg_Terminaly;
        private System.Windows.Forms.BindingSource bs_Terminaly;
        private Fask.Interfaces.DataSets.Terminal dsTerminaly;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDTERMINALDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATEREQDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dBTYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btn_aktualizovat;
        private ProgressControls.ProgressIndicator progressIndicatorUpdate;
        private System.ComponentModel.BackgroundWorker bw_Load;
        private System.Windows.Forms.Button btn_VNC;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.Button btn_konf_andr;
        private System.Windows.Forms.Button btn_konf_validace;
    }
}