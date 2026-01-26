namespace MSTW_Update
{
    partial class Form1
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
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_VerzeOld = new System.Windows.Forms.Label();
            this.lbl_path = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_VerzeNew = new System.Windows.Forms.Label();
            this.lbl_test = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Test";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 302);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(238, 24);
            this.statusBar1.Text = "statusBar1";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lbl_test);
            this.panel1.Controls.Add(this.lbl_path);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbl_VerzeNew);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbl_VerzeOld);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 326);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.Text = "Verze Old:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 20);
            this.label2.Text = "Path:";
            // 
            // lbl_VerzeOld
            // 
            this.lbl_VerzeOld.Location = new System.Drawing.Point(84, 43);
            this.lbl_VerzeOld.Name = "lbl_VerzeOld";
            this.lbl_VerzeOld.Size = new System.Drawing.Size(136, 20);
            this.lbl_VerzeOld.Text = "label1";
            // 
            // lbl_path
            // 
            this.lbl_path.Location = new System.Drawing.Point(74, 97);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(161, 20);
            this.lbl_path.Text = "label1";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.Text = "Verze New:";
            // 
            // lbl_VerzeNew
            // 
            this.lbl_VerzeNew.Location = new System.Drawing.Point(84, 63);
            this.lbl_VerzeNew.Name = "lbl_VerzeNew";
            this.lbl_VerzeNew.Size = new System.Drawing.Size(136, 20);
            this.lbl_VerzeNew.Text = "label1";
            // 
            // lbl_test
            // 
            this.lbl_test.Location = new System.Drawing.Point(36, 157);
            this.lbl_test.Name = "lbl_test";
            this.lbl_test.Size = new System.Drawing.Size(161, 20);
            this.lbl_test.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 326);
            this.ControlBox = false;
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_path;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_VerzeOld;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_VerzeNew;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_test;
    }
}

