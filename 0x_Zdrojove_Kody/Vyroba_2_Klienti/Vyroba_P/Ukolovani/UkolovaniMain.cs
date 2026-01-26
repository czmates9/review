using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Vyroba_P.Extensions;
using Fask.Vyroba_P.UkolovaniService;
//Fask.Vyroba_P.Extensions

namespace Fask.Vyroba_P.Ukolovani
{
    public partial class UkolovaniMain : Form
    {
        public Fask.Vyroba_P.UkolovaniService.Ukoly.CZ_UKOL_ServiceManRow CZ_UKOL_ServiceManVybrana
        {
            get
            {
                try
                {
                    return (bs_ServiceMan.Current as DataRowView).Row as Fask.Vyroba_P.UkolovaniService.Ukoly.CZ_UKOL_ServiceManRow;
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    return null;
                }
            }
        }

        public UkolovaniMain()
        {
            InitializeComponent();
        }

        private void UkolovaniMain_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
        }

        private void RefreshTable()
        {
            try
            {

                this.ds_ServiceMan = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.ukolovaniService.GetServiceMan();

                bs_ServiceMan.DataSource = this.ds_ServiceMan.CZ_UKOL_ServiceMan;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void panelButton_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButton.Width / 2, panelButton.Height);
            buttonStorno.Size = nsize;
            //buttonOK.Size = nsize;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void PerformOK() 
        {

            if (CZ_UKOL_ServiceManVybrana == null)
            {
                MessageBox.Show(this, "Neni vybrán žádný servisni technik!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            try
            {
                SendServiceMan();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }


            DialogResult = DialogResult.OK;
        }

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void SendServiceMan() 
        {
            try
            {
                //int phone;
                //string From = "";
                //string To = "";
                //string Subject = "";
                string Poznamka = "";

                #region Zadat poznamku

                using (FormPoznamka frmpoz = new FormPoznamka())
                {
                    DialogResult res = frmpoz.ShowDialog();

                    if (res == DialogResult.OK)
                    {
                        Poznamka = frmpoz.Poznamka;
                    }
                }

                #endregion



                Pristupy.FASK_LoginsDataTable dt = new Pristupy.FASK_LoginsDataTable(); 

                if (Globals.Pracovnik != null)
                {

                    //Data.VyrobaCEDataSet.LoginsDataTable dt = new Fask.Vyroba_P.Data.VyrobaCEDataSet.LoginsDataTable();
                    dt.AddFASK_LoginsRow(Globals.Pracovnik.id, Globals.Pracovnik.firstname, Globals.Pracovnik.surname, Globals.Pracovnik.psswd, DateTime.Now, DateTime.Now, DateTime.Now, null);
                }
                else
                {
                    using (Odvadeni.FormIDPracovnika frmIDPracovnika = new Fask.Vyroba_P.Odvadeni.FormIDPracovnika())
                    {
                        if (frmIDPracovnika.ShowDialog(this) == DialogResult.Cancel)
                            return;

                       var idPracovnik = frmIDPracovnika.Pracovnik;
                       dt.AddFASK_LoginsRow(idPracovnik.id, idPracovnik.firstname, idPracovnik.surname, idPracovnik.psswd, DateTime.Now, DateTime.Now, DateTime.Now, null);
             
                    }
                }


     

                if (!CZ_UKOL_ServiceManVybrana.IsEMAILNull())
                {
                    if (!Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.ukolovaniService.SendEmailToServiceManFull(dt, Settings.TerminalID, Settings.MachineID, CZ_UKOL_ServiceManVybrana.EMAIL, Settings.Email_Predmet_email, Poznamka))
                    {
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Email nebyl odeslan");
                    }
                }

                if (!CZ_UKOL_ServiceManVybrana.IsOPERATORNull())
                {
                    if (!Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.ukolovaniService.SendEmailToServiceManFull(dt, Settings.TerminalID, Settings.MachineID, CZ_UKOL_ServiceManVybrana.OPERATOR, Settings.Email_Predmet_sms, Poznamka))
                    {
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "SMS nebyla odeslana");
                    }
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        
        }

        private void UkolovaniMain_Shown(object sender, EventArgs e)
        {

            try
            {
                this.dgv_ServiceMan.LoadConfiguration(this.GetType().ToString());
                RefreshTable();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void UkolovaniMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dgv_ServiceMan.SaveConfiguration(this.GetType().ToString());
        }

    }
}
