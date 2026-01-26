namespace Fask.Events
{
    partial class FormUdalosti
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
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new Fask.Graphic.GraphicButton();
            this.buttonStorno = new Fask.Graphic.GraphicButton();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemAktualize = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemOK = new System.Windows.Forms.MenuItem();
            this.menuItemStorno = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelComponents.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dataGrid1.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dataGrid1.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dataGrid1.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dataGrid1.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.MultiSelect = false;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.NumberFormat = "N";
            this.dataGrid1.RowHeightDefault = 23;
            this.dataGrid1.Size = new System.Drawing.Size(283, 215);
            this.dataGrid1.Sort = "";
            this.dataGrid1.SortByHeaderDoubleClick = true;
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.CurrentRowIndexChanged += new System.EventHandler(this.dataGrid1_CurrentRowIndexChanged);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 215);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(283, 71);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.BitmapNormal = null;
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.FocusMargin = 5;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Pressed = false;
            this.buttonOK.Size = new System.Drawing.Size(163, 71);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Transparent = System.Drawing.Color.White;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.BitmapNormal = null;
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.FocusMargin = 5;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Pressed = false;
            this.buttonStorno.Size = new System.Drawing.Size(120, 71);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Transparent = System.Drawing.Color.White;
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.dataGrid1);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(283, 215);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemAktualize);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.menuItemOK);
            this.menuItem1.MenuItems.Add(this.menuItemStorno);
            this.menuItem1.Text = "Menu";
            // 
            // menuItemAktualize
            // 
            this.menuItemAktualize.Text = "Aktualizovat";
            this.menuItemAktualize.Click += new System.EventHandler(this.menuItemAktualize_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // menuItemOK
            // 
            this.menuItemOK.Text = "OK";
            this.menuItemOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // menuItemStorno
            // 
            this.menuItemStorno.Text = "Storno";
            this.menuItemStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // FormUdalosti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(283, 286);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "FormUdalosti";
            this.Text = "Události obsluhy";
            this.Load += new System.EventHandler(this.FormUdalosti_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormUdalosti_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelComponents.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.DataGrid2 dataGrid1;
        public System.Windows.Forms.Panel panelButtons;
        public Fask.Graphic.GraphicButton buttonOK;
        public Fask.Graphic.GraphicButton buttonStorno;
        public System.Windows.Forms.Panel panelComponents;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemAktualize;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemOK;
        private System.Windows.Forms.MenuItem menuItemStorno;

    }
}
