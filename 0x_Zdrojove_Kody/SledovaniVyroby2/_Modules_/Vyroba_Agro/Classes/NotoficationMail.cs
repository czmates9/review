using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Fask.Emailing;
using FASK.SledovaniVyroby.ErrorLog;
using FASK.SledovaniVyroby.Module.Vyroba_Agro.DataSets;
using System.Drawing;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes
{
    public sealed class NotificationMail
    {
        public static Bitmap bitmapstavodhlaseni = new Bitmap(100, 100);
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
                        Email.AddAttachment(filenamestavodhlaseni);
                        //Odeslani
                        Email.SendEmailMessageAsynch();
                    }
                    catch (Exception ethread)
                    {
                        Log.WriteException(ethread);
                        Log.Write(message, "Email message (not sended)");
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
                message += "\n  Linka: " + Logging.LogConfig.MachineID.Trim();
                message += "\n  Směna: " + smenaid;
                message += "\n Celkem: " + vyrobadatahistorymemory.FASK_Events.Count + " výrobků";
                message += "\n Rozpis výroby:";
                foreach (var item in vyrobadatahistorymemory.FASK_Events)
                {
                    message += String.Format("\n - čk: {0}; P: {1}; N: {2}; C:{3}; M:{4}", item.barcodeReaded.Trim(), item.qty, item.qtyReal, item.qty + item.qtyReal,item.material);
                }

                Log.WriteSmenaLog(message);

                SendEmail(message);
            }
        }
    }
}
