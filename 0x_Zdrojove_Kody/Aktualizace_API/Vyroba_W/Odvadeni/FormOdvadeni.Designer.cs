namespace Fask.Vyroba_W.Odvadeni
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
            this.panelOperaceCislo = new System.Windows.Forms.Panel();
            this.panelOperaceRozpracovana = new System.Windows.Forms.Panel();
            this.labelRozpracovanaOperace = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.panelOperaceCislo.SuspendLayout();
            this.panelOperaceRozpracovana.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxVyrobniOperace
            // 
            this.textBoxVyrobniOperace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVyrobniOperace.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.textBoxVyrobniOperace.Location = new System.Drawing.Point(13, 47);
            this.textBoxVyrobniOperace.Name = "textBoxVyrobniOperace";
            this.textBoxVyrobniOperace.Size = new System.Drawing.Size(291, 29);
            this.textBoxVyrobniOperace.TabIndex = 0;
            // 
            // labelVyrobniOperace
            // 
            this.labelVyrobniOperace.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.labelVyrobniOperace.Location = new System.Drawing.Point(13, 10);
            this.labelVyrobniOperace.Name = "labelVyrobniOperace";
            this.labelVyrobniOperace.Size = new System.Drawing.Size(149, 25);
            this.labelVyrobniOperace.Text = "Èíslo operace";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 290);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(327, 71);
            this.panelButtons.GotFocus += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(141, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(186, 71);
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
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panelOperaceCislo
            // 
            this.panelOperaceCislo.Controls.Add(this.labelVyrobniOperace);
            this.panelOperaceCislo.Controls.Add(this.textBoxVyrobniOperace);
            this.panelOperaceCislo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOperaceCislo.Location = new System.Drawing.Point(0, 0);
            this.panelOperaceCislo.Name = "panelOperaceCislo";
            this.panelOperaceCislo.Size = new System.Drawing.Size(327, 91);
            // 
            // panelOperaceRozpracovana
            // 
            this.panelOperaceRozpracovana.Controls.Add(this.labelRozpracovanaOperace);
            this.panelOperaceRozpracovana.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOperaceRozpracovana.Location = new System.Drawing.Point(0, 91);
            this.panelOperaceRozpracovana.Name = "panelOperaceRozpracovana";
            this.panelOperaceRozpracovana.Size = new System.Drawing.Size(327, 104);
            // 
            // labelRozpracovanaOperace
            // 
            this.labelRozpracovanaOperace.Location = new System.Drawing.Point(13, 13);
            this.labelRozpracovanaOperace.Name = "labelRozpracovanaOperace";
            this.labelRozpracovanaOperace.Size = new System.Drawing.Size(291, 20);
            this.labelRozpracovanaOperace.Text = "-";
            // 
            // FormOdvadeni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(327, 361);
            this.ControlBox = false;
            this.Controls.Add(this.panelOperaceRozpracovana);
            this.Controls.Add(this.panelOperaceCislo);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormOdvadeni";
            this.Text = "Odvádìní výroby";
            this.Load += new System.EventHandler(this.FormOdvadeni_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOdvadeni_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelOperaceCislo.ResumeLayout(false);
            this.panelOperaceRozpracovana.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxVyrobniOperace;
        private System.Windows.Forms.Label labelVyrobniOperace;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Panel panelOperaceCislo;
        private System.Windows.Forms.Panel panelOperaceRozpracovana;
        private System.Windows.Forms.Label labelRozpracovanaOperace;

    }
}