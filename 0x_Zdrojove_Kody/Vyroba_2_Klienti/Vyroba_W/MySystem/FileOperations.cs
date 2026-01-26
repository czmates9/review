using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Fask.Vyroba_W.MySystem
{
    class FileOperations
    {
        public static void DBSave(string filename, ref byte[] data)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(
                    filename,
                    FileMode.Create,
                    FileAccess.Write);
                fs.Write(data, 0, data.Length);
            }
            finally
            {
                fs.Flush();
                fs.Close();
                fs = null;
            }
        }

        public static byte[] DBLoad(string filename)
        {
            FileStream fs = null;
            byte[] data = new byte[0];
            try
            {
                long fileLen = (new FileInfo(filename)).Length;
                data = new byte[fileLen];
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                int bytesRead = fs.Read(data, 0, (int)fileLen);
                if ((long)bytesRead < fileLen)
					Logging.Log.Write("bytesRead < fileLen : " + filename, "FileOperations, LoadDB");
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
            return data;
        }
    }
}
