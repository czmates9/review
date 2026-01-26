namespace Definition_SQL_Struncture
{
    partial class Definice_Dokumentace
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
            this.panel_Filtr = new System.Windows.Forms.Panel();
            this.cb_Type_Note = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_Type = new System.Windows.Forms.ComboBox();
            this.btn_ClearFilter = new System.Windows.Forms.Button();
            this.tb_TableName = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.tb_ColumnName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dg_dok = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Schema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_dok = new System.Windows.Forms.BindingSource(this.components);
            this.ds_dok = new Definition_SQL_Struncture.Dokumentace.Dok();
            this.panel_button = new System.Windows.Forms.Panel();
            this.btn_Gen_HTML = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Edit = new System.Windows.Forms.Button();
            this.panel_Filtr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_dok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_dok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_dok)).BeginInit();
            this.panel_button.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Filtr
            // 
            this.panel_Filtr.Controls.Add(this.cb_Type_Note);
            this.panel_Filtr.Controls.Add(this.label1);
            this.panel_Filtr.Controls.Add(this.label3);
            this.panel_Filtr.Controls.Add(this.cb_Type);
            this.panel_Filtr.Controls.Add(this.btn_ClearFilter);
            this.panel_Filtr.Controls.Add(this.tb_TableName);
            this.panel_Filtr.Controls.Add(this.btn_Search);
            this.panel_Filtr.Controls.Add(this.tb_ColumnName);
            this.panel_Filtr.Controls.Add(this.label4);
            this.panel_Filtr.Controls.Add(this.label2);
            this.panel_Filtr.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Filtr.Location = new System.Drawing.Point(0, 0);
            this.panel_Filtr.Name = "panel_Filtr";
            this.panel_Filtr.Size = new System.Drawing.Size(917, 159);
            this.panel_Filtr.TabIndex = 14;
            // 
            // cb_Type_Note
            // 
            this.cb_Type_Note.FormattingEnabled = true;
            this.cb_Type_Note.Location = new System.Drawing.Point(130, 112);
            this.cb_Type_Note.Name = "cb_Type_Note";
            this.cb_Type_Note.Size = new System.Drawing.Size(172, 21);
            this.cb_Type_Note.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(27, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Typ poznamky";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(25, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Jmeno tabulky";
            // 
            // cb_Type
            // 
            this.cb_Type.FormattingEnabled = true;
            this.cb_Type.Location = new System.Drawing.Point(130, 71);
            this.cb_Type.Name = "cb_Type";
            this.cb_Type.Size = new System.Drawing.Size(172, 21);
            this.cb_Type.TabIndex = 2;
            // 
            // btn_ClearFilter
            // 
            this.btn_ClearFilter.Location = new System.Drawing.Point(431, 17);
            this.btn_ClearFilter.Name = "btn_ClearFilter";
            this.btn_ClearFilter.Size = new System.Drawing.Size(111, 75);
            this.btn_ClearFilter.TabIndex = 4;
            this.btn_ClearFilter.Text = "Vyčistit filtry";
            this.btn_ClearFilter.UseVisualStyleBackColor = true;
            this.btn_ClearFilter.Click += new System.EventHandler(this.btn_ClearFilter_Click);
            // 
            // tb_TableName
            // 
            this.tb_TableName.Location = new System.Drawing.Point(130, 19);
            this.tb_TableName.Name = "tb_TableName";
            this.tb_TableName.Size = new System.Drawing.Size(172, 20);
            this.tb_TableName.TabIndex = 0;
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(308, 17);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(117, 75);
            this.btn_Search.TabIndex = 3;
            this.btn_Search.Text = "Hledej";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // tb_ColumnName
            // 
            this.tb_ColumnName.Location = new System.Drawing.Point(130, 45);
            this.tb_ColumnName.Name = "tb_ColumnName";
            this.tb_ColumnName.Size = new System.Drawing.Size(172, 20);
            this.tb_ColumnName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(16, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Jmeno stloupce";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(53, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Datovy typ";
            // 
            // dg_dok
            // 
            this.dg_dok.AllowUserToAddRows = false;
            this.dg_dok.AllowUserToDeleteRows = false;
            this.dg_dok.AllowUserToOrderColumns = true;
            this.dg_dok.AllowUserToResizeRows = false;
            this.dg_dok.AutoGenerateColumns = false;
            this.dg_dok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_dok.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Schema,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            this.dg_dok.DataSource = this.bs_dok;
            this.dg_dok.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_dok.Location = new System.Drawing.Point(0, 159);
            this.dg_dok.Name = "dg_dok";
            this.dg_dok.ReadOnly = true;
            this.dg_dok.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_dok.Size = new System.Drawing.Size(917, 498);
            this.dg_dok.TabIndex = 15;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "TABLE";
            this.dataGridViewTextBoxColumn1.HeaderText = "Jmeno tabulky";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // Schema
            // 
            this.Schema.DataPropertyName = "Schema";
            this.Schema.HeaderText = "Schema v DB";
            this.Schema.Name = "Schema";
            this.Schema.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "typ";
            this.dataGridViewTextBoxColumn2.HeaderText = "Typ v DB";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "COLUMN";
            this.dataGridViewTextBoxColumn3.HeaderText = "Nazev sloupce";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "TYPECOL";
            this.dataGridViewTextBoxColumn4.HeaderText = "Typ sloupce";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "MAXLEN";
            this.dataGridViewTextBoxColumn5.HeaderText = "Max delka";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "DEFVAL";
            this.dataGridViewTextBoxColumn6.HeaderText = "Vychozi hodnota";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "ISNULL";
            this.dataGridViewTextBoxColumn7.HeaderText = "Je NULL ?";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "NUMPREC";
            this.dataGridViewTextBoxColumn8.HeaderText = "presnost desetinne";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "NUMSCALE";
            this.dataGridViewTextBoxColumn9.HeaderText = "rozsah desetinne";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "DTPREC";
            this.dataGridViewTextBoxColumn10.HeaderText = "datum presnost";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "DESCCOL";
            this.dataGridViewTextBoxColumn11.HeaderText = "Popis do dokumentace";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "DESCNAME";
            this.dataGridViewTextBoxColumn12.HeaderText = "Typ popisu";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // bs_dok
            // 
            this.bs_dok.DataMember = "DT_Documentation";
            this.bs_dok.DataSource = this.ds_dok;
            // 
            // ds_dok
            // 
            this.ds_dok.DataSetName = "Dok";
            this.ds_dok.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel_button
            // 
            this.panel_button.Controls.Add(this.btn_Gen_HTML);
            this.panel_button.Controls.Add(this.btn_Delete);
            this.panel_button.Controls.Add(this.btn_Edit);
            this.panel_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_button.Location = new System.Drawing.Point(917, 0);
            this.panel_button.Name = "panel_button";
            this.panel_button.Size = new System.Drawing.Size(111, 657);
            this.panel_button.TabIndex = 16;
            // 
            // btn_Gen_HTML
            // 
            this.btn_Gen_HTML.Location = new System.Drawing.Point(6, 575);
            this.btn_Gen_HTML.Name = "btn_Gen_HTML";
            this.btn_Gen_HTML.Size = new System.Drawing.Size(93, 70);
            this.btn_Gen_HTML.TabIndex = 8;
            this.btn_Gen_HTML.Text = "Generuj HTML";
            this.btn_Gen_HTML.UseVisualStyleBackColor = true;
            this.btn_Gen_HTML.Click += new System.EventHandler(this.btn_Gen_HTML_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.Location = new System.Drawing.Point(6, 88);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(93, 70);
            this.btn_Delete.TabIndex = 7;
            this.btn_Delete.Text = "Smazat";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Edit
            // 
            this.btn_Edit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Edit.Location = new System.Drawing.Point(6, 12);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(93, 70);
            this.btn_Edit.TabIndex = 6;
            this.btn_Edit.Text = "Upravit";
            this.btn_Edit.UseVisualStyleBackColor = true;
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // Definice_Dokumentace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 657);
            this.Controls.Add(this.dg_dok);
            this.Controls.Add(this.panel_Filtr);
            this.Controls.Add(this.panel_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Definice_Dokumentace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Definice_Dokumentace";
            this.Load += new System.EventHandler(this.Definice_Dokumentace_Load);
            this.panel_Filtr.ResumeLayout(false);
            this.panel_Filtr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_dok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_dok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_dok)).EndInit();
            this.panel_button.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Filtr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_Type;
        private System.Windows.Forms.Button btn_ClearFilter;
        private System.Windows.Forms.TextBox tb_TableName;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.TextBox tb_ColumnName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dg_dok;
        private System.Windows.Forms.Panel panel_button;
        private System.Windows.Forms.Button btn_Edit;
        private System.Windows.Forms.BindingSource bs_dok;
        private Dokumentace.Dok ds_dok;
        private System.Windows.Forms.ComboBox cb_Type_Note;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Schema;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Gen_HTML;
    }
}