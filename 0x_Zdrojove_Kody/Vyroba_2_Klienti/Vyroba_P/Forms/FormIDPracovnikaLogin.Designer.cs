namespace Fask.Vyroba_P.Forms
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.labelHeslo = new System.Windows.Forms.Label();
            this.textBoxHeslo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelComponents.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(607, 71);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.keyboardcontrol1);
            this.panelComponents.Controls.Add(this.labelHeslo);
            this.panelComponents.Controls.Add(this.textBoxHeslo);
            this.panelComponents.Controls.Add(this.label1);
            this.panelComponents.Controls.Add(this.textBoxID);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(727, 554);
            this.panelComponents.TabIndex = 0;
            // 
            // keyboardcontrol1
            // 
            this.keyboardcontrol1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.keyboardcontrol1.KeyboardType = KeyboardClassLibrary.BoW.Numeric;
            this.keyboardcontrol1.Location = new System.Drawing.Point(0, 196);
            this.keyboardcontrol1.MinimumSize = new System.Drawing.Size(469, 256);
            this.keyboardcontrol1.Name = "keyboardcontrol1";
            this.keyboardcontrol1.Size = new System.Drawing.Size(727, 358);
            this.keyboardcontrol1.TabIndex = 3;
            this.keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // labelHeslo
            // 
            this.labelHeslo.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelHeslo.Location = new System.Drawing.Point(21, 102);
            this.labelHeslo.Name = "labelHeslo";
            this.labelHeslo.Size = new System.Drawing.Size(180, 27);
            this.labelHeslo.TabIndex = 2;
            this.labelHeslo.Text = "Heslo";
            // 
            // textBoxHeslo
            // 
            this.textBoxHeslo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHeslo.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.textBoxHeslo.Location = new System.Drawing.Point(21, 132);
            this.textBoxHeslo.Name = "textBoxHeslo";
            this.textBoxHeslo.PasswordChar = '*';
            this.textBoxHeslo.Size = new System.Drawing.Size(684, 33);
            this.textBoxHeslo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID pracovníka";
            // 
            // textBoxID
            // 
            this.textBoxID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxID.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.textBoxID.Location = new System.Drawing.Point(18, 50);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(684, 33);
            this.textBoxID.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 554);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(727, 71);
            this.panelButtons.TabIndex = 1;
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
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
            // FormIDPracovnikaLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(727, 625);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormIDPracovnikaLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zadejte pracovníka";
            this.Activated += new System.EventHandler(this.FormIDPracovnikaLogin_Activated);
            this.Load += new System.EventHandler(this.FormIDPracovnikaLogin_Load);
            this.Shown += new System.EventHandler(this.FormIDPracovnikaLogin_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormIDPracovnikaLogin_KeyDown);
            this.panelComponents.ResumeLayout(false);
            this.panelComponents.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Panel panelComponents;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelHeslo;
        private System.Windows.Forms.TextBox textBoxHeslo;
        private KeyboardClassLibrary.Keyboardcontrol keyboardcontrol1;
    }
}
