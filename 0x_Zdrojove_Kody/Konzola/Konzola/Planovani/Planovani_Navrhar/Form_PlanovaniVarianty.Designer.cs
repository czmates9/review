namespace Konzola.Planovani.Planovani_Navrhar
{
    partial class Form_PlanovaniVarianty
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmi_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Konec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Varianta = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_vybratVariantu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_upravitVariantu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_smazatVariantu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_novaVarianta = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_duplikaceVarianty = new System.Windows.Forms.ToolStripMenuItem();
            this.bw_Varianty = new System.ComponentModel.BackgroundWorker();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.label1 = new System.Windows.Forms.Label();
            this.dg_Varianty = new Zuby.ADGV.AdvancedDataGridView();
            this.dESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_Varianty = new System.Windows.Forms.BindingSource(this.components);
            this.ds_Varianty = new Fask.Interfaces.DataSets.Vyroba_Planovani();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.buttonsPanel1 = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Varianty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Varianty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Varianty)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Menu,
            this.tsmi_Varianta});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(447, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmi_Menu
            // 
            this.tsmi_Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Konec});
            this.tsmi_Menu.Name = "tsmi_Menu";
            this.tsmi_Menu.Size = new System.Drawing.Size(50, 20);
            this.tsmi_Menu.Text = "Menu";
            // 
            // tsmi_Konec
            // 
            this.tsmi_Konec.Name = "tsmi_Konec";
            this.tsmi_Konec.Size = new System.Drawing.Size(107, 22);
            this.tsmi_Konec.Text = "Konec";
            this.tsmi_Konec.Click += new System.EventHandler(this.tsmi_Konec_Click);
            // 
            // tsmi_Varianta
            // 
            this.tsmi_Varianta.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_vybratVariantu,
            this.tsmi_upravitVariantu,
            this.tsmi_smazatVariantu,
            this.tsmi_novaVarianta,
            this.tsmi_duplikaceVarianty});
            this.tsmi_Varianta.Name = "tsmi_Varianta";
            this.tsmi_Varianta.Size = new System.Drawing.Size(61, 20);
            this.tsmi_Varianta.Text = "Varianty";
            // 
            // tsmi_vybratVariantu
            // 
            this.tsmi_vybratVariantu.Name = "tsmi_vybratVariantu";
            this.tsmi_vybratVariantu.Size = new System.Drawing.Size(177, 22);
            this.tsmi_vybratVariantu.Text = "Spustit variantu";
            this.tsmi_vybratVariantu.Click += new System.EventHandler(this.tsmi_vybratVariantu_Click);
            // 
            // tsmi_upravitVariantu
            // 
            this.tsmi_upravitVariantu.Name = "tsmi_upravitVariantu";
            this.tsmi_upravitVariantu.Size = new System.Drawing.Size(177, 22);
            this.tsmi_upravitVariantu.Text = "Upravit variantu";
            this.tsmi_upravitVariantu.Click += new System.EventHandler(this.tsmi_upravitVariantu_Click);
            // 
            // tsmi_smazatVariantu
            // 
            this.tsmi_smazatVariantu.Name = "tsmi_smazatVariantu";
            this.tsmi_smazatVariantu.Size = new System.Drawing.Size(177, 22);
            this.tsmi_smazatVariantu.Text = "Smazat variantu";
            this.tsmi_smazatVariantu.Click += new System.EventHandler(this.tsmi_smazatVariantu_Click);
            // 
            // tsmi_novaVarianta
            // 
            this.tsmi_novaVarianta.Name = "tsmi_novaVarianta";
            this.tsmi_novaVarianta.Size = new System.Drawing.Size(177, 22);
            this.tsmi_novaVarianta.Text = "Nová varianta";
            this.tsmi_novaVarianta.Click += new System.EventHandler(this.tsmi_novaVarianta_Click);
            // 
            // tsmi_duplikaceVarianty
            // 
            this.tsmi_duplikaceVarianty.Name = "tsmi_duplikaceVarianty";
            this.tsmi_duplikaceVarianty.Size = new System.Drawing.Size(177, 22);
            this.tsmi_duplikaceVarianty.Text = "Duplikovat variantu";
            this.tsmi_duplikaceVarianty.Click += new System.EventHandler(this.tsmi_duplikaceVarianty_Click);
            // 
            // bw_Varianty
            // 
            this.bw_Varianty.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Varianty_DoWork);
            this.bw_Varianty.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Varianty_RunWorkerCompleted);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(182, 258);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 31;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(0, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(447, 34);
            this.label1.TabIndex = 32;
            this.label1.Text = "Návrhář varianty";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dg_Varianty
            // 
            this.dg_Varianty.AllowUserToAddRows = false;
            this.dg_Varianty.AllowUserToDeleteRows = false;
            this.dg_Varianty.AllowUserToOrderColumns = true;
            this.dg_Varianty.AutoGenerateColumns = false;
            this.dg_Varianty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Varianty.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dESCDataGridViewTextBoxColumn});
            this.dg_Varianty.DataSource = this.bs_Varianty;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_Varianty.DefaultCellStyle = dataGridViewCellStyle1;
            this.dg_Varianty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Varianty.FilterAndSortEnabled = true;
            this.dg_Varianty.Location = new System.Drawing.Point(0, 85);
            this.dg_Varianty.MultiSelect = false;
            this.dg_Varianty.Name = "dg_Varianty";
            this.dg_Varianty.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Varianty.Size = new System.Drawing.Size(447, 520);
            this.dg_Varianty.TabIndex = 2;
            // 
            // dESCDataGridViewTextBoxColumn
            // 
            this.dESCDataGridViewTextBoxColumn.DataPropertyName = "DESC";
            this.dESCDataGridViewTextBoxColumn.HeaderText = "Nazev varianty";
            this.dESCDataGridViewTextBoxColumn.Name = "dESCDataGridViewTextBoxColumn";
            this.dESCDataGridViewTextBoxColumn.ReadOnly = true;
            this.dESCDataGridViewTextBoxColumn.Width = 400;
            // 
            // bs_Varianty
            // 
            this.bs_Varianty.DataMember = "FASK_PLANOVANI";
            this.bs_Varianty.DataSource = this.ds_Varianty;
            // 
            // ds_Varianty
            // 
            this.ds_Varianty.DataSetName = "Vyroba_Planovani";
            this.ds_Varianty.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 58);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(447, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 4;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // buttonsPanel1
            // 
            this.buttonsPanel1.AutoScroll = true;
            this.buttonsPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonsPanel1.Location = new System.Drawing.Point(447, 0);
            this.buttonsPanel1.Name = "buttonsPanel1";
            this.buttonsPanel1.Size = new System.Drawing.Size(102, 605);
            this.buttonsPanel1.TabIndex = 0;
            // 
            // Form_PlanovaniVarianty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 605);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dg_Varianty);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.buttonsPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_PlanovaniVarianty";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plánovaní varianty";
            this.Load += new System.EventHandler(this.Form_PlanovaniVarianty_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Varianty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Varianty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Varianty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Fask.AdvancedButtonsPanel.ButtonsPanel buttonsPanel1;
        private Zuby.ADGV.AdvancedDataGridView dg_Varianty;
        private System.Windows.Forms.BindingSource bs_Varianty;
        private Fask.Interfaces.DataSets.Vyroba_Planovani ds_Varianty;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Menu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Konec;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Varianta;
        private System.Windows.Forms.ToolStripMenuItem tsmi_vybratVariantu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_upravitVariantu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_smazatVariantu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_novaVarianta;
        private System.ComponentModel.BackgroundWorker bw_Varianty;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem tsmi_duplikaceVarianty;
    }
}