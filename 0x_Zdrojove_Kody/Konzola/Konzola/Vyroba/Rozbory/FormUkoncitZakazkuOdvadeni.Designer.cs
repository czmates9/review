namespace Konzola.Vyroba
{
    partial class FormUkoncitZakazkuOdvadeni
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
            this.label3 = new System.Windows.Forms.Label();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelQty = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dateTimePickerTIMESTART = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerTIME = new System.Windows.Forms.DateTimePicker();
            this.textBoxQty = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelButtons.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Datum ukončení:";
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 71);
            this.buttonStorno.TabIndex = 3;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 391);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(334, 71);
            this.panelButtons.TabIndex = 4;
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(214, 71);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelQty
            // 
            this.labelQty.AutoSize = true;
            this.labelQty.Location = new System.Drawing.Point(56, 76);
            this.labelQty.Name = "labelQty";
            this.labelQty.Size = new System.Drawing.Size(64, 13);
            this.labelQty.TabIndex = 0;
            this.labelQty.Text = "Počet kusů:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dateTimePickerTIMESTART);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dateTimePickerTIME);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.labelQty);
            this.panel2.Controls.Add(this.textBoxQty);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 462);
            this.panel2.TabIndex = 3;
            // 
            // dateTimePickerTIMESTART
            // 
            this.dateTimePickerTIMESTART.Checked = false;
            this.dateTimePickerTIMESTART.CustomFormat = "HH:mm:ss dd.MM.yyyy";
            this.dateTimePickerTIMESTART.Enabled = false;
            this.dateTimePickerTIMESTART.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTIMESTART.Location = new System.Drawing.Point(129, 21);
            this.dateTimePickerTIMESTART.Name = "dateTimePickerTIMESTART";
            this.dateTimePickerTIMESTART.Size = new System.Drawing.Size(170, 20);
            this.dateTimePickerTIMESTART.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Datum zahájení:";
            // 
            // dateTimePickerTIME
            // 
            this.dateTimePickerTIME.Checked = false;
            this.dateTimePickerTIME.CustomFormat = "HH:mm:ss dd.MM.yyyy";
            this.dateTimePickerTIME.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTIME.Location = new System.Drawing.Point(129, 47);
            this.dateTimePickerTIME.Name = "dateTimePickerTIME";
            this.dateTimePickerTIME.Size = new System.Drawing.Size(170, 20);
            this.dateTimePickerTIME.TabIndex = 1;
            // 
            // textBoxQty
            // 
            this.textBoxQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQty.Location = new System.Drawing.Point(129, 73);
            this.textBoxQty.Name = "textBoxQty";
            this.textBoxQty.Size = new System.Drawing.Size(170, 20);
            this.textBoxQty.TabIndex = 2;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormUkoncitZakazkuOdvadeni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 462);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Name = "FormUkoncitZakazkuOdvadeni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormVPHEdit";
            this.Load += new System.EventHandler(this.FormVPHEdit_Load);
            this.Shown += new System.EventHandler(this.FormVPHEdit_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormVPHEdit_KeyDown);
            this.Resize += new System.EventHandler(this.FormVPHEdit_Resize);
            this.panelButtons.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelQty;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxQty;
        private System.Windows.Forms.DateTimePicker dateTimePickerTIME;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DateTimePicker dateTimePickerTIMESTART;
        private System.Windows.Forms.Label label1;
    }
}