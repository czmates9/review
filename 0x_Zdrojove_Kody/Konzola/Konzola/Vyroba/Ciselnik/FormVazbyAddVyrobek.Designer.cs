namespace Konzola.Vyroba
{
    partial class FormVazbyAddVyrobek
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVazbyAddVyrobek));
            this.dgVyrobek = new Zuby.ADGV.AdvancedDataGridView();
            this.bsVyrobek = new System.Windows.Forms.BindingSource(this.components);
            this.dsVyrobek = new Fask.Interfaces.DataSets.Zbozi();
            this.zaznamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.obnovitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonNovy = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressIndicatorVyrobek = new ProgressControls.ProgressIndicator();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Vyrobek_SKL_ID = new System.Windows.Forms.ComboBox();
            this.comboBox_Vyrobek_LOCNCODE = new System.Windows.Forms.ComboBox();
            this.comboBox_Vyrobek_CZ_CarKod = new System.Windows.Forms.ComboBox();
            this.comboBox_Vyrobek_VNDITNUM = new System.Windows.Forms.ComboBox();
            this.comboBox_Vyrobek_ITEMDESC = new System.Windows.Forms.ComboBox();
            this.comboBox_Vyrobek_ITEMNMBR = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_Filtr_Vyrobky = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFiltry_AddVyrobek = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNastavit_AddVyrobek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZmena_AddVyrobek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPridat_AddVyrobek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdebrat_AddVyrobek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVycistit_AddVyrobek = new System.Windows.Forms.ToolStripButton();
            this.bw_vyrobek = new System.ComponentModel.BackgroundWorker();
            this.ITEMNMBR = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.ITEMDESC = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.VNDITNUM = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CZ_CarKod = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.LOCNCODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.SKL_ID = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.SKL_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.QTYPACK = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.MJ = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.DMJ = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.TIMEMODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TAXRATE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PRICE0 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PRICE1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PRICE2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PRICE3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PRICE4 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.PRICE5 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CZ_SerNum_Track = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CZ_SerNum_Delka = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CZ_Rez1_Track = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CZ_Rez2_Track = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CZ_Rez3_Track = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CZ_Rez4_Track = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.REZ1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.DEX_ROW_ID = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.ITEMCODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.ODB_ID = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.TIMEFROM = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.TIMETO = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.LSTMod = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.loginid = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgVyrobek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVyrobek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVyrobek)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgVyrobek
            // 
            this.dgVyrobek.AllowUserToAddRows = false;
            this.dgVyrobek.AllowUserToDeleteRows = false;
            this.dgVyrobek.AllowUserToOrderColumns = true;
            this.dgVyrobek.AllowUserToResizeRows = false;
            this.dgVyrobek.AutoGenerateColumns = false;
            this.dgVyrobek.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVyrobek.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ITEMNMBR,
            this.ITEMDESC,
            this.VNDITNUM,
            this.CZ_CarKod,
            this.LOCNCODE,
            this.SKL_ID,
            this.SKL_DESC,
            this.QTY,
            this.QTYPACK,
            this.MJ,
            this.DMJ,
            this.TIMEMODE,
            this.TAXRATE,
            this.PRICE0,
            this.PRICE1,
            this.PRICE2,
            this.PRICE3,
            this.PRICE4,
            this.PRICE5,
            this.CZ_SerNum_Track,
            this.CZ_SerNum_Delka,
            this.CZ_Rez1_Track,
            this.CZ_Rez2_Track,
            this.CZ_Rez3_Track,
            this.CZ_Rez4_Track,
            this.REZ1,
            this.DEX_ROW_ID,
            this.ITEMCODE,
            this.ODB_ID,
            this.TIMEFROM,
            this.TIMETO,
            this.LSTMod,
            this.loginid});
            this.dgVyrobek.DataSource = this.bsVyrobek;
            this.dgVyrobek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVyrobek.EnableHeadersVisualStyles = false;
            this.dgVyrobek.FilterAndSortEnabled = true;
            this.dgVyrobek.Location = new System.Drawing.Point(0, 197);
            this.dgVyrobek.Name = "dgVyrobek";
            this.dgVyrobek.ReadOnly = true;
            this.dgVyrobek.RowHeadersVisible = false;
            this.dgVyrobek.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVyrobek.Size = new System.Drawing.Size(672, 346);
            this.dgVyrobek.TabIndex = 1;
            // 
            // bsVyrobek
            // 
            this.bsVyrobek.DataMember = "FASK_ZASOBY_KONZOLA";
            this.bsVyrobek.DataSource = this.dsVyrobek;

            // 
            // dsVyrobek
            // 
            this.dsVyrobek.DataSetName = "KonzolaDataSet";
            this.dsVyrobek.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // zaznamToolStripMenuItem
            // 
            this.zaznamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.obnovitToolStripMenuItem,
            this.toolStripSeparator2,
            this.konecToolStripMenuItem});
            this.zaznamToolStripMenuItem.Name = "zaznamToolStripMenuItem";
            this.zaznamToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.zaznamToolStripMenuItem.Text = "Menu";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(104, 6);
            // 
            // obnovitToolStripMenuItem
            // 
            this.obnovitToolStripMenuItem.Name = "obnovitToolStripMenuItem";
            this.obnovitToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.obnovitToolStripMenuItem.Text = "Přidat";
            this.obnovitToolStripMenuItem.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(104, 6);
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.konecToolStripMenuItem.Text = "Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.buttonKonec_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonKonec);
            this.panelButtons.Controls.Add(this.buttonNovy);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(672, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 543);
            this.panelButtons.TabIndex = 2;
            // 
            // buttonKonec
            // 
            this.buttonKonec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonKonec.Location = new System.Drawing.Point(6, 468);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(73, 63);
            this.buttonKonec.TabIndex = 3;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Click += new System.EventHandler(this.buttonKonec_Click);
            // 
            // buttonNovy
            // 
            this.buttonNovy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNovy.Location = new System.Drawing.Point(6, 170);
            this.buttonNovy.Name = "buttonNovy";
            this.buttonNovy.Size = new System.Drawing.Size(73, 63);
            this.buttonNovy.TabIndex = 0;
            this.buttonNovy.Text = "Přidat";
            this.buttonNovy.UseVisualStyleBackColor = true;
            this.buttonNovy.Click += new System.EventHandler(this.buttonNovy_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zaznamToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(672, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressIndicatorVyrobek);
            this.panel1.Controls.Add(this.dgVyrobek);
            this.panel1.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.menuStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(672, 543);
            this.panel1.TabIndex = 3;
            // 
            // progressIndicatorVyrobek
            // 
            this.progressIndicatorVyrobek.Location = new System.Drawing.Point(295, 336);
            this.progressIndicatorVyrobek.Name = "progressIndicatorVyrobek";
            this.progressIndicatorVyrobek.Percentage = 0F;
            this.progressIndicatorVyrobek.Size = new System.Drawing.Size(88, 88);
            this.progressIndicatorVyrobek.TabIndex = 40;
            this.progressIndicatorVyrobek.Text = "progressIndicator1";
            this.progressIndicatorVyrobek.Visible = false;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 170);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(672, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 41;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox_Vyrobek_SKL_ID);
            this.groupBox1.Controls.Add(this.comboBox_Vyrobek_LOCNCODE);
            this.groupBox1.Controls.Add(this.comboBox_Vyrobek_CZ_CarKod);
            this.groupBox1.Controls.Add(this.comboBox_Vyrobek_VNDITNUM);
            this.groupBox1.Controls.Add(this.comboBox_Vyrobek_ITEMDESC);
            this.groupBox1.Controls.Add(this.comboBox_Vyrobek_ITEMNMBR);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.button_Filtr_Vyrobky);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 146);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Lokace:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_Vyrobek_SKL_ID
            // 
            this.comboBox_Vyrobek_SKL_ID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Vyrobek_SKL_ID.FormattingEnabled = true;
            this.comboBox_Vyrobek_SKL_ID.Location = new System.Drawing.Point(269, 112);
            this.comboBox_Vyrobek_SKL_ID.Name = "comboBox_Vyrobek_SKL_ID";
            this.comboBox_Vyrobek_SKL_ID.Size = new System.Drawing.Size(103, 21);
            this.comboBox_Vyrobek_SKL_ID.TabIndex = 34;
            // 
            // comboBox_Vyrobek_LOCNCODE
            // 
            this.comboBox_Vyrobek_LOCNCODE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Vyrobek_LOCNCODE.FormattingEnabled = true;
            this.comboBox_Vyrobek_LOCNCODE.Location = new System.Drawing.Point(269, 85);
            this.comboBox_Vyrobek_LOCNCODE.Name = "comboBox_Vyrobek_LOCNCODE";
            this.comboBox_Vyrobek_LOCNCODE.Size = new System.Drawing.Size(103, 21);
            this.comboBox_Vyrobek_LOCNCODE.TabIndex = 35;
            // 
            // comboBox_Vyrobek_CZ_CarKod
            // 
            this.comboBox_Vyrobek_CZ_CarKod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Vyrobek_CZ_CarKod.FormattingEnabled = true;
            this.comboBox_Vyrobek_CZ_CarKod.Location = new System.Drawing.Point(269, 58);
            this.comboBox_Vyrobek_CZ_CarKod.Name = "comboBox_Vyrobek_CZ_CarKod";
            this.comboBox_Vyrobek_CZ_CarKod.Size = new System.Drawing.Size(103, 21);
            this.comboBox_Vyrobek_CZ_CarKod.TabIndex = 30;
            // 
            // comboBox_Vyrobek_VNDITNUM
            // 
            this.comboBox_Vyrobek_VNDITNUM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Vyrobek_VNDITNUM.FormattingEnabled = true;
            this.comboBox_Vyrobek_VNDITNUM.Location = new System.Drawing.Point(106, 111);
            this.comboBox_Vyrobek_VNDITNUM.Name = "comboBox_Vyrobek_VNDITNUM";
            this.comboBox_Vyrobek_VNDITNUM.Size = new System.Drawing.Size(103, 21);
            this.comboBox_Vyrobek_VNDITNUM.TabIndex = 31;
            // 
            // comboBox_Vyrobek_ITEMDESC
            // 
            this.comboBox_Vyrobek_ITEMDESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Vyrobek_ITEMDESC.FormattingEnabled = true;
            this.comboBox_Vyrobek_ITEMDESC.Location = new System.Drawing.Point(106, 84);
            this.comboBox_Vyrobek_ITEMDESC.Name = "comboBox_Vyrobek_ITEMDESC";
            this.comboBox_Vyrobek_ITEMDESC.Size = new System.Drawing.Size(103, 21);
            this.comboBox_Vyrobek_ITEMDESC.TabIndex = 33;
            // 
            // comboBox_Vyrobek_ITEMNMBR
            // 
            this.comboBox_Vyrobek_ITEMNMBR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Vyrobek_ITEMNMBR.FormattingEnabled = true;
            this.comboBox_Vyrobek_ITEMNMBR.Location = new System.Drawing.Point(106, 57);
            this.comboBox_Vyrobek_ITEMNMBR.Name = "comboBox_Vyrobek_ITEMNMBR";
            this.comboBox_Vyrobek_ITEMNMBR.Size = new System.Drawing.Size(103, 21);
            this.comboBox_Vyrobek_ITEMNMBR.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Č. kód:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Č. k. dodavatele:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Pol. Číslo:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Popis :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(226, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Sklad:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_Filtr_Vyrobky
            // 
            this.button_Filtr_Vyrobky.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Filtr_Vyrobky.Location = new System.Drawing.Point(388, 58);
            this.button_Filtr_Vyrobky.Name = "button_Filtr_Vyrobky";
            this.button_Filtr_Vyrobky.Size = new System.Drawing.Size(99, 74);
            this.button_Filtr_Vyrobky.TabIndex = 29;
            this.button_Filtr_Vyrobky.Text = "Vyhledat";
            this.button_Filtr_Vyrobky.UseVisualStyleBackColor = true;
            this.button_Filtr_Vyrobky.Click += new System.EventHandler(this.button_Filtr_Vyrobky_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbFiltry_AddVyrobek,
            this.tsbNastavit_AddVyrobek,
            this.toolStripSeparator3,
            this.tsbZmena_AddVyrobek,
            this.toolStripSeparator4,
            this.tsbPridat_AddVyrobek,
            this.toolStripSeparator5,
            this.tsbOdebrat_AddVyrobek,
            this.toolStripSeparator6,
            this.tsbVycistit_AddVyrobek});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(666, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Filtry:";
            // 
            // tscbFiltry_AddVyrobek
            // 
            this.tscbFiltry_AddVyrobek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry_AddVyrobek.Name = "tscbFiltry_AddVyrobek";
            this.tscbFiltry_AddVyrobek.Size = new System.Drawing.Size(121, 25);
            // 
            // tsbNastavit_AddVyrobek
            // 
            this.tsbNastavit_AddVyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit_AddVyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit_AddVyrobek.Image")));
            this.tsbNastavit_AddVyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit_AddVyrobek.Name = "tsbNastavit_AddVyrobek";
            this.tsbNastavit_AddVyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbNastavit_AddVyrobek.Text = "Nastavit";
            this.tsbNastavit_AddVyrobek.Click += new System.EventHandler(this.tsbNastavit_AddVyrobek_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbZmena_AddVyrobek
            // 
            this.tsbZmena_AddVyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena_AddVyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena_AddVyrobek.Image")));
            this.tsbZmena_AddVyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena_AddVyrobek.Name = "tsbZmena_AddVyrobek";
            this.tsbZmena_AddVyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbZmena_AddVyrobek.Text = "Změna";
            this.tsbZmena_AddVyrobek.Click += new System.EventHandler(this.tsbZmena_AddVyrobek_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPridat_AddVyrobek
            // 
            this.tsbPridat_AddVyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat_AddVyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat_AddVyrobek.Image")));
            this.tsbPridat_AddVyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat_AddVyrobek.Name = "tsbPridat_AddVyrobek";
            this.tsbPridat_AddVyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbPridat_AddVyrobek.Text = "Uložit";
            this.tsbPridat_AddVyrobek.Click += new System.EventHandler(this.tsbPridat_AddVyrobek_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOdebrat_AddVyrobek
            // 
            this.tsbOdebrat_AddVyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat_AddVyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat_AddVyrobek.Image")));
            this.tsbOdebrat_AddVyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat_AddVyrobek.Name = "tsbOdebrat_AddVyrobek";
            this.tsbOdebrat_AddVyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbOdebrat_AddVyrobek.Text = "Odebrat";
            this.tsbOdebrat_AddVyrobek.Click += new System.EventHandler(this.tsbOdebrat_AddVyrobek_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbVycistit_AddVyrobek
            // 
            this.tsbVycistit_AddVyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit_AddVyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit_AddVyrobek.Image")));
            this.tsbVycistit_AddVyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit_AddVyrobek.Name = "tsbVycistit_AddVyrobek";
            this.tsbVycistit_AddVyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbVycistit_AddVyrobek.Text = "Vyčistit";
            this.tsbVycistit_AddVyrobek.Click += new System.EventHandler(this.tsbVycistit_AddVyrobek_Click);
            // 
            // bw_vyrobek
            // 
            this.bw_vyrobek.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_vyrobek_DoWork);
            this.bw_vyrobek.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_vyrobek_RunWorkerCompleted);
            // 
            // ITEMNMBR
            // 
            this.ITEMNMBR.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR.FilteringEnabled = false;
            this.ITEMNMBR.HeaderText = "Pol. číslo";
            this.ITEMNMBR.Name = "ITEMNMBR";
            this.ITEMNMBR.ReadOnly = true;
            this.ITEMNMBR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMDESC
            // 
            this.ITEMDESC.DataPropertyName = "ITEMDESC";
            this.ITEMDESC.FilteringEnabled = false;
            this.ITEMDESC.HeaderText = "Popis";
            this.ITEMDESC.Name = "ITEMDESC";
            this.ITEMDESC.ReadOnly = true;
            this.ITEMDESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // VNDITNUM
            // 
            this.VNDITNUM.DataPropertyName = "VNDITNUM";
            this.VNDITNUM.FilteringEnabled = false;
            this.VNDITNUM.HeaderText = "Č. k. dodavatele";
            this.VNDITNUM.Name = "VNDITNUM";
            this.VNDITNUM.ReadOnly = true;
            this.VNDITNUM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CZ_CarKod
            // 
            this.CZ_CarKod.DataPropertyName = "CZ_CarKod";
            this.CZ_CarKod.FilteringEnabled = false;
            this.CZ_CarKod.HeaderText = "Č. kód";
            this.CZ_CarKod.Name = "CZ_CarKod";
            this.CZ_CarKod.ReadOnly = true;
            this.CZ_CarKod.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // LOCNCODE
            // 
            this.LOCNCODE.DataPropertyName = "LOCNCODE";
            this.LOCNCODE.FilteringEnabled = false;
            this.LOCNCODE.HeaderText = "Lokace";
            this.LOCNCODE.Name = "LOCNCODE";
            this.LOCNCODE.ReadOnly = true;
            this.LOCNCODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_ID
            // 
            this.SKL_ID.DataPropertyName = "SKL_ID";
            this.SKL_ID.FilteringEnabled = false;
            this.SKL_ID.HeaderText = "ID sklad";
            this.SKL_ID.Name = "SKL_ID";
            this.SKL_ID.ReadOnly = true;
            this.SKL_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_DESC
            // 
            this.SKL_DESC.DataPropertyName = "SKL_DESC";
            this.SKL_DESC.HeaderText = "Název skladu";
            this.SKL_DESC.Name = "SKL_DESC";
            this.SKL_DESC.ReadOnly = true;
            // 
            // QTY
            // 
            this.QTY.DataPropertyName = "QTY";
            this.QTY.FilteringEnabled = false;
            this.QTY.HeaderText = "Množství";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // QTYPACK
            // 
            this.QTYPACK.DataPropertyName = "QTYPACK";
            this.QTYPACK.FilteringEnabled = false;
            this.QTYPACK.HeaderText = "Množství v balení";
            this.QTYPACK.Name = "QTYPACK";
            this.QTYPACK.ReadOnly = true;
            this.QTYPACK.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MJ
            // 
            this.MJ.DataPropertyName = "MJ";
            this.MJ.FilteringEnabled = false;
            this.MJ.HeaderText = "Měrná jednotka";
            this.MJ.Name = "MJ";
            this.MJ.ReadOnly = true;
            this.MJ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DMJ
            // 
            this.DMJ.DataPropertyName = "DMJ";
            this.DMJ.FilteringEnabled = false;
            this.DMJ.HeaderText = "Dop. měrná jednotka";
            this.DMJ.Name = "DMJ";
            this.DMJ.ReadOnly = true;
            this.DMJ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMEMODE
            // 
            this.TIMEMODE.DataPropertyName = "TIMEMODE";
            this.TIMEMODE.HeaderText = "Typ sledování času";
            this.TIMEMODE.Name = "TIMEMODE";
            this.TIMEMODE.ReadOnly = true;
            // 
            // TAXRATE
            // 
            this.TAXRATE.DataPropertyName = "TAXRATE";
            this.TAXRATE.FilteringEnabled = false;
            this.TAXRATE.HeaderText = "výše DPH";
            this.TAXRATE.Name = "TAXRATE";
            this.TAXRATE.ReadOnly = true;
            this.TAXRATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PRICE0
            // 
            this.PRICE0.DataPropertyName = "PRICE0";
            this.PRICE0.FilteringEnabled = false;
            this.PRICE0.HeaderText = "Základ. cena";
            this.PRICE0.Name = "PRICE0";
            this.PRICE0.ReadOnly = true;
            this.PRICE0.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PRICE1
            // 
            this.PRICE1.DataPropertyName = "PRICE1";
            this.PRICE1.FilteringEnabled = false;
            this.PRICE1.HeaderText = "Cen. hladina 1";
            this.PRICE1.Name = "PRICE1";
            this.PRICE1.ReadOnly = true;
            this.PRICE1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PRICE2
            // 
            this.PRICE2.DataPropertyName = "PRICE2";
            this.PRICE2.FilteringEnabled = false;
            this.PRICE2.HeaderText = "Cen. hladina 2";
            this.PRICE2.Name = "PRICE2";
            this.PRICE2.ReadOnly = true;
            this.PRICE2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PRICE3
            // 
            this.PRICE3.DataPropertyName = "PRICE3";
            this.PRICE3.FilteringEnabled = false;
            this.PRICE3.HeaderText = "Cen. hladina 3";
            this.PRICE3.Name = "PRICE3";
            this.PRICE3.ReadOnly = true;
            this.PRICE3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PRICE4
            // 
            this.PRICE4.DataPropertyName = "PRICE4";
            this.PRICE4.FilteringEnabled = false;
            this.PRICE4.HeaderText = "Cen. hladina 4";
            this.PRICE4.Name = "PRICE4";
            this.PRICE4.ReadOnly = true;
            this.PRICE4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PRICE5
            // 
            this.PRICE5.DataPropertyName = "PRICE5";
            this.PRICE5.FilteringEnabled = false;
            this.PRICE5.HeaderText = "Cen. hladina 5";
            this.PRICE5.Name = "PRICE5";
            this.PRICE5.ReadOnly = true;
            this.PRICE5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CZ_SerNum_Track
            // 
            this.CZ_SerNum_Track.DataPropertyName = "CZ_SerNum_Track";
            this.CZ_SerNum_Track.FilteringEnabled = false;
            this.CZ_SerNum_Track.HeaderText = "Sledovat na sér. č.";
            this.CZ_SerNum_Track.Name = "CZ_SerNum_Track";
            this.CZ_SerNum_Track.ReadOnly = true;
            this.CZ_SerNum_Track.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CZ_SerNum_Delka
            // 
            this.CZ_SerNum_Delka.DataPropertyName = "CZ_SerNum_Delka";
            this.CZ_SerNum_Delka.FilteringEnabled = false;
            this.CZ_SerNum_Delka.HeaderText = "Délka sér. č.";
            this.CZ_SerNum_Delka.Name = "CZ_SerNum_Delka";
            this.CZ_SerNum_Delka.ReadOnly = true;
            this.CZ_SerNum_Delka.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CZ_Rez1_Track
            // 
            this.CZ_Rez1_Track.DataPropertyName = "CZ_Rez1_Track";
            this.CZ_Rez1_Track.FilteringEnabled = false;
            this.CZ_Rez1_Track.HeaderText = "CZ_Rez1_Track";
            this.CZ_Rez1_Track.Name = "CZ_Rez1_Track";
            this.CZ_Rez1_Track.ReadOnly = true;
            this.CZ_Rez1_Track.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CZ_Rez1_Track.Visible = false;
            // 
            // CZ_Rez2_Track
            // 
            this.CZ_Rez2_Track.DataPropertyName = "CZ_Rez2_Track";
            this.CZ_Rez2_Track.FilteringEnabled = false;
            this.CZ_Rez2_Track.HeaderText = "CZ_Rez2_Track";
            this.CZ_Rez2_Track.Name = "CZ_Rez2_Track";
            this.CZ_Rez2_Track.ReadOnly = true;
            this.CZ_Rez2_Track.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CZ_Rez2_Track.Visible = false;
            // 
            // CZ_Rez3_Track
            // 
            this.CZ_Rez3_Track.DataPropertyName = "CZ_Rez3_Track";
            this.CZ_Rez3_Track.FilteringEnabled = false;
            this.CZ_Rez3_Track.HeaderText = "CZ_Rez3_Track";
            this.CZ_Rez3_Track.Name = "CZ_Rez3_Track";
            this.CZ_Rez3_Track.ReadOnly = true;
            this.CZ_Rez3_Track.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CZ_Rez3_Track.Visible = false;
            // 
            // CZ_Rez4_Track
            // 
            this.CZ_Rez4_Track.DataPropertyName = "CZ_Rez4_Track";
            this.CZ_Rez4_Track.FilteringEnabled = false;
            this.CZ_Rez4_Track.HeaderText = "CZ_Rez4_Track";
            this.CZ_Rez4_Track.Name = "CZ_Rez4_Track";
            this.CZ_Rez4_Track.ReadOnly = true;
            this.CZ_Rez4_Track.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CZ_Rez4_Track.Visible = false;
            // 
            // REZ1
            // 
            this.REZ1.DataPropertyName = "REZ1";
            this.REZ1.FilteringEnabled = false;
            this.REZ1.HeaderText = "REZ1";
            this.REZ1.Name = "REZ1";
            this.REZ1.ReadOnly = true;
            this.REZ1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.REZ1.Visible = false;
            // 
            // DEX_ROW_ID
            // 
            this.DEX_ROW_ID.DataPropertyName = "DEX_ROW_ID";
            this.DEX_ROW_ID.FilteringEnabled = false;
            this.DEX_ROW_ID.HeaderText = "Index";
            this.DEX_ROW_ID.Name = "DEX_ROW_ID";
            this.DEX_ROW_ID.ReadOnly = true;
            this.DEX_ROW_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMCODE
            // 
            this.ITEMCODE.DataPropertyName = "ITEMCODE";
            this.ITEMCODE.FilteringEnabled = false;
            this.ITEMCODE.HeaderText = "ITEMCODE";
            this.ITEMCODE.Name = "ITEMCODE";
            this.ITEMCODE.ReadOnly = true;
            this.ITEMCODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ITEMCODE.Visible = false;
            // 
            // ODB_ID
            // 
            this.ODB_ID.DataPropertyName = "ODB_ID";
            this.ODB_ID.FilteringEnabled = false;
            this.ODB_ID.HeaderText = "Odběratel";
            this.ODB_ID.Name = "ODB_ID";
            this.ODB_ID.ReadOnly = true;
            this.ODB_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMEFROM
            // 
            this.TIMEFROM.DataPropertyName = "TIMEFROM";
            this.TIMEFROM.FilteringEnabled = false;
            this.TIMEFROM.HeaderText = "Platnost od";
            this.TIMEFROM.Name = "TIMEFROM";
            this.TIMEFROM.ReadOnly = true;
            this.TIMEFROM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMETO
            // 
            this.TIMETO.DataPropertyName = "TIMETO";
            this.TIMETO.FilteringEnabled = false;
            this.TIMETO.HeaderText = "Platnost do";
            this.TIMETO.Name = "TIMETO";
            this.TIMETO.ReadOnly = true;
            this.TIMETO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // LSTMod
            // 
            this.LSTMod.DataPropertyName = "LSTMod";
            this.LSTMod.FilteringEnabled = false;
            this.LSTMod.HeaderText = "Poslední úprava";
            this.LSTMod.Name = "LSTMod";
            this.LSTMod.ReadOnly = true;
            this.LSTMod.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // loginid
            // 
            this.loginid.DataPropertyName = "loginid";
            this.loginid.FilteringEnabled = false;
            this.loginid.HeaderText = "ID uživatel";
            this.loginid.Name = "loginid";
            this.loginid.ReadOnly = true;
            this.loginid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // FormVazbyAddVyrobek
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 543);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormVazbyAddVyrobek";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Přidat výrobek";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVazbyAddVyrobek_FormClosing);
            this.Load += new System.EventHandler(this.FormVazbyAddVyrobek_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormVazbyAddVyrobek_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgVyrobek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVyrobek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVyrobek)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgVyrobek;
        private System.Windows.Forms.ToolStripMenuItem zaznamToolStripMenuItem;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonNovy;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.Panel panel1;
        private Fask.Interfaces.DataSets.Zbozi dsVyrobek;
        private System.Windows.Forms.BindingSource bsVyrobek;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem obnovitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbFiltry_AddVyrobek;
        private System.Windows.Forms.ToolStripButton tsbNastavit_AddVyrobek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbZmena_AddVyrobek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbPridat_AddVyrobek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbOdebrat_AddVyrobek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbVycistit_AddVyrobek;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Vyrobek_SKL_ID;
        private System.Windows.Forms.ComboBox comboBox_Vyrobek_LOCNCODE;
        private System.Windows.Forms.ComboBox comboBox_Vyrobek_CZ_CarKod;
        private System.Windows.Forms.ComboBox comboBox_Vyrobek_VNDITNUM;
        private System.Windows.Forms.ComboBox comboBox_Vyrobek_ITEMDESC;
        private System.Windows.Forms.ComboBox comboBox_Vyrobek_ITEMNMBR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_Filtr_Vyrobky;
        private ProgressControls.ProgressIndicator progressIndicatorVyrobek;
        private System.ComponentModel.BackgroundWorker bw_vyrobek;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ITEMNMBR;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ITEMDESC;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn VNDITNUM;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CZ_CarKod;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn LOCNCODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn SKL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_DESC;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn QTY;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn QTYPACK;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn MJ;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn DMJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEMODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn TAXRATE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PRICE0;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PRICE1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PRICE2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PRICE3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PRICE4;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn PRICE5;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CZ_SerNum_Track;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CZ_SerNum_Delka;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CZ_Rez1_Track;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CZ_Rez2_Track;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CZ_Rez3_Track;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CZ_Rez4_Track;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn REZ1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn DEX_ROW_ID;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ITEMCODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ODB_ID;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn TIMEFROM;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn TIMETO;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn LSTMod;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn loginid;
    }
}