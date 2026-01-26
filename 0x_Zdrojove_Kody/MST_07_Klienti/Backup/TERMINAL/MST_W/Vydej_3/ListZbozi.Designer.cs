namespace Fask.MST_W.Vydej_3
{
    partial class ListZbozi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListZbozi));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemHledatCK = new System.Windows.Forms.MenuItem();
            this.menuItemVybrat = new System.Windows.Forms.MenuItem();
            this.menuItemZpet = new System.Windows.Forms.MenuItem();
            this.dgZbozi = new Fask.Graphic.DataGrid2();
            ((System.ComponentModel.ISupportInitialize)(this.dgZbozi)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemHledatCK);
            this.menuItem1.MenuItems.Add(this.menuItemVybrat);
            this.menuItem1.MenuItems.Add(this.menuItemZpet);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemHledatCK
            // 
            resources.ApplyResources(this.menuItemHledatCK, "menuItemHledatCK");
            this.menuItemHledatCK.Click += new System.EventHandler(this.menuItemHledatCK_Click);
            // 
            // menuItemVybrat
            // 
            resources.ApplyResources(this.menuItemVybrat, "menuItemVybrat");
            this.menuItemVybrat.Click += new System.EventHandler(this.menuItemVybrat_Click);
            // 
            // menuItemZpet
            // 
            resources.ApplyResources(this.menuItemZpet, "menuItemZpet");
            this.menuItemZpet.Click += new System.EventHandler(this.menuItemZpet_Click);
            // 
            // dgZbozi
            // 
            this.dgZbozi.BackColorAlternating = System.Drawing.Color.Gold;
            this.dgZbozi.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgZbozi.CurrentRow = null;
            resources.ApplyResources(this.dgZbozi, "dgZbozi");
            this.dgZbozi.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dgZbozi.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dgZbozi.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dgZbozi.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dgZbozi.MultiSelect = false;
            this.dgZbozi.Name = "dgZbozi";
            this.dgZbozi.NumberFormat = "N";
            this.dgZbozi.RowHeightDefault = 23;
            this.dgZbozi.Sort = "";
            this.dgZbozi.SortByHeaderDoubleClick = true;
            // 
            // ListZbozi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dgZbozi);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "ListZbozi";
            this.Load += new System.EventHandler(this.ListZbozi_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ListZbozi_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListZbozi_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgZbozi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.DataGrid2 dgZbozi;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemVybrat;
        private System.Windows.Forms.MenuItem menuItemZpet;
        private System.Windows.Forms.MenuItem menuItemHledatCK;
    }
}