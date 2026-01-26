namespace Fask.Vyroba_W.Forms
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItemAplikace = new System.Windows.Forms.MenuItem();
			this.menuItemPrihlasitOdhladit = new System.Windows.Forms.MenuItem();
			this.menuItemLine1 = new System.Windows.Forms.MenuItem();
			this.menuItemKonec = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItemConfiguration = new System.Windows.Forms.MenuItem();
			this.menuItemTimeSynchronization = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItemDownloadDatabase = new System.Windows.Forms.MenuItem();
			this.menuItemOdeslatData = new System.Windows.Forms.MenuItem();
			this.timerShown = new System.Windows.Forms.Timer();
			this.buttonOdvadeni = new System.Windows.Forms.Button();
			this.statusBarInfo = new System.Windows.Forms.StatusBar();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonKorekce = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.buttonUdalosti = new System.Windows.Forms.Button();
			this.buttonTisk = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItemAplikace);
			this.mainMenu1.MenuItems.Add(this.menuItem3);
			// 
			// menuItemAplikace
			// 
			this.menuItemAplikace.MenuItems.Add(this.menuItemPrihlasitOdhladit);
			this.menuItemAplikace.MenuItems.Add(this.menuItemLine1);
			this.menuItemAplikace.MenuItems.Add(this.menuItemKonec);
			this.menuItemAplikace.Text = "Aplikace";
			// 
			// menuItemPrihlasitOdhladit
			// 
			this.menuItemPrihlasitOdhladit.Text = "Pøihlásit / Odhlásit";
			this.menuItemPrihlasitOdhladit.Click += new System.EventHandler(this.menuItemPrihlasitOdhlasit_Click);
			// 
			// menuItemLine1
			// 
			this.menuItemLine1.Text = "-";
			// 
			// menuItemKonec
			// 
			this.menuItemKonec.Text = "Konec";
			this.menuItemKonec.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.MenuItems.Add(this.menuItemConfiguration);
			this.menuItem3.MenuItems.Add(this.menuItemTimeSynchronization);
			this.menuItem3.MenuItems.Add(this.menuItem4);
			this.menuItem3.MenuItems.Add(this.menuItemDownloadDatabase);
			this.menuItem3.MenuItems.Add(this.menuItemOdeslatData);
			this.menuItem3.Text = "System";
			// 
			// menuItemConfiguration
			// 
			this.menuItemConfiguration.Text = "Konfigurace";
			this.menuItemConfiguration.Click += new System.EventHandler(this.menuItemConfiguration_Click);
			// 
			// menuItemTimeSynchronization
			// 
			this.menuItemTimeSynchronization.Text = "Synchronizace èasu se serverem";
			this.menuItemTimeSynchronization.Click += new System.EventHandler(this.menuItemTimeSynchronization_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Text = "-";
			// 
			// menuItemDownloadDatabase
			// 
			this.menuItemDownloadDatabase.Text = "Aktualizace databáze";
			this.menuItemDownloadDatabase.Click += new System.EventHandler(this.menuItemDownloadDatabase_Click);
			// 
			// menuItemOdeslatData
			// 
			this.menuItemOdeslatData.Text = "Odeslat data";
			this.menuItemOdeslatData.Click += new System.EventHandler(this.menuItemOdeslatData_Click);
			// 
			// timerShown
			// 
			this.timerShown.Tick += new System.EventHandler(this.FormMain_Shown);
			// 
			// buttonOdvadeni
			// 
			this.buttonOdvadeni.BackColor = System.Drawing.Color.IndianRed;
			this.buttonOdvadeni.Dock = System.Windows.Forms.DockStyle.Top;
			this.buttonOdvadeni.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.buttonOdvadeni.Location = new System.Drawing.Point(0, 0);
			this.buttonOdvadeni.Name = "buttonOdvadeni";
			this.buttonOdvadeni.Size = new System.Drawing.Size(204, 202);
			this.buttonOdvadeni.TabIndex = 0;
			this.buttonOdvadeni.Text = "Odvádìní";
			this.buttonOdvadeni.Click += new System.EventHandler(this.buttonOdvadeni_Click);
			this.buttonOdvadeni.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonOdvadeni_KeyDown);
			// 
			// statusBarInfo
			// 
			this.statusBarInfo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.statusBarInfo.Location = new System.Drawing.Point(0, 423);
			this.statusBarInfo.Name = "statusBarInfo";
			this.statusBarInfo.Size = new System.Drawing.Size(431, 20);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonKorekce);
			this.panel1.Controls.Add(this.buttonOdvadeni);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(204, 423);
			this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
			// 
			// buttonKorekce
			// 
			this.buttonKorekce.BackColor = System.Drawing.Color.RosyBrown;
			this.buttonKorekce.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonKorekce.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.buttonKorekce.Location = new System.Drawing.Point(0, 202);
			this.buttonKorekce.Name = "buttonKorekce";
			this.buttonKorekce.Size = new System.Drawing.Size(204, 221);
			this.buttonKorekce.TabIndex = 1;
			this.buttonKorekce.Text = "Korekce";
			this.buttonKorekce.Click += new System.EventHandler(this.buttonKorekce_Click);
			this.buttonKorekce.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonKorekce_KeyDown);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.buttonUdalosti);
			this.panel2.Controls.Add(this.buttonTisk);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(204, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(227, 423);
			// 
			// buttonUdalosti
			// 
			this.buttonUdalosti.BackColor = System.Drawing.Color.MediumAquamarine;
			this.buttonUdalosti.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonUdalosti.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.buttonUdalosti.Location = new System.Drawing.Point(0, 202);
			this.buttonUdalosti.Name = "buttonUdalosti";
			this.buttonUdalosti.Size = new System.Drawing.Size(227, 221);
			this.buttonUdalosti.TabIndex = 2;
			this.buttonUdalosti.Text = "Události";
			this.buttonUdalosti.Click += new System.EventHandler(this.buttonUdalosti_Click);
			this.buttonUdalosti.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonUdalosti_KeyDown);
			// 
			// buttonTisk
			// 
			this.buttonTisk.BackColor = System.Drawing.Color.Khaki;
			this.buttonTisk.Dock = System.Windows.Forms.DockStyle.Top;
			this.buttonTisk.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.buttonTisk.Location = new System.Drawing.Point(0, 0);
			this.buttonTisk.Name = "buttonTisk";
			this.buttonTisk.Size = new System.Drawing.Size(227, 202);
			this.buttonTisk.TabIndex = 3;
			this.buttonTisk.Text = "Tisk";
			this.buttonTisk.Click += new System.EventHandler(this.buttonTisk_Click);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(431, 443);
			this.ControlBox = false;
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.statusBarInfo);
			this.KeyPreview = true;
			this.Menu = this.mainMenu1;
			this.Name = "FormMain";
			this.Text = "Výroba";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormMain_Closing);
			this.Resize += new System.EventHandler(this.FormMain_Resize);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItemAplikace;
        private System.Windows.Forms.MenuItem menuItemKonec;
        private System.Windows.Forms.Timer timerShown;
        private System.Windows.Forms.Button buttonOdvadeni;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItemTimeSynchronization;
        private System.Windows.Forms.StatusBar statusBarInfo;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemDownloadDatabase;
        private System.Windows.Forms.MenuItem menuItemPrihlasitOdhladit;
        private System.Windows.Forms.MenuItem menuItemLine1;
        private System.Windows.Forms.MenuItem menuItemConfiguration;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonKorekce;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonUdalosti;
        private System.Windows.Forms.MenuItem menuItemOdeslatData;
        private System.Windows.Forms.Button buttonTisk;
    }
}
