using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Extension
{
    public static class CZMST093_Extension
    {

        public static Fask.WEBAPI.API_BusinessObjects.BO_CZMST093 DataSetToBO(this Fask.Interfaces.DataSets.Sklady ds)
        {
            try
            {
                Fask.WEBAPI.API_BusinessObjects.BO_CZMST093 listObjektu_FZ = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST093(); //vytvoreni tabulky
                List<Fask.WEBAPI.API_BusinessObjects.BO_CZMST093_row> rowList = new List<Fask.WEBAPI.API_BusinessObjects.BO_CZMST093_row>(); //vytvoreni docasne tabulky

                foreach (var item in ds.CZMST093)
                {
                    Fask.WEBAPI.API_BusinessObjects.BO_CZMST093_row row = new Fask.WEBAPI.API_BusinessObjects.BO_CZMST093_row(); //docasny radek


                    #region FASK_ZASOBY_ALL 
                    row.DEX_ROW_ID = item.DEX_ROW_ID;

                    row.skl_id = string.IsNullOrEmpty(item.skl_id) ? string.Empty : item.skl_id;

                    row.skl_desc = string.IsNullOrEmpty(item.skl_desc) ? string.Empty : item.skl_desc;

                    row.skl_typ = string.IsNullOrEmpty(item.skl_typ) ? string.Empty : item.skl_typ;

                    row.skl_carcode = string.IsNullOrEmpty(item.skl_carcode) ? string.Empty : item.skl_carcode;
                    #endregion


                    rowList.Add(row); //pridani docasneho radku do docasne tabulky

                }

                listObjektu_FZ.rows = rowList.ToArray(); // prekopirovani docasne tabulky a konvertovani na celou obalku

                return listObjektu_FZ;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }



        public static Fask.Interfaces.DataSets.Sklady BOToDataSet(this Fask.WEBAPI.API_BusinessObjects.BO_CZMST093 bo)
        {

            try
            {
                Fask.Interfaces.DataSets.Sklady ds = new Fask.Interfaces.DataSets.Sklady();

                foreach (var item in bo.rows.ToList<Fask.WEBAPI.API_BusinessObjects.BO_CZMST093_row>())
                {
                    var x = ds.CZMST093.NewCZMST093Row();


                    #region kontrola a plneni datasetu

                    x.DEX_ROW_ID = item.DEX_ROW_ID;

                    x.skl_id = string.IsNullOrEmpty(item.skl_id) ? string.Empty : item.skl_id.Trim();

                    x.skl_desc = string.IsNullOrEmpty(item.skl_desc) ? string.Empty : item.skl_desc.Trim();

                    x.skl_typ = string.IsNullOrEmpty(item.skl_typ) ? string.Empty : item.skl_typ.Trim();

                    x.skl_carcode = string.IsNullOrEmpty(item.skl_carcode) ? string.Empty : item.skl_carcode.Trim();

                    #endregion


                    ds.CZMST093.AddCZMST093Row(x);


                }

                ds.CZMST093.AcceptChanges();

                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
