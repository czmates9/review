namespace Module.DCDIdeal.InkJet
{
    partial class frmPotvrzeniKusu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPotvrzeniKusu));
            this.label1 = new System.Windows.Forms.Label();
            this.txtPocetKusu = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.txtPoznamka = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPoradi = new System.Windows.Forms.TextBox();
            this.txtPozice = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtZakazka = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Poèet skuteènì odvedených kusù : ";
            // 
            // txtPocetKusu
            // 
            this.txtPocetKusu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPocetKusu.Location = new System.Drawing.Point(198, 31);
            this.txtPocetKusu.Name = "txtPocetKusu";
            this.txtPocetKusu.Size = new System.Drawing.Size(78, 20);
            this.txtPocetKusu.TabIndex = 1;
            this.txtPocetKusu.TextChanged += new System.EventHandler(this.txtPocetKusu_TextChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(201, 218);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStorno.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonStorno.Location = new System.Drawing.Point(117, 218);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(75, 23);
            this.buttonStorno.TabIndex = 5;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.UseVisualStyleBackColor = true;
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // txtPoznamka
            // 
            this.txtPoznamka.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPoznamka.Location = new System.Drawing.Point(16, 74);
            this.txtPoznamka.Multiline = true;
            this.txtPoznamka.Name = "txtPoznamka";
            this.txtPoznamka.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPoznamka.Size = new System.Drawing.Size(260, 44);
            this.txtPoznamka.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(13, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Poznámka obsluhy : ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelLogin
            // 
            this.labelLogin.Location = new System.Drawing.Point(13, 6);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(172, 13);
            this.labelLogin.TabIndex = 6;
            this.labelLogin.Text = "<,>";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLogin.Location = new System.Drawing.Point(198, 1);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(78, 23);
            this.buttonLogin.TabIndex = 7;
            this.buttonLogin.Text = "Zmìnit";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(9, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.TabIndex = 40;
            this.label5.Text = "Poøadí : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPoradi
            // 
            this.txtPoradi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPoradi.Location = new System.Drawing.Point(136, 176);
            this.txtPoradi.MaxLength = 2;
            this.txtPoradi.Name = "txtPoradi";
            this.txtPoradi.ReadOnly = true;
            this.txtPoradi.Size = new System.Drawing.Size(91, 20);
            this.txtPoradi.TabIndex = 41;
            // 
            // txtPozice
            // 
            this.txtPozice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPozice.Location = new System.Drawing.Point(136, 150);
            this.txtPozice.MaxLength = 4;
            this.txtPozice.Name = "txtPozice";
            this.txtPozice.ReadOnly = true;
            this.txtPozice.Size = new System.Drawing.Size(91, 20);
            this.txtPozice.TabIndex = 39;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(9, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 20);
            this.label8.TabIndex = 38;
            this.label8.Text = "Pozice : ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZakazka
            // 
            this.txtZakazka.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZakazka.Location = new System.Drawing.Point(136, 124);
            this.txtZakazka.MaxLength = 8;
            this.txtZakazka.Name = "txtZakazka";
            this.txtZakazka.ReadOnly = true;
            this.txtZakazka.Size = new System.Drawing.Size(91, 20);
            this.txtZakazka.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Location = new System.Drawing.Point(9, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 20);
            this.label10.TabIndex = 36;
            this.label10.Text = "Zakázka : ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmPotvrzeniKusu
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonStorno;
            this.ClientSize = new System.Drawing.Size(288, 253);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPoradi);
            this.Controls.Add(this.txtPozice);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtZakazka);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.txtPoznamka);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.buttonStorno);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.txtPocetKusu);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(304, 291);
            this.Name = "frmPotvrzeniKusu";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Potvrzení zpracování";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmPotvrzeniKusu_Load);
            this.Shown += new System.EventHandler(this.frmPotvrzeniKusu_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPocetKusu;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.TextBox txtPoznamka;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtPoradi;
        public System.Windows.Forms.TextBox txtPozice;
        public System.Windows.Forms.TextBox txtZakazka;
    }
}