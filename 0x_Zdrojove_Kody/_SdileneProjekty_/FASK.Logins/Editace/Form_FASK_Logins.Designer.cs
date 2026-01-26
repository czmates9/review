namespace FASK.Logins.Editace
{
    partial class Form_FASK_Logins
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.progressIndicator_Login = new ProgressControls.ProgressIndicator();
            this.dg_Logins = new System.Windows.Forms.DataGridView();
            this.uSERIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.surnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.psswdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cREATEDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vALIDFROMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vALIDTODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RFID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_Logins = new System.Windows.Forms.BindingSource(this.components);
            this.ds_Logins = new FASK.Logins.DataSets.Pristupy();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtp_ValidTo = new System.Windows.Forms.DateTimePicker();
            this.dtp_ValidFrom = new System.Windows.Forms.DateTimePicker();
            this.dtp_Create = new System.Windows.Forms.DateTimePicker();
            this.tb_SurName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_FirstName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_USERID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Filtr_Logins = new System.Windows.Forms.Button();
            this.progressIndicator_Auth = new ProgressControls.ProgressIndicator();
            this.dg_Auth = new System.Windows.Forms.DataGridView();
            this.dEXROWIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aGENDAIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aUTHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_Auth = new System.Windows.Forms.BindingSource(this.components);
            this.ds_Auth = new FASK.Logins.DataSets.Pristupy();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_Filtr_Auth = new System.Windows.Forms.Button();
            this.tb_IDAgendy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_Konec = new System.Windows.Forms.Button();
            this.btn_Edit_Prava = new System.Windows.Forms.Button();
            this.btn_Delete_Auth = new System.Windows.Forms.Button();
            this.btn_Delete_Login = new System.Windows.Forms.Button();
            this.btn_Edit_Login = new System.Windows.Forms.Button();
            this.btn_Add_Auth = new System.Windows.Forms.Button();
            this.btn_Add_Login = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bw_Login = new System.ComponentModel.BackgroundWorker();
            this.bw_Auth = new System.ComponentModel.BackgroundWorker();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Uziv_Import = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Logins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Logins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Logins)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Auth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Auth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Auth)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.progressIndicator_Login);
            this.splitContainer1.Panel1.Controls.Add(this.dg_Logins);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.progressIndicator_Auth);
            this.splitContainer1.Panel2.Controls.Add(this.dg_Auth);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(735, 650);
            this.splitContainer1.SplitterDistance = 377;
            this.splitContainer1.TabIndex = 0;
            // 
            // progressIndicator_Login
            // 
            this.progressIndicator_Login.Location = new System.Drawing.Point(147, 312);
            this.progressIndicator_Login.Name = "progressIndicator_Login";
            this.progressIndicator_Login.Percentage = 0F;
            this.progressIndicator_Login.Size = new System.Drawing.Size(98, 98);
            this.progressIndicator_Login.TabIndex = 39;
            this.progressIndicator_Login.Text = "progressIndicator1";
            this.progressIndicator_Login.Visible = false;
            // 
            // dg_Logins
            // 
            this.dg_Logins.AllowUserToAddRows = false;
            this.dg_Logins.AllowUserToDeleteRows = false;
            this.dg_Logins.AllowUserToOrderColumns = true;
            this.dg_Logins.AllowUserToResizeRows = false;
            this.dg_Logins.AutoGenerateColumns = false;
            this.dg_Logins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Logins.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uSERIDDataGridViewTextBoxColumn,
            this.firstnameDataGridViewTextBoxColumn,
            this.surnameDataGridViewTextBoxColumn,
            this.psswdDataGridViewTextBoxColumn,
            this.cREATEDDataGridViewTextBoxColumn,
            this.vALIDFROMDataGridViewTextBoxColumn,
            this.vALIDTODataGridViewTextBoxColumn,
            this.RFID});
            this.dg_Logins.DataSource = this.bs_Logins;
            this.dg_Logins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Logins.Location = new System.Drawing.Point(0, 202);
            this.dg_Logins.MultiSelect = false;
            this.dg_Logins.Name = "dg_Logins";
            this.dg_Logins.ReadOnly = true;
            this.dg_Logins.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Logins.Size = new System.Drawing.Size(377, 448);
            this.dg_Logins.TabIndex = 0;
            this.dg_Logins.SelectionChanged += new System.EventHandler(this.dg_Logins_SelectionChanged);
            // 
            // uSERIDDataGridViewTextBoxColumn
            // 
            this.uSERIDDataGridViewTextBoxColumn.DataPropertyName = "USERID";
            this.uSERIDDataGridViewTextBoxColumn.HeaderText = "ID Uživatele";
            this.uSERIDDataGridViewTextBoxColumn.Name = "uSERIDDataGridViewTextBoxColumn";
            this.uSERIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firstnameDataGridViewTextBoxColumn
            // 
            this.firstnameDataGridViewTextBoxColumn.DataPropertyName = "firstname";
            this.firstnameDataGridViewTextBoxColumn.HeaderText = "Jméno";
            this.firstnameDataGridViewTextBoxColumn.Name = "firstnameDataGridViewTextBoxColumn";
            this.firstnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // surnameDataGridViewTextBoxColumn
            // 
            this.surnameDataGridViewTextBoxColumn.DataPropertyName = "surname";
            this.surnameDataGridViewTextBoxColumn.HeaderText = "Příjmení";
            this.surnameDataGridViewTextBoxColumn.Name = "surnameDataGridViewTextBoxColumn";
            this.surnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // psswdDataGridViewTextBoxColumn
            // 
            this.psswdDataGridViewTextBoxColumn.DataPropertyName = "psswd";
            this.psswdDataGridViewTextBoxColumn.HeaderText = "Heslo";
            this.psswdDataGridViewTextBoxColumn.Name = "psswdDataGridViewTextBoxColumn";
            this.psswdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cREATEDDataGridViewTextBoxColumn
            // 
            this.cREATEDDataGridViewTextBoxColumn.DataPropertyName = "CREATED";
            this.cREATEDDataGridViewTextBoxColumn.HeaderText = "Založeno";
            this.cREATEDDataGridViewTextBoxColumn.Name = "cREATEDDataGridViewTextBoxColumn";
            this.cREATEDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vALIDFROMDataGridViewTextBoxColumn
            // 
            this.vALIDFROMDataGridViewTextBoxColumn.DataPropertyName = "VALIDFROM";
            this.vALIDFROMDataGridViewTextBoxColumn.HeaderText = "Platný od";
            this.vALIDFROMDataGridViewTextBoxColumn.Name = "vALIDFROMDataGridViewTextBoxColumn";
            this.vALIDFROMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vALIDTODataGridViewTextBoxColumn
            // 
            this.vALIDTODataGridViewTextBoxColumn.DataPropertyName = "VALIDTO";
            this.vALIDTODataGridViewTextBoxColumn.HeaderText = "Platný do";
            this.vALIDTODataGridViewTextBoxColumn.Name = "vALIDTODataGridViewTextBoxColumn";
            this.vALIDTODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // RFID
            // 
            this.RFID.DataPropertyName = "RFID";
            this.RFID.HeaderText = "RFID";
            this.RFID.Name = "RFID";
            this.RFID.ReadOnly = true;
            // 
            // bs_Logins
            // 
            this.bs_Logins.DataMember = "FASK_Logins";
            this.bs_Logins.DataSource = this.ds_Logins;
            // 
            // ds_Logins
            // 
            this.ds_Logins.DataSetName = "Pristupy";
            this.ds_Logins.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtp_ValidTo);
            this.panel2.Controls.Add(this.dtp_ValidFrom);
            this.panel2.Controls.Add(this.dtp_Create);
            this.panel2.Controls.Add(this.tb_SurName);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tb_FirstName);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tb_USERID);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btn_Filtr_Logins);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(377, 202);
            this.panel2.TabIndex = 0;
            // 
            // dtp_ValidTo
            // 
            this.dtp_ValidTo.Checked = false;
            this.dtp_ValidTo.Location = new System.Drawing.Point(90, 158);
            this.dtp_ValidTo.Name = "dtp_ValidTo";
            this.dtp_ValidTo.ShowCheckBox = true;
            this.dtp_ValidTo.Size = new System.Drawing.Size(200, 20);
            this.dtp_ValidTo.TabIndex = 5;
            // 
            // dtp_ValidFrom
            // 
            this.dtp_ValidFrom.Checked = false;
            this.dtp_ValidFrom.Location = new System.Drawing.Point(90, 132);
            this.dtp_ValidFrom.Name = "dtp_ValidFrom";
            this.dtp_ValidFrom.ShowCheckBox = true;
            this.dtp_ValidFrom.Size = new System.Drawing.Size(200, 20);
            this.dtp_ValidFrom.TabIndex = 4;
            // 
            // dtp_Create
            // 
            this.dtp_Create.Checked = false;
            this.dtp_Create.Location = new System.Drawing.Point(90, 106);
            this.dtp_Create.Name = "dtp_Create";
            this.dtp_Create.ShowCheckBox = true;
            this.dtp_Create.Size = new System.Drawing.Size(200, 20);
            this.dtp_Create.TabIndex = 3;
            // 
            // tb_SurName
            // 
            this.tb_SurName.Location = new System.Drawing.Point(90, 71);
            this.tb_SurName.Name = "tb_SurName";
            this.tb_SurName.Size = new System.Drawing.Size(162, 20);
            this.tb_SurName.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Platný do";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Příjmení";
            // 
            // tb_FirstName
            // 
            this.tb_FirstName.Location = new System.Drawing.Point(90, 45);
            this.tb_FirstName.Name = "tb_FirstName";
            this.tb_FirstName.Size = new System.Drawing.Size(162, 20);
            this.tb_FirstName.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Platný od";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Jméno";
            // 
            // tb_USERID
            // 
            this.tb_USERID.Location = new System.Drawing.Point(90, 20);
            this.tb_USERID.Name = "tb_USERID";
            this.tb_USERID.Size = new System.Drawing.Size(162, 20);
            this.tb_USERID.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Založeno";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID uživatele";
            // 
            // btn_Filtr_Logins
            // 
            this.btn_Filtr_Logins.Location = new System.Drawing.Point(275, 20);
            this.btn_Filtr_Logins.Margin = new System.Windows.Forms.Padding(20);
            this.btn_Filtr_Logins.Name = "btn_Filtr_Logins";
            this.btn_Filtr_Logins.Size = new System.Drawing.Size(82, 78);
            this.btn_Filtr_Logins.TabIndex = 6;
            this.btn_Filtr_Logins.Text = "Vyhledat";
            this.btn_Filtr_Logins.UseVisualStyleBackColor = true;
            this.btn_Filtr_Logins.Click += new System.EventHandler(this.btn_Filtr_Logins_Click);
            // 
            // progressIndicator_Auth
            // 
            this.progressIndicator_Auth.Location = new System.Drawing.Point(116, 312);
            this.progressIndicator_Auth.Name = "progressIndicator_Auth";
            this.progressIndicator_Auth.Percentage = 0F;
            this.progressIndicator_Auth.Size = new System.Drawing.Size(98, 98);
            this.progressIndicator_Auth.TabIndex = 39;
            this.progressIndicator_Auth.Text = "progressIndicator1";
            this.progressIndicator_Auth.Visible = false;
            // 
            // dg_Auth
            // 
            this.dg_Auth.AllowUserToAddRows = false;
            this.dg_Auth.AllowUserToDeleteRows = false;
            this.dg_Auth.AllowUserToResizeRows = false;
            this.dg_Auth.AutoGenerateColumns = false;
            this.dg_Auth.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Auth.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dEXROWIDDataGridViewTextBoxColumn,
            this.aGENDAIDDataGridViewTextBoxColumn,
            this.aUTHDataGridViewTextBoxColumn});
            this.dg_Auth.DataSource = this.bs_Auth;
            this.dg_Auth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Auth.Location = new System.Drawing.Point(0, 202);
            this.dg_Auth.Name = "dg_Auth";
            this.dg_Auth.ReadOnly = true;
            this.dg_Auth.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Auth.Size = new System.Drawing.Size(354, 448);
            this.dg_Auth.TabIndex = 0;
            // 
            // dEXROWIDDataGridViewTextBoxColumn
            // 
            this.dEXROWIDDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID";
            this.dEXROWIDDataGridViewTextBoxColumn.HeaderText = "Index";
            this.dEXROWIDDataGridViewTextBoxColumn.Name = "dEXROWIDDataGridViewTextBoxColumn";
            this.dEXROWIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // aGENDAIDDataGridViewTextBoxColumn
            // 
            this.aGENDAIDDataGridViewTextBoxColumn.DataPropertyName = "AGENDAID";
            this.aGENDAIDDataGridViewTextBoxColumn.HeaderText = "ID Agendy";
            this.aGENDAIDDataGridViewTextBoxColumn.Name = "aGENDAIDDataGridViewTextBoxColumn";
            this.aGENDAIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // aUTHDataGridViewTextBoxColumn
            // 
            this.aUTHDataGridViewTextBoxColumn.DataPropertyName = "AUTH";
            this.aUTHDataGridViewTextBoxColumn.HeaderText = "Autorizace";
            this.aUTHDataGridViewTextBoxColumn.Name = "aUTHDataGridViewTextBoxColumn";
            this.aUTHDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bs_Auth
            // 
            this.bs_Auth.AllowNew = true;
            this.bs_Auth.DataMember = "FASK_Logins_Auth";
            this.bs_Auth.DataSource = this.ds_Auth;
            // 
            // ds_Auth
            // 
            this.ds_Auth.DataSetName = "Pristupy";
            this.ds_Auth.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_Filtr_Auth);
            this.panel3.Controls.Add(this.tb_IDAgendy);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(354, 202);
            this.panel3.TabIndex = 0;
            // 
            // btn_Filtr_Auth
            // 
            this.btn_Filtr_Auth.Location = new System.Drawing.Point(252, 20);
            this.btn_Filtr_Auth.Margin = new System.Windows.Forms.Padding(20);
            this.btn_Filtr_Auth.Name = "btn_Filtr_Auth";
            this.btn_Filtr_Auth.Size = new System.Drawing.Size(82, 78);
            this.btn_Filtr_Auth.TabIndex = 1;
            this.btn_Filtr_Auth.Text = "Vyhledat";
            this.btn_Filtr_Auth.UseVisualStyleBackColor = true;
            this.btn_Filtr_Auth.Click += new System.EventHandler(this.btn_Filtr_Auth_Click);
            // 
            // tb_IDAgendy
            // 
            this.tb_IDAgendy.Location = new System.Drawing.Point(84, 23);
            this.tb_IDAgendy.Multiline = true;
            this.tb_IDAgendy.Name = "tb_IDAgendy";
            this.tb_IDAgendy.Size = new System.Drawing.Size(162, 75);
            this.tb_IDAgendy.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ID Agendy";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Uziv_Import);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.btn_Konec);
            this.panel1.Controls.Add(this.btn_Edit_Prava);
            this.panel1.Controls.Add(this.btn_Delete_Auth);
            this.panel1.Controls.Add(this.btn_Delete_Login);
            this.panel1.Controls.Add(this.btn_Edit_Login);
            this.panel1.Controls.Add(this.btn_Add_Auth);
            this.panel1.Controls.Add(this.btn_Add_Login);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(735, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(84, 674);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(6, 261);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(73, 10);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(6, 150);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(73, 10);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            // 
            // btn_Konec
            // 
            this.btn_Konec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Konec.Location = new System.Drawing.Point(6, 622);
            this.btn_Konec.Name = "btn_Konec";
            this.btn_Konec.Size = new System.Drawing.Size(73, 40);
            this.btn_Konec.TabIndex = 6;
            this.btn_Konec.Text = "Konec";
            this.btn_Konec.UseVisualStyleBackColor = true;
            this.btn_Konec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // btn_Edit_Prava
            // 
            this.btn_Edit_Prava.Location = new System.Drawing.Point(6, 280);
            this.btn_Edit_Prava.Name = "btn_Edit_Prava";
            this.btn_Edit_Prava.Size = new System.Drawing.Size(73, 40);
            this.btn_Edit_Prava.TabIndex = 5;
            this.btn_Edit_Prava.Text = " Úprava práva";
            this.btn_Edit_Prava.UseVisualStyleBackColor = true;
            this.btn_Edit_Prava.Visible = false;
            this.btn_Edit_Prava.Click += new System.EventHandler(this.btn_Edit_Prava_Click);
            // 
            // btn_Delete_Auth
            // 
            this.btn_Delete_Auth.Location = new System.Drawing.Point(6, 215);
            this.btn_Delete_Auth.Name = "btn_Delete_Auth";
            this.btn_Delete_Auth.Size = new System.Drawing.Size(73, 40);
            this.btn_Delete_Auth.TabIndex = 4;
            this.btn_Delete_Auth.Text = "Odstranit práva";
            this.btn_Delete_Auth.UseVisualStyleBackColor = true;
            this.btn_Delete_Auth.Click += new System.EventHandler(this.btn_Delete_Auth_Click);
            // 
            // btn_Delete_Login
            // 
            this.btn_Delete_Login.Location = new System.Drawing.Point(6, 104);
            this.btn_Delete_Login.Name = "btn_Delete_Login";
            this.btn_Delete_Login.Size = new System.Drawing.Size(73, 40);
            this.btn_Delete_Login.TabIndex = 2;
            this.btn_Delete_Login.Text = "Odstranit uživatele";
            this.btn_Delete_Login.UseVisualStyleBackColor = true;
            this.btn_Delete_Login.Click += new System.EventHandler(this.btn_Delete_Login_Click);
            // 
            // btn_Edit_Login
            // 
            this.btn_Edit_Login.Location = new System.Drawing.Point(6, 58);
            this.btn_Edit_Login.Name = "btn_Edit_Login";
            this.btn_Edit_Login.Size = new System.Drawing.Size(73, 40);
            this.btn_Edit_Login.TabIndex = 1;
            this.btn_Edit_Login.Text = "Upravit uživatele";
            this.btn_Edit_Login.UseVisualStyleBackColor = true;
            this.btn_Edit_Login.Click += new System.EventHandler(this.btn_Edit_Login_Click);
            // 
            // btn_Add_Auth
            // 
            this.btn_Add_Auth.Location = new System.Drawing.Point(6, 169);
            this.btn_Add_Auth.Name = "btn_Add_Auth";
            this.btn_Add_Auth.Size = new System.Drawing.Size(73, 40);
            this.btn_Add_Auth.TabIndex = 3;
            this.btn_Add_Auth.Text = "Přidat práva";
            this.btn_Add_Auth.UseVisualStyleBackColor = true;
            this.btn_Add_Auth.Click += new System.EventHandler(this.btn_Add_Auth_Click);
            // 
            // btn_Add_Login
            // 
            this.btn_Add_Login.Location = new System.Drawing.Point(6, 12);
            this.btn_Add_Login.Name = "btn_Add_Login";
            this.btn_Add_Login.Size = new System.Drawing.Size(73, 40);
            this.btn_Add_Login.TabIndex = 0;
            this.btn_Add_Login.Text = "Přidat uživatele";
            this.btn_Add_Login.UseVisualStyleBackColor = true;
            this.btn_Add_Login.Click += new System.EventHandler(this.btn_Add_Login_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(735, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konecToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.konecToolStripMenuItem.Text = "Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // bw_Login
            // 
            this.bw_Login.WorkerSupportsCancellation = true;
            this.bw_Login.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Login_DoWork);
            this.bw_Login.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Login_RunWorkerCompleted);
            // 
            // bw_Auth
            // 
            this.bw_Auth.WorkerSupportsCancellation = true;
            this.bw_Auth.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Auth_DoWork);
            this.bw_Auth.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Auth_RunWorkerCompleted);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "USERID";
            this.dataGridViewTextBoxColumn1.HeaderText = "ID Uživatele";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "firstname";
            this.dataGridViewTextBoxColumn2.HeaderText = "Jméno";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "surname";
            this.dataGridViewTextBoxColumn3.HeaderText = "Příjmení";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "psswd";
            this.dataGridViewTextBoxColumn4.HeaderText = "Heslo";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "CREATED";
            this.dataGridViewTextBoxColumn5.HeaderText = "Založeno";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "VALIDFROM";
            this.dataGridViewTextBoxColumn6.HeaderText = "Platný od";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "VALIDTO";
            this.dataGridViewTextBoxColumn7.HeaderText = "Platný do";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "DEX_ROW_ID";
            this.dataGridViewTextBoxColumn8.HeaderText = "Index";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "AGENDAID";
            this.dataGridViewTextBoxColumn9.HeaderText = "ID Agendy";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "AUTH";
            this.dataGridViewTextBoxColumn10.HeaderText = "Autorizace";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // btn_Uziv_Import
            // 
            this.btn_Uziv_Import.Location = new System.Drawing.Point(6, 326);
            this.btn_Uziv_Import.Name = "btn_Uziv_Import";
            this.btn_Uziv_Import.Size = new System.Drawing.Size(73, 40);
            this.btn_Uziv_Import.TabIndex = 13;
            this.btn_Uziv_Import.Text = " Import";
            this.btn_Uziv_Import.UseVisualStyleBackColor = true;
            this.btn_Uziv_Import.Visible = false;
            this.btn_Uziv_Import.Click += new System.EventHandler(this.btn_Uziv_Import_Click);
            // 
            // Form_FASK_Logins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 674);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_FASK_Logins";
            this.Text = "Uživatelé";
            this.Load += new System.EventHandler(this.Form_FASK_Logins_Load);
            this.Shown += new System.EventHandler(this.Form_FASK_Logins_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Logins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Logins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Logins)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Auth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Auth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Auth)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private System.Windows.Forms.DataGridView dg_Logins;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource bs_Logins;
        private DataSets.Pristupy ds_Logins;
        private System.Windows.Forms.DataGridView dg_Auth;
        private System.Windows.Forms.BindingSource bs_Auth;
        private DataSets.Pristupy ds_Auth;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_Add_Login;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_Delete_Login;
        private System.Windows.Forms.Button btn_Edit_Login;
        private System.Windows.Forms.Button btn_Konec;
        private System.Windows.Forms.Button btn_Delete_Auth;
        private System.Windows.Forms.Button btn_Add_Auth;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aGENDAIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aUTHDataGridViewTextBoxColumn;
        private ProgressControls.ProgressIndicator progressIndicator_Login;
        private ProgressControls.ProgressIndicator progressIndicator_Auth;
        private System.ComponentModel.BackgroundWorker bw_Login;
        private System.ComponentModel.BackgroundWorker bw_Auth;
        private System.Windows.Forms.Button btn_Filtr_Logins;
        private System.Windows.Forms.Button btn_Filtr_Auth;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DateTimePicker dtp_ValidTo;
        private System.Windows.Forms.DateTimePicker dtp_ValidFrom;
        private System.Windows.Forms.DateTimePicker dtp_Create;
        private System.Windows.Forms.TextBox tb_SurName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_FirstName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_USERID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_IDAgendy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Edit_Prava;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn surnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn psswdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cREATEDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vALIDFROMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vALIDTODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RFID;
        private System.Windows.Forms.Button btn_Uziv_Import;
    }
}