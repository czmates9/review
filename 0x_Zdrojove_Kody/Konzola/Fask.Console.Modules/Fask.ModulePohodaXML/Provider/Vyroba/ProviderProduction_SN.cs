using Fask.Interfaces.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.Production_SN.IProduction_SN,
        Fask.Interfaces.Vyroba.Production_SN.IProduction_SN_GetFiltrovanyProduction_SNList
    {
        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyProduction_SNList(Fask.Interfaces.Filtry.Production_SNListFiltr filtr)
        {
            Globals_V1.LoadConfiguration();
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;

            command.CommandText =
            " SELECT " +
            " p.dateeve " +
            " , hlavicky.SOPDESC popiszakazky " +
            " , p.SOPNUMBE " +
            " , o.name operationName " +
            " , z.ITEMDESC " +
            " , p.CountEntries " +
            " , p.ITEMNMBR " +
            " , p.BarcodeP " +
            " , z.VNDITNUM " +
            " , z.ITEMCODE " +
            " , p.qty " +
            " , pol.SerNumT " +
            " , SN.Expirace " +
            " , SN.SERLNMBR " +
            " , SN.QTY as QTY_SERLTNUM " +
            " , sn.REZ_1 " +
            " , sn.REZ_2 " +
            " , sn.REZ_3 " +
            " , sn.REZ_4 " +
            " , p.machineid " +
            " , m.name machineName " +
            " , p.UserID " +
            " , l.firstname " +
            " , l.surname " +
            " , p.TermID " +
            " , p.SKL_ID " +
            " , sklady.skl_desc " +
            " , POHODA_Zasoby.RelSkTyp as ITEMTYPE " +
            " , POHODA_CleneniSklad.Vetev1 " +
            " , POHODA_CleneniSklad.Vetev2 " +
            " , POHODA_CleneniSklad.Vetev3 " +
            " , POHODA_CleneniSklad.Vetev4 " +
            " , POHODA_CleneniSklad.Vetev5 " +
            " , POHODA_CleneniSklad.Vetev6 " +
            " , POHODA_CleneniSklad.Vetev7 " +
            " , ITEMTYPE_Text = CASE " +
            " WHEN RelSkTyp = 1 THEN '" + Enum.GetName(typeof(Fask.Interfaces.Classes.TypPolozky), 1) + "' " +
            " WHEN RelSkTyp = 2 THEN '" + Enum.GetName(typeof(Fask.Interfaces.Classes.TypPolozky), 2) + "' " +
            " WHEN RelSkTyp = 3 THEN '" + Enum.GetName(typeof(Fask.Interfaces.Classes.TypPolozky), 3) + "' " +
            " WHEN RelSkTyp = 4 THEN '" + Enum.GetName(typeof(Fask.Interfaces.Classes.TypPolozky), 4) + "' " +
            " WHEN RelSkTyp = 5 THEN '" + Enum.GetName(typeof(Fask.Interfaces.Classes.TypPolozky), 5) + "' " +
            " WHEN RelSkTyp = 6 THEN '" + Enum.GetName(typeof(Fask.Interfaces.Classes.TypPolozky), 6) + "' " +
            " END " +
            " , p.QTYPACKMJ " +
            " FROM Production_SN as SN " +
            " LEFT JOIN Production p ON p.GUID = SN.GUID_Production " +
            " LEFT JOIN(select distinct ITEMDESC, ITEMNMBR, ITEMCODE, VNDITNUM from FASK_ZASOBY ) z on z.itemnmbr = p.itemnmbr " +
            " LEFT JOIN FASK_LOGINS l on l.USERID = p.userid " +
            " LEFT JOIN Operations o on o.id = p.operationid " +
            " LEFT JOIN Machines m on m.id = p.machineid " +
            " LEFT JOIN CZMST093 sklady on sklady.skl_id = p.skl_id " +
            " LEFT JOIN CZPRO_VPH hlavicky on hlavicky.SOPNUMBE = p.SOPNUMBE " +
            " LEFT JOIN CZPRO_VPP pol on pol.CountEntries = hlavicky.CountEntries AND pol.SOPNUMBE = hlavicky.SOPNUMBE AND pol.ITEMNMBR = p.itemnmbr " +
            " LEFT JOIN " + Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda.Trim() + ".dbo.SKz as POHODA_Zasoby ON POHODA_Zasoby.ID = Z.ITEMNMBR " +
            " LEFT JOIN " + Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda.Trim() + ".dbo.SKSt as POHODA_CleneniSklad ON POHODA_CleneniSklad.RefSklad = sklady.skl_id AND POHODA_CleneniSklad.ID =  POHODA_Zasoby.RefStruct ";


            command.CommandText += " WHERE 1=1 ";


            // hledaní zboží
            if (!string.IsNullOrEmpty(filtr.ITEMDESC))
            {
                command.CommandText += " AND z.ITEMDESC like '%' + @itemdesc + '%' ";
                command.Parameters.AddWithValue("@itemdesc", filtr.ITEMDESC.Trim());
            }

            //Objednavka
            if (!string.IsNullOrEmpty(filtr.SOPNUMBE))
            {
                command.CommandText += " AND p.SOPNUMBE = @SOPNUMBE ";
                command.Parameters.AddWithValue("@SOPNUMBE", filtr.SOPNUMBE.Trim());
            }

            //// hledání uživatele
            if (!string.IsNullOrEmpty(filtr.USERID))
            {
                command.CommandText += " AND p.UserID = @UserID ";
                command.Parameters.AddWithValue("@UserID", filtr.USERID.Trim());
            }

            //Kod položky
            if (!string.IsNullOrEmpty(filtr.ITEMCODE))
            {
                command.CommandText += " AND z.ITEMCODE = @ITEMCODE ";
                command.Parameters.AddWithValue("@ITEMCODE", filtr.ITEMCODE.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.SERLNMBR))
            {
                command.CommandText += " AND SN.SERLNMBR = @SERLNMBR ";
                command.Parameters.AddWithValue("@SERLNMBR", filtr.SERLNMBR.Trim());
            }

            // hledání podle datumu
            if (filtr.DatumOd && filtr.DatumDo)
            {
                command.CommandText += " AND dateeve between @datumOd and @datumDo";
                command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
                command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
            }
            else
            {
                if (filtr.DatumOd)
                {
                    command.CommandText += " AND dateeve > @datumOd";
                    command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
                }
                else if (filtr.DatumDo)
                {
                    command.CommandText += " AND dateeve < @datumDo";
                    command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
                }
            }





            command.CommandText += " ORDER BY dateeve desc";

            vyrobaDataSet1.Production_SN_Pohled.Clear();
            vyrobaDataSet1.Production_SN_Pohled.AcceptChanges();
            vyrobaDataSet1.Production_SN_Pohled.BeginLoadData();
            adapter.SelectCommand = command;
            adapter.Fill(vyrobaDataSet1.Production_SN_Pohled);
            vyrobaDataSet1.Production_SN_Pohled.EndLoadData();

            return vyrobaDataSet1;
        }
    }
}
