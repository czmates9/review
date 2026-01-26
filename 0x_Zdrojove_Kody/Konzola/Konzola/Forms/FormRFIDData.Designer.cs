namespace Konzola.Forms
{
    partial class FormRFIDData
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
            this.btn_RFID = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_RFID
            // 
            this.btn_RFID.Location = new System.Drawing.Point(71, 65);
            this.btn_RFID.Name = "btn_RFID";
            this.btn_RFID.Size = new System.Drawing.Size(121, 107);
            this.btn_RFID.TabIndex = 0;
            this.btn_RFID.Text = "button1";
            this.btn_RFID.UseVisualStyleBackColor = true;
            this.btn_RFID.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormRFIDData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 478);
            this.Controls.Add(this.btn_RFID);
            this.Name = "FormRFIDData";
            this.Text = "FormRFIDData";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRFIDData_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_RFID;
    }
}