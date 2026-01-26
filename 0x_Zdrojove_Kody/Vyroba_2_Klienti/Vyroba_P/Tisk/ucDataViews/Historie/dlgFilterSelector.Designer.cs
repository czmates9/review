namespace Fask.Vyroba_P.ucDataViews.Historie
{
    partial class dlgFilterSelector
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCasDleFiltru = new System.Windows.Forms.RadioButton();
            this.rbCasMesicni = new System.Windows.Forms.RadioButton();
            this.rbCasDenni = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbUzivatelDleFiltru = new System.Windows.Forms.RadioButton();
            this.rbUzivatelPrihlaseny = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbStrojDleFiltru = new System.Windows.Forms.RadioButton();
            this.rbStrojNastaveny = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonOK.Location = new System.Drawing.Point(155, 213);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(107, 36);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonStorno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonStorno.Location = new System.Drawing.Point(12, 213);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(109, 36);
            this.buttonStorno.TabIndex = 2;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.UseVisualStyleBackColor = true;
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCasDleFiltru);
            this.groupBox1.Controls.Add(this.rbCasMesicni);
            this.groupBox1.Controls.Add(this.rbCasDenni);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(118, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Čas";
            // 
            // rbCasDleFiltru
            // 
            this.rbCasDleFiltru.AutoSize = true;
            this.rbCasDleFiltru.Location = new System.Drawing.Point(17, 65);
            this.rbCasDleFiltru.Name = "rbCasDleFiltru";
            this.rbCasDleFiltru.Size = new System.Drawing.Size(61, 17);
            this.rbCasDleFiltru.TabIndex = 2;
            this.rbCasDleFiltru.Text = "dle filtru";
            this.rbCasDleFiltru.UseVisualStyleBackColor = true;
            // 
            // rbCasMesicni
            // 
            this.rbCasMesicni.AutoSize = true;
            this.rbCasMesicni.Location = new System.Drawing.Point(17, 42);
            this.rbCasMesicni.Name = "rbCasMesicni";
            this.rbCasMesicni.Size = new System.Drawing.Size(64, 17);
            this.rbCasMesicni.TabIndex = 1;
            this.rbCasMesicni.Text = "měsíční";
            this.rbCasMesicni.UseVisualStyleBackColor = true;
            // 
            // rbCasDenni
            // 
            this.rbCasDenni.AutoSize = true;
            this.rbCasDenni.Checked = true;
            this.rbCasDenni.Location = new System.Drawing.Point(17, 19);
            this.rbCasDenni.Name = "rbCasDenni";
            this.rbCasDenni.Size = new System.Drawing.Size(53, 17);
            this.rbCasDenni.TabIndex = 0;
            this.rbCasDenni.TabStop = true;
            this.rbCasDenni.Text = "denní";
            this.rbCasDenni.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbUzivatelDleFiltru);
            this.groupBox2.Controls.Add(this.rbUzivatelPrihlaseny);
            this.groupBox2.Location = new System.Drawing.Point(13, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(118, 84);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Uživatel";
            // 
            // rbUzivatelDleFiltru
            // 
            this.rbUzivatelDleFiltru.AutoSize = true;
            this.rbUzivatelDleFiltru.Location = new System.Drawing.Point(17, 42);
            this.rbUzivatelDleFiltru.Name = "rbUzivatelDleFiltru";
            this.rbUzivatelDleFiltru.Size = new System.Drawing.Size(61, 17);
            this.rbUzivatelDleFiltru.TabIndex = 1;
            this.rbUzivatelDleFiltru.Text = "dle filtru";
            this.rbUzivatelDleFiltru.UseVisualStyleBackColor = true;
            // 
            // rbUzivatelPrihlaseny
            // 
            this.rbUzivatelPrihlaseny.AutoSize = true;
            this.rbUzivatelPrihlaseny.Checked = true;
            this.rbUzivatelPrihlaseny.Location = new System.Drawing.Point(17, 19);
            this.rbUzivatelPrihlaseny.Name = "rbUzivatelPrihlaseny";
            this.rbUzivatelPrihlaseny.Size = new System.Drawing.Size(73, 17);
            this.rbUzivatelPrihlaseny.TabIndex = 0;
            this.rbUzivatelPrihlaseny.TabStop = true;
            this.rbUzivatelPrihlaseny.Text = "přihlášený";
            this.rbUzivatelPrihlaseny.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbStrojDleFiltru);
            this.groupBox3.Controls.Add(this.rbStrojNastaveny);
            this.groupBox3.Location = new System.Drawing.Point(137, 123);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(118, 84);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Stroj";
            // 
            // rbStrojDleFiltru
            // 
            this.rbStrojDleFiltru.AutoSize = true;
            this.rbStrojDleFiltru.Location = new System.Drawing.Point(17, 42);
            this.rbStrojDleFiltru.Name = "rbStrojDleFiltru";
            this.rbStrojDleFiltru.Size = new System.Drawing.Size(61, 17);
            this.rbStrojDleFiltru.TabIndex = 1;
            this.rbStrojDleFiltru.Text = "dle filtru";
            this.rbStrojDleFiltru.UseVisualStyleBackColor = true;
            // 
            // rbStrojNastaveny
            // 
            this.rbStrojNastaveny.AutoSize = true;
            this.rbStrojNastaveny.Checked = true;
            this.rbStrojNastaveny.Location = new System.Drawing.Point(17, 19);
            this.rbStrojNastaveny.Name = "rbStrojNastaveny";
            this.rbStrojNastaveny.Size = new System.Drawing.Size(74, 17);
            this.rbStrojNastaveny.TabIndex = 0;
            this.rbStrojNastaveny.TabStop = true;
            this.rbStrojNastaveny.Text = "nastavený";
            this.rbStrojNastaveny.UseVisualStyleBackColor = true;
            // 
            // dlgFilterSelector
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonStorno;
            this.ClientSize = new System.Drawing.Size(274, 260);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonStorno);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(280, 289);
            this.Name = "dlgFilterSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Výběr filtru";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCasDleFiltru;
        private System.Windows.Forms.RadioButton rbCasMesicni;
        private System.Windows.Forms.RadioButton rbCasDenni;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbUzivatelDleFiltru;
        private System.Windows.Forms.RadioButton rbUzivatelPrihlaseny;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbStrojDleFiltru;
        private System.Windows.Forms.RadioButton rbStrojNastaveny;
    }
}