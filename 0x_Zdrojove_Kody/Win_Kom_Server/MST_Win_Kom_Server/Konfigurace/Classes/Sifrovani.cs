using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.MST_W_Server.Konfigurace.Classes
{
    public class Sifrovani
    {

        public static bool Sifruj = false;
        public static string Prefix = "PWD_";

        public static string Sifruj_Do_Base64(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Sifruj_Z_Base64(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}