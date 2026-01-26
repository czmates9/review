namespace Vyroba_Konzola.Ciselniky
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLOGIN = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxEAN = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxHASH = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSECONDNAME = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxFIRSTNAME = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxADM = new System.Windows.Forms.CheckBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPASSWD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBoxLOGIN
            // 
            this.textBoxLOGIN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLOGIN.Location = new System.Drawing.Point(73, 38);
            this.textBoxLOGIN.Name = "textBoxLOGIN";
            this.textBoxLOGIN.Size = new System.Drawing.Size(236, 20);
            this.textBoxLOGIN.TabIndex = 2;
            this.textBoxLOGIN.TextChanged += new System.EventHandler(this.textBoxJmeno_TextChanged);
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
            this.panelButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.panelButtons_Paint);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxEAN);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBoxHASH);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textBoxSECONDNAME);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.textBoxFIRSTNAME);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.checkBoxADM);
            this.panel2.Controls.Add(this.textBoxID);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBoxLOGIN);
            this.panel2.Controls.Add(this.textBoxPASSWD);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 391);
            this.panel2.TabIndex = 1;
            // 
            // textBoxEAN
            // 
            this.textBoxEAN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEAN.Location = new System.Drawing.Point(73, 168);
            this.textBoxEAN.Name = "textBoxEAN";
            this.textBoxEAN.Size = new System.Drawing.Size(236, 20);
            this.textBoxEAN.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Čár. kód:";
            // 
            // textBoxHASH
            // 
            this.textBoxHASH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHASH.Location = new System.Drawing.Point(73, 142);
            this.textBoxHASH.Name = "textBoxHASH";
            this.textBoxHASH.Size = new System.Drawing.Size(236, 20);
            this.textBoxHASH.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "HASH:";
            // 
            // textBoxSECONDNAME
            // 
            this.textBoxSECONDNAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSECONDNAME.Location = new System.Drawing.Point(73, 116);
            this.textBoxSECONDNAME.Name = "textBoxSECONDNAME";
            this.textBoxSECONDNAME.Size = new System.Drawing.Size(236, 20);
            this.textBoxSECONDNAME.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Příjmení:";
            // 
            // textBoxFIRSTNAME
            // 
            this.textBoxFIRSTNAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFIRSTNAME.Location = new System.Drawing.Point(73, 90);
            this.textBoxFIRSTNAME.Name = "textBoxFIRSTNAME";
            this.textBoxFIRSTNAME.Size = new System.Drawing.Size(236, 20);
            this.textBoxFIRSTNAME.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Jméno:";
            // 
            // checkBoxADM
            // 
            this.checkBoxADM.AutoSize = true;
            this.checkBoxADM.Location = new System.Drawing.Point(73, 194);
            this.checkBoxADM.Name = "checkBoxADM";
            this.checkBoxADM.Size = new System.Drawing.Size(85, 17);
            this.checkBoxADM.TabIndex = 8;
            this.checkBoxADM.Text = "administrátor";
            this.checkBoxADM.UseVisualStyleBackColor = true;
            // 
            // textBoxID
            // 
            this.textBoxID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxID.Location = new System.Drawing.Point(72, 12);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(237, 20);
            this.textBoxID.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "ID:";
            // 
            // textBoxPASSWD
            // 
            this.textBoxPASSWD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPASSWD.Location = new System.Drawing.Point(73, 64);
            this.textBoxPASSWD.Name = "textBoxPASSWD";
            this.textBoxPASSWD.Size = new System.Drawing.Size(236, 20);
            this.textBoxPASSWD.TabIndex = 3;
            this.textBoxPASSWD.TextChanged += new System.EventHandler(this.textBoxPrijmeni_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Heslo:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
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
            this.Text = "Nový uživatel";
            this.Load += new System.EventHandler(this.FormUzivateleEdit_Load);
            this.Shown += new System.EventHandler(this.FormUzivateleEdit_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUzivateleEdit_KeyDown);
            this.Resize += new System.EventHandler(this.FormUzivateleEdit_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLOGIN;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.TextBox textBoxPASSWD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSECONDNAME;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxFIRSTNAME;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxADM;
        private System.Windows.Forms.TextBox textBoxEAN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxHASH;
        private System.Windows.Forms.Label label9;
    }
}