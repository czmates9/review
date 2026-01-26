namespace Production.Forms
{
    partial class FormSkupinyList
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.vyrobaDataSet1 = new Production.DataServices.VyrobaDataSet();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonObnovit = new System.Windows.Forms.Button();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonOdstranit = new System.Windows.Forms.Button();
            this.buttonUpravit = new System.Windows.Forms.Button();
            this.buttonNovy = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.zaznamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upravitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odstranitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.obnovitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaDataSet1)).BeginInit();
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
            this.id,
            this.name,
            this.description});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(498, 444);
            this.dataGridView1.TabIndex = 1;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "Groups";
            this.bindingSource1.DataSource = this.vyrobaDataSet1;
            // 
            // vyrobaDataSet1
            // 
            this.vyrobaDataSet1.DataSetName = "VyrobaDataSet";
            this.vyrobaDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonObnovit);
            this.panelButtons.Controls.Add(this.buttonKonec);
            this.panelButtons.Controls.Add(this.buttonOdstranit);
            this.panelButtons.Controls.Add(this.buttonUpravit);
            this.panelButtons.Controls.Add(this.buttonNovy);
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
            this.buttonObnovit.TabIndex = 9;
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
            this.buttonKonec.TabIndex = 3;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // buttonOdstranit
            // 
            this.buttonOdstranit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdstranit.Location = new System.Drawing.Point(6, 162);
            this.buttonOdstranit.Name = "buttonOdstranit";
            this.buttonOdstranit.Size = new System.Drawing.Size(73, 63);
            this.buttonOdstranit.TabIndex = 2;
            this.buttonOdstranit.Text = "Odstranit";
            this.buttonOdstranit.UseVisualStyleBackColor = true;
            this.buttonOdstranit.Click += new System.EventHandler(this.buttonOdstranit_Click);
            // 
            // buttonUpravit
            // 
            this.buttonUpravit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpravit.Location = new System.Drawing.Point(6, 93);
            this.buttonUpravit.Name = "buttonUpravit";
            this.buttonUpravit.Size = new System.Drawing.Size(73, 63);
            this.buttonUpravit.TabIndex = 1;
            this.buttonUpravit.Text = "Upravit";
            this.buttonUpravit.UseVisualStyleBackColor = true;
            this.buttonUpravit.Click += new System.EventHandler(this.buttonUpravit_Click);
            // 
            // buttonNovy
            // 
            this.buttonNovy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNovy.Location = new System.Drawing.Point(6, 24);
            this.buttonNovy.Name = "buttonNovy";
            this.buttonNovy.Size = new System.Drawing.Size(73, 63);
            this.buttonNovy.TabIndex = 0;
            this.buttonNovy.Text = "Nový";
            this.buttonNovy.UseVisualStyleBackColor = true;
            this.buttonNovy.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.menuStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 468);
            this.panel1.TabIndex = 3;
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
            this.novýToolStripMenuItem,
            this.upravitToolStripMenuItem,
            this.odstranitToolStripMenuItem,
            this.toolStripSeparator1,
            this.obnovitToolStripMenuItem,
            this.konecToolStripMenuItem});
            this.zaznamToolStripMenuItem.Name = "zaznamToolStripMenuItem";
            this.zaznamToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.zaznamToolStripMenuItem.Text = "Skupina";
            // 
            // novýToolStripMenuItem
            // 
            this.novýToolStripMenuItem.Name = "novýToolStripMenuItem";
            this.novýToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.novýToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.novýToolStripMenuItem.Text = "Nový";
            this.novýToolStripMenuItem.Click += new System.EventHandler(this.novýToolStripMenuItem_Click);
            // 
            // upravitToolStripMenuItem
            // 
            this.upravitToolStripMenuItem.Name = "upravitToolStripMenuItem";
            this.upravitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.upravitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.upravitToolStripMenuItem.Text = "Upravit";
            this.upravitToolStripMenuItem.Click += new System.EventHandler(this.upravitToolStripMenuItem_Click);
            // 
            // odstranitToolStripMenuItem
            // 
            this.odstranitToolStripMenuItem.Name = "odstranitToolStripMenuItem";
            this.odstranitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.odstranitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.odstranitToolStripMenuItem.Text = "Odstranit";
            this.odstranitToolStripMenuItem.Click += new System.EventHandler(this.odstranitToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(163, 6);
            // 
            // obnovitToolStripMenuItem
            // 
            this.obnovitToolStripMenuItem.Name = "obnovitToolStripMenuItem";
            this.obnovitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.obnovitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.obnovitToolStripMenuItem.Text = "Aktualizovat";
            this.obnovitToolStripMenuItem.Click += new System.EventHandler(this.obnovitToolStripMenuItem_Click);
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.konecToolStripMenuItem.Text = "Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID skupiny";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "Název";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // description
            // 
            this.description.DataPropertyName = "description";
            this.description.HeaderText = "Popis";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            // 
            // FormSkupinyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 468);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormSkupinyList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Skupiny";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUzivateleList_FormClosing);
            this.Load += new System.EventHandler(this.FormUzivateleList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUzivateleList_KeyDown);
            this.Resize += new System.EventHandler(this.FormUzivateleList_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vyrobaDataSet1)).EndInit();
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
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem zaznamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novýToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upravitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem odstranitToolStripMenuItem;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DataServices.VyrobaDataSet vyrobaDataSet1;
        private System.Windows.Forms.Button buttonOdstranit;
        private System.Windows.Forms.Button buttonUpravit;
        private System.Windows.Forms.Button buttonNovy;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private System.Windows.Forms.Button buttonObnovit;
        private System.Windows.Forms.ToolStripMenuItem obnovitToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
    }
}