using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;

namespace RemoteViewing.Example
{
    public class Config
    {
        public String Host { get; set; }
        public int Port { get; set; }
        public String Password { get; set; }
        public byte[] ProtectedPassword { get; set; }
        public PixelFormat PixelFormat { get; set; }

        public static Config ReadFromFile(String fileName)
        {
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(fileName));
        }

        public void Save(String fileName)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public void ProtectPassword()
        {
            if (Password != null)
            {
                ProtectedPassword = ProtectedData.Protect(Encoding.UTF8.GetBytes(Password), null, DataProtectionScope.CurrentUser);
                Password = null;
            }
        }

        public String GetUnprotectedPassword()
        {
            var p = ProtectedData.Unprotect(ProtectedPassword, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(p);
        }
    }
}
