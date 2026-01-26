using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Fask.Interfaces.Classes;
using Fask.Logging;
using Fask.ModulePohodaXML.Pohoda_DataSets;
using System.IO;
using Fask.ModulePohodaXML.Classes;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider : Fask.Interfaces.Vydej.IVydej2,
        Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneHlavicky,
        Fask.Interfaces.Vydej.IVydej2_GetHlavickaByCountEntries,
        Fask.Interfaces.Vydej.IVydej2_GetHlavicky,
        Fask.Interfaces.Vydej.IVydej2_UpdatePriorityDavka_byPriority,
        Fask.Interfaces.Vydej.IVydej2_UpdatePriorityDavka_byUserID,
        Fask.Interfaces.Vydej.IVydej2_GenerateDavka,
        Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavky,
        Fask.Interfaces.Vydej.IVydej2_UpdateSE,
        Fask.Interfaces.Vydej.IVydej2_InsertSE,
        Fask.Interfaces.Vydej.IVydej2_DeleteSE,
        Fask.Interfaces.Vydej.IVydej2_UvolnitDavku,
        Fask.Interfaces.Vydej.IVydej2_KontrolaDavky,
        Fask.Interfaces.Vydej.IVydej2_UpdateSE_QTY_ByDEXROWID,
        Fask.Interfaces.Vydej.IVydej2_GetAdresaOdberatel,
        Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavkySI,
        Fask.Interfaces.Vydej.IVydej2_KontrolaDavky_TEST,
        Fask.Interfaces.Vydej.IVydej2_Get_SE_MaxCountEntries,
        Fask.Interfaces.Vydej.IVydej2_InsertSI,
        Fask.Interfaces.Vydej.IVydej2_UpdateSE_storno,
        Fask.Interfaces.Vydej.IVydej2_DeleteSI,
        Fask.Interfaces.Vydej.IVydej2_UpdateSI
    {

        #region IVydej2_GetHlavicky Members

        Fask.Interfaces.DataSets.Vydej Fask.Interfaces.Vydej.IVydej2_GetHlavicky.GetHlavicky()
        {
            Fask.Interfaces.Filtry.VydejHlavickyFiltr filtr = new Fask.Interfaces.Filtry.VydejHlavickyFiltr();
            return GetFiltrovaneHlavicky(filtr);
        }

        #endregion

        #region IVydej2_GetFiltrovaneHlavicky Members

        public Fask.Interfaces.DataSets.Vydej GetFiltrovaneHlavicky(Fask.Interfaces.Filtry.VydejHlavickyFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Vydej dsVydej = new Fask.Interfaces.DataSets.Vydej();
            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                command.CommandText =
                    "SELECT A.COUNTENTRIES, A.PRIORITY, A.SOPNUMBE, B.CNTITEMS CNTITEMS, C.QTYSHPPDSUM SUMITEMS, D.ROZPRACOVANO ROZPRACOVANO, E.CZ_Doslo, A.USERID, MIN(uzivatele.USERID) as LOGIN, MIN(uzivatele.FIRSTNAME) as FIRSTNAME, MIN(uzivatele.surname) as SECONDNAME " +
                    "from " +
                    Fask.SQL.Constants.Common.TABLE_CZMST_SE + " as A " +
                    "INNER JOIN " +
                    "( " +
                    "   SELECT X.COUNTENTRIES, X.SOPNUMBE, COUNT(X.ITEMNMBR) CNTITEMS " +
                    "   FROM ( " +
                    "   SELECT COUNTENTRIES, SOPNUMBE, ITEMNMBR " +
                    "   FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SE +
                    "   WHERE QTYPACK=0 " +
                    "   GROUP BY COUNTENTRIES, SOPNUMBE, ITEMNMBR " +
                    "   ) X " +
                    "   GROUP BY X.COUNTENTRIES, X.SOPNUMBE " +
                    ") B ON " +
                    "B.COUNTENTRIES=A.COUNTENTRIES " +
                    "AND B.SOPNUMBE=A.SOPNUMBE " +
                    "INNER JOIN " +
                    "( " +
                    "   SELECT COUNTENTRIES, SOPNUMBE, SUM(QTYSHPPD) AS QTYSHPPDSUM " +
                    "   FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SE +
                    "   WHERE QTYPACK=0 " +
                    "   GROUP BY COUNTENTRIES, SOPNUMBE	" +
                    ") C ON " +
                    "C.COUNTENTRIES=A.COUNTENTRIES " +
                    "AND C.SOPNUMBE=A.SOPNUMBE " +
                    "INNER JOIN " +
                    "( " +
                    "   SELECT COUNTENTRIES, SOPNUMBE, CZ_Doslo" +
                    "   FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SE +
                    "   GROUP BY COUNTENTRIES, SOPNUMBE	, CZ_Doslo " +
                    ") E ON " +
                    "E.COUNTENTRIES=A.COUNTENTRIES  " +
                    "AND E.SOPNUMBE=A.SOPNUMBE " +
                    "LEFT OUTER JOIN " +
                    "(" +
                    "   SELECT COUNTENTRIES, SOPNUMBE, COUNT(*) AS ROZPRACOVANO FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SE +
                    "   GROUP BY COUNTENTRIES, SOPNUMBE " +
                    ") D ON " +
                    "D.COUNTENTRIES=A.COUNTENTRIES " +
                    "and D.SOPNUMBE=A.SOPNUMBE " +
                    "LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " uzivatele on uzivatele.USERID = A.USERID " +
                    "WHERE " +
                    "1=1 "
                    ;

                // CountEntries
                if (filtr.CountEntries != null && !string.IsNullOrEmpty(filtr.CountEntries.Trim()))
                {
                    command.CommandText += "AND A.CountEntries=@countentries ";
                    command.Parameters.AddWithValue("@countentries", filtr.CountEntries);
                }

                if (filtr.ZobrazitPouzeNestazeneDavky)
                {
                    command.CommandText += "AND A.CZ_Doslo=0 ";
                }

                // Sopnumbe
                if (filtr.Sopnumbe != null && !string.IsNullOrEmpty(filtr.Sopnumbe.Trim()))
                {
                    command.CommandText += "AND A.Sopnumbe=@sopnumbe ";
                    command.Parameters.AddWithValue("@sopnumbe", filtr.Sopnumbe);
                }

                // rozpracovano
                if (filtr.Rozpracovano != null && !string.IsNullOrEmpty(filtr.Rozpracovano.Trim()))
                {
                    command.CommandText += "AND A.Rozpracovano=@rozpracovano ";
                    command.Parameters.AddWithValue("@rozpracovano", filtr.Rozpracovano);
                }

                // priority
                if (filtr.Priority != null && !string.IsNullOrEmpty(filtr.Priority.Trim()))
                {
                    command.CommandText += "AND A.Priority=@priority ";
                    command.Parameters.AddWithValue("@priority", filtr.Priority);
                }

                command.CommandText +=
                    "GROUP BY A.PRIORITY, A.COUNTENTRIES, A.SOPNUMBE, B.CNTITEMS, C.QTYSHPPDSUM, D.ROZPRACOVANO, E.CZ_Doslo, A.USERID " +
                    "order by A.CountEntries desc"
                    ;

                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(dsVydej, dsVydej.Hlavicky.TableName);

                return dsVydej;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IVydej2_UpdatePriorityDavka_byUserID Members

        public bool UpdatePriorityDavka(int countentries, int? userID)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter taVydej = null;

            try
            {
                Globals_V1.LoadConfiguration();
                taVydej = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                taVydej.Connection = connection;
                taVydej.MyTransaction = trans;

                // kontrola CZ_Doslo, zdali je mozne pokracovat ...
                int? result= null;
                 object tmp = taVydej.GetCZDosloByCountEntries(countentries);

                if (tmp is int)
                    result = (int)tmp;

                // kontrola, zdali CZ_Doslo je nastaveno na hodnotu 0
                if (result.HasValue && result.Value == 0)
                {
                    // CZ_Doslo je na 0, pokracovat ...
                    taVydej.UpdatePriorityUserByCountEntries(userID, countentries);
                }
                else
                    throw new Exception("Prioritu je možné změnit pouze u nestažených dávek.");

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }

                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        #endregion

        #region IVydej2_UpdatePriorityDavka_byPriority Members

        public bool UpdatePriorityDavka(int countentries, byte priority)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter taVydej = null;

            try
            {
                Globals_V1.LoadConfiguration();
                taVydej = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                taVydej.Connection = connection;
                taVydej.MyTransaction = trans;

                // kontrola CZ_Doslo, zdali je mozne pokracovat ...
                int? result = null;
                object tmp = taVydej.GetCZDosloByCountEntries(countentries);

                if (tmp is int)
                    result = (int)tmp;

                // kontrola, zdali CZ_Doslo je nastaveno na hodnotu 0
                if (result.HasValue && result.Value == 0)
                {
                    // CZ_Doslo je na 0, pokracovat ...
                    taVydej.UpdatePriorityByCountEntries(priority, countentries);
                }
                else
                    throw new Exception("Prioritu je možné změnit pouze u nestažených dávek.");

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }

                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        #endregion

        #region IVydej2_GetHlavickaByCountEntries Members

        public Fask.Interfaces.DataSets.Vydej.HlavickyRow GetHlavickaByCountEntries(int CountEntries)
        {
            try
            {
                Fask.Interfaces.Filtry.VydejHlavickyFiltr filtr = new Fask.Interfaces.Filtry.VydejHlavickyFiltr();
                filtr.CountEntries = CountEntries.ToString();
                Fask.Interfaces.DataSets.Vydej dsVydej = GetFiltrovaneHlavicky(filtr);
                if (dsVydej != null && dsVydej.Hlavicky.Count > 0)
                    return dsVydej.Hlavicky.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IVydej2_GenerateDavka Members

        public Fask.Interfaces.Classes.StatusInfo Vydej_GenerateDavka(Fask.Interfaces.Classes.Objednavka objednavka, Fask.Interfaces.Classes.Sklad sklad, int? CountEntries)
        {
            Globals_V1.LoadConfiguration();
            //switch (statustoreturn)
            //{
            //    case 0: statusinfo = "OK"; break;
            //    case 1: statusinfo = "Již existuje"; break;
            //    case 2: statusinfo = "Neexistuje"; break;
            //    case 3: statusinfo = "Bylo nahráno"; break;
            //    default:
            //        statusinfo = "Neznámý status";
            //        break;
            //}
            Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 1");
            StatusInfo si = new StatusInfo();
            si.Description = "Vydej_GenerateDavka start";
            si.ID = 0;

            // TODO : generovat data dokladu na zaklade cisla dokladu ... 
            // 1) test, zda v SE existuje rozpracovany doklad ...
            // 2) Muze byt Faktura ze skladu nebo Prevodka ze skladu (Sklad musi byt uveden)
            // Dotazuje se pres xml, ale nejdrive zjisti dotazem, zda doklad existuje a je to 
            // a) fakttura
            // b) prevodka ze zvoleneho skladu

            #region Test code
            //Globals.LoadConfiguration(); //nacteni konfigurace
            //Datasets.DatabasePohodaTableAdapters.FATableAdapter ta_fa = new Datasets.DatabasePohodaTableAdapters.FATableAdapter();
            //ta_fa.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
            //var tbl_fa = ta_fa.GetDataBy_Typ_Vyrizeno(1, false);

            //Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();
            //StringBuilder s = new StringBuilder();
            //foreach (var row_fa in tbl_fa)
            //{
            //    s.AppendLine(row_fa.Cislo);
            //}
            //si.Description = s.ToString();
            //si.ID = tbl_fa.Count;
            #endregion

            // 1) test na rozpracovany doklad 
            Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 2");
            //Tady mi vrati pouze pokud je 
            //CZ_Doslo < 100  // Dávka je vygenerovana a připravena pro stažení do terminalu, anebo je už stažená v terminalu
            //CZ_Doslo = 255  // Dávka je vygenerovana a připravena pro uvolnení do terminalu
            byte? czdoslo = Database.Vydej.CZMSTSE_SOPNUMBER_CZDOSLO(objednavka.ID);
            Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 3");
            if (czdoslo.HasValue)
            { //existuje a je stazene v terminalu => vrati chybu ... nelze vytvorit duplicitu ...
                if (czdoslo.Value > 0)
                {

                    if (czdoslo.Value == 255)
                    {
                        si.ID = -1;
                        si.Description = string.Format("Existuje neuvolněná dávka pro doklad '{0}' ", objednavka.ID);
                        si.InnerException = new Exception(si.Description);
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.Warn, si.Description);
                        return si;
                    }
                    else
                    {
                        si.ID = -1;
                        si.Description = string.Format("Existuje rozpracovaná dávka pro doklad '{0}' na terminálu č.:{1}", objednavka.ID, czdoslo.Value);
                        si.InnerException = new Exception(si.Description);
                        Fask.Logging.ExceptionHandler2.Handle(LogLevel.Warn, si.Description);
                        return si;
                    }
                }
                else if (czdoslo.Value == 0) //je pripravena ke zpracovani => uzavrit ... 
                {
                    si.ID = -1;
                    si.Description = string.Format("Existuje připravená dávka pro doklad '{0}'", objednavka.ID);
                    si.InnerException = new Exception(si.Description);
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Warn, si.Description);
                    return si;
                    //Database.Vydej.CZMSTSE_UPDATE_CZDOSLO(objednavka.ID);
                }
                else if (czdoslo.Value < 0)
                {
                    //Tohle je principialne blbost.... lebo CZ_Doslo je byzte a byte je od 0 do 255

                    si.ID = -1;
                    si.Description = string.Format("Existuje rozpracovaná dávka:'{0}' na konzoli.", objednavka.ID);
                    si.InnerException = new Exception(si.Description);
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info,si.Description);
                    return si;
                }
            }


            Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 4");
            // Dotazeni ID skladu
            if (String.IsNullOrEmpty(sklad.ID))
                sklad.ID = Globals_V1.Konfigurace.Vydej[0].HlavnySkladID.ToString();
            Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 5");
            // 2)a) test zda je to faktura
            if (Database.Pohoda.FA_Exists(objednavka.ID))
            {
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 5a");
                // Pokud je na fakture polozka z jineho skladu, tak neumoznit provest export(vydej) 
                // => oznamit na uroven terminalu...
                if (!Database.Pohoda.FA_JedenSklad(objednavka.ID, sklad.ID))
                {
                    throw new Exception("Faktura '" + objednavka.ID + "' obsahuje položky z více skladů!");
                }
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 5b");
                string stav = this.ExportVydejkaPohoda_Z_Faktury_DirectAccess2DB(objednavka, sklad);
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 5c");
                if (stav != "OK")
                {
                    si.ID = -10;
                    si.Description = stav;
                    si.InnerException = new Exception(si.Description);
                    return si;
                }
                else
                {
                    si.ID = int.Parse(objednavka.CisloDavky);
                    si.Description = "OK";
                    si.InnerException = null;
                }
            }
            // 2)b) test zda je to prevodka
            else if (Database.Pohoda.Prevod_Exists(objednavka.ID))
            {
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 6a");
                // Pokud je na prevodce polozka z jineho skladu, tak neumoznit provest export(vydej) 
                // => oznamit na uroven terminalu...
                if (!Database.Pohoda.Prevod_JedenZdrojSklad(objednavka.ID, sklad.ID))
                {
                    throw new Exception("Převodka '" + objednavka.ID + "' obsahuje položky z jiných skladů!");
                }
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 6b");
                string stav = this.ExportVydejkaPohoda_Z_Prevodka(objednavka, sklad);
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 6c");
                if (stav != "OK")
                {
                    si.ID = -10;
                    si.Description = stav;
                    si.InnerException = new Exception(si.Description);
                    return si;
                }
                else
                {
                    si.ID = int.Parse(objednavka.CisloDavky);
                    si.Description = "OK";
                    si.InnerException = null;
                }
            }
            // 2)c) test zda je to vydejka
            else if (Database.Pohoda.Vydejka_Exists(objednavka.ID))
            {
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 7a");
                // Pokud je na vydejce polozka z jineho skladu, tak neumoznit provest export(vydej) 
                // => oznamit na uroven terminalu...
                if (!Database.Pohoda.Vydejka_JedenSklad(objednavka.ID, sklad.ID))
                {
                    throw new Exception("Výdejka '" + objednavka.ID + "' obsahuje položky z jiných skladů!");
                }
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 7b");
                string stav = this.ExportVydejkaPohoda_Z_Vydejky(objednavka, sklad);
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 7c");
                if (stav != "OK")
                {
                    si.ID = -10;
                    si.Description = stav;
                    si.InnerException = new Exception(si.Description);
                    return si;
                }
                else
                {
                    si.ID = int.Parse(objednavka.CisloDavky);
                    si.Description = "OK";
                    si.InnerException = null;
                }
            }
            // 2)d) test zda je to prodejka
            else if (Database.Pohoda.Prodejka_Exists(objednavka.ID))
            {
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 8a");
                // Pokud je na vydejce polozka z jineho skladu, tak neumoznit provest export(vydej) 
                // => oznamit na uroven terminalu...
                if (!Database.Pohoda.Prodejka_JedenSklad(objednavka.ID, sklad.ID))
                {
                    throw new Exception("Prodejka '" + objednavka.ID + "' obsahuje položky z jiných skladů!");
                }
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 8b");
                string stav = this.ExportVydejkaPohoda_Z_Prodejky(objednavka, sklad);
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 8c");
                if (stav != "OK")
                {
                    si.ID = -10;
                    si.Description = stav;
                    si.InnerException = new Exception(si.Description);
                    return si;
                }
                else
                {
                    si.ID = int.Parse(objednavka.CisloDavky);
                    si.Description = "OK";
                    si.InnerException = null;
                }
            }
            // 2)e) test zda je to prijata objednavka
            else if (Database.Pohoda.PrjateObjednavky_Exists(objednavka.ID))
            {

                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 9a");
                // Pokud je na vydejce polozka z jineho skladu, tak neumoznit provest export(vydej) 
                // => oznamit na uroven terminalu...
                if (!Database.Pohoda.PrjateObjednavky_JedenSklad(objednavka.ID, sklad.ID))
                {
                    throw new Exception("Výdejka '" + objednavka.ID + "' obsahuje položky z jiných skladů!");
                }
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 9b");
                string stav = this.ExportVydejkaPohoda_Z_PrjateObjednavky(objednavka, sklad, CountEntries);
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 9c");
                if (stav != "OK")
                {
                    si.ID = -10;
                    si.Description = stav;
                    si.InnerException = new Exception(si.Description);
                    return si;
                }
                else
                {
                    si.ID = int.Parse(objednavka.CisloDavky);
                    si.Description = "OK";
                    si.InnerException = null;
                }

            }
            // 2) nepodporovany typ dokladu
            else
            {
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 10a");
                si.Description = "Doklad '" + objednavka.ID + "' nenalezen.";
                if (sklad != null)
                    si.Description += "\nSklad " + sklad.ID;
                si.InnerException = new Exception(si.Description);
                si.ID = -10;
                throw new Exception(si.Description);
            }

            Fask.Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej_GenerateDavka: 11");
            return si;
        }



        #endregion

        #region Metody pro komunikaci s pohodu

        /// <summary>
        /// generuje davku z faktury a ulozi ji do czmst_se... 
        /// </summary>
        /// <param name="objednavka">nese id(cislo) dokladu(faktury)</param>
        /// <returns>OK = vse dobre, jinak nejaky jiny text k pripadnemu zobrazeni na terminalu</returns>
        public string ExportVydejkaPohoda_Z_Faktury_DirectAccess2DB(
            Objednavka objednavka
            , Sklad sklad
            )
        {
            try
            {

                if (String.IsNullOrEmpty(objednavka.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();


                if (pom != "OK")
                    return pom;

                Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
                if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                {
                    ParamTA = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
                    ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                }

                // 1) nacist hlavicku faktury
                // 2) nacist polozky faktury
                // 3) nacist doplnujici informace k polozkam
                // 4) ulozit do predlohy vydeje
                // 5) verifikace
                // - konec - 
                // pri chybe vratit informaci o chybe ...

                DatabasePohoda ds_pohoda = new DatabasePohoda();

                //Pohoda_DataSets.DatabasePohodaTableAdapters.FakturaCisloTableAdapter ta_pohoda_fakturacislo = new Pohoda_DataSets.DatabasePohodaTableAdapters.FakturaCisloTableAdapter();

                //ta_pohoda_fakturacislo.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

                // nacteni faktury a polozek faktury
                //ta_pohoda_fakturacislo.Fill(ds_pohoda.FakturaCislo, objednavka.ID);
                Database.Pohoda.FakturaCislo_FillByNoMSTParams(ds_pohoda.FakturaCislo, objednavka.ID);

                

                // dotazeni parametru pro locncode (vychozi lokaci ..)
                //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
                //SKzParametryTableAdapter.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

                // *************************************************
                // naplneni do czmst_se ... 
                // *************************************************
                Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                CZMST_SETableAdapter.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                Pohoda_DataSets.Vydej.CZMST_SEDataTable seTable = new Pohoda_DataSets.Vydej.CZMST_SEDataTable();
                Pohoda_DataSets.Vydej.CZMST_SERow seRow = null;

                int cisloDavky = 0;

                int SE_ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
                int SE_MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;




                foreach (var invoiceItem in ds_pohoda.FakturaCislo)
                {
                    // Pokud to neni skladova polozka, tak ji neresit ...
                    // TODO : otestovat, zda to takto je ... => tedy test na NULL hodnotu...
                    if ((invoiceItem == null) || (invoiceItem.IsSKz_IDNull()))
                        continue;

                    // dotazeni vychozi lokace pro zasobu
                    string locncodeDeafult = string.Empty;
                    if (invoiceItem != null)
                    {
                        if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                        {
                            //DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, invoiceItem.FApol_ID);
                            DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, invoiceItem.FApol_ID);
                            
                            DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
                            if (skzParametry_row != null)
                            {
                                locncodeDeafult = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
                            }
                        }
                    }

                    seRow = seTable.NewCZMST_SERow();

                    seRow.ITEMDESC = invoiceItem.FApol_SText; // nazev polozky => z xml <inv:text>
                    if (seRow.ITEMDESC.Length > SE_ITEMDESC_MaxLength)
                    {
                        seRow.ITEMDESC = seRow.ITEMDESC.Remove(SE_ITEMDESC_MaxLength);
                    }

                    seRow.ITEMNMBR = invoiceItem.SKz_ID.ToString(); // cislo polozky => z xml <inv:stockItem><typ:stockItem><typ:id>


                    ////MaR 11.12.2024 pridani dat na J
                    seRow.ITEMTYPE = "J";

                    //seRow.ITEMTYPE = ""; //bez typu
                    //seRow.ITEMCODE = ""; // ? ITEMCODE neni???
                    seRow.CountEntries = cisloDavky; // TODO : ??? nove cislo davky ... 

                    //seRow.CZ_CarKod = typPLU == null ? string.Empty : typPLU.Value; //carovy kod => dotahnout z xml PLU
                    seRow.CZ_CarKod = invoiceItem.IsSKz_IDSNull() ? string.Empty : invoiceItem.SKz_IDS.Trim();
                    seRow.CZ_DatVyr_Delka = 0; //?
                    seRow.CZ_DatVyr_Track = 0; //?
                    seRow.CZ_Doslo = 255; // pripraveno pro zpracovani
                    seRow.CZ_SerNum_Delka = 0; //? delka serioveho cisla 
                    //seRow.CZ_SerNum_Track = 0; //? sledovat na SN/Sarze => je sledovano na seriove cislo => zjistit v Pohoda
                    //seRow.CZ_SerNum_Track = (byte)(skz_row == null ? 0 : skz_row.RelSKzVC);
                    //seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack(
                    //    (invoiceItem == null) || (invoiceItem.IsSKz_RelSKzVCNull()) ? (int?)null : (int?)invoiceItem.SKz_RelSKzVC
                    //    );

                    //if (Properties.Settings.Default.POHODA_E1)
                    //{
                    //    if (invoiceItem.IsSKz_VPrFXTSNull())
                    //    {
                    //        seRow.CZ_SerNum_Track = 0;
                    //    }
                    //    else
                    //    {
                    //        if (invoiceItem.SKz_VPrFXTS)
                    //        {
                    //            seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((invoiceItem == null) || (invoiceItem.IsSKz_RefVPrFXTSNull()) ? (int?)null : ((int?)invoiceItem.SKz_RefVPrFXTS - 1), invoiceItem.SKz_VPrFXTS);
                    //        }
                    //        else
                    //        {
                    //            seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((invoiceItem == null) || (invoiceItem.IsSKz_RefVPrFVTSNull()) ? (int?)null : ((int?)invoiceItem.SKz_RefVPrFVTS - 1), invoiceItem.SKz_VPrFVTS);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((invoiceItem == null) || (invoiceItem.IsSKz_RelSKzVCNull()) ? (int?)null : (int?)invoiceItem.SKz_RelSKzVC, true);
                    //}

                    //TaD 1.8.2019 , přechod na FASK_ZASOBY_PARAMETRY

                    if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                    {
                        seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(invoiceItem.IsSKz_RelSKzVCNull() ? (int?)null : (int?)invoiceItem.SKz_RelSKzVC, invoiceItem);
                    }
                    else
                    {
                        Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
                        var dt_param = ParamTA.GetDataByITEMNMBR(invoiceItem.SKz_ID.ToString());

                        if ((dt_param != null) && (dt_param.Count > 0))
                        {
                            dt_row_param = dt_param.First();
                        }

                        seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(invoiceItem.IsSKz_RelSKzVCNull() ? (int?)null : (int?)invoiceItem.SKz_RelSKzVC, dt_row_param, Pohoda.TypAgendy.Vydej);
                    }
                    


                    seRow.CZ_SW_Delka = 0; //?
                    seRow.CZ_SW_Track = 0; //?
                    //seRow.DEX_ROW_ID
                    seRow.LOCNCODE = locncodeDeafult;
                    seRow.Note = ""; //? poznamka 
                    seRow.ORD = invoiceItem.FApol_ID; //int.Parse(invoiceItem.Element(inv + "id").Value.Trim()); //? poradi polozky na dokladu - pro pohoda id zaznamu v tabulce polozek <inv:id>
                    seRow.PRINTED = 0;
                    seRow.PRIORITY = 3; //? priorita ... 
                    seRow.QTYPACK = 0; // TODO : rozpad na varianty baleni dle car kodu ... 
                    seRow.QTYPAL = 0; //? palety neresime ... ???
                    seRow.QTYSHPPD = Convert.ToDecimal(invoiceItem.FApol_Mnozstvi); //decimal.Parse(invoiceItem.Element(inv + "quantity").Value.Trim(), System.Globalization.NumberFormatInfo.InvariantInfo); // mnozstvi k vydeji => <inv:quantity>
                    seRow.SKL_ID = invoiceItem.Skz_RefSklad.ToString(); //stockItem.Element(typ + "store").Element(typ + "id").Value.Trim(); //? id skladu => <inv:stockItem><typ:store><typ:id> : jde o identifikator skladu
                    seRow.SOPNUMBE = objednavka.ID;
                    seRow.TYPEPAL = ""; //? neresime palety ...
                    seRow.USERID = 0;
                    seRow.VNDDOCNM = ""; //? co by tady melo byt >>>
                    seRow.VNDITNUM = invoiceItem.IsSKz_EANNull() ? string.Empty : invoiceItem.SKz_EAN.Trim(); //typEAN == null ? string.Empty : typEAN.Value; //carovy kod zbozi ... => dotahnout z xml 
                    
                    
                    if (invoiceItem != null && !invoiceItem.IsFApol_MJNull())
                    {
                        seRow.MJ = invoiceItem.FApol_MJ;
                        if (seRow.MJ.Length > SE_MJ_MaxLength)
                        {
                            seRow.MJ = seRow.MJ.Remove(SE_MJ_MaxLength);
                        }
                    }
                    else
                    {
                        seRow.MJ = string.Empty;
                    }

                    seRow.CZ_REZ1_Track = 0; //?
                    seRow.CZ_REZ2_Track = 0; //?
                    // Nacist vychozi lokaci ...
                    // az tady, protoze musi mit nastaveny skl_id a itemnmbr...
                    Vydej.LocncodeFindAlgorithmVychozi(seRow);

                    //vlozit do se ...
                    seTable.AddCZMST_SERow(seRow);

                    if (Globals_V1.Konfigurace.Vydej[0].MerneJednotky_DotahovatDalsiVarianty)
                    {
                        // MJ2 a qtypack (pokud existuje)
                        // TODO: pridat mernou jednotku
                        if (invoiceItem != null && !invoiceItem.IsSkz_MJ2Null() && !invoiceItem.IsSkz_MJ2KoefNull())
                        {
                            // kopie zaznamu
                            Pohoda_DataSets.Vydej.CZMST_SERow seRow2 = seTable.NewCZMST_SERow();
                            seRow2.ItemArray = seRow.ItemArray.Clone() as object[];
                            seRow = seRow2;

                            seRow.QTYPACK = (decimal)invoiceItem.Skz_MJ2Koef;
                            seRow.MJ = invoiceItem.Skz_MJ2;
                            if (seRow.MJ.Length > SE_MJ_MaxLength)
                            {
                                seRow.MJ = seRow.MJ.Remove(SE_MJ_MaxLength);
                            }

                            seTable.AddCZMST_SERow(seRow);
                        }

                        // MJ3 a qtypack (pokud existuje)
                        if (invoiceItem != null && !invoiceItem.IsSkz_MJ3Null() && !invoiceItem.IsSkz_MJ3KoefNull())
                        {
                            // kopie zaznamu
                            Pohoda_DataSets.Vydej.CZMST_SERow seRow3 = seTable.NewCZMST_SERow();
                            seRow3.ItemArray = seRow.ItemArray.Clone() as object[];
                            seRow = seRow3;

                            seRow.MJ = invoiceItem.Skz_MJ3;
                            seRow.QTYPACK = (decimal)invoiceItem.Skz_MJ3Koef;
                            // TODO: osetrit delku
                            if (seRow.MJ.Length > SE_MJ_MaxLength)
                            {
                                seRow.MJ = seRow.MJ.Remove(SE_MJ_MaxLength);
                            }

                            seTable.AddCZMST_SERow(seRow);
                        }
                    }

                    seRow = null;
                }


                try
                {
                    // ulozit do se
                    CZMST_SETableAdapter.Connection.Open();
                    CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                    // prideleni cisla davky v transakci ..
                    cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries(CZMST_SETableAdapter.Connection, CZMST_SETableAdapter.Transaction);
                    cisloDavky += 1;
                    foreach (var item in seTable)
                    {
                        item.CountEntries = cisloDavky;

                        // porad je pouze pridana, nikoli zmenena po zmene countentries...
                        item.AcceptChanges();
                        item.SetAdded();
                    }
                    int updatedRows = CZMST_SETableAdapter.Update(seTable);
                    CZMST_SETableAdapter.Transaction.Commit();
                    objednavka.CisloDavky = cisloDavky.ToString();
                }
                catch (Exception ex)
                {

                Fask.Logging.ExceptionHandler2.Handle(ex);
                    try
                    {
                        CZMST_SETableAdapter.Transaction.Rollback();
                    }
                    catch { }
                    throw new Exception("Uložení načtených položek se nezdařilo!");
                }
                finally
                {
                    if ((CZMST_SETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                        CZMST_SETableAdapter.Connection.Close();
                }

                return "OK";
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error,"(Vydejka: " + objednavka.ID + ")");
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
                //return ex.Message;
            }


        }


        /// <summary>
        /// generuje davku z prodejky a ulozi ji do czmst_se... 
        /// </summary>
        /// <param name="objednavka">nese id(cislo) dokladu(prodejky)</param>
        /// <returns>OK = vse dobre, jinak nejaky jiny text k pripadnemu zobrazeni na terminalu</returns>
        public string ExportVydejkaPohoda_Z_Prodejky(
            Fask.Interfaces.Classes.Objednavka objednavka
            , Fask.Interfaces.Classes.Sklad sklad
            )
        {
            try
            {
                string filename;
                filename = XML.PohodaComunication.FilenameCompose(XML.PohodaComunication._export_prodejka + ".xml");

                if (String.IsNullOrEmpty(objednavka.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Vydej.CreateRequest_Prodejka_XML(filename, objednavka))
                    return "CHYBA";

                //pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(filename, saveToDB, objednavka, new Fask.Server.Interfaces.Classes.User());
                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                return Vydej.LoadResponse_Prodejka_XML(Path.GetFileName(responsefilename), objednavka);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error,"(Prodejka: " + objednavka.ID + ")");
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
                //return ex.Message;
            }
        }

        /// <summary>
        /// generuje davku z prevodky a ulozi ji do czmst_se... 
        /// </summary>
        /// <param name="objednavka">nese id(cislo) dokladu(prevodky)</param>
        /// <returns>OK = vse dobre, jinak nejaky jiny text k pripadnemu zobrazeni na terminalu</returns>
        public string ExportVydejkaPohoda_Z_Prevodka(
            Fask.Interfaces.Classes.Objednavka doklad
            , Fask.Interfaces.Classes.Sklad sklad
            )
        {
            try
            {
                string filename;
                filename = XML.PohodaComunication.FilenameCompose(XML.PohodaComunication._export_prevodka + ".xml");

                if (String.IsNullOrEmpty(doklad.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;
                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    return pom;

                if (!Vydej.CreateRequest_Prevodka_XML(filename, doklad))
                    return "CHYBA";

                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                return Vydej.LoadResponse_Prevodka_XML(Path.GetFileName(responsefilename), true, doklad);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error ,"(Vydejka z prevodky: " + doklad.ID + ")");
                Fask.Logging.ExceptionHandler2.Handle(ex);
                
                throw ex;
                //return ex.Message;
            }
        }


        /// <summary>
        /// generuje davku z vydejky a ulozi ji do czmst_se... 
        /// </summary>
        /// <param name="objednavka">nese id(cislo) dokladu(vydejky)</param>
        /// <returns>OK = vse dobre, jinak nejaky jiny text k pripadnemu zobrazeni na terminalu</returns>
        public string ExportVydejkaPohoda_Z_Vydejky(
            Fask.Interfaces.Classes.Objednavka objednavka
            , Fask.Interfaces.Classes.Sklad sklad
            )
        {
            try
            {
                string filename;
                filename = XML.PohodaComunication.FilenameCompose(XML.PohodaComunication._export_vydejka + ".xml");

                if (String.IsNullOrEmpty(objednavka.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Vydej.CreateRequest_Vydejka_XML(filename, objednavka))
                    return "CHYBA";

                //pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(filename, saveToDB, objednavka, new Fask.Server.Interfaces.Classes.User());
                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                return Vydej.LoadResponse_Vydejka_XML(Path.GetFileName(responsefilename), objednavka);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(Vydejka: " + objednavka.ID + ")");
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
                //return ex.Message;
            }
        }



        /// <summary>
        /// Metoda pro generovani předlohy na výdej
        /// </summary>
        /// <param name="objednavka"></param>
        /// <param name="sklad"></param>
        /// <param name="CountEntries">Předavany parametr číslo dávky, z duvodu slučovana</param>
        /// <returns></returns>
        private string ExportVydejkaPohoda_Z_PrjateObjednavky(
            Objednavka objednavka,
            Sklad sklad,
            int? CountEntries
            )
        {
            try
            {
                string filename;
                filename = XML.PohodaComunication.FilenameCompose(XML.PohodaComunication._export_PrjateObjednavky + ".xml");

                if (String.IsNullOrEmpty(objednavka.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Vydej.CreateRequest_PrjateObjednavky_XML(filename, objednavka))// CreateRequest_Vydejka_XML(filename, objednavka))
                    return "CHYBA";

                bool saveToDB = objednavka.ID != string.Empty ? true : false;

                //pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(filename, saveToDB, objednavka, new Fask.Server.Interfaces.Classes.User());
                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                return Vydej.LoadResponse_PrjateObjednavky_XML(Path.GetFileName(responsefilename), saveToDB, objednavka, CountEntries);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Error,"(Vydejka: " + objednavka.ID + ")" );
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
                //return ex.Message;
            }
        }

        #endregion

        #region IVydej2_GetFiltrovaneDavky Members

        #region 21.5.2025 zakomentovano a udelana nova metoda
        //public Fask.Interfaces.DataSets.Vydej GetFiltrovaneDavky(Fask.Interfaces.Filtry.VydejDavkyFiltr filtr)
        //{

        //    Globals_V1.LoadConfiguration();

        //    System.Data.SqlClient.SqlConnection connection = null;
        //    System.Data.SqlClient.SqlCommand command = null;
        //    System.Data.SqlClient.SqlDataAdapter adapter = null;

        //    Fask.Interfaces.DataSets.Vydej dsVydej = new Fask.Interfaces.DataSets.Vydej();
        //    try
        //    {
        //        connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        //        command = new SqlCommand();
        //        adapter = new SqlDataAdapter();

        //        command.CommandText =
        //            "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SE +
        //            " WHERE" +
        //            " 1=1 ";


        //        command.CommandText += " AND ( 1!=1 ";


        //        if (filtr.UvolneneDavky)
        //        {
        //            command.CommandText += "OR CZ_Doslo=0 ";
        //        }

        //        if (filtr.NEUvolneneDavky)
        //        {
        //            command.CommandText += "OR CZ_Doslo=255 ";
        //        }

        //        if (filtr.StazeneDavky)
        //        {
        //            command.CommandText += "OR ( CZ_Doslo > 0 AND CZ_Doslo < 100 ) ";
        //        }


        //        if (filtr.SpracovaneDavky)
        //        {
        //            command.CommandText += "OR ( CZ_Doslo > 100 AND CZ_Doslo < 200 ) ";
        //        }


        //        if (filtr.MrtveDavky)
        //        {
        //            command.CommandText += "OR CZ_Doslo=201 ";
        //        }

        //        command.CommandText += ")";

        //        //}




        //        if (filtr.CountEntries != null && !string.IsNullOrEmpty(filtr.CountEntries.Trim()))
        //        {
        //            command.CommandText += "AND CountEntries=@countentries ";
        //            command.Parameters.AddWithValue("@countentries", filtr.CountEntries);
        //        }



        //        // Sopnumbe
        //        if (filtr.Sopnumbe != null && !string.IsNullOrEmpty(filtr.Sopnumbe.Trim()))
        //        {
        //            command.CommandText += "AND Sopnumbe=@sopnumbe ";
        //            command.Parameters.AddWithValue("@sopnumbe", filtr.Sopnumbe);
        //        }

        //        if (filtr.ITEMNMBR != null && !string.IsNullOrEmpty(filtr.ITEMNMBR.Trim()))
        //        {
        //            command.CommandText += "AND ITEMNMBR=@ITEMNMBR ";
        //            command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR);
        //        }

        //        if (filtr.PouzeKladneMnozstvy)
        //        {
        //            command.CommandText += " AND QTYSHPPD > 0 ";
        //        }

        //        // rozpracovano
        //        //if (filtr.Rozpracovano != null && !string.IsNullOrEmpty(filtr.Rozpracovano.Trim()))
        //        //{
        //        //    command.CommandText += "AND A.Rozpracovano=@rozpracovano ";
        //        //    command.Parameters.AddWithValue("@rozpracovano", filtr.Rozpracovano);
        //        //}

        //        // priority
        //        //if (filtr.Priority != null && !string.IsNullOrEmpty(filtr.Priority.Trim()))
        //        //{
        //        //    command.CommandText += "AND A.Priority=@priority ";
        //        //    command.Parameters.AddWithValue("@priority", filtr.Priority);
        //        //}

        //        //command.CommandText +=
        //        //    "GROUP BY A.PRIORITY, A.COUNTENTRIES, A.SOPNUMBE, B.CNTITEMS, C.QTYSHPPDSUM, D.ROZPRACOVANO, E.CZ_Doslo, A.USERID " +
        //        //    "order by A.CountEntries desc"
        //        //    ;

        //        command.CommandText += "order by";

        //        if (Globals_V1.Konfigurace.Konzola[0].Vydej_GetFiltrovaneDavky_CountEntries_desc)
        //        {
        //            command.CommandText += " CountEntries desc";
        //        }
        //        else 
        //        {
        //            command.CommandText += " CountEntries asc";
        //        }

        //        if (Globals_V1.Konfigurace.Konzola[0].Vydej_GetFiltrovaneDavky_CountEntries_desc)
        //        {
        //            command.CommandText += " , DEX_ROW_ID desc";
        //        }
        //        else
        //        {
        //            command.CommandText += " , DEX_ROW_ID asc";
        //        }




        //        command.Connection = connection;
        //        adapter.SelectCommand = command;

        //        adapter.Fill(dsVydej, dsVydej.CZMST_SE.TableName);

        //        return dsVydej;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //} 
        #endregion


        public Fask.Interfaces.DataSets.Vydej GetFiltrovaneDavky(Fask.Interfaces.Filtry.VydejDavkyFiltr filtr)
        {
            Globals_V1.LoadConfiguration();
            var dsVydej = new Fask.Interfaces.DataSets.Vydej();

            string tableSE = Fask.SQL.Constants.Common.TABLE_CZMST_SE;
            string tableSI = "CZMST_SI";
            bool descOrder = Globals_V1.Konfigurace.Konzola[0].Vydej_GetFiltrovaneDavky_CountEntries_desc;

            #region MaR zakomentovano 16.6.2025
            //var queryBuilder = new StringBuilder();
            //queryBuilder.Append("SELECT SE.*, SI.QTYSHPPD AS MnozstviNasnimane, (SE.QTYSHPPD - SI.QTYSHPPD) AS MnozstviZbyva ");
            //queryBuilder.Append($"FROM {tableSE} SE ");
            //queryBuilder.Append($"LEFT JOIN {tableSI} SI ON SE.CountEntries = SI.CountEntries AND SE.ORD = SI.ORD ");
            //queryBuilder.Append("WHERE 1=1 "); 
            #endregion

            var queryBuilder = new StringBuilder();
            queryBuilder.Append($@"
        SELECT 
            SE.*, 
            ISNULL(Nasnimano.MnozstviNasnimane, 0) AS MnozstviNasnimane,
            SE.QTYSHPPD - ISNULL(Nasnimano.MnozstviNasnimane, 0) AS MnozstviZbyva
        FROM {tableSE} SE
        OUTER APPLY (
            SELECT SUM(SI.QTYSHPPD) AS MnozstviNasnimane
            FROM {tableSI} SI
            WHERE 
                SI.CountEntries = SE.CountEntries AND
                SI.ORD = SE.ORD AND
                SI.ITEMNMBR = SE.ITEMNMBR
        ) AS Nasnimano
        WHERE 1=1 ");



            var czDosloConditions = new List<string>();
            if (filtr.UvolneneDavky)
                czDosloConditions.Add("SE.CZ_Doslo = 0");
            if (filtr.NEUvolneneDavky)
                czDosloConditions.Add("SE.CZ_Doslo = 255");
            if (filtr.StazeneDavky)
                czDosloConditions.Add("SE.CZ_Doslo > 0 AND SE.CZ_Doslo < 100");
            if (filtr.SpracovaneDavky)
                czDosloConditions.Add("SE.CZ_Doslo > 100 AND SE.CZ_Doslo < 200");
            if (filtr.MrtveDavky)
                czDosloConditions.Add("SE.CZ_Doslo = 201");

            if (czDosloConditions.Count > 0)
                queryBuilder.Append("AND (" + string.Join(" OR ", czDosloConditions) + ") ");

            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(filtr.CountEntries))
            {
                queryBuilder.Append("AND SE.CountEntries = @countentries ");
                parameters.Add(new SqlParameter("@countentries", filtr.CountEntries));
            }

            if (!string.IsNullOrWhiteSpace(filtr.Sopnumbe))
            {
                queryBuilder.Append("AND SE.Sopnumbe = @sopnumbe ");
                parameters.Add(new SqlParameter("@sopnumbe", filtr.Sopnumbe));
            }

            if (!string.IsNullOrWhiteSpace(filtr.ITEMNMBR))
            {
                queryBuilder.Append("AND SE.ITEMNMBR = @ITEMNMBR ");
                parameters.Add(new SqlParameter("@ITEMNMBR", filtr.ITEMNMBR));
            }

            if (filtr.PouzeKladneMnozstvy)
            {
                queryBuilder.Append("AND SE.QTYSHPPD > 0 ");
            }




            var types = new List<string>();

            if (filtr.ITEMTYPE_J)
            {
                types.Add("J");
                types.Add(""); // pokud má být "" bráno jako alternativa k 'J'
            }

            if (filtr.ITEMTYPE_I) types.Add("I");
            if (filtr.ITEMTYPE_P) types.Add("P");
            if (filtr.ITEMTYPE_E) types.Add("E");
            if (filtr.ITEMTYPE_V) types.Add("V");
            if (filtr.ITEMTYPE_O) types.Add("O");

            // pokud je něco ve výběru, sestav podmínku
            if (types.Count > 0)
            {
                var joinedTypes = string.Join("','", types);
                queryBuilder.Append($"AND SE.ITEMTYPE IN ('{joinedTypes}') ");
            }






            // PriznakPolozky filtr
            if (filtr.PriznakPolozky_vykryte)
            {
                queryBuilder.Append("AND (SE.QTYSHPPD - ISNULL(SI.QTYSHPPD, 0)) <= 0 ");
            }
            else if (filtr.PriznakPolozky_nevykryte)
            {
                queryBuilder.Append("AND (SE.QTYSHPPD - ISNULL(SI.QTYSHPPD, 0)) > 0 ");
            }
            else if (filtr.PriznakPolozky_neplnene)
            {
                queryBuilder.Append("AND SI.QTYSHPPD IS NULL ");
            }

            // Filtr: SE.TypPolozky IN (@typPolozky0, @typPolozky1, ...)
            if (!string.IsNullOrWhiteSpace(filtr.TypPolozky))
            {
                var hodnoty = filtr.TypPolozky
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select((val, i) => new { ParamName = $"@typPolozky{i}", Value = val.Trim() })
                    .ToList();

                if (hodnoty.Count > 0)
                {
                    var podminky = string.Join(", ", hodnoty.Select(h => h.ParamName));
                    queryBuilder.Append($"AND SE.ITEMTYPE IN ({podminky}) ");

                    foreach (var h in hodnoty)
                        parameters.Add(new SqlParameter(h.ParamName, h.Value));
                }
            }

            // Filtr: SE.PriznakDavky IN (@priznakDavky0, @priznakDavky1, ...)
            if (!string.IsNullOrWhiteSpace(filtr.PriznakDavky))
            {
                var hodnoty = filtr.PriznakDavky
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select((val, i) => new { ParamName = $"@priznakDavky{i}", Value = val.Trim() })
                    .ToList();

                if (hodnoty.Count > 0)
                {
                    var podminky = string.Join(", ", hodnoty.Select(h => h.ParamName));
                    queryBuilder.Append($"AND SE.CZ_Doslo IN ({podminky}) ");

                    foreach (var h in hodnoty)
                        parameters.Add(new SqlParameter(h.ParamName, h.Value));
                }
            }




            queryBuilder.Append($"ORDER BY SE.CountEntries {(descOrder ? "DESC" : "ASC")}, SE.DEX_ROW_ID {(descOrder ? "DESC" : "ASC")}");

            using (var connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
            using (var command = new SqlCommand(queryBuilder.ToString(), connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                if (parameters.Count > 0)
                    command.Parameters.AddRange(parameters.ToArray());

                adapter.Fill(dsVydej, dsVydej.CZMST_SE.TableName);
            }

            return dsVydej;
        }


        #endregion

        #region IVydej2_UpdateSE Members

        public bool UpdateSE(Fask.Interfaces.DataSets.Vydej.CZMST_SERow SERow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                ta_se.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_se.MyTransaction = trans;

                ta_se.Update(SERow);

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        #endregion

        #region IVydej2_InsertSE Members

        public bool InsertSE(Fask.Interfaces.DataSets.Vydej.CZMST_SERow SERow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                ta_se.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_se.MyTransaction = trans;

                ta_se.Insert(SERow.CountEntries,
                    SERow.SOPNUMBE,
                    SERow.IsITEMNMBRNull() ? null : SERow.ITEMNMBR,
                    SERow.ITEMTYPE,
                    SERow.ITEMDESC,
                    SERow.IsVNDDOCNMNull() ? null : SERow.VNDDOCNM,
                    SERow.IsVNDITNUMNull() ? null : SERow.VNDITNUM,
                    SERow.ORD,
                    SERow.CZ_CarKod,
                    SERow.IsSKL_IDNull() ? null : SERow.SKL_ID,
                    SERow.IsLOCNCODENull() ? null : SERow.LOCNCODE,
                    SERow.MJ,
                    SERow.QTYSHPPD,
                    SERow.QTYPACK,
                    SERow.CZ_DatVyr_Track,
                    SERow.CZ_DatVyr_Delka,
                    SERow.CZ_SerNum_Track,
                    SERow.CZ_SerNum_Delka,
                    SERow.CZ_SW_Track,
                    SERow.CZ_SW_Delka,
                    SERow.CZ_Doslo,
                    SERow.IsNoteNull() ? null : SERow.Note,
                    SERow.IsTYPEPALNull() ? null : SERow.TYPEPAL,
                    SERow.IsQTYPALNull() ? (decimal?)null : SERow.QTYPAL,
                    SERow.PRIORITY,
                    SERow.IsPRINTEDNull() ? (byte?)null : SERow.PRINTED,
                    SERow.IsUSERIDNull() ? (int?)null : SERow.USERID,
                    //SERow.IsCZ_REZ1_TrackNull() ? (byte?)null : SERow.CZ_REZ1_Track,
                    //SERow.IsCZ_REZ2_TrackNull() ? (byte?)null : SERow.CZ_REZ2_Track,
                    SERow.IsCZ_REZ1_TrackNull() ? (byte)0 : SERow.CZ_REZ1_Track,
                    SERow.IsCZ_REZ2_TrackNull() ? (byte)0 : SERow.CZ_REZ2_Track,
                    SERow.IsITEMCODENull() ? null : SERow.ITEMCODE,
                    SERow.IsWEIGHTNull() ? (decimal?)null : SERow.WEIGHT
                    );

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        #endregion

        #region IVydej2_DeleteSE Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">DEX ROW ID</param>
        /// <returns></returns>
        public bool DeleteSE(int ID)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                ta_se.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_se.MyTransaction = trans;

                ta_se.Delete(ID);

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        #endregion

        #region IVydej2_UvolnitDavku Members

        public bool Vydej_UvolnitDavku(int CountEntries)
        {
            //System.Data.SqlClient.SqlTransaction trans = null;
            //System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                //connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                //connection.Open();

                //Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                //ta_se.Connection = connection;
                //trans = connection.BeginTransaction(IsolationLevel.Serializable);
                //ta_se.MyTransaction = trans;

                //ta_se.Update_CZDoslo_Nula_ByCountEntries(CountEntries);

                //if (trans != null)
                //    trans.Commit();

                //return true;

                return Database.Vydej.UpdateCzDosloByCountEntries(0, CountEntries);

            }
            catch(Exception ex)
            {
                //try
                //{
                //    if (trans != null)
                //        trans.Rollback();
                //}
                //catch { }
                //throw;
                throw ex;
                
            }
            //finally
            //{
                //if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                //    connection.Close();
            //}
        }

        #endregion

        #region IVydej2_KontrolaDavky Members

        public Fask.Interfaces.DataSets.Vydej KontrolaDavky(int CountEntries)
        {
            #region TaD 28.6.2019 Pouziti DLL pro Disponibilitu

            Fask.Interfaces.DataSets.Vydej dsVydej = new Fask.Interfaces.DataSets.Vydej();

            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter seta = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                seta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                Pohoda_DataSets.Vydej.CZMST_SEDataTable dtSE = new Pohoda_DataSets.Vydej.CZMST_SEDataTable();
                

                seta.FillByCountEntriesOrderBy_ITEMNMBR_ID(dtSE, CountEntries);

                Globals_V1.LoadConfiguration();

                Fask.POHODA.Disponibility.ValidateData dsDisp = new POHODA.Disponibility.ValidateData();

                OrderedEnumerableRowCollection<Pohoda_DataSets.Vydej.CZMST_SERow> dtSEOrder = dtSE.OrderBy(x => x.ITEMNMBR);

                foreach (Pohoda_DataSets.Vydej.CZMST_SERow item in dtSEOrder)
                {
                    Fask.POHODA.Disponibility.ValidateData.DataDispRow Row = dsDisp.DataDisp.NewDataDispRow();

                    Row.ITEMDESC = item.ITEMDESC;
                    Row.DEX_ROW_ID = item.DEX_ROW_ID;
                    Row.ITEMNMBR = item.ITEMNMBR;
                    Row.SKL_ID = item.SKL_ID;
                    Row.QTY = item.QTYSHPPD;
                    Row.SOPNUMBE = item.SOPNUMBE;
                    Row.ORD = item.ORD;
                    Row.SetSKz_RezerNull();
                    Row.SetSKz_StavZNull();
                    Row.SetOBJ_RezerNull();

                    dsDisp.DataDisp.AddDataDispRow(Row);
                }

                Fask.POHODA.Disponibility.CheckDisp disp = new Fask.POHODA.Disponibility.CheckDisp();
                Fask.POHODA.Disponibility.ValidateData.VydejKontrolaDataTable DTOut = disp.KontrolaDisponibilityDT(dsDisp, Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);


                foreach (var item in DTOut)
                {
                    dsVydej.VydejKontrola.ImportRow(item);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


                return dsVydej; 
            #endregion


            #region OLD
            //Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter seta = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
            //seta.Connection = new SqlConnection(Globals.ConnectionString);

            ////Pohoda_DataSets.DatabasePohodaTableAdapters.OBJTableAdapter OBJta = new Pohoda_DataSets.DatabasePohodaTableAdapters.OBJTableAdapter();
            ////OBJta.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

            ////Pohoda_DataSets.DatabasePohodaTableAdapters.OBJpolTableAdapter OBJPolta = new Pohoda_DataSets.DatabasePohodaTableAdapters.OBJpolTableAdapter();
            ////OBJPolta.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

            ////Pohoda_DataSets.DatabasePohodaTableAdapters.KontrolaTableAdapter Kta = new Pohoda_DataSets.DatabasePohodaTableAdapters.KontrolaTableAdapter();
            ////Kta.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);


            //Pohoda_DataSets.Vydej.CZMST_SEDataTable dtSE = new Pohoda_DataSets.Vydej.CZMST_SEDataTable();
            ////Pohoda_DataSets.DatabasePohoda.OBJDataTable dtOBJ = new DatabasePohoda.OBJDataTable();
            ////Pohoda_DataSets.DatabasePohoda.OBJpolDataTable dtOBJPol = new DatabasePohoda.OBJpolDataTable();

            //Fask.Interfaces.DataSets.Vydej dsVydej = new Fask.Interfaces.DataSets.Vydej();

            //try
            //{


            //    seta.FillByCountEntriesOrderBy_ITEMNMBR_ID(dtSE, CountEntries);

            //    if ((dtSE != null) && (dtSE.Count > 0))
            //    {

            //        string result;

            //        //if (false)
            //        //{
            //        //    var group = dtSE.GroupBy(x => x.ITEMNMBR);

            //        //    foreach (var item in group)
            //        //    {

            //        //    }


            //        //    var dtSOP = dtSE.GroupBy(x => x.SOPNUMBE);

            //        //    List<string> SopNumbeList = new List<string>();
            //        //    bool first = true;

            //        //    foreach (var item in dtSOP)
            //        //    {
            //        //        if (first)
            //        //        {

            //        //            string tmp = "(" + "'" + item.Key.Trim() + "'";
            //        //            SopNumbeList.Add(tmp);
            //        //            first = false;
            //        //        }
            //        //        else
            //        //            SopNumbeList.Add("'" + item.Key.Trim() + "'");
            //        //    }

            //        //    string a = SopNumbeList[SopNumbeList.Count - 1];
            //        //    SopNumbeList[SopNumbeList.Count - 1] = a + ")";

            //        //    result = String.Join(", ", SopNumbeList.ToArray());
            //        //}


            //        //if (SopNumbeList.Count > 0)
            //        //{
            //        //    string SOP = SopNumbeList[0];




            //        Fask.Interfaces.DataSets.Vydej.VydejKontrolaRow PrevRow = null;

            //        //decimal? RezervaJA = 0;
            //        //decimal RezervaOstatni = 0;

            //        #region pokus

            //        Fask.Interfaces.DataSets.Vydej.VydejKontrolaRow Radek = dsVydej.VydejKontrola.NewVydejKontrolaRow();

            //        bool Flag_PrevRow; // příznak zda se jedna o prvni zaznam 
            //        bool Flag_SameITEMNMBR;
            //        bool Flag_Rezervovano;

            //        decimal SKz_StavZ; // kompletny stav skladu pro danu položku
            //        decimal SKz_Rezer; // počet kolik je dohromady rezervovano na položke na sklade
            //        decimal SKz_ObjedP;  // počet kolik je dohromady na objednavkach .... ted se nepouživa

            //        decimal VolnePolozky; // jedna se o počet volnych položek ktere sou na sklade a nejsou rezervovany

            //        decimal? RezervovanoJA;
            //        decimal RezervovanoOstatni = 0;

            //        decimal PolozekZbude;

            //        decimal POhodaQTY;

            //        #endregion

            //        foreach (var item in dtSE)
            //        {


            //            //if (true)
            //            //{
            //            #region druhy pokus

            //            Radek = dsVydej.VydejKontrola.NewVydejKontrolaRow();

            //            Flag_PrevRow = false; // příznak zda se jedna o prvni zaznam 
            //            Flag_SameITEMNMBR = false;
            //            Flag_Rezervovano = false;

            //            SKz_StavZ = 0; // kompletny stav skladu pro danu položku
            //            SKz_Rezer = 0; // počet kolik je dohromady rezervovano na položke na sklade
            //            SKz_ObjedP = 0;  // počet kolik je dohromady na objednavkach .... ted se nepouživa

            //            VolnePolozky = 0; // jedna se o počet volnych položek ktere sou na sklade a nejsou rezervovany

            //            RezervovanoJA = null;
            //            RezervovanoOstatni = 0;

            //            PolozekZbude = 0;

            //            POhodaQTY = GetOBJPredlohaQTY(item.ORD);





            //            if (PrevRow != null)
            //            {
            //                Flag_PrevRow = true;

            //                if (item.ITEMNMBR.Trim() == PrevRow.ITEMNMBR.Trim())
            //                { Flag_SameITEMNMBR = true; }
            //                else
            //                { Flag_SameITEMNMBR = false; }
            //            }
            //            else
            //            {
            //                Flag_PrevRow = false;
            //                Flag_SameITEMNMBR = false;
            //            }

            //            //Pohoda_DataSets.DatabasePohoda.KontrolaDataTable KontrolaDT = Kta.GetData_ITEMNMBR_SOPNUMBE(item.SOPNUMBE.Trim(), int.Parse(item.ITEMNMBR.Trim()));
            //            Pohoda_DataSets.DatabasePohoda.KontrolaDataTable KontrolaDT = Database.Pohoda.Kontrola_GetData_ITEMNMBR_SOPNUMBE(item.SOPNUMBE.Trim(), int.Parse(item.ITEMNMBR.Trim()));


            //            if ((KontrolaDT != null) && (KontrolaDT.Count > 0))
            //            {
            //                SKz_StavZ = (decimal)KontrolaDT[0].SKz_StavZ;
            //                Flag_Rezervovano = KontrolaDT[0].OBJ_Rezer;
            //                SKz_Rezer = (decimal)KontrolaDT[0].SKz_Rezer;
            //                SKz_ObjedP = (decimal)KontrolaDT[0].SKz_ObjedP;
            //            }
            //            else
            //            {
            //                //Položka nenalezena ... preskakuju
            //                Log.Write("Položka nenalezena v Pohoda tabulkach...");
            //                continue;
            //            }

            //            VolnePolozky = SKz_StavZ - (decimal)SKz_Rezer;

            //            if (Flag_Rezervovano)
            //            {
            //                //RezervovanoJA = GetRezervovano(int.Parse(item.ITEMNMBR.Trim()), item.SOPNUMBE.Trim());
            //                RezervovanoJA = POhodaQTY;

            //                if (Flag_SameITEMNMBR)
            //                {
            //                    RezervovanoOstatni = PrevRow.REZ_OSTATNI - POhodaQTY;
            //                    PolozekZbude = PrevRow.ZBUDE;// -item.QTYSHPPD;
            //                }
            //                else
            //                {
            //                    RezervovanoOstatni = SKz_Rezer - POhodaQTY;
            //                    PolozekZbude = VolnePolozky;
            //                }

            //                //PolozekZbude = VolnePolozky;

            //                //if (RezervovanoJA != null)
            //                //{
            //                //if (Flag_SameITEMNMBR && !string.IsNullOrEmpty(PrevRow.REZ_JA))
            //                //{
            //                //    RezervovanoOstatni = SKz_Rezer - PrevRow.ZADAT;
            //                //}
            //                //else
            //                //{
            //                //    RezervovanoOstatni = SKz_Rezer - (decimal)RezervovanoJA;
            //                //}
            //                //}
            //                //else
            //                //{ Log.Write("Položka nenalezena v Pohoda tabulkach... "); }


            //            }
            //            else
            //            {
            //                RezervovanoJA = null;
            //                RezervovanoOstatni = SKz_Rezer;

            //                if (Flag_SameITEMNMBR) // && string.IsNullOrEmpty(PrevRow.REZ_JA))
            //                {
            //                    PolozekZbude = PrevRow.ZBUDE - item.QTYSHPPD;
            //                }
            //                else
            //                {
            //                    PolozekZbude = VolnePolozky - item.QTYSHPPD;
            //                }

            //            }

            //            Radek.DEX_ROW_ID = item.DEX_ROW_ID;
            //            Radek.ITEMDESC = item.ITEMDESC.Trim();
            //            Radek.ITEMNMBR = item.ITEMNMBR.Trim();
            //            Radek.QTYSHPPD = item.QTYSHPPD;
            //            Radek.CZ_CarKod = item.CZ_CarKod.Trim();
            //            Radek.REZ_JA = RezervovanoJA == null ? string.Empty : ((decimal)RezervovanoJA).ToString("0.00");
            //            Radek.REZ_OSTATNI = RezervovanoOstatni;
            //            Radek.SOPNUMBE = item.SOPNUMBE.Trim();
            //            Radek.STAV_SKLAD = SKz_StavZ;
            //            Radek.ZADAT = item.QTYSHPPD;
            //            Radek.ZBUDE = PolozekZbude;
            //            Radek.QTY_OBJ_Pohoda = (decimal)POhodaQTY;


            //            PrevRow = Radek;

            //            dsVydej.VydejKontrola.AddVydejKontrolaRow(Radek);



            //            #endregion
            //            //}

            //            #region Prvni pokus
            //            //else
            //            //{



            //            //    decimal Predloha = 0;
            //            //    decimal KDispozici = 0;
            //            //    bool OBJRezervovano = false;
            //            //    decimal SKzRezervovano = 0;
            //            //    decimal Sklad = 0;
            //            //    string ITEMNMBR = "";
            //            //    string ITEMDESC = "";
            //            //    string SOPNUMBE = "";
            //            //    int DEX_ROW_ID = 0;
            //            //    decimal Navrhloc = 0;
            //            //    decimal ObjednavkyAll = 0;





            //            //    Predloha = item.QTYSHPPD;        // CZMST_SE QTYSHPPD
            //            //    ITEMNMBR = item.ITEMNMBR.Trim(); // CZMST_SE ITEMNMBR
            //            //    ITEMDESC = item.ITEMDESC.Trim(); // CZMST_SE ITEMDESC
            //            //    DEX_ROW_ID = item.DEX_ROW_ID;    // CZMST_SE DEX_ROW_ID
            //            //    SOPNUMBE = item.SOPNUMBE.Trim(); // CZMST_SE SOPNUMBE
            //            //    Navrhloc = Predloha;


            //            //    //command.CommandText +=
            //            //    //    "select o.Rezer as OBJRezervace, s.Rezer as SKzRezervace, s.StavZ as Sklad " +
            //            //    //    "from OBJ as o " +
            //            //    //    "left join OBJPol as p ON p.RefAg = o.ID " +
            //            //    //    "left join SKz as s ON s.ID = p.RefSKz " +
            //            //    //    "where o.Cislo = '" + SOPNUMBE + "' AND s.ID = " + ITEMNMBR;


            //            //    //DataSet ds = new DataSet();

            //            //    //DataTable dt = new DataTable("StavSkladu");

            //            //    //System.Data.DataColumn columnSKzRezervovano = new DataColumn("SKzRezervace", typeof(decimal));
            //            //    //System.Data.DataColumn columnOBJRezervovano = new DataColumn("OBJRezervace", typeof(bool));
            //            //    //System.Data.DataColumn columnSklad = new DataColumn("Sklad", typeof(decimal));


            //            //    //dt.Columns.Add(columnSKzRezervovano);
            //            //    //dt.Columns.Add(columnOBJRezervovano);
            //            //    //dt.Columns.Add(columnSklad);

            //            //    //ds.Tables.Add(dt);




            //            //    Pohoda_DataSets.DatabasePohoda.KontrolaDataTable dt = Kta.GetData_ITEMNMBR_SOPNUMBE(SOPNUMBE, int.Parse(ITEMNMBR));
            //            //    //Kta.Fill_ITEMNMBR_SOPNUMBE(

            //            //    if ((dt != null) && (dt.Count > 0))
            //            //    {
            //            //        //Sklad = (decimal)ds.Tables["StavSkladu"].Rows[0]["Sklad"]; // SKz Mnozstvi
            //            //        //OBJRezervovano = (bool)ds.Tables["StavSkladu"].Rows[0]["OBJRezervace"];  // OBJ Rezer
            //            //        //SKzRezervovano = (decimal)ds.Tables["StavSkladu"].Rows[0]["SKzRezervace"];  // SKz Rezer

            //            //        Sklad = (decimal)dt[0].SKz_StavZ;
            //            //        OBJRezervovano = dt[0].OBJ_Rezer;
            //            //        SKzRezervovano = (decimal)dt[0].SKz_Rezer;
            //            //        ObjednavkyAll = (decimal)dt[0].SKz_ObjedP;

            //            //    }
            //            //    else
            //            //    {
            //            //        Sklad = 0; // SKz Mnozstvi mnozstvi na sklade
            //            //        SKzRezervovano = 0; // SKz Rezer pocet kolik je dohromady rezervovano
            //            //        OBJRezervovano = false; // OBJ Rezer priznak zda je rezervovano
            //            //        ObjednavkyAll = 0; // SKz ObjedP - objedvanky prijate
            //            //    }

            //            //    decimal volne = Sklad - (decimal)SKzRezervovano;

            //            //    if (PrevRow != null)
            //            //    {
            //            //        // jedna se o druhy anebo N zaznam

            //            //        if (item.ITEMNMBR.Trim() == PrevRow.ITEMNMBR.Trim())
            //            //        {
            //            //            //jedna se o druhy zaznam jiz existujici polozky

            //            //            if (OBJRezervovano)
            //            //            {
            //            //                if (RezervaJA == null)
            //            //                {
            //            //                    //Nemam rezervovano ale presto chci brat ... 
            //            //                    //neco je zle
            //            //                    Log.Write("Nemam rezervovano ale presto chci brat ... ");
            //            //                    throw new Exception("Nemam rezervovano ale presto chci brat z rezervovanych ... ");
            //            //                }
            //            //                else
            //            //                {
            //            //                    KDispozici = volne + ((decimal)RezervaJA - Predloha);
            //            //                }
            //            //            }
            //            //            else
            //            //            {
            //            //                KDispozici = volne - Predloha;
            //            //            }

            //            //            //KDispozici = PrevRow.KDispozici - Navrhloc;
            //            //        }
            //            //        else
            //            //        {
            //            //            //jedna se o prvni zaznam nove polozky

            //            //            //KDispozici = Sklad - SKzRezervovano;
            //            //            RezervaJA = GetRezervovanoList(ITEMNMBR, result);

            //            //            if (RezervaJA != null)
            //            //                RezervaOstatni = SKzRezervovano - (decimal)RezervaJA;
            //            //            else
            //            //                RezervaOstatni = SKzRezervovano;


            //            //            if (OBJRezervovano)
            //            //            {
            //            //                if (RezervaJA == null)
            //            //                {
            //            //                    //Nemam rezervovano ale presto chci brat ... 
            //            //                    //neco je zle
            //            //                    Log.Write("Nemam rezervovano ale presto chci brat ... ");
            //            //                    throw new Exception("Nemam rezervovano ale presto chci brat z rezervovanych ... ");
            //            //                }
            //            //                else
            //            //                {
            //            //                    KDispozici = volne + ((decimal)RezervaJA - Predloha);
            //            //                }
            //            //            }
            //            //            else
            //            //            {
            //            //                KDispozici = volne - Predloha;
            //            //            }
            //            //        }

            //            //    }
            //            //    else
            //            //    {
            //            //        //Prvni zaznam

            //            //        RezervaJA = GetRezervovanoList(ITEMNMBR, result);

            //            //        if (RezervaJA != null)
            //            //            RezervaOstatni = SKzRezervovano - (decimal)RezervaJA;
            //            //        else
            //            //            RezervaOstatni = SKzRezervovano;

            //            //        if (OBJRezervovano)
            //            //        {
            //            //            if (RezervaJA == null)
            //            //            {
            //            //                //Nemam rezervovano ale presto chci brat ... 
            //            //                //neco je zle
            //            //                Log.Write("Nemam rezervovano ale presto chci brat ... ");
            //            //                throw new Exception("Nemam rezervovano ale presto chci brat z rezervovanych ... ");
            //            //            }
            //            //            else
            //            //            {
            //            //                KDispozici = volne + ((decimal)RezervaJA - Predloha);
            //            //            }
            //            //        }
            //            //        else
            //            //        {
            //            //            KDispozici = volne - Predloha;
            //            //        }

            //            //        //KDispozici = Sklad - SKzRezervovano;
            //            //    }


            //            //    //RezervaJA = 


            //            //    //if (Predloha < KDispozici)
            //            //    //{

            //            //    //    Navrh = Predloha;
            //            //    //}
            //            //    //else
            //            //    //{
            //            //    //    Navrh = KDispozici - Predloha;
            //            //    //}

            //            //    //if (Navrh < 0)
            //            //    //{
            //            //    //    KDispozici = Navrh;
            //            //    //}
            //            //    //else 
            //            //    //{
            //            //    //    KDispozici = Sklad - Rezervovano;
            //            //    //}


            //            //    Fask.Interfaces.DataSets.Vydej.VydekKontrola1Row ROW = dsVydej.VydekKontrola1.NewVydekKontrola1Row();

            //            //    ROW.DEX_ROW_ID = DEX_ROW_ID;
            //            //    ROW.ITEMDESC = ITEMDESC;
            //            //    ROW.ITEMNMBR = ITEMNMBR;
            //            //    ROW.QTYSHPPD = Predloha;
            //            //    ROW.REZ_JA = RezervaJA == null ? "" : ((decimal)RezervaJA).ToString("0.00");
            //            //    //ROW.REZ_OSTATNI = SKzRezervovano - (RezervaJA == null ? 0 : (decimal)RezervaJA);
            //            //    ROW.REZ_OSTATNI = RezervaOstatni;
            //            //    ROW.SOPNUMBE = SOPNUMBE;
            //            //    ROW.STAV_SKLAD = Sklad;
            //            //    ROW.ZADAT = Navrhloc;
            //            //    ROW.ZBUDE = KDispozici;

            //            //    if (OBJRezervovano)
            //            //    {
            //            //        if (RezervaJA != null)
            //            //        {
            //            //            RezervaJA = (decimal)RezervaJA - Predloha;
            //            //        }
            //            //    }

            //            //    //ROW.Predloha = Predloha;
            //            //    //ROW.KDispozici = KDispozici;
            //            //    //ROW.OBJRezervace = OBJRezervovano;
            //            //    //ROW.SKzRezervace = SKzRezervovano;
            //            //    //ROW.Sklad = Sklad;
            //            //    //ROW.ITEMNMBR = ITEMNMBR;
            //            //    //ROW.ITEMDESC = ITEMDESC;
            //            //    //ROW.DEX_ROW_ID = DEX_ROW_ID;
            //            //    //ROW.Navrh = Navrhloc;
            //            //    //ROW.ITEMDESC = ITEMDESC;
            //            //    //ROW.SOPNUMBE = SOPNUMBE;
            //            //    //ROW.ObjednavkyAll = ObjednavkyAll;

            //            //    PrevRow = ROW;

            //            //    dsVydej.VydekKontrola1.AddVydekKontrola1Row(ROW);


            //            //    //}


            //            //    //OBJta.FillByCislo(dtOBJ, SOP.Trim());

            //            //    //if ((dtOBJ != null) && (dtOBJ.Count > 0))
            //            //    //{
            //            //    //    var ObjednavkaHlavicka = dtOBJ[0];
            //            //    //    bool Rezervace = ObjednavkaHlavicka.Rezer;
            //            //    //    int ID = ObjednavkaHlavicka.ID;

            //            //    //    OBJPolta.FillByRefAg(dtOBJPol, ID);

            //            //    //    if ((dtOBJPol != null) && (dtOBJPol.Count > 0))
            //            //    //    {

            //            //    //        foreach (var item in dtOBJPol)
            //            //    //        {


            //            //    //        }
            //            //    //    }
            //            //    //}
            //            //} 
            //            #endregion

            //        }
            //    }

            //    return dsVydej;

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            #endregion



        }

        private decimal? GetRezervovanoList(string ITEMNMBR, string SOPNUMBERList)
        {
            System.Data.OleDb.OleDbConnection connection = null;
            System.Data.OleDb.OleDbCommand command = null;
            //System.Data.OleDb.OleDbDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                command = new System.Data.OleDb.OleDbCommand();
                //adapter = new System.Data.OleDb.OleDbDataAdapter();


                command.CommandText = "select SUM(p.Mnozstvi) " +
                    " from OBJ as o " +
                    " left join OBJPol as p ON p.RefAg = o.ID " +
                    " left join SKz as s ON s.ID = p.RefSKz " +
                    " where o.Cislo in " + SOPNUMBERList +
                    " AND o.Rezer = 1 " +
                    " AND s.ID = " + ITEMNMBR +
                    " group by s.ID, s.Nazev";


                command.Connection = connection;
                //adapter.SelectCommand = command;


                connection.Open();

                object rez_jaOBJECT = command.ExecuteScalar();

                connection.Close();



                if (rez_jaOBJECT is double)
                {
                    return (decimal)((double)rez_jaOBJECT);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;

            }
        }


        private decimal? GetRezervovano(int ITEMNMBR, string SOPNUMBER)
        {
            System.Data.OleDb.OleDbConnection connection = null;
            System.Data.OleDb.OleDbCommand command = null;
            //System.Data.OleDb.OleDbDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                command = new System.Data.OleDb.OleDbCommand();
                //adapter = new System.Data.OleDb.OleDbDataAdapter();


                command.CommandText = "select SUM(p.Mnozstvi) " +
                    " from OBJ as o " +
                    " left join OBJPol as p ON p.RefAg = o.ID " +
                    " left join SKz as s ON s.ID = p.RefSKz " +
                    " where o.Cislo = '" + SOPNUMBER + "'" +
                    " AND o.Rezer = 1 " +
                    " AND s.ID = " + ITEMNMBR.ToString() +
                    " group by s.ID, s.Nazev";


                command.Connection = connection;
                //adapter.SelectCommand = command;


                connection.Open();

                object rez_jaOBJECT = command.ExecuteScalar();

                connection.Close();



                if (rez_jaOBJECT is double)
                {
                    return (decimal)((double)rez_jaOBJECT);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;

            }
        }


        private decimal GetOBJPredlohaQTY(int ORD)
        {
            System.Data.OleDb.OleDbConnection connection = null;
            System.Data.OleDb.OleDbCommand command = null;
            //System.Data.OleDb.OleDbDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                command = new System.Data.OleDb.OleDbCommand();
                //adapter = new System.Data.OleDb.OleDbDataAdapter();


                command.CommandText = "select Mnozstvi" +
                    " from OBJPol " +
                    " where ID = " + ORD.ToString();


                command.Connection = connection;
                //adapter.SelectCommand = command;


                connection.Open();

                object rez_jaOBJECT = command.ExecuteScalar();

                connection.Close();



                if (rez_jaOBJECT is double)
                {
                    return (decimal)((double)rez_jaOBJECT);
                }
                else
                {
                    throw new Exception("Nenalezena polozka na Objednavke...");
                    //return null;
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception("Nenalezena polozka na Objednavke...");
                //return null;

            }
        }


        #endregion

        #region IVydej2_UpdateSE_QTY_ByDEXROWID Members

        public bool UpdateSE_QTY_ByDEXROWID(decimal QTY, int DEXROWID)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
                ta_se.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_se.MyTransaction = trans;

                // ta_se.Update(SERow);
                ta_se.UpdateQTY(QTY, DEXROWID);

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        #endregion

        #region IVydej2_GetAdresy Members

        public Fask.Interfaces.DataSets.Vydej GetAdresaOdberatel(string SOPNUMBE)
        {
            System.Data.OleDb.OleDbConnection connection = null;
            System.Data.OleDb.OleDbCommand command = null;
            System.Data.OleDb.OleDbDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Vydej ds = new Fask.Interfaces.DataSets.Vydej();
            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                command = new System.Data.OleDb.OleDbCommand();
                adapter = new System.Data.OleDb.OleDbDataAdapter();

                //command.CommandText =   "SELECT " +
                //                        "Firma as Param_Odberatel_Firma, " +
                //                        "Ulice as Param_Odberatel_Adresa, " +
                //                        "PSC as Param_Odberatel_PSC, " +
                //                        "Obec as Param_Odberatel_Obec, " +
                //                        "ICO as Param_Odberatel_ICO, " +
                //                        "DIC as Param_Odberatel_DIC, " +
                //                        "Tel as Param_Odberatel_Telefon, " +
                //                        "Fax as Param_Odberatel_Fax, " +
                //                        "Email as Param_Odberatel_Email " +
                //                        "FROM OBJ " +
                //                        "WHERE Cislo = '" + SOPNUMBE.Trim() + "'";


                command.CommandText = "SELECT Firma, Ulice, PSC, Obec, ICO, DIC, Tel, Fax, Email," +
                                            " Firma2, Ulice2, PSC2, Obec2, Tel2, Email2" +
                                        " FROM OBJ" +
                                        " WHERE Cislo = '" + SOPNUMBE.Trim() + "'";


                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(ds, ds.OBJ_Adresy.TableName);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IVydej2_GetFiltrovaneDavkySI Members

        #region 22.5.2024 old MaR
        //public Fask.Interfaces.DataSets.Vydej GetFiltrovaneDavkySI(Fask.Interfaces.Filtry.VydejNasnimaneFiltr filtr)
        //{
        //    System.Data.SqlClient.SqlConnection connection = null;
        //    System.Data.SqlClient.SqlCommand command = null;
        //    System.Data.SqlClient.SqlDataAdapter adapter = null;

        //    Fask.Interfaces.DataSets.Vydej dsVydej = new Fask.Interfaces.DataSets.Vydej();
        //    try
        //    {
        //        Globals_V1.LoadConfiguration();
        //        connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        //        command = new SqlCommand();
        //        adapter = new SqlDataAdapter();

        //        command.CommandText =
        //            "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SI +
        //            " WHERE" +
        //            " 1=1 ";



        //        if (filtr.CountEntries != null && !string.IsNullOrEmpty(filtr.CountEntries.Trim()))
        //        {
        //            command.CommandText += "AND CountEntries=@countentries ";
        //            command.Parameters.AddWithValue("@countentries", filtr.CountEntries);
        //        }



        //        if (filtr.ITEMNMBR != null && !string.IsNullOrEmpty(filtr.ITEMNMBR.Trim()))
        //        {
        //            command.CommandText += "AND ITEMNMBR=@ITEMNMBR ";
        //            command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR);
        //        }

        //        if (filtr.SOPNUMBE != null && !string.IsNullOrEmpty(filtr.SOPNUMBE.Trim()))
        //        {
        //            command.CommandText += "AND SOPNUMBE=@SOPNUMBE ";
        //            command.Parameters.AddWithValue("@SOPNUMBE", filtr.SOPNUMBE);
        //        }



        //        command.CommandText += "order by";

        //        if (Globals_V1.Konfigurace.Konzola[0].Vydej_GetFiltrovaneDavky_CountEntries_desc)
        //        {
        //            command.CommandText += " CountEntries desc";
        //        }
        //        else
        //        {
        //            command.CommandText += " CountEntries asc";
        //        }

        //        if (Globals_V1.Konfigurace.Konzola[0].Vydej_GetFiltrovaneDavky_CountEntries_desc)
        //        {
        //            command.CommandText += " , DEX_ROW_ID desc";
        //        }
        //        else
        //        {
        //            command.CommandText += " , DEX_ROW_ID asc";
        //        }




        //        command.Connection = connection;
        //        adapter.SelectCommand = command;

        //        adapter.Fill(dsVydej, dsVydej.CZMST_SI.TableName);

        //        return dsVydej;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //} 
        #endregion

        public Fask.Interfaces.DataSets.Vydej GetFiltrovaneDavkySI(Fask.Interfaces.Filtry.VydejNasnimaneFiltr filtr)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Vydej dsVydej = new Fask.Interfaces.DataSets.Vydej();

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                //var sb = new StringBuilder();
                //sb.Append("SELECT SI.*, SE.ITEMTYPE, SE.ITEMDESC "); // rozšíříme SELECT o sloupce z druhé tabulky
                //sb.Append("FROM CZMST_SI SI ");
                //sb.Append("LEFT JOIN CZMST_SE SE ON SI.CountEntries  = SE.CountEntries  AND SI.ORD = SE.ORD ");
                //sb.Append("WHERE 1=1 ");

                var sb = new StringBuilder();

                //27.8.2025 MaR zakomentoval
                //sb.Append("SELECT SI.*, ");
                ////sb.Append("SI.ITEMCODE as kodPolozky, ");
                ////sb.Append("SE.ITEMCODE as ITEMCODE, ");
                //sb.Append("SE.ITEMTYPE, ");
                //sb.Append("SE.ITEMDESC ");
                //sb.Append("FROM CZMST_SI SI ");
                //sb.Append("LEFT JOIN CZMST_SE SE ON SI.CountEntries = SE.CountEntries AND SI.ORD = SE.ORD ");


                sb.Append("WITH Z AS (");
                sb.Append("    SELECT");
                sb.Append("        SE.CountEntries,");
                sb.Append("        SE.ORD,");
                sb.Append("        SE.ITEMTYPE,");
                sb.Append("        SE.ITEMDESC,");
                sb.Append("        ROW_NUMBER() OVER (");
                sb.Append("            PARTITION BY SE.CountEntries, SE.ORD");
                sb.Append("            ORDER BY SE.ORD ASC");
                sb.Append("        ) AS rn");
                sb.Append("    FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SE + " AS SE");
                sb.Append(" )");
                sb.Append(" SELECT SI.*, Z.ITEMTYPE, Z.ITEMDESC");
                sb.Append(" FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_SI + " AS SI");
                sb.Append(" LEFT JOIN Z");
                sb.Append("    ON Z.CountEntries = SI.CountEntries");
                sb.Append("   AND Z.ORD  = SI.ORD");
                sb.Append("   AND Z.rn = 1 ");

                sb.Append("WHERE 1=1 ");


                // Jednoduché filtry
                if (!string.IsNullOrWhiteSpace(filtr.CountEntries))
                {
                    sb.Append("AND SI.CountEntries = @countentries ");
                    command.Parameters.AddWithValue("@countentries", filtr.CountEntries.Trim());
                }

                if (!string.IsNullOrWhiteSpace(filtr.ITEMNMBR))
                {
                    sb.Append("AND SI.ITEMNMBR = @ITEMNMBR ");
                    command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR.Trim());
                }

                if (!string.IsNullOrWhiteSpace(filtr.SOPNUMBE))
                {
                    sb.Append("AND SI.SOPNUMBE = @SOPNUMBE ");
                    command.Parameters.AddWithValue("@SOPNUMBE", filtr.SOPNUMBE.Trim());
                }

                if (!string.IsNullOrWhiteSpace(filtr.ITEMDESC))
                {
                    sb.Append("AND Z.ITEMDESC LIKE @itemdesc ");
                    command.Parameters.AddWithValue("@itemdesc", "%" + filtr.ITEMDESC.Trim() + "%");
                } 
                
                // Seznamový filtr: ITEMTYPE
                if (!string.IsNullOrWhiteSpace(filtr.ITEMCODE))
                {
                    var hodnoty = filtr.ITEMCODE
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select((val, i) => new { ParamName = $"@itemcode{i}", Value = val.Trim() })
                        .ToList();

                    if (hodnoty.Count > 0)
                    {
                        sb.Append("AND SI.ITEMCODE IN (" + string.Join(", ", hodnoty.Select(h => h.ParamName)) + ") ");
                        foreach (var h in hodnoty)
                            command.Parameters.AddWithValue(h.ParamName, h.Value);
                    }
                }

                var types = new List<string>();

                if (filtr.ITEMTYPE_J)
                {
                    types.Add("J");
                    types.Add(""); // pokud má být "" bráno jako alternativa k 'J'
                }

                if (filtr.ITEMTYPE_I) types.Add("I");
                if (filtr.ITEMTYPE_P) types.Add("P");
                if (filtr.ITEMTYPE_E) types.Add("E");
                if (filtr.ITEMTYPE_V) types.Add("V");
                if (filtr.ITEMTYPE_O) types.Add("O");

                // pokud je něco ve výběru, sestav podmínku
                if (types.Count > 0)
                {
                    var joinedTypes = string.Join("','", types);
                    sb.Append($"AND Z.ITEMTYPE IN ('{joinedTypes}') ");
                }

                // Seznamový filtr: ITEMTYPE
                if (!string.IsNullOrWhiteSpace(filtr.ITEMTYPE))
                {
                    var hodnoty = filtr.ITEMTYPE
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select((val, i) => new { ParamName = $"@itemtype{i}", Value = val.Trim() })
                        .ToList();

                    if (hodnoty.Count > 0)
                    {
                        sb.Append("AND Z.ITEMTYPE IN (" + string.Join(", ", hodnoty.Select(h => h.ParamName)) + ") ");
                        foreach (var h in hodnoty)
                            command.Parameters.AddWithValue(h.ParamName, h.Value);
                    }
                }

                // USERID (nullable int)
                if (filtr.USERID != null && filtr.USERID.Count > 0)
                {
                    var paramNames = new List<string>();
                    for (int i = 0; i < filtr.USERID.Count; i++)
                    {
                        string paramName = "@userid" + i;
                        paramNames.Add(paramName);
                        command.Parameters.AddWithValue(paramName, filtr.USERID[i]);
                    }

                    sb.Append("AND SI.USER_ID IN (" + string.Join(", ", paramNames) + ") ");
                }


                if (filtr.ID_TERMINAL != null && filtr.ID_TERMINAL.Count > 0)
                {
                    var terminalParams = new List<string>();
                    for (int i = 0; i < filtr.ID_TERMINAL.Count; i++)
                    {
                        string paramName = "@terminal" + i;
                        terminalParams.Add(paramName);
                        command.Parameters.AddWithValue(paramName, filtr.ID_TERMINAL[i]);
                    }

                    sb.Append("AND SI.ID_TERMINAL IN (" + string.Join(", ", terminalParams) + ") ");
                }

                // ---- Začátek: Nový blok pro DATEDONE ----
                if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO != null)
                {
                    sb.Append("AND DATEDONE BETWEEN @DATEDONE_OD AND @DATEDONE_DO ");
                    command.Parameters.AddWithValue("@DATEDONE_OD", filtr.DATEDONE_OD.Value.ToString("yyyyMMdd"));
                    command.Parameters.AddWithValue("@DATEDONE_DO", filtr.DATEDONE_DO.Value.ToString("yyyyMMdd"));
                }
                else if (filtr.DATEDONE_OD == null && filtr.DATEDONE_DO != null)
                {
                    sb.Append("AND DATEDONE BETWEEN '19990101' AND @DATEDONE_DO ");
                    command.Parameters.AddWithValue("@DATEDONE_DO", filtr.DATEDONE_DO.Value.ToString("yyyyMMdd"));
                }
                else if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO == null)
                {
                    sb.Append("AND DATEDONE BETWEEN @DATEDONE_OD AND '25000101' ");
                    command.Parameters.AddWithValue("@DATEDONE_OD", filtr.DATEDONE_OD.Value.ToString("yyyyMMdd"));
                }

                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant) && !filtr.Dateeve_TimeVariant.Contains("unknow"))
                {
                    var arr = filtr.Dateeve_TimeVariant.Split(';');
                    var TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(
                        typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);

                    int cislo = (int)TimeVar;

                    if (cislo != 0 && cislo < 8)
                    {
                        sb.Append("AND TIMEDONE >= @TIME_TV ");
                        command.Parameters.AddWithValue("@TIME_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("HHmmss"));
                    }

                    sb.Append("AND DATEDONE >= @dateeve_TV ");
                    command.Parameters.AddWithValue("@dateeve_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("yyyyMMdd"));
                }
                // ---- Konec: Nový blok pro DATEDONE ----

                // Řazení
                sb.Append("ORDER BY SI.CountEntries ");
                sb.Append(Globals_V1.Konfigurace.Konzola[0].Vydej_GetFiltrovaneDavky_CountEntries_desc ? "DESC" : "ASC");
                sb.Append(", SI.DEX_ROW_ID ");
                sb.Append(Globals_V1.Konfigurace.Konzola[0].Vydej_GetFiltrovaneDavky_CountEntries_desc ? "DESC" : "ASC");

                command.CommandText = sb.ToString();
                command.Connection = connection;
                adapter.SelectCommand = command;
                adapter.Fill(dsVydej, dsVydej.CZMST_SI.TableName);

                return dsVydej;
            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region IVydej2_KontrolaDavky_TEST Members

        public void KontrolaDavky_TEST(string CountEntries, out Fask.POHODA.Disponibility.ValidateData DTOut, out Fask.POHODA.Disponibility.ValidateData dsDisp, Fask.Interfaces.Classes.TypZdrojeDat zdroj)
        {
            //Fask.Interfaces.DataSets.Vydej dsVydej = new Fask.Interfaces.DataSets.Vydej();

            try
            {
                Globals_V1.LoadConfiguration();
                dsDisp = new POHODA.Disponibility.ValidateData();


                if ( zdroj == TypZdrojeDat.CZMST_SE)
                {
                    #region CZMST_SE
                    Fask.Interfaces.Filtry.VydejDavkyFiltr filtr = new Fask.Interfaces.Filtry.VydejDavkyFiltr();
                    filtr.CountEntries = CountEntries;
                    filtr.UvolneneDavky = true;
                    filtr.NEUvolneneDavky = true;
                    filtr.StazeneDavky = true;
                    filtr.SpracovaneDavky = true;
                    filtr.MrtveDavky = true;

                    Fask.Interfaces.DataSets.Vydej vyd = GetFiltrovaneDavky(filtr);

                    OrderedEnumerableRowCollection<Fask.Interfaces.DataSets.Vydej.CZMST_SERow> dtSEOrder = vyd.CZMST_SE.OrderBy(x => x.ITEMNMBR);

                    foreach (Fask.Interfaces.DataSets.Vydej.CZMST_SERow item in dtSEOrder)
                    {
                        Fask.POHODA.Disponibility.ValidateData.DataDispRow Row = dsDisp.DataDisp.NewDataDispRow();

                        Row.ITEMDESC = item.ITEMDESC;
                        Row.DEX_ROW_ID = item.DEX_ROW_ID;
                        Row.ITEMNMBR = item.ITEMNMBR;
                        Row.SKL_ID = item.SKL_ID;
                        Row.QTY = item.QTYSHPPD;
                        Row.SOPNUMBE = item.SOPNUMBE;
                        Row.ORD = item.ORD;
                        Row.SetSKz_RezerNull();
                        Row.SetSKz_StavZNull();
                        Row.SetOBJ_RezerNull();

                        dsDisp.DataDisp.AddDataDispRow(Row);
                    }  
                    #endregion
                }
                else if (zdroj == TypZdrojeDat.CZMST_SI)
                {
                    #region CZMST_SI

                    Fask.Interfaces.Filtry.VydejNasnimaneFiltr filtr = new Fask.Interfaces.Filtry.VydejNasnimaneFiltr();
                    filtr.CountEntries = CountEntries;

                    Fask.Interfaces.DataSets.Vydej vyd = GetFiltrovaneDavkySI(filtr);

                    OrderedEnumerableRowCollection<Fask.Interfaces.DataSets.Vydej.CZMST_SIRow> dtSIOrder = vyd.CZMST_SI.OrderBy(x => x.ITEMNMBR);

                    foreach (Fask.Interfaces.DataSets.Vydej.CZMST_SIRow item in dtSIOrder)
                    {
                        Fask.POHODA.Disponibility.ValidateData.DataDispRow Row = dsDisp.DataDisp.NewDataDispRow();

                        //Row.ITEMDESC = string.Empty;
                        Row.DEX_ROW_ID = item.DEX_ROW_ID;

                        Row.ITEMNMBR = item.ITEMNMBR;
                        Row.SKL_ID = item.SKL_ID;
                        Row.QTY = item.QTYSHPPD;

                        Row.SOPNUMBE = item.SOPNUMBE;
                        Row.ORD = item.ORD;

                        Row.SetSKz_RezerNull();
                        Row.SetSKz_StavZNull();
                        Row.SetOBJ_RezerNull();

                        dsDisp.DataDisp.AddDataDispRow(Row);
                    }  
                    #endregion
                }
                else if (zdroj == TypZdrojeDat.CZMST_DI)
                {

                    #region CZMST_DI

                    Fask.Interfaces.Filtry.ProdejFiltr filtr = new Fask.Interfaces.Filtry.ProdejFiltr();
                    filtr.CountEntries = CountEntries;

                    Fask.Interfaces.DataSets.Prodej vyd = Prodej_GetFiltrovaneDavky(filtr);

                    OrderedEnumerableRowCollection<Fask.Interfaces.DataSets.Prodej.CZMST_DIRow> dtSIOrder = vyd.CZMST_DI.OrderBy(x => x.ITEMNMBR);

                    foreach (Fask.Interfaces.DataSets.Prodej.CZMST_DIRow item in dtSIOrder)
                    {
                        Fask.POHODA.Disponibility.ValidateData.DataDispRow Row = dsDisp.DataDisp.NewDataDispRow();

                        //Row.ITEMDESC = string.Empty;
                        Row.DEX_ROW_ID = item.DEX_ROW_ID;

                        Row.ITEMNMBR = item.ITEMNMBR;
                        Row.SKL_ID = item.SKL_ID;
                        Row.QTY = item.QTYSHPPD;

                        Row.SetSOPNUMBENull();
                        Row.SetORDNull();

                        Row.SetSKz_RezerNull();
                        Row.SetSKz_StavZNull();
                        Row.SetOBJ_RezerNull();

                        dsDisp.DataDisp.AddDataDispRow(Row);
                    }
                    #endregion

                }
                else if (zdroj == TypZdrojeDat.ProductionSources)
                {



                }
                else
                {
                    //nothing
 
                }





                Fask.POHODA.Disponibility.CheckDisp disp = new Fask.POHODA.Disponibility.CheckDisp();
                 var DT = disp.KontrolaDisponibilityDT(dsDisp, Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                 DTOut = new POHODA.Disponibility.ValidateData();

                 foreach (var item in DT)
                 {
                     DTOut.VydejKontrola.ImportRow(item);
                 }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            //return dsVydej; 
        }

        #endregion

        #region IVydej2_Get_SE_MaxCountEntries Member

        int Fask.Interfaces.Vydej.IVydej2_Get_SE_MaxCountEntries.Get_SE_MaxCountEntries()
        {
            try
            {
                Globals_V1.LoadConfiguration();

                using (System.Data.SqlClient.SqlConnection con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (System.Data.SqlClient.SqlCommand sqlcommand = con.CreateCommand())
                    {
                        sqlcommand.CommandType = System.Data.CommandType.Text;
                        sqlcommand.CommandText = "SELECT max( CountEntries ) FROM CZMST_SE";

                        con.Open();
                        object o = sqlcommand.ExecuteScalar();

                        con.Close();

                        try
                        {
                            int countentries = Convert.ToInt32(o);
                            return countentries;
                        }
                        catch (Exception ex)
                        {
                            Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                            return 0; //pokud nenalezeno ... ???
                        }

                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return 0;
            }
        }

        #endregion

        #region IVydej2_InsertSI Members

        public int InsertSI(Fask.Interfaces.DataSets.Vydej.CZMST_SIRow SIRow)
        {

            SqlTransaction trans = null;
            SqlConnection con = null;

            try
            {
                Globals_V1.LoadConfiguration();

                using (con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    con.Open();
                    trans = con.BeginTransaction(IsolationLevel.Serializable);

                    using (var com = con.CreateCommand())
                    {
                        using (var ada = new SqlDataAdapter())
                        {
                            
                            ada.InsertCommand = com;
                            ada.InsertCommand.Connection = con;
                            ada.InsertCommand.Transaction = trans;
                            ada.InsertCommand.CommandType = System.Data.CommandType.Text;

                            #region SQL insert command

                            ada.InsertCommand.CommandText = @"INSERT INTO [CZMST_SI] (" +
                                                    " [CountEntries]," + " [SOPNUMBE]," + " [ITEMNMBR]," + " [ORD]," + " [VNDDOCNM]," +
                                                    " [VNDITNUM]," + " [CZ_CarKod]," + " [SKL_ID]," + " [LOCNCODE]," + " [MJ]," +
                                                    " [QTYSHPPD]," + " [QTYPACK]," + " [QTYSHPPDMJ]," + " [SERLTNUM]," + " [KOD_SW]," + 
                                                    " [DAT_VYROBY]," + " [REZ_1]," + "[REZ_2]," + " [ODBER_ID]," + " [DATEDONE]," +
                                                    " [TIMEDONE]," + " [USER_ID]," + " [TYPEPAL]," + " [NMBRPAL]," + " [PRINTED]," +
                                                    " [GUID]," + " [INPUT_MODE]," + " [ID_TERMINAL]," +  " [ITEMCODE]," + " [WEIGHT]," + 
                                                    " [Expirace] " +
                                                    " ) VALUES ( " +
                                                    " @CountEntries," + " @SOPNUMBE," + " @ITEMNMBR," + " @ORD," + " @VNDDOCNM," +
                                                    " @VNDITNUM," + " @CZ_CarKod," + " @SKL_ID," + " @LOCNCODE," + " @MJ," +
                                                    " @QTYSHPPD," + " @QTYPACK," + " @QTYSHPPDMJ," + " @SERLTNUM," + " @KOD_SW," + 
                                                    " @DAT_VYROBY," + " @REZ_1," + " @REZ_2," + " @ODBER_ID," + " @DATEDONE," + 
                                                    " @TIMEDONE," + " @USER_ID," + " @TYPEPAL," + " @NMBRPAL," + " @PRINTED," +
                                                    " @GUID," + " @INPUT_MODE," + " @ID_TERMINAL," + " @ITEMCODE," + " @WEIGHT," + 
                                                    " @Expirace" +
                                                    " )";

                            ada.InsertCommand.Parameters.Add( new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", Value = SIRow.CountEntries });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", Value = SIRow.SOPNUMBE == null ? (object)DBNull.Value : SIRow.SOPNUMBE });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", Value = SIRow.ITEMNMBR == null ? (object)DBNull.Value : SIRow.ITEMNMBR });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", Value = SIRow.ORD  });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM", Value = SIRow.VNDDOCNM == null ? (object)DBNull.Value : SIRow.VNDDOCNM });

                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", Value = SIRow.IsVNDITNUMNull() ? (object)DBNull.Value : SIRow.VNDITNUM });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", Value = SIRow.CZ_CarKod == null ? (object)DBNull.Value : SIRow.CZ_CarKod });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", Value = SIRow.SKL_ID == null ? (object)DBNull.Value : SIRow.SKL_ID });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", Value = SIRow.LOCNCODE == null ? (object)DBNull.Value : SIRow.LOCNCODE });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", Value = SIRow.MJ == null ? (object)DBNull.Value : SIRow.MJ });

                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", Value = SIRow.IsQTYSHPPDMJNull() ? (object)DBNull.Value : SIRow.QTYSHPPD });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", Value = SIRow.QTYPACK });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ", Value = SIRow.IsQTYSHPPDMJNull() ? (object)DBNull.Value : SIRow.QTYSHPPDMJ });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM", Value = SIRow.SERLTNUM == null ? (object)DBNull.Value : SIRow.SERLTNUM });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, SourceColumn = "KOD_SW", Value = SIRow.KOD_SW == null ? (object)DBNull.Value : SIRow.KOD_SW });

                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, SourceColumn = "DAT_VYROBY", Value = SIRow.DAT_VYROBY == null ? (object)DBNull.Value : SIRow.DAT_VYROBY });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1", Value = SIRow.REZ_1 == null ? (object)DBNull.Value : SIRow.REZ_1 });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2", Value = SIRow.REZ_2 == null ? (object)DBNull.Value : SIRow.REZ_2 });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@ODBER_ID", DbType = System.Data.DbType.String, SourceColumn = "ODBER_ID", Value = SIRow.ODBER_ID == null ? (object)DBNull.Value : SIRow.ODBER_ID });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE", Value = SIRow.DATEDONE == null ? (object)DBNull.Value : SIRow.DATEDONE });

                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE", Value = SIRow.TIMEDONE == null ? (object)DBNull.Value : SIRow.TIMEDONE });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID", Value = SIRow.USER_ID  });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL", Value = SIRow.TYPEPAL == null ? (object)DBNull.Value : SIRow.TYPEPAL });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL", Value = SIRow.NMBRPAL == null ? (object)DBNull.Value : SIRow.NMBRPAL });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Boolean, SourceColumn = "PRINTED", Value = SIRow.IsPRINTEDNull() ? (object)DBNull.Value : SIRow.PRINTED });

                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID", Value = SIRow.IsGUIDNull() ? (object)DBNull.Value : SIRow.GUID });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE", Value = SIRow.INPUT_MODE  });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL", Value = SIRow.ID_TERMINAL  });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", Value = SIRow.ITEMCODE == null ? (object)DBNull.Value : SIRow.ITEMCODE });
                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", Value = SIRow.IsWEIGHTNull() ? (object)DBNull.Value : SIRow.WEIGHT });

                           ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace", Value = SIRow.IsExpiraceNull() ? (object)DBNull.Value : SIRow.Expirace });

                            #endregion

                            if (trans != null)
                                trans.Commit();

                            int returnValue = ada.InsertCommand.ExecuteNonQuery();
                            return returnValue;

                        }
                    }
                }
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((con != null) && (con.State & ConnectionState.Open) == ConnectionState.Open)
                    con.Close();
            }
        }



        #endregion

        #region IVydej2_UpdateSE_storno Members
        public int UpdateSE_storno(Interfaces.DataSets.Vydej.CZMST_SEDataTable dt)
        {
            int pocet = 0;

            foreach (var row in dt)
            {
                Database.Vydej o = new Database.Vydej();
                pocet += o.Update_row(row, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            }



            return pocet;
        }



        #endregion
        public bool DeleteSI(int ID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSI(Interfaces.DataSets.Vydej.CZMST_SIRow SIRow)
        {
            SIRow.Delete();

            return Database.Sklady_CZMST_SI.Update(SIRow, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB) > 0;
        }

        public bool UpdateSI(Interfaces.DataSets.Vydej.CZMST_SIRow SIRow)
        {
            return Database.Sklady_CZMST_SI.Update(SIRow, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB) > 0;
        }
    }
}
