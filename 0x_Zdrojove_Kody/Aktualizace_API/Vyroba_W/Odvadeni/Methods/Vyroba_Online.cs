using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Vyroba_W.Odvadeni.Methods
{
    public class Vyroba_Online
    {
        /// <summary>
        /// Online zjisteni informace o vyrobnim prikazu
        /// </summary>
        /// <param name="vyrobaS">webservice vyroba</param>
        /// <param name="vyrobaDS">dataset vyroby</param>
        /// <param name="cisloOperace">identifikator operace vyrobniho prikazu</param>
		internal static void CheckOperation( Fask.SQLiteDBs.DataSets.Vyroba vyrobaDS, string cisloOperace, out Fask.Vyroba_W.WebServiceVyroba.ProductionState productionStateEnabled)
        {
            productionStateEnabled = Fask.Vyroba_W.WebServiceVyroba.ProductionState.Unknown;

            if (!Settings.Vyroba_Online)
                return;

            try
            {
				var vyrobaDSweb = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Vyroba_Online_CheckOperation(cisloOperace, Settings.TerminalID.ToString(), out productionStateEnabled);
                if (vyrobaDSweb.CZPRO_VPH.Count > 0)
                {
                    vyrobaDS.CZPRO_VPH.Merge(vyrobaDSweb.CZPRO_VPH);
                    vyrobaDS.CZPRO_VPP.Merge(vyrobaDSweb.CZPRO_VPP);
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex, true);
            }
        }

        /// <summary>
        /// Provede zapis hodnoty online
        /// </summary>
        /// <param name="pRow">aktualni odvod</param>
        /// <returns>Successfull online write</returns>
		internal static bool Online_Vyroba_Zapis_Odvod(Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow pRow)
        {
            if (!Settings.Vyroba_Online)
                return true;

            string message;
            Fask.Vyroba_W.WebServiceVyroba.ProductionObject productionObject = new Fask.Vyroba_W.WebServiceVyroba.ProductionObject();
            #region Nastaveni parametru Production Objecktu
            productionObject.operaceID = pRow.BarcodeP;
            productionObject.mnozstviVyrobeno = pRow.qty;
            productionObject.mnozstviZmetek = 0;
            productionObject.popisZmetek = string.Empty;
            productionObject.terminalID = pRow.TermID.ToString();
            // stav operace a vlastnosti ... 
            if (!pRow.IsTIMEPREPSTARTNull() && pRow.IsTIMEPREPSTOPNull() && pRow.IsTIMESTARTNull() && pRow.IsTIMESTOPNull())
            { // start pripravy
                productionObject.stavOperace = Fask.Vyroba_W.WebServiceVyroba.ProductionState.Priprava_Start;
                productionObject.casOperace = pRow.TIMEPREPSTART;
            }
            else if (!pRow.IsTIMEPREPSTARTNull() && !pRow.IsTIMEPREPSTOPNull() && pRow.IsTIMESTARTNull() && pRow.IsTIMESTOPNull())
            { // stop pripravy
                productionObject.stavOperace = Fask.Vyroba_W.WebServiceVyroba.ProductionState.Priprava_Stop;
                productionObject.casOperace = pRow.TIMEPREPSTOP;
            }
            else if (!pRow.IsTIMESTARTNull() && pRow.IsTIMESTOPNull())
            { // start odvodu
                productionObject.stavOperace = Fask.Vyroba_W.WebServiceVyroba.ProductionState.Odvod_Start;
                productionObject.casOperace = pRow.TIMESTART;
            }
            else if (!pRow.IsTIMESTOPNull())
            { // stop odvodu
                productionObject.stavOperace = Fask.Vyroba_W.WebServiceVyroba.ProductionState.Odvod_Stop;
                productionObject.casOperace = pRow.TIMESTOP;
            }
            // ? Korekce ?
            // tyto bude asi jeste slozitejsi ...
            else if (!pRow.IsTIMECORSTARTNull() && pRow.IsTIMECORSTOPNull())
            { // start korekce
                productionObject.stavOperace = Fask.Vyroba_W.WebServiceVyroba.ProductionState.Korekce_Start;
                productionObject.casOperace = pRow.TIMECORSTART;
            }
            else if (!pRow.IsTIMECORSTOPNull())
            { // start korekce
                productionObject.stavOperace = Fask.Vyroba_W.WebServiceVyroba.ProductionState.Korekce_Stop;
                productionObject.casOperace = pRow.TIMECORSTOP;
            }
            else
            {
                productionObject.stavOperace = Fask.Vyroba_W.WebServiceVyroba.ProductionState.Unknown;
                productionObject.casOperace = DateTime.Now;
            }
            #endregion

            while (true)
            {
                try
                {
					if (!Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Vyroba_Online_WriteOperation(productionObject, out message))
                    {
                        throw new Exception(message);
                    }
                    break; // ukonci cyklus
                }
                catch (Exception ex)
                {
                    Logging.ExceptionHandler2.Handle(ex, false);
                    System.Windows.Forms.DialogResult drOpakovat = System.Windows.Forms.MessageBox.Show(ex.Message + "\nOnline zápis odvodu se nezdařil\nOpakovat?", "Online zápis", System.Windows.Forms.MessageBoxButtons.RetryCancel, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                    if (drOpakovat == System.Windows.Forms.DialogResult.Retry)
                        continue;
                    else
                        return false;
                }
            }
            return true;
        }

    }
}
