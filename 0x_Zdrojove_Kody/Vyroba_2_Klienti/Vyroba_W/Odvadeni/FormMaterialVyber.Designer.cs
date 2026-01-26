namespace Fask.Vyroba_W.Odvadeni
{
    partial class FormMaterialVyber
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
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.fASKCONS095BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dGTBColumn_ITEMDESC = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dGTBColumn_ITEMNMBR = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dGTBColumn_ITEMCODE = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dGTBColumn_MJ = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dGTBColumn_QTY = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dGTBColumn_VNDITNUM = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dGTBColumn_CZ_CarKod = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dGTBColumn_SKL_ID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dGTBColumn_LOCNCODE = new System.Windows.Forms.DataGridTextBoxColumn();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItemAkce = new System.Windows.Forms.MenuItem();
            this.menuItemAkceStorno = new System.Windows.Forms.MenuItem();
            this.menuItemAkceOK = new System.Windows.Forms.MenuItem();
            this.panelButtons.SuspendLayout();
            this.panelComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fASKCONS095BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 254);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(238, 41);
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(120, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(118, 41);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonStorno
            // 
            this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.buttonStorno.Location = new System.Drawing.Point(0, 0);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(120, 41);
            this.buttonStorno.TabIndex = 0;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // panelComponents
            // 
            this.panelComponents.Controls.Add(this.dataGrid1);
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 0);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(238, 254);
            // 
            // fASKCONS095BindingSource
            // 
            this.fASKCONS095BindingSource.AllowNew = false;
            this.fASKCONS095BindingSource.Sort = "";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.DarkGreen;
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.DataSource = this.fASKCONS095BindingSource;
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dataGrid1.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dataGrid1.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dataGrid1.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.MultiSelect = false;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.NumberFormat = "N";
            this.dataGrid1.RowHeightDefault = 23;
            this.dataGrid1.Size = new System.Drawing.Size(238, 254);
            this.dataGrid1.Sort = "";
            this.dataGrid1.SortByHeaderDoubleClick = true;
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_ITEMDESC);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_ITEMNMBR);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_ITEMCODE);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_MJ);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_QTY);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_VNDITNUM);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_CZ_CarKod);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_SKL_ID);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dGTBColumn_LOCNCODE);
            this.dataGridTableStyle1.MappingName = "FASK_CONS_095";
            // 
            // dGTBColumn_ITEMDESC
            // 
            this.dGTBColumn_ITEMDESC.Format = "";
            this.dGTBColumn_ITEMDESC.FormatInfo = null;
            this.dGTBColumn_ITEMDESC.HeaderText = "Název";
            this.dGTBColumn_ITEMDESC.MappingName = "ITEMDESC";
            // 
            // dGTBColumn_ITEMNMBR
            // 
            this.dGTBColumn_ITEMNMBR.Format = "";
            this.dGTBColumn_ITEMNMBR.FormatInfo = null;
            this.dGTBColumn_ITEMNMBR.HeaderText = "Pol.è.";
            this.dGTBColumn_ITEMNMBR.MappingName = "ITEMNMBR";
            // 
            // dGTBColumn_ITEMCODE
            // 
            this.dGTBColumn_ITEMCODE.Format = "";
            this.dGTBColumn_ITEMCODE.FormatInfo = null;
            this.dGTBColumn_ITEMCODE.HeaderText = "Ozn.";
            this.dGTBColumn_ITEMCODE.MappingName = "ITEMCODE";
            // 
            // dGTBColumn_MJ
            // 
            this.dGTBColumn_MJ.Format = "";
            this.dGTBColumn_MJ.FormatInfo = null;
            this.dGTBColumn_MJ.HeaderText = "MJ";
            this.dGTBColumn_MJ.MappingName = "MJ";
            // 
            // dGTBColumn_QTY
            // 
            this.dGTBColumn_QTY.Format = "";
            this.dGTBColumn_QTY.FormatInfo = null;
            this.dGTBColumn_QTY.HeaderText = "Množství";
            this.dGTBColumn_QTY.MappingName = "QTY";
            // 
            // dGTBColumn_VNDITNUM
            // 
            this.dGTBColumn_VNDITNUM.Format = "";
            this.dGTBColumn_VNDITNUM.FormatInfo = null;
            this.dGTBColumn_VNDITNUM.HeaderText = "Èár. kód";
            this.dGTBColumn_VNDITNUM.MappingName = "VNDITNUM";
            // 
            // dGTBColumn_CZ_CarKod
            // 
            this.dGTBColumn_CZ_CarKod.Format = "";
            this.dGTBColumn_CZ_CarKod.FormatInfo = null;
            this.dGTBColumn_CZ_CarKod.HeaderText = "Èár. kód. 2";
            this.dGTBColumn_CZ_CarKod.MappingName = "CZ_CarKod";
            // 
            // dGTBColumn_SKL_ID
            // 
            this.dGTBColumn_SKL_ID.Format = "";
            this.dGTBColumn_SKL_ID.FormatInfo = null;
            this.dGTBColumn_SKL_ID.HeaderText = "Sklad ID";
            this.dGTBColumn_SKL_ID.MappingName = "SKL_ID";
            // 
            // dGTBColumn_LOCNCODE
            // 
            this.dGTBColumn_LOCNCODE.Format = "";
            this.dGTBColumn_LOCNCODE.FormatInfo = null;
            this.dGTBColumn_LOCNCODE.HeaderText = "Lokace";
            this.dGTBColumn_LOCNCODE.MappingName = "LOCNCODE";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemAkce);
            // 
            // menuItemAkce
            // 
            this.menuItemAkce.MenuItems.Add(this.menuItemAkceStorno);
            this.menuItemAkce.MenuItems.Add(this.menuItemAkceOK);
            this.menuItemAkce.Text = "Akce";
            // 
            // menuItemAkceStorno
            // 
            this.menuItemAkceStorno.Text = "Storno";
            this.menuItemAkceStorno.Click += new System.EventHandler(this.menuItemAkceStorno_Click);
            // 
            // menuItemAkceOK
            // 
            this.menuItemAkceOK.Text = "OK";
            this.menuItemAkceOK.Click += new System.EventHandler(this.menuItemAkceOK_Click);
            // 
            // FormMaterialVyber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FormMaterialVyber";
            this.Text = "Materiál-Výbìr";
            this.Load += new System.EventHandler(this.FormMaterialVyber_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMaterialVyber_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelComponents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fASKCONS095BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.Button buttonStorno;
        public System.Windows.Forms.Panel panelComponents;
        public System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItemAkce;
        private System.Windows.Forms.MenuItem menuItemAkceStorno;
        private System.Windows.Forms.MenuItem menuItemAkceOK;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.BindingSource fASKCONS095BindingSource;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_ITEMDESC;
        private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_ITEMNMBR;
        private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_ITEMCODE;
        private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_MJ;
        private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_QTY;
        private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_VNDITNUM;
        private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_CZ_CarKod;
        private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_SKL_ID;
        private System.Windows.Forms.DataGridTextBoxColumn dGTBColumn_LOCNCODE;

    }
}
