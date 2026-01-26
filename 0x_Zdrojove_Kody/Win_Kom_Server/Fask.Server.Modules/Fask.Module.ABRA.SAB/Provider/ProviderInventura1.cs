using Fask.DataSets;
using Fask.Server.Interfaces.Classes;
using Fask.Server.Interfaces.Inventura1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

using Fask.Module.ABRA.SAB.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using Fask.Tracing;

namespace Fask.Module.ABRA.SAB.Provider
{
    public partial class Provider : 
        Fask.Server.Interfaces.Inventura1.IInventura1, 
        Fask.Server.Interfaces.WebControl.IWebControl
    {
        #region IWebControl

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

        private Label lblStatusPrehledDat = null;
        private Label lblStavPrehledDat = null;
        private Label lblPrehledNote = null;

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

        #region Promenne pro ReOpen

        private PlaceHolder phReOpen = null;
        private Label lblReOpenCisloInventury = null;
        private Button btnReOpenAction = null;
        private Label lblReOpenText = null;

        private DropDownList lblReOpenIDsInv;

        #endregion

        #region Promenne pro ExportOne

        private PlaceHolder phExportOne = null;
        private Label lblExportOneCisloInventury = null;
        private Button btnExportOneAction = null;
        private Label lblExportOneText = null;

        private DropDownList lblExportOneIDsInv;

        private Button btnExportOne_Ano = null;
        private Button btnExportOne_Ne = null;

        private void ClearExportOne()
        {
            lblExportOneCisloInventury = null;
            btnExportOneAction = null;
            lblExportOneText = null;

            lblExportOneIDsInv = null;

            btnExportOne_Ano = null;
            btnExportOne_Ne = null;
        }

        #endregion


        #region Value konstaty

        public const string INV_Export= "export";
        public const string INT_Ukonceni = "ukonceni";
        public const string INT_Prehled = "prehled_dat";
        public const string INT_NaplnLokMech = "napln_lok_mech";
        public const string INT_ZnovuOtevreni = "reopen";
        public const string INT_ExportovatPouzeJednu = "exportone";

        #endregion


        #endregion

        public object getAction(TreeNode selectedAction, Page page)
        {

            KillAll();

            if (selectedAction.Value == INV_Export)
            {
                #region export

                if (phExport == null)
                {
                    phExport = new PlaceHolder();

                    phExport.Page = page;

                    btnExportAction = new Button();
                    btnExportAction.Style.Add(HtmlTextWriterStyle.MarginLeft, "200px");
                    btnExportAction.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
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
                //else
                //{
                //    phExport.Page = page;

                //    btnExportAction.Click -= new EventHandler(btnExportAction_Click);
                //    btnExportAction.Click += new EventHandler(btnExportAction_Click);
                //}
                return phExport;

                #endregion
            }
            else if (selectedAction.Value == INT_Ukonceni)
            {
                #region ukoncitInventuru



                if (phUkoncit == null)
                {
                    phUkoncit = new PlaceHolder();

                    phUkoncit.Page = page;

                    var lblUkoncit_EMPTY = new Label();
                    lblUkoncit_EMPTY.Style.Add(HtmlTextWriterStyle.MarginBottom, "150px");
                    phUkoncit.Controls.Add(lblUkoncit_EMPTY);


                    phUkoncit.Controls.Add(new LiteralControl("<br />"));

                    lblUkoncitCisloInventury = new Label();
                    lblUkoncitCisloInventury.Text = "Inventura: ";
                    lblUkoncitCisloInventury.Style.Add(HtmlTextWriterStyle.MarginLeft, "200px");
                    lblUkoncitCisloInventury.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
                    phUkoncit.Controls.Add(lblUkoncitCisloInventury);
                    lblUkoncitCisloInventury.ID = "lblUkoncitCisloInventury";

                    lblUkoncitIDsInv = new DropDownList();
                    phUkoncit.Controls.Add(lblUkoncitIDsInv);
                    lblUkoncitIDsInv.ID = "lblUkoncitIDsInv";

                    phUkoncit.Controls.Add(new LiteralControl("<br />"));

                    btnUkoncitAction = new Button();
                    btnUkoncitAction.Style.Add(HtmlTextWriterStyle.MarginLeft, "200px");
                    btnUkoncitAction.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
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
                //else
                //{
                //    phUkoncit.Page = page;
                //    btnUkoncitAction.Click -= new EventHandler(btnEndInventura_Click);
                //    btnUkoncitAction.Click += new EventHandler(btnEndInventura_Click);
                //}

                createDropDownListInventur_Ukoncit();

                return phUkoncit;
                #endregion
            }
            else if (selectedAction.Value == INT_Prehled)
            {
                #region prehled dat



                if (phPrehled == null)
                {
                    phPrehled = new PlaceHolder();

                    phPrehled.Page = page;
                    gvInventura = new GridView();

                    gvInventura.PageIndexChanging -= new GridViewPageEventHandler(gview_PageIndexChanging);
                    gvInventura.PageIndexChanging += new GridViewPageEventHandler(gview_PageIndexChanging);

                    initPrehledPlaceHolder(page);
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
                    lblStatusPrehledDat.Text = "";
                    

                    btnTiskovaSestava.Click -= new EventHandler(btnTiskovaSestava_Click);
                    btnTiskovaSestava.Click += new EventHandler(btnTiskovaSestava_Click);


                    gvInventura.PageIndexChanging -= new GridViewPageEventHandler(gview_PageIndexChanging);
                    gvInventura.PageIndexChanging += new GridViewPageEventHandler(gview_PageIndexChanging);
                }

                createDropDownListInventur_Prehled();

                return phPrehled;
                #endregion
            }
            else if (selectedAction.Value == INT_NaplnLokMech)
            {
                #region LokMech

  

                if (phExportLokace == null)
                {
                    phExportLokace = new PlaceHolder();

                    phExportLokace.Page = page;
                    // btnNaplnitLokaceAction
                    btnNaplnitLokaceAction = new Button();
                    btnNaplnitLokaceAction.Style.Add(HtmlTextWriterStyle.MarginLeft, "200px");
                    btnNaplnitLokaceAction.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
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
                //else
                //{
                //    phExportLokace.Page = page;
                //    btnNaplnitLokaceAction.Click -= new EventHandler(btnExportLokaceAction_Click);
                //    btnNaplnitLokaceAction.Click += new EventHandler(btnExportLokaceAction_Click);
                //}
                return phExportLokace;

                #endregion
            }
            else if (selectedAction.Value == INT_ZnovuOtevreni)
            {
                #region Otevření už ukončené inventury

                if (phReOpen == null)
                {
                    //Vytvořeni objektu placeHolder
                    phReOpen = new PlaceHolder();

                    phReOpen.Page = page;

                    var lblReOpen_EMPTY = new Label();
                    lblReOpen_EMPTY.Style.Add(HtmlTextWriterStyle.MarginBottom, "150px");
                    phReOpen.Controls.Add(lblReOpen_EMPTY);


                    phReOpen.Controls.Add(new LiteralControl("<br />"));

                    //Pridani labelu který je pred DropDown jak oznečení
                    lblReOpenCisloInventury = new Label();
                    lblReOpenCisloInventury.Style.Add(HtmlTextWriterStyle.MarginLeft, "200px");
                    lblReOpenCisloInventury.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
                    lblReOpenCisloInventury.Text = "Inventura: ";
                    phReOpen.Controls.Add(lblReOpenCisloInventury);
                    lblReOpenCisloInventury.ID = "lblReOpenCisloInventury";

                    lblReOpenIDsInv = new DropDownList();
                    phReOpen.Controls.Add(lblReOpenIDsInv);
                    lblReOpenIDsInv.ID = "lblReOpenIDsInv";


                    phReOpen.Controls.Add(new LiteralControl("<br />"));

                    btnReOpenAction = new Button();
                    btnReOpenAction.Style.Add(HtmlTextWriterStyle.MarginLeft, "200px");
                    btnReOpenAction.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
                    btnReOpenAction.OnClientClick = "if (! confirm('Opravdu chcete znovu otevřít inventuru?')) return false;";
                    btnReOpenAction.Click += new EventHandler(btnReOpen_Click);
                    btnReOpenAction.Text = "Otevřít inventuru";
                    phReOpen.Controls.Add(btnReOpenAction);
                    btnReOpenAction.ID = "btnReOpenAction";


                    phReOpen.Controls.Add(new LiteralControl("<br />"));
                    phReOpen.Controls.Add(new LiteralControl("<br />"));

                    lblReOpenText = new Label();
                    lblReOpenText.ID = "lblReOpenText";
                    //lblReOpenText.Text = " bla bla bla bla  bla bla bla bla  bla bla bla bla ";
                    phReOpen.Controls.Add(lblReOpenText);

                }
                //else
                //{
                //    phReOpen.Page = page;

                //    btnReOpenAction.Click -= new EventHandler(btnReOpen_Click);
                //    btnReOpenAction.Click += new EventHandler(btnReOpen_Click);
                //}

                createDropDownListInventur_ReOpen();

                return phReOpen;

                #endregion
            }
            else if (selectedAction.Value == INT_ExportovatPouzeJednu)
            {
                #region Export jednej inventury

                if (phExportOne == null)
                {
                    //Vytvořeni objektu placeHolder
                    phExportOne = new PlaceHolder();

                    phExportOne.Page = page;

                    var lblExportOne_EMPTY = new Label();
                    lblExportOne_EMPTY.Style.Add(HtmlTextWriterStyle.MarginBottom, "150px");
                    phExportOne.Controls.Add(lblExportOne_EMPTY);


                    phExportOne.Controls.Add(new LiteralControl("<br />"));

                    //Pridani labelu který je pred DropDown jak oznečení
                    lblExportOneCisloInventury = new Label();
                    lblExportOneCisloInventury.Style.Add(HtmlTextWriterStyle.MarginLeft, "200px");
                    lblExportOneCisloInventury.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
                    lblExportOneCisloInventury.Text = "Inventura: ";
                    phExportOne.Controls.Add(lblExportOneCisloInventury);
                    lblExportOneCisloInventury.ID = "lblExportOneCisloInventury";

                    lblExportOneIDsInv = new DropDownList();
                    phExportOne.Controls.Add(lblExportOneIDsInv);
                    lblExportOneIDsInv.ID = "lblExportOneIDsInv";


                    phExportOne.Controls.Add(new LiteralControl("<br />"));

                    btnExportOneAction = new Button();
                    btnExportOneAction.Style.Add(HtmlTextWriterStyle.MarginLeft, "200px");
                    btnExportOneAction.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
                    //btnExportOneAction.OnClientClick = "if (! confirm('Opravdu chcete exportovat inventuru?')) return false;";
                    btnExportOneAction.Click += new EventHandler(btnExportOne_Click);
                    btnExportOneAction.Text = "Exportovat inventuru";
                    phExportOne.Controls.Add(btnExportOneAction);
                    btnExportOneAction.ID = "btnExportOneAction";


                    phExportOne.Controls.Add(new LiteralControl("<br />"));
                    phExportOne.Controls.Add(new LiteralControl("<br />"));

                    lblExportOneText = new Label();
                    lblExportOneText.ID = "lblExportOneText";
                    //lblReOpenText.Text = " bla bla bla bla  bla bla bla bla  bla bla bla bla ";
                    phExportOne.Controls.Add(lblExportOneText);

                    phExportOne.Controls.Add(new LiteralControl("<br />"));

                    btnExportOne_Ano = new Button();
                    btnExportOne_Ano.Style.Add(HtmlTextWriterStyle.MarginLeft, "200px");
                    btnExportOne_Ano.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
                    btnExportOne_Ano.Style.Add(HtmlTextWriterStyle.Height, "50px");
                    btnExportOne_Ano.Style.Add(HtmlTextWriterStyle.Width, "100px");
                    btnExportOne_Ano.Click += new EventHandler(btnExportOne_Ano_Click);
                    btnExportOne_Ano.Text = "Ano";
                    phExportOne.Controls.Add(btnExportOne_Ano);
                    btnExportOne_Ano.ID = "btnA";
                    btnExportOne_Ano.Visible = false;


                    btnExportOne_Ne = new Button();
                    btnExportOne_Ne.Style.Add(HtmlTextWriterStyle.MarginLeft, "10px");
                    btnExportOne_Ne.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
                    btnExportOne_Ne.Style.Add(HtmlTextWriterStyle.Height, "50px");
                    btnExportOne_Ne.Style.Add(HtmlTextWriterStyle.Width, "100px");
                    btnExportOne_Ne.Click += new EventHandler(btnExportOne_NE_Click);
                    btnExportOne_Ne.Text = "Ne";
                    phExportOne.Controls.Add(btnExportOne_Ne);
                    btnExportOne_Ne.ID = "btnN";
                    btnExportOne_Ne.Visible = false;

                }
                //else
                //{
                //    phExportOne.Page = page;
                //    btnExportOneAction.Click -= new EventHandler(btnExportOne_Click);
                //    btnExportOneAction.Click += new EventHandler(btnExportOne_Click);
                //}

                createDropDownListInventur_inABRA();

                return phExportOne;

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
                tn.Value = INV_Export;
                tn.Text = "Export inventury - ABRA";
                actions.Add(tn);

                TreeNode tn1 = new TreeNode();
                tn1.Value = INT_Ukonceni;
                tn1.Text = "Ukončit inventuru - ABRA";
                actions.Add(tn1);


                TreeNode tn3 = new TreeNode();
                tn3.Value = INT_Prehled;
                tn3.Text = "Přehled zpracovaných dat";
                actions.Add(tn3);

                TreeNode tn4 = new TreeNode();
                tn4.Value = INT_NaplnLokMech;
                tn4.Text = "Naplnit lokační mechanismus";
                actions.Add(tn4);

                TreeNode tn5 = new TreeNode();
                tn5.Value = INT_ZnovuOtevreni;
                tn5.Text = "Otevření ukončené inventury";
                actions.Add(tn5);

                TreeNode tn6 = new TreeNode();
                tn6.Value = INT_ExportovatPouzeJednu;
                tn6.Text = "Export pro jednu inventuru - ABRA";
                actions.Add(tn6);

            }

            return actions;
        }

        #region Private methods for IWebControl

        private void KillAll()
        {
            if (phExport != null)
            {
                phExport.Dispose();
                phExport = null;
            }

            if (phUkoncit != null)
            {
                phUkoncit.Dispose();
                phUkoncit = null;
            }

            if (phPrehled != null)
            {
                phPrehled.Dispose();
                phPrehled = null;
            }

            if (phExportLokace != null)
            {
                phExportLokace.Dispose();
                phExportLokace = null;
            }

            if (phReOpen != null)
            {
                phReOpen.Dispose();
                phReOpen = null;
            }

            if (phExportOne != null)
            {
                ClearExportOne();
                phExportOne.Dispose();
                phExportOne = null;
            }

        }

        #region Button eventy

        private void btnExportOne_Click(object sender, EventArgs e)
        {

            //lblExportOneIDsInv.Enabled = false;
            //btnExportOneAction.Visible = false;

            //lblExportOneText.Text = " bla bla bla bla  bla bla bla bla  bla bla bla bla ";
            ////lblExportOneText.Text = "Really?";
            //btnExportOne_Ano.Visible = true;
            //btnExportOne_Ne.Visible = true;

            string DIP = lblExportOneIDsInv.SelectedValue;

            Fask.Module.ABRA.SAB.Classes.Export_DIP Edip = new Export_DIP()
            {
                DIP = DIP
            };

            if(sender != null && sender is System.Web.UI.Control)
            {
                (sender as System.Web.UI.Control).Page.Session[Constants.Common.ExportSession_DIP_Text] = Edip;
            }


            Database.Inventura i = new Database.Inventura();

            var RowTerminalID = i.Get_TerminalID_I1(DIP);
            
         
            if (RowTerminalID != null)
            {
                if (RowTerminalID.TerminalID == 200)
                {
                    lblExportOneText.Text = "6. Dávka byla stornována! Chcete předlohu znovu vygenerovat?";
                    lblExportOneIDsInv.Enabled = false;
                    btnExportOneAction.Visible = false;
                    btnExportOne_Ne.Visible = true;
                    btnExportOne_Ano.Visible = true;
                }
                else if (RowTerminalID.TerminalID == 0)
                {
                    lblExportOneText.Text = "1.	Dávka byla již vygenerovaná! Chcete předlohu přegenerovat do nové dávky? Původní dávka bude stornována a nebude možné původní dávku stáhnout do terminálu! ";
                    lblExportOneIDsInv.Enabled = false;
                    btnExportOneAction.Visible = false;
                    btnExportOne_Ne.Visible = true;
                    btnExportOne_Ano.Visible = true;
                }
                else if (RowTerminalID.TerminalID > 100)
                {
                    lblExportOneText.Text = "4.	Data již byla odeslaná z web rozhraní do IS ABRA! </p>Přejete si předlohu znovu vygenerovat?";
                    lblExportOneIDsInv.Enabled = false;
                    btnExportOneAction.Visible = false;
                    btnExportOne_Ne.Visible = true;
                    btnExportOne_Ano.Visible = true;
                }                
                else if (RowTerminalID.TerminalID > 0)
                {

                    int? cntrow = i.GetCountRowI4(DIP, RowTerminalID.CountEntries);

                    if (cntrow.HasValue)
                    {
                        lblExportOneText.Text = "3.	Data již byla odeslána z terminálu na server k dalšímu zpracování! Pokud zvolíte ANO stornuje se původní dávka s nasnímanýma hodnotama a vytvoří se nová!";
                        lblExportOneIDsInv.Enabled = false;
                        btnExportOneAction.Visible = false;
                        btnExportOne_Ano.Visible = true;
                        btnExportOne_Ne.Visible = true;

                    }
                    else
                    {
                        lblExportOneText.Text = "2. Předloha byla již stažena do terminálu! Není možné předlohu vygenerovat znovu! Číslo terminálu:'" + RowTerminalID.TerminalID.ToString()+"'";
                        lblExportOneIDsInv.Enabled = false;
                        btnExportOneAction.Visible = false;
                        btnExportOne_Ne.Visible = true;
                        btnExportOne_Ne.Text = "OK";
                    }
                }
                else
                {
                    string msg = "Neznámy stav.";
                    msg += "ID Terminalu: " +  (!RowTerminalID.IsTerminalIDNull() ? RowTerminalID.TerminalID.ToString() : " je null (toaletný papir bez ruličky)") ; 

                    lblExportOneText.Text = msg;
                    lblExportOneIDsInv.Enabled = false;
                    btnExportOneAction.Visible = false;
                    btnExportOne_Ne.Visible = true;
                    btnExportOne_Ne.Text = "OK";
                }
            }
            else
            {
                lblExportOneText.Text = "5.Vygenerovat předlohu?";
                lblExportOneIDsInv.Enabled = false;
                btnExportOneAction.Visible = false;
                btnExportOne_Ne.Visible = true;
                btnExportOne_Ano.Visible = true;
            }

        }

        private void btnExportOne_Ano_Click(object sender, EventArgs e)
        {

            StatusObject so = null;
            try
            {
                Globals_V1.LoadConfiguration();

                string DIP = null;

                if (sender != null && sender is System.Web.UI.Control)
                {
                    var eDip_O = (sender as System.Web.UI.Control).Page.Session[Constants.Common.ExportSession_DIP_Text];

                    if (eDip_O != null && eDip_O is Fask.Module.ABRA.SAB.Classes.Export_DIP)
                    {
                        var eDip = eDip_O as Fask.Module.ABRA.SAB.Classes.Export_DIP;
                        DIP = eDip.DIP;
                    }
                }

                if (DIP is null)
                    throw new Exception("Neni vybrán DIP pro export!");


                Database.Inventura i = new Database.Inventura();
                var RowTerminalID = i.Get_TerminalID_I1(DIP);
               

                //šilenost:
                // 1. musí byt davka u nás => TerminalID.HasValue
                // 2. Prva varianta, že je davka ešte nestažena a nezpracovana,  => TerminalID.Value == 0
                // 3. druha varianta : 
                // TerminalID.Value > 0 dávka je odeslana z terminalu, takže se ví jeho ID
                // &&
                // TerminalID.Value < 100  dávka ale ešte neni ukončena
                // &&
                // cnt.HasValue d8vka má nejake nasnimane řadky
                // &&
                // cnt.Value > 0 dávka má víc jak 0 nasmimanych řadku





                if (RowTerminalID != null)
                {
                    int? cnt = i.GetCountRowI4(DIP, RowTerminalID.CountEntries);

                    if (!RowTerminalID.IsTerminalIDNull() && (RowTerminalID.TerminalID == 0 || ((RowTerminalID.TerminalID > 0 && RowTerminalID.TerminalID < 100 && cnt.HasValue && cnt.Value > 0))))
                    {
                        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                        var updateuvolnit = "UPDATE " + Constants.Common.TABLE_CZMST_I1 + " SET TerminalID=200 WHERE CountEntries=" + RowTerminalID.CountEntries.ToString();
                        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(updateuvolnit, conn);
                        conn.Open();
                        int rows = comm.ExecuteNonQuery();
                        conn.Close();
                    } 
                }

                string pom = (new Uri(System.IO.Path.Combine(MyPath.Path.LogsDirectory,
                    Globals_V1.Konfigurace.Inventura1[0].PathStateDataFile))).LocalPath;

                string filePath = System.IO.Path.Combine(pom, "Inventura1ExportOne.so");

                so = new StatusObject(filePath);
                if (so.Exists)
                {
                    lblExportOneText.Text = "Export inventury se nezdařil - jiz probiha";
                    return;
                }

                so.Write("probiha export");
                bool succes = false;
                int CountEntries = 0;

                succes = Classes.ABRA.LoadInventura(DIP, out CountEntries);

                if (succes)
                {
                    lblExportOneText.Text = "Dílčí inventarný protokol " + DIP + " byl exportován do dávky číslo: " + CountEntries.ToString();
                }
                else
                    lblExportOneText.Text = "Export inventury se nezdařil";

                so.Write("export ukoncen");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                lblExportOneText.Text = ex.Message;
            }
            finally
            {
                if (so != null)
                    so.Delete();

                btnExportOne_Ne.Visible = false;
                btnExportOne_Ano.Visible = false;
                btnExportOneAction.Visible = true;
                btnExportOneAction.Enabled = true;
                lblExportOneIDsInv.Enabled = true;
            }

        }

        private void btnExportOne_NE_Click(object sender, EventArgs e)
        {
            try
            {

                lblExportOneIDsInv.Enabled = true;
                btnExportOneAction.Visible = true;

                lblExportOneText.Text = "";
                btnExportOne_Ano.Visible = false;
                btnExportOne_Ne.Visible = false;
                btnExportOne_Ne.Text = "Ne";
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void btnExportAction_Click(object sender, EventArgs e)
        {
            StatusObject so = null;
            try
            {

                string pom = (new Uri(System.IO.Path.Combine(MyPath.Path.LogsDirectory,
                    Globals_V1.Konfigurace.Inventura1[0].PathStateDataFile))).LocalPath;

                string filePath = System.IO.Path.Combine(pom, "Inventura1Export.so");

                so = new StatusObject(filePath);
                if (so.Exists)
                {
                    lblExportText.Text = "Export inventury se nezdařil - jiz probiha";
                    return;
                }

                so.Write("probiha export");
                bool succes = false;
                int PocetInventur = 0;

                succes = Classes.ABRA.LoadInventura(out PocetInventur);

                if (succes)
                {
                    if (PocetInventur == 0)
                        lblExportText.Text = "žádná inventura nebyla exportována";
                    else
                        lblExportText.Text = "Počet exportovaných dílčích inventarných protokolů :" + PocetInventur.ToString();

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

                string DIP_CountEntries = lblUkoncitIDsInv.SelectedValue;

                if (string.IsNullOrEmpty(DIP_CountEntries))
                {
                    lblUkoncitText.Text = "Ooops! Snaha se cení, ale musíte mít vybranou inventuru!";
                    return;
                }


                TracId tracid = new TracId(null, null, null, "btnEndInventura_Click");
                Trac.Write("Start END inventura", tracid);

                    //string statusFile = (new Uri(System.IO.Path.Combine(
                    //    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
                    //    @"..\ExportImport\Inventura1Ukonceni.so"))).LocalPath;

                    string pom = (new Uri(System.IO.Path.Combine(MyPath.Path.LogsDirectory,
                Globals_V1.Konfigurace.Inventura1[0].PathStateDataFile))).LocalPath;

                string filePath = System.IO.Path.Combine(pom, "Inventura1Ukonceni.so");

                so = new StatusObject(filePath);
                if (so.Exists)
                {
                    lblUkoncitText.Text = "Ukončení inventury se nezdařilo - jiz probiha";
                    return;
                }

                // nacteni poctu neuzavrenych zaznamu inventury
                string selectTermID = "select COUNT(*) from " + Constants.Common.TABLE_CZMST_I1 + " where TerminalID<100 and CountEntries=" + DIP_CountEntries;
                
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

                Trac.Write("probiha export", tracid);
                so.Write("probiha export");
                bool succes = false;


                string updatei1 = "Update " + Constants.Common.TABLE_CZMST_I1 + " set TerminalID= TerminalID + 100 where CountEntries=" + DIP_CountEntries;

                Trac.Write("Start ImportInventura", tracid);
                succes = Classes.ABRA.ImportInventura(int.Parse(DIP_CountEntries));
                Trac.Write("Stop ImportInventura", tracid);

                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatei1, sqlcon);

                int rows = command.ExecuteNonQuery();

                if (succes)
                    lblUkoncitText.Text = "Inventura úspěšně importována";
                else
                    lblUkoncitText.Text = "Import inventury se nezdařil";

                Trac.Write("Stop END inventura", tracid);
            }
            catch (Exception ex)
            {
                lblUkoncitText.Text = ex.Message;
                //throw ex;
            }
            finally
            {
                if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();

                if (so != null)
                    so.Delete();

            }
        }

        private void btnReOpen_Click(object sender, EventArgs e)
        {
            //TODO Implementovat
            SqlConnection sqlcon = null;
            StatusObject so = null;

            try
            {
                //string statusFile = (new Uri(System.IO.Path.Combine(
                //    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
                //    @"..\ExportImport\Inventura1ReOpen.so"))).LocalPath;

                string pom = (new Uri(System.IO.Path.Combine(MyPath.Path.LogsDirectory,
                Globals_V1.Konfigurace.Inventura1[0].PathStateDataFile))).LocalPath;

                string filePath = System.IO.Path.Combine(pom, "Inventura1ReOpen.so");


                so = new StatusObject(filePath);
                if (so.Exists)
                {
                    lblReOpenText.Text = "Otevření inventury se nezdařilo - jiz probíhá.";
                    return;
                }

                // nacteni poctu uzavrenych zaznamu inventury
                
                
                Globals_V1.LoadConfiguration();

                string selectCount = "select COUNT(*) from " + Constants.Common.TABLE_CZMST_I1 + " where TerminalID>100 and CountEntries=" + lblReOpenIDsInv.SelectedValue;
                sqlcon = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                sqlcon.Open();

                // kontrola, zdali jiz byla inventura uzavrena
                System.Data.SqlClient.SqlCommand selectCommand = new System.Data.SqlClient.SqlCommand(selectCount, sqlcon);
                int count = (int)selectCommand.ExecuteScalar();

                if (count == 0)
                {
                    lblReOpenText.Text = "Inventura již byla otevřena!!";
                    return;
                }

                so.Write("probíha ReOpen");

                string TerminalID = string.Empty;

                string selectTermID = "SELECT distinct ID_TERMINAL FROM " + Constants.Common.TABLE_CZMST_I4 + " where CountEntries=" + lblReOpenIDsInv.SelectedValue;
                System.Data.SqlClient.SqlCommand scom = new System.Data.SqlClient.SqlCommand(selectTermID, sqlcon);
                TerminalID = scom.ExecuteScalar().ToString();

                string updatei1 = "Update " + Constants.Common.TABLE_CZMST_I1 + " set TerminalID = " + TerminalID + " where CountEntries=" + lblReOpenIDsInv.SelectedValue;
                
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatei1, sqlcon);

                int rows = command.ExecuteNonQuery();

                if (rows > 0)
                {
                    string msg = string.Format("Inventura úspěšně otevřena pro '{0}' řádků a pro ID Terminálu '{1}'.", rows, TerminalID);

                    lblReOpenText.Text = msg;
                }
                else
                {
                    lblReOpenText.Text = "Otevření inventury se nezdařilo";
                }

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
                    FillReportDataSet(null, report, lblPrehledIDsInv.SelectedValue);
                }
                //string s = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

                //string lStyleSheetPath = (new Uri(System.IO.Path.Combine(Globals_V1.Konfigurace.Inventura1[0].PathToXSLT, "TiskovaSestavaInventura1.xslt"))).LocalPath;

                //string lXmlPath = (new Uri(System.IO.Path.Combine(Globals_V1.Konfigurace.Inventura1[0].PathDataOutputFile, "Inventura1TiskovaSestava" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml"))).LocalPath;

                //string lOutputPath = (new Uri(System.IO.Path.Combine(Globals_V1.Konfigurace.Inventura1[0].PathDataOutputFile, "Inventura1TiskovaSestava" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html"))).LocalPath;

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


        /// <summary>
        /// TODO lblPrehledIDsInv.SelectedValue tohle je špatne, obdobne jak pri exportu jednej využit session pro prepinani...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrehled_Click(object sender, EventArgs e)
        {
            if (phPrehled != null)
            {
                reportInventura1 = new SQL_Datasets.ReportInventura1();

                string DIP = lblPrehledIDsInv.SelectedValue;

                Fask.Module.ABRA.SAB.Classes.Prehled_DIP Edip = new Prehled_DIP()
                {
                    DIP = DIP
                };

                if (sender != null && sender is System.Web.UI.Control)
                {
                    (sender as System.Web.UI.Control).Page.Session[Constants.Common.PrehledSession_DIP_Text] = Edip;
                }

                FillReportDataSet(null, reportInventura1, DIP);
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

                FillReportDataSet(null, report, lblPrehledIDsInv.SelectedValue);
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

        private string Get_PrehledCountEntries(object sender)
        {
            string DIP = null;

            try
            {

                if (sender != null && sender is System.Web.UI.Control)
                {
                    var eDip_O = (sender as System.Web.UI.Control).Page.Session[Constants.Common.PrehledSession_DIP_Text];

                    if (eDip_O != null && eDip_O is Fask.Module.ABRA.SAB.Classes.Prehled_DIP)
                    {
                        var eDip = eDip_O as Fask.Module.ABRA.SAB.Classes.Prehled_DIP;
                        DIP = eDip.DIP;
                    }
                }

                if (DIP is null)
                    throw new Exception("Neni vybrán DIP pro přehled!");

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }

            return DIP;
        }

        private void btnExportLokaceAction_Click(object sender, EventArgs e)
        {
            lblExportLokaceText.Text = "Máme už ten lokační mechanizmus od docenta Šmoka, pane vedoucí ?" +
                " Nemáme, nevedeme. Zeptejte se příští týden, máme dostat zboží.";
            return;

            #region TaD 17.10.2022 Zakomentovano, nachystano kdyby SAB chtelo lok.mech. 

            //SqlConnection conn = null;
            //System.Data.SqlClient.SqlCommand insertCommand = null;
            //SqlCommand countCommand = null;
            //SqlTransaction trans = null;

            //try
            //{
            //    lblExportLokaceText.Text = string.Empty;

            //    conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //    conn.Open();
            //    trans = conn.BeginTransaction(IsolationLevel.Serializable);

            //    string countcmdtext = "select COUNT(*) from " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " ;";
            //    countCommand = new SqlCommand(countcmdtext, conn, trans);
            //    int rowcount = (int)countCommand.ExecuteScalar();
            //    if (rowcount > 0)
            //    {
            //        throw new Exception("Tabulka " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV + " již obsahuje '" + rowcount + "' záznamů. Záznamy z inventury nebudou přidány.");
            //    }

            //    string insertcmdtext = "INSERT INTO " + Constants.Common.TABLE_CZMST_SKLADLOKACE_STAV +
            //   "([ITEMNMBR], " +
            //   "[ITEMDESC], " +
            //   "[QTYSHPPD_DEF], " +
            //   "[QTYSHPPD], " +
            //   "[SERLTNUM], " +
            //   "[SKL_ID], " +
            //   "[LOCNCODE], " +
            //   "[DATECHANGE], " +
            //   "[EXPIRATION], " +
            //   "[QTYSHPPD_DEF_DATE]) " +
            //   "select " +
            //   "i4.ITEMNMBR as ITEMNMBR, " +
            //   "ISNULL(i1.ITEMDESC, '') as ITEMDESC, " +
            //   "SUM(i4.QUANTITY) as QTYSHPPD_DEF, " +
            //   "SUM(i4.QUANTITY) as QTYSHPPD, " +
            //   "ISNULL(i4.SERLNMBR, '') as SERLTNUM, " +
            //   "ISNULL(i4.skl_id, '') as SKL_ID, " +
            //   "ISNULL(i4.LOCNCODE, '') as LOCNCODE, " +
            //   "GETDATE() as DATECHANGE, " +
            //   "null as EXPIRATION, " +
            //   "GETDATE() as QTYSHPPD_DEF_DATE " +
            //   "from " + Constants.Common.TABLE_CZMST_I4 + " i4 " +
            //   "left join " + Constants.Common.TABLE_CZMST_I1 + " i1 on i1.ITEMNMBR = i4.ITEMNMBR " +
            //   "group by i4.ITEMNMBR, i1.ITEMDESC, i4.SERLNMBR, i4.skl_id, i4.LOCNCODE;"
            //   ;

            //    insertCommand = new System.Data.SqlClient.SqlCommand(insertcmdtext, conn, trans);

            //    int rows = insertCommand.ExecuteNonQuery();

            //    if (trans != null)
            //        trans.Commit();

            //    lblExportLokaceText.Text = "Lokační mechanismus úspešně naplněn inventurními daty dne " + DateTime.Now + ".<br />Do lokačního mechanismu bylo přidáno '" + rows + "' nových záznamů.";
            //}
            //catch (Exception ex)
            //{
            //    try
            //    {
            //        // chyba, rollback transakce ...
            //        if (trans != null)
            //            trans.Rollback();
            //    }
            //    catch { }
            //    lblExportLokaceText.Text = "Chyba při exportu dat do lokačního mechanismu: " + ex.Message;
            //}
            //finally
            //{
            //    if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
            //        conn.Close();

            //} 

            #endregion
        }

        #endregion

        #region createDropDownList

        private void createDropDownListInventur_inABRA()
        {

            string Desc = "Desc";
            //string CE = "CountEntries";

            Fask.DataSets.Inventury1 inv = Database.ABRA.SeznamInventurProExport();

            if (lblExportOneIDsInv != null)
                lblExportOneIDsInv.Items.Clear();

            if (lblExportOneIDsInv != null)
            {
                lblExportOneIDsInv.DataSource = inv.Hlavicky_Seznam;
                lblExportOneIDsInv.DataTextField = Desc;
                lblExportOneIDsInv.DataValueField = Desc;
                lblExportOneIDsInv.DataBind();
            }
        }

        private void createDropDownListInventur_ReOpen()
        {

            string Desc = "Desc";
            string CE = "CountEntries";

            Database.Inventura i = new Database.Inventura();

            Fask.DataSets.Inventury1 inv = i.GetInventuryList_ReOpen();

            if (lblReOpenIDsInv != null)
                lblReOpenIDsInv.Items.Clear();

            if (lblReOpenIDsInv != null)
            {
                lblReOpenIDsInv.DataSource = inv.Hlavicky_Seznam;
                lblReOpenIDsInv.DataTextField = Desc;
                lblReOpenIDsInv.DataValueField = CE;
                lblReOpenIDsInv.DataBind();
            }

        }

        private void createDropDownListInventur_Prehled()
        {

            string Desc = "Desc";
            string CE = "CountEntries";

            Database.Inventura i = new Database.Inventura();

            Fask.DataSets.Inventury1 inv = i.GetInventuryList_Prehled();
            if (lblPrehledIDsInv != null)
                lblPrehledIDsInv.Items.Clear();

            if (lblPrehledIDsInv != null)
            {
                lblPrehledIDsInv.DataSource = inv.Hlavicky_Seznam;
                lblPrehledIDsInv.DataTextField = Desc;
                lblPrehledIDsInv.DataValueField = CE;
                lblPrehledIDsInv.DataBind();
            }
        }

        private void createDropDownListInventur_Ukoncit()
        {

            string Desc = "Desc";
            string CE = "CountEntries";

            Database.Inventura i = new Database.Inventura();

            Fask.DataSets.Inventury1 inv = i.GetInventuryList_Ukoncit();

            if (lblUkoncitIDsInv != null)
                lblUkoncitIDsInv.Items.Clear();

            if (lblUkoncitIDsInv != null)
            {
                lblUkoncitIDsInv.DataSource = inv.Hlavicky_Seznam;
                lblUkoncitIDsInv.DataTextField = Desc;
                lblUkoncitIDsInv.DataValueField = CE;
                lblUkoncitIDsInv.DataBind();
            }
        }

        #endregion

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
                SortGridView(sender, sortExpression, DESCENDING, sender);
            }
            else
            {
                LblHidePomocna.Text = ASCENDING;
                SortGridView(sender, sortExpression, ASCENDING, sender);
            }
        }

        private void SortGridView(object sender, string sortExpression, string direction, object source)
        {

            if (reportInventura1 == null)
                FillReportDataSet(sender, reportInventura1, Get_PrehledCountEntries(sender));

            DataTable dt = reportInventura1.Tables[0];

            DataView dv = new DataView(dt);
            dv.Sort = sortExpression + direction;

            gvInventura.DataSource = dv;
            gvInventura.DataBind();
        }

        private void FillReportDataSet(object sender, SQL_Datasets.ReportInventura1 report, string idInv)
        {

            //if (string.IsNullOrEmpty(idInv))
            //{
            //    if (sender != null && sender is System.Web.UI.Control)
            //    {
            //        var eDip_O = (sender as System.Web.UI.Control).Page.Session[Constants.Common.PrehledSession_DIP_Text];

            //        if (eDip_O != null && eDip_O is Fask.Module.ABRA.SAB.Classes.Export_DIP)
            //        {
            //            var eDip = eDip_O as Fask.Module.ABRA.SAB.Classes.Export_DIP;
            //            idInv = eDip.DIP;
            //        }
            //    }
            //    else
            //        return;

            //    if (idInv is null)
            //        throw new Exception("Neni vybrán DIP pro export!");
            //}

            if (string.IsNullOrEmpty(idInv))
                return;

            SQL_Datasets.Inventura.CZMST_I1DataTable i1 = null;
            SQL_Datasets.Inventura.CZMST_I4DataTable i4 = null;
            SQL_Datasets.Inventura.CZMST093DataTable czmst093 = null;

            int id = int.Parse(idInv);

            Database.Inventura inv = new Database.Inventura();
            i1 = inv.GetDataByCountEntries_I1(id/*lbIDsInv.SelectedValue*/);
            i4 = inv.GetDataByCountEntries_I4(id/*lbIDsInv.SelectedValue*/);
            czmst093 = inv.GetData_CZMST093();

            if (i1 != null && i1.Count > 0)
            {
                byte TID = i1.First().TerminalID;
                string state = "";

                if (TID == 0)
                    state = "Nová";
                else if (TID == 200)
                    state = "Stornována";
                else if (TID > 100)
                    state = "Zpracována";
                else if (TID < 100)
                    state = "Stažený v terminalu";

                lblStatusPrehledDat.Text = string.Format("Status inventury je :'" + state + "' TID:'{0}'", TID);
            }


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

        private void initPrehledPlaceHolder(Page page)
        {
            var lblPrehled_EMPTY = new Label();
            lblPrehled_EMPTY.Style.Add(HtmlTextWriterStyle.MarginBottom, "150px");
            phPrehled.Controls.Add(lblPrehled_EMPTY);


            phPrehled.Controls.Add(new LiteralControl("<br />"));

            lblPrehledText = new Label();
            lblPrehledText.Text = "Inventura: ";
            lblPrehledText.Style.Add(HtmlTextWriterStyle.MarginLeft, "200px");
            lblPrehledText.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
            phPrehled.Controls.Add(lblPrehledText);
            lblPrehledText.ID = "lblPrehledText";

            lblPrehledIDsInv = new DropDownList();
            lblPrehledIDsInv.ID = "lblPrehledIDsInv";

            phPrehled.Controls.Add(lblPrehledIDsInv);

            phPrehled.Controls.Add(new LiteralControl("<br />"));

            phPrehled.Controls.Add(new LiteralControl("<br />"));

            btnPrehledAction = new Button();
            btnPrehledAction.Text = "Vygenerovat přehled dat";
            btnPrehledAction.Style.Add(HtmlTextWriterStyle.MarginLeft, "200px");
            btnPrehledAction.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
            btnPrehledAction.Click += new EventHandler(btnPrehled_Click);
            phPrehled.Controls.Add(btnPrehledAction);
            btnPrehledAction.ID = "btnPrehledAction";

            phPrehled.Controls.Add(new LiteralControl("<br />"));

            btnExportDat = new Button();
            btnExportDat.Text = "Export data CSV";
            btnExportDat.Style.Add(HtmlTextWriterStyle.MarginLeft, "200px");
            btnExportDat.Style.Add(HtmlTextWriterStyle.MarginTop, "10px");
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


            phPrehled.Controls.Add(new LiteralControl("<br />"));

            lblStatusPrehledDat = new Label();
            lblStatusPrehledDat.Text = "";
            phPrehled.Controls.Add(lblStatusPrehledDat);
            lblStatusPrehledDat.ID = "lblStatusPrehledDat";

            phPrehled.Controls.Add(new LiteralControl("<br />"));

            lblPrehledNote = new Label();
            lblPrehledNote.Text = "<p>Legenda barev:</p>" +
                "<ul>" +
                "<li >" +
                    "<b style=\"-webkit-text-stroke:1px gray; \">bílá/šedá</b>" + "-položka nebyla čtečkou inventarizovaná(například: položka nebyla nalezena) " +
                "</li>" +
                "<li>" +
                    "<b style=\"color: green; -webkit-text-stroke:1px gray; \">zelená</b>" + "-evidovaný stav se rovná skutečnému stavu " +
                "</li>" +
                "<li>" +
                    "<b style=\"color: red; -webkit-text-stroke:1px gray; \">červená</b>" + "-skutečného stavu je méně než evidovaného " +
                "</li>" +
                "<li>" +
                    "<b style=\"color: yellow; -webkit-text-stroke: 1px gray; \">žlutá</b>" + "-skutečného stavu je více než evidovaného " +
                "</li>" +
                "</ul>";
            phPrehled.Controls.Add(lblPrehledNote);
            lblPrehledNote.ID = "lblPrehledNote";

            phPrehled.Controls.Add(new LiteralControl("<br />"));

            reportInventura1 = new SQL_Datasets.ReportInventura1();

            string idInv = null;

            if (page != null )
            {
                var eDip_O = page.Session[Constants.Common.PrehledSession_DIP_Text];

                if (eDip_O != null && eDip_O is Fask.Module.ABRA.SAB.Classes.Prehled_DIP)
                {
                    var eDip = eDip_O as Fask.Module.ABRA.SAB.Classes.Prehled_DIP;
                    idInv = eDip.DIP;
                }
            }
            else
                return;

            if (idInv is null)
                idInv = lblPrehledIDsInv.SelectedValue;


            FillReportDataSet(null, reportInventura1, idInv);

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

        #region IInventura1

        #region Implementovane, a neco delaji...

        public Inventura1 Inventura_GetInventura(Davka davka, Terminal terminal)
        {
            try
            {
                Fask.DataSets.Inventura1 inventura = new Fask.DataSets.Inventura1();

                string select1 = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I1  + " WHERE CountEntries=" + davka.ID + " ORDER BY dex_row_id";
                string select2 = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I2  + " WHERE CountEntries=" + davka.ID + " ORDER BY dex_row_id";
                string select3 = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I3  + " WHERE CountEntries=" + davka.ID + " ORDER BY dex_row_id";
                string select4 = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I4  + " WHERE CountEntries=" + davka.ID + " ORDER BY dex_row_id";
                string select5 = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I1H + " WHERE CountEntries=" + davka.ID;


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

                using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select4, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    sql.Fill(inventura, inventura.CZMST_I4.TableName);
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
            string selectCount = "SELECT Count(CountEntries) as davka FROM " + Constants.Common.TABLE_CZMST_I1 + " WHERE CountEntries=" + davka.ID + " AND (TerminalID<=0 OR TerminalID=" + terminal.ID + ")";
            string update = "UPDATE " + Constants.Common.TABLE_CZMST_I1 + " SET TerminalID=" + terminal.ID + " WHERE countentries=" + davka.ID;


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
                    select = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I1H +
                        "   WHERE State < 2" +
                        " and CountEntries in (" +
                        " SELECT distinct CountEntries FROM " + Constants.Common.TABLE_CZMST_I1 +
                        " WHERE TerminalID < 100" +
                        (!String.IsNullOrEmpty(sklad.ID) ? (" AND SKL_ID Like '" + sklad.ID + "%'") : "") + //filtr na sklad s like
                        ")" +
                        " ORDER BY CountEntries";
                }
                else
                {
                    select = "SELECT * FROM " + Constants.Common.TABLE_CZMST_I1H +
                        " WHERE State < 2" +
                        " AND CountEntries in (" +
                        " SELECT distinct CountEntries FROM " + Constants.Common.TABLE_CZMST_I1 +
                        " WHERE TerminalID <= 0 or TerminalID=" + terminal.ID +
                        (!String.IsNullOrEmpty(sklad.ID) ? (" AND SKL_ID Like '" + sklad.ID + "%'") : "") + //filtr na sklad s like
                        ") ORDER BY CountEntries";
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
                    string updateuvolnit;
                    updateuvolnit = "UPDATE " + Constants.Common.TABLE_CZMST_I1 + " SET TerminalID=0 WHERE CountEntries=" + davka.ID;
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
                        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand("SELECT Count(*) AS number FROM " + Constants.Common.TABLE_CZMST_I4 + " WHERE countentries=" + davka.ID, conn);
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
                            string guidTest = "SELECT COUNT(*) from " + Constants.Common.TABLE_CZMST_I4 + " WHERE GUID='" + inventuradata.CZMST_I4.First().GUID.ToString() + "'";
                            if (inventuradata.CZMST_I4.Count > 1)
                            {
                                guidTest += " OR GUID='" + inventuradata.CZMST_I4.Last().GUID.ToString() + "'";
                            }

                            System.Data.SqlClient.SqlCommand testguidcommand = new SqlCommand(guidTest, conn, iTrans1);
                            int guidcount = (int)testguidcommand.ExecuteScalar();

                            if (guidcount == 0)
                            {
                                // guidy nejsou v DB, je mozne ulozit data
                                //SQL_Datasets.InventuraTableAdapters.CZMST_I4TableAdapter i4ta = new SQL_Datasets.InventuraTableAdapters.CZMST_I4TableAdapter();
                                //i4ta.Connection = conn;
                                //i4ta.Transaction = iTrans1;
                                //i4ta.Update(inventuradata.CZMST_I4.Select(null, null, DataViewRowState.Added));

                                var dt = inventuradata.CZMST_I4.Select(null, null, DataViewRowState.Added);
                                Database.Inventura i = new Database.Inventura();
                                i.Update_I4(dt,conn, iTrans1 );
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

                        string updatei1 = "Update " + Constants.Common.TABLE_CZMST_I1 + " set TerminalID=" + (terminal.ID + 100) + " where CountEntries=" + davka.ID;
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

        #region Implementovane, ale nic nedelaji...

        public bool Inventura_AfterProcessedAction(Davka davka)
        {
            return true;
        }

        #endregion

        #region NEimplementovane...

        public bool Inventura_OnlineCheckState(Davka countentries, Terminal terminal, string itemnmbr, out byte o_terminalid)
        {
            throw new NotImplementedException();
        }

        public bool Inventura_OnlineUnCheckState(Davka davka, Terminal terminal, string itemnmbr, out byte o_terminalid)
        {
            throw new NotImplementedException();
        } 

        #endregion

        #endregion


    }
}
