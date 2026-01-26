using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Xml.Linq;
using System.IO;
using System.Globalization;
using Fask.SQL.XML.Classes;
using System.Data;

namespace Fask.SQL.XML
{
    public static class MST_Pohoda
    {
        public const string _unknown = "_unknown";
        public const string _export_faktura_vydana = "_export_fa_v";
        public const string _export_prevodka = "_export_prevodka";
        public const string _export_vydejka = "_export_vydejka";
        public const string _export_prodejka = "_export_prodejka";
        public const string _export_objednavka_prijata = "_export_obj_p";
        public const string _export_objednavka_vydana = "_export_obj_v";
        public const string _export_zasoby = "_export_zasoby";
        public const string _export_adresy = "_export_adresy";
        public const string _export_sklady = "_export_sklady";
        public const string _import_objednavka_prijata = "_import_obj_p";
        public const string _import_objednavka_vydana = "_import_obj_v";
        public const string _import_vydejka = "_import_vydejka";
        public const string _import_faktura = "_import_faktura";
        public const string _import_prijemka = "_import_prijemka";
        public const string _import_prodejka = "_import_prodejka";
        public const string _import_prevodka = "_import_prevodka";
        public const string _export_PrjateObjednavky = "_export_PrjateObjednavky";
        public const string _import_faktura_vydana = "_import_fa_v";

        private static Datasets.Zbozi Zbozi_BPDS = new Datasets.Zbozi();
		private static Datasets.Prodej Prodej_BPDS = new Datasets.Prodej();

        #region Komunikace s POHODA


        public static string FilenameCompose_FullPath(string fileidentification)
        {
            var filename = DateTime.Now.ToString("yyMMdd") + "_" + Guid.NewGuid() + fileidentification;

            return Path.Combine(Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory, filename);
        }

        public static void CheckExistResponseFile(string PathToResponseFile)
        {
            try
            {
                if (!File.Exists(PathToResponseFile))
                {
                    var msg = "Response soubor '" + Path.GetFileName(PathToResponseFile) + "' neexistuje!";
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Fatal, msg);
                    throw new Exception(msg);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        //    /// <summary>
        //    /// Spusti pohodu a ceka na odpoved ...
        //    /// </summary>
        //    /// <param name="pathToINIFile"></param>
        //    /// <returns></returns>
        //    public static string RunPohodaExeFile(string pathToINIFile)
        //    {
        //        try
        //        {
        //            Fask.MyPath.Network.Drives.MapDrive(
        //Globals_V1.Konfigurace.ComunicatorDriveMapping[0].Letter,
        //Globals_V1.Konfigurace.ComunicatorDriveMapping[0].UNCPath,
        //Globals_V1.Konfigurace.ComunicatorDriveMapping[0].Domain,
        //Globals_V1.Konfigurace.ComunicatorDriveMapping[0].User,
        //Globals_V1.Konfigurace.ComunicatorDriveMapping[0].Password
        //);


        //            Process p = new Process();
        //            p.StartInfo.FileName = Globals_V1.Konfigurace.PohodaInfo[0].PathToPOHODAexe;
        //            p.StartInfo.Arguments = "/XML " + "\"" + Globals_V1.Konfigurace.PohodaInfo[0].Login_pohoda + "\" " + "\"" + Globals_V1.Konfigurace.PohodaInfo[0].Password_pohoda + "\" " + "\"" + pathToINIFile + "\"";
        //            p.StartInfo.UseShellExecute = Globals_V1.Konfigurace.PohodaInfo[0].Process_UseShellExecute;

        //Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Pohoda spustena s temito argumenty: " + p.StartInfo.Arguments + " a z tohoto umisteni: " + p.StartInfo.FileName);

        //            p.Start();
        //            p.WaitForExit();
        //            try
        //            {
        //	Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info , "Exit Code: " + p.ExitCode);
        //            }
        //            catch (Exception ex)
        //            {
        //	Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "RunPohodaExeFile", ex);
        //            }
        //            finally
        //            {
        //                //Fask.MyPath.Network.Drives.UnMapDrive(Properties.Settings.Default.Communicator_Drive_Mapping_Letter);
        //            }

        //            return "OK";
        //        }
        //        catch (Exception ex)
        //        {
        //Fask.Logging.ExceptionHandler2.Handle(ex);
        //            //return ex.ToString();
        //            throw ex;
        //        }
        //    }

        ///// <summary>
        ///// Nahradí text v souboru
        ///// </summary>
        ///// <param name="filePath">Cesta k textovemu souboru.</param>
        ///// <param name="searchText">Text ktery se hledá.</param>
        ///// <param name="replaceText">Text kterí to nahradí.</param>
        //static public void ReplaceInFile(string filePath, string searchText, string replaceText)
        //{
        //    String strFile = File.ReadAllText(filePath);

        //    strFile = strFile.Replace(searchText, replaceText);

        //    File.WriteAllText(filePath, strFile);
        //}

        #endregion

        /// <summary>
        /// Vytvoreni requestu pro export adres
        /// </summary>
        /// <param name="file">cesta k souboru</param>
        /// <param name="note"> poznamka</param>
        /// <param name="cislaDokladu">číslo dokladu</param>
        /// <returns></returns>
        public static bool CreateAdresyXML(string filename, string note, List<string> cislaDokladu)
        {
            try
            {

                List<object> filterList = new List<object>();
                List<object> mainfilterList = new List<object>();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace lAdb = "http://www.stormware.cz/schema/version_2/list_addBook.xsd";
                XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";



                object[] filter = null;
                object mainFilter = null;

                filter = filterList.ToArray();

                mainFilter = new XElement(ftr + "filter", filter);

                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "lAdb", "http://www.stormware.cz/schema/version_2/list_addBook.xsd"),
                    new XAttribute(XNamespace.Xmlns + "ftr", "http://www.stormware.cz/schema/version_2/filter.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "Za001"),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_S),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", note),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "a55"),
                            new XAttribute("version", "2.0"),

                                new XElement(lAdb + "listAddressBookRequest",
                                    new XAttribute("version", "2.0"),
                                    new XAttribute("addressBookVersion", "2.0"),

                                        new XElement(lAdb + "requestAddressBook")

                                        )
                                )
                    );


