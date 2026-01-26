namespace Fask.MST_W.Prodej_3
{
    partial class ProdejVyberDavky
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProdejVyberDavky));
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemNova = new System.Windows.Forms.MenuItem();
            this.menuItemVybrat = new System.Windows.Forms.MenuItem();
            this.menuItemSmazat = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItemZpet = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
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
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemNova);
            this.menuItem1.MenuItems.Add(this.menuItemVybrat);
            this.menuItem1.MenuItems.Add(this.menuItemSmazat);
            this.menuItem1.MenuItems.Add(this.menuItem5);
            this.menuItem1.MenuItems.Add(this.menuItemZpet);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemNova
            // 
            resources.ApplyResources(this.menuItemNova, "menuItemNova");
            this.menuItemNova.Click += new System.EventHandler(this.menuItemNova_Click);
            // 
            // menuItemVybrat
            // 
            resources.ApplyResources(this.menuItemVybrat, "menuItemVybrat");
            this.menuItemVybrat.Click += new System.EventHandler(this.menuItemVybrat_Click);
            // 
            // menuItemSmazat
            // 
            resources.ApplyResources(this.menuItemSmazat, "menuItemSmazat");
            this.menuItemSmazat.Click += new System.EventHandler(this.menuItemSmazat_Click);
            // 
            // menuItem5
            // 
            resources.ApplyResources(this.menuItem5, "menuItem5");
            // 
            // menuItemZpet
            // 
            resources.ApplyResources(this.menuItemZpet, "menuItemZpet");
            this.menuItemZpet.Click += new System.EventHandler(this.menuItemZpet_Click);
            // 
            // ProdejVyberDavky
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ProdejVyberDavky";
            this.Deactivate += new System.EventHandler(this.ProdejVyberDavky_Deactivate);
            this.Load += new System.EventHandler(this.ProdejVyberDavky_Load);
            this.Activated += new System.EventHandler(this.ProdejVyberDavky_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProdejVyberDavky_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemNova;
        private System.Windows.Forms.MenuItem menuItemVybrat;
        private System.Windows.Forms.MenuItem menuItemSmazat;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItemZpet;
    }
}