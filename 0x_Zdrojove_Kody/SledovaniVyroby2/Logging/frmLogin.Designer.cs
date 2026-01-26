namespace FASK.SledovaniVyroby.Logging
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.serialPortIN = new System.IO.Ports.SerialPort(this.components);
            this.serialPortIN2 = new System.IO.Ports.SerialPort(this.components);
            this.SuspendLayout();
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtID.Location = new System.Drawing.Point(124, 12);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(179, 38);
            this.txtID.TabIndex = 1;
            // 
            // txtPWD
            // 
            this.txtPWD.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPWD.Location = new System.Drawing.Point(124, 67);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.Size = new System.Drawing.Size(179, 38);
            this.txtPWD.TabIndex = 3;
            this.txtPWD.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(9, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "Heslo : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(165, 125);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(5);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(150, 50);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonStorno.Location = new System.Drawing.Point(5, 125);
            this.buttonStorno.Margin = new System.Windows.Forms.Padding(5);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(150, 50);
            this.buttonStorno.TabIndex = 5;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.UseVisualStyleBackColor = true;
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
            // frmLogin
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonStorno;
            this.ClientSize = new System.Drawing.Size(328, 186);
            this.ControlBox = false;
            this.Controls.Add(this.buttonStorno);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPWD);
            this.Controls.Add(this.txtID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.frmLogin_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonStorno;
        public System.Windows.Forms.TextBox txtID;
        public System.Windows.Forms.TextBox txtPWD;
        private System.IO.Ports.SerialPort serialPortIN;
        private System.IO.Ports.SerialPort serialPortIN2;
    }
}