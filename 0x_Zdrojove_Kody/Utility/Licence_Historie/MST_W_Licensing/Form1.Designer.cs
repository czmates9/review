namespace MST_W_Licensing
{
    partial class Form_Main
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
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtZakaznik = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialogLicence = new System.Windows.Forms.SaveFileDialog();
            this.menuItemLoad = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemLoad);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.Text = "Menu";
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Generate";
            this.menuItem3.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Close";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtFilename);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtZakaznik);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 293);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(0, 253);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(324, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "Generate";
            this.button1.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilename.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.txtFilename.Location = new System.Drawing.Point(80, 3);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(241, 19);
            this.txtFilename.TabIndex = 0;
            this.txtFilename.Text = "mst_w.ini";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.Text = "Soubor :";
            // 
            // txtZakaznik
            // 
            this.txtZakaznik.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZakaznik.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.txtZakaznik.Location = new System.Drawing.Point(80, 27);
            this.txtZakaznik.Name = "txtZakaznik";
            this.txtZakaznik.Size = new System.Drawing.Size(241, 19);
            this.txtZakaznik.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(4, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.Text = "Zákazník :";
            // 
            // saveFileDialogLicence
            // 
            this.saveFileDialogLicence.FileName = "mst_w.ini";
            this.saveFileDialogLicence.Filter = "Licenses|*.ini|All files|*.*";
            // 
            // menuItemLoad
            // 
            this.menuItemLoad.Text = "Load";
            this.menuItemLoad.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(324, 293);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "Form_Main";
            this.Text = "MST_W Licensing";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_Main_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtZakaznik;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialogLicence;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuItem menuItemLoad;
    }
}

