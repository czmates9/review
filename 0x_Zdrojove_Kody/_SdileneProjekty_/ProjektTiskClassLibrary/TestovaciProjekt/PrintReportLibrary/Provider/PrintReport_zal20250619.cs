using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Data;
using System.Xml.Linq;

namespace PrintReportLibrary
{


    /// <summary>
    /// Enum ktery obsahuje typ reportu, ke kazdemu typu je potreba v Properties nadefinovat nazev reportu...
    /// </summary>
    public enum TypeReport
    {
        DoPradelny_OK,
        ZPradelny_OK,
        DoPradelny_Zle,
        ZPradelny_Zle
    }

    public enum Projekt 
    {
        DDD,
        ZZS,
        MST
    }

    public enum TypeData
    {
        Unknow,
        BindingSource,
        DataTable,
        IEnumerable,
        Object,
        Typ,
        DS_Soupis_Polozky,
        DS_Soupis_Hlavicka,
        DS_Soupis_Paticka
    }


    public partial class CoolPrintPreviewDialog 
    {

        #region Promenne

        /// <summary>
        /// Public
        /// </summary>

        private string _countEntries;
        public string CountEntries
        {
            set { this._countEntries = value; }
            get { return this._countEntries; }
        }


        private string _hlavickaKod;
        public string HlavickaKod
        {
            set { this._hlavickaKod = value; }
            get { return this._hlavickaKod; }
        }


        private string _hlavickaKodIMG;
        public string HlavickaKodIMG
        {
            set { this._hlavickaKodIMG = value; }
            get { return this._hlavickaKodIMG; }
        }
        

        private TypeReport _typereport;
        public TypeReport Typereport
        {
            set { this._typereport = value; }
            get { return this._typereport; }
        }

        private string _PrinterName;
        public string PrinterName
        {
            set { this._PrinterName = value; }
            get { return this._PrinterName; }
        }


        private bool _ShowPreview;
        public bool ShowPreview
        {
            set { this._ShowPreview = value; }
            get { return this._ShowPreview; }
        }

        private string _path = null;
                public string Path
        {
            set { this._path = value; }
            get { return this._path; }
        }

                private Projekt _projekt;
                public Projekt Projekt
                {
                    set { this._projekt = value; }
                    get { return this._projekt; }
                }
        

        #region Predavane data do reportu

        private List<TypeData> _typedata;
        public List<TypeData> typedata
        {
            set { _typedata = value; }
            get { return _typedata; }
        }

        private string _Name = null;
        public string NazevDataTable
        {
            set { _Name = value; }
            get { return _Name; }
        }

        private DataSets.DS_Soupis _DS_Soupis;
        public DataSets.DS_Soupis DS_Soupis
        {
            set { this._DS_Soupis = value; }
            get { return this._DS_Soupis; }
        }

        private Microsoft.Reporting.WinForms.ReportParameter[] _Params = null;
        public Microsoft.Reporting.WinForms.ReportParameter[] Params
        {
            set { _Params = value; }
            get { return _Params; }
        }


                /// <summary>
        /// Binding Source pro report
        /// </summary>
        private System.Windows.Forms.BindingSource _bindingsource = null;
        public System.Windows.Forms.BindingSource bindingsource
        {
            set { _bindingsource = value; }
            get { return _bindingsource; }

        }


        // Datatable je moc obecne, pro konkretne
        /// <summary>
        /// DataTable predavany do reportu
        /// </summary>
        private DataTable _DataTable = null;
        public DataTable DataTable
        {
            set { _DataTable = value; }
            get { return _DataTable; }
        }

        /// <summary>
        /// IEnumerable predavane data
        /// </summary>
        private System.Collections.IEnumerable _ienumerable = null;
        public System.Collections.IEnumerable ienumerable
        {
            set { _ienumerable = value; }
            get { return _ienumerable; }

        }

        /// <summary>
        /// Objekt
        /// </summary>
        private object _Objekt = null;
        public object Objekt
        {
            set { _Objekt = value; }
            get { return _Objekt; }
        }

        /// <summary>
        /// Type
        /// </summary>
        private Type _typ = null;
        public Type typ
        {
            set { _typ = value; }
            get { return _typ; }
        }

        #endregion


        #endregion



        /// <summary>
        /// Private
        /// </summary>
        /// 

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        private LocalReport report;

