namespace Konzola.Forms
{
    partial class InputBox_Text_And_Checkbox
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.labelText = new System.Windows.Forms.Label();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_GroupBox = new System.Windows.Forms.Label();
            this.tb_InGroupBox = new System.Windows.Forms.TextBox();
            this.panelButtons.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(204, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(154, 71);
            this.buttonOK.TabIndex = 9;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 268);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(358, 71);
            this.panelButtons.TabIndex = 3;
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(204, 71);
            this.buttonStorno.TabIndex = 8;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // labelText
            // 
            this.labelText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelText.Location = new System.Drawing.Point(12, 9);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(298, 108);
            this.labelText.TabIndex = 4;
            this.labelText.Text = "Vstup";
            this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxText
            // 
            this.textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxText.Location = new System.Drawing.Point(12, 120);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(334, 20);
            this.textBoxText.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 156);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tb_InGroupBox);
            this.groupBox1.Controls.Add(this.lb_GroupBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 179);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 83);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // lb_GroupBox
            // 
            this.lb_GroupBox.Location = new System.Drawing.Point(19, 19);
            this.lb_GroupBox.Name = "lb_GroupBox";
            this.lb_GroupBox.Size = new System.Drawing.Size(309, 23);
            this.lb_GroupBox.TabIndex = 0;
            this.lb_GroupBox.Text = "XXXXXXXX";
            // 
            // tb_InGroupBox
            // 
            this.tb_InGroupBox.Location = new System.Drawing.Point(19, 45);
            this.tb_InGroupBox.Name = "tb_InGroupBox";
            this.tb_InGroupBox.Size = new System.Drawing.Size(309, 20);
            this.tb_InGroupBox.TabIndex = 1;
            // 
            // InputBox_Text_And_Checkbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 339);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.panelButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "InputBox_Text_And_Checkbox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vstup";
            this.Load += new System.EventHandler(this.InputBox_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_InGroupBox;
        private System.Windows.Forms.Label lb_GroupBox;
    }
}