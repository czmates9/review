using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Fask.Emailing;
using FASK.SledovaniVyroby.ErrorLog;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.DataSets;
using System.Drawing;
using Fask.Logging;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Modbus.Classes
{
    public sealed class NotificationMail
    {
        public static Bitmap bitmapstavodhlaseni = new Bitmap(100, 100);
        public static Bitmap bitmapstavodhlaseni_vyroba = new Bitmap(100, 100);
        public static string filenamestavodhlaseni = string.Empty;

        private NotificationMail()
        {
        }

        //Metoda pro odeslani emailu.
        public static void SendEmail(string message)
        {
            //Thread t = new Thread(
            //    (ThreadStart)delegate
            //    {
                    try
                    {
                        //Nacteni konfigurace z konfiguracniho souboru
                        Email.LoadConfiguration();
                        //Vlozeni Tela zpravy
                        Email.Body = message;
                        //Vytvoreni zpravy
                        Email.CreateEmailMessage();
                        //Prida obrazek do emailu ...
                        Email.AddAttachment(bitmapstavodhlaseni, "StavOdhlaseni.jpg");
                        Email.AddAttachment(bitmapstavodhlaseni_vyroba, "StavOdhlaseni_Vyroba.jpg");
                        Email.AddAttachment(filenamestavodhlaseni);
                        //Odeslani
                        Email.SendEmailMessageAsynch();
                    }
                    catch (Exception ethread)
                    {
                        //Log.WriteException(ethread);
                        //Log.Write(message, "Email message (not sended)");
                ExceptionHandler2.Handle(ethread);
                ExceptionHandler2.Handle(message + "Email message (not sended)", "Log_Vyroba_Agro_Modbus_2", "txt");
            }
            //    }
            //);
            //t.Name = "Email Vyroba Agro Send";
            //t.Start();
        }

        public static void SendEmailOdhlaseniSmeny(string context, string smenaid, Data vyrobadatahistorymemory)
        {
            if (AgroConfig.config.Agro[0].KonecSmenyEmailSend)
            {
                string message = "Odhlášení směny" + (string.IsNullOrEmpty(context) ? string.Empty : " (" + context + ")");
                message += "\n   Linka: " + Logging.LogConfig.MachineID.Trim();
                message += "\n   Směna: " + smenaid;
                message += "\n Výrobků: " + vyrobadatahistorymemory.FASK_Events.Count;
                message += "\n   Pytlů: " + vyrobadatahistorymemory.FASK_Events.Sum(x => (x.qty + x.qtyReal));
                message += "\n   Palet: " + vyrobadatahistorymemory.FASK_Events.Sum(x => x.pocetPalet);
                message += "\n Rozpis výroby:";
                foreach (var item in vyrobadatahistorymemory.FASK_Events)
                {
                    //message += String.Format("\n - čk: {0}; P: {1}; N: {2}; C:{3}; M:{4}", item.barcodeReaded.Trim(), item.qty, item.qtyReal, item.qty + item.qtyReal,item.material);
                    message += String.Format("\n - čk: {0}; Pytlů: {1}; Palet: {2}; M:{3}", item.barcodeReaded.Trim(), item.qty + item.qtyReal, item.pocetPalet, item.material);
                }

                //Log.WriteSmenaLog(message);
                ExceptionHandler2.Handle(message, "Log_Vyroba_Agro_Modbus_2", "txt");

                SendEmail(message);
            }
        }
    }
}
