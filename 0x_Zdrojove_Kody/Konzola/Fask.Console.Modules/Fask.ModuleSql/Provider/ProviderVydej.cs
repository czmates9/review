using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Fask.Interfaces.DataSets;

namespace Fask.ModuleSql
{
    public partial class Provider : Fask.Interfaces.Vydej.IVydej2,
        Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneHlavicky,
        Fask.Interfaces.Vydej.IVydej2_GetHlavickaByCountEntries,
        Fask.Interfaces.Vydej.IVydej2_GetHlavicky,
        Fask.Interfaces.Vydej.IVydej2_UpdatePriorityDavka_byPriority,
        Fask.Interfaces.Vydej.IVydej2_UpdatePriorityDavka_byUserID,
        Fask.Interfaces.Vydej.IVydej2_DeleteSE,
        Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavky,
        Fask.Interfaces.Vydej.IVydej2_InsertSE,
        Fask.Interfaces.Vydej.IVydej2_UpdateSE,
        Fask.Interfaces.Vydej.IVydej2_GetFiltrovaneDavkySI,
        Fask.Interfaces.Vydej.IVydej2_UpdateSE_storno,
        Fask.Interfaces.Vydej.IVydej2_UpdateSI,
        Fask.Interfaces.Vydej.IVydej2_DeleteSI

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
                connection = new SqlConnection(ConnectionString);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                command.CommandText =
                    "SELECT A.COUNTENTRIES, A.PRIORITY, A.SOPNUMBE, B.CNTITEMS CNTITEMS, C.QTYSHPPDSUM SUMITEMS, D.ROZPRACOVANO ROZPRACOVANO, E.CZ_Doslo, A.USERID, MIN(uzivatele.LOGIN) as LOGIN, MIN(uzivatele.FIRSTNAME) as FIRSTNAME, MIN(uzivatele.SECONDNAME) as SECONDNAME " +
                    "from " +
                    Fask.SQL.Constants.Common.TABLE_CZMST_SE + " as A " +
                    "INNER JOIN " +
                    "( " +
                    "   SELECT X.COUNTENTRIES, X.SOPNUMBE, COUNT(X.ITEMNMBR) CNTITEMS " +
                    "   FROM ( " +
                    "   SELECT COUNTENTRIES, SOPNUMBE, ITEMNMBR " +
                    "   FROM CZMST_SE " +
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
                    "   FROM CZMST_SE " +
                    "   WHERE QTYPACK=0 " +
                    "   GROUP BY COUNTENTRIES, SOPNUMBE	" +
                    ") C ON " +
                    "C.COUNTENTRIES=A.COUNTENTRIES " +
                    "AND C.SOPNUMBE=A.SOPNUMBE " +
                    "INNER JOIN " +
                    "( " +
                    "   SELECT COUNTENTRIES, SOPNUMBE, CZ_Doslo" +
                    "   FROM CZMST_SE " +
                    "   GROUP BY COUNTENTRIES, SOPNUMBE	, CZ_Doslo " +
                    ") E ON " +
                    "E.COUNTENTRIES=A.COUNTENTRIES  " +
                    "AND E.SOPNUMBE=A.SOPNUMBE " +
                    "LEFT OUTER JOIN " +
                    "(" +
                    "   SELECT COUNTENTRIES, SOPNUMBE, COUNT(*) AS ROZPRACOVANO FROM CZMST_SI " +
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
            SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter taVydej = null;

