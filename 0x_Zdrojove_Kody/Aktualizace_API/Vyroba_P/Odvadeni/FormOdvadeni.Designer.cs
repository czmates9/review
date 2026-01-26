namespace Fask.Aktualizace_API.Odvadeni
{
    partial class FormOdvadeni
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
            this.textBoxVyrobniOperace = new System.Windows.Forms.TextBox();
            this.labelVyrobniOperace = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.button_zrusitoperaci = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.ucKeyboard1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxVyrobniOperace
            // 
            this.textBoxVyrobniOperace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVyrobniOperace.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.textBoxVyrobniOperace.Location = new System.Drawing.Point(16, 63);
            this.textBoxVyrobniOperace.Name = "textBoxVyrobniOperace";
            this.textBoxVyrobniOperace.Size = new System.Drawing.Size(690, 30);
            this.textBoxVyrobniOperace.TabIndex = 0;
            // 
            // labelVyrobniOperace
            // 
            this.labelVyrobniOperace.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.labelVyrobniOperace.Location = new System.Drawing.Point(16, 26);
            this.labelVyrobniOperace.Name = "labelVyrobniOperace";
            this.labelVyrobniOperace.Size = new System.Drawing.Size(235, 25);
            this.labelVyrobniOperace.TabIndex = 1;
            this.labelVyrobniOperace.Text = "Položka VP (EAN): ";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.button_zrusitoperaci);
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
            // button_zrusitoperaci
            // 
            this.button_zrusitoperaci.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_zrusitoperaci.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.button_zrusitoperaci.Location = new System.Drawing.Point(196, 0);
            this.button_zrusitoperaci.Name = "button_zrusitoperaci";
            this.button_zrusitoperaci.Size = new System.Drawing.Size(322, 71);
            this.button_zrusitoperaci.TabIndex = 2;
            this.button_zrusitoperaci.Text = "Zrušit poslední operaci";
            this.button_zrusitoperaci.UseVisualStyleBackColor = true;
            this.button_zrusitoperaci.Click += new System.EventHandler(this.button_zrusitoperaci_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(518, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(209, 71);
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
            this.buttonStorno.Size = new System.Drawing.Size(196, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucKeyboard1.KeyboardType = KeyboardClassLibrary.BoW.Numeric;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 110);
            this.ucKeyboard1.MinimumSize = new System.Drawing.Size(469, 256);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(727, 353);
            this.ucKeyboard1.TabIndex = 2;
            this.ucKeyboard1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.ucKeyboard1_UserKeyPressed);
            // 
            // FormOdvadeni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(727, 534);
            this.ControlBox = false;
            this.Controls.Add(this.ucKeyboard1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.labelVyrobniOperace);
            this.Controls.Add(this.textBoxVyrobniOperace);
            this.KeyPreview = true;
            this.Name = "FormOdvadeni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Odvádìní výroby";
            this.Load += new System.EventHandler(this.FormBase_Load);
            this.Shown += new System.EventHandler(this.FormOdvadeni_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOdvadeni_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxVyrobniOperace;
        private System.Windows.Forms.Label labelVyrobniOperace;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private KeyboardClassLibrary.Keyboardcontrol ucKeyboard1;
        private System.Windows.Forms.Button button_zrusitoperaci;

    }
}