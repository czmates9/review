using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fask.Logging
{
    public interface ILog
    {

        bool writeErrorData(string data, string specification);
        bool writeErrorData(FileInfo fi, string specification);
        bool writeErrorData(byte[] data, string specification);
        bool writeErrorData(System.Data.DataRow[] datarows, string specification);
        bool writeErrorData(System.Data.DataSet dataset, string specification);
        bool writeErrorData(System.Data.DataSet ds);
        bool writeErrorData(System.Data.DataTable table);
        bool writeOKData(string data, string specification);
        bool writeOKData(FileInfo fi, string specification);
        bool writeOKData(byte[] data, string specification);
        bool writeOKData(System.Data.DataSet dataset, string specification);
        bool writeError(string messagedata, string filename);
        bool writeErrorLog(Exception ex);
        bool writeErrorLog(string errormessage);
        bool writeErrorLog(string modul, string method, string message);
        bool writeLog(string message, string context);
        bool writeLicenceLog(string message);
        bool DeleteLicenceLog();
    }
}
