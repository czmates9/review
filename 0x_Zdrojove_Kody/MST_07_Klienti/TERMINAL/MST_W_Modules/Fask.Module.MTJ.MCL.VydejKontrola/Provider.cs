using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Module.MTJ.MCL.VydejKontrola
{
    public class Provider : Fask.ModuleProvider.IModuleProvider
    {
        #region IModuleProvider Members

        public void Execute(Fask.ModuleProvider.ExecuteParams execParam, Fask.ScannerProvider.IScannerProvider scanner)
        {
            Globals.Scanner = scanner;
            Globals.TermID = execParam.termId;
            Globals.UserID = execParam.userId;
            Globals.UserLogin = execParam.userLogin;
            Globals.UserPwd = execParam.userPwd;

            Globals.ServerAddress = execParam.serverAddress;
            Globals.ServerTimeout = execParam.serverTimeout;
            Globals.UIGridFont = execParam.UIGridFont;
            Globals.UIFormatDesCisel = execParam.UIFormatDesCisel;

            using (FormMain frmMain = new FormMain())
            {
                frmMain.ShowDialog();
            }
        }

        #endregion
    }
}
