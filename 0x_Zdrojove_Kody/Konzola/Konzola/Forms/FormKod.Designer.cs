namespace Konzola.Forms
{
    partial class FormKod
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_NazevKodu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(0, 448);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(400, 44);
            this.buttonOK.TabIndex = 9;
            this.buttonOK.Text = "Zavřít";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label_NazevKodu
            // 
            this.label_NazevKodu.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_NazevKodu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_NazevKodu.Location = new System.Drawing.Point(0, 0);
            this.label_NazevKodu.Name = "label_NazevKodu";
            this.label_NazevKodu.Size = new System.Drawing.Size(400, 48);
            this.label_NazevKodu.TabIndex = 10;
            this.label_NazevKodu.Text = "Nazev Kodu";
            this.label_NazevKodu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormKod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 492);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_NazevKodu);
            this.Controls.Add(this.buttonOK);
            this.KeyPreview = true;
            this.Name = "FormKod";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "QR Kód";
            this.Load += new System.EventHandler(this.InputBox_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_NazevKodu;
    }
}