using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Fask.Logging;

namespace Fask.MST_W_Server.Routines
{
    public class ManageDataFiles
    {
        public static bool Move(string source, string destination)
        {
            try
            {
                destination = DestinationCheck(destination);

                File.Move(source, destination);

                return true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle("Fask.MST_W_Server.Routines.ManageDataFiles", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool Copy(string source, string destination)
        {
            try
            {
                destination = DestinationCheck(destination);

                File.Copy(source, destination);

                return true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle("Fask.MST_W_Server.Routines.ManageDataFiles", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static string DestinationCheck(string destination)
        {
            string path = Path.GetDirectoryName(destination);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (File.Exists(destination))
            {
                destination = Path.Combine(
                    Path.GetDirectoryName(destination),
                    Path.GetFileNameWithoutExtension(destination)) 
                    + "_" + DateTime.Now.ToFileTime().ToString()
                    + Path.GetExtension(destination);
                //destination = DestinationCheck(destination);
            }
            return destination;
        }
    }
}