                root.Save(filename);

                return true;

            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.XML.MST_Pohoda", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }

        public static bool CreateZasobyXML(string filename, string note, List<string> cislaDokladu)
        {
            try
            {
                List<object> filterList = new List<object>();
                List<object> mainfilterList = new List<object>();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace stk = "http://www.stormware.cz/schema/version_2/stock.xsd";
                XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
                XNamespace lStk = "http://www.stormware.cz/schema/version_2/list_stock.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                object[] filter = null;
                object mainFilter = null;

                filter = filterList.ToArray();

                mainFilter = new XElement(ftr + "filter", filter);

                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "stk", "http://www.stormware.cz/schema/version_2/stock.xsd"),
                    new XAttribute(XNamespace.Xmlns + "ftr", "http://www.stormware.cz/schema/version_2/filter.xsd"),
                    new XAttribute(XNamespace.Xmlns + "lStk", "http://www.stormware.cz/schema/version_2/list_stock.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "Za001"),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_S),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", note),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "a55"),
                            new XAttribute("version", "2.0"),

                                new XElement(lStk + "listStockRequest",
                                    new XAttribute("version", "2.0"),
                                    new XAttribute("stockVersion", "2.0"),

                                        new XElement(lStk + "requestStock",
                                            mainFilter
                                               )

                                        )
                                )
                    );


                root.Save(filename);

                return true;

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        /// <summary>
        /// Nacte data z vydanych objednavek 
        /// </summary>
        /// <param name="filename">cesta ke xml souboru</param>
        public static string LoadZasobyXML(string filename)
        {

            XML.MST_Pohoda.CheckExistResponseFile(filename);

            string actualStrName = string.Empty;
            string previousStrName = string.Empty;
            bool InVydejkItem = false;
            int price = 0;
            string actualSonnumber = string.Empty;

			Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter FASK_ZASOBY_TA = new Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter();
			FASK_ZASOBY_TA.Connection =new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            
			FASK_ZASOBY_TA.DeleteQuery();

            Datasets.Zbozi.FASK_ZASOBYRow zboziRow = null;

            XmlTextReader reader = new XmlTextReader(filename);

            try
            {
				int ITEMNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["ITEMNMBR"].MaxLength;
				int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["ITEMDESC"].MaxLength;
				int VNDITNUM_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["VNDITNUM"].MaxLength;
				int CZ_CarKod_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["CZ_CarKod"].MaxLength;
				int LOCNCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["LOCNCODE"].MaxLength;
				int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["MJ"].MaxLength;

				zboziRow = Zbozi_BPDS.FASK_ZASOBY.NewFASK_ZASOBYRow();

                NastavPromenne(zboziRow);

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:

                            previousStrName = actualStrName;
                            actualStrName = reader.Name;

                            if (actualStrName == "stk:stockHeader")
                            {
                                InVydejkItem = true;
                                if (zboziRow.ITEMNMBR != string.Empty)
                                { //jsme u dalsiho zbozi na prijemce...ulozime aktulani zbozi 

									FASK_ZASOBY_TA.Insert(
										zboziRow.ITEMNMBR, 
										zboziRow.ITEMDESC, 
										zboziRow.ITEMCODE , 
										zboziRow.VNDITNUM, 
										zboziRow.CZ_CarKod, 
										zboziRow.LOCNCODE, 
										zboziRow.SKL_ID,
                                        zboziRow.QTY, 
										zboziRow.QTYPACK, 
										zboziRow.MJ, 
										zboziRow.DMJ, 
										zboziRow.TAXRATE, 
										zboziRow.PRICE0, 
										zboziRow.PRICE1, 
										zboziRow.PRICE2, 
										zboziRow.PRICE3, 
										zboziRow.PRICE4, 
										zboziRow.PRICE5,
                                        zboziRow.CZ_SerNum_Track, 
										zboziRow.CZ_SerNum_Delka, 
										zboziRow.CZ_Rez1_Track, 
										zboziRow.CZ_Rez2_Track, 
										zboziRow.CZ_Rez3_Track, 
										zboziRow.CZ_Rez4_Track, 
										zboziRow.REZ1,
										zboziRow.REZ2, 
										zboziRow.REZ3,
										zboziRow.REZ4, 
										zboziRow.ODB_ID,
										zboziRow.mena_ID,
										zboziRow.SERLTNUM,
										zboziRow.WEIGHT,
										zboziRow.TIMEFROM,
										zboziRow.TIMETO,
										zboziRow.LSTMod,
										zboziRow.loginid,
                                        zboziRow.CZ_Expirace_Track,
                                        zboziRow.EXPIRACE
                                        );


									zboziRow = Zbozi_BPDS.FASK_ZASOBY.NewFASK_ZASOBYRow();

                                    NastavPromenne(zboziRow);
                                    InVydejkItem = true;
                                    price = 0;
                                }

                            }

                            break;

                        case XmlNodeType.Text:

                            if (actualStrName == "stk:id")
                            {
                                if (InVydejkItem)
                                {
                                    if(zboziRow.ITEMNMBR == string.Empty)
                                        zboziRow.ITEMNMBR = reader.Value;

									if (zboziRow.ITEMNMBR.Length > ITEMNMBR_MaxLength)
                                    {
										zboziRow.ITEMNMBR = zboziRow.ITEMNMBR.Remove(ITEMNMBR_MaxLength);
                                    }
                                }
                            }
                            if (actualStrName == "typ:id")
                            {
                                if(zboziRow.LOCNCODE == string.Empty)
                                    zboziRow.LOCNCODE = reader.Value;

                                if (zboziRow.LOCNCODE.Length > LOCNCODE_MaxLength)
                                {
									zboziRow.LOCNCODE = zboziRow.LOCNCODE.Remove(LOCNCODE_MaxLength);
                                }
                            }
                            else if (actualStrName == "typ:numberRequested")
                            {
                                actualSonnumber = reader.Value;
                            }
                            else if (actualStrName == "stk:EAN" /*|| actualStrName == "stk:PLU"*/)
                            {
                                zboziRow.VNDITNUM = reader.Value;

                                if (zboziRow.VNDITNUM.Length > VNDITNUM_MaxLength)
                                {
									zboziRow.VNDITNUM = zboziRow.VNDITNUM.Remove(VNDITNUM_MaxLength);
                                }
                            }
                            else if (actualStrName == "stk:name")
                            {
                                if (InVydejkItem)
                                {
                                    zboziRow.ITEMDESC = reader.Value;

                                    if (zboziRow.ITEMDESC.Length > ITEMDESC_MaxLength)
                                    {
										zboziRow.ITEMDESC = zboziRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
                                    }
                                }
                            }
                            else if (actualStrName == "stk:code")
                            {
                                zboziRow.CZ_CarKod = reader.Value;

                                if (zboziRow.CZ_CarKod.Length > CZ_CarKod_MaxLength)
                                {
									zboziRow.CZ_CarKod = zboziRow.CZ_CarKod.Remove(CZ_CarKod_MaxLength);
                                }
                            }
                            else if (actualStrName == "stk:isSerialNumber")
                            {
                                if (Convert.ToBoolean(reader.Value))
                                    zboziRow.CZ_SerNum_Track = 1;
                            }
                            else if (actualStrName == "stk:isBatch")
                            {
                                if (Convert.ToBoolean(reader.Value))
                                    zboziRow.CZ_SerNum_Track = 2;
                            }
                            else if (actualStrName == "ord:coefficient")
                                zboziRow.QTYPACK = Convert.ToDecimal(reader.Value.Remove(reader.Value.IndexOf('.')));
                            else if (actualStrName == "stk:unit")
                            {
                                zboziRow.MJ = reader.Value;

                                if (zboziRow.MJ.Length > MJ_MaxLength)
                                {
									zboziRow.MJ = zboziRow.MJ.Remove(MJ_MaxLength);
                                }
                            }
                            else if (actualStrName == "stk:count")
                            {
                                if (zboziRow.QTY == -99999)
                                    zboziRow.QTY = Convert.ToDecimal(reader.Value.Remove(reader.Value.IndexOf('.')));
                            }
                            else if (actualStrName == "typ:price")
                            {
                                if (InVydejkItem)
                                {
                                    if (price == 0)
                                        zboziRow.PRICE0 = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                                    if (price == 1)
                                        zboziRow.PRICE1 = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                                    if (price == 2)
                                        zboziRow.PRICE2 = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                                    if (price == 3)
                                        zboziRow.PRICE3 = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                                    if (price == 4)
                                        zboziRow.PRICE4 = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                                    if (price == 5)
                                        zboziRow.PRICE5 = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);

                                    price++;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }

                return "OK";
            }
            catch (XmlException e)
            {
				Fask.Logging.ExceptionHandler2.Handle(e);
                return e.Message;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return ex.Message;
            }
            finally
            {
                if (zboziRow.ITEMNMBR != string.Empty)
                {
					FASK_ZASOBY_TA.Insert(
								zboziRow.ITEMNMBR,
								zboziRow.ITEMDESC,
								zboziRow.ITEMCODE,
								zboziRow.VNDITNUM,
								zboziRow.CZ_CarKod,
								zboziRow.LOCNCODE,
								zboziRow.SKL_ID,
								zboziRow.QTY,
								zboziRow.QTYPACK,
								zboziRow.MJ,
								zboziRow.DMJ,
								zboziRow.TAXRATE,
								zboziRow.PRICE0,
								zboziRow.PRICE1,
								zboziRow.PRICE2,
								zboziRow.PRICE3,
								zboziRow.PRICE4,
								zboziRow.PRICE5,
								zboziRow.CZ_SerNum_Track,
								zboziRow.CZ_SerNum_Delka,
								zboziRow.CZ_Rez1_Track,
								zboziRow.CZ_Rez2_Track,
								zboziRow.CZ_Rez3_Track,
								zboziRow.CZ_Rez4_Track,
								zboziRow.REZ1,
								zboziRow.REZ2,
								zboziRow.REZ3,
								zboziRow.REZ4,
								zboziRow.ODB_ID,
								zboziRow.mena_ID,
								zboziRow.SERLTNUM,
								zboziRow.WEIGHT,
								zboziRow.TIMEFROM,
								zboziRow.TIMETO,
								zboziRow.LSTMod,
								zboziRow.loginid,
                                zboziRow.CZ_Expirace_Track,
                                zboziRow.EXPIRACE
                                );

                }

                if (reader != null)
                    reader.Close();
            }
        }

        private static void NastavPromenne(Datasets.Zbozi.FASK_ZASOBYRow zboziRow)
        {
            zboziRow.ITEMDESC = string.Empty; 
            zboziRow.ITEMNMBR = string.Empty;
			zboziRow.ITEMCODE = string.Empty;
            zboziRow.VNDITNUM = string.Empty;
			zboziRow.CZ_CarKod = string.Empty;
			zboziRow.LOCNCODE = string.Empty;
			zboziRow.SKL_ID = string.Empty;
			zboziRow.QTY = -99999;
            zboziRow.QTYPACK = 0;
			zboziRow.MJ = string.Empty;
			zboziRow.DMJ = string.Empty;
            zboziRow.TAXRATE = 0;
            zboziRow.PRICE0 = 0;
            zboziRow.PRICE1 = 0;
            zboziRow.PRICE2 = 0;
            zboziRow.PRICE3 = 0;
            zboziRow.PRICE4 = 0;
            zboziRow.PRICE5 = 0;
			zboziRow.CZ_SerNum_Track = 0;
			zboziRow.CZ_SerNum_Delka = 0;
            zboziRow.CZ_Rez1_Track = 0;
            zboziRow.CZ_Rez2_Track = 0;
            zboziRow.CZ_Rez3_Track = 0;
            zboziRow.CZ_Rez4_Track = 0;
            zboziRow.REZ1 = string.Empty;
			zboziRow.REZ2 = string.Empty;
			zboziRow.REZ3 = string.Empty;
			zboziRow.REZ4 = string.Empty;

			zboziRow.ODB_ID = string.Empty;
			zboziRow.mena_ID = string.Empty;
			zboziRow.SERLTNUM = string.Empty;
			zboziRow.WEIGHT = 0;
			zboziRow.SetTIMEFROMNull();
			zboziRow.SetTIMETONull();
			zboziRow.SetLSTModNull();
			zboziRow.loginid = string.Empty;

            zboziRow.CZ_Expirace_Track = 0;
            zboziRow.SetEXPIRACENull();
        }

        /// <summary>
        /// Nacte data z vydanych objednavek 
        /// </summary>
        /// <param name="filename">cesta ke xml souboru</param>
        public static string LoadAdresyXML(string filename)
        {
            XML.MST_Pohoda.CheckExistResponseFile(filename);

            string actualStrName = string.Empty;
            string previousStrName = string.Empty;
            bool InVydejkItem = false;
            int indexTypID = 0;

            string actualSonnumber = string.Empty;

			Datasets.ProdejTableAdapters.CZMST090TableAdapter CZMST_090TableAdapter = new Datasets.ProdejTableAdapters.CZMST090TableAdapter();
            CZMST_090TableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

            CZMST_090TableAdapter.DeleteQuery();

            Datasets.Prodej.CZMST090Row AddrRow = null;

            XmlTextReader reader = new XmlTextReader(filename);

            try
            {
				AddrRow = Prodej_BPDS.CZMST090.NewCZMST090Row();
                AddrRow.odb_desc = string.Empty;
                AddrRow.odb_id = string.Empty;
                AddrRow.odb_carcode = string.Empty;
                AddrRow.odb_ico = string.Empty;

				int odb_carcode_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_carcode"].MaxLength;
				int odb_ico_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_ico"].MaxLength;
				int odb_desc_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_desc"].MaxLength;
				

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:

                            previousStrName = actualStrName;
                            actualStrName = reader.Name;

                            if (actualStrName == "adb:addressbookHeader")
                            {
                                InVydejkItem = true;
                                if (AddrRow.odb_desc != string.Empty)
                                { //jsme u dalsiho zbozi na prijemce...ulozime aktulani zbozi 

                                    //AddrRow.CountEntries = actualCountEntries;
                                    //AddrRow.SOPNUMBE = actualSonnumber;

                                    CZMST_090TableAdapter.Insert(AddrRow.odb_id, AddrRow.odb_desc, AddrRow.odb_typ, AddrRow.odb_carcode, AddrRow.odb_ico, AddrRow.mena_ID);

									AddrRow = Prodej_BPDS.CZMST090.NewCZMST090Row();
                                    AddrRow.odb_desc = string.Empty;
                                    AddrRow.odb_id = string.Empty;
                                    AddrRow.odb_carcode = string.Empty;
                                    AddrRow.odb_ico = string.Empty;

                                    InVydejkItem = true;
                                    indexTypID = 0;
                                }

                            }
                            break;

                        case XmlNodeType.Text:

                            if (actualStrName == "typ:company")
                            {
                                if (InVydejkItem)
                                {
                                    if (indexTypID == 0)
                                        AddrRow.odb_desc = reader.Value;

                                    if (AddrRow.odb_desc.Length > odb_desc_MaxLength)
                                    {
										AddrRow.odb_desc = AddrRow.odb_desc.Remove(odb_desc_MaxLength);
                                    }

                                    indexTypID++;
                                }

                                AddrRow.odb_typ = "0";
                            }
                            else if (actualStrName == "typ:numberRequested")
                            {
                                AddrRow.odb_carcode = reader.Value;

                                if (AddrRow.odb_carcode.Length > odb_carcode_MaxLength)
                                {
									AddrRow.odb_carcode = AddrRow.odb_carcode.Remove(odb_carcode_MaxLength);
                                }

                            }
                            else if (actualStrName == "typ:ico")
                            {
                                AddrRow.odb_ico = reader.Value;

                                if (AddrRow.odb_ico.Length > odb_ico_MaxLength)
                                {
									AddrRow.odb_ico = AddrRow.odb_ico.Remove(odb_ico_MaxLength);
                                }
                            }
                            else if (actualStrName == "adb:id")
                            {
                                if (previousStrName == "adb:addressbookHeader")
                                {
                                    AddrRow.odb_id = reader.Value;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }

                return "OK";
            }
            catch (XmlException e)
            {
                throw e;
                //Log.writeErrorLog(e.ToString());
                //return e.Message;
            }
            catch (Exception ex)
            {
                throw ex;
                //Log.writeErrorLog(ex.ToString());
                //return ex.Message;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public static string LoadSkladyXML(string filename)
        {

            XML.MST_Pohoda.CheckExistResponseFile(filename);

            string actualStrName = string.Empty;
            string previousStrName = string.Empty;

            string actualSonnumber = string.Empty;

			Datasets.ProdejTableAdapters.CZMST093TableAdapter CZMST_093TableAdapter = new Datasets.ProdejTableAdapters.CZMST093TableAdapter();
            CZMST_093TableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

            CZMST_093TableAdapter.DeleteQuery();

			Datasets.Prodej ProdejBPDS = new Datasets.Prodej();

            Datasets.Prodej.CZMST093Row AddrRow = null;

            XmlTextReader reader = new XmlTextReader(filename);

            try
            {
				AddrRow = ProdejBPDS.CZMST093.NewCZMST093Row();
                AddrRow.skl_carcode = string.Empty;
                AddrRow.skl_desc = string.Empty;
                AddrRow.skl_id = string.Empty;
                AddrRow.skl_typ = string.Empty;

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:

                            previousStrName = actualStrName;
                            actualStrName = reader.Name;

                            if (actualStrName == "lst:itemStorage")
                            {
                                string idStore = reader.GetAttribute("idStore");
                                if (idStore != null)
                                {
                                    AddrRow.skl_carcode = string.Empty;
                                    AddrRow.skl_desc = reader.GetAttribute("code");
                                    AddrRow.skl_id = idStore;
                                    AddrRow.skl_typ = reader.GetAttribute("id");

                                    CZMST_093TableAdapter.Insert(AddrRow.skl_id, AddrRow.skl_desc, AddrRow.skl_typ, AddrRow.skl_carcode);

									AddrRow = ProdejBPDS.CZMST093.NewCZMST093Row();
                                    AddrRow.skl_carcode = string.Empty;
                                    AddrRow.skl_desc = string.Empty;
                                    AddrRow.skl_id = string.Empty;
                                    AddrRow.skl_typ = string.Empty;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }

                return "OK";
            }
            catch (XmlException e)
            {
				Fask.Logging.ExceptionHandler2.Handle(e);
                return e.Message;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return ex.Message;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

		// \TODO : !!! Odstranit objekt Objednavka a pokusit se nahradit necim efektivnejsim, co bude logicky korektni ... ???
        public static string ZpracovaniUlozeniVsechDatDoDB(
            string filename, 
            bool save, 
            Fask.Server.Interfaces.Classes.Objednavka objednavka, 
            Fask.Server.Interfaces.Classes.User uzivatel,
            out string responsefilename
            )
        {
            //string newFileName = Globals_V1.Konfigurace.PohodaInfo[0].PathToINIFile + Guid.NewGuid().ToString() + ".ini";
            //File.Copy(Globals_V1.Konfigurace.PohodaInfo[0].PathToINIFile, newFileName);

            //ReplaceInFile(newFileName, "$source_xml$", Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + filename);
            //ReplaceInFile(newFileName, "$Catalog$", Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda );


            //string test = RunPohodaExeFile(newFileName);
            //if (test != "OK")
            //    return test;

            //File.Delete(newFileName);

            ////ReplaceInFile(Globals.PathToINIFile, Globals.PathToInputDirectory + filename, "$source_xml$");


            responsefilename = string.Empty;
            if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
            {
                throw new Exception("Komunikace s Pohodou se nezdařila");
            }

            if (filename.Contains(_export_objednavka_prijata))
            {
                return Vydej.LoadPrijataObjednavkaXML(responsefilename, save);
            }
            else if (filename.Contains(_export_objednavka_vydana))
            {
                //Presunuto do zpracovani ve volajici rutine...
                //return Prijem.LoadVydanaObjednavkaXML(Path.GetFileName(filename), save, objednavka);
                return "OK";
            }
            else if (filename.Contains(_export_zasoby))
            {
                return LoadZasobyXML(responsefilename);
            }
            else if (filename.Contains(_export_adresy))
            {
                return LoadAdresyXML(responsefilename);
            }
            else if (filename.Contains(_export_sklady))
            {
                return LoadSkladyXML(responsefilename);
            }
            else if (filename.Contains(_import_vydejka))
            { 
                //UpdateCreatorObj(uzivatel, filename);
                return "OK";
            }
            else if (filename.Contains(_import_prijemka))
            {
                UpdateCreatorSKPP(uzivatel, responsefilename);
                return "OK";
            }
            else if (filename.Contains(_import_objednavka_vydana))
            {
                UpdateCreatorObj(uzivatel, responsefilename);
                return "OK";
            }
            else if (filename.Contains(_import_objednavka_prijata))
            {
                UpdateCreatorObj(uzivatel, filename);
                return "OK";
            }
            else if (filename.Contains(_import_prevodka))
            {
                //UpdateCreatorObj(uzivatel, filename);
                return "OK";
            }
            else if (filename.Contains(_unknown))
            {
                return "OK";
            }

            return "NE";
        }

        public static void UpdateCreatorSKPP(Fask.Server.Interfaces.Classes.User uzivatel, string filename)
        {
            try
            {
                //TaD 4.8.2020 Tahle metoda nefunguje korektně, práce s requestama....

                //string pom = Path.Combine("\\Response\\", filename);
                //string filepath = Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + pom;

                //string filepath = Path.Combine(Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory, @"Response\" + filename);

                XML.Classes.Response2 response = new XML.Classes.Response2(filename);


                if (response.DocumentNumber != string.Empty)
                {
                    string acronym = string.Empty;
					acronym = Globals_V1.Konfigurace.PohodaInfo[0].Login_IDS_pohoda;

					Database.Pohoda.SKPP_UpdateCreatorByCislo(acronym, response.DocumentNumber);
                }
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
            }

        }

        public static void UpdateCreatorObj(Fask.Server.Interfaces.Classes.User uzivatel, string filename)
        {
            try
            {
                //string pom = Path.Combine("\\Response\\", filename);
                //string filepath = Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + pom;

                //string filepath = Path.Combine(Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory, @"Response\" + filename);

                XML.Classes.Response2 response = new XML.Classes.Response2(filename);


                if (response.DocumentNumber != string.Empty)
                {
                    string acronym = string.Empty;
					acronym = Globals_V1.Konfigurace.PohodaInfo[0].Login_IDS_pohoda;

					Database.Pohoda.OBJ_UpdateCreatorByCislo(acronym, response.DocumentNumber);
                }
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
            }

        }
    }
}
