namespace ProgramVersion
{
    partial class VerzeProgramu_MST_Ctecka
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
            this.bs_Info = new System.Windows.Forms.BindingSource(this.components);
            this.ds_Info = new ProgramVersion.DataSet.UpdateInfo();
            this.dg_Info = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Open_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Save_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAs_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Close_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.úpravyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.add_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delete_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edit_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.clearedit_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clear_all_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nápovědaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_zobraznapovedu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.change_button = new System.Windows.Forms.Button();
            this.Delete_button = new System.Windows.Forms.Button();
            this.Add_button = new System.Windows.Forms.Button();
            this.textBox_Message = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_URL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_rev = new System.Windows.Forms.TextBox();
            this.textBox_minor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_build = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_major = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Info)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Info)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Info)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // bs_Info
            // 
            this.bs_Info.DataMember = "UpdateData";
            this.bs_Info.DataSource = this.ds_Info;
            // 
            // ds_Info
            // 
            this.ds_Info.DataSetName = "UpdateInfo";
            this.ds_Info.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dg_Info
            // 
            this.dg_Info.AllowUserToAddRows = false;
            this.dg_Info.AllowUserToDeleteRows = false;
            this.dg_Info.AllowUserToOrderColumns = true;
            this.dg_Info.AllowUserToResizeRows = false;
            this.dg_Info.AutoGenerateColumns = false;
            this.dg_Info.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg_Info.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Info.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.versionDataGridViewTextBoxColumn,
            this.messageDataGridViewTextBoxColumn,
            this.linkDataGridViewTextBoxColumn});
            this.dg_Info.DataSource = this.bs_Info;
            this.dg_Info.Dock = System.Windows.Forms.DockStyle.Top;
            this.dg_Info.Location = new System.Drawing.Point(0, 24);
            this.dg_Info.MultiSelect = false;
            this.dg_Info.Name = "dg_Info";
            this.dg_Info.ReadOnly = true;
            this.dg_Info.RowHeadersVisible = false;
            this.dg_Info.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Info.Size = new System.Drawing.Size(582, 184);
            this.dg_Info.TabIndex = 0;
            this.dg_Info.SelectionChanged += new System.EventHandler(this.dg_Info_SelectionChanged);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Název Programu";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // versionDataGridViewTextBoxColumn
            // 
            this.versionDataGridViewTextBoxColumn.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn.HeaderText = "Verze";
            this.versionDataGridViewTextBoxColumn.Name = "versionDataGridViewTextBoxColumn";
            this.versionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.HeaderText = "Správa";
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            this.messageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // linkDataGridViewTextBoxColumn
            // 
            this.linkDataGridViewTextBoxColumn.DataPropertyName = "Link";
            this.linkDataGridViewTextBoxColumn.HeaderText = "Cesta k souboru";
            this.linkDataGridViewTextBoxColumn.Name = "linkDataGridViewTextBoxColumn";
            this.linkDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.úpravyToolStripMenuItem,
            this.nápovědaToolStripMenuItem,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(582, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Open_toolStripMenuItem,
            this.Save_toolStripMenuItem,
            this.SaveAs_toolStripMenuItem,
            this.toolStripSeparator2,
            this.Close_ToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.menuToolStripMenuItem.Text = "Soubor";
            // 
            // Open_toolStripMenuItem
            // 
            this.Open_toolStripMenuItem.Name = "Open_toolStripMenuItem";
            this.Open_toolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.Open_toolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.Open_toolStripMenuItem.Text = "Otevřít...";
            this.Open_toolStripMenuItem.Click += new System.EventHandler(this.Open_toolStripMenuItem_Click);
            // 
            // Save_toolStripMenuItem
            // 
            this.Save_toolStripMenuItem.Name = "Save_toolStripMenuItem";
            this.Save_toolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.Save_toolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.Save_toolStripMenuItem.Text = "Uložit";
            this.Save_toolStripMenuItem.Click += new System.EventHandler(this.Save_toolStripMenuItem_Click);
            // 
            // SaveAs_toolStripMenuItem
            // 
            this.SaveAs_toolStripMenuItem.Name = "SaveAs_toolStripMenuItem";
            this.SaveAs_toolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.SaveAs_toolStripMenuItem.Text = "Uložit jako...";
            this.SaveAs_toolStripMenuItem.Click += new System.EventHandler(this.SaveAs_toolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // Close_ToolStripMenuItem
            // 
            this.Close_ToolStripMenuItem.Name = "Close_ToolStripMenuItem";
            this.Close_ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.Close_ToolStripMenuItem.Text = "Ukončit";
            this.Close_ToolStripMenuItem.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // úpravyToolStripMenuItem
            // 
            this.úpravyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add_toolStripMenuItem,
            this.delete_toolStripMenuItem,
            this.edit_toolStripMenuItem,
            this.toolStripSeparator3,
            this.clearedit_toolStripMenuItem,
            this.clear_all_toolStripMenuItem});
            this.úpravyToolStripMenuItem.Name = "úpravyToolStripMenuItem";
            this.úpravyToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.úpravyToolStripMenuItem.Text = "Úpravy";
            // 
            // add_toolStripMenuItem
            // 
            this.add_toolStripMenuItem.Name = "add_toolStripMenuItem";
            this.add_toolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.add_toolStripMenuItem.Text = "Přidat";
            this.add_toolStripMenuItem.Click += new System.EventHandler(this.Add_button_Click);
            // 
            // delete_toolStripMenuItem
            // 
            this.delete_toolStripMenuItem.Name = "delete_toolStripMenuItem";
            this.delete_toolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.delete_toolStripMenuItem.Text = "Smazat";
            this.delete_toolStripMenuItem.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // edit_toolStripMenuItem
            // 
            this.edit_toolStripMenuItem.Name = "edit_toolStripMenuItem";
            this.edit_toolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.edit_toolStripMenuItem.Text = "Zmenit";
            this.edit_toolStripMenuItem.Click += new System.EventHandler(this.change_button_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(190, 6);
            // 
            // clearedit_toolStripMenuItem
            // 
            this.clearedit_toolStripMenuItem.Name = "clearedit_toolStripMenuItem";
            this.clearedit_toolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.clearedit_toolStripMenuItem.Text = "Smazat Editace informací";
            this.clearedit_toolStripMenuItem.Click += new System.EventHandler(this.clearedit_toolStripMenuItem_Click);
            // 
            // clear_all_toolStripMenuItem
            // 
            this.clear_all_toolStripMenuItem.Name = "clear_all_toolStripMenuItem";
            this.clear_all_toolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.clear_all_toolStripMenuItem.Text = "Smazat vše";
            this.clear_all_toolStripMenuItem.Click += new System.EventHandler(this.clear_all_toolStripMenuItem_Click);
            // 
            // nápovědaToolStripMenuItem
            // 
            this.nápovědaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_zobraznapovedu,
            this.toolStripSeparator1,
            this.toolStripMenuItem2});
            this.nápovědaToolStripMenuItem.Name = "nápovědaToolStripMenuItem";
            this.nápovědaToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.nápovědaToolStripMenuItem.Text = "Nápověda";
            // 
            // tsmi_zobraznapovedu
            // 
            this.tsmi_zobraznapovedu.Name = "tsmi_zobraznapovedu";
            this.tsmi_zobraznapovedu.Size = new System.Drawing.Size(219, 22);
            this.tsmi_zobraznapovedu.Text = "Zobrazit nápovědu";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(216, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItem2.Text = "O programu : Program Version";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.uploadToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.testToolStripMenuItem.Text = "test";
            this.testToolStripMenuItem.Visible = false;
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.uploadToolStripMenuItem.Text = "Upload";
            this.uploadToolStripMenuItem.Click += new System.EventHandler(this.uploadToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.change_button);
            this.groupBox1.Controls.Add(this.Delete_button);
            this.groupBox1.Controls.Add(this.Add_button);
            this.groupBox1.Controls.Add(this.textBox_Message);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_URL);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_rev);
            this.groupBox1.Controls.Add(this.textBox_minor);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox_build);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox_major);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_Name);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 208);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(582, 195);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Editace infomaci";
            // 
            // change_button
            // 
            this.change_button.Location = new System.Drawing.Point(391, 199);
            this.change_button.Name = "change_button";
            this.change_button.Size = new System.Drawing.Size(188, 35);
            this.change_button.TabIndex = 5;
            this.change_button.Text = "Zmenit";
            this.change_button.UseVisualStyleBackColor = true;
            this.change_button.Click += new System.EventHandler(this.change_button_Click);
            // 
            // Delete_button
            // 
            this.Delete_button.Location = new System.Drawing.Point(197, 199);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(188, 35);
            this.Delete_button.TabIndex = 4;
            this.Delete_button.Text = "Smazat";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // Add_button
            // 
            this.Add_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Add_button.Location = new System.Drawing.Point(3, 199);
            this.Add_button.Name = "Add_button";
            this.Add_button.Size = new System.Drawing.Size(188, 35);
            this.Add_button.TabIndex = 3;
            this.Add_button.Text = "Přidat";
            this.Add_button.UseVisualStyleBackColor = true;
            this.Add_button.Click += new System.EventHandler(this.Add_button_Click);
            // 
            // textBox_Message
            // 
            this.textBox_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Message.Location = new System.Drawing.Point(357, 19);
            this.textBox_Message.Multiline = true;
            this.textBox_Message.Name = "textBox_Message";
            this.textBox_Message.Size = new System.Drawing.Size(184, 84);
            this.textBox_Message.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Správa :";
            // 
            // textBox_URL
            // 
            this.textBox_URL.Location = new System.Drawing.Point(127, 158);
            this.textBox_URL.Name = "textBox_URL";
            this.textBox_URL.Size = new System.Drawing.Size(414, 20);
            this.textBox_URL.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Cesta k souboru :";
            // 
            // textBox_rev
            // 
            this.textBox_rev.Location = new System.Drawing.Point(208, 132);
            this.textBox_rev.Name = "textBox_rev";
            this.textBox_rev.Size = new System.Drawing.Size(83, 20);
            this.textBox_rev.TabIndex = 2;
            // 
            // textBox_minor
            // 
            this.textBox_minor.Location = new System.Drawing.Point(208, 83);
            this.textBox_minor.Name = "textBox_minor";
            this.textBox_minor.Size = new System.Drawing.Size(83, 20);
            this.textBox_minor.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(156, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Revize :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(146, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Minoritná :";
            // 
            // textBox_build
            // 
            this.textBox_build.Location = new System.Drawing.Point(208, 106);
            this.textBox_build.Name = "textBox_build";
            this.textBox_build.Size = new System.Drawing.Size(83, 20);
            this.textBox_build.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(166, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Build :";
            // 
            // textBox_major
            // 
            this.textBox_major.Location = new System.Drawing.Point(208, 54);
            this.textBox_major.Name = "textBox_major";
            this.textBox_major.Size = new System.Drawing.Size(83, 20);
            this.textBox_major.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(146, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Majoritná :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Verze :";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(127, 19);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(164, 20);
            this.textBox_Name.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Název Programu :";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 426);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(582, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // VerzeProgramu_MST_Ctecka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 448);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dg_Info);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "VerzeProgramu_MST_Ctecka";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verze MST_W";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.bs_Info)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Info)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Info)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bs_Info;
        private ProgramVersion.DataSet.UpdateInfo ds_Info;
        private System.Windows.Forms.DataGridView dg_Info;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Close_ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_major;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Message;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_URL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_rev;
        private System.Windows.Forms.TextBox textBox_minor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_build;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem úpravyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nápovědaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Open_toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_zobraznapovedu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem Save_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAs_toolStripMenuItem;
        private System.Windows.Forms.Button Add_button;
        private System.Windows.Forms.Button change_button;
        private System.Windows.Forms.Button Delete_button;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripMenuItem add_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delete_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edit_toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem clearedit_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clear_all_toolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadToolStripMenuItem;
    }
}

