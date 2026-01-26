using Fask.BO;
using Fask.Console.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Extension
{
    public static class Fask_Zasoby_Extension
    {

        public static FASK_ZASOBY DataSetToBO(this Zbozi ds)
        {
            FASK_ZASOBY listObjektu_FZ = new FASK_ZASOBY(); //vytvoreni tabulky
            List<FASK_ZASOBY_row> rowList = new List<FASK_ZASOBY_row>(); //vytvoreni docasne tabulky

            foreach (var item in ds.FASK_ZASOBY)
            {
                FASK_ZASOBY_row row = new FASK_ZASOBY_row(); //docasny radek


                #region FASK_ZASOBY 
                row.DEX_ROW_ID = item.DEX_ROW_ID;

                row.ITEMNMBR = item.ITEMNMBR;

                row.ITEMDESC = item.IsITEMDESCNull() ? null : item.ITEMDESC.Trim();

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

                row.CZ_Rez1_Track = item.CZ_Rez1_Track; //zmenit

                row.CZ_Rez2_Track = item.CZ_Rez2_Track; //zmenit

                row.CZ_Rez3_Track = item.CZ_Rez3_Track; //zmenit

                row.CZ_Rez4_Track = item.CZ_Rez4_Track; //zmenit

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

                row.SKL_DESC = item.IsSKL_DESCNull() ? null : item.SKL_DESC.Trim();

                #endregion


                rowList.Add(row); //pridani docasneho radku do docasne tabulky

            }

            listObjektu_FZ.rows = rowList.ToArray(); // prekopirovani docasne tabulky a konvertovani na celou obalku

            return listObjektu_FZ;


        }



        public static Zbozi BOToDataSet(this FASK_ZASOBY bo)
        {

            Zbozi ds = new Zbozi();

            foreach (var item in bo.rows.ToList<FASK_ZASOBY_row>())
            {
                var x = ds.FASK_ZASOBY.NewFASK_ZASOBYRow();


                #region kontrola a plneni datasetu

                x.DEX_ROW_ID = item.DEX_ROW_ID;

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

                x.REZ1 = string.IsNullOrEmpty(item.REZ1) ? string.Empty : item.REZ1.Trim();

                x.REZ2 = string.IsNullOrEmpty(item.REZ2) ? string.Empty : item.REZ2.Trim();

                x.REZ3 = string.IsNullOrEmpty(item.REZ3) ? string.Empty : item.REZ3.Trim();

                x.REZ4 = string.IsNullOrEmpty(item.REZ4) ? string.Empty : item.REZ4.Trim();

                x.ODB_ID = string.IsNullOrEmpty(item.ODB_ID) ? string.Empty : item.ODB_ID.Trim();

                x.mena_ID = string.IsNullOrEmpty(item.mena_ID) ? string.Empty : item.mena_ID.Trim();

                x.SERLTNUM = string.IsNullOrEmpty(item.SERLTNUM) ? string.Empty : item.SERLTNUM.Trim();

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

                x.SKL_DESC = string.IsNullOrEmpty(item.SKL_DESC) ? string.Empty : item.SKL_DESC.Trim();

                #endregion


                ds.FASK_ZASOBY.AddFASK_ZASOBYRow(x);


            }

            ds.FASK_ZASOBY.AcceptChanges();

            return ds;


        }
    }
}
