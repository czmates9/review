using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MES_Android.Prodej
{
    public class FormReport  : IDisposable
    {
        private decimal _naSklad;
        public decimal NaSklad
        {
            set { this._naSklad = value; }
            get { return this._naSklad; }
        }

        private decimal _naExpedici;
        public decimal NaExpedici
        {
            set { this._naExpedici = value; }
            get { return this._naExpedici; }
        }

        #region DataNaZobrazeni

        public string CZ_CarKod = string.Empty;
        public string ITEMDESC = string.Empty;
        public string ITEMNMBR = string.Empty;

        public decimal MnozstviDodavatelePozadovano = 0;
        public decimal MnozstviDodavateleDodano = 0;
        public decimal MnozstviDodavateleDodat = 0;
        public decimal MnozstviOdberateliPozadovano = 0;
        public decimal MnozstviOdberatelumDodano = 0;
        public decimal MnozstviOdberatelumDodat = 0;
        //public decimal Vysledek = 0;

        #endregion
        private Android.App.AlertDialog dialog;
        private Android.App.AlertDialog.Builder ad;
        private Android.Views.InputMethods.InputMethodManager mgr;
        private AppCompatActivity _Parent;

        private DialogResult _ConfirmBoxResult;
        private bool _IsCurrentlyInConfirmProcess = true;

        private TextView Report_PopisPolozky_Value = null;
        private TextView Report_IDPolozky_Value = null;
        private TextView Report_CarKod_Value = null;
        private TextView Report_Sklad_Value = null;
        private TextView Report_Expedice_Value = null;
        private TextView Report_PozadovanoOdDodavatele_Value = null;
        private TextView Report_JizDodano_Value = null;
        private TextView Report_ZbyvaNaObjednavce_Value = null;
        private TextView Report_PozadovanoOdberateli_Value = null;

        private Button btn_OK;
        private Button btn_Storno;

        public DialogResult ShowDialog(AppCompatActivity _parent)
        {
            _Parent = _parent;

            if (_Parent != null)
            {

                Action messageBoxDelegate = () => Show();

                _parent.RunOnUiThread(messageBoxDelegate);

                while (_IsCurrentlyInConfirmProcess)
                {
                    Thread.Sleep(1000);
                }
            }

            dialog.Dismiss();
            dialog.Dispose();
            dialog = null;

            return _ConfirmBoxResult;
        }

        public void Dispose()
        {
            ad.Dispose();
            ad = null;
        }


        private void Show()
        {
            try
            {

                LayoutInflater inflater = (LayoutInflater)_Parent.GetSystemService(Context.LayoutInflaterService);
                View formElementsView = inflater.Inflate(Resource.Layout.Dialog_Report, null, false);

                Report_PopisPolozky_Value = (TextView)formElementsView.FindViewById(Resource.Id.Report_PopisPolozky_Value);
                Report_IDPolozky_Value = (TextView)formElementsView.FindViewById(Resource.Id.Report_IDPolozky_Value);
                Report_CarKod_Value = (TextView)formElementsView.FindViewById(Resource.Id.Report_CarKod_Value);
                Report_Sklad_Value = (TextView)formElementsView.FindViewById(Resource.Id.Report_Sklad_Value);
                Report_Expedice_Value = (TextView)formElementsView.FindViewById(Resource.Id.Report_Expedice_Value);
                Report_PozadovanoOdDodavatele_Value = (TextView)formElementsView.FindViewById(Resource.Id.Report_PozadovanoOdDodavatele_Value);
                Report_JizDodano_Value = (TextView)formElementsView.FindViewById(Resource.Id.Report_JizDodano_Value);
                Report_ZbyvaNaObjednavce_Value = (TextView)formElementsView.FindViewById(Resource.Id.Report_ZbyvaNaObjednavce_Value);
                Report_PozadovanoOdberateli_Value = (TextView)formElementsView.FindViewById(Resource.Id.Report_PozadovanoOdberateli_Value);

                btn_OK = (Button)formElementsView.FindViewById(Resource.Id.Rep_btn_OK);
                btn_Storno = (Button)formElementsView.FindViewById(Resource.Id.Rep_btn_St);

                btn_OK.Click += Btn_OK_Click;
                btn_Storno.Click += Btn_Storno_Click;

                mgr = (Android.Views.InputMethods.InputMethodManager)_Parent.GetSystemService(Context.InputMethodService);
                mgr.ShowSoftInput(formElementsView, Android.Views.InputMethods.ShowFlags.Forced);


                Report_PozadovanoOdDodavatele_Value.Text = this.MnozstviDodavatelePozadovano.ToString(Config.Settings.UIFormatDesCisel);
                Report_JizDodano_Value.Text = this.MnozstviOdberatelumDodano.ToString(Config.Settings.UIFormatDesCisel);
                Report_ZbyvaNaObjednavce_Value.Text = this.MnozstviOdberatelumDodat.ToString(Config.Settings.UIFormatDesCisel);

                Report_PozadovanoOdberateli_Value.Text = this.MnozstviOdberateliPozadovano.ToString(Config.Settings.UIFormatDesCisel);

                Report_CarKod_Value.Text = string.IsNullOrEmpty(this.CZ_CarKod) ? "-" : this.CZ_CarKod.Trim();
                Report_PopisPolozky_Value.Text = string.IsNullOrEmpty(this.ITEMDESC) ? "-" : this.ITEMDESC.Trim();
                Report_IDPolozky_Value.Text = string.IsNullOrEmpty(this.ITEMNMBR) ? "-" : this.ITEMNMBR.Trim();

                Report_Expedice_Value.Text = this._naExpedici.ToString(Config.Settings.UIFormatDesCisel);
                Report_Sklad_Value.Text = this._naSklad.ToString(Config.Settings.UIFormatDesCisel);


                ad = new Android.App.AlertDialog.Builder(_Parent, Resource.Style.Theme_AppCompat_DayNight_DialogWhenLarge);
                ad.SetCancelable(false);

                ad.SetView(formElementsView);

                dialog = ad.Show();

                InputMethodManager imm = (InputMethodManager)_Parent.GetSystemService(Context.InputMethodService);
                imm.ToggleSoftInput(ShowFlags.Forced, 0);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }

        private void Btn_Storno_Click(object sender, EventArgs e)
        {
            _ConfirmBoxResult = DialogResult.Cancel;
            _IsCurrentlyInConfirmProcess = false;
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            _ConfirmBoxResult = DialogResult.OK;
            _IsCurrentlyInConfirmProcess = false;
        }
    }
}