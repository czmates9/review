using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using FASK.MST_WINDOWS.Main.Configuration;
//using Fask.MST_W.ServerAccess;

namespace FASK.MST_WINDOWS.FileTransfer
{
    public class FileOperations
    {
        /// <summary>
        /// Smaze uvedeny soubor z adresare terminalu ...
        /// </summary>
        /// <param name="filename"></param>
        public static void DeleteFileOnServer(string filename)
        {
            FASK.MST_WINDOWS.Main._WebRefernces_Globals.FileTransferServiceSession fts = new FASK.MST_WINDOWS.Main._WebRefernces_Globals.FileTransferServiceSession();
            fts.Url = Config.Main_KomServer + "FileTransfer.asmx";
            fts.Timeout = 1000;
            //fts.UpdateWebServiceCredentials();
            fts.Delete(byte.Parse(Config.Main_TerminalID), filename);
        }
    }
}
