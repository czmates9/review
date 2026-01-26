using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Providers;
using System.Data;

namespace Fask.ModuleSql
{
    public partial class Provider : Fask.Interfaces.Providers.IVyrabenePolozky2,
        Interfaces.Providers.IVyrabenePolozky2_Get
    {


        #region vyrabene polozky

        Hlavni IVyrabenePolozky2_Get.GetVyrabenePolozkyFiltrovane(Fask.Interfaces.Filtry.VyrabenePolozkyFiltr filtr)
        {
            Hlavni dsHlavni = new Hlavni();

            // Použití using pro správu zdrojů
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand())
            using (var adapter = new SqlDataAdapter())
            {
                connection.Open();
                command.Connection = connection;

                try
                {

                    // Dynamické vytváření podmínek
                    List<string> conditions = new List<string>();
                    if (filtr != null)
                    {
                        // Pokud jsou vyplněny obě hodnoty, průnik je automaticky zajištěn logikou WHERE klauzule
                        if (filtr.DateProd_OD != null)
                        {
                            conditions.Add("vph.DateProd >= @dateprod_OD");
                            command.Parameters.Add("@dateprod_OD", SqlDbType.SmallInt).Value = filtr.DateProd_OD;
                        }

                        if (filtr.DateProd_DO != null)
                        {
                            conditions.Add("vph.DateProd <= @dateprod_DO");
                            command.Parameters.Add("@dateprod_DO", SqlDbType.SmallInt).Value = filtr.DateProd_DO;
                        }

                        // Logika pro JenNezrealizovane
                        if (filtr.JenNezrealizovane)
                        {
                            conditions.Add("(vpp.QTYSHPPD - ISNULL(psum.QTYODVEDENO, 0)) > 0");
                        }

                        // Kombinace podmínek Aktivní, Neaktivní, Ukončeno
                        List<string> activeConditions = new List<string>();
                        if (filtr.Aktivni)
                        {
                            activeConditions.Add("vph.Active = @aktivni");
                            command.Parameters.Add("@aktivni", SqlDbType.TinyInt).Value = 1;
                        }

                        if (filtr.Neaktivni)
                        {
                            activeConditions.Add("vph.Active = @neaktivni");
                            command.Parameters.Add("@neaktivni", SqlDbType.TinyInt).Value = 0;
                        }

                        if (filtr.Ukonceno)
                        {
                            activeConditions.Add("vph.Active = @ukonceno");
                            command.Parameters.Add("@ukonceno", SqlDbType.TinyInt).Value = 200;
                        }

                        // Pokud existují podmínky Aktivní/Neaktivní/Ukončeno, přidej je jako OR
                        if (activeConditions.Count > 0)
                        {
                            conditions.Add($"({string.Join(" OR ", activeConditions)})");
                        }

                        // Další filtry
                        if (!string.IsNullOrEmpty(filtr.CountEntries?.Trim()))
                        {
                            conditions.Add("vph.CountEntries = @countentries");
                            command.Parameters.Add("@countentries", SqlDbType.VarChar).Value = filtr.CountEntries.Trim();
                        }

                        if (!string.IsNullOrEmpty(filtr.CountEntries_VPP?.Trim()))
                        {
                            conditions.Add("vpp.CountEntries = @countentries_VPP");
                            command.Parameters.Add("@countentries_VPP", SqlDbType.VarChar).Value = filtr.CountEntries_VPP.Trim();
                        }

                        if (!string.IsNullOrEmpty(filtr.DateProd?.Trim()))
                        {
                            conditions.Add("vph.DateProd = @dateprod");
                            command.Parameters.Add("@dateprod", SqlDbType.Date).Value = DateTime.Parse(filtr.DateProd.Trim());
                        }

                        if (!string.IsNullOrEmpty(filtr.ITEMDESC?.Trim()))
                        {
                            conditions.Add("vpp.ITEMDESC LIKE @itemdesc");
                            command.Parameters.Add("@itemdesc", SqlDbType.VarChar).Value = $"%{filtr.ITEMDESC.Trim()}%";
                        }

                        if (!string.IsNullOrEmpty(filtr.VNDITNUM?.Trim()))
                        {
                            conditions.Add("vpp.VNDITNUM = @vnditnum");
                            command.Parameters.Add("@vnditnum", SqlDbType.VarChar).Value = filtr.VNDITNUM.Trim();
                        }

                        if (!string.IsNullOrEmpty(filtr.SOPNUMBE?.Trim()))
                        {
                            conditions.Add("vph.SOPNUMBE = @sopnumbe");
                            command.Parameters.Add("@sopnumbe", SqlDbType.VarChar).Value = filtr.SOPNUMBE.Trim();
                        }
                    }

                    // Sestavení WHERE klauzule
                    string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";

                    // Sestavení SQL dotazu
                    command.CommandText = $@"
SELECT 
    vph.CountEntries AS CountEntries,
    vph.SOPNUMBE AS SOPNUMBE,
    vph.SOPTYPE,
    vph.SOPDESC,
    vph.VNDDOCNMH,
    vph.BarcodeH,
    vph.LOCNCODE AS LOCNCODE,
    vph.DateProd,
    vph.Rez1,
    vph.Rez2,
    vph.TermID AS TermID,
    vph.LSTMod AS LSTMod,
    vph.DEX_ROW_ID AS DEX_ROW_ID_P,
    vph.Active,
    vph.USERID,

    vpp.CountEntries AS CountEntries_VPP,
    vpp.ITEMNMBR,
    vpp.ITEMTYPE,
    vpp.ITEMDESC,
    vpp.ITEMMJ,
    vpp.VNDDOCNMP,
    vpp.VNDITNUM,
    vpp.ORD,
    vpp.BarcodeP,
    vpp.LOCNCODE AS LOCNCODE_VPP,
    vpp.QTYSHPPD,
    vpp.QTYDOKON,
    vpp.QTYPACK,
    vpp.QTYPACKMJ,
    vpp.TIMEMODE,
    vpp.TIMEPREP,
    vpp.TIMEUNIT,
    vpp.DtProdT,
    vpp.DtProdL,
    vpp.SerNumT,
    vpp.SerNumL,
    vpp.VerT,
    vpp.VerL,
    vpp.TermID AS TermID_VPP,
    vpp.LSTMod AS LSTMod_VPP,
    vpp.DEX_ROW_ID AS DEX_ROW_ID_VPP,
    vpp.Realization_Start,
    vpp.Realization_Stop,
    vpp.BarcodeT,
    vpp.CZ_REZ1_Track,
    vpp.CZ_REZ2_Track,
    vpp.CZ_REZ3_Track,
    vpp.CZ_REZ4_Track,
    vpp.CZ_REZ5_Track,
    vpp.WEIGHT_TARA,
    vpp.WEIGHT_NETTO,
    vpp.WEIGHT_TOL_PLUS,
    vpp.WEIGHT_TOL_MINUS,
    vpp.SOPNUMBE AS SOPNUMBE_VPP,

    psum.QTYODVEDENO,
    psum.CNTODVEDENO,
    
    -- Vypočtená hodnota MnozstviZbyva
    (vpp.QTYSHPPD - ISNULL(psum.QTYODVEDENO, 0)) AS MnozstviZbyva

FROM 
    {Fask.SQL.Constants.Common.TABLE_CZPRO_VPH} AS vph
LEFT JOIN
    {Fask.SQL.Constants.Common.TABLE_CZPRO_VPP} AS vpp
ON 
    vph.CountEntries = vpp.CountEntries
LEFT OUTER JOIN 
    (
        SELECT 
            CountEntries, 
            SOPNUMBE, 
            ITEMNMBR, 
            BarcodeP, 
            SUM(qty) AS QTYODVEDENO, 
            COUNT(*) AS CNTODVEDENO
        FROM 
            {Fask.SQL.Constants.Common.TABLE_PRODUCTION}
        GROUP BY 
            CountEntries, SOPNUMBE, ITEMNMBR, BarcodeP
    ) AS psum
ON 
    psum.CountEntries = vpp.CountEntries
    AND psum.SOPNUMBE = vpp.SOPNUMBE
    AND psum.ITEMNMBR = vpp.ITEMNMBR
    AND psum.BarcodeP = vpp.BarcodeP
{whereClause}
ORDER BY 
    CountEntries;
";



                    adapter.SelectCommand = command;
                    adapter.Fill(dsHlavni, dsHlavni.VyrabenePolozky.TableName);
                }
                catch (Exception ex)
                {
                    // Přidej logování nebo jinou formu zpracování chyb
                    throw new ApplicationException("Chyba při zpracování SQL dotazu.", ex);
                }
            }

            return dsHlavni;
        }

        #endregion



    }
}