        private IWin32Window owner = null;


        //#endregion

        /// <summary>
        /// CTor 
        /// </summary>
        public CoolPrintPreviewDialog() : this(null)
        { 
            report = new LocalReport(); 
        }

        public bool Print(IWin32Window owner) 
        {
            this.owner = owner;

            if (!InitPrint())
                return false;

            if (!RenderReport("Image", "PNG"))
                return false;

            if (!PrintOut())
                return false;

            return true;
        }


        public bool Print()
        {
            return Print(null);
        }

        protected bool PrintOut()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");

            PrintDocument printDoc = new PrintDocument();

            printDoc.DocumentName = "FASK Tisk Report";
            printDoc.PrinterSettings.PrinterName = this._PrinterName;

            Margins margins = new Margins(100, 100, 100, 100);
            printDoc.DefaultPageSettings.Margins = margins;

            PaperSize papersize = new PaperSize("A4", 827, 1169);
            papersize.RawKind = 9;
            printDoc.DefaultPageSettings.PaperSize = papersize;

            PrinterSettings.PaperSizeCollection size = printDoc.PrinterSettings.PaperSizes;

            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;

                ///Tady je rozhodovani zda se bude tisknout primo nebo cez dialog
                ///

                if (_ShowPreview)
                {
                    this.Document = printDoc;
                    this.ShowDialog(this.owner);

                    //using (var dlg = new CoolPrintPreviewDialog())
                    //{
                    //    dlg.Document = printDoc;
                        
                    //    dlg.ShowDialog();
                    //}
                }
                else 
                {
                    printDoc.Print();
                }
            }


