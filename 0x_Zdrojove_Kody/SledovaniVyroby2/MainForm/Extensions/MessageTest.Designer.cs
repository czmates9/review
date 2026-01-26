using System.Drawing;
using System;
using System.Windows.Forms;
namespace Fask.Vyroba_P.Extensions
{
    partial class MessageTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource FlexibleMessageBoxFormBindingSource;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxForIcon;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.FlexibleMessageBoxFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxForIcon = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panelHlavni = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.FlexibleMessageBoxFormBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForIcon)).BeginInit();
            this.panelHlavni.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.AutoSize = true;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(87, 75);
            this.button1.MinimumSize = new System.Drawing.Size(0, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 24);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxMessage.BackColor = System.Drawing.Color.White;
            this.richTextBoxMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxMessage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.FlexibleMessageBoxFormBindingSource, "MessageText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.richTextBoxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxMessage.Location = new System.Drawing.Point(50, 19);
            this.richTextBoxMessage.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.ReadOnly = true;
            this.richTextBoxMessage.Size = new System.Drawing.Size(199, 40);
            this.richTextBoxMessage.TabIndex = 0;
            this.richTextBoxMessage.Text = "<Message>";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.richTextBoxMessage);
            this.panel1.Controls.Add(this.pictureBoxForIcon);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 67);
            this.panel1.TabIndex = 1;
            // 
            // pictureBoxForIcon
            // 
            this.pictureBoxForIcon.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxForIcon.Location = new System.Drawing.Point(15, 19);
            this.pictureBoxForIcon.Name = "pictureBoxForIcon";
            this.pictureBoxForIcon.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxForIcon.TabIndex = 8;
            this.pictureBoxForIcon.TabStop = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.AutoSize = true;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(145, 75);
            this.button2.MinimumSize = new System.Drawing.Size(0, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 24);
            this.button2.TabIndex = 3;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.AutoSize = true;
            this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button3.Location = new System.Drawing.Point(202, 75);
            this.button3.MinimumSize = new System.Drawing.Size(0, 24);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(55, 24);
            this.button3.TabIndex = 0;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // panelHlavni
            // 
            this.panelHlavni.Controls.Add(this.panel1);
            this.panelHlavni.Controls.Add(this.button3);
            this.panelHlavni.Controls.Add(this.button1);
            this.panelHlavni.Controls.Add(this.button2);
            this.panelHlavni.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHlavni.Location = new System.Drawing.Point(0, 0);
            this.panelHlavni.Name = "panelHlavni";
            this.panelHlavni.Size = new System.Drawing.Size(260, 102);
            this.panelHlavni.TabIndex = 4;
            // 
            // MessageTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 102);
            this.ControlBox = false;
            this.Controls.Add(this.panelHlavni);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.FlexibleMessageBoxFormBindingSource, "CaptionText", true));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageTest";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "<Caption>";
            ((System.ComponentModel.ISupportInitialize)(this.FlexibleMessageBoxFormBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForIcon)).EndInit();
            this.panelHlavni.ResumeLayout(false);
            this.panelHlavni.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelHlavni;

    }
}