namespace Fask.Vyroba_P.Korekce
{
    partial class FormKorekceStart
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
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.tlCorrections = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.panelComponents.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 562);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(719, 71);
            this.panelButtons.TabIndex = 1;
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(719, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Zpìt";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.AutoScroll = true;
            this.panelComponents.Controls.Add(this.tlCorrections);
            this.panelComponents.Controls.Add(this.label1);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(719, 562);
            this.panelComponents.TabIndex = 0;
            // 
            // tlCorrections
            // 
            this.tlCorrections.ColumnCount = 2;
            this.tlCorrections.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.15906F));
            this.tlCorrections.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.84094F));
            this.tlCorrections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlCorrections.Location = new System.Drawing.Point(0, 25);
            this.tlCorrections.Name = "tlCorrections";
            this.tlCorrections.Padding = new System.Windows.Forms.Padding(20);
            this.tlCorrections.RowCount = 2;
            this.tlCorrections.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlCorrections.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlCorrections.Size = new System.Drawing.Size(719, 537);
            this.tlCorrections.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(719, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Korekce èasu - zaèátek";
            // 
            // FormKorekceStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(719, 633);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.KeyPreview = true;
            this.Name = "FormKorekceStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Korekce èasu - zaèátek";
            this.Load += new System.EventHandler(this.FormBaseButtonOKStorno_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormBaseButtonOKStorno_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelComponents.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        public System.Windows.Forms.Panel panelComponents;
        private System.Windows.Forms.TableLayoutPanel tlCorrections;
        private System.Windows.Forms.Label label1;

    }
}