            return true;
        }

        protected void PrintPage(object sender, PrintPageEventArgs ev)
        {
            try
            {
                //Ked sa generuju stranky na tisk anebo znovu pregenerovavaju tak nastaveni znovu na nulu
                if (m_currentPageIndex >= m_streams.Count)
                    m_currentPageIndex = 0;

                //Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
                Image pageImage = Image.FromStream(m_streams[m_currentPageIndex]);


                m_streams[m_currentPageIndex].Position = 0;
                Stream str = m_streams[m_currentPageIndex];
                str.Position = 0;

                // Adjust rectangular area with printer margins.
                Rectangle adjustedRect = new Rectangle(
                    ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                    ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                    ev.PageBounds.Width,
                    ev.PageBounds.Height);



                // Draw a white background for the report
                ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

                // Draw the report content
                ev.Graphics.DrawImage(pageImage, adjustedRect);

                // Prepare for the next page. Make sure we haven't hit the end.
                m_currentPageIndex++;

                ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool RenderReport_old(string Format, string OutputFormat)
        {
            string deviceInfo = string.Format("<DeviceInfo><OutputFormat>{0}</OutputFormat>"
                                   + "<PageWidth>8.27in</PageWidth>"
                                   + " <PageHeight>11.69in</PageHeight>"
                                   + " <MarginTop>0.393in</MarginTop>"
                                   + " <MarginLeft>0.393in</MarginLeft>"
                                   + " <MarginRight>0.393in</MarginRight>"
                                   + " <MarginBottom>0.393in</MarginBottom>"
                                   + " <DpiX>600</DpiX>"
                                   + " <DpiY>600</DpiY>"
                                   + " <Columns>0</Columns>"
                                   + " <ColumnSpacing>0.3937in</ColumnSpacing>"
                                   + " <PrintDpiX>8.27in</PrintDpiX>"
                                   + " <PrintDpiY>11.69in</PrintDpiY>"
                                   + " </DeviceInfo>", OutputFormat);


            try
            {
                Warning[] warnings;
                m_streams = new List<Stream>();
                report.Render(Format, deviceInfo, CreateStream, out warnings); //zde to vyhodi chybu: {"An error occurred during local report processing."} ...pouzivam dataset Hlavni a visual studio 2019 je dobre deviceInfo? zkus celou tuto metodu reimplementovat

                if (warnings.Count() > 0)
                {
                    ///neco s warningama udelat... ale co??
                }

                foreach (Stream stream in m_streams)
                    stream.Position = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return true;
        }

        private bool RenderReport(string format, string outputFormat)
        {
            if (string.IsNullOrWhiteSpace(outputFormat))
            {
                throw new ArgumentException("Output format cannot be null or empty.");
            }

            // Escapování speciálních znaků v outputFormat
            string outputFormatEscaped = System.Security.SecurityElement.Escape(outputFormat);

            // Vytvoření DeviceInfo pomocí XElement
            var deviceInfoXml = new XElement("DeviceInfo",
                new XElement("OutputFormat", outputFormatEscaped),
                new XElement("PageWidth", "8.27in"),
                new XElement("PageHeight", "11.69in"),
                new XElement("MarginTop", "0.393in"),
                new XElement("MarginLeft", "0.393in"),
                new XElement("MarginRight", "0.393in"),
                new XElement("MarginBottom", "0.393in"),
                // Přidání DPI nastavení, které pomáhá předcházet zkreslení textu
                //JaS 6.2.2025: Do budoucna možnost navázat na licencování, bez licence rozostřit, popřípadě přidat vodoznak "DEMO"
                new XElement("DpiX", "300"),
                new XElement("DpiY", "300")
            );

            string deviceInfo = deviceInfoXml.ToString();
            Console.WriteLine($"Generated DeviceInfo XML: {deviceInfo}");

            try
            {
                // Inicializace listu pro streamy
                m_streams = new List<Stream>();
                Warning[] warnings;

                // Renderování reportu
                report.Render(format, deviceInfo, CreateStream, out warnings); //tady je chyba: {"An error occurred during local report processing."}

                // Zpracování warningů
                if (warnings != null && warnings.Length > 0)
                {
                    foreach (var warning in warnings)
                    {
                        Console.WriteLine($"Warning: {warning.Message}");
                    }
                }

                // Resetování pozic streamů
                foreach (Stream stream in m_streams)
                {
                    stream.Position = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during report rendering: {ex.Message}");
                throw; // Zachová původní stack trace
            }

            // Návrat true pouze v případě, že existují validní streamy
            return m_streams.Count > 0;
        }





        protected Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        protected bool InitPrint() 
        {
            string reportname = string.Empty;

            switch (this._typereport)
            {
                case TypeReport.DoPradelny_OK:
                    reportname = Properties.Settings.Default.NameReport_DoPradelny;
                    break;
                case TypeReport.ZPradelny_OK:
                    reportname = Properties.Settings.Default.NameReport_ZPradelny;
                    break;
                case TypeReport.DoPradelny_Zle:
                    reportname = Properties.Settings.Default.Zmetek;
                    break;
                case TypeReport.ZPradelny_Zle:
                    reportname = Properties.Settings.Default.NameReport_DoPradelny;
                    break;
                default:
                    reportname = Properties.Settings.Default.NameReport_DoPradelny;
                    break;
            }

            if (this._path != null)
            {
                report.ReportPath = this._path;
            }
            else
            {
                report.ReportPath = System.IO.Path.Combine(MyPath.PrintTemplateDirectory, reportname);
            }

            
            report.EnableExternalImages = true;

            if (this._Params != null) 
            {
                foreach (Microsoft.Reporting.WinForms.ReportParameter item in this._Params)
                {
                    if (item.Values.Count != 1)
                    {
                        //kaslu na to vynecham....
                    }
                    else 
                    {
                        parameterInsertValue(item.Name, item.Values[0]);
                    }

                    
                }

            }


            //Microsoft.Reporting.WinForms.ReportDataSource reportDataSourcePolozky = new ReportDataSource();
            //reportDataSourcePolozky.Name = "DS_Soupis_Polozky";
            //reportDataSourcePolozky.Value = this._DS_Soupis.Polozky;
            ////report.DataSources.Add(new ReportDataSource("DS_Soupis_Hlavicka",this._DS_Soupis));
            //report.DataSources.Add(reportDataSourcePolozky);
            ////report.DataSources.Add(new ReportDataSource("DS_Soupis_Paticka", this._DS_Soupis));


            if(this._typedata.Contains(TypeData.DS_Soupis_Hlavicka))
            {
                 report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DS_Soupis_Hlavicka", (DataTable)this._DS_Soupis.Hlavicka));
            }
            if(this._typedata.Contains(TypeData.DS_Soupis_Polozky))
            {
                 report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DS_Soupis_Polozky", (DataTable)this._DS_Soupis.Polozky));
            }
            if(this._typedata.Contains(TypeData.DS_Soupis_Paticka))
            {
                 report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DS_Soupis_Paticka" ,(DataTable)this._DS_Soupis.Paticka));
            }


            if(this._typedata.Contains(TypeData.BindingSource))
            {
                 report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(_Name, _bindingsource));
            }
            
            if(this._typedata.Contains(TypeData.DataTable))
            {
                report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(_Name, _DataTable));
            }
            if(this._typedata.Contains(TypeData.IEnumerable))
            {
                report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(_Name, _ienumerable));
            }
            if(this._typedata.Contains(TypeData.Object))
            {
                report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(_Name, _Objekt));
            }
            if(this._typedata.Contains(TypeData.Typ))
            {
                report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(_Name, _typ));
            }




            bool useLOGO = true;

            if (useLOGO)
            {
                //report.EnableExternalImages = true;
                parameterInsertValue("LogoHeader", new Uri(System.IO.Path.Combine(MyPath.PrintTemplateDirectory, "LOGO.png")).AbsoluteUri);
            }

            parameterInsertValue("Param_CountEntries", this._countEntries);

            if (!string.IsNullOrEmpty(this._countEntries))
            {
                ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                zw.Format = ZXing.BarcodeFormat.CODE_128;
                zw.Options.Height = 50;
                zw.Options.PureBarcode = true;
                System.Drawing.Bitmap image1 = zw.Write(this._countEntries);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] imgReportBarcode = ms.ToArray();
                ms.Close();

                parameterInsertValue("Param_CountEntries_IMG", Convert.ToBase64String(imgReportBarcode));
            }
            else
            {
                parameterInsertValue("Param_CountEntries_IMG", string.Empty);
            }



            parameterInsertValue("Param_HlavickaKod", this._hlavickaKod);

            if (!string.IsNullOrEmpty(this._hlavickaKodIMG))
            {
                ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                zw.Format = ZXing.BarcodeFormat.CODE_128;
                zw.Options.Height = 50;
                zw.Options.PureBarcode = true;
                System.Drawing.Bitmap image1 = zw.Write(this._hlavickaKodIMG);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] imgReportBarcode = ms.ToArray();
                ms.Close();

                parameterInsertValue("Param_HlavickaKod_IMG", Convert.ToBase64String(imgReportBarcode));
                parameterInsertValue("Param_HlavickaKod_val_IMG", this._hlavickaKodIMG);
            }
            else
            {
                parameterInsertValue("Param_HlavickaKod_IMG", string.Empty);
                parameterInsertValue("Param_HlavickaKod_val_IMG", string.Empty);
            }

            return true;
        }

        /// <summary>
        /// Metoda pro vlozeni parametru do reportu
        /// </summary>
        /// <param name="key"> nazev parametru</param>
        /// <param name="val"> hodnota parametru</param>
        /// <returns> vraci bool hodnotu ci byla spesne vlozena nebo ne</returns>
        private bool parameterInsertValue(string key, string val)
        {
            try
            {
                report.SetParameters(new ReportParameter(key, val));
                return true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                return false;
            }
        }


        void SaveToFile(Stream s, string type, string count)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();


            if (type == "PDF")
            {
                saveFileDialog1.Filter = "PDF File|*.pdf";
                saveFileDialog1.Title = "Save an Pfd File";
                saveFileDialog1.FileName = "NewFile";
                saveFileDialog1.DefaultExt = ".pdf";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "")
                {
                    byte[] buffer = new byte[1024]; // Change this to whatever you need

                    using (System.IO.FileStream output = new FileStream(saveFileDialog1.FileName, FileMode.Create))
                    {
                        int readBytes = 0;
                        while ((readBytes = s.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, readBytes);
                        }
                    }
                }


            }
            else if (type == "PNG")
            {
                saveFileDialog1.Filter = "Png Image|*.png";
                saveFileDialog1.Title = "Save an Image File";
                saveFileDialog1.FileName = string.Format("NewFile{0}", count == null ? string.Empty : count);
                saveFileDialog1.DefaultExt = ".png";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "")
                {
                    Image img = System.Drawing.Image.FromStream(s);
                    img.Save(saveFileDialog1.FileName, ImageFormat.Png);
                }


            }
            else
            {
                ///err
            }

        }



    }
}
