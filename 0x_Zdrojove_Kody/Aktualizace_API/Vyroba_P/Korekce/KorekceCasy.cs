using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Aktualizace_API.Extensions;

namespace Fask.Aktualizace_API.Korekce
{
    public class KorekceCasy
    {
        #region Casy vypocitane
        /// <summary>
        /// Pripravny cas
        /// </summary>
        public TimeSpan lTSPripravnyCas = TimeSpan.Zero;
        /// <summary>
        /// Jednotkovy cas
        /// </summary>
        public TimeSpan lTSJednotka = TimeSpan.Zero;
        /// <summary>
        /// Predpokladany cas = Jednotkovy cas * pocet kusu
        /// </summary>
        public TimeSpan lTSPredpokladNormy = TimeSpan.Zero;
        /// <summary>
        /// Rozdil casu od predpokladaneho casu
        /// </summary>
        public TimeSpan lTSRozdilOdNormy = TimeSpan.Zero;
        /// <summary>
        /// Povoleny rozdil od normy procentuelne
        /// </summary>
        public TimeSpan lTSRozdilPovolenZProcent = TimeSpan.Zero;
        /// <summary>
        /// Povoleny rozdil od normy v case
        /// </summary>
        public TimeSpan lTSRozdilPovolenZMinimum = TimeSpan.Zero;
        /// <summary>
        /// Celkovy cas jiz zadanych korekcy
        /// </summary>
        public TimeSpan lTSKorekceSuma = TimeSpan.Zero;
        public TimeSpan lTSKorekceSumaZpozdeni = TimeSpan.Zero;
        public TimeSpan lTSKorekceSumaUspora = TimeSpan.Zero;
        /// <summary>
        /// Celkovy cas operace
        /// </summary>
        public TimeSpan lTSCelkovyCas = TimeSpan.Zero;
        #endregion

        /// <summary>
        /// Posledni cas operace uzivatele
        /// </summary>
        public DateTime? _lstOperationUser = null;

        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow _productionRow = null;
        /// <summary>
        /// Seznam zadanych korekci ...
        /// </summary>
        public List<Korekce> _korekce = null;

        /// <summary>
        /// Provede vypocty nad uvedenymi informacemi a vraci casy lTS..
        /// </summary>
        public void Calculate()
        {
            // 1) vynulovani ...
            lTSPripravnyCas = TimeSpan.Zero;
            lTSJednotka = TimeSpan.Zero;
            lTSPredpokladNormy = TimeSpan.Zero;
            lTSRozdilOdNormy = TimeSpan.Zero;
            lTSRozdilPovolenZProcent = TimeSpan.Zero;
            lTSRozdilPovolenZMinimum = TimeSpan.Zero;
            lTSKorekceSuma = TimeSpan.Zero;
            lTSCelkovyCas = TimeSpan.Zero;

            //2) vypocty

            //a) pripravny cas
            try
            {
                lTSPripravnyCas = TimeSpan.FromMinutes(_productionRow.TIMEPREP); //new TimeSpan(0, _vpp.TIMEPREP, 0).ToString();
            }
            catch { }
            //b) jednotkovy cas
            try
            {
                lTSJednotka = TimeSpan.FromMinutes(_productionRow.TIMEUNIT); //new TimeSpan(0, _vpp.TIMEUNIT, 0).ToString();
            }
            catch { }

            //c) predpoklad normy
            // do predpokladu se musi zapocitat i pripadny pripravny cas..
            // - to zda se ma zapocitat i pripravny cas je reseno pri dokoncovani operace v FormOdvadeniMethods.StopOdvod()...
            try
            {
                lTSPredpokladNormy =
                    TimeSpan.FromMinutes(lTSJednotka.TotalMinutes * Convert.ToDouble(_productionRow.qty))
                    + lTSPripravnyCas;
            }
            catch { }

            //d) celkova delka korekci...
            // 1.9.2016 JiS => zmena zpusobu zadavani korekci ...
            //try
            //{
            //    TimeSpan ts = TimeSpan.FromMinutes(_productionRow.IsTIMECORNull() ? 0 : _productionRow.TIMECOR); //new TimeSpan(0,_productionRow.TIMECOR, 0).ToString();
            //    labelKorekceCasu.Text = ts.ToStringHHmmss();
            //}
            //catch { }
            try
            {
                // seznam korekce je listu ... 
                // zobrazit celkovy cas korekci + pocet zadanych korekci ...
                //lTSKorekceSuma = TimeSpan.Zero;
                //_korekce.ForEach(x => lTSKorekceSuma = lTSKorekceSuma.Add(x.delka));
                //_korekce.ForEach((Action<Korekce>) delegate(Korekce x) {
                //    if (x.type.HasValue && x.type == 1)
                //    {
                //        lTSKorekceSuma = lTSKorekceSuma.Subtract(x.delka);
                //    }
                //    else
                //    {
                //        lTSKorekceSuma = lTSKorekceSuma.Add(x.delka);
                //    }
                //});
                lTSKorekceSuma = _korekce.SumKorekce();
                lTSKorekceSumaZpozdeni = _korekce.SumKorekceZpozdeni();
                lTSKorekceSumaUspora = _korekce.SumKorekceUspora();
            }
            catch { }

            // celkovy cas...
            try
            {
                // 12.10.2016 JiS
                // 1) celkovy cas je rozdilny pri operacich typu Stop[TimeMode=0] a Start/Stop[TimeMode=1], Start/Start/Stop[TimeMode=2]
                //    a) TimeMode=0 => zde neni cas zahajeni operace a tedy se bere jako zahajeni cas posledni akce uzivatele nebo cas posledniho odvodu(akce) uzivatele...
                //    b) TimeMode=[1,2] => zde existuje cas zahajeni operace, tedy se musi vzit jako zacatek tento cas a celkova delka tedy od tohoto okamziku
                //       duvod: pokud uzivatel zapomene nebo z jineho duvodu nedojde k ukonceni operace, tak by pak vznikaly casove disproporce...
                //       dusledek: uzivateli je vynuceno zadani zaporne Korekce(-K)

                //labelCelkovyCas.Text = (_productionRow.TIMESTOP - Settings.LastProductionDateTime).ToString();                    
                if (this._productionRow.TIMEMODE == 0)
                {
                    lTSCelkovyCas =
                        (_productionRow.TIMESTOP
                        - (_lstOperationUser ?? Settings.LastProductionDateTime)
                        - (_productionRow.IsTIMECORNull() ? TimeSpan.FromMinutes(0) : TimeSpan.FromMinutes(_productionRow.TIMECOR)));
                    //labelCelkovyCas.Text = string.Format("{0:00}:{1:00}:{2:00}", ts.TotalHours, ts.Minutes, ts.Seconds);
                }
                else //if ((this._productionRow.TIMEMODE == 1) || (this._productionRow.TIMEMODE == 2))
                {
                    lTSCelkovyCas =
                        (_productionRow.TIMESTOP
                        - (_productionRow.TIMESTART)
                        - (_productionRow.IsTIMECORNull() ? TimeSpan.Zero : TimeSpan.FromMinutes(_productionRow.TIMECOR)));
                }
            }
            catch { }

            try
            {
                lTSRozdilOdNormy = (lTSCelkovyCas - lTSKorekceSuma - lTSPredpokladNormy);
                lTSRozdilPovolenZProcent = TimeSpan.FromMinutes((lTSPredpokladNormy.TotalMinutes / 100) * Convert.ToDouble(Settings.CorrectsCheckPercentValue));
                lTSRozdilPovolenZMinimum = Settings.CorrectsCheckMinimumValue;
            }
            catch { }
        }

