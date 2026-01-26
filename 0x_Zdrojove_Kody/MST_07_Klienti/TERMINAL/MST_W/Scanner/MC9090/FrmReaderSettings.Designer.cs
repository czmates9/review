namespace Fask.MST_W.Scanner.MC9090
{
    partial class FrmReaderSettings
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
            this.pnlGen2TagReadParam = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numUpDownStartingQ = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlGen2TagReadParam.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGen2TagReadParam
            // 
            this.pnlGen2TagReadParam.Controls.Add(this.label5);
            this.pnlGen2TagReadParam.Controls.Add(this.label4);
            this.pnlGen2TagReadParam.Controls.Add(this.numUpDownStartingQ);
            this.pnlGen2TagReadParam.Controls.Add(this.label3);
            this.pnlGen2TagReadParam.Location = new System.Drawing.Point(13, 7);
            this.pnlGen2TagReadParam.Name = "pnlGen2TagReadParam";
            this.pnlGen2TagReadParam.Size = new System.Drawing.Size(211, 104);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(10, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(186, 30);
            this.label5.Text = "Choose higher value  if large number  of tags are expected in the field.";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 20);
            this.label4.Text = "Gen2 Tag Read Parameter :";
            // 
            // numUpDownStartingQ
            // 
            this.numUpDownStartingQ.Location = new System.Drawing.Point(95, 31);
            this.numUpDownStartingQ.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numUpDownStartingQ.Name = "numUpDownStartingQ";
            this.numUpDownStartingQ.ReadOnly = true;
            this.numUpDownStartingQ.Size = new System.Drawing.Size(88, 24);
            this.numUpDownStartingQ.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.Tag = "Sets the number of slots in the first Inventory Round.The starting Q should be ch" +
                "osen higher for a larger expected number of tags in the field.";
            this.label3.Text = "Starting Q";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(127, 119);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 20);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(35, 119);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 20);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ReaderSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(239, 153);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pnlGen2TagReadParam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReaderSettings";
            this.Text = "Reader Settings";
            this.Load += new System.EventHandler(this.ReaderSettings_Load);
            this.pnlGen2TagReadParam.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGen2TagReadParam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numUpDownStartingQ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}