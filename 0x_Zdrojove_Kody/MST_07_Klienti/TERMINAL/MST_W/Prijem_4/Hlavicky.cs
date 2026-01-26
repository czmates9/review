using System;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.PrijemService;
using Fask.MST_W.Forms;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace Fask.MST_W.Prijem_4
{
    public static class Hlavicky
    {
        private static string prijemhlavickyxml = Main.StiahnutePrijemkyName;
        private static string prijemhlavickyxmlschema = Main.StiahnutePrijemkyNameSchema;
        private static PrijemDavky prijemdavky = new PrijemDavky();

        public static PrijemDavky Davky
        {
            get
            {
                return prijemdavky;
            }
        }

        static Hlavicky()
        {
            LoadHlavicky();
        }

        public static void LoadHlavicky()
        {
            if (System.IO.File.Exists(prijemhlavickyxml))
            {
                try
                {
                    prijemdavky.Clear();
                    if (File.Exists(prijemhlavickyxmlschema))
                    {
                        prijemdavky.ReadXmlSchemaDynamic(prijemhlavickyxmlschema);
                    }
                    if (File.Exists(prijemhlavickyxml))
                    {
                        prijemdavky.ReadXml(prijemhlavickyxml);
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
                PrijemDavky.HlavickyRow hr = null;
                for (int i = prijemdavky.Hlavicky.Count - 1; i >= 0; i--)
                {
                    hr = prijemdavky.Hlavicky[i];
                    if (!System.IO.File.Exists(System.IO.Path.Combine(Main.StorageDir, hr.CountEntries + "." + Main.Ext_Prijem)))
                    {
                        hr.Delete();
                    }
                }

                prijemdavky.AcceptChanges();

                //2. pokud existuje soubor vydejky, ktery neni v hlavickach, tak ho prida, ale bez popisu
                foreach (string filevydej in System.IO.Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Prijem))
                {
                    string davka = System.IO.Path.GetFileNameWithoutExtension(filevydej);
                    if (!HlavickaExists(davka))
                    {
                        // TODO : dotazeni informaci do hlavicky (Objednavka, Sklad, ...)
                        hr = prijemdavky.Hlavicky.NewHlavickyRow();
                        hr.CountEntries = davka;
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
            prijemdavky.AcceptChanges();
            prijemdavky.WriteXmlSchema(prijemhlavickyxmlschema);
            prijemdavky.WriteXml(prijemhlavickyxml);
        }

        public static void WriteSchemaWithDynamicColumns(PrijemService.PrijemDavky prijemdavkyFromServer)
        {
            if (prijemdavkyFromServer.Hlavicky.Columns.Count > prijemdavky.Hlavicky.Columns.Count)
            {
                prijemdavkyFromServer.WriteXmlSchema(prijemhlavickyxmlschema);
                LoadHlavicky();
            }
        }

        public static bool HlavickaSelect(PrijemDavky.HlavickyRow hrow)
        {
            PrijemDavky.HlavickyRow[] hrows = (PrijemDavky.HlavickyRow[])prijemdavky.Hlavicky.Select("CountEntries='" + hrow.CountEntries + "'");
            if (hrows.Length > 0)
            {
                return false;
            }
            return true;
        }

        public static bool HlavickaExists(string cisloDavky)
        {
            PrijemDavky.HlavickyRow[] hrows = (PrijemDavky.HlavickyRow[])prijemdavky.Hlavicky.Select("CountEntries='" + cisloDavky.Trim()+"'");
            return hrows.Length > 0;
        }

        public static void HlavickaAdd(PrijemDavky.HlavickyRow hrow)
        {

            PrijemDavky.HlavickyRow hrown = prijemdavky.Hlavicky.NewHlavickyRow();
            if (!hrow.IsCountEntriesNull()) hrown.CountEntries = hrow.CountEntries;
            if (!hrow.IsPONUMBERNull()) hrown.PONUMBER = hrow.PONUMBER;
            if (!hrow.IsSKL_IDNull()) hrown.SKL_ID = hrow.SKL_ID;
            if (!hrow.IsCntItemsNull()) hrown.CntItems = hrow.CntItems;
            if (!hrow.IsSumItemsNull()) hrown.SumItems = hrow.SumItems;

            if (!hrow.IsSloucenaNull())
                hrown.Sloucena = hrow.Sloucena;
            else if (!hrow.IsCountEntriesNull() && hrow.CountEntries.Contains("S"))
                hrown.Sloucena = false;
            else
                hrown.Sloucena = false;
           //prijemdavky.Hlavicky.AddHlavickyRow(hrown);

            foreach (System.Data.DataColumn dcol in hrow.Table.Columns)
            {
                if (!prijemdavky.Hlavicky.Columns.Contains(dcol.ColumnName))
                {
                    prijemdavky.Hlavicky.Columns.Add(dcol.ColumnName, dcol.DataType);
                }
            }

            //prijemdavky.Hlavicky.ImportRow(hrow); nefunguje korektne, nepridava hlavicky

            prijemdavky.Hlavicky.AddHlavickyRow(hrown); // opravena synchronizace hlavicek, pokud stornuje stahovani hlavicky
            
            Write();
        }

        public static void HlavickaDelete(PrijemDavky.HlavickyRow hrow)
        {
            HlavickaDelete(hrow.CountEntries);
        }

        public static void HlavickaDelete(string davka)
        {
            PrijemDavky.HlavickyRow[] hrows = (PrijemDavky.HlavickyRow[])prijemdavky.Hlavicky.Select("CountEntries='" + davka +"'");
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


        private static PrijemDavky.HlavickyRow[] FindHlavickyRows(string davka)
        {
            PrijemDavky.HlavickyRow[] hrows = (PrijemDavky.HlavickyRow[])prijemdavky.Hlavicky.Select("CountEntries='" + davka + "'");

            return hrows;
        }



        public static PrijemDavky.HlavickyRow[] HlavickaFind(string countEntries)
        {
            return FindHlavickyRows(countEntries);
        }


        public static void SetSloucena(PrijemDavky.HlavickyRow chosenRow, bool p)
        {
            PrijemDavky.HlavickyRow[] hrows = FindHlavickyRows(chosenRow.CountEntries.ToString());
            PrijemDavky.HlavickyRow hrow = hrows[0];
            if (!hrow.Sloucena && p) //neni sloucena, ale ma se sloucit ...
            {
                PrijemService.PrijemDavky.HlavickyRow s1row = Hlavicky.HlavickaFind("S1")[0];

                if (!s1row.IsPONUMBERNull())
                    s1row.PONUMBER = (int.Parse(s1row.PONUMBER) + 1).ToString();
                else
                    s1row.PONUMBER = "1";
                if (!hrow.IsCntItemsNull())
                    s1row.CntItems += hrow.CntItems;
                if (!hrow.IsSumItemsNull())
                    s1row.SumItems += hrow.SumItems;

            }
            else if (hrow.Sloucena && !p) //je sloucena, ale ma se oddelit
            {
                PrijemService.PrijemDavky.HlavickyRow s1row = Hlavicky.HlavickaFind("S1")[0];

                if (!s1row.IsPONUMBERNull())
                    s1row.PONUMBER = (int.Parse(s1row.PONUMBER) - 1).ToString();
                else
                    s1row.PONUMBER = "0";
                if (!hrow.IsCntItemsNull())
                    s1row.CntItems -= hrow.CntItems;
                if (!hrow.IsSumItemsNull())
                    s1row.SumItems -= hrow.SumItems;

            }
            else
            {
                // 1) je sloucena a opet se slucuje
                // 2) nebo neni sloucena a oddeluje se
                // => nic se nedeje, zustava jak je ...
            }
            hrow.Sloucena = p;

            Write();
        }
    }
}
