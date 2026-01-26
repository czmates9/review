using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Fask.MST_W.MySystem
{
    class FileOperations
    {
        public static void VydejkaSave(string filename, VydejService.Vydej vydejData)
        {
            vydejData.WriteXml(filename, System.Data.XmlWriteMode.DiffGram);
        }

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
                fs.Flush();
            }
            finally
            {
                fs.Close();
                fs = null;
            }
        }

        public static void DBSave(string filename, ref byte[] data, long offset)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(
                    filename,
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite);
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            finally
            {
                fs.Close();
                fs = null;
            }
        }


        public static void VydejkaLoadXml(string filename, VydejService.Vydej vydejData)
        {
            vydejData.ReadXml(filename, System.Data.XmlReadMode.IgnoreSchema);
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

        public static byte[] DBLoad(string filename, long offset, int bufferLength)
        {
            Logging.TracId tid0 = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, "MySystem.FileOperations", "DBLoad");
            Logging.Trace2.Write("Start", "nacitani dat vydejky", tid0);
            FileStream fs = null;
            byte[] data = new byte[0];
            try
            {
                long fileLen = (new FileInfo(filename)).Length;
                data = new byte[fileLen - offset < bufferLength ? fileLen - offset : bufferLength];

                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                fs.Seek(offset, SeekOrigin.Begin);

                int bytesRead = fs.Read(data, 0, data.Length);
                if ((long)bytesRead < data.Length)
                    Logging.Log.Write("bytesRead < bufferLength : " + filename, "FileOperations, LoadDB");
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
            Logging.Trace2.Write("End", "nacitani dat vydejky", tid0);
            return data;
        }

        public static string CheckFileHash(string filename)
        {
            string FilePath = filename;
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hash;
            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
                hash = md5.ComputeHash(fs);
            return BitConverter.ToString(hash);
        }
    }
}
