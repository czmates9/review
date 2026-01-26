namespace Konzola.Ciselniky
{
    partial class FormLokaceTypyEdit
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
            this.tbType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelNapoveda = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbNapoveda = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.cbLokaceBezna = new System.Windows.Forms.CheckBox();
            this.cbLokaceVychozi = new System.Windows.Forms.CheckBox();
            this.cbLokacePrijmova = new System.Windows.Forms.CheckBox();
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelNapoveda.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbType
            // 
            this.tbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbType.Location = new System.Drawing.Point(75, 21);
            this.tbType.Name = "tbType";
            this.tbType.Size = new System.Drawing.Size(287, 20);
            this.tbType.TabIndex = 1;
            this.tbType.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Typ:";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 391);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(381, 71);
            this.panelButtons.TabIndex = 2;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(261, 71);
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
            this.panel2.Controls.Add(this.cbLokacePrijmova);
            this.panel2.Controls.Add(this.cbLokaceVychozi);
            this.panel2.Controls.Add(this.cbLokaceBezna);
            this.panel2.Controls.Add(this.panelNapoveda);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tbDescription);
            this.panel2.Controls.Add(this.tbType);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(381, 391);
            this.panel2.TabIndex = 1;
            // 
            // panelNapoveda
            // 
            this.panelNapoveda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNapoveda.Controls.Add(this.groupBox1);
            this.panelNapoveda.Location = new System.Drawing.Point(3, 143);
            this.panelNapoveda.Name = "panelNapoveda";
            this.panelNapoveda.Size = new System.Drawing.Size(375, 245);
            this.panelNapoveda.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNapoveda);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 245);
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
            this.tbNapoveda.Size = new System.Drawing.Size(369, 226);
            this.tbNapoveda.TabIndex = 0;
            this.tbNapoveda.TabStop = false;
            this.tbNapoveda.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Označení:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tbDescription
            // 
            this.tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescription.Location = new System.Drawing.Point(75, 47);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(287, 20);
            this.tbDescription.TabIndex = 2;
            this.tbDescription.Enter += new System.EventHandler(this.formZdrojeEdit_Enter);
            // 
            // cbLokaceBezna
            // 
            this.cbLokaceBezna.AutoSize = true;
            this.cbLokaceBezna.Location = new System.Drawing.Point(75, 74);
            this.cbLokaceBezna.Name = "cbLokaceBezna";
            this.cbLokaceBezna.Size = new System.Drawing.Size(91, 17);
            this.cbLokaceBezna.TabIndex = 3;
            this.cbLokaceBezna.Text = "Běžná lokace";
            this.cbLokaceBezna.UseVisualStyleBackColor = true;
            // 
            // cbLokaceVychozi
            // 
            this.cbLokaceVychozi.AutoSize = true;
            this.cbLokaceVychozi.Location = new System.Drawing.Point(75, 97);
            this.cbLokaceVychozi.Name = "cbLokaceVychozi";
            this.cbLokaceVychozi.Size = new System.Drawing.Size(100, 17);
            this.cbLokaceVychozi.TabIndex = 4;
            this.cbLokaceVychozi.Text = "Výchozí lokace";
            this.cbLokaceVychozi.UseVisualStyleBackColor = true;
            // 
            // cbLokacePrijmova
            // 
            this.cbLokacePrijmova.AutoSize = true;
            this.cbLokacePrijmova.Location = new System.Drawing.Point(75, 120);
            this.cbLokacePrijmova.Name = "cbLokacePrijmova";
            this.cbLokacePrijmova.Size = new System.Drawing.Size(104, 17);
            this.cbLokacePrijmova.TabIndex = 5;
            this.cbLokacePrijmova.Text = "Příjmová lokace";
            this.cbLokacePrijmova.UseVisualStyleBackColor = true;
            // 
            // FormLokaceTypyEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormLokaceTypyEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Typ lokace";
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

        private System.Windows.Forms.TextBox tbType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tbNapoveda;
        private System.Windows.Forms.Panel panelNapoveda;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.CheckBox cbLokacePrijmova;
        private System.Windows.Forms.CheckBox cbLokaceVychozi;
        private System.Windows.Forms.CheckBox cbLokaceBezna;
    }
}