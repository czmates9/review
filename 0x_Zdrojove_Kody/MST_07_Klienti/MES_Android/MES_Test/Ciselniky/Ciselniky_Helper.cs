using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MES_Android.Ciselniky.CiselnikServiceOperations;

namespace MES_Android.Ciselniky
{
    public class Ciselniky_Helper
    {
        #region Original pokus

        //    public static async Task<bool> SynchronizeCiselniky(
        //Operation typ,
        //Android.Support.V7.App.AppCompatActivity parent,
        //Android.Support.V4.App.FragmentTransaction Transaction,
        //string SkladFilter)
        //    {

        //        var tcs = new TaskCompletionSource<bool>();

        //        try
        //        {

        //            Task<bool> zboziTask;
        //            Task<bool> OdberateleTask;
        //            Task<bool> SkladTask;
        //            Task<bool> MenyTask;
        //            Task<bool> StrediskaTask;
        //            Task<bool> TypDokladTask;
        //            Task<bool> LokaceTask;
        //            Task<bool> PracovniciTask;


        //            Task<bool> zboziTask_E;
        //            Task<bool> OdberateleTask_E;
        //            Task<bool> SkladTask_E;
        //            Task<bool> MenyTask_E;
        //            Task<bool> StrediskaTask_E;
        //            Task<bool> PracovniciTask_E;

        //            var ListTasku = new List<Task>();

        //            switch (typ)
        //            {
        //                case Operation.All:
        //                    zboziTask = KatalogZbozi(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    OdberateleTask = KatalogOdberatele(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    SkladTask = KatalogSklady(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    MenyTask = KatalogMen(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    StrediskaTask = KatalogStrediska(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    TypDokladTask = KatalogTypDokladu(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    LokaceTask = KatalogLokace(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    PracovniciTask = KatalogPracovnici(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    ListTasku.Add(zboziTask);
        //                    ListTasku.Add(OdberateleTask);
        //                    ListTasku.Add(SkladTask);
        //                    ListTasku.Add(MenyTask);
        //                    ListTasku.Add(StrediskaTask);
        //                    ListTasku.Add(TypDokladTask);
        //                    ListTasku.Add(LokaceTask);
        //                    ListTasku.Add(PracovniciTask);
        //                    break;
        //                case Operation.Zbozi:
        //                    zboziTask = KatalogZbozi(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    ListTasku.Add(zboziTask);
        //                    break;
        //                case Operation.Odberatele:
        //                    OdberateleTask = KatalogOdberatele(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    ListTasku.Add(OdberateleTask);
        //                    break;
        //                case Operation.Strediska:
        //                    StrediskaTask = KatalogStrediska(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    ListTasku.Add(StrediskaTask);
        //                    break;
        //                case Operation.TypDokladu:
        //                    TypDokladTask = KatalogTypDokladu(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    ListTasku.Add(TypDokladTask);
        //                    break;
        //                case Operation.Sklady:
        //                    SkladTask = KatalogSklady(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    ListTasku.Add(SkladTask);
        //                    break;
        //                case Operation.Pracovnici:
        //                    PracovniciTask = KatalogPracovnici(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    ListTasku.Add(PracovniciTask);
        //                    break;
        //                case Operation.Lokace:
        //                    LokaceTask = KatalogLokace(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    ListTasku.Add(LokaceTask);
        //                    break;
        //                case Operation.Meny:
        //                    MenyTask = KatalogMen(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    ListTasku.Add(MenyTask);
        //                    break;
        //                case Operation.ExportAll:
        //                    zboziTask_E = KatalogZboziExport(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    OdberateleTask_E = KatalogOdberateleExport(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    SkladTask_E = KatalogSkladyExport(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    MenyTask_E = KatalogMenyExport(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    StrediskaTask_E = KatalogStrediskaExport(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    PracovniciTask_E = KatalogPracovniciExport(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    ListTasku.Add(zboziTask_E);
        //                    ListTasku.Add(OdberateleTask_E);
        //                    ListTasku.Add(SkladTask_E);
        //                    ListTasku.Add(MenyTask_E);
        //                    ListTasku.Add(StrediskaTask_E);
        //                    ListTasku.Add(PracovniciTask_E);
        //                    break;
        //                case Operation.ExportZbozi:
        //                    zboziTask_E = KatalogZboziExport(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik, SkladFilter);
        //                    ListTasku.Add(zboziTask_E);
        //                    break;
        //                case Operation.ExportOdberatele:
        //                    OdberateleTask_E = KatalogOdberateleExport(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    ListTasku.Add(OdberateleTask_E);
        //                    break;
        //                case Operation.ExportSklady:
        //                    SkladTask_E = KatalogSkladyExport(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    ListTasku.Add(SkladTask_E);
        //                    break;
        //                case Operation.ExportMeny:
        //                    MenyTask_E = KatalogMenyExport(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    ListTasku.Add(MenyTask_E);
        //                    break;
        //                case Operation.ExportStrediska:
        //                    StrediskaTask_E = KatalogStrediskaExport(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    ListTasku.Add(StrediskaTask_E);
        //                    break;
        //                case Operation.ExportPracovnici:
        //                    PracovniciTask_E = KatalogPracovniciExport(parent, Transaction, Classes.DataInfo_Static.ProdejGO_Instance.servis_ciselnik);
        //                    ListTasku.Add(PracovniciTask_E);
        //                    break;
        //                default:
        //                    break;
        //            }

        //            while (ListTasku.Count > 0)
        //            {
        //                Task finishedTask = await Task.WhenAny(ListTasku);
        //                ListTasku.Remove(finishedTask);
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            Fask.Logging.ExceptionHandler2.Handle(ex);
        //        }

        //        return true;
        //    }


        #endregion


        //public async void SynchronizeCiselniky(Operation typ, Android.Support.V7.App.AppCompatActivity parent, string SkladFilter)
        public async Task<bool> SynchronizeCiselniky(Operation typ, Android.Support.V7.App.AppCompatActivity parent, string SkladFilter )
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            try
            {

                Classes.ProgressDialog_Infinity.Show(parent);

                CiselnikServiceOperations ciselnik = null;
                VyrobaServiceOperations ciselnik_Vyroba = null;

                if (typ == Operation.All)
                {
                    ciselnik = new CiselnikServiceOperations();
                    ciselnik_Vyroba = new VyrobaServiceOperations();
                }
                else if (typ == Operation.OdvadeniVyroby || typ == Operation.Production)
                {
                    ciselnik_Vyroba = new VyrobaServiceOperations();
                }
                else
                {
                    ciselnik = new CiselnikServiceOperations();
                }


                    if (typ == Operation.All)
                {
                    #region Zbozi
                    try
                    {

                        if (Konfigurace_Singleton.Instance.ExportZbozi)
                        {

                            var dr = await MessageBoxAsync.Show(parent, "Exportovat číselník zboží?", "Dotaz", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                var tcs_ZE = new TaskCompletionSource<bool>();
                                var TE = ciselnik.KatalogCiselnik(tcs_ZE, parent, Operation.ExportZbozi, "Probíhá export číselníku zboží", SkladFilter);
                                var resE = await TE;
                            }
                        }

                        var tcs_Z = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_Z, parent, Operation.Zbozi, "Stahuje se číselník zboží", SkladFilter);
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                    #region Odberatele
                    try
                    {

                        if (Konfigurace_Singleton.Instance.ExportOdberatele)
                        {
                            var dr = await MessageBoxAsync.Show(parent, "Exportovat číselník odběratelů?", "Dotaz", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                var tcs_OE = new TaskCompletionSource<bool>();
                                var TE = ciselnik.KatalogCiselnik(tcs_OE, parent, Operation.ExportOdberatele, "Probíhá export číselníku odběratelů");
                                var resE = await TE;
                            }
                        }

                        var tcs_O = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_O, parent, Operation.Odberatele, "Stahuje se číselník odběratelů", SkladFilter);
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                    #region Sklady
                    try
                    {

                        if (Konfigurace_Singleton.Instance.ExportSklady)
                        {
                            var dr = await MessageBoxAsync.Show(parent, "Exportovat číselník skladů?", "Dotaz", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                var tcs_SE = new TaskCompletionSource<bool>();
                                var TE = ciselnik.KatalogCiselnik(tcs_SE, parent, Operation.ExportSklady, "Probíhá export číselníku skladů");
                                var resE = await TE;
                            }
                        }

                        var tcs_S = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_S, parent, Operation.Sklady, "Stahuje se číselník skladů");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                    #region Meny
                    try
                    {

                        if (Konfigurace_Singleton.Instance.ExportMeny)
                        {
                            var dr = await MessageBoxAsync.Show(parent, "Exportovat číselník měn?", "Dotaz", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                var tcs_ME = new TaskCompletionSource<bool>();
                                var TE = ciselnik.KatalogCiselnik(tcs_ME, parent, Operation.ExportMeny, "Probíhá export číselníku měn");
                                var resE = await TE;
                            }
                        }

                        var tcs_M = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_M, parent, Operation.Meny, "Stahuje se číselník měn");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                    #region Strediska

                    try
                    {

                        if (Konfigurace_Singleton.Instance.ExportStrediska)
                        {
                            var dr = await MessageBoxAsync.Show(parent, "Exportovat číselník středisek?", "Dotaz", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                var tcs_STE = new TaskCompletionSource<bool>();
                                var TE = ciselnik.KatalogCiselnik(tcs_STE, parent, Operation.ExportStrediska, "Probíhá export číselníku středisek");
                                var resE = await TE;
                            }
                        }

                        var tcs_ST = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_ST, parent, Operation.Strediska, "Stahuje se číselník středisek", SkladFilter);
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }

                    #endregion

                    #region TypDokladu
                    try
                    {
                        var tcs_TD = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_TD, parent, Operation.TypDokladu, "Stahuje se číselník typů dokladů", SkladFilter);
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                    #region Lokace
                    try
                    {

                        var tcs_L = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_L, parent, Operation.Lokace, "Stahuje se číselník lokací");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                    #region Pracovnici
                    try
                    {

                        if (Konfigurace_Singleton.Instance.ExportPracovnici)
                        {
                            var dr = await MessageBoxAsync.Show(parent, "Exportovat číselník pracovníků?", "Dotaz", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                var tcs_PE = new TaskCompletionSource<bool>();
                                var TE = ciselnik.KatalogCiselnik(tcs_PE, parent, Operation.ExportPracovnici, "Probíhá export číselníku pracovníků");
                                var resE = await TE;
                            }
                        }

                        var tcs_P = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_P, parent, Operation.Pracovnici, "Stahuje se číselník pracovníků");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                    #region OdvadeniVyroby
                    try
                    {


                        var tcs_OV = new TaskCompletionSource<bool>();
                        var T = ciselnik_Vyroba.KatalogVyroba(tcs_OV, parent, VyrobaServiceOperations.Operation.OdvadeniVyroby, "Stahuje se číselník odvádění výroby");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                    #region Production
                    try
                    {


                        var tcs_P = new TaskCompletionSource<bool>();
                        var T = ciselnik_Vyroba.KatalogProduction(tcs_P, parent, VyrobaServiceOperations.Operation.Production, "Stahuje se production");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                    #region InternalState
                    try
                    {


                        var tcs_I = new TaskCompletionSource<bool>();
                        var T = ciselnik_Vyroba.KatalogInternalState(tcs_I, parent, VyrobaServiceOperations.Operation.InternalState, "Stahuje se InternalState");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion



                }
                else if (typ == Operation.Zbozi)
                {
                    #region Zbozi
                    try
                    {

                        if (Konfigurace_Singleton.Instance.ExportZbozi)
                        {

                            var dr = await MessageBoxAsync.Show(parent, "Exportovat číselník zboží?", "Dotaz", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                var tcs_ZE = new TaskCompletionSource<bool>();
                                var TE = ciselnik.KatalogCiselnik(tcs_ZE, parent, Operation.ExportZbozi, "Probíhá export číselníku zboží", SkladFilter);
                                var resE = await TE;
                            }
                        }

                        var tcs_Z = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_Z, parent, Operation.Zbozi, "Stahuje se číselník zboží", SkladFilter);
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                }
                else if (typ == Operation.Odberatele)
                {
                    #region Odberatele
                    try
                    {

                        if (Konfigurace_Singleton.Instance.ExportOdberatele)
                        {
                            var dr = await MessageBoxAsync.Show(parent, "Exportovat číselník odběratelů?", "Dotaz", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                var tcs_OE = new TaskCompletionSource<bool>();
                                var TE = ciselnik.KatalogCiselnik(tcs_OE, parent, Operation.ExportOdberatele, "Probíhá export číselníku odběratelů");
                                var resE = await TE;
                            }
                        }

                        var tcs_O = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_O, parent, Operation.Odberatele, "Stahuje se číselník odběratelů", SkladFilter);
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                }
                else if (typ == Operation.Strediska)
                {
                    #region Strediska

                    try
                    {

                        if (Konfigurace_Singleton.Instance.ExportStrediska)
                        {
                            var dr = await MessageBoxAsync.Show(parent, "Exportovat číselník středisek?", "Dotaz", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                var tcs_STE = new TaskCompletionSource<bool>();
                                var TE = ciselnik.KatalogCiselnik(tcs_STE, parent, Operation.ExportStrediska, "Probíhá export číselníku středisek");
                                var resE = await TE;
                            }
                        }

                        var tcs_ST = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_ST, parent, Operation.Strediska, "Stahuje se číselník středisek", SkladFilter);
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }

                    #endregion

                }
                else if (typ == Operation.TypDokladu)
                {
                    #region TypDokladu
                    try
                    {
                        var tcs_TD = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_TD, parent, Operation.TypDokladu, "Stahuje se číselník typů dokladů", SkladFilter);
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                }
                else if (typ == Operation.Sklady)
                {
                    #region Sklady
                    try
                    {

                        if (Konfigurace_Singleton.Instance.ExportSklady)
                        {
                            var dr = await MessageBoxAsync.Show(parent, "Exportovat číselník skladů?", "Dotaz", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                var tcs_SE = new TaskCompletionSource<bool>();
                                var TE = ciselnik.KatalogCiselnik(tcs_SE, parent, Operation.ExportSklady, "Probíhá export číselníku skladů");
                                var resE = await TE;
                            }
                        }

                        var tcs_S = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_S, parent, Operation.Sklady, "Stahuje se číselník skladů");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                }
                else if (typ == Operation.Pracovnici)
                {
                    #region Pracovnici
                    try
                    {

                        if (Konfigurace_Singleton.Instance.ExportPracovnici)
                        {
                            var dr = await MessageBoxAsync.Show(parent, "Exportovat číselník pracovníků?", "Dotaz", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                var tcs_PE = new TaskCompletionSource<bool>();
                                var TE = ciselnik.KatalogCiselnik(tcs_PE, parent, Operation.ExportPracovnici, "Probíhá export číselníku pracovníků");
                                var resE = await TE;
                            }
                        }

                        var tcs_P = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_P, parent, Operation.Pracovnici, "Stahuje se číselník pracovníků");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                }
                else if (typ == Operation.Lokace)
                {
                    #region Lokace
                    try
                    {

                        var tcs_L = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_L, parent, Operation.Lokace, "Stahuje se číselník lokací");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                }
                else if (typ == Operation.Meny)
                {
                    #region Meny
                    try
                    {

                        if (Konfigurace_Singleton.Instance.ExportMeny)
                        {
                            var dr = await MessageBoxAsync.Show(parent, "Exportovat číselník měn?", "Dotaz", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                var tcs_ME = new TaskCompletionSource<bool>();
                                var TE = ciselnik.KatalogCiselnik(tcs_ME, parent, Operation.ExportMeny, "Probíhá export číselníku měn");
                                var resE = await TE;
                            }
                        }

                        var tcs_M = new TaskCompletionSource<bool>();
                        var T = ciselnik.KatalogCiselnik(tcs_M, parent, Operation.Meny, "Stahuje se číselník měn");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                }
                else if (typ == Operation.OdvadeniVyroby)
                {
                    #region OdvadeniVyroby
                    try
                    {


                        var tcs_OV = new TaskCompletionSource<bool>();
                        var T = ciselnik_Vyroba.KatalogVyroba(tcs_OV, parent, VyrobaServiceOperations.Operation.OdvadeniVyroby, "Stahuje se číselník odvádění výroby");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                }
                else if (typ == Operation.Production)
                {
                    #region Production
                    try
                    {


                        var tcs_P = new TaskCompletionSource<bool>();
                        var T = ciselnik_Vyroba.KatalogProduction(tcs_P, parent, VyrobaServiceOperations.Operation.Production, "Stahuje se production");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                }
                else if (typ == Operation.InternalState)
                {
                    #region InternalState
                    try
                    {


                        var tcs_I = new TaskCompletionSource<bool>();
                        var T = ciselnik_Vyroba.KatalogInternalState(tcs_I, parent, VyrobaServiceOperations.Operation.InternalState, "Stahuje se InternalState");
                        var res = await T;
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                    }
                    #endregion

                }

                tcs.SetResult(true);

                return tcs.Task.Result;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                Classes.ProgressDialog_Infinity.Dispose();
                await MessageBoxAsync.Show(parent, ex.Message, "Error", MessageBoxButtons.OK);
                tcs.SetException(ex);
                return false;
            }
            finally
            {
                Classes.ProgressDialog_Infinity.Dispose();
            }
        }

        #region Co k sebe patri


        //public static TaskCompletionSource<bool> tcs;

        public Task<bool> TEST_Zbozi(
        Android.Support.V7.App.AppCompatActivity _parent,
            string SkladFilter
            , string description = "Stahuje se číselník zboží")
        {

            //tcs = null;
            var tcs = new TaskCompletionSource<bool>();

            try
            {
                //var description = "Stahuje se číselník zboží";

                Classes.ProgressDialog_Infinity.Show(_parent, description);

                Task.Run(() => { finishingtool(tcs, description); });

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                tcs.SetException(ex);
            }

            return tcs.Task;
        }

        private async void finishingtool(
            TaskCompletionSource<bool> tcs, string description
            )
        {
            int a = 0;
            while (a < 10)
            {
                await Task.Delay(1000);
                //Thread.Sleep(1000);
                Classes.ProgressDialog_Infinity.Message = $"{description} A: {a}";
                a++;
            }
            tcs.SetResult(true);
        }

        #endregion

    }
}