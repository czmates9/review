
namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku
{
    partial class FormDeaktivace
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_ne = new System.Windows.Forms.Button();
            this.btn_ano = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_ne);
            this.groupBox2.Controls.Add(this.btn_ano);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox2.Location = new System.Drawing.Point(-1, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(552, 351);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Deaktivace záznamů";
            // 
            // btn_ne
            // 
            this.btn_ne.Location = new System.Drawing.Point(284, 181);
            this.btn_ne.Name = "btn_ne";
            this.btn_ne.Size = new System.Drawing.Size(262, 157);
            this.btn_ne.TabIndex = 1;
            this.btn_ne.Text = "NE";
            this.btn_ne.UseVisualStyleBackColor = true;
            this.btn_ne.Click += new System.EventHandler(this.btn_ne_Click);
            // 
            // btn_ano
            // 
            this.btn_ano.Location = new System.Drawing.Point(6, 181);
            this.btn_ano.Name = "btn_ano";
            this.btn_ano.Size = new System.Drawing.Size(262, 157);
            this.btn_ano.TabIndex = 1;
            this.btn_ano.Text = "ANO";
            this.btn_ano.UseVisualStyleBackColor = true;
            this.btn_ano.Click += new System.EventHandler(this.btn_ano_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(81, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(408, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Odjely všechny palety ???";
            // 
            // FormDeaktivace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 350);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDeaktivace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormDeaktivace";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_ne;
        private System.Windows.Forms.Button btn_ano;
        private System.Windows.Forms.Label label1;
    }
}