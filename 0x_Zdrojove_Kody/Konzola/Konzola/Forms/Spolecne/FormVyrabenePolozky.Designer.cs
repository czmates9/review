namespace Konzola.Forms.Spolecne
{
    partial class FormVyrabenePolozky
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVyrabenePolozky));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chB_Nezrealizovane = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tB_CountEntries_VPP = new System.Windows.Forms.TextBox();
            this.tB_ITEMDESC = new System.Windows.Forms.TextBox();
            this.tB_VNDITNUM = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chB_Ukonceno = new System.Windows.Forms.CheckBox();
            this.chB_Neaktivni = new System.Windows.Forms.CheckBox();
            this.chB_Aktivni = new System.Windows.Forms.CheckBox();
            this.tB_SOPNUMBE = new System.Windows.Forms.TextBox();
            this.tB_DateProd_DO = new System.Windows.Forms.TextBox();
            this.tB_DateProd_OD = new System.Windows.Forms.TextBox();
            this.tB_CountEntries = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tsFiltry = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFiltry = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNastavit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZmena = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPridat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdebrat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVycistit = new System.Windows.Forms.ToolStripButton();
            this.buttonOdznacitVse = new System.Windows.Forms.Button();
            this.buttonOznacitVse = new System.Windows.Forms.Button();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExporty = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskEtiketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExceOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPolozka = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportovatPrijemku = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSchvalitVybrane = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUkoncitZakazkuKorekci = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpravit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiArchivaceVybrane = new System.Windows.Forms.ToolStripMenuItem();
            this.progressIndicatorVyrobek = new ProgressControls.ProgressIndicator();
            this.bw_VyrabenePolozky = new System.ComponentModel.BackgroundWorker();
            this.bw_import = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tssl_Production_Count = new System.Windows.Forms.ToolStripLabel();
            this.bw_Schvaleni = new System.ComponentModel.BackgroundWorker();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.advancedDataGridViewSearchToolBar1 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.dgVyrabenePolozky = new Zuby.ADGV.AdvancedDataGridView();
            this.bsVyrabenePolozky = new System.Windows.Forms.BindingSource(this.components);
            this.bsVyrabenePolozky_00 = new System.Windows.Forms.BindingSource(this.components);
            this.dsVyrabenePolozky = new Fask.Interfaces.DataSets.Hlavni();
            this.activeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countEntriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPNUMBEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPTYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDDOCNMHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodeHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vPHLOCNCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateProdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rez1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rez2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.termIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uSERIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lSTModDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEXROWIDPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEXROWIDVPPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countEntriesVPPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sOPNUMBEVPPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMTYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMMJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDDOCNMPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oRDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodePDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barcodeTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOCNCODEVPPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYDOKONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYPACKDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYODVEDENO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MnozstviZbyva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNTODVEDENO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYPACKMJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMEMODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMEPREPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIMEUNITDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtProdTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtProdLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serNumTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serNumLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.verTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.verLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.termIDVPPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lSTModVPPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cZREZ1TrackDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cZREZ2TrackDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cZREZ3TrackDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cZREZ4TrackDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cZREZ5TrackDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wEIGHTTARADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wEIGHTNETTODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wEIGHTTOLPLUSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wEIGHTTOLMINUSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVyrabenePolozky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVyrabenePolozky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVyrabenePolozky_00)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVyrabenePolozky)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Číslo dávky:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chB_Nezrealizovane);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Controls.Add(this.buttonOdznacitVse);
            this.panel1.Controls.Add(this.buttonOznacitVse);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 261);
            this.panel1.TabIndex = 7;
            // 
            // chB_Nezrealizovane
            // 
            this.chB_Nezrealizovane.AutoSize = true;
            this.chB_Nezrealizovane.Location = new System.Drawing.Point(703, 82);
            this.chB_Nezrealizovane.Name = "chB_Nezrealizovane";
            this.chB_Nezrealizovane.Size = new System.Drawing.Size(117, 17);
            this.chB_Nezrealizovane.TabIndex = 65;
            this.chB_Nezrealizovane.Text = "Jen nezrealizovane";
            this.chB_Nezrealizovane.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tB_CountEntries_VPP);
            this.groupBox2.Controls.Add(this.tB_ITEMDESC);
            this.groupBox2.Controls.Add(this.tB_VNDITNUM);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(347, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 97);
            this.groupBox2.TabIndex = 64;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Položky:";
            // 
            // tB_CountEntries_VPP
            // 
            this.tB_CountEntries_VPP.Location = new System.Drawing.Point(116, 22);
            this.tB_CountEntries_VPP.Name = "tB_CountEntries_VPP";
            this.tB_CountEntries_VPP.Size = new System.Drawing.Size(212, 20);
            this.tB_CountEntries_VPP.TabIndex = 61;
            // 
            // tB_ITEMDESC
            // 
            this.tB_ITEMDESC.Location = new System.Drawing.Point(116, 50);
            this.tB_ITEMDESC.Name = "tB_ITEMDESC";
            this.tB_ITEMDESC.Size = new System.Drawing.Size(212, 20);
            this.tB_ITEMDESC.TabIndex = 61;
            // 
            // tB_VNDITNUM
            // 
            this.tB_VNDITNUM.Location = new System.Drawing.Point(116, 74);
            this.tB_VNDITNUM.Name = "tB_VNDITNUM";
            this.tB_VNDITNUM.Size = new System.Drawing.Size(212, 20);
            this.tB_VNDITNUM.TabIndex = 61;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(59, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Pol.číslo:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 13);
            this.label10.TabIndex = 62;
            this.label10.Text = "Číslo položky externí:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Popis položky:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chB_Ukonceno);
            this.groupBox1.Controls.Add(this.chB_Neaktivni);
            this.groupBox1.Controls.Add(this.chB_Aktivni);
            this.groupBox1.Controls.Add(this.tB_SOPNUMBE);
            this.groupBox1.Controls.Add(this.tB_DateProd_DO);
            this.groupBox1.Controls.Add(this.tB_DateProd_OD);
            this.groupBox1.Controls.Add(this.tB_CountEntries);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 155);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hlavičky:";
            // 
            // chB_Ukonceno
            // 
            this.chB_Ukonceno.AutoSize = true;
            this.chB_Ukonceno.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chB_Ukonceno.Checked = true;
            this.chB_Ukonceno.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_Ukonceno.Location = new System.Drawing.Point(235, 19);
            this.chB_Ukonceno.Name = "chB_Ukonceno";
            this.chB_Ukonceno.Size = new System.Drawing.Size(82, 17);
            this.chB_Ukonceno.TabIndex = 64;
            this.chB_Ukonceno.Text = "Ukončeno: ";
            this.chB_Ukonceno.UseVisualStyleBackColor = true;
            // 
            // chB_Neaktivni
            // 
            this.chB_Neaktivni.AutoSize = true;
            this.chB_Neaktivni.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chB_Neaktivni.Checked = true;
            this.chB_Neaktivni.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_Neaktivni.Location = new System.Drawing.Point(69, 19);
            this.chB_Neaktivni.Name = "chB_Neaktivni";
            this.chB_Neaktivni.Size = new System.Drawing.Size(79, 17);
            this.chB_Neaktivni.TabIndex = 64;
            this.chB_Neaktivni.Text = "Neaktivní: ";
            this.chB_Neaktivni.UseVisualStyleBackColor = true;
            // 
            // chB_Aktivni
            // 
            this.chB_Aktivni.AutoSize = true;
            this.chB_Aktivni.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chB_Aktivni.Checked = true;
            this.chB_Aktivni.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_Aktivni.Location = new System.Drawing.Point(163, 19);
            this.chB_Aktivni.Name = "chB_Aktivni";
            this.chB_Aktivni.Size = new System.Drawing.Size(66, 17);
            this.chB_Aktivni.TabIndex = 64;
            this.chB_Aktivni.Text = "Aktivní: ";
            this.chB_Aktivni.UseVisualStyleBackColor = true;
            // 
            // tB_SOPNUMBE
            // 
            this.tB_SOPNUMBE.Location = new System.Drawing.Point(100, 70);
            this.tB_SOPNUMBE.Name = "tB_SOPNUMBE";
            this.tB_SOPNUMBE.Size = new System.Drawing.Size(217, 20);
            this.tB_SOPNUMBE.TabIndex = 63;
            // 
            // tB_DateProd_DO
            // 
            this.tB_DateProd_DO.Location = new System.Drawing.Point(249, 125);
            this.tB_DateProd_DO.Name = "tB_DateProd_DO";
            this.tB_DateProd_DO.Size = new System.Drawing.Size(67, 20);
            this.tB_DateProd_DO.TabIndex = 63;
            // 
            // tB_DateProd_OD
            // 
            this.tB_DateProd_OD.Location = new System.Drawing.Point(249, 100);
            this.tB_DateProd_OD.Name = "tB_DateProd_OD";
            this.tB_DateProd_OD.Size = new System.Drawing.Size(67, 20);
            this.tB_DateProd_OD.TabIndex = 63;
            // 
            // tB_CountEntries
            // 
            this.tB_CountEntries.Location = new System.Drawing.Point(100, 46);
            this.tB_CountEntries.Name = "tB_CountEntries";
            this.tB_CountEntries.Size = new System.Drawing.Size(217, 20);
            this.tB_CountEntries.TabIndex = 63;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(21, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Oček. směna výroby";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Výrobní zakázka:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(217, 128);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Do";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Od";
            // 
            // tsFiltry
            // 
            this.tsFiltry.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsFiltry.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsFiltry.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbFiltry,
            this.tsbNastavit,
            this.toolStripSeparator2,
            this.tsbZmena,
            this.toolStripSeparator4,
            this.tsbPridat,
            this.toolStripSeparator3,
            this.tsbOdebrat,
            this.toolStripSeparator5,
            this.tsbVycistit});
            this.tsFiltry.Location = new System.Drawing.Point(0, 24);
            this.tsFiltry.Name = "tsFiltry";
            this.tsFiltry.Size = new System.Drawing.Size(1045, 27);
            this.tsFiltry.TabIndex = 59;
            this.tsFiltry.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 24);
            this.toolStripLabel1.Text = "Filtry:";
            // 
            // tscbFiltry
            // 
            this.tscbFiltry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry.DropDownWidth = 170;
            this.tscbFiltry.Name = "tscbFiltry";
            this.tscbFiltry.Size = new System.Drawing.Size(170, 27);
            // 
            // tsbNastavit
            // 
            this.tsbNastavit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit.Image")));
            this.tsbNastavit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit.Name = "tsbNastavit";
            this.tsbNastavit.Size = new System.Drawing.Size(24, 24);
            this.tsbNastavit.ToolTipText = "Nastavit";
            this.tsbNastavit.Click += new System.EventHandler(this.tsbNastavit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbZmena
            // 
            this.tsbZmena.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena.Image")));
            this.tsbZmena.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena.Name = "tsbZmena";
            this.tsbZmena.Size = new System.Drawing.Size(24, 24);
            this.tsbZmena.Text = "Změna";
            this.tsbZmena.Click += new System.EventHandler(this.tsbZmena_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbPridat
            // 
            this.tsbPridat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat.Image")));
            this.tsbPridat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat.Name = "tsbPridat";
            this.tsbPridat.Size = new System.Drawing.Size(24, 24);
            this.tsbPridat.Text = "Uložit";
            this.tsbPridat.Click += new System.EventHandler(this.tsbPridat_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbOdebrat
            // 
            this.tsbOdebrat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat.Image")));
            this.tsbOdebrat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat.Name = "tsbOdebrat";
            this.tsbOdebrat.Size = new System.Drawing.Size(24, 24);
            this.tsbOdebrat.Text = "Odebrat";
            this.tsbOdebrat.Click += new System.EventHandler(this.tsbOdebrat_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbVycistit
            // 
            this.tsbVycistit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit.Image")));
            this.tsbVycistit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit.Name = "tsbVycistit";
            this.tsbVycistit.Size = new System.Drawing.Size(24, 24);
            this.tsbVycistit.Text = "Vyčistit";
            this.tsbVycistit.Click += new System.EventHandler(this.tsbVycistit_Click);
            // 
            // buttonOdznacitVse
            // 
            this.buttonOdznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOdznacitVse.Enabled = false;
            this.buttonOdznacitVse.Location = new System.Drawing.Point(956, 172);
            this.buttonOdznacitVse.Name = "buttonOdznacitVse";
            this.buttonOdznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOdznacitVse.TabIndex = 13;
            this.buttonOdznacitVse.Text = "Odznačit vše";
            this.buttonOdznacitVse.UseVisualStyleBackColor = true;
            this.buttonOdznacitVse.Click += new System.EventHandler(this.buttonOdznacitVse_Click);
            // 
            // buttonOznacitVse
            // 
            this.buttonOznacitVse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOznacitVse.Enabled = false;
            this.buttonOznacitVse.Location = new System.Drawing.Point(867, 172);
            this.buttonOznacitVse.Name = "buttonOznacitVse";
            this.buttonOznacitVse.Size = new System.Drawing.Size(83, 23);
            this.buttonOznacitVse.TabIndex = 12;
            this.buttonOznacitVse.Text = "Označit vše";
            this.buttonOznacitVse.UseVisualStyleBackColor = true;
            this.buttonOznacitVse.Click += new System.EventHandler(this.buttonOznacitVse_Click);
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(956, 92);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(83, 74);
            this.buttonVyhledat.TabIndex = 11;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiExporty,
            this.tsmiPolozka,
            this.tsmiAkce});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1045, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiMenu
            // 
            this.tsmiMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiKonec});
            this.tsmiMenu.Name = "tsmiMenu";
            this.tsmiMenu.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenu.Text = "Menu";
            // 
            // tsmiKonec
            // 
            this.tsmiKonec.Name = "tsmiKonec";
            this.tsmiKonec.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.tsmiKonec.Size = new System.Drawing.Size(148, 22);
            this.tsmiKonec.Text = "Konec";
            this.tsmiKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // tsmiExporty
            // 
            this.tsmiExporty.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiskToolStripMenuItem,
            this.tiskEtiketToolStripMenuItem,
            this.toolStripSeparator6,
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator1,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExceOznacene,
            this.toolStripSeparator7,
            this.tsmiExportDoXMLVse,
            this.tsmiExportDoXMLOznacene});
            this.tsmiExporty.Name = "tsmiExporty";
            this.tsmiExporty.Size = new System.Drawing.Size(55, 20);
            this.tsmiExporty.Text = "Výstup";
            // 
            // tiskToolStripMenuItem
            // 
            this.tiskToolStripMenuItem.Name = "tiskToolStripMenuItem";
            this.tiskToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+P, Ctrl+R";
            this.tiskToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tiskToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.tiskToolStripMenuItem.Text = "Tisk";
            this.tiskToolStripMenuItem.Click += new System.EventHandler(this.tiskToolStripMenuItem_Click);
            // 
            // tiskEtiketToolStripMenuItem
            // 
            this.tiskEtiketToolStripMenuItem.Name = "tiskEtiketToolStripMenuItem";
            this.tiskEtiketToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+E, Ctrl+T";
            this.tiskEtiketToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tiskEtiketToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.tiskEtiketToolStripMenuItem.Text = "Tisk etiket";
            this.tiskEtiketToolStripMenuItem.Click += new System.EventHandler(this.tiskEtiketToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiExportDoCSVVse
            // 
            this.tsmiExportDoCSVVse.Name = "tsmiExportDoCSVVse";
            this.tsmiExportDoCSVVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoCSVVse.Text = "Export do CSV vše";
            this.tsmiExportDoCSVVse.Click += new System.EventHandler(this.exportDoCSVVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoCSVOznacene
            // 
            this.tsmiExportDoCSVOznacene.Name = "tsmiExportDoCSVOznacene";
            this.tsmiExportDoCSVOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoCSVOznacene.Text = "Export do CSV označené";
            this.tsmiExportDoCSVOznacene.Click += new System.EventHandler(this.exportDoCSVOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiExportDoExcelVse
            // 
            this.tsmiExportDoExcelVse.Name = "tsmiExportDoExcelVse";
            this.tsmiExportDoExcelVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoExcelVse.Text = "Export do Excel vše";
            this.tsmiExportDoExcelVse.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoExceOznacene
            // 
            this.tsmiExportDoExceOznacene.Name = "tsmiExportDoExceOznacene";
            this.tsmiExportDoExceOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoExceOznacene.Text = "Export do Excel označené";
            this.tsmiExportDoExceOznacene.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiExportDoXMLVse
            // 
            this.tsmiExportDoXMLVse.Name = "tsmiExportDoXMLVse";
            this.tsmiExportDoXMLVse.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoXMLVse.Text = "Export do XML Vše";
            this.tsmiExportDoXMLVse.Click += new System.EventHandler(this.exportDoXMLVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoXMLOznacene
            // 
            this.tsmiExportDoXMLOznacene.Name = "tsmiExportDoXMLOznacene";
            this.tsmiExportDoXMLOznacene.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportDoXMLOznacene.Text = "Export do XML označené";
            this.tsmiExportDoXMLOznacene.Click += new System.EventHandler(this.exportDoXMLOznaceneToolStripMenuItem_Click);
            // 
            // tsmiPolozka
            // 
            this.tsmiPolozka.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportovatPrijemku,
            this.tsmiSchvalitVybrane,
            this.tsmiUkoncitZakazkuKorekci,
            this.tsmiUpravit});
            this.tsmiPolozka.Name = "tsmiPolozka";
            this.tsmiPolozka.Size = new System.Drawing.Size(60, 20);
            this.tsmiPolozka.Text = "Položka";
            // 
            // tsmiExportovatPrijemku
            // 
            this.tsmiExportovatPrijemku.Name = "tsmiExportovatPrijemku";
            this.tsmiExportovatPrijemku.Size = new System.Drawing.Size(208, 22);
            this.tsmiExportovatPrijemku.Text = "Exportovat Příjemku";
            this.tsmiExportovatPrijemku.Click += new System.EventHandler(this.tsmiExportovatPrijemku_Click);
            // 
            // tsmiSchvalitVybrane
            // 
            this.tsmiSchvalitVybrane.Name = "tsmiSchvalitVybrane";
            this.tsmiSchvalitVybrane.Size = new System.Drawing.Size(208, 22);
            this.tsmiSchvalitVybrane.Text = "Schválit vybrané";
            this.tsmiSchvalitVybrane.Click += new System.EventHandler(this.tsmiSchvalitVybrane_Click);
            // 
            // tsmiUkoncitZakazkuKorekci
            // 
            this.tsmiUkoncitZakazkuKorekci.Name = "tsmiUkoncitZakazkuKorekci";
            this.tsmiUkoncitZakazkuKorekci.Size = new System.Drawing.Size(208, 22);
            this.tsmiUkoncitZakazkuKorekci.Text = "Ukončit zakázku / korekci";
            this.tsmiUkoncitZakazkuKorekci.Click += new System.EventHandler(this.tsmiUkoncitZakazkuKorekci_Click);
            // 
            // tsmiUpravit
            // 
            this.tsmiUpravit.Name = "tsmiUpravit";
            this.tsmiUpravit.Size = new System.Drawing.Size(208, 22);
            this.tsmiUpravit.Text = "Upravit";
            this.tsmiUpravit.Click += new System.EventHandler(this.tsmiUpravit_Click);
            // 
            // tsmiAkce
            // 
            this.tsmiAkce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiArchivaceVybrane});
            this.tsmiAkce.Name = "tsmiAkce";
            this.tsmiAkce.Size = new System.Drawing.Size(45, 20);
            this.tsmiAkce.Text = "Akce";
            // 
            // tsmiArchivaceVybrane
            // 
            this.tsmiArchivaceVybrane.Name = "tsmiArchivaceVybrane";
            this.tsmiArchivaceVybrane.Size = new System.Drawing.Size(184, 22);
            this.tsmiArchivaceVybrane.Text = "Archivace vybraných";
            this.tsmiArchivaceVybrane.Click += new System.EventHandler(this.tsmiArchivaceVybrane_Click);
            // 
            // progressIndicatorVyrobek
            // 
            this.progressIndicatorVyrobek.Location = new System.Drawing.Point(395, 346);
            this.progressIndicatorVyrobek.Name = "progressIndicatorVyrobek";
            this.progressIndicatorVyrobek.Percentage = 0F;
            this.progressIndicatorVyrobek.Size = new System.Drawing.Size(98, 98);
            this.progressIndicatorVyrobek.TabIndex = 105;
            this.progressIndicatorVyrobek.Text = "progressIndicator1";
            this.progressIndicatorVyrobek.Visible = false;
            // 
            // bw_VyrabenePolozky
            // 
            this.bw_VyrabenePolozky.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Production_DoWork);
            this.bw_VyrabenePolozky.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Production_RunWorkerCompleted);
            // 
            // bw_import
            // 
            this.bw_import.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_import_DoWork);
            this.bw_import.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_import_RunWorkerCompleted);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_Production_Count});
            this.toolStrip1.Location = new System.Drawing.Point(0, 607);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1045, 25);
            this.toolStrip1.TabIndex = 107;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tssl_Production_Count
            // 
            this.tssl_Production_Count.Name = "tssl_Production_Count";
            this.tssl_Production_Count.Size = new System.Drawing.Size(86, 22);
            this.tssl_Production_Count.Text = "toolStripLabel1";
            // 
            // bw_Schvaleni
            // 
            this.bw_Schvaleni.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Schvaleni_DoWork);
            this.bw_Schvaleni.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Schvaleni_RunWorkerCompleted);
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(1045, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(101, 632);
            this.panelButtons.TabIndex = 8;
            // 
            // advancedDataGridViewSearchToolBar1
            // 
            this.advancedDataGridViewSearchToolBar1.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar1.Location = new System.Drawing.Point(0, 261);
            this.advancedDataGridViewSearchToolBar1.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar1.Name = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar1.Size = new System.Drawing.Size(1045, 27);
            this.advancedDataGridViewSearchToolBar1.TabIndex = 106;
            this.advancedDataGridViewSearchToolBar1.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar1.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar1_Search);
            // 
            // dgVyrabenePolozky
            // 
            this.dgVyrabenePolozky.AllowUserToAddRows = false;
            this.dgVyrabenePolozky.AllowUserToDeleteRows = false;
            this.dgVyrabenePolozky.AllowUserToOrderColumns = true;
            this.dgVyrabenePolozky.AllowUserToResizeRows = false;
            this.dgVyrabenePolozky.AutoGenerateColumns = false;
            this.dgVyrabenePolozky.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVyrabenePolozky.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.activeDataGridViewTextBoxColumn,
            this.countEntriesDataGridViewTextBoxColumn,
            this.sOPNUMBEDataGridViewTextBoxColumn,
            this.sOPTYPEDataGridViewTextBoxColumn,
            this.sOPDESCDataGridViewTextBoxColumn,
            this.vNDDOCNMHDataGridViewTextBoxColumn,
            this.barcodeHDataGridViewTextBoxColumn,
            this.vPHLOCNCODEDataGridViewTextBoxColumn,
            this.dateProdDataGridViewTextBoxColumn,
            this.rez1DataGridViewTextBoxColumn,
            this.rez2DataGridViewTextBoxColumn,
            this.termIDDataGridViewTextBoxColumn,
            this.uSERIDDataGridViewTextBoxColumn,
            this.lSTModDataGridViewTextBoxColumn,
            this.dEXROWIDPDataGridViewTextBoxColumn,
            this.dEXROWIDVPPDataGridViewTextBoxColumn,
            this.countEntriesVPPDataGridViewTextBoxColumn,
            this.sOPNUMBEVPPDataGridViewTextBoxColumn,
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMTYPEDataGridViewTextBoxColumn,
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.iTEMMJDataGridViewTextBoxColumn,
            this.vNDDOCNMPDataGridViewTextBoxColumn,
            this.vNDITNUMDataGridViewTextBoxColumn,
            this.oRDDataGridViewTextBoxColumn,
            this.barcodePDataGridViewTextBoxColumn,
            this.barcodeTDataGridViewTextBoxColumn,
            this.lOCNCODEVPPDataGridViewTextBoxColumn,
            this.qTYSHPPDDataGridViewTextBoxColumn,
            this.qTYDOKONDataGridViewTextBoxColumn,
            this.qTYPACKDataGridViewTextBoxColumn,
            this.QTYODVEDENO,
            this.MnozstviZbyva,
            this.CNTODVEDENO,
            this.qTYPACKMJDataGridViewTextBoxColumn,
            this.tIMEMODEDataGridViewTextBoxColumn,
            this.tIMEPREPDataGridViewTextBoxColumn,
            this.tIMEUNITDataGridViewTextBoxColumn,
            this.dtProdTDataGridViewTextBoxColumn,
            this.dtProdLDataGridViewTextBoxColumn,
            this.serNumTDataGridViewTextBoxColumn,
            this.serNumLDataGridViewTextBoxColumn,
            this.verTDataGridViewTextBoxColumn,
            this.verLDataGridViewTextBoxColumn,
            this.termIDVPPDataGridViewTextBoxColumn,
            this.lSTModVPPDataGridViewTextBoxColumn,
            this.cZREZ1TrackDataGridViewTextBoxColumn,
            this.cZREZ2TrackDataGridViewTextBoxColumn,
            this.cZREZ3TrackDataGridViewTextBoxColumn,
            this.cZREZ4TrackDataGridViewTextBoxColumn,
            this.cZREZ5TrackDataGridViewTextBoxColumn,
            this.wEIGHTTARADataGridViewTextBoxColumn,
            this.wEIGHTNETTODataGridViewTextBoxColumn,
            this.wEIGHTTOLPLUSDataGridViewTextBoxColumn,
            this.wEIGHTTOLMINUSDataGridViewTextBoxColumn});
            this.dgVyrabenePolozky.DataSource = this.bsVyrabenePolozky;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgVyrabenePolozky.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgVyrabenePolozky.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVyrabenePolozky.EnableHeadersVisualStyles = false;
            this.dgVyrabenePolozky.FilterAndSortEnabled = true;
            this.dgVyrabenePolozky.Location = new System.Drawing.Point(0, 288);
            this.dgVyrabenePolozky.Name = "dgVyrabenePolozky";
            this.dgVyrabenePolozky.ReadOnly = true;
            this.dgVyrabenePolozky.RowHeadersVisible = false;
            this.dgVyrabenePolozky.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVyrabenePolozky.Size = new System.Drawing.Size(1045, 319);
            this.dgVyrabenePolozky.TabIndex = 30;
            this.dgVyrabenePolozky.SelectionChanged += new System.EventHandler(this.dgVyrobek_SelectionChanged);
            // 
            // bsVyrabenePolozky
            // 
            this.bsVyrabenePolozky.DataMember = "VyrabenePolozky";
            this.bsVyrabenePolozky.DataSource = this.bsVyrabenePolozky_00;
            // 
            // bsVyrabenePolozky_00
            // 
            this.bsVyrabenePolozky_00.DataSource = this.dsVyrabenePolozky;
            this.bsVyrabenePolozky_00.Position = 0;
            // 
            // dsVyrabenePolozky
            // 
            this.dsVyrabenePolozky.DataSetName = "VyrabenePolozkyDataSet";
            this.dsVyrabenePolozky.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // activeDataGridViewTextBoxColumn
            // 
            this.activeDataGridViewTextBoxColumn.DataPropertyName = "Active";
            this.activeDataGridViewTextBoxColumn.HeaderText = "Aktivní";
            this.activeDataGridViewTextBoxColumn.Name = "activeDataGridViewTextBoxColumn";
            this.activeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // countEntriesDataGridViewTextBoxColumn
            // 
            this.countEntriesDataGridViewTextBoxColumn.DataPropertyName = "CountEntries";
            this.countEntriesDataGridViewTextBoxColumn.HeaderText = "Číslo dávky";
            this.countEntriesDataGridViewTextBoxColumn.Name = "countEntriesDataGridViewTextBoxColumn";
            this.countEntriesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sOPNUMBEDataGridViewTextBoxColumn
            // 
            this.sOPNUMBEDataGridViewTextBoxColumn.DataPropertyName = "SOPNUMBE";
            this.sOPNUMBEDataGridViewTextBoxColumn.HeaderText = "Výrobní Zakázka";
            this.sOPNUMBEDataGridViewTextBoxColumn.Name = "sOPNUMBEDataGridViewTextBoxColumn";
            this.sOPNUMBEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sOPTYPEDataGridViewTextBoxColumn
            // 
            this.sOPTYPEDataGridViewTextBoxColumn.DataPropertyName = "SOPTYPE";
            this.sOPTYPEDataGridViewTextBoxColumn.HeaderText = "Typ zakázky";
            this.sOPTYPEDataGridViewTextBoxColumn.Name = "sOPTYPEDataGridViewTextBoxColumn";
            this.sOPTYPEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sOPDESCDataGridViewTextBoxColumn
            // 
            this.sOPDESCDataGridViewTextBoxColumn.DataPropertyName = "SOPDESC";
            this.sOPDESCDataGridViewTextBoxColumn.HeaderText = "Popis zakázky";
            this.sOPDESCDataGridViewTextBoxColumn.Name = "sOPDESCDataGridViewTextBoxColumn";
            this.sOPDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vNDDOCNMHDataGridViewTextBoxColumn
            // 
            this.vNDDOCNMHDataGridViewTextBoxColumn.DataPropertyName = "VNDDOCNMH";
            this.vNDDOCNMHDataGridViewTextBoxColumn.HeaderText = "Objednatel číslo";
            this.vNDDOCNMHDataGridViewTextBoxColumn.Name = "vNDDOCNMHDataGridViewTextBoxColumn";
            this.vNDDOCNMHDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // barcodeHDataGridViewTextBoxColumn
            // 
            this.barcodeHDataGridViewTextBoxColumn.DataPropertyName = "BarcodeH";
            this.barcodeHDataGridViewTextBoxColumn.HeaderText = "Č. kód";
            this.barcodeHDataGridViewTextBoxColumn.Name = "barcodeHDataGridViewTextBoxColumn";
            this.barcodeHDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vPHLOCNCODEDataGridViewTextBoxColumn
            // 
            this.vPHLOCNCODEDataGridViewTextBoxColumn.DataPropertyName = "LOCNCODE";
            this.vPHLOCNCODEDataGridViewTextBoxColumn.HeaderText = "Lokace";
            this.vPHLOCNCODEDataGridViewTextBoxColumn.Name = "vPHLOCNCODEDataGridViewTextBoxColumn";
            this.vPHLOCNCODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateProdDataGridViewTextBoxColumn
            // 
            this.dateProdDataGridViewTextBoxColumn.DataPropertyName = "DateProd";
            this.dateProdDataGridViewTextBoxColumn.HeaderText = "Oček. směna výroby";
            this.dateProdDataGridViewTextBoxColumn.Name = "dateProdDataGridViewTextBoxColumn";
            this.dateProdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rez1DataGridViewTextBoxColumn
            // 
            this.rez1DataGridViewTextBoxColumn.DataPropertyName = "Rez1";
            this.rez1DataGridViewTextBoxColumn.HeaderText = "Rezerva 1";
            this.rez1DataGridViewTextBoxColumn.Name = "rez1DataGridViewTextBoxColumn";
            this.rez1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rez2DataGridViewTextBoxColumn
            // 
            this.rez2DataGridViewTextBoxColumn.DataPropertyName = "Rez2";
            this.rez2DataGridViewTextBoxColumn.HeaderText = "Rezerva 2";
            this.rez2DataGridViewTextBoxColumn.Name = "rez2DataGridViewTextBoxColumn";
            this.rez2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // termIDDataGridViewTextBoxColumn
            // 
            this.termIDDataGridViewTextBoxColumn.DataPropertyName = "TermID";
            this.termIDDataGridViewTextBoxColumn.HeaderText = "ID terminál";
            this.termIDDataGridViewTextBoxColumn.Name = "termIDDataGridViewTextBoxColumn";
            this.termIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // uSERIDDataGridViewTextBoxColumn
            // 
            this.uSERIDDataGridViewTextBoxColumn.DataPropertyName = "USERID";
            this.uSERIDDataGridViewTextBoxColumn.HeaderText = "ID uživatele";
            this.uSERIDDataGridViewTextBoxColumn.Name = "uSERIDDataGridViewTextBoxColumn";
            this.uSERIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lSTModDataGridViewTextBoxColumn
            // 
            this.lSTModDataGridViewTextBoxColumn.DataPropertyName = "LSTMod";
            this.lSTModDataGridViewTextBoxColumn.HeaderText = "Posl. úprava hl.";
            this.lSTModDataGridViewTextBoxColumn.Name = "lSTModDataGridViewTextBoxColumn";
            this.lSTModDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dEXROWIDPDataGridViewTextBoxColumn
            // 
            this.dEXROWIDPDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID_P";
            this.dEXROWIDPDataGridViewTextBoxColumn.HeaderText = "Index V";
            this.dEXROWIDPDataGridViewTextBoxColumn.Name = "dEXROWIDPDataGridViewTextBoxColumn";
            this.dEXROWIDPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dEXROWIDVPPDataGridViewTextBoxColumn
            // 
            this.dEXROWIDVPPDataGridViewTextBoxColumn.DataPropertyName = "DEX_ROW_ID_VPP";
            this.dEXROWIDVPPDataGridViewTextBoxColumn.HeaderText = "Index N";
            this.dEXROWIDVPPDataGridViewTextBoxColumn.Name = "dEXROWIDVPPDataGridViewTextBoxColumn";
            this.dEXROWIDVPPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // countEntriesVPPDataGridViewTextBoxColumn
            // 
            this.countEntriesVPPDataGridViewTextBoxColumn.DataPropertyName = "CountEntries_VPP";
            this.countEntriesVPPDataGridViewTextBoxColumn.HeaderText = "Pol. číslo";
            this.countEntriesVPPDataGridViewTextBoxColumn.Name = "countEntriesVPPDataGridViewTextBoxColumn";
            this.countEntriesVPPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sOPNUMBEVPPDataGridViewTextBoxColumn
            // 
            this.sOPNUMBEVPPDataGridViewTextBoxColumn.DataPropertyName = "SOPNUMBE_VPP";
            this.sOPNUMBEVPPDataGridViewTextBoxColumn.HeaderText = "Výrobní Zakázka";
            this.sOPNUMBEVPPDataGridViewTextBoxColumn.Name = "sOPNUMBEVPPDataGridViewTextBoxColumn";
            this.sOPNUMBEVPPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "Pol. zboží číslo";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMTYPEDataGridViewTextBoxColumn
            // 
            this.iTEMTYPEDataGridViewTextBoxColumn.DataPropertyName = "ITEMTYPE";
            this.iTEMTYPEDataGridViewTextBoxColumn.HeaderText = "Typ položky";
            this.iTEMTYPEDataGridViewTextBoxColumn.Name = "iTEMTYPEDataGridViewTextBoxColumn";
            this.iTEMTYPEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Popis položky";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMMJDataGridViewTextBoxColumn
            // 
            this.iTEMMJDataGridViewTextBoxColumn.DataPropertyName = "ITEMMJ";
            this.iTEMMJDataGridViewTextBoxColumn.HeaderText = "Měrná jednotka";
            this.iTEMMJDataGridViewTextBoxColumn.Name = "iTEMMJDataGridViewTextBoxColumn";
            this.iTEMMJDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vNDDOCNMPDataGridViewTextBoxColumn
            // 
            this.vNDDOCNMPDataGridViewTextBoxColumn.DataPropertyName = "VNDDOCNMP";
            this.vNDDOCNMPDataGridViewTextBoxColumn.HeaderText = "Číslo dok. externí";
            this.vNDDOCNMPDataGridViewTextBoxColumn.Name = "vNDDOCNMPDataGridViewTextBoxColumn";
            this.vNDDOCNMPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vNDITNUMDataGridViewTextBoxColumn
            // 
            this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "Číslo položky externí";
            this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
            this.vNDITNUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // oRDDataGridViewTextBoxColumn
            // 
            this.oRDDataGridViewTextBoxColumn.DataPropertyName = "ORD";
            this.oRDDataGridViewTextBoxColumn.HeaderText = "Pořadí položky";
            this.oRDDataGridViewTextBoxColumn.Name = "oRDDataGridViewTextBoxColumn";
            this.oRDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // barcodePDataGridViewTextBoxColumn
            // 
            this.barcodePDataGridViewTextBoxColumn.DataPropertyName = "BarcodeP";
            this.barcodePDataGridViewTextBoxColumn.HeaderText = "Č. kód";
            this.barcodePDataGridViewTextBoxColumn.Name = "barcodePDataGridViewTextBoxColumn";
            this.barcodePDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // barcodeTDataGridViewTextBoxColumn
            // 
            this.barcodeTDataGridViewTextBoxColumn.DataPropertyName = "BarcodeT";
            this.barcodeTDataGridViewTextBoxColumn.HeaderText = "Č. kód Track";
            this.barcodeTDataGridViewTextBoxColumn.Name = "barcodeTDataGridViewTextBoxColumn";
            this.barcodeTDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lOCNCODEVPPDataGridViewTextBoxColumn
            // 
            this.lOCNCODEVPPDataGridViewTextBoxColumn.DataPropertyName = "LOCNCODE_VPP";
            this.lOCNCODEVPPDataGridViewTextBoxColumn.HeaderText = "Lokace";
            this.lOCNCODEVPPDataGridViewTextBoxColumn.Name = "lOCNCODEVPPDataGridViewTextBoxColumn";
            this.lOCNCODEVPPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYSHPPDDataGridViewTextBoxColumn
            // 
            this.qTYSHPPDDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPD";
            this.qTYSHPPDDataGridViewTextBoxColumn.HeaderText = "Množství";
            this.qTYSHPPDDataGridViewTextBoxColumn.Name = "qTYSHPPDDataGridViewTextBoxColumn";
            this.qTYSHPPDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYDOKONDataGridViewTextBoxColumn
            // 
            this.qTYDOKONDataGridViewTextBoxColumn.DataPropertyName = "QTYDOKON";
            this.qTYDOKONDataGridViewTextBoxColumn.HeaderText = "Množství dok. ruč. odvodem";
            this.qTYDOKONDataGridViewTextBoxColumn.Name = "qTYDOKONDataGridViewTextBoxColumn";
            this.qTYDOKONDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYPACKDataGridViewTextBoxColumn
            // 
            this.qTYPACKDataGridViewTextBoxColumn.DataPropertyName = "QTYPACK";
            this.qTYPACKDataGridViewTextBoxColumn.HeaderText = "Množství v balení";
            this.qTYPACKDataGridViewTextBoxColumn.Name = "qTYPACKDataGridViewTextBoxColumn";
            this.qTYPACKDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // QTYODVEDENO
            // 
            this.QTYODVEDENO.DataPropertyName = "QTYODVEDENO";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.QTYODVEDENO.DefaultCellStyle = dataGridViewCellStyle1;
            this.QTYODVEDENO.HeaderText = "Množství odvedeno";
            this.QTYODVEDENO.Name = "QTYODVEDENO";
            this.QTYODVEDENO.ReadOnly = true;
            // 
            // MnozstviZbyva
            // 
            this.MnozstviZbyva.DataPropertyName = "MnozstviZbyva";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.MnozstviZbyva.DefaultCellStyle = dataGridViewCellStyle2;
            this.MnozstviZbyva.HeaderText = "Množství zbývá";
            this.MnozstviZbyva.Name = "MnozstviZbyva";
            this.MnozstviZbyva.ReadOnly = true;
            // 
            // CNTODVEDENO
            // 
            this.CNTODVEDENO.DataPropertyName = "CNTODVEDENO";
            this.CNTODVEDENO.HeaderText = "Počet záznamů";
            this.CNTODVEDENO.Name = "CNTODVEDENO";
            this.CNTODVEDENO.ReadOnly = true;
            // 
            // qTYPACKMJDataGridViewTextBoxColumn
            // 
            this.qTYPACKMJDataGridViewTextBoxColumn.DataPropertyName = "QTYPACKMJ";
            this.qTYPACKMJDataGridViewTextBoxColumn.HeaderText = "Měrná jednotka balení";
            this.qTYPACKMJDataGridViewTextBoxColumn.Name = "qTYPACKMJDataGridViewTextBoxColumn";
            this.qTYPACKMJDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tIMEMODEDataGridViewTextBoxColumn
            // 
            this.tIMEMODEDataGridViewTextBoxColumn.DataPropertyName = "TIMEMODE";
            this.tIMEMODEDataGridViewTextBoxColumn.HeaderText = "Typ sledování";
            this.tIMEMODEDataGridViewTextBoxColumn.Name = "tIMEMODEDataGridViewTextBoxColumn";
            this.tIMEMODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tIMEPREPDataGridViewTextBoxColumn
            // 
            this.tIMEPREPDataGridViewTextBoxColumn.DataPropertyName = "TIMEPREP";
            this.tIMEPREPDataGridViewTextBoxColumn.HeaderText = "Přípravný čas";
            this.tIMEPREPDataGridViewTextBoxColumn.Name = "tIMEPREPDataGridViewTextBoxColumn";
            this.tIMEPREPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tIMEUNITDataGridViewTextBoxColumn
            // 
            this.tIMEUNITDataGridViewTextBoxColumn.DataPropertyName = "TIMEUNIT";
            this.tIMEUNITDataGridViewTextBoxColumn.HeaderText = "Jednotkový čas";
            this.tIMEUNITDataGridViewTextBoxColumn.Name = "tIMEUNITDataGridViewTextBoxColumn";
            this.tIMEUNITDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dtProdTDataGridViewTextBoxColumn
            // 
            this.dtProdTDataGridViewTextBoxColumn.DataPropertyName = "DtProdT";
            this.dtProdTDataGridViewTextBoxColumn.HeaderText = "Sledovat dat. výroby";
            this.dtProdTDataGridViewTextBoxColumn.Name = "dtProdTDataGridViewTextBoxColumn";
            this.dtProdTDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dtProdLDataGridViewTextBoxColumn
            // 
            this.dtProdLDataGridViewTextBoxColumn.DataPropertyName = "DtProdL";
            this.dtProdLDataGridViewTextBoxColumn.HeaderText = "Požad. rozs. pole dat. výroby";
            this.dtProdLDataGridViewTextBoxColumn.Name = "dtProdLDataGridViewTextBoxColumn";
            this.dtProdLDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // serNumTDataGridViewTextBoxColumn
            // 
            this.serNumTDataGridViewTextBoxColumn.DataPropertyName = "SerNumT";
            this.serNumTDataGridViewTextBoxColumn.HeaderText = "Sledovat SN";
            this.serNumTDataGridViewTextBoxColumn.Name = "serNumTDataGridViewTextBoxColumn";
            this.serNumTDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // serNumLDataGridViewTextBoxColumn
            // 
            this.serNumLDataGridViewTextBoxColumn.DataPropertyName = "SerNumL";
            this.serNumLDataGridViewTextBoxColumn.HeaderText = "Požad. rozs. pole SN";
            this.serNumLDataGridViewTextBoxColumn.Name = "serNumLDataGridViewTextBoxColumn";
            this.serNumLDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // verTDataGridViewTextBoxColumn
            // 
            this.verTDataGridViewTextBoxColumn.DataPropertyName = "VerT";
            this.verTDataGridViewTextBoxColumn.HeaderText = "Sledovat verzi";
            this.verTDataGridViewTextBoxColumn.Name = "verTDataGridViewTextBoxColumn";
            this.verTDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // verLDataGridViewTextBoxColumn
            // 
            this.verLDataGridViewTextBoxColumn.DataPropertyName = "VerL";
            this.verLDataGridViewTextBoxColumn.HeaderText = "Požad. rozs. pole verze";
            this.verLDataGridViewTextBoxColumn.Name = "verLDataGridViewTextBoxColumn";
            this.verLDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // termIDVPPDataGridViewTextBoxColumn
            // 
            this.termIDVPPDataGridViewTextBoxColumn.DataPropertyName = "TermID_VPP";
            this.termIDVPPDataGridViewTextBoxColumn.HeaderText = "ID terminál";
            this.termIDVPPDataGridViewTextBoxColumn.Name = "termIDVPPDataGridViewTextBoxColumn";
            this.termIDVPPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lSTModVPPDataGridViewTextBoxColumn
            // 
            this.lSTModVPPDataGridViewTextBoxColumn.DataPropertyName = "LSTMod_VPP";
            this.lSTModVPPDataGridViewTextBoxColumn.HeaderText = "Posl. úprava pol.";
            this.lSTModVPPDataGridViewTextBoxColumn.Name = "lSTModVPPDataGridViewTextBoxColumn";
            this.lSTModVPPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cZREZ1TrackDataGridViewTextBoxColumn
            // 
            this.cZREZ1TrackDataGridViewTextBoxColumn.DataPropertyName = "CZ_REZ1_Track";
            this.cZREZ1TrackDataGridViewTextBoxColumn.HeaderText = "REZ1 Příznak";
            this.cZREZ1TrackDataGridViewTextBoxColumn.Name = "cZREZ1TrackDataGridViewTextBoxColumn";
            this.cZREZ1TrackDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cZREZ2TrackDataGridViewTextBoxColumn
            // 
            this.cZREZ2TrackDataGridViewTextBoxColumn.DataPropertyName = "CZ_REZ2_Track";
            this.cZREZ2TrackDataGridViewTextBoxColumn.HeaderText = "REZ2 Příznak";
            this.cZREZ2TrackDataGridViewTextBoxColumn.Name = "cZREZ2TrackDataGridViewTextBoxColumn";
            this.cZREZ2TrackDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cZREZ3TrackDataGridViewTextBoxColumn
            // 
            this.cZREZ3TrackDataGridViewTextBoxColumn.DataPropertyName = "CZ_REZ3_Track";
            this.cZREZ3TrackDataGridViewTextBoxColumn.HeaderText = "REZ3 Příznak";
            this.cZREZ3TrackDataGridViewTextBoxColumn.Name = "cZREZ3TrackDataGridViewTextBoxColumn";
            this.cZREZ3TrackDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cZREZ4TrackDataGridViewTextBoxColumn
            // 
            this.cZREZ4TrackDataGridViewTextBoxColumn.DataPropertyName = "CZ_REZ4_Track";
            this.cZREZ4TrackDataGridViewTextBoxColumn.HeaderText = "REZ4 Příznak";
            this.cZREZ4TrackDataGridViewTextBoxColumn.Name = "cZREZ4TrackDataGridViewTextBoxColumn";
            this.cZREZ4TrackDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cZREZ5TrackDataGridViewTextBoxColumn
            // 
            this.cZREZ5TrackDataGridViewTextBoxColumn.DataPropertyName = "CZ_REZ5_Track";
            this.cZREZ5TrackDataGridViewTextBoxColumn.HeaderText = "REZ5 Příznak";
            this.cZREZ5TrackDataGridViewTextBoxColumn.Name = "cZREZ5TrackDataGridViewTextBoxColumn";
            this.cZREZ5TrackDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // wEIGHTTARADataGridViewTextBoxColumn
            // 
            this.wEIGHTTARADataGridViewTextBoxColumn.DataPropertyName = "WEIGHT_TARA";
            this.wEIGHTTARADataGridViewTextBoxColumn.HeaderText = "Váha obalu";
            this.wEIGHTTARADataGridViewTextBoxColumn.Name = "wEIGHTTARADataGridViewTextBoxColumn";
            this.wEIGHTTARADataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // wEIGHTNETTODataGridViewTextBoxColumn
            // 
            this.wEIGHTNETTODataGridViewTextBoxColumn.DataPropertyName = "WEIGHT_NETTO";
            this.wEIGHTNETTODataGridViewTextBoxColumn.HeaderText = "Váha materialu";
            this.wEIGHTNETTODataGridViewTextBoxColumn.Name = "wEIGHTNETTODataGridViewTextBoxColumn";
            this.wEIGHTNETTODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // wEIGHTTOLPLUSDataGridViewTextBoxColumn
            // 
            this.wEIGHTTOLPLUSDataGridViewTextBoxColumn.DataPropertyName = "WEIGHT_TOL_PLUS";
            this.wEIGHTTOLPLUSDataGridViewTextBoxColumn.HeaderText = "Váha tolerance plus";
            this.wEIGHTTOLPLUSDataGridViewTextBoxColumn.Name = "wEIGHTTOLPLUSDataGridViewTextBoxColumn";
            this.wEIGHTTOLPLUSDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // wEIGHTTOLMINUSDataGridViewTextBoxColumn
            // 
            this.wEIGHTTOLMINUSDataGridViewTextBoxColumn.DataPropertyName = "WEIGHT_TOL_MINUS";
            this.wEIGHTTOLMINUSDataGridViewTextBoxColumn.HeaderText = "Váha tolerance minus";
            this.wEIGHTTOLMINUSDataGridViewTextBoxColumn.Name = "wEIGHTTOLMINUSDataGridViewTextBoxColumn";
            this.wEIGHTTOLMINUSDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FormVyrabenePolozky
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 632);
            this.Controls.Add(this.progressIndicatorVyrobek);
            this.Controls.Add(this.dgVyrabenePolozky);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.advancedDataGridViewSearchToolBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormVyrabenePolozky";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Přehled výrobky";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProductionList_FormClosing);
            this.Load += new System.EventHandler(this.FormProductionList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormProductionList_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVyrabenePolozky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVyrabenePolozky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVyrabenePolozky_00)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVyrabenePolozky)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public Fask.Interfaces.DataSets.Hlavni dsVyrabenePolozky;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonVyhledat;
        private System.Windows.Forms.Button buttonOdznacitVse;
        private System.Windows.Forms.Button buttonOznacitVse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private ProgressControls.ProgressIndicator progressIndicatorVyrobek;
        private System.ComponentModel.BackgroundWorker bw_VyrabenePolozky;
        private System.ComponentModel.BackgroundWorker bw_import;
        private System.Windows.Forms.ToolStripMenuItem tsmiExporty;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExceOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private System.Windows.Forms.ToolStripMenuItem tsmiPolozka;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportovatPrijemku;
        private System.Windows.Forms.ToolStripMenuItem tsmiSchvalitVybrane;
        private System.Windows.Forms.ToolStripMenuItem tsmiUkoncitZakazkuKorekci;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpravit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tssl_Production_Count;
        protected System.Windows.Forms.ToolStrip tsFiltry;
        protected System.Windows.Forms.ToolStripLabel toolStripLabel1;
        protected System.Windows.Forms.ToolStripComboBox tscbFiltry;
        protected System.Windows.Forms.ToolStripButton tsbNastavit;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        protected System.Windows.Forms.ToolStripButton tsbZmena;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        protected System.Windows.Forms.ToolStripButton tsbPridat;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        protected System.Windows.Forms.ToolStripButton tsbOdebrat;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        protected System.Windows.Forms.ToolStripButton tsbVycistit;
        private System.Windows.Forms.ToolStripMenuItem tiskEtiketToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.ComponentModel.BackgroundWorker bw_Schvaleni;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tB_VNDITNUM;
        private System.Windows.Forms.ToolStripMenuItem tiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAkce;
        private System.Windows.Forms.ToolStripMenuItem tsmiArchivaceVybrane;
        private System.Windows.Forms.BindingSource bsVyrabenePolozky_00;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar1;
        private Zuby.ADGV.AdvancedDataGridView dgVyrabenePolozky;
        private System.Windows.Forms.BindingSource bsVyrabenePolozky;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tB_CountEntries_VPP;
        private System.Windows.Forms.TextBox tB_ITEMDESC;
        private System.Windows.Forms.TextBox tB_SOPNUMBE;
        private System.Windows.Forms.TextBox tB_DateProd_OD;
        private System.Windows.Forms.TextBox tB_CountEntries;
        private System.Windows.Forms.CheckBox chB_Ukonceno;
        private System.Windows.Forms.CheckBox chB_Neaktivni;
        private System.Windows.Forms.CheckBox chB_Aktivni;
        private System.Windows.Forms.CheckBox chB_Nezrealizovane;
        private System.Windows.Forms.TextBox tB_DateProd_DO;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn activeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPNUMBEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPTYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDDOCNMHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodeHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vPHLOCNCODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateProdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rez1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rez2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn termIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lSTModDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDVPPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countEntriesVPPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sOPNUMBEVPPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMTYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMMJDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDDOCNMPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vNDITNUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oRDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodePDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcodeTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOCNCODEVPPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYSHPPDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYDOKONDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYPACKDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYODVEDENO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MnozstviZbyva;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNTODVEDENO;
        private System.Windows.Forms.DataGridViewTextBoxColumn qTYPACKMJDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMEMODEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMEPREPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIMEUNITDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtProdTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtProdLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serNumTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serNumLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn verTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn verLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn termIDVPPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lSTModVPPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cZREZ1TrackDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cZREZ2TrackDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cZREZ3TrackDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cZREZ4TrackDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cZREZ5TrackDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wEIGHTTARADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wEIGHTNETTODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wEIGHTTOLPLUSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wEIGHTTOLMINUSDataGridViewTextBoxColumn;
    }
}