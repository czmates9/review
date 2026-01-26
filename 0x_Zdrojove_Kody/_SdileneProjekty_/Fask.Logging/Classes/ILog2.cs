using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Logging
{
    internal interface ILog2
    {

        bool EnablePrint { get; set; } 

        void SetPath(string Path);
        
        bool write(LogLevel level, string Message);
        bool write(Exception ex);
		bool write(Exception ex, string Modul, string Metoda);
		bool write(LogLevel level, string Modul, string Metoda, string Message);
        bool write(System.Data.DataSet DS);
        bool write(System.Data.DataTable DT);

        bool writeToFile(string message, string PathToFile);

        bool writeToFile(string message, string FileName, string FileExtension);

        string write(byte[] data, string specification);

		bool write_Email(string Message);

		bool DeleteLog(LogLevel level);

        void WriteScannerCSV(int message, string ean);
        void WriteDataStoreCSV(string login, decimal qty, decimal qtyreal, string barcode);
        
        string GetLog(string FileName);
    }
}
