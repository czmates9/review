namespace FASK.MST_WINDOWS.Main
{
    partial class frmMainApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainApp));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aplikaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.nastaveníToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.zbožíToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.typiDokladuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skladyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oberateleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pracovniciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.støediskaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uživateleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.všechnyAktualizovatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.errorLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modulyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsDatabaseSynchronizationStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsModuleStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.loginToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.konecToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aplikaceToolStripMenuItem,
            this.modulyToolStripMenuItem,
            this.toolStripMenuItem6});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.toolStripMenuItem6;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 48);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aplikaceToolStripMenuItem
            // 
            this.aplikaceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.logoutToolStripMenuItem4,
            this.toolStripMenuItem4,
            this.nastaveníToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.errorLogToolStripMenuItem,
            this.toolStripMenuItem5,
            this.konecToolStripMenuItem});
            this.aplikaceToolStripMenuItem.Name = "aplikaceToolStripMenuItem";
            this.aplikaceToolStripMenuItem.Size = new System.Drawing.Size(148, 44);
            this.aplikaceToolStripMenuItem.Text = "&Aplikace";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Image = global::FASK.MST_WINDOWS.Main.Properties.Resources.ActionRequired_03;
            this.loginToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(430, 44);
            this.loginToolStripMenuItem.Text = "&Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem4
            // 
            this.logoutToolStripMenuItem4.Image = global::FASK.MST_WINDOWS.Main.Properties.Resources.ActionRequired_03;
            this.logoutToolStripMenuItem4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.logoutToolStripMenuItem4.Name = "logoutToolStripMenuItem4";
            this.logoutToolStripMenuItem4.Size = new System.Drawing.Size(430, 44);
            this.logoutToolStripMenuItem4.Text = "&Logout";
            this.logoutToolStripMenuItem4.Click += new System.EventHandler(this.logoutToolStripMenuItem4_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(427, 6);
            // 
            // nastaveníToolStripMenuItem
            // 
            this.nastaveníToolStripMenuItem.Image = global::FASK.MST_WINDOWS.Main.Properties.Resources.gear_1;
            this.nastaveníToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nastaveníToolStripMenuItem.Name = "nastaveníToolStripMenuItem";
            this.nastaveníToolStripMenuItem.Size = new System.Drawing.Size(430, 44);
            this.nastaveníToolStripMenuItem.Text = "&Nastavení";
            this.nastaveníToolStripMenuItem.Click += new System.EventHandler(this.nastaveníToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.všechnyAktualizovatToolStripMenuItem,
            this.toolStripSeparator2,
            this.uživateleToolStripMenuItem,
            this.toolStripSeparator1,
            this.zbožíToolStripMenuItem,
            this.typiDokladuToolStripMenuItem,
            this.skladyToolStripMenuItem,
            this.oberateleToolStripMenuItem,
            this.pracovniciToolStripMenuItem,
            this.støediskaToolStripMenuItem});
            this.toolStripMenuItem2.Image = global::FASK.MST_WINDOWS.Main.Properties.Resources.database;
            this.toolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(430, 44);
            this.toolStripMenuItem2.Text = "&Synchronizace èíselníky";
            // 
            // zbožíToolStripMenuItem
            // 
            this.zbožíToolStripMenuItem.Name = "zbožíToolStripMenuItem";
            this.zbožíToolStripMenuItem.Size = new System.Drawing.Size(395, 44);
            this.zbožíToolStripMenuItem.Text = "Zboží";
            this.zbožíToolStripMenuItem.Click += new System.EventHandler(this.zbožíToolStripMenuItem_Click);
            // 
            // typiDokladuToolStripMenuItem
            // 
            this.typiDokladuToolStripMenuItem.Name = "typiDokladuToolStripMenuItem";
            this.typiDokladuToolStripMenuItem.Size = new System.Drawing.Size(395, 44);
            this.typiDokladuToolStripMenuItem.Text = "Typy dokladù";
            this.typiDokladuToolStripMenuItem.Click += new System.EventHandler(this.typiDokladuToolStripMenuItem_Click);
            // 
            // skladyToolStripMenuItem
            // 
            this.skladyToolStripMenuItem.Name = "skladyToolStripMenuItem";
            this.skladyToolStripMenuItem.Size = new System.Drawing.Size(395, 44);
            this.skladyToolStripMenuItem.Text = "Sklady";
            this.skladyToolStripMenuItem.Click += new System.EventHandler(this.skladyToolStripMenuItem_Click);
            // 
            // oberateleToolStripMenuItem
            // 
            this.oberateleToolStripMenuItem.Name = "oberateleToolStripMenuItem";
            this.oberateleToolStripMenuItem.Size = new System.Drawing.Size(395, 44);
            this.oberateleToolStripMenuItem.Text = "Oberatele";
            this.oberateleToolStripMenuItem.Click += new System.EventHandler(this.oberateleToolStripMenuItem_Click);
            // 
            // pracovniciToolStripMenuItem
            // 
            this.pracovniciToolStripMenuItem.Name = "pracovniciToolStripMenuItem";
            this.pracovniciToolStripMenuItem.Size = new System.Drawing.Size(395, 44);
            this.pracovniciToolStripMenuItem.Text = "Pracovnici";
            this.pracovniciToolStripMenuItem.Click += new System.EventHandler(this.pracovniciToolStripMenuItem_Click);
            // 
            // støediskaToolStripMenuItem
            // 
            this.støediskaToolStripMenuItem.Name = "støediskaToolStripMenuItem";
            this.støediskaToolStripMenuItem.Size = new System.Drawing.Size(395, 44);
            this.støediskaToolStripMenuItem.Text = "Støediska";
            this.støediskaToolStripMenuItem.Click += new System.EventHandler(this.støediskaToolStripMenuItem_Click);
            // 
            // uživateleToolStripMenuItem
            // 
            this.uživateleToolStripMenuItem.Name = "uživateleToolStripMenuItem";
            this.uživateleToolStripMenuItem.Size = new System.Drawing.Size(395, 44);
            this.uživateleToolStripMenuItem.Text = "Uživatele";
            this.uživateleToolStripMenuItem.Click += new System.EventHandler(this.uživateleToolStripMenuItem_Click);
            // 
            // všechnyAktualizovatToolStripMenuItem
            // 
            this.všechnyAktualizovatToolStripMenuItem.Name = "všechnyAktualizovatToolStripMenuItem";
            this.všechnyAktualizovatToolStripMenuItem.Size = new System.Drawing.Size(395, 44);
            this.všechnyAktualizovatToolStripMenuItem.Text = "Všechny aktualizovat";
            this.všechnyAktualizovatToolStripMenuItem.Click += new System.EventHandler(this.všechnyAktualizovatToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(427, 6);
            // 
            // errorLogToolStripMenuItem
            // 
            this.errorLogToolStripMenuItem.Name = "errorLogToolStripMenuItem";
            this.errorLogToolStripMenuItem.Size = new System.Drawing.Size(430, 44);
            this.errorLogToolStripMenuItem.Text = "Logování chyb";
            this.errorLogToolStripMenuItem.Click += new System.EventHandler(this.errorLogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(427, 6);
            this.toolStripMenuItem5.Visible = false;
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Image = global::FASK.MST_WINDOWS.Main.Properties.Resources.BuilderDialog_delete;
            this.konecToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(430, 44);
            this.konecToolStripMenuItem.Text = "&Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // modulyToolStripMenuItem
            // 
            this.modulyToolStripMenuItem.Name = "modulyToolStripMenuItem";
            this.modulyToolStripMenuItem.Size = new System.Drawing.Size(132, 44);
            this.modulyToolStripMenuItem.Text = "&Moduly";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(42, 44);
            this.toolStripMenuItem6.Text = "|";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsDatabaseSynchronizationStatus,
            this.tsModuleStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsDatabaseSynchronizationStatus
            // 
            this.tsDatabaseSynchronizationStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.tsDatabaseSynchronizationStatus.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tsDatabaseSynchronizationStatus.Name = "tsDatabaseSynchronizationStatus";
            this.tsDatabaseSynchronizationStatus.Size = new System.Drawing.Size(4, 17);
            // 
            // tsModuleStatus
            // 
            this.tsModuleStatus.Name = "tsModuleStatus";
            this.tsModuleStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // loginToolStripMenuItem1
            // 
            this.loginToolStripMenuItem1.Image = global::FASK.MST_WINDOWS.Main.Properties.Resources.ActionRequired_03;
            this.loginToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loginToolStripMenuItem1.Name = "loginToolStripMenuItem1";
            this.loginToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.loginToolStripMenuItem1.Text = "Login";
            this.loginToolStripMenuItem1.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem5
            // 
            this.logoutToolStripMenuItem5.Image = global::FASK.MST_WINDOWS.Main.Properties.Resources.ActionRequired_03;
            this.logoutToolStripMenuItem5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.logoutToolStripMenuItem5.Name = "logoutToolStripMenuItem5";
            this.logoutToolStripMenuItem5.Size = new System.Drawing.Size(112, 22);
            this.logoutToolStripMenuItem5.Text = "Logout";
            this.logoutToolStripMenuItem5.Click += new System.EventHandler(this.logoutToolStripMenuItem5_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(109, 6);
            // 
            // konecToolStripMenuItem1
            // 
            this.konecToolStripMenuItem1.Image = global::FASK.MST_WINDOWS.Main.Properties.Resources.BuilderDialog_delete;
            this.konecToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.konecToolStripMenuItem1.Name = "konecToolStripMenuItem1";
            this.konecToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.konecToolStripMenuItem1.Text = "Konec";
            this.konecToolStripMenuItem1.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(392, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(392, 6);
            // 
            // frmMainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMainApp";
            this.Text = "Sledovani vyroby";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainApp_FormClosing);
            this.Load += new System.EventHandler(this.frmMainApp_Load);
            this.Shown += new System.EventHandler(this.frmMainApp_Shown);
            this.Resize += new System.EventHandler(this.frmMainApp_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aplikaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem modulyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nastaveníToolStripMenuItem;
        //private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsDatabaseSynchronizationStatus;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem errorLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tsModuleStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem zbožíToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem typiDokladuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skladyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oberateleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pracovniciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem støediskaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uživateleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem všechnyAktualizovatToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}