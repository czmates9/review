namespace Konzola.Terminal
{
    partial class FormVNC
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
            RemoteViewing.Vnc.VncClient vncClient1 = new RemoteViewing.Vnc.VncClient();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_Format = new System.Windows.Forms.ComboBox();
            this.vncControl1 = new RemoteViewing.Windows.Forms.VncControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cb_Format);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 45);
            this.panel1.TabIndex = 1;
            // 
            // cb_Format
            // 
            this.cb_Format.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Format.FormattingEnabled = true;
            this.cb_Format.Location = new System.Drawing.Point(12, 12);
            this.cb_Format.Name = "cb_Format";
            this.cb_Format.Size = new System.Drawing.Size(296, 21);
            this.cb_Format.TabIndex = 1;
            this.cb_Format.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // vncControl1
            // 
            this.vncControl1.AllowClipboardSharingFromServer = false;
            this.vncControl1.AllowClipboardSharingToServer = false;
            this.vncControl1.AllowInput = true;
            this.vncControl1.AllowRemoteCursor = true;
            this.vncControl1.BackColor = System.Drawing.Color.Black;
            vncClient1.MaxUpdateRate = 15D;
            vncClient1.UserData = null;
            this.vncControl1.Client = vncClient1;
            this.vncControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vncControl1.HideLocalCursor = false;
            this.vncControl1.Location = new System.Drawing.Point(0, 45);
            this.vncControl1.Name = "vncControl1";
            this.vncControl1.PixelFormat = System.Drawing.Imaging.PixelFormat.DontCare;
            this.vncControl1.ScaleFactor = 1F;
            this.vncControl1.Size = new System.Drawing.Size(320, 320);
            this.vncControl1.TabIndex = 0;
            this.vncControl1.Connected += new System.EventHandler(this.vncControl1_Connected);
            this.vncControl1.ConnectionFailed += new System.EventHandler(this.vncControl1_ConnectionFailed);
            this.vncControl1.Closed += new System.EventHandler(this.vncControl1_Closed);
            // 
            // FormVNC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 365);
            this.Controls.Add(this.vncControl1);
            this.Controls.Add(this.panel1);
            this.Name = "FormVNC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormVNC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVNC_FormClosing);
            this.Load += new System.EventHandler(this.FormVNC_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RemoteViewing.Windows.Forms.VncControl vncControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cb_Format;
    }
}