namespace Fask.Aktualizace_API.OdvadeniNadop
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.ucKeyboard1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.panelOdchody = new System.Windows.Forms.Panel();
            this.buttonKonecPrestavky = new System.Windows.Forms.Button();
            this.buttonOdchodZPracoviste = new System.Windows.Forms.Button();
            this.buttonZahajeniPrestavky = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusPracovnik = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelButtons.SuspendLayout();
            this.panelOdchody.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxVyrobniOperace
            // 
            this.textBoxVyrobniOperace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVyrobniOperace.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.textBoxVyrobniOperace.Location = new System.Drawing.Point(16, 63);
            this.textBoxVyrobniOperace.Name = "textBoxVyrobniOperace";
            this.textBoxVyrobniOperace.Size = new System.Drawing.Size(580, 30);
            this.textBoxVyrobniOperace.TabIndex = 0;
            // 
            // labelVyrobniOperace
            // 
            this.labelVyrobniOperace.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.labelVyrobniOperace.Location = new System.Drawing.Point(16, 26);
            this.labelVyrobniOperace.Name = "labelVyrobniOperace";
            this.labelVyrobniOperace.Size = new System.Drawing.Size(235, 25);
            this.labelVyrobniOperace.TabIndex = 1;
            this.labelVyrobniOperace.Text = "Zakázka :";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 596);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(710, 71);
            this.panelButtons.TabIndex = 0;
            this.panelButtons.GotFocus += new System.EventHandler(this.panelButtons_Resize);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(141, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(569, 71);
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
            this.buttonStorno.Size = new System.Drawing.Size(141, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Zpìt";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucKeyboard1.KeyboardType = KeyboardClassLibrary.BoW.Numeric;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 238);
            this.ucKeyboard1.MinimumSize = new System.Drawing.Size(469, 256);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(710, 358);
            this.ucKeyboard1.TabIndex = 2;
            this.ucKeyboard1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.ucKeyboard1_UserKeyPressed);
            // 
            // panelOdchody
            // 
            this.panelOdchody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOdchody.Controls.Add(this.buttonKonecPrestavky);
            this.panelOdchody.Controls.Add(this.buttonOdchodZPracoviste);
            this.panelOdchody.Controls.Add(this.buttonZahajeniPrestavky);
            this.panelOdchody.Location = new System.Drawing.Point(20, 116);
            this.panelOdchody.Name = "panelOdchody";
            this.panelOdchody.Size = new System.Drawing.Size(576, 100);
            this.panelOdchody.TabIndex = 3;
            // 
            // buttonKonecPrestavky
            // 
            this.buttonKonecPrestavky.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonKonecPrestavky.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonKonecPrestavky.Location = new System.Drawing.Point(141, 0);
            this.buttonKonecPrestavky.Name = "buttonKonecPrestavky";
            this.buttonKonecPrestavky.Size = new System.Drawing.Size(294, 100);
            this.buttonKonecPrestavky.TabIndex = 3;
            this.buttonKonecPrestavky.Text = "Konec pøestávky";
            this.buttonKonecPrestavky.Click += new System.EventHandler(this.buttonKonecPrestavky_Click);
            // 
            // buttonOdchodZPracoviste
            // 
            this.buttonOdchodZPracoviste.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOdchodZPracoviste.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOdchodZPracoviste.Location = new System.Drawing.Point(435, 0);
            this.buttonOdchodZPracoviste.Name = "buttonOdchodZPracoviste";
            this.buttonOdchodZPracoviste.Size = new System.Drawing.Size(141, 100);
            this.buttonOdchodZPracoviste.TabIndex = 2;
            this.buttonOdchodZPracoviste.Text = "Odchod z pracovištì";
            this.buttonOdchodZPracoviste.Click += new System.EventHandler(this.buttonOdchodZPracoviste_Click);
            // 
            // buttonZahajeniPrestavky
            // 
            this.buttonZahajeniPrestavky.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonZahajeniPrestavky.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonZahajeniPrestavky.Location = new System.Drawing.Point(0, 0);
            this.buttonZahajeniPrestavky.Name = "buttonZahajeniPrestavky";
            this.buttonZahajeniPrestavky.Size = new System.Drawing.Size(141, 100);
            this.buttonZahajeniPrestavky.TabIndex = 1;
            this.buttonZahajeniPrestavky.Text = "Zahájení pøestávky";
            this.buttonZahajeniPrestavky.Click += new System.EventHandler(this.buttonZahajeniPrestavky_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusPracovnik});
            this.statusStrip1.Location = new System.Drawing.Point(0, 216);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(710, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusPracovnik
            // 
            this.toolStripStatusPracovnik.Name = "toolStripStatusPracovnik";
            this.toolStripStatusPracovnik.Size = new System.Drawing.Size(20, 17);
            this.toolStripStatusPracovnik.Text = "P: ";
            // 
            // FormOdvadeni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(727, 534);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelOdchody);
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
            this.panelOdchody.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.Panel panelOdchody;
        public System.Windows.Forms.Button buttonKonecPrestavky;
        public System.Windows.Forms.Button buttonOdchodZPracoviste;
        public System.Windows.Forms.Button buttonZahajeniPrestavky;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusPracovnik;

    }
}