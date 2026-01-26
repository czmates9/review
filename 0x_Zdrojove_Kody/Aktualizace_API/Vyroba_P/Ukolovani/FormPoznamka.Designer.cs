namespace Fask.Aktualizace_API.Ukolovani
{
    partial class FormPoznamka
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
            this.textBoxPoznamka = new System.Windows.Forms.TextBox();
            this.labelVyrobniOperace = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.ucKeyboard1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPoznamka
            // 
            this.textBoxPoznamka.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPoznamka.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.textBoxPoznamka.Location = new System.Drawing.Point(16, 63);
            this.textBoxPoznamka.Name = "textBoxPoznamka";
            this.textBoxPoznamka.Size = new System.Drawing.Size(690, 30);
            this.textBoxPoznamka.TabIndex = 0;
            // 
            // labelVyrobniOperace
            // 
            this.labelVyrobniOperace.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.labelVyrobniOperace.Location = new System.Drawing.Point(16, 26);
            this.labelVyrobniOperace.Name = "labelVyrobniOperace";
            this.labelVyrobniOperace.Size = new System.Drawing.Size(235, 25);
            this.labelVyrobniOperace.TabIndex = 1;
            this.labelVyrobniOperace.Text = "Poznamka:";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 463);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(727, 71);
            this.panelButtons.TabIndex = 0;
            this.panelButtons.GotFocus += new System.EventHandler(this.panelButtons_Resize);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(345, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(382, 71);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(345, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucKeyboard1.KeyboardType = KeyboardClassLibrary.BoW.Standard_without_arrows;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 110);
            this.ucKeyboard1.MinimumSize = new System.Drawing.Size(469, 256);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(727, 353);
            this.ucKeyboard1.TabIndex = 2;
            this.ucKeyboard1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.ucKeyboard1_UserKeyPressed);
            // 
            // FormPoznamka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(727, 534);
            this.ControlBox = false;
            this.Controls.Add(this.ucKeyboard1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.labelVyrobniOperace);
            this.Controls.Add(this.textBoxPoznamka);
            this.KeyPreview = true;
            this.Name = "FormPoznamka";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Poznamka";
            this.Load += new System.EventHandler(this.FormBase_Load);
            this.Shown += new System.EventHandler(this.FormOdvadeni_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOdvadeni_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPoznamka;
        private System.Windows.Forms.Label labelVyrobniOperace;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private KeyboardClassLibrary.Keyboardcontrol ucKeyboard1;

    }
}