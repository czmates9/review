using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.IO;

namespace Konzola.ImportniMustky.Extensions
{

    [Flags]
    public enum MyErrorEnum
    {
        None = 0,
        DefDod_Error = 1 << 0,
        RefAg_Error = 1 << 1,
        IDS_SKz_Error = 1 << 2,
        ID_sSklad_Error = 1 << 3,
        RefAD_Error = 1 << 4,
        Firma_Error = 1 << 5,
        NakupC_Error = 1 << 6,
        RefCM_Error = 1 << 7,
        CmKurs_Error = 1 << 8,
        EAN_Error = 1 << 9,
        MJEAN_Error = 1 << 10,
        MJkoefEAN_Error = 1 << 11,
        Pozn_Error = 1 << 12,

        DefDod_isEMPTY = 1 << 13,
        IDS_SKz_isEMPTY = 1 << 14,
        ID_sSklad_isEMPTY = 1 << 15,
        EAN_isEMPTY = 1 << 16,
        IDS_SKz_NOT_EXIST_POHODA = 1 << 17,
        ID_sSklad_NOT_EXIST_POHODA = 1 << 18,
        RefAD_NOT_EXIST_POHODA = 1 << 19,
        RefCM_NOT_EXIST_POHODA = 1 << 20,
        RefAg_NOT_EXIST_POHODA = 1 << 21
    }


