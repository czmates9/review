namespace Production.Forms
{
    partial class FormUzivateleEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxHeslo = new System.Windows.Forms.TextBox();
            this.textBoxPrijmeni = new System.Windows.Forms.TextBox();
            this.textBoxJmeno = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxVedouciSmeny = new System.Windows.Forms.CheckBox();
            this.comboBoxSkupina = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Jméno:";
            // 
            // textBoxId
            // 
            this.textBoxId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxId.Location = new System.Drawing.Point(75, 21);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(240, 20);
            this.textBoxId.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Příjmení:";
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
            this.label4.Location = new System.Drawing.Point(32, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Heslo:";
            // 
            // textBoxHeslo
            // 
            this.textBoxHeslo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHeslo.Location = new System.Drawing.Point(75, 102);
            this.textBoxHeslo.Name = "textBoxHeslo";
            this.textBoxHeslo.Size = new System.Drawing.Size(240, 20);
            this.textBoxHeslo.TabIndex = 4;
            // 
            // textBoxPrijmeni
            // 
            this.textBoxPrijmeni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPrijmeni.Location = new System.Drawing.Point(75, 73);
            this.textBoxPrijmeni.Name = "textBoxPrijmeni";
            this.textBoxPrijmeni.Size = new System.Drawing.Size(240, 20);
            this.textBoxPrijmeni.TabIndex = 3;
            // 
            // textBoxJmeno
            // 
            this.textBoxJmeno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJmeno.Location = new System.Drawing.Point(75, 47);
            this.textBoxJmeno.Name = "textBoxJmeno";
            this.textBoxJmeno.Size = new System.Drawing.Size(240, 20);
            this.textBoxJmeno.TabIndex = 2;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 391);
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
            this.buttonOK.TabIndex = 8;
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
            this.buttonStorno.TabIndex = 7;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.comboBoxSkupina);
            this.panel2.Controls.Add(this.checkBoxVedouciSmeny);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBoxJmeno);
            this.panel2.Controls.Add(this.textBoxId);
            this.panel2.Controls.Add(this.textBoxPrijmeni);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textBoxHeslo);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 391);
            this.panel2.TabIndex = 1;
            // 
            // checkBoxVedouciSmeny
            // 
            this.checkBoxVedouciSmeny.AutoSize = true;
            this.checkBoxVedouciSmeny.Location = new System.Drawing.Point(75, 155);
            this.checkBoxVedouciSmeny.Name = "checkBoxVedouciSmeny";
            this.checkBoxVedouciSmeny.Size = new System.Drawing.Size(100, 17);
            this.checkBoxVedouciSmeny.TabIndex = 6;
            this.checkBoxVedouciSmeny.Text = "Vedoucí směny";
            this.checkBoxVedouciSmeny.UseVisualStyleBackColor = true;
            // 
            // comboBoxSkupina
            // 
            this.comboBoxSkupina.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSkupina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSkupina.FormattingEnabled = true;
            this.comboBoxSkupina.Location = new System.Drawing.Point(75, 128);
            this.comboBoxSkupina.Name = "comboBoxSkupina";
            this.comboBoxSkupina.Size = new System.Drawing.Size(240, 21);
            this.comboBoxSkupina.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Skupina:";
            // 
            // FormUzivateleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormUzivateleEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormUzivateleEdit";
            this.Load += new System.EventHandler(this.FormUzivateleEdit_Load);
            this.Shown += new System.EventHandler(this.FormUzivateleEdit_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUzivateleEdit_KeyDown);
            this.Resize += new System.EventHandler(this.FormUzivateleEdit_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxHeslo;
        private System.Windows.Forms.TextBox textBoxPrijmeni;
        private System.Windows.Forms.TextBox textBoxJmeno;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.CheckBox checkBoxVedouciSmeny;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxSkupina;
    }
}