
namespace Fask.Aktualizace_API.Forms
{
    partial class FormDialog
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
            this.btn_left = new System.Windows.Forms.Button();
            this.btn_right = new System.Windows.Forms.Button();
            this.l_text = new System.Windows.Forms.Label();
            this.gB_1 = new System.Windows.Forms.GroupBox();
            this.gB_2 = new System.Windows.Forms.GroupBox();
            this.gB_1.SuspendLayout();
            this.gB_2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_left
            // 
            this.btn_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_left.Location = new System.Drawing.Point(3, 16);
            this.btn_left.Name = "btn_left";
            this.btn_left.Size = new System.Drawing.Size(202, 102);
            this.btn_left.TabIndex = 0;
            this.btn_left.Text = "button1";
            this.btn_left.UseVisualStyleBackColor = true;
            // 
            // btn_right
            // 
            this.btn_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_right.Location = new System.Drawing.Point(337, 16);
            this.btn_right.Name = "btn_right";
            this.btn_right.Size = new System.Drawing.Size(202, 102);
            this.btn_right.TabIndex = 0;
            this.btn_right.Text = "button1";
            this.btn_right.UseVisualStyleBackColor = true;
            // 
            // l_text
            // 
            this.l_text.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.l_text.AutoSize = true;
            this.l_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.l_text.Location = new System.Drawing.Point(15, 30);
            this.l_text.Name = "l_text";
            this.l_text.Size = new System.Drawing.Size(85, 29);
            this.l_text.TabIndex = 1;
            this.l_text.Text = "label1";
            // 
            // gB_1
            // 
            this.gB_1.Controls.Add(this.btn_left);
            this.gB_1.Controls.Add(this.btn_right);
            this.gB_1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gB_1.Location = new System.Drawing.Point(0, 142);
            this.gB_1.Name = "gB_1";
            this.gB_1.Size = new System.Drawing.Size(542, 121);
            this.gB_1.TabIndex = 2;
            this.gB_1.TabStop = false;
            // 
            // gB_2
            // 
            this.gB_2.Controls.Add(this.l_text);
            this.gB_2.Dock = System.Windows.Forms.DockStyle.Top;
            this.gB_2.Location = new System.Drawing.Point(0, 0);
            this.gB_2.Name = "gB_2";
            this.gB_2.Size = new System.Drawing.Size(542, 146);
            this.gB_2.TabIndex = 3;
            this.gB_2.TabStop = false;
            // 
            // FormDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 263);
            this.Controls.Add(this.gB_2);
            this.Controls.Add(this.gB_1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDialog";
            this.Text = "FormDialog";
            this.gB_1.ResumeLayout(false);
            this.gB_2.ResumeLayout(false);
            this.gB_2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_left;
        private System.Windows.Forms.Button btn_right;
        private System.Windows.Forms.Label l_text;
        private System.Windows.Forms.GroupBox gB_1;
        private System.Windows.Forms.GroupBox gB_2;
    }
}