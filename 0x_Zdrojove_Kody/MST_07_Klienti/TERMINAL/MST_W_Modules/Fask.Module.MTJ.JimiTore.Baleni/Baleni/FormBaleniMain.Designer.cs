namespace Fask.Module.MTJ.JimiTore.Baleni.Baleni
{
    partial class FormBaleniMain
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTisk = new System.Windows.Forms.Button();
            this.tbKodObjednavky = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.Text = "Akce";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Konec";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTisk);
            this.panel1.Controls.Add(this.tbKodObjednavky);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 210);
            // 
            // btnTisk
            // 
            this.btnTisk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTisk.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnTisk.Location = new System.Drawing.Point(157, 66);
            this.btnTisk.Name = "btnTisk";
            this.btnTisk.Size = new System.Drawing.Size(72, 39);
            this.btnTisk.TabIndex = 2;
            this.btnTisk.Text = "Tisk";
            this.btnTisk.Click += new System.EventHandler(this.btnTisk_Click);
            // 
            // tbKodObjednavky
            // 
            this.tbKodObjednavky.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKodObjednavky.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.tbKodObjednavky.Location = new System.Drawing.Point(10, 31);
            this.tbKodObjednavky.Name = "tbKodObjednavky";
            this.tbKodObjednavky.Size = new System.Drawing.Size(219, 29);
            this.tbKodObjednavky.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(10, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 25);
            this.label1.Text = "Kód objednávky:";
            // 
            // FormBaleniMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(239, 210);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "FormBaleniMain";
            this.Text = "Balení";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTisk;
        private System.Windows.Forms.TextBox tbKodObjednavky;
        private System.Windows.Forms.Label label1;
    }
}