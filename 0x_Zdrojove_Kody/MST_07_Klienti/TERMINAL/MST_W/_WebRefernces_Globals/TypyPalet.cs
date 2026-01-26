using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.Forms;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.ServerAccess;
using System.Drawing;

namespace Fask.MST_W._WebRefernces_Globals
{
    public class TypyPalet
    {
        public static void Actualize_TypyPalet()
        {
            //ConfigurationService.Configuration configurations = new Fask.MST_W.ConfigurationService.Configuration();
            ConfigurationServiceSession configurations = new ConfigurationServiceSession();
            configurations.Timeout = MST_Global.ServiceTimeOut;
            configurations.Url = MST_Global.ServerAddress + "Configuration.asmx";
            configurations.UpdateWebServiceCredentials();

            string typypalet = null;

            try
            {
                Program.mstw.mbw.BeginPracujiForm("Aktualizace typů palet");
                typypalet = configurations.GetTypyPalet(MST_Global.TerminalID);
                Program.mstw.mbw.EndPracujiForm();
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                MessageBoxBig.Show(ex.Message, "Aktualizace typů palet", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            if (typypalet != null)
            {
                StreamWriter sw = null;
                try
                {
                    sw = new StreamWriter(MST_W.Main.ConfigTypyPalet, false);
                    sw.Write(typypalet);
                    sw.Close();
                    sw = null;
                }
                catch (Exception ex)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    MessageBoxBig.Show(ex.Message, "Aktualizace typů palet", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                        sw = null;
                    }
                }
            }

            Program.mstw.mbw.EndPracujiForm();
            MessageBoxBig.Show("Aktualizace typů palet úspěšně dokončena", Color.DarkGreen);
        }
    }
}
