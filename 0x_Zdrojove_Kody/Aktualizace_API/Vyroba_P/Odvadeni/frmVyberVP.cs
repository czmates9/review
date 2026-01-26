using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using RestSharp;
using Fask.Logging;
using Fask.Aktualizace_API.Forms;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku
{
    public partial class frmVyberVP : Form
    {
        private int _cisloLinky;
        private int _cisloLinkyArchivace = 0;
        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable dtvpp_IN = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow dtvpp_OUT = null;
        private string cisloPolozky;

        public frmVyberVP(Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable dtvpp_IN, string text, string cisloPolozky)
        {

            try
            {
                InitializeComponent();
                //_cisloLinky = cisloLinky;
                l_text_top.Text = text;
                this.dtvpp_IN = dtvpp_IN;
                this.cisloPolozky = cisloPolozky;
                //_cisloLinkyArchivace = cisloLinky;
            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH -- frmArchivaceZaznamu - {0}", ex));
                ExceptionHandler2.Handle(ex);
            }
        }

        private void frmVyberVP_Load(object sender, EventArgs e)
        {
            try
            {

                bt_storno.Text = "STORNO";
                bt_ok.Text = "OK";

                //Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable dtvpp = null;
                //dsZbozi = new Fask.Console.Interfaces.DataSets.Zbozi();
                bs_vyberVP.DataSource = dtvpp_IN;
                ScannerStart();
                // PerformVyhledat();
            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH -- frmArchivaceZaznamu_Load - {0}", ex));
                ExceptionHandler2.Handle(ex);
            }
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {
            PerformOK();
            //DialogResult = DialogResult.OK;
        }

        private void PerformOK()
        {
            try
            {
                if (SelectedRow == null)
                {
                    MessageBox.Show("Není vybrán záznam", this.Text, MessageBoxButtons.OK);
                    return;
                }

                dtvpp_OUT = SelectedRow;

                this.DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ScannerStop();
            }
        }

        private void ScannerStart()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.DataReady += new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Enable();
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        }

        private void ScannerStop()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Disable();
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            try
            {
                if (e.BarcodeData.Trim().Length == 0)
                    return;

                //foreach vybrat dany radek a vratit, nastavit na out a dialog result ok
                //pokud nedohledal tak nic
                foreach (var item in dtvpp_IN)
                {
                    if (item.CountEntries.ToString() == e.BarcodeData.Trim())
                    {
                        dtvpp_OUT = item;
                        break;
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }

        }

        void Scanner_DataReady(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            try
            {
                this.BeginInvoke(new Fask.Aktualizace_API.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow SelectedRow
        {
            get
            {
                try
                {
                   // return ((dg_vyberVP.BindingContext[bs_vyberVP].Current)) as Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable;
                    return ((DataRowView)(dg_vyberVP.BindingContext[bs_vyberVP].Current)).Row as Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow;
                }
                catch (Exception ex)
                {

                    ExceptionHandler2.Handle(ex);
                    return null;
                }
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            int panelSirka = panel1.Width / 3;
            bt_storno.Width = panelSirka;
            bt_ok.Width = panelSirka;
        }

        #region ProgressIndicator

#if false

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
#endif


        #endregion

   


        private void bt_storno_Click(object sender, EventArgs e)
        {
            try
            {
                // PerformVyhledat();
                ScannerStop();
                DialogResult = DialogResult.Cancel;
            
            }
            catch (Exception ex)
            {

               // Log.Write(string.Format("CATCH -- bt_vyhledat_Click - {0}", ex));
                ExceptionHandler2.Handle(ex);
            }
        }




        private void frmVyberVP_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ScannerStop();
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        }

        private void bt_nacteniVP_Click(object sender, EventArgs e)
        {

            try
            {
                FormMain.Instance_FormMain.AktualizaceDat();

                dtvpp_IN = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcodeP_CZPRO_VPP(cisloPolozky);
                bs_vyberVP.DataSource = dtvpp_IN;
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        }
    }
}
