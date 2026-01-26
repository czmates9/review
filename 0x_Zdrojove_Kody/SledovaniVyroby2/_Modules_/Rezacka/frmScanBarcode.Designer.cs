namespace FASK.SledovaniVyroby.Module.Rezacka
{
    partial class frmScanBarcode
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
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.serialPortIN = new System.IO.Ports.SerialPort(this.components);
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(266, 20);
            this.lblInfo.TabIndex = 85;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtBarcode.Location = new System.Drawing.Point(0, 20);
            this.txtBarcode.MaxLength = 37;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(266, 20);
            this.txtBarcode.TabIndex = 86;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(179, 46);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 91;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click_1);
            // 
            // serialPortIN
            // 
            this.serialPortIN.ReadTimeout = 500;
            this.serialPortIN.WriteTimeout = 5000;
            this.serialPortIN.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortIN_DataReceived_1);
            // 
            // frmScanBarcode
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 72);
            this.ControlBox = false;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblInfo);
            this.Name = "frmScanBarcode";
            this.Text = "Scanování čárového kódu";
            this.Load += new System.EventHandler(this.frmScanBarcode_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmScanBarcode_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnOk;
        private System.IO.Ports.SerialPort serialPortIN;
    }
}