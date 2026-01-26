using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Fask.Server.Interfaces.Classes;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;

namespace Fask.ModuleSql
{
	/// <summary>
	/// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část Inventura2.
	/// </summary>
    class ProviderInventura2 : Fask.Server.Interfaces.Inventura2.IInventura2
		, Fask.Server.Interfaces.WebControl.IWebControl
    {

		#region Nazvy tabulek + cesta k souboru Params

		public System.Collections.Specialized.NameValueCollection OutputParams { get; set; }
		private string TABLE_Inventura = "INVENTUR";
		private string TABLE_Majetek = "MAJETEK"; 

		#endregion

        #region IInventura2 Members

		/// <summary>
		/// Metoda která vraci seznam dostupnych inventurnich predloh
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <returns>Dataset - Inventury2, naplnen datama pro inventuru</returns>
        public Fask.DataSets.Inventury2 Inventura2_GetInventury(Terminal terminal)
        {
            try
            {
                Globals.LoadConfiguration();
                //Vrati seznam davek inventury, ktere jsou volne pro stazeni nebo jsou stazene terminalem, ktery zada o stazeni
                string select = string.Empty;
                if (Globals.Konfigurace.Inventura2[0].DavkaTerminalVice)
                {
                    select = "SELECT distinct ID_INV FROM " + TABLE_Majetek + " WHERE ID_TERM < 100 or ID_TERM is NULL order by ID_INV";
                }
                else
                {
                    select = "SELECT distinct ID_INV FROM " + TABLE_Majetek + " WHERE ID_TERM <= 0 or ID_TERM is NULL or ID_TERM=" + terminal.ID + " order by ID_INV";
                }

                System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(select, Globals.Konfigurace.ConnectionString[0].FASKDB);


                Fask.DataSets.Inventury2 inventury = new Fask.DataSets.Inventury2();
                dataAdapter.Fill(inventury, inventury.Hlavicky.TableName);

                inventury.AcceptChanges();

                return inventury;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// Metoda pro dotaženi inventury z DB pro pripravu souboru
		/// </summary>
		/// <param name="davka">Davka</param>
		/// <param name="terminal">Terminal</param>
		/// <returns>Dataset Inventura2 - naplnen datama</returns>
        public Fask.DataSets.Inventura2 Inventura2_GetInventura(Davka davka, Terminal terminal)
        {
            Globals.LoadConfiguration();
            Fask.DataSets.Inventura2 i2 = new Fask.DataSets.Inventura2();

            System.Data.SqlClient.SqlConnection sql = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            SqlDataAdapter sqlda = new SqlDataAdapter();

            sql.Open();

            sqlda.SelectCommand = new SqlCommand("select * from MAJETEK where ID_INV=" + davka.ID.Value, sql);
            sqlda.Fill(i2.MAJETEK);
            //ta_majetek.Fill(inventura2.MAJETEK, cislodavky);
            ////slouzi pro inicializaci vlastnostni SelectCommand...
            //try { ta_majetek.Fill(null, null); }
            //catch { }
            //ta_majetek.Adapter.SelectCommand.Parameters[0].Value = cislodavky;
            //ireader = ta_majetek.Adapter.SelectCommand.ExecuteReader();
            //inventura2sqlce.MAJETEK.Load(ireader);

            sqlda.SelectCommand = new SqlCommand("select * from KANCL", sql);
            sqlda.Fill(i2.KANCL);
            //ta_kancl.Fill(inventura2.KANCL);
            ////slouzi pro inicializaci vlastnostni SelectCommand...
            //try { ta_kancl.Fill(null); }
            //catch { }
            //ireader = ta_kancl.Adapter.SelectCommand.ExecuteReader();
            //inventura2sqlce.KANCL.Load(ireader);

            sqlda.SelectCommand = new SqlCommand("select * from LOKACE", sql);
            sqlda.Fill(i2.LOKACE);
            //ta_lokace.Fill(inventura2.LOKACE);
            ////slouzi pro inicializaci vlastnostni SelectCommand...
            //try { ta_lokace.Fill(null); }
            //catch { }
            //ireader = ta_lokace.Adapter.SelectCommand.ExecuteReader();
            //inventura2sqlce.LOKACE.Load(ireader);

            sqlda.SelectCommand = new SqlCommand("select * from OSOBY", sql);
            sqlda.Fill(i2.OSOBY);
            //ta_osoby.Fill(inventura2.OSOBY);
            ////slouzi pro inicializaci vlastnostni SelectCommand...
            //try { ta_osoby.Fill(null); }
            //catch { }
            //ireader = ta_osoby.Adapter.SelectCommand.ExecuteReader();
            //inventura2sqlce.OSOBY.Load(ireader);

            sqlda.SelectCommand = new SqlCommand("select * from UCSTR", sql);
            sqlda.Fill(i2.UCSTR);
            //ta_strediska.Fill(inventura2.UCSTR);


            sql.Close();

            return i2;
        }

		/// <summary>
		/// Metoda blokuje data po prenosu do terminalu, je poslednim krokem pri prenosu dat
		/// </summary>
		/// <param name="davka">Davka</param>
		/// <param name="terminal">Terminal</param>
		/// <returns>True-OK, False-Chyba</returns>
        public bool Inventura2_GetInventuraReceived(Davka davka, Terminal terminal)
        {
            Globals.LoadConfiguration();
            string selectCount = "SELECT Count(ID_INV) as davka FROM " + TABLE_Majetek + " where ID_INV='" + davka.ID + "' AND (ID_TERM is NULL or ID_TERM<=0 or ID_TERM=" + terminal.ID + ")";
            string update = "Update " + TABLE_Majetek + " set ID_TERM=" + terminal.ID + " where ID_INV='" + davka.ID + "'";
 
            System.Data.SqlClient.SqlConnection sql = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(selectCount, sql);
            System.Data.SqlClient.SqlCommand commandUpdate = new System.Data.SqlClient.SqlCommand(update, sql);
            SqlTransaction itrans = null;

            int res = 0;
            try
            {
                sql.Open();
                itrans = sql.BeginTransaction(IsolationLevel.Serializable);

                command.Transaction = itrans;
                object r = command.ExecuteScalar();
                if (r == null)
                    throw new Exception("Dávka nenalezena.");

                if (!Globals.Konfigurace.Inventura2[0].DavkaTerminalVice)
                {
                    if (r is int && ((int)r) <= 0)
                        throw new Exception("Dávka se již zpracovává.");
                }


                commandUpdate.Transaction = itrans;
                res = commandUpdate.ExecuteNonQuery();
                if (itrans != null)
                    itrans.Commit();

            }
            catch (Exception ex)
            {
                if (itrans != null)
                    itrans.Rollback();

                throw ex;
            }
            finally
            {
                if (sql.State == ConnectionState.Open)
                    sql.Close();
            }

            return (res > 0);
        }

		/// <summary>
		/// Metoda která zpracuje prenesena data nebo uvolni davku...
		/// </summary>
		/// <param name="davka">Davka</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="inventuradata">Data pro zpracovaní</param>
		/// <param name="processInventuraState">Zpracovat/Uvolnit</param>
		/// <returns>StatusObject - Nese informace o stavu</returns>
        public StatusObject Inventura2_Process(Davka davka, Terminal terminal, Fask.DataSets.Inventura2 inventuradata, Fask.Server.Interfaces.Inventura2.ProcessState processInventuraState)
        {
            Globals.LoadConfiguration();
            SqlTransaction iTrans1 = null;
            
            string guidDavka = inventuradata.HLAVICKY[0].GUID.ToString();
            string filePath = Path.Combine(Globals.Konfigurace.Inventura2[0].PathStateDataFile, guidDavka);

            StatusObject so = new StatusObject(filePath);

            bool uvolnitdavku = processInventuraState == Fask.Server.Interfaces.Inventura2.ProcessState.Uvolnit;

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);


            try
            {
                so.Write("vytvareni connection");
                conn.Open();

                if (uvolnitdavku) //uvolnit davku
                {
                    so.Write("uvolnit davku");

                    string updateuvolnit = "Update " + TABLE_Majetek + " set ID_TERM=0 where ID_INV='" + davka.ID + "'";
                    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(updateuvolnit, conn, iTrans1);
                    int rows = comm.ExecuteNonQuery();
                }
                else //zapsat davku
                {
                    so.Write("zapsat davku");
                    bool allowInsertData = true;
                    if (!Globals.Konfigurace.Inventura2[0].DavkaTerminalVice)
                    {
                        //Test zda je mozne data pridat, jestlize jiz existuji, tak nepridat. 
                        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand("Select Count(*) as number from " + TABLE_Inventura + " where ID_INV=" + davka.ID, conn);
                        object datacount = comm.ExecuteScalar();
                        if (datacount != null && ((int)datacount) > 0)
                            allowInsertData = false;//Data allready exists in database 
                    }

                    if (allowInsertData)
                    {
                        so.Write("Update databaze");

                        iTrans1 = conn.BeginTransaction();
                        string updatei1 = "Update " + TABLE_Majetek + " set ID_TERM=" + (terminal.ID + 100) + " where ID_INV=" + davka.ID;
                        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatei1, conn, iTrans1);

        
                        //SQL_Datasets.Inventura2 ds = new Fask.ModuleSql.SQL_Datasets.Inventura2();
                        //ds.INVENTUR.AddINVENTURRow("adas", "", "nazev", "", 0, "", "", "", "", 0, 0, 0, "", 0, DateTime.Now);

                        SQL_Datasets.Inventura2TableAdapters.INVENTURTableAdapter invta = new Fask.ModuleSql.SQL_Datasets.Inventura2TableAdapters.INVENTURTableAdapter();
                        invta.Connection = conn;
                        invta.Transaction = (System.Data.SqlClient.SqlTransaction)iTrans1;
                        invta.Update(inventuradata.INVENTUR.Select(null, null, DataViewRowState.Added));

                        if (!Globals.Konfigurace.Inventura2[0].DavkaTerminalVice)
                        {
                            int rows = command.ExecuteNonQuery();
                        }
                    }
                }


                so.Write("commit transakce");

                if (iTrans1 != null)
                    iTrans1.Commit();
            }
            catch (Exception ex)
            {
                if (iTrans1 != null)
                    iTrans1.Rollback();

                so.Exception = true;
                so.Write(ex.Message);

                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                    conn.Close();
            }

            #region Action after data processed
            if (Globals.Konfigurace.Inventura2[0].AfterDataProcessed_Action_Asynchronous)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Inventura2_AfterProcessedActionAsync));
                thread.Start(davka);
            }
            else
            {
                if (!Inventura2_AfterProcessedAction(davka))
                {
                    so.Exception = true;
                    so.Write("chyba");
                    return so;
                }
            }
			#endregion


			so.SetOK();

            return so;

        }


        public void Inventura2_AfterProcessedActionAsync(object davka)
        {
            try
            {
                Fask.Server.Interfaces.Classes.Davka d = (Fask.Server.Interfaces.Classes.Davka)davka;
                Inventura2_AfterProcessedAction(d);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// Metoda která se volá po zpracovaní dat
		/// </summary>
		/// <param name="davka">Davka</param>
		/// <returns>True-OK, False-Chyba</returns>
        public bool Inventura2_AfterProcessedAction(Davka davka)
        {
            try
            {
                Globals.LoadConfiguration();
                string aDP_Action = Globals.Konfigurace.Inventura2[0].AfterDataProcessed_Action;
                string aDP_Action_P1 = Globals.Konfigurace.Inventura2[0].AfterDataProcessed_Action_P1;
                string aDP_Action_P2 = Globals.Konfigurace.Inventura2[0].AfterDataProcessed_Action_P2;
                if (aDP_Action.Length != 0)
                {
                    Routines.AfterProcessAction.Execute(Globals.Konfigurace.ConnectionString[0].FASKDB, TABLE_Inventura, (int)davka.ID, aDP_Action, aDP_Action_P1, aDP_Action_P2, 120);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

		#region IWebControl Members

		#region Parametry ( je ich tam vic než je momentalne potreba)

		//obecne
		//private PlaceHolder phExport;
		//private PlaceHolder phUkoncit;
		private PlaceHolder phPrehled;

		//private Label statusExportLbl;
		//private Label statusUkoncitLbl;
		//private Label statusPrehledLbl;

		private DropDownList lblUkoncitIDsInv = null;
		private DropDownList lbIPrehledDsInv;

		//private Label lblExportText;
		//private Label lblUkoncitText;
		private Label lblPrehledText;

		//private Button btnExportAction;
		private Button btnPrehledAction;
		//private Button btnUkoncitAction;

		private Button btnTiskovaSestava;
		private Button btnTiskovaSestavaRozdilova;
		//export
		//private Label lblExport;

		//private TextBox txtExport;

		//private CheckBox chckDrobny;
		//private CheckBox chckDlouhodoby;
		//private CheckBox chckLeasingovy;
		//prehled
		private Label lblPrehledNote;
		private GridView gvInventura;
		private Label generatedTimeLbl;
		private Button btnFilter;
		private TextBox txtFilterExpr;

		private Label LblHidePomocna;

		private List<TreeNode> actions;

		private const string ASCENDING = " ASC";
		private const string DESCENDING = " DESC";
		
		#endregion

		public List<System.Web.UI.WebControls.TreeNode> getActions(User uzivatel)
		{
			actions = new List<TreeNode>();

			//if (uzivatel.ADM == 1)
			//{
			//    TreeNode tn = new TreeNode();
			//    tn.Value = "Export inventury";
			//    tn.Text = "Export inventury";
			//    actions.Add(tn);

			//    TreeNode tn1 = new TreeNode();
			//    tn1.Value = "Ukončit inventuru";
			//    tn1.Text = "Ukončit inventuru";
			//    actions.Add(tn1);
			//}

			TreeNode tn2 = new TreeNode();
			tn2.Value = "Přehled dat";
			tn2.Text = "Přehled dat";
			actions.Add(tn2);

			return actions;
		}

		
		public object getAction(System.Web.UI.WebControls.TreeNode tn, System.Web.UI.Page page)
		{
			if (tn.Value == "Přehled dat")
			{
				try
				{
					#region prehledDat
					if (phPrehled == null)
					{
						phPrehled = new PlaceHolder();
						phPrehled.Page = page;
						initPlaceHolderPrehledDat();
					}
					else
					{
						phPrehled.Page = page;

						btnPrehledAction.Click -= new EventHandler(btnPrehled_Click);
						btnPrehledAction.Click += new EventHandler(btnPrehled_Click);

						//lbIPrehledDsInv.SelectedIndexChanged -= new EventHandler(lbIPrehledDsInv_SelectedIndexChanged);
						//lbIPrehledDsInv.SelectedIndexChanged += new EventHandler(lbIPrehledDsInv_SelectedIndexChanged);

						gvInventura.PageIndexChanging -= new GridViewPageEventHandler(gview_PageIndexChanging);
						gvInventura.PageIndexChanging += new GridViewPageEventHandler(gview_PageIndexChanging);

						gvInventura.Sorting -= new GridViewSortEventHandler(gview_Sorting);
						gvInventura.Sorting += new GridViewSortEventHandler(gview_Sorting);

						gvInventura.RowDataBound -= new GridViewRowEventHandler(gview_RowDataBound);
						gvInventura.RowDataBound += new GridViewRowEventHandler(gview_RowDataBound);

						btnTiskovaSestava.Click -= new EventHandler(btnPrintSestavaBtn_Click);
						btnTiskovaSestava.Click += new EventHandler(btnPrintSestavaBtn_Click);

						btnTiskovaSestavaRozdilova.Click -= new EventHandler(btnPrintSestavaRozdilovaBtn_Click);
						btnTiskovaSestavaRozdilova.Click += new EventHandler(btnPrintSestavaRozdilovaBtn_Click);

						btnFilter.Click -= new EventHandler(btnFilter_Click);
						btnFilter.Click += new EventHandler(btnFilter_Click);
					}

					createDropDownListInventur();

					return phPrehled;
					#endregion

				}
				catch (Exception ex)
				{
					phPrehled = null;
					throw ex;
				}
			}

			return null;
		}

		#region Pohled na data, METODY

		private SQL_Datasets.Report reportInventura2;

		private void initPlaceHolderPrehledDat()
		{
			//PlaceHolder phpom = new PlaceHolder();

			lblPrehledText = new Label();
			lblPrehledText.Text = "Číslo inventury: ";
			phPrehled.Controls.Add(lblPrehledText);
			lblPrehledText.ID = "lblPrehledText";

			lbIPrehledDsInv = new DropDownList();
			lbIPrehledDsInv.ID = "lblPrehledIDsInv";

			phPrehled.Controls.Add(lbIPrehledDsInv);

			phPrehled.Controls.Add(new LiteralControl("<br />"));

			generatedTimeLbl = new Label();
			generatedTimeLbl.Text = "";
			phPrehled.Controls.Add(generatedTimeLbl);
			generatedTimeLbl.ID = "generatedTimeLbl";

			phPrehled.Controls.Add(new LiteralControl("<br />"));

			btnPrehledAction = new Button();
			btnPrehledAction.Text = "Vygenerovat přehled dat";
			btnPrehledAction.Click += new EventHandler(btnPrehled_Click);
			phPrehled.Controls.Add(btnPrehledAction);
			btnPrehledAction.ID = "btnPrehledAction";

			phPrehled.Controls.Add(new LiteralControl("<br />"));

			btnTiskovaSestava = new Button();
			btnTiskovaSestava.Text = "Vytvořit tiskovou sestavu celou";
			btnTiskovaSestava.Click += new EventHandler(btnPrintSestavaBtn_Click);
			phPrehled.Controls.Add(btnTiskovaSestava);
			btnTiskovaSestava.ID = "btnTiskovaSestava";

			btnTiskovaSestavaRozdilova = new Button();
			btnTiskovaSestavaRozdilova.Text = "Vytvořit tiskovou sestavu rozdílovou";
			btnTiskovaSestavaRozdilova.Click += new EventHandler(btnPrintSestavaRozdilovaBtn_Click);
			phPrehled.Controls.Add(btnTiskovaSestavaRozdilova);
			btnTiskovaSestavaRozdilova.ID = "btnTiskovaSestavaRozdilova";



			txtFilterExpr = new TextBox();
			txtFilterExpr.Text = "";
			phPrehled.Controls.Add(txtFilterExpr);
			txtFilterExpr.ID = "txtFilterExpr";

			btnFilter = new Button();
			btnFilter.Text = "Filtruj";
			btnFilter.Click += new EventHandler(btnFilter_Click);
			phPrehled.Controls.Add(btnFilter);
			btnFilter.ID = "btnFilter";

			phPrehled.Controls.Add(new LiteralControl("<br />"));

			lblPrehledNote = new Label();
			lblPrehledNote.Text = "Legenda barev: bílá - dosud neinventarizováno, zelená - odpovídá předloze, červená - přelokování, žlutá - rozdíl oproti předloze";
			phPrehled.Controls.Add(lblPrehledNote);
			lblPrehledNote.ID = "lblPrehledNote";

			phPrehled.Controls.Add(new LiteralControl("<br />"));

			gvInventura = new GridView();

			gvInventura.PageIndexChanging -= new GridViewPageEventHandler(gview_PageIndexChanging);
			gvInventura.PageIndexChanging += new GridViewPageEventHandler(gview_PageIndexChanging);

			phPrehled.Controls.Add(gvInventura);
			setGridProperties();


			reportInventura2 = new SQL_Datasets.Report();
			FillReportDataSet(reportInventura2, "", false, "");

			gvInventura.AllowPaging = true;
			gvInventura.DataSource = reportInventura2;

			gvInventura.DataBind();


			//phPrehled.Controls.Add(getGridView(idInv));

			phPrehled.Controls.Add(new LiteralControl("<br />"));

			generatedTimeLbl.Text = "Vygenerováno: " + DateTime.Now.ToLongTimeString();

			LblHidePomocna = new Label();
			LblHidePomocna.Text = " ASC";
			LblHidePomocna.Visible = false;
			phPrehled.Controls.Add(LblHidePomocna);
		}

		void btnPrehled_Click(object sender, EventArgs e)
		{
			if (phPrehled != null)
			{
				reportInventura2 = new SQL_Datasets.Report();
				FillReportDataSet(reportInventura2, lbIPrehledDsInv.SelectedValue, false, "");

				gvInventura.DataSource = reportInventura2;
				gvInventura.DataBind();
			}
			//ph.Controls.Clear();
			// ph.Controls.Add((PlaceHolder)generatePrehledDat(lbIDsInv.SelectedValue));
		}

		private void FillReportDataSet(SQL_Datasets.Report report, string idInv, bool isDiff, string filterExpr)
		{
            Globals.LoadConfiguration();

            if (idInv == string.Empty && lbIPrehledDsInv != null)
			{
				if (lbIPrehledDsInv.SelectedValue == "")
					return;

				idInv = lbIPrehledDsInv.SelectedValue;
			}

			SQL_Datasets.Inventura2.MAJETEKDataTable majetek = null;
			SQL_Datasets.Inventura2.INVENTURDataTable inventura = null;

			SQL_Datasets.Inventura2TableAdapters.MAJETEKTableAdapter majetek_ta = new SQL_Datasets.Inventura2TableAdapters.MAJETEKTableAdapter();
            majetek_ta.Connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
			SQL_Datasets.Inventura2TableAdapters.INVENTURTableAdapter inventura_ta = new SQL_Datasets.Inventura2TableAdapters.INVENTURTableAdapter();
            inventura_ta.Connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

			if (filterExpr == string.Empty)
			{
				majetek = majetek_ta.GetDataByIDInv(idInv/*lbIDsInv.SelectedValue*/);
				inventura = inventura_ta.GetDataByIDInv(Decimal.Parse(idInv/*lbIDsInv.SelectedValue*/));
			}
			else
			{
				majetek = majetek_ta.GetDataByIdInvAndNazev(idInv, "%" + filterExpr + "%");
				inventura = inventura_ta.GetDataByIdInvAndNazev(Decimal.Parse(idInv), "%" + filterExpr + "%");
			}

			SQL_Datasets.Inventura2TableAdapters.KANCLTableAdapter kancl_ta = new SQL_Datasets.Inventura2TableAdapters.KANCLTableAdapter();
            kancl_ta.Connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

			SQL_Datasets.Inventura2.KANCLDataTable dt = kancl_ta.GetData();
			System.Collections.Specialized.NameValueCollection kancelare = new System.Collections.Specialized.NameValueCollection();
			System.Collections.Specialized.NameValueCollection kancelareNazvy = new System.Collections.Specialized.NameValueCollection();
			/*foreach (DataSets.Inventura2.KANCLRow item in dt)
			{
				kancelare.Add(item.KANCL, item.TEXT);
				kancelareNazvy.Add(item.KANCL, item.NAZEV);
			}*/

			foreach (SQL_Datasets.Inventura2.MAJETEKRow item in majetek)
			{
				//DataSets.Inventura2.INVENTURRow[] invPol = (DataSets.Inventura2.INVENTURRow[])inventura.Select("I_CISLO='" + item.I_CISLO + "'");
				Object obj = inventura.Compute("Sum(KUSU)", "I_CISLO='" + item.I_CISLO + "'");
				String kusu = obj.ToString();

				SQL_Datasets.Inventura2.INVENTURRow[] result = (SQL_Datasets.Inventura2.INVENTURRow[])inventura.Select("I_CISLO='" + item.I_CISLO + "'");

				string kanclTextPuvodni = "-";
				string kanclTextNovy = "-";
				string kanclNazev = "-";

				SQL_Datasets.Inventura2.KANCLRow[] resultKancl = null;

				if (result.Length > 0)
				{
					if (!result[0].IsKANCELARNull())
					{
						resultKancl = (SQL_Datasets.Inventura2.KANCLRow[])dt.Select("KANCL='" + result[0].KANCELAR.Trim() + "'");
						if (resultKancl.Length > 0)
						{
							kanclTextNovy = resultKancl[0].IsTEXTNull() ? "- " : resultKancl[0].TEXT;
							kanclNazev = resultKancl[0].IsNAZEVNull() ? "-" : resultKancl[0].NAZEV;
						}
					}


				}
				if (!item.IsKANCELARNull() && item.KANCELAR.Trim().Length > 0)
				{
					resultKancl = (SQL_Datasets.Inventura2.KANCLRow[])dt.Select("KANCL='" + item.KANCELAR.Trim() + "'");
					if (resultKancl.Length > 0)
						kanclTextPuvodni = resultKancl[0].TEXT;
				}

				if (isDiff)
				{
					if (item.KUSU.ToString() != kusu)
						report.Polozka.AddPolozkaRow(item.KATEGORIE.Trim(), item.I_CISLO.Trim(), item.KUSU.ToString().Trim(), kusu.Trim(), item.NAZEV.Trim(), idInv/* lbIDsInv.SelectedValue*/, kanclTextPuvodni.Trim(), kanclTextNovy.Trim(), kanclNazev.Trim());

				}
				else
					report.Polozka.AddPolozkaRow(item.KATEGORIE.Trim(), item.I_CISLO.Trim(), item.KUSU.ToString().Trim(), kusu.Trim(), item.NAZEV.Trim(), idInv/* lbIDsInv.SelectedValue*/, kanclTextPuvodni.Trim(), kanclTextNovy.Trim(), kanclNazev.Trim());
			}
		}

		void gview_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvInventura.PageIndex = e.NewPageIndex;
			gvInventura.DataBind();

		}

		void gview_Sorting(object sender, GridViewSortEventArgs e)
		{
			string sortExpression = e.SortExpression;

			if (LblHidePomocna.Text == ASCENDING)
			{
				LblHidePomocna.Text = DESCENDING;
				SortGridView(sortExpression, DESCENDING, sender);
			}
			else
			{
				LblHidePomocna.Text = ASCENDING;
				SortGridView(sortExpression, ASCENDING, sender);
			}
		}

		protected void gview_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				DataRow row = ((DataRowView)e.Row.DataItem).Row;
				string kusu_evid = row.Field<string>("MnozstviEvidovane");
				string nacteno = row.Field<string>("MnozstviSkutecne");

				string kancl = row.Field<string>("kancl");
				string kanc_novy = row.Field<string>("kancl_novy");

				if (kanc_novy != kancl && kanc_novy != "-")
				{
					e.Row.BackColor = Color.Red;
				}
				else if (kusu_evid != nacteno)
				{

					Decimal kusu_nacteno;
					Decimal kusu_evid_dec;
					if (Decimal.TryParse(nacteno, out kusu_nacteno) && Decimal.TryParse(kusu_evid, out kusu_evid_dec))
					{
						if (((kusu_nacteno < kusu_evid_dec) || (kusu_nacteno > kusu_evid_dec)) && kusu_nacteno > 0)
							e.Row.BackColor = Color.Yellow;
					}
				}
				else
				{
					e.Row.BackColor = Color.LightGreen;
				}
			}
		}

		private void SortGridView(string sortExpression, string direction, object source)
		{
			//DataTable dt = //((Fask.ModulePohoda.DataSets.Report)((GridView)source).DataSource).Tables[0];// report.Tables[0];

			SQL_Datasets.Report report = new SQL_Datasets.Report();

			FillReportDataSet(report, lbIPrehledDsInv.SelectedValue, false, "");

			DataTable dt = report.Tables[0];

			DataView dv = new DataView(dt);
			dv.Sort = sortExpression + direction;

			gvInventura.DataSource = dv;
			gvInventura.DataBind();
		}


		void btnPrintSestavaBtn_Click(object sender, EventArgs e)
		{
			SQL_Datasets.Report report = new SQL_Datasets.Report();

			//if (((Button)sender).Text.Equals("Vytvořit tiskovou sestavu rozdílovou"))
			//    FillReportDataSet(report, lbIDsInv.SelectedValue, true, "");
			//else
			FillReportDataSet(report, lbIPrehledDsInv.SelectedValue, false, "");

            //string s = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            Globals.LoadConfiguration();

			string lStyleSheetPath = (new Uri(System.IO.Path.Combine(Path.Combine(Fask.MyPath.Path.RootPath, Globals.Konfigurace.Inventura2[0].PathToXSLT), "TiskovaSestavaInventura2.xslt"))).LocalPath;
			string lXmlPath = (new Uri(System.IO.Path.Combine(Path.Combine(Fask.MyPath.Path.RootPath, Globals.Konfigurace.Inventura2[0].PathDataOutputFile), "Inventura2TiskovaSestava" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml"))).LocalPath;
			string lOutputPath = (new Uri(System.IO.Path.Combine(Path.Combine(Fask.MyPath.Path.RootPath, Globals.Konfigurace.Inventura2[0].PathDataOutputFile), "Inventura2TiskovaSestava" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html"))).LocalPath;


			report.Namespace = "";
			//StreamWriter lWriter = null;
			XmlWriter lWriter = null;
			try
			{
				report.WriteXml(lXmlPath);

				XPathDocument lDoc = new XPathDocument(lXmlPath);
				//XslTransform lTransform = new XslTransform();
				XslCompiledTransform lTransform = new XslCompiledTransform();
				//lWriter = new StreamWriter(lOutputPath);
				lWriter = XmlWriter.Create(lOutputPath);

				lTransform.Load(lStyleSheetPath);
				lTransform.Transform(lDoc, null, lWriter, null);

				lWriter.Close();

				System.Web.HttpResponse response = ((PlaceHolder)((Button)sender).Parent.Parent).Page.Response;
				response.ClearContent();
				response.Clear();
				response.ContentType = "text/plain";
				response.AddHeader("Content-Disposition", "attachment; filename=" + "tiskova_sestava.html" + ";");
				response.TransmitFile(lOutputPath);
				response.Flush();
				response.End();

				//System.Diagnostics.Process.Start(lOutputPath);
				//((PlaceHolder)((Button)sender).Parent.Parent).Page.Response.Output.WriteLine("<script>window.open('urltothefile');</script>")

				//Page page = ((PlaceHolder)((Button)sender).Parent.Parent).Page;

				//For security reasons, there is no standard way in Javascript to manipulate/open local files (javascript windows open local file)
				// ((PlaceHolder)((Button)sender).Parent.Parent).Page.Response.Output.WriteLine("<script>window.open('" + lOutputPath + "');</script>");

			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (lWriter != null)
					lWriter.Close();
			}
		}



		void btnPrintSestavaRozdilovaBtn_Click(object sender, EventArgs e)
		{
			SQL_Datasets.Report report = new SQL_Datasets.Report();

			FillReportDataSet(report, lbIPrehledDsInv.SelectedValue, true, "");

            //string s = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            Globals.LoadConfiguration();

			string lStyleSheetPath = (new Uri(System.IO.Path.Combine(Path.Combine(Fask.MyPath.Path.RootPath, Globals.Konfigurace.Inventura2[0].PathToXSLT), "TiskovaSestavaInventura2.xslt"))).LocalPath;
			string lXmlPath = (new Uri(System.IO.Path.Combine(Path.Combine(Fask.MyPath.Path.RootPath, Globals.Konfigurace.Inventura2[0].PathDataOutputFile), "Inventura2TiskovaSestava" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml"))).LocalPath;
			string lOutputPath = (new Uri(System.IO.Path.Combine(Path.Combine(Fask.MyPath.Path.RootPath, Globals.Konfigurace.Inventura2[0].PathDataOutputFile), "Inventura2TiskovaSestava" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html"))).LocalPath;


			report.Namespace = "";
			//StreamWriter lWriter = null;
			XmlWriter lWriter = null;
			try
			{
				report.WriteXml(lXmlPath);

				XPathDocument lDoc = new XPathDocument(lXmlPath);
				//XslTransform lTransform = new XslTransform();
				XslCompiledTransform lTransform = new XslCompiledTransform();
				//lWriter = new StreamWriter(lOutputPath);
				lWriter = XmlWriter.Create(lOutputPath);

				lTransform.Load(lStyleSheetPath);
				lTransform.Transform(lDoc, null, lWriter, null);

				lWriter.Close();

				System.Web.HttpResponse response = ((PlaceHolder)((Button)sender).Parent.Parent).Page.Response;
				response.ClearContent();
				response.Clear();
				response.ContentType = "text/plain";
				response.AddHeader("Content-Disposition", "attachment; filename=" + "tiskova_sestava_rozdilova.html" + ";");
				response.TransmitFile(lOutputPath);
				response.Flush();
				response.End();

				//For security reasons, there is no standard way in Javascript to manipulate/open local files (javascript windows open local file)
				// ((PlaceHolder)((Button)sender).Parent.Parent).Page.Response.Output.WriteLine("<script>window.open('" + lOutputPath + "');</script>");

			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (lWriter != null)
					lWriter.Close();
			}
		}


		void btnFilter_Click(object sender, EventArgs e)
		{
			SQL_Datasets.Report report = new SQL_Datasets.Report();

			FillReportDataSet(report, lbIPrehledDsInv.SelectedValue, false, txtFilterExpr.Text);

			DataTable dt = report.Tables[0];

			DataView dv = new DataView(dt);

			gvInventura.DataSource = dv;
			gvInventura.DataBind();
		}

		private void createDropDownListInventur()
		{
			Fask.DataSets.Inventury2 inv = GetInventuryList();

			if (lbIPrehledDsInv != null)
				lbIPrehledDsInv.Items.Clear();
			if (lblUkoncitIDsInv != null)
				lblUkoncitIDsInv.Items.Clear();


			foreach (Fask.DataSets.Inventury2.HlavickyRow item in inv.Hlavicky)
			{
				if (lbIPrehledDsInv != null)
					lbIPrehledDsInv.Items.Add(item.ID_INV.ToString());
				if (lblUkoncitIDsInv != null)
					lblUkoncitIDsInv.Items.Add(item.ID_INV.ToString());
			}
		}


		private Fask.DataSets.Inventury2 GetInventuryList()
		{
			try
			{
                Globals.LoadConfiguration();
                string select = "SELECT distinct ID_INV FROM " + TABLE_Majetek + " order by ID_INV";

				System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(select, Globals.Konfigurace.ConnectionString[0].FASKDB);

				Fask.DataSets.Inventury2 inventury = new Fask.DataSets.Inventury2();
				dataAdapter.Fill(inventury, inventury.Hlavicky.TableName);

				inventury.AcceptChanges();

				return inventury;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void setGridProperties()
		{
			gvInventura.BackColor = Color.White;
			gvInventura.BorderColor = Color.Gray;
			gvInventura.BorderStyle = BorderStyle.Solid;
			gvInventura.BorderWidth = new Unit(1, UnitType.Pixel);
			gvInventura.ForeColor = Color.Black;

			gvInventura.Sorting += new GridViewSortEventHandler(gview_Sorting);
			// gview.EnableSortingAndPagingCallbacks = true;
			gvInventura.AllowSorting = true;
			gvInventura.RowDataBound += new GridViewRowEventHandler(gview_RowDataBound);
			gvInventura.HeaderStyle.BackColor = Color.Navy;
			gvInventura.HeaderStyle.ForeColor = Color.White;
			gvInventura.HeaderStyle.BorderColor = Color.DarkGray;
			gvInventura.HeaderStyle.BorderWidth = new Unit(1, UnitType.Pixel);
			gvInventura.HeaderStyle.BorderStyle = BorderStyle.Solid;
			gvInventura.AlternatingRowStyle.BackColor = Color.LightGray;

			gvInventura.PageSize = 20;
			gvInventura.PagerSettings.Mode = PagerButtons.NumericFirstLast;

			BindGridViewColumns();
		}

		private void BindGridViewColumns()
		{
			gvInventura.AutoGenerateColumns = false;
			gvInventura.ID = "gvInventura";

			BoundField column = new BoundField();
			column.DataField = "Kategorie";
			column.HeaderText = "Typ";
			column.SortExpression = "Kategorie";
			gvInventura.Columns.Add(column);

			BoundField column1 = new BoundField();
			column1.DataField = "MnozstviEvidovane";
			column1.HeaderText = "Množství evidované";
			column1.SortExpression = "MnozstviEvidovane";
			gvInventura.Columns.Add(column1);

			BoundField column2 = new BoundField();
			column2.DataField = "MnozstviSkutecne";
			column2.HeaderText = "Množství skutečné";
			column2.SortExpression = "MnozstviSkutecne";
			gvInventura.Columns.Add(column2);

			BoundField column3 = new BoundField();
			column3.DataField = "I_Cislo";
			column3.HeaderText = "Číslo položky";
			column3.SortExpression = "I_Cislo";
			gvInventura.Columns.Add(column3);

			BoundField column4 = new BoundField();
			column4.DataField = "Nazev";
			column4.HeaderText = "Název položky";
			column4.SortExpression = "Nazev";
			gvInventura.Columns.Add(column4);

			BoundField column5 = new BoundField();
			column5.DataField = "ID_Inv";
			column5.HeaderText = "Inventura číslo";
			column5.SortExpression = "ID_Inv";
			gvInventura.Columns.Add(column5);

			BoundField column6 = new BoundField();
			column6.DataField = "Kancl";
			column6.HeaderText = "Místnost";
			column6.SortExpression = "Kancl";
			gvInventura.Columns.Add(column6);

			BoundField column8 = new BoundField();
			column8.DataField = "Kancl_novy";
			column8.HeaderText = "Místnost nová";
			column8.SortExpression = "Kancl_novy";
			gvInventura.Columns.Add(column8);
			/*
			BoundField column7 = new BoundField();
			column7.DataField = "Stredisko";
			column7.HeaderText = "Středisko";
			column7.SortExpression = "Stredisko";
			gvInventura.Columns.Add(column7);*/
		}



		#endregion

		#endregion

	}
}
