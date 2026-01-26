namespace Fask.MST_W.Forms
{
    partial class FormPodbarveniPriorit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPodbarveniPriorit));
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new Fask.Graphic.GraphicButton();
            this.buttonStorno = new Fask.Graphic.GraphicButton();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemPridat = new System.Windows.Forms.MenuItem();
            this.menuItemSmazat = new System.Windows.Forms.MenuItem();
            this.priorityColorsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.priorityColors = new Fask.MST_W.Config.PriorityColors();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.panelNastaveni = new System.Windows.Forms.Panel();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priorityColorsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.priorityColors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.panelNastaveni.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
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
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemPridat);
            this.menuItem1.MenuItems.Add(this.menuItemSmazat);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemPridat
            // 
            resources.ApplyResources(this.menuItemPridat, "menuItemPridat");
            this.menuItemPridat.Click += new System.EventHandler(this.menuItemPridat_Click);
            // 
            // menuItemSmazat
            // 
            resources.ApplyResources(this.menuItemSmazat, "menuItemSmazat");
            this.menuItemSmazat.Click += new System.EventHandler(this.menuItemSmazat_Click);
            // 
            // priorityColorsBindingSource
            // 
            this.priorityColorsBindingSource.DataMember = "PriorityColor";
            this.priorityColorsBindingSource.DataSource = this.priorityColors;
            this.priorityColorsBindingSource.Sort = "";
            // 
            // priorityColors
            // 
            this.priorityColors.DataSetName = "PriorityColors";
            this.priorityColors.Locale = new System.Globalization.CultureInfo("");
            this.priorityColors.Prefix = "";
            this.priorityColors.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.DataSource = this.priorityColorsBindingSource;
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
            this.dataGrid1.DoubleClick += new System.EventHandler(this.dataGrid1_DoubleClick);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // panelNastaveni
            // 
            this.panelNastaveni.Controls.Add(this.dataGrid1);
            resources.ApplyResources(this.panelNastaveni, "panelNastaveni");
            this.panelNastaveni.Name = "panelNastaveni";
            // 
            // FormPodbarveniPriorit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panelNastaveni);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "FormPodbarveniPriorit";
            this.Deactivate += new System.EventHandler(this.FormPodbarveniPriorit_Deactivate);
            this.Load += new System.EventHandler(this.FormPodbarveniPriorit_Load);
            this.Activated += new System.EventHandler(this.FormPodbarveniPriorit_Activated);
            this.Resize += new System.EventHandler(this.FormPodbarveniPriorit_Resize);
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.priorityColorsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.priorityColors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.panelNastaveni.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton buttonOK;
        private Fask.Graphic.GraphicButton buttonStorno;
        private System.Windows.Forms.BindingSource priorityColorsBindingSource;
        private Fask.MST_W.Config.PriorityColors priorityColors;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemPridat;
        private System.Windows.Forms.MenuItem menuItemSmazat;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.Panel panelNastaveni;
    }
}