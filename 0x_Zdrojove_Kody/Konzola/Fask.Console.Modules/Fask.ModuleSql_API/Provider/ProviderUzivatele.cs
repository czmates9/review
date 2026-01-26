using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql_API
{
    public partial class Provider : Fask.Interfaces.Uzivatele.IUzivatele2,
        Fask.Interfaces.Uzivatele.IUzivatele2_OverUzivatele
    {
        public bool OverUzivatele(string UserID, string pwd, string TID, string TID_typ)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                //misto vztvoreni instance yavolat metodu providera kde se preda IDUyivatele heslo TODO MaR
                FASK.Logins.Uzivatel.Instance = 
                    new FASK.Logins.Uzivatel(
                        Globals_V1.Konfigurace.Nastaveni[0].Adresa,
                        Globals_V1.Konfigurace.Nastaveni[0].Autorizace_DoAPI,
                        Globals_V1.Konfigurace.Nastaveni[0].AliasDB,
                        Globals_V1.Konfigurace.Nastaveni[0].isHTTPS,
                        Globals_V1.Konfigurace.Nastaveni[0].API_TimeOut,
                        UserID,
                        TID,
                        TID_typ);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw new Exception("Nastala chyba při načtení uživatele!");
            }

            if (FASK.Logins.Uzivatel.Instance.UserID == null)
                throw new Exception("Zadaný uživatel neexistuje");

            if (FASK.Logins.Uzivatel.Instance.Heslo.Trim() != pwd)
                throw new Exception("Heslo uživatele " + FASK.Logins.Uzivatel.Instance.FirstName.Trim() + " " + FASK.Logins.Uzivatel.Instance.SurName.Trim() + " není zadáno správně");


            if (!FASK.Logins.Uzivatel.Instance.GetPravaKonzole())
                throw new Exception("Zadaný uživatel nemá práva na konzolu!");

            return true;

        }
    }
}
