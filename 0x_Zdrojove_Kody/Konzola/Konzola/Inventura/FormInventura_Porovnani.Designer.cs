 
using System;
using System.Windows.Forms;

namespace Konzola.Inventura
{
    partial class FormInventura_Porovnani
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInventura_Porovnani));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Konec = new System.Windows.Forms.ToolStripMenuItem();
            this.výstupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tsmiExportDoCSVVse_I123 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoCSVOznacene_I123 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoExcelVse_I123 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoExceOznacene_I123 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportDoXMLVse_I123 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportDoXMLOznacene_I123 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chb_V_3 = new System.Windows.Forms.CheckBox();
            this.chb_V_2 = new System.Windows.Forms.CheckBox();
            this.chb_V_1 = new System.Windows.Forms.CheckBox();
            this.chb_V_0 = new System.Windows.Forms.CheckBox();
            this.chb_V_4 = new System.Windows.Forms.CheckBox();
            this.tb_CountEntries = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
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
            this.progressIndicator_I4 = new ProgressControls.ProgressIndicator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_I4_Count = new System.Windows.Forms.ToolStripStatusLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.progressIndicator_I123 = new ProgressControls.ProgressIndicator();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.tssl_I123_Count = new System.Windows.Forms.ToolStripStatusLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.bw_LoadData = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dg_I4 = new Zuby.ADGV.AdvancedDataGridView();
            this.countEntriesDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLDESCDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qUANTITYDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mJDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sERLNMBRDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expiraceDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_I4 = new System.Windows.Forms.BindingSource(this.components);
            this.ds_I4 = new Fask.Interfaces.DataSets.Inventura_Compare();
            this.dg_SearchToolBar_I4 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.dg_I123 = new Zuby.ADGV.AdvancedDataGridView();
            this.countEntriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vNDITNUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sKLDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qUANTITYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mJDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sERLNMBRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expiraceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_I123 = new System.Windows.Forms.BindingSource(this.components);
            this.ds_I123 = new Fask.Interfaces.DataSets.Inventura_Compare();
            this.dg_SearchToolBar_I123 = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tsFiltry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_I4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_I4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_I4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_I123)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_I123)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_I123)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.výstupToolStripMenuItem,
            this.tsmiExporty,
            this.tsmiExportyFASK});
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
            // výstupToolStripMenuItem
            // 
            this.výstupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiskToolStripMenuItem});
            this.výstupToolStripMenuItem.Name = "výstupToolStripMenuItem";
            this.výstupToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.výstupToolStripMenuItem.Text = "Výstup";
            // 
            // tiskToolStripMenuItem
            // 
            this.tiskToolStripMenuItem.Name = "tiskToolStripMenuItem";
            this.tiskToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tiskToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.tiskToolStripMenuItem.Text = "Tisk";
            this.tiskToolStripMenuItem.Click += new System.EventHandler(this.tiskToolStripMenuItem_Click);
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
            this.tsmiExporty.Size = new System.Drawing.Size(105, 20);
            this.tsmiExporty.Text = "Výstup předloha";
            // 
            // tsmiExportDoCSVVse
            // 
            this.tsmiExportDoCSVVse.Name = "tsmiExportDoCSVVse";
            this.tsmiExportDoCSVVse.Size = new System.Drawing.Size(258, 22);
            this.tsmiExportDoCSVVse.Text = "Export do CSV vše předloha";
            this.tsmiExportDoCSVVse.Click += new System.EventHandler(this.exportDoCSVVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoCSVOznacene
            // 
            this.tsmiExportDoCSVOznacene.Name = "tsmiExportDoCSVOznacene";
            this.tsmiExportDoCSVOznacene.Size = new System.Drawing.Size(258, 22);
            this.tsmiExportDoCSVOznacene.Text = "Export do CSV označené předloha";
            this.tsmiExportDoCSVOznacene.Click += new System.EventHandler(this.exportDoCSVOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(255, 6);
            // 
            // tsmiExportDoExcelVse
            // 
            this.tsmiExportDoExcelVse.Name = "tsmiExportDoExcelVse";
            this.tsmiExportDoExcelVse.Size = new System.Drawing.Size(258, 22);
            this.tsmiExportDoExcelVse.Text = "Export do Excel vše předloha";
            this.tsmiExportDoExcelVse.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoExceOznacene
            // 
            this.tsmiExportDoExceOznacene.Name = "tsmiExportDoExceOznacene";
            this.tsmiExportDoExceOznacene.Size = new System.Drawing.Size(258, 22);
            this.tsmiExportDoExceOznacene.Text = "Export do Excel označené předloha";
            this.tsmiExportDoExceOznacene.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(255, 6);
            // 
            // tsmiExportDoXMLVse
            // 
            this.tsmiExportDoXMLVse.Name = "tsmiExportDoXMLVse";
            this.tsmiExportDoXMLVse.Size = new System.Drawing.Size(258, 22);
            this.tsmiExportDoXMLVse.Text = "Export do XML Vše předloha";
            this.tsmiExportDoXMLVse.Click += new System.EventHandler(this.exportDoXMLVseToolStripMenuItem_Click);
            // 
            // tsmiExportDoXMLOznacene
            // 
            this.tsmiExportDoXMLOznacene.Name = "tsmiExportDoXMLOznacene";
            this.tsmiExportDoXMLOznacene.Size = new System.Drawing.Size(258, 22);
            this.tsmiExportDoXMLOznacene.Text = "Export do XML označené předloha";
            this.tsmiExportDoXMLOznacene.Click += new System.EventHandler(this.exportDoXMLOznaceneToolStripMenuItem_Click);
            // 
            // tsmiExportyFASK
            // 
            this.tsmiExportyFASK.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportDoCSVVse_I123,
            this.tsmiExportDoCSVOznacene_I123,
            this.toolStripSeparator1,
            this.tsmiExportDoExcelVse_I123,
            this.tsmiExportDoExceOznacene_I123,
            this.toolStripSeparator8,
            this.tsmiExportDoXMLVse_I123,
            this.tsmiExportDoXMLOznacene_I123});
            this.tsmiExportyFASK.Name = "tsmiExportyFASK";
            this.tsmiExportyFASK.Size = new System.Drawing.Size(117, 20);
            this.tsmiExportyFASK.Text = "Výstup nasnímáno";
            // 
            // tsmiExportDoCSVVse_I123
            // 
            this.tsmiExportDoCSVVse_I123.Name = "tsmiExportDoCSVVse_I123";
            this.tsmiExportDoCSVVse_I123.Size = new System.Drawing.Size(270, 22);
            this.tsmiExportDoCSVVse_I123.Text = "Export do CSV vše nasnímáno";
            this.tsmiExportDoCSVVse_I123.Click += new System.EventHandler(this.exportDoCSVVseToolStripMenuItem_I123_Click);
            // 
            // tsmiExportDoCSVOznacene_I123
            // 
            this.tsmiExportDoCSVOznacene_I123.Name = "tsmiExportDoCSVOznacene_I123";
            this.tsmiExportDoCSVOznacene_I123.Size = new System.Drawing.Size(270, 22);
            this.tsmiExportDoCSVOznacene_I123.Text = "Export do CSV označené nasnímáno";
            this.tsmiExportDoCSVOznacene_I123.Click += new System.EventHandler(this.exportDoCSVOznaceneToolStripMenuItem_I123_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(267, 6);
            // 
            // tsmiExportDoExcelVse_I123
            // 
            this.tsmiExportDoExcelVse_I123.Name = "tsmiExportDoExcelVse_I123";
            this.tsmiExportDoExcelVse_I123.Size = new System.Drawing.Size(270, 22);
            this.tsmiExportDoExcelVse_I123.Text = "Export do Excel vše nasnímáno";
            this.tsmiExportDoExcelVse_I123.Click += new System.EventHandler(this.exportDoExcelVseToolStripMenuItem_I123_Click);
            // 
            // tsmiExportDoExceOznacene_I123
            // 
            this.tsmiExportDoExceOznacene_I123.Name = "tsmiExportDoExceOznacene_I123";
            this.tsmiExportDoExceOznacene_I123.Size = new System.Drawing.Size(270, 22);
            this.tsmiExportDoExceOznacene_I123.Text = "Export do Excel označené nasnímáno";
            this.tsmiExportDoExceOznacene_I123.Click += new System.EventHandler(this.exportDoExceOznaceneToolStripMenuItem_I123_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(267, 6);
            // 
            // tsmiExportDoXMLVse_I123
            // 
            this.tsmiExportDoXMLVse_I123.Name = "tsmiExportDoXMLVse_I123";
            this.tsmiExportDoXMLVse_I123.Size = new System.Drawing.Size(270, 22);
            this.tsmiExportDoXMLVse_I123.Text = "Export do XML Vše nasnímáno";
            this.tsmiExportDoXMLVse_I123.Click += new System.EventHandler(this.exportDoXMLVseToolStripMenuItem_I123_Click);
            // 
            // tsmiExportDoXMLOznacene_I123
            // 
            this.tsmiExportDoXMLOznacene_I123.Name = "tsmiExportDoXMLOznacene_I123";
            this.tsmiExportDoXMLOznacene_I123.Size = new System.Drawing.Size(270, 22);
            this.tsmiExportDoXMLOznacene_I123.Text = "Export do XML označené nasnímáno";
            this.tsmiExportDoXMLOznacene_I123.Click += new System.EventHandler(this.exportDoXMLOznaceneToolStripMenuItem_I123_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chb_V_3);
            this.panel1.Controls.Add(this.chb_V_2);
            this.panel1.Controls.Add(this.chb_V_1);
            this.panel1.Controls.Add(this.chb_V_0);
            this.panel1.Controls.Add(this.chb_V_4);
            this.panel1.Controls.Add(this.tb_CountEntries);
            this.panel1.Controls.Add(this.label7);
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
            this.panel1.Size = new System.Drawing.Size(942, 168);
            this.panel1.TabIndex = 2;
            // 
            // chb_V_3
            // 
            this.chb_V_3.AutoSize = true;
            this.chb_V_3.BackColor = System.Drawing.Color.Tomato;
            this.chb_V_3.Checked = true;
            this.chb_V_3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_V_3.Location = new System.Drawing.Point(423, 104);
            this.chb_V_3.Name = "chb_V_3";
            this.chb_V_3.Size = new System.Drawing.Size(234, 17);
            this.chb_V_3.TabIndex = 67;
            this.chb_V_3.Text = "Skutečného stavu je více než evidovaného";
            this.chb_V_3.UseVisualStyleBackColor = false;
            // 
            // chb_V_2
            // 
            this.chb_V_2.AutoSize = true;
            this.chb_V_2.BackColor = System.Drawing.Color.Yellow;
            this.chb_V_2.Checked = true;
            this.chb_V_2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_V_2.Location = new System.Drawing.Point(423, 81);
            this.chb_V_2.Name = "chb_V_2";
            this.chb_V_2.Size = new System.Drawing.Size(238, 17);
            this.chb_V_2.TabIndex = 66;
            this.chb_V_2.Text = "Skutečného stavu je méně než evidovaného";
            this.chb_V_2.UseVisualStyleBackColor = false;
            // 
            // chb_V_1
            // 
            this.chb_V_1.AutoSize = true;
            this.chb_V_1.BackColor = System.Drawing.Color.Lime;
            this.chb_V_1.Checked = true;
            this.chb_V_1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_V_1.Location = new System.Drawing.Point(423, 61);
            this.chb_V_1.Name = "chb_V_1";
            this.chb_V_1.Size = new System.Drawing.Size(233, 17);
            this.chb_V_1.TabIndex = 65;
            this.chb_V_1.Text = "Evidovaný stav se rovná skutečnému stavu";
            this.chb_V_1.UseVisualStyleBackColor = false;
            // 
            // chb_V_0
            // 
            this.chb_V_0.AutoSize = true;
            this.chb_V_0.BackColor = System.Drawing.Color.Orange;
            this.chb_V_0.Checked = true;
            this.chb_V_0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_V_0.Location = new System.Drawing.Point(423, 39);
            this.chb_V_0.Name = "chb_V_0";
            this.chb_V_0.Size = new System.Drawing.Size(154, 17);
            this.chb_V_0.TabIndex = 64;
            this.chb_V_0.Text = "Položka nebyla nasnímaná";
            this.chb_V_0.UseVisualStyleBackColor = false;
            // 
            // chb_V_4
            // 
            this.chb_V_4.AutoSize = true;
            this.chb_V_4.BackColor = System.Drawing.Color.Orange;
            this.chb_V_4.Checked = true;
            this.chb_V_4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_V_4.Location = new System.Drawing.Point(423, 127);
            this.chb_V_4.Name = "chb_V_4";
            this.chb_V_4.Size = new System.Drawing.Size(304, 17);
            this.chb_V_4.TabIndex = 61;
            this.chb_V_4.Text = "Nalezen neevidovaný atribut položky (SN/šarže/exspirace)";
            this.chb_V_4.UseVisualStyleBackColor = false;
            // 
            // tb_CountEntries
            // 
            this.tb_CountEntries.Location = new System.Drawing.Point(120, 35);
            this.tb_CountEntries.Name = "tb_CountEntries";
            this.tb_CountEntries.Size = new System.Drawing.Size(247, 20);
            this.tb_CountEntries.TabIndex = 55;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 54;
            this.label7.Text = "Číslo dávky:";
            // 
            // chb_SarzeSN_R
            // 
            this.chb_SarzeSN_R.AutoSize = true;
            this.chb_SarzeSN_R.Location = new System.Drawing.Point(373, 138);
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
            this.chb_NazevPolozky_R.Location = new System.Drawing.Point(373, 114);
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
            this.chb_KodPolozky_R.Location = new System.Drawing.Point(373, 90);
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
            this.chb_SarzeSN_L.Location = new System.Drawing.Point(99, 138);
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
            this.chb_NazevPolozky_L.Location = new System.Drawing.Point(99, 114);
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
            this.chb_KodPolozky_L.Location = new System.Drawing.Point(99, 90);
            this.chb_KodPolozky_L.Name = "chb_KodPolozky_L";
            this.chb_KodPolozky_L.Size = new System.Drawing.Size(15, 14);
            this.chb_KodPolozky_L.TabIndex = 48;
            this.chb_KodPolozky_L.UseVisualStyleBackColor = true;
            this.chb_KodPolozky_L.MouseLeave += new System.EventHandler(this.chb_L_MouseLeave);
            this.chb_KodPolozky_L.MouseHover += new System.EventHandler(this.chb_L_MouseHover);
            // 
            // tb_SarzeSN
            // 
            this.tb_SarzeSN.Location = new System.Drawing.Point(120, 135);
            this.tb_SarzeSN.Name = "tb_SarzeSN";
            this.tb_SarzeSN.Size = new System.Drawing.Size(247, 20);
            this.tb_SarzeSN.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Šarže / SN:";
            // 
            // tb_NazevPolozky
            // 
            this.tb_NazevPolozky.Location = new System.Drawing.Point(120, 111);
            this.tb_NazevPolozky.Name = "tb_NazevPolozky";
            this.tb_NazevPolozky.Size = new System.Drawing.Size(247, 20);
            this.tb_NazevPolozky.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Název položky:";
            // 
            // tb_KodPolozky
            // 
            this.tb_KodPolozky.Location = new System.Drawing.Point(120, 87);
            this.tb_KodPolozky.Name = "tb_KodPolozky";
            this.tb_KodPolozky.Size = new System.Drawing.Size(247, 20);
            this.tb_KodPolozky.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 91);
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
            this.tb_SKL_ID.Location = new System.Drawing.Point(120, 61);
            this.tb_SKL_ID.Name = "tb_SKL_ID";
            this.tb_SKL_ID.Size = new System.Drawing.Size(247, 20);
            this.tb_SKL_ID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 65);
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 192);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.progressIndicator_I4);
            this.splitContainer1.Panel1.Controls.Add(this.dg_I4);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.dg_SearchToolBar_I4);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.progressIndicator_I123);
            this.splitContainer1.Panel2.Controls.Add(this.dg_I123);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip2);
            this.splitContainer1.Panel2.Controls.Add(this.dg_SearchToolBar_I123);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Size = new System.Drawing.Size(942, 489);
            this.splitContainer1.SplitterDistance = 471;
            this.splitContainer1.TabIndex = 3;
            // 
            // progressIndicator_I4
            // 
            this.progressIndicator_I4.Location = new System.Drawing.Point(173, 197);
            this.progressIndicator_I4.Name = "progressIndicator_I4";
            this.progressIndicator_I4.Percentage = 0F;
            this.progressIndicator_I4.Size = new System.Drawing.Size(105, 105);
            this.progressIndicator_I4.TabIndex = 40;
            this.progressIndicator_I4.Text = "progressIndicator2";
            this.progressIndicator_I4.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_I4_Count});
            this.statusStrip1.Location = new System.Drawing.Point(0, 467);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(471, 22);
            this.statusStrip1.TabIndex = 41;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_I4_Count
            // 
            this.tssl_I4_Count.Name = "tssl_I4_Count";
            this.tssl_I4_Count.Size = new System.Drawing.Size(118, 17);
            this.tssl_I4_Count.Text = "toolStripStatusLabel1";
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(471, 40);
            this.label5.TabIndex = 61;
            this.label5.Text = "Nasnímáno";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressIndicator_I123
            // 
            this.progressIndicator_I123.Location = new System.Drawing.Point(175, 197);
            this.progressIndicator_I123.Name = "progressIndicator_I123";
            this.progressIndicator_I123.Percentage = 0F;
            this.progressIndicator_I123.Size = new System.Drawing.Size(105, 105);
            this.progressIndicator_I123.TabIndex = 39;
            this.progressIndicator_I123.Text = "progressIndicator1";
            this.progressIndicator_I123.Visible = false;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_I123_Count});
            this.statusStrip2.Location = new System.Drawing.Point(0, 467);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(467, 22);
            this.statusStrip2.TabIndex = 40;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // tssl_I123_Count
            // 
            this.tssl_I123_Count.Name = "tssl_I123_Count";
            this.tssl_I123_Count.Size = new System.Drawing.Size(118, 17);
            this.tssl_I123_Count.Text = "toolStripStatusLabel1";
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(467, 40);
            this.label6.TabIndex = 62;
            this.label6.Text = "Předloha";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bw_LoadData
            // 
            this.bw_LoadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_LoadData_DoWork);
            this.bw_LoadData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_LoadData_RunWorkerCompleted);
            // 
            // dg_I4
            // 
            this.dg_I4.AllowUserToAddRows = false;
            this.dg_I4.AllowUserToDeleteRows = false;
            this.dg_I4.AllowUserToOrderColumns = true;
            this.dg_I4.AllowUserToResizeRows = false;
            this.dg_I4.AutoGenerateColumns = false;
            this.dg_I4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_I4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.countEntriesDataGridViewTextBoxColumn1,
            this.iTEMNMBRDataGridViewTextBoxColumn1,
            this.iTEMDESCDataGridViewTextBoxColumn1,
            this.ITEMCODE,
            this.vNDITNUMDataGridViewTextBoxColumn1,
            this.sKLIDDataGridViewTextBoxColumn1,
            this.sKLDESCDataGridViewTextBoxColumn1,
            this.qUANTITYDataGridViewTextBoxColumn1,
            this.mJDataGridViewTextBoxColumn1,
            this.sERLNMBRDataGridViewTextBoxColumn1,
            this.expiraceDataGridViewTextBoxColumn1,
            this.status});
            this.dg_I4.DataSource = this.bs_I4;
            this.dg_I4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_I4.EnableHeadersVisualStyles = false;
            this.dg_I4.FilterAndSortEnabled = true;
            this.dg_I4.Location = new System.Drawing.Point(0, 67);
            this.dg_I4.Name = "dg_I4";
            this.dg_I4.ReadOnly = true;
            this.dg_I4.RowHeadersVisible = false;
            this.dg_I4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_I4.Size = new System.Drawing.Size(471, 400);
            this.dg_I4.TabIndex = 1;
            this.dg_I4.TabStop = false;
            this.dg_I4.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_I4_CellFormatting);
            this.dg_I4.SelectionChanged += new System.EventHandler(this.dg_I4_SelectionChanged);
            // 
            // countEntriesDataGridViewTextBoxColumn1
            // 
            this.countEntriesDataGridViewTextBoxColumn1.DataPropertyName = "CountEntries";
            this.countEntriesDataGridViewTextBoxColumn1.HeaderText = "Číslo dávky";
            this.countEntriesDataGridViewTextBoxColumn1.Name = "countEntriesDataGridViewTextBoxColumn1";
            this.countEntriesDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn1
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn1.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn1.HeaderText = "ID položky";
            this.iTEMNMBRDataGridViewTextBoxColumn1.Name = "iTEMNMBRDataGridViewTextBoxColumn1";
            this.iTEMNMBRDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // iTEMDESCDataGridViewTextBoxColumn1
            // 
            this.iTEMDESCDataGridViewTextBoxColumn1.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn1.HeaderText = "Popis položky";
            this.iTEMDESCDataGridViewTextBoxColumn1.Name = "iTEMDESCDataGridViewTextBoxColumn1";
            this.iTEMDESCDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // ITEMCODE
            // 
            this.ITEMCODE.DataPropertyName = "ITEMCODE";
            this.ITEMCODE.HeaderText = "Kód položky";
            this.ITEMCODE.Name = "ITEMCODE";
            this.ITEMCODE.ReadOnly = true;
            // 
            // vNDITNUMDataGridViewTextBoxColumn1
            // 
            this.vNDITNUMDataGridViewTextBoxColumn1.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn1.HeaderText = "Čár. Kód";
            this.vNDITNUMDataGridViewTextBoxColumn1.Name = "vNDITNUMDataGridViewTextBoxColumn1";
            this.vNDITNUMDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // sKLIDDataGridViewTextBoxColumn1
            // 
            this.sKLIDDataGridViewTextBoxColumn1.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn1.HeaderText = "ID skladu";
            this.sKLIDDataGridViewTextBoxColumn1.Name = "sKLIDDataGridViewTextBoxColumn1";
            this.sKLIDDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // sKLDESCDataGridViewTextBoxColumn1
            // 
            this.sKLDESCDataGridViewTextBoxColumn1.DataPropertyName = "SKL_DESC";
            this.sKLDESCDataGridViewTextBoxColumn1.HeaderText = "Název skladu";
            this.sKLDESCDataGridViewTextBoxColumn1.Name = "sKLDESCDataGridViewTextBoxColumn1";
            this.sKLDESCDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // qUANTITYDataGridViewTextBoxColumn1
            // 
            this.qUANTITYDataGridViewTextBoxColumn1.DataPropertyName = "QUANTITY";
            this.qUANTITYDataGridViewTextBoxColumn1.HeaderText = "Množství";
            this.qUANTITYDataGridViewTextBoxColumn1.Name = "qUANTITYDataGridViewTextBoxColumn1";
            this.qUANTITYDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // mJDataGridViewTextBoxColumn1
            // 
            this.mJDataGridViewTextBoxColumn1.DataPropertyName = "MJ";
            this.mJDataGridViewTextBoxColumn1.HeaderText = "Měrná jednotka";
            this.mJDataGridViewTextBoxColumn1.Name = "mJDataGridViewTextBoxColumn1";
            this.mJDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // sERLNMBRDataGridViewTextBoxColumn1
            // 
            this.sERLNMBRDataGridViewTextBoxColumn1.DataPropertyName = "SERLNMBR";
            this.sERLNMBRDataGridViewTextBoxColumn1.HeaderText = "Šarže/SN";
            this.sERLNMBRDataGridViewTextBoxColumn1.Name = "sERLNMBRDataGridViewTextBoxColumn1";
            this.sERLNMBRDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // expiraceDataGridViewTextBoxColumn1
            // 
            this.expiraceDataGridViewTextBoxColumn1.DataPropertyName = "Expirace";
            this.expiraceDataGridViewTextBoxColumn1.HeaderText = "Expirace";
            this.expiraceDataGridViewTextBoxColumn1.Name = "expiraceDataGridViewTextBoxColumn1";
            this.expiraceDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // bs_I4
            // 
            this.bs_I4.DataMember = "CZMST_I4";
            this.bs_I4.DataSource = this.ds_I4;
            // 
            // ds_I4
            // 
            this.ds_I4.DataSetName = "SkladLokace_CompareToIS";
            this.ds_I4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dg_SearchToolBar_I4
            // 
            this.dg_SearchToolBar_I4.AllowMerge = false;
            this.dg_SearchToolBar_I4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.dg_SearchToolBar_I4.Location = new System.Drawing.Point(0, 40);
            this.dg_SearchToolBar_I4.MaximumSize = new System.Drawing.Size(0, 27);
            this.dg_SearchToolBar_I4.MinimumSize = new System.Drawing.Size(0, 27);
            this.dg_SearchToolBar_I4.Name = "dg_SearchToolBar_I4";
            this.dg_SearchToolBar_I4.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.dg_SearchToolBar_I4.Size = new System.Drawing.Size(471, 27);
            this.dg_SearchToolBar_I4.TabIndex = 0;
            this.dg_SearchToolBar_I4.Text = "advancedDataGridViewSearchToolBar1";
            this.dg_SearchToolBar_I4.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.dg_SearchToolBar_I4_Search);
            // 
            // dg_I123
            // 
            this.dg_I123.AllowUserToAddRows = false;
            this.dg_I123.AllowUserToDeleteRows = false;
            this.dg_I123.AllowUserToOrderColumns = true;
            this.dg_I123.AllowUserToResizeRows = false;
            this.dg_I123.AutoGenerateColumns = false;
            this.dg_I123.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_I123.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.countEntriesDataGridViewTextBoxColumn,
            this.iTEMNMBRDataGridViewTextBoxColumn,
            this.iTEMDESCDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn1,
            this.vNDITNUMDataGridViewTextBoxColumn,
            this.sKLIDDataGridViewTextBoxColumn,
            this.sKLDESCDataGridViewTextBoxColumn,
            this.qUANTITYDataGridViewTextBoxColumn,
            this.mJDataGridViewTextBoxColumn,
            this.sERLNMBRDataGridViewTextBoxColumn,
            this.expiraceDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn2});
            this.dg_I123.DataSource = this.bs_I123;
            this.dg_I123.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_I123.EnableHeadersVisualStyles = false;
            this.dg_I123.FilterAndSortEnabled = true;
            this.dg_I123.Location = new System.Drawing.Point(0, 67);
            this.dg_I123.Name = "dg_I123";
            this.dg_I123.ReadOnly = true;
            this.dg_I123.RowHeadersVisible = false;
            this.dg_I123.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_I123.Size = new System.Drawing.Size(467, 400);
            this.dg_I123.TabIndex = 1;
            this.dg_I123.TabStop = false;
            this.dg_I123.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_I123_CellFormatting);
            this.dg_I123.SelectionChanged += new System.EventHandler(this.dg_I123_SelectionChanged);
            // 
            // countEntriesDataGridViewTextBoxColumn
            // 
            this.countEntriesDataGridViewTextBoxColumn.DataPropertyName = "CountEntries";
            this.countEntriesDataGridViewTextBoxColumn.HeaderText = "Číslo dávky";
            this.countEntriesDataGridViewTextBoxColumn.Name = "countEntriesDataGridViewTextBoxColumn";
            this.countEntriesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMNMBRDataGridViewTextBoxColumn
            // 
            this.iTEMNMBRDataGridViewTextBoxColumn.DataPropertyName = "ITEMNMBR";
            this.iTEMNMBRDataGridViewTextBoxColumn.HeaderText = "ID položky";
            this.iTEMNMBRDataGridViewTextBoxColumn.Name = "iTEMNMBRDataGridViewTextBoxColumn";
            this.iTEMNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMDESCDataGridViewTextBoxColumn
            // 
            this.iTEMDESCDataGridViewTextBoxColumn.DataPropertyName = "ITEMDESC";
            this.iTEMDESCDataGridViewTextBoxColumn.HeaderText = "Popis položky";
            this.iTEMDESCDataGridViewTextBoxColumn.Name = "iTEMDESCDataGridViewTextBoxColumn";
            this.iTEMDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ITEMCODE";
            this.dataGridViewTextBoxColumn1.HeaderText = "Kód položky";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // vNDITNUMDataGridViewTextBoxColumn
            // 
            this.vNDITNUMDataGridViewTextBoxColumn.DataPropertyName = "VNDITNUM";
            this.vNDITNUMDataGridViewTextBoxColumn.HeaderText = "Čár. Kód";
            this.vNDITNUMDataGridViewTextBoxColumn.Name = "vNDITNUMDataGridViewTextBoxColumn";
            this.vNDITNUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sKLIDDataGridViewTextBoxColumn
            // 
            this.sKLIDDataGridViewTextBoxColumn.DataPropertyName = "SKL_ID";
            this.sKLIDDataGridViewTextBoxColumn.HeaderText = "ID skladu";
            this.sKLIDDataGridViewTextBoxColumn.Name = "sKLIDDataGridViewTextBoxColumn";
            this.sKLIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sKLDESCDataGridViewTextBoxColumn
            // 
            this.sKLDESCDataGridViewTextBoxColumn.DataPropertyName = "SKL_DESC";
            this.sKLDESCDataGridViewTextBoxColumn.HeaderText = "Název skladu";
            this.sKLDESCDataGridViewTextBoxColumn.Name = "sKLDESCDataGridViewTextBoxColumn";
            this.sKLDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qUANTITYDataGridViewTextBoxColumn
            // 
            this.qUANTITYDataGridViewTextBoxColumn.DataPropertyName = "QUANTITY";
            this.qUANTITYDataGridViewTextBoxColumn.HeaderText = "Množství";
            this.qUANTITYDataGridViewTextBoxColumn.Name = "qUANTITYDataGridViewTextBoxColumn";
            this.qUANTITYDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mJDataGridViewTextBoxColumn
            // 
            this.mJDataGridViewTextBoxColumn.DataPropertyName = "MJ";
            this.mJDataGridViewTextBoxColumn.HeaderText = "Měrná jednotka";
            this.mJDataGridViewTextBoxColumn.Name = "mJDataGridViewTextBoxColumn";
            this.mJDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sERLNMBRDataGridViewTextBoxColumn
            // 
            this.sERLNMBRDataGridViewTextBoxColumn.DataPropertyName = "SERLNMBR";
            this.sERLNMBRDataGridViewTextBoxColumn.HeaderText = "Šarže/SN";
            this.sERLNMBRDataGridViewTextBoxColumn.Name = "sERLNMBRDataGridViewTextBoxColumn";
            this.sERLNMBRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // expiraceDataGridViewTextBoxColumn
            // 
            this.expiraceDataGridViewTextBoxColumn.DataPropertyName = "Expirace";
            this.expiraceDataGridViewTextBoxColumn.HeaderText = "Expirace";
            this.expiraceDataGridViewTextBoxColumn.Name = "expiraceDataGridViewTextBoxColumn";
            this.expiraceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "status";
            this.dataGridViewTextBoxColumn2.HeaderText = "Status";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // bs_I123
            // 
            this.bs_I123.DataMember = "CZMST_I123";
            this.bs_I123.DataSource = this.ds_I123;
            // 
            // ds_I123
            // 
            this.ds_I123.DataSetName = "SkladLokace_CompareToIS";
            this.ds_I123.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dg_SearchToolBar_I123
            // 
            this.dg_SearchToolBar_I123.AllowMerge = false;
            this.dg_SearchToolBar_I123.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.dg_SearchToolBar_I123.Location = new System.Drawing.Point(0, 40);
            this.dg_SearchToolBar_I123.MaximumSize = new System.Drawing.Size(0, 27);
            this.dg_SearchToolBar_I123.MinimumSize = new System.Drawing.Size(0, 27);
            this.dg_SearchToolBar_I123.Name = "dg_SearchToolBar_I123";
            this.dg_SearchToolBar_I123.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.dg_SearchToolBar_I123.Size = new System.Drawing.Size(467, 27);
            this.dg_SearchToolBar_I123.TabIndex = 0;
            this.dg_SearchToolBar_I123.Text = "advancedDataGridViewSearchToolBar2";
            this.dg_SearchToolBar_I123.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.dg_SearchToolBar_I123_Search);
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
            // FormInventura_Porovnani
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.Name = "FormInventura_Porovnani";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Porovnání zásob IS vs. LokMech";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormInventura_Porovnani_FormClosing);
            this.Load += new System.EventHandler(this.FormInventura_Porovnani_Load);
            this.Shown += new System.EventHandler(this.FormInventura_Porovnani_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormInventura_Porovnani_KeyDown);
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
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_I4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_I4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_I4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_I123)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_I123)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_I123)).EndInit();
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
        private Zuby.ADGV.AdvancedDataGridView dg_I4;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar dg_SearchToolBar_I4;
        private Zuby.ADGV.AdvancedDataGridView dg_I123;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar dg_SearchToolBar_I123;
        private Button buttonVyhledat;
        private System.ComponentModel.BackgroundWorker bw_LoadData;
        private Fask.Interfaces.DataSets.Inventura_Compare ds_I4;
        private Fask.Interfaces.DataSets.Inventura_Compare ds_I123;
        private BindingSource bs_I4;
        private BindingSource bs_I123;
        private ProgressControls.ProgressIndicator progressIndicator_I123;
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
        private ProgressControls.ProgressIndicator progressIndicator_I4;
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
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tssl_I4_Count;
        private StatusStrip statusStrip2;
        private ToolStripStatusLabel tssl_I123_Count;
        private Label label5;
        private Label label6;
        private ToolTip toolTip1;
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
        private ToolStripMenuItem tsmiExportDoCSVVse_I123;
        private ToolStripMenuItem tsmiExportDoCSVOznacene_I123;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tsmiExportDoExcelVse_I123;
        private ToolStripMenuItem tsmiExportDoExceOznacene_I123;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem tsmiExportDoXMLVse_I123;
        private ToolStripMenuItem tsmiExportDoXMLOznacene_I123;
        private TextBox tb_CountEntries;
        private Label label7;
        private CheckBox chb_V_3;
        private CheckBox chb_V_2;
        private CheckBox chb_V_1;
        private CheckBox chb_V_0;
        private CheckBox chb_V_4;
        private ToolStripMenuItem výstupToolStripMenuItem;
        private ToolStripMenuItem tiskToolStripMenuItem;
        private DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn ITEMCODE;
        private DataGridViewTextBoxColumn vNDITNUMDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn sKLDESCDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn qUANTITYDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn mJDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn sERLNMBRDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn expiraceDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn status;
        private DataGridViewTextBoxColumn countEntriesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iTEMNMBRDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iTEMDESCDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn vNDITNUMDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sKLDESCDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn qUANTITYDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn mJDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sERLNMBRDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn expiraceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}