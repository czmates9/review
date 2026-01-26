using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.FileTransfer
{
    public class FileOperations
    {
        /// <summary>
        /// Smaze uvedeny soubor z adresare terminalu ...
        /// </summary>
        /// <param name="filename"></param>
        public static void DeleteFileOnServer(string filename)
        {
            _WebRefernces_Globals.FileTransferServiceSession fts = new Fask.MST_W._WebRefernces_Globals.FileTransferServiceSession();
            fts.Url = MST_Global.ServerAddress + "FileTransfer.asmx";
			fts.Timeout = MST_Global.ServiceTimeOut;
            fts.UpdateWebServiceCredentials();
            fts.Delete(MST_Global.TerminalID, filename);
        }
    }
}
