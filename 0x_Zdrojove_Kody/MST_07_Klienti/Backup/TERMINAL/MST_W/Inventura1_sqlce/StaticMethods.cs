using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Inventura1_sqlce
{
    public class StaticMethods
    {
        #region Online Checking
        public static bool OnlineCheck(int davka, string itemnmbr, ref bool o_checked)
        {
            byte o_tid = MST_Global.TerminalID;

            if (!MST_Global.Inventura1OnlineKontrola)
                return true;


            _WebRefernces_Globals.Inventura1ServiceSession _i1_service = new _WebRefernces_Globals.Inventura1ServiceSession();
            _i1_service.Url = MST_Global.ServerAddress + "Inventura1.asmx";
            _i1_service.Timeout = MST_Global.Inventura1OnlineTimeout;
            _i1_service.UpdateWebServiceCredentials();

            do
            {
                try
                {
                    Program.mstw.mbw.BeginPracujiForm("Probíhá Online kontrola");
                    o_checked = _i1_service.OnlineCheckState(davka, MST_Global.TerminalID, itemnmbr.Trim(), out o_tid);
                    if (!o_checked)
                        MessageBoxBig.Show("Položka již byla inventarizována terminálem s ID=" + o_tid, "Online kontrola", MessageBoxButtons.OK, MessageBoxBigIcon.Information);

                    return o_checked;
                }
                catch (Exception ex)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    Logging.Log.Write(ex);
                    if (DialogResult.Retry == MessageBoxBig.Show(ex.Message + "\nOpakovat kontrolu?", "Online kontrola", MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning))
                        continue;

                    o_checked = false;
                    if (DialogResult.No == MessageBoxBig.Show("Vložit položku bez online kontroly?", "Online kontrola", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question))
                        return false;
                    else
                        return true;
                }
                finally
                {
                    Program.mstw.mbw.EndPracujiForm();
                }
            } while (true);
        }

        public static bool OnlineUnCheck(int davka, string itemnmbr, ref bool o_unchecked)
        {
            byte o_tid = MST_Global.TerminalID;

            if (!MST_Global.Inventura1OnlineKontrola)
                return true;

            _WebRefernces_Globals.Inventura1ServiceSession _i1_service = new _WebRefernces_Globals.Inventura1ServiceSession();
            _i1_service.Url = MST_Global.ServerAddress + "Inventura1.asmx";
            _i1_service.Timeout = MST_Global.Inventura1OnlineTimeout;
            _i1_service.UpdateWebServiceCredentials();

            do
            {
                try
                {
                    Program.mstw.mbw.BeginPracujiForm("Probíhá Online kontrola");
                    o_unchecked = _i1_service.OnlineUnCheckState(davka, MST_Global.TerminalID, itemnmbr.Trim(), out o_tid);
                    if (!o_unchecked)
                        MessageBoxBig.Show("Položka byla inventarizována terminálem s ID=" + o_tid, "Online kontrola", MessageBoxButtons.OK, MessageBoxBigIcon.Information);

                    return o_unchecked;
                }
                catch (Exception ex)
                {
                    Program.mstw.mbw.EndPracujiForm();
                    Logging.Log.Write(ex);
                    if (DialogResult.Retry == MessageBoxBig.Show(ex.Message + "\nOpakovat kontrolu?", "Online kontrola", MessageBoxButtons.RetryCancel, MessageBoxBigIcon.Warning))
                        continue;

                    o_unchecked = false;
                    return o_unchecked;
                }
                finally
                {
                    Program.mstw.mbw.EndPracujiForm();
                }
            } while (true);
        }
        #endregion

    }
}
