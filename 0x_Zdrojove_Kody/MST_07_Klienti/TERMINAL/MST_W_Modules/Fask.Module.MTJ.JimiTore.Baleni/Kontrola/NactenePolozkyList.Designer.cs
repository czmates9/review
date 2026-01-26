namespace Fask.Module.MTJ.JimiTore.Baleni.Kontrola
{
    partial class NactenePolozkyList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NactenePolozkyList));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miAktualizovat = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miPrerusit = new System.Windows.Forms.MenuItem();
            this.bsKontrola = new System.Windows.Forms.BindingSource(this.components);
            this.dsKontrola = new Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.zpet_but = new Fask.Graphic.GraphicButton();
            this.sbinfo = new System.Windows.Forms.StatusBar();
            this.timerLoad = new System.Windows.Forms.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.bsKontrola)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsKontrola)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.miAktualizovat);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.miPrerusit);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // miAktualizovat
            // 
            resources.ApplyResources(this.miAktualizovat, "miAktualizovat");
            this.miAktualizovat.Click += new System.EventHandler(this.miAktualizovat_Click);
            // 
            // menuItem3
            // 
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // miPrerusit
            // 
            resources.ApplyResources(this.miPrerusit, "miPrerusit");
            this.miPrerusit.Click += new System.EventHandler(this.miKonec_Click);
            // 
            // bsKontrola
            // 
            this.bsKontrola.DataMember = "X425_CTRL";
            this.bsKontrola.DataSource = this.dsKontrola;
            this.bsKontrola.Sort = "";
            // 
            // dsKontrola
            // 
            this.dsKontrola.DataSetName = "VydejJimiTore";
            this.dsKontrola.Prefix = "";
            this.dsKontrola.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.DataSource = this.bsKontrola;
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
            // sbinfo
            // 
            resources.ApplyResources(this.sbinfo, "sbinfo");
            this.sbinfo.Name = "sbinfo";
            // 
            // timerLoad
            // 
            this.timerLoad.Interval = 150;
            this.timerLoad.Tick += new System.EventHandler(this.timerLoad_Tick);
            // 
            // NactenePolozkyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.sbinfo);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "NactenePolozkyList";
            this.Load += new System.EventHandler(this.ServisDynamickaTabulka_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServisDynamickaTabulka_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.bsKontrola)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsKontrola)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miPrerusit;
        private Fask.Graphic.DataGrid2 dataGrid1;
        protected System.Windows.Forms.Panel panelButtons;
        private Fask.Graphic.GraphicButton zpet_but;
        private System.Windows.Forms.StatusBar sbinfo;
        private System.Windows.Forms.BindingSource bsKontrola;
        private Fask.Module.MTJ.JimiTore.Baleni.WebServiceBaleni.VydejJimiTore dsKontrola;
        private System.Windows.Forms.MenuItem miAktualizovat;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.Timer timerLoad;
    }
}