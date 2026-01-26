namespace Fask.Module.Inside
{
    partial class FormPrevodPalety
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbEXP = new System.Windows.Forms.RadioButton();
            this.rbKOOPIN = new System.Windows.Forms.RadioButton();
            this.rbKOOPOUT = new System.Windows.Forms.RadioButton();
            this.txtPaleta = new System.Windows.Forms.TextBox();
            this.lblPaleta = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStorno
            // 
            this.buttonStorno.Location = new System.Drawing.Point(3, 181);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(102, 57);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(111, 181);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(102, 57);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rbEXP);
            this.panel1.Controls.Add(this.rbKOOPIN);
            this.panel1.Controls.Add(this.rbKOOPOUT);
            this.panel1.Location = new System.Drawing.Point(3, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(293, 102);
            // 
            // rbEXP
            // 
            this.rbEXP.Location = new System.Drawing.Point(16, 69);
            this.rbEXP.Name = "rbEXP";
            this.rbEXP.Size = new System.Drawing.Size(100, 20);
            this.rbEXP.TabIndex = 0;
            this.rbEXP.TabStop = false;
            this.rbEXP.Text = "EXP";
            this.rbEXP.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbKOOPIN
            // 
            this.rbKOOPIN.Location = new System.Drawing.Point(16, 43);
            this.rbKOOPIN.Name = "rbKOOPIN";
            this.rbKOOPIN.Size = new System.Drawing.Size(100, 20);
            this.rbKOOPIN.TabIndex = 0;
            this.rbKOOPIN.TabStop = false;
            this.rbKOOPIN.Text = "KOOPIN";
            this.rbKOOPIN.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbKOOPOUT
            // 
            this.rbKOOPOUT.Location = new System.Drawing.Point(16, 17);
            this.rbKOOPOUT.Name = "rbKOOPOUT";
            this.rbKOOPOUT.Size = new System.Drawing.Size(100, 20);
            this.rbKOOPOUT.TabIndex = 0;
            this.rbKOOPOUT.TabStop = false;
            this.rbKOOPOUT.Text = "KOOPOUT";
            this.rbKOOPOUT.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // txtPaleta
            // 
            this.txtPaleta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPaleta.Location = new System.Drawing.Point(3, 152);
            this.txtPaleta.Name = "txtPaleta";
            this.txtPaleta.Size = new System.Drawing.Size(293, 23);
            this.txtPaleta.TabIndex = 2;
            // 
            // lblPaleta
            // 
            this.lblPaleta.Location = new System.Drawing.Point(16, 129);
            this.lblPaleta.Name = "lblPaleta";
            this.lblPaleta.Size = new System.Drawing.Size(100, 20);
            this.lblPaleta.Text = "Cislo palety";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Druh prevodu";
            // 
            // FormPrevodPalety
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(299, 275);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblPaleta);
            this.Controls.Add(this.txtPaleta);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonStorno);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu1;
            this.Name = "FormPrevodPalety";
            this.Text = "FormPrevodPalety";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbEXP;
        private System.Windows.Forms.RadioButton rbKOOPIN;
        private System.Windows.Forms.RadioButton rbKOOPOUT;
        private System.Windows.Forms.TextBox txtPaleta;
        private System.Windows.Forms.Label lblPaleta;
        private System.Windows.Forms.Label label2;
    }
}