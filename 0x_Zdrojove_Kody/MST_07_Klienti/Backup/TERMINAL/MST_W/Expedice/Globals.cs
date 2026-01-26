using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace Fask.MST_W.Expedice
{
    public class Globals
    {
        private static string _SkladID = string.Empty;
        /// <summary>
        /// ID skladu, se kterym se bude pracovat.
        /// </summary>
        public static string SkladID
        {
            get { return _SkladID; }
            set { _SkladID = value; }
        }

        private static bool _povolitParsovaniCK = false;
        /// <summary>
        /// Povoleni parsovani caroveho kodu polozek.
        /// </summary>
        public static bool PovolitParsovaniCK
        {
            get { return _povolitParsovaniCK; }
            set { _povolitParsovaniCK = value; }
        }

        private static bool _povolitPredvyplneniMnozstvi = false;
        /// <summary>
        /// Povoleni predvyplneni mnozstvi.
        /// </summary>
        public static bool PovolitPredvyplneniMnozstvi
        {
            get { return _povolitPredvyplneniMnozstvi; }
            set { _povolitPredvyplneniMnozstvi = value; }
        }

        private static bool _zobrazitZadaniSarzePouzeJednou = false;
        /// <summary>
        /// Povoleni zobrazeni dialogu zadani sarze pouze u prvni zadane polozky (nasledne se bude prebirat)
        /// </summary>
        public static bool ZobrazitZadaniSarzePouzeJednou
        {
            get { return _zobrazitZadaniSarzePouzeJednou; }
            set { _zobrazitZadaniSarzePouzeJednou = value; }
        }

        private static bool _zobrazitDialogZadaniMnozstviParsovanehoKodu = true;
        /// <summary>
        /// Povoleni zobrazeni dialogu zadani sarze pouze u prvni zadane polozky (nasledne se bude prebirat)
        /// </summary>
        public static bool ZobrazitDialogZadaniMnozstviParsovanehoKodu
        {
            get { return _zobrazitDialogZadaniMnozstviParsovanehoKodu; }
            set { _zobrazitDialogZadaniMnozstviParsovanehoKodu = value; }
        }

        private static bool _povolitTiskPalet = false;
        /// <summary>
        /// Povoleni tisku paletovych listku pri baleni.
        /// </summary>
        public static bool PovolitTiskPalet
        {
            get { return _povolitTiskPalet; }
            set { _povolitTiskPalet = value; }
        }

        private static bool _povolitTiskSoupisu = false;
        /// <summary>
        /// Povoleni tisku soupisu pri expedici.
        /// </summary>
        public static bool PovolitTiskSoupisu
        {
            get { return _povolitTiskSoupisu; }
            set { _povolitTiskSoupisu = value; }
        }

        #region TaD Dialogy Expedice
        /// <summary>
        /// zobrazovat dialog o opusteni modulu.
        /// </summary>
        private static bool _Expedice_DialogOpusteniModulu = true;
        public static bool Expedice_DialogOpusteniModulu
        {
            get { return _Expedice_DialogOpusteniModulu; }
            set { _Expedice_DialogOpusteniModulu = value; }
        }

        #endregion

        private static bool _ShowButtonExpedice_Expedice = true;
        public static bool ShowButtonExpedice_Expedice
        {
            get { return _ShowButtonExpedice_Expedice; }
            set { _ShowButtonExpedice_Expedice = value; }
        }

        private static bool _ShowButtonExpedice_Baleni = true;
        public static bool ShowButtonExpedice_Baleni
        {
            get { return _ShowButtonExpedice_Baleni; }
            set { _ShowButtonExpedice_Baleni = value; }
        }

        public static bool Load(string filename)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filename);

                _SkladID = MST_Global.LoadElement(xmldoc, "/Config/Modules/Expedice/SkladID", _SkladID);
                _povolitParsovaniCK = Convert.ToBoolean(MST_Global.LoadElement(xmldoc, "/Config/Modules/Expedice/PovolitParsovaniCK", _povolitParsovaniCK.ToString()));
                _povolitPredvyplneniMnozstvi = Convert.ToBoolean(MST_Global.LoadElement(xmldoc, "/Config/Modules/Expedice/PovolitPredvyplneniMnozstvi", _povolitPredvyplneniMnozstvi.ToString()));
                _zobrazitZadaniSarzePouzeJednou = Convert.ToBoolean(MST_Global.LoadElement(xmldoc, "/Config/Modules/Expedice/ZobrazitZadaniSarzePouzeJednou", _zobrazitZadaniSarzePouzeJednou.ToString()));
                _zobrazitDialogZadaniMnozstviParsovanehoKodu = Convert.ToBoolean(MST_Global.LoadElement(xmldoc, "/Config/Modules/Expedice/ZobrazitDialogZadaniMnozstviParsovanehoKodu", _zobrazitDialogZadaniMnozstviParsovanehoKodu.ToString()));
                _povolitTiskPalet = Convert.ToBoolean(MST_Global.LoadElement(xmldoc, "/Config/Modules/Expedice/PovolitTiskPalet", _povolitTiskPalet.ToString()));
                _povolitTiskSoupisu = Convert.ToBoolean(MST_Global.LoadElement(xmldoc, "/Config/Modules/Expedice/PovolitTiskSoupisu", _povolitTiskSoupisu.ToString()));
                _ShowButtonExpedice_Baleni = Convert.ToBoolean(MST_Global.LoadElement(xmldoc, "/Config/Modules/Expedice/ShowButtonExpedice_Baleni", _ShowButtonExpedice_Baleni .ToString()));
                _ShowButtonExpedice_Expedice = Convert.ToBoolean(MST_Global.LoadElement(xmldoc, "/Config/Modules/Expedice/ShowButtonExpedice_Expedice", _ShowButtonExpedice_Expedice.ToString()));


                #region TaD Dialogy Expedice
                _Expedice_DialogOpusteniModulu = Convert.ToBoolean(MST_Global.LoadElement(xmldoc, "/Config/Modules/Expedice/Expedice_DialogOpusteniModulu", _Expedice_DialogOpusteniModulu.ToString()));
                #endregion

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Save(string filename)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filename);

                MST_Global.SaveElement(xmldoc, "/Config/Modules/Expedice/SkladID", _SkladID);
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Expedice/PovolitParsovaniCK", _povolitParsovaniCK.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Expedice/PovolitPredvyplneniMnozstvi", _povolitPredvyplneniMnozstvi.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Expedice/ZobrazitZadaniSarzePouzeJednou", _zobrazitZadaniSarzePouzeJednou.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Expedice/ZobrazitDialogZadaniMnozstviParsovanehoKodu", _zobrazitDialogZadaniMnozstviParsovanehoKodu.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Expedice/PovolitTiskPalet", _povolitTiskPalet.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Expedice/PovolitTiskSoupisu", _povolitTiskSoupisu.ToString());

                MST_Global.SaveElement(xmldoc, "/Config/Modules/Expedice/ShowButtonExpedice_Baleni", _ShowButtonExpedice_Baleni.ToString());
                MST_Global.SaveElement(xmldoc, "/Config/Modules/Expedice/ShowButtonExpedice_Expedice", _ShowButtonExpedice_Expedice.ToString());


                #region TaD Dialogy Expedice

                MST_Global.SaveElement(xmldoc, "/Config/Modules/Expedice/Expedice_DialogOpusteniModulu", _Expedice_DialogOpusteniModulu.ToString());

                #endregion


                xmldoc.Save(filename);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
