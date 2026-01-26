namespace Konzola.Tisk
{
    partial class FormTiskovaSablonaEdit
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
            this.label3 = new System.Windows.Forms.Label();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tB_Nazev = new System.Windows.Forms.TextBox();
            this.tB_NazevOkna = new System.Windows.Forms.TextBox();
            this.tB_FormalCesta = new System.Windows.Forms.TextBox();
            this.tB_Machineid = new System.Windows.Forms.TextBox();
            this.tB_Loginid = new System.Windows.Forms.TextBox();
            this.tB_Typ = new System.Windows.Forms.TextBox();
            this.tB_Ord = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Název okna:";
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(185, 71);
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
            this.panelButtons.Size = new System.Drawing.Size(383, 71);
            this.panelButtons.TabIndex = 4;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(185, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(198, 71);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Název:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tB_Nazev);
            this.panel2.Controls.Add(this.tB_NazevOkna);
            this.panel2.Controls.Add(this.tB_FormalCesta);
            this.panel2.Controls.Add(this.tB_Machineid);
            this.panel2.Controls.Add(this.tB_Loginid);
            this.panel2.Controls.Add(this.tB_Typ);
            this.panel2.Controls.Add(this.tB_Ord);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(383, 462);
            this.panel2.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Typ:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Loginid:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Machineid:";
            // 
            // tB_Nazev
            // 
            this.tB_Nazev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_Nazev.Location = new System.Drawing.Point(93, 47);
            this.tB_Nazev.Name = "tB_Nazev";
            this.tB_Nazev.Size = new System.Drawing.Size(241, 20);
            this.tB_Nazev.TabIndex = 2;
            // 
            // tB_NazevOkna
            // 
            this.tB_NazevOkna.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_NazevOkna.Location = new System.Drawing.Point(93, 21);
            this.tB_NazevOkna.Name = "tB_NazevOkna";
            this.tB_NazevOkna.Size = new System.Drawing.Size(241, 20);
            this.tB_NazevOkna.TabIndex = 1;
            this.tB_NazevOkna.TextChanged += new System.EventHandler(this.textBoxId_TextChanged);
            // 
            // tB_FormalCesta
            // 
            this.tB_FormalCesta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_FormalCesta.Location = new System.Drawing.Point(93, 176);
            this.tB_FormalCesta.Name = "tB_FormalCesta";
            this.tB_FormalCesta.Size = new System.Drawing.Size(241, 20);
            this.tB_FormalCesta.TabIndex = 7;
            // 
            // tB_Machineid
            // 
            this.tB_Machineid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_Machineid.Location = new System.Drawing.Point(93, 151);
            this.tB_Machineid.Name = "tB_Machineid";
            this.tB_Machineid.Size = new System.Drawing.Size(241, 20);
            this.tB_Machineid.TabIndex = 6;
            // 
            // tB_Loginid
            // 
            this.tB_Loginid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_Loginid.Location = new System.Drawing.Point(93, 125);
            this.tB_Loginid.Name = "tB_Loginid";
            this.tB_Loginid.Size = new System.Drawing.Size(241, 20);
            this.tB_Loginid.TabIndex = 5;
            // 
            // tB_Typ
            // 
            this.tB_Typ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_Typ.Location = new System.Drawing.Point(93, 99);
            this.tB_Typ.Name = "tB_Typ";
            this.tB_Typ.Size = new System.Drawing.Size(241, 20);
            this.tB_Typ.TabIndex = 4;
            // 
            // tB_Ord
            // 
            this.tB_Ord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_Ord.Location = new System.Drawing.Point(93, 73);
            this.tB_Ord.Name = "tB_Ord";
            this.tB_Ord.Size = new System.Drawing.Size(241, 20);
            this.tB_Ord.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Formulář cesta:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ord:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormTiskovaSablonaEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 462);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Name = "FormTiskovaSablonaEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormTiskovaSablonaEdit";
            this.Load += new System.EventHandler(this.FormTiskovaSablonaEdit_Load);
            this.Shown += new System.EventHandler(this.FormTiskovaSablonaEdit_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormTiskovaSablonaEdit_KeyDown);
            this.Resize += new System.EventHandler(this.FormTiskovaSablonaEdit_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tB_Nazev;
        private System.Windows.Forms.TextBox tB_NazevOkna;
        private System.Windows.Forms.TextBox tB_Ord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tB_FormalCesta;
        private System.Windows.Forms.TextBox tB_Machineid;
        private System.Windows.Forms.TextBox tB_Loginid;
        private System.Windows.Forms.TextBox tB_Typ;
        private System.Windows.Forms.Label label5;
    }
}