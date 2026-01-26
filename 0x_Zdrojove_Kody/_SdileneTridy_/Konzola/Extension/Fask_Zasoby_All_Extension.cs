using Fask.BO;
using Fask.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Extension
{
    public static class Fask_Zasoby_All_Extension
    {

        public static FASK_ZASOBY_ALL DataSetToBO(this Zbozi ds)
        {
            FASK_ZASOBY_ALL listObjektu_FZ = new FASK_ZASOBY_ALL(); //vytvoreni tabulky
            List<FASK_ZASOBY_ALL_row> rowList = new List<FASK_ZASOBY_ALL_row>(); //vytvoreni docasne tabulky

            foreach (var item in ds.FASK_ZASOBY_ALL_KONZOLA)
            {
                FASK_ZASOBY_ALL_row row = new FASK_ZASOBY_ALL_row(); //docasny radek


                #region FASK_ZASOBY_ALL 
                row.DEX_ROW_ID_Zbozi = item.DEX_ROW_ID_Zbozi;

                row.ITEMNMBR =  item.ITEMNMBR;

                row.ITEMDESC = item.IsITEMDESCNull() ? null : item.ITEMNMBR.Trim();

                row.ITEMCODE = item.IsITEMCODENull() ? null : item.ITEMCODE.Trim();

                row.VNDITNUM = item.IsVNDITNUMNull() ? null : item.VNDITNUM.Trim();

                row.CZ_CarKod = item.IsCZ_CarKodNull() ? null : item.CZ_CarKod.Trim(); //naplneni docasneho radku

                row.LOCNCODE = item.IsLOCNCODENull() ? null : item.LOCNCODE.Trim();

                row.SKL_ID = item.IsSKL_IDNull() ? null : item.SKL_ID.Trim();

                row.QTY = item.QTY; //zmenit

                row.QTYPACK = item.IsQTYPACKNull() ? null : (decimal?)item.QTYPACK;

                row.MJ = string.IsNullOrEmpty(item.MJ) ? string.Empty : item.MJ; //zmenit

                row.DMJ = string.IsNullOrEmpty(item.DMJ) ? string.Empty : item.DMJ; //zmenit

                row.TAXRATE = item.IsTAXRATENull() ? null : (decimal?)item.TAXRATE;

                row.PRICE0 = item.IsPRICE0Null() ? null : (decimal?)item.PRICE0;

                row.PRICE1 = item.IsPRICE1Null() ? null : (decimal?)item.PRICE1;

                row.PRICE2 = item.IsPRICE2Null() ? null : (decimal?)item.PRICE2;

                row.PRICE3 = item.IsPRICE3Null() ? null : (decimal?)item.PRICE3;

                row.PRICE4 = item.IsPRICE4Null() ? null : (decimal?)item.PRICE4;

                row.PRICE5 = item.IsPRICE5Null() ? null : (decimal?)item.PRICE5;

                row.CZ_SerNum_Track = item.CZ_SerNum_Track; //zmenit

                row.CZ_SerNum_Delka = item.CZ_SerNum_Delka; //zmenit

                row.CZ_Rez1_Track =  item.CZ_Rez1_Track; //zmenit

                row.CZ_Rez2_Track =  item.CZ_Rez2_Track; //zmenit

                row.CZ_Rez3_Track = item.CZ_Rez3_Track; //zmenit

                row.CZ_Rez4_Track =  item.CZ_Rez4_Track; //zmenit

                row.REZ1 = item.IsREZ1Null() ? null : item.REZ1.Trim();

                row.REZ2 = item.IsREZ2Null() ? null : item.REZ2.Trim();

                row.REZ3 = item.IsREZ3Null() ? null : item.REZ3.Trim();

                row.REZ4 = item.IsREZ4Null() ? null : item.REZ4.Trim();

                row.ODB_ID = item.IsODB_IDNull() ? null : item.ODB_ID.Trim();

                row.mena_ID = item.Ismena_IDNull() ? null : item.mena_ID.Trim();

                row.SERLTNUM = item.IsSERLTNUMNull() ? null : item.SERLTNUM.Trim();

                row.WEIGHT = item.IsWEIGHTNull() ? null : (decimal?)item.WEIGHT;

                row.TIMEFROM = item.IsTIMEFROMNull() ? null : (DateTime?)item.TIMEFROM;

                row.TIMETO = item.IsTIMETONull() ? null : (DateTime?)item.TIMETO;

                row.LSTMod = item.IsLSTModNull() ? null : (DateTime?)item.LSTMod;

                row.loginid = item.IsloginidNull() ? null : item.loginid.Trim();

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

                row.DEX_ROW_ID_PARAMETRY = item.DEX_ROW_ID_PARAMETRY;

                row.SKL_DESC = item.IsSKL_DESCNull() ? null : item.SKL_DESC.Trim();

                row.ITEMTYPE = item.IsITEMTYPENull() ? null : item.ITEMTYPE.Trim();

                row.Vetev1 = item.IsVetev1Null() ? null : item.Vetev1.Trim();

                row.Vetev2 = item.IsVetev2Null() ? null : item.Vetev2.Trim();

                row.Vetev3 = item.IsVetev3Null() ? null : item.Vetev3.Trim();

                row.Vetev4 = item.IsVetev4Null() ? null : item.Vetev4.Trim();

                row.Vetev5 = item.IsVetev5Null() ? null : item.Vetev5.Trim();

                row.Vetev6 = item.IsVetev6Null() ? null : item.Vetev6.Trim();

                row.Vetev7 = item.IsVetev7Null() ? null : item.Vetev7.Trim();

                row.ITEMTYPE_Text = item.IsITEMTYPE_TextNull() ? null : item.ITEMTYPE_Text.Trim();
                #endregion


                rowList.Add(row); //pridani docasneho radku do docasne tabulky

            }

            listObjektu_FZ.rows = rowList.ToArray(); // prekopirovani docasne tabulky a konvertovani na celou obalku

            return listObjektu_FZ;


        }



        public static Zbozi BOToDataSet(this FASK_ZASOBY_ALL bo)
        {

            Zbozi ds = new Zbozi();

            foreach (var item in bo.rows.ToList<FASK_ZASOBY_ALL_row>())
            {
                var x = ds.FASK_ZASOBY_ALL_KONZOLA.NewFASK_ZASOBY_ALL_KONZOLARow();


                #region kontrola a plneni datasetu

                x.DEX_ROW_ID_Zbozi = item.DEX_ROW_ID_Zbozi;

                x.ITEMNMBR = item.ITEMNMBR;

                x.ITEMDESC = string.IsNullOrEmpty(item.ITEMDESC) ? string.Empty : item.ITEMDESC.Trim();

                x.ITEMCODE = string.IsNullOrEmpty(item.ITEMCODE) ? string.Empty : item.ITEMCODE.Trim();

                x.VNDITNUM = string.IsNullOrEmpty(item.VNDITNUM) ? string.Empty : item.VNDITNUM.Trim();

                x.CZ_CarKod = string.IsNullOrEmpty(item.CZ_CarKod) ? string.Empty : item.CZ_CarKod.Trim();

                x.LOCNCODE = string.IsNullOrEmpty(item.LOCNCODE) ? string.Empty : item.LOCNCODE.Trim();

                x.SKL_ID = string.IsNullOrEmpty(item.SKL_ID) ? string.Empty : item.SKL_ID.Trim();

                x.QTY = item.QTY; //zmenit

                if (item.QTYPACK.HasValue)
                {
                    x.QTYPACK = item.QTYPACK.Value;
                }
                else
                {
                    x.SetQTYPACKNull();
                }

                x.MJ = string.IsNullOrEmpty(item.MJ) ? string.Empty : item.MJ; //zmenit

                x.DMJ = string.IsNullOrEmpty(item.DMJ) ? string.Empty : item.DMJ; //zmenit

                if (item.TAXRATE.HasValue)
                {
                    x.TAXRATE = item.TAXRATE.Value;
                }
                else
                {
                    x.SetTAXRATENull();
                }

                if (item.PRICE0.HasValue)
                {
                    x.PRICE0 = item.PRICE0.Value;
                }
                else
                {
                    x.SetPRICE0Null();
                }

                if (item.PRICE1.HasValue)
                {
                    x.PRICE1 = item.PRICE1.Value;
                }
                else
                {
                    x.SetPRICE1Null();
                }

                if (item.PRICE2.HasValue)
                {
                    x.PRICE2 = item.PRICE2.Value;
                }
                else
                {
                    x.SetPRICE2Null();
                }

                if (item.PRICE3.HasValue)
                {
                    x.PRICE3 = item.PRICE3.Value;
                }
                else
                {
                    x.SetPRICE3Null();
                }

                if (item.PRICE4.HasValue)
                {
                    x.PRICE4 = item.PRICE4.Value;
                }
                else
                {
                    x.SetPRICE4Null();
                }

                if (item.PRICE5.HasValue)
                {
                    x.PRICE5 = item.PRICE5.Value;
                }
                else
                {
                    x.SetPRICE5Null();
                }

                x.CZ_SerNum_Track = item.CZ_SerNum_Track; //zmenit

                x.CZ_SerNum_Delka = item.CZ_SerNum_Delka; //zmenit

                x.CZ_Rez1_Track = item.CZ_Rez1_Track; //zmenit

                x.CZ_Rez2_Track = item.CZ_Rez2_Track; //zmenit

                x.CZ_Rez3_Track = item.CZ_Rez3_Track; //zmenit

                x.CZ_Rez4_Track = item.CZ_Rez4_Track; //zmenit

                x.REZ1 = string.IsNullOrEmpty(item.VNDITNUM) ? string.Empty : item.VNDITNUM.Trim();

                x.REZ2 = string.IsNullOrEmpty(item.VNDITNUM) ? string.Empty : item.VNDITNUM.Trim();

                x.REZ3 = string.IsNullOrEmpty(item.VNDITNUM) ? string.Empty : item.VNDITNUM.Trim();

                x.REZ4 = string.IsNullOrEmpty(item.VNDITNUM) ? string.Empty : item.VNDITNUM.Trim();

                x.ODB_ID = string.IsNullOrEmpty(item.VNDITNUM) ? string.Empty : item.VNDITNUM.Trim();

                x.mena_ID = string.IsNullOrEmpty(item.VNDITNUM) ? string.Empty : item.VNDITNUM.Trim();

                x.SERLTNUM = string.IsNullOrEmpty(item.VNDITNUM) ? string.Empty : item.VNDITNUM.Trim();

                if (item.WEIGHT.HasValue)
                {
                    x.WEIGHT = item.WEIGHT.Value;
                }
                else
                {
                    x.SetWEIGHTNull();
                }

                if (item.TIMEFROM.HasValue)
                {
                    x.TIMEFROM = item.TIMEFROM.Value;
                }
                else
                {
                    x.SetTIMEFROMNull();
                }

                if (item.TIMETO.HasValue)
                {
                    x.TIMETO = item.TIMETO.Value;
                }
                else
                {
                    x.SetTIMETONull();
                }

                if (item.LSTMod.HasValue)
                {
                    x.LSTMod = item.LSTMod.Value;
                }
                else
                {
                    x.SetLSTModNull();
                }

                    x.loginid = item.loginid;
               
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

                x.DEX_ROW_ID_PARAMETRY = item.DEX_ROW_ID_PARAMETRY;

                x.SKL_DESC = string.IsNullOrEmpty(item.SKL_DESC) ? string.Empty : item.SKL_DESC.Trim();

                x.ITEMTYPE = string.IsNullOrEmpty(item.ITEMTYPE) ? string.Empty : item.ITEMTYPE.Trim();

                x.Vetev1 = string.IsNullOrEmpty(item.Vetev1) ? string.Empty : item.Vetev1.Trim();

                x.Vetev2 = string.IsNullOrEmpty(item.Vetev2) ? string.Empty : item.Vetev2.Trim();

                x.Vetev3 = string.IsNullOrEmpty(item.Vetev3) ? string.Empty : item.Vetev3.Trim();

                x.Vetev4 = string.IsNullOrEmpty(item.Vetev4) ? string.Empty : item.Vetev4.Trim();

                x.Vetev5 = string.IsNullOrEmpty(item.Vetev5) ? string.Empty : item.Vetev5.Trim();

                x.Vetev6 = string.IsNullOrEmpty(item.Vetev6) ? string.Empty : item.Vetev6.Trim();

                x.Vetev7 = string.IsNullOrEmpty(item.Vetev7) ? string.Empty : item.Vetev7.Trim();

                x.ITEMTYPE_Text = string.IsNullOrEmpty(item.ITEMTYPE_Text) ? string.Empty : item.ITEMTYPE_Text.Trim();

                #endregion


                ds.FASK_ZASOBY_ALL_KONZOLA.AddFASK_ZASOBY_ALL_KONZOLARow(x);


            }

            ds.FASK_ZASOBY_ALL_KONZOLA.AcceptChanges();
            
            return ds;


        }
    }
}
