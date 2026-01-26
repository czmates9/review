using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.Forms;
using System.Windows.Forms;

namespace Fask.MST_W.Online.BYZNYS
{
    public class Algorithms
    {
        const string _FunctionLabelNewEAN = "Nový EAN";

        /// <summary>
        /// Online funkce k vložení nového EAN kódu s výběrem zboží
        /// </summary>
        /// <returns>Nový vložený EAN, null-pokud není uloženo</returns>
        public static string InsertNewEAN()
        {
            DatabaseOnline.SKLADRow zbozi = null;

            using (FormVyberZbozi frmVyberZbozi = new FormVyberZbozi())
            {
                if (frmVyberZbozi.ShowDialog() == DialogResult.Cancel)
                    return null;

                zbozi = frmVyberZbozi.ZBOZI_Selected;
            }

            if (zbozi == null)
                return null;

            return InsertNewEAN(zbozi.KLIC_MA, zbozi.NAZEV_MAT);
        }

        /// <summary>
        /// Online funkce k vložení nového EAN kódu
        /// </summary>
        /// <param name="klic_ma">klic materialu ke kterému se má vložit nový EAN</param>
        /// <param name="nazev_ma">název materiálu k zobrazení</param>
        /// <returns>Nový vložený EAN, null-pokud není uloženo</returns>
        public static string InsertNewEAN(int klic_ma, string nazev_ma)
        {
            DatabaseOnline.ONL_PARTNERRow partner = null;
            string newean = string.Empty;

            using (Forms.SejmiKodForm sejmiKod = new Fask.MST_W.Forms.SejmiKodForm(
                "Zadejte nový EAN",
                 Fask.MST_W.Forms.SejmiKodForm.TypeOfCode.AlphaNumeric,
                 0, false, false, string.Empty, 20))
            {
                if (sejmiKod.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return null;

                newean = sejmiKod.Kod;
            }

            if (Settings.Online_BYZNYS_VyberPartneraKlicDefault_Povolit)
            {
                DatabaseOnlineTableAdapters.ONL_PARTNERTableAdapter onlpartner = new Fask.MST_W.Online.BYZNYS.DatabaseOnlineTableAdapters.ONL_PARTNERTableAdapter();
                onlpartner.Connection.ConnectionString = Settings.Online_BYZNYS_ConnectionString;
                DatabaseOnline.ONL_PARTNERDataTable partnerdt = onlpartner.GetData(0, Settings.Online_BYZNYS_VyberPartneraKlicDefault.ToString());
                if (partnerdt.Count <= 0)
                {
                    MessageBoxBig.Show("Partner s klíčem '" + Settings.Online_BYZNYS_VyberPartneraKlicDefault + "' nenalezen", "Nový EAN", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                    return null;
                }
                partner = partnerdt[0];
            }
            else
            {
                using (FormVyberPartnera frmPartner = new FormVyberPartnera())
                {
                    if (frmPartner.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                        return null;

                    partner = frmPartner.ONL_PARTNER_Selected;
                }
            }

            using (FormNovyEAN frmEan = new FormNovyEAN())
            {
                frmEan.Klic_MA = klic_ma;
                frmEan.Klic_Odb = partner != null ? (int?)partner.klic_odb : null;
                frmEan.Nazev_MA = nazev_ma;
                frmEan.Nazev_Odb = partner != null ? partner.nazev : null;
                frmEan.NewEAN = newean;

                if (frmEan.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return null;
            }

            return newean;
        }
    }
}
