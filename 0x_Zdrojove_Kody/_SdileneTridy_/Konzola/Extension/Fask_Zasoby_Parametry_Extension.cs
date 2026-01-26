using Fask.BO;
using Fask.Console.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Extension
{
    public static class Fask_Zasoby_Parametry_Extension
    {

        public static FASK_ZASOBY_PARAMETRY DataSetToBO(this Zbozi.FASK_ZASOBY_PARAMETRYDataTable dt)
        {
            FASK_ZASOBY_PARAMETRY listObjektu_FZ = new FASK_ZASOBY_PARAMETRY(); //vytvoreni tabulky
            List<FASK_ZASOBY_PARAMETRY_row> rowList = new List<FASK_ZASOBY_PARAMETRY_row>(); //vytvoreni docasne tabulky

            foreach (var item in dt)
            {
                FASK_ZASOBY_PARAMETRY_row row = new FASK_ZASOBY_PARAMETRY_row(); //docasny radek

                #region FASK_ZASOBY_PARAMETRY 
                row.DEX_ROW_ID = item.DEX_ROW_ID;

                row.ITEMNMBR = item.ITEMNMBR;

                row.VPrFVTS = item.IsVPrFVTSNull() ? false : item.VPrFVTS;

                row.VPrFPTS = item.IsVPrFPTSNull() ? false : item.VPrFPTS;

                row.VPrFDTS = item.IsVPrFDTSNull() ? false : item.VPrFDTS;

                row.VPrFITS = item.IsVPrFITSNull() ? false : item.VPrFITS;

                row.VPrFXTS = item.IsVPrFXTSNull() ? false : item.VPrFXTS;

                row.RefVPrFVTS = item.IsRefVPrFVTSNull() ? null : (int?)item.RefVPrFVTS;

                row.RefVPrFPTS = item.IsRefVPrFPTSNull() ? null : (int?)item.RefVPrFPTS;

                row.RefVPrFDTS = item.IsRefVPrFDTSNull() ? null : (int?)item.RefVPrFDTS;

                row.RefVPrFITS = item.IsRefVPrFITSNull() ? null : (int?)item.RefVPrFITS;

                row.RefVPrFXTS = item.IsRefVPrFXTSNull() ? null : (int?)item.RefVPrFXTS;

                row.VPrTIMEPREP = item.IsVPrTIMEPREPNull() ? null : (double?)item.VPrTIMEPREP;

                row.VPrTIMEUNIT = item.IsVPrTIMEUNITNull() ? null : (double?)item.VPrTIMEUNIT;

                row.RefVPrTIMEMODE = item.IsRefVPrTIMEMODENull() ? null : (int?)item.RefVPrTIMEMODE;

                #endregion


                rowList.Add(row); //pridani docasneho radku do docasne tabulky

            }

            listObjektu_FZ.rows = rowList.ToArray(); // prekopirovani docasne tabulky a konvertovani na celou obalku

            return listObjektu_FZ;


        }



        public static Zbozi.FASK_ZASOBY_PARAMETRYDataTable BOToDataSet(this FASK_ZASOBY_PARAMETRY bo)
        {

            Zbozi.FASK_ZASOBY_PARAMETRYDataTable dt = new Zbozi.FASK_ZASOBY_PARAMETRYDataTable();

            foreach (var item in bo.rows.ToList<FASK_ZASOBY_PARAMETRY_row>())
            {
                var x = dt.NewFASK_ZASOBY_PARAMETRYRow();


                #region kontrola a plneni datasetu

                x.DEX_ROW_ID = item.DEX_ROW_ID;

                x.ITEMNMBR = item.ITEMNMBR;

                x.VPrFVTS = item.VPrFVTS;

                x.VPrFPTS = item.VPrFPTS;

                x.VPrFDTS = item.VPrFDTS;

                x.VPrFITS = item.VPrFITS;

                x.VPrFXTS = item.VPrFXTS;


                if (item.RefVPrFVTS.HasValue)
                {
                    x.RefVPrFVTS = item.RefVPrFVTS.Value;
                }
                else
                {
                    x.SetRefVPrFVTSNull();
                }

                if (item.RefVPrFPTS.HasValue)
                {
                    x.RefVPrFPTS = item.RefVPrFPTS.Value;
                }
                else
                {
                    x.SetRefVPrFPTSNull();
                }

                if (item.RefVPrFDTS.HasValue)
                {
                    x.RefVPrFDTS = item.RefVPrFDTS.Value;
                }
                else
                {
                    x.SetRefVPrFDTSNull();
                }

                if (item.RefVPrFITS.HasValue)
                {
                    x.RefVPrFITS = item.RefVPrFITS.Value;
                }
                else
                {
                    x.SetRefVPrFITSNull();
                }

                if (item.RefVPrFXTS.HasValue)
                {
                    x.RefVPrFXTS = item.RefVPrFXTS.Value;
                }
                else
                {
                    x.SetRefVPrFXTSNull();
                }

                if (item.VPrTIMEPREP.HasValue)
                {
                    x.VPrTIMEPREP = item.VPrTIMEPREP.Value;
                }
                else
                {
                    x.SetVPrTIMEPREPNull();
                }

                if (item.VPrTIMEUNIT.HasValue)
                {
                    x.VPrTIMEUNIT = item.VPrTIMEUNIT.Value;
                }
                else
                {
                    x.SetVPrTIMEUNITNull();
                }

                if (item.RefVPrTIMEMODE.HasValue)
                {
                    x.RefVPrTIMEMODE = item.RefVPrTIMEMODE.Value;
                }
                else
                {
                    x.SetRefVPrTIMEMODENull();
                }

                #endregion


                dt.AddFASK_ZASOBY_PARAMETRYRow(x);


            }

            dt.AcceptChanges();

            return dt;


        }
    }
}
