using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.ErrorLog;
using FASK.SledovaniVyroby.Module.Vyroba_SV.Configuration;
using FASK.SledovaniVyroby.ModuleIfc;
using RestSharp;
using Fask.Logging;
using Fask.Constants;

namespace FASK.SledovaniVyroby.Module.Vyroba_SV
{
    public partial class frmArchivaceZaznamu : Form
    {

       private  ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable _dataKArchivaci = new ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable();
        private ParametryProArchivaci par;

        public frmArchivaceZaznamu(ParametryProArchivaci parametry, string text)
        {

            try
            {
                InitializeComponent();
             
                l_text_top.Text = text;
                par = parametry;
            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH -- frmArchivaceZaznamu - {0}", ex));
                ExceptionHandler2.Handle(ex);
            }
        }

        private void frmArchivaceZaznamu_Load(object sender, EventArgs e)
        {
            try
            {
                bt_vyhledat.Text = Fask.Constants.AGRO.BTN_vyhledat;
                bt_deaktivace.Text = Fask.Constants.AGRO.BTN_deaktivovat;
                bt_zpet.Text = Fask.Constants.AGRO.BTN_zpet;
                PerformVyhledat();
            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH -- frmArchivaceZaznamu_Load - {0}", ex));
                ExceptionHandler2.Handle(ex);
            }
        }

        private void bt_zpet_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            int panelSirka = panel1.Width / 3;
            bt_vyhledat.Width = panelSirka;
            bt_zpet.Width = panelSirka;
        }

        #region ProgressIndicator

        private void ProgressIndicatorStop()
        {
            progressIndicator1.Stop();
            progressIndicator1.Visible = false;
        }

        private void ProgressIndicatorStart()
        {
            try
            {
                // prepocet stredu datagridu
                this.progressIndicator1.Location = new Point(this.dg_archivace.Location.X + (this.dg_archivace.Width / 2) - (progressIndicator1.Size.Width / 2), this.dg_archivace.Location.Y + (this.dg_archivace.Height / 2) - (progressIndicator1.Size.Height / 2));
            }
            catch { }
            progressIndicator1.Start();
            progressIndicator1.Visible = true;
        }


        #endregion

        #region BackGround Worker bwLoadZaznamy

        private void bwLoadFE_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //var cislin = (int)e.Argument;


                var param = (ParametryProArchivaci)e.Argument;
                if (bw_archivace.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events = new Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events();

                #region old code - design patern

                ////logika vyhledani zaznamu se vstupnim parametrem cisla linky
                ////    //--------------START-----------------------------
                //FaskEventsDataSet.FASK_Events_archivaceDataTable D0 = new FaskEventsDataSet.FASK_Events_archivaceDataTable();
                //string separator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator;
                //// API komunikace
                ////FASK_Events o = new FASK_Events();
                //IRestResponse restResponse;
                //string param = "SledovaniVyroby_archivaceZaznamu_data";

                //string JSON = "";
                //Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events = new Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events();
                ////
                //if(cislin == AGRO.status_21)
                //{
                //    filtr_Events.machineid = -1;
                //    filtr_Events.status = AGRO.status_21;
                //    filtr_Events.separator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator_strec;
                //}
                //else if(cislin == AGRO.status_22)
                //{
                //    filtr_Events.machineid = -1;
                //    filtr_Events.status = AGRO.status_22;
                //    filtr_Events.separator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator_strec;
                //}
                //else
                //{
                //    filtr_Events.machineid = cislin;
                //    filtr_Events.status = AGRO.status_0;
                //    filtr_Events.separator = separator;
                //}





                //                JSON = Classes.WEBAPI.JSON_Class.Serialize_JSON(filtr_Events);

                //                if (!Classes.WEBAPI.Comunication.Communicate(Classes.WEBAPI.Comunication.REST_Type.POST, out restResponse, param, JSON, "nakladka"))
                //                {
                //                    throw new Exception("Komunikace s IS AGRO se nezdařila");
                //                }

                //                if (restResponse.StatusCode == HttpStatusCode.OK)
                //                {
                //                    D0 = Newtonsoft.Json.JsonConvert.DeserializeObject<FaskEventsDataSet.FASK_Events_archivaceDataTable>(restResponse.Content);
                //                }


                //                //zaznamy pro deaktivaci--dat do pameti?? -> pote overit zda je mohu zdeaktivovat?? 
                //                if (D0 == null)
                //                {
                //#if DEBUG
                //                    //Log.Write(string.Format("chybi zaznam -- bwLoadFE_DoWork "));
                //                    ExceptionHandler2.Handle("chybi zaznam -- bwLoadFE_DoWork ", "Log_LV", "txt");
                //#endif
                //                }
                //                else
                //                {
                //                    _dataKArchivaci = D0;
                //                }
                //    //--------------END-------------------------------
                #endregion
                filtr_Events.machineid = param.CisloLinky;
                filtr_Events.status = param.Status;
                filtr_Events.separator = param.Separator;

                ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable D0 = Database.Classes.Vyroba_Remote.LoadFE(
                          Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                          int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                          filtr_Events);


                if (bw_archivace.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = D0;
            }
            catch (Exception ex)
            {
                //Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //Log.Write(string.Format("CATCH -- bwLoadFE_DoWork - {0}", ex));
                ExceptionHandler2.Handle(ex);
            }
        }

        private void bwLoadFE_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
                    // handle the error
                    ds_archivace = new ICommDatabase.DSVyroba();
                    bs_archivace.DataSource = ds_archivace;
                    //Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, e.Error);
#if DEBUG
                    //Log.Write(string.Format("ERROR -- bwLoadFE_RunWorkerCompleted "));
                    ExceptionHandler2.Handle("ERROR -- bwLoadFE_RunWorkerCompleted ", "Log_LV", "txt");
#endif
                    //MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
                    // handle cancellation
                    // TODO: maji se vymazat data z datasetu?? asi ano ...
                    //if (dsSkladPohyb == null)
                    ds_archivace = new ICommDatabase.DSVyroba();
                    bs_archivace.DataSource = ds_archivace;
                }
                else
                {
                    // uspesne dokonceno ...
                    // use it on the UI thread
                    var dt = (ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable)e.Result;


                    

                    if (dt == null)
                        ds_archivace = new ICommDatabase.DSVyroba();
                    else
                    {
                        ds_archivace.FASK_Events_archivace.Clear();

                        //foreach (var item in dt.FASK_Events_archivace)
                        foreach (var item in dt)
                        {
                            ds_archivace.FASK_Events_archivace.ImportRow(item);
                        }
                    }

                    bs_archivace.DataSource = ds_archivace;
                }

            }
            catch (Exception ex)
            {
                //Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //Log.Write(string.Format("CATCH -- bwLoadFE_RunWorkerCompleted - {0}", ex));
                ExceptionHandler2.Handle(ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorStop();
            }
        }

        #endregion

        #region BackGround Worker bwDeaktivaceZaznamy

        private void bwDeaktivaceFE_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //var cislin = (int)e.Argument;


                var cislin = (ParametryProArchivaci)e.Argument;
                if (bw_deaktivace.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                int pocet = Database.Classes.Vyroba_Remote.DeaktivaceFE(
                                Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                                int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                                cislin.Status,
                                ds_archivace.FASK_Events_archivace,
                                cislin.CisloLinky);

                if (bw_deaktivace.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = pocet;
            }
            catch (Exception ex)
            {
                //Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //Log.Write(string.Format("CATCH -- bwDeaktivaceFE_DoWork - {0}", ex));
                ExceptionHandler2.Handle(ex);
            }
        }


        private void bwDeaktivaceFE_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check error, check cancel, then use result
                if (e.Error != null)
                {
#if DEBUG
                    //Log.Write(string.Format("ERROR -- bwDeaktivaceFE_RunWorkerCompleted "));
                    ExceptionHandler2.Handle("ERROR -- bwDeaktivaceFE_RunWorkerCompleted ", "Log_LV", "txt");
#endif
                    // MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Cancelled)
                {
#if DEBUG
                    //Log.Write(string.Format("CANCEL -- bwDeaktivaceFE_RunWorkerCompleted "));
                    ExceptionHandler2.Handle("CANCEL -- bwDeaktivaceFE_RunWorkerCompleted ", "Log_LV", "txt");
#endif
                }
                else
                {
                   // Log.Write(string.Format("DEAKTIVACE-ARCHIVOVANO -- Linka_{0} ", _cisloLinkyArchivace));
                    //string log_value = string.Format("DEAKTIVACE-ARCHIVOVANO -- Linka_{0} ", _cisloLinkyArchivace);
                    //ExceptionHandler2.Handle(log_value, "Log_LV", "txt");


                    PerformVyhledat();


                    //ODKOMENTOVAT je-li potreba informovat
                    //int pocet = (int)e.Result;
                    //string info = string.Format("Úspěšně deaktivováno: {0} ", pocet);
                    //string infoHlavicka = "deaktivace";
                    //MessageBox.Show(info, infoHlavicka, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception ex)
            {
                //Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //Log.Write(string.Format("CATCH -- bwDeaktivaceFE_RunWorkerCompleted - {0}", ex));
                ExceptionHandler2.Handle(ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressIndicatorStop();
            }
        }

        #endregion

        private void bt_vyhledat_Click(object sender, EventArgs e)
        {
            try
            {
                PerformVyhledat();
            }
            catch (Exception ex)
            {

               // Log.Write(string.Format("CATCH -- bt_vyhledat_Click - {0}", ex));
                ExceptionHandler2.Handle(ex);
            }
        }

        private void PerformVyhledat()
        {
            try
            {
              

                if (bw_archivace.IsBusy)
                {
                    bw_archivace.CancelAsync();
                    while (bw_archivace.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                //Fask.Console.Interfaces.Classes.ZboziListFiltr filtr = new Fask.Console.Interfaces.Classes.ZboziListFiltr();
                //if (!CreateFilter(ref filtr))
                //    return;

                int FirstDisplayedScrollingRowIndex = this.dg_archivace.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_archivace.RunWorkerAsync(par);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_archivace.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_archivace.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                //Log.Write(string.Format("CATCH -- PerformVyhledat - {0}", ex));
                ExceptionHandler2.Handle(ex);
                //Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bt_deaktivace_Click(object sender, EventArgs e)
        {

            try
            {
                using (FormDeaktivace frm = new FormDeaktivace())
                {
                    frm.WindowState = FormWindowState.Normal;
                    var dr = frm.ShowDialog();
                    if (dr == DialogResult.Yes)
                    {
                        PerformDeaktivovat();
                    }
                    else if (dr == DialogResult.No)
                    {
                        PerformVyhledat();
                    }
                }
            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH -- bt_deaktivace_Click - {0}", ex));
                ExceptionHandler2.Handle(ex);
            }


            //var dr = MessageBox.Show("Opravdu chcete deaktivovat?", "Deaktivace záznamů", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

         
            
        }

        private void PerformDeaktivovat()
        {
            try
            {

                if (bw_deaktivace.IsBusy)
                {
                    bw_deaktivace.CancelAsync();
                    while (bw_deaktivace.IsBusy)
                    {
                        Application.DoEvents();
                    }
                }

                ProgressIndicatorStart();

                bw_deaktivace.RunWorkerAsync(par);
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                //Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //Log.Write(string.Format("CATCH -- PerformDeaktivovat - {0}", ex));
                ExceptionHandler2.Handle(ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

    public class ParametryProArchivaci
    {
        public int CisloLinky;
        public int Status;
        public string Separator;
    }
}
