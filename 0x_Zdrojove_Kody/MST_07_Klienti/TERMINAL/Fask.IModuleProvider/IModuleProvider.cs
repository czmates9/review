using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.ModuleProvider
{
    /// <summary>
    /// Parametry spousteni
    /// </summary>
    public struct ExecuteParams
    {
        public string userLogin;
        public int userId;
        public string userPwd;
        public byte termId;

        public string serverAddress;
        public int serverTimeout;
        public int UIGridFont;
        public string UIFormatDesCisel;

        public ExecuteParams(byte termId, string userLogin, int userId, string userPwd, string serverAddress, int serverTimeout, int UIGridFont, string UIFormatDesCisel)
        {
            this.termId = termId;
            this.userLogin = userLogin;
            this.userId = userId;
            this.userPwd = userPwd;
            this.UIGridFont = UIGridFont;

            this.serverAddress = serverAddress;
            this.serverTimeout = serverTimeout;
            this.UIFormatDesCisel = UIFormatDesCisel;
        }
    }

    /// <summary>
    /// Rozhrani pro externi moduly
    /// </summary>
    /// <example> Execute(new ExecuteParams("1111", "1"), scanner) </example>
    public interface IModuleProvider
    { 
        /// <summary>
        /// Provede spusteni modulu
        /// </summary>
        /// <param name="login"></param>
        void Execute(ExecuteParams execParam, Fask.ScannerProvider.IScannerProvider scanner);
        //void Execute(ExecuteParams execParam);
    }
}
