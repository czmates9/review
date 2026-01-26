using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MES_Android.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_Android.OdvadeniVyroby.Classes
{
    public class Vyroba_Online
    {
        /// <summary>
        /// Online zjisteni informace o vyrobnim prikazu
        /// </summary>
        /// <param name="vyrobaS">webservice vyroba</param>
        /// <param name="vyrobaDS">dataset vyroby</param>
        /// <param name="cisloOperace">identifikator operace vyrobniho prikazu</param>
        internal static void CheckOperation(Fask.SQLiteDBs.DataSets.Vyroba vyrobaDS, string cisloOperace, out MES_Android.Vyroba.ProductionState productionStateEnabled)
        {
            productionStateEnabled = MES_Android.Vyroba.ProductionState.Unknown;

            if (!Konfigurace_Singleton.Instance.Vyroba.Vyroba_Online)
                return;

            try
            {
                var vyrobaDSweb = DataInfo_Static.VyrobaGO_Instance.vyrobaServis.Vyroba_Online_CheckOperation(cisloOperace, Config.Settings.TerminalID.ToString(), out productionStateEnabled);
                if (vyrobaDSweb.CZPRO_VPH.Count > 0)
                {
                    vyrobaDS.CZPRO_VPH.Merge(vyrobaDSweb.CZPRO_VPH);
                    vyrobaDS.CZPRO_VPP.Merge(vyrobaDSweb.CZPRO_VPP);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        /// <summary>
        /// Provede zapis hodnoty online
        /// </summary>
        /// <param name="pRow">aktualni odvod</param>
        /// <returns>Successfull online write</returns>
        internal static async Task<bool> Online_Vyroba_Zapis_Odvod(AppCompatActivity _parent,Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow pRow)
        {
            if (!Konfigurace_Singleton.Instance.Vyroba.Vyroba_Online)
                return true;

            string message;
            Vyroba.ProductionObject productionObject = new Vyroba.ProductionObject();
            #region Nastaveni parametru Production Objecktu
            productionObject.operaceID = pRow.BarcodeP;
            productionObject.mnozstviVyrobeno = pRow.qty;
            productionObject.mnozstviZmetek = 0;
            productionObject.popisZmetek = string.Empty;
            productionObject.terminalID = pRow.TermID.ToString();
            // stav operace a vlastnosti ... 
            if (!pRow.IsTIMEPREPSTARTNull() && pRow.IsTIMEPREPSTOPNull() && pRow.IsTIMESTARTNull() && pRow.IsTIMESTOPNull())
            { // start pripravy
                productionObject.stavOperace = Vyroba.ProductionState.Priprava_Start;
                productionObject.casOperace = pRow.TIMEPREPSTART;
            }
            else if (!pRow.IsTIMEPREPSTARTNull() && !pRow.IsTIMEPREPSTOPNull() && pRow.IsTIMESTARTNull() && pRow.IsTIMESTOPNull())
            { // stop pripravy
                productionObject.stavOperace = Vyroba.ProductionState.Priprava_Stop;
                productionObject.casOperace = pRow.TIMEPREPSTOP;
            }
            else if (!pRow.IsTIMESTARTNull() && pRow.IsTIMESTOPNull())
            { // start odvodu
                productionObject.stavOperace = Vyroba.ProductionState.Odvod_Start;
                productionObject.casOperace = pRow.TIMESTART;
            }
            else if (!pRow.IsTIMESTOPNull())
            { // stop odvodu
                productionObject.stavOperace = Vyroba.ProductionState.Odvod_Stop;
                productionObject.casOperace = pRow.TIMESTOP;
            }
            // ? Korekce ?
            // tyto bude asi jeste slozitejsi ...
            else if (!pRow.IsTIMECORSTARTNull() && pRow.IsTIMECORSTOPNull())
            { // start korekce
                productionObject.stavOperace = Vyroba.ProductionState.Korekce_Start;
                productionObject.casOperace = pRow.TIMECORSTART;
            }
            else if (!pRow.IsTIMECORSTOPNull())
            { // start korekce
                productionObject.stavOperace = Vyroba.ProductionState.Korekce_Stop;
                productionObject.casOperace = pRow.TIMECORSTOP;
            }
            else
            {
                productionObject.stavOperace = Vyroba.ProductionState.Unknown;
                productionObject.casOperace = DateTime.Now;
            }
            #endregion

            while (true)
            {
                try
                {
                    
                    if (!DataInfo_Static.VyrobaGO_Instance.vyrobaServis.Vyroba_Online_WriteOperation(productionObject, out message))
                    {
                        throw new Exception(message);
                    }
                    break; // ukonci cyklus
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    DialogResult drOpakovat = await MessageBoxAsync.Show(_parent, ex.Message + "\nOnline zápis odvodu se nezdařil\nOpakovat?", "Online zápis", MessageBoxButtons.RetryCancel);
                    if (drOpakovat == DialogResult.Retry)
                        continue;
                    else
                        return false;
                }
            }
            return true;
        }

    }
}