        /// <summary>
        /// Validace korekci. V pripade problemu vyhazuje vyjimku s textem vyjimky
        /// </summary>
        public bool Validate()
        {
            // Test na velikost korekce ...

            //1) pokud je :
            // - pozadovano hlidani korekci 
            // - timeunit je vetsi jak 0
            // tak se provadi hlidani korekci, jinak je to validni ...

            if (Settings.CorrectsCheckEnable
                && (_productionRow.TIMEUNIT > (0 + float.Epsilon)) // test na vetsi nez 0!!!
                )
            {
                if (Settings.CorrectsCheckPercentEnable && Settings.CorrectsCheckMinimumEnable)
                {
                    if (lTSRozdilOdNormy > TimeSpan.Zero 
                        && lTSRozdilOdNormy > lTSRozdilPovolenZProcent 
                        && lTSRozdilOdNormy > lTSRozdilPovolenZMinimum
                        )
                    {
                        throw new KorekceExceptionZpozdeni("Překročen normovaný čas.\nZadejte odpovídající korekci!");
                        //return false;
                    }

                    if (lTSRozdilOdNormy < TimeSpan.Zero 
                        && lTSRozdilOdNormy.Negate() > lTSRozdilPovolenZProcent 
                        && lTSRozdilOdNormy.Negate() > lTSRozdilPovolenZMinimum
                        )
                    {
                        throw new KorekceExceptionUspora("Normovaný čas nedosažen.\nZadejte odpovídající korekci!");
                        //return false;
                    }
                }
                else if (Settings.CorrectsCheckPercentEnable && !Settings.CorrectsCheckMinimumEnable)
                {
                    if (lTSRozdilOdNormy > TimeSpan.Zero
                        && lTSRozdilOdNormy > lTSRozdilPovolenZProcent
                        )
                    {
                        throw new KorekceExceptionZpozdeni("Překročen normovaný čas.\nZadejte odpovídající korekci!");
                        //return false;
                    }

                    if (lTSRozdilOdNormy < TimeSpan.Zero
                        && lTSRozdilOdNormy.Negate() > lTSRozdilPovolenZProcent 
                        )
                    {
                        throw new KorekceExceptionUspora("Normovaný čas nedosažen.\nZadejte odpovídající korekci!");
                        //return false;
                    }
                }
                else if (!Settings.CorrectsCheckPercentEnable && Settings.CorrectsCheckMinimumEnable)
                {
                    if (lTSRozdilOdNormy > TimeSpan.Zero
                        && lTSRozdilOdNormy > lTSRozdilPovolenZMinimum
                        )
                    {
                        throw new KorekceExceptionZpozdeni("Překročen normovaný čas.\nZadejte odpovídající korekci!");
                        //return false;
                    }

                    if (lTSRozdilOdNormy < TimeSpan.Zero
                        && lTSRozdilOdNormy.Negate() > lTSRozdilPovolenZMinimum
                        )
                    {
                        throw new KorekceExceptionUspora("Normovaný čas nedosažen.\nZadejte odpovídající korekci!");
                        //return false;
                    }
                }
                else
                { // nic se nekontroluje, jakobyto nebylo zapnute...???
                }
            }

            // Pokud az sem, tak je vse ok ...
            return true;
        }

    }
}
