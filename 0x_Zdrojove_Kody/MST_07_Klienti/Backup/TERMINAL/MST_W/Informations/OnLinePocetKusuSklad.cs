using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Informations
{
    public partial class OnLinePocetKusuSklad : System.Windows.Forms.Form
    {
        private string itemnmbr = null;
        private string location = null;
        private float mnozstvi = float.NaN;
        private string itemdesc = null;

        private _WebRefernces_Globals.InformationServiceSession infoservice = null;

        private delegate void DelegateNoParam();

        public OnLinePocetKusuSklad(string itemnmbr, string itemdesc)
        {
            InitializeComponent();

            this.itemdesc = itemdesc;
            this.itemnmbr = itemnmbr;

            UpdateUI();

            try
            {
                infoservice = new _WebRefernces_Globals.InformationServiceSession();
                infoservice.Timeout = MST_Global.ServiceTimeOut;
                infoservice.Url = MST_Global.ServerAddress + "Informations.asmx";
                infoservice.UpdateWebServiceCredentials();

                infoservice.BeginMnozstviNaSklade(itemnmbr, new AsyncCallback(KonecMnozstvi), null);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }

        }

        public OnLinePocetKusuSklad(string itemnmbr, string location, string itemdesc)
        {
            InitializeComponent();

            this.itemnmbr = itemnmbr;
            this.location = location;
            this.itemdesc = itemdesc;

            UpdateUI();

            try
            {
                infoservice = new _WebRefernces_Globals.InformationServiceSession();
                infoservice.Timeout = MST_Global.ServiceTimeOut;
                infoservice.Url = MST_Global.ServerAddress + "Informations.asmx";
                infoservice.UpdateWebServiceCredentials();

                infoservice.BeginMnozstviNaSklade_Itemnumber_Location(itemnmbr, location, new AsyncCallback(KonecMnozstviLokace), null);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }

        }

        private void UpdateUI()
        {
            try
            {
                this.lMnozstvi.Data = mnozstvi.ToString();
                if (float.IsNaN(mnozstvi))
                {
                    this.lMnozstvi.DataBackColor = Color.Red;
                }
                else
                    //this.lMnozstvi.DataBackColor = Color.Green;
                    this.lMnozstvi.DataBackColor = SystemColors.Window;
                this.lItemnmbr.Data = itemnmbr;
                this.lLocncode.Data = location;
                this.lItemdesc.Data = itemdesc;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void KonecMnozstvi(IAsyncResult ares)
        {
            try
            {
                mnozstvi = infoservice.EndMnozstviNaSklade(ares);
                this.BeginInvoke(new DelegateNoParam(UpdateUI));
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
                //this.BeginInvoke(new DelegateNoParam(this.Invalidate));
            }
        }


        private void KonecMnozstviLokace(IAsyncResult ares)
        {
            try
            {
                mnozstvi = infoservice.EndMnozstviNaSklade_Itemnumber_Location(ares);
                this.BeginInvoke(new DelegateNoParam(UpdateUI));
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message);
            }
            finally
            {
                //this.BeginInvoke(new DelegateNoParam(this.Invalidate));
            }
        }

        private void OnLinePocetKusuSklad_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
        }

        private void OnLinePocetKusuSklad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformOK();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void PerformOK()
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (infoservice != null)
            {
                try
                {
                    infoservice.Abort();
                }
                catch { }
            }
        }
    }
}