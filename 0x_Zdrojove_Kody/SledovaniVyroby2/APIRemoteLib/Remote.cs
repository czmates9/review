using APIRemoteLib.Classes;
using Fask.Logging;
using Fask.WEBAPI;
using Fask.WEBAPI.API_BusinessObjects;
using ICommDatabase;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace APIRemoteLib
{
    public class Remote : ICommDatabase.ISQLDatabase
    {

        #region Parameters

        private string _configFileName = "APIRemoteLib.xml";
        private string Autorizace_DoAPI = @"";
        private string Adresa = @"";
        private string AliasDB = @"";
        private bool isHTTPS = false;
        private int API_TimeOut = 5000;

        #endregion

        #region LoadConfig

        public void LoadConfiguration()
        {
            LoadConfiguration(_configFileName);
        }

        public void LoadConfiguration(string FileName)
        {
            string configFilePath = (new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), FileName))).LocalPath;

            //Vytvoreni xml dokumentu
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(configFilePath);

            Autorizace_DoAPI = LoadElement(xmldoc, "/WEB_API/Autorizace_DoAPI");
            Adresa = LoadElement(xmldoc, "/WEB_API/Adresa");
            AliasDB = LoadElement(xmldoc, "/WEB_API/AliasDB");
            isHTTPS = bool.Parse(LoadElement(xmldoc, "/WEB_API/isHTTPS"));
            API_TimeOut = int.Parse(LoadElement(xmldoc, "/WEB_API/API_TimeOut"));
        }

        private static string LoadElement(XmlDocument XmlDoc, string NodeName)
        {
            string nodeValue = string.Empty;

            //Konkretni uzel
            XmlElement configNode = XmlDoc.SelectSingleNode(NodeName) as XmlElement;

            if (configNode != null)
            {
                //Vlozeni obsahu uzlu.
                try { nodeValue = configNode.InnerText; }
                catch { }
            }

            return nodeValue;
        }

        #endregion

        #region Komunikace s API
        public enum REST_Type
        {
            GET,
            POST,
            PUT,
            DELETE,
            unknow
        }

        private bool Communicate(REST_Type _Type, out IRestResponse response, string Param, string JSON_Message = "", int TID = 666, string volajici = "SledovaniVyroby")
        {
            try
            {
                if (_Type == REST_Type.POST || _Type == REST_Type.PUT)
                {
                    if (string.IsNullOrEmpty(JSON_Message))
                        throw new Exception("Obsah JSON je prázdy, ale pro " + _Type.ToString() + " musí byt vyplnení!");
                }

                Fask.RestSharp.API.Communication_4_7 com;


                com = new Fask.RestSharp.API.Communication_4_7(
                  Adresa,
                  Autorizace_DoAPI,
                  AliasDB,
                  isHTTPS,
                  API_TimeOut,
                  TID.ToString(),
                  volajici
                  );


                response = null;

                switch (_Type)
                {
                    case REST_Type.GET:
                        response = com.REST_GET(Param);
                        break;
                    case REST_Type.POST:
                        response = com.REST_POST(Param, JSON_Message);
                        break;
                    case REST_Type.PUT:
                        response = com.REST_PUT(Param, JSON_Message);
                        break;
                    case REST_Type.DELETE:
                        response = com.REST_DELETE(Param);
                        break;
                    case REST_Type.unknow:
                    default:
                        ExceptionHandler2.Handle(LogLevel.Error, "Communication -- Neznamy typ requestu...");
                        break;
                }

                return true;
                //return response.StatusCode == HttpStatusCode.OK;

            }
            catch (System.Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }
        #endregion

        #region FASK_Events

        public DSVyroba.FASK_EventsDataTable GetFASK_Events_ByStatus(int MachineID, int Status, List<string> descFilter)
        {
            try
            {
                // API komunikace
                IRestResponse restResponse;
                string param = "SV_GetFASK_Events_ByStatus";
                string JSON = "";

                DSVyroba.FASK_EventsDataTable prom = new DSVyroba.FASK_EventsDataTable();

               Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr = new Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events();
                filtr.machineid = MachineID;
                filtr.status = Status;

                string popisky = string.Join(";", descFilter);
                filtr.separator = popisky;


                JSON = Classes.JSON_Class.Serialize_JSON(filtr);

                if (!Communicate(REST_Type.POST, out restResponse, param, JSON, MachineID))
                {
                    //Log.Write("Komunikace s IS AGRO se nezdarila");
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {

                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        prom = Newtonsoft.Json.JsonConvert.DeserializeObject<DSVyroba.FASK_EventsDataTable>(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");

                    return prom;
                }

                return null;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return null;

            }
        }

        public FASK_Events_row SledovaniVyroby_data(int MachineID, Filtr_FASK_Events filtr_Events)
        {
            // API komunikace
            //FASK_Events o = new FASK_Events();
            IRestResponse restResponse;
            string param = "SledovaniVyroby_data";
            FASK_Events_row o = null;
            string JSON = "";

            JSON = JSON_Class.Serialize_JSON(filtr_Events);

            if (!Communicate(REST_Type.POST, out restResponse, param, JSON, MachineID,"nakladka"))
            {
                //throw new Exception("Komunikace s IS AGRO se nezdařila");
                ExceptionHandler2.Handle(LogLevel.Error, "Komunikace s IS AGRO se nezdarila--" + restResponse.Content);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                o = Newtonsoft.Json.JsonConvert.DeserializeObject<Fask.WEBAPI.API_BusinessObjects.FASK_Events_row>(restResponse.Content);
            }
            if (o == null)
            {
                //Log.Write("chybi zaznam!! GetSSCC_zaznam");
                var log_hlaska = string.Format("chybi zaznam!! GetSSCC_zaznam");
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
            }

            return o;
        }

        public bool SV_ZmenaStatusu(int MachineID, int status)
        {
            bool remoteData = false;
            // API komunikace
            IRestResponse restResponse;
            string param = "SV_ZmenaStatusu";
            string JSON = "";
            JSON = JSON_Class.Serialize_JSON(status);


            if (!Communicate(REST_Type.POST, out restResponse, param, JSON, MachineID))
            {
                ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {


                if (!string.IsNullOrEmpty(restResponse.Content))
                {
                    remoteData = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);
                }
                else
                    throw new Exception("Data nenalezena");


                return remoteData;
            }

            return remoteData;

        }

        public bool FaskEvents_Update(int MachineID, Guid? G, int StatusNew)
        {
            try
            {
                bool stav = false;

                // API komunikace

                IRestResponse restResponse;
                string param = "SledovaniVyroby_updateStatus";
                string JSON = "";



                if (G.HasValue)
                {
                    Fask.WEBAPI.API_BusinessObjects.Filtr_UpdateStatus fs = new Fask.WEBAPI.API_BusinessObjects.Filtr_UpdateStatus()
                    {
                        G = G.Value,
                        Status = StatusNew
                    };

                    JSON = JSON_Class.Serialize_JSON(fs);

                    if (!Communicate(REST_Type.POST, out restResponse, param, JSON, MachineID))
                    {
                        //Log.Write("Komunikace s IS AGRO se nezdarila");
                        ExceptionHandler2.Handle(LogLevel.Error, "Komunikace s IS AGRO se nezdarila--" + restResponse.Content);
                    }

                    if (restResponse.StatusCode == HttpStatusCode.OK)
                    {
                        stav = true;
                    }
                }
                else
                    throw new Exception("GUID jako primarni klic nebyl zadan");

                return stav;

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;

            }

        }

        public bool FaskEvents_WriteToRow(int MachineID, Fask.WEBAPI.API_BusinessObjects.FASK_Events_row o, int StatusNew, string desc)
        {
            try
            {
                bool stav = false;

                IRestResponse restResponse;
                string param = "SledovaniVyroby_zapisStatus";
                string JSON = "";

                if (o != null)
                {
                    o.dateeve = DateTime.Now;
                    o.description = desc;
                    o.status = StatusNew;
                    o.faskGUID = Guid.NewGuid();
                    o.isProcessed = null;
                   // o.WEIGHT = 0;
                }

                JSON = JSON_Class.Serialize_JSON(o);

                if (!Communicate(REST_Type.POST, out restResponse, param, JSON, MachineID))
                {
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace s IS AGRO se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    stav = true;
                }

                return stav;

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        #region Archivace

        public int DeaktivaceFE(int MachineID, int Status, ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable _dataKArchivaci, int BocediID)
        {
            string param = string.Empty;

            if (Status == 21 || Status == 22 || Status == 23 || Status == 24)
            {
                param = "SledovaniVyroby_archivaceZaznamu_deaktivace_vykladka";
               // Deaktivace_ERR_Logs(MachineID, Status, BocediID);
            }
            else
            {
                param = "SledovaniVyroby_archivaceZaznamu_deaktivace_nakladka";
            }

            int pocet = 0;
            IRestResponse restResponse;
            string JSON = "";

            JSON = JSON_Class.Serialize_JSON(_dataKArchivaci);

            if (!Communicate(REST_Type.POST, out restResponse, param, JSON, MachineID))
            {
                throw new Exception("Komunikace s IS AGRO se nezdařila");
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                pocet = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(restResponse.Content);
            }

            return pocet;
        }

        private bool Deaktivace_ERR_Logs(int MachineID, int Status, int BocediID)
        {
            try
            {
                bool vysledek = false;
                int bocediID = -1;

                if (Status == 21)
                {
                    bocediID = BocediID;
                }
                if (Status == 22)
                {
                    bocediID = BocediID;
                }
                else
                {
                    return false;
                }



                #region komunikace se SERVER

                IRestResponse restResponse;
                string param = "SV_Logs_deaktivace";
                string JSON = "";
                JSON = JSON_Class.Serialize_JSON(bocediID);

                if (!Communicate(REST_Type.POST, out restResponse, param, JSON, MachineID))
                {
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace s IS AGRO se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    vysledek = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);

                    if (vysledek)
                    {
                        ExceptionHandler2.Handle("LOGS - deaktivace data OK", "Log_LV", "txt");
                        return true;
                    }
                    else
                    {
                        ExceptionHandler2.Handle("LOGS - deaktivace data ERROR", "Log_LV", "txt");
                        return false;
                    }
                }
                else
                {
                    ExceptionHandler2.Handle("LOGS - deaktivace data ERROR", "Log_LV", "txt");
                    return false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex.Message, "Log_LV", "txt");
                return false;
            }

        }

        public ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable LoadFE(int MachineID,  Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events)
        {

            ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable D0 = new ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable();
            IRestResponse restResponse;
            string param = "SledovaniVyroby_archivaceZaznamu_data";

            string JSON = "";

            JSON = JSON_Class.Serialize_JSON(filtr_Events);

            if (!Communicate(REST_Type.POST, out restResponse, param, JSON, MachineID))
            {
                throw new Exception("Komunikace s IS AGRO se nezdařila");
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                D0 = Newtonsoft.Json.JsonConvert.DeserializeObject<ICommDatabase.DSVyroba.FASK_Events_archivaceDataTable>(restResponse.Content);
            }

            return D0;
        }


        #endregion


        #endregion

        #region FASK_Logins

        public bool Fill_FASK_Logins(DSVyroba.FASK_LoginsDataTable dataTable, int MachineID, bool ClearBeforeFill = true)
        {
            try
            {
                // API komunikace
                IRestResponse restResponse;
                string param = "LoginsByPermission";
                string JSON = "";

                Fask.WEBAPI.API_BusinessObjects.FASK_Login_Permissions per = new Fask.WEBAPI.API_BusinessObjects.FASK_Login_Permissions()
                {
                    Permissions = "V_"
                };


                JSON = Classes.JSON_Class.Serialize_JSON(per);

                if (!Communicate(REST_Type.POST, out restResponse, param, JSON, MachineID))
                {
                    //Log.Write("Komunikace s IS AGRO se nezdarila");
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    IEnumerable<Fask.WEBAPI.API_BusinessObjects.FASK_Login> remoteData = null;
                    
                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        remoteData = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Fask.WEBAPI.API_BusinessObjects.FASK_Login>>(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");

                    dataTable.Clear();

                    foreach (Fask.WEBAPI.API_BusinessObjects.FASK_Login item in remoteData)
                    {
                        dataTable.AddFASK_LoginsRow(
                            item.USERID,
                            item.firstname,
                            item.surname,
                            item.psswd,
                            item.CREATED.HasValue ? item.CREATED.Value : DateTime.MinValue,
                            item.VALIDFROM.HasValue ? item.VALIDFROM.Value : DateTime.MinValue,
                            item.VALIDTO.HasValue ? item.VALIDTO.Value : DateTime.MinValue,
                            string.Empty
                            );
                    }

                    dataTable.AcceptChanges();

                    //dataTable = restResponse.Content;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;

            }
        }

        #endregion

        #region FASK_Machine

        public bool Fill_FASK_Machine(DSVyroba.FASK_MachinesDataTable dataTable, int MachineID, bool ClearBeforeFill = true)
        {
            try
            {
                if ((ClearBeforeFill == true))
                {
                    dataTable.Clear();
                }

                // API komunikace
                IRestResponse restResponse;
                string param = "SV_Fill_FASK_Machine";
                string JSON = "";

             


                //JSON = Classes.JSON_Class.Serialize_JSON(per);

                if (!Communicate(REST_Type.GET, out restResponse, param, JSON, MachineID))
                {
                    //Log.Write("Komunikace s IS AGRO se nezdarila");
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    DSVyroba.FASK_MachinesDataTable remoteData = null;

                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        remoteData = Newtonsoft.Json.JsonConvert.DeserializeObject<DSVyroba.FASK_MachinesDataTable>(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");

                    dataTable.Clear();

                    //dataTable = remoteData;

                    foreach (DSVyroba.FASK_MachinesRow item in remoteData)
                    {
                        dataTable.AddFASK_MachinesRow(
                            item.id,
                            item.machinetype,
                            item.name,
                            item.description,
                            item.koeficient
                            );
                    }

                    dataTable.AcceptChanges();

                    //dataTable = restResponse.Content;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;

            }
        }

        #endregion

        #region FASK_MachineType

        public bool Fill_FASK_MachineType(DSVyroba.FASK_MachineTypeDataTable dataTable, int MachineID, bool ClearBeforeFill = true)
        {
            try
            {
                if ((ClearBeforeFill == true))
                {
                    dataTable.Clear();
                }

                // API komunikace
                IRestResponse restResponse;
                string param = "SV_Fill_FASK_MachineType";
                string JSON = "";


                if (!Communicate(REST_Type.GET, out restResponse, param, JSON, MachineID))
                {
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    DSVyroba.FASK_MachineTypeDataTable remoteData = null;

                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        remoteData = Newtonsoft.Json.JsonConvert.DeserializeObject<DSVyroba.FASK_MachineTypeDataTable>(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");

                    dataTable.Clear();

                    dataTable = remoteData;

                    dataTable.AcceptChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;

            }
        }

        #endregion

        #region CZPRO_VPP

        public DSVyroba.CZPRO_VPPDataTable getVyroba_CZPRO_VPP(int MachineID)
        {
            try
            {
                DSVyroba.CZPRO_VPPDataTable dataTable = new DSVyroba.CZPRO_VPPDataTable();

                // API komunikace
                IRestResponse restResponse;
                string param = "SV_getVyroba_CZPRO_VPP";
                string JSON = "";


                if (!Communicate(REST_Type.GET, out restResponse, param, JSON, MachineID))
                {
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    DSVyroba.CZPRO_VPPDataTable remoteData = null;

                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        remoteData = Newtonsoft.Json.JsonConvert.DeserializeObject<DSVyroba.CZPRO_VPPDataTable>(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");

                    dataTable.Clear();

                    dataTable = remoteData;

                    dataTable.AcceptChanges();
                    return dataTable;
                }

                return null;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return null;

            }
        }


        #endregion

        #region CZPRO_VPH

        public DSVyroba.CZPRO_VPHDataTable getVyroba_CZPRO_VPH(int MachineID)
        {
            try
            {
                DSVyroba.CZPRO_VPHDataTable dataTable = new DSVyroba.CZPRO_VPHDataTable();

                // API komunikace
                IRestResponse restResponse;
                string param = "SV_getVyroba_CZPRO_VPH";
                string JSON = "";


                if (!Communicate(REST_Type.GET, out restResponse, param, JSON, MachineID))
                {
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    DSVyroba.CZPRO_VPHDataTable remoteData = null;

                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        remoteData = Newtonsoft.Json.JsonConvert.DeserializeObject<DSVyroba.CZPRO_VPHDataTable>(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");

                    dataTable.Clear();

                    dataTable = remoteData;

                    dataTable.AcceptChanges();
                    return dataTable;
                }

                return null;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return null;

            }
        }

        #endregion

        #region FASK_Events

        public int? FASK_Events_CountGUID(Guid Guid, int MachineID)
        {
            try
            {
                int? remoteData = null;

                // API komunikace
                IRestResponse restResponse;
                string param = "SV_FASK_Events_CountGUID";
                string JSON = "";
                JSON = Classes.JSON_Class.Serialize_JSON(Guid);


                if (!Communicate(REST_Type.POST, out restResponse, param, JSON, MachineID))
                {
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    

                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        remoteData = Newtonsoft.Json.JsonConvert.DeserializeObject<int?>(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");

                    
                    return remoteData;
                }

                return null;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return null;

            }
        }

        public int FASK_EventsInsert
            (
            string loginid,
            string machineid,
            System.DateTime dateeve,
            decimal qty,
            decimal qtyReal,
            string description,
            string barcodeReaded,
            string barcodeSended,
            string zakazka,
            string popis,
            System.Guid faskGUID,
            string reportType,
            string IDO,
            string scan1,
            string scan2,
            string scan3,
            string sensor,
            string material,
            string VPH,
            int? VPPol,
            string EAN_IS,
            string IS_ID,
            int? status,
            string NMBRPAL,
            global::System.Guid? productionGuid,
            decimal QTYPACK,
            string PackType,
            decimal? WEIGHT,
            byte BarcodeT,
            string REZ_1,
            string REZ_2,
            string REZ_3,
            string REZ_4,
            string REZ_5
    )
        {

            try
            {
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row dataObject = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();

                #region inicializace parametrů

                dataObject.loginid = loginid;
                dataObject.machineid = machineid;
                dataObject.dateeve = dateeve;
                dataObject.qty = qty;
                dataObject.qtyReal = qtyReal;
                dataObject.description = description;
                dataObject.barcodeReaded = barcodeReaded;
                dataObject.barcodeSended = barcodeSended;
                dataObject.zakazka = zakazka;
                dataObject.popis = popis;
                dataObject.faskGUID = faskGUID;
                dataObject.reportType = reportType;
                dataObject.IDO = IDO;
                dataObject.scan1 = scan1;
                dataObject.scan2 = scan2;
                dataObject.scan3 = scan3;
                dataObject.sensor = sensor;
                dataObject.material = material;
                dataObject.VPH = VPH;
                dataObject.VPPol = VPPol;
                dataObject.EAN_IS = EAN_IS;
                dataObject.IS_ID = IS_ID;
                dataObject.status = status;
                dataObject.NMBRPAL = NMBRPAL;
                dataObject.productionGuid = productionGuid;
                dataObject.QTYPACK = QTYPACK;
                dataObject.PackType = PackType;
                dataObject.WEIGHT = WEIGHT;

                dataObject.BarcodeT = BarcodeT;
                dataObject.REZ_1 = REZ_1;
                dataObject.REZ_2 = REZ_2;
                dataObject.REZ_3 = REZ_3;
                dataObject.REZ_4 = REZ_4;
                dataObject.REZ_5 = REZ_5;

                #endregion

                // API komunikace
                IRestResponse restResponse;
                string param = "SV_FASK_EventsInsert";
                string JSON = "";
                JSON = Classes.JSON_Class.Serialize_JSON(dataObject);


                if (!Communicate(REST_Type.POST, out restResponse, param, JSON, int.Parse(machineid)))
                {
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<int>(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");
                }
                return 1;
                //if (string.IsNullOrEmpty(restResponse.Content))
                //    throw new Exception("Data nenalezena");

                //return Newtonsoft.Json.JsonConvert.DeserializeObject<int>(restResponse.Content);

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return 0;

            }
        }



        #endregion

        #region FASK_UserEvents

        public int? FASK_UserEvents_CountGUID(Guid Guid, int MachineID)
        {
            try
            {
                int? remoteData = null;

                // API komunikace
                IRestResponse restResponse;
                string param = "SV_FASK_UserEvents_CountGUID";
                string JSON = "";
                JSON = Classes.JSON_Class.Serialize_JSON(Guid);


                if (!Communicate(REST_Type.POST, out restResponse, param, JSON, MachineID))
                {
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {


                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        remoteData = Newtonsoft.Json.JsonConvert.DeserializeObject<int?>(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");


                    return remoteData;
                }

                return null;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return null;

            }
        }

        public bool FASK_UserEventsInsert(
            string loginid,
            string machineid,
            System.DateTime dateeve,
            string statusid,
            System.Guid faskGUID,
            string rez_1,
            string rez_2
            )
        {

            try
            {
                Fask.WEBAPI.API_BusinessObjects.FASK_UserEvents dataObject = new Fask.WEBAPI.API_BusinessObjects.FASK_UserEvents();

                #region inicializace parametrů

                dataObject.loginid = loginid;
                dataObject.machineid = machineid;
                dataObject.dateeve = dateeve;
                dataObject.statusid = statusid;
                dataObject.faskGUID = faskGUID;
                dataObject.rez_1 = rez_1;
                dataObject.rez_2 = rez_2;
                #endregion

                // API komunikace
                IRestResponse restResponse;
                string param = "SV_FASK_UserEventsInsert";
                string JSON = "";
                JSON = Classes.JSON_Class.Serialize_JSON(dataObject);


                if (!Communicate(REST_Type.POST, out restResponse, param, JSON, int.Parse(machineid)))
                {
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {


                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");


                }

                return false;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;

            }
        }

        #endregion

        #region FASK_Operations

        public bool Fill_FASK_Operations(DSVyroba.FASK_OperationsDataTable dataTable, int MachineID, bool ClearBeforeFill)
        {
            try
            {
                if ((ClearBeforeFill == true))
                {
                    dataTable.Clear();
                }

                // API komunikace
                IRestResponse restResponse;
                string param = "SV_Fill_FASK_Operations";
                string JSON = "";




                //JSON = Classes.JSON_Class.Serialize_JSON(per);

                if (!Communicate(REST_Type.GET, out restResponse, param, JSON, MachineID))
                {
                    //Log.Write("Komunikace s IS AGRO se nezdarila");
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    DSVyroba.FASK_OperationsDataTable remoteData = null;

                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        remoteData = Newtonsoft.Json.JsonConvert.DeserializeObject<DSVyroba.FASK_OperationsDataTable>(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");

                    dataTable.Clear();

                    dataTable = remoteData;

                    //foreach (FASK_Login item in remoteData)
                    //{
                    //    dataTable.AddFASK_LoginsRow(
                    //        item.USERID,
                    //        item.firstname,
                    //        item.surname,
                    //        item.psswd,
                    //        item.CREATED.HasValue ? item.CREATED.Value : DateTime.MinValue,
                    //        item.VALIDFROM.HasValue ? item.VALIDFROM.Value : DateTime.MinValue,
                    //        item.VALIDTO.HasValue ? item.VALIDTO.Value : DateTime.MinValue,
                    //        string.Empty
                    //        );
                    //}

                    dataTable.AcceptChanges();

                    //dataTable = restResponse.Content;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;

            }
        }


        #endregion

        #region FASK_Operations_Next

        public bool Fill_FASK_Operations_Next(DSVyroba.FASK_Operations_NextDataTable dataTable, int MachineID, bool ClearBeforeFill)
        {
            try
            {
                if ((ClearBeforeFill == true))
                {
                    dataTable.Clear();
                }

                // API komunikace
                IRestResponse restResponse;
                string param = "SV_Fill_FASK_Operations_Next";
                string JSON = "";




                //JSON = Classes.JSON_Class.Serialize_JSON(per);

                if (!Communicate(REST_Type.GET, out restResponse, param, JSON, MachineID))
                {
                    //Log.Write("Komunikace s IS AGRO se nezdarila");
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    DSVyroba.FASK_Operations_NextDataTable remoteData = null;

                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        remoteData = Newtonsoft.Json.JsonConvert.DeserializeObject<DSVyroba.FASK_Operations_NextDataTable>(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");

                    dataTable.Clear();

                    dataTable = remoteData;

                    //foreach (FASK_Login item in remoteData)
                    //{
                    //    dataTable.AddFASK_LoginsRow(
                    //        item.USERID,
                    //        item.firstname,
                    //        item.surname,
                    //        item.psswd,
                    //        item.CREATED.HasValue ? item.CREATED.Value : DateTime.MinValue,
                    //        item.VALIDFROM.HasValue ? item.VALIDFROM.Value : DateTime.MinValue,
                    //        item.VALIDTO.HasValue ? item.VALIDTO.Value : DateTime.MinValue,
                    //        string.Empty
                    //        );
                    //}

                    dataTable.AcceptChanges();

                    //dataTable = restResponse.Content;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;

            }
        }


        #endregion

        #region Pomocne metody z AGRO modulu


        public string ReturnSarze(int MachineID, string smenaID, string userID, string linkaID)
        {
            try
            {
                ExceptionHandler2.Handle("ReturnSarze--start", "Sarze", "txt");

                // API komunikace
                IRestResponse restResponse;
                string param = string.Format("ReturnSarze/{0}/{1}/{2}", smenaID, userID, linkaID);
  

                if (!Communicate(REST_Type.GET, out restResponse, param, TID: MachineID))
                {
                    //Log.Write("Komunikace s IS AGRO se nezdarila");
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    ExceptionHandler2.Handle("ReturnSarze--StatusCode-OK", "Sarze", "txt");

                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        ExceptionHandler2.Handle("ReturnSarze--Content:"+ restResponse.Content, "Sarze", "txt");
                        return restResponse.Content;
                    }
                    else
                        throw new Exception("Data nenalezena");
                }
                else
                {
                    throw new Exception("Chyba komunikace:" + restResponse.StatusCode.ToString());
                }

                

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return null;

            }
        }

        public bool ReturnID(int MachineID, string inID)
        {
            try
            {
                // API komunikace
                IRestResponse restResponse;
                string param = string.Format("ReturnID/{0}", inID);


                if (!Communicate(REST_Type.GET, out restResponse, param, TID: MachineID))
                {
                    //Log.Write("Komunikace s IS AGRO se nezdarila");
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        return bool.Parse(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");
                }
                else
                {
                    throw new Exception("Chyba komunikace:" + restResponse.StatusCode.ToString());
                }

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;

            }
        }

        public bool ReturnHeslo(int MachineID, string inHESLO, string inID)
        {
            try
            {
                // API komunikace
                IRestResponse restResponse;
                string param = string.Format("ReturnHeslo/{0}/{1}", inHESLO, inID);


                if (!Communicate(REST_Type.GET, out restResponse, param, TID: MachineID))
                {
                    //Log.Write("Komunikace s IS AGRO se nezdarila");
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    if (!string.IsNullOrEmpty(restResponse.Content))
                    {
                        return bool.Parse(restResponse.Content);
                    }
                    else
                        throw new Exception("Data nenalezena");
                }
                else
                {
                    throw new Exception("Chyba komunikace:" + restResponse.StatusCode.ToString());
                }

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;

            }
        }


        #endregion

        #region FASK_EventsErr

        public void SV_Logs_insert(int MachineID, DSVyroba.FASK_EventsErrDataTable dt)
        {
            #region transformace do BO

            Fask.WEBAPI.API_BusinessObjects.BO_Logs bo = new Fask.WEBAPI.API_BusinessObjects.BO_Logs();
            BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Logs, Fask.WEBAPI.API_BusinessObjects.BO_Logs_row, DSVyroba.FASK_EventsErrDataTable, DSVyroba.FASK_EventsErrRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Logs, Fask.WEBAPI.API_BusinessObjects.BO_Logs_row, DSVyroba.FASK_EventsErrDataTable, DSVyroba.FASK_EventsErrRow>();
            bo = y.GetBOFromDT(dt);

            #endregion

            IRestResponse restResponse;
            string param = "SV_Logs_insert";
            string JSON = "";


            JSON = Classes.JSON_Class.Serialize_JSON(bo);

            if (!Communicate(REST_Type.POST, out restResponse, param, JSON, MachineID))
            {
                ExceptionHandler2.Handle(LogLevel.Error, "Komunikace s IS AGRO se nezdarila--" + restResponse.Content);
            }

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                bool neco = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);
                if (neco)
                {
                    ExceptionHandler2.Handle("LOGS - odeslana data OK", "Log_LV", "txt");
                }
                else
                {
                    ExceptionHandler2.Handle("LOGS - odeslana data ERROR", "Log_LV", "txt");
                }
            }
            else
            {
                Exception neco_2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Exception>(restResponse.Content);
                ExceptionHandler2.Handle("LOGS - odeslana data ERROR", "Log_LV", "txt");
            }
        }

 
        #endregion
    }
}
