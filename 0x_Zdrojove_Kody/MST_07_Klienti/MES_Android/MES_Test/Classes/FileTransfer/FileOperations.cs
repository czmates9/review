using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MES_Android
{
    public class FileOperations
    {
        /// <summary>
        /// Smaze uvedeny soubor z adresare terminalu ...
        /// </summary>
        /// <param name="filename"></param>
        public static void DeleteFileOnServer(string filename)
        {
            //_WebRefernces_Globals.FileTransferServiceSession fts = new Fask.MST_W._WebRefernces_Globals.FileTransferServiceSession();
            //fts.Url = Config.Settings.Adresa + "FileTransfer.asmx";
            //fts.Timeout = 1000;
            //fts.UpdateWebServiceCredentials();
            //fts.Delete(MST_Global.TerminalID, filename);

            //[Obselete]
           FileTransfer.FileTransfer ft = new FileTransfer.FileTransfer();
            ft.Url = Config.Settings.Adresa + "FileTransfer.asmx";
            ft.Timeout = Config.Settings.TimeOut;
            ft.Delete(Config.Settings.TerminalID, filename);
        }
    }
}