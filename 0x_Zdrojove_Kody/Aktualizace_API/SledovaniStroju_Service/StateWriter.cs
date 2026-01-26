using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FASK
{
	public class StateWriter
	{
        public static string LogPath
        {
            get
            {
                Uri u = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                return Path.Combine(Path.GetDirectoryName(u.LocalPath), Properties.Settings.Default.LogFile);
            }
        }

        public static void Delete()
        {
            File.Delete(LogPath);
        }

		public static void Write(string stateLine, bool append)
		{
			StreamWriter sw = null;
			try 
			{
				// Proved neco dalsiho ... 
                sw = new StreamWriter(LogPath, append);
                sw.WriteLine(DateTime.Now.ToString("G"));
				sw.WriteLine(stateLine);
				sw.Close();        
				sw = null;
			}
			catch //(Exception ex)
			{
				
			}        
			finally
			{
				if (sw != null)
				{
					sw.Close();
					sw = null;
				}
			}
		}
	}
}
