using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.MySystem
{
    public class Comm
    {
        public Comm()
        {
        }

        /// <summary>
        /// Zobrazi list dostupnymi davkami a nabidne jejich stazeni
        /// </summary>
        /// <returns>Vraci pocet stazenych davek</returns>
        public int StahniDavku()
        {
            return 0;
        }

        /// <summary>
        /// Odesle dokoncenou davku
        /// </summary>
        /// <returns>True: pokud se odeslani zdari</returns>
        public bool OdesliDavku(VydejService.Vydej vydejData)
        {
            /*            if (!stavVydeje())
                        {
                            if (MessageBoxBig.Show("Tato dávka ješte není dokonèena. Opravdu ji chcete odeslat?",
                                "Dotaz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Cancel)
                                return;
                        }
                        mbw.BeginPracujiForm();
                        try
                        {
                            vydejService.ProcessVydejka(vydejData);
                        }
                        catch (Exception ex)
                        {
                            mbw.EndPracujiForm();
                            MessageBoxBig.Show("Nepodaøilo se odeslat dávku!");
                        }
                        mbw.EndPracujiForm();
                        File.Delete(listDavkamaForm.FileName);
            */
            return false;
        }
    }
}
