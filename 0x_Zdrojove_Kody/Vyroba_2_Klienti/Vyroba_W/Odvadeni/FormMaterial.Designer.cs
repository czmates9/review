namespace Fask.Vyroba_W.Odvadeni
{
    partial class FormMaterial
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
			this.panelComponents = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.productionSourcesBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dataGrid1 = new Fask.Graphic.DataGrid2();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.panelbutton = new System.Windows.Forms.Panel();
			this.buttonStorno = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItemAkce = new System.Windows.Forms.MenuItem();
			this.menuItemHledat = new System.Windows.Forms.MenuItem();
			this.menuItemHledatCarkod = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemAkceSmazat = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemAkceOK = new System.Windows.Forms.MenuItem();
			this.menuItemStorno = new System.Windows.Forms.MenuItem();
			this.panelComponents.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.productionSourcesBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.panelbutton.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelComponents
			// 
			this.panelComponents.Controls.Add(this.panel1);
			this.panelComponents.Controls.Add(this.panelbutton);
			this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelComponents.Location = new System.Drawing.Point(0, 0);
			this.panelComponents.Name = "panelComponents";
			this.panelComponents.Size = new System.Drawing.Size(238, 295);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.dataGrid1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(238, 237);
			// 
			// productionSourcesBindingSource
			// 
			this.productionSourcesBindingSource.AllowNew = false;
			this.productionSourcesBindingSource.Sort = "";
			// 
			// dataGrid1
			// 
			this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
			this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.dataGrid1.CurrentRow = null;
			this.dataGrid1.DataSource = this.productionSourcesBindingSource;
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
			this.dataGrid1.Size = new System.Drawing.Size(238, 237);
			this.dataGrid1.Sort = "";
			this.dataGrid1.SortByHeaderDoubleClick = true;
			this.dataGrid1.TabIndex = 0;
			this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
			this.dataGridTableStyle1.MappingName = "Production_Sources";
			// 
			// dataGridTextBoxColumn1
			// 
			this.dataGridTextBoxColumn1.Format = "";
			this.dataGridTextBoxColumn1.FormatInfo = null;
			this.dataGridTextBoxColumn1.HeaderText = "Název";
			this.dataGridTextBoxColumn1.MappingName = "ITEMNAME";
			// 
			// dataGridTextBoxColumn2
			// 
			this.dataGridTextBoxColumn2.Format = "";
			this.dataGridTextBoxColumn2.FormatInfo = null;
			this.dataGridTextBoxColumn2.HeaderText = "Pol.è.";
			this.dataGridTextBoxColumn2.MappingName = "ITEMNMBR";
			// 
			// dataGridTextBoxColumn3
			// 
			this.dataGridTextBoxColumn3.Format = "";
			this.dataGridTextBoxColumn3.FormatInfo = null;
			this.dataGridTextBoxColumn3.HeaderText = "Ozn.";
			this.dataGridTextBoxColumn3.MappingName = "ITEMCODE";
			// 
			// dataGridTextBoxColumn4
			// 
			this.dataGridTextBoxColumn4.Format = "";
			this.dataGridTextBoxColumn4.FormatInfo = null;
			this.dataGridTextBoxColumn4.HeaderText = "MJ";
			this.dataGridTextBoxColumn4.MappingName = "MJ";
			// 
			// dataGridTextBoxColumn5
			// 
			this.dataGridTextBoxColumn5.Format = "";
			this.dataGridTextBoxColumn5.FormatInfo = null;
			this.dataGridTextBoxColumn5.HeaderText = "Množství";
			this.dataGridTextBoxColumn5.MappingName = "QTYSHPPD";
			// 
			// panelbutton
			// 
			this.panelbutton.Controls.Add(this.buttonStorno);
			this.panelbutton.Controls.Add(this.buttonOK);
			this.panelbutton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelbutton.Location = new System.Drawing.Point(0, 237);
			this.panelbutton.Name = "panelbutton";
			this.panelbutton.Size = new System.Drawing.Size(238, 58);
			this.panelbutton.Resize += new System.EventHandler(this.panelbutton_Resize);
			// 
			// buttonStorno
			// 
			this.buttonStorno.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonStorno.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.buttonStorno.Location = new System.Drawing.Point(0, 0);
			this.buttonStorno.Name = "buttonStorno";
			this.buttonStorno.Size = new System.Drawing.Size(119, 58);
			this.buttonStorno.TabIndex = 1;
			this.buttonStorno.Text = "Storno";
			this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Dock = System.Windows.Forms.DockStyle.Right;
			this.buttonOK.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.buttonOK.Location = new System.Drawing.Point(119, 0);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(119, 58);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItemAkce);
			// 
			// menuItemAkce
			// 
			this.menuItemAkce.MenuItems.Add(this.menuItemHledat);
			this.menuItemAkce.MenuItems.Add(this.menuItem2);
			this.menuItemAkce.MenuItems.Add(this.menuItemAkceSmazat);
			this.menuItemAkce.MenuItems.Add(this.menuItem1);
			this.menuItemAkce.MenuItems.Add(this.menuItemAkceOK);
			this.menuItemAkce.MenuItems.Add(this.menuItemStorno);
			this.menuItemAkce.Text = "Akce";
			// 
			// menuItemHledat
			// 
			this.menuItemHledat.MenuItems.Add(this.menuItemHledatCarkod);
			this.menuItemHledat.Text = "Hledat";
			// 
			// menuItemHledatCarkod
			// 
			this.menuItemHledatCarkod.Text = "Èár. kód";
			this.menuItemHledatCarkod.Click += new System.EventHandler(this.menuItemHledatCarkod_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Text = "-";
			// 
			// menuItemAkceSmazat
			// 
			this.menuItemAkceSmazat.Text = "Smazat";
			this.menuItemAkceSmazat.Click += new System.EventHandler(this.menuItemAkceSmazat_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Text = "-";
			// 
			// menuItemAkceOK
			// 
			this.menuItemAkceOK.Text = "OK";
			this.menuItemAkceOK.Click += new System.EventHandler(this.menuItemAkceOK_Click);
			// 
			// menuItemStorno
			// 
			this.menuItemStorno.Text = "Storno";
			this.menuItemStorno.Click += new System.EventHandler(this.menuItemStorno_Click);
			// 
			// FormMaterial
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(238, 295);
			this.ControlBox = false;
			this.Controls.Add(this.panelComponents);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Name = "FormMaterial";
			this.Text = "Materiál ";
			this.Load += new System.EventHandler(this.FormMaterial_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMaterial_KeyDown);
			this.panelComponents.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.productionSourcesBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.panelbutton.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelComponents;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.BindingSource productionSourcesBindingSource;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItemAkce;
        private System.Windows.Forms.MenuItem menuItemAkceOK;
        private System.Windows.Forms.MenuItem menuItemAkceSmazat;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemHledat;
        private System.Windows.Forms.MenuItem menuItemHledatCarkod;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Panel panelbutton;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.MenuItem menuItemStorno;
        private System.Windows.Forms.Panel panel1;

    }
}
