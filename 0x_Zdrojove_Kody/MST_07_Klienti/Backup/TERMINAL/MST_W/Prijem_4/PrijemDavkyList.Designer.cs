namespace Fask.MST_W.Prijem_4
{
    partial class PrijemDavkyList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrijemDavkyList));
            this.buttonOK = new Fask.Graphic.GraphicButton();
            this.buttonStorno = new Fask.Graphic.GraphicButton();
            this.hlavickyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgI1 = new Fask.Graphic.DataGrid2();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumnDavka = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnSOPNUMBE = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumnCntItems = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.dataGridTextBoxColumnSumItems = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.dataGridTextBoxColumnSKLID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemObjednavkaDetail = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemUzavrit = new System.Windows.Forms.MenuItem();
            this.menuItemNezrealizovanePrij = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.hlavickyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgI1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            // hlavickyBindingSource
            // 
            this.hlavickyBindingSource.DataMember = "Hlavicky";
            this.hlavickyBindingSource.DataSource = typeof(Fask.MST_W.PrijemService.PrijemDavky);
            this.hlavickyBindingSource.Sort = "";
            // 
            // dgI1
            // 
            this.dgI1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dgI1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgI1.CurrentRow = null;
            this.dgI1.DataSource = this.hlavickyBindingSource;
            resources.ApplyResources(this.dgI1, "dgI1");
            this.dgI1.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dgI1.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dgI1.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dgI1.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dgI1.MultiSelect = false;
            this.dgI1.Name = "dgI1";
            this.dgI1.NumberFormat = "N";
            this.dgI1.RowHeightDefault = 23;
            this.dgI1.Sort = "";
            this.dgI1.SortByHeaderDoubleClick = true;
            this.dgI1.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnDavka);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnSOPNUMBE);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnCntItems);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnSumItems);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnSKLID);
            this.dataGridTableStyle1.MappingName = "Hlavicky";
            // 
            // dataGridTextBoxColumnDavka
            // 
            this.dataGridTextBoxColumnDavka.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnDavka.Format = "";
            this.dataGridTextBoxColumnDavka.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnDavka, "dataGridTextBoxColumnDavka");
            this.dataGridTextBoxColumnDavka.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnDavka.SelectionShow = false;
            this.dataGridTextBoxColumnDavka.Tag = "";
            // 
            // dataGridTextBoxColumnSOPNUMBE
            // 
            this.dataGridTextBoxColumnSOPNUMBE.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnSOPNUMBE.Format = "";
            this.dataGridTextBoxColumnSOPNUMBE.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnSOPNUMBE, "dataGridTextBoxColumnSOPNUMBE");
            this.dataGridTextBoxColumnSOPNUMBE.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnSOPNUMBE.SelectionShow = false;
            this.dataGridTextBoxColumnSOPNUMBE.Tag = "";
            // 
            // dataGridTextBoxColumnCntItems
            // 
            this.dataGridTextBoxColumnCntItems.Alignment = System.Drawing.StringAlignment.Far;
            this.dataGridTextBoxColumnCntItems.Format = "N";
            this.dataGridTextBoxColumnCntItems.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnCntItems, "dataGridTextBoxColumnCntItems");
            this.dataGridTextBoxColumnCntItems.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnCntItems.SelectionShow = false;
            this.dataGridTextBoxColumnCntItems.Tag = "";
            // 
            // dataGridTextBoxColumnSumItems
            // 
            this.dataGridTextBoxColumnSumItems.Alignment = System.Drawing.StringAlignment.Far;
            this.dataGridTextBoxColumnSumItems.Format = "N";
            this.dataGridTextBoxColumnSumItems.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnSumItems, "dataGridTextBoxColumnSumItems");
            this.dataGridTextBoxColumnSumItems.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnSumItems.SelectionShow = false;
            this.dataGridTextBoxColumnSumItems.Tag = "";
            // 
            // dataGridTextBoxColumnSKLID
            // 
            this.dataGridTextBoxColumnSKLID.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnSKLID.Format = "";
            this.dataGridTextBoxColumnSKLID.FormatInfo = null;
            resources.ApplyResources(this.dataGridTextBoxColumnSKLID, "dataGridTextBoxColumnSKLID");
            this.dataGridTextBoxColumnSKLID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumnSKLID.SelectionShow = false;
            this.dataGridTextBoxColumnSKLID.Tag = "";
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
            this.menuItem1.MenuItems.Add(this.menuItemObjednavkaDetail);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItemUzavrit);
            this.menuItem1.MenuItems.Add(this.menuItemNezrealizovanePrij);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemObjednavkaDetail
            // 
            resources.ApplyResources(this.menuItemObjednavkaDetail, "menuItemObjednavkaDetail");
            this.menuItemObjednavkaDetail.Click += new System.EventHandler(this.menuItemObjednavkaDetail_Click);
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
            // menuItemUzavrit
            // 
            resources.ApplyResources(this.menuItemUzavrit, "menuItemUzavrit");
            this.menuItemUzavrit.Click += new System.EventHandler(this.menuItemUzavrit_Click);
            // 
            // menuItemNezrealizovanePrij
            // 
            resources.ApplyResources(this.menuItemNezrealizovanePrij, "menuItemNezrealizovanePrij");
            this.menuItemNezrealizovanePrij.Click += new System.EventHandler(this.menuItemNezrealizovanePrij_Click);
            // 
            // PrijemDavkyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dgI1);
            this.Controls.Add(this.panel1);
            this.Menu = this.mainMenu1;
            this.Name = "PrijemDavkyList";
            this.Deactivate += new System.EventHandler(this.PrijemDavkyList_Deactivate);
            this.Load += new System.EventHandler(this.PrijemDavkyList_Load);
            this.Activated += new System.EventHandler(this.PrijemDavkyList_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrijemDavkyList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.hlavickyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgI1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.GraphicButton buttonOK;
        private Fask.Graphic.GraphicButton buttonStorno;
        private Fask.Graphic.DataGrid2 dgI1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnDavka;
        private System.Windows.Forms.BindingSource hlavickyBindingSource;
        private System.Windows.Forms.Panel panel1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnSOPNUMBE;
        private Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumnCntItems;
        private Fask.Graphic.DataGrid2NumberBoxColumn dataGridTextBoxColumnSumItems;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemObjednavkaDetail;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItemUzavrit;
        private System.Windows.Forms.MenuItem menuItemNezrealizovanePrij;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumnSKLID;
    }
}