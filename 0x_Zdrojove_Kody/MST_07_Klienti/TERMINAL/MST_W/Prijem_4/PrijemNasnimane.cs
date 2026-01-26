using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using System.IO;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemNasnimane : System.Windows.Forms.Form
    {
        public bool Deleted { get; set; }
        public Prijem_4.PrijemList FormPrijemList = null;
        
        private Fask.SQLiteDBs.DataSets.Prijem prijemDataParametry;
        private Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow prijemActual;

        //private string sqlfilename = string.Empty;

        private System.Guid df_guid = System.Guid.Empty;
        private decimal df_qtypack = 0;

        private int? countItems = 0;
        private int poradiPolozka = 1;

        public PrijemNasnimane(Fask.SQLiteDBs.DataSets.Prijem prijemDataParametry)
        {
            InitializeComponent();
            this.KeyPreview = true;

            //this.sqlfilename = System.IO.Path.Combine(Main.StorageDir, davka + "." + Main.Ext_Prijem);
            //this.sqlfilename = sqlfilename;
            this.prijemDataParametry = prijemDataParametry;

            Deleted = false;
        }
        
        private void LoadRow(int poradi)
        {
            //countItems = prijem_controller.CZMST_PI_Count_All();
            countItems = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.CountQuery_PI();

            if ((countItems ?? 0) <= 0)
            {
                SeznamJePrazdny();
                return;
            }

            //prijemActual = prijem_controller.CZMST_PI_Get_One(poradi - 1); //poradi - 1 = index
            prijemActual = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.CZMST_PI_Get_One(poradi - 1);

            updateForm();
        }

        private void updateForm()
        {
            try
            {
                df_Countentries.Data = "-";
                df_Itemnmbr.Data = "-";
                df_Itemdesc.Data = "-";
                df_Ponumber.Data = "-";
                df_Serialnumber.Data = "-";
                df_Expirace.Data = "-";
                df_Quantity.Data = "-";
                df_Locncode.Data = "-";
                df_Kodsw.Data = "-";
                df_Datvyroby.Data = "-";
                df_Rez1.Data = "-";
                df_Rez2.Data = "-";
                df_nmbrpal.Data = "-";

                // TODO : nalezeni nazvu polozky
                string itemnmbr = prijemActual.IsITEMNMBRNull() ? string.Empty : prijemActual.ITEMNMBR.Trim(); //chosenRow.ITEMNMBR;
                //string itemdesc = prijem_controller.CZMST_PE_Get_ItemDescription(itemnmbr);
                string itemdesc = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.CZMST_PE_Get_ItemDescription(itemnmbr);
                string itemcode = prijemActual.IsITEMCODENull() ? string.Empty : prijemActual.ITEMCODE.Trim();
                string ponumber = prijemActual.PONUMBER.Trim();
                string serialnumber = prijemActual.SERLTNUM.Trim();
                string expirace = prijemActual.IsExpiraceNull() ? string.Empty : prijemActual.Expirace.ToShortDateString();
                var qtyshppd = prijemActual.QTYSHPPD;
                string locncode = prijemActual.IsLOCNCODENull() ? string.Empty : prijemActual.LOCNCODE.Trim();
                string kodsw = prijemActual.IsKOD_SWNull() ? string.Empty : prijemActual.KOD_SW.Trim();
                string datvyroby = prijemActual.IsDAT_VYROBYNull() ? string.Empty : prijemActual.DAT_VYROBY.Trim();
                string rez1 = prijemActual.IsREZ_1Null() ? string.Empty : prijemActual.REZ_1.Trim();
                string rez2 = prijemActual.IsREZ_1Null() ? string.Empty : prijemActual.REZ_2.Trim();
                var guid = prijemActual.guid;
                var qtypack = prijemActual.QTYPACK;
                string mj = prijemActual.IsMJNull() ? string.Empty : prijemActual.MJ.Trim();
                string nmbrpal = prijemActual.IsNMBRPALNull() ? string.Empty : prijemActual.NMBRPAL.Trim();

                df_Itemdesc.Data = itemdesc ?? "-";
                df_Itemnmbr.Data = itemcode + " (" + itemnmbr + ")";

                df_Ponumber.Data = ponumber;
                df_Serialnumber.Data = string.IsNullOrEmpty(serialnumber) ? "N/A" : serialnumber;
                df_Expirace.Data = expirace;
                df_Quantity.Data = qtyshppd.ToString(Settings.UIFormatDesCisel);

                df_Locncode.Data = locncode;
                df_Kodsw.Data = kodsw;
                df_Datvyroby.Data = datvyroby;
                df_Rez1.Data = rez1;
                df_Rez2.Data = rez2;

                df_guid = guid;
                df_qtypack = qtypack;

                df_Countentries.Data = Prijem_4.PrijemMain.prijemInstance.globalObject.Davka; //Path.GetFileNameWithoutExtension(sqlfilename);

                statusBar1.Text = poradiPolozka.ToString() + "/" + (countItems ?? 0).ToString();

                df_MJ.Data = mj;

                df_nmbrpal.Data = nmbrpal;
            }
            catch //(Exception ex)
            {
                //MessageBoxBig.Show(ex.Message);
            }
        }

        private void SeznamJePrazdny()
        {
            MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemNasnimaneSeznamJePrazdny);
            PerformOK();
        }

        private void hlavniMenu_but_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void PrijemNasnimane_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                poradiPolozka = 1;
                LoadRow(poradiPolozka);
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
				poradiPolozka = (countItems ?? 0);
                LoadRow(poradiPolozka);
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                if (poradiPolozka > 1)
                    poradiPolozka--;
                LoadRow(poradiPolozka);
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
				if (poradiPolozka < (countItems ?? 0))
                    poradiPolozka++;
                LoadRow(poradiPolozka);
            }
            //else if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            else if ((e.KeyCode == System.Windows.Forms.Keys.Back))
            {
                // Enter
                smaz();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformOK();
                return;
            }

            updateForm();
        }

        private void smaz()
        {
            try
            {
                if (MessageBoxBig.Show(Fask.Localization.Localization.Prijem4PrijemNasnimaneOdstraneniPolozkyDotaz, Fask.Localization.Localization.Prijem4PrijemNasnimaneDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                    == DialogResult.No)
                    return;

                int countentries = prijemActual.CountEntries;
                decimal mnozstvi_polozky = prijemActual.QTYSHPPD;
                string itemnmbr = prijemActual.ITEMNMBR.Trim();
                string ponumber = prijemActual.PONUMBER.Trim();
                int ord = prijemActual.ORD;

                #region Online delete
                if (Globals.OnlinePohyby)
                {

                    // TODO : online smazani ...
                    // pridat parametr Online akce do konfigurace 
                    // musi se vratit uspech z online funkce 
                    // vytahnout data z davky, ktera se maji mazat, staci v podstate jen guid...???
                    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dt_pi = null;
                    //using (Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter _pi_ta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter())
                    //{
                    //    _pi_ta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + sqlfilename);
                    //    dt_pi = _pi_ta.GetDataByKey(countentries, ponumber, ord, itemnmbr);
                    //}
                    dt_pi = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByKey_PI(countentries, ponumber, ord, itemnmbr);

                    //priprava pro server
                    PrijemService.Prijem dtPOnline = new Fask.MST_W.PrijemService.Prijem();
                    foreach (Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow pi in dt_pi)
                    {
                        dtPOnline.CZMST_PI.ImportRow(pi);
                    }

                    PrijemService.StatusObject so = Prijem_4.PrijemMain.prijemInstance.globalObject.service_prijem.Online_Del(countentries, MST_Global.TerminalID, dtPOnline);
                    if (so.Exception || so.StatusText != "OK")
                    {
                        MessageBoxBig.Show(so.StatusText, "Online mazání dat", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                        return;
                    }
                }
                #endregion

                #region lokace
                if (!prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_POVOLITNull() && prijemDataParametry.Parametry[0].CONFIG_LOKACE_POVOLIT)
                {
                    // guid, ktery se maze
                    //Guid g = (Guid)sqlresultset["GUID"];
                    Guid g = prijemActual.guid;

                    _WebRefernces_Globals.LokaceServiceSession lokaceService = new Fask.MST_W._WebRefernces_Globals.LokaceServiceSession();
                    lokaceService.Url = MST_Global.ServerAddress + "Lokace.asmx";
                    lokaceService.Timeout = prijemDataParametry.Parametry[0].IsCONFIG_LOKACE_TIMEOUTNull() ? 20000 : prijemDataParametry.Parametry[0].CONFIG_LOKACE_TIMEOUT;

                    Cursor.Current = Cursors.WaitCursor;

                    //Volani sluzby a kontrola navratveho kodu.
                    Logging.Log.WriteAdvanced(string.Empty, string.Empty);
                    Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:P,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.P + ",Function:" + this.ToString() + ".DeleteRecordByGuid - start,guid winformat: " + g.ToString(), "LocationLog");
                    // odstraneni online zaznamu
                    Fask.MST_W.LokaceService.StatusLokace sl = lokaceService.DeleteRecordByGuid(g, Fask.MST_W.LokaceService.ModulName.PRIJEM);

                    Cursor.Current = Cursors.Default;
                    switch (sl.State)
                    {
                        case Fask.MST_W.LokaceService.States.OK:
                            break;
                        case Fask.MST_W.LokaceService.States.ERROR:
                            MessageBoxBig.Show("Nepodaøilo se odstranit záznamy lokací - nasnímané množství nebude smazáno! - " + sl.ErrorMessage, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                            return;
                        default:
                            MessageBoxBig.Show("Neoèekávaná chyba, nepodaøilo se odstranit záznamy lokací - nasnímané množství nebude smazáno!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                            return;
                    }

                    Logging.Log.WriteAdvanced("Operation:del,Mode:online,Modul:P,TypeOfRecord:" + Fask.MST_W.LokaceService.TypeOfRecord.P + ",Function:" + this.ToString() + ".DeleteRecordByGuid - end", "LocationLog");
                }
                #endregion

                // smazani dle guidu ... 
                //prijem_controller.CZMST_PI_DeleteByGuid(prijemActual.guid);
                //Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.Ta_pi.Delete(prijemActual.guid);
                Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.CZMST_PI_DeleteByGuid(prijemActual.guid);

                Deleted = true;

                FormPrijemList.UpdateMnozstviAddValue(itemnmbr, ponumber, ord, -mnozstvi_polozky);

				countItems = (countItems ?? 0);
                countItems--;
				if (poradiPolozka > countItems)
					poradiPolozka = countItems.Value;                
                LoadRow(poradiPolozka); // znovu nacteni polozky nasledujici po smazane
            }
            catch (Exception e)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(e);
                MessageBoxBig.Show(e.Message, Fask.Localization.Localization.Prijem4PrijemNasnimaneSmazaniPolozky, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally 
            {
            }
        }

        private void PrijemNasnimane_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            LoadRow(poradiPolozka);
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            smaz();
            updateForm();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void finalize()
        {
            //prijem_controller.Dispose();
        }

        private void PerformOK()
        {
            this.finalize();
            DialogResult = DialogResult.OK;
        }

        private void miTypOznaceniZmena_Click(object sender, EventArgs e)
        {

        }

        private void menuItemTisk_Click(object sender, EventArgs e)
        {
            try
            {
                //Dictionary<string, string> data = new Dictionary<string, string>();
                //data.Add("COUNTENTRIES", df_Countentries.Data.Trim());
                //data.Add("ITEMNMBR", df_Itemnmbr.Data.Trim());
                //data.Add("ITEMDESC", df_Itemdesc.Data.Trim());
                //data.Add("PONUMBER", df_Ponumber.Data.Trim());
                //data.Add("SERLTNUM", df_Serialnumber.Data.Trim());
                //data.Add("QTYSHPPD", df_Quantity.Data.Trim());
                //data.Add("LOCNCODE", df_Locncode.Data.Trim());
                //data.Add("KOD_SW", df_Kodsw.Data.Trim());
                //data.Add("DAT_VYROBY", df_Datvyroby.Data.Trim());
                //data.Add("REZ_1", df_Rez1.Data.Trim());
                //data.Add("REZ_2", df_Rez2.Data.Trim());
                //PrijemTisk.Print(data, MST_Global.PrintServerTemplateNamePrijemNasnimane);

                Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow pe = null;
                Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow pi = null;

                //using (Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter pita = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter())
                //{
                //    pita.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + this.sqlfilename);
                //    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable pidt = pita.GetDataByGuid(df_guid);
                //    pi = pidt[0];
                //}
                pi = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByGuid_PI(df_guid)[0];

                //using (Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter peta = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter())
                //{
                //    peta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + this.sqlfilename);
                //    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable pedt = peta.GetDataByKey2(pi.PONUMBER, pi.ITEMNMBR, pi.ORD, df_qtypack);
                //    pe = pedt[0];
                //}
                pe = Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.GetDataByKey2_PE(pi.PONUMBER, pi.ITEMNMBR, pi.ORD, df_qtypack)[0];

                //bool vytisteno = PrijemTisk.Print(pe, pi, MST_Global.PrintServerTemplateNamePrijemNasnimane, null);
                bool vytisteno = PrijemTisk.Print(pe, pi, PrinterFactory.PrinterModules.PrijemNasnimane, null,null, pe.CZ_SerNum_Track.ToString());
                Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "3", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "p", pi.CountEntries, pi.PONUMBER, pi.ITEMNMBR, vytisteno.ToString(), null));
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }

    }
}