namespace Fask.Vyroba_P.Forms
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemAplikace = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOdhlasitSmenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOdhlasitPracovnika = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOdhlasit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTimeSynchronization = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemDownloadDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.odeslatDataVyrobyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.timerShown = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusPracovnik = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusStroj = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusZakazka = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarEmptySpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarContacts = new System.Windows.Forms.ToolStripStatusLabel();
            this.ucTime = new Fask.Vyroba_P.Controls.ucTime();
            this.akceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sledováníPapouchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAplikace,
            this.menuItem3,
            this.akceToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(669, 56);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemAplikace
            // 
            this.menuItemAplikace.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOdhlasitSmenu,
            this.menuItemOdhlasitPracovnika,
            this.menuItemOdhlasit,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripSeparator1,
            this.menuItemKonec});
            this.menuItemAplikace.Name = "menuItemAplikace";
            this.menuItemAplikace.Size = new System.Drawing.Size(176, 52);
            this.menuItemAplikace.Text = "Aplikace";
            // 
            // menuItemOdhlasitSmenu
            // 
            this.menuItemOdhlasitSmenu.Enabled = false;
            this.menuItemOdhlasitSmenu.Name = "menuItemOdhlasitSmenu";
            this.menuItemOdhlasitSmenu.Size = new System.Drawing.Size(444, 52);
            this.menuItemOdhlasitSmenu.Text = "Odhlásit směnu";
            this.menuItemOdhlasitSmenu.Visible = false;
            this.menuItemOdhlasitSmenu.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItemOdhlasitPracovnika
            // 
            this.menuItemOdhlasitPracovnika.Name = "menuItemOdhlasitPracovnika";
            this.menuItemOdhlasitPracovnika.Size = new System.Drawing.Size(444, 52);
            this.menuItemOdhlasitPracovnika.Text = "Odhlásit pracovníka";
            this.menuItemOdhlasitPracovnika.Visible = false;
            this.menuItemOdhlasitPracovnika.Click += new System.EventHandler(this.menuItemOdhlasitPracovnika_Click);
            // 
            // menuItemOdhlasit
            // 
            this.menuItemOdhlasit.Name = "menuItemOdhlasit";
            this.menuItemOdhlasit.Size = new System.Drawing.Size(444, 52);
            this.menuItemOdhlasit.Text = "Přihlásit / Odhlásit";
            this.menuItemOdhlasit.Click += new System.EventHandler(this.menuItemOdhlasit_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(441, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(444, 52);
            this.toolStripMenuItem2.Text = "O aplikaci";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(441, 6);
            // 
            // menuItemKonec
            // 
            this.menuItemKonec.Name = "menuItemKonec";
            this.menuItemKonec.Size = new System.Drawing.Size(444, 52);
            this.menuItemKonec.Text = "Konec";
            this.menuItemKonec.Click += new System.EventHandler(this.mi_Konec_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemConfiguration,
            this.menuItemTimeSynchronization,
            this.toolStripMenuItem3,
            this.menuItemDownloadDatabase,
            this.odeslatDataVyrobyToolStripMenuItem});
            this.menuItem3.Name = "menuItem3";
            this.menuItem3.Size = new System.Drawing.Size(160, 52);
            this.menuItem3.Text = "Systém";
            // 
            // menuItemConfiguration
            // 
            this.menuItemConfiguration.Name = "menuItemConfiguration";
            this.menuItemConfiguration.Size = new System.Drawing.Size(468, 52);
            this.menuItemConfiguration.Text = "Konfigurace";
            this.menuItemConfiguration.Click += new System.EventHandler(this.menuItemConfiguration_Click);
            // 
            // menuItemTimeSynchronization
            // 
            this.menuItemTimeSynchronization.Name = "menuItemTimeSynchronization";
            this.menuItemTimeSynchronization.Size = new System.Drawing.Size(468, 52);
            this.menuItemTimeSynchronization.Text = "Synchronizace času";
            this.menuItemTimeSynchronization.Click += new System.EventHandler(this.menuItemTimeSynchronization_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(465, 6);
            // 
            // menuItemDownloadDatabase
            // 
            this.menuItemDownloadDatabase.Name = "menuItemDownloadDatabase";
            this.menuItemDownloadDatabase.Size = new System.Drawing.Size(468, 52);
            this.menuItemDownloadDatabase.Text = "Aktualizace databáze";
            this.menuItemDownloadDatabase.Click += new System.EventHandler(this.menuItemDownloadDatabase_Click);
            // 
            // odeslatDataVyrobyToolStripMenuItem
            // 
            this.odeslatDataVyrobyToolStripMenuItem.Name = "odeslatDataVyrobyToolStripMenuItem";
            this.odeslatDataVyrobyToolStripMenuItem.Size = new System.Drawing.Size(468, 52);
            this.odeslatDataVyrobyToolStripMenuItem.Text = "Odeslat data výroby";
            this.odeslatDataVyrobyToolStripMenuItem.Click += new System.EventHandler(this.odeslatDataVyrobyToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 56);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(669, 422);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // timerShown
            // 
            this.timerShown.Tick += new System.EventHandler(this.FormMain_Shown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarInfo,
            this.toolStripProgressBar1,
            this.toolStripStatusPracovnik,
            this.toolStripStatusStroj,
            this.toolStripStatusZakazka,
            this.statusBarEmptySpace,
            this.statusBarContacts});
            this.statusStrip1.Location = new System.Drawing.Point(0, 478);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(669, 30);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusBarInfo
            // 
            this.statusBarInfo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.statusBarInfo.Name = "statusBarInfo";
            this.statusBarInfo.Size = new System.Drawing.Size(73, 25);
            this.statusBarInfo.Text = "Status info";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 24);
            // 
            // toolStripStatusPracovnik
            // 
            this.toolStripStatusPracovnik.Name = "toolStripStatusPracovnik";
            this.toolStripStatusPracovnik.Size = new System.Drawing.Size(58, 25);
            this.toolStripStatusPracovnik.Text = "V: P:";
            this.toolStripStatusPracovnik.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // toolStripStatusStroj
            // 
            this.toolStripStatusStroj.Name = "toolStripStatusStroj";
            this.toolStripStatusStroj.Size = new System.Drawing.Size(0, 25);
            // 
            // toolStripStatusZakazka
            // 
            this.toolStripStatusZakazka.Name = "toolStripStatusZakazka";
            this.toolStripStatusZakazka.Size = new System.Drawing.Size(31, 25);
            this.toolStripStatusZakazka.Text = "Z:";
            // 
            // statusBarEmptySpace
            // 
            this.statusBarEmptySpace.Name = "statusBarEmptySpace";
            this.statusBarEmptySpace.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.statusBarEmptySpace.Size = new System.Drawing.Size(280, 25);
            this.statusBarEmptySpace.Spring = true;
            // 
            // statusBarContacts
            // 
            this.statusBarContacts.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.statusBarContacts.Name = "statusBarContacts";
            this.statusBarContacts.Size = new System.Drawing.Size(110, 25);
            this.statusBarContacts.Text = "FASK, spol. s.r.o.";
            // 
            // ucTime
            // 
            this.ucTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ucTime.FormatTime = null;
            this.ucTime.FormatTimeWidth = null;
            this.ucTime.Location = new System.Drawing.Point(522, 3);
            this.ucTime.Name = "ucTime";
            this.ucTime.Size = new System.Drawing.Size(144, 50);
            this.ucTime.TabIndex = 0;
            // 
            // akceToolStripMenuItem
            // 
            this.akceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sledováníPapouchToolStripMenuItem});
            this.akceToolStripMenuItem.Name = "akceToolStripMenuItem";
            this.akceToolStripMenuItem.Size = new System.Drawing.Size(115, 52);
            this.akceToolStripMenuItem.Text = "Akce";
            // 
            // sledováníPapouchToolStripMenuItem
            // 
            this.sledováníPapouchToolStripMenuItem.Name = "sledováníPapouchToolStripMenuItem";
            this.sledováníPapouchToolStripMenuItem.Size = new System.Drawing.Size(430, 52);
            this.sledováníPapouchToolStripMenuItem.Text = "Sledování papouch";
            this.sledováníPapouchToolStripMenuItem.Click += new System.EventHandler(this.sledováiPapouchToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 508);
            this.Controls.Add(this.ucTime);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "Výroba";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemAplikace;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItemKonec;
        private System.Windows.Forms.ToolStripMenuItem menuItem3;
        private System.Windows.Forms.ToolStripMenuItem menuItemOdhlasitSmenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemConfiguration;
        private System.Windows.Forms.ToolStripMenuItem menuItemTimeSynchronization;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem menuItemDownloadDatabase;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer timerShown;
        private System.Windows.Forms.ToolStripMenuItem odeslatDataVyrobyToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarInfo;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem menuItemOdhlasitPracovnika;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusPracovnik;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusStroj;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusZakazka;
        private System.Windows.Forms.ToolStripStatusLabel statusBarEmptySpace;
        private System.Windows.Forms.ToolStripStatusLabel statusBarContacts;
        private Fask.Vyroba_P.Controls.ucTime ucTime;
        private System.Windows.Forms.ToolStripMenuItem menuItemOdhlasit;
        private System.Windows.Forms.ToolStripMenuItem akceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sledováníPapouchToolStripMenuItem;
    }
}