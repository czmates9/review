namespace Fask.MST_W
{
    partial class Main
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
            try
            {
                Logging.Log.Write("main dispose");
                Logging.Log.Write("main dispose: Scanner is null=" + (this.Scanner == null).ToString());

            }
            catch (System.Exception exDispose)
            {
                Logging.Log.Write(exDispose);
            } 
            
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.timerLoad = new System.Windows.Forms.Timer();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.mi_KonScan = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItemRFIDSnimat = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItemOnline = new System.Windows.Forms.MenuItem();
            this.menuItemOnlineNovyEAN = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonExpedice = new Fask.Graphic.GraphicButton();
            this.buttonServis = new Fask.Graphic.GraphicButton();
            this.buttonTasks = new Fask.Graphic.GraphicButton();
            this.buttonEvents = new Fask.Graphic.GraphicButton();
            this.buttonInventura2 = new Fask.Graphic.GraphicButton();
            this.buttonProdej = new Fask.Graphic.GraphicButton();
            this.buttonVydej = new Fask.Graphic.GraphicButton();
            this.buttonInventura1csv = new Fask.Graphic.GraphicButton();
            this.buttonPrijem = new Fask.Graphic.GraphicButton();
            this.statusBarInfo = new System.Windows.Forms.StatusBar();
            this.menuItemInfo = new System.Windows.Forms.MenuItem();
            this.menuItemInfoIPAdresy = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerLoad
            // 
            this.timerLoad.Interval = 200;
            this.timerLoad.Tick += new System.EventHandler(this.Main_MST_W_Shown);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem6);
            this.mainMenu1.MenuItems.Add(this.menuItemOnline);
            this.mainMenu1.MenuItems.Add(this.menuItemInfo);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItem7);
            this.menuItem1.MenuItems.Add(this.mi_KonScan);
            this.menuItem1.MenuItems.Add(this.menuItem5);
            this.menuItem1.MenuItems.Add(this.menuItemRFIDSnimat);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.Text = "Aplikace";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Login";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Konfigurace";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click_1);
            // 
            // menuItem7
            // 
            this.menuItem7.Text = "-";
            // 
            // mi_KonScan
            // 
            this.mi_KonScan.Text = "Kon. Scan";
            this.mi_KonScan.Click += new System.EventHandler(this.mi_KonScan_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "-";
            // 
            // menuItemRFIDSnimat
            // 
            this.menuItemRFIDSnimat.Enabled = false;
            this.menuItemRFIDSnimat.Text = "RFID Snimat";
            this.menuItemRFIDSnimat.Click += new System.EventHandler(this.menuItemRFIDSnimat_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "Konec";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Text = "Moduly";
            // 
            // menuItemOnline
            // 
            this.menuItemOnline.MenuItems.Add(this.menuItemOnlineNovyEAN);
            this.menuItemOnline.Text = "Online";
            // 
            // menuItemOnlineNovyEAN
            // 
            this.menuItemOnlineNovyEAN.Text = "F5 - Nový EAN";
            this.menuItemOnlineNovyEAN.Click += new System.EventHandler(this.menuItemOnlineNovyEAN_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonExpedice);
            this.panel1.Controls.Add(this.buttonServis);
            this.panel1.Controls.Add(this.buttonTasks);
            this.panel1.Controls.Add(this.buttonEvents);
            this.panel1.Controls.Add(this.buttonInventura2);
            this.panel1.Controls.Add(this.buttonProdej);
            this.panel1.Controls.Add(this.buttonVydej);
            this.panel1.Controls.Add(this.buttonInventura1csv);
            this.panel1.Controls.Add(this.buttonPrijem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 307);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // buttonExpedice
            // 
            this.buttonExpedice.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonExpedice.BitmapNormal = ((System.Drawing.Bitmap)(resources.GetObject("buttonExpedice.BitmapNormal")));
            this.buttonExpedice.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonExpedice.FocusMargin = 2;
            this.buttonExpedice.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.buttonExpedice.Location = new System.Drawing.Point(0, 273);
            this.buttonExpedice.Name = "buttonExpedice";
            this.buttonExpedice.Pressed = false;
            this.buttonExpedice.Size = new System.Drawing.Size(238, 34);
            this.buttonExpedice.TabIndex = 12;
            this.buttonExpedice.Text = "Expedice";
            this.buttonExpedice.Transparent = System.Drawing.Color.Magenta;
            this.buttonExpedice.Click += new System.EventHandler(this.buttonExpedice_Click);
            // 
            // buttonServis
            // 
            this.buttonServis.BackColor = System.Drawing.Color.Moccasin;
            this.buttonServis.BitmapNormal = ((System.Drawing.Bitmap)(resources.GetObject("buttonServis.BitmapNormal")));
            this.buttonServis.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonServis.FocusMargin = 2;
            this.buttonServis.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.buttonServis.Location = new System.Drawing.Point(0, 239);
            this.buttonServis.Name = "buttonServis";
            this.buttonServis.Pressed = false;
            this.buttonServis.Size = new System.Drawing.Size(238, 34);
            this.buttonServis.TabIndex = 11;
            this.buttonServis.Text = "Servis";
            this.buttonServis.Transparent = System.Drawing.Color.Magenta;
            this.buttonServis.Click += new System.EventHandler(this.buttonServis_Click);
            this.buttonServis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonServis_KeyDown);
            // 
            // buttonTasks
            // 
            this.buttonTasks.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonTasks.BitmapNormal = ((System.Drawing.Bitmap)(resources.GetObject("buttonTasks.BitmapNormal")));
            this.buttonTasks.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonTasks.FocusMargin = 2;
            this.buttonTasks.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.buttonTasks.Location = new System.Drawing.Point(0, 205);
            this.buttonTasks.Name = "buttonTasks";
            this.buttonTasks.Pressed = false;
            this.buttonTasks.Size = new System.Drawing.Size(238, 34);
            this.buttonTasks.TabIndex = 10;
            this.buttonTasks.Text = "Úkoly";
            this.buttonTasks.Transparent = System.Drawing.Color.Magenta;
            this.buttonTasks.Click += new System.EventHandler(this.buttonTasks_Click);
            this.buttonTasks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonTasks_KeyDown);
            // 
            // buttonEvents
            // 
            this.buttonEvents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonEvents.BitmapNormal = ((System.Drawing.Bitmap)(resources.GetObject("buttonEvents.BitmapNormal")));
            this.buttonEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonEvents.FocusMargin = 2;
            this.buttonEvents.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.buttonEvents.Location = new System.Drawing.Point(0, 171);
            this.buttonEvents.Name = "buttonEvents";
            this.buttonEvents.Pressed = false;
            this.buttonEvents.Size = new System.Drawing.Size(238, 34);
            this.buttonEvents.TabIndex = 9;
            this.buttonEvents.Text = "Události";
            this.buttonEvents.Transparent = System.Drawing.Color.Magenta;
            this.buttonEvents.Click += new System.EventHandler(this.buttonEvents_Click);
            this.buttonEvents.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonEvents_KeyDown);
            // 
            // buttonInventura2
            // 
            this.buttonInventura2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonInventura2.BitmapNormal = ((System.Drawing.Bitmap)(resources.GetObject("buttonInventura2.BitmapNormal")));
            this.buttonInventura2.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonInventura2.FocusMargin = 2;
            this.buttonInventura2.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.buttonInventura2.Location = new System.Drawing.Point(0, 137);
            this.buttonInventura2.Name = "buttonInventura2";
            this.buttonInventura2.Pressed = false;
            this.buttonInventura2.Size = new System.Drawing.Size(238, 34);
            this.buttonInventura2.TabIndex = 8;
            this.buttonInventura2.Text = "Inventura 2";
            this.buttonInventura2.Transparent = System.Drawing.Color.Magenta;
            this.buttonInventura2.Click += new System.EventHandler(this.buttonInventura2_Click);
            this.buttonInventura2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonInventura2_KeyDown);
            // 
            // buttonProdej
            // 
            this.buttonProdej.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.buttonProdej.BitmapNormal = ((System.Drawing.Bitmap)(resources.GetObject("buttonProdej.BitmapNormal")));
            this.buttonProdej.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonProdej.FocusMargin = 2;
            this.buttonProdej.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.buttonProdej.Location = new System.Drawing.Point(0, 102);
            this.buttonProdej.Name = "buttonProdej";
            this.buttonProdej.Pressed = false;
            this.buttonProdej.Size = new System.Drawing.Size(238, 35);
            this.buttonProdej.TabIndex = 7;
            this.buttonProdej.Text = "Prodej";
            this.buttonProdej.Transparent = System.Drawing.Color.Magenta;
            this.buttonProdej.Click += new System.EventHandler(this.buttonProdej_Click);
            this.buttonProdej.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonProdej_KeyDown);
            // 
            // buttonVydej
            // 
            this.buttonVydej.BackColor = System.Drawing.Color.Cyan;
            this.buttonVydej.BitmapNormal = ((System.Drawing.Bitmap)(resources.GetObject("buttonVydej.BitmapNormal")));
            this.buttonVydej.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonVydej.FocusMargin = 2;
            this.buttonVydej.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.buttonVydej.Location = new System.Drawing.Point(0, 65);
            this.buttonVydej.Name = "buttonVydej";
            this.buttonVydej.Pressed = false;
            this.buttonVydej.Size = new System.Drawing.Size(238, 37);
            this.buttonVydej.TabIndex = 3;
            this.buttonVydej.Text = "Výdej";
            this.buttonVydej.Transparent = System.Drawing.Color.Magenta;
            this.buttonVydej.Click += new System.EventHandler(this.buttonVydej_Click);
            this.buttonVydej.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonVydej_KeyDown);
            // 
            // buttonInventura1csv
            // 
            this.buttonInventura1csv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonInventura1csv.BitmapNormal = ((System.Drawing.Bitmap)(resources.GetObject("buttonInventura1csv.BitmapNormal")));
            this.buttonInventura1csv.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonInventura1csv.FocusMargin = 2;
            this.buttonInventura1csv.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.buttonInventura1csv.Location = new System.Drawing.Point(0, 31);
            this.buttonInventura1csv.Name = "buttonInventura1csv";
            this.buttonInventura1csv.Pressed = false;
            this.buttonInventura1csv.Size = new System.Drawing.Size(238, 34);
            this.buttonInventura1csv.TabIndex = 2;
            this.buttonInventura1csv.Text = "Inventura 1";
            this.buttonInventura1csv.Transparent = System.Drawing.Color.Magenta;
            this.buttonInventura1csv.Click += new System.EventHandler(this.buttonInventura1csv_Click);
            this.buttonInventura1csv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonInventura1csv_KeyDown);
            // 
            // buttonPrijem
            // 
            this.buttonPrijem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonPrijem.BitmapNormal = ((System.Drawing.Bitmap)(resources.GetObject("buttonPrijem.BitmapNormal")));
            this.buttonPrijem.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonPrijem.FocusMargin = 2;
            this.buttonPrijem.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.buttonPrijem.Location = new System.Drawing.Point(0, 0);
            this.buttonPrijem.Name = "buttonPrijem";
            this.buttonPrijem.Pressed = false;
            this.buttonPrijem.Size = new System.Drawing.Size(238, 31);
            this.buttonPrijem.TabIndex = 1;
            this.buttonPrijem.Text = "Pøíjem";
            this.buttonPrijem.Transparent = System.Drawing.Color.Magenta;
            this.buttonPrijem.Click += new System.EventHandler(this.buttonPrijem_Click);
            this.buttonPrijem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonPrijem_KeyDown);
            // 
            // statusBarInfo
            // 
            this.statusBarInfo.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.statusBarInfo.Location = new System.Drawing.Point(0, 307);
            this.statusBarInfo.Name = "statusBarInfo";
            this.statusBarInfo.Size = new System.Drawing.Size(238, 19);
            // 
            // menuItemInfo
            // 
            this.menuItemInfo.MenuItems.Add(this.menuItemInfoIPAdresy);
            this.menuItemInfo.Text = "Info";
            // 
            // menuItemInfoIPAdresy
            // 
            this.menuItemInfoIPAdresy.Text = "IP Adresy";
            this.menuItemInfoIPAdresy.Click += new System.EventHandler(this.menuItemInfoIPAdresy_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 326);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusBarInfo);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "Main";
            this.Text = "MST_W";
            this.Deactivate += new System.EventHandler(this.Main_Deactivate);
            this.Load += new System.EventHandler(this.Main_MST_W_Load);
            this.DoubleClick += new System.EventHandler(this.Main_DoubleClick);
            this.Activated += new System.EventHandler(this.Main_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Main_Closing);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        

        #endregion

        //private Fask.Graphic.GraphicButton buttonVydej;
        //private Fask.Graphic.GraphicButton buttonInventura1csv;
        //private Fask.Graphic.GraphicButton buttonProdej;
        //private Fask.Graphic.GraphicButton buttonPrijem;
        private System.Windows.Forms.Timer timerLoad;
        //private Fask.Graphic.GraphicButton button_Zavoz;
        //private Fask.Graphic.GraphicButton button_prijem_2;
        //private Fask.Graphic.GraphicButton button_Vydej_VS;
        //private Fask.Graphic.GraphicButton button_polohovani;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem6;
        //nove buttony
        private Graphic.GraphicButton buttonVydej;
        private Graphic.GraphicButton buttonInventura1csv;
        private Graphic.GraphicButton buttonProdej;
        private Graphic.GraphicButton buttonPrijem;
        private System.Windows.Forms.Panel panel1;
        private Fask.Graphic.GraphicButton buttonInventura2;
        private System.Windows.Forms.StatusBar statusBarInfo;
        private System.Windows.Forms.MenuItem menuItemOnline;
        private System.Windows.Forms.MenuItem menuItemOnlineNovyEAN;
        private Fask.Graphic.GraphicButton buttonEvents;
        private Fask.Graphic.GraphicButton buttonTasks;
        private Fask.Graphic.GraphicButton buttonServis;
        private Fask.Graphic.GraphicButton buttonExpedice;
        private System.Windows.Forms.MenuItem menuItemRFIDSnimat;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem mi_KonScan;
        private System.Windows.Forms.MenuItem menuItemInfo;
        private System.Windows.Forms.MenuItem menuItemInfoIPAdresy;

    }
}