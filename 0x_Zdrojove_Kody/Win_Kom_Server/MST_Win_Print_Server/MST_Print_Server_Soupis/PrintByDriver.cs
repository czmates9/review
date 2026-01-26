using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Reporting;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
//using MST_Print_Server_Logging;
using Fask.Logging;

namespace MST_Print_Server_Soupis
{
    class PrintByDriver
    {
        private MST_Print_Server_ZPL_Printing.TiskParams printerParams;

        LocalReport report;

        //public string sopnumber = string.Empty;

        List<byte[]> pages;

        private Graphics.EnumerateMetafileProc m_delegate = null;
        private MemoryStream m_currentPageStream;
        private Metafile m_metafile = null;
        int m_numberOfPages;
        private int m_currentPrintingPage;
        private int m_lastPrintingPage;
        private int m_copies = 1;
        private Guid uniquePrintGuid;
        public int Copies { get { return m_copies; } set { m_copies = value; } }


        public PrintByDriver(LocalReport _report, MST_Print_Server_ZPL_Printing.TiskParams _printerParams, Guid guid)//, string _sopnumber)
        {
            if (_printerParams.CONFIG_NAME == string.Empty)
                throw new Exception("neni zadany nazev tiskarny");

            report = _report;
            this.printerParams = _printerParams;
            //sopnumber = _sopnumber.Trim();
            uniquePrintGuid = guid;
            PrintBinary pb = new PrintBinary(report);
            pages = pb.RenderReport();

            m_numberOfPages = pages.Count();

        }


        public bool PrintReport()
        {
            //Log.Write("PrintReport()");
            try
            {

                //PrintingPermission pperm = new PrintingPermission(System.Security.Permissions.PermissionState.Unrestricted);
                //pperm.Level = PrintingPermissionLevel.AllPrinting;

                // Wait for the report to completely render.
                if (m_numberOfPages < 1)
                    return false;
                PrinterSettings printerSettings = new PrinterSettings();
                printerSettings.MaximumPage = m_numberOfPages;
                printerSettings.MinimumPage = 1;
                printerSettings.PrintRange = PrintRange.SomePages;
                printerSettings.FromPage = 1;
                printerSettings.ToPage = m_numberOfPages;
                printerSettings.PrinterName = printerParams.CONFIG_NAME;
                printerSettings.Copies = Convert.ToInt16(m_copies); 
                // velikost papíru
                //printerSettings.DefaultPageSettings.PaperSize = new PaperSize("PaperA5", 583, 827);
                printerSettings.DefaultPageSettings.PaperSize = new PaperSize(printerParams.PAPER_KIND, printerParams.WIDTH_PAPER_SIZE, printerParams.HEIGHT_PAPER_SIZE);
                
                //var psizes = printerSettings.PaperSizes;
                //PaperSize paperSize = new PaperSize("PaperA5", printerParams.WIDTH_PAPER_SIZE, printerParams.HEIGHT_PAPER_SIZE);
                //paperSize.PaperName = paperSize.Kind.ToString();
                //printerSettings.DefaultPageSettings.PaperSize = paperSize;

                PrintDocument pd = new PrintDocument();
                m_currentPrintingPage = 1;
                m_lastPrintingPage = m_numberOfPages;
                pd.PrinterSettings = printerSettings;
                pd.DocumentName = "MST Print Server job";

                // Print report
                // Console.WriteLine("Printing report...");
                //Log.Write("pd.PrintPage += new PrintPageEventHandler( ...");
                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

				if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
                {
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.PrintInfo, "========== Printers =========");
                    //Log.Write("========== Printers =========");
                    foreach (string pname in PrinterSettings.InstalledPrinters)
                    {
                        PrinterSettings ps = new PrinterSettings();
                        ps.PrinterName = pname;

						Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.PrintInfo, pname + " : Valid=" + ps.IsValid);
                        //Log.Write(pname + " : Valid=" + ps.IsValid);
                    }
                }

                //Log.Write("User=" + System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                //Log.Write("WindowsIdentity.Impersonate(IntPtr.Zero)");
                //using (System.Security.Principal.WindowsImpersonationContext wic =
                //    System.Security.Principal.WindowsIdentity.Impersonate(IntPtr.Zero))
                //{
                //    Log.Write("User=" + System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                //    Log.Write("WIC=" + wic.ToString());

