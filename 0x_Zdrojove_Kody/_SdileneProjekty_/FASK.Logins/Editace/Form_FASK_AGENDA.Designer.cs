namespace FASK.Logins.Editace
{
    partial class Form_FASK_AGENDA
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
            this.bs_Agenda = new System.Windows.Forms.BindingSource(this.components);
            this.ds_Agenda = new FASK.Logins.DataSets.Pristupy();
            this.bw_Agenda = new System.ComponentModel.BackgroundWorker();
            this.dg_Agenda = new System.Windows.Forms.DataGridView();
            this.aGENDAIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dESCIPTIONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aUTHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButtonsZobrazeniList = new System.Windows.Forms.Panel();
            this.btn_Konec2 = new System.Windows.Forms.Button();
            this.btn_Delete_Auth = new System.Windows.Forms.Button();
            this.btn_Edit_Auth = new System.Windows.Forms.Button();
            this.btn_Add_Auth = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_DESC = new System.Windows.Forms.TextBox();
            this.tb_NAME = new System.Windows.Forms.TextBox();
            this.tb_AGENDAID = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenuVyber = new System.Windows.Forms.ToolStripMenuItem();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.konecToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panelButtonsZobrazeniVyber = new System.Windows.Forms.Panel();
            this.btn_Konec1 = new System.Windows.Forms.Button();
            this.btn_AddPrava_Auth = new System.Windows.Forms.Button();
            this.progressIndicator_Auth = new ProgressControls.ProgressIndicator();
            ((System.ComponentModel.ISupportInitialize)(this.bs_Agenda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Agenda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Agenda)).BeginInit();
            this.panelButtonsZobrazeniList.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.panelButtonsZobrazeniVyber.SuspendLayout();
            this.SuspendLayout();
            // 
            // bs_Agenda
            // 
            this.bs_Agenda.DataMember = "FASK_AGENDA";
            this.bs_Agenda.DataSource = this.ds_Agenda;
            // 
            // ds_Agenda
            // 
            this.ds_Agenda.DataSetName = "Pristupy";
            this.ds_Agenda.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bw_Agenda
            // 
            this.bw_Agenda.WorkerSupportsCancellation = true;
            this.bw_Agenda.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Agenda_DoWork);
            this.bw_Agenda.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Agenda_RunWorkerCompleted);
            // 
            // dg_Agenda
            // 
            this.dg_Agenda.AllowUserToAddRows = false;
            this.dg_Agenda.AllowUserToDeleteRows = false;
            this.dg_Agenda.AllowUserToOrderColumns = true;
            this.dg_Agenda.AllowUserToResizeRows = false;
            this.dg_Agenda.AutoGenerateColumns = false;
            this.dg_Agenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Agenda.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aGENDAIDDataGridViewTextBoxColumn,
            this.nAMEDataGridViewTextBoxColumn,
            this.dESCIPTIONDataGridViewTextBoxColumn,
            this.aUTHDataGridViewTextBoxColumn});
            this.dg_Agenda.DataSource = this.bs_Agenda;
            this.dg_Agenda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_Agenda.Location = new System.Drawing.Point(0, 155);
            this.dg_Agenda.Name = "dg_Agenda";
            this.dg_Agenda.ReadOnly = true;
            this.dg_Agenda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Agenda.Size = new System.Drawing.Size(683, 361);
            this.dg_Agenda.TabIndex = 0;
            // 
            // aGENDAIDDataGridViewTextBoxColumn
            // 
            this.aGENDAIDDataGridViewTextBoxColumn.DataPropertyName = "AGENDAID";
            this.aGENDAIDDataGridViewTextBoxColumn.HeaderText = "ID Agendy";
            this.aGENDAIDDataGridViewTextBoxColumn.Name = "aGENDAIDDataGridViewTextBoxColumn";
            this.aGENDAIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.aGENDAIDDataGridViewTextBoxColumn.Width = 175;
            // 
            // nAMEDataGridViewTextBoxColumn
            // 
            this.nAMEDataGridViewTextBoxColumn.DataPropertyName = "NAME";
            this.nAMEDataGridViewTextBoxColumn.HeaderText = "Jméno";
            this.nAMEDataGridViewTextBoxColumn.Name = "nAMEDataGridViewTextBoxColumn";
            this.nAMEDataGridViewTextBoxColumn.ReadOnly = true;
            this.nAMEDataGridViewTextBoxColumn.Width = 175;
            // 
            // dESCIPTIONDataGridViewTextBoxColumn
            // 
            this.dESCIPTIONDataGridViewTextBoxColumn.DataPropertyName = "DESCIPTION";
            this.dESCIPTIONDataGridViewTextBoxColumn.HeaderText = "Popis";
            this.dESCIPTIONDataGridViewTextBoxColumn.Name = "dESCIPTIONDataGridViewTextBoxColumn";
            this.dESCIPTIONDataGridViewTextBoxColumn.ReadOnly = true;
            this.dESCIPTIONDataGridViewTextBoxColumn.Width = 175;
            // 
            // aUTHDataGridViewTextBoxColumn
            // 
            this.aUTHDataGridViewTextBoxColumn.DataPropertyName = "AUTH";
            this.aUTHDataGridViewTextBoxColumn.HeaderText = "Autorizace";
            this.aUTHDataGridViewTextBoxColumn.Name = "aUTHDataGridViewTextBoxColumn";
            this.aUTHDataGridViewTextBoxColumn.ReadOnly = true;
            this.aUTHDataGridViewTextBoxColumn.Width = 175;
            // 
            // panelButtonsZobrazeniList
            // 
            this.panelButtonsZobrazeniList.Controls.Add(this.btn_Konec2);
            this.panelButtonsZobrazeniList.Controls.Add(this.btn_Delete_Auth);
            this.panelButtonsZobrazeniList.Controls.Add(this.btn_Edit_Auth);
            this.panelButtonsZobrazeniList.Controls.Add(this.btn_Add_Auth);
            this.panelButtonsZobrazeniList.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniList.Location = new System.Drawing.Point(775, 24);
            this.panelButtonsZobrazeniList.Name = "panelButtonsZobrazeniList";
            this.panelButtonsZobrazeniList.Size = new System.Drawing.Size(85, 492);
            this.panelButtonsZobrazeniList.TabIndex = 1;
            // 
            // btn_Konec2
            // 
            this.btn_Konec2.Location = new System.Drawing.Point(6, 440);
            this.btn_Konec2.Name = "btn_Konec2";
            this.btn_Konec2.Size = new System.Drawing.Size(73, 40);
            this.btn_Konec2.TabIndex = 3;
            this.btn_Konec2.Text = "Konec";
            this.btn_Konec2.UseVisualStyleBackColor = true;
            this.btn_Konec2.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // btn_Delete_Auth
            // 
            this.btn_Delete_Auth.Location = new System.Drawing.Point(6, 107);
            this.btn_Delete_Auth.Name = "btn_Delete_Auth";
            this.btn_Delete_Auth.Size = new System.Drawing.Size(73, 40);
            this.btn_Delete_Auth.TabIndex = 2;
            this.btn_Delete_Auth.Text = "Odstranit právo";
            this.btn_Delete_Auth.UseVisualStyleBackColor = true;
            this.btn_Delete_Auth.Click += new System.EventHandler(this.btn_Delete_Auth_Click);
            // 
            // btn_Edit_Auth
            // 
            this.btn_Edit_Auth.Location = new System.Drawing.Point(6, 61);
            this.btn_Edit_Auth.Name = "btn_Edit_Auth";
            this.btn_Edit_Auth.Size = new System.Drawing.Size(73, 40);
            this.btn_Edit_Auth.TabIndex = 1;
            this.btn_Edit_Auth.Text = "Upravit právo";
            this.btn_Edit_Auth.UseVisualStyleBackColor = true;
            this.btn_Edit_Auth.Click += new System.EventHandler(this.btn_Edit_Auth_Click);
            // 
            // btn_Add_Auth
            // 
            this.btn_Add_Auth.Location = new System.Drawing.Point(6, 15);
            this.btn_Add_Auth.Name = "btn_Add_Auth";
            this.btn_Add_Auth.Size = new System.Drawing.Size(73, 40);
            this.btn_Add_Auth.TabIndex = 0;
            this.btn_Add_Auth.Text = "Přidat právo";
            this.btn_Add_Auth.UseVisualStyleBackColor = true;
            this.btn_Add_Auth.Click += new System.EventHandler(this.btn_Add_Auth_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.tb_DESC);
            this.panel2.Controls.Add(this.tb_NAME);
            this.panel2.Controls.Add(this.tb_AGENDAID);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(683, 131);
            this.panel2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(282, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Popis:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Jméno:";
            // 
            // tb_DESC
            // 
            this.tb_DESC.Location = new System.Drawing.Point(326, 42);
            this.tb_DESC.Name = "tb_DESC";
            this.tb_DESC.Size = new System.Drawing.Size(134, 20);
            this.tb_DESC.TabIndex = 2;
            // 
            // tb_NAME
            // 
            this.tb_NAME.Location = new System.Drawing.Point(326, 15);
            this.tb_NAME.Name = "tb_NAME";
            this.tb_NAME.Size = new System.Drawing.Size(134, 20);
            this.tb_NAME.TabIndex = 1;
            // 
            // tb_AGENDAID
            // 
            this.tb_AGENDAID.Location = new System.Drawing.Point(80, 15);
            this.tb_AGENDAID.Multiline = true;
            this.tb_AGENDAID.Name = "tb_AGENDAID";
            this.tb_AGENDAID.Size = new System.Drawing.Size(190, 98);
            this.tb_AGENDAID.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(486, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 63);
            this.button1.TabIndex = 3;
            this.button1.Text = "Vyhledat";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID Agendy :";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenuVyber,
            this.tsmiMenuList});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(860, 24);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // tsmiMenuVyber
            // 
            this.tsmiMenuVyber.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konecToolStripMenuItem});
            this.tsmiMenuVyber.Name = "tsmiMenuVyber";
            this.tsmiMenuVyber.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenuVyber.Text = "Menu";
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.konecToolStripMenuItem.Text = "Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // tsmiMenuList
            // 
            this.tsmiMenuList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konecToolStripMenuItem1});
            this.tsmiMenuList.Name = "tsmiMenuList";
            this.tsmiMenuList.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenuList.Text = "Menu";
            // 
            // konecToolStripMenuItem1
            // 
            this.konecToolStripMenuItem1.Name = "konecToolStripMenuItem1";
            this.konecToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.konecToolStripMenuItem1.Text = "Konec";
            // 
            // panelButtonsZobrazeniVyber
            // 
            this.panelButtonsZobrazeniVyber.Controls.Add(this.btn_Konec1);
            this.panelButtonsZobrazeniVyber.Controls.Add(this.btn_AddPrava_Auth);
            this.panelButtonsZobrazeniVyber.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtonsZobrazeniVyber.Location = new System.Drawing.Point(683, 24);
            this.panelButtonsZobrazeniVyber.Name = "panelButtonsZobrazeniVyber";
            this.panelButtonsZobrazeniVyber.Size = new System.Drawing.Size(92, 492);
            this.panelButtonsZobrazeniVyber.TabIndex = 4;
            // 
            // btn_Konec1
            // 
            this.btn_Konec1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Konec1.Location = new System.Drawing.Point(6, 440);
            this.btn_Konec1.Name = "btn_Konec1";
            this.btn_Konec1.Size = new System.Drawing.Size(73, 40);
            this.btn_Konec1.TabIndex = 1;
            this.btn_Konec1.Text = "Konec";
            this.btn_Konec1.UseVisualStyleBackColor = true;
            this.btn_Konec1.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // btn_AddPrava_Auth
            // 
            this.btn_AddPrava_Auth.Location = new System.Drawing.Point(6, 15);
            this.btn_AddPrava_Auth.Name = "btn_AddPrava_Auth";
            this.btn_AddPrava_Auth.Size = new System.Drawing.Size(73, 58);
            this.btn_AddPrava_Auth.TabIndex = 0;
            this.btn_AddPrava_Auth.Text = "Přidat právo/ práva";
            this.btn_AddPrava_Auth.UseVisualStyleBackColor = true;
            this.btn_AddPrava_Auth.Click += new System.EventHandler(this.btn_AddPrava_Auth_Click);
            // 
            // progressIndicator_Auth
            // 
            this.progressIndicator_Auth.Location = new System.Drawing.Point(326, 268);
            this.progressIndicator_Auth.Name = "progressIndicator_Auth";
            this.progressIndicator_Auth.Percentage = 0F;
            this.progressIndicator_Auth.Size = new System.Drawing.Size(98, 98);
            this.progressIndicator_Auth.TabIndex = 40;
            this.progressIndicator_Auth.Text = "progressIndicator1";
            this.progressIndicator_Auth.Visible = false;
            // 
            // Form_FASK_AGENDA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 516);
            this.Controls.Add(this.progressIndicator_Auth);
            this.Controls.Add(this.dg_Agenda);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelButtonsZobrazeniVyber);
            this.Controls.Add(this.panelButtonsZobrazeniList);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "Form_FASK_AGENDA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Přehled práv";
            this.Load += new System.EventHandler(this.Form_FASK_AGENDA_Load);
            this.Shown += new System.EventHandler(this.Form_FASK_AGENDA_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.bs_Agenda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Agenda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Agenda)).EndInit();
            this.panelButtonsZobrazeniList.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panelButtonsZobrazeniVyber.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bs_Agenda;
        private System.ComponentModel.BackgroundWorker bw_Agenda;
        private DataSets.Pristupy ds_Agenda;
        private System.Windows.Forms.Panel panelButtonsZobrazeniList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuVyber;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private System.Windows.Forms.Panel panelButtonsZobrazeniVyber;
        private System.Windows.Forms.Button btn_Delete_Auth;
        private System.Windows.Forms.Button btn_Edit_Auth;
        private System.Windows.Forms.Button btn_Add_Auth;
        private System.Windows.Forms.Button btn_Konec2;
        private System.Windows.Forms.Button btn_Konec1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuList;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem1;
        private System.Windows.Forms.Button btn_AddPrava_Auth;
        private System.Windows.Forms.DataGridViewTextBoxColumn aGENDAIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dESCIPTIONDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aUTHDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox tb_AGENDAID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_DESC;
        private System.Windows.Forms.TextBox tb_NAME;
        private ProgressControls.ProgressIndicator progressIndicator_Auth;
        public System.Windows.Forms.DataGridView dg_Agenda;
    }
}