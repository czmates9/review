namespace Vyroba_Konzola.Ciselniky
{
    partial class FormUzivateleSelect
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lOGINDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pASSWDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aDMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fIRSTNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sECONDNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hASHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eANDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.ukolovaniDataset1 = new Fask.Console.Interfaces.DataSets.Ukolovani();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonObnovit = new System.Windows.Forms.Button();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonVybratUzivatele = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.zaznamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vybratToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.obnovitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ukolovaniDataset1)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lOGINDataGridViewTextBoxColumn,
            this.pASSWDDataGridViewTextBoxColumn,
            this.iDDataGridViewTextBoxColumn,
            this.aDMDataGridViewTextBoxColumn,
            this.fIRSTNAMEDataGridViewTextBoxColumn,
            this.sECONDNAMEDataGridViewTextBoxColumn,
            this.hASHDataGridViewTextBoxColumn,
            this.eANDataGridViewTextBoxColumn,
            this.cODEDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(498, 444);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // lOGINDataGridViewTextBoxColumn
            // 
            this.lOGINDataGridViewTextBoxColumn.DataPropertyName = "LOGIN";
            this.lOGINDataGridViewTextBoxColumn.HeaderText = "Login";
            this.lOGINDataGridViewTextBoxColumn.Name = "lOGINDataGridViewTextBoxColumn";
            this.lOGINDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pASSWDDataGridViewTextBoxColumn
            // 
            this.pASSWDDataGridViewTextBoxColumn.DataPropertyName = "PASSWD";
            this.pASSWDDataGridViewTextBoxColumn.HeaderText = "Heslo";
            this.pASSWDDataGridViewTextBoxColumn.Name = "pASSWDDataGridViewTextBoxColumn";
            this.pASSWDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // aDMDataGridViewTextBoxColumn
            // 
            this.aDMDataGridViewTextBoxColumn.DataPropertyName = "ADM";
            this.aDMDataGridViewTextBoxColumn.HeaderText = "Admin";
            this.aDMDataGridViewTextBoxColumn.Name = "aDMDataGridViewTextBoxColumn";
            this.aDMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fIRSTNAMEDataGridViewTextBoxColumn
            // 
            this.fIRSTNAMEDataGridViewTextBoxColumn.DataPropertyName = "FIRSTNAME";
            this.fIRSTNAMEDataGridViewTextBoxColumn.HeaderText = "Jméno";
            this.fIRSTNAMEDataGridViewTextBoxColumn.Name = "fIRSTNAMEDataGridViewTextBoxColumn";
            this.fIRSTNAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sECONDNAMEDataGridViewTextBoxColumn
            // 
            this.sECONDNAMEDataGridViewTextBoxColumn.DataPropertyName = "SECONDNAME";
            this.sECONDNAMEDataGridViewTextBoxColumn.HeaderText = "Příjmení";
            this.sECONDNAMEDataGridViewTextBoxColumn.Name = "sECONDNAMEDataGridViewTextBoxColumn";
            this.sECONDNAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hASHDataGridViewTextBoxColumn
            // 
            this.hASHDataGridViewTextBoxColumn.DataPropertyName = "HASH";
            this.hASHDataGridViewTextBoxColumn.HeaderText = "HASH";
            this.hASHDataGridViewTextBoxColumn.Name = "hASHDataGridViewTextBoxColumn";
            this.hASHDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // eANDataGridViewTextBoxColumn
            // 
            this.eANDataGridViewTextBoxColumn.DataPropertyName = "EAN";
            this.eANDataGridViewTextBoxColumn.HeaderText = "EAN";
            this.eANDataGridViewTextBoxColumn.Name = "eANDataGridViewTextBoxColumn";
            this.eANDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cODEDataGridViewTextBoxColumn
            // 
            this.cODEDataGridViewTextBoxColumn.DataPropertyName = "CODE";
            this.cODEDataGridViewTextBoxColumn.HeaderText = "CODE";
            this.cODEDataGridViewTextBoxColumn.Name = "cODEDataGridViewTextBoxColumn";
            this.cODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "CZMSTPWD";
            this.bindingSource1.DataSource = this.ukolovaniDataset1;
            // 
            // ukolovaniDataset1
            // 
            this.ukolovaniDataset1.DataSetName = "UkolovaniDataset";
            this.ukolovaniDataset1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonObnovit);
            this.panelButtons.Controls.Add(this.buttonKonec);
            this.panelButtons.Controls.Add(this.buttonVybratUzivatele);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(498, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 468);
            this.panelButtons.TabIndex = 2;
            // 
            // buttonObnovit
            // 
            this.buttonObnovit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonObnovit.Location = new System.Drawing.Point(6, 324);
            this.buttonObnovit.Name = "buttonObnovit";
            this.buttonObnovit.Size = new System.Drawing.Size(73, 63);
            this.buttonObnovit.TabIndex = 3;
            this.buttonObnovit.Text = "Aktualizovat";
            this.buttonObnovit.UseVisualStyleBackColor = true;
            this.buttonObnovit.Click += new System.EventHandler(this.obnovitToolStripMenuItem_Click);
            // 
            // buttonKonec
            // 
            this.buttonKonec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKonec.Location = new System.Drawing.Point(6, 393);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(73, 63);
            this.buttonKonec.TabIndex = 4;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // buttonVybratUzivatele
            // 
            this.buttonVybratUzivatele.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVybratUzivatele.Location = new System.Drawing.Point(6, 24);
            this.buttonVybratUzivatele.Name = "buttonVybratUzivatele";
            this.buttonVybratUzivatele.Size = new System.Drawing.Size(73, 63);
            this.buttonVybratUzivatele.TabIndex = 2;
            this.buttonVybratUzivatele.Text = "Vybrat uživatele";
            this.buttonVybratUzivatele.UseVisualStyleBackColor = true;
            this.buttonVybratUzivatele.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
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
            this.zaznamToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(498, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // zaznamToolStripMenuItem
            // 
            this.zaznamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vybratToolStripMenuItem,
            this.toolStripSeparator1,
            this.obnovitToolStripMenuItem,
            this.toolStripSeparator2,
            this.konecToolStripMenuItem});
            this.zaznamToolStripMenuItem.Name = "zaznamToolStripMenuItem";
            this.zaznamToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.zaznamToolStripMenuItem.Text = "Menu";
            // 
            // vybratToolStripMenuItem
            // 
            this.vybratToolStripMenuItem.Name = "vybratToolStripMenuItem";
            this.vybratToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.vybratToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.vybratToolStripMenuItem.Text = "Vybrat";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // obnovitToolStripMenuItem
            // 
            this.obnovitToolStripMenuItem.Name = "obnovitToolStripMenuItem";
            this.obnovitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.obnovitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.obnovitToolStripMenuItem.Text = "Aktualizovat";
            this.obnovitToolStripMenuItem.Click += new System.EventHandler(this.obnovitToolStripMenuItem_Click);
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.konecToolStripMenuItem.Text = "Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(155, 6);
            // 
            // FormUzivateleSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 468);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormUzivateleSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výběr uživatele";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUzivateleList_FormClosing);
            this.Load += new System.EventHandler(this.FormUzivateleList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUzivateleList_KeyDown);
            this.Resize += new System.EventHandler(this.FormUzivateleList_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ukolovaniDataset1)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button buttonVybratUzivatele;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.Button buttonObnovit;
        private Fask.Console.Interfaces.DataSets.Ukolovani ukolovaniDataset1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem zaznamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vybratToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem obnovitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOGINDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pASSWDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aDMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fIRSTNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sECONDNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hASHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eANDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}