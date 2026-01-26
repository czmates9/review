namespace FASK.SledovaniVyroby.Module.Vyroba_Agro
{
    partial class InformationUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblText = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.lblWarning = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProhazovani = new System.Windows.Forms.Label();
            this.lblProhazSymbol = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSarze = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblText.Location = new System.Drawing.Point(2, 3);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(315, 44);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "label1";
            // 
            // txtText
            // 
            this.txtText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtText.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtText.Location = new System.Drawing.Point(0, 125);
            this.txtText.MaxLength = 13;
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(320, 40);
            this.txtText.TabIndex = 1;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(0, 50);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(317, 35);
            this.lblWarning.TabIndex = 2;
            this.lblWarning.Text = "-";
            this.lblWarning.Click += new System.EventHandler(this.lblWarning_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(2, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Prohazování:";
            // 
            // lblProhazovani
            // 
            this.lblProhazovani.AutoSize = true;
            this.lblProhazovani.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblProhazovani.Location = new System.Drawing.Point(101, 80);
            this.lblProhazovani.Name = "lblProhazovani";
            this.lblProhazovani.Size = new System.Drawing.Size(15, 20);
            this.lblProhazovani.TabIndex = 6;
            this.lblProhazovani.Text = "-";
            // 
            // lblProhazSymbol
            // 
            this.lblProhazSymbol.AutoSize = true;
            this.lblProhazSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblProhazSymbol.Location = new System.Drawing.Point(189, 80);
            this.lblProhazSymbol.Name = "lblProhazSymbol";
            this.lblProhazSymbol.Size = new System.Drawing.Size(17, 24);
            this.lblProhazSymbol.TabIndex = 7;
            this.lblProhazSymbol.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(3, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "Šarže:";
            // 
            // lblSarze
            // 
            this.lblSarze.AutoSize = true;
            this.lblSarze.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblSarze.Location = new System.Drawing.Point(101, 100);
            this.lblSarze.Name = "lblSarze";
            this.lblSarze.Size = new System.Drawing.Size(15, 20);
            this.lblSarze.TabIndex = 9;
            this.lblSarze.Text = "-";
            // 
            // InformationUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.lblSarze);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblProhazSymbol);
            this.Controls.Add(this.lblProhazovani);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.lblText);
            this.Name = "InformationUC";
            this.Size = new System.Drawing.Size(320, 165);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProhazovani;
        private System.Windows.Forms.Label lblProhazSymbol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSarze;
    }
}
