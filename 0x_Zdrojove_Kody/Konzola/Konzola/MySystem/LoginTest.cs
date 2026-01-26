using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Konzola.MySystem
{
    class LoginTest
    {
        ///// <summary>
        ///// Test, zdali je uživatel přihlášen a je administrátor. Pokud není, vrací false.
        ///// </summary>
        ///// <returns></returns>
        //public static bool UserLoginAdminTest()
        //{            
        //    if (!UserLoginTest())
        //        return false;

        //    ///TODO zistit prava

        //    //if (Globals.PracovnikOpravneni == null || Globals.PracovnikOpravneni.ADM <= 0)
        //    //{
        //    //    MessageBox.Show("Uživatel nemá dostatečná oprávnění", "Chyba", MessageBoxButtons.OK);

        //    //    return false;
        //    //}



        //    return true;
        //}


        /// <summary>
        /// Test, zdali je uživatel přihlášen a je administrátor. Pokud není, vrací false.
        /// </summary>
        /// <returns></returns>
        public static string UserLoginAdminTestKonfigurace()
        {
            if (!UserLoginTest())
                return "CANCEL";

            //TODO zistovat zda je admin...

            //if (Globals.PracovnikOpravneni == null || Globals.PracovnikOpravneni.ADM <= 0)
            if (!FASK.Logins.Uzivatel.Instance.GetPravaKonzole_Admin())
            {
                if (MessageBox.Show("Uživatel nemá dostatečná oprávnění!" + Environment.NewLine + "Příhlásit se pomoci hesla?", "Upozornení", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    return "HESLO";
                }

                return "CANCEL";
            }

            return "ADM";
        }

        /// <summary>
        /// Test, zdali je uživatel přihlášen. Pokud není, vrací false.
        /// </summary>
        /// <returns></returns>
        public static bool UserLoginTest()
        {
            if ((FASK.Logins.Uzivatel.Instance == null) || (FASK.Logins.Uzivatel.Instance.UserID == null))
            {
                MessageBox.Show("Uživatel není přihlášen", "Chyba", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }
    }
}
