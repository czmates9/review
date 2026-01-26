namespace Fask.Vyroba_W.Korekce
{
    partial class FormKorekce
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
            this.panelComponents = new System.Windows.Forms.Panel();
            this.dateTimePickerDelka = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDuvod = new System.Windows.Forms.ComboBox();
            this.panelButtons.SuspendLayout();
            this.panelComponents.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 224);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(238, 71);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(118, 71);
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
            this.buttonStorno.Size = new System.Drawing.Size(120, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.AutoScroll = true;
            this.panelComponents.Controls.Add(this.dateTimePickerDelka);
            this.panelComponents.Controls.Add(this.label4);
            this.panelComponents.Controls.Add(this.label1);
            this.panelComponents.Controls.Add(this.cbDuvod);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(238, 224);
            // 
            // dateTimePickerDelka
            // 
            this.dateTimePickerDelka.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerDelka.CalendarFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.dateTimePickerDelka.CustomFormat = "HH:mm";
            this.dateTimePickerDelka.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.dateTimePickerDelka.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDelka.Location = new System.Drawing.Point(12, 108);
            this.dateTimePickerDelka.Name = "dateTimePickerDelka";
            this.dateTimePickerDelka.ShowUpDown = true;
            this.dateTimePickerDelka.Size = new System.Drawing.Size(209, 40);
            this.dateTimePickerDelka.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(3, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 23);
            this.label4.Text = "Délka :";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.Text = "Dùvod : ";
            // 
            // cbDuvod
            // 
            this.cbDuvod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDuvod.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.cbDuvod.Location = new System.Drawing.Point(12, 23);
            this.cbDuvod.Name = "cbDuvod";
            this.cbDuvod.Size = new System.Drawing.Size(209, 39);
            this.cbDuvod.TabIndex = 0;
            this.cbDuvod.SelectedIndexChanged += new System.EventHandler(this.cbDuvod_SelectedIndexChanged);
            // 
            // FormKorekce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormKorekce";
            this.Text = "Korekce èasu";
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
        public System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDuvod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerDelka;

    }
}
