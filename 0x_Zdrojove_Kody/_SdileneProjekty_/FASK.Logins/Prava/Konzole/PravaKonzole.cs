using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.Logins
{
    public partial class Uzivatel
    {

        public bool GetPravaKonzole()
        {

            if (ListinaPravASlobod.Count > 0)
            {
                foreach (Classes.IPrava item in ListinaPravASlobod)
                {
                    if (item is Classes._)
                        return true;

                    if (item is Classes.K_Admin_)
                        return true;

                    if (item is Classes.K_)
                        return true;
                }

                return false;
            }
            else
                return false;
        }

        public bool GetPravaKonzole_Admin()
        {

            if (ListinaPravASlobod.Count > 0)
            {
                foreach (Classes.IPrava item in ListinaPravASlobod)
                {
                    if (item is Classes._)
                        return true;

                    if (item is Classes.K_Admin_)
                        return true;
                }

                return false;
            }
            else
                return false;
        }

        public bool GetPravaKonzole_IT()
        {

            if (ListinaPravASlobod.Count > 0)
            {
                foreach (Classes.IPrava item in ListinaPravASlobod)
                {
                    if (item is Classes._)
                        return true;

                    //if (item is Classes.K_Admin_)
                    //    return true;

                    if (item is Classes.K_IT_)
                        return true;

                }

                return false;
            }
            else
                return false;
        }

        public bool GetPravaKonzole_IT_Arch()
        {

            if (ListinaPravASlobod.Count > 0)
            {
                foreach (Classes.IPrava item in ListinaPravASlobod)
                {
                    //if (item is Classes._)
                    //    return true;

                    //if (item is Classes.K_Admin_)
                    //    return true;

                    if (item is Classes.K_IT_Arch)
                        return true;

                }

                return false;
            }
            else
                return false;
        }


        public bool GetPravaKonzole_P_ZP()
        {

            if (ListinaPravASlobod.Count > 0)
            {
                foreach (Classes.IPrava item in ListinaPravASlobod)
                {
                    //if (item is Classes._)
                    //    return true;

                    //if (item is Classes.K_Admin_)
                    //    return true;

                    if (item is Classes.K_P_ZP)
                        return true;

                }

                return false;
            }
            else
                return false;
        }

        public bool GetPravaKonzole_Editace()
        {

            if (ListinaPravASlobod.Count > 0)
            {
                foreach (Classes.IPrava item in ListinaPravASlobod)
                {
                    //if (item is Classes._)
                    //    return true;

                    //if (item is Classes.K_Admin_)
                    //    return true;

                    if (item is Classes.K_E_)
                        return true;

                }

                return false;
            }
            else
                return false;
        }


        public bool GetPravaKonzole_Import()
        {

            if (ListinaPravASlobod.Count > 0)
            {
                foreach (Classes.IPrava item in ListinaPravASlobod)
                {
                    //if (item is Classes._)
                    //    return true;

                    //if (item is Classes.K_Admin_)
                    //    return true;

                    if (item is Classes.K_Imp_)
                        return true;

                }

                return false;
            }
            else
                return false;
        }


        public bool GetPravaKonzole_P_Approval()
        {

            if (ListinaPravASlobod.Count > 0)
            {
                foreach (Classes.IPrava item in ListinaPravASlobod)
                {
                    if (item is Classes._)
                        return true;

                    if (item is Classes.K_P_Approval_)
                        return true;

                }

                return false;
            }
            else
                return false;
        }



    }
}
