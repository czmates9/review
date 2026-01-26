using Fask.BO;
using Fask.Console.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Extension
{
    public static class CZMST093_Extension
    {

        public static CZMST093 DataSetToBO(this Sklady ds)
        {
            CZMST093 listObjektu_FZ = new CZMST093(); //vytvoreni tabulky
            List<CZMST093_row> rowList = new List<CZMST093_row>(); //vytvoreni docasne tabulky

            foreach (var item in ds.CZMST093)
            {
                CZMST093_row row = new CZMST093_row(); //docasny radek


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



        public static Sklady BOToDataSet(this CZMST093 bo)
        {

            Sklady ds = new Sklady();

            foreach (var item in bo.rows.ToList<CZMST093_row>())
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
    }
}
