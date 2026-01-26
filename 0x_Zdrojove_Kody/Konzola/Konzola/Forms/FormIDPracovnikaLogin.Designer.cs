namespace Konzola.Forms
{
    partial class FormIDPracovnikaLogin
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
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelHeslo = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.textBoxHeslo = new System.Windows.Forms.TextBox();
            this.panelButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 201);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(392, 71);
            this.panelButtons.TabIndex = 2;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(272, 71);
            this.buttonOK.TabIndex = 3;
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
            this.buttonStorno.TabIndex = 2;
            this.buttonStorno.Text = "Konec";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click_1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelHeslo);
            this.panel1.Controls.Add(this.textBoxID);
            this.panel1.Controls.Add(this.textBoxHeslo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(392, 201);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID uživatele";
            // 
            // labelHeslo
            // 
            this.labelHeslo.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelHeslo.Location = new System.Drawing.Point(15, 91);
            this.labelHeslo.Name = "labelHeslo";
            this.labelHeslo.Size = new System.Drawing.Size(180, 27);
            this.labelHeslo.TabIndex = 2;
            this.labelHeslo.Text = "Heslo";
            // 
            // textBoxID
            // 
            this.textBoxID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxID.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.textBoxID.Location = new System.Drawing.Point(12, 39);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(368, 33);
            this.textBoxID.TabIndex = 0;
            // 
            // textBoxHeslo
            // 
            this.textBoxHeslo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHeslo.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.textBoxHeslo.Location = new System.Drawing.Point(15, 121);
            this.textBoxHeslo.Name = "textBoxHeslo";
            this.textBoxHeslo.PasswordChar = '*';
            this.textBoxHeslo.Size = new System.Drawing.Size(365, 33);
            this.textBoxHeslo.TabIndex = 1;
            // 
            // FormIDPracovnikaLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 272);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 280);
            this.Name = "FormIDPracovnikaLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zadejte uživatele";
            this.Load += new System.EventHandler(this.FormIDPracovnikaLogin_Load);
            this.Shown += new System.EventHandler(this.FormIDPracovnikaLogin_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormIDPracovnikaLogin_KeyDown);
            this.Resize += new System.EventHandler(this.FormIDPracovnikaLogin_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelHeslo;
        private System.Windows.Forms.TextBox textBoxHeslo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxID;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
    }
}