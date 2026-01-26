namespace FASK.SledovaniVyroby.Module.Pila
{
    partial class frmPotvrzeniTisku
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPotvrzeniTisku));
            this.txtPocetKusu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtItemNumber = new System.Windows.Forms.TextBox();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProfileLength = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPoznamka = new System.Windows.Forms.TextBox();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPocetKusu
            // 
            this.txtPocetKusu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPocetKusu.Location = new System.Drawing.Point(197, 8);
            this.txtPocetKusu.Name = "txtPocetKusu";
            this.txtPocetKusu.Size = new System.Drawing.Size(82, 20);
            this.txtPocetKusu.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Počet skutečně odvedených kusů : ";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(12, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.TabIndex = 58;
            this.label5.Text = "Číslo položky : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtItemNumber
            // 
            this.txtItemNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItemNumber.Location = new System.Drawing.Point(139, 174);
            this.txtItemNumber.MaxLength = 2;
            this.txtItemNumber.Name = "txtItemNumber";
            this.txtItemNumber.ReadOnly = true;
            this.txtItemNumber.Size = new System.Drawing.Size(91, 20);
            this.txtItemNumber.TabIndex = 59;
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderNumber.Location = new System.Drawing.Point(139, 148);
            this.txtOrderNumber.MaxLength = 4;
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.ReadOnly = true;
            this.txtOrderNumber.Size = new System.Drawing.Size(91, 20);
            this.txtOrderNumber.TabIndex = 57;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(12, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 20);
            this.label8.TabIndex = 56;
            this.label8.Text = "Číslo zakázky : ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProfileLength
            // 
            this.txtProfileLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfileLength.Location = new System.Drawing.Point(139, 122);
            this.txtProfileLength.MaxLength = 8;
            this.txtProfileLength.Name = "txtProfileLength";
            this.txtProfileLength.ReadOnly = true;
            this.txtProfileLength.Size = new System.Drawing.Size(91, 20);
            this.txtProfileLength.TabIndex = 55;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Location = new System.Drawing.Point(12, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 20);
            this.label10.TabIndex = 54;
            this.label10.Text = "Délka profilu : ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPoznamka
            // 
            this.txtPoznamka.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPoznamka.Location = new System.Drawing.Point(15, 34);
            this.txtPoznamka.Multiline = true;
            this.txtPoznamka.Name = "txtPoznamka";
            this.txtPoznamka.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPoznamka.Size = new System.Drawing.Size(264, 82);
            this.txtPoznamka.TabIndex = 3;
            // 
            // buttonStorno
            // 
            this.buttonStorno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStorno.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonStorno.Location = new System.Drawing.Point(123, 211);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(75, 23);
            this.buttonStorno.TabIndex = 7;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.UseVisualStyleBackColor = true;
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click_1);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(204, 211);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click_1);
            // 
            // frmPotvrzeniTisku
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonStorno;
            this.ClientSize = new System.Drawing.Size(288, 253);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtItemNumber);
            this.Controls.Add(this.txtOrderNumber);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtProfileLength);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtPoznamka);
            this.Controls.Add(this.buttonStorno);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.txtPocetKusu);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(304, 291);
            this.Name = "frmPotvrzeniTisku";
            this.Text = "Potvrzení tisku";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPocetKusu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtItemNumber;
        public System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtProfileLength;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPoznamka;
        private System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Button buttonOK;
    }
}