namespace Production.Forms
{
    partial class FormZboziEdit
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
            this.label3 = new System.Windows.Forms.Label();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxPripravnyCas = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxJednotkovyCas = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxTypSledovaniCasu = new System.Windows.Forms.ComboBox();
            this.textBoxNazevPolozky = new System.Windows.Forms.TextBox();
            this.textBoxCisloPolozky = new System.Windows.Forms.TextBox();
            this.textBoxCarovyKodPolozky = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMnozstviVBaleni = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMernaJednotka = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Položka číslo:";
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 71);
            this.buttonStorno.TabIndex = 9;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 391);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(334, 71);
            this.panelButtons.TabIndex = 4;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(214, 71);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Název položky:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxMernaJednotka);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.textBoxPripravnyCas);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.textBoxJednotkovyCas);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.comboBoxTypSledovaniCasu);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBoxNazevPolozky);
            this.panel2.Controls.Add(this.textBoxCisloPolozky);
            this.panel2.Controls.Add(this.textBoxCarovyKodPolozky);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textBoxMnozstviVBaleni);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 462);
            this.panel2.TabIndex = 3;
            // 
            // textBoxPripravnyCas
            // 
            this.textBoxPripravnyCas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPripravnyCas.Location = new System.Drawing.Point(152, 177);
            this.textBoxPripravnyCas.Name = "textBoxPripravnyCas";
            this.textBoxPripravnyCas.Size = new System.Drawing.Size(170, 20);
            this.textBoxPripravnyCas.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(59, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Přípravný čas:";
            // 
            // textBoxJednotkovyCas
            // 
            this.textBoxJednotkovyCas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJednotkovyCas.Location = new System.Drawing.Point(152, 151);
            this.textBoxJednotkovyCas.Name = "textBoxJednotkovyCas";
            this.textBoxJednotkovyCas.Size = new System.Drawing.Size(170, 20);
            this.textBoxJednotkovyCas.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Jednotkový čas:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Typ sledování času:";
            // 
            // comboBoxTypSledovaniCasu
            // 
            this.comboBoxTypSledovaniCasu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTypSledovaniCasu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypSledovaniCasu.FormattingEnabled = true;
            this.comboBoxTypSledovaniCasu.Location = new System.Drawing.Point(152, 203);
            this.comboBoxTypSledovaniCasu.Name = "comboBoxTypSledovaniCasu";
            this.comboBoxTypSledovaniCasu.Size = new System.Drawing.Size(174, 21);
            this.comboBoxTypSledovaniCasu.TabIndex = 8;
            // 
            // textBoxNazevPolozky
            // 
            this.textBoxNazevPolozky.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNazevPolozky.Location = new System.Drawing.Point(152, 47);
            this.textBoxNazevPolozky.Name = "textBoxNazevPolozky";
            this.textBoxNazevPolozky.Size = new System.Drawing.Size(170, 20);
            this.textBoxNazevPolozky.TabIndex = 2;
            // 
            // textBoxCisloPolozky
            // 
            this.textBoxCisloPolozky.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCisloPolozky.Location = new System.Drawing.Point(152, 21);
            this.textBoxCisloPolozky.Name = "textBoxCisloPolozky";
            this.textBoxCisloPolozky.Size = new System.Drawing.Size(170, 20);
            this.textBoxCisloPolozky.TabIndex = 1;
            // 
            // textBoxCarovyKodPolozky
            // 
            this.textBoxCarovyKodPolozky.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCarovyKodPolozky.Location = new System.Drawing.Point(152, 73);
            this.textBoxCarovyKodPolozky.Name = "textBoxCarovyKodPolozky";
            this.textBoxCarovyKodPolozky.Size = new System.Drawing.Size(170, 20);
            this.textBoxCarovyKodPolozky.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Čárový kód položky:";
            // 
            // textBoxMnozstviVBaleni
            // 
            this.textBoxMnozstviVBaleni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMnozstviVBaleni.Location = new System.Drawing.Point(152, 99);
            this.textBoxMnozstviVBaleni.Name = "textBoxMnozstviVBaleni";
            this.textBoxMnozstviVBaleni.Size = new System.Drawing.Size(170, 20);
            this.textBoxMnozstviVBaleni.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Množství v balení:";
            // 
            // textBoxMernaJednotka
            // 
            this.textBoxMernaJednotka.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMernaJednotka.Location = new System.Drawing.Point(152, 125);
            this.textBoxMernaJednotka.Name = "textBoxMernaJednotka";
            this.textBoxMernaJednotka.Size = new System.Drawing.Size(170, 20);
            this.textBoxMernaJednotka.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(49, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Měrná jednotka:";
            // 
            // FormZboziEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 462);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Name = "FormZboziEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormZboziEdit";
            this.Load += new System.EventHandler(this.FormZboziEdit_Load);
            this.Shown += new System.EventHandler(this.FormZboziEdit_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormZboziEdit_KeyDown);
            this.Resize += new System.EventHandler(this.FormZboziEdit_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxNazevPolozky;
        private System.Windows.Forms.TextBox textBoxCisloPolozky;
        private System.Windows.Forms.TextBox textBoxCarovyKodPolozky;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMnozstviVBaleni;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxTypSledovaniCasu;
        private System.Windows.Forms.TextBox textBoxPripravnyCas;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxJednotkovyCas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxMernaJednotka;
        private System.Windows.Forms.Label label8;
    }
}