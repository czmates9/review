namespace Fask.MST_W.Prodej_3
{
    partial class ProdejVyberPalety
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProdejVyberPalety));
            this.zpet_but = new System.Windows.Forms.Button();
            this.ok_but = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tOznaceni = new System.Windows.Forms.TextBox();
            this.cbTyp = new System.Windows.Forms.ComboBox();
            this.typyPalet = new Fask.MST_W.Schema.TypyPalet();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemAktualize = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.typyPalet)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // zpet_but
            // 
            resources.ApplyResources(this.zpet_but, "zpet_but");
            this.zpet_but.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.zpet_but.Name = "zpet_but";
            this.zpet_but.TabStop = false;
            this.zpet_but.Click += new System.EventHandler(this.zpet_but_Click);
            // 
            // ok_but
            // 
            resources.ApplyResources(this.ok_but, "ok_but");
            this.ok_but.Name = "ok_but";
            this.ok_but.TabStop = false;
            this.ok_but.Click += new System.EventHandler(this.ok_but_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tOznaceni
            // 
            resources.ApplyResources(this.tOznaceni, "tOznaceni");
            this.tOznaceni.Name = "tOznaceni";
            // 
            // cbTyp
            // 
            resources.ApplyResources(this.cbTyp, "cbTyp");
            this.cbTyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cbTyp.Name = "cbTyp";
            this.cbTyp.SelectedIndexChanged += new System.EventHandler(this.cbTyp_SelectedIndexChanged);
            // 
            // typyPalet
            // 
            this.typyPalet.DataSetName = "TypyPalet";
            this.typyPalet.EnforceConstraints = false;
            this.typyPalet.Locale = new System.Globalization.CultureInfo("");
            this.typyPalet.Prefix = "";
            this.typyPalet.SchemaSerializationMode = System.Data.SchemaSerializationMode.ExcludeSchema;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemAktualize);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemAktualize
            // 
            resources.ApplyResources(this.menuItemAktualize, "menuItemAktualize");
            this.menuItemAktualize.Click += new System.EventHandler(this.menuItemAktualize_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbTyp);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tOznaceni);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // ProdejVyberPalety
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.zpet_but);
            this.Controls.Add(this.ok_but);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ProdejVyberPalety";
            this.Load += new System.EventHandler(this.ProdejVyberPalety_Load);
            this.Activated += new System.EventHandler(this.ProdejVyberPalety_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProdejVyberPalety_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.typyPalet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button zpet_but;
        private System.Windows.Forms.Button ok_but;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tOznaceni;
        private Fask.MST_W.Schema.TypyPalet typyPalet;
        private System.Windows.Forms.ComboBox cbTyp;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemAktualize;
        private System.Windows.Forms.Panel panel1;
    }
}