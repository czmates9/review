
using System;
using System.Windows.Forms;

namespace Konzola.SkladLokace
{
    partial class FormSKladLokace_PorovnaniVuciIS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSKladLokace_PorovnaniVuciIS));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Konec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExporty = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExceOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportyFASK = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVVse_FASK = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene_FASK = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse_FASK = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExceOznacene_FASK = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse_FASK = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene_FASK = new System.Windows.Forms.ToolStripMenuItem();
            this.zobrazitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_IS_Pohyby = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_LokMechStav = new System.Windows.Forms.ToolStripMenuItem();
            this.logikaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_poloAutomat = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chb_V_3 = new System.Windows.Forms.CheckBox();
            this.chb_V_2 = new System.Windows.Forms.CheckBox();
            this.chb_V_1 = new System.Windows.Forms.CheckBox();
            this.chb_V_0 = new System.Windows.Forms.CheckBox();
            this.chb_V_6 = new System.Windows.Forms.CheckBox();
            this.chb_V_5 = new System.Windows.Forms.CheckBox();
            this.chb_V_4 = new System.Windows.Forms.CheckBox();
            this.chb_SarzeSN_R = new System.Windows.Forms.CheckBox();
            this.chb_NazevPolozky_R = new System.Windows.Forms.CheckBox();
            this.chb_KodPolozky_R = new System.Windows.Forms.CheckBox();
            this.chb_SarzeSN_L = new System.Windows.Forms.CheckBox();
            this.chb_NazevPolozky_L = new System.Windows.Forms.CheckBox();
            this.chb_KodPolozky_L = new System.Windows.Forms.CheckBox();
            this.tb_SarzeSN = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_NazevPolozky = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_KodPolozky = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.tb_SKL_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonVyhledat = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dg_IS = new Zuby.ADGV.AdvancedDataGridView();
            this.iTEMNMBRDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMCODEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYSHPPD_Pohoda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mJDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERLTNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eXPIRACEDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skldescDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_IS = new System.Windows.Forms.BindingSource(this.components);
            this.ds_IS = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_IS_Count = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressIndicator_IS = new ProgressControls.ProgressIndicator();
            this.dg_SearchToolBar_IS = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.label5 = new System.Windows.Forms.Label();
            this.dg_FASK = new Zuby.ADGV.AdvancedDataGridView();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMCODEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qTYSHPPDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eXPIRACEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skldescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_FASK = new System.Windows.Forms.BindingSource(this.components);
            this.ds_FASK = new Fask.Interfaces.DataSets.SkladLokace_CompareToIS();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.tssl_FASK_Count = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressIndicator_FASK = new ProgressControls.ProgressIndicator();
            this.dg_SearchToolBar_FASK = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.label6 = new System.Windows.Forms.Label();
            this.bw_LoadData = new System.ComponentModel.BackgroundWorker();
            this.bw_PoloAutomat = new System.ComponentModel.BackgroundWorker();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_IS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_IS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_IS)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_FASK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_FASK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_FASK)).BeginInit();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.tsmiExporty,
            this.tsmiExportyFASK,
            this.zobrazitToolStripMenuItem,
            this.logikaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1064, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Konec});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // tsmi_Konec
            // 
            this.tsmi_Konec.Name = "tsmi_Konec";
            this.tsmi_Konec.Size = new System.Drawing.Size(107, 22);
            this.tsmi_Konec.Text = "Konec";
            this.tsmi_Konec.Click += new System.EventHandler(this.tsmi_Konec_Click);
            // 
            // tsmiExporty
            // 
            this.tsmiExporty.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportDoCSVVse,
            this.tsmiExportDoCSVOznacene,
            this.toolStripSeparator6,
            this.tsmiExportDoExcelVse,
            this.tsmiExportDoExceOznacene,
            this.toolStripSeparator7,
            this.tsmiExportDoXMLVse,
            this.tsmiExportDoXMLOznacene});
            this.tsmiExporty.Name = "tsmiExporty";
            this.tsmiExporty.Size = new System.Drawing.Size(67, 20);
            this.tsmiExporty.Text = "Výstup IS";
            // 
            // tsmiExportDoCSVVse
            // 
            this.tsmiExportDoCSVVse.Name = "tsmiExportDoCSVVse";
            this.tsmiExportDoCSVVse.Size = new System.Drawing.Size(220, 22);
            this.tsmiExportDoCSVVse.Text = "Export do CSV vše IS";
            this.tsmiExportDoCSVVse.Click += new System.EventHandler(this.exportDoCSVVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoCSVOznacene
            // 
            this.tsmiExportDoCSVOznacene.Name = "tsmiExportDoCSVOznacene";
            this.tsmiExportDoCSVOznacene.Size = new System.Drawing.Size(220, 22);
            this.tsmiExportDoCSVOznacene.Text = "Export do CSV označené IS";
            this.tsmiExportDoCSVOznacene.Click += new System.EventHandler(this.exportDoCSVOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(217, 6);
            // 
            // tsmiExportDoExcelVse
            // 
            this.tsmiExportDoExcelVse.Name = "tsmiExportDoExcelVse";
            this.tsmiExportDoExcelVse.Size = new System.Drawing.Size(220, 22);
            this.tsmiExportDoExcelVse.Text = "Export do Excel vše IS";
            this.tsmiExportDoExcelVse.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoExceOznacene
            // 
            this.tsmiExportDoExceOznacene.Name = "tsmiExportDoExceOznacene";
            this.tsmiExportDoExceOznacene.Size = new System.Drawing.Size(220, 22);
            this.tsmiExportDoExceOznacene.Text = "Export do Excel označené IS";
            this.tsmiExportDoExceOznacene.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(217, 6);
            // 
            // tsmiExportDoXMLVse
            // 
            this.tsmiExportDoXMLVse.Name = "tsmiExportDoXMLVse";
            this.tsmiExportDoXMLVse.Size = new System.Drawing.Size(220, 22);
            this.tsmiExportDoXMLVse.Text = "Export do XML Vše IS";
            this.tsmiExportDoXMLVse.Click += new System.EventHandler(this.exportDoXMLVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoXMLOznacene
            // 
            this.tsmiExportDoXMLOznacene.Name = "tsmiExportDoXMLOznacene";
            this.tsmiExportDoXMLOznacene.Size = new System.Drawing.Size(220, 22);
            this.tsmiExportDoXMLOznacene.Text = "Export do XML označené IS";
            this.tsmiExportDoXMLOznacene.Click += new System.EventHandler(this.exportDoXMLOznaceneToolStripMenuItem_Click);
            // 
            // tsmiExportyFASK
            // 
            this.tsmiExportyFASK.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportDoCSVVse_FASK,
            this.tsmiExportDoCSVOznacene_FASK,
            this.toolStripSeparator1,
            this.tsmiExportDoExcelVse_FASK,
            this.tsmiExportDoExceOznacene_FASK,
            this.toolStripSeparator8,
            this.tsmiExportDoXMLVse_FASK,
            this.tsmiExportDoXMLOznacene_FASK});
            this.tsmiExportyFASK.Name = "tsmiExportyFASK";
            this.tsmiExportyFASK.Size = new System.Drawing.Size(107, 20);
            this.tsmiExportyFASK.Text = "Výstup LokMech";
            // 
            // tsmiExportDoCSVVse_FASK
            // 
            this.tsmiExportDoCSVVse_FASK.Name = "tsmiExportDoCSVVse_FASK";
            this.tsmiExportDoCSVVse_FASK.Size = new System.Drawing.Size(260, 22);
            this.tsmiExportDoCSVVse_FASK.Text = "Export do CSV vše LokMech";
            this.tsmiExportDoCSVVse_FASK.Click += new System.EventHandler(this.exportDoCSVVseToolStripMenuItem_FASK_Click);
            // 
            // tsmiExportDoCSVOznacene_FASK
            // 
            this.tsmiExportDoCSVOznacene_FASK.Name = "tsmiExportDoCSVOznacene_FASK";
            this.tsmiExportDoCSVOznacene_FASK.Size = new System.Drawing.Size(260, 22);
            this.tsmiExportDoCSVOznacene_FASK.Text = "Export do CSV označené LokMech";
            this.tsmiExportDoCSVOznacene_FASK.Click += new System.EventHandler(this.exportDoCSVOznaceneToolStripMenuItem_FASK_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(257, 6);
            // 
            // tsmiExportDoExcelVse_FASK
            // 
            this.tsmiExportDoExcelVse_FASK.Name = "tsmiExportDoExcelVse_FASK";
            this.tsmiExportDoExcelVse_FASK.Size = new System.Drawing.Size(260, 22);
            this.tsmiExportDoExcelVse_FASK.Text = "Export do Excel vše LokMech";
            this.tsmiExportDoExcelVse_FASK.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_FASK_Click);
            // 
            // tsmiExportDoExceOznacene_FASK
            // 
            this.tsmiExportDoExceOznacene_FASK.Name = "tsmiExportDoExceOznacene_FASK";
            this.tsmiExportDoExceOznacene_FASK.Size = new System.Drawing.Size(260, 22);
            this.tsmiExportDoExceOznacene_FASK.Text = "Export do Excel označené LokMech";
            this.tsmiExportDoExceOznacene_FASK.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_FASK_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(257, 6);
            // 
            // tsmiExportDoXMLVse_FASK
            // 
            this.tsmiExportDoXMLVse_FASK.Name = "tsmiExportDoXMLVse_FASK";
            this.tsmiExportDoXMLVse_FASK.Size = new System.Drawing.Size(260, 22);
            this.tsmiExportDoXMLVse_FASK.Text = "Export do XML Vše LokMech";
            this.tsmiExportDoXMLVse_FASK.Click += new System.EventHandler(this.exportDoXMLVseToolStripMenuItem_FASK_Click);
            // 
            // tsmiExportDoXMLOznacene_FASK
            // 
            this.tsmiExportDoXMLOznacene_FASK.Name = "tsmiExportDoXMLOznacene_FASK";
            this.tsmiExportDoXMLOznacene_FASK.Size = new System.Drawing.Size(260, 22);
            this.tsmiExportDoXMLOznacene_FASK.Text = "Export do XML označené LokMech";
            this.tsmiExportDoXMLOznacene_FASK.Click += new System.EventHandler(this.exportDoXMLOznaceneToolStripMenuItem_FASK_Click);
            // 
            // zobrazitToolStripMenuItem
            // 
            this.zobrazitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_IS_Pohyby,
            this.tsmi_LokMechStav});
            this.zobrazitToolStripMenuItem.Name = "zobrazitToolStripMenuItem";
            this.zobrazitToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.zobrazitToolStripMenuItem.Text = "Zobrazit";
            // 
            // tsmi_IS_Pohyby
            // 
            this.tsmi_IS_Pohyby.Name = "tsmi_IS_Pohyby";
            this.tsmi_IS_Pohyby.Size = new System.Drawing.Size(216, 22);
            this.tsmi_IS_Pohyby.Text = "Pohyby IS";
            this.tsmi_IS_Pohyby.Click += new System.EventHandler(this.tsmi_IS_Pohyby_Click);
            // 
            // tsmi_LokMechStav
            // 
            this.tsmi_LokMechStav.Name = "tsmi_LokMechStav";
            this.tsmi_LokMechStav.Size = new System.Drawing.Size(216, 22);
            this.tsmi_LokMechStav.Text = "Lokační mechanismus stav";
            this.tsmi_LokMechStav.Click += new System.EventHandler(this.tsmi_LokMechStav_Click);
            // 
            // logikaToolStripMenuItem
            // 
            this.logikaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_poloAutomat});
            this.logikaToolStripMenuItem.Name = "logikaToolStripMenuItem";
            this.logikaToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.logikaToolStripMenuItem.Text = "Logika";
            // 
            // tsmi_poloAutomat
            // 
            this.tsmi_poloAutomat.Name = "tsmi_poloAutomat";
            this.tsmi_poloAutomat.Size = new System.Drawing.Size(260, 22);
            this.tsmi_poloAutomat.Text = "Poloautomatická korekce LokMech";
            this.tsmi_poloAutomat.Click += new System.EventHandler(this.tsmi_poloAutomat_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chb_V_3);
            this.panel1.Controls.Add(this.chb_V_2);
            this.panel1.Controls.Add(this.chb_V_1);
            this.panel1.Controls.Add(this.chb_V_0);
            this.panel1.Controls.Add(this.chb_V_6);
            this.panel1.Controls.Add(this.chb_V_5);
            this.panel1.Controls.Add(this.chb_V_4);
            this.panel1.Controls.Add(this.chb_SarzeSN_R);
            this.panel1.Controls.Add(this.chb_NazevPolozky_R);
            this.panel1.Controls.Add(this.chb_KodPolozky_R);
            this.panel1.Controls.Add(this.chb_SarzeSN_L);
            this.panel1.Controls.Add(this.chb_NazevPolozky_L);
            this.panel1.Controls.Add(this.chb_KodPolozky_L);
            this.panel1.Controls.Add(this.tb_SarzeSN);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tb_NazevPolozky);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tb_KodPolozky);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tsFiltry);
            this.panel1.Controls.Add(this.tb_SKL_ID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonVyhledat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(942, 171);
            this.panel1.TabIndex = 2;
            // 
            // chb_V_3
            // 
            this.chb_V_3.AutoSize = true;
            this.chb_V_3.Location = new System.Drawing.Point(517, 84);
            this.chb_V_3.Name = "chb_V_3";
            this.chb_V_3.Size = new System.Drawing.Size(74, 17);
            this.chb_V_3.TabIndex = 60;
            this.chb_V_3.Text = "Varianta 3";
            this.chb_V_3.UseVisualStyleBackColor = true;
            // 
            // chb_V_2
            // 
            this.chb_V_2.AutoSize = true;
            this.chb_V_2.Checked = true;
            this.chb_V_2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_V_2.Location = new System.Drawing.Point(517, 61);
            this.chb_V_2.Name = "chb_V_2";
            this.chb_V_2.Size = new System.Drawing.Size(74, 17);
            this.chb_V_2.TabIndex = 59;
            this.chb_V_2.Text = "Varianta 2";
            this.chb_V_2.UseVisualStyleBackColor = true;
            // 
            // chb_V_1
            // 
            this.chb_V_1.AutoSize = true;
            this.chb_V_1.Checked = true;
            this.chb_V_1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_V_1.Location = new System.Drawing.Point(517, 41);
            this.chb_V_1.Name = "chb_V_1";
            this.chb_V_1.Size = new System.Drawing.Size(74, 17);
            this.chb_V_1.TabIndex = 58;
            this.chb_V_1.Text = "Varianta 1";
            this.chb_V_1.UseVisualStyleBackColor = true;
            // 
            // chb_V_0
            // 
            this.chb_V_0.AutoSize = true;
            this.chb_V_0.Checked = true;
            this.chb_V_0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_V_0.Location = new System.Drawing.Point(437, 41);
            this.chb_V_0.Name = "chb_V_0";
            this.chb_V_0.Size = new System.Drawing.Size(74, 17);
            this.chb_V_0.TabIndex = 57;
            this.chb_V_0.Text = "Varianta 0";
            this.chb_V_0.UseVisualStyleBackColor = true;
            // 
            // chb_V_6
            // 
            this.chb_V_6.AutoSize = true;
            this.chb_V_6.Location = new System.Drawing.Point(597, 84);
            this.chb_V_6.Name = "chb_V_6";
            this.chb_V_6.Size = new System.Drawing.Size(74, 17);
            this.chb_V_6.TabIndex = 56;
            this.chb_V_6.Text = "Varianta 6";
            this.chb_V_6.UseVisualStyleBackColor = true;
            // 
            // chb_V_5
            // 
            this.chb_V_5.AutoSize = true;
            this.chb_V_5.Checked = true;
            this.chb_V_5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_V_5.Location = new System.Drawing.Point(597, 61);
            this.chb_V_5.Name = "chb_V_5";
            this.chb_V_5.Size = new System.Drawing.Size(74, 17);
            this.chb_V_5.TabIndex = 55;
            this.chb_V_5.Text = "Varianta 5";
            this.chb_V_5.UseVisualStyleBackColor = true;
            // 
            // chb_V_4
            // 
            this.chb_V_4.AutoSize = true;
            this.chb_V_4.Checked = true;
            this.chb_V_4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_V_4.Location = new System.Drawing.Point(597, 41);
            this.chb_V_4.Name = "chb_V_4";
            this.chb_V_4.Size = new System.Drawing.Size(74, 17);
            this.chb_V_4.TabIndex = 54;
            this.chb_V_4.Text = "Varianta 4";
            this.chb_V_4.UseVisualStyleBackColor = true;
            // 
            // chb_SarzeSN_R
            // 
            this.chb_SarzeSN_R.AutoSize = true;
            this.chb_SarzeSN_R.Location = new System.Drawing.Point(373, 119);
            this.chb_SarzeSN_R.Name = "chb_SarzeSN_R";
            this.chb_SarzeSN_R.Size = new System.Drawing.Size(15, 14);
            this.chb_SarzeSN_R.TabIndex = 53;
            this.chb_SarzeSN_R.UseVisualStyleBackColor = true;
            this.chb_SarzeSN_R.MouseLeave += new System.EventHandler(this.chb_R_MouseLeave);
            this.chb_SarzeSN_R.MouseHover += new System.EventHandler(this.chb_R_MouseHover);
            // 
            // chb_NazevPolozky_R
            // 
            this.chb_NazevPolozky_R.AutoSize = true;
            this.chb_NazevPolozky_R.Location = new System.Drawing.Point(373, 95);
            this.chb_NazevPolozky_R.Name = "chb_NazevPolozky_R";
            this.chb_NazevPolozky_R.Size = new System.Drawing.Size(15, 14);
            this.chb_NazevPolozky_R.TabIndex = 52;
            this.chb_NazevPolozky_R.UseVisualStyleBackColor = true;
            this.chb_NazevPolozky_R.MouseLeave += new System.EventHandler(this.chb_R_MouseLeave);
            this.chb_NazevPolozky_R.MouseHover += new System.EventHandler(this.chb_R_MouseHover);
            // 
            // chb_KodPolozky_R
            // 
            this.chb_KodPolozky_R.AutoSize = true;
            this.chb_KodPolozky_R.Location = new System.Drawing.Point(373, 71);
            this.chb_KodPolozky_R.Name = "chb_KodPolozky_R";
            this.chb_KodPolozky_R.Size = new System.Drawing.Size(15, 14);
            this.chb_KodPolozky_R.TabIndex = 51;
            this.chb_KodPolozky_R.UseVisualStyleBackColor = true;
            this.chb_KodPolozky_R.MouseLeave += new System.EventHandler(this.chb_R_MouseLeave);
            this.chb_KodPolozky_R.MouseHover += new System.EventHandler(this.chb_R_MouseHover);
            // 
            // chb_SarzeSN_L
            // 
            this.chb_SarzeSN_L.AutoSize = true;
            this.chb_SarzeSN_L.Location = new System.Drawing.Point(99, 119);
            this.chb_SarzeSN_L.Name = "chb_SarzeSN_L";
            this.chb_SarzeSN_L.Size = new System.Drawing.Size(15, 14);
            this.chb_SarzeSN_L.TabIndex = 50;
            this.chb_SarzeSN_L.UseVisualStyleBackColor = true;
            this.chb_SarzeSN_L.MouseLeave += new System.EventHandler(this.chb_L_MouseLeave);
            this.chb_SarzeSN_L.MouseHover += new System.EventHandler(this.chb_L_MouseHover);
            // 
            // chb_NazevPolozky_L
            // 
            this.chb_NazevPolozky_L.AutoSize = true;
            this.chb_NazevPolozky_L.Location = new System.Drawing.Point(99, 95);
            this.chb_NazevPolozky_L.Name = "chb_NazevPolozky_L";
            this.chb_NazevPolozky_L.Size = new System.Drawing.Size(15, 14);
            this.chb_NazevPolozky_L.TabIndex = 49;
            this.chb_NazevPolozky_L.UseVisualStyleBackColor = true;
            this.chb_NazevPolozky_L.MouseLeave += new System.EventHandler(this.chb_L_MouseLeave);
            this.chb_NazevPolozky_L.MouseHover += new System.EventHandler(this.chb_L_MouseHover);
            // 
            // chb_KodPolozky_L
            // 
            this.chb_KodPolozky_L.AutoSize = true;
            this.chb_KodPolozky_L.Location = new System.Drawing.Point(99, 71);
            this.chb_KodPolozky_L.Name = "chb_KodPolozky_L";
            this.chb_KodPolozky_L.Size = new System.Drawing.Size(15, 14);
            this.chb_KodPolozky_L.TabIndex = 48;
            this.chb_KodPolozky_L.UseVisualStyleBackColor = true;
            this.chb_KodPolozky_L.MouseLeave += new System.EventHandler(this.chb_L_MouseLeave);
            this.chb_KodPolozky_L.MouseHover += new System.EventHandler(this.chb_L_MouseHover);
            // 
            // tb_SarzeSN
            // 
            this.tb_SarzeSN.Location = new System.Drawing.Point(120, 116);
            this.tb_SarzeSN.Name = "tb_SarzeSN";
            this.tb_SarzeSN.Size = new System.Drawing.Size(247, 20);
            this.tb_SarzeSN.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Šarže / SN:";
            // 
            // tb_NazevPolozky
            // 
            this.tb_NazevPolozky.Location = new System.Drawing.Point(120, 92);
            this.tb_NazevPolozky.Name = "tb_NazevPolozky";
            this.tb_NazevPolozky.Size = new System.Drawing.Size(247, 20);
            this.tb_NazevPolozky.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Název položky:";
            // 
            // tb_KodPolozky
            // 
            this.tb_KodPolozky.Location = new System.Drawing.Point(120, 68);
            this.tb_KodPolozky.Name = "tb_KodPolozky";
            this.tb_KodPolozky.Size = new System.Drawing.Size(247, 20);
            this.tb_KodPolozky.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Kód položky:";
            // 
            // tsFiltry
            // 
            this.tsFiltry.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
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
            this.tsFiltry.Location = new System.Drawing.Point(0, 0);
            this.tsFiltry.Name = "tsFiltry";
            this.tsFiltry.Size = new System.Drawing.Size(942, 25);
            this.tsFiltry.TabIndex = 41;
            this.tsFiltry.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Filtry:";
            // 
            // tscbFiltry
            // 
            this.tscbFiltry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry.DropDownWidth = 170;
            this.tscbFiltry.Name = "tscbFiltry";
            this.tscbFiltry.Size = new System.Drawing.Size(170, 25);
            // 
            // tsbNastavit
            // 
            this.tsbNastavit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit.Image")));
            this.tsbNastavit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit.Name = "tsbNastavit";
            this.tsbNastavit.Size = new System.Drawing.Size(23, 22);
            this.tsbNastavit.ToolTipText = "Nastavit";
            this.tsbNastavit.Click += new System.EventHandler(this.tsbNastavit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbZmena
            // 
            this.tsbZmena.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena.Image")));
            this.tsbZmena.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena.Name = "tsbZmena";
            this.tsbZmena.Size = new System.Drawing.Size(23, 22);
            this.tsbZmena.Text = "Změna";
            this.tsbZmena.Click += new System.EventHandler(this.tsbZmena_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPridat
            // 
            this.tsbPridat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat.Image")));
            this.tsbPridat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat.Name = "tsbPridat";
            this.tsbPridat.Size = new System.Drawing.Size(23, 22);
            this.tsbPridat.Text = "Uložit";
            this.tsbPridat.Click += new System.EventHandler(this.tsbPridat_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOdebrat
            // 
            this.tsbOdebrat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat.Image")));
            this.tsbOdebrat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat.Name = "tsbOdebrat";
            this.tsbOdebrat.Size = new System.Drawing.Size(23, 22);
            this.tsbOdebrat.Text = "Odebrat";
            this.tsbOdebrat.Click += new System.EventHandler(this.tsbOdebrat_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbVycistit
            // 
            this.tsbVycistit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit.Image")));
            this.tsbVycistit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit.Name = "tsbVycistit";
            this.tsbVycistit.Size = new System.Drawing.Size(23, 22);
            this.tsbVycistit.Text = "Vyčistit";
            this.tsbVycistit.Click += new System.EventHandler(this.tsbVycistit_Click);
            // 
            // tb_SKL_ID
            // 
            this.tb_SKL_ID.Location = new System.Drawing.Point(98, 42);
            this.tb_SKL_ID.Name = "tb_SKL_ID";
            this.tb_SKL_ID.Size = new System.Drawing.Size(269, 20);
            this.tb_SKL_ID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID Skladu:";
            // 
            // buttonVyhledat
            // 
            this.buttonVyhledat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyhledat.Location = new System.Drawing.Point(817, 42);
            this.buttonVyhledat.Name = "buttonVyhledat";
            this.buttonVyhledat.Size = new System.Drawing.Size(107, 89);
            this.buttonVyhledat.TabIndex = 0;
            this.buttonVyhledat.Text = "Vyhledat";
            this.buttonVyhledat.UseVisualStyleBackColor = true;
            this.buttonVyhledat.Click += new System.EventHandler(this.buttonVyhledat_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 195);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dg_IS);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.progressIndicator_IS);
            this.splitContainer1.Panel1.Controls.Add(this.dg_SearchToolBar_IS);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dg_FASK);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip2);
            this.splitContainer1.Panel2.Controls.Add(this.progressIndicator_FASK);
            this.splitContainer1.Panel2.Controls.Add(this.dg_SearchToolBar_FASK);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Size = new System.Drawing.Size(942, 486);
            this.splitContainer1.SplitterDistance = 471;
            this.splitContainer1.TabIndex = 3;
            // 
            // dg_IS
            // 
            this.dg_IS.AllowUserToAddRows = false;
            this.dg_IS.AllowUserToDeleteRows = false;
            this.dg_IS.AllowUserToOrderColumns = true;
            this.dg_IS.AllowUserToResizeRows = false;
            this.dg_IS.AutoGenerateColumns = false;
            this.dg_IS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_IS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMNMBRDataGridViewTextBoxColumn1,
            this.iTEMCODEDataGridViewTextBoxColumn1,
            this.iTEMDESCDataGridViewTextBoxColumn1,
            this.vNDITNUMDataGridViewTextBoxColumn1,
            this.QTYSHPPD_Pohoda,
            this.mJDataGridViewTextBoxColumn1,
            this.SERLTNUM,
            this.eXPIRACEDataGridViewTextBoxColumn1,
            this.sKLIDDataGridViewTextBoxColumn1,
            this.skldescDataGridViewTextBoxColumn1,
            this.status});
            this.dg_IS.DataSource = this.bs_IS;
            this.dg_IS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_IS.EnableHeadersVisualStyles = false;
            this.dg_IS.FilterAndSortEnabled = true;
            this.dg_IS.Location = new System.Drawing.Point(0, 67);
            this.dg_IS.Name = "dg_IS";
            this.dg_IS.ReadOnly = true;
            this.dg_IS.RowHeadersVisible = false;
            this.dg_IS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_IS.Size = new System.Drawing.Size(471, 397);
            this.dg_IS.TabIndex = 1;
            this.dg_IS.TabStop = false;
            this.dg_IS.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_IS_CellFormatting);
            this.dg_IS.SelectionChanged += new System.EventHandler(this.dg_IS_SelectionChanged);
            // 
            // iTEMNMBRDataGridViewTextBoxColumn1
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn1.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn1.HeaderText = "Číslo položky";
            this.iTEMNMBRDataGridViewTextBoxColumn1.Name = "iTEMNMBRDataGridViewTextBoxColumn1";
            this.iTEMNMBRDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // iTEMCODEDataGridViewTextBoxColumn1
            // 
            this.iTEMCODEDataGridViewTextBoxColumn1.DataPropertyName = "ITEMCODE";
            this.iTEMCODEDataGridViewTextBoxColumn1.HeaderText = "Kód položky";
            this.iTEMCODEDataGridViewTextBoxColumn1.Name = "iTEMCODEDataGridViewTextBoxColumn1";
            this.iTEMCODEDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // iTEMDESCDataGridViewTextBoxColumn1
            // 
            this.iTEMDESCDataGridViewTextBoxColumn1.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn1.HeaderText = "Označení položky";
            this.iTEMDESCDataGridViewTextBoxColumn1.Name = "iTEMDESCDataGridViewTextBoxColumn1";
            this.iTEMDESCDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // vNDITNUMDataGridViewTextBoxColumn1
            // 
            this.vNDITNUMDataGridViewTextBoxColumn1.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn1.HeaderText = "Čář. kód";
            this.vNDITNUMDataGridViewTextBoxColumn1.Name = "vNDITNUMDataGridViewTextBoxColumn1";
            this.vNDITNUMDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // QTYSHPPD_Pohoda
            // 
            this.QTYSHPPD_Pohoda.DataPropertyName = "QTYSHPPD_Pohoda";
            this.QTYSHPPD_Pohoda.HeaderText = "Množství";
            this.QTYSHPPD_Pohoda.Name = "QTYSHPPD_Pohoda";
            this.QTYSHPPD_Pohoda.ReadOnly = true;
            // 
            // mJDataGridViewTextBoxColumn1
            // 
            this.mJDataGridViewTextBoxColumn1.DataPropertyName = "MJ";
            this.mJDataGridViewTextBoxColumn1.HeaderText = "Měrná jednotka";
            this.mJDataGridViewTextBoxColumn1.Name = "mJDataGridViewTextBoxColumn1";
            this.mJDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // SERLTNUM
            // 
            this.SERLTNUM.DataPropertyName = "SERLTNUM";
            this.SERLTNUM.HeaderText = "Šarže/SN";
            this.SERLTNUM.Name = "SERLTNUM";
            this.SERLTNUM.ReadOnly = true;
            // 
            // eXPIRACEDataGridViewTextBoxColumn1
            // 
            this.eXPIRACEDataGridViewTextBoxColumn1.DataPropertyName = "EXPIRACE";
            this.eXPIRACEDataGridViewTextBoxColumn1.HeaderText = "Expirace";
            this.eXPIRACEDataGridViewTextBoxColumn1.Name = "eXPIRACEDataGridViewTextBoxColumn1";
            this.eXPIRACEDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // sKLIDDataGridViewTextBoxColumn1
            // 
            this.sKLIDDataGridViewTextBoxColumn1.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn1.HeaderText = "ID Skladu";
            this.sKLIDDataGridViewTextBoxColumn1.Name = "sKLIDDataGridViewTextBoxColumn1";
            this.sKLIDDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // skldescDataGridViewTextBoxColumn1
            // 
            this.skldescDataGridViewTextBoxColumn1.DataPropertyName = "skl_desc";
            this.skldescDataGridViewTextBoxColumn1.HeaderText = "Název skladu";
            this.skldescDataGridViewTextBoxColumn1.Name = "skldescDataGridViewTextBoxColumn1";
            this.skldescDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // bs_IS
            // 
            this.bs_IS.DataMember = "SKz_Stav";
            this.bs_IS.DataSource = this.ds_IS;
            // 
            // ds_IS
            // 
            this.ds_IS.DataSetName = "SkladLokace_CompareToIS";
            this.ds_IS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_IS_Count});
            this.statusStrip1.Location = new System.Drawing.Point(0, 464);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(471, 22);
            this.statusStrip1.TabIndex = 41;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_IS_Count
            // 
            this.tssl_IS_Count.Name = "tssl_IS_Count";
            this.tssl_IS_Count.Size = new System.Drawing.Size(118, 17);
            this.tssl_IS_Count.Text = "toolStripStatusLabel1";
            // 
            // progressIndicator_IS
            // 
            this.progressIndicator_IS.Location = new System.Drawing.Point(173, 197);
            this.progressIndicator_IS.Name = "progressIndicator_IS";
            this.progressIndicator_IS.Percentage = 0F;
            this.progressIndicator_IS.Size = new System.Drawing.Size(105, 105);
            this.progressIndicator_IS.TabIndex = 40;
            this.progressIndicator_IS.Text = "progressIndicator2";
            this.progressIndicator_IS.Visible = false;
            // 
            // dg_SearchToolBar_IS
            // 
            this.dg_SearchToolBar_IS.AllowMerge = false;
            this.dg_SearchToolBar_IS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.dg_SearchToolBar_IS.Location = new System.Drawing.Point(0, 40);
            this.dg_SearchToolBar_IS.MaximumSize = new System.Drawing.Size(0, 27);
            this.dg_SearchToolBar_IS.MinimumSize = new System.Drawing.Size(0, 27);
            this.dg_SearchToolBar_IS.Name = "dg_SearchToolBar_IS";
            this.dg_SearchToolBar_IS.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.dg_SearchToolBar_IS.Size = new System.Drawing.Size(471, 27);
            this.dg_SearchToolBar_IS.TabIndex = 0;
            this.dg_SearchToolBar_IS.Text = "advancedDataGridViewSearchToolBar1";
            this.dg_SearchToolBar_IS.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.dg_SearchToolBar_IS_Search);
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(471, 40);
            this.label5.TabIndex = 61;
            this.label5.Text = "Informační systém";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dg_FASK
            // 
            this.dg_FASK.AllowUserToAddRows = false;
            this.dg_FASK.AllowUserToDeleteRows = false;
            this.dg_FASK.AllowUserToOrderColumns = true;
            this.dg_FASK.AllowUserToResizeRows = false;
            this.dg_FASK.AutoGenerateColumns = false;
            this.dg_FASK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_FASK.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMCODEDataGridViewTextBoxColumn,
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.vNDITNUMDataGridViewTextBoxColumn,
            this.qTYSHPPDDataGridViewTextBoxColumn,
            this.mJDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn1,
            this.eXPIRACEDataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.skldescDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn});
            this.dg_FASK.DataSource = this.bs_FASK;
            this.dg_FASK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_FASK.EnableHeadersVisualStyles = false;
            this.dg_FASK.FilterAndSortEnabled = true;
            this.dg_FASK.Location = new System.Drawing.Point(0, 67);
            this.dg_FASK.Name = "dg_FASK";
            this.dg_FASK.ReadOnly = true;
            this.dg_FASK.RowHeadersVisible = false;
            this.dg_FASK.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_FASK.Size = new System.Drawing.Size(467, 397);
            this.dg_FASK.TabIndex = 1;
            this.dg_FASK.TabStop = false;
            this.dg_FASK.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_FASK_CellFormatting);
            this.dg_FASK.SelectionChanged += new System.EventHandler(this.dg_FASK_SelectionChanged);
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "Číslo položky";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMCODEDataGridViewTextBoxColumn
            // 
            this.iTEMCODEDataGridViewTextBoxColumn.DataPropertyName = "ITEMCODE";
            this.iTEMCODEDataGridViewTextBoxColumn.HeaderText = "Kód položky";
            this.iTEMCODEDataGridViewTextBoxColumn.Name = "iTEMCODEDataGridViewTextBoxColumn";
            this.iTEMCODEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Označení položky";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vNDITNUMDataGridViewTextBoxColumn
            // 
            this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "Čár. kód";
            this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
            this.vNDITNUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qTYSHPPDDataGridViewTextBoxColumn
            // 
            this.qTYSHPPDDataGridViewTextBoxColumn.DataPropertyName = "QTYSHPPD";
            this.qTYSHPPDDataGridViewTextBoxColumn.HeaderText = "Množství";
            this.qTYSHPPDDataGridViewTextBoxColumn.Name = "qTYSHPPDDataGridViewTextBoxColumn";
            this.qTYSHPPDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mJDataGridViewTextBoxColumn
            // 
            this.mJDataGridViewTextBoxColumn.DataPropertyName = "MJ";
            this.mJDataGridViewTextBoxColumn.HeaderText = "Měrná jednotka";
            this.mJDataGridViewTextBoxColumn.Name = "mJDataGridViewTextBoxColumn";
            this.mJDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "SERLTNUM";
            this.dataGridViewTextBoxColumn1.HeaderText = "Šarže/SN";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // eXPIRACEDataGridViewTextBoxColumn
            // 
            this.eXPIRACEDataGridViewTextBoxColumn.DataPropertyName = "EXPIRACE";
            this.eXPIRACEDataGridViewTextBoxColumn.HeaderText = "Expirace";
            this.eXPIRACEDataGridViewTextBoxColumn.Name = "eXPIRACEDataGridViewTextBoxColumn";
            this.eXPIRACEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sKLIDDataGridViewTextBoxColumn
            // 
            this.sKLIDDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.HeaderText = "ID Skladu";
            this.sKLIDDataGridViewTextBoxColumn.Name = "sKLIDDataGridViewTextBoxColumn";
            this.sKLIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // skldescDataGridViewTextBoxColumn
            // 
            this.skldescDataGridViewTextBoxColumn.DataPropertyName = "skl_desc";
            this.skldescDataGridViewTextBoxColumn.HeaderText = "Název skladu";
            this.skldescDataGridViewTextBoxColumn.Name = "skldescDataGridViewTextBoxColumn";
            this.skldescDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bs_FASK
            // 
            this.bs_FASK.DataMember = "LokMech_Stav";
            this.bs_FASK.DataSource = this.ds_FASK;
            // 
            // ds_FASK
            // 
            this.ds_FASK.DataSetName = "SkladLokace_CompareToIS";
            this.ds_FASK.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_FASK_Count});
            this.statusStrip2.Location = new System.Drawing.Point(0, 464);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(467, 22);
            this.statusStrip2.TabIndex = 40;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // tssl_FASK_Count
            // 
            this.tssl_FASK_Count.Name = "tssl_FASK_Count";
            this.tssl_FASK_Count.Size = new System.Drawing.Size(118, 17);
            this.tssl_FASK_Count.Text = "toolStripStatusLabel1";
            // 
            // progressIndicator_FASK
            // 
            this.progressIndicator_FASK.Location = new System.Drawing.Point(175, 197);
            this.progressIndicator_FASK.Name = "progressIndicator_FASK";
            this.progressIndicator_FASK.Percentage = 0F;
            this.progressIndicator_FASK.Size = new System.Drawing.Size(105, 105);
            this.progressIndicator_FASK.TabIndex = 39;
            this.progressIndicator_FASK.Text = "progressIndicator1";
            this.progressIndicator_FASK.Visible = false;
            // 
            // dg_SearchToolBar_FASK
            // 
            this.dg_SearchToolBar_FASK.AllowMerge = false;
            this.dg_SearchToolBar_FASK.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.dg_SearchToolBar_FASK.Location = new System.Drawing.Point(0, 40);
            this.dg_SearchToolBar_FASK.MaximumSize = new System.Drawing.Size(0, 27);
            this.dg_SearchToolBar_FASK.MinimumSize = new System.Drawing.Size(0, 27);
            this.dg_SearchToolBar_FASK.Name = "dg_SearchToolBar_FASK";
            this.dg_SearchToolBar_FASK.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.dg_SearchToolBar_FASK.Size = new System.Drawing.Size(467, 27);
            this.dg_SearchToolBar_FASK.TabIndex = 0;
            this.dg_SearchToolBar_FASK.Text = "advancedDataGridViewSearchToolBar2";
            this.dg_SearchToolBar_FASK.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.dg_SearchToolBar_FASK_Search);
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(467, 40);
            this.label6.TabIndex = 62;
            this.label6.Text = "Lokační mechanismus";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bw_LoadData
            // 
            this.bw_LoadData.WorkerSupportsCancellation = true;
            this.bw_LoadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_LoadData_DoWork);
            this.bw_LoadData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_LoadData_RunWorkerCompleted);
            // 
            // bw_PoloAutomat
            // 
            this.bw_PoloAutomat.WorkerSupportsCancellation = true;
            this.bw_PoloAutomat.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_PoloAutomat_DoWork);
            this.bw_PoloAutomat.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_PoloAutomat_RunWorkerCompleted);
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(942, 24);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(122, 657);
            this.panelButtons.TabIndex = 1;
            // 
            // FormSKladLokace_PorovnaniVuciIS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.Name = "FormSKladLokace_PorovnaniVuciIS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Porovnání zásob IS vs. LokMech";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSKladLokace_PorovnaniVuciIS_FormClosing);
            this.Load += new System.EventHandler(this.FormSKladLokace_PorovnaniVuciIS_Load);
            this.Shown += new System.EventHandler(this.FormSKladLokace_PorovnaniVuciIS_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSKladLokace_PorovnaniVuciIS_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsFiltry.ResumeLayout(false);
            this.tsFiltry.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_IS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_IS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_IS)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_FASK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_FASK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_FASK)).EndInit();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem tsmi_Konec;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private Panel panel1;
        private SplitContainer splitContainer1;
        private Zuby.ADGV.AdvancedDataGridView dg_IS;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar dg_SearchToolBar_IS;
        private Zuby.ADGV.AdvancedDataGridView dg_FASK;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar dg_SearchToolBar_FASK;
        private Button buttonVyhledat;
        private System.ComponentModel.BackgroundWorker bw_LoadData;
        private Fask.Interfaces.DataSets.SkladLokace_CompareToIS ds_IS;
        private Fask.Interfaces.DataSets.SkladLokace_CompareToIS ds_FASK;
        private BindingSource bs_IS;
        private BindingSource bs_FASK;
        private ProgressControls.ProgressIndicator progressIndicator_FASK;
        private TextBox tb_SKL_ID;
        private Label label1;
        private ToolStrip tsFiltry;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox tscbFiltry;
        private ToolStripButton tsbNastavit;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbZmena;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton tsbPridat;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton tsbOdebrat;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton tsbVycistit;
        private ProgressControls.ProgressIndicator progressIndicator_IS;
        //private DataGridViewTextBoxColumn qTYSHPPDDataGridViewTextBoxColumn1;
        private CheckBox chb_SarzeSN_R;
        private CheckBox chb_NazevPolozky_R;
        private CheckBox chb_KodPolozky_R;
        private CheckBox chb_SarzeSN_L;
        private CheckBox chb_NazevPolozky_L;
        private CheckBox chb_KodPolozky_L;
        private TextBox tb_SarzeSN;
        private Label label4;
        private TextBox tb_NazevPolozky;
        private Label label3;
        private TextBox tb_KodPolozky;
        private Label label2;
        private CheckBox chb_V_3;
        private CheckBox chb_V_2;
        private CheckBox chb_V_1;
        private CheckBox chb_V_0;
        private CheckBox chb_V_6;
        private CheckBox chb_V_5;
        private CheckBox chb_V_4;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tssl_IS_Count;
        private StatusStrip statusStrip2;
        private ToolStripStatusLabel tssl_FASK_Count;
        private ToolStripMenuItem zobrazitToolStripMenuItem;
        private ToolStripMenuItem tsmi_IS_Pohyby;
        private ToolStripMenuItem tsmi_LokMechStav;
        private ToolStripMenuItem logikaToolStripMenuItem;
        private ToolStripMenuItem tsmi_poloAutomat;
        private System.ComponentModel.BackgroundWorker bw_PoloAutomat;
        private Label label5;
        private Label label6;
        private ToolTip toolTip1;
        private DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn iTEMCODEDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn vNDITNUMDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn QTYSHPPD_Pohoda;
        private DataGridViewTextBoxColumn mJDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn SERLTNUM;
        private DataGridViewTextBoxColumn eXPIRACEDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn skldescDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn status;
        private DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iTEMCODEDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn vNDITNUMDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn qTYSHPPDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn mJDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn eXPIRACEDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn skldescDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private ToolStripMenuItem tsmiExporty;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoCSVOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExcelVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoExceOznacene;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLVse;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportDoXMLOznacene;
        private ToolStripMenuItem tsmiExportyFASK;
        private ToolStripMenuItem tsmiExportDoCSVVse_FASK;
        private ToolStripMenuItem tsmiExportDoCSVOznacene_FASK;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tsmiExportDoExcelVse_FASK;
        private ToolStripMenuItem tsmiExportDoExceOznacene_FASK;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem tsmiExportDoXMLVse_FASK;
        private ToolStripMenuItem tsmiExportDoXMLOznacene_FASK;
    }
}