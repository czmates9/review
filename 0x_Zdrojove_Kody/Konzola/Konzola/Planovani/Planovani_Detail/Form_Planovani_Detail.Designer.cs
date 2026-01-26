
namespace Konzola.Planovani.Planovani_Detail
{
    partial class Form_Planovani_Detail
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
            this.label1 = new System.Windows.Forms.Label();
            this.bw_detail = new System.ComponentModel.BackgroundWorker();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.dg_detail = new Zuby.ADGV.AdvancedDataGridView();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_Skratka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLDescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYStavDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_detail = new System.Windows.Forms.BindingSource(this.components);
            this.ds_detail = new Fask.Interfaces.DataSets.Vyroba_Planovani();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            ((System.ComponentModel.ISupportInitialize)(this.dg_detail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_detail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_detail)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gold;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(657, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "Detail karty";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bw_detail
            // 
            this.bw_detail.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_detail_DoWork);
            this.bw_detail.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_detail_RunWorkerCompleted);
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 49);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(657, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 1;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // dg_detail
            // 
            this.dg_detail.AllowUserToAddRows = false;
            this.dg_detail.AllowUserToDeleteRows = false;
            this.dg_detail.AllowUserToOrderColumns = true;
            this.dg_detail.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_detail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_detail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_detail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMCODEDataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.SKL_Skratka,
            this.sKLDescDataGridViewTextBoxColumn,
            this.qTYStavDataGridViewTextBoxColumn});
            this.dg_detail.DataSource = this.bs_detail;
            this.dg_detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_detail.FilterAndSortEnabled = true;
            this.dg_detail.Location = new System.Drawing.Point(0, 76);
            this.dg_detail.Name = "dg_detail";
            this.dg_detail.ReadOnly = true;
            this.dg_detail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_detail.Size = new System.Drawing.Size(657, 385);
            this.dg_detail.TabIndex = 2;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "ID položky";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMCODEDataGridViewTextBoxColumn
            // 
            this.iTEMCODEDataGridViewTextBoxColumn.DataPropertyName = "ITEMCODE";
            this.iTEMCODEDataGridViewTextBoxColumn.HeaderText = "Kód položky";
            this.iTEMCODEDataGridViewTextBoxColumn.Name = "iTEMCODEDataGridViewTextBoxColumn";
            this.iTEMCODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sKLIDDataGridViewTextBoxColumn
            // 
            this.sKLIDDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.HeaderText = "ID Skladu";
            this.sKLIDDataGridViewTextBoxColumn.Name = "sKLIDDataGridViewTextBoxColumn";
            this.sKLIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // SKL_Skratka
            // 
            this.SKL_Skratka.DataPropertyName = "SKL_Skratka";
            this.SKL_Skratka.HeaderText = "Skratka skladu";
            this.SKL_Skratka.Name = "SKL_Skratka";
            this.SKL_Skratka.ReadOnly = true;
            // 
            // sKLDescDataGridViewTextBoxColumn
            // 
            this.sKLDescDataGridViewTextBoxColumn.DataPropertyName = "SKL_Desc";
            this.sKLDescDataGridViewTextBoxColumn.HeaderText = "Nazev skladu";
            this.sKLDescDataGridViewTextBoxColumn.Name = "sKLDescDataGridViewTextBoxColumn";
            this.sKLDescDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYStavDataGridViewTextBoxColumn
            // 
            this.qTYStavDataGridViewTextBoxColumn.DataPropertyName = "QTY_Stav";
            this.qTYStavDataGridViewTextBoxColumn.HeaderText = "Množství";
            this.qTYStavDataGridViewTextBoxColumn.Name = "qTYStavDataGridViewTextBoxColumn";
            this.qTYStavDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bs_detail
            // 
            this.bs_detail.DataMember = "Planovani_Detail";
            this.bs_detail.DataSource = this.ds_detail;
            // 
            // ds_detail
            // 
            this.ds_detail.DataSetName = "Vyroba_Planovani";
            this.ds_detail.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(228, 205);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 32;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // Form_Planovani_Detail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 461);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dg_detail);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.label1);
            this.Name = "Form_Planovani_Detail";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_Planovani_Detail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_detail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_detail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_detail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource bs_detail;
        private Fask.Interfaces.DataSets.Vyroba_Planovani ds_detail;
        private System.ComponentModel.BackgroundWorker bw_detail;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private Zuby.ADGV.AdvancedDataGridView dg_detail;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_Skratka;
        private System.Windows.Forms.DataGridViewTextBoxColumn sKLDescDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYStavDataGridViewTextBoxColumn;
    }
}