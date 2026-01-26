using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Fask.AdvancedButtonsPanel;
using Fask.Logging;
using System.Windows.Forms;

namespace Konzola.Extensions
{
    public static class ButtonsPanelExtension
    {
        public static void LoadConfiguration(this ButtonsPanel bt, string fileName)
        {

            try
            {
                Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].DataGridVazatNaUzivatele && FASK.Logins.Uzivatel.Instance.UserID != null)
                    fileName = FASK.Logins.Uzivatel.Instance.UserID.Trim() + fileName;

                fileName = Path.Combine(MySystem.MyPath.ConfigButtonDirectory, fileName + ".xml");

                if (!File.Exists(fileName))
                    return;

                using (var streamWriter = new StreamReader(fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(item[]), new XmlRootAttribute() { ElementName = "items" });
                    bt.VisibleBTN = ((item[])serializer.Deserialize(streamWriter)).ToDictionary(i => i.id, i => i.value);

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Konzola.Extensions.ButtonPanelExtension", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }

        public static void SaveConfiguration(this ButtonsPanel bt, string fileName)
        {
            try
            {
                Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].DataGridVazatNaUzivatele && FASK.Logins.Uzivatel.Instance.UserID != null)
                    fileName = FASK.Logins.Uzivatel.Instance.UserID.Trim() + fileName;

                fileName = Path.Combine(MySystem.MyPath.ConfigButtonDirectory, fileName + ".xml");

                using (var streamWriter = new StreamWriter(fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(item[]), new XmlRootAttribute() { ElementName = "items" });
                    serializer.Serialize(streamWriter,
                  bt.VisibleBTN.Select(kv => new item() { id = kv.Key, value = kv.Value }).ToArray());
                }

            }
            catch (System.UnauthorizedAccessException ex)
            {
                string msg = string.Format("Uživatel přihlášen od Windows, nemá dostatnečná práva zápisu uživatelských nastavení." +
                        Environment.NewLine +
                        Environment.NewLine +
                        "Pro vyřešení tohoto problému, kontaktuj vašeho správce IT! " +
                        Environment.NewLine +
                        Environment.NewLine +
                        "Chyba nastala u souboru: '{0}'", fileName);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Konzola.Extensions.ButtonPanelExtension", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }

    public class item
    {
        [XmlAttribute]
        public string id;
        [XmlAttribute]
        public bool value;
    }
}
