namespace MSTW_Update
{
    partial class Notification
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.message_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.version_label = new System.Windows.Forms.Label();
            this.appname_label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Cancle = new System.Windows.Forms.Button();
            this.btn_Install = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 32);
            this.label1.Text = "aktualizace k dispozici";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.message_textbox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.version_label);
            this.panel1.Controls.Add(this.appname_label);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(246, 238);
            // 
            // message_textbox
            // 
            this.message_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.message_textbox.Location = new System.Drawing.Point(11, 67);
            this.message_textbox.Multiline = true;
            this.message_textbox.Name = "message_textbox";
            this.message_textbox.ReadOnly = true;
            this.message_textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.message_textbox.Size = new System.Drawing.Size(225, 74);
            this.message_textbox.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 19);
            this.label2.Text = "Nová verze";
            // 
            // version_label
            // 
            this.version_label.Location = new System.Drawing.Point(66, 44);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(170, 20);
            this.version_label.Text = "1.0.0.0";
            // 
            // appname_label
            // 
            this.appname_label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.appname_label.Location = new System.Drawing.Point(3, 24);
            this.appname_label.Name = "appname_label";
            this.appname_label.Size = new System.Drawing.Size(233, 20);
            this.appname_label.Text = "Název aplikace";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 20);
            this.label5.Text = "Verze:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_Cancle);
            this.panel2.Controls.Add(this.btn_Install);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 144);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(246, 94);
            // 
            // btn_Cancle
            // 
            this.btn_Cancle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_Cancle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btn_Cancle.Location = new System.Drawing.Point(0, 47);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Size = new System.Drawing.Size(246, 47);
            this.btn_Cancle.TabIndex = 3;
            this.btn_Cancle.Text = "Zrušit";
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // btn_Install
            // 
            this.btn_Install.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Install.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btn_Install.Location = new System.Drawing.Point(0, 0);
            this.btn_Install.Name = "btn_Install";
            this.btn_Install.Size = new System.Drawing.Size(246, 47);
            this.btn_Install.TabIndex = 2;
            this.btn_Install.Text = "Nainstalujte";
            this.btn_Install.Click += new System.EventHandler(this.btn_Install_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(238, 47);
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(246, 270);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "Notification";
            this.Text = "Notification";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox message_textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label version_label;
        private System.Windows.Forms.Label appname_label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btn_Cancle;
        private System.Windows.Forms.Button btn_Install;
    }
}