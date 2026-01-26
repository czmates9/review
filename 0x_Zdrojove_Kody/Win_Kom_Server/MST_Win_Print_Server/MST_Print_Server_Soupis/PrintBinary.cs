using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WinForms;
using Fask.Logging;

namespace MST_Print_Server_Soupis
{
    class PrintBinary
    {
        private const int TISKARNA_ID = 99;

        LocalReport report;
        MST_Print_Server_ZPL_Printing.RAW_Printing raw_printing_object;

        public PrintBinary(LocalReport _report)
        {
            report = _report;

            raw_printing_object = new MST_Print_Server_ZPL_Printing.RAW_Printing();
        }

        //public bool Print()
        //{
        //    bool succed = false;

        //    try
        //    {
        //        List<byte[]> pages = RenderReport();

        //        raw_printing_object.NactiTabulkaTiskarna(TISKARNA_ID);

        //        if (raw_printing_object.PrintParams.CONFIG_IP != "")
        //        {
        //            foreach (byte[] page in pages)
        //            {
        //                succed = raw_printing_object.PrintIP(page);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return succed;

        //}

        public List<byte[]> RenderReport()
        {
            // Constants
            const string dpiX = "600";
            const string dpiY = "600";
            const string outFotmat = "emf";

            // Private variables for rendering
            string deviceInfo = null;
            string format = "IMAGE";
            string encoding;
            string mimeType;
            Warning[] warnings = null;
            string fileNameExtensions = string.Empty;
            string[] streamIDs = null;
            //Byte[][] pages = null;

            report.Refresh();

            List<Byte[]> pages = new List<byte[]>();
            int i = -1;

            Guid debugGuid = Guid.NewGuid();
            // main loop
            do
            {
                i++;

                // Build device info based on the start page
                deviceInfo =
                    String.Format(@"<DeviceInfo>
                                    <OutputFormat>{0}</OutputFormat>
                                    <StartPage>{3}</StartPage>
                                    <DpiX>{1}</DpiX>
                                    <DpiY>{2}</DpiY>
                                    </DeviceInfo>", outFotmat, dpiX, dpiY, i + 1);

                //Exectute the report and get page count.
                try
                {
                    //report.Render(,

                    Byte[] actualPage = report.Render(
                       format,
                       deviceInfo,
                       out mimeType,
                       out encoding,
                       out fileNameExtensions,
                       out streamIDs,
                       out warnings
                       );


                    //if (Log.Enable)
                    //{
                    //    string debugfilename = "Souhrnne_Info_" + i.ToString() + "_" + debugGuid.ToString() + "." + fileNameExtensions;
                    //    File.WriteAllBytes(Path.Combine(Log.Directory, debugfilename), actualPage);
                    //}

                    pages.Add(actualPage);

                }
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    throw ex;
                }

            } while (pages[i].Length != 0);


            pages.RemoveAt(i);


            return pages;
        }
    }
}
