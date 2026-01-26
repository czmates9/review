
namespace Fask.Vyroba_P.Forms
{
    partial class FormSledovaniPapouch
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
            this.btn_vycist = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_vycist
            // 
            this.btn_vycist.Location = new System.Drawing.Point(101, 233);
            this.btn_vycist.Name = "btn_vycist";
            this.btn_vycist.Size = new System.Drawing.Size(120, 23);
            this.btn_vycist.TabIndex = 0;
            this.btn_vycist.Text = "Vyčtení hodnot";
            this.btn_vycist.UseVisualStyleBackColor = true;
            this.btn_vycist.Click += new System.EventHandler(this.btn_vycist_Click);
            // 
            // FormSledovaniPapouch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_vycist);
            this.Name = "FormSledovaniPapouch";
            this.Text = "Sledování Papouch";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_vycist;
    }
}