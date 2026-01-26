namespace Konzola.Servis
{
    partial class FormZdrojeStavyEdit
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
            this.tbIDZdroj = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbIDCinnost = new System.Windows.Forms.TextBox();
            this.tbIDStav = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelNapoveda = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbNapoveda = new System.Windows.Forms.RichTextBox();
            this.btnZdrojID = new System.Windows.Forms.Button();
            this.btnStavID = new System.Windows.Forms.Button();
            this.btnCinnostID = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelNapoveda.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbIDZdroj
            // 
            this.tbIDZdroj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIDZdroj.Location = new System.Drawing.Point(75, 21);
            this.tbIDZdroj.Name = "tbIDZdroj";
            this.tbIDZdroj.ReadOnly = true;
            this.tbIDZdroj.Size = new System.Drawing.Size(240, 20);
            this.tbIDZdroj.TabIndex = 1;
            this.tbIDZdroj.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ID stavu:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "ID zdroje:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "ID činnosti:";
            // 
            // tbIDCinnost
            // 
            this.tbIDCinnost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIDCinnost.Location = new System.Drawing.Point(75, 128);
            this.tbIDCinnost.Name = "tbIDCinnost";
            this.tbIDCinnost.ReadOnly = true;
            this.tbIDCinnost.Size = new System.Drawing.Size(240, 20);
            this.tbIDCinnost.TabIndex = 5;
            this.tbIDCinnost.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // tbIDStav
            // 
            this.tbIDStav.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIDStav.Location = new System.Drawing.Point(75, 73);
            this.tbIDStav.Name = "tbIDStav";
            this.tbIDStav.ReadOnly = true;
            this.tbIDStav.Size = new System.Drawing.Size(240, 20);
            this.tbIDStav.TabIndex = 3;
            this.tbIDStav.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
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
            this.panel2.Controls.Add(this.panelNapoveda);
            this.panel2.Controls.Add(this.btnZdrojID);
            this.panel2.Controls.Add(this.btnStavID);
            this.panel2.Controls.Add(this.btnCinnostID);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tbIDZdroj);
            this.panel2.Controls.Add(this.tbIDStav);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.tbIDCinnost);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 391);
            this.panel2.TabIndex = 1;
            // 
            // panelNapoveda
            // 
            this.panelNapoveda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNapoveda.Controls.Add(this.groupBox1);
            this.panelNapoveda.Location = new System.Drawing.Point(3, 183);
            this.panelNapoveda.Name = "panelNapoveda";
            this.panelNapoveda.Size = new System.Drawing.Size(328, 205);
            this.panelNapoveda.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNapoveda);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 205);
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
            this.tbNapoveda.Size = new System.Drawing.Size(322, 186);
            this.tbNapoveda.TabIndex = 0;
            this.tbNapoveda.TabStop = false;
            this.tbNapoveda.Text = "";
            // 
            // btnZdrojID
            // 
            this.btnZdrojID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZdrojID.Location = new System.Drawing.Point(225, 47);
            this.btnZdrojID.Name = "btnZdrojID";
            this.btnZdrojID.Size = new System.Drawing.Size(90, 23);
            this.btnZdrojID.TabIndex = 2;
            this.btnZdrojID.Text = "Přiřadit zdroj";
            this.btnZdrojID.UseVisualStyleBackColor = true;
            this.btnZdrojID.Click += new System.EventHandler(this.btnZdrojID_Click);
            // 
            // btnStavID
            // 
            this.btnStavID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStavID.Location = new System.Drawing.Point(225, 99);
            this.btnStavID.Name = "btnStavID";
            this.btnStavID.Size = new System.Drawing.Size(90, 23);
            this.btnStavID.TabIndex = 4;
            this.btnStavID.Text = "Přiřadit stav";
            this.btnStavID.UseVisualStyleBackColor = true;
            this.btnStavID.Click += new System.EventHandler(this.btnStavID_Click);
            // 
            // btnCinnostID
            // 
            this.btnCinnostID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCinnostID.Location = new System.Drawing.Point(225, 154);
            this.btnCinnostID.Name = "btnCinnostID";
            this.btnCinnostID.Size = new System.Drawing.Size(90, 23);
            this.btnCinnostID.TabIndex = 6;
            this.btnCinnostID.Text = "Přiřadit činnost";
            this.btnCinnostID.UseVisualStyleBackColor = true;
            this.btnCinnostID.Click += new System.EventHandler(this.btnCinnostID_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormZdrojeStavyEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormZdrojeStavyEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stav zdroje";
            this.Load += new System.EventHandler(this.FormUzivateleEdit_Load);
            this.Shown += new System.EventHandler(this.FormUzivateleEdit_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUzivateleEdit_KeyDown);
            this.Resize += new System.EventHandler(this.FormUzivateleEdit_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelNapoveda.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbIDZdroj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbIDCinnost;
        private System.Windows.Forms.TextBox tbIDStav;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tbNapoveda;
        private System.Windows.Forms.Button btnCinnostID;
        private System.Windows.Forms.Panel panelNapoveda;
        private System.Windows.Forms.Button btnZdrojID;
        private System.Windows.Forms.Button btnStavID;
    }
}