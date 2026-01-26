namespace Konzola.Vyroba
{
    partial class FormVazby_P_PS_List
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVazby_P_PS_List));
            this.dgVyrobky = new Zuby.ADGV.AdvancedDataGridView();
            this.CountEntries = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPNUMBE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMMJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEMODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEPREPSTART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEPREPSTOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEPREP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMEUNIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMESTART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMESTOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMECORSTART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMECORSTOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMECOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMECRID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMECRIDTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operationid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateeve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyReal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYPACK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYPACKMJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarcodeP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TermID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISOK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOUBEHGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CORRGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyOld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idVS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateedit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCNCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERLTNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXPIRATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REZ_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REZ_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REZ_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REZ_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REZ_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_vyrobky = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet_Vyrobky = new Fask.Interfaces.DataSets.Vyroba();
            this.panelButtons = new Fask.AdvancedButtonsPanel.ButtonsPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.progressIndicatorVyrobek = new ProgressControls.ProgressIndicator();
            this.advancedDataGridViewSearchToolBar_Vyrobky = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel_FiltryVyrobky = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFiltry_Vyrobek = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNastavit_Vyrobek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZmena_Vyrobek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPridat_Vyrobek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdebrat_Vyrobek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVycistit_Vyrobek = new System.Windows.Forms.ToolStripButton();
            this.comboBox_Vyrobek_MJ = new System.Windows.Forms.ComboBox();
            this.comboBox_Vyrobek_ITEMDESC = new System.Windows.Forms.ComboBox();
            this.comboBox_Vyrobek_ITEMNMBR = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_Filtr_Vyrobky = new System.Windows.Forms.Button();
            this.progressIndicatorMaterial = new ProgressControls.ProgressIndicator();
            this.dgMaterialy = new Zuby.ADGV.AdvancedDataGridView();
            this.CountEntries_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOPNUMBE_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNMBR_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMTYPE_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_ID_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKL_DESC_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCNCODE_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYSHPPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYSHPPDMJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTYPACK_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERLTNUM_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GUID_Production = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GUID_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USER_ID_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TERMINAL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEX_ROW_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NMBRPAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TYPEPAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRINTED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISOK_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idVS_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateedit_PS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_materialy = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet_Materialy = new Fask.Interfaces.DataSets.Vyroba();
            this.advancedDataGridViewSearchToolBar_Materialy = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_MJ = new System.Windows.Forms.ComboBox();
            this.comboBox_ITEMDESC = new System.Windows.Forms.ComboBox();
            this.button_filtr_Material = new System.Windows.Forms.Button();
            this.comboBox_ITEMNMBR = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFiltry_Material = new System.Windows.Forms.ToolStripComboBox();
            this.tsbNastavit_Material = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZmena_Material = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPridat_Material = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOdebrat_Material = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVycistit_Material = new System.Windows.Forms.ToolStripButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ZruseniPriznakuISOK = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZpracovatVyrobu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiupravitVyrobek = new System.Windows.Forms.ToolStripMenuItem();
            this.bw_Materialy_stav = new System.ComponentModel.BackgroundWorker();
            this.bw_Vyrobky_stav = new System.ComponentModel.BackgroundWorker();
            this.bw_ImportToIS = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgVyrobky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_vyrobky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet_Vyrobky)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel_FiltryVyrobky.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMaterialy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_materialy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet_Materialy)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgVyrobky
            // 
            this.dgVyrobky.AllowUserToAddRows = false;
            this.dgVyrobky.AllowUserToDeleteRows = false;
            this.dgVyrobky.AllowUserToOrderColumns = true;
            this.dgVyrobky.AllowUserToResizeRows = false;
            this.dgVyrobky.AutoGenerateColumns = false;
            this.dgVyrobky.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVyrobky.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CountEntries,
            this.SOPNUMBE,
            this.ITEMNMBR,
            this.ITEMTYPE,
            this.ITEMMJ,
            this.ITEMDESC,
            this.ORD,
            this.TIMEMODE,
            this.TIMEPREPSTART,
            this.TIMEPREPSTOP,
            this.TIMEPREP,
            this.TIMEUNIT,
            this.TIMESTART,
            this.TIMESTOP,
            this.TIMECORSTART,
            this.TIMECORSTOP,
            this.TIMECOR,
            this.TIMECRID,
            this.TIMECRIDTYPE,
            this.id,
            this.loginid,
            this.machineid,
            this.operationid,
            this.dateeve,
            this.qty,
            this.qtyReal,
            this.QTYPACK,
            this.QTYPACKMJ,
            this.description,
            this.BarcodeP,
            this.UserID,
            this.TermID,
            this.ISOK,
            this.GUID,
            this.SOUBEHGUID,
            this.CORRGUID,
            this.qtyOld,
            this.idVS,
            this.dateedit,
            this.SKL_ID,
            this.SKL_DESC,
            this.LOCNCODE,
            this.SERLTNUM,
            this.EXPIRATION,
            this.REZ_1,
            this.REZ_2,
            this.REZ_3,
            this.REZ_4,
            this.REZ_5});
            this.dgVyrobky.DataSource = this.bs_vyrobky;
            this.dgVyrobky.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVyrobky.EnableHeadersVisualStyles = false;
            this.dgVyrobky.FilterAndSortEnabled = true;
            this.dgVyrobky.Location = new System.Drawing.Point(0, 169);
            this.dgVyrobky.MultiSelect = false;
            this.dgVyrobky.Name = "dgVyrobky";
            this.dgVyrobky.ReadOnly = true;
            this.dgVyrobky.RowHeadersVisible = false;
            this.dgVyrobky.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVyrobky.Size = new System.Drawing.Size(397, 481);
            this.dgVyrobky.TabIndex = 1;
            this.dgVyrobky.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgVyrobky_DataError);
            this.dgVyrobky.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // CountEntries
            // 
            this.CountEntries.DataPropertyName = "CountEntries";
            this.CountEntries.HeaderText = "Číslo dávky";
            this.CountEntries.Name = "CountEntries";
            this.CountEntries.ReadOnly = true;
            this.CountEntries.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SOPNUMBE
            // 
            this.SOPNUMBE.DataPropertyName = "SOPNUMBE";
            this.SOPNUMBE.HeaderText = "Výrobní Zakázka";
            this.SOPNUMBE.Name = "SOPNUMBE";
            this.SOPNUMBE.ReadOnly = true;
            this.SOPNUMBE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMNMBR
            // 
            this.ITEMNMBR.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR.HeaderText = "Pol. číslo";
            this.ITEMNMBR.Name = "ITEMNMBR";
            this.ITEMNMBR.ReadOnly = true;
            this.ITEMNMBR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMTYPE
            // 
            this.ITEMTYPE.DataPropertyName = "ITEMTYPE";
            this.ITEMTYPE.HeaderText = "Typ položky";
            this.ITEMTYPE.Name = "ITEMTYPE";
            this.ITEMTYPE.ReadOnly = true;
            this.ITEMTYPE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMMJ
            // 
            this.ITEMMJ.DataPropertyName = "ITEMMJ";
            this.ITEMMJ.HeaderText = "Měrná jednotka";
            this.ITEMMJ.Name = "ITEMMJ";
            this.ITEMMJ.ReadOnly = true;
            this.ITEMMJ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMDESC
            // 
            this.ITEMDESC.DataPropertyName = "ITEMDESC";
            this.ITEMDESC.HeaderText = "Popis zboží";
            this.ITEMDESC.Name = "ITEMDESC";
            this.ITEMDESC.ReadOnly = true;
            this.ITEMDESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ORD
            // 
            this.ORD.DataPropertyName = "ORD";
            this.ORD.HeaderText = "Pořadí položky";
            this.ORD.Name = "ORD";
            this.ORD.ReadOnly = true;
            this.ORD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMEMODE
            // 
            this.TIMEMODE.DataPropertyName = "TIMEMODE";
            this.TIMEMODE.HeaderText = "Typ sledování času";
            this.TIMEMODE.Name = "TIMEMODE";
            this.TIMEMODE.ReadOnly = true;
            this.TIMEMODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMEPREPSTART
            // 
            this.TIMEPREPSTART.DataPropertyName = "TIMEPREPSTART";
            this.TIMEPREPSTART.HeaderText = "Start čas přípravy";
            this.TIMEPREPSTART.Name = "TIMEPREPSTART";
            this.TIMEPREPSTART.ReadOnly = true;
            this.TIMEPREPSTART.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMEPREPSTOP
            // 
            this.TIMEPREPSTOP.DataPropertyName = "TIMEPREPSTOP";
            this.TIMEPREPSTOP.HeaderText = "Stop čas přípravy";
            this.TIMEPREPSTOP.Name = "TIMEPREPSTOP";
            this.TIMEPREPSTOP.ReadOnly = true;
            this.TIMEPREPSTOP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMEPREP
            // 
            this.TIMEPREP.DataPropertyName = "TIMEPREP";
            this.TIMEPREP.HeaderText = "Přípravný čas v minutách";
            this.TIMEPREP.Name = "TIMEPREP";
            this.TIMEPREP.ReadOnly = true;
            this.TIMEPREP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMEUNIT
            // 
            this.TIMEUNIT.DataPropertyName = "TIMEUNIT";
            this.TIMEUNIT.HeaderText = "Jednotkový čas";
            this.TIMEUNIT.Name = "TIMEUNIT";
            this.TIMEUNIT.ReadOnly = true;
            this.TIMEUNIT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMESTART
            // 
            this.TIMESTART.DataPropertyName = "TIMESTART";
            this.TIMESTART.HeaderText = "Čas zahájení";
            this.TIMESTART.Name = "TIMESTART";
            this.TIMESTART.ReadOnly = true;
            this.TIMESTART.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMESTOP
            // 
            this.TIMESTOP.DataPropertyName = "TIMESTOP";
            this.TIMESTOP.HeaderText = "Čas ukončení";
            this.TIMESTOP.Name = "TIMESTOP";
            this.TIMESTOP.ReadOnly = true;
            this.TIMESTOP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMECORSTART
            // 
            this.TIMECORSTART.DataPropertyName = "TIMECORSTART";
            this.TIMECORSTART.HeaderText = "Čas zahájení korekce";
            this.TIMECORSTART.Name = "TIMECORSTART";
            this.TIMECORSTART.ReadOnly = true;
            this.TIMECORSTART.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMECORSTOP
            // 
            this.TIMECORSTOP.DataPropertyName = "TIMECORSTOP";
            this.TIMECORSTOP.HeaderText = "Čas ukončení korekce";
            this.TIMECORSTOP.Name = "TIMECORSTOP";
            this.TIMECORSTOP.ReadOnly = true;
            this.TIMECORSTOP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMECOR
            // 
            this.TIMECOR.DataPropertyName = "TIMECOR";
            this.TIMECOR.HeaderText = "Korekce času v minutách";
            this.TIMECOR.Name = "TIMECOR";
            this.TIMECOR.ReadOnly = true;
            this.TIMECOR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMECRID
            // 
            this.TIMECRID.DataPropertyName = "TIMECRID";
            this.TIMECRID.HeaderText = "id korekce";
            this.TIMECRID.Name = "TIMECRID";
            this.TIMECRID.ReadOnly = true;
            this.TIMECRID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TIMECRIDTYPE
            // 
            this.TIMECRIDTYPE.DataPropertyName = "TIMECRIDTYPE";
            this.TIMECRIDTYPE.HeaderText = "Typ korekce";
            this.TIMECRIDTYPE.Name = "TIMECRIDTYPE";
            this.TIMECRIDTYPE.ReadOnly = true;
            this.TIMECRIDTYPE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id události";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // loginid
            // 
            this.loginid.DataPropertyName = "loginid";
            this.loginid.HeaderText = "id vedoucího směny";
            this.loginid.Name = "loginid";
            this.loginid.ReadOnly = true;
            this.loginid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // machineid
            // 
            this.machineid.DataPropertyName = "machineid";
            this.machineid.HeaderText = "id stroje";
            this.machineid.Name = "machineid";
            this.machineid.ReadOnly = true;
            this.machineid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // operationid
            // 
            this.operationid.DataPropertyName = "operationid";
            this.operationid.HeaderText = "id operace";
            this.operationid.Name = "operationid";
            this.operationid.ReadOnly = true;
            this.operationid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dateeve
            // 
            this.dateeve.DataPropertyName = "dateeve";
            this.dateeve.HeaderText = "Datum";
            this.dateeve.Name = "dateeve";
            this.dateeve.ReadOnly = true;
            this.dateeve.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "Počet kusů";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            this.qty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // qtyReal
            // 
            this.qtyReal.DataPropertyName = "qtyReal";
            this.qtyReal.HeaderText = "Počet kusů sejmuto";
            this.qtyReal.Name = "qtyReal";
            this.qtyReal.ReadOnly = true;
            this.qtyReal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // QTYPACK
            // 
            this.QTYPACK.DataPropertyName = "QTYPACK";
            this.QTYPACK.HeaderText = "Množství v balení";
            this.QTYPACK.Name = "QTYPACK";
            this.QTYPACK.ReadOnly = true;
            this.QTYPACK.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // QTYPACKMJ
            // 
            this.QTYPACKMJ.DataPropertyName = "QTYPACKMJ";
            this.QTYPACKMJ.HeaderText = "Měrná jednotka balení";
            this.QTYPACKMJ.Name = "QTYPACKMJ";
            this.QTYPACKMJ.ReadOnly = true;
            this.QTYPACKMJ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // description
            // 
            this.description.DataPropertyName = "description";
            this.description.HeaderText = "Popis";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // BarcodeP
            // 
            this.BarcodeP.DataPropertyName = "BarcodeP";
            this.BarcodeP.HeaderText = "Čár. kód položky";
            this.BarcodeP.Name = "BarcodeP";
            this.BarcodeP.ReadOnly = true;
            this.BarcodeP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // UserID
            // 
            this.UserID.DataPropertyName = "UserID";
            this.UserID.HeaderText = "id pracovníka";
            this.UserID.Name = "UserID";
            this.UserID.ReadOnly = true;
            this.UserID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TermID
            // 
            this.TermID.DataPropertyName = "TermID";
            this.TermID.HeaderText = "id terminálu";
            this.TermID.Name = "TermID";
            this.TermID.ReadOnly = true;
            this.TermID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ISOK
            // 
            this.ISOK.DataPropertyName = "ISOK";
            this.ISOK.HeaderText = "Čas převzetí do IS";
            this.ISOK.Name = "ISOK";
            this.ISOK.ReadOnly = true;
            this.ISOK.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // GUID
            // 
            this.GUID.DataPropertyName = "GUID";
            this.GUID.HeaderText = "GUID";
            this.GUID.Name = "GUID";
            this.GUID.ReadOnly = true;
            this.GUID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SOUBEHGUID
            // 
            this.SOUBEHGUID.DataPropertyName = "SOUBEHGUID";
            this.SOUBEHGUID.HeaderText = "Souběh GUID";
            this.SOUBEHGUID.Name = "SOUBEHGUID";
            this.SOUBEHGUID.ReadOnly = true;
            this.SOUBEHGUID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CORRGUID
            // 
            this.CORRGUID.DataPropertyName = "CORRGUID";
            this.CORRGUID.HeaderText = "Korekce GUID";
            this.CORRGUID.Name = "CORRGUID";
            this.CORRGUID.ReadOnly = true;
            this.CORRGUID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // qtyOld
            // 
            this.qtyOld.DataPropertyName = "qtyOld";
            this.qtyOld.HeaderText = "Původní množství";
            this.qtyOld.Name = "qtyOld";
            this.qtyOld.ReadOnly = true;
            this.qtyOld.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // idVS
            // 
            this.idVS.DataPropertyName = "idVS";
            this.idVS.HeaderText = "id vedoucího směny";
            this.idVS.Name = "idVS";
            this.idVS.ReadOnly = true;
            this.idVS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dateedit
            // 
            this.dateedit.DataPropertyName = "dateedit";
            this.dateedit.HeaderText = "Datum a čas editace";
            this.dateedit.Name = "dateedit";
            this.dateedit.ReadOnly = true;
            this.dateedit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_ID
            // 
            this.SKL_ID.DataPropertyName = "SKL_ID";
            this.SKL_ID.HeaderText = "Sklad ID";
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
            this.SKL_DESC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // LOCNCODE
            // 
            this.LOCNCODE.DataPropertyName = "LOCNCODE";
            this.LOCNCODE.HeaderText = "Lokace";
            this.LOCNCODE.Name = "LOCNCODE";
            this.LOCNCODE.ReadOnly = true;
            this.LOCNCODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SERLTNUM
            // 
            this.SERLTNUM.DataPropertyName = "SERLTNUM";
            this.SERLTNUM.HeaderText = "Šarže/SN";
            this.SERLTNUM.Name = "SERLTNUM";
            this.SERLTNUM.ReadOnly = true;
            // 
            // EXPIRATION
            // 
            this.EXPIRATION.DataPropertyName = "EXPIRATION";
            this.EXPIRATION.HeaderText = "Expirace";
            this.EXPIRATION.Name = "EXPIRATION";
            this.EXPIRATION.ReadOnly = true;
            // 
            // REZ_1
            // 
            this.REZ_1.DataPropertyName = "REZ_1";
            this.REZ_1.HeaderText = "REZ_1";
            this.REZ_1.Name = "REZ_1";
            this.REZ_1.ReadOnly = true;
            // 
            // REZ_2
            // 
            this.REZ_2.DataPropertyName = "REZ_2";
            this.REZ_2.HeaderText = "REZ_2";
            this.REZ_2.Name = "REZ_2";
            this.REZ_2.ReadOnly = true;
            // 
            // REZ_3
            // 
            this.REZ_3.DataPropertyName = "REZ_3";
            this.REZ_3.HeaderText = "REZ_3";
            this.REZ_3.Name = "REZ_3";
            this.REZ_3.ReadOnly = true;
            // 
            // REZ_4
            // 
            this.REZ_4.DataPropertyName = "REZ_4";
            this.REZ_4.HeaderText = "REZ_4";
            this.REZ_4.Name = "REZ_4";
            this.REZ_4.ReadOnly = true;
            // 
            // REZ_5
            // 
            this.REZ_5.DataPropertyName = "REZ_5";
            this.REZ_5.HeaderText = "REZ_5";
            this.REZ_5.Name = "REZ_5";
            this.REZ_5.ReadOnly = true;
            // 
            // bs_vyrobky
            // 
            this.bs_vyrobky.DataMember = "Production_Odvod";
            this.bs_vyrobky.DataSource = this.DataSet_Vyrobky;
            // 
            // DataSet_Vyrobky
            // 
            this.DataSet_Vyrobky.DataSetName = "VyrobaDataSet";
            this.DataSet_Vyrobky.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(735, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(84, 674);
            this.panelButtons.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.menuStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 674);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.progressIndicatorVyrobek);
            this.splitContainer1.Panel1.Controls.Add(this.dgVyrobky);
            this.splitContainer1.Panel1.Controls.Add(this.advancedDataGridViewSearchToolBar_Vyrobky);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.progressIndicatorMaterial);
            this.splitContainer1.Panel2.Controls.Add(this.dgMaterialy);
            this.splitContainer1.Panel2.Controls.Add(this.advancedDataGridViewSearchToolBar_Materialy);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(735, 650);
            this.splitContainer1.SplitterDistance = 397;
            this.splitContainer1.TabIndex = 4;
            // 
            // progressIndicatorVyrobek
            // 
            this.progressIndicatorVyrobek.Location = new System.Drawing.Point(166, 352);
            this.progressIndicatorVyrobek.Name = "progressIndicatorVyrobek";
            this.progressIndicatorVyrobek.Percentage = 0F;
            this.progressIndicatorVyrobek.Size = new System.Drawing.Size(98, 98);
            this.progressIndicatorVyrobek.TabIndex = 39;
            this.progressIndicatorVyrobek.Text = "progressIndicator1";
            this.progressIndicatorVyrobek.Visible = false;
            // 
            // advancedDataGridViewSearchToolBar_Vyrobky
            // 
            this.advancedDataGridViewSearchToolBar_Vyrobky.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar_Vyrobky.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar_Vyrobky.Location = new System.Drawing.Point(0, 142);
            this.advancedDataGridViewSearchToolBar_Vyrobky.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_Vyrobky.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_Vyrobky.Name = "advancedDataGridViewSearchToolBar_Vyrobky";
            this.advancedDataGridViewSearchToolBar_Vyrobky.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar_Vyrobky.Size = new System.Drawing.Size(397, 27);
            this.advancedDataGridViewSearchToolBar_Vyrobky.TabIndex = 40;
            this.advancedDataGridViewSearchToolBar_Vyrobky.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar_Vyrobky.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar_Vyrobky_Search);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel_FiltryVyrobky);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(397, 142);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtr Výrobky";
            // 
            // panel_FiltryVyrobky
            // 
            this.panel_FiltryVyrobky.AutoScroll = true;
            this.panel_FiltryVyrobky.Controls.Add(this.toolStrip2);
            this.panel_FiltryVyrobky.Controls.Add(this.comboBox_Vyrobek_MJ);
            this.panel_FiltryVyrobky.Controls.Add(this.comboBox_Vyrobek_ITEMDESC);
            this.panel_FiltryVyrobky.Controls.Add(this.comboBox_Vyrobek_ITEMNMBR);
            this.panel_FiltryVyrobky.Controls.Add(this.label3);
            this.panel_FiltryVyrobky.Controls.Add(this.label4);
            this.panel_FiltryVyrobky.Controls.Add(this.label5);
            this.panel_FiltryVyrobky.Controls.Add(this.button_Filtr_Vyrobky);
            this.panel_FiltryVyrobky.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_FiltryVyrobky.Location = new System.Drawing.Point(3, 16);
            this.panel_FiltryVyrobky.Name = "panel_FiltryVyrobky";
            this.panel_FiltryVyrobky.Size = new System.Drawing.Size(391, 123);
            this.panel_FiltryVyrobky.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tscbFiltry_Vyrobek,
            this.tsbNastavit_Vyrobek,
            this.toolStripSeparator7,
            this.tsbZmena_Vyrobek,
            this.toolStripSeparator8,
            this.tsbPridat_Vyrobek,
            this.toolStripSeparator9,
            this.tsbOdebrat_Vyrobek,
            this.toolStripSeparator10,
            this.tsbVycistit_Vyrobek});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(391, 25);
            this.toolStrip2.TabIndex = 16;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(30, 22);
            this.toolStripLabel2.Text = "Filtr:";
            // 
            // tscbFiltry_Vyrobek
            // 
            this.tscbFiltry_Vyrobek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry_Vyrobek.Name = "tscbFiltry_Vyrobek";
            this.tscbFiltry_Vyrobek.Size = new System.Drawing.Size(121, 25);
            // 
            // tsbNastavit_Vyrobek
            // 
            this.tsbNastavit_Vyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit_Vyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit_Vyrobek.Image")));
            this.tsbNastavit_Vyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit_Vyrobek.Name = "tsbNastavit_Vyrobek";
            this.tsbNastavit_Vyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbNastavit_Vyrobek.Text = "Nastavit";
            this.tsbNastavit_Vyrobek.ToolTipText = "Nastavit";
            this.tsbNastavit_Vyrobek.Click += new System.EventHandler(this.tsbNastavit_Material_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbZmena_Vyrobek
            // 
            this.tsbZmena_Vyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena_Vyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena_Vyrobek.Image")));
            this.tsbZmena_Vyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena_Vyrobek.Name = "tsbZmena_Vyrobek";
            this.tsbZmena_Vyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbZmena_Vyrobek.Text = "Změna";
            this.tsbZmena_Vyrobek.Click += new System.EventHandler(this.tsbZmena_Vyrobek_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPridat_Vyrobek
            // 
            this.tsbPridat_Vyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat_Vyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat_Vyrobek.Image")));
            this.tsbPridat_Vyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat_Vyrobek.Name = "tsbPridat_Vyrobek";
            this.tsbPridat_Vyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbPridat_Vyrobek.Text = "Uložit";
            this.tsbPridat_Vyrobek.Click += new System.EventHandler(this.tsbPridat_Vyrobek_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOdebrat_Vyrobek
            // 
            this.tsbOdebrat_Vyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat_Vyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat_Vyrobek.Image")));
            this.tsbOdebrat_Vyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat_Vyrobek.Name = "tsbOdebrat_Vyrobek";
            this.tsbOdebrat_Vyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbOdebrat_Vyrobek.Text = "Odebrat";
            this.tsbOdebrat_Vyrobek.Click += new System.EventHandler(this.tsbOdebrat_Vyrobek_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbVycistit_Vyrobek
            // 
            this.tsbVycistit_Vyrobek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit_Vyrobek.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit_Vyrobek.Image")));
            this.tsbVycistit_Vyrobek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit_Vyrobek.Name = "tsbVycistit_Vyrobek";
            this.tsbVycistit_Vyrobek.Size = new System.Drawing.Size(23, 22);
            this.tsbVycistit_Vyrobek.Text = "Vyčistit";
            this.tsbVycistit_Vyrobek.Click += new System.EventHandler(this.tsbVycistit_Vyrobek_Click);
            // 
            // comboBox_Vyrobek_MJ
            // 
            this.comboBox_Vyrobek_MJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Vyrobek_MJ.FormattingEnabled = true;
            this.comboBox_Vyrobek_MJ.Location = new System.Drawing.Point(97, 93);
            this.comboBox_Vyrobek_MJ.Name = "comboBox_Vyrobek_MJ";
            this.comboBox_Vyrobek_MJ.Size = new System.Drawing.Size(103, 21);
            this.comboBox_Vyrobek_MJ.TabIndex = 5;
            // 
            // comboBox_Vyrobek_ITEMDESC
            // 
            this.comboBox_Vyrobek_ITEMDESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Vyrobek_ITEMDESC.FormattingEnabled = true;
            this.comboBox_Vyrobek_ITEMDESC.Location = new System.Drawing.Point(97, 66);
            this.comboBox_Vyrobek_ITEMDESC.Name = "comboBox_Vyrobek_ITEMDESC";
            this.comboBox_Vyrobek_ITEMDESC.Size = new System.Drawing.Size(103, 21);
            this.comboBox_Vyrobek_ITEMDESC.TabIndex = 7;
            // 
            // comboBox_Vyrobek_ITEMNMBR
            // 
            this.comboBox_Vyrobek_ITEMNMBR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Vyrobek_ITEMNMBR.FormattingEnabled = true;
            this.comboBox_Vyrobek_ITEMNMBR.Location = new System.Drawing.Point(97, 39);
            this.comboBox_Vyrobek_ITEMNMBR.Name = "comboBox_Vyrobek_ITEMNMBR";
            this.comboBox_Vyrobek_ITEMNMBR.Size = new System.Drawing.Size(103, 21);
            this.comboBox_Vyrobek_ITEMNMBR.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Měrná jednotka:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Pol. Číslo:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Popis :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_Filtr_Vyrobky
            // 
            this.button_Filtr_Vyrobky.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Filtr_Vyrobky.Location = new System.Drawing.Point(206, 36);
            this.button_Filtr_Vyrobky.Name = "button_Filtr_Vyrobky";
            this.button_Filtr_Vyrobky.Size = new System.Drawing.Size(82, 78);
            this.button_Filtr_Vyrobky.TabIndex = 0;
            this.button_Filtr_Vyrobky.Text = "Vyhledat";
            this.button_Filtr_Vyrobky.UseVisualStyleBackColor = true;
            this.button_Filtr_Vyrobky.Click += new System.EventHandler(this.button_Filtr_Vyrobky_Click);
            // 
            // progressIndicatorMaterial
            // 
            this.progressIndicatorMaterial.Location = new System.Drawing.Point(111, 352);
            this.progressIndicatorMaterial.Name = "progressIndicatorMaterial";
            this.progressIndicatorMaterial.Percentage = 0F;
            this.progressIndicatorMaterial.Size = new System.Drawing.Size(98, 98);
            this.progressIndicatorMaterial.TabIndex = 39;
            this.progressIndicatorMaterial.Text = "progressIndicator1";
            this.progressIndicatorMaterial.Visible = false;
            // 
            // dgMaterialy
            // 
            this.dgMaterialy.AllowUserToAddRows = false;
            this.dgMaterialy.AllowUserToDeleteRows = false;
            this.dgMaterialy.AllowUserToOrderColumns = true;
            this.dgMaterialy.AllowUserToResizeRows = false;
            this.dgMaterialy.AutoGenerateColumns = false;
            this.dgMaterialy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMaterialy.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CountEntries_PS,
            this.SOPNUMBE_PS,
            this.ITEMNAME,
            this.ITEMNMBR_PS,
            this.ITEMTYPE_PS,
            this.SKL_ID_PS,
            this.SKL_DESC_PS,
            this.ITEMCODE,
            this.LOCNCODE_PS,
            this.MJ,
            this.QTYSHPPD,
            this.QTYSHPPDMJ,
            this.QTYPACK_PS,
            this.SERLTNUM_PS,
            this.GUID_Production,
            this.GUID_PS,
            this.USER_ID_PS,
            this.TERMINAL_ID,
            this.DEX_ROW_ID,
            this.WEIGHT,
            this.NMBRPAL,
            this.TYPEPAL,
            this.PRINTED,
            this.ISOK_PS,
            this.idVS_PS,
            this.dateedit_PS});
            this.dgMaterialy.DataSource = this.bs_materialy;
            this.dgMaterialy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMaterialy.EnableHeadersVisualStyles = false;
            this.dgMaterialy.FilterAndSortEnabled = true;
            this.dgMaterialy.Location = new System.Drawing.Point(0, 169);
            this.dgMaterialy.Name = "dgMaterialy";
            this.dgMaterialy.ReadOnly = true;
            this.dgMaterialy.RowHeadersVisible = false;
            this.dgMaterialy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMaterialy.Size = new System.Drawing.Size(334, 481);
            this.dgMaterialy.TabIndex = 2;
            this.dgMaterialy.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgMaterialy_DataError);
            // 
            // CountEntries_PS
            // 
            this.CountEntries_PS.DataPropertyName = "CountEntries";
            this.CountEntries_PS.HeaderText = "Číslo dávky";
            this.CountEntries_PS.Name = "CountEntries_PS";
            this.CountEntries_PS.ReadOnly = true;
            this.CountEntries_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SOPNUMBE_PS
            // 
            this.SOPNUMBE_PS.DataPropertyName = "SOPNUMBE";
            this.SOPNUMBE_PS.HeaderText = "Číslo zakázky";
            this.SOPNUMBE_PS.Name = "SOPNUMBE_PS";
            this.SOPNUMBE_PS.ReadOnly = true;
            this.SOPNUMBE_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMNAME
            // 
            this.ITEMNAME.DataPropertyName = "ITEMNAME";
            this.ITEMNAME.HeaderText = "Název položky";
            this.ITEMNAME.Name = "ITEMNAME";
            this.ITEMNAME.ReadOnly = true;
            this.ITEMNAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMNMBR_PS
            // 
            this.ITEMNMBR_PS.DataPropertyName = "ITEMNMBR";
            this.ITEMNMBR_PS.HeaderText = "Číslo položky";
            this.ITEMNMBR_PS.Name = "ITEMNMBR_PS";
            this.ITEMNMBR_PS.ReadOnly = true;
            // 
            // ITEMTYPE_PS
            // 
            this.ITEMTYPE_PS.DataPropertyName = "ITEMTYPE";
            this.ITEMTYPE_PS.HeaderText = "Typ položky";
            this.ITEMTYPE_PS.Name = "ITEMTYPE_PS";
            this.ITEMTYPE_PS.ReadOnly = true;
            this.ITEMTYPE_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_ID_PS
            // 
            this.SKL_ID_PS.DataPropertyName = "SKL_ID";
            this.SKL_ID_PS.HeaderText = "Sklad ID";
            this.SKL_ID_PS.Name = "SKL_ID_PS";
            this.SKL_ID_PS.ReadOnly = true;
            this.SKL_ID_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SKL_DESC_PS
            // 
            this.SKL_DESC_PS.DataPropertyName = "SKL_DESC";
            this.SKL_DESC_PS.HeaderText = "Název skladu";
            this.SKL_DESC_PS.Name = "SKL_DESC_PS";
            this.SKL_DESC_PS.ReadOnly = true;
            this.SKL_DESC_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ITEMCODE
            // 
            this.ITEMCODE.DataPropertyName = "ITEMCODE";
            this.ITEMCODE.HeaderText = "Kód položky";
            this.ITEMCODE.Name = "ITEMCODE";
            this.ITEMCODE.ReadOnly = true;
            this.ITEMCODE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // LOCNCODE_PS
            // 
            this.LOCNCODE_PS.DataPropertyName = "LOCNCODE";
            this.LOCNCODE_PS.HeaderText = "Lokace";
            this.LOCNCODE_PS.Name = "LOCNCODE_PS";
            this.LOCNCODE_PS.ReadOnly = true;
            this.LOCNCODE_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MJ
            // 
            this.MJ.DataPropertyName = "MJ";
            this.MJ.HeaderText = "Měrná jednotka";
            this.MJ.Name = "MJ";
            this.MJ.ReadOnly = true;
            this.MJ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // QTYSHPPD
            // 
            this.QTYSHPPD.DataPropertyName = "QTYSHPPD";
            this.QTYSHPPD.HeaderText = "Požadované množství";
            this.QTYSHPPD.Name = "QTYSHPPD";
            this.QTYSHPPD.ReadOnly = true;
            this.QTYSHPPD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // QTYSHPPDMJ
            // 
            this.QTYSHPPDMJ.DataPropertyName = "QTYSHPPDMJ";
            this.QTYSHPPDMJ.HeaderText = "Požadované množství MJ";
            this.QTYSHPPDMJ.Name = "QTYSHPPDMJ";
            this.QTYSHPPDMJ.ReadOnly = true;
            this.QTYSHPPDMJ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // QTYPACK_PS
            // 
            this.QTYPACK_PS.DataPropertyName = "QTYPACK";
            this.QTYPACK_PS.HeaderText = "Množství v balení";
            this.QTYPACK_PS.Name = "QTYPACK_PS";
            this.QTYPACK_PS.ReadOnly = true;
            this.QTYPACK_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SERLTNUM_PS
            // 
            this.SERLTNUM_PS.DataPropertyName = "SERLTNUM";
            this.SERLTNUM_PS.HeaderText = "Sériové číslo";
            this.SERLTNUM_PS.Name = "SERLTNUM_PS";
            this.SERLTNUM_PS.ReadOnly = true;
            this.SERLTNUM_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // GUID_Production
            // 
            this.GUID_Production.DataPropertyName = "GUID_Production";
            this.GUID_Production.HeaderText = "GUID Production";
            this.GUID_Production.Name = "GUID_Production";
            this.GUID_Production.ReadOnly = true;
            this.GUID_Production.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // GUID_PS
            // 
            this.GUID_PS.DataPropertyName = "GUID";
            this.GUID_PS.HeaderText = "GUID";
            this.GUID_PS.Name = "GUID_PS";
            this.GUID_PS.ReadOnly = true;
            this.GUID_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // USER_ID_PS
            // 
            this.USER_ID_PS.DataPropertyName = "USER_ID";
            this.USER_ID_PS.HeaderText = "Pracovník ID";
            this.USER_ID_PS.Name = "USER_ID_PS";
            this.USER_ID_PS.ReadOnly = true;
            this.USER_ID_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TERMINAL_ID
            // 
            this.TERMINAL_ID.DataPropertyName = "TERMINAL_ID";
            this.TERMINAL_ID.HeaderText = "Terminál ID";
            this.TERMINAL_ID.Name = "TERMINAL_ID";
            this.TERMINAL_ID.ReadOnly = true;
            this.TERMINAL_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DEX_ROW_ID
            // 
            this.DEX_ROW_ID.DataPropertyName = "DEX_ROW_ID";
            this.DEX_ROW_ID.HeaderText = "Jedinečné ID";
            this.DEX_ROW_ID.Name = "DEX_ROW_ID";
            this.DEX_ROW_ID.ReadOnly = true;
            this.DEX_ROW_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // WEIGHT
            // 
            this.WEIGHT.DataPropertyName = "WEIGHT";
            this.WEIGHT.HeaderText = "Váha";
            this.WEIGHT.Name = "WEIGHT";
            this.WEIGHT.ReadOnly = true;
            this.WEIGHT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // NMBRPAL
            // 
            this.NMBRPAL.DataPropertyName = "NMBRPAL";
            this.NMBRPAL.HeaderText = "Číslo palety";
            this.NMBRPAL.Name = "NMBRPAL";
            this.NMBRPAL.ReadOnly = true;
            this.NMBRPAL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TYPEPAL
            // 
            this.TYPEPAL.DataPropertyName = "TYPEPAL";
            this.TYPEPAL.HeaderText = "Typ palety";
            this.TYPEPAL.Name = "TYPEPAL";
            this.TYPEPAL.ReadOnly = true;
            this.TYPEPAL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PRINTED
            // 
            this.PRINTED.DataPropertyName = "PRINTED";
            this.PRINTED.HeaderText = "Vytisknuto";
            this.PRINTED.Name = "PRINTED";
            this.PRINTED.ReadOnly = true;
            this.PRINTED.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ISOK_PS
            // 
            this.ISOK_PS.DataPropertyName = "ISOK";
            this.ISOK_PS.HeaderText = "Datum a čas převzetí";
            this.ISOK_PS.Name = "ISOK_PS";
            this.ISOK_PS.ReadOnly = true;
            this.ISOK_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // idVS_PS
            // 
            this.idVS_PS.DataPropertyName = "idVS";
            this.idVS_PS.HeaderText = "Vedoucí směny ID";
            this.idVS_PS.Name = "idVS_PS";
            this.idVS_PS.ReadOnly = true;
            this.idVS_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dateedit_PS
            // 
            this.dateedit_PS.DataPropertyName = "dateedit";
            this.dateedit_PS.HeaderText = "Datum a čas editace";
            this.dateedit_PS.Name = "dateedit_PS";
            this.dateedit_PS.ReadOnly = true;
            this.dateedit_PS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // bs_materialy
            // 
            this.bs_materialy.DataMember = "Production_Sources_Odvod";
            this.bs_materialy.DataSource = this.DataSet_Materialy;
            // 
            // DataSet_Materialy
            // 
            this.DataSet_Materialy.DataSetName = "VyrobaDataSet";
            this.DataSet_Materialy.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // advancedDataGridViewSearchToolBar_Materialy
            // 
            this.advancedDataGridViewSearchToolBar_Materialy.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar_Materialy.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar_Materialy.Location = new System.Drawing.Point(0, 142);
            this.advancedDataGridViewSearchToolBar_Materialy.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_Materialy.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_Materialy.Name = "advancedDataGridViewSearchToolBar_Materialy";
            this.advancedDataGridViewSearchToolBar_Materialy.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar_Materialy.Size = new System.Drawing.Size(334, 27);
            this.advancedDataGridViewSearchToolBar_Materialy.TabIndex = 40;
            this.advancedDataGridViewSearchToolBar_Materialy.Text = "advancedDataGridViewSearchToolBar2";
            this.advancedDataGridViewSearchToolBar_Materialy.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar_Materialy_Search);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_MJ);
            this.groupBox1.Controls.Add(this.comboBox_ITEMDESC);
            this.groupBox1.Controls.Add(this.button_filtr_Material);
            this.groupBox1.Controls.Add(this.comboBox_ITEMNMBR);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 142);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtr Materiály";
            // 
            // comboBox_MJ
            // 
            this.comboBox_MJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_MJ.FormattingEnabled = true;
            this.comboBox_MJ.Location = new System.Drawing.Point(96, 113);
            this.comboBox_MJ.Name = "comboBox_MJ";
            this.comboBox_MJ.Size = new System.Drawing.Size(103, 21);
            this.comboBox_MJ.TabIndex = 1;
            // 
            // comboBox_ITEMDESC
            // 
            this.comboBox_ITEMDESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_ITEMDESC.FormattingEnabled = true;
            this.comboBox_ITEMDESC.Location = new System.Drawing.Point(96, 86);
            this.comboBox_ITEMDESC.Name = "comboBox_ITEMDESC";
            this.comboBox_ITEMDESC.Size = new System.Drawing.Size(103, 21);
            this.comboBox_ITEMDESC.TabIndex = 1;
            // 
            // button_filtr_Material
            // 
            this.button_filtr_Material.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_filtr_Material.Location = new System.Drawing.Point(205, 56);
            this.button_filtr_Material.Name = "button_filtr_Material";
            this.button_filtr_Material.Size = new System.Drawing.Size(82, 78);
            this.button_filtr_Material.TabIndex = 0;
            this.button_filtr_Material.Text = "Vyhledat";
            this.button_filtr_Material.UseVisualStyleBackColor = true;
            this.button_filtr_Material.Click += new System.EventHandler(this.button_filtr_Material_Click);
            // 
            // comboBox_ITEMNMBR
            // 
            this.comboBox_ITEMNMBR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_ITEMNMBR.FormattingEnabled = true;
            this.comboBox_ITEMNMBR.Location = new System.Drawing.Point(96, 59);
            this.comboBox_ITEMNMBR.Name = "comboBox_ITEMNMBR";
            this.comboBox_ITEMNMBR.Size = new System.Drawing.Size(103, 21);
            this.comboBox_ITEMNMBR.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbFiltry_Material,
            this.tsbNastavit_Material,
            this.toolStripSeparator3,
            this.tsbZmena_Material,
            this.toolStripSeparator4,
            this.tsbPridat_Material,
            this.toolStripSeparator5,
            this.tsbOdebrat_Material,
            this.toolStripSeparator6,
            this.tsbVycistit_Material});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(328, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(30, 22);
            this.toolStripLabel1.Text = "Filtr:";
            // 
            // tscbFiltry_Material
            // 
            this.tscbFiltry_Material.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFiltry_Material.Name = "tscbFiltry_Material";
            this.tscbFiltry_Material.Size = new System.Drawing.Size(121, 25);
            // 
            // tsbNastavit_Material
            // 
            this.tsbNastavit_Material.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNastavit_Material.Image = ((System.Drawing.Image)(resources.GetObject("tsbNastavit_Material.Image")));
            this.tsbNastavit_Material.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNastavit_Material.Name = "tsbNastavit_Material";
            this.tsbNastavit_Material.Size = new System.Drawing.Size(23, 22);
            this.tsbNastavit_Material.Text = "Nastavit";
            this.tsbNastavit_Material.ToolTipText = "Nastavit";
            this.tsbNastavit_Material.Click += new System.EventHandler(this.tsbNastavit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbZmena_Material
            // 
            this.tsbZmena_Material.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZmena_Material.Image = ((System.Drawing.Image)(resources.GetObject("tsbZmena_Material.Image")));
            this.tsbZmena_Material.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZmena_Material.Name = "tsbZmena_Material";
            this.tsbZmena_Material.Size = new System.Drawing.Size(23, 22);
            this.tsbZmena_Material.Text = "Změna";
            this.tsbZmena_Material.Click += new System.EventHandler(this.tsbZmena_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPridat_Material
            // 
            this.tsbPridat_Material.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPridat_Material.Image = ((System.Drawing.Image)(resources.GetObject("tsbPridat_Material.Image")));
            this.tsbPridat_Material.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPridat_Material.Name = "tsbPridat_Material";
            this.tsbPridat_Material.Size = new System.Drawing.Size(23, 22);
            this.tsbPridat_Material.Text = "Uložit";
            this.tsbPridat_Material.Click += new System.EventHandler(this.tsbPridat_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOdebrat_Material
            // 
            this.tsbOdebrat_Material.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOdebrat_Material.Image = ((System.Drawing.Image)(resources.GetObject("tsbOdebrat_Material.Image")));
            this.tsbOdebrat_Material.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOdebrat_Material.Name = "tsbOdebrat_Material";
            this.tsbOdebrat_Material.Size = new System.Drawing.Size(23, 22);
            this.tsbOdebrat_Material.Text = "Odebrat";
            this.tsbOdebrat_Material.Click += new System.EventHandler(this.tsbOdebrat_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbVycistit_Material
            // 
            this.tsbVycistit_Material.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVycistit_Material.Image = ((System.Drawing.Image)(resources.GetObject("tsbVycistit_Material.Image")));
            this.tsbVycistit_Material.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVycistit_Material.Name = "tsbVycistit_Material";
            this.tsbVycistit_Material.Size = new System.Drawing.Size(23, 22);
            this.tsbVycistit_Material.Text = "Vyčistit";
            this.tsbVycistit_Material.Click += new System.EventHandler(this.tsbVycistit_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Měrná jednotka:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Pol. Číslo:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(51, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Popis :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip2
            // 
            this.menuStrip2.AllowMerge = false;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiAkce});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(735, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip1";
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
            this.tsmiKonec.Size = new System.Drawing.Size(107, 22);
            this.tsmiKonec.Text = "Konec";
            this.tsmiKonec.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // tsmiAkce
            // 
            this.tsmiAkce.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ZruseniPriznakuISOK,
            this.tsmiZpracovatVyrobu,
            this.tsmiupravitVyrobek});
            this.tsmiAkce.Name = "tsmiAkce";
            this.tsmiAkce.Size = new System.Drawing.Size(45, 20);
            this.tsmiAkce.Text = "Akce";
            // 
            // tsmi_ZruseniPriznakuISOK
            // 
            this.tsmi_ZruseniPriznakuISOK.Name = "tsmi_ZruseniPriznakuISOK";
            this.tsmi_ZruseniPriznakuISOK.Size = new System.Drawing.Size(180, 22);
            this.tsmi_ZruseniPriznakuISOK.Text = "Zrušení času ISOK";
            this.tsmi_ZruseniPriznakuISOK.Click += new System.EventHandler(this.tsmi_ZruseniPriznakuISOK_Click);
            // 
            // tsmiZpracovatVyrobu
            // 
            this.tsmiZpracovatVyrobu.Name = "tsmiZpracovatVyrobu";
            this.tsmiZpracovatVyrobu.Size = new System.Drawing.Size(180, 22);
            this.tsmiZpracovatVyrobu.Text = "Zpracovat výrobu";
            this.tsmiZpracovatVyrobu.Click += new System.EventHandler(this.tsmiZpracovatVyrobu_Click);
            // 
            // tsmiupravitVyrobek
            // 
            this.tsmiupravitVyrobek.Name = "tsmiupravitVyrobek";
            this.tsmiupravitVyrobek.Size = new System.Drawing.Size(180, 22);
            this.tsmiupravitVyrobek.Text = "Upravit výrobek";
            this.tsmiupravitVyrobek.Click += new System.EventHandler(this.tsmiupravitVyrobek_Click);
            // 
            // bw_Materialy_stav
            // 
            this.bw_Materialy_stav.WorkerSupportsCancellation = true;
            this.bw_Materialy_stav.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Materialy_stav_DoWork);
            this.bw_Materialy_stav.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Materialy_stav_RunWorkerCompleted);
            // 
            // bw_Vyrobky_stav
            // 
            this.bw_Vyrobky_stav.WorkerSupportsCancellation = true;
            this.bw_Vyrobky_stav.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Vyrobky_stav_DoWork);
            this.bw_Vyrobky_stav.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_Vyrobky_stav_RunWorkerCompleted);
            // 
            // bw_ImportToIS
            // 
            this.bw_ImportToIS.WorkerSupportsCancellation = true;
            this.bw_ImportToIS.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_ImportToIS_DoWork);
            this.bw_ImportToIS.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_ImportToIS_RunWorkerCompleted);
            // 
            // FormVazby_P_PS_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 674);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.KeyPreview = true;
            this.Name = "FormVazby_P_PS_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Odvod Pohoda";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVazby_P_PS_List_FormClosing);
            this.Load += new System.EventHandler(this.FormVazby_P_PS_List_Load);
            this.Shown += new System.EventHandler(this.FormVazby_P_PS_List_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormVazby_P_PS_List_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgVyrobky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_vyrobky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet_Vyrobky)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel_FiltryVyrobky.ResumeLayout(false);
            this.panel_FiltryVyrobky.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMaterialy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_materialy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet_Materialy)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView dgVyrobky;
        private Fask.AdvancedButtonsPanel.ButtonsPanel panelButtons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private Zuby.ADGV.AdvancedDataGridView dgMaterialy;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.BindingSource bs_vyrobky;
        private System.Windows.Forms.BindingSource bs_materialy;
        private System.Windows.Forms.Button button_Filtr_Vyrobky;
        private System.Windows.Forms.Panel panel_FiltryVyrobky;
        private ProgressControls.ProgressIndicator progressIndicatorVyrobek;
        private ProgressControls.ProgressIndicator progressIndicatorMaterial;
        private System.ComponentModel.BackgroundWorker bw_Materialy_stav;
        private System.ComponentModel.BackgroundWorker bw_Vyrobky_stav;
        private System.Windows.Forms.ComboBox comboBox_Vyrobek_MJ;
        private System.Windows.Forms.ComboBox comboBox_Vyrobek_ITEMDESC;
        private System.Windows.Forms.ComboBox comboBox_Vyrobek_ITEMNMBR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tscbFiltry_Vyrobek;
        private System.Windows.Forms.ToolStripButton tsbNastavit_Vyrobek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbZmena_Vyrobek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbPridat_Vyrobek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton tsbOdebrat_Vyrobek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton tsbVycistit_Vyrobek;
        private Fask.Interfaces.DataSets.Vyroba DataSet_Vyrobky;
        //private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRDefDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dESCDefDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn mJDefDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNMBRfolDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dESCFolDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn mJFolDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn cZCarKodDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn lOCNCODEDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn sKLIDDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn qTYDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn qTYPACKDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dMJDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn tAXRATEDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn pRICE0DataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn pRICE1DataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn pRICE2DataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn pRICE3DataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn pRICE4DataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn pRICE5DataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn cZSerNumTrackDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn cZSerNumDelkaDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn cZRez1TrackDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn cZRez2TrackDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn cZRez3TrackDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn cZRez4TrackDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn rEZ1DataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dEXROWIDDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn iTEMCODEDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn oDBIDDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn tIMEMODEDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn tIMEPREPDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn tIMEUNITDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn tIMETODataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn lSTModDataGridViewTextBoxColumn;
        private Fask.Interfaces.DataSets.Vyroba DataSet_Materialy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox_MJ;
        private System.Windows.Forms.ComboBox comboBox_ITEMDESC;
        private System.Windows.Forms.Button button_filtr_Material;
        private System.Windows.Forms.ComboBox comboBox_ITEMNMBR;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbFiltry_Material;
        private System.Windows.Forms.ToolStripButton tsbNastavit_Material;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbZmena_Material;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbPridat_Material;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbOdebrat_Material;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbVycistit_Material;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem tsmiKonec;
        private System.ComponentModel.BackgroundWorker bw_ImportToIS;
        private System.Windows.Forms.ToolStripMenuItem tsmiAkce;
        private System.Windows.Forms.ToolStripMenuItem tsmiZpracovatVyrobu;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar_Vyrobky;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar_Materialy;
        private System.Windows.Forms.ToolStripMenuItem tsmiupravitVyrobek;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ZruseniPriznakuISOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountEntries_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPNUMBE_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMTYPE_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_ID_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_DESC_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCNCODE_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn MJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYSHPPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYSHPPDMJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYPACK_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERLTNUM_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUID_Production;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUID_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn USER_ID_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn TERMINAL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEX_ROW_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn NMBRPAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn TYPEPAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRINTED;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISOK_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn idVS_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateedit_PS;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountEntries;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOPNUMBE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMMJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEMODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEPREPSTART;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEPREPSTOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEPREP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMEUNIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMESTART;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMESTOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMECORSTART;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMECORSTOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMECOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMECRID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMECRIDTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn loginid;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineid;
        private System.Windows.Forms.DataGridViewTextBoxColumn operationid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateeve;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyReal;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYPACK;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTYPACKMJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarcodeP;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TermID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOUBEHGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CORRGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyOld;
        private System.Windows.Forms.DataGridViewTextBoxColumn idVS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateedit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKL_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCNCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERLTNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXPIRATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn REZ_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn REZ_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn REZ_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn REZ_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn REZ_5;
    }
}