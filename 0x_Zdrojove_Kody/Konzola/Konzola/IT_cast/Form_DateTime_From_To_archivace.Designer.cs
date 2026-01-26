namespace Konzola.IT_cast
{
    partial class Form_DateTime_From_To_archivace
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
            this.dtp_OD = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_DO = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Storno = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtp_OD
            // 
            this.dtp_OD.Checked = false;
            this.dtp_OD.Location = new System.Drawing.Point(50, 90);
            this.dtp_OD.Name = "dtp_OD";
            this.dtp_OD.ShowCheckBox = true;
            this.dtp_OD.Size = new System.Drawing.Size(200, 20);
            this.dtp_OD.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(35, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Datum a čas OD";
            // 
            // dtp_DO
            // 
            this.dtp_DO.Checked = false;
            this.dtp_DO.Location = new System.Drawing.Point(320, 90);
            this.dtp_DO.Name = "dtp_DO";
            this.dtp_DO.ShowCheckBox = true;
            this.dtp_DO.Size = new System.Drawing.Size(200, 20);
            this.dtp_DO.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(306, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Datum a čas DO";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Storno);
            this.panel1.Controls.Add(this.button_OK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 165);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 72);
            this.panel1.TabIndex = 2;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // button_Storno
            // 
            this.button_Storno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Storno.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.button_Storno.Location = new System.Drawing.Point(0, 0);
            this.button_Storno.Name = "button_Storno";
            this.button_Storno.Size = new System.Drawing.Size(249, 72);
            this.button_Storno.TabIndex = 0;
            this.button_Storno.Text = "Storno";
            this.button_Storno.UseVisualStyleBackColor = true;
            this.button_Storno.Click += new System.EventHandler(this.button_Storno_Click);
            // 
            // button_OK
            // 
            this.button_OK.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_OK.Location = new System.Drawing.Point(249, 0);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(279, 72);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // Form_DateTime_From_To_archivace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 237);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtp_DO);
            this.Controls.Add(this.dtp_OD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Form_DateTime_From_To_archivace";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.Form_DateTime_From_To_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_DateTime_From_To_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtp_OD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_DO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Storno;
        private System.Windows.Forms.Button button_OK;
    }
}