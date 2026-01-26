using Fask.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes
{
    public class Inventura
    {

        public bool AlgoritmusDoplneniNeznamejSarze(SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable dsInv, string DIP)
        {
            try
            {
                TracId tracid = new TracId(null, null, null, "AlgoritmusDoplneniNeznamejSarze");
                Trac.Write("Start pred foreach", tracid);
                foreach (SQL_Datasets.Inventura.CZMST_I4_GrupaRow item in dsInv)
                {


                    if (!string.IsNullOrEmpty(item.SERLNMBR))
                    {
                        //šarže/sn je vyplnena
                        string ID_Sarze = string.Empty;

                        DateTime? exp = item.IsExpiraceNull() ? (DateTime?)null : item.Expirace;

                        Trac.Write("Start Get ID šarže", tracid);
                        ID_Sarze = Database.ABRA.Get_ID_StoreBatch(item.SERLNMBR.Trim(), null,  item.ITEMNMBR.Trim());
                        Trac.Write("Stop Get ID šarže: '" + ID_Sarze+ "'", tracid);

                        if (string.IsNullOrEmpty(ID_Sarze))
                        {
                            Trac.Write("Start šarže NEnalezena v skladu", tracid);
                            //šarže NEnalezena v skladu
                            bool? SN = null;
                            if (item.CZ_SerNum_Track == 1)
                                SN = true;
                            else if (item.CZ_SerNum_Track == 2)
                                SN = false;

                            Trac.Write("Start Create_Sarzi", tracid);
                            string state = Fask.Module.ABRA.SAB.Classes.ABRA.Create_Sarzi(item.ITEMNMBR.Trim(), item.SERLNMBR.Trim(), exp, SN, out ID_Sarze);
                            Trac.Write("Stop Create_Sarzi:'" + state + "'" + "ID Sarze:'" + ID_Sarze + "'", tracid);

                            if (string.IsNullOrEmpty(ID_Sarze))
                            {
                                throw new Exception("Nenalezeno ID nové šarže");
                            }

                            Trac.Write("Stop šarže NEnalezena v skladu: '" + ID_Sarze + "'", tracid);
                        }
                        else
                        {

                            Trac.Write("Start CheckExpirace", tracid);
                            ID_Sarze = Fask.Module.ABRA.SAB.Classes.ABRA.CheckExpirace(item, exp);
                            Trac.Write("Stop CheckExpirace: '" + ID_Sarze + "'", tracid);
                            if (string.IsNullOrEmpty(ID_Sarze))
                            {
                                throw new Exception("Nenalezeno ID nové šarže");
                            }
                        }

                        //V temto momente už znám ID šarže, budto sem si ho vytvoril anebo ho mám z seznamu...
                        Trac.Write("V temto momente už znám ID šarže, budto sem si ho vytvoril anebo ho mám z seznamu...", tracid);

                        Trac.Write("Start Get_ID_HIPBatch ", tracid);
                        string ID_HIPBatch = Database.ABRA.Get_ID_HIPBatch(DIP, item.SKL_ID.Trim(), item.ITEMNMBR.Trim(), ID_Sarze);
                        Trac.Write("Stop Get_ID_HIPBatch: '" + ID_HIPBatch + "'", tracid);


                        if (string.IsNullOrEmpty(ID_HIPBatch))
                        {

                            Trac.Write("Start Get_ID_HIPRow ", tracid);
                            string ID_HIPRow = Database.ABRA.Get_ID_HIPRow(DIP, item.SKL_ID.Trim(), item.ITEMNMBR.Trim());
                            Trac.Write("Stop Get_ID_HIPRow :'" + ID_HIPRow + "'", tracid);

                            if (string.IsNullOrEmpty(ID_HIPRow))
                            {
                                Trac.Write("FUJ ?ZLE? Stop Get_ID_HIPRow: '" + ID_HIPRow + "'", tracid);
                                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, string.Format("Upozornení nezname šarze(Get_ID_HIPRow), nenalezeno pro DIP: '{0}' pro SKL_ID: '{1}' pro ITEMNMBR: '{2}'", DIP, item.SKL_ID, item.ITEMNMBR));
                                continue;
                            }

                            string MJ = item.MJ.Trim();

                            Trac.Write("Start Create_HIPBatch ", tracid);
                            string state = Fask.Module.ABRA.SAB.Classes.ABRA.Create_HIPBatch(ID_HIPRow, MJ, ID_Sarze, out ID_HIPBatch);
                     
                            if (string.IsNullOrEmpty(ID_HIPBatch))
                            {
                                Trac.Write("FUJ ?ZLE? Stop Create_HIPBatch: '" + ID_HIPBatch + "'", tracid);
                                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, string.Format("Upozornení nezname šarze(Create_HIPBatch), nenalezeno pro ID_HIPRow: '{0}' pro MJ: '{1}' pro ID_Sarze: '{2}'", ID_HIPRow, MJ, ID_Sarze));
                                continue;
                            }

                            Trac.Write("Stop Create_HIPBatch:'" + state + "'", tracid);


                            if (state != "OK")
                                throw new Exception(state);
                        }

                        Trac.Write("V temto momente sem už vytvořil šarži v HIP ", tracid);
                        // v temto momente sem už vytvořil šarži v HIP

                        Trac.Write("Start Get_ID_DIPRow ", tracid);
                        string ID_DipRow = Database.ABRA.Get_ID_DIPRow(DIP, item.SKL_ID, item.ITEMNMBR);

                        if (string.IsNullOrEmpty(ID_DipRow))
                        {
                            Trac.Write("FUJ ?ZLE? Stop Get_ID_DIPRow: '" + ID_DipRow + "'", tracid);
                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, string.Format("Upozornení nezname šarze (Get_ID_DIPRow), nenalezeno pro DIP: '{0}' pro SKL_ID: '{1}' pro ITEMNMBR: '{2}'", DIP, item.SKL_ID, item.ITEMNMBR));
                            continue;
                        }

                        Trac.Write("Stop Get_ID_DIPRow :'"+ ID_DipRow + "'", tracid);

                        Trac.Write("Start Create_DIPBatch ", tracid);
                        string st = Fask.Module.ABRA.SAB.Classes.ABRA.Create_DIPBatch(ID_DipRow, ID_HIPBatch, item.QUANTITY, item.MJ);
                        Trac.Write("Stop Create_DIPBatch:'" + st + "'", tracid);

                        if (st != "OK")
                            throw new Exception(st);

                        //TaD 10.6.2020 Fuj... v foreach nesmí byt return !!! 
                        //return true;

                    }
                    else
                    {
                        // šarže/sn neni zadana... i když je položka sledovana na šarže
                        //Bude to Zalogovani, ale položka bude přeskočena
                        string txt = string.Format(DateTime.Now.ToString("G") + " : Položka '{0}' s track:'{1}' na DIP:'{2}' nemá vyplnenou šarži i když je sledovaná na šarže." + Environment.NewLine, item.ITEMNMBR, item.CZ_SerNum_Track, DIP);
                        Constants.SaveToFile.Save(txt, "Inv", "Inventura_Warning", ".txt");

                    }
                }

                Trac.Write("Stop po foreach", tracid);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            return true;
        }


    }

    /// <summary>
    /// Pomocna třida ktera nese inforace o položce, misto objetu DataRow anebo DataTable....
    /// </summary>
    public class Polozky_Inv
    {
        public string DIP_Desc;
        public string ITEMNMBR;
        public string LOCNCODE;
        public string SKL_ID;
        public decimal? QUANTITY; // celkove QTY pro položku
        public string SERLNMBR;
        public decimal? QTY_SERLNMBR; // QTY k jednej šarži nebo SN
        public int? USERID;
        public byte CZ_SerNum_Track;
    }
}