                if (!pd.PrinterSettings.IsValid)
                {
					Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.PrintInfo, "PrinterName: " + pd.PrinterSettings.PrinterName + " is not valid!");
                }
                pd.Print();
                //}
            }

            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            finally
            {
                // Clean up goes here.
            }

            return true;
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            //Log.Write("pd_PrintPage(object sender, PrintPageEventArgs ev)");
            ev.HasMorePages = false;
            if (m_currentPrintingPage <= m_lastPrintingPage && MoveToPage(m_currentPrintingPage))
            {
                // Draw the page
                //Log.Write("ReportDrawPage(ev.Graphics);");
                //ReportDrawPage(ev.Graphics);
                //Log.Write("ReportDrawPage(ev);");
                ReportDrawPage(ev);


                // If the next page is less than or equal to the last page, 
                // print another page.
                if (++m_currentPrintingPage <= m_lastPrintingPage)
                    ev.HasMorePages = true;
            }
        }



        // Method to draw the current emf memory stream 
        private void ReportDrawPage(PrintPageEventArgs ev)//Graphics g)
        {
            //Log.Write("ReportDrawPage(PrintPageEventArgs ev)"); //Graphics g)");

            if (null == m_currentPageStream || 0 == m_currentPageStream.Length || null == m_metafile)
                return;
            lock (this)
            {
                // Set the metafile delegate.
                int width = m_metafile.Width;
                int height = m_metafile.Height;
                m_delegate = new Graphics.EnumerateMetafileProc(MetafileCallback);

                Bitmap bmp = new Bitmap(
                    m_metafile.Width,
                    m_metafile.Height
                    //, ev.Graphics
                    );

                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);

                RectangleF gRectF = g.VisibleClipBounds;
                g.EnumerateMetafile(m_metafile, g.VisibleClipBounds, m_delegate);
                float scale = 3;
                PointF pSopnumber = new PointF(gRectF.Left + 60 * scale, gRectF.Bottom - 70 * scale);
                //g.DrawString(sopnumber, new Font("Arial", 10 * scale), Brushes.Black, pSopnumber);
                g.Dispose();

                ev.Graphics.DrawImage(bmp, ev.PageBounds, gRectF, GraphicsUnit.Pixel);

				if (Fask.Logging.ExceptionHandler2.EnablePrintLogging)
                {
                    //string debugfilename = "Souhrnne_Info_" + sopnumber + "_" + m_currentPrintingPage.ToString() + "_" + (Guid.NewGuid()).ToString() + ".jpg";
                    string debugfilename = "Souhrn_" + m_currentPrintingPage.ToString() + "_" + uniquePrintGuid + ".jpg";
                    //File.WriteAllBytes(Path.Combine(Log.Directory, debugfilename), actualPage);

                    if (!Directory.Exists(Fask.MyPath.Path.PrintLogDirectory))
                        Directory.CreateDirectory(Fask.MyPath.Path.PrintLogDirectory);

                    bmp.Save(Path.Combine(Fask.MyPath.Path.PrintLogDirectory, debugfilename), ImageFormat.Jpeg);
                    bmp.Dispose();
                }
                // Clean up
                m_delegate = null;

            }
        }


		/*
		// Method to draw the current emf memory stream 
		private void ReportDrawPage(PrintPageEventArgs ev)//Graphics g)
		{
			if (null == m_currentPageStream || 0 == m_currentPageStream.Length || null == m_metafile)
				return;
			lock (this)
			{
				// Set the metafile delegate.
				int width = m_metafile.Width;
				int height = m_metafile.Height;
				//m_delegate = new Graphics.EnumerateMetafileProc(MetafileCallback);

				//Bitmap bmp = new Bitmap(
				//    ev.PageBounds.Width
				//    , ev.PageBounds.Height
				//    //, ev.Graphics
				//    );

				MemoryStream ms = new MemoryStream(m_currentPageStream.ToArray());
				Image img = Image.FromStream(ms);
				Bitmap bmp = CloneImage(img);
				//Bitmap bmp = new Bitmap(img); 
				//Bitmap bmp = Bitmap.FromStream(ms);
                
				//Graphics g = Graphics.FromImage(bmp);
				//g.Clear(Color.White);
                
				//ev.Graphics.DrawImage(bmp, Point.Empty);
				GraphicsUnit gU = GraphicsUnit.Document;

				RectangleF gF = bmp.GetBounds(ref gU);
				Graphics g = Graphics.FromImage(bmp);

				float scale = 3;

				PointF pSopnumber = new PointF(gF.Left + 60 * scale, gF.Bottom - 70 * scale);
				Font fSopnumber = new Font("Arial", 10 * scale);
				g.DrawString(sopnumber, fSopnumber, Brushes.Black, pSopnumber);

				g.Dispose();

				ev.Graphics.DrawImage(bmp, ev.PageBounds, gF, gU);                
                
				//Point pSopnumber = new Point(ev.PageBounds.Left + 60, ev.PageBounds.Bottom - 70);
				//g.DrawString(sopnumber, new Font("Arial", 10), Brushes.Black, pSopnumber);
				// zrusit comment
				//ev.Graphics.DrawString(sopnumber, new Font("Arial", 10), Brushes.Black, pSopnumber);

				//g.Dispose();

				//ev.Graphics.DrawImage(bmp, ev.PageBounds);

				if (Log.Enable)
				{
					//Bitmap bmp1 = new Bitmap(
					//    ev.PageBounds.Width
					//    , ev.PageBounds.Height
					//    //, ev.Graphics
					//    );
					//Graphics g1 = Graphics.FromImage(bmp1);
					//g1.Clear(Color.White);
					////g1.Clear(Color.White);
					////Image img1 = Image.FromStream(new MemoryStream(m_currentPageStream.ToArray()));
					//g1.DrawImage(bmp, ev.PageBounds, bmp.GetBounds(ref gU), gU);
					//g1.DrawString(sopnumber, new Font("Arial", 10), Brushes.Black, pSopnumber);


					//Bitmap btmdmp = new Bitmap(ev.PageBounds.Width, ev.PageBounds.Height);//, ev.Graphics);
					//Graphics g2 = Graphics.FromImage(btmdmp);
					//g2.Clear(Color.White);
                    
					//string debugfilename = "Souhrnne_Info_" + sopnumber + "_" + m_currentPrintingPage.ToString() + "_" + (Guid.NewGuid()).ToString() + ".bmp";
					//File.WriteAllBytes(Path.Combine(Log.Directory, debugfilename), actualPage);
                    
					//Point pSopnumber = new Point(ev.PageBounds.Left + 60, ev.PageBounds.Bottom - 70);
					//g2.DrawString(sopnumber, new Font("Arial", 10), Brushes.Black, pSopnumber);
					//g2.Dispose();
					//bmp1.Save(Path.Combine(Log.Directory, "a-" + debugfilename), ImageFormat.Bmp);
					//bmp1.Dispose();
					// tiskne se pouze cerna barva
					//Bitmap tmp = new Bitmap(ev.PageBounds.Width, ev.PageBounds.Height, ev.Graphics);
					//tmp.Save(Path.Combine(Log.Directory, debugfilename), ImageFormat.Bmp);
					//tmp.Dispose();
					//g1.Dispose();



					string debugfilename = "Souhrnne_Info_" + sopnumber + "_" + m_currentPrintingPage.ToString() + "_" + (Guid.NewGuid()).ToString() + ".jpg";
					//File.WriteAllBytes(Path.Combine(Log.Directory, debugfilename), actualPage);
					bmp.Save(Path.Combine(Log.Directory, debugfilename), ImageFormat.Jpeg);
					bmp.Dispose();
				}
                
				// \TODO : ulozit do souboru ???

				// Clean up
				m_delegate = null;
			}
		}

		private Bitmap CloneImage(Image img)
		{
			Bitmap img2 = new Bitmap(img.Width, img.Height, img.PixelFormat);
			img2.LockBits(img2.);
			using (Graphics g = Graphics.FromImage(img2))
			{
				g.Clear(Color.White);
				g.DrawImageUnscaled(img, 0, 0);
			}
			return img2;
		}
		*/


		private bool MoveToPage(Int32 page)
        {
            // Check to make sure that the current page exists in
            // the array list
            if (null == pages[m_currentPrintingPage - 1])
                return false;
            // Set current page stream equal to the rendered page
            m_currentPageStream = new MemoryStream(pages[m_currentPrintingPage - 1]);
            // Set its postion to start.
            m_currentPageStream.Position = 0;
            // Initialize the metafile
            if (null != m_metafile)
            {
                m_metafile.Dispose();
                m_metafile = null;
            }
            // Load the metafile image for this page
            m_metafile = new Metafile((Stream)m_currentPageStream);
            return true;
        }

        private bool MetafileCallback(
           EmfPlusRecordType recordType,
           int flags,
           int dataSize,
           IntPtr data,
           PlayRecordCallback callbackData)
        {
            byte[] dataArray = null;
            // Dance around unmanaged code.
            if (data != IntPtr.Zero)
            {
                // Copy the unmanaged record to a managed byte buffer 
                // that can be used by PlayRecord.
                dataArray = new byte[dataSize];
                Marshal.Copy(data, dataArray, 0, dataSize);
            }
            // play the record.      
            m_metafile.PlayRecord(recordType, flags, dataSize, dataArray);

            return true;
        }
    }
}
