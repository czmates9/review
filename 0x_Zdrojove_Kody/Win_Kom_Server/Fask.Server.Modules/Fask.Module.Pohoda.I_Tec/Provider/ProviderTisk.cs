using Fask.Server.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Pohoda.I_Tec.Provider
{
    public partial class Provider :
        Fask.Server.Interfaces.Tisky.ITisky2,
        Fask.Server.Interfaces.Tisky.ITisky2_MetodaSoupis
    {
        public bool TiskMetodaSoupis(ref DSValues dataHeader, ref List<DSValues> dataRowList, ref DSValues dataFooter)
        {
            try
            {
                string SOPNUMBE = string.Empty;

                if (dataRowList != null && dataRowList.Count > 0)
                {


                    #region Vypočet vahy

                    //decimal SUM = 0;

                    //foreach (var ds in dataRowList)
                    //{
                        
                    //    foreach (Fask.Server.Interfaces.DataSets.DSValues.ValuesRow item in ds.Values)
                    //    {
                    //        decimal? tmp = GetValue(item, "GROSSWEIGHT");

                    //        if (tmp.HasValue)
                    //        {
                    //            SUM += tmp.Value;
                    //            continue;
                    //        }
                    //    }
                    //}

                    //dataHeader.Values.BeginLoadData();
                    //dataHeader.Values.AddValuesRow("GROSSWEIGHT_SUM", SUM.ToString("0.000"));
                    //dataHeader.Values.EndLoadData();
                    //dataHeader.AcceptChanges();

                    #endregion

                    #region Objem

                    foreach (var ds in dataRowList)
                    {
                        decimal? DIMENSIONWIDTH = 0;
                        decimal? DIMENSIONHEIGHT = 0;
                        decimal? DIMENSIONDEPTH = 0;
                        decimal Objem = 0;
                   
                        foreach (Fask.Server.Interfaces.DataSets.DSValues.ValuesRow item in ds.Values)
                        {

                            if (item.Key == "DIMENSIONWIDTH")
                            {
                                DIMENSIONWIDTH = GetValue(item, "DIMENSIONWIDTH");

                                if (!DIMENSIONWIDTH.HasValue)
                                    DIMENSIONWIDTH = 0;
                            }

                            if (item.Key == "DIMENSIONHEIGHT")
                            {
                                DIMENSIONHEIGHT = GetValue(item, "DIMENSIONHEIGHT");

                                if (!DIMENSIONHEIGHT.HasValue)
                                    DIMENSIONHEIGHT = 0;
                            }

                            if (item.Key == "DIMENSIONDEPTH")
                            {
                                DIMENSIONDEPTH = GetValue(item, "DIMENSIONDEPTH");

                                if (!DIMENSIONDEPTH.HasValue)
                                    DIMENSIONDEPTH = 0;
                            }
                        }

                        Objem = (DIMENSIONWIDTH.Value/1000) * (DIMENSIONHEIGHT.Value/1000) * (DIMENSIONDEPTH.Value/1000);
                        
                        ds.Values.BeginLoadData();
                        ds.Values.AddValuesRow("OBJEM", Objem.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture));
                        ds.Values.EndLoadData();
                        ds.AcceptChanges();

                    }

                    IDictionary<string, SumyHodnot> valuePairs = new Dictionary<string, SumyHodnot>();

                    foreach (var ds in dataRowList)
                    {
                        decimal? Vaha = 0;
                        decimal? Objem = 0;
                        string NMBRPAL = string.Empty;

                        foreach (Fask.Server.Interfaces.DataSets.DSValues.ValuesRow item in ds.Values)
                        {
                            if(item.Key == "NMBRPAL")
                            {
                                NMBRPAL = item.Value;
                            }

                            if (item.Key == "GROSSWEIGHT")
                            {
                                Vaha = GetValue(item, "GROSSWEIGHT");
                            }

                            if (item.Key == "OBJEM")
                            {
                                Objem = GetValue(item, "OBJEM");
                            }

                        }


                        if (!string.IsNullOrEmpty(NMBRPAL))
                        {

                            if (!valuePairs.ContainsKey(NMBRPAL))
                            {
                                valuePairs.Add(NMBRPAL, new SumyHodnot(
                                    Vaha.HasValue ? Vaha.Value : 0,
                                    Objem.HasValue ? Objem.Value : 0));
                            } 
                        }

                    }

                    decimal? Vaha_SUM = 0;
                    decimal? Objem_SUM = 0;

                    foreach (var item in valuePairs)
                    {
                        Vaha_SUM += item.Value.Vaha;
                        Objem_SUM += item.Value.Objem;
                    }


                    dataHeader.Values.BeginLoadData();
                    dataHeader.Values.AddValuesRow("OBJEM_SUM", (Objem_SUM.HasValue ? Objem_SUM.Value : 0).ToString("0.000", System.Globalization.CultureInfo.InvariantCulture));
                    dataHeader.Values.AddValuesRow("GROSSWEIGHT_SUM", (Vaha_SUM.HasValue ? Vaha_SUM.Value : 0).ToString("0.000", System.Globalization.CultureInfo.InvariantCulture));
                    dataHeader.Values.EndLoadData();
                    dataHeader.AcceptChanges();

                    #endregion


                    foreach (Fask.Server.Interfaces.DataSets.DSValues.ValuesRow item in dataRowList[0].Values)
                    {
                        if (item.Key == "SOPNUMBE")
                        {
                            SOPNUMBE = item.Value;
                            break;
                        }
                    }


                    if (string.IsNullOrEmpty(SOPNUMBE))
                        return true;


                    dataHeader.Values.BeginLoadData();

                    #region Adresa
                    //Konfiguračne možnost dotahovat adresu online pomoci detailu

                    string Firma = string.Empty;
                    string Utvar = string.Empty;
                    string Jmeno = string.Empty;
                    string Ulice = string.Empty;
                    string PSC = string.Empty;
                    string Obec = string.Empty;
                    string ICO = string.Empty;
                    string DIC = string.Empty;

                    //DataSet data = GetAdresa(SOPNUMBESeznam[0]);
                    DataSet data = Database.Pohoda.Tisk_GetData(SOPNUMBE);

                    if (data.Tables.Count == 0 || (data.Tables.Count > 0 && data.Tables[0].Rows.Count == 0))
                    {
                        //e.Graphics.DrawString("Žádná data k dispozici", fnt, solid, x, y);
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Server nevratil žadnou adresu");
                    }
                    else
                    {
                        DataTable dtAdresa = data.Tables[0];

                        if (dtAdresa.Rows.Count == 1)
                        {
                            DataRow dwAdresa = dtAdresa.Rows[0];

                            string Row_Firma = dwAdresa["Firma"] is string ? (string)dwAdresa["Firma"] : null;
                            string Row_Utvar = dwAdresa["Utvar"] is string ? (string)dwAdresa["Utvar"] : null;
                            string Row_Jmeno = dwAdresa["Jmeno"] is string ? (string)dwAdresa["Jmeno"] : null;
                            string Row_Ulice = dwAdresa["Ulice"] is string ? (string)dwAdresa["Ulice"] : null;
                            string Row_PSC = dwAdresa["PSC"] is string ? (string)dwAdresa["PSC"] : null;
                            string Row_Obec = dwAdresa["Obec"] is string ? (string)dwAdresa["Obec"] : null;

                            string Row_Firma2 = dwAdresa["Firma2"] is string ? (string)dwAdresa["Firma2"] : null;
                            string Row_Utvar2 = dwAdresa["Utvar2"] is string ? (string)dwAdresa["Utvar2"] : null;
                            string Row_Jmeno2 = dwAdresa["Jmeno2"] is string ? (string)dwAdresa["Jmeno2"] : null;
                            string Row_Ulice2 = dwAdresa["Ulice2"] is string ? (string)dwAdresa["Ulice2"] : null;
                            string Row_PSC2 = dwAdresa["PSC2"] is string ? (string)dwAdresa["PSC2"] : null;
                            string Row_Obec2 = dwAdresa["Obec2"] is string ? (string)dwAdresa["Obec2"] : null;


                            string Row_ICO = dwAdresa["ICO"] is string ? (string)dwAdresa["ICO"] : null;
                            string Row_DIC = dwAdresa["DIC"] is string ? (string)dwAdresa["DIC"] : null;


                            #region Rozpad

                            // TODO TaD dodelat logiku rozpadu na jednotlive + konfigurace

                            //if (false)
                            //{
                            //    try
                            //    {
                            //        if (!Row.IsFirma2Null())
                            //        {
                            //            if (string.IsNullOrEmpty(Row.Firma2.Trim()))
                            //                Firma = "-";
                            //            else
                            //                Firma = Row.Firma2.Trim();
                            //        }
                            //        else
                            //        {
                            //            if (Row.IsFirmaNull())
                            //                Firma = "-";
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(Row.Firma.Trim()))
                            //                    Firma = "-";
                            //                else
                            //                    Firma = Row.Firma.Trim();
                            //            }
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        Firma = "-";
                            //        Logging.Log.Write(ex);
                            //    }

                            //    try
                            //    {
                            //        if (!Row.IsUtvar2Null())
                            //        {
                            //            if (string.IsNullOrEmpty(Row.Utvar2.Trim()))
                            //                Utvar = "-";
                            //            else
                            //                Utvar = Row.Utvar2.Trim();
                            //        }
                            //        else
                            //        {
                            //            if (Row.IsUtvarNull())
                            //                Utvar = "-";
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(Row.Utvar.Trim()))
                            //                    Utvar = "-";
                            //                else
                            //                    Utvar = Row.Utvar.Trim();
                            //            }
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        Utvar = "-";
                            //        Logging.Log.Write(ex);
                            //    }

                            //    try
                            //    {
                            //        if (!Row.IsJmeno2Null())
                            //        {
                            //            if (string.IsNullOrEmpty(Row.Jmeno2.Trim()))
                            //                Jmeno = "-";
                            //            else
                            //                Jmeno = Row.Jmeno2.Trim();
                            //        }
                            //        else
                            //        {
                            //            if (Row.IsJmenoNull())
                            //                Jmeno = "-";
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(Row.Jmeno.Trim()))
                            //                    Jmeno = "-";
                            //                else
                            //                    Jmeno = Row.Jmeno.Trim();
                            //            }
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        Jmeno = "-";
                            //        Logging.Log.Write(ex);
                            //    }

                            //    try
                            //    {
                            //        if (!Row.IsUlice2Null())
                            //        {
                            //            if (string.IsNullOrEmpty(Row.Ulice2.Trim()))
                            //                Ulice = "-";
                            //            else
                            //                Ulice = Row.Ulice2.Trim();
                            //        }
                            //        else
                            //        {
                            //            if (Row.IsUliceNull())
                            //                Ulice = "-";
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(Row.Ulice.Trim()))
                            //                    Ulice = "-";
                            //                else
                            //                    Ulice = Row.Ulice.Trim();
                            //            }
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        Ulice = "-";
                            //        Logging.Log.Write(ex);
                            //    }

                            //    try
                            //    {
                            //        if (!Row.IsPSC2Null())
                            //        {
                            //            if (string.IsNullOrEmpty(Row.PSC2.Trim()))
                            //                PSC = "-";
                            //            else
                            //                PSC = Row.PSC2.Trim();
                            //        }
                            //        else
                            //        {
                            //            if (Row.IsPSCNull())
                            //                PSC = "-";
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(Row.PSC.Trim()))
                            //                    PSC = "-";
                            //                else
                            //                    PSC = Row.PSC.Trim();
                            //            }
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        PSC = "-";
                            //        Logging.Log.Write(ex);
                            //    }

                            //    try
                            //    {
                            //        if (!Row.IsObec2Null())
                            //        {
                            //            if (string.IsNullOrEmpty(Row.Obec2.Trim()))
                            //                Obec = "-";
                            //            else
                            //                Obec = Row.Obec2.Trim();
                            //        }
                            //        else
                            //        {
                            //            if (Row.IsObecNull())
                            //                Obec = "-";
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(Row.Obec.Trim()))
                            //                    Obec = "-";
                            //                else
                            //                    Obec = Row.Obec.Trim();
                            //            }
                            //        }
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        Obec = "-";
                            //        Logging.Log.Write(ex);
                            //    }

                            //}
                            //else
                            //{
                            #endregion

                            if (
                                !string.IsNullOrEmpty(Row_Firma2) ||
                                !string.IsNullOrEmpty(Row_Utvar2) ||
                                !string.IsNullOrEmpty(Row_Jmeno2) ||
                                !string.IsNullOrEmpty(Row_Ulice2) ||
                                !string.IsNullOrEmpty(Row_PSC2) ||
                                !string.IsNullOrEmpty(Row_Obec2)
                                )
                            {

                                Firma = string.IsNullOrEmpty(Row_Firma2) ? "-" : Row_Firma2.Trim();
                                Utvar = string.IsNullOrEmpty(Row_Utvar2) ? "-" : Row_Utvar2.Trim();
                                Jmeno = string.IsNullOrEmpty(Row_Jmeno2) ? "-" : Row_Jmeno2.Trim();
                                Ulice = string.IsNullOrEmpty(Row_Ulice2) ? "-" : Row_Ulice2.Trim();
                                PSC = string.IsNullOrEmpty(Row_PSC2) ? "-" : Row_PSC2.Trim();
                                Obec = string.IsNullOrEmpty(Row_Obec2) ? "-" : Row_Obec2.Trim();
                            }
                            else
                            {
                                Firma = string.IsNullOrEmpty(Row_Firma) ? "-" : Row_Firma.Trim();
                                Utvar = string.IsNullOrEmpty(Row_Utvar) ? "-" : Row_Utvar.Trim();
                                Jmeno = string.IsNullOrEmpty(Row_Jmeno) ? "-" : Row_Jmeno.Trim();
                                Ulice = string.IsNullOrEmpty(Row_Ulice) ? "-" : Row_Ulice.Trim();
                                PSC = string.IsNullOrEmpty(Row_PSC) ? "-" : Row_PSC.Trim();
                                Obec = string.IsNullOrEmpty(Row_Obec) ? "-" : Row_Obec.Trim();
                            }
                        }

                        //}
                    }

                    dataHeader.Values.AddValuesRow("Firma", Firma.Trim());
                    dataHeader.Values.AddValuesRow("Utvar", Utvar.Trim());
                    dataHeader.Values.AddValuesRow("Jmeno", Jmeno.Trim());
                    dataHeader.Values.AddValuesRow("Ulice", Ulice.Trim());
                    dataHeader.Values.AddValuesRow("PSC", PSC.Trim());
                    dataHeader.Values.AddValuesRow("Obec", Obec.Trim());
                    dataHeader.Values.AddValuesRow("ICO", ICO.Trim());
                    dataHeader.Values.AddValuesRow("DIC", DIC.Trim());

                    #endregion // end adresa


                    dataHeader.Values.EndLoadData();
                    dataHeader.AcceptChanges();
                }
                else
                    return true;
            }
            catch (System.Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
            }

            return true;
        }


        #region Private Metody

        private decimal? GetValue(Fask.Server.Interfaces.DataSets.DSValues.ValuesRow item, string NameColumn)
        {
            try
            {
                if (item.Key == NameColumn)
                {
                    if (!string.IsNullOrEmpty(item.Value))
                    {
                        decimal tmp = 0;

                        try
                        {
                            tmp = decimal.Parse(item.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                            return tmp;
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        public class SumyHodnot
        {
            public decimal Vaha { get; set; }
            public decimal Objem { get; set; }

            public SumyHodnot(decimal Vaha, decimal Objem)
            {
                this.Vaha = Vaha;
                this.Objem = Objem;

            }

        }


            #endregion
        }
}
