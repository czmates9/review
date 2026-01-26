namespace Fask.Vyroba_W.Forms
{
    partial class FormUserLoginWithTimeInput
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
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerDate.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateTimePickerDate.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDate.Location = new System.Drawing.Point(12, 78);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(212, 38);
            this.dateTimePickerDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 75);
            this.label1.Text = "Datum a èas zahájení práce :";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 228);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(238, 67);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(123, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(115, 67);
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
            this.buttonStorno.Size = new System.Drawing.Size(123, 67);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // dateTimePickerTime
            // 
            this.dateTimePickerTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerTime.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateTimePickerTime.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTime.Location = new System.Drawing.Point(12, 122);
            this.dateTimePickerTime.Name = "dateTimePickerTime";
            this.dateTimePickerTime.ShowUpDown = true;
            this.dateTimePickerTime.Size = new System.Drawing.Size(213, 38);
            this.dateTimePickerTime.TabIndex = 1;
            // 
            // FormUserLoginWithTimeInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.dateTimePickerTime);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerDate);
            this.KeyPreview = true;
            this.Name = "FormUserLoginWithTimeInput";
            this.Text = "Pøihlášení uživatele";
            this.Load += new System.EventHandler(this.FormUserLoginWithTimeInput_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUserLoginWithTimeInput_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.DateTimePicker dateTimePickerTime;
    }
}