    public static class DataSet_Import
    {
        public static void GetDataTableFromExcel(
    this Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel ds,
    string path,
    bool hasHeader = true,
    string NameList = "IMPORT",
    int? pocetStloupcu = 12,
    int? StartRow = 1,
    int? StartColumn = 1,
    Fask.Interfaces.IMES prov = null
    )
        {
            //Vytvoreni objektu pro pracu s EXCEL
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                //Nacteni souboru jak stream
                using (var stream = File.OpenRead(path))
                {
                    //Nacteni do objektu EXCEL
                    pck.Load(stream);
                }

                //Jeden list objekt
                OfficeOpenXml.ExcelWorksheet ws = null;

                //Pokud je pouze jeden list tak vzit ten
                if (pck.Workbook.Worksheets.Count == 1)
                    ws = pck.Workbook.Worksheets.First();
                else if (pck.Workbook.Worksheets.Count > 1)
                {
                    //Pokud je víc listů tak 
                    foreach (OfficeOpenXml.ExcelWorksheet item in pck.Workbook.Worksheets)
                    {
                        if (item.Name == NameList)
                        {
                            ws = item;
                            break;
                        }
                    }
                }
                else
                    throw new Exception("Nenalezen list pro import!"); // Pokud neni žadny hodit chybu


                //Pokud nebyla nazelen list tak hodit chybu
                if (ws == null)
                    throw new Exception("Nenalezen list pro import!");


                int cntStopColumn = GetColumns_Count(ws, pocetStloupcu);
                int cntStartRow = GetStartRow_Count(ws, StartRow);
                int cntStartColumn = GetStartColumn_Count(ws, StartColumn);

                try
                {
                    //Vyhledat podle mena tabulku

                    //pokud nebyla tabulka naleze tak se vytvoří
                    if (ds == null)
                    {
                        //Vlozit tabulku podle mena
                        ds = new Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel();
                    }

                    ds.FASK_ZASOBY_IMPORT_POHODA_SKzNC.Clear();
                }
                catch (Exception ex)
                {
                    ds = new Fask.Interfaces.DataSets_Import.ImportPOHODA_FromExcel();
                }

                //Pokud je povolena hlavicka tak vzit pozici start radku + 1, lebo ten jeden je ta hlavička
                //pokud neni hlavicka tak se veme ten radek
                var startRow = hasHeader ? cntStartRow + 1 : cntStartRow;

                //cyklusem projet všechny radky až do konce
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    //[int FromRow, int FromCol, int ToRow, int ToCol]
                    var wsRow = ws.Cells[rowNum, cntStartColumn, rowNum, cntStopColumn];



                    var row = ds.FASK_ZASOBY_IMPORT_POHODA_SKzNC.NewFASK_ZASOBY_IMPORT_POHODA_SKzNCRow();

                    int rowPoz = rowNum;
                    int ColumnPoz = 1;

                    #region Nevyplnen radek
                    try
                    {
                        if (string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text) &&
                            string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz+1].Text) &&
                            string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz+2].Text)
                            )
                            continue;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    #endregion

                    #region DefDod
                    try
                    {

                        //MaR zakomentovano 27.6.2025
                        //if (!string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text))
                        //    row.DefDod = bool.Parse(wsRow[rowPoz, ColumnPoz].Text);
                        //else
                        //{
                        //    row.DefDod = false;
                        //    row.Status_Err += (int)MyErrorEnum.DefDod_isEMPTY;
                        //}


                        //MaR nove 27.6.2025
                        string defDodText = wsRow[rowPoz, ColumnPoz].Text.Trim().ToLower();
                        if (!string.IsNullOrEmpty(defDodText))
                        {
                            if (defDodText == "true" || defDodText == "1" || defDodText == "ano" || defDodText == "x")
                                row.DefDod = true;
                            else if (defDodText == "false" || defDodText == "0" || defDodText == "ne" || defDodText == "")
                                row.DefDod = false;
                            else
                            {
                                // Hodnota je neplatná, nastavíme FALSE a zalogujeme chybu
                                row.DefDod = false;
                                row.Status_Err += (int)MyErrorEnum.DefDod_Error;
                            }
                        }
                        else
                        {
                            // Pokud je hodnota prázdná, nastavíme FALSE jako výchozí
                            row.DefDod = false;
                            row.Status_Err += (int)MyErrorEnum.DefDod_isEMPTY;
                        }




                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        row.DefDod = false;
                        //row.SetDefDodNull();
                        row.Status_Err += (int)MyErrorEnum.DefDod_Error;
                    }

                    ColumnPoz++;
                    #endregion

                    #region IDS_SKz
                    try
                    {

                        if (!string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text))
                        {
                            row.IDS_SKz = wsRow[rowPoz, ColumnPoz].Text.Trim();


                            bool state = true;
                            if ((prov != null) && prov is Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)
                                state = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)prov).EXIST("IDS","SKz", (object)row.IDS_SKz);
                            else
                                throw new Exception("IImportnyMustky_ImportPohoda_SKzNC_EXIST not implementet");

                            if(!state)
                                row.Status_Err += (int)MyErrorEnum.IDS_SKz_NOT_EXIST_POHODA;
                        }
                        else
                        {
                            row.SetIDS_SKzNull();
                            row.Status_Err += (int)MyErrorEnum.IDS_SKz_isEMPTY;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        row.SetIDS_SKzNull();
                        row.Status_Err += (int)MyErrorEnum.IDS_SKz_Error;
                    }

                    ColumnPoz++;
                    #endregion

                    #region ID_sSklad
                    try
                    {

                        if (!string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text))
                        {
                            row.ID_sSklad = int.Parse(wsRow[rowPoz, ColumnPoz].Text);

                            bool state = true;
                            if ((prov != null) && prov is Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)
                                state = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)prov).EXIST("ID", "sSklad", (object)row.ID_sSklad);
                            else
                                throw new Exception("IImportnyMustky_ImportPohoda_SKzNC_EXIST not implementet");

                            if (!state)
                                row.Status_Err += (int)MyErrorEnum.ID_sSklad_NOT_EXIST_POHODA;
                        }
                        else
                        {
                            row.SetID_sSkladNull();
                            row.Status_Err += (int)MyErrorEnum.ID_sSklad_isEMPTY;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        row.SetID_sSkladNull();
                        row.Status_Err += (int)MyErrorEnum.ID_sSklad_Error;
                    }

                    ColumnPoz++;
                    #endregion

                    #region RefAD
                    try
                    {

                        if (!string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text))
                        {
                            row.RefAD = int.Parse(wsRow[rowPoz, ColumnPoz].Text);

                            bool state = true;
                            if ((prov != null) && prov is Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)
                                state = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)prov).EXIST("ID", "AD", (object)row.RefAD);
                            else
                                throw new Exception("IImportnyMustky_ImportPohoda_SKzNC_EXIST not implementet");

                            if (!state)
                                row.Status_Err += (int)MyErrorEnum.RefAD_NOT_EXIST_POHODA;
                        }
                        else
                            row.SetRefADNull();
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        row.SetRefADNull();
                        row.Status_Err += (int)MyErrorEnum.RefAD_Error;
                    }

                    ColumnPoz++;
                    #endregion

                    #region Firma
                    try
                    {

                        if (!string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text))
                            row.Firma = wsRow[rowPoz, ColumnPoz].Text.Trim();
                        else
                            row.SetFirmaNull();
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        row.SetFirmaNull();
                        row.Status_Err += (int)MyErrorEnum.Firma_Error;
                    }

                    ColumnPoz++;
                    #endregion

                    #region NakupC
                    try
                    {

                        if (!string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text))
                            row.NakupC = decimal.Parse(GetDecimalWithComma(wsRow[rowPoz, ColumnPoz].Text));
                        else
                            row.SetNakupCNull();
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        row.SetNakupCNull();
                        row.Status_Err += (int)MyErrorEnum.NakupC_Error;
                    }

                    ColumnPoz++;
                    #endregion

                    #region RefCM
                    try
                    {

                        if (!string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text))
                        {
                            row.RefCM = int.Parse(wsRow[rowPoz, ColumnPoz].Text);

                            bool state = true;
                            if ((prov != null) && prov is Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)
                                state = ((Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST)prov).EXIST("ID", "sCMeny", (object)row.RefCM);
                            else
                                throw new Exception("IImportnyMustky_ImportPohoda_SKzNC_EXIST not implementet");

                            if (!state)
                                row.Status_Err += (int)MyErrorEnum.RefCM_NOT_EXIST_POHODA;

                        }
                        else
                            row.SetRefCMNull();
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        row.SetRefCMNull();
                        row.Status_Err += (int)MyErrorEnum.RefCM_Error;
                    }

                    ColumnPoz++;
                    #endregion

                    #region CmKurs
                    try
                    {

                        if (!string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text))
                            row.CmKurs = decimal.Parse(GetDecimalWithComma(wsRow[rowPoz, ColumnPoz].Text));
                        else
                            row.SetCmKursNull();
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        row.SetCmKursNull();
                        row.Status_Err += (int)MyErrorEnum.CmKurs_Error;
                    }

                    ColumnPoz++;
                    #endregion

                    #region EAN
                    try
                    {

                        if (!string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text))
                            row.EAN = wsRow[rowPoz, ColumnPoz].Text.Trim();
                        else
                        {
                            row.SetEANNull();
                            row.Status_Err += (int)MyErrorEnum.EAN_isEMPTY;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        row.SetEANNull();
                        row.Status_Err += (int)MyErrorEnum.EAN_Error;
                    }

                    ColumnPoz++;
                    #endregion

                    #region MJEAN
                    try
                    {

                        if (!string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text))
                            row.MJEAN = wsRow[rowPoz, ColumnPoz].Text.Trim();
                        else
                            row.SetMJEANNull();
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        row.SetMJEANNull();
                        row.Status_Err += (int)MyErrorEnum.MJEAN_Error;
                    }

                    ColumnPoz++;
                    #endregion

                    #region MJkoefEAN
                    try
                    {

                        if (!string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text))
                            row.MJkoefEAN = decimal.Parse(GetDecimalWithComma(wsRow[rowPoz, ColumnPoz].Text));
                        else
                            row.SetMJkoefEANNull();
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        row.SetMJkoefEANNull();
                        row.Status_Err += (int)MyErrorEnum.MJkoefEAN_Error;
                    }

                    ColumnPoz++;
                    #endregion

                    #region Pozn
                    try
                    {

                        if (!string.IsNullOrEmpty(wsRow[rowPoz, ColumnPoz].Text))
                            row.Pozn = wsRow[rowPoz, ColumnPoz].Text.Trim();
                        else
                            row.SetPoznNull();
                    }
                    catch (System.Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                        row.SetPoznNull();
                        row.Status_Err += (int)MyErrorEnum.Pozn_Error;
                    } 
                    #endregion

                    ds.FASK_ZASOBY_IMPORT_POHODA_SKzNC.AddFASK_ZASOBY_IMPORT_POHODA_SKzNCRow(row);

                }

               // ds.FASK_ZASOBY_IMPORT_POHODA_SKzNC.AcceptChanges();
            }
        }

        private static int GetColumns_Count(OfficeOpenXml.ExcelWorksheet ws, int? pocetStloupcu)
        {
            //Nadefinovany počet stlouzpců ktere se maju využit
            if (pocetStloupcu.HasValue)
                return pocetStloupcu.Value;
            else
                return ws.Dimension.End.Column; // Vsechny stloupce s nečim vyplnenim asi
        }

        private static int GetStartRow_Count(OfficeOpenXml.ExcelWorksheet ws, int? pocetStloupcu)
        {
            //Nadefinovany počet stlouzpců ktere se maju využit
            if (pocetStloupcu.HasValue)
                return pocetStloupcu.Value;
            else
                return 1;
        }

        private static int GetStartColumn_Count(OfficeOpenXml.ExcelWorksheet ws, int? pocetStloupcu)
        {
            //Nadefinovany počet stlouzpců ktere se maju využit
            if (pocetStloupcu.HasValue)
                return pocetStloupcu.Value;
            else
                return 1;
        }

        private static string GetDecimalWithComma(string txt)
        {
            return txt.Replace(".", ",");
        }

    }
}
