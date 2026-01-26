namespace Fask.Aktualizace_API.Forms
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
            this.ucKeyboard1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusPracovnik = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelButtons.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.dateTimePickerDate.Size = new System.Drawing.Size(531, 38);
            this.dateTimePickerDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(531, 75);
            this.label1.TabIndex = 2;
            this.label1.Text = "Datum a èas zahájení práce :";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 540);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(727, 67);
            this.panelButtons.TabIndex = 2;
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(123, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(604, 67);
            this.buttonOK.TabIndex = 0;
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
            this.buttonStorno.TabIndex = 1;
            this.buttonStorno.Text = "Zpìt";
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
            this.dateTimePickerTime.Size = new System.Drawing.Size(532, 38);
            this.dateTimePickerTime.TabIndex = 1;
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucKeyboard1.KeyboardType = KeyboardClassLibrary.BoW.Numeric;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 182);
            this.ucKeyboard1.MinimumSize = new System.Drawing.Size(469, 256);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(727, 358);
            this.ucKeyboard1.TabIndex = 5;
            this.ucKeyboard1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.ucKeyboard1_UserKeyPressed);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusPracovnik});
            this.statusStrip1.Location = new System.Drawing.Point(0, 160);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(727, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusPracovnik
            // 
            this.toolStripStatusPracovnik.Name = "toolStripStatusPracovnik";
            this.toolStripStatusPracovnik.Size = new System.Drawing.Size(20, 17);
            this.toolStripStatusPracovnik.Text = "P: ";
            // 
            // FormUserLoginWithTimeInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(744, 594);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ucKeyboard1);
            this.Controls.Add(this.dateTimePickerTime);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerDate);
            this.KeyPreview = true;
            this.Name = "FormUserLoginWithTimeInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pøihlášení uživatele";
            this.Load += new System.EventHandler(this.FormUserLoginWithTimeInput_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUserLoginWithTimeInput_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.DateTimePicker dateTimePickerTime;
        private KeyboardClassLibrary.Keyboardcontrol ucKeyboard1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusPracovnik;
    }
}