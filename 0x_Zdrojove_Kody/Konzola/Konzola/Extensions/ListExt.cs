using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;

namespace Konzola.Extensions
{
    public static class ListExt
    {
        /// <summary>
        /// Extension pro ulozeni Listu do xml souboru.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="filename"></param>
        public static void WriteXML<T>(this List<T> o, string fileName)
        {
            try
            {
                Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FiltryVazatNaUzivatele && FASK.Logins.Uzivatel.Instance.UserID != null)
                    fileName = FASK.Logins.Uzivatel.Instance.UserID.Trim() + fileName;

                string filePath = Path.Combine(MySystem.MyPath.FilterDirectory, fileName + ".xml");
                using (var streamWriter = new StreamWriter(filePath))
                {
                    //var xmlSerializer = new XmlSerializer(typeof(List<T>));
                    XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(List<T>) })[0];
                    xmlSerializer.Serialize(streamWriter, o);
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

        ///// <summary>
        ///// Metoda pro nacteni pro ulozeni Listu z xml souboru.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="filename"></param>
        ///// <returns></returns>
        //public static List<T> ReadFromXML<T>(string filename)
        //{
        //    Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();

        //    if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FiltryVazatNaUzivatele && FASK.Logins.Uzivatel.Instance.UserID != null)
        //        filename = FASK.Logins.Uzivatel.Instance.UserID.Trim() + filename;

        //    string filePath = Path.Combine(MySystem.MyPath.FilterDirectory, filename + ".xml");
        //    if (!File.Exists(filePath))
        //        return new List<T>();

        //    XmlDocument xmlDoc = new XmlDocument();
        //    XPathNavigator nav = xmlDoc.CreateNavigator();
        //    using (StreamReader reader = new StreamReader(filePath))
        //    {
        //        //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
        //        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(List<T>) })[0];
        //        return (List<T>)xmlSerializer.Deserialize(reader);
        //    }
        //}


        /// <summary>
        /// Metoda pro načtení a uložení Listu z XML souboru.
        /// Obsahuje robustní ošetření chyb, včetně neplatného XML.
        /// </summary>
        /// <typeparam name="T">Typ objektu v seznamu.</typeparam>
        /// <param name="filename">Název souboru (bez přípony).</param>
        /// <returns>List objektů daného typu nebo prázdný List při chybě.</returns>
        public static List<T> ReadFromXML<T>(string filename)
        {
            try
            {
                Konfigurace.Globals_Konfig_Konzola.LoadConfiguration();

                if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FiltryVazatNaUzivatele &&
                    FASK.Logins.Uzivatel.Instance.UserID != null)
                {
                    filename = FASK.Logins.Uzivatel.Instance.UserID.Trim() + filename;
                }

                string filePath = Path.Combine(MySystem.MyPath.FilterDirectory, filename + ".xml");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Soubor {filePath} neexistuje.");
                    return new List<T>();
                }

                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.Load(filePath);
                }
                catch (XmlException ex)
                {
                    Console.WriteLine($"Chyba při načítání XML souboru: {ex.Message} (řádek {ex.LineNumber}, sloupec {ex.LinePosition})");
                    return new List<T>();
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    try
                    {
                        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(List<T>) })[0];
                        return (List<T>)xmlSerializer.Deserialize(reader);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"Chyba při deserializaci XML souboru: {ex.Message}");
                        return new List<T>();
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Nedostatečná oprávnění pro přístup k souboru: {ex.Message}");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Adresář neexistuje: {ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Soubor nebyl nalezen: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Chyba při čtení nebo zápisu souboru: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekávaná chyba: {ex.Message}");
            }

            // Pokud nastane jakákoli výjimka, vrátí prázdný seznam.
            return new List<T>();
        }




    }
}
