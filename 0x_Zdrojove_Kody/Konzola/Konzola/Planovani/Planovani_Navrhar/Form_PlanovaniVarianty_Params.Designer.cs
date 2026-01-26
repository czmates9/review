namespace Konzola.Planovani.Planovani_Navrhar
{
    partial class Form_PlanovaniVarianty_Params
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
            this.variantyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Ulozit = new System.Windows.Forms.ToolStripMenuItem();
            this.bw_Varianty = new System.ComponentModel.BackgroundWorker();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.dg_Params = new Zuby.ADGV.AdvancedDataGridView();
            this.ColumnName_Lokalizace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bs_Params = new System.Windows.Forms.BindingSource(this.components);
            this.ds_Params = new Fask.Interfaces.DataSets.Vyroba_Planovani();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.buttonsPanel1 = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Params)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Params)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Params)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Menu,
            this.variantyToolStripMenuItem});
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
            // variantyToolStripMenuItem
            // 
            this.variantyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Ulozit});
            this.variantyToolStripMenuItem.Name = "variantyToolStripMenuItem";
            this.variantyToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.variantyToolStripMenuItem.Text = "Varianty";
            // 
            // tsmi_Ulozit
            // 
            this.tsmi_Ulozit.Name = "tsmi_Ulozit";
            this.tsmi_Ulozit.Size = new System.Drawing.Size(104, 22);
            this.tsmi_Ulozit.Text = "Uložit";
            this.tsmi_Ulozit.Click += new System.EventHandler(this.tsmi_Ulozit_Click);
            // 
            // bw_Varianty
            // 
            this.bw_Varianty.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Varianty_DoWork);
            this.bw_Varianty.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Varianty_RunWorkerCompleted);
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.Location = new System.Drawing.Point(202, 257);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(90, 90);
            this.progressIndicator1.TabIndex = 31;
            this.progressIndicator1.Text = "progressIndicator1";
            this.progressIndicator1.Visible = false;
            // 
            // dg_Params
            // 
            this.dg_Params.AllowUserToAddRows = false;
            this.dg_Params.AllowUserToDeleteRows = false;
            this.dg_Params.AllowUserToOrderColumns = true;
            this.dg_Params.AutoGenerateColumns = false;
            this.dg_Params.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Params.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName_Lokalizace,
            this.valueDataGridViewCheckBoxColumn});
            this.dg_Params.DataSource = this.bs_Params;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_Params.DefaultCellStyle = dataGridViewCellStyle1;
            this.dg_Params.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Params.FilterAndSortEnabled = true;
            this.dg_Params.Location = new System.Drawing.Point(0, 94);
            this.dg_Params.MultiSelect = false;
            this.dg_Params.Name = "dg_Params";
            this.dg_Params.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Params.Size = new System.Drawing.Size(447, 511);
            this.dg_Params.TabIndex = 2;
            this.dg_Params.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_Params_CellValueChanged);
            this.dg_Params.CurrentCellDirtyStateChanged += new System.EventHandler(this.dg_Params_CurrentCellDirtyStateChanged);
            // 
            // ColumnName_Lokalizace
            // 
            this.ColumnName_Lokalizace.DataPropertyName = "ColumnName_Lokalizace";
            this.ColumnName_Lokalizace.HeaderText = "Název parametru";
            this.ColumnName_Lokalizace.Name = "ColumnName_Lokalizace";
            this.ColumnName_Lokalizace.ReadOnly = true;
            this.ColumnName_Lokalizace.Width = 300;
            // 
            // valueDataGridViewCheckBoxColumn
            // 
            this.valueDataGridViewCheckBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewCheckBoxColumn.HeaderText = "Stav";
            this.valueDataGridViewCheckBoxColumn.Name = "valueDataGridViewCheckBoxColumn";
            // 
            // bs_Params
            // 
            this.bs_Params.DataMember = "FASK_PLANOVANI_PARAMS_Nastaveni";
            this.bs_Params.DataSource = this.ds_Params;
            // 
            // ds_Params
            // 
            this.ds_Params.DataSetName = "Vyroba_Planovani";
            this.ds_Params.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 67);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(447, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 32;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
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
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LimeGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(0, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(447, 43);
            this.label1.TabIndex = 33;
            this.label1.Text = "Parametry";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_PlanovaniVarianty_Params
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 605);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.dg_Params);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.buttonsPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_PlanovaniVarianty_Params";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plánovaní parametry";
            this.Load += new System.EventHandler(this.Form_PlanovaniVarianty_Params_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Params)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Params)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Params)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Fask.AdvancedButtonsPanel.ButtonsPanel buttonsPanel1;
        private Zuby.ADGV.AdvancedDataGridView dg_Params;
        private System.Windows.Forms.BindingSource bs_Params;
        private Fask.Interfaces.DataSets.Vyroba_Planovani ds_Params;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Menu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Konec;
        private System.Windows.Forms.ToolStripMenuItem variantyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Ulozit;
        private System.ComponentModel.BackgroundWorker bw_Varianty;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName_Lokalizace;
        private System.Windows.Forms.DataGridViewCheckBoxColumn valueDataGridViewCheckBoxColumn;
    }
}