
namespace Konzola.Vyroba.Transakce
{
    partial class FormVazby_Zpracovat
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.dG_Vyrobky = new System.Windows.Forms.DataGridView();
            this.dG_Materialy = new System.Windows.Forms.DataGridView();
            this.btn_ano = new System.Windows.Forms.Button();
            this.btn_ne = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dG_Vyrobky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dG_Materialy)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_ne);
            this.panel2.Controls.Add(this.btn_ano);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 435);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 100);
            this.panel2.TabIndex = 1;
            // 
            // dG_Vyrobky
            // 
            this.dG_Vyrobky.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dG_Vyrobky.Dock = System.Windows.Forms.DockStyle.Top;
            this.dG_Vyrobky.Location = new System.Drawing.Point(0, 0);
            this.dG_Vyrobky.Name = "dG_Vyrobky";
            this.dG_Vyrobky.Size = new System.Drawing.Size(800, 150);
            this.dG_Vyrobky.TabIndex = 2;
            // 
            // dG_Materialy
            // 
            this.dG_Materialy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dG_Materialy.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dG_Materialy.Location = new System.Drawing.Point(0, 285);
            this.dG_Materialy.Name = "dG_Materialy";
            this.dG_Materialy.Size = new System.Drawing.Size(800, 150);
            this.dG_Materialy.TabIndex = 3;
            // 
            // btn_ano
            // 
            this.btn_ano.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_ano.Location = new System.Drawing.Point(51, 17);
            this.btn_ano.Name = "btn_ano";
            this.btn_ano.Size = new System.Drawing.Size(213, 71);
            this.btn_ano.TabIndex = 0;
            this.btn_ano.Text = "ANO";
            this.btn_ano.UseVisualStyleBackColor = true;
            // 
            // btn_ne
            // 
            this.btn_ne.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_ne.Location = new System.Drawing.Point(478, 17);
            this.btn_ne.Name = "btn_ne";
            this.btn_ne.Size = new System.Drawing.Size(213, 71);
            this.btn_ne.TabIndex = 0;
            this.btn_ne.Text = "NE";
            this.btn_ne.UseVisualStyleBackColor = true;
            // 
            // FormVazby_Zpracovat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 535);
            this.Controls.Add(this.dG_Materialy);
            this.Controls.Add(this.dG_Vyrobky);
            this.Controls.Add(this.panel2);
            this.Name = "FormVazby_Zpracovat";
            this.Text = "FormVazby_Zpracovat";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dG_Vyrobky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dG_Materialy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_ne;
        private System.Windows.Forms.Button btn_ano;
        private System.Windows.Forms.DataGridView dG_Vyrobky;
        private System.Windows.Forms.DataGridView dG_Materialy;
    }
}