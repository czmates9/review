namespace FASK.SledovaniVyroby.Logging
{
    partial class frmLogin1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin1));
            this.panelComponents = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.serialPortIN = new System.IO.Ports.SerialPort(this.components);
            this.serialPortIN2 = new System.IO.Ports.SerialPort(this.components);
            this.ucKeyboard1 = new KeyboardClassLibrary.Keyboardcontrol();
            this.panelComponents.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.ucKeyboard1);
            this.panelComponents.Controls.Add(this.label2);
            this.panelComponents.Controls.Add(this.txtPWD);
            this.panelComponents.Controls.Add(this.label1);
            this.panelComponents.Controls.Add(this.txtID);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(737, 571);
            this.panelComponents.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(18, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "Heslo";
            // 
            // txtPWD
            // 
            this.txtPWD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPWD.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.txtPWD.Location = new System.Drawing.Point(18, 151);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new System.Drawing.Size(694, 40);
            this.txtPWD.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(487, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID pracovníka";
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.txtID.Location = new System.Drawing.Point(18, 50);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(694, 40);
            this.txtID.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 571);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(737, 71);
            this.panelButtons.TabIndex = 6;
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(341, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(396, 71);
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
            this.buttonStorno.Size = new System.Drawing.Size(341, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // serialPortIN
            // 
            this.serialPortIN.ReadTimeout = 500;
            this.serialPortIN.WriteTimeout = 4500;
            this.serialPortIN.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortIN_DataReceived);
            // 
            // serialPortIN2
            // 
            this.serialPortIN2.ReadTimeout = 500;
            this.serialPortIN2.WriteTimeout = 4500;
            this.serialPortIN2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortIN2_DataReceived);
            // 
            // ucKeyboard1
            // 
            this.ucKeyboard1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucKeyboard1.KeyboardType = KeyboardClassLibrary.BoW.Numeric;
            this.ucKeyboard1.Location = new System.Drawing.Point(0, 213);
            this.ucKeyboard1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucKeyboard1.MinimumSize = new System.Drawing.Size(469, 256);
            this.ucKeyboard1.Name = "ucKeyboard1";
            this.ucKeyboard1.Size = new System.Drawing.Size(737, 358);
            this.ucKeyboard1.TabIndex = 5;
            this.ucKeyboard1.TabStop = false;
            this.ucKeyboard1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(this.ucKeyboard1_UserKeyPressed);
            // 
            // frmLogin1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 642);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogin1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login1";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.frmLogin1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin1_FormClosing);
            this.Load += new System.EventHandler(this.frmLogin1_Load);
            this.panelComponents.ResumeLayout(false);
            this.panelComponents.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelComponents;
        public KeyboardClassLibrary.Keyboardcontrol ucKeyboard1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtPWD;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtID;
        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonStorno;
        public System.IO.Ports.SerialPort serialPortIN;
        public System.IO.Ports.SerialPort serialPortIN2;
    }
}