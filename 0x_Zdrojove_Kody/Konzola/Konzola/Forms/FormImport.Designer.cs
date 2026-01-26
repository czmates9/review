
namespace Konzola.Forms
{
    partial class FormImport
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
            this.btn_Vybrat = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Zpět = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Vybrat
            // 
            this.btn_Vybrat.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Vybrat.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_Vybrat.Location = new System.Drawing.Point(0, 0);
            this.btn_Vybrat.Name = "btn_Vybrat";
            this.btn_Vybrat.Size = new System.Drawing.Size(191, 116);
            this.btn_Vybrat.TabIndex = 0;
            this.btn_Vybrat.Text = "Vybrat";
            this.btn_Vybrat.UseVisualStyleBackColor = true;
            this.btn_Vybrat.Click += new System.EventHandler(this.btn_Vybrat_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Zpět);
            this.panel1.Controls.Add(this.btn_Vybrat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 195);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 116);
            this.panel1.TabIndex = 1;
            // 
            // btn_Zpět
            // 
            this.btn_Zpět.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Zpět.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold);
            this.btn_Zpět.Location = new System.Drawing.Point(193, 0);
            this.btn_Zpět.Name = "btn_Zpět";
            this.btn_Zpět.Size = new System.Drawing.Size(191, 116);
            this.btn_Zpět.TabIndex = 1;
            this.btn_Zpět.Text = "Zpět";
            this.btn_Zpět.UseVisualStyleBackColor = true;
            this.btn_Zpět.Click += new System.EventHandler(this.btn_Zpet_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton2);
            this.panel2.Controls.Add(this.radioButton1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(384, 195);
            this.panel2.TabIndex = 2;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.radioButton2.Location = new System.Drawing.Point(48, 103);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(170, 33);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "CSV soubor";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButton1.Location = new System.Drawing.Point(48, 60);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(152, 33);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Procedura";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // FormImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 311);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormImport";
            this.Text = "Import zásob";
            this.Load += new System.EventHandler(this.FormImport_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Vybrat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Zpět;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}