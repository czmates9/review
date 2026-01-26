using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Fask.Parsing.Codes;
using MES_Android.Classes;
using MES_Android.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_Android.OdvadeniVyroby.Logika
{
    public enum ShowTypes
    {
        _Unknown,
        Back,
        StornoOK
    }

    public class OdvadeniVyroby_Odvadeni_LogikaAsync
    {

        private OdvadeniVyroby _parent;
        public OdvadeniVyroby Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public DateTime? _PracovnikLoginDateTime = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _Pracovnik = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik
        {
            get { return _Pracovnik; }
            set
            {
                _Pracovnik = value;
                if (_Pracovnik == null)
                    _PracovnikLoginDateTime = null;
                else
                    _PracovnikLoginDateTime = DateTime.Now;
            }
        }

        public OdvadeniVyroby_Odvadeni_LogikaAsync(OdvadeniVyroby Parent, Users Uzivatel)
        {
            this._parent = Parent;


            Fask.SQLiteDBs.DataSets.Vyroba.LoginsDataTable dt = new Fask.SQLiteDBs.DataSets.Vyroba.LoginsDataTable();
            Pracovnik = dt.NewLoginsRow();
            Pracovnik.firstname = Uzivatel.FIRSTNAME;
            Pracovnik.id = Uzivatel.ID.ToString();
            Pracovnik.psswd = Uzivatel.Pwd;
            Pracovnik.surname = Uzivatel.SECONDNAME;

            Pracovnik.Login = Uzivatel.Login;
        }

        public async void PerfomOdvadeni_Async(Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow ZAKAZKA)
        {
            await PerfomOdvadeni(ZAKAZKA);
        }



        public async Task PerfomOdvadeni(Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow ZAKAZKA)
        {
                try
                {
                    Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idPracovnik = null;
                    Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idMachine = null;

                // TaD - Uživatel v MES musí byt vždy prihlaseny.... 
                // Takže tahle větev nidky nenastane

                idPracovnik = _Pracovnik;

                //if (Konfigurace_Singleton.Instance.Vyroba.UEventPracovnikLoginEnabled && _Pracovnik != null)
                //    {
                //        idPracovnik = _Pracovnik;
                //    }
                //    else
                //    {



                //    //Zadani ID pracovnika
                //    using (FormIDPracovnika frmIDPracovnika = new FormIDPracovnika())
                //    {
                //        if (frmIDPracovnika.ShowDialog() == DialogResult.Cancel)
                //            return;

                //        idPracovnik = frmIDPracovnika.Pracovnik;
                //    }
                //}

                //Zjisteni posledniho casu operace pracovnika
                //Fask.SQLiteDBs.DataSets.InternalStateTableAdapters.LstOperationUserTableAdapter lstoperta = new Fask.SQLiteDBs.DataSets.InternalStateTableAdapters.LstOperationUserTableAdapter();
                //lstoperta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
                //DateTime lstopertimePracovnik = lstoperta.GetLastOperationDateTime(idPracovnik.id) ?? DateTime.MinValue;

                DateTime lstopertimePracovnik;
                lstopertimePracovnik = DataInfo_Static.VyrobaGO_Instance.controller_InternalState.GetLastOperationDateTime(idPracovnik.Login) ?? DateTime.MinValue;


                if(Konfigurace_Singleton.Instance.Vyroba.LoginUserTimeOut_Enable)
                {
                    //Datum a cas posledni oprace pracovnika je vetsi nez maximalni mozny cas pracovnika
                    // => zadat datum a cas prichodu a zaznamenat prichod
                    if (lstopertimePracovnik < (DateTime.Now - Konfigurace_Singleton.Instance.Vyroba.LoginUserTimeOut))
                    {

                        #region Android nove
                        var stat_UserLog = await GetUserLoginWithTimeInputAsync();
                        lstopertimePracovnik = stat_UserLog.UserLoginDateTime.HasValue ? stat_UserLog.UserLoginDateTime.Value : DateTime.MinValue;
                        Fask.SQLiteDBs.DataSets.InternalState.UpdateInternalStateLstOperationUser(idPracovnik.Login, lstopertimePracovnik);

                        //Zaznamenat udalost prihlaseni pracovnika
                        //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
                        //ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                        DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.Insert_UserEvents(Konfigurace_Singleton.Instance.Vyroba.LastProductionUserID, null, lstopertimePracovnik, Konfigurace_Singleton.Instance.Vyroba.UEventPracovnikPrihlaseni, idPracovnik.Login, Config.Settings.TerminalID, string.Empty, Guid.NewGuid());

                        #endregion

                        #region Puvodne
                        //using (FormUserLoginWithTimeInput formusertime = new FormUserLoginWithTimeInput())
                        //    {
                        //        if (formusertime.ShowDialog() == DialogResult.Cancel)
                        //            return;

                        //        //datum a cas posledni operace
                        //    lstopertimePracovnik = formusertime.UserLoginDateTime;
                        //    Fask.SQLiteDBs.DataSets.InternalState.UpdateInternalStateLstOperationUser(idPracovnik.id, lstopertimePracovnik);

                        //    //Zaznamenat udalost prihlaseni pracovnika
                        //    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
                        //    //ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                        //    DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.Insert_UserEvents(Konfigurace_Singleton.Instance.Vyroba.LastProductionUserID, null, lstopertimePracovnik, Konfigurace_Singleton.Instance.Vyroba.UEventPracovnikPrihlaseni, idPracovnik.id, Config.Settings.TerminalID, string.Empty, Guid.NewGuid());
                        //    }
                        //    
                        #endregion
                    }
                }
                else
                {
                    lstopertimePracovnik = DateTime.Now;
                    Fask.SQLiteDBs.DataSets.InternalState.UpdateInternalStateLstOperationUser(idPracovnik.Login, lstopertimePracovnik);
                    DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.Insert_UserEvents(Konfigurace_Singleton.Instance.Vyroba.LastProductionUserID, null, lstopertimePracovnik, Konfigurace_Singleton.Instance.Vyroba.UEventPracovnikPrihlaseni, idPracovnik.Login, Config.Settings.TerminalID, string.Empty, Guid.NewGuid());
                }


                //Zadani id stroje
                if (!String.IsNullOrEmpty(Konfigurace_Singleton.Instance.Vyroba.Production_MachineID_Preset))
                {
                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter taM = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter();
                    //taM.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
                    var dtM = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataByID_Machines(Konfigurace_Singleton.Instance.Vyroba.Production_MachineID_Preset);
                    if (dtM.Count() > 0)
                        idMachine = dtM[0];
                }


                #region Android nove
                if (idMachine == null)
                {
                    var idMachineOBJ = await GetIDMachineAsync();

                    if (idMachineOBJ != null)
                        idMachine = idMachineOBJ.IDMachine;
                }
                #endregion

                #region Puvodne
                
                //if (idMachine == null)
                //{
                //    using (FormIDMachine frmIDMachine = new FormIDMachine())
                //    {
                //        if (frmIDMachine.ShowDialog() == DialogResult.Cancel)
                //            return;

                //        idMachine = frmIDMachine.Machine;
                //    }
                //} 

                #endregion

                #region Android nove

                var stat_lok_O = await GetOdvadeniVyroby_SberDatAsync(idPracovnik, idMachine, ZAKAZKA);

                #endregion

                #region Puvodne

                //Odvadeni
                //using (FormOdvadeni frmOdvadeni = new FormOdvadeni(idPracovnik, idMachine))
                //{
                //    frmOdvadeni.ShowDialog();
                //} 

                #endregion



            }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    await MessageBoxAsync.Show(_parent, ex.Message, "PerfomOdvadeni", MessageBoxButtons.OK);
                }
            }


        #region Metody pro volaní pomocne aktivity

        private Task<O_Get_UserLoginWithTimeInput> GetUserLoginWithTimeInputAsync()
        {
            try
            {
                _parent.TCS_GetUserLoginWithTimeInputAsync = new TaskCompletionSource<O_Get_UserLoginWithTimeInput>();

                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici(
                        typeof(UserLoginWithTimeInput),
                        _parent.ResoultCode_UserLoginWithTimeInput
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //return null;
                _parent.TCS_GetUserLoginWithTimeInputAsync.SetException(ex);
            }

            return _parent.TCS_GetUserLoginWithTimeInputAsync.Task;
        }

        private Task<O_GetOdvadeniVyroby_SberDatAsync> GetOdvadeniVyroby_SberDatAsync(
            Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik, 
            Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow Machine,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow ZAKAZKA)
        {
            try
            {
                _parent.TCS_GetOdvadeniVyroby_SberDatAsync = new TaskCompletionSource<O_GetOdvadeniVyroby_SberDatAsync>();

                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici(
                        typeof(OdvadeniVyroby_SberDat),
                        _parent.ResoultCode_OdvadeniVyroby_SberDat,
                        Pracovnik,
                        Machine,
                        ZAKAZKA
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //return null;
                _parent.TCS_GetOdvadeniVyroby_SberDatAsync.SetException(ex);
            }

            return _parent.TCS_GetOdvadeniVyroby_SberDatAsync.Task;
        }

        private Task<O_GetIDMachineAsync> GetIDMachineAsync()
        {
            try
            {
                _parent.TCS_GetIDMachineAsync = new TaskCompletionSource<O_GetIDMachineAsync>();

                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici(
                        typeof(IDMachine),
                        _parent.ResoultCode_IDMachine
                        );
                };

                _parent.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //return null;
                _parent.TCS_GetIDMachineAsync.SetException(ex);
            }

            return _parent.TCS_GetIDMachineAsync.Task;
        }


        #endregion

        #region Pomocne class objekty

        public class O_Get_UserLoginWithTimeInput
        {
            public DateTime? UserLoginDateTime;
        }

        public class O_GetOdvadeniVyroby_SberDatAsync
        {
            public string Status;
        }

        public class O_GetIDMachineAsync
        {
            public Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow IDMachine;
        }

        #endregion

        #region Start nasleduji aktivity

        private void StartAktivityNasledujici(
            Type typAktivity,
            int ResoultCode = 0,
            Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik = null, 
            Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow Machine = null,
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow ZAKAZKA = null
            )
        {
            Intent intent = new Intent(_parent, typAktivity);

            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(_parent.Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if (Pracovnik != null)
            {
                Bundle bundleA = new Bundle();
                bundleA.PutBinder(DataInfo_Static.object_OV_Pracovnik, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow>(Pracovnik));
                intent.PutExtra(DataInfo_Static.OV_Pracovnik, bundleA);
            }

            if (Machine != null)
            {
                Bundle bundleB = new Bundle();
                bundleB.PutBinder(DataInfo_Static.object_OV_Machine, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow>(Machine));
                intent.PutExtra(DataInfo_Static.OV_Machine, bundleB);
            }

            if (ZAKAZKA != null)
            {
                Bundle bundleC = new Bundle();
                bundleC.PutBinder(DataInfo_Static.object_OV_Zakazka, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow>(ZAKAZKA));
                intent.PutExtra(DataInfo_Static.OV_Zakazka, bundleC);
            }

            _parent.StartActivityForResult(intent, ResoultCode);

        }


        #endregion
    }
}