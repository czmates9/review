using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Logging
{
    internal interface ILog2
    {
        void SetPath(string Path);

        bool write(LogLevel level, string Message);
        bool write(Exception ex);
		bool write(Exception ex, string Modul, string Metoda);
		bool write(LogLevel level, string Modul, string Metoda, string Message);
        bool write(System.Data.DataSet DS);
        bool write(System.Data.DataTable DT);

        bool writeToFile(string message, string PathToFile);

		string write(byte[] data, string specification);

		bool write_Email(string Message);

		bool DeleteLog(LogLevel level);
    }
}
