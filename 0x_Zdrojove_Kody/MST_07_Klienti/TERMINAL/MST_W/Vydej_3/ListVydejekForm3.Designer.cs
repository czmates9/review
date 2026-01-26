namespace Fask.MST_W.Vydej_3
{
    partial class ListVydejekForm3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListVydejekForm3));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonStorno = new Fask.Graphic.GraphicButton();
            this.buttonStahnout = new Fask.Graphic.GraphicButton();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miRazeniOrig = new System.Windows.Forms.MenuItem();
            this.miRazeniDavka = new System.Windows.Forms.MenuItem();
            this.miRazeniObjednavka = new System.Windows.Forms.MenuItem();
            this.miRazeniPolozek = new System.Windows.Forms.MenuItem();
            this.miRazeniPozekCelkem = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miStornovatVydejku = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonStorno);
            this.panel1.Controls.Add(this.buttonStahnout);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
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
            // buttonStahnout
            // 
            this.buttonStahnout.BitmapNormal = null;
            resources.ApplyResources(this.buttonStahnout, "buttonStahnout");
            this.buttonStahnout.FocusMargin = 5;
            this.buttonStahnout.Name = "buttonStahnout";
            this.buttonStahnout.Pressed = false;
            this.buttonStahnout.Transparent = System.Drawing.Color.White;
            this.buttonStahnout.Click += new System.EventHandler(this.buttonStahnout_Click);
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
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem5);
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem4);
            // 
            // menuItem5
            // 
            this.menuItem5.MenuItems.Add(this.miRazeniOrig);
            this.menuItem5.MenuItems.Add(this.miRazeniDavka);
            this.menuItem5.MenuItems.Add(this.miRazeniObjednavka);
            this.menuItem5.MenuItems.Add(this.miRazeniPolozek);
            this.menuItem5.MenuItems.Add(this.miRazeniPozekCelkem);
            this.menuItem5.MenuItems.Add(this.menuItem7);
            resources.ApplyResources(this.menuItem5, "menuItem5");
            // 
            // miRazeniOrig
            // 
            resources.ApplyResources(this.miRazeniOrig, "miRazeniOrig");
            this.miRazeniOrig.Click += new System.EventHandler(this.miRazeniOrig_Click);
            // 
            // miRazeniDavka
            // 
            resources.ApplyResources(this.miRazeniDavka, "miRazeniDavka");
            this.miRazeniDavka.Click += new System.EventHandler(this.miRazeniDavka_Click);
            // 
            // miRazeniObjednavka
            // 
            resources.ApplyResources(this.miRazeniObjednavka, "miRazeniObjednavka");
            this.miRazeniObjednavka.Click += new System.EventHandler(this.miRazeniObjednavka_Click);
            // 
            // miRazeniPolozek
            // 
            resources.ApplyResources(this.miRazeniPolozek, "miRazeniPolozek");
            this.miRazeniPolozek.Click += new System.EventHandler(this.miRazeniPolozek_Click);
            // 
            // miRazeniPozekCelkem
            // 
            resources.ApplyResources(this.miRazeniPozekCelkem, "miRazeniPozekCelkem");
            this.miRazeniPozekCelkem.Click += new System.EventHandler(this.miRazeniPozekCelkem_Click);
            // 
            // menuItem7
            // 
            resources.ApplyResources(this.menuItem7, "menuItem7");
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.miStornovatVydejku);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItem2
            // 
            resources.ApplyResources(this.menuItem2, "menuItem2");
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            resources.ApplyResources(this.menuItem3, "menuItem3");
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // miStornovatVydejku
            // 
            resources.ApplyResources(this.miStornovatVydejku, "miStornovatVydejku");
            this.miStornovatVydejku.Click += new System.EventHandler(this.miStornovatVydejku_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.MenuItems.Add(this.menuItem6);
            resources.ApplyResources(this.menuItem4, "menuItem4");
            // 
            // menuItem6
            // 
            resources.ApplyResources(this.menuItem6, "menuItem6");
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // ListVydejekForm3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ListVydejekForm3";
            this.Deactivate += new System.EventHandler(this.ListVydejekForm3_Deactivate);
            this.Load += new System.EventHandler(this.ListVydejekForm3_Load);
            this.Activated += new System.EventHandler(this.ListVydejekForm3_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListVydejekForm2_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Fask.Graphic.GraphicButton buttonStorno;
        private Fask.Graphic.GraphicButton buttonStahnout;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem miRazeniOrig;
        private System.Windows.Forms.MenuItem miRazeniDavka;
        private System.Windows.Forms.MenuItem miRazeniObjednavka;
        private System.Windows.Forms.MenuItem miRazeniPolozek;
        private System.Windows.Forms.MenuItem miRazeniPozekCelkem;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem miStornovatVydejku;
    }
}