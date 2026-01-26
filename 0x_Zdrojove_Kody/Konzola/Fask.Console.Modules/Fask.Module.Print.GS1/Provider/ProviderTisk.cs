using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using Fask.Interfaces.DataSets;

namespace Fask.Module.Print.GS1
{
    public class ProviderTisk :
        Fask.Interfaces.Tisky.ITisky2,
        Fask.Interfaces.Tisky.ITisky2_MetodaEtiketa,
        Fask.Interfaces.Tisky.ITisky2_MetodaEtiketa_GS1
    {
   
        #region ITisky2_Metoda Members

        public bool TiskMetodaEtiketa(int terminalID, ref string templateName, ref DSValues data, int pocetVytisku)
        {
            try
            {
                string GS1_KOD = string.Empty;
                string SERLTNUM = string.Empty;
                string VNDITNUM = string.Empty;
                DateTime? EXPIRACE = null;

                string SERNUMTRACK = string.Empty;

                #region Nacteni params

                foreach (DSValues.ValuesRow item in data.Values)
                {
                    if (item.Key == "SERLTNUM")
                    {
                        SERLTNUM = item.Value;
                    }
                    else if (item.Key == "VNDITNUM")
                    {
                        VNDITNUM = item.Value;
                    }
                    else if (item.Key == "EXPIRATION")
                    {
                        try
                        {
                            EXPIRACE = DateTime.Parse(item.Value);
                        }
                        catch (Exception ex)
                        {
                            Logging.ExceptionHandler2.Handle(ex);
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Parsovani času TISK GS1 ERR");
                        }
                    }
                    else if (item.Key == "CZ_SERNUM_TRACK")
                    {
                        SERNUMTRACK = item.Value;
                    }
                }
                #endregion
                
                data.Values.BeginLoadData();

                if (!string.IsNullOrEmpty(VNDITNUM))
                {
                   

                    if (VNDITNUM.Length < 14)
                    {
                        VNDITNUM = VNDITNUM.PadLeft(14, '0');
                    }

                    if (VNDITNUM.Length != 14)
                    {
                        throw new Exception("GTIN nemá 14 znaků");
                    }

                    GS1_KOD += "01" + VNDITNUM;
                    data.Values.AddValuesRow("GS1_LOK_GTIN", Properties.Settings.Default.GS1_GTIN);


                    if (EXPIRACE.HasValue)
                    {
                        string datumexpirace = EXPIRACE.Value.ToString("yyMMdd");
                        GS1_KOD += "17" + datumexpirace;
                        data.Values.AddValuesRow("GS1_LOK_EXP", Properties.Settings.Default.GS1_UseByDate);
                    }


                    if (!string.IsNullOrEmpty(SERLTNUM))
                    {
                        if (SERNUMTRACK == "1")
                        {
                            if (!string.IsNullOrEmpty(SERLTNUM))
                            {
                                GS1_KOD += "21" + SERLTNUM;
                                data.Values.AddValuesRow("GS1_LOK_LOT_SN", Properties.Settings.Default.GS1_SN);
                            }
                        }
                        else if (SERNUMTRACK == "2")
                        {
                            if (!string.IsNullOrEmpty(SERLTNUM))
                            {
                                GS1_KOD += "10" + SERLTNUM;
                                data.Values.AddValuesRow("GS1_LOK_LOT_SN", Properties.Settings.Default.GS1_LOT);
                            }
                        }

                    }                
                }


                data.Values.AddValuesRow("GS1_KOD", GS1_KOD);
                data.Values.AddValuesRow("GS1_LOK_REF", Properties.Settings.Default.GS1_REF);

                if (EXPIRACE.HasValue)
                {
                    string exp = string.Empty;

                    exp += EXPIRACE.Value.ToString("dd.MM.yyyy");
                    exp += " (";
                    exp += EXPIRACE.Value.ToString("yyMMdd");
                    exp += ")";

                    data.Values.AddValuesRow("EXPIRACE_RRMMDD", exp);
                }

                data.Values.EndLoadData();
                data.AcceptChanges();
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(data.Values);
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

            return true;
        }

        public Vyroba.CZPRO_VPPDataTable TiskMetodaEtiketa_GS1(ref Vyroba.CZPRO_VPPDataTable data)
        {
            try
            {
                string GS1_KOD = string.Empty;
                string SERLTNUM = string.Empty;
                string VNDITNUM = string.Empty;
                DateTime? EXPIRACE = null;

                string SERNUMTRACK = string.Empty;

                #region Nacteni params

                foreach (Vyroba.CZPRO_VPPRow row in data.Rows)
                {
                    string key = row["Key"].ToString();
                    string value = row["Value"].ToString();

                    if (key == "SERLTNUM")
                    {
                        SERLTNUM = value;
                    }
                    else if (key == "VNDITNUM")
                    {
                        VNDITNUM = value;
                    }
                    else if (key == "EXPIRATION")
                    {
                        try
                        {
                            EXPIRACE = DateTime.Parse(value);
                        }
                        catch (Exception ex)
                        {
                            Logging.ExceptionHandler2.Handle(ex);
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Parsovani času TISK GS1 ERR");
                        }
                    }
                    else if (key == "CZ_SERNUM_TRACK")
                    {
                        SERNUMTRACK = value;
                    }
                }


                #endregion



                data.BeginLoadData();

                if (!string.IsNullOrEmpty(VNDITNUM))
                {
                    if (VNDITNUM.Length < 14)
                    {
                        VNDITNUM = VNDITNUM.PadLeft(14, '0');
                    }

                    if (VNDITNUM.Length != 14)
                    {
                        throw new Exception("GTIN nemá 14 znaků");
                    }

                    GS1_KOD += "01" + VNDITNUM;
                    data.Rows.Add("GS1_LOK_GTIN", Properties.Settings.Default.GS1_GTIN);

                    if (EXPIRACE.HasValue)
                    {
                        string datumexpirace = EXPIRACE.Value.ToString("yyMMdd");
                        GS1_KOD += "17" + datumexpirace;
                        data.Rows.Add("GS1_LOK_EXP", Properties.Settings.Default.GS1_UseByDate);
                    }

                    if (!string.IsNullOrEmpty(SERLTNUM))
                    {
                        if (SERNUMTRACK == "1")
                        {
                            if (!string.IsNullOrEmpty(SERLTNUM))
                            {
                                GS1_KOD += "21" + SERLTNUM;
                                data.Rows.Add("GS1_LOK_LOT_SN", Properties.Settings.Default.GS1_SN);
                            }
                        }
                        else if (SERNUMTRACK == "2")
                        {
                            if (!string.IsNullOrEmpty(SERLTNUM))
                            {
                                GS1_KOD += "10" + SERLTNUM;
                                data.Rows.Add("GS1_LOK_LOT_SN", Properties.Settings.Default.GS1_LOT);
                            }
                        }
                    }
                }

                data.Rows.Add("GS1_KOD", GS1_KOD);
                data.Rows.Add("GS1_LOK_REF", Properties.Settings.Default.GS1_REF);

                if (EXPIRACE.HasValue)
                {
                    string exp = string.Empty;
                    exp += EXPIRACE.Value.ToString("dd.MM.yyyy");
                    exp += " (";
                    exp += EXPIRACE.Value.ToString("yyMMdd");
                    exp += ")";

                    data.Rows.Add("EXPIRACE_RRMMDD", exp);
                }

                data.EndLoadData();
                data.AcceptChanges();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(data);
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

            return data;
        }

        #endregion
    }
}
