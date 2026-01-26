using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Module.Inside
{
    public class Provider : Fask.ModuleProvider.IModuleProvider
    {
        #region IModuleProvider Members

        public void Execute(Fask.ModuleProvider.ExecuteParams execParam, Fask.ScannerProvider.IScannerProvider scanner)
        {
            Globals.Scanner = scanner;
            Globals.UserID = execParam.userId;
            Globals.UserLogin = execParam.userLogin;
            Globals.UserPwd = execParam.userPwd;

            using (FormMain frmMain = new FormMain())
            {
                frmMain.ShowDialog();
            }
        }

        #endregion
    }
}
