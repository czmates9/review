namespace Fask.Vyroba_P.OdvadeniNadop
{
    partial class FormOperace
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
            this.labelInfo = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.tlOperations = new System.Windows.Forms.TableLayoutPanel();
            this.labelZakazka = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelInfo.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.labelInfo.Location = new System.Drawing.Point(0, 23);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(727, 25);
            this.labelInfo.TabIndex = 1;
            this.labelInfo.Text = "Info";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 463);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(727, 71);
            this.panelButtons.TabIndex = 0;
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(727, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Zpìt";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // tlOperations
            // 
            this.tlOperations.AutoSize = true;
            this.tlOperations.ColumnCount = 2;
            this.tlOperations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlOperations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlOperations.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlOperations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlOperations.Location = new System.Drawing.Point(0, 48);
            this.tlOperations.Name = "tlOperations";
            this.tlOperations.Padding = new System.Windows.Forms.Padding(20);
            this.tlOperations.RowCount = 3;
            this.tlOperations.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlOperations.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlOperations.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlOperations.Size = new System.Drawing.Size(727, 415);
            this.tlOperations.TabIndex = 2;
            // 
            // labelZakazka
            // 
            this.labelZakazka.AutoSize = true;
            this.labelZakazka.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelZakazka.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.labelZakazka.Location = new System.Drawing.Point(0, 0);
            this.labelZakazka.Name = "labelZakazka";
            this.labelZakazka.Size = new System.Drawing.Size(87, 23);
            this.labelZakazka.TabIndex = 3;
            this.labelZakazka.Text = "Zakázka";
            // 
            // FormOperace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(727, 534);
            this.ControlBox = false;
            this.Controls.Add(this.tlOperations);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.labelZakazka);
            this.Controls.Add(this.panelButtons);
            this.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.KeyPreview = true;
            this.Name = "FormOperace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Odvádìní výroby";
            this.Load += new System.EventHandler(this.FormBase_Load);
            this.Shown += new System.EventHandler(this.FormOperace_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOperace_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.TableLayoutPanel tlOperations;
        private System.Windows.Forms.Label labelZakazka;

    }
}