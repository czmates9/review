namespace Fask.MST_W.Prodej_3
{
    partial class ProdejVyberTypuDokladu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProdejVyberTypuDokladu));
            this.buttonStorno = new Fask.Graphic.GraphicButton();
            this.buttonOK = new Fask.Graphic.GraphicButton();
            this.timerLoad = new System.Windows.Forms.Timer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemAktualize = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemNajit = new System.Windows.Forms.MenuItem();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStorno
            // 
            this.buttonStorno.BitmapNormal = null;
            resources.ApplyResources(this.buttonStorno, "buttonStorno");
            this.buttonStorno.FocusMargin = 5;
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Pressed = false;
            this.buttonStorno.Transparent = System.Drawing.Color.White;
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.BitmapNormal = null;
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.FocusMargin = 5;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Pressed = false;
            this.buttonOK.Transparent = System.Drawing.Color.White;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // timerLoad
            // 
            this.timerLoad.Tick += new System.EventHandler(this.ProdejTypDokladu_Shown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Controls.Add(this.buttonStorno);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemAktualize);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItemNajit);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemAktualize
            // 
            resources.ApplyResources(this.menuItemAktualize, "menuItemAktualize");
            this.menuItemAktualize.Click += new System.EventHandler(this.menuItemAktualize_Click);
            // 
            // menuItem3
            // 
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // menuItemNajit
            // 
            resources.ApplyResources(this.menuItemNajit, "menuItemNajit");
            this.menuItemNajit.Click += new System.EventHandler(this.menuItemNajit_Click);
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
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // ProdejVyberTypuDokladu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ProdejVyberTypuDokladu";
            this.Deactivate += new System.EventHandler(this.ProdejVyberTypuDokladu_Deactivate);
            this.Load += new System.EventHandler(this.ProdejVyberTypDokladu_Load);
            this.Activated += new System.EventHandler(this.ProdejVyberTypuDokladu_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ProdejTypDokladu_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProdejTypDokladu_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton buttonStorno;
        private Fask.Graphic.GraphicButton buttonOK;
        private System.Windows.Forms.Timer timerLoad;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemNajit;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.MenuItem menuItemAktualize;
        private System.Windows.Forms.MenuItem menuItem3;
    }
}