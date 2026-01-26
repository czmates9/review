
/*
 *  Postup psani emailu:
 *  1.Nacteni konfigurace z konfiguracniho souboru - LoadConfiguration().
 *  2.Pripadne zmeny ci doplneni (nactenych) dat - SenderEmailAdress, IsHtml, ...
 *  3.Vytvoreni zpravy z aktualnich dat - CreateEmailMessage().
 *  4.Pridani priloh - AddAttachment().
 *  5.Odeslani emailu - SendEmailMessage(). 
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Fask.Logging
{
    internal static class Email
    {
        //Promenne
        private static NetworkCredential cred = null;
        private static SmtpClient smtp = null;
        private static MailMessage msg = null;

        private static string _configFileName = "EmailConfig.xml";
        /// <summary>
        /// Jmeno konfiguracniho souboru
        /// </summary>
        public static string ConfigFileName { get { return _configFileName; } set { _configFileName = value; } }

        private static string _smtpServer = string.Empty;
        /// <summary>
        /// Adresa SMTP serveru.
        /// </summary>
        public static string SmtpServer
        {
            get
            {
                if (_smtpServer != string.Empty) return _smtpServer;
                else throw new ApplicationException("Adresa SMTP serveru nebyla zadána (retezec je prazdny).");
            }
            set
            {
                if (value != string.Empty) _smtpServer = value;
                else throw new ApplicationException("Adresa SMTP serveru nebyla zadána (retezec je prazdny).");
            }
        }

        private static int _port = 0;
        /// <summary>
        /// Port pro posilani emailu.
        /// </summary>
        public static int Port
        {
            get
            {
                if (_port > 0) return _port;
                else throw new ApplicationException("Port nebyl zadan, nebo byl zadan chybne.");
            }
            set
            {
                if (value > 0) _port = value;
                else throw new ApplicationException("Port nebyl zadan, nebo byl zadan chybne.");
            }
        }

        private static string _senderEmail = string.Empty;
        /// <summary>
        /// Adresa odesilatele emailu.
        /// </summary>
        public static string SenderEmail
        {
            get
            {
                if (_senderEmail != string.Empty) return _senderEmail;
                else throw new ApplicationException("Adresa odesilatele nebyla zadána (retezec je prazdny).");
            }
            set
            {
                if (value != string.Empty) _senderEmail = value;
                else throw new ApplicationException("Adresa odesilatele nebyla zadána (retezec je prazdny).");
            }
        }

        private static string _senderName = string.Empty;
        /// <summary>
        /// Adresa odesilatele emailu.
        /// </summary>
        public static string SenderName
        {
            get
            {
                if (_senderName != string.Empty) return _senderName;
                else throw new ApplicationException("Jmeno odesilatele nebylo zadáno (retezec je prazdny).");
            }
            set
            {
                if (value != string.Empty) _senderName = value;
                else throw new ApplicationException("Jmeno odesilatele nebylo zadáno (retezec je prazdny).");
            }
        }

        private static string _senderLogin = string.Empty;
        /// <summary>
        /// Prihlasovaci jmeno odesilatele emailu.
        /// </summary>
        public static string SenderLogin
        {
            get
            {
                if (_senderLogin != string.Empty) return _senderLogin;
                else throw new ApplicationException("Login odesilatele nebyl zadán (retezec je prazdny).");
            }
            set
            {
                if (value != string.Empty) _senderLogin = value;
                else throw new ApplicationException("Login odesilatele nebyl zadán (retezec je prazdny).");
            }
        }

        private static string _senderPassword = string.Empty;
        /// <summary>
        /// Heslo odesilatele emailu.
        /// </summary>
        public static string SenderPassword
        {
            get
            {
                if (_senderPassword != string.Empty) return _senderPassword;
                else throw new ApplicationException("Heslo odesilatele nebylo zadáno (retezec je prazdny).");
            }
            set
            {
                if (value != string.Empty) _senderPassword = value;
                else throw new ApplicationException("Heslo odesilatele nebylo zadáno (retezec je prazdny).");
            }
        }

        /// <summary>
        /// Seznam emailovych adres prijemce.
        /// </summary>
        public static List<string> RecipientsAdress = new List<string>();

        /// <summary>
        /// Jedna adresa odesilatele.
        /// </summary>
        private static string _recipientAdress
        {
            set
            {   //upraveno
                if (value != string.Empty && !RecipientsAdress.Contains(value)) RecipientsAdress.Add(value);
                //else throw new ApplicationException("Adresa prijemce nebyla zadána (retezec je prazdny).");
            }
        }

        private static string _head = string.Empty;
        /// <summary>
        /// Hlavicka zpravy.
        /// </summary>
        public static string Head
        {
            get
            {
                if (_head != string.Empty) return _head;
                else throw new ApplicationException("Neni vyplnena hlavicka zpravy.");
            }
            set
            {
                if (value != string.Empty) _head = value;
                else throw new ApplicationException("Neni vyplnena hlavicka zpravy.");
            }
        }

        private static string _body = string.Empty;
        /// <summary>
        /// Hlavicka zpravy.
        /// </summary>
        public static string Body
        {
            get
            {
                if (_body != string.Empty) return _body;
                else throw new ApplicationException("Neni vyplneno telo zpravy.");
            }
            set
            {
                if (value != string.Empty) _body = value;
                else throw new ApplicationException("Neni vyplneno telo zpravy.");
            }
        }

        private static bool _isBodyHtml = false;
        /// <summary>
        /// Informace zda je zprava napsana v jazyku HTML.
        /// </summary>
        public static bool IsBodyHtml { get { return _isBodyHtml; } set { _isBodyHtml = value; } }

        private static bool _attachErrorFile = false;
        /// <summary>
        /// Povoleni pripojeni chyboveho souboru
        /// </summary>
        public static bool AttachErrorFile { get { return _attachErrorFile; } set { _attachErrorFile = value; } }


        /// <summary>
        /// Nacteni obsahu konkretniho elementu s xml souboru.
        /// </summary>
        /// <param name="XmlDoc">Konfiguracni XML dokument.</param>
        /// <param name="NodeName">Jmeno uzlu v XML dokumentu.</param>
        /// <returns>Obsah uzlu.</returns>
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

        /// <summary>
        /// Nacteni obsahu konkretniho elementu s xml souboru.
        /// </summary>
        /// <param name="XmlDoc">Konfiguracni XML dokument.</param>
        /// <param name="xmlnode">Uzel v XML dokumentu.</param>
        /// <returns>Obsah uzlu.</returns>
        private static string LoadElement(XmlNode xmlnode)
        {
            string nodeValue = string.Empty;

            //Konkretni uzel
            XmlElement configNode = xmlnode as XmlElement;

            if (configNode != null)
            {
                //Vlozeni obsahu uzlu.
                try { nodeValue = configNode.InnerText; }
                catch { }
            }

            return nodeValue;
        }


        /// <summary>
        /// Nacteni aktualni konfigurace s xml souboru.
        /// </summary>
        public static void LoadConfiguration()
        {
            LoadConfiguration(_configFileName);
        }

        /// <summary>
        /// Nacteni aktualni konfigurace s xml souboru.
        /// </summary>
        /// <param name="FileName">Jmeno konfiguracniho souboru.</param>
        public static void LoadConfiguration(string FileName)
        {
            //Zjisteni cesty ke konfiguracnimu souboru.
            string configFilePath = (new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), FileName))).LocalPath;

            //Vytvoreni xml dokumentu
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(configFilePath);

            //Nacteni konfigurace + kontrola + ulozeni do promennych predanych jako parametry - tvorba adres a dalsiho
            SmtpServer = LoadElement(xmldoc, "/Emailing/SmtpServer");
            Port = Convert.ToInt32(LoadElement(xmldoc, "/Emailing/Port"));
            SenderEmail = LoadElement(xmldoc, "/Emailing/SenderEmailAdress");
            SenderName = LoadElement(xmldoc, "/Emailing/SenderName");
            SenderLogin = LoadElement(xmldoc, "/Emailing/SenderLogin");
            SenderPassword = LoadElement(xmldoc, "/Emailing/SenderPassword");

            IsBodyHtml = (LoadElement(xmldoc, "/Emailing/UseHTML").ToUpper() == "TRUE");
            _head = LoadElement(xmldoc, "/Emailing/Head");
            _body = LoadElement(xmldoc, "/Emailing/Body");
            AttachErrorFile = (LoadElement(xmldoc, "/Emailing/AttachErrorFile").ToUpper() == "TRUE");
            EnableSSL = (LoadElement(xmldoc, "/Emailing/EnableSSL").ToUpper() == "TRUE");
            //Jestlize hodnota je zadana
            string str=string.Empty;
            if ((str = LoadElement(xmldoc, "/Emailing/Timeout")) != string.Empty) Timeout = int.Parse(str);

            //Zjisteni poctu prijemcu
            XmlElement el = xmldoc.SelectSingleNode("/Emailing/Recipients") as XmlElement;
            //Jestlize pocet atributu neni prave 1
            //if (el.Attributes.Count != 1) throw new ApplicationException("Chybny konfiguracni soubor - neni zadan pocet prijemcu.");

            //Pridany 
            if (el != null)
            {
                XmlNodeList nodelist = xmldoc.SelectNodes("/Emailing/Recipients/RecipientEmailAdress");
                if (nodelist.Count > 0)
                {
                    foreach (XmlNode rcpnode in nodelist)
                    {
                        //_recipientAdress = LoadElement(rcpnode);
                        _recipientAdress = (rcpnode as XmlElement).InnerText.Trim();
                    }
                }
            }
        }

        /// <summary>
        /// Vytvoreni zpravy - inicializace vseho nutneho pro odeslani.
        /// </summary>
        public static void CreateEmailMessage()
        {
            //Zprava
            msg = new MailMessage();
            //Odesilatel
            msg.From = new MailAddress(SenderEmail, SenderName);
            //Prijemci
            foreach (string RecepientAdress in RecipientsAdress) msg.To.Add(new MailAddress(RecepientAdress));
            //Text
            msg.Subject = Head;
            msg.Body = Body;
            msg.IsBodyHtml = IsBodyHtml;

            //Server
            smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //Adresa
            smtp.Host = SmtpServer;
            smtp.Port = Port;
            //Nastaveni SSL a Timeoutu
            smtp.EnableSsl = EnableSSL;
            if (_timeout > 0) smtp.Timeout = Timeout;

            //Nastaveni pristupu
            cred = new NetworkCredential();
            cred.UserName = SenderLogin;
            cred.Password = SenderPassword;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = cred;
        }

        /// <summary>
        /// Odeslani zpravy podle nastavenych parametru
        /// </summary>
        public static void SendEmailMessage()
        {
            bool PotlacitCertifikaty = true;

            if(PotlacitCertifikaty) 
            {
                ServicePointManager.ServerCertificateValidationCallback =
            delegate(
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                    ) { return true; }; 
            }

            //Odeslani
            smtp.Send(msg);
        }

        /// <summary>
        /// Odeslani zpravy podle zadanych parametru.
        /// </summary>
        /// <param name="Head">Hlavicka.</param>
        /// <param name="Body">Telo.</param>
        public static void CreateEmailMessage(string Head, string Body)
        {
            //Nastaveni novych vlastnosti
            Email.Head = Head;
            Email.Body = Body;

            //Vytvoreni zbytku
            CreateEmailMessage();
        }

        /// <summary>
        /// Pridani prilohy ke zprave.
        /// </summary>
        /// <param name="FileName">Nazev souboru.</param>
        public static void AddAttachment(string FileName)
        {
            //jestlize je vyzadano pripojovani chybovych souboru
            if (AttachErrorFile)
            {
                //Vytvoreni prilohy
                Attachment data = new Attachment(FileName);
                //Pridani ke zprave
                msg.Attachments.Add(data);
            }
            else msg.Body += "\n\nNebylo mozne pridat prilohu " + FileName + ", protoze jejich pridavani neni povoleno v konfiguracnim souboru.";
        }

        //JK - pridano 6.9. 
        private static int _timeout = 0;
        public static int Timeout 
        {
            get {
                if (_timeout <= 0) throw new ApplicationException("Nastavena "+_timeout+" hodnota neni povolena.");
                else return _timeout;
            }
            set
            {
                if (value <= 0) throw new ApplicationException("Nastavovana hodnota "+value+" neni povolena.");
                else _timeout = value;
            }
        }
        private static bool _enableSSL = false;
        public static bool EnableSSL { get { return _enableSSL; } set { _enableSSL = value; } }
    }
}
