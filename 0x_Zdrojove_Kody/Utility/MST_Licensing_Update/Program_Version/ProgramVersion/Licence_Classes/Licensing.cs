using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Web;


namespace ProgramVersion.LicenceClasses
{
    public static class Licensing
    {

        /// <summary>
        /// Slouzi pro nacteni souboru licence a pak moznost pridat klic na zasifrovany
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string GetFileContents(string FileName)
        {
            try
            {
                return GetFileContents(FileName, 5000);
            }
            catch { throw; }
        }

        public static string GetFileContents(string FileName, int TimeOut)
        {
            StreamReader Reader = null;
            int StartTime = System.Environment.TickCount;
            try
            {
                bool Opened = false;
                while (!Opened)
                {
                    try
                    {
                        if (System.Environment.TickCount - StartTime >= TimeOut)
                            throw new System.IO.IOException("File opening timed out");
                        Reader = File.OpenText(FileName);
                        Opened = true;
                    }
                    catch (System.IO.IOException e)
                    {
                        throw e;
                    }
                }
                string Contents = Reader.ReadToEnd();
                Reader.Close();
                return Contents;
            }
            catch
            {
                return "";
            }
            finally
            {
                if (Reader != null)
                {
                    Reader.Close();
                    Reader.Dispose();
                }
            }
        }
    }
}