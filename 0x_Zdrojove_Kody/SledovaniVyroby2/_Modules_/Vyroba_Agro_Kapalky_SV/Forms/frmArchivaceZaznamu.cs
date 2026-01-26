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
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Kapalky_SV.Configuration;
using FASK.SledovaniVyroby.ModuleIfc;
using RestSharp;
using Fask.Logging;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Kapalky_SV.Forms
{
    public partial class frmArchivaceZaznamu : Form
    {
        
        #region Parametry
        

        //private int _cisloLinky;
        //private int _cisloLinkyArchivace = 0;
        //private ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable _dataKArchivaci = new ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable();
        private ParametryProArchivaci par;

        #endregion

        #region Eventy formu

        //public frmArchivaceZaznamu(int cisloLinky, string text)
        public frmArchivaceZaznamu(ParametryProArchivaci parametry, string text)
        {

            try
            {
                InitializeComponent();
                //_cisloLinky = cisloLinky;
                l_text_top.Text = text;
                //_cisloLinkyArchivace = cisloLinky;
                par = parametry;
            }
            catch (Exception ex)
            {
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
                ExceptionHandler2.Handle(ex);
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            int panelSirka = panel1.Width / 3;
            bt_vyhledat.Width = panelSirka;
            bt_zpet.Width = panelSirka;
        }

        #endregion

        #region Button Eventy

        private void bt_zpet_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void bt_vyhledat_Click(object sender, EventArgs e)
        {
            try
            {
                PerformVyhledat();
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
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
                ExceptionHandler2.Handle(ex);
            }
        }


        #endregion

        #region Perform metody

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

                int FirstDisplayedScrollingRowIndex = this.dg_archivace.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index

                bw_archivace.RunWorkerAsync(par);

                if ((FirstDisplayedScrollingRowIndex >= 0) && ((this.dg_archivace.Rows.Count - 1) >= FirstDisplayedScrollingRowIndex)) this.dg_archivace.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex; //Restore Scroll Index
            }
            catch (Exception ex)
            {
                ProgressIndicatorStop();
                ExceptionHandler2.Handle(ex);
            }
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
                ExceptionHandler2.Handle(ex);
            }
        }

        #endregion


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
                var param = (ParametryProArchivaci)e.Argument;
                if (bw_archivace.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events = new Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events();

                //if (cislin == 21)
                //{
                //    filtr_Events.machineid = -1;
                //    filtr_Events.status = 21;
                //    filtr_Events.separator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator_strec;
                //}
                //else if (cislin == 22)
                //{
                //    filtr_Events.machineid = -1;
                //    filtr_Events.status = 22;
                //    filtr_Events.separator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator_strec;
                //}
                //else
                //{
                //    filtr_Events.machineid = cislin;
                //    filtr_Events.status = 0;
                //    filtr_Events.separator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator;
                //}

                filtr_Events.machineid = param.CisloLinky;
                filtr_Events.status = param.Status;
                filtr_Events.separator = param.Separator;
               

                //ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable D0 = Classes.Komunikace.LoadFE(cislin);
                ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable D0 = Database.Classes.Vyroba_Remote.LoadFE(
                            Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                            int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                            filtr_Events);

//                if (D0 == null)
//                {
//#if DEBUG
//                    ExceptionHandler2.Handle("chybi zaznam -- bwLoadFE_DoWork ", "Log_LV", "txt");
//#endif
//                }
//                else
//                {
//                    _dataKArchivaci = D0;
//                }

                if (bw_archivace.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = D0;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }
        private void bwLoadFE_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    ds_archivace = new ICommDatabase.DSVyroba();
                    bs_archivace.DataSource = ds_archivace;
#if DEBUG
                    ExceptionHandler2.Handle("ERROR -- bwLoadFE_RunWorkerCompleted ", "Log_LV", "txt");
#endif
                }
                else if (e.Cancelled)
                {
                    ds_archivace = new ICommDatabase.DSVyroba();
                    bs_archivace.DataSource = ds_archivace;
                }
                else
                {
                    var dt = (ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable)e.Result;
                    
                    if (dt == null)
                        ds_archivace = new ICommDatabase.DSVyroba();
                    else
                    {
                        ds_archivace.FASK_Events_archivace.Clear();

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
                ExceptionHandler2.Handle(ex);
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
                var cislin = (ParametryProArchivaci)e.Argument;

                if (bw_deaktivace.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //int pocet = Classes.Komunikace.DeaktivaceFE(cislin, _dataKArchivaci);
                int pocet = Database.Classes.Vyroba_Remote.DeaktivaceFE(
                    Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                    int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                    cislin.Status, 
                    ds_archivace.FASK_Events_archivace,
                    Fask.Constants.AGRO.bocedi_4_K);

                if (bw_deaktivace.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                e.Result = pocet;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }


 
        private void bwDeaktivaceFE_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
#if DEBUG
                    ExceptionHandler2.Handle("ERROR -- bwDeaktivaceFE_RunWorkerCompleted ", "Log_LV", "txt");
#endif
                }
                else if (e.Cancelled)
                {
#if DEBUG
                    ExceptionHandler2.Handle("CANCEL -- bwDeaktivaceFE_RunWorkerCompleted ", "Log_LV", "txt");
#endif
                }
                else
                {
                    //string log_value = string.Format("DEAKTIVACE-ARCHIVOVANO -- Linka_{0} ", _cisloLinkyArchivace);
                    //ExceptionHandler2.Handle(log_value, "Log_LV", "txt");

                    PerformVyhledat();
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
            finally
            {
                ProgressIndicatorStop();
            }
        }

        #endregion

  

    }

    public class ParametryProArchivaci 
    {
        public int CisloLinky;
        public int Status;
        public string Separator;
    }
}
