namespace Konzola.Ciselniky
{
    partial class FormOdberateleEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbOdb_id = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbOdb_typ = new System.Windows.Forms.TextBox();
            this.tbOdb_carcode = new System.Windows.Forms.TextBox();
            this.tbOdb_desc = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbOdb_Dodavatel = new System.Windows.Forms.CheckBox();
            this.cbOdb_Odberatel = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbNapoveda = new System.Windows.Forms.RichTextBox();
            this.tbOdb_dic = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbOdb_psc = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbOdb_cisloOr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbOdb_ulice = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbOdb_misto = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbMena_id = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbOdb_ico = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelNapoveda = new System.Windows.Forms.Panel();
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panelNapoveda.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Označení:";
            // 
            // tbOdb_id
            // 
            this.tbOdb_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOdb_id.Location = new System.Drawing.Point(75, 21);
            this.tbOdb_id.Name = "tbOdb_id";
            this.tbOdb_id.Size = new System.Drawing.Size(240, 20);
            this.tbOdb_id.TabIndex = 1;
            this.tbOdb_id.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Čár. kód:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "id:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Typ:";
            // 
            // tbOdb_typ
            // 
            this.tbOdb_typ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOdb_typ.Location = new System.Drawing.Point(75, 73);
            this.tbOdb_typ.Name = "tbOdb_typ";
            this.tbOdb_typ.Size = new System.Drawing.Size(240, 20);
            this.tbOdb_typ.TabIndex = 3;
            this.tbOdb_typ.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // tbOdb_carcode
            // 
            this.tbOdb_carcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOdb_carcode.Location = new System.Drawing.Point(75, 99);
            this.tbOdb_carcode.Name = "tbOdb_carcode";
            this.tbOdb_carcode.Size = new System.Drawing.Size(240, 20);
            this.tbOdb_carcode.TabIndex = 4;
            this.tbOdb_carcode.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // tbOdb_desc
            // 
            this.tbOdb_desc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOdb_desc.Location = new System.Drawing.Point(75, 47);
            this.tbOdb_desc.Name = "tbOdb_desc";
            this.tbOdb_desc.Size = new System.Drawing.Size(240, 20);
            this.tbOdb_desc.TabIndex = 2;
            this.tbOdb_desc.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 471);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(334, 71);
            this.panelButtons.TabIndex = 2;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(214, 71);
            this.buttonOK.TabIndex = 15;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click_1);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 71);
            this.buttonStorno.TabIndex = 14;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelNapoveda);
            this.panel2.Controls.Add(this.cbOdb_Dodavatel);
            this.panel2.Controls.Add(this.cbOdb_Odberatel);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbOdb_desc);
            this.panel2.Controls.Add(this.tbOdb_id);
            this.panel2.Controls.Add(this.tbOdb_dic);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.tbOdb_psc);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.tbOdb_cisloOr);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.tbOdb_ulice);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.tbOdb_misto);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.tbMena_id);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.tbOdb_ico);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.tbOdb_carcode);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.tbOdb_typ);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 471);
            this.panel2.TabIndex = 1;
            // 
            // cbOdb_Dodavatel
            // 
            this.cbOdb_Dodavatel.AutoSize = true;
            this.cbOdb_Dodavatel.Location = new System.Drawing.Point(75, 331);
            this.cbOdb_Dodavatel.Name = "cbOdb_Dodavatel";
            this.cbOdb_Dodavatel.Size = new System.Drawing.Size(75, 17);
            this.cbOdb_Dodavatel.TabIndex = 13;
            this.cbOdb_Dodavatel.Text = "Dodavatel";
            this.cbOdb_Dodavatel.UseVisualStyleBackColor = true;
            // 
            // cbOdb_Odberatel
            // 
            this.cbOdb_Odberatel.AutoSize = true;
            this.cbOdb_Odberatel.Location = new System.Drawing.Point(75, 308);
            this.cbOdb_Odberatel.Name = "cbOdb_Odberatel";
            this.cbOdb_Odberatel.Size = new System.Drawing.Size(72, 17);
            this.cbOdb_Odberatel.TabIndex = 12;
            this.cbOdb_Odberatel.Text = "Odběratel";
            this.cbOdb_Odberatel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNapoveda);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 114);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nápověda";
            // 
            // tbNapoveda
            // 
            this.tbNapoveda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNapoveda.Location = new System.Drawing.Point(3, 16);
            this.tbNapoveda.Name = "tbNapoveda";
            this.tbNapoveda.ReadOnly = true;
            this.tbNapoveda.Size = new System.Drawing.Size(322, 95);
            this.tbNapoveda.TabIndex = 0;
            this.tbNapoveda.TabStop = false;
            this.tbNapoveda.Text = "";
            // 
            // tbOdb_dic
            // 
            this.tbOdb_dic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOdb_dic.Location = new System.Drawing.Point(75, 281);
            this.tbOdb_dic.Name = "tbOdb_dic";
            this.tbOdb_dic.Size = new System.Drawing.Size(240, 20);
            this.tbOdb_dic.TabIndex = 11;
            this.tbOdb_dic.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(41, 284);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "DIČ:";
            // 
            // tbOdb_psc
            // 
            this.tbOdb_psc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOdb_psc.Location = new System.Drawing.Point(75, 255);
            this.tbOdb_psc.Name = "tbOdb_psc";
            this.tbOdb_psc.Size = new System.Drawing.Size(240, 20);
            this.tbOdb_psc.TabIndex = 10;
            this.tbOdb_psc.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(38, 258);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "PSČ:";
            // 
            // tbOdb_cisloOr
            // 
            this.tbOdb_cisloOr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOdb_cisloOr.Location = new System.Drawing.Point(75, 229);
            this.tbOdb_cisloOr.Name = "tbOdb_cisloOr";
            this.tbOdb_cisloOr.Size = new System.Drawing.Size(240, 20);
            this.tbOdb_cisloOr.TabIndex = 9;
            this.tbOdb_cisloOr.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 236);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Orient. č.:";
            // 
            // tbOdb_ulice
            // 
            this.tbOdb_ulice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOdb_ulice.Location = new System.Drawing.Point(75, 203);
            this.tbOdb_ulice.Name = "tbOdb_ulice";
            this.tbOdb_ulice.Size = new System.Drawing.Size(240, 20);
            this.tbOdb_ulice.TabIndex = 8;
            this.tbOdb_ulice.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Ulice:";
            // 
            // tbOdb_misto
            // 
            this.tbOdb_misto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOdb_misto.Location = new System.Drawing.Point(75, 177);
            this.tbOdb_misto.Name = "tbOdb_misto";
            this.tbOdb_misto.Size = new System.Drawing.Size(240, 20);
            this.tbOdb_misto.TabIndex = 7;
            this.tbOdb_misto.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Místo sídla:";
            // 
            // tbMena_id
            // 
            this.tbMena_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMena_id.Location = new System.Drawing.Point(75, 151);
            this.tbMena_id.Name = "tbMena_id";
            this.tbMena_id.Size = new System.Drawing.Size(240, 20);
            this.tbMena_id.TabIndex = 6;
            this.tbMena_id.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "ID měny:";
            // 
            // tbOdb_ico
            // 
            this.tbOdb_ico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOdb_ico.Location = new System.Drawing.Point(75, 125);
            this.tbOdb_ico.Name = "tbOdb_ico";
            this.tbOdb_ico.Size = new System.Drawing.Size(240, 20);
            this.tbOdb_ico.TabIndex = 5;
            this.tbOdb_ico.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "IČO:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panelNapoveda
            // 
            this.panelNapoveda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNapoveda.Controls.Add(this.groupBox1);
            this.panelNapoveda.Location = new System.Drawing.Point(3, 354);
            this.panelNapoveda.Name = "panelNapoveda";
            this.panelNapoveda.Size = new System.Drawing.Size(328, 114);
            this.panelNapoveda.TabIndex = 14;
            // 
            // FormOdberateleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 542);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormOdberateleEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zdroje";
            this.Load += new System.EventHandler(this.FormUzivateleEdit_Load);
            this.Shown += new System.EventHandler(this.FormUzivateleEdit_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUzivateleEdit_KeyDown);
            this.Resize += new System.EventHandler(this.FormUzivateleEdit_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panelNapoveda.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbOdb_id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbOdb_typ;
        private System.Windows.Forms.TextBox tbOdb_carcode;
        private System.Windows.Forms.TextBox tbOdb_desc;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tbNapoveda;
        private System.Windows.Forms.TextBox tbOdb_ulice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbOdb_misto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbMena_id;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbOdb_ico;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbOdb_dic;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbOdb_psc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbOdb_cisloOr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cbOdb_Dodavatel;
        private System.Windows.Forms.CheckBox cbOdb_Odberatel;
        private System.Windows.Forms.Panel panelNapoveda;
    }
}