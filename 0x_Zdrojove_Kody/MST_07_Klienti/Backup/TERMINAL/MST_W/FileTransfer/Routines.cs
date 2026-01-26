using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Fask.MST_W.FileTransfer
{
    public class Routines
    {
        public static void DownloadDecompressDelete(string filename)
        {
            FileTransfer.Downloading.DownloadFileFromServer(filename + ".zip");
            FileTransfer.CompressFile.DeCompressFromZip(filename + ".zip", filename);
            Fask.MST_W.FileTransfer.FileOperations.DeleteFileOnServer(Path.GetFileName(filename + ".zip"));
            Fask.MST_W.FileTransfer.FileOperations.DeleteFileOnServer(Path.GetFileName(filename));
            File.Delete(filename + ".zip");
        }
    }
}
