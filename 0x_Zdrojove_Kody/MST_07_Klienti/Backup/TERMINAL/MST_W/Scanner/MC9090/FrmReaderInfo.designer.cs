namespace Fask.MST_W.Scanner.MC9090
{
    partial class FrmReaderInfo
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
            this.m_txtFWVersion = new System.Windows.Forms.TextBox();
            this.m_txtModNo = new System.Windows.Forms.TextBox();
            this.m_lblFWVersion = new System.Windows.Forms.Label();
            this.m_lblModelNo = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtSDKVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtSerNo = new System.Windows.Forms.TextBox();
            this.lblSerNo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_txtFWVersion
            // 
            this.m_txtFWVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtFWVersion.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.m_txtFWVersion.Location = new System.Drawing.Point(105, 92);
            this.m_txtFWVersion.Name = "m_txtFWVersion";
            this.m_txtFWVersion.ReadOnly = true;
            this.m_txtFWVersion.Size = new System.Drawing.Size(105, 19);
            this.m_txtFWVersion.TabIndex = 47;
            // 
            // m_txtModNo
            // 
            this.m_txtModNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtModNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.m_txtModNo.Location = new System.Drawing.Point(105, 33);
            this.m_txtModNo.Name = "m_txtModNo";
            this.m_txtModNo.ReadOnly = true;
            this.m_txtModNo.Size = new System.Drawing.Size(105, 19);
            this.m_txtModNo.TabIndex = 40;
            // 
            // m_lblFWVersion
            // 
            this.m_lblFWVersion.Location = new System.Drawing.Point(12, 93);
            this.m_lblFWVersion.Name = "m_lblFWVersion";
            this.m_lblFWVersion.Size = new System.Drawing.Size(82, 18);
            this.m_lblFWVersion.Text = "FW Version";
            // 
            // m_lblModelNo
            // 
            this.m_lblModelNo.Location = new System.Drawing.Point(12, 34);
            this.m_lblModelNo.Name = "m_lblModelNo";
            this.m_lblModelNo.Size = new System.Drawing.Size(72, 18);
            this.m_lblModelNo.Text = "Model";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnOk.Location = new System.Drawing.Point(73, 168);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 20);
            this.btnOk.TabIndex = 58;
            this.btnOk.Text = "OK";
            // 
            // txtSDKVersion
            // 
            this.txtSDKVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSDKVersion.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSDKVersion.Location = new System.Drawing.Point(105, 121);
            this.txtSDKVersion.Name = "txtSDKVersion";
            this.txtSDKVersion.ReadOnly = true;
            this.txtSDKVersion.Size = new System.Drawing.Size(105, 19);
            this.txtSDKVersion.TabIndex = 67;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 18);
            this.label1.Text = "SDK Version";
            // 
            // m_txtSerNo
            // 
            this.m_txtSerNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtSerNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.m_txtSerNo.Location = new System.Drawing.Point(105, 62);
            this.m_txtSerNo.Name = "m_txtSerNo";
            this.m_txtSerNo.ReadOnly = true;
            this.m_txtSerNo.Size = new System.Drawing.Size(105, 19);
            this.m_txtSerNo.TabIndex = 76;
            // 
            // lblSerNo
            // 
            this.lblSerNo.Location = new System.Drawing.Point(12, 63);
            this.lblSerNo.Name = "lblSerNo";
            this.lblSerNo.Size = new System.Drawing.Size(91, 18);
            this.lblSerNo.Text = "Serial Number";
            // 
            // FrmReaderInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(223, 200);
            this.Controls.Add(this.m_txtSerNo);
            this.Controls.Add(this.lblSerNo);
            this.Controls.Add(this.txtSDKVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.m_txtFWVersion);
            this.Controls.Add(this.m_txtModNo);
            this.Controls.Add(this.m_lblFWVersion);
            this.Controls.Add(this.m_lblModelNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmReaderInfo";
            this.Text = "Reader Information";
            this.Load += new System.EventHandler(this.ReaderInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtFWVersion;
        private System.Windows.Forms.TextBox m_txtModNo;
        private System.Windows.Forms.Label m_lblFWVersion;
        private System.Windows.Forms.Label m_lblModelNo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtSDKVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtSerNo;
        private System.Windows.Forms.Label lblSerNo;
    }
}