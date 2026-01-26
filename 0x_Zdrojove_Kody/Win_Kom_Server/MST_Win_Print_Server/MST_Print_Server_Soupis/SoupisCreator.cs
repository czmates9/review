using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WinForms;
using System.Data;
using System.Security.Principal;
using System.Runtime.InteropServices;
using MST_Print_Server_ZPL_Printing;
//using Microsoft.Reporting.WinForms;
//using Microsoft.Reporting.WinForms;
using System.Security;
using System.Security.Permissions;
using System.Reflection;
using System.Security.Policy;
using Fask.MyPath;

namespace MST_Print_Server_Soupis
{
    public class SoupisCreator 
    {
        public User user = null;

        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        private MST_Print_Server_ZPL_Printing.TiskParams printerParams;
        // nazvy datasetu, ktery se pouziva na zobrazeni dat
        private const string dataSource_Hlavicka = "DS_Soupis_Hlavicka";
        private const string dataSource_Polozky = "DS_Soupis_Polozky";
        private const string dataSource_Paticka = "DS_Soupis_Paticka";
        private Guid uniquePrintGuid = new Guid();

        /// <summary>
        /// Cesta k reportu.
        /// </summary>
        public string ReportFilePath { get; set; }
        LocalReport report;

        // data soupisu
        private Dictionary<string, string> keysHeader;
        private List<Dictionary<string, string>> keysRows;
        private Dictionary<string, string> keysFooter;
        private int pocet_vytisku;
        public Dictionary<string, string> keysParams;

        public SoupisCreator(
            MST_Print_Server_ZPL_Printing.TiskParams printerParams, 
            Dictionary<string, string> dataHeader, 
            List<Dictionary<string, string>> dataRows, 
            Dictionary<string, string> dataFooter,
            Dictionary<string, string> dataParams,
            int pocetVytisku, 
            string reportPath)
        {
            if (!System.IO.File.Exists(reportPath))
                throw new Exception("Sablona neexistuje: " + reportPath);

            this.printerParams = printerParams;

            this.keysHeader = dataHeader;
            this.keysRows = dataRows;
            this.keysFooter = dataFooter;
            this.keysParams = dataParams;

            this.ReportFilePath = reportPath;

            this.pocet_vytisku = pocetVytisku;
            report = new LocalReport();
        }

        public bool Print()
        {
            Init();
            ////Log.Write("Init()");

            ////Log.Write("FillReport(report);");

            //MST_Print_Server_DB_Layer.DBTableAdapters.CZMST_TISKARNATableAdapter adapter = new MST_Print_Server_DB_Layer.DBTableAdapters.CZMST_TISKARNATableAdapter();
            ////adapter.Connection.ConnectionString = connstring;


            //MST_Print_Server_DB_Layer.DB.CZMST_TISKARNADataTable tiskarna = adapter.GetDataByTiskarna(printerID);
            ////Log.Write("");

            //if (tiskarna.Rows.Count >= 1)
            //{
            //    //Log.Write("printerName = ((MST_Print_Server_DB_Layer ...");
            //    string printerName = ((MST_Print_Server_DB_Layer.DB.CZMST_TISKARNARow)tiskarna.Rows[0]).NAME.Trim();
            //    //Log.Write("pd = new PrintByDriver(report, printerName);");
            //PrintByDriver pd = new PrintByDriver(report, printerName, sopnumber);
            PrintByDriver pd = new PrintByDriver(report, printerParams, uniquePrintGuid); //, sopnumber);
            //    //Log.Write("pd.Copies = this.Copies;");
            pd.Copies = this.pocet_vytisku;
            //    //Log.Write("return pd.PrintReport();");
            // kontrola uzivatele na empty a podle toho se rozhodne, jak tisknout
            if (user != null && !string.IsNullOrEmpty(user.Name) && impersonateValidUser(user))
            {
                bool res = pd.PrintReport();
                //return pd.PrintReport();
                undoImpersonation();
                return res;
            }
            else
                return pd.PrintReport();
        }

        private void Init()
        {
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceHlavicka = new ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSourcePolozky = new ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSourcePaticka = new ReportDataSource();

			// \TODO: napojit datasety na rdlc
            //report.ReportPath = @"d:\_work_\_Vyvoj_Subversion_01\MST_04.01\SERVER\MST_Win_Print_Server\MST_Print_Server\PrintTemplates\A4\stitek.rdlc";
            report.ReportPath = ReportFilePath;
            report.EnableExternalImages = true;

            //report.AddTrustedCodeModuleInCurrentAppDomain("Fask.BarCode.ReportFactory, Version=1.0.0.0, Culture=neutral, PublicKeyToken=c26c6b0d10b69bb4");
            //report.ExecuteReportInCurrentAppDomain(AppDomain.CurrentDomain.Evidence);

            //PermissionSet permissions = new PermissionSet(PermissionState.None);
            //permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            //permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            //report.SetBasePermissionsForSandboxAppDomain(permissions);
            //Assembly asm = Assembly.Load("Fask.BarCode.ReportFactory, Version=1.0.0.0, Culture=neutral, PublicKeyToken=c26c6b0d10b69bb4");
            //AssemblyName asm_name = asm.GetName();
            //report.AddFullTrustModuleInSandboxAppDomain(new StrongName(new StrongNamePublicKeyBlob(asm_name.GetPublicKeyToken()), asm_name.Name, asm_name.Version));

            // fill dyn header
            // dataSource_Test_Header
            DS_Soupis dsSoupis = new DS_Soupis();

            // naplneni hlavicky daty
            var rowHlavicka = dsSoupis.Hlavicka.NewHlavickaRow();

            foreach (var item in keysHeader)
            {
                if(dsSoupis.Hlavicka.Columns.Contains(item.Key))
                    rowHlavicka[item.Key] = item.Value != null ? item.Value : string.Empty;
                //dtHeader.Columns.Add(item.Key);
                //DataRow drNew = dtHeader.NewRow();
                //drNew[item.Key] = item.Value;
                parameterInsertValue(item.Key, item.Value != null ? item.Value : string.Empty);

            }

            //Loadig logo
            bool useLOGO = true;

            try
            {
                if (useLOGO)
                {
                    //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    //string serverpath =  +  @"\\\LOGO.jpg";
                    //System.Drawing.Bitmap image1 = new System.Drawing.Bitmap(serverpath);
                    //image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    report.EnableExternalImages = true;
                    //string imagePath = new Uri("File://" + serverpath).AbsoluteUri;

                    //ReportParameter("ImageURL", )

                    ReportParameter parameter = new ReportParameter("LogoHeader", new Uri(System.IO.Path.Combine(Fask.MyPath.Path.RootPath, @"PrintTemplates\A4\LOGO.png")).AbsoluteUri);
                    report.SetParameters(parameter);
                    //rowHlavicka.Logo = ms.ToArray();
                    //ms.Close();
                }
            }
            catch
            {}

            dsSoupis.Hlavicka.AddHlavickaRow(rowHlavicka);

            reportDataSourceHlavicka.Name = dataSource_Hlavicka;
            reportDataSourceHlavicka.Value = dsSoupis.Hlavicka;
            report.DataSources.Add(reportDataSourceHlavicka);

            // naplneni polozek
            int poradi_polozky = 1;
            foreach (var rowkey in keysRows)
            {
                var rowPolozka = dsSoupis.Polozky.NewPolozkyRow();
                rowPolozka.Poradi_polozky = poradi_polozky.ToString();
                foreach (var item in rowkey)
                {
                    if (dsSoupis.Polozky.Columns.Contains(item.Key))
                        rowPolozka[item.Key] = item.Value != null ? item.Value : string.Empty;
                }

                //if (!rowPolozka.IsCountEntriesNull())
                //{
                //    ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                //    zw.Format = ZXing.BarcodeFormat.CODE_128;
                //    zw.Options.Height = 50;
                //    //var c128 = zw.Encoder.encode("1234", ZXing.BarcodeFormat.CODE_128, 100, 20);
                //    //System.Drawing.Bitmap image1 = zw.Write(c128);
                //    System.Drawing.Bitmap image1 = zw.Write(rowPolozka.CountEntries);
                //    //zw.Renderer.Render(c128, ZXing.BarcodeFormat.CODE_128,

                //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //    //Properties.Resources.Image1.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //    image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //    rowPolozka.CountEntries_Image = ms.ToArray();
                //    ms.Close();
                //}


                //if (!rowPolozka.IsBarcodeNull())
                //{
                //    ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                //    zw.Format = ZXing.BarcodeFormat.CODE_128;
                //    zw.Options.Height = 50;
                //    //var c128 = zw.Encoder.encode("1234", ZXing.BarcodeFormat.CODE_128, 100, 20);
                //    //System.Drawing.Bitmap image1 = zw.Write(c128);
                //    System.Drawing.Bitmap image1 = zw.Write(rowPolozka.Barcode);
                //    //zw.Renderer.Render(c128, ZXing.BarcodeFormat.CODE_128,

                //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //    //Properties.Resources.Image1.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //    image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //    rowPolozka.IMAGE = ms.ToArray();
                //    ms.Close();
                //}

                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Debug,"VNDITNUM pred ZXIng: '" + (rowPolozka.IsVNDITNUMNull() ? "-" : rowPolozka.VNDITNUM) + "'");

                if (!rowPolozka.IsVNDITNUMNull() && !string.IsNullOrEmpty(rowPolozka.VNDITNUM))
                {
                    ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                    zw.Format = ZXing.BarcodeFormat.CODE_128;
                    zw.Options.Height = 50;
                    //var c128 = zw.Encoder.encode("1234", ZXing.BarcodeFormat.CODE_128, 100, 20);
                    //System.Drawing.Bitmap image1 = zw.Write(c128);
                    System.Drawing.Bitmap image1 = zw.Write(rowPolozka.VNDITNUM.Trim());
                    //zw.Renderer.Render(c128, ZXing.BarcodeFormat.CODE_128,

                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    //Properties.Resources.Image1.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] arr_vnditnum = ms.ToArray();
                    rowPolozka.VNDITNUM_Code = Convert.ToBase64String(arr_vnditnum);
                    ms.Close();
                }

                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Debug, "SOPNUMBE pred ZXIng: '" + (rowPolozka.IsSOPNUMBENull() ? "-" : rowPolozka.SOPNUMBE) + "'");

                if (!rowPolozka.IsSOPNUMBENull() && !string.IsNullOrEmpty(rowPolozka.SOPNUMBE))
                {
                    ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                    zw.Format = ZXing.BarcodeFormat.CODE_128;
                    zw.Options.Height = 50;
                    zw.Options.PureBarcode = true;
                    System.Drawing.Bitmap image1 = zw.Write(rowPolozka.SOPNUMBE.Trim());
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] arr_sopnumbe = ms.ToArray();
                    rowPolozka.SOPNUMBE_Code = Convert.ToBase64String(arr_sopnumbe);
                    ms.Close();
                }

                if (!rowPolozka.IsQTYSHPPDNull())
				{
					try
					{
						rowPolozka.QTYSHPPD_Dec = decimal.Parse(rowPolozka.QTYSHPPD, System.Globalization.CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
						rowPolozka.SetQTYSHPPD_DecNull();
					}
				}



                dsSoupis.Polozky.AddPolozkyRow(rowPolozka);
                poradi_polozky++;
            }
            reportDataSourcePolozky.Name = dataSource_Polozky;
            reportDataSourcePolozky.Value = dsSoupis.Polozky;
            report.DataSources.Add(reportDataSourcePolozky);


			try
			{
            int pocetbaliku = dsSoupis.Polozky.GroupBy(x => x.NMBRPAL).Count();
            parameterInsertValue("countBaliku", pocetbaliku.ToString());
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				//neřeší se počet baliku, neobsahuje NMBRPAL
				;
			}

            //var reportParams = report.GetParameters();
            //foreach (var reportParam in reportParams)
            //{
            //    reportParam.Values.Clear();
            //    reportParam.Values.Add(string.Empty);
            //}
            if (keysParams != null)
            {
                foreach (var lParam in keysParams)
                {
                    //if (reportParams.Where( x => x.Values.Contains(
                    //var rParam = reportParams.First(x => x.Name.Equals(lParam.Key, StringComparison.InvariantCultureIgnoreCase));
                    //if (rParam != null)
                    //var rParamsExists = reportParams.Where(x => x.Name.Equals(lParam.Key, StringComparison.InvariantCultureIgnoreCase));
                    //if (rParamsExists.Count() > 0)
                    //{ // nasel ho, zmeni ...
                    //    var rParam = rParamsExists.First();
                    //    //rParam.Values.Clear();
                    //    rParam.Values.Add(lParam.Value);
                    //}
                    //else
                    //{ // nenasel ho, prida ...
                    //    report.SetParameters(new ReportParameter(lParam.Key, lParam.Value));
                    //    reportParams = report.GetParameters();
                    //}
                    parameterInsertValue(lParam);
                }
            }




            // naplneni paticky
            // dataSource_Paticka
            var rowPaticka = dsSoupis.Paticka.NewPatickaRow();
            DateTime dtNow = DateTime.Now;
            rowPaticka[dsSoupis.Paticka.Current_DateColumn.ColumnName] = dtNow;
            uniquePrintGuid = Guid.NewGuid();
            rowPaticka[dsSoupis.Paticka.UNIQUE_PRINT_IDColumn.ColumnName] = uniquePrintGuid;
            
            // naplneni parametru
            // Litra
            //ReportParameter rpParam = new ReportParameter("ParamGuidPrintID", uniquePrintGuid.ToString());
            //report.SetParameters(rpParam);
            parameterInsertValue("ParamGuidPrintID", uniquePrintGuid.ToString());
            //ReportParameter rpBeginPrint = new ReportParameter("ParamDateBeginPrint", dtNow.ToString("d.M.yyyy H:m:s"));
            //report.SetParameters(rpBeginPrint);
            parameterInsertValue("ParamDateBeginPrint", dtNow.ToString("d.M.yyyy H:m:s"));
            //ReportParameter rpStrDesc = new ReportParameter("Param_str_desc", rowHlavicka.Isstr_descNull() ? string.Empty : rowHlavicka.str_desc.Trim());
            //report.SetParameters(rpStrDesc);
            parameterInsertValue("Param_str_desc", rowHlavicka.Isstr_descNull() ? string.Empty : rowHlavicka.str_desc.Trim());
            //ReportParameter rpStrCarcode = new ReportParameter("Param_str_carcode", rowHlavicka.Isstr_carcodeNull() ? string.Empty : rowHlavicka.str_carcode.Trim());
            //report.SetParameters(rpStrCarcode);
            parameterInsertValue("Param_str_carcode", rowHlavicka.Isstr_carcodeNull() ? string.Empty : rowHlavicka.str_carcode.Trim());

            parameterInsertValue("Param_str_carcode", rowHlavicka.Isstr_carcodeNull() ? string.Empty : rowHlavicka.str_carcode.Trim());


            parameterInsertValue("Param_CountEntries", rowHlavicka.IsCountEntriesNull() ? string.Empty : rowHlavicka.CountEntries.ToString());

	        parameterInsertValue("Param_FIRSTNAME", rowHlavicka.IsFIRSTNAMENull() ? string.Empty : rowHlavicka.FIRSTNAME.ToString());
	        parameterInsertValue("Param_SECONDNAME", rowHlavicka.IsSECONDNAMENull() ? string.Empty : rowHlavicka.SECONDNAME.ToString());
	        parameterInsertValue("Param_LOGIN", rowHlavicka.IsLOGINNull() ? string.Empty : rowHlavicka.LOGIN.ToString());


            // Steinex
            foreach (var item in keysFooter)
            {
                if(dsSoupis.Paticka.Columns.Contains(item.Key))
                    rowPaticka[item.Key] = item.Value != null ? item.Value : string.Empty;
                parameterInsertValue(item.Key, item.Value != null ? item.Value : string.Empty);
            }
            //ReportParameter rpStrGuidID = new ReportParameter("Param_GUID_ID", rowHlavicka.IsIDNull() ? string.Empty : rowHlavicka.ID.ToString());
            //report.SetParameters(rpStrGuidID);


            //TaD 19.4.2018 CountEntries do Hlavicky
            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Debug, "SOPNUMBE pred ZXIng: '" + (rowHlavicka.IsCountEntriesNull() ? "-" : rowHlavicka.CountEntries) + "'");
            if (!rowHlavicka.IsCountEntriesNull() && !string.IsNullOrEmpty(rowHlavicka.CountEntries))
            {
                ZXing.BarcodeWriter zw = new ZXing.BarcodeWriter();
                zw.Format = ZXing.BarcodeFormat.CODE_128;
                zw.Options.Height = 50;
                zw.Options.PureBarcode = true;
                //var c128 = zw.Encoder.encode("1234", ZXing.BarcodeFormat.CODE_128, 100, 20);
                //System.Drawing.Bitmap image1 = zw.Write(c128);
                System.Drawing.Bitmap image1 = zw.Write(rowHlavicka.CountEntries.ToString());
                //zw.Renderer.Render(c128, ZXing.BarcodeFormat.CODE_128,

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //Properties.Resources.Image1.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                image1.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] imgReportBarcode = ms.ToArray();
                ms.Close();

                parameterInsertValue("Param_CountEntries_IMG", Convert.ToBase64String(imgReportBarcode));
            }
            else
            {
                parameterInsertValue("Param_CountEntries_IMG", rowHlavicka.IsIDNull() ? string.Empty : rowHlavicka.ID.ToString());
            }



            dsSoupis.Paticka.AddPatickaRow(rowPaticka);
            reportDataSourcePaticka.Name = dataSource_Paticka;
            reportDataSourcePaticka.Value = dsSoupis.Paticka;
            report.DataSources.Add(reportDataSourcePaticka);
        }

        private bool parameterInsertValue(string key, string val)
        {
            try
            {
                report.SetParameters(new ReportParameter(key, val));
                return true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        private bool parameterInsertValue(KeyValuePair<string, string> lParam)
        {
            return this.parameterInsertValue(lParam.Key, lParam.Value);
        }

        private bool impersonateValidUser(User us)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(us.Name, us.DomainName, us.Password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        private void undoImpersonation()
        {
            impersonationContext.Undo();
        }
    }
}
