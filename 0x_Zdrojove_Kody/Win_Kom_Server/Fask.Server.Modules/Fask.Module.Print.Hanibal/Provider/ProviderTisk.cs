using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using Fask.Server.Interfaces.DataSets;

namespace Fask.Module.Print.Hanibal
{
    public class ProviderTisk :
        Fask.Server.Interfaces.Tisky.ITisky2,
        Fask.Server.Interfaces.Tisky.ITisky2_MetodaEtiketa,
        Fask.Server.Interfaces.Tisky.ITisky2_MetodaSoupis
    {

        #region ITisky2_Metoda Members

        bool Fask.Server.Interfaces.Tisky.ITisky2_MetodaEtiketa.TiskMetodaEtiketa(int terminalID, ref string templateName, ref Fask.Server.Interfaces.DataSets.DSValues data, int pocetVytisku)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                //Dotazeni EAN pro car kod
                // dotazeni cen podle nejake logiky
                // posle dotazenych cen vybrat vhodnou šablonu
                // podle delky textku vybrat vhornou šablonu

                Classes.Cenovka cenovka = new Fask.Module.Print.Hanibal.Classes.Cenovka();

                foreach (Fask.Server.Interfaces.DataSets.DSValues.ValuesRow item in data.Values)
                {
                    if (item.Key == "ITEMNMBR")
                    {
                        int itemnmbr;
                        try
                        {

                            itemnmbr = int.Parse(item.Value);
                        }
                        catch (Exception ex)
                        {
                            Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                            return false;
                        }


                        cenovka.InitCenovka(itemnmbr);
                        break;
                    }
                }


                data.Values.BeginLoadData();

                data.Values.AddValuesRow("TEXT", cenovka.nazev);
                data.Values.AddValuesRow("EAN", cenovka.EAN);


                string EAN13_Code128_TypC = string.Empty;

                if (cenovka.EAN.Length % 2 == 0)
                {
                    EAN13_Code128_TypC = ">;";
                    EAN13_Code128_TypC += cenovka.EAN;

                }
                else
                {
                    EAN13_Code128_TypC += ">;";
                    EAN13_Code128_TypC += cenovka.EAN.Substring(0, cenovka.EAN.Length - 1);
                    EAN13_Code128_TypC += ">6";
                    EAN13_Code128_TypC += cenovka.EAN[cenovka.EAN.Length - 1].ToString();
                }

                data.Values.AddValuesRow("EAN13_Code128_TypC", EAN13_Code128_TypC);





                #region Puvodna logika delky textu
                //if (cenovka.nazev.Length < 20)
                //{
                //    //data.Values.AddValuesRow("TEXT1", cenovka.nazev);
                //    templateName = Properties.Settings.Default.Template1;
                //    //e.Graphics.DrawString(this.nazev, new Font("Arial", (float)HanibalEAN_Settings.Default.CenovkaNazevSize, FontStyle.Bold), Brushes.Black, new RectangleF(x, y, cenovkaWidth, (float)HanibalEAN_Settings.Default.CenovkaNazevBox), format);
                //}
                //else if (cenovka.nazev.Length > 50)
                //{
                //    //data.Values.AddValuesRow("TEXT1", cenovka.nazev);
                //    templateName = Properties.Settings.Default.Template3;
                //    //if (cenovka.nazev.Length >90)
                //    //{
                //    //    string text1 = cenovka.nazev.Substring(0,30);
                //    //    string text2 = cenovka.nazev.Substring(30, 30);
                //    //    string text3 = cenovka.nazev.Substring(60, 30);

                //    //    data.Values.AddValuesRow("TEXT1", text1);
                //    //    data.Values.AddValuesRow("TEXT2", text2);
                //    //    data.Values.AddValuesRow("TEXT3", text3);

                //    //    templateName = Properties.Settings.Default.Template3;

                //    //}
                //    //else 
                //    //{
                //    //    string text1 = cenovka.nazev.Substring(0, 30);
                //    //    string text2 = cenovka.nazev.Substring(30,30);
                //    //    string text3 = cenovka.nazev.Substring(60);

                //    //    data.Values.AddValuesRow("TEXT1", text1);
                //    //    data.Values.AddValuesRow("TEXT2", text2);
                //    //    data.Values.AddValuesRow("TEXT3", text3);

                //    //    templateName = Properties.Settings.Default.Template3;

                //    //}

                //    //e.Graphics.DrawString(this.nazev, new Font("Arial", (float)(HanibalEAN_Settings.Default.CenovkaNazevSize - 2), FontStyle.Bold), Brushes.Black, new RectangleF(0f, y, cenovkaWidth + x, (float)HanibalEAN_Settings.Default.CenovkaNazevBox), format);
                //}
                //else
                //{
                //    //string text1 = cenovka.nazev.Substring(0, 30);
                //    //string text2 = cenovka.nazev.Substring(30);
                //    //string text3 = cenovka.nazev.Substring(80, 120);

                //    //data.Values.AddValuesRow("TEXT1", text1);
                //    //data.Values.AddValuesRow("TEXT2", text2);
                //    //data.Values.AddValuesRow("TEXT3", text3);

                //    templateName = Properties.Settings.Default.Template2;
                //    //e.Graphics.DrawString(this.nazev, new Font("Arial", (float)(HanibalEAN_Settings.Default.CenovkaNazevSize - 1), FontStyle.Bold), Brushes.Black, new RectangleF(0f, y, cenovkaWidth + x, (float)HanibalEAN_Settings.Default.CenovkaNazevBox), format);
                //} 
                #endregion

                #region Nova logika



                //if (cenovka.nazev.Length < 20)
                if (cenovka.nazev.Length < Globals_V1.Konfigurace.PrintParams[0].PocetZnakuMensiNez)
                { templateName = Globals_V1.Konfigurace.PrintParams[0].Template1; }
                //else if (cenovka.nazev.Length > 50)
                else if (cenovka.nazev.Length > Globals_V1.Konfigurace.PrintParams[0].PocetZnakuVetsiNez)
                { templateName = Globals_V1.Konfigurace.PrintParams[0].Template3; }
                else
                { templateName = Globals_V1.Konfigurace.PrintParams[0].Template2; }



                #endregion

                bool tiskCena = true;

                try
                {
                    var dataRow = data.Values.FindByKey("TISKCENA");
                    if (dataRow != null)
                        tiskCena = bool.Parse(dataRow.Value);
                    else
                        tiskCena = true;

                }
                catch (Exception ex)
                {
                    Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    tiskCena = true;
                    //throw;
                }

                if (tiskCena)
                {

                    if (cenovka.zobrazDveCeny)
                    {
                        data.Values.AddValuesRow("SKRT", Globals_V1.Konfigurace.PrintParams[0].ZPL_SKRT);
                        data.Values.AddValuesRow("NAMEPRICE", cenovka.cena1text + " / " + cenovka.cena2text);
                        data.Values.AddValuesRow("PRICE", cenovka.GetTextCena1() + " / " + cenovka.GetTextCena2());
                    }
                    else
                    {
                        data.Values.AddValuesRow("NAMEPRICE", cenovka.cena1text);
                        data.Values.AddValuesRow("PRICE", cenovka.GetTextCena1());
                    }
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

        #endregion

        #region ITisky2_Metoda Members

        public bool TiskMetodaSoupis(ref DSValues dataHeader, ref List<DSValues> dataRowList, ref DSValues dataFooter)
        {

            try
            {
                if (dataRowList != null && dataRowList.Count > 0)
                {

                    foreach (DSValues item in dataRowList)
                    {
                        foreach (DSValues.ValuesRow row in item.Values)
                        {
                            if (row.Key == "SOPNUMBE")
                            {
                                string PDoklad = string.Empty;
                                string Pozn = string.Empty;
                                string IDS = string.Empty;

                                Classes.Database db = new Classes.Database();
                                db.Get_Data(
                                    row.Value,
                                    out PDoklad,
                                    out Pozn,
                                    out IDS
                                    );

                                item.Values.BeginLoadData();

                                item.Values.AddValuesRow("Poznamka", Pozn);
                                item.Values.AddValuesRow("Doklad", PDoklad);
                                item.Values.AddValuesRow("Doprava", IDS);

                                item.Values.EndLoadData();
                                item.AcceptChanges();

                                break;
                            }

                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return true;
        }

        #endregion
    }
}
