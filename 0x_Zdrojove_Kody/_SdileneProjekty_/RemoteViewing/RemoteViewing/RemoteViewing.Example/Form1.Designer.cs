namespace RemoteViewing.Example
{
    partial class Form1
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
            RemoteViewing.Vnc.VncClient vncClient2 = new RemoteViewing.Vnc.VncClient();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cb_Format = new System.Windows.Forms.ComboBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.vncControl1 = new RemoteViewing.Windows.Forms.VncControl();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(316, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save Format";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cb_Format
            // 
            this.cb_Format.FormattingEnabled = true;
            this.cb_Format.Location = new System.Drawing.Point(93, 14);
            this.cb_Format.Name = "cb_Format";
            this.cb_Format.Size = new System.Drawing.Size(217, 21);
            this.cb_Format.TabIndex = 3;
            this.cb_Format.SelectedIndexChanged += new System.EventHandler(this.cb_Format_SelectedIndexChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(93, 41);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(217, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // vncControl1
            // 
            this.vncControl1.AllowClipboardSharingFromServer = false;
            this.vncControl1.AllowClipboardSharingToServer = false;
            this.vncControl1.AllowInput = true;
            this.vncControl1.AllowRemoteCursor = true;
            this.vncControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vncControl1.BackColor = System.Drawing.Color.Black;
            vncClient2.MaxUpdateRate = 15D;
            vncClient2.UserData = null;
            this.vncControl1.Client = vncClient2;
            this.vncControl1.HideLocalCursor = false;
            this.vncControl1.Location = new System.Drawing.Point(12, 94);
            this.vncControl1.Name = "vncControl1";
            this.vncControl1.PixelFormat = System.Drawing.Imaging.PixelFormat.DontCare;
            this.vncControl1.ScaleFactor = 1F;
            this.vncControl1.Size = new System.Drawing.Size(396, 351);
            this.vncControl1.TabIndex = 0;
            this.vncControl1.Connected += new System.EventHandler(this.vncControl1_Connected);
            this.vncControl1.ConnectionFailed += new System.EventHandler(this.vncControl1_ConnectionFailed);
            this.vncControl1.Closed += new System.EventHandler(this.vncControl1_Closed);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 457);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.cb_Format);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.vncControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.VncControl vncControl1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cb_Format;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}

