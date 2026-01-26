using Fask.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql
{
    public partial class Provider : Fask.Interfaces.Prodej.IProdej2,
        Fask.Interfaces.Prodej.IProdej2_Prodej_GetFiltrovaneDavky,
        Fask.Interfaces.Prodej.IProdej2_UpdateDI,
        Fask.Interfaces.Prodej.IProdej2_DeleteDI

    {


        #region IProdej2_GetFiltrovaneDavky Members

        #region old 27.5.2025 MaR
        public Fask.Interfaces.DataSets.Prodej Prodej_GetFiltrovaneDavkyOld(Fask.Interfaces.Filtry.ProdejFiltr filtr)
        {

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Prodej dsProdej = new Fask.Interfaces.DataSets.Prodej();
            try
            {
                connection = new SqlConnection(ConnectionString);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                command.CommandText =
                    "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_DI +
                    " WHERE" +
                    " 1=1 ";


                if (filtr.CountEntries != null && !string.IsNullOrEmpty(filtr.CountEntries.Trim()))
                {
                    command.CommandText += "AND CountEntries=@countentries ";
                    command.Parameters.AddWithValue("@countentries", filtr.CountEntries);
                }


                if (filtr.ITEMNMBR != null && !string.IsNullOrEmpty(filtr.ITEMNMBR.Trim()))
                {
                    command.CommandText += "AND ITEMNMBR=@ITEMNMBR ";
                    command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR);
                }

                if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO != null)
                {
                    string datum_OD = string.Empty;
                    datum_OD = filtr.DATEDONE_OD.Value.ToString("yyyyMMdd");

                    string datum_DO = string.Empty;
                    datum_DO = filtr.DATEDONE_DO.Value.ToString("yyyyMMdd");


                    command.CommandText += "AND DATEDONE between @DATEDONE_OD and @DATEDONE_DO ";
                    command.Parameters.AddWithValue("@DATEDONE_OD", datum_OD);
                    command.Parameters.AddWithValue("@DATEDONE_DO", datum_DO);
                }

                if (filtr.DATEDONE_OD == null && filtr.DATEDONE_DO != null)
                {

                    string datum_DO = string.Empty;
                    datum_DO = filtr.DATEDONE_DO.Value.ToString("yyyyMMdd");

                    command.CommandText += "AND DATEDONE between '19990101' and @DATEDONE_DO ";
                    command.Parameters.AddWithValue("@DATEDONE_DO", datum_DO);
                }

                if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO == null)
                {
                    string datum_OD = string.Empty;
                    datum_OD = filtr.DATEDONE_OD.Value.ToString("yyyyMMdd");

                    command.CommandText += "AND DATEDONE between @DATEDONE_OD and 25000101 ";
                    command.Parameters.AddWithValue("@DATEDONE_OD", datum_OD);
                }

                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant))
                {
                    if (!filtr.Dateeve_TimeVariant.Contains("unknow"))
                    {

                        var arr = filtr.Dateeve_TimeVariant.Split(';');
                        Fask.Interfaces.Classes.TimeFilters.TimeVariants TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);

                        int cislo = 0;
                        cislo = ((int)TimeVar);

                        if (cislo != 0 && cislo < 8)
                        {
                            command.CommandText += " AND TIMEDONE >= @TIME_TV ";
                            command.Parameters.AddWithValue("@TIME_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("HHmmss"));

                        }


                        string den = Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("yyyyMMdd");
                        string cas = Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("HHmmss");


                        command.CommandText += " AND DATEDONE >= @dateeve_TV ";
                        command.Parameters.AddWithValue("@dateeve_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar).ToString("yyyyMMdd"));


                    }
                }


                //if (filtr.DATEDONE_DO != null )
                //{
                //    string datum_DO = string.Empty;
                //    datum_DO = filtr.DATEDONE_DO.Value.ToString("yyyyMMdd");

                //    command.CommandText += "AND DATEDONE between '19990101' and '@DATEDONE_DO' ";
                //    command.Parameters.AddWithValue("@DATEDONE_DO", datum_DO);
                //}

                command.CommandText += "order by";
                command.CommandText += " CountEntries";
                command.CommandText += " , DEX_ROW_ID";


                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(dsProdej, dsProdej.CZMST_DI.TableName);

                return dsProdej;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public Fask.Interfaces.DataSets.Prodej Prodej_GetFiltrovaneDavky(Fask.Interfaces.Filtry.ProdejFiltr filtr)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Prodej dsProdej = new Fask.Interfaces.DataSets.Prodej();

            try
            {
                connection = new SqlConnection(ConnectionString);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                var sb = new StringBuilder();


                //27.8.2025 MaR zakomentoval
                //sb.Append("SELECT DI.*, FZ2.ITEMDESC ");
                //sb.Append("FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_DI + " DI ");
                //sb.Append("LEFT JOIN FASK_ZASOBY FZ2 ON DI.ITEMNMBR = FZ2.ITEMNMBR AND DI.QTYPACK = FZ2.QTYPACK ");


                sb.Append("WITH Z AS (");
                sb.Append("    SELECT");
                sb.Append("        FZ2.ITEMNMBR,");
                sb.Append("        FZ2.QTYPACK,");
                sb.Append("        FZ2.ITEMDESC,");
                sb.Append("        ROW_NUMBER() OVER (");
                sb.Append("            PARTITION BY FZ2.ITEMNMBR, FZ2.QTYPACK");
                sb.Append("            ORDER BY FZ2.QTYPACK ASC");
                sb.Append("        ) AS rn");
                sb.Append("    FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " AS FZ2");
                sb.Append(" )");
                sb.Append(" SELECT DI.*, Z.ITEMDESC");
                sb.Append(" FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_DI + " AS DI");
                sb.Append(" LEFT JOIN Z");
                sb.Append("    ON Z.ITEMNMBR = DI.ITEMNMBR");
                sb.Append("   AND Z.QTYPACK  = DI.QTYPACK");
                sb.Append("   AND Z.rn = 1 ");




                sb.Append("WHERE 1=1 ");

                if (!string.IsNullOrWhiteSpace(filtr.CountEntries))
                {
                    sb.Append("AND DI.CountEntries = @countentries ");
                    command.Parameters.AddWithValue("@countentries", filtr.CountEntries.Trim());
                }

                if (!string.IsNullOrWhiteSpace(filtr.ITEMNMBR))
                {
                    sb.Append("AND DI.ITEMNMBR = @ITEMNMBR ");
                    command.Parameters.AddWithValue("@ITEMNMBR", filtr.ITEMNMBR.Trim());
                }

                if (!string.IsNullOrWhiteSpace(filtr.ITEMDESC))
                {
                    sb.Append("AND FZ2.ITEMDESC LIKE @itemdesc ");
                    command.Parameters.AddWithValue("@itemdesc", "%" + filtr.ITEMDESC.Trim() + "%");
                }

                if (!string.IsNullOrWhiteSpace(filtr.ITEMCODE))
                {
                    var hodnoty = filtr.ITEMCODE
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select((val, i) => new { ParamName = $"@itemcode{i}", Value = val.Trim() })
                        .ToList();

                    if (hodnoty.Count > 0)
                    {
                        sb.Append("AND DI.ITEMCODE IN (" + string.Join(", ", hodnoty.Select(h => h.ParamName)) + ") ");
                        foreach (var h in hodnoty)
                            command.Parameters.AddWithValue(h.ParamName, h.Value);
                    }
                }



                if (!string.IsNullOrWhiteSpace(filtr.DOC_ID))
                {
                    var hodnoty = filtr.DOC_ID.Split(',').Select((val, i) => new { Param = "@docid" + i, Val = val.Trim() }).ToList();
                    if (hodnoty.Count > 0)
                    {
                        sb.Append("AND DI.DOC_ID IN (" + string.Join(", ", hodnoty.Select(h => h.Param)) + ") ");
                        foreach (var h in hodnoty)
                            command.Parameters.AddWithValue(h.Param, h.Val);
                    }
                }

                if (!string.IsNullOrWhiteSpace(filtr.DOC_ID2))
                {
                    var hodnoty = filtr.DOC_ID2.Split(',').Select((val, i) => new { Param = "@docid2" + i, Val = val.Trim() }).ToList();
                    if (hodnoty.Count > 0)
                    {
                        sb.Append("AND DI.DOC_ID2 IN (" + string.Join(", ", hodnoty.Select(h => h.Param)) + ") ");
                        foreach (var h in hodnoty)
                            command.Parameters.AddWithValue(h.Param, h.Val);
                    }
                }

                if (filtr.USERID != null && filtr.USERID.Count > 0)
                {
                    var paramNames = filtr.USERID.Select((val, i) => "@userid" + i).ToList();
                    sb.Append("AND DI.USER_ID IN (" + string.Join(", ", paramNames) + ") ");
                    for (int i = 0; i < filtr.USERID.Count; i++)
                        command.Parameters.AddWithValue(paramNames[i], filtr.USERID[i]);
                }

                if (filtr.ID_TERMINAL != null && filtr.ID_TERMINAL.Count > 0)
                {
                    var paramNames = filtr.ID_TERMINAL.Select((val, i) => "@terminal" + i).ToList();
                    sb.Append("AND DI.ID_TERMINAL IN (" + string.Join(", ", paramNames) + ") ");
                    for (int i = 0; i < filtr.ID_TERMINAL.Count; i++)
                        command.Parameters.AddWithValue(paramNames[i], filtr.ID_TERMINAL[i]);
                }

                if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO != null)
                {
                    sb.Append("AND DI.DATEDONE BETWEEN @DATEDONE_OD AND @DATEDONE_DO ");
                    command.Parameters.AddWithValue("@DATEDONE_OD", filtr.DATEDONE_OD.Value.ToString("yyyyMMdd"));
                    command.Parameters.AddWithValue("@DATEDONE_DO", filtr.DATEDONE_DO.Value.ToString("yyyyMMdd"));
                }
                else if (filtr.DATEDONE_OD == null && filtr.DATEDONE_DO != null)
                {
                    sb.Append("AND DI.DATEDONE BETWEEN '19990101' AND @DATEDONE_DO ");
                    command.Parameters.AddWithValue("@DATEDONE_DO", filtr.DATEDONE_DO.Value.ToString("yyyyMMdd"));
                }
                else if (filtr.DATEDONE_OD != null && filtr.DATEDONE_DO == null)
                {
                    sb.Append("AND DI.DATEDONE BETWEEN @DATEDONE_OD AND '25000101' ");
                    command.Parameters.AddWithValue("@DATEDONE_OD", filtr.DATEDONE_OD.Value.ToString("yyyyMMdd"));
                }

                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant) && !filtr.Dateeve_TimeVariant.Contains("unknow"))
                {
                    var arr = filtr.Dateeve_TimeVariant.Split(';');
                    var TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);
                    var dt = Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar);

                    if ((int)TimeVar != 0 && (int)TimeVar < 8)
                    {
                        sb.Append("AND DI.TIMEDONE >= @TIME_TV ");
                        command.Parameters.AddWithValue("@TIME_TV", dt.ToString("HHmmss"));
                    }

                    sb.Append("AND DI.DATEDONE >= @dateeve_TV ");
                    command.Parameters.AddWithValue("@dateeve_TV", dt.ToString("yyyyMMdd"));
                }

                sb.Append("ORDER BY DI.CountEntries ASC, DI.DEX_ROW_ID ASC");

                command.CommandText = sb.ToString();
                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(dsProdej, dsProdej.CZMST_DI.TableName);

                return dsProdej;
            }
            catch
            {
                throw;
            }
        }



        #endregion

        #region DI
        public bool UpdateDI(Prodej.CZMST_DIRow DIRow)
        {
            return Database.Sklady_CZMST_DI.Update(DIRow, ConnectionString) > 0;
            //throw new NotImplementedException();
        }


        public bool DeleteDI(int ID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDI(Prodej.CZMST_DIRow DIRow)
        {
            DIRow.Delete();

            return Database.Sklady_CZMST_DI.Update(DIRow, ConnectionString) > 0;
            //throw new NotImplementedException();
        }
        #endregion

    }
}
