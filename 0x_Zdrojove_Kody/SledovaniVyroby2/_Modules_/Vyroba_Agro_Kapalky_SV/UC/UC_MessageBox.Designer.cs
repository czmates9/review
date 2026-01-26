
namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Kapalky_SV.UC
{
    partial class UC_MessageBox
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_ano = new System.Windows.Forms.Button();
            this.btn_ne = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_ne);
            this.groupBox2.Controls.Add(this.btn_ano);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 180);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Deaktivace záznamů";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(24, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(292, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Opravdu chcete deaktivovat?";
            // 
            // btn_ano
            // 
            this.btn_ano.Location = new System.Drawing.Point(19, 90);
            this.btn_ano.Name = "btn_ano";
            this.btn_ano.Size = new System.Drawing.Size(138, 73);
            this.btn_ano.TabIndex = 1;
            this.btn_ano.Text = "ANO";
            this.btn_ano.UseVisualStyleBackColor = true;
            this.btn_ano.Click += new System.EventHandler(this.btn_ano_Click);
            // 
            // btn_ne
            // 
            this.btn_ne.Location = new System.Drawing.Point(178, 90);
            this.btn_ne.Name = "btn_ne";
            this.btn_ne.Size = new System.Drawing.Size(138, 73);
            this.btn_ne.TabIndex = 1;
            this.btn_ne.Text = "NE";
            this.btn_ne.UseVisualStyleBackColor = true;
            this.btn_ne.Click += new System.EventHandler(this.btn_ne_Click);
            // 
            // UC_MessageBox
            // 
            this.Controls.Add(this.groupBox2);
            this.Name = "UC_MessageBox";
            this.Size = new System.Drawing.Size(350, 180);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_ne;
        private System.Windows.Forms.Button btn_ano;
    }
}
