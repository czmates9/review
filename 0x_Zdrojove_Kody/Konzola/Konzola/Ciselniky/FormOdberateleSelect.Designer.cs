namespace Konzola.Ciselniky
{
    partial class FormOdberateleSelect
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgOdberatele = new Zuby.ADGV.AdvancedDataGridView();
            this.odbidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odbdescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odbtypDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odbcarcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odbicoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menaidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odbmistoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odbuliceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odbcisloOrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odbpscDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odbdicDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odbOdberatelDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odbDodavatelDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEXROWIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsOdberatele = new System.Windows.Forms.BindingSource(this.components);
            this.dsOdberatele = new Fask.Interfaces.DataSets.Odberatele();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVybrat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAktualizovat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgOdberatele)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOdberatele)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOdberatele)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgOdberatele
            // 
            this.dgOdberatele.AllowUserToAddRows = false;
            this.dgOdberatele.AllowUserToDeleteRows = false;
            this.dgOdberatele.AllowUserToOrderColumns = true;
            this.dgOdberatele.AllowUserToResizeRows = false;
            this.dgOdberatele.AutoGenerateColumns = false;
            this.dgOdberatele.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOdberatele.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.odbidDataGridViewTextBoxColumn,
            this.odbdescDataGridViewTextBoxColumn,
            this.odbtypDataGridViewTextBoxColumn,
            this.odbcarcodeDataGridViewTextBoxColumn,
            this.odbicoDataGridViewTextBoxColumn,
            this.menaidDataGridViewTextBoxColumn,
            this.odbmistoDataGridViewTextBoxColumn,
            this.odbuliceDataGridViewTextBoxColumn,
            this.odbcisloOrDataGridViewTextBoxColumn,
            this.odbpscDataGridViewTextBoxColumn,
            this.odbdicDataGridViewTextBoxColumn,
            this.odbOdberatelDataGridViewCheckBoxColumn,
            this.odbDodavatelDataGridViewCheckBoxColumn,
            this.dEXROWIDDataGridViewTextBoxColumn});
            this.dgOdberatele.DataSource = this.bsOdberatele;
            this.dgOdberatele.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOdberatele.EnableHeadersVisualStyles = false;
            this.dgOdberatele.Location = new System.Drawing.Point(0, 51);
            this.dgOdberatele.Name = "dgOdberatele";
            this.dgOdberatele.ReadOnly = true;
            this.dgOdberatele.RowHeadersVisible = false;
            this.dgOdberatele.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOdberatele.Size = new System.Drawing.Size(498, 417);
            this.dgOdberatele.TabIndex = 1;
            this.dgOdberatele.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgOdberatele.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dgOdberatele.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // odbidDataGridViewTextBoxColumn
            // 
            this.odbidDataGridViewTextBoxColumn.DataPropertyName = "odb_id";
            dataGridViewCellStyle1.NullValue = "-";
            this.odbidDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.odbidDataGridViewTextBoxColumn.HeaderText = "ID";
            this.odbidDataGridViewTextBoxColumn.Name = "odbidDataGridViewTextBoxColumn";
            this.odbidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // odbdescDataGridViewTextBoxColumn
            // 
            this.odbdescDataGridViewTextBoxColumn.DataPropertyName = "odb_desc";
            dataGridViewCellStyle2.NullValue = "-";
            this.odbdescDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.odbdescDataGridViewTextBoxColumn.HeaderText = "Označení";
            this.odbdescDataGridViewTextBoxColumn.Name = "odbdescDataGridViewTextBoxColumn";
            this.odbdescDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // odbtypDataGridViewTextBoxColumn
            // 
            this.odbtypDataGridViewTextBoxColumn.DataPropertyName = "odb_typ";
            dataGridViewCellStyle3.NullValue = "-";
            this.odbtypDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.odbtypDataGridViewTextBoxColumn.HeaderText = "Typ";
            this.odbtypDataGridViewTextBoxColumn.Name = "odbtypDataGridViewTextBoxColumn";
            this.odbtypDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // odbcarcodeDataGridViewTextBoxColumn
            // 
            this.odbcarcodeDataGridViewTextBoxColumn.DataPropertyName = "odb_carcode";
            dataGridViewCellStyle4.NullValue = "-";
            this.odbcarcodeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.odbcarcodeDataGridViewTextBoxColumn.HeaderText = "Čár. kód";
            this.odbcarcodeDataGridViewTextBoxColumn.Name = "odbcarcodeDataGridViewTextBoxColumn";
            this.odbcarcodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // odbicoDataGridViewTextBoxColumn
            // 
            this.odbicoDataGridViewTextBoxColumn.DataPropertyName = "odb_ico";
            dataGridViewCellStyle5.NullValue = "-";
            this.odbicoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.odbicoDataGridViewTextBoxColumn.HeaderText = "IČO";
            this.odbicoDataGridViewTextBoxColumn.Name = "odbicoDataGridViewTextBoxColumn";
            this.odbicoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // menaidDataGridViewTextBoxColumn
            // 
            this.menaidDataGridViewTextBoxColumn.DataPropertyName = "mena_id";
            dataGridViewCellStyle6.NullValue = "-";
            this.menaidDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.menaidDataGridViewTextBoxColumn.HeaderText = "ID měny";
            this.menaidDataGridViewTextBoxColumn.Name = "menaidDataGridViewTextBoxColumn";
            this.menaidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // odbmistoDataGridViewTextBoxColumn
            // 
            this.odbmistoDataGridViewTextBoxColumn.DataPropertyName = "odb_misto";
            dataGridViewCellStyle7.NullValue = "-";
            this.odbmistoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.odbmistoDataGridViewTextBoxColumn.HeaderText = "Místo sídla";
            this.odbmistoDataGridViewTextBoxColumn.Name = "odbmistoDataGridViewTextBoxColumn";
            this.odbmistoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // odbuliceDataGridViewTextBoxColumn
            // 
            this.odbuliceDataGridViewTextBoxColumn.DataPropertyName = "odb_ulice";
            dataGridViewCellStyle8.NullValue = "-";
            this.odbuliceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.odbuliceDataGridViewTextBoxColumn.HeaderText = "Ulice";
            this.odbuliceDataGridViewTextBoxColumn.Name = "odbuliceDataGridViewTextBoxColumn";
            this.odbuliceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // odbcisloOrDataGridViewTextBoxColumn
            // 
            this.odbcisloOrDataGridViewTextBoxColumn.DataPropertyName = "odb_cisloOr";
            dataGridViewCellStyle9.NullValue = "-";
            this.odbcisloOrDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.odbcisloOrDataGridViewTextBoxColumn.HeaderText = "Orientační číslo";
            this.odbcisloOrDataGridViewTextBoxColumn.Name = "odbcisloOrDataGridViewTextBoxColumn";
            this.odbcisloOrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // odbpscDataGridViewTextBoxColumn
            // 
            this.odbpscDataGridViewTextBoxColumn.DataPropertyName = "odb_psc";
            dataGridViewCellStyle10.NullValue = "-";
            this.odbpscDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.odbpscDataGridViewTextBoxColumn.HeaderText = "PSČ";
            this.odbpscDataGridViewTextBoxColumn.Name = "odbpscDataGridViewTextBoxColumn";
            this.odbpscDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // odbdicDataGridViewTextBoxColumn
            // 
            this.odbdicDataGridViewTextBoxColumn.DataPropertyName = "odb_dic";
            dataGridViewCellStyle11.NullValue = "-";
            this.odbdicDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.odbdicDataGridViewTextBoxColumn.HeaderText = "DIČ";
            this.odbdicDataGridViewTextBoxColumn.Name = "odbdicDataGridViewTextBoxColumn";
            this.odbdicDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // odbOdberatelDataGridViewCheckBoxColumn
            // 
            this.odbOdberatelDataGridViewCheckBoxColumn.DataPropertyName = "odb_Odberatel";
            dataGridViewCellStyle12.NullValue = "-";
            this.odbOdberatelDataGridViewCheckBoxColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.odbOdberatelDataGridViewCheckBoxColumn.HeaderText = "Odběratel";
            this.odbOdberatelDataGridViewCheckBoxColumn.Name = "odbOdberatelDataGridViewCheckBoxColumn";
            this.odbOdberatelDataGridViewCheckBoxColumn.ReadOnly = true;
            this.odbOdberatelDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.odbOdberatelDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // odbDodavatelDataGridViewCheckBoxColumn
            // 
            this.odbDodavatelDataGridViewCheckBoxColumn.DataPropertyName = "odb_Dodavatel";
            dataGridViewCellStyle13.NullValue = "-";
            this.odbDodavatelDataGridViewCheckBoxColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.odbDodavatelDataGridViewCheckBoxColumn.HeaderText = "Dodavatel";
            this.odbDodavatelDataGridViewCheckBoxColumn.Name = "odbDodavatelDataGridViewCheckBoxColumn";
            this.odbDodavatelDataGridViewCheckBoxColumn.ReadOnly = true;
            this.odbDodavatelDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.odbDodavatelDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dEXROWIDDataGridViewTextBoxColumn
            // 
            this.dEXROWIDDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID";
            dataGridViewCellStyle14.NullValue = "-";
            this.dEXROWIDDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle14;
            this.dEXROWIDDataGridViewTextBoxColumn.HeaderText = "Index";
            this.dEXROWIDDataGridViewTextBoxColumn.Name = "dEXROWIDDataGridViewTextBoxColumn";
            this.dEXROWIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsOdberatele
            // 
            this.bsOdberatele.DataMember = "CZMST090";
            this.bsOdberatele.DataSource = this.dsOdberatele;
            // 
            // dsOdberatele
            // 
            this.dsOdberatele.DataSetName = "Odberatele";
            this.dsOdberatele.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panelButtons
            // 
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(498, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 468);
            this.panelButtons.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgOdberatele);
            this.panel1.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.panel1.Controls.Add(this.menuStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 468);
            this.panel1.TabIndex = 1;
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(498, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // tsmiMenu
            // 
            this.tsmiMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiKonec,
            this.toolStripSeparator1,
            this.tsmiAktualizovat,
            this.toolStripSeparator2,
            this.tsmiVybrat});
            this.tsmiMenu.Name = "tsmiMenu";
            this.tsmiMenu.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenu.Text = "Menu";
            // 
            // tsmiVybrat
            // 
            this.tsmiVybrat.Name = "tsmiVybrat";
            this.tsmiVybrat.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiVybrat.Size = new System.Drawing.Size(158, 22);
            this.tsmiVybrat.Text = "Vybrat";
            this.tsmiVybrat.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // tsmiAktualizovat
            // 
            this.tsmiAktualizovat.Name = "tsmiAktualizovat";
            this.tsmiAktualizovat.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tsmiAktualizovat.Size = new System.Drawing.Size(158, 22);
            this.tsmiAktualizovat.Text = "Aktualizovat";
            this.tsmiAktualizovat.Click += new System.EventHandler(this.obnovitToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(155, 6);
            // 
            // tsmiKonec
            // 
            this.tsmiKonec.Name = "tsmiKonec";
            this.tsmiKonec.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonec.Size = new System.Drawing.Size(158, 22);
            this.tsmiKonec.Text = "Konec";
            this.tsmiKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 24);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(498, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 3;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // FormOdberateleSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 468);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormOdberateleSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výběr odběratele";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOdberateleSelect_FormClosing);
            this.Load += new System.EventHandler(this.FormOdberateleSelect_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOdberateleSelect_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgOdberatele)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOdberatele)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOdberatele)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgOdberatele;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource bsOdberatele;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiVybrat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAktualizovat;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private Fask.Interfaces.DataSets.Odberatele dsOdberatele;
        private System.Windows.Forms.DataGridViewTextBoxColumn odbidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn odbdescDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn odbtypDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn odbcarcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn odbicoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn menaidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn odbmistoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn odbuliceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn odbcisloOrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn odbpscDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn odbdicDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn odbOdberatelDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn odbDodavatelDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
    }
}