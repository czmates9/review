using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.MySystem
{
    public static class MyAssambly
    {

        #region Get assambly
        public static System.Reflection.Assembly GetAssembly(string pAssemblyName)
        {
            System.Reflection.Assembly tMyAssembly = null;

            if (string.IsNullOrEmpty(pAssemblyName)) { return tMyAssembly; }
            tMyAssembly = GetAssemblyEmbedded(pAssemblyName);
            if (tMyAssembly == null) { GetAssemblyDLL(pAssemblyName); }

            return tMyAssembly;
        }//System.Reflection.Assembly GetAssemblyEmbedded(string pAssemblyDisplayName)


        public static System.Reflection.Assembly GetAssemblyEmbedded(string pAssemblyDisplayName)
        {
            System.Reflection.Assembly tMyAssembly = null;

            if (string.IsNullOrEmpty(pAssemblyDisplayName)) { return tMyAssembly; }
            try //try #a
            {
                tMyAssembly = System.Reflection.Assembly.Load(pAssemblyDisplayName);
            }// try #a
            catch (Exception ex)
            {
                string m = ex.Message;
            }// try #a
            return tMyAssembly;
        }//System.Reflection.Assembly GetAssemblyEmbedded(string pAssemblyDisplayName)


        public static System.Reflection.Assembly GetAssemblyDLL(string pAssemblyNameDLL)
        {
            System.Reflection.Assembly tMyAssembly = null;

            if (string.IsNullOrEmpty(pAssemblyNameDLL)) { return tMyAssembly; }
            try //try #a
            {
                if (!pAssemblyNameDLL.ToLower().EndsWith(".dll")) { pAssemblyNameDLL += ".dll"; }
                tMyAssembly = System.Reflection.Assembly.LoadFrom(pAssemblyNameDLL);
            }// try #a
            catch (Exception ex)
            {
                string m = ex.Message;
            }// try #a
            return tMyAssembly;
        }//System.Reflection.Assembly GetAssemblyFile(string pAssemblyNameDLL)


        public static string GetVersionStringFromAssembly(string pAssemblyDisplayName)
        {
            string tVersion = "Unknown";
            System.Reflection.Assembly tMyAssembly = null;

            tMyAssembly = GetAssembly(pAssemblyDisplayName);
            if (tMyAssembly == null) { return tVersion; }
            tVersion = GetVersionString(tMyAssembly.GetName().Version.ToString());
            return tVersion;
        }//string GetVersionStringFromAssemblyEmbedded(string pAssemblyDisplayName)


        public static string GetVersionString(Version pVersion)
        {
            string tVersion = "Unknown";
            if (pVersion == null) { return tVersion; }
            tVersion = GetVersionString(pVersion.ToString());
            return tVersion;
        }//string GetVersionString(Version pVersion)


        public static string GetVersionString(string pVersionString)
        {
            string tVersion = "Unknown";
            string[] aVersion;

            if (string.IsNullOrEmpty(pVersionString)) { return tVersion; }
            aVersion = pVersionString.Split('.');
            if (aVersion.Length > 0) { tVersion = aVersion[0]; }
            if (aVersion.Length > 1) { tVersion += "." + aVersion[1]; }
            if (aVersion.Length > 2) { tVersion += "." + aVersion[2].PadLeft(4, '0'); }
            if (aVersion.Length > 3) { tVersion += "." + aVersion[3].PadLeft(4, '0'); }

            return tVersion;
        }//string GetVersionString(Version pVersion)


        public static string GetVersionStringFromAssemblyEmbedded(string pAssemblyDisplayName)
        {
            string tVersion = "Unknown";
            System.Reflection.Assembly tMyAssembly = null;

            tMyAssembly = GetAssemblyEmbedded(pAssemblyDisplayName);
            if (tMyAssembly == null) { return tVersion; }
            tVersion = GetVersionString(tMyAssembly.GetName().Version.ToString());
            return tVersion;
        }//string GetVersionStringFromAssemblyEmbedded(string pAssemblyDisplayName)


        public static string GetVersionStringFromAssemblyDLL(string pAssemblyDisplayName)
        {


            string tVersion = "Unknown";
            System.Reflection.Assembly tMyAssembly = null;

            tMyAssembly = GetAssemblyDLL(pAssemblyDisplayName);
            if (tMyAssembly == null) { return tVersion; }
            tVersion = GetVersionString(tMyAssembly.GetName().Version.ToString());
            return tVersion;
        }//string GetVersionStringFromAssemblyEmbedded(string pAssemblyDisplayName)

        #endregion


        #region Example hot to use

                //        try
                //{
                //    //string gvsfae = MyAssambly.GetVersionStringFromAssemblyEmbedded("\\Windows\\sqlceme35.dll");
                //    //string gvsfad1 = MyAssambly.GetVersionStringFromAssemblyDLL("\\Windows\\sqlceme35.dll");
                //    string gvsfad1 = MyAssambly.GetVersionStringFromAssemblyDLL(Path.Combine("Windows", "SymDev.dll"));
                //    //string gvsfae = MyAssambly.GetVersionStringFromAssemblyEmbedded(Path.Combine(Main.WrkDir, "ionic.zip.cf.dll"));
                //    string gvsfad2 = MyAssambly.GetVersionStringFromAssemblyDLL(Path.Combine(Main.WrkDir, "ionic.zip.cf.dll"));


                //    //Logging.Log.Write("Verze gvsfae :" + gvsfae, "Prijem List");
                //    Logging.Log.Write("Verze gvsfad1 :" + gvsfad1, "Prijem List");
                //    Logging.Log.Write("Verze gvsfad2 :" + gvsfad2, "Prijem List");
                //    //Assembly assembly = Assembly.LoadFrom("sqlcecompact35.dll");
                //    //Version ver = assembly.GetName().Version;
                //   //Logging.Log.Write(string.Format("Verze:{0}:{1}:{2}:{3}", ver.Major, ver.Minor, ver.Revision, ver.Build), "Prijem List");
                //}
                //catch
                //{
                //    Logging.Log.Write("Verze Nenalezena", "Prijem List");

                //}

        #endregion

    }
}
