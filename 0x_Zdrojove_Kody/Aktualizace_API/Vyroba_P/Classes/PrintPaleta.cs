using Fask.Aktualizace_API.Forms;
using JR.Utils.GUI.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fask.Aktualizace_API.Classes
{
    public class PrintPaleta
    {

        #region Tisk stitku
        public void PerformPaletaTisk(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow_data)
        {
            try
            {
                if (!Settings.TiskPalety)
                    return;

                while (true)
                {
                    try
                    {
                        Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable dt = new SQLiteDBs.DataSets.Vyroba.ProductionDataTable();
                        Dictionary<string, string> data = new Dictionary<string, string>();

                        //skladani caroveho kodu GS1 --START---------------------
                        //zadat key
                        //zadat value

                        string GS1_KOD_1_1D = string.Empty;
                        string GS1_KOD_1_TX = string.Empty;
                        string GS1_KOD_2_1D = string.Empty;
                        string GS1_KOD_2_TX = string.Empty;
                        string SSCC = string.Empty;
                        string SSCC_bez_nul = string.Empty;
                        string WEIGHT = string.Empty;






                        if (productionRow_data != null)
                        {
                            //------------START-DATA----------------
                            //dotahovat data SSCC a WEIGHT

                            if (!productionRow_data.IsWEIGHTNull())
                            {
                                WEIGHT = productionRow_data.WEIGHT.ToString();
                            }

                            if (!data.ContainsKey("WEIGHT"))
                                data.Add("WEIGHT", WEIGHT);

                            if (!productionRow_data.IsNMBRPALNull())
                            {
                                //SSCC a WEIGHT doplnit logiku dohledani
                                //SSCC a WEIGHT natvrdo zadano
                                SSCC = productionRow_data.NMBRPAL;
                                SSCC_bez_nul = SSCC.Substring(2);
                            }

                            if (!data.ContainsKey("SSCC"))
                                data.Add("SSCC", SSCC_bez_nul);

                            //------------END-DATA----------------

                            GS1_KOD_1_1D = "02" + productionRow_data.BarcodeP.PadLeft(14, '0') + "37" + productionRow_data.qty.ToString("0000") + "";
                            GS1_KOD_1_TX = "(02)" + productionRow_data.BarcodeP.PadLeft(14, '0') + "(37)" + productionRow_data.qty.ToString("0000") + ""; //(02) (37)
                            GS1_KOD_2_1D = SSCC; //SSCC neni v production
                            GS1_KOD_2_TX = "(00)" + SSCC_bez_nul; // SSCC neni v production
                        }




                        if (!data.ContainsKey("GS1_KOD_1_1D"))
                            data.Add("GS1_KOD_1_1D", GS1_KOD_1_1D);

                        if (!data.ContainsKey("GS1_KOD_1_TX"))
                            data.Add("GS1_KOD_1_TX", GS1_KOD_1_TX);

                        if (!data.ContainsKey("GS1_KOD_2_1D"))
                            data.Add("GS1_KOD_2_1D", GS1_KOD_2_1D);

                        if (!data.ContainsKey("GS1_KOD_2_TX"))
                            data.Add("GS1_KOD_2_TX", GS1_KOD_2_TX);

                        //skladani caroveho kodu GS1 --END---------------------

                        data.Add("SOURCE", "Automat");

                        foreach (DataColumn dcol in dt.Columns)
                        {
                            string key = dcol.ColumnName;
                            string value = productionRow_data[dcol.ColumnName].ToString();
                            if (!data.ContainsKey(key))
                                data.Add(key, value);
                        }

                        //TODO : nejake pocty dat a dalsi podrobnosti o vydejovych datech ???

                        //bool vytisteno = VydejTisk.Print(data, MST_Global.PrintServerTemplateNameVydejPalListek);
                        bool vytisteno = PrintSendToPrinterFactory(data, Settings.TiskNazevSablony, null);
                        // Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "3", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "v", listPolozekVydej.ListPolozek[0].CountEntries, listPolozekVydej.ListPolozek[0].SOPNUMBE, null, vytisteno.ToString(), null));

                        //MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3TiskPaletovehoListkuDokoncen, sopnumber.Trim()), Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        return;
                    }
                    catch (Exception ex)
                    {
                        // Logging.Log.Write(ex);
                        //DialogResult dr = MessageBoxBig.Show(ex.Message + "\n\n" + Fask.Localization.Localization.Vydej3ListPolozek3OpakovatTiskDokladu + "'" + sopnumber.Trim() + "'?", Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
                        //if (dr == DialogResult.No)
                        //    return;
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void PerformPaletaTisk(Fask.SQLiteDBs.DataSets.Vyroba.ProductionHistRow productionRow_data)
        {
            try
            {
                if (!Settings.TiskPalety)
                    return;

                while (true)
                {
                    try
                    {
                        Fask.SQLiteDBs.DataSets.Vyroba.ProductionHistDataTable dt = new SQLiteDBs.DataSets.Vyroba.ProductionHistDataTable();
                        Dictionary<string, string> data = new Dictionary<string, string>();

                        //skladani caroveho kodu GS1 --START---------------------
                        //zadat key
                        //zadat value

                        string GS1_KOD_1_1D = string.Empty;
                        string GS1_KOD_1_TX = string.Empty;
                        string GS1_KOD_2_1D = string.Empty;
                        string GS1_KOD_2_TX = string.Empty;
                        string SSCC = string.Empty;
                        string SSCC_bez_nul = string.Empty;
                        string WEIGHT = string.Empty;






                        if (productionRow_data != null)
                        {
                            //------------START-DATA----------------
                            //dotahovat data SSCC a WEIGHT

                            if (!productionRow_data.IsWEIGHTNull())
                            {
                                WEIGHT = productionRow_data.WEIGHT.ToString();
                            }

                            if (!data.ContainsKey("WEIGHT"))
                                data.Add("WEIGHT", WEIGHT);

                            if (!productionRow_data.IsNMBRPALNull())
                            {
                                //SSCC a WEIGHT doplnit logiku dohledani
                                //SSCC a WEIGHT natvrdo zadano
                                SSCC = productionRow_data.NMBRPAL;
                                SSCC_bez_nul = SSCC.Substring(2);
                            }

                            if (!data.ContainsKey("SSCC"))
                                data.Add("SSCC", SSCC_bez_nul);

                            //------------END-DATA----------------

                            GS1_KOD_1_1D = "02" + productionRow_data.BarcodeP.PadLeft(14, '0') + "37" + productionRow_data.qty.ToString("0000") + "";
                            GS1_KOD_1_TX = "(02)" + productionRow_data.BarcodeP.PadLeft(14, '0') + "(37)" + productionRow_data.qty.ToString("0000") + ""; //(02) (37)
                            GS1_KOD_2_1D = SSCC; //SSCC neni v production
                            GS1_KOD_2_TX = "(00)" + SSCC_bez_nul; // SSCC neni v production
                        }




                        if (!data.ContainsKey("GS1_KOD_1_1D"))
                            data.Add("GS1_KOD_1_1D", GS1_KOD_1_1D);

                        if (!data.ContainsKey("GS1_KOD_1_TX"))
                            data.Add("GS1_KOD_1_TX", GS1_KOD_1_TX);

                        if (!data.ContainsKey("GS1_KOD_2_1D"))
                            data.Add("GS1_KOD_2_1D", GS1_KOD_2_1D);

                        if (!data.ContainsKey("GS1_KOD_2_TX"))
                            data.Add("GS1_KOD_2_TX", GS1_KOD_2_TX);

                        //skladani caroveho kodu GS1 --END---------------------

                        data.Add("SOURCE", "Manual");


                        foreach (DataColumn dcol in dt.Columns)
                        {
                            string key = dcol.ColumnName;
                            string value = productionRow_data[dcol.ColumnName].ToString();
                            if (!data.ContainsKey(key))
                                data.Add(key, value);
                        }

                        //TODO : nejake pocty dat a dalsi podrobnosti o vydejovych datech ???

                        //bool vytisteno = VydejTisk.Print(data, MST_Global.PrintServerTemplateNameVydejPalListek);
                        bool vytisteno = PrintSendToPrinterFactory(data, Settings.TiskNazevSablony, null);
                        // Program.mstw.eventsUser.add(new Fask.Events.Event(Guid.NewGuid(), "3", Fask.Events.Event.etype_print, DateTime.Now, MST_Global.TerminalID, MST_Global.UserID, null, null, "v", listPolozekVydej.ListPolozek[0].CountEntries, listPolozekVydej.ListPolozek[0].SOPNUMBE, null, vytisteno.ToString(), null));

                        //MessageBoxBig.Show(string.Format(Fask.Localization.Localization.Vydej3ListPolozek3TiskPaletovehoListkuDokoncen, sopnumber.Trim()), Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        return;
                    }
                    catch (Exception ex)
                    {
                        // Logging.Log.Write(ex);
                        //DialogResult dr = MessageBoxBig.Show(ex.Message + "\n\n" + Fask.Localization.Localization.Vydej3ListPolozek3OpakovatTiskDokladu + "'" + sopnumber.Trim() + "'?", Fask.Localization.Localization.Vydej3ListPolozek3TiskPalListku, MessageBoxButtons.YesNo, MessageBoxBigIcon.Critical);
                        //if (dr == DialogResult.No)
                        //    return;
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public static bool Print(Dictionary<string, string> data, string templatename)
        //{
        //    try
        //    {
        //        //return Print(printParams, printData, templatename, 1);
        //        return PrintSendToPrinterFactory(data, templatename, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Logging.Log.Write(ex);
        //        MessageBoxBig.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
        //        return false;
        //    }
        //    finally
        //    {
        //    }
        //}


        private bool PrintSendToPrinterFactory(
            Dictionary<string, string> printData,
            string templateName,
            int? pocetVytisku
            )
        {
            bool printed = false;

            try
            {
                string pocetVytiskuStr = pocetVytisku.ToString();

                do
                {
                    if (!Settings.TiskMnozstviJednaAutomaticky)
                    {
                        if (string.IsNullOrEmpty(Settings.TiskMnozstviPredvyplnit))
                        {
                            //TODO pridat novy form s dotazem na počet vytisku
                            using (FormInputTiskMnozstvi frm = new FormInputTiskMnozstvi())
                            {
                                //frm.WindowState = FormWindowState.Maximized;
                                frm.WindowState = FormWindowState.Normal;

                                DialogResult dr = frm.ShowDialog();
                                if (dr == DialogResult.OK)
                                {
                                    pocetVytiskuStr = frm.Kod;

                                    //return true;
                                }
                                else if (dr == DialogResult.Cancel)
                                    return false;
                            }
                        }
                        else if (!string.IsNullOrEmpty(Settings.TiskMnozstviPredvyplnit) && int.Parse(Settings.TiskMnozstviPredvyplnit) <= 0)
                        {
                            //TODO pridat novy form s dotazem na počet vytisku
                            using (FormInputTiskMnozstvi frm = new FormInputTiskMnozstvi())
                            {
                                //frm.WindowState = FormWindowState.Maximized;
                                frm.WindowState = FormWindowState.Normal;

                                DialogResult dr = frm.ShowDialog();
                                if (dr == DialogResult.OK)
                                {
                                    pocetVytiskuStr = frm.Kod;

                                    //return true;
                                }
                                else if (dr == DialogResult.Cancel)
                                    return false;
                            }
                        }
                        else if (!string.IsNullOrEmpty(Settings.TiskMnozstviPredvyplnit) && int.Parse(Settings.TiskMnozstviPredvyplnit) > 0)
                        {
                            pocetVytiskuStr = Settings.TiskMnozstviPredvyplnit;
                        }
                    }
                    else if (Settings.TiskMnozstviJednaAutomaticky)
                    {
                        pocetVytiskuStr = "1";
                    }


                    //DialogResult dr = InputBox.Show("Počet výtisků", pocetVytiskuStr, out pocetVytiskuStr, Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
                    //if (dr == DialogResult.Cancel)
                    //    return false;

                    try
                    {
                        pocetVytisku = int.Parse(pocetVytiskuStr);
                    }
                    catch (Exception ex)
                    {
                        FlexibleMessageBox.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxIcon.Error);


                        continue;
                    }

                    if (pocetVytisku <= 0)
                    {
                        FlexibleMessageBox.Show("Počet výtisků musí být vyšší než 0", "Printing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    if (pocetVytisku > 100)
                    {
                        FlexibleMessageBox.Show("Počet výtisků nesmí být vyšší než 100", "Printing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    //pokud az sem, tak ok ... pustit do tisku
                    break;
                } while (true);

                Cursor.Current = Cursors.WaitCursor;

                printed = Print(printData, templateName, pocetVytisku ?? 1);

                return printed;
            }
            catch (WebException webex)
            {
                Cursor.Current = Cursors.Default;
                Logging.ExceptionHandler2.Handle(webex);
                FlexibleMessageBox.Show(webex.Message, "Printing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return printed;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.ExceptionHandler2.Handle(ex);
                FlexibleMessageBox.Show(ex.Message, "Printing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return printed;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool Print(Dictionary<string, string> printData, string templateName, int pocet)
        {

            if (!Settings.Tisk_OneWayPrint)
                return Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.tiskServis.Etiketa(0, templateName, prepareTiskParams(), prepareTiskValues(printData), pocet);
            else
                Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.tiskServis.EtiketaBezNavratu(0, templateName, prepareTiskParams(), prepareTiskValues(printData), pocet);

            return true;
        }

        private WebServiceTisk.TiskParams prepareTiskParams()
        {
            WebServiceTisk.TiskParams tiskParams = new WebServiceTisk.TiskParams();
            tiskParams.CONFIG_NAME = Settings.TiskNazevTiskarny; // to je vse ???
            return tiskParams;
        }

        private WebServiceTisk.DSValues prepareTiskValues(Dictionary<string, string> data)
        {
            WebServiceTisk.DSValues tiskValues = new WebServiceTisk.DSValues();
            tiskValues.Values.BeginLoadData();
            foreach (var item in data)
            {
                tiskValues.Values.AddValuesRow(item.Key, item.Value);
            }
            tiskValues.Values.EndLoadData();
            tiskValues.AcceptChanges();
            return tiskValues;
        }

        #endregion
    }
}