            try
            {
                taVydej = new SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter();
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                taVydej.Connection = connection;
                taVydej.MyTransaction = trans;

                // kontrola CZ_Doslo, zdali je mozne pokracovat ...
                object result_obj = taVydej.GetCZDosloByCountEntries(countentries);

                int? result_int = (int?)result_obj;

                // kontrola, zdali CZ_Doslo je nastaveno na hodnotu 0
                if (result_int.HasValue && result_int.Value == 0)
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
            SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter taVydej = null;

            try
            {
                taVydej = new SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter();
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                taVydej.Connection = connection;
                taVydej.MyTransaction = trans;

                // kontrola CZ_Doslo, zdali je mozne pokracovat ...
                object result_obj = taVydej.GetCZDosloByCountEntries(countentries);


                int? result_int = (int?)result_obj;

                // kontrola, zdali CZ_Doslo je nastaveno na hodnotu 0
                if (result_int.HasValue && result_int.Value == 0)
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

        #region IVydej2_GetFiltrovaneDavky Members

        #region 21.5.2025 zakomentovano MaR a udelano new
        //public Fask.Interfaces.DataSets.Vydej GetFiltrovaneDavky(Fask.Interfaces.Filtry.VydejDavkyFiltr filtr)
        //{
        //    System.Data.SqlClient.SqlConnection connection = null;
        //    System.Data.SqlClient.SqlCommand command = null;
        //    System.Data.SqlClient.SqlDataAdapter adapter = null;

        //    Fask.Interfaces.DataSets.Vydej dsVydej = new Fask.Interfaces.DataSets.Vydej();
        //    try
        //    {
        //        connection = new SqlConnection(ConnectionString);
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
        //        command.CommandText += " order by CountEntries , DEX_ROW_ID ";
        //                      command.Connection = connection;
        //        adapter.SelectCommand = command;

        //        adapter.Fill(dsVydej, dsVydej.CZMST_SE.TableName);

        //        return dsVydej;
        //    }
        //    catch(Exception ex)
        //    {
        //        Logging.ExceptionHandler2.Handle(ex);
        //        return null;
        //    }
        //} 
        #endregion
        public Fask.Interfaces.DataSets.Vydej GetFiltrovaneDavky(Fask.Interfaces.Filtry.VydejDavkyFiltr filtr)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Vydej dsVydej = new Fask.Interfaces.DataSets.Vydej();

            try
            {
                connection = new SqlConnection(ConnectionString);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                StringBuilder sb = new StringBuilder();

                //sb.Append("SELECT SE.*, SI.QTYSHPPD AS MnozstviNasnimane, (SE.QTYSHPPD - SI.QTYSHPPD) AS MnozstviZbyva ");
                //sb.Append("FROM CZMST_SE SE ");
                //sb.Append("LEFT JOIN CZMST_SI SI ON SE.CountEntries = SI.CountEntries AND SE.ORD = SI.ORD ");
                //sb.Append("WHERE 1=1 ");

                sb.Append(@"
            SELECT 
                SE.*, 
                ISNULL(Nasnimano.MnozstviNasnimane, 0) AS MnozstviNasnimane,
                SE.QTYSHPPD - ISNULL(Nasnimano.MnozstviNasnimane, 0) AS MnozstviZbyva
            FROM CZMST_SE SE
            OUTER APPLY (
                SELECT SUM(SI.QTYSHPPD) AS MnozstviNasnimane
                FROM CZMST_SI SI
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
                    sb.Append("AND (" + string.Join(" OR ", czDosloConditions) + ") ");

                if (!string.IsNullOrWhiteSpace(filtr.CountEntries))
                {
                    sb.Append("AND SE.CountEntries = @countentries ");
                    command.Parameters.AddWithValue("@countentries", filtr.CountEntries.Trim());
                }

                if (!string.IsNullOrWhiteSpace(filtr.Sopnumbe))
                {
                    sb.Append("AND SE.Sopnumbe = @sopnumbe ");
                    command.Parameters.AddWithValue("@sopnumbe", filtr.Sopnumbe.Trim());
                }

                if (!string.IsNullOrWhiteSpace(filtr.ITEMNMBR))
                {
                    sb.Append("AND SE.ITEMNMBR = @ITEMNMBR ");
                    command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR.Trim());
                }

                if (filtr.PouzeKladneMnozstvy)
                {
                    sb.Append("AND SE.QTYSHPPD > 0 ");
                }

                // PriznakPolozky filtr
                if (filtr.PriznakPolozky_vykryte)
                {
                    sb.Append("AND (SE.QTYSHPPD - ISNULL(SI.QTYSHPPD, 0)) <= 0 ");
                }
                else if (filtr.PriznakPolozky_nevykryte)
                {
                    sb.Append("AND (SE.QTYSHPPD - ISNULL(SI.QTYSHPPD, 0)) > 0 ");
                }
                else if (filtr.PriznakPolozky_neplnene)
                {
                    sb.Append("AND SI.QTYSHPPD IS NULL ");
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
                    sb.Append($"AND SE.ITEMTYPE IN ('{joinedTypes}') ");
                }

                // TypPolozky – IN seznam
                if (!string.IsNullOrWhiteSpace(filtr.TypPolozky))
                {
                    var hodnoty = filtr.TypPolozky
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select((val, i) => new { ParamName = $"@typPolozky{i}", Value = val.Trim() })
                        .ToList();

                    if (hodnoty.Count > 0)
                    {
                        var podminky = string.Join(", ", hodnoty.Select(h => h.ParamName));
                        sb.Append($"AND SE.ITEMTYPE IN ({podminky}) ");

                        foreach (var h in hodnoty)
                            command.Parameters.AddWithValue(h.ParamName, h.Value);
                    }
                }

                // PriznakDavky – IN seznam
                if (!string.IsNullOrWhiteSpace(filtr.PriznakDavky))
                {
                    var hodnoty = filtr.PriznakDavky
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select((val, i) => new { ParamName = $"@priznakDavky{i}", Value = val.Trim() })
                        .ToList();

                    if (hodnoty.Count > 0)
                    {
                        var podminky = string.Join(", ", hodnoty.Select(h => h.ParamName));
                        sb.Append($"AND SE.PriznakDavky IN ({podminky}) ");

                        foreach (var h in hodnoty)
                            command.Parameters.AddWithValue(h.ParamName, h.Value);
                    }
                }

                sb.Append("ORDER BY SE.CountEntries, SE.DEX_ROW_ID ");

                command.CommandText = sb.ToString();
                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(dsVydej, dsVydej.CZMST_SE.TableName);

                return dsVydej;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        #endregion

        #region IVydej2_UpdateSE Members

        public bool UpdateSE(Fask.Interfaces.DataSets.Vydej.CZMST_SERow SERow)
        {

            return Database.Sklady_CZMST_SE.Update(SERow, ConnectionString) > 0;
            #region old
            //System.Data.SqlClient.SqlTransaction trans = null;
            //System.Data.SqlClient.SqlConnection connection = null;

            //try
            //{
            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    connection.Open();

            //    Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter();
            //    ta_se.Connection = connection;
            //    trans = connection.BeginTransaction(IsolationLevel.Serializable);
            //    ta_se.MyTransaction = trans;

            //    ta_se.Update(SERow);

            //    if (trans != null)
            //        trans.Commit();

            //    return true;
            //}
            //catch
            //{
            //    try
            //    {
            //        if (trans != null)
            //            trans.Rollback();
            //    }
            //    catch { }
            //    throw;
            //}
            //finally
            //{
            //    if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
            //        connection.Close();
            //} 
            #endregion
        }

        #endregion

        #region IVydej2_InsertSE Members

        public bool InsertSE(Fask.Interfaces.DataSets.Vydej.CZMST_SERow SERow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter();
                ta_se.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_se.MyTransaction = trans;

                ta_se.Insert(SERow.CountEntries,
                    SERow.SOPNUMBE,
                    SERow.ITEMNMBR,
                    SERow.ITEMDESC,
                    SERow.ITEMTYPE,
                    SERow.VNDDOCNM,
                    SERow.VNDITNUM,
                    SERow.ORD,
                    SERow.CZ_CarKod,
                    SERow.SKL_ID,
                    SERow.LOCNCODE,
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
                    SERow.Note,
                    SERow.TYPEPAL,
                    SERow.QTYPAL,
                    SERow.PRIORITY,
                    SERow.PRINTED,
                    SERow.USERID,
                    SERow.CZ_REZ1_Track,
                    SERow.CZ_REZ2_Track,
                    SERow.ITEMCODE,
                    SERow.WEIGHT
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
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();

                Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter ta_se = new Fask.ModuleSql.SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter();
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


        #region IVydej2_GetFiltrovaneDavkySI Members

        #region old MaR 26.5.2025
        //public Fask.Interfaces.DataSets.Vydej GetFiltrovaneDavkySI(Fask.Interfaces.Filtry.VydejNasnimaneFiltr filtr)
        //{
        //    System.Data.SqlClient.SqlConnection connection = null;
        //    System.Data.SqlClient.SqlCommand command = null;
        //    System.Data.SqlClient.SqlDataAdapter adapter = null;

        //    Fask.Interfaces.DataSets.Vydej dsVydej = new Fask.Interfaces.DataSets.Vydej();
        //    try
        //    {
        //        connection = new SqlConnection(ConnectionString);
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

        //        if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO != null)
        //        {
        //            string datum_OD = string.Empty;
        //            datum_OD = filtr.DATEDONE_OD.Value.ToString("yyyyMMdd");

        //            string datum_DO = string.Empty;
        //            datum_DO = filtr.DATEDONE_DO.Value.ToString("yyyyMMdd");


        //            command.CommandText += "AND DATEDONE between @DATEDONE_OD and @DATEDONE_DO ";
        //            command.Parameters.AddWithValue("@DATEDONE_OD", datum_OD);
        //            command.Parameters.AddWithValue("@DATEDONE_DO", datum_DO);
        //        }

        //        if (filtr.DATEDONE_OD == null && filtr.DATEDONE_DO != null)
        //        {

        //            string datum_DO = string.Empty;
        //            datum_DO = filtr.DATEDONE_DO.Value.ToString("yyyyMMdd");

        //            command.CommandText += "AND DATEDONE between '19990101' and @DATEDONE_DO ";
        //            command.Parameters.AddWithValue("@DATEDONE_DO", datum_DO);
        //        }

        //        if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO == null)
        //        {
        //            string datum_OD = string.Empty;
        //            datum_OD = filtr.DATEDONE_OD.Value.ToString("yyyyMMdd");

        //            command.CommandText += "AND DATEDONE between @DATEDONE_OD and 25000101 ";
        //            command.Parameters.AddWithValue("@DATEDONE_OD", datum_OD);
        //        }

        //        if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant))
        //        {
        //            if (!filtr.Dateeve_TimeVariant.Contains("unknow"))
        //            {

        //                var arr = filtr.Dateeve_TimeVariant.Split(';');
        //                Fask.Interfaces.Classes.TimeFilters.TimeVariants TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);

        //                int cislo = 0;
        //                cislo = ((int)TimeVar);

        //                if (cislo != 0 && cislo < 8)
        //                {
        //                    command.CommandText += " AND TIMEDONE >= @TIME_TV ";
        //                    command.Parameters.AddWithValue("@TIME_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("HHmmss"));

        //                }


        //                //string den = Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("yyyyMMdd");
        //                //string cas = Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("HHmmss");


        //                command.CommandText += " AND DATEDONE >= @dateeve_TV ";
        //                command.Parameters.AddWithValue("@dateeve_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("yyyyMMdd"));


        //            }
        //        }

        //        command.CommandText += " order by CountEntries, DEX_ROW_ID";


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
            var dsVydej = new Fask.Interfaces.DataSets.Vydej();

            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand())
            using (var adapter = new SqlDataAdapter())
            {
                var sb = new StringBuilder();
                //27.8.2025 MaR zakomentoval
                //sb.Append("SELECT SI.*, SE.ITEMTYPE, SE.ITEMDESC ");
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
                    var itemtypes = filtr.ITEMTYPE
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select((v, i) => new { Param = $"@itemtype{i}", Value = v.Trim() })
                        .ToList();

                    if (itemtypes.Count > 0)
                    {
                        sb.Append("AND Z.ITEMTYPE IN (").Append(string.Join(", ", itemtypes.Select(p => p.Param))).Append(") ");
                        foreach (var p in itemtypes)
                            command.Parameters.AddWithValue(p.Param, p.Value);
                    }
                }

                // USERID seznam
                if (filtr.USERID != null && filtr.USERID.Count > 0)
                {
                    var userParams = new List<string>();
                    for (int i = 0; i < filtr.USERID.Count; i++)
                    {
                        var param = "@userid" + i;
                        userParams.Add(param);
                        command.Parameters.AddWithValue(param, filtr.USERID[i]);
                    }
                    sb.Append("AND SI.USER_ID IN (").Append(string.Join(", ", userParams)).Append(") ");
                }

                // ID_TERMINAL seznam
                if (filtr.ID_TERMINAL != null && filtr.ID_TERMINAL.Count > 0)
                {
                    var termParams = new List<string>();
                    for (int i = 0; i < filtr.ID_TERMINAL.Count; i++)
                    {
                        var param = "@terminal" + i;
                        termParams.Add(param);
                        command.Parameters.AddWithValue(param, filtr.ID_TERMINAL[i]);
                    }
                    sb.Append("AND SI.ID_TERMINAL IN (").Append(string.Join(", ", termParams)).Append(") ");
                }

                // DATEDONE filtry
                if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO != null)
                {
                    sb.Append("AND SI.DATEDONE BETWEEN @DATEDONE_OD AND @DATEDONE_DO ");
                    command.Parameters.AddWithValue("@DATEDONE_OD", filtr.DATEDONE_OD.Value.ToString("yyyyMMdd"));
                    command.Parameters.AddWithValue("@DATEDONE_DO", filtr.DATEDONE_DO.Value.ToString("yyyyMMdd"));
                }
                else if (filtr.DATEDONE_OD == null && filtr.DATEDONE_DO != null)
                {
                    sb.Append("AND SI.DATEDONE BETWEEN '19990101' AND @DATEDONE_DO ");
                    command.Parameters.AddWithValue("@DATEDONE_DO", filtr.DATEDONE_DO.Value.ToString("yyyyMMdd"));
                }
                else if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO == null)
                {
                    sb.Append("AND SI.DATEDONE BETWEEN @DATEDONE_OD AND '25000101' ");
                    command.Parameters.AddWithValue("@DATEDONE_OD", filtr.DATEDONE_OD.Value.ToString("yyyyMMdd"));
                }

                // Časová varianta
                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant) && !filtr.Dateeve_TimeVariant.Contains("unknow"))
                {
                    var arr = filtr.Dateeve_TimeVariant.Split(';');
                    var timeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(
                        typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);
                    int typ = (int)timeVar;

                    if (typ != 0 && typ < 8)
                    {
                        sb.Append("AND SI.TIMEDONE >= @TIME_TV ");
                        command.Parameters.AddWithValue("@TIME_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, timeVar).ToString("HHmmss"));
                    }

                    sb.Append("AND SI.DATEDONE >= @dateeve_TV ");
                    command.Parameters.AddWithValue("@dateeve_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, timeVar).ToString("yyyyMMdd"));
                }

                // Řazení
                sb.Append("ORDER BY SI.CountEntries ASC, SI.DEX_ROW_ID ASC");

                // Provádění dotazu
                command.CommandText = sb.ToString();
                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(dsVydej, dsVydej.CZMST_SI.TableName);

                return dsVydej;
            }
        }


        #endregion

        #region IVydej2_UpdateSE_storno Members
        public int UpdateSE_storno(Vydej.CZMST_SEDataTable dt)
        {
            int pocet = 0;

            foreach (var row in dt)
            {
                Database.Vydej o = new Database.Vydej();
               pocet += o.Update_row(row, ConnectionString);

            }

            

            return pocet;
        }


        #endregion

        #region SI
        public bool UpdateSI(Vydej.CZMST_SIRow SIRow)
        {
            return Database.Sklady_CZMST_SI.Update(SIRow, ConnectionString) > 0;
           // throw new NotImplementedException();
        }
        public bool DeleteSI(Vydej.CZMST_SIRow SIRow)
        {

            SIRow.Delete();

            return Database.Sklady_CZMST_SI.Update(SIRow, ConnectionString) > 0;
            // throw new NotImplementedException();
        }

        public bool DeleteSI(int ID)
        {
            throw new NotImplementedException();

           
        }
        #endregion
    }
}
