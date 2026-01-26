namespace Fask.Module.MTJ.JimiTore.Baleni.Forms
{
    partial class FormVyberPrijemky
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
            this.btnVybrat = new System.Windows.Forms.Button();
            this.tbKodPrijemky = new System.Windows.Forms.TextBox();
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
            this.panel1.Controls.Add(this.btnVybrat);
            this.panel1.Controls.Add(this.tbKodPrijemky);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 210);
            // 
            // btnVybrat
            // 
            this.btnVybrat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVybrat.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnVybrat.Location = new System.Drawing.Point(157, 66);
            this.btnVybrat.Name = "btnVybrat";
            this.btnVybrat.Size = new System.Drawing.Size(72, 39);
            this.btnVybrat.TabIndex = 2;
            this.btnVybrat.Text = "Vybrat";
            this.btnVybrat.Click += new System.EventHandler(this.btnVybrat_Click);
            // 
            // tbKodPrijemky
            // 
            this.tbKodPrijemky.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKodPrijemky.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.tbKodPrijemky.Location = new System.Drawing.Point(10, 31);
            this.tbKodPrijemky.Name = "tbKodPrijemky";
            this.tbKodPrijemky.Size = new System.Drawing.Size(219, 29);
            this.tbKodPrijemky.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(10, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 25);
            this.label1.Text = "Kód příjemky:";
            // 
            // FormVyberPrijemky
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(239, 210);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "FormVyberPrijemky";
            this.Text = "Výběr příjemky";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnVybrat;
        private System.Windows.Forms.TextBox tbKodPrijemky;
        private System.Windows.Forms.Label label1;
    }
}