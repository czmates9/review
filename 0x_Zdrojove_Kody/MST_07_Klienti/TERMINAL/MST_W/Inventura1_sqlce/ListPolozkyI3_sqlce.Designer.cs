namespace Fask.MST_W.Inventura1_sqlce
{
    partial class ListPolozkyI3_sqlce
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListPolozkyI3_sqlce));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemVyber = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemStorno = new System.Windows.Forms.MenuItem();
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dgDavka = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgItemnmbr = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgCZCarkod = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgQTYPACK = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgVNDITNUM = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgMJ = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgVENDNAME = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgVENDORID = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgItemdesc = new Fask.Graphic.DataGrid2TextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemVyber);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItemStorno);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemVyber
            // 
            resources.ApplyResources(this.menuItemVyber, "menuItemVyber");
            this.menuItemVyber.Click += new System.EventHandler(this.menuItemVyber_Click);
            // 
            // menuItem3
            // 
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // menuItemStorno
            // 
            resources.ApplyResources(this.menuItemStorno, "menuItemStorno");
            this.menuItemStorno.Click += new System.EventHandler(this.menuItemStorno_Click);
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
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgDavka);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgItemnmbr);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgCZCarkod);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgQTYPACK);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgVNDITNUM);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgMJ);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgVENDNAME);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgVENDORID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgItemdesc);
            this.dataGridTableStyle1.MappingName = "CZMST_I3";
            // 
            // dgDavka
            // 
            this.dgDavka.Alignment = System.Drawing.StringAlignment.Near;
            this.dgDavka.Format = "";
            this.dgDavka.FormatInfo = null;
            resources.ApplyResources(this.dgDavka, "dgDavka");
            this.dgDavka.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgDavka.SelectionShow = false;
            this.dgDavka.Tag = "";
            // 
            // dgItemnmbr
            // 
            this.dgItemnmbr.Alignment = System.Drawing.StringAlignment.Near;
            this.dgItemnmbr.Format = "";
            this.dgItemnmbr.FormatInfo = null;
            resources.ApplyResources(this.dgItemnmbr, "dgItemnmbr");
            this.dgItemnmbr.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgItemnmbr.SelectionShow = false;
            this.dgItemnmbr.Tag = "";
            // 
            // dgCZCarkod
            // 
            this.dgCZCarkod.Alignment = System.Drawing.StringAlignment.Near;
            this.dgCZCarkod.Format = "";
            this.dgCZCarkod.FormatInfo = null;
            resources.ApplyResources(this.dgCZCarkod, "dgCZCarkod");
            this.dgCZCarkod.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgCZCarkod.SelectionShow = false;
            this.dgCZCarkod.Tag = "";
            // 
            // dgQTYPACK
            // 
            this.dgQTYPACK.Alignment = System.Drawing.StringAlignment.Near;
            this.dgQTYPACK.Format = "";
            this.dgQTYPACK.FormatInfo = null;
            resources.ApplyResources(this.dgQTYPACK, "dgQTYPACK");
            this.dgQTYPACK.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgQTYPACK.SelectionShow = false;
            this.dgQTYPACK.Tag = "";
            // 
            // dgVNDITNUM
            // 
            this.dgVNDITNUM.Alignment = System.Drawing.StringAlignment.Near;
            this.dgVNDITNUM.Format = "";
            this.dgVNDITNUM.FormatInfo = null;
            resources.ApplyResources(this.dgVNDITNUM, "dgVNDITNUM");
            this.dgVNDITNUM.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgVNDITNUM.SelectionShow = false;
            this.dgVNDITNUM.Tag = "";
            // 
            // dgMJ
            // 
            this.dgMJ.Alignment = System.Drawing.StringAlignment.Near;
            this.dgMJ.Format = "";
            this.dgMJ.FormatInfo = null;
            resources.ApplyResources(this.dgMJ, "dgMJ");
            this.dgMJ.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgMJ.SelectionShow = false;
            this.dgMJ.Tag = "";
            // 
            // dgVENDNAME
            // 
            this.dgVENDNAME.Alignment = System.Drawing.StringAlignment.Near;
            this.dgVENDNAME.Format = "";
            this.dgVENDNAME.FormatInfo = null;
            resources.ApplyResources(this.dgVENDNAME, "dgVENDNAME");
            this.dgVENDNAME.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgVENDNAME.SelectionShow = false;
            this.dgVENDNAME.Tag = "";
            // 
            // dgVENDORID
            // 
            this.dgVENDORID.Alignment = System.Drawing.StringAlignment.Near;
            this.dgVENDORID.Format = "";
            this.dgVENDORID.FormatInfo = null;
            resources.ApplyResources(this.dgVENDORID, "dgVENDORID");
            this.dgVENDORID.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgVENDORID.SelectionShow = false;
            this.dgVENDORID.Tag = "";
            // 
            // dgItemdesc
            // 
            this.dgItemdesc.Alignment = System.Drawing.StringAlignment.Near;
            this.dgItemdesc.Format = "";
            this.dgItemdesc.FormatInfo = null;
            resources.ApplyResources(this.dgItemdesc, "dgItemdesc");
            this.dgItemdesc.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgItemdesc.SelectionShow = false;
            this.dgItemdesc.Tag = "";
            // 
            // ListPolozkyI3_sqlce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.dataGrid1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "ListPolozkyI3_sqlce";
            this.Load += new System.EventHandler(this.ListPolozkyI3_sqlce_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListPolozkyI3_sqlce_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemVyber;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItemStorno;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dgDavka;
        private Fask.Graphic.DataGrid2TextBoxColumn dgItemnmbr;
        private Fask.Graphic.DataGrid2TextBoxColumn dgCZCarkod;
        private Fask.Graphic.DataGrid2TextBoxColumn dgQTYPACK;
        private Fask.Graphic.DataGrid2TextBoxColumn dgVNDITNUM;
        private Fask.Graphic.DataGrid2TextBoxColumn dgMJ;
        private Fask.Graphic.DataGrid2TextBoxColumn dgVENDNAME;
        private Fask.Graphic.DataGrid2TextBoxColumn dgVENDORID;
        private Fask.Graphic.DataGrid2TextBoxColumn dgItemdesc;
    }
}