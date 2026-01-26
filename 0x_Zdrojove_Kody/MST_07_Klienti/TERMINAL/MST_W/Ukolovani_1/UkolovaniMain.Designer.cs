namespace Fask.MST_W.Ukolovani_1
{
    partial class UkolovaniMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu;

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemActualize = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemOpen = new System.Windows.Forms.MenuItem();
            this.menuItemKonec = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemZobrazeniVse = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItemZobrazeniNove = new System.Windows.Forms.MenuItem();
            this.menuItemZobrazeniAktivni = new System.Windows.Forms.MenuItem();
            this.menuItemZobrazeniDokoncene = new System.Windows.Forms.MenuItem();
            this.panelMain = new System.Windows.Forms.Panel();
            this.ukolyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgUkoly = new Fask.Graphic.DataGrid2();
            this.ukoly = new Fask.SQLiteDBs.DataSets.Ukoly();
            this.dataGridTableStyleUkoly = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn2 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn3 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn4 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn5 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn6 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn7 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn8 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn9 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn10 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn11 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn12 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dataGridTextBoxColumn13 = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ukolyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgUkoly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ukoly)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuItem1);
            this.mainMenu.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemActualize);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItemOpen);
            this.menuItem1.MenuItems.Add(this.menuItemKonec);
            this.menuItem1.Text = "Menu";
            // 
            // menuItemActualize
            // 
            this.menuItemActualize.Text = "1 - Aktualizace";
            this.menuItemActualize.Click += new System.EventHandler(this.menuItemActualize_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Enter - Otevřít";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Text = "-";
            // 
            // menuItemKonec
            // 
            this.menuItemKonec.Text = "Esc - Konec ";
            this.menuItemKonec.Click += new System.EventHandler(this.menuItemKonec_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.menuItemZobrazeniVse);
            this.menuItem2.MenuItems.Add(this.menuItem5);
            this.menuItem2.MenuItems.Add(this.menuItemZobrazeniNove);
            this.menuItem2.MenuItems.Add(this.menuItemZobrazeniAktivni);
            this.menuItem2.MenuItems.Add(this.menuItemZobrazeniDokoncene);
            this.menuItem2.Text = "Zobrazení";
            // 
            // menuItemZobrazeniVse
            // 
            this.menuItemZobrazeniVse.Text = "F1 - Vše";
            this.menuItemZobrazeniVse.Click += new System.EventHandler(this.menuItemZobrazeniVse_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "-";
            // 
            // menuItemZobrazeniNove
            // 
            this.menuItemZobrazeniNove.Text = "F2 - Nové";
            this.menuItemZobrazeniNove.Click += new System.EventHandler(this.menuItemZobrazeniNove_Click);
            // 
            // menuItemZobrazeniAktivni
            // 
            this.menuItemZobrazeniAktivni.Text = "F3 - Aktivní";
            this.menuItemZobrazeniAktivni.Click += new System.EventHandler(this.menuItemZobrazeniAktivni_Click);
            // 
            // menuItemZobrazeniDokoncene
            // 
            this.menuItemZobrazeniDokoncene.Text = "F4 - Dokončené";
            this.menuItemZobrazeniDokoncene.Click += new System.EventHandler(this.menuItemZobrazeniDokoncene_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dgUkoly);
            this.panelMain.Controls.Add(this.statusBar);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(306, 326);
            // 
            // ukolyBindingSource
            // 
            this.ukolyBindingSource.DataMember = "Ukoly";
            this.ukolyBindingSource.DataSource = this.ukoly;
            this.ukolyBindingSource.Sort = "";
            // 
            // dgUkoly
            // 
            this.dgUkoly.BackColorAlternating = System.Drawing.Color.Gold;
            this.dgUkoly.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgUkoly.CurrentRow = null;
            this.dgUkoly.DataSource = this.ukolyBindingSource;
            this.dgUkoly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgUkoly.HeaderBackColorSelected = System.Drawing.Color.Green;
            this.dgUkoly.HeaderForeColorSelected = System.Drawing.Color.Brown;
            this.dgUkoly.KeyScrollDown = System.Windows.Forms.Keys.D5;
            this.dgUkoly.KeyScrollUp = System.Windows.Forms.Keys.D2;
            this.dgUkoly.Location = new System.Drawing.Point(0, 0);
            this.dgUkoly.MultiSelect = false;
            this.dgUkoly.Name = "dgUkoly";
            this.dgUkoly.NumberFormat = "N";
            this.dgUkoly.RowHeightDefault = 23;
            this.dgUkoly.Size = new System.Drawing.Size(306, 307);
            this.dgUkoly.Sort = "";
            this.dgUkoly.SortByHeaderDoubleClick = true;
            this.dgUkoly.TabIndex = 0;
            this.dgUkoly.TableStyles.Add(this.dataGridTableStyleUkoly);
            // 
            // ukoly
            // 
            this.ukoly.DataSetName = "Ukoly";
            this.ukoly.Locale = new System.Globalization.CultureInfo("");
            this.ukoly.Prefix = "";
            this.ukoly.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridTableStyleUkoly
            // 
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn6);
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn7);
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn8);
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn9);
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn10);
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn11);
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn12);
            this.dataGridTableStyleUkoly.GridColumnStyles.Add(this.dataGridTextBoxColumn13);
            this.dataGridTableStyleUkoly.MappingName = "Ukoly";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Název";
            this.dataGridTextBoxColumn1.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn1.MappingName = "Name";
            this.dataGridTextBoxColumn1.NullText = "-";
            this.dataGridTextBoxColumn1.SelectionShow = false;
            this.dataGridTextBoxColumn1.Tag = "";
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Popis";
            this.dataGridTextBoxColumn2.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn2.MappingName = "Description";
            this.dataGridTextBoxColumn2.NullText = "-";
            this.dataGridTextBoxColumn2.SelectionShow = false;
            this.dataGridTextBoxColumn2.Tag = "";
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Vytvořil ID";
            this.dataGridTextBoxColumn3.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn3.MappingName = "CreatorID";
            this.dataGridTextBoxColumn3.NullText = "-";
            this.dataGridTextBoxColumn3.SelectionShow = false;
            this.dataGridTextBoxColumn3.Tag = "";
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "Vytvořeno";
            this.dataGridTextBoxColumn4.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn4.MappingName = "DateCreated";
            this.dataGridTextBoxColumn4.NullText = "-";
            this.dataGridTextBoxColumn4.SelectionShow = false;
            this.dataGridTextBoxColumn4.Tag = "";
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "Začít";
            this.dataGridTextBoxColumn5.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn5.MappingName = "DateFrom";
            this.dataGridTextBoxColumn5.NullText = "-";
            this.dataGridTextBoxColumn5.SelectionShow = false;
            this.dataGridTextBoxColumn5.Tag = "";
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "Dokončit";
            this.dataGridTextBoxColumn6.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn6.MappingName = "DateTo";
            this.dataGridTextBoxColumn6.NullText = "-";
            this.dataGridTextBoxColumn6.SelectionShow = false;
            this.dataGridTextBoxColumn6.Tag = "";
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "Priorita";
            this.dataGridTextBoxColumn7.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn7.MappingName = "Priority";
            this.dataGridTextBoxColumn7.NullText = "-";
            this.dataGridTextBoxColumn7.SelectionShow = false;
            this.dataGridTextBoxColumn7.Tag = "";
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            this.dataGridTextBoxColumn8.HeaderText = "Druh";
            this.dataGridTextBoxColumn8.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn8.MappingName = "Kind";
            this.dataGridTextBoxColumn8.NullText = "-";
            this.dataGridTextBoxColumn8.SelectionShow = false;
            this.dataGridTextBoxColumn8.Tag = "";
            // 
            // dataGridTextBoxColumn9
            // 
            this.dataGridTextBoxColumn9.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn9.Format = "";
            this.dataGridTextBoxColumn9.FormatInfo = null;
            this.dataGridTextBoxColumn9.HeaderText = "Typ";
            this.dataGridTextBoxColumn9.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn9.MappingName = "Type";
            this.dataGridTextBoxColumn9.NullText = "-";
            this.dataGridTextBoxColumn9.SelectionShow = false;
            this.dataGridTextBoxColumn9.Tag = "";
            // 
            // dataGridTextBoxColumn10
            // 
            this.dataGridTextBoxColumn10.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn10.Format = "";
            this.dataGridTextBoxColumn10.FormatInfo = null;
            this.dataGridTextBoxColumn10.HeaderText = "Stav";
            this.dataGridTextBoxColumn10.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn10.MappingName = "State_Uziv";
            this.dataGridTextBoxColumn10.NullText = "-";
            this.dataGridTextBoxColumn10.SelectionShow = false;
            this.dataGridTextBoxColumn10.Tag = "";
            // 
            // dataGridTextBoxColumn11
            // 
            this.dataGridTextBoxColumn11.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn11.Format = "";
            this.dataGridTextBoxColumn11.FormatInfo = null;
            this.dataGridTextBoxColumn11.HeaderText = "Poznámka";
            this.dataGridTextBoxColumn11.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn11.MappingName = "Note";
            this.dataGridTextBoxColumn11.NullText = "-";
            this.dataGridTextBoxColumn11.SelectionShow = false;
            this.dataGridTextBoxColumn11.Tag = "";
            // 
            // dataGridTextBoxColumn12
            // 
            this.dataGridTextBoxColumn12.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn12.Format = "";
            this.dataGridTextBoxColumn12.FormatInfo = null;
            this.dataGridTextBoxColumn12.HeaderText = "Připomenutí";
            this.dataGridTextBoxColumn12.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn12.MappingName = "DateNotify";
            this.dataGridTextBoxColumn12.NullText = "-";
            this.dataGridTextBoxColumn12.SelectionShow = false;
            this.dataGridTextBoxColumn12.Tag = "";
            // 
            // dataGridTextBoxColumn13
            // 
            this.dataGridTextBoxColumn13.Alignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn13.Format = "";
            this.dataGridTextBoxColumn13.FormatInfo = null;
            this.dataGridTextBoxColumn13.HeaderText = "Dokončeno";
            this.dataGridTextBoxColumn13.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dataGridTextBoxColumn13.MappingName = "DateFinished";
            this.dataGridTextBoxColumn13.NullText = "-";
            this.dataGridTextBoxColumn13.SelectionShow = false;
            this.dataGridTextBoxColumn13.Tag = "";
            // 
            // statusBar
            // 
            this.statusBar.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.statusBar.Location = new System.Drawing.Point(0, 307);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(306, 19);
            // 
            // UkolovaniMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(306, 326);
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.KeyPreview = true;
            this.Menu = this.mainMenu;
            this.Name = "UkolovaniMain";
            this.Text = "Úkoly";
            this.Load += new System.EventHandler(this.UkolovaniMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UkolovaniMain_KeyDown);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ukolyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgUkoly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ukoly)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemKonec;
        private System.Windows.Forms.Panel panelMain;
        private Fask.Graphic.DataGrid2 dgUkoly;
        private System.Windows.Forms.BindingSource ukolyBindingSource;
        private Fask.SQLiteDBs.DataSets.Ukoly ukoly;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyleUkoly;
        private System.Windows.Forms.MenuItem menuItemActualize;
        private System.Windows.Forms.MenuItem menuItemOpen;
        private System.Windows.Forms.MenuItem menuItem3;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn1;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn2;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn3;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn4;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn5;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn6;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn7;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn8;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn9;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn10;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn11;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn12;
        private Fask.Graphic.DataGrid2TextBoxColumn dataGridTextBoxColumn13;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItemZobrazeniVse;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItemZobrazeniAktivni;
        private System.Windows.Forms.MenuItem menuItemZobrazeniNove;
        private System.Windows.Forms.MenuItem menuItemZobrazeniDokoncene;
        private System.Windows.Forms.StatusBar statusBar;
    }
}