using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Fask.BarCodeGraphics;
using Fask.Logging;

namespace MST_Print_Server_ZPL_Printing
{
    public abstract class StringComposing
    {
        // data etikety
        protected Dictionary<string, string> keys;
        // data soupisu
        protected Dictionary<string, string> keysHeader;
        protected List<Dictionary<string, string>> keysRows;
        protected Dictionary<string, string> keysFooter;

        protected RAW_Printing raw_printing_object;

        protected List<string> finalStrings;
        /// <summary>
        /// Vysledny ZPL zdrojovy subor
        /// </summary>
        public List<string> FinalStrings
        {
            get { return finalStrings; }
        }

        /// <summary>
        /// Provider k grafice.
        /// </summary>
        protected Fask.BarCodeGraphics.IBarCodeGraphics provider = null;

        /// <summary>
        /// Nahradzanie premennych vo formate $variable$
        /// </summary>
        /// <param name="zpl_template"></param>
        /// <returns></returns>
        protected string Replace(string zpl_template)
        {
            return Replace(zpl_template, this.keys);
        }

        /// <summary>
        /// Nahradzanie premennych vo formate $variable$
        /// </summary>
        /// <param name="zpl_template"></param>
        /// <returns></returns>
        protected string Replace(string zpl_template, Dictionary<string, string> dict)
        {
            StringBuilder s_novytext = new StringBuilder(zpl_template);

            try
            {
                s_novytext = ReplaceTemplateKeys(dict, s_novytext);
                //foreach (string key in dict.Keys)
                //{
                //    //s_novytext = s_novytext.Replace("$" + key + "$", dict[key]);
                //}

                // pridani Image, pokud je v sablone pozadovan
                s_novytext = ReplaceImage(s_novytext);
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return s_novytext.ToString();
        }

        protected StringBuilder ReplaceImage(StringBuilder sb)
        {
            // kontrola, zdali je vyplnen provider
            if (provider != null)
            {
                try
                {
                    //string data = "ahoj\nahoj2#QR,480,480,43,115,0,1234567891lakskdkdkds123456789palalalakaka#ahoj3\nahoj4";
                    //StringBuilder sb = new StringBuilder(data);
                    return provider.AddGraphics(sb);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
                return sb;
        }


        /// <summary>
        /// Prepise nalezene klice pomoci regularniho vyrazu => \$(\w+)((,)(\d+))?\$
        /// $[id](,[delka])?$
        /// [id] = identifikator
        /// [delka] = maximalni delka retezce (nemusi byt definovano, pak vraci cely retezec)
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="template">Template</param>
        /// <returns>Novy objekt s nahrazenymi parametry</returns>
        private StringBuilder ReplaceTemplateKeys(Dictionary<string, string> data, StringBuilder template)
        {
            StringBuilder sbNew = new StringBuilder(template.ToString());

            // RegEx 
            // => \$(\w+|.+,\d+)\$
            // => \$\w+(,\d+)?\$
            // => \$(\w+)((,)(\d+))?\$ 
            //  Group[0] = cely match
            //  Group[1] = identifikator (\w+)
            //  Group[2] = postfix ((,)(\d+))?
            //  Group[3] = carka (,)
            //  Group[4] = delka (\d+)
            //  Group[5] = postfix ((,)(\d+))?
            //  Group[6] = carka (,)
            //  Group[7] = delka (\d+)

            // puvodni - Obsahuje odpovidajici matche
            //System.Text.RegularExpressions.MatchCollection matches =
            //    System.Text.RegularExpressions.Regex.Matches(sbNew.ToString(), @"\$(\w+)((,)(\d+))?((,)(\d+))?\$");
            // puvodni - Obsahuje odpovídající matche
            System.Text.RegularExpressions.MatchCollection matches =
                System.Text.RegularExpressions.Regex.Matches(sbNew.ToString(), @"\$([\w ]+)((,)(\d+))?((,)(\d+))?\$");
            
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                string tresult = string.Empty;
                string key = match.Groups[1].Value;
                if (!data.ContainsKey(key))
                { //klic v datech nenalezen, nahradim do sablony prazdnym retezcem ...
                    tresult = string.Empty;
                }
                else
                { //klic nalezen, tak se ho pokusim naformatovat ...
                    tresult = data[key];
                    int? p1 = null;
                    try { p1 = int.Parse(match.Groups[4].Value); }
                    catch { }
                    int? p2 = null;
                    try { p2 = int.Parse(match.Groups[7].Value); }
                    catch { }
                    if (p2.HasValue)
                    {
                        if (tresult.Length < p1.Value) //index mimo rozsah 
                            tresult = string.Empty;
                        else if (p2.Value <= 0)
                        {
                            tresult = tresult.Substring(
                                p1.Value,
                                // test na preteceni maximalni delky
                                tresult.Length - p1.Value
                                );
                        }
                        else // index v rozsahu
                            tresult = tresult.Substring(
                                p1.Value,
                                // test na preteceni maximalni delky
                                tresult.Length < (p1.Value + p2.Value) ? tresult.Length - p1.Value : p2.Value
                                );
                    }
                    else if (p1.HasValue && p1.Value > 0)
                    {
                        tresult = tresult.Substring(0, tresult.Length < p1.Value ? tresult.Length : p1.Value);
                    }
                    else
                    { // ??? neni nutny ... $<key>$
                    }

                }

                // finalni nahrazeni matche vysledkem formatovani ...
                sbNew.Replace(match.Value, tresult);
            }

            return sbNew;
        }

        /// <summary>
        /// Nahradzanie premennych vo formate $variable$
        /// </summary>
        /// <param name="zpl_template"></param>
        /// <returns></returns>
        protected string Replace(StringBuilder sb_zpl_template)
        {
            return this.Replace(sb_zpl_template, this.keys);
        }

        /// <summary>
        /// Nahradzanie premennych vo formate $variable$
        /// </summary>
        /// <param name="zpl_template"></param>
        /// <returns></returns>
        protected string Replace(StringBuilder sb_zpl_template, Dictionary<string, string> dict)
        {
            try
            {
                foreach (string key in dict.Keys)
                {
                    sb_zpl_template.Replace("$" + key + "$", dict[key]);                    
                }
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return sb_zpl_template.ToString();
        }


    }
}
