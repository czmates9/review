namespace Fask.MST_W.Expedice
{
    partial class ExpediceBaleniVyberHlavickyList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpediceBaleniVyberHlavickyList));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miNovyPrikaz = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miNajitLokaci = new System.Windows.Forms.MenuItem();
            this.miAktualizovat = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.miOdstranitPrikaz = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miPrerusit = new System.Windows.Forms.MenuItem();
            this.bsHlavicky = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.zpet_but = new Fask.Graphic.GraphicButton();
            this.ok_but = new Fask.Graphic.GraphicButton();
            this.timerLoad = new System.Windows.Forms.Timer();
            this.dsExpediceBaleniHlavicky = new Fask.MST_W.ExpediceService.ExpediceBaleniHlavicky();
            ((System.ComponentModel.ISupportInitialize)(this.bsHlavicky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsExpediceBaleniHlavicky)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.miNovyPrikaz);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.miNajitLokaci);
            this.menuItem1.MenuItems.Add(this.miAktualizovat);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.miOdstranitPrikaz);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.miPrerusit);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // miNovyPrikaz
            // 
            resources.ApplyResources(this.miNovyPrikaz, "miNovyPrikaz");
            this.miNovyPrikaz.Click += new System.EventHandler(this.miNovyPrikaz_Click);
            // 
            // menuItem3
            // 
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // miNajitLokaci
            // 
            resources.ApplyResources(this.miNajitLokaci, "miNajitLokaci");
            this.miNajitLokaci.Click += new System.EventHandler(this.miNajitLokaci_Click);
            // 
            // miAktualizovat
            // 
            resources.ApplyResources(this.miAktualizovat, "miAktualizovat");
            this.miAktualizovat.Click += new System.EventHandler(this.miAktualizovat_Click);
            // 
            // menuItem4
            // 
            resources.ApplyResources(this.menuItem4, "menuItem4");
            // 
            // miOdstranitPrikaz
            // 
            resources.ApplyResources(this.miOdstranitPrikaz, "miOdstranitPrikaz");
            this.miOdstranitPrikaz.Click += new System.EventHandler(this.miOdstranitPrikaz_Click);
            // 
            // menuItem2
            // 
            resources.ApplyResources(this.menuItem2, "menuItem2");
            // 
            // miPrerusit
            // 
            resources.ApplyResources(this.miPrerusit, "miPrerusit");
            this.miPrerusit.Click += new System.EventHandler(this.miKonec_Click);
            // 
            // bsHlavicky
            // 
            this.bsHlavicky.DataMember = "CZMST_Expedice_Baleni_Hlavicka";
            this.bsHlavicky.DataSource = this.dsExpediceBaleniHlavicky;
            this.bsHlavicky.Sort = "";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            resources.ApplyResources(this.dataGrid1, "dataGrid1");
            this.dataGrid1.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dataGrid1.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dataGrid1.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dataGrid1.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dataGrid1.MultiSelect = false;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.NumberFormat = "N";
            this.dataGrid1.RowHeightDefault = 23;
            this.dataGrid1.Sort = "";
            this.dataGrid1.SortByHeaderDoubleClick = true;
            this.dataGrid1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGrid1_KeyPress);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.zpet_but);
            this.panelButtons.Controls.Add(this.ok_but);
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // zpet_but
            // 
            this.zpet_but.BitmapNormal = null;
            resources.ApplyResources(this.zpet_but, "zpet_but");
            this.zpet_but.FocusMargin = 5;
            this.zpet_but.Name = "zpet_but";
            this.zpet_but.Pressed = false;
            this.zpet_but.Transparent = System.Drawing.Color.White;
            this.zpet_but.Click += new System.EventHandler(this.zpet_but_Click);
            // 
            // ok_but
            // 
            this.ok_but.BitmapNormal = null;
            resources.ApplyResources(this.ok_but, "ok_but");
            this.ok_but.FocusMargin = 5;
            this.ok_but.Name = "ok_but";
            this.ok_but.Pressed = false;
            this.ok_but.Transparent = System.Drawing.Color.White;
            this.ok_but.Click += new System.EventHandler(this.ok_but_Click);
            // 
            // timerLoad
            // 
            this.timerLoad.Tick += new System.EventHandler(this.ExpediceVyberHlavickyList_Shown);
            // 
            // dsExpediceBaleniHlavicky
            // 
            this.dsExpediceBaleniHlavicky.DataSetName = "ExpediceBaleniHlavicky";
            this.dsExpediceBaleniHlavicky.Prefix = "";
            this.dsExpediceBaleniHlavicky.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ExpediceBaleniVyberHlavickyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ExpediceBaleniVyberHlavickyList";
            this.Load += new System.EventHandler(this.PrijemVyberPrijmoveLokaceList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrijemVyberPrijmoveLokaceList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.bsHlavicky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dsExpediceBaleniHlavicky)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miPrerusit;
        private Fask.Graphic.DataGrid2 dataGrid1;
        protected System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton zpet_but;
        private Fask.Graphic.GraphicButton ok_but;
        private System.Windows.Forms.MenuItem miAktualizovat;
        private System.Windows.Forms.MenuItem miNajitLokaci;
        private System.Windows.Forms.BindingSource bsHlavicky;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miNovyPrikaz;
        private System.Windows.Forms.MenuItem miOdstranitPrikaz;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.Timer timerLoad;
        private Fask.MST_W.ExpediceService.ExpediceBaleniHlavicky dsExpediceBaleniHlavicky;
    }
}