using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Fask.Logging
{
    public class Log2 : ILog
    {
        public bool DeleteLicenceLog()
        {
            throw new NotImplementedException();
        }
        public bool writeError(string messagedata, string filename)
        {
            throw new NotImplementedException();
        }
        public bool writeLicenceLog(string message)
        {
            throw new NotImplementedException();
        }
        public bool writeLog(string message, string context)
        {
            throw new NotImplementedException();
        }

        #region ErrorData
        public bool writeErrorData(string data, string specification)
        {
            throw new NotImplementedException();
        }

        public bool writeErrorData(FileInfo fi, string specification)
        {
            throw new NotImplementedException();
        }

        public bool writeErrorData(byte[] data, string specification)
        {
            throw new NotImplementedException();
        }

        public bool writeErrorData(DataRow[] datarows, string specification)
        {
            throw new NotImplementedException();
        }

        public bool writeErrorData(DataSet dataset, string specification)
        {
            throw new NotImplementedException();
        }

        public bool writeErrorData(DataSet ds)
        {
            throw new NotImplementedException();
        }

        public bool writeErrorData(DataTable table)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ErrorLog
        public bool writeErrorLog(Exception ex)
        {
            throw new NotImplementedException();
        }

        public bool writeErrorLog(string errormessage)
        {
            throw new NotImplementedException();
        }

        public bool writeErrorLog(string modul, string method, string message)
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region OKData
        public bool writeOKData(string data, string specification)
        {
            throw new NotImplementedException();
        }

        public bool writeOKData(FileInfo fi, string specification)
        {
            throw new NotImplementedException();
        }

        public bool writeOKData(byte[] data, string specification)
        {
            throw new NotImplementedException();
        }

        public bool writeOKData(DataSet dataset, string specification)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
