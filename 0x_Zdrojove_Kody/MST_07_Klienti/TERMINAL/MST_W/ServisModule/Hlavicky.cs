using System;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.ServisModuleWService;
using Fask.MST_W.Forms;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace Fask.MST_W.ServisModule
{
    public static class Hlavicky
    {
        private static string servishlavickyxml = Main.StiahnuteServiskyName;
        private static string servishlavickyxmlschema = Main.StiahnuteServiskyNameSchema;
        private static ServisDavky servisdavky = new ServisDavky();

        public static ServisDavky Davky
        {
            get
            {
                return servisdavky;
            }
        }

        static Hlavicky()
        {
            LoadHlavicky();
        }

        public static void LoadHlavicky()
        {
            if (System.IO.File.Exists(servishlavickyxml))
            {
                try
                {
                    servisdavky.Clear();
                    if (File.Exists(servishlavickyxmlschema))
                    {
                        servisdavky.ReadXmlSchemaDynamic(servishlavickyxmlschema);
                    }
                    if (File.Exists(servishlavickyxml))
                    {
                        servisdavky.ReadXml(servishlavickyxml);
                    }
                }
                catch
                {
                }
            }

            Synchronize();
        }

        public static void Synchronize()
        {
            //foreach (PrijemDavky.HlavickyRow hr in prijemdavky.Hlavicky)
            //{
            //    if (!System.IO.File.Exists(System.IO.Path.Combine(Main.DataDir, hr.CountEntries.ToString() + "." + Main.PrijemIExtData)))
            //    {
            //        hr.Delete();
            //    }
            //}

            try
            {
                //1.Projde hlavicky a pokud je v hlavickach zaznam a k ni neexisutje soubor, tak ji smaze
                ServisDavky.HlavickyRow hr = null;
                for (int i = servisdavky.Hlavicky.Count - 1; i >= 0; i--)
                {
                    hr = servisdavky.Hlavicky[i];
                    if (!System.IO.File.Exists(System.IO.Path.Combine(Main.StorageDir, hr.CountEntries + "." + Main.Ext_ServisI)))
                    {
                        hr.Delete();
                    }
                }

                servisdavky.AcceptChanges();

                //2. pokud existuje soubor vydejky, ktery neni v hlavickach, tak ho prida, ale bez popisu
                foreach (string filevydej in System.IO.Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_ServisI))
                {
                    string davka = System.IO.Path.GetFileNameWithoutExtension(filevydej);
                    if (!HlavickaExists(davka))
                    {
                        // TODO : dotazeni informaci do hlavicky (Objednavka, Sklad, ...)
                        hr = servisdavky.Hlavicky.NewHlavickyRow();
                        hr.CountEntries = davka;

                        //hr.DOCUMENT_NUMBER = string.Empty;
                        //hr.CntItems = 0;
                        //hr.OkruhID = string.Empty;
                        //hr.PONUMBER = string.Empty;
                        //hr.SumItems = 0;
                        //hr.CntItems = 0;
                        HlavickaAdd(hr);
                    }
                }

                Write();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Hlavièky", MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private static void Write()
        {
            servisdavky.AcceptChanges();
            servisdavky.WriteXmlSchema(servishlavickyxmlschema);
            servisdavky.WriteXml(servishlavickyxml);
        }

        public static void WriteSchemaWithDynamicColumns(ServisModuleWService.ServisDavky servisdavkyFromServer)
        {
            if (servisdavkyFromServer.Hlavicky.Columns.Count > servisdavky.Hlavicky.Columns.Count)
            {
                servisdavkyFromServer.WriteXmlSchema(servishlavickyxmlschema);
                LoadHlavicky();
            }
        }

        public static bool HlavickaSelect(ServisDavky.HlavickyRow hrow)
        {
            ServisDavky.HlavickyRow[] hrows = (ServisDavky.HlavickyRow[])servisdavky.Hlavicky.Select("CountEntries='" + hrow.CountEntries + "'");
            if (hrows.Length > 0)
            {
                return false;
            }
            return true;
        }

        public static bool HlavickaExists(string cisloDavky)
        {
            ServisDavky.HlavickyRow[] hrows = (ServisDavky.HlavickyRow[])servisdavky.Hlavicky.Select("CountEntries='" + cisloDavky.Trim() + "'");
            return hrows.Length > 0;
        }

        public static void HlavickaAdd(ServisDavky.HlavickyRow hrow)
        {
            ServisDavky.HlavickyRow hrown = servisdavky.Hlavicky.NewHlavickyRow();
            if (!hrow.IsCountEntriesNull()) hrown.CountEntries = hrow.CountEntries;
            if (!hrow.IsDOCUMENT_NUMBERNull()) hrown.DOCUMENT_NUMBER = hrow.DOCUMENT_NUMBER;
            if (!hrow.IsCntItemsNull()) hrown.CntItems = hrow.CntItems;
            if (!hrow.IsOkruhIDNull()) hrown.OkruhID = hrow.OkruhID;
            if (!hrow.IsODB_IDNull()) hrown.ODB_ID = hrow.ODB_ID;
            if (!hrow.IsOdbOznaceniNull()) hrown.OdbOznaceni = hrow.OdbOznaceni;
            if (!hrow.IsOkruhOznaceniNull()) hrown.OkruhOznaceni = hrow.OkruhOznaceni;
            if (!hrow.IsBarcodeNull()) hrown.Barcode = hrow.Barcode;
            //if (!hrow.IsSloucenaNull())
            //    hrown.Sloucena = hrow.Sloucena;
            //else if (!hrow.IsCountEntriesNull() && hrow.CountEntries.Contains("S"))
            //    hrown.Sloucena = false;
            //else
            //    hrown.Sloucena = false;
           //prijemdavky.Hlavicky.AddHlavickyRow(hrown);

            foreach (System.Data.DataColumn dcol in hrow.Table.Columns)
            {
                if (!servisdavky.Hlavicky.Columns.Contains(dcol.ColumnName))
                {
                    servisdavky.Hlavicky.Columns.Add(dcol.ColumnName, dcol.DataType);
                }
            }

            //prijemdavky.Hlavicky.ImportRow(hrow); nefunguje korektne, nepridava hlavicky

            servisdavky.Hlavicky.AddHlavickyRow(hrown); // opravena synchronizace hlavicek, pokud stornuje stahovani hlavicky
            
            Write();
        }

        public static void HlavickaDelete(ServisDavky.HlavickyRow hrow)
        {
            HlavickaDelete(hrow.CountEntries);
        }

        public static void HlavickaDelete(string davka)
        {
            ServisDavky.HlavickyRow[] hrows = (ServisDavky.HlavickyRow[])servisdavky.Hlavicky.Select("CountEntries='" + davka + "'");
            if (hrows.Length > 0)
            {
                //foreach (PrijemDavky.HlavickyRow hr in hrows)
                //{
                //    hr.Delete();
                //}

                for (int i = hrows.Length - 1; i >= 0; i--)
                {
                    hrows[i].Delete();
                }
            }

            Write();
        }


        private static ServisDavky.HlavickyRow[] FindHlavickyRows(string davka)
        {
            ServisDavky.HlavickyRow[] hrows = (ServisDavky.HlavickyRow[])servisdavky.Hlavicky.Select("CountEntries='" + davka + "'");

            return hrows;
        }



        public static ServisDavky.HlavickyRow[] HlavickaFind(string countEntries)
        {
            return FindHlavickyRows(countEntries);
        }


        //public static void SetSloucena(ServisDavky.HlavickyRow chosenRow, bool p)
        //{
        //    ServisDavky.HlavickyRow[] hrows = FindHlavickyRows(chosenRow.CountEntries.ToString());
        //    ServisDavky.HlavickyRow hrow = hrows[0];
        //    if (!hrow.Sloucena && p) //neni sloucena, ale ma se sloucit ...
        //    {
        //        PrijemService.PrijemDavky.HlavickyRow s1row = Hlavicky.HlavickaFind("S1")[0];

        //        if (!s1row.IsPONUMBERNull())
        //            s1row.PONUMBER = (int.Parse(s1row.PONUMBER) + 1).ToString();
        //        else
        //            s1row.PONUMBER = "1";
        //        if (!hrow.IsCntItemsNull())
        //            s1row.CntItems += hrow.CntItems;
        //        if (!hrow.IsSumItemsNull())
        //            s1row.SumItems += hrow.SumItems;

        //    }
        //    else if (hrow.Sloucena && !p) //je sloucena, ale ma se oddelit
        //    {
        //        PrijemService.PrijemDavky.HlavickyRow s1row = Hlavicky.HlavickaFind("S1")[0];

        //        if (!s1row.IsPONUMBERNull())
        //            s1row.PONUMBER = (int.Parse(s1row.PONUMBER) - 1).ToString();
        //        else
        //            s1row.PONUMBER = "0";
        //        if (!hrow.IsCntItemsNull())
        //            s1row.CntItems -= hrow.CntItems;
        //        if (!hrow.IsSumItemsNull())
        //            s1row.SumItems -= hrow.SumItems;

        //    }
        //    else
        //    {
        //        // 1) je sloucena a opet se slucuje
        //        // 2) nebo neni sloucena a oddeluje se
        //        // => nic se nedeje, zustava jak je ...
        //    }
        //    hrow.Sloucena = p;

        //    Write();
        //}
    }
}
