namespace Fask.MST_W.Prijem_4
{
    partial class PrijemVyberPolozky
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrijemVyberPolozky));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new Fask.Graphic.GraphicButton();
            this.buttonStorno = new Fask.Graphic.GraphicButton();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.cZMSTPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid1 = new Fask.Graphic.DataGrid2();
            this.prijem = new Fask.SQLiteDBs.DataSets.Prijem();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dgPONUMBER = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgITEMNMBR = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgITEMDESC = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgORD = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.dgVNDDOCNM = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgVNDITNUM = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgCZCARKOD = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgLOCNCODE = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgQTYSHPPD = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.dgQTYPACK = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.dgNasnimano = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.dgZbyva = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.dgMJ = new Fask.Graphic.DataGrid2NumberBoxColumn();
            this.dgWEIGHT = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dgITEMCODE = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.dfWEIGHT = new Fask.Graphic.DataField();
            this.dfITEMCODE = new Fask.Graphic.DataField();
            this.dfMJ = new Fask.Graphic.DataField();
            this.dfZbyva = new Fask.Graphic.DataField();
            this.dfNasnimano = new Fask.Graphic.DataField();
            this.dfQTYSHPPD = new Fask.Graphic.DataField();
            this.dfPONUMBER = new Fask.Graphic.DataField();
            this.dfORD = new Fask.Graphic.DataField();
            this.df_LOCNCODE = new Fask.Graphic.DataField();
            this.df_CZCARKOD = new Fask.Graphic.DataField();
            this.df_ITEMDESC = new Fask.Graphic.DataField();
            this.df_VNDDOCNM = new Fask.Graphic.DataField();
            this.df_VNDITNUM = new Fask.Graphic.DataField();
            this.df_QTYPACK = new Fask.Graphic.DataField();
            this.df_itemnmbr = new Fask.Graphic.DataField();
            this.dgNMBRPAL = new Fask.Graphic.DataGrid2TextBoxColumn();
            this.dfNMBRPAL = new Fask.Graphic.DataField();
            this.panelButtons.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cZMSTPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prijem)).BeginInit();
            this.panelDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem4);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItem3
            // 
            resources.ApplyResources(this.menuItem3, "menuItem3");
            this.menuItem3.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // menuItem2
            // 
            resources.ApplyResources(this.menuItem2, "menuItem2");
            this.menuItem2.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.MenuItems.Add(this.menuItem5);
            this.menuItem4.MenuItems.Add(this.menuItem6);
            resources.ApplyResources(this.menuItem4, "menuItem4");
            // 
            // menuItem5
            // 
            resources.ApplyResources(this.menuItem5, "menuItem5");
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem6
            // 
            resources.ApplyResources(this.menuItem6, "menuItem6");
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonStorno);
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Resize += new System.EventHandler(this.panelButtons_Resize);
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
            this.buttonOK.Resize += new System.EventHandler(this.buttonOK_Resize);
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
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dataGrid1);
            resources.ApplyResources(this.panelGrid, "panelGrid");
            this.panelGrid.Name = "panelGrid";
            // 
            // cZMSTPEBindingSource
            // 
            this.cZMSTPEBindingSource.DataMember = "CZMST_PE";
            this.cZMSTPEBindingSource.DataSource = this.prijem;
            this.cZMSTPEBindingSource.Sort = "";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColorAlternating = System.Drawing.Color.Gold;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.CurrentRow = null;
            this.dataGrid1.DataSource = this.cZMSTPEBindingSource;
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
            this.dataGrid1.CurrentRowIndexChanged += new System.EventHandler(this.dataGrid1_CurrentRowIndexChanged);
            // 
            // prijem
            // 
            this.prijem.DataSetName = "Prijem";
            this.prijem.Prefix = "";
            this.prijem.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgPONUMBER);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgITEMNMBR);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgITEMDESC);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgORD);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgVNDDOCNM);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgVNDITNUM);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgCZCARKOD);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgLOCNCODE);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgQTYSHPPD);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgQTYPACK);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgNasnimano);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgZbyva);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgMJ);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgWEIGHT);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgITEMCODE);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dgNMBRPAL);
            this.dataGridTableStyle1.MappingName = "CZMST_PE";
            // 
            // dgPONUMBER
            // 
            this.dgPONUMBER.Alignment = System.Drawing.StringAlignment.Near;
            this.dgPONUMBER.Format = "";
            this.dgPONUMBER.FormatInfo = null;
            resources.ApplyResources(this.dgPONUMBER, "dgPONUMBER");
            this.dgPONUMBER.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgPONUMBER.SelectionShow = false;
            this.dgPONUMBER.Tag = "";
            // 
            // dgITEMNMBR
            // 
            this.dgITEMNMBR.Alignment = System.Drawing.StringAlignment.Near;
            this.dgITEMNMBR.Format = "";
            this.dgITEMNMBR.FormatInfo = null;
            resources.ApplyResources(this.dgITEMNMBR, "dgITEMNMBR");
            this.dgITEMNMBR.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgITEMNMBR.SelectionShow = false;
            this.dgITEMNMBR.Tag = "";
            // 
            // dgITEMDESC
            // 
            this.dgITEMDESC.Alignment = System.Drawing.StringAlignment.Near;
            this.dgITEMDESC.Format = "";
            this.dgITEMDESC.FormatInfo = null;
            resources.ApplyResources(this.dgITEMDESC, "dgITEMDESC");
            this.dgITEMDESC.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgITEMDESC.SelectionShow = false;
            this.dgITEMDESC.Tag = "";
            // 
            // dgORD
            // 
            this.dgORD.Alignment = System.Drawing.StringAlignment.Near;
            this.dgORD.Format = "";
            this.dgORD.FormatInfo = null;
            resources.ApplyResources(this.dgORD, "dgORD");
            this.dgORD.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgORD.SelectionShow = false;
            this.dgORD.Tag = "";
            // 
            // dgVNDDOCNM
            // 
            this.dgVNDDOCNM.Alignment = System.Drawing.StringAlignment.Near;
            this.dgVNDDOCNM.Format = "";
            this.dgVNDDOCNM.FormatInfo = null;
            resources.ApplyResources(this.dgVNDDOCNM, "dgVNDDOCNM");
            this.dgVNDDOCNM.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgVNDDOCNM.SelectionShow = false;
            this.dgVNDDOCNM.Tag = "";
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
            // dgCZCARKOD
            // 
            this.dgCZCARKOD.Alignment = System.Drawing.StringAlignment.Near;
            this.dgCZCARKOD.Format = "";
            this.dgCZCARKOD.FormatInfo = null;
            resources.ApplyResources(this.dgCZCARKOD, "dgCZCARKOD");
            this.dgCZCARKOD.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgCZCARKOD.SelectionShow = false;
            this.dgCZCARKOD.Tag = "";
            // 
            // dgLOCNCODE
            // 
            this.dgLOCNCODE.Alignment = System.Drawing.StringAlignment.Near;
            this.dgLOCNCODE.Format = "";
            this.dgLOCNCODE.FormatInfo = null;
            resources.ApplyResources(this.dgLOCNCODE, "dgLOCNCODE");
            this.dgLOCNCODE.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgLOCNCODE.SelectionShow = false;
            this.dgLOCNCODE.Tag = "";
            // 
            // dgQTYSHPPD
            // 
            this.dgQTYSHPPD.Alignment = System.Drawing.StringAlignment.Near;
            this.dgQTYSHPPD.Format = "";
            this.dgQTYSHPPD.FormatInfo = null;
            resources.ApplyResources(this.dgQTYSHPPD, "dgQTYSHPPD");
            this.dgQTYSHPPD.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgQTYSHPPD.SelectionShow = false;
            this.dgQTYSHPPD.Tag = "";
            // 
            // dgQTYPACK
            // 
            this.dgQTYPACK.Alignment = System.Drawing.StringAlignment.Far;
            this.dgQTYPACK.Format = "";
            this.dgQTYPACK.FormatInfo = null;
            resources.ApplyResources(this.dgQTYPACK, "dgQTYPACK");
            this.dgQTYPACK.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgQTYPACK.SelectionShow = false;
            this.dgQTYPACK.Tag = "";
            // 
            // dgNasnimano
            // 
            this.dgNasnimano.Alignment = System.Drawing.StringAlignment.Near;
            this.dgNasnimano.Format = "";
            this.dgNasnimano.FormatInfo = null;
            resources.ApplyResources(this.dgNasnimano, "dgNasnimano");
            this.dgNasnimano.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgNasnimano.SelectionShow = false;
            this.dgNasnimano.Tag = "";
            // 
            // dgZbyva
            // 
            this.dgZbyva.Alignment = System.Drawing.StringAlignment.Near;
            this.dgZbyva.Format = "";
            this.dgZbyva.FormatInfo = null;
            resources.ApplyResources(this.dgZbyva, "dgZbyva");
            this.dgZbyva.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgZbyva.SelectionShow = false;
            this.dgZbyva.Tag = "";
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
            // dgWEIGHT
            // 
            this.dgWEIGHT.Alignment = System.Drawing.StringAlignment.Near;
            this.dgWEIGHT.Format = "";
            this.dgWEIGHT.FormatInfo = null;
            resources.ApplyResources(this.dgWEIGHT, "dgWEIGHT");
            this.dgWEIGHT.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgWEIGHT.SelectionShow = false;
            this.dgWEIGHT.Tag = "";
            // 
            // dgITEMCODE
            // 
            this.dgITEMCODE.Alignment = System.Drawing.StringAlignment.Near;
            this.dgITEMCODE.Format = "";
            this.dgITEMCODE.FormatInfo = null;
            resources.ApplyResources(this.dgITEMCODE, "dgITEMCODE");
            this.dgITEMCODE.LineAlignment = System.Drawing.StringAlignment.Near;
            this.dgITEMCODE.SelectionShow = false;
            this.dgITEMCODE.Tag = "";
            // 
            // panelDetail
            // 
            resources.ApplyResources(this.panelDetail, "panelDetail");
            this.panelDetail.Controls.Add(this.dfNMBRPAL);
            this.panelDetail.Controls.Add(this.dfWEIGHT);
            this.panelDetail.Controls.Add(this.dfITEMCODE);
            this.panelDetail.Controls.Add(this.dfMJ);
            this.panelDetail.Controls.Add(this.dfZbyva);
            this.panelDetail.Controls.Add(this.dfNasnimano);
            this.panelDetail.Controls.Add(this.dfQTYSHPPD);
            this.panelDetail.Controls.Add(this.dfPONUMBER);
            this.panelDetail.Controls.Add(this.dfORD);
            this.panelDetail.Controls.Add(this.df_LOCNCODE);
            this.panelDetail.Controls.Add(this.df_CZCARKOD);
            this.panelDetail.Controls.Add(this.df_ITEMDESC);
            this.panelDetail.Controls.Add(this.df_VNDDOCNM);
            this.panelDetail.Controls.Add(this.df_VNDITNUM);
            this.panelDetail.Controls.Add(this.df_QTYPACK);
            this.panelDetail.Controls.Add(this.df_itemnmbr);
            this.panelDetail.Name = "panelDetail";
            // 
            // dfWEIGHT
            // 
            resources.ApplyResources(this.dfWEIGHT, "dfWEIGHT");
            this.dfWEIGHT.Data = "";
            this.dfWEIGHT.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfWEIGHT.DataMaxLength = 32767;
            this.dfWEIGHT.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfWEIGHT.MultiLine = false;
            this.dfWEIGHT.Name = "dfWEIGHT";
            this.dfWEIGHT.Popis = "Váha";
            this.dfWEIGHT.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfWEIGHT.PopisWidth = 70;
            this.dfWEIGHT.ReadOnly = true;
            // 
            // dfITEMCODE
            // 
            resources.ApplyResources(this.dfITEMCODE, "dfITEMCODE");
            this.dfITEMCODE.Data = "";
            this.dfITEMCODE.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfITEMCODE.DataMaxLength = 32767;
            this.dfITEMCODE.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfITEMCODE.MultiLine = false;
            this.dfITEMCODE.Name = "dfITEMCODE";
            this.dfITEMCODE.Popis = "Kód položky";
            this.dfITEMCODE.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfITEMCODE.PopisWidth = 70;
            this.dfITEMCODE.ReadOnly = true;
            // 
            // dfMJ
            // 
            resources.ApplyResources(this.dfMJ, "dfMJ");
            this.dfMJ.Data = "";
            this.dfMJ.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfMJ.DataMaxLength = 32767;
            this.dfMJ.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfMJ.MultiLine = false;
            this.dfMJ.Name = "dfMJ";
            this.dfMJ.Popis = "MJ";
            this.dfMJ.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfMJ.PopisWidth = 70;
            this.dfMJ.ReadOnly = true;
            // 
            // dfZbyva
            // 
            resources.ApplyResources(this.dfZbyva, "dfZbyva");
            this.dfZbyva.Data = "";
            this.dfZbyva.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfZbyva.DataMaxLength = 32767;
            this.dfZbyva.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfZbyva.MultiLine = false;
            this.dfZbyva.Name = "dfZbyva";
            this.dfZbyva.Popis = "Zbývá";
            this.dfZbyva.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfZbyva.PopisWidth = 70;
            this.dfZbyva.ReadOnly = true;
            // 
            // dfNasnimano
            // 
            resources.ApplyResources(this.dfNasnimano, "dfNasnimano");
            this.dfNasnimano.Data = "";
            this.dfNasnimano.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfNasnimano.DataMaxLength = 32767;
            this.dfNasnimano.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfNasnimano.MultiLine = false;
            this.dfNasnimano.Name = "dfNasnimano";
            this.dfNasnimano.Popis = "Nasnímáno";
            this.dfNasnimano.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfNasnimano.PopisWidth = 70;
            this.dfNasnimano.ReadOnly = true;
            // 
            // dfQTYSHPPD
            // 
            resources.ApplyResources(this.dfQTYSHPPD, "dfQTYSHPPD");
            this.dfQTYSHPPD.Data = "";
            this.dfQTYSHPPD.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfQTYSHPPD.DataMaxLength = 32767;
            this.dfQTYSHPPD.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfQTYSHPPD.MultiLine = false;
            this.dfQTYSHPPD.Name = "dfQTYSHPPD";
            this.dfQTYSHPPD.Popis = "Množství";
            this.dfQTYSHPPD.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfQTYSHPPD.PopisWidth = 70;
            this.dfQTYSHPPD.ReadOnly = true;
            // 
            // dfPONUMBER
            // 
            resources.ApplyResources(this.dfPONUMBER, "dfPONUMBER");
            this.dfPONUMBER.Data = "";
            this.dfPONUMBER.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfPONUMBER.DataMaxLength = 32767;
            this.dfPONUMBER.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfPONUMBER.MultiLine = false;
            this.dfPONUMBER.Name = "dfPONUMBER";
            this.dfPONUMBER.Popis = "Objednávka";
            this.dfPONUMBER.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfPONUMBER.PopisWidth = 70;
            this.dfPONUMBER.ReadOnly = true;
            // 
            // dfORD
            // 
            resources.ApplyResources(this.dfORD, "dfORD");
            this.dfORD.Data = "";
            this.dfORD.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfORD.DataMaxLength = 32767;
            this.dfORD.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfORD.MultiLine = false;
            this.dfORD.Name = "dfORD";
            this.dfORD.Popis = "Pořadí";
            this.dfORD.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfORD.PopisWidth = 70;
            this.dfORD.ReadOnly = true;
            // 
            // df_LOCNCODE
            // 
            resources.ApplyResources(this.df_LOCNCODE, "df_LOCNCODE");
            this.df_LOCNCODE.Data = "";
            this.df_LOCNCODE.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_LOCNCODE.DataMaxLength = 32767;
            this.df_LOCNCODE.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.df_LOCNCODE.MultiLine = false;
            this.df_LOCNCODE.Name = "df_LOCNCODE";
            this.df_LOCNCODE.Popis = "Lokace";
            this.df_LOCNCODE.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_LOCNCODE.PopisWidth = 70;
            this.df_LOCNCODE.ReadOnly = true;
            // 
            // df_CZCARKOD
            // 
            resources.ApplyResources(this.df_CZCARKOD, "df_CZCARKOD");
            this.df_CZCARKOD.Data = "";
            this.df_CZCARKOD.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_CZCARKOD.DataMaxLength = 32767;
            this.df_CZCARKOD.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.df_CZCARKOD.MultiLine = false;
            this.df_CZCARKOD.Name = "df_CZCARKOD";
            this.df_CZCARKOD.Popis = "Čár.kód 2";
            this.df_CZCARKOD.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_CZCARKOD.PopisWidth = 70;
            this.df_CZCARKOD.ReadOnly = true;
            // 
            // df_ITEMDESC
            // 
            resources.ApplyResources(this.df_ITEMDESC, "df_ITEMDESC");
            this.df_ITEMDESC.Data = "";
            this.df_ITEMDESC.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_ITEMDESC.DataMaxLength = 32767;
            this.df_ITEMDESC.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.df_ITEMDESC.MultiLine = true;
            this.df_ITEMDESC.Name = "df_ITEMDESC";
            this.df_ITEMDESC.Popis = "Název";
            this.df_ITEMDESC.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_ITEMDESC.PopisWidth = 70;
            this.df_ITEMDESC.ReadOnly = true;
            // 
            // df_VNDDOCNM
            // 
            resources.ApplyResources(this.df_VNDDOCNM, "df_VNDDOCNM");
            this.df_VNDDOCNM.Data = "";
            this.df_VNDDOCNM.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_VNDDOCNM.DataMaxLength = 32767;
            this.df_VNDDOCNM.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.df_VNDDOCNM.MultiLine = false;
            this.df_VNDDOCNM.Name = "df_VNDDOCNM";
            this.df_VNDDOCNM.Popis = "VNDDOCNM";
            this.df_VNDDOCNM.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_VNDDOCNM.PopisWidth = 70;
            this.df_VNDDOCNM.ReadOnly = true;
            // 
            // df_VNDITNUM
            // 
            resources.ApplyResources(this.df_VNDITNUM, "df_VNDITNUM");
            this.df_VNDITNUM.Data = "";
            this.df_VNDITNUM.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_VNDITNUM.DataMaxLength = 32767;
            this.df_VNDITNUM.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.df_VNDITNUM.MultiLine = false;
            this.df_VNDITNUM.Name = "df_VNDITNUM";
            this.df_VNDITNUM.Popis = "Čár.kód";
            this.df_VNDITNUM.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_VNDITNUM.PopisWidth = 70;
            this.df_VNDITNUM.ReadOnly = true;
            // 
            // df_QTYPACK
            // 
            resources.ApplyResources(this.df_QTYPACK, "df_QTYPACK");
            this.df_QTYPACK.Data = "";
            this.df_QTYPACK.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_QTYPACK.DataMaxLength = 32767;
            this.df_QTYPACK.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.df_QTYPACK.MultiLine = false;
            this.df_QTYPACK.Name = "df_QTYPACK";
            this.df_QTYPACK.Popis = "Balení";
            this.df_QTYPACK.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_QTYPACK.PopisWidth = 70;
            this.df_QTYPACK.ReadOnly = true;
            // 
            // df_itemnmbr
            // 
            resources.ApplyResources(this.df_itemnmbr, "df_itemnmbr");
            this.df_itemnmbr.Data = "";
            this.df_itemnmbr.DataBackColor = System.Drawing.SystemColors.Window;
            this.df_itemnmbr.DataMaxLength = 32767;
            this.df_itemnmbr.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.df_itemnmbr.MultiLine = false;
            this.df_itemnmbr.Name = "df_itemnmbr";
            this.df_itemnmbr.Popis = "Položka č.";
            this.df_itemnmbr.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.df_itemnmbr.PopisWidth = 70;
            this.df_itemnmbr.ReadOnly = true;
            // 
            // dgNMBRPAL
            // 
            this.dgNMBRPAL.Format = "";
            this.dgNMBRPAL.FormatInfo = null;
            resources.ApplyResources(this.dgNMBRPAL, "dgNMBRPAL");
            // 
            // dfNMBRPAL
            // 
            resources.ApplyResources(this.dfNMBRPAL, "dfNMBRPAL");
            this.dfNMBRPAL.Data = "";
            this.dfNMBRPAL.DataBackColor = System.Drawing.SystemColors.Window;
            this.dfNMBRPAL.DataMaxLength = 32767;
            this.dfNMBRPAL.DataTextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dfNMBRPAL.MultiLine = false;
            this.dfNMBRPAL.Name = "dfNMBRPAL";
            this.dfNMBRPAL.Popis = "SSCC palety";
            this.dfNMBRPAL.PopisTextAlign = System.Drawing.ContentAlignment.TopRight;
            this.dfNMBRPAL.PopisWidth = 70;
            this.dfNMBRPAL.ReadOnly = true;
            // 
            // PrijemVyberPolozky
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "PrijemVyberPolozky";
            this.Load += new System.EventHandler(this.PrijemVyberPolozky_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrijemVyberPolozky_KeyDown);
            this.panelButtons.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cZMSTPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prijem)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private Fask.Graphic.GraphicButton buttonOK;
        private Fask.Graphic.GraphicButton buttonStorno;
        private System.Windows.Forms.Panel panelDetail;
        private Fask.Graphic.DataGrid2 dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private Fask.Graphic.DataGrid2TextBoxColumn dgITEMNMBR;
        private Fask.Graphic.DataGrid2TextBoxColumn dgITEMDESC;
        private Fask.Graphic.DataGrid2NumberBoxColumn dgQTYPACK;
        private Fask.Graphic.DataGrid2TextBoxColumn dgVNDDOCNM;
        private Fask.Graphic.DataField df_ITEMDESC;
        private Fask.Graphic.DataField df_itemnmbr;
        private Fask.Graphic.DataField df_VNDDOCNM;
        private Fask.Graphic.DataField df_QTYPACK;
        private Fask.SQLiteDBs.DataSets.Prijem prijem;
        private System.Windows.Forms.BindingSource cZMSTPEBindingSource;
        private Fask.Graphic.DataGrid2TextBoxColumn dgVNDITNUM;
        private Fask.Graphic.DataGrid2TextBoxColumn dgCZCARKOD;
        private Fask.Graphic.DataGrid2TextBoxColumn dgLOCNCODE;
        private Fask.Graphic.DataField df_CZCARKOD;
        private Fask.Graphic.DataField df_VNDITNUM;
        private Fask.Graphic.DataField df_LOCNCODE;
        private Fask.Graphic.DataGrid2NumberBoxColumn dgQTYSHPPD;
        private Fask.Graphic.DataGrid2TextBoxColumn dgPONUMBER;
        private Fask.Graphic.DataGrid2NumberBoxColumn dgORD;
        private Fask.Graphic.DataGrid2NumberBoxColumn dgNasnimano;
        private Fask.Graphic.DataGrid2NumberBoxColumn dgZbyva;
        private Fask.Graphic.DataField dfPONUMBER;
        private Fask.Graphic.DataField dfORD;
        private Fask.Graphic.DataField dfZbyva;
        private Fask.Graphic.DataField dfNasnimano;
        private Fask.Graphic.DataField dfQTYSHPPD;
        private Fask.Graphic.DataGrid2NumberBoxColumn dgMJ;
        private Fask.Graphic.DataField dfMJ;
        private Fask.Graphic.DataField dfWEIGHT;
        private Fask.Graphic.DataField dfITEMCODE;
        private Fask.Graphic.DataGrid2TextBoxColumn dgWEIGHT;
        private Fask.Graphic.DataGrid2TextBoxColumn dgITEMCODE;
        private Fask.Graphic.DataGrid2TextBoxColumn dgNMBRPAL;
        private Fask.Graphic.DataField dfNMBRPAL;
    }
}