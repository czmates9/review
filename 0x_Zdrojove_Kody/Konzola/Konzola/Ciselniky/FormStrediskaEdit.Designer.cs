namespace Konzola.Ciselniky
{
    partial class FormStrediskaEdit
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
            this.tbSklID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSklTyp = new System.Windows.Forms.TextBox();
            this.tbSklDesc = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelNapoveda = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbNapoveda = new System.Windows.Forms.RichTextBox();
            this.tb_ODB_ID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_SKL_ID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSklCarcode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelNapoveda.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Název:";
            // 
            // tbSklID
            // 
            this.tbSklID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSklID.Location = new System.Drawing.Point(87, 21);
            this.tbSklID.Name = "tbSklID";
            this.tbSklID.Size = new System.Drawing.Size(228, 20);
            this.tbSklID.TabIndex = 0;
            this.tbSklID.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Typ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "ID strediska:";
            // 
            // tbSklTyp
            // 
            this.tbSklTyp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSklTyp.Location = new System.Drawing.Point(87, 73);
            this.tbSklTyp.Name = "tbSklTyp";
            this.tbSklTyp.Size = new System.Drawing.Size(228, 20);
            this.tbSklTyp.TabIndex = 2;
            this.tbSklTyp.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // tbSklDesc
            // 
            this.tbSklDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSklDesc.Location = new System.Drawing.Point(87, 47);
            this.tbSklDesc.Name = "tbSklDesc";
            this.tbSklDesc.Size = new System.Drawing.Size(228, 20);
            this.tbSklDesc.TabIndex = 1;
            this.tbSklDesc.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
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
            this.buttonOK.TabIndex = 1;
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
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelNapoveda);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbSklDesc);
            this.panel2.Controls.Add(this.tbSklID);
            this.panel2.Controls.Add(this.tb_ODB_ID);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.tb_SKL_ID);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.tbSklCarcode);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tbSklTyp);
            this.panel2.Controls.Add(this.label2);
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
            this.panelNapoveda.Location = new System.Drawing.Point(3, 189);
            this.panelNapoveda.Name = "panelNapoveda";
            this.panelNapoveda.Size = new System.Drawing.Size(328, 199);
            this.panelNapoveda.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNapoveda);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 199);
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
            this.tbNapoveda.Size = new System.Drawing.Size(322, 180);
            this.tbNapoveda.TabIndex = 0;
            this.tbNapoveda.TabStop = false;
            this.tbNapoveda.Text = "";
            // 
            // tb_ODB_ID
            // 
            this.tb_ODB_ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_ODB_ID.Location = new System.Drawing.Point(87, 160);
            this.tb_ODB_ID.Name = "tb_ODB_ID";
            this.tb_ODB_ID.Size = new System.Drawing.Size(228, 20);
            this.tb_ODB_ID.TabIndex = 5;
            this.tb_ODB_ID.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Odberatel ID:";
            // 
            // tb_SKL_ID
            // 
            this.tb_SKL_ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_SKL_ID.Location = new System.Drawing.Point(87, 134);
            this.tb_SKL_ID.Name = "tb_SKL_ID";
            this.tb_SKL_ID.Size = new System.Drawing.Size(228, 20);
            this.tb_SKL_ID.TabIndex = 4;
            this.tb_SKL_ID.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Sklad ID:";
            // 
            // tbSklCarcode
            // 
            this.tbSklCarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSklCarcode.Location = new System.Drawing.Point(87, 99);
            this.tbSklCarcode.Name = "tbSklCarcode";
            this.tbSklCarcode.Size = new System.Drawing.Size(228, 20);
            this.tbSklCarcode.TabIndex = 3;
            this.tbSklCarcode.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Čár. kód:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormStrediskaEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormStrediskaEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Strediska";
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

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSklID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSklTyp;
        private System.Windows.Forms.TextBox tbSklDesc;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tbNapoveda;
        private System.Windows.Forms.Panel panelNapoveda;
        private System.Windows.Forms.TextBox tbSklCarcode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_ODB_ID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_SKL_ID;
        private System.Windows.Forms.Label label5;
    }
}