namespace Definition_SQL_Struncture
{
    partial class Form_Main
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
            this.btn_DS_Info = new System.Windows.Forms.Button();
            this.btn_Dok = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_DS_Info
            // 
            this.btn_DS_Info.Location = new System.Drawing.Point(12, 12);
            this.btn_DS_Info.Name = "btn_DS_Info";
            this.btn_DS_Info.Size = new System.Drawing.Size(127, 58);
            this.btn_DS_Info.TabIndex = 0;
            this.btn_DS_Info.Text = "DS_Informations";
            this.btn_DS_Info.UseVisualStyleBackColor = true;
            this.btn_DS_Info.Click += new System.EventHandler(this.btn_DS_Info_Click);
            // 
            // btn_Dok
            // 
            this.btn_Dok.Location = new System.Drawing.Point(145, 12);
            this.btn_Dok.Name = "btn_Dok";
            this.btn_Dok.Size = new System.Drawing.Size(127, 58);
            this.btn_Dok.TabIndex = 0;
            this.btn_Dok.Text = "Dokumentace";
            this.btn_Dok.UseVisualStyleBackColor = true;
            this.btn_Dok.Click += new System.EventHandler(this.btn_Dok_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(260, 58);
            this.button1.TabIndex = 1;
            this.button1.Text = "Konec";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 152);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Dok);
            this.Controls.Add(this.btn_DS_Info);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "T-SQL tool by TaD";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_DS_Info;
        private System.Windows.Forms.Button btn_Dok;
        private System.Windows.Forms.Button button1;
    }
}