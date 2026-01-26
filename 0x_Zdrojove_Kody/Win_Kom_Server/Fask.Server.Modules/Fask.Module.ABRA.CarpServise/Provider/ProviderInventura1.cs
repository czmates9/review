using Fask.DataSets;
using Fask.Module.ABRA.CarpServise.Classes;
using Fask.Server.Interfaces.Classes;
using Fask.Server.Interfaces.Inventura1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Fask.Module.ABRA.CarpServise
{
    public partial class Provider : Fask.Server.Interfaces.Inventura1.IInventura1, Fask.Server.Interfaces.WebControl.IWebControl
    {

        #region Názvy tabulek

        private string TABLE_CZMST_I1 = "CZMST_I1";
        private string TABLE_CZMST_I1H = "CZMST_I1H";
        private string TABLE_CZMST_I2 = "CZMST_I2";
        private string TABLE_CZMST_I3 = "CZMST_I3";
        private string TABLE_CZMST_I4 = "CZMST_I4";

        #endregion

        #region IWebControl Members

        #region Parametry

        private List<TreeNode> actions = null;
        private PlaceHolder phPrehled = null;
        private PlaceHolder phUkoncit = null;
        private PlaceHolder phExport = null;
        private PlaceHolder phExportLokace = null;  // export dat do lokačního mechanismu

        private Label lblUkoncitText = null;
        private Label lblPrehledText = null;
        private Label lblExportText = null;
        private Label lblExportLokaceText = null;

        private Label lblUkoncitCisloInventury = null;
        //private Label lblPrehledCisloInventury = null;
        //private Label lblExportCisloInventury = null;

        private Button btnUkoncitAction = null;
        private Button btnPrehledAction = null;
        private Button btnExportAction = null;
        private Button btnNaplnitLokaceAction = null;

        private Label lblStavPrehledDat = null;

        private Button btnExportDat = null;
        private Button btnTiskovaSestava = null;

        //private TextBox txtUkoncitIdInv = null;
        //private TextBox txtPrehledIdInv = null;
        //private TextBox txtExportIdInv = null;

        private DropDownList lblUkoncitIDsInv;
        private DropDownList lblPrehledIDsInv;

        private GridView gvInventura;

        //private Button btnDiffSestava;

        private Label LblHidePomocna;

        #endregion

        public object getAction(TreeNode selectedAction, Page page)
        {
            if (selectedAction.Value == "Export inventury - ABRA")
            {
                #region export
                if (phExport == null)
                {
                    phExport = new PlaceHolder();

                    btnExportAction = new Button();
                    btnExportAction.OnClientClick = "if (! confirm('Opravdu chcete provést export inventury?')) return false;";
                    btnExportAction.Click += new EventHandler(btnExportAction_Click);
                    btnExportAction.Text = "Exportovat inventuru";
                    phExport.Controls.Add(btnExportAction);
                    btnExportAction.ID = "btnExportAction";

                    phExport.Controls.Add(new LiteralControl("<br />"));

                    lblExportText = new Label();
                    lblExportText.ID = "lblExportText";
                    phExport.Controls.Add(lblExportText);

                }
                else
                {
                    btnExportAction.Click -= new EventHandler(btnExportAction_Click);
                    btnExportAction.Click += new EventHandler(btnExportAction_Click);
                }
                return phExport;

                #endregion
            }
            else if (selectedAction.Value == "Ukončit inventuru - ABRA")
            {
                #region ukoncitInventuru
                if (phUkoncit == null)
                {
                    phUkoncit = new PlaceHolder();

                    lblUkoncitCisloInventury = new Label();
                    lblUkoncitCisloInventury.Text = "Číslo inventury: ";
                    phUkoncit.Controls.Add(lblUkoncitCisloInventury);
                    lblUkoncitCisloInventury.ID = "lblUkoncitCisloInventury";

                    lblUkoncitIDsInv = new DropDownList();
                    phUkoncit.Controls.Add(lblUkoncitIDsInv);
                    lblUkoncitIDsInv.ID = "lblUkoncitIDsInv";

                    phUkoncit.Controls.Add(new LiteralControl("<br />"));

                    btnUkoncitAction = new Button();
                    btnUkoncitAction.OnClientClick = "if (! confirm('Opravdu chcete ukončit inventuru?')) return false;";
                    btnUkoncitAction.Click += new EventHandler(btnEndInventura_Click);
                    btnUkoncitAction.Text = "Ukončit inventuru";
                    phUkoncit.Controls.Add(btnUkoncitAction);
                    btnUkoncitAction.ID = "btnUkoncitAction";


                    phUkoncit.Controls.Add(new LiteralControl("<br />"));

                    lblUkoncitText = new Label();
                    lblUkoncitText.ID = "lblUkoncitText";
                    phUkoncit.Controls.Add(lblUkoncitText);
                }
                else
                {
                    btnUkoncitAction.Click -= new EventHandler(btnEndInventura_Click);
                    btnUkoncitAction.Click += new EventHandler(btnEndInventura_Click);
                }

                createDropDownListInventur();

                return phUkoncit;
                #endregion
            }
            else if (selectedAction.Value == "Přehled zpracovaných dat")
            {
                #region prehled dat
                if (phPrehled == null)
                {
                    phPrehled = new PlaceHolder();

                    phPrehled.Page = page;
                    gvInventura = new GridView();

                    gvInventura.PageIndexChanging -= new GridViewPageEventHandler(gview_PageIndexChanging);
                    gvInventura.PageIndexChanging += new GridViewPageEventHandler(gview_PageIndexChanging);

                    initPrehledPlaceHolder();
                }
                else
                {
                    phPrehled.Page = page;

                    btnPrehledAction.Click -= new EventHandler(btnPrehled_Click);
                    btnPrehledAction.Click += new EventHandler(btnPrehled_Click);

                    gvInventura.Sorting -= new GridViewSortEventHandler(gview_Sorting);
                    gvInventura.Sorting += new GridViewSortEventHandler(gview_Sorting);

                    gvInventura.RowDataBound -= new GridViewRowEventHandler(gview_RowDataBound);
                    gvInventura.RowDataBound += new GridViewRowEventHandler(gview_RowDataBound);


                    btnExportDat.Click -= new EventHandler(btnExportDatCSV_Click);
                    btnExportDat.Click += new EventHandler(btnExportDatCSV_Click);

                    lblStavPrehledDat.Text = "";

                    btnTiskovaSestava.Click -= new EventHandler(btnTiskovaSestava_Click);
                    btnTiskovaSestava.Click += new EventHandler(btnTiskovaSestava_Click);


                    gvInventura.PageIndexChanging -= new GridViewPageEventHandler(gview_PageIndexChanging);
                    gvInventura.PageIndexChanging += new GridViewPageEventHandler(gview_PageIndexChanging);
                }

                createDropDownListInventur();

                return phPrehled;
                #endregion
            }
            else if (selectedAction.Value == "Naplnit lokační mechanismus")
            {
                #region export
                if (phExportLokace == null)
                {
                    phExportLokace = new PlaceHolder();
                    // btnNaplnitLokaceAction
                    btnNaplnitLokaceAction = new Button();
                    btnNaplnitLokaceAction.OnClientClick = "if (! confirm('Opravdu chcete naplnit lokační mechanismus nasnímanými inventurními daty?')) return false;";
                    btnNaplnitLokaceAction.Click += new EventHandler(btnExportLokaceAction_Click);
                    btnNaplnitLokaceAction.Text = "Naplnit lokační mechanismus";
                    phExportLokace.Controls.Add(btnNaplnitLokaceAction);
                    btnNaplnitLokaceAction.ID = "btnNaplnitLokaceAction";

                    phExportLokace.Controls.Add(new LiteralControl("<br />"));

                    lblExportLokaceText = new Label();
                    lblExportLokaceText.ID = "lblExportLokaceText";
                    phExportLokace.Controls.Add(lblExportLokaceText);

                }
                else
                {
                    btnNaplnitLokaceAction.Click -= new EventHandler(btnExportLokaceAction_Click);
                    btnNaplnitLokaceAction.Click += new EventHandler(btnExportLokaceAction_Click);
                }
                return phExportLokace;

                #endregion
            }
            return null;
        }

        public List<TreeNode> getActions(User uzivatel)
        {
            if (actions == null)
            {
                actions = new List<TreeNode>();

                TreeNode tn = new TreeNode();
                tn.Value = "Export inventury - ABRA";
                tn.Text = "Export inventury - ABRA";
                actions.Add(tn);

                TreeNode tn1 = new TreeNode();
                tn1.Value = "Ukončit inventuru - ABRA";
                tn1.Text = "Ukončit inventuru - ABRA";
                actions.Add(tn1);


                TreeNode tn3 = new TreeNode();
                tn3.Value = "Přehled zpracovaných dat";
                tn3.Text = "Přehled zpracovaných dat";
                actions.Add(tn3);

                TreeNode tn4 = new TreeNode();
                tn4.Value = "Naplnit lokační mechanismus";
                tn4.Text = "Naplnit lokační mechanismus";
                actions.Add(tn4);
            }

            return actions;
        }

        #region Private methods for IWebControl

        #region Button eventy
        
        private void btnExportAction_Click(object sender, EventArgs e)
        {
            StatusObject so = null;
            try
            {
                string statusFile = (new Uri(System.IO.Path.Combine(
                           System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
                           @"..\ExportImport\Inventura1Import.so"))).LocalPath;

                so = new StatusObject(statusFile);
                if (so.Exists)
                {
                    lblExportText.Text = "Export inventury se nezdařil - jiz probiha";
                    return;
                }

                so.Write("probiha export");
                bool succes = false;
                int PocetInventur = 0;

                succes = Inventura.LoadInventura(out PocetInventur);

                if (succes)
                {
                    if(PocetInventur == 0)
                        lblExportText.Text = "žádná inventura nebyla exportována";
                    else
                        lblExportText.Text = "Inventury úspěšně exportovány :" + PocetInventur.ToString();

                }
                else
                    lblExportText.Text = "Export inventury se nezdařil";

                so.Write("export ukoncen");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                if (so != null)
                    so.Delete();
            }
        }

        private void btnEndInventura_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = null;
            StatusObject so = null;

            try
            {
                string statusFile = (new Uri(System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
                    @"..\ExportImport\Inventura1Export.so"))).LocalPath;

                so = new StatusObject(statusFile);
                if (so.Exists)
                {
                    lblUkoncitText.Text = "Export inventury se nezdařil - jiz probiha";
                    return;
                }

                // nacteni poctu neuzavrenych zaznamu inventury
                string selectTermID = "select COUNT(*) from " + TABLE_CZMST_I1 + " where TerminalID<100 and CountEntries=" + lblUkoncitIDsInv.SelectedValue;
                Globals_V1.LoadConfiguration();
                sqlcon = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                sqlcon.Open();

                // kontrola, zdali jiz byla inventura uzavrena
                System.Data.SqlClient.SqlCommand selectCommand = new System.Data.SqlClient.SqlCommand(selectTermID, sqlcon);
                int count = (int)selectCommand.ExecuteScalar();

                if (count == 0)
                {
                    lblUkoncitText.Text = "Inventura již byla ukončena!!";
                    return;
                }

                so.Write("probiha export");
                bool succes = false;


                string updatei1 = "Update " + TABLE_CZMST_I1 + " set TerminalID= TerminalID + 100 where CountEntries=" + lblUkoncitIDsInv.SelectedValue;

                succes = Inventura.ImportInventura(int.Parse(lblUkoncitIDsInv.SelectedValue), true);

                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatei1, sqlcon);


                int rows = command.ExecuteNonQuery();

                if (succes)
                    lblUkoncitText.Text = "Inventura úspěšně importována";
                else
                    lblUkoncitText.Text = "Import inventury se nezdařil";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();

                if (so != null)
                    so.Delete();

            }
        }

        private void btnExportDatCSV_Click(object sender, EventArgs e)
        {
            try
            {

                SQL_Datasets.ReportInventura1 report = null;
                if (reportInventura1 != null && reportInventura1.Polozka != null && reportInventura1.Polozka.Count > 0)
                    report = reportInventura1;
                else
                {
                    report = new SQL_Datasets.ReportInventura1();
                    //if (((Button)sender).Text.Equals("Vytvořit tiskovou sestavu rozdílovou"))
                    //    FillReportDataSet(report, lbIDsInv.SelectedValue, true, "");
                    //else
                    FillReportDataSet(report, lblPrehledIDsInv.SelectedValue);
                }
                //string s = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

                string lStyleSheetPath = (new Uri(System.IO.Path.Combine(Globals_V1.Konfigurace.Inventura1[0].PathToXSLT, "TiskovaSestavaInventura1.xslt"))).LocalPath;

                string lXmlPath = (new Uri(System.IO.Path.Combine(Globals_V1.Konfigurace.Inventura1[0].PathDataOutputFile, "Inventura1TiskovaSestava" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml"))).LocalPath;

                string lOutputPath = (new Uri(System.IO.Path.Combine(Globals_V1.Konfigurace.Inventura1[0].PathDataOutputFile, "Inventura1TiskovaSestava" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html"))).LocalPath;

                string lCSVPath = (new Uri(System.IO.Path.Combine(Globals_V1.Konfigurace.Inventura1[0].PathDataOutputFile, "Inventura1TiskovaSestava" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv"))).LocalPath;

                //report.Namespace = "";
                //StreamWriter lWriter = null;

                //report.WriteXml(lXmlPath);

                System.IO.StreamWriter file = new System.IO.StreamWriter(lCSVPath, false, Encoding.UTF8);

                //file.WriteLine("CisloPolozky;Kod;Nazev;Skut. stav; Evid. stav;Lokace;Sklad"); //Čár. kód položky
                file.WriteLine("CisloPolozky;Kod;Nazev;Skut. stav; Evid. stav;Lokace;Sklad;Čár. kód");
                foreach (SQL_Datasets.ReportInventura1.PolozkaRow row in report.Polozka)
                {
                    string finalrow = row.Itemnmbr.Trim() + ";" +   //toto je vzdy cislo ...
                                                                    //(row.IsITEMCODENull() ? "" : row.ITEMCODE) + ";" +
                        "\"" + (row.IsITEMCODENull() ? "" : row.ITEMCODE.Trim().Replace("\"", "\"\"")) + "\"" + ";" + // i zde je problem s textem, protoze excel prevadi hodnoty s carkami nebo cisly na cisla. ..  a pro jistotu i uvozovky ...
                                                                                                                      //(row.IsNazevNull() ? "" : row.Nazev) + ";" + 
                        "\"" + (row.IsNazevNull() ? "" : row.Nazev.Trim().Replace("\"", "\"\"")) + "\"" + ";" + //jedna se o text, ktery muze obsahovat strednik a uvozovky, proto je uvozen uvozovkami ...
                                                                                                                //(row.IsMnozstviSkutecneNull() ? "" : row.MnozstviSkutecne.ToString("0.00", CultureInfo.InvariantCulture)) + ";" + 
                                                                                                                //(row.IsMnozstviEvidovaneNull() ? "" : row.MnozstviEvidovane.ToString("0.00", CultureInfo.InvariantCulture)) + ";" +
                        (row.IsMnozstviSkutecneNull() ? "" : row.MnozstviSkutecne.ToString()) + ";" +
                        (row.IsMnozstviEvidovaneNull() ? "" : row.MnozstviEvidovane.ToString()) + ";" +
                        (row.IsLokaceNull() ? "" : row.Lokace.Trim().Replace("\"", "\"\"")) + ";" +
                        (row.IsSkladNull() ? "" : row.Sklad.Trim().Replace("\"", "\"\"")) + ";" +
                        "\"" + (row.IsEANNull() ? "" : row.EAN.Trim().Replace("\"", "\"\"")) + "\"";

                    file.WriteLine(
                        finalrow.Replace("\n", "").Replace("\r\n", "")
                        );
                }
                file.Close();

                System.Web.HttpResponse response = ((PlaceHolder)((Button)sender).Parent.Parent).Page.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition", "attachment; filename=" + "data.csv" + ";");
                response.TransmitFile(lCSVPath);
                response.Flush();
                response.End();

                lblStavPrehledDat.Text = "Inventura úspěšně exportována!";

                return;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                //if (lWriter != null)
                //    lWriter.Close();
            }
        }

        private void btnPrehled_Click(object sender, EventArgs e)
        {
            if (phPrehled != null)
            {
                reportInventura1 = new SQL_Datasets.ReportInventura1();
                FillReportDataSet(reportInventura1, lblPrehledIDsInv.SelectedValue);
                gvInventura.DataSource = reportInventura1;
                gvInventura.DataBind();
            }
        }

        private void btnTiskovaSestava_Click(object sender, EventArgs e)
        {

            Globals_V1.LoadConfiguration();

            SQL_Datasets.ReportInventura1 report = null;
            if (reportInventura1 != null && reportInventura1.Polozka != null && reportInventura1.Polozka.Count > 0)
                report = reportInventura1;
            else
            {
                report = new SQL_Datasets.ReportInventura1();

                FillReportDataSet(report, lblPrehledIDsInv.SelectedValue);
            }
    
            string lStyleSheetPath = (new Uri(System.IO.Path.Combine(Globals_V1.Konfigurace.Inventura1[0].PathToXSLT, "TiskovaSestavaInventura1.xslt"))).LocalPath;
            string lXmlPath = (new Uri(System.IO.Path.Combine(Globals_V1.Konfigurace.Inventura1[0].PathDataOutputFile, "Inventura1TiskovaSestava" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml"))).LocalPath;
            string lOutputPath = (new Uri(System.IO.Path.Combine(Globals_V1.Konfigurace.Inventura1[0].PathDataOutputFile, "Inventura1TiskovaSestava" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html"))).LocalPath;
         

            report.Namespace = "";
            XmlWriter lWriter = null;
            try
            {
                report.WriteXml(lXmlPath);


                XPathDocument lDoc = new XPathDocument(lXmlPath);
                XslCompiledTransform lTransform = new XslCompiledTransform();
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

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (lWriter != null)
                //    lWriter.Close();
            }
        }

        private void btnExportLokaceAction_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand insertCommand = null;
            SqlCommand countCommand = null;
            SqlTransaction trans = null;

            try
            {
                lblExportLokaceText.Text = string.Empty;

                conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                conn.Open();
                trans = conn.BeginTransaction(IsolationLevel.Serializable);

                string countcmdtext = "select COUNT(*) from [CZMST_SkladLokace_Stav];";
                countCommand = new SqlCommand(countcmdtext, conn, trans);
                int rowcount = (int)countCommand.ExecuteScalar();
                if (rowcount > 0)
                {
                    throw new Exception("Tabulka [CZMST_SkladLokace_Stav] již obsahuje '" + rowcount + "' záznamů. Záznamy z inventury nebudou přidány.");
                }

                string insertcmdtext = "INSERT INTO [CZMST_SkladLokace_Stav] " +
               "([ITEMNMBR], " +
               "[ITEMDESC], " +
               "[QTYSHPPD_DEF], " +
               "[QTYSHPPD], " +
               "[SERLTNUM], " +
               "[SKL_ID], " +
               "[LOCNCODE], " +
               "[DATECHANGE], " +
               "[EXPIRATION], " +
               "[QTYSHPPD_DEF_DATE]) " +
               "select " +
               "i4.ITEMNMBR as ITEMNMBR, " +
               "ISNULL(i1.ITEMDESC, '') as ITEMDESC, " +
               "SUM(i4.QUANTITY) as QTYSHPPD_DEF, " +
               "SUM(i4.QUANTITY) as QTYSHPPD, " +
               "ISNULL(i4.SERLNMBR, '') as SERLTNUM, " +
               "ISNULL(i4.skl_id, '') as SKL_ID, " +
               "ISNULL(i4.LOCNCODE, '') as LOCNCODE, " +
               "GETDATE() as DATECHANGE, " +
               "null as EXPIRATION, " +
               "GETDATE() as QTYSHPPD_DEF_DATE " +
               "from [CZMST_I4] i4 " +
               "left join [CZMST_I1] i1 on i1.ITEMNMBR = i4.ITEMNMBR " +
               "group by i4.ITEMNMBR, i1.ITEMDESC, i4.SERLNMBR, i4.skl_id, i4.LOCNCODE;"
               ;

                insertCommand = new System.Data.SqlClient.SqlCommand(insertcmdtext, conn, trans);

                int rows = insertCommand.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                lblExportLokaceText.Text = "Lokační mechanismus úspešně naplněn inventurními daty dne " + DateTime.Now + ".<br />Do lokačního mechanismu bylo přidáno '" + rows + "' nových záznamů.";
            }
            catch (Exception ex)
            {
                try
                {
                    // chyba, rollback transakce ...
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                lblExportLokaceText.Text = "Chyba při exportu dat do lokačního mechanismu: " + ex.Message;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                    conn.Close();

            }
        }

        #endregion

        private void createDropDownListInventur()
        {
            Fask.DataSets.Inventury1 inv = GetInventuryList();
            if (lblPrehledIDsInv != null)
                lblPrehledIDsInv.Items.Clear();
            if (lblUkoncitIDsInv != null)
                lblUkoncitIDsInv.Items.Clear();

            if (lblPrehledIDsInv != null)
            {
                lblPrehledIDsInv.DataSource = inv.Hlavicky;
                lblPrehledIDsInv.DataTextField = "Description";
                lblPrehledIDsInv.DataValueField = "CountEntries";
                lblPrehledIDsInv.DataBind();
            }

            if (lblUkoncitIDsInv != null)
            {
                lblUkoncitIDsInv.DataSource = inv.Hlavicky;
                lblUkoncitIDsInv.DataTextField = "Description";
                lblUkoncitIDsInv.DataValueField = "CountEntries";
                lblUkoncitIDsInv.DataBind();
            }
            /*foreach (Fask.DataSets.Inventury1.HlavickyRow item in inv.Hlavicky)
            {
                if (lblPrehledIDsInv != null)
                    lblPrehledIDsInv.Items.Add(item.CountEntries.ToString());
                if (lblUkoncitIDsInv != null)
                    lblUkoncitIDsInv.Items.Add(item.CountEntries.ToString());
            }*/
        }

        private Fask.DataSets.Inventury1 GetInventuryList()
        {
            try
            {

                string select = "SELECT * FROM " + TABLE_CZMST_I1H;

                System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(select, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                Fask.DataSets.Inventury1 inventury = new Fask.DataSets.Inventury1();
                dataAdapter.Fill(inventury, inventury.Hlavicky.TableName);
                inventury.AcceptChanges();

                return inventury;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region GridView eventy

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        private SQL_Datasets.ReportInventura1 reportInventura1;

        void gview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInventura.PageIndex = e.NewPageIndex;
            gvInventura.DataBind();
        }

        private void gview_Sorting(object sender, GridViewSortEventArgs e)
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

        private void SortGridView(string sortExpression, string direction, object source)
        {
            if (reportInventura1 == null)
                FillReportDataSet(reportInventura1, lblPrehledIDsInv.SelectedValue);

            DataTable dt = reportInventura1.Tables[0];

            DataView dv = new DataView(dt);
            dv.Sort = sortExpression + direction;

            gvInventura.DataSource = dv;
            gvInventura.DataBind();
        }

        private void FillReportDataSet(SQL_Datasets.ReportInventura1 report, string idInv)
        {
            if (idInv == string.Empty && lblPrehledIDsInv != null)
            {
                if (lblPrehledIDsInv.SelectedValue == "")
                    return;

                idInv = lblPrehledIDsInv.SelectedValue;
            }

            SQL_Datasets.InventuraTableAdapters.CZMST_I1TableAdapter i1_ta = new SQL_Datasets.InventuraTableAdapters.CZMST_I1TableAdapter();
            SQL_Datasets.InventuraTableAdapters.CZMST_I4TableAdapter i4_ta = new SQL_Datasets.InventuraTableAdapters.CZMST_I4TableAdapter();
            SQL_Datasets.InventuraTableAdapters.CZMST093TableAdapter sklady_ta = new SQL_Datasets.InventuraTableAdapters.CZMST093TableAdapter();

            Globals_V1.LoadConfiguration();

            i1_ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
            i4_ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
            sklady_ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

            SQL_Datasets.Inventura.CZMST_I1DataTable i1 = null;
            SQL_Datasets.Inventura.CZMST_I4DataTable i4 = null;
            SQL_Datasets.Inventura.CZMST093DataTable czmst093 = null;

            int id = int.Parse(idInv);

            i1 = i1_ta.GetDataByCountEntries(id/*lbIDsInv.SelectedValue*/);
            i4 = i4_ta.GetDataByCountEntries(id/*lbIDsInv.SelectedValue*/);
            czmst093 = sklady_ta.GetData();


            foreach (SQL_Datasets.Inventura.CZMST_I1Row item in i1)
            {
                Object obj = i4.Compute("Sum(QUANTITY)", "ITEMNMBR='" + item.ITEMNMBR + "'");
                decimal? kusu = null;
                try { kusu = (decimal)obj; }
                catch { }

                SQL_Datasets.ReportInventura1.PolozkaRow nr = report.Polozka.NewPolozkaRow();
                nr.Itemnmbr = item.ITEMNMBR.Trim();
                nr.EAN = item.CZ_CarKod.Trim();
                nr.MnozstviEvidovane = item.QUANTITY;
                if (kusu.HasValue)
                    nr.MnozstviSkutecne = kusu.Value;
                nr.Nazev = item.ITEMDESC.Trim();
                nr.Lokace = item.LOCNCODE.Trim();
                SQL_Datasets.Inventura.CZMST093Row[] skladrows = (SQL_Datasets.Inventura.CZMST093Row[])czmst093.Select("skl_id='" + item.skl_id.Trim() + "'");
                if (skladrows.Length > 0)
                    nr.Sklad = skladrows[0].skl_desc;
                else
                    nr.Sklad = item.skl_id.Trim();
                nr.ID_Inv = idInv;

                nr.ITEMCODE = item.IsITEMCODENull() ? "-" : item.ITEMCODE;

                report.Polozka.AddPolozkaRow(nr);
            }
        }

        protected void gview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                decimal? kusu = row.Field<decimal?>("MnozstviEvidovane");
                decimal? nacteno = row.Field<decimal?>("MnozstviSkutecne");

                if (kusu == nacteno)
                    e.Row.BackColor = Color.LightGreen;

                if (nacteno < kusu)
                    e.Row.BackColor = Color.Red;

                if (nacteno > kusu)
                    e.Row.BackColor = Color.Yellow;

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
            gvInventura.PagerSettings.PageButtonCount = 20;
            gvInventura.PagerSettings.Position = PagerPosition.TopAndBottom;

            BindGridViewColumns();
        }

        private void BindGridViewColumns()
        {
            gvInventura.AutoGenerateColumns = false;


            BoundField column = new BoundField();
            column.DataField = "Itemnmbr";
            column.HeaderText = "Číslo položky";
            column.SortExpression = "Itemnmbr";
            gvInventura.Columns.Add(column);

            BoundField columnCode = new BoundField();
            columnCode.DataField = "ITEMCODE";
            columnCode.HeaderText = "Kód";
            columnCode.SortExpression = "ITEMCODE";
            gvInventura.Columns.Add(columnCode);

            BoundField column4 = new BoundField();
            column4.DataField = "Nazev";
            column4.HeaderText = "Název";
            column4.SortExpression = "Nazev";
            gvInventura.Columns.Add(column4);

            BoundField column2 = new BoundField();
            column2.DataField = "MnozstviSkutecne";
            column2.HeaderText = "Skut. stav";
            column2.SortExpression = "MnozstviSkutecne";
            gvInventura.Columns.Add(column2);

            BoundField column1 = new BoundField();
            column1.DataField = "MnozstviEvidovane";
            column1.HeaderText = "Evid. stav";
            column1.SortExpression = "MnozstviEvidovane";
            gvInventura.Columns.Add(column1);



            BoundField column3 = new BoundField();
            column3.DataField = "EAN";
            column3.HeaderText = "Čár. kód";
            column3.SortExpression = "EAN";
            gvInventura.Columns.Add(column3);
            /*
             BoundField column5 = new BoundField();
             column5.DataField = "ID_Inv";
             column5.HeaderText = "Inventura číslo";
             column5.SortExpression = "ID_Inv";
             gvInventura.Columns.Add(column5);
          

             BoundField column6 = new BoundField();
             column6.DataField = "Lokace";
             column6.HeaderText = "Lokace";
             column6.SortExpression = "Lokace";
             gvInventura.Columns.Add(column6);
             */

            BoundField column7 = new BoundField();
            column7.DataField = "Sklad";
            column7.HeaderText = "Sklad";
            column7.SortExpression = "Sklad";
            gvInventura.Columns.Add(column7);
        }

        #endregion

        private void initPrehledPlaceHolder()
        {
            lblPrehledText = new Label();
            lblPrehledText.Text = "Inventura: ";
            phPrehled.Controls.Add(lblPrehledText);
            lblPrehledText.ID = "lblPrehledText";

            lblPrehledIDsInv = new DropDownList();
            lblPrehledIDsInv.ID = "lblPrehledIDsInv";

            phPrehled.Controls.Add(lblPrehledIDsInv);

            phPrehled.Controls.Add(new LiteralControl("<br />"));

            phPrehled.Controls.Add(new LiteralControl("<br />"));

            btnPrehledAction = new Button();
            btnPrehledAction.Text = "Vygenerovat přehled dat";
            btnPrehledAction.Click += new EventHandler(btnPrehled_Click);
            phPrehled.Controls.Add(btnPrehledAction);
            btnPrehledAction.ID = "btnPrehledAction";

            phPrehled.Controls.Add(new LiteralControl("<br />"));

            btnExportDat = new Button();
            btnExportDat.Text = "Export data CSV";
            btnExportDat.Click += new EventHandler(btnExportDatCSV_Click);
            phPrehled.Controls.Add(btnExportDat);
            btnExportDat.ID = "btnExportDat";

            btnTiskovaSestava = new Button();
            btnTiskovaSestava.Text = "Vytvořit tiskovou sestavu";
            btnTiskovaSestava.Click += new EventHandler(btnTiskovaSestava_Click);
            phPrehled.Controls.Add(btnTiskovaSestava);
            btnTiskovaSestava.ID = "btnTiskovaSestava";

            phPrehled.Controls.Add(new LiteralControl("<br />"));

            lblStavPrehledDat = new Label();
            lblStavPrehledDat.Text = "";
            phPrehled.Controls.Add(lblStavPrehledDat);
            lblStavPrehledDat.ID = "lblStavPrehledDat";

            phPrehled.Controls.Add(new LiteralControl("<br />"));

            gvInventura.ID = "gvInventura";
            phPrehled.Controls.Add(gvInventura);
            setGridProperties();


            reportInventura1 = new SQL_Datasets.ReportInventura1();
            FillReportDataSet(reportInventura1, "");

            gvInventura.AllowPaging = true;
            gvInventura.DataSource = reportInventura1;

            gvInventura.DataBind();


            LblHidePomocna = new Label();
            LblHidePomocna.Text = " ASC";
            LblHidePomocna.Visible = false;
            phPrehled.Controls.Add(LblHidePomocna);

            //return phpom;
        }

        #endregion


        #endregion

        #region IInventura1 Members

        public bool Inventura_AfterProcessedAction(Davka davka)
        {
            return true;
        }

        public Inventura1 Inventura_GetInventura(Davka davka, Terminal terminal)
        {
            try
            {
                Fask.DataSets.Inventura1 inventura = new Fask.DataSets.Inventura1();

                string select1 = "SELECT * FROM " + TABLE_CZMST_I1 + " where CountEntries=" + davka.ID + " order by dex_row_id";
                string select2 = "SELECT * FROM " + TABLE_CZMST_I2 + " where CountEntries=" + davka.ID + " order by dex_row_id";
                string select3 = "SELECT * FROM " + TABLE_CZMST_I3 + " where CountEntries=" + davka.ID + " order by dex_row_id";
                string select4 = "SELECT * FROM " + TABLE_CZMST_I4 + " where CountEntries=" + davka.ID + " order by dex_row_id";
                string select5 = "SELECT * FROM " + TABLE_CZMST_I1H + " where CountEntries=" + davka.ID;


                Globals_V1.LoadConfiguration();

                using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select1, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    sql.Fill(inventura, inventura.CZMST_I1.TableName);
                }

                using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select2, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    sql.Fill(inventura, inventura.CZMST_I2.TableName);
                }

                using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select3, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    sql.Fill(inventura, inventura.CZMST_I3.TableName);
                }

                using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select5, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    sql.Fill(inventura, inventura.CZMST_I1H.TableName);
                }

                return inventura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Inventura_GetInventuraReceived(Davka davka, Terminal terminal)
        {
            string selectCount = "SELECT Count(CountEntries) as davka FROM " + TABLE_CZMST_I1 + " where CountEntries=" + davka.ID + " AND (TerminalID<=0 or TerminalID=" + terminal.ID + ")";
            string update = "Update " + TABLE_CZMST_I1 + " set TerminalID=" + terminal.ID + " where countentries=" + davka.ID;


            Globals_V1.LoadConfiguration();

            System.Data.SqlClient.SqlConnection sql = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

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

                if (!Globals_V1.Konfigurace.Inventura1[0].DavkaTerminalVice)
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

        public Inventury1 Inventura_GetInventury(Terminal terminal, Sklad sklad)
        {
            Globals_V1.LoadConfiguration();
            try
            {
                //Vrati seznam davek inventury, ktere jsou volne pro stazeni nebo jsou stazene terminalem, ktery zada o stazeni
                string select = string.Empty;
                if (Globals_V1.Konfigurace.Inventura1[0].DavkaTerminalVice)
                {
                    //select = "SELECT distinct CountEntries FROM " + TABLE_CZMST_I1 + " WHERE TerminalID < 100 order by countentries";
                    //select = "SELECT * FROM " + TABLE_CZMST_I1H + " WHERE State < 2 order by countentries";
                    select = "SELECT * FROM " + TABLE_CZMST_I1H +
                        "   WHERE State < 2" +
                        " and CountEntries in (" +
                        " SELECT distinct CountEntries FROM " + TABLE_CZMST_I1 +
                        " WHERE TerminalID < 100" +
                        (!String.IsNullOrEmpty(sklad.ID) ? (" AND SKL_ID Like '" + sklad.ID + "%'") : "") + //filtr na sklad s like
                        ")" +
                        " order by CountEntries";
                }
                else
                {
                    //select = "SELECT distinct CountEntries FROM " + TABLE_CZMST_I1 + " WHERE TerminalID <= 0 or TerminalID=" + terminal.ID + " order by countentries";
                    select = "SELECT * FROM " + TABLE_CZMST_I1H +
                        "   WHERE State < 2" +
                        " and CountEntries in (" +
                        " SELECT distinct CountEntries FROM " + TABLE_CZMST_I1 +
                        " WHERE TerminalID <= 0 or TerminalID=" + terminal.ID +
                        (!String.IsNullOrEmpty(sklad.ID) ? (" AND SKL_ID Like '" + sklad.ID + "%'") : "") + //filtr na sklad s like
                        ") order by CountEntries";
                }

                System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(select, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);


                Fask.DataSets.Inventury1 inventury = new Fask.DataSets.Inventury1();
                dataAdapter.Fill(inventury, inventury.Hlavicky.TableName);

                inventury.AcceptChanges();

                return inventury;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Inventura_OnlineCheckState(Davka countentries, Terminal terminal, string itemnmbr, out byte o_terminalid)
        {
            throw new NotImplementedException();
        }

        public bool Inventura_OnlineUnCheckState(Davka davka, Terminal terminal, string itemnmbr, out byte o_terminalid)
        {
            throw new NotImplementedException();
        }

        public StatusObject Inventura_Process(Davka davka, Terminal terminal, Inventura1 inventuradata, ProcessState processInventuraState)
        {
            Globals_V1.LoadConfiguration();

            // \TODO : zpracovani inventurnich dat terminalu 
            SqlTransaction iTrans1 = null;

            string guidDavka;
            if (inventuradata.CZMST_IH.Count > 0)
                guidDavka = inventuradata.CZMST_IH[0].GUID.ToString();
            else
                guidDavka = Guid.NewGuid().ToString();

            string pom = (new Uri(System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
                Globals_V1.Konfigurace.Inventura1[0].PathStateDataFile))).LocalPath;

            string filePath = System.IO.Path.Combine(pom, guidDavka + ".txt");

              StatusObject so = new Fask.Server.Interfaces.Classes.StatusObject(filePath);

            bool uvolnitdavku = processInventuraState == Fask.Server.Interfaces.Inventura1.ProcessState.Uvolnit;

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            try
            {
                so.Write("vytvareni connection");
                conn.Open();

                if (uvolnitdavku) //uvolnit davku
                {
                    so.Write("uvolnit davku");

                    string updateuvolnit = "Update " + TABLE_CZMST_I1 + " set TerminalID=0 where CountEntries=" + davka.ID;
                    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(updateuvolnit, conn, iTrans1);
                    int rows = comm.ExecuteNonQuery();
                }
                else //zapsat davku
                {
                    so.Write("zapsat davku");
                    bool allowInsertData = true;
                    if (!Globals_V1.Konfigurace.Inventura1[0].DavkaTerminalVice)
                    {
                        //Test zda je mozne data pridat, jestlize jiz existuji, tak nepridat. 
                        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand("Select Count(*) as number from " + TABLE_CZMST_I4 + " where countentries=" + davka.ID, conn);
                        object datacount = comm.ExecuteScalar();
                        if (datacount != null && ((int)datacount) > 0)
                            allowInsertData = false;
                    }

                    if (allowInsertData)
                    {
                        so.Write("Update databaze");

                        iTrans1 = conn.BeginTransaction();

                        if (inventuradata.CZMST_I4.Count > 0)
                        {
                            // kontrola na duplicitu guid prvniho a posledniho zaznamu
                            string guidTest = "select COUNT(*) from " + TABLE_CZMST_I4 + " where GUID='" + inventuradata.CZMST_I4.First().GUID.ToString() + "'";
                            if (inventuradata.CZMST_I4.Count > 1)
                            {
                                guidTest += " OR GUID='" + inventuradata.CZMST_I4.Last().GUID.ToString() + "'";
                            }

                            System.Data.SqlClient.SqlCommand testguidcommand = new SqlCommand(guidTest, conn, iTrans1);
                            int guidcount = (int)testguidcommand.ExecuteScalar();

                            if (guidcount == 0)
                            {
                                // guidy nejsou v DB, je mozne ulozit data
                                SQL_Datasets.InventuraTableAdapters.CZMST_I4TableAdapter i4ta = new SQL_Datasets.InventuraTableAdapters.CZMST_I4TableAdapter();
                                i4ta.Connection = conn;
                                i4ta.Transaction = iTrans1;
                                i4ta.Update(inventuradata.CZMST_I4.Select(null, null, DataViewRowState.Added));
                            }
                            else
                            {
                                // jeden z guidu je jiz v DB, zalogovat ...
                                string errortext = "Davka:" + davka.ID + ",GUID:" + inventuradata.CZMST_I4.First().GUID.ToString();
                                if (inventuradata.CZMST_I4.Count > 1)
                                    errortext += " nebo " + inventuradata.CZMST_I4.Last().GUID.ToString();

                                errortext += "jiz je v databazi.";
                                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, errortext);
                            }
                        }

                        string updatei1 = "Update " + TABLE_CZMST_I1 + " set TerminalID=" + (terminal.ID + 100) + " where CountEntries=" + davka.ID;
                        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatei1, conn, iTrans1);

                        if (!Globals_V1.Konfigurace.Inventura1[0].DavkaTerminalVice)
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

            so.SetOK();

            return so;
        } 

        #endregion
    }
}
