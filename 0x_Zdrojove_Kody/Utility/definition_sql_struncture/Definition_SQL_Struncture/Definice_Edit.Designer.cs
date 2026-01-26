namespace Definition_SQL_Struncture
{
    partial class Definice_Edit
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_TABLE_NAME = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_COLUMN_DEFAULT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_NUMERIC_PRECISION = new System.Windows.Forms.TextBox();
            this.tb_CHARACTER_MAXIMUM_LENGTH = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_NUMRIC_SCALE = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.tb_DATETIME_PRECISION = new System.Windows.Forms.TextBox();
            this.cbox_DATA_TYPE = new System.Windows.Forms.ComboBox();
            this.tb_COLUMN_NAME = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbox_IS_NULLABLE = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(163, 313);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 50);
            this.button1.TabIndex = 9;
            this.button1.Text = "Uložit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(35, 313);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 50);
            this.button2.TabIndex = 10;
            this.button2.Text = "Storno";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nazev Tabulky :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_TABLE_NAME
            // 
            this.tb_TABLE_NAME.Location = new System.Drawing.Point(141, 11);
            this.tb_TABLE_NAME.Name = "tb_TABLE_NAME";
            this.tb_TABLE_NAME.Size = new System.Drawing.Size(139, 20);
            this.tb_TABLE_NAME.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vychozi hodnota :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_COLUMN_DEFAULT
            // 
            this.tb_COLUMN_DEFAULT.Location = new System.Drawing.Point(141, 77);
            this.tb_COLUMN_DEFAULT.Name = "tb_COLUMN_DEFAULT";
            this.tb_COLUMN_DEFAULT.Size = new System.Drawing.Size(139, 20);
            this.tb_COLUMN_DEFAULT.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(32, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Typ :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(32, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Numeric Presnost :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(32, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Maximalni delka:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_NUMERIC_PRECISION
            // 
            this.tb_NUMERIC_PRECISION.Location = new System.Drawing.Point(140, 211);
            this.tb_NUMERIC_PRECISION.Name = "tb_NUMERIC_PRECISION";
            this.tb_NUMERIC_PRECISION.Size = new System.Drawing.Size(139, 20);
            this.tb_NUMERIC_PRECISION.TabIndex = 6;
            // 
            // tb_CHARACTER_MAXIMUM_LENGTH
            // 
            this.tb_CHARACTER_MAXIMUM_LENGTH.Location = new System.Drawing.Point(140, 178);
            this.tb_CHARACTER_MAXIMUM_LENGTH.Name = "tb_CHARACTER_MAXIMUM_LENGTH";
            this.tb_CHARACTER_MAXIMUM_LENGTH.Size = new System.Drawing.Size(139, 20);
            this.tb_CHARACTER_MAXIMUM_LENGTH.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(32, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Numeric rozsah :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_NUMRIC_SCALE
            // 
            this.tb_NUMRIC_SCALE.Location = new System.Drawing.Point(140, 244);
            this.tb_NUMRIC_SCALE.Name = "tb_NUMRIC_SCALE";
            this.tb_NUMRIC_SCALE.Size = new System.Drawing.Size(139, 20);
            this.tb_NUMRIC_SCALE.TabIndex = 7;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(32, 277);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Datum presnost :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_DATETIME_PRECISION
            // 
            this.tb_DATETIME_PRECISION.Location = new System.Drawing.Point(140, 277);
            this.tb_DATETIME_PRECISION.Name = "tb_DATETIME_PRECISION";
            this.tb_DATETIME_PRECISION.Size = new System.Drawing.Size(139, 20);
            this.tb_DATETIME_PRECISION.TabIndex = 8;
            // 
            // cbox_DATA_TYPE
            // 
            this.cbox_DATA_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_DATA_TYPE.FormattingEnabled = true;
            this.cbox_DATA_TYPE.Location = new System.Drawing.Point(140, 144);
            this.cbox_DATA_TYPE.Name = "cbox_DATA_TYPE";
            this.cbox_DATA_TYPE.Size = new System.Drawing.Size(139, 21);
            this.cbox_DATA_TYPE.TabIndex = 4;
            // 
            // tb_COLUMN_NAME
            // 
            this.tb_COLUMN_NAME.Location = new System.Drawing.Point(140, 44);
            this.tb_COLUMN_NAME.Name = "tb_COLUMN_NAME";
            this.tb_COLUMN_NAME.Size = new System.Drawing.Size(139, 20);
            this.tb_COLUMN_NAME.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(32, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nazev Stloupce :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(32, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Povol null :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbox_IS_NULLABLE
            // 
            this.cbox_IS_NULLABLE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_IS_NULLABLE.FormattingEnabled = true;
            this.cbox_IS_NULLABLE.Items.AddRange(new object[] {
            "",
            "YES",
            "NO"});
            this.cbox_IS_NULLABLE.Location = new System.Drawing.Point(140, 110);
            this.cbox_IS_NULLABLE.Name = "cbox_IS_NULLABLE";
            this.cbox_IS_NULLABLE.Size = new System.Drawing.Size(139, 21);
            this.cbox_IS_NULLABLE.TabIndex = 3;
            // 
            // Definice_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 375);
            this.Controls.Add(this.cbox_IS_NULLABLE);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tb_COLUMN_NAME);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbox_DATA_TYPE);
            this.Controls.Add(this.tb_DATETIME_PRECISION);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tb_NUMRIC_SCALE);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_CHARACTER_MAXIMUM_LENGTH);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_NUMERIC_PRECISION);
            this.Controls.Add(this.tb_COLUMN_DEFAULT);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_TABLE_NAME);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Definice_Edit";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.EditKonstanty_Edit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_TABLE_NAME;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_COLUMN_DEFAULT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_NUMERIC_PRECISION;
        private System.Windows.Forms.TextBox tb_CHARACTER_MAXIMUM_LENGTH;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_NUMRIC_SCALE;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox tb_DATETIME_PRECISION;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbox_DATA_TYPE;
        private System.Windows.Forms.TextBox tb_COLUMN_NAME;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbox_IS_NULLABLE;
    }
}