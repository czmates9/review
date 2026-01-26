using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Fask.ModuleSql.Classes
{
    public static class Inventura
    {


        internal static bool Import_ToXML_Inventura(int countentries, bool inv_edn, string ConnectionString, string Path)
        {
            try
            {
                SQL_Datasets.InventuraTableAdapters.CZMST_I4_GrupTableAdapter ta = new SQL_Datasets.InventuraTableAdapters.CZMST_I4_GrupTableAdapter();
                ta.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);


                SQL_Datasets.InventuraTableAdapters.CZMST_I1_QTY_TableAdapter taQTY = new SQL_Datasets.InventuraTableAdapters.CZMST_I1_QTY_TableAdapter();
                taQTY.Connection = new System.Data.SqlClient.SqlConnection(ConnectionString);


                SQL_Datasets.Inventura.CZMST_I4_GrupDataTable dt_polozky = ta.GetDataGroupByQTY(countentries);


                //dt_polozky.WriteXml(Path);

                List<XElement> Rows = new List<XElement>();


                foreach (var item in dt_polozky)
                {
                    decimal? QTY_Orig = null;

                     var dt = taQTY.GetData(item.CountEntries, item.ITEMNMBR.Trim());

                     if ((dt != null) && (dt.Count > 0))
                    {
                        QTY_Orig = dt.First().QUANTITY;
                    }


                    Rows.Add(new XElement("ITEM",
                        new XElement("COUNTENTRIES", item.CountEntries),
                        new XElement("ID", item.ITEMNMBR.Trim()),
                        new XElement("CODE", item.CZ_CarKod.Trim()),
                        new XElement("STORE_CODE", item.SKL_ID.Trim()),
                        new XElement("UNIT", item.MJ.Trim()),
                        new XElement("QUANTITY", item.QUANTITY),
                        new XElement("QUANTITY_ORIG", QTY_Orig),
                        new XElement("USERID", item.USERID)
                        ));
		 
                }


                XElement root = new XElement("STOCK", Rows);

                root.Save(Path);


                return true;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }
    }
}
