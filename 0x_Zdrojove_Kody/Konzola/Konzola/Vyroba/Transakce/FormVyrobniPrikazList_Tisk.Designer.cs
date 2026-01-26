
namespace Konzola.Vyroba.Transakce
{
    partial class FormVyrobniPrikazList_Tisk
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
            this.dGW_files = new System.Windows.Forms.DataGridView();
            this.btn_zrusit = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dGW_files)).BeginInit();
            this.SuspendLayout();
            // 
            // dGW_files
            // 
            this.dGW_files.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGW_files.Dock = System.Windows.Forms.DockStyle.Top;
            this.dGW_files.Location = new System.Drawing.Point(0, 0);
            this.dGW_files.Name = "dGW_files";
            this.dGW_files.Size = new System.Drawing.Size(486, 344);
            this.dGW_files.TabIndex = 0;
            // 
            // btn_zrusit
            // 
            this.btn_zrusit.Location = new System.Drawing.Point(12, 360);
            this.btn_zrusit.Name = "btn_zrusit";
            this.btn_zrusit.Size = new System.Drawing.Size(108, 46);
            this.btn_zrusit.TabIndex = 1;
            this.btn_zrusit.Text = "zrušit";
            this.btn_zrusit.UseVisualStyleBackColor = true;
            this.btn_zrusit.Click += new System.EventHandler(this.btn_zrusit_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(366, 360);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(108, 46);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // FormVyrobniPrikazList_Tisk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 420);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_zrusit);
            this.Controls.Add(this.dGW_files);
            this.Name = "FormVyrobniPrikazList_Tisk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Výběr tiskové šablony";
            this.Load += new System.EventHandler(this.FormVyrobniPrikazList_Tisk_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGW_files)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGW_files;
        private System.Windows.Forms.Button btn_zrusit;
        private System.Windows.Forms.Button btn_ok;
    }
}