
namespace Fask.Aktualizace_API.Konzola.Forms
{
    partial class FormKonzolaMain
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
            this.l_localPath = new System.Windows.Forms.Label();
            this.txtLocalPath = new System.Windows.Forms.TextBox();
            this.btn_localPath = new System.Windows.Forms.Button();
            this.btn_ukonceni = new System.Windows.Forms.Button();
            this.panelDown = new System.Windows.Forms.Panel();
            this.btn_aktualizace = new System.Windows.Forms.Button();
            this.btn_obnova = new System.Windows.Forms.Button();
            this.btn_zaloha = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.gB_par = new System.Windows.Forms.GroupBox();
            this.toolStripProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.chB_Akt_vse = new System.Windows.Forms.CheckBox();
            this.gB_Details = new System.Windows.Forms.GroupBox();
            this.btn_obnovaPath = new System.Windows.Forms.Button();
            this.txtlPathObnova = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ulozPath = new System.Windows.Forms.Button();
            this.btn_PathNacist = new System.Windows.Forms.Button();
            this.btn_PathAktualizace = new System.Windows.Forms.Button();
            this.btn_PathZaloha = new System.Windows.Forms.Button();
            this.txtlPathZdrojAkt = new System.Windows.Forms.TextBox();
            this.l_server_zaloha = new System.Windows.Forms.Label();
            this.txtlPathZaloha = new System.Windows.Forms.TextBox();
            this.l_konzola_zaloha = new System.Windows.Forms.Label();
            this.panelDown.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.gB_par.SuspendLayout();
            this.gB_Details.SuspendLayout();
            this.SuspendLayout();
            // 
            // l_localPath
            // 
            this.l_localPath.AutoSize = true;
            this.l_localPath.Location = new System.Drawing.Point(14, 36);
            this.l_localPath.Name = "l_localPath";
            this.l_localPath.Size = new System.Drawing.Size(315, 25);
            this.l_localPath.TabIndex = 0;
            this.l_localPath.Text = "Adresář konzola stará verze:";
            // 
            // txtLocalPath
            // 
            this.txtLocalPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalPath.Enabled = false;
            this.txtLocalPath.Location = new System.Drawing.Point(344, 33);
            this.txtLocalPath.Name = "txtLocalPath";
            this.txtLocalPath.Size = new System.Drawing.Size(645, 31);
            this.txtLocalPath.TabIndex = 1;
            // 
            // btn_localPath
            // 
            this.btn_localPath.Location = new System.Drawing.Point(11, 191);
            this.btn_localPath.Name = "btn_localPath";
            this.btn_localPath.Size = new System.Drawing.Size(184, 71);
            this.btn_localPath.TabIndex = 2;
            this.btn_localPath.Text = "Výběr adresáře stará verze";
            this.btn_localPath.UseVisualStyleBackColor = true;
            this.btn_localPath.Click += new System.EventHandler(this.btn_localPath_Click);
            // 
            // btn_ukonceni
            // 
            this.btn_ukonceni.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_ukonceni.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_ukonceni.Location = new System.Drawing.Point(0, 0);
            this.btn_ukonceni.Name = "btn_ukonceni";
            this.btn_ukonceni.Size = new System.Drawing.Size(289, 164);
            this.btn_ukonceni.TabIndex = 3;
            this.btn_ukonceni.Text = "Zpět";
            this.btn_ukonceni.UseVisualStyleBackColor = true;
            this.btn_ukonceni.Click += new System.EventHandler(this.btn_ukonceni_Click);
            // 
            // panelDown
            // 
            this.panelDown.Controls.Add(this.btn_aktualizace);
            this.panelDown.Controls.Add(this.btn_obnova);
            this.panelDown.Controls.Add(this.btn_ukonceni);
            this.panelDown.Controls.Add(this.btn_zaloha);
            this.panelDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDown.Location = new System.Drawing.Point(0, 623);
            this.panelDown.Name = "panelDown";
            this.panelDown.Size = new System.Drawing.Size(1045, 164);
            this.panelDown.TabIndex = 4;
            // 
            // btn_aktualizace
            // 
            this.btn_aktualizace.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_aktualizace.Location = new System.Drawing.Point(541, 0);
            this.btn_aktualizace.Name = "btn_aktualizace";
            this.btn_aktualizace.Size = new System.Drawing.Size(266, 164);
            this.btn_aktualizace.TabIndex = 5;
            this.btn_aktualizace.Text = "Aktualizovat";
            this.btn_aktualizace.UseVisualStyleBackColor = true;
            this.btn_aktualizace.Click += new System.EventHandler(this.btn_aktualizace_Click);
            // 
            // btn_obnova
            // 
            this.btn_obnova.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_obnova.Enabled = false;
            this.btn_obnova.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_obnova.Location = new System.Drawing.Point(845, 0);
            this.btn_obnova.Name = "btn_obnova";
            this.btn_obnova.Size = new System.Drawing.Size(200, 164);
            this.btn_obnova.TabIndex = 4;
            this.btn_obnova.Text = "Obnovit";
            this.btn_obnova.UseVisualStyleBackColor = true;
            this.btn_obnova.Click += new System.EventHandler(this.btn_obnova_Click);
            // 
            // btn_zaloha
            // 
            this.btn_zaloha.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_zaloha.Location = new System.Drawing.Point(339, 0);
            this.btn_zaloha.Name = "btn_zaloha";
            this.btn_zaloha.Size = new System.Drawing.Size(200, 164);
            this.btn_zaloha.TabIndex = 4;
            this.btn_zaloha.Text = "Zálohovat";
            this.btn_zaloha.UseVisualStyleBackColor = true;
            this.btn_zaloha.Click += new System.EventHandler(this.btn_zaloha_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.gB_par);
            this.panelTop.Controls.Add(this.gB_Details);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1045, 623);
            this.panelTop.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "Konzola";
            // 
            // gB_par
            // 
            this.gB_par.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gB_par.AutoSize = true;
            this.gB_par.Controls.Add(this.toolStripProgressBar1);
            this.gB_par.Controls.Add(this.chB_Akt_vse);
            this.gB_par.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gB_par.Location = new System.Drawing.Point(12, 424);
            this.gB_par.Name = "gB_par";
            this.gB_par.Size = new System.Drawing.Size(1018, 193);
            this.gB_par.TabIndex = 7;
            this.gB_par.TabStop = false;
            this.gB_par.Text = "Parametry aktualizace";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripProgressBar1.Location = new System.Drawing.Point(3, 158);
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(1012, 32);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar1.TabIndex = 9;
            // 
            // chB_Akt_vse
            // 
            this.chB_Akt_vse.AutoSize = true;
            this.chB_Akt_vse.Location = new System.Drawing.Point(15, 41);
            this.chB_Akt_vse.Name = "chB_Akt_vse";
            this.chB_Akt_vse.Size = new System.Drawing.Size(204, 29);
            this.chB_Akt_vse.TabIndex = 0;
            this.chB_Akt_vse.Text = "Aktualizovat vše";
            this.chB_Akt_vse.UseVisualStyleBackColor = true;
            // 
            // gB_Details
            // 
            this.gB_Details.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gB_Details.AutoSize = true;
            this.gB_Details.Controls.Add(this.btn_obnovaPath);
            this.gB_Details.Controls.Add(this.txtlPathObnova);
            this.gB_Details.Controls.Add(this.label1);
            this.gB_Details.Controls.Add(this.btn_ulozPath);
            this.gB_Details.Controls.Add(this.btn_PathNacist);
            this.gB_Details.Controls.Add(this.btn_PathAktualizace);
            this.gB_Details.Controls.Add(this.btn_PathZaloha);
            this.gB_Details.Controls.Add(this.txtlPathZdrojAkt);
            this.gB_Details.Controls.Add(this.l_server_zaloha);
            this.gB_Details.Controls.Add(this.txtlPathZaloha);
            this.gB_Details.Controls.Add(this.l_konzola_zaloha);
            this.gB_Details.Controls.Add(this.txtLocalPath);
            this.gB_Details.Controls.Add(this.btn_localPath);
            this.gB_Details.Controls.Add(this.l_localPath);
            this.gB_Details.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gB_Details.Location = new System.Drawing.Point(12, 49);
            this.gB_Details.Name = "gB_Details";
            this.gB_Details.Size = new System.Drawing.Size(1018, 422);
            this.gB_Details.TabIndex = 3;
            this.gB_Details.TabStop = false;
            this.gB_Details.Text = "Adresáře";
            // 
            // btn_obnovaPath
            // 
            this.btn_obnovaPath.Enabled = false;
            this.btn_obnovaPath.Location = new System.Drawing.Point(576, 191);
            this.btn_obnovaPath.Name = "btn_obnovaPath";
            this.btn_obnovaPath.Size = new System.Drawing.Size(219, 71);
            this.btn_obnovaPath.TabIndex = 12;
            this.btn_obnovaPath.Text = "Výběr zip souboru k obnovení";
            this.btn_obnovaPath.UseVisualStyleBackColor = true;
            this.btn_obnovaPath.Click += new System.EventHandler(this.btn_obnovaPath_Click);
            // 
            // txtlPathObnova
            // 
            this.txtlPathObnova.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtlPathObnova.Enabled = false;
            this.txtlPathObnova.Location = new System.Drawing.Point(345, 146);
            this.txtlPathObnova.Name = "txtlPathObnova";
            this.txtlPathObnova.Size = new System.Drawing.Size(645, 31);
            this.txtlPathObnova.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 149);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Zip soubor k obnovení: ";
            // 
            // btn_ulozPath
            // 
            this.btn_ulozPath.Location = new System.Drawing.Point(283, 268);
            this.btn_ulozPath.Name = "btn_ulozPath";
            this.btn_ulozPath.Size = new System.Drawing.Size(144, 71);
            this.btn_ulozPath.TabIndex = 9;
            this.btn_ulozPath.Text = "Ulož cesty adresářů";
            this.btn_ulozPath.UseVisualStyleBackColor = true;
            this.btn_ulozPath.Click += new System.EventHandler(this.btn_ulozPath_Click);
            // 
            // btn_PathNacist
            // 
            this.btn_PathNacist.Location = new System.Drawing.Point(133, 268);
            this.btn_PathNacist.Name = "btn_PathNacist";
            this.btn_PathNacist.Size = new System.Drawing.Size(144, 71);
            this.btn_PathNacist.TabIndex = 9;
            this.btn_PathNacist.Text = "Načti cesty adresářů";
            this.btn_PathNacist.UseVisualStyleBackColor = true;
            this.btn_PathNacist.Click += new System.EventHandler(this.btn_PathNacist_Click);
            // 
            // btn_PathAktualizace
            // 
            this.btn_PathAktualizace.Location = new System.Drawing.Point(386, 191);
            this.btn_PathAktualizace.Name = "btn_PathAktualizace";
            this.btn_PathAktualizace.Size = new System.Drawing.Size(184, 71);
            this.btn_PathAktualizace.TabIndex = 8;
            this.btn_PathAktualizace.Text = "Výběr adresáře nová verze";
            this.btn_PathAktualizace.UseVisualStyleBackColor = true;
            this.btn_PathAktualizace.Click += new System.EventHandler(this.btn_PathAktualizace_Click);
            // 
            // btn_PathZaloha
            // 
            this.btn_PathZaloha.Location = new System.Drawing.Point(201, 191);
            this.btn_PathZaloha.Name = "btn_PathZaloha";
            this.btn_PathZaloha.Size = new System.Drawing.Size(183, 71);
            this.btn_PathZaloha.TabIndex = 7;
            this.btn_PathZaloha.Text = "Výběr adresáře záloha";
            this.btn_PathZaloha.UseVisualStyleBackColor = true;
            this.btn_PathZaloha.Click += new System.EventHandler(this.btn_PathZaloha_Click);
            // 
            // txtlPathZdrojAkt
            // 
            this.txtlPathZdrojAkt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtlPathZdrojAkt.Enabled = false;
            this.txtlPathZdrojAkt.Location = new System.Drawing.Point(344, 107);
            this.txtlPathZdrojAkt.Name = "txtlPathZdrojAkt";
            this.txtlPathZdrojAkt.Size = new System.Drawing.Size(645, 31);
            this.txtlPathZdrojAkt.TabIndex = 6;
            // 
            // l_server_zaloha
            // 
            this.l_server_zaloha.AutoSize = true;
            this.l_server_zaloha.Location = new System.Drawing.Point(14, 110);
            this.l_server_zaloha.Name = "l_server_zaloha";
            this.l_server_zaloha.Size = new System.Drawing.Size(320, 25);
            this.l_server_zaloha.TabIndex = 5;
            this.l_server_zaloha.Text = "Adresář konzola nová verze: ";
            // 
            // txtlPathZaloha
            // 
            this.txtlPathZaloha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtlPathZaloha.Enabled = false;
            this.txtlPathZaloha.Location = new System.Drawing.Point(344, 70);
            this.txtlPathZaloha.Name = "txtlPathZaloha";
            this.txtlPathZaloha.Size = new System.Drawing.Size(645, 31);
            this.txtlPathZaloha.TabIndex = 4;
            // 
            // l_konzola_zaloha
            // 
            this.l_konzola_zaloha.AutoSize = true;
            this.l_konzola_zaloha.Location = new System.Drawing.Point(62, 76);
            this.l_konzola_zaloha.Name = "l_konzola_zaloha";
            this.l_konzola_zaloha.Size = new System.Drawing.Size(274, 25);
            this.l_konzola_zaloha.TabIndex = 3;
            this.l_konzola_zaloha.Text = "Adresář konzola záloha: ";
            // 
            // FormKonzolaMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 787);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelDown);
            this.Name = "FormKonzolaMain";
            this.Text = "Konzola aktualizace";
            this.Load += new System.EventHandler(this.FormKonzolaMain_Load);
            this.panelDown.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.gB_par.ResumeLayout(false);
            this.gB_par.PerformLayout();
            this.gB_Details.ResumeLayout(false);
            this.gB_Details.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label l_localPath;
        private System.Windows.Forms.TextBox txtLocalPath;
        private System.Windows.Forms.Button btn_localPath;
        private System.Windows.Forms.Button btn_ukonceni;
        private System.Windows.Forms.Panel panelDown;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.GroupBox gB_Details;
        private System.Windows.Forms.Button btn_PathNacist;
        private System.Windows.Forms.Button btn_PathAktualizace;
        private System.Windows.Forms.Button btn_PathZaloha;
        private System.Windows.Forms.TextBox txtlPathZdrojAkt;
        private System.Windows.Forms.Label l_server_zaloha;
        private System.Windows.Forms.TextBox txtlPathZaloha;
        private System.Windows.Forms.Label l_konzola_zaloha;
        private System.Windows.Forms.Button btn_aktualizace;
        private System.Windows.Forms.Button btn_zaloha;
        private System.Windows.Forms.GroupBox gB_par;
        private System.Windows.Forms.CheckBox chB_Akt_vse;
        private System.Windows.Forms.Button btn_ulozPath;
        private System.Windows.Forms.Button btn_obnovaPath;
        private System.Windows.Forms.TextBox txtlPathObnova;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_obnova;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar toolStripProgressBar1;
    }
}