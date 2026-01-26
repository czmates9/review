using System;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.VydejService;
using Fask.MST_W.Forms;
using System.IO;


namespace Fask.MST_W.Vydej_3
{
    public static class Hlavicky
    {
        private static string vydejVShlavixkyxmlSchema = Main.StiahnuteVydajkyNameSchema;
        private static string vydejVShlavixkyxml = Main.StiahnuteVydajkyName;
        private static Vydejky vydejky = new Vydejky();

        public static Vydejky Davky
        {
            get
            {
                Synchronize();
                return vydejky;
            }
        }

        static Hlavicky()
        {
            LoadHlavicky();
        }

        public static void LoadHlavicky()
        {
            if (System.IO.File.Exists(vydejVShlavixkyxml))
            {
                try
                {
                    vydejky.Clear();
                    if (File.Exists(vydejVShlavixkyxmlSchema))
                    {
                        vydejky.ReadXmlSchemaDynamic(vydejVShlavixkyxmlSchema);
                    }
                    if (File.Exists(vydejVShlavixkyxml))
                    {
                        vydejky.ReadXml(vydejVShlavixkyxml);
                    }
                }
                catch
                {
                }
            }

            Synchronize();
        }

        //Synchronizace metabaze hlavicek vydejek
        public static void Synchronize()
        {
            //foreach (Vydejky.HlavickyRow hr in vydejky.Hlavicky)
            //{
            //    if (!System.IO.File.Exists(System.IO.Path.Combine(Main.DataDir, hr.CountEntries.ToString() + "." + Main.VydejIExt)))
            //    {
            //        hr.Delete();
            //    }
            //}

            try
            {
                //1.Projde hlavicky a pokud je v hlavickach zaznam a k ni neexisutje soubor, tak ji smaze
                Vydejky.HlavickyRow hr = null;
                for (int i = vydejky.Hlavicky.Count - 1; i >= 0; i--)
                {
                    hr = vydejky.Hlavicky[i];
                    if (!System.IO.File.Exists(System.IO.Path.Combine(Main.StorageDir, hr.CountEntries.ToString() + "." + Main.Ext_Vydej)))
                    {
                        hr.Delete();
                    }
                }

                vydejky.AcceptChanges();

                //2. pokud existuje soubor vydejky, ktery neni v hlavickach, tak ho prida, ale bez popisu
                foreach (string filevydej in System.IO.Directory.GetFiles(Main.StorageDir, "*." + Main.Ext_Vydej))
                {
                    string davka = System.IO.Path.GetFileNameWithoutExtension(filevydej);
                    if (!HlavickaExists(davka))
                    {
                        hr = vydejky.Hlavicky.NewHlavickyRow();
                        hr.CountEntries = davka;
                        hr.SOPNUMBE = string.Empty;
                        hr.SumItems = 0;
                        hr.CntItems = 0;
                        hr.Info1 = string.Empty;
                        hr.PRIORITY = 3;
                        hr.VNDDOCNM = string.Empty;
                        hr.Sloucena = false;
                        HlavickaAdd(hr);
                    }
                }

                Write();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, "Hlavièky", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private static void Write()
        {
            vydejky.AcceptChanges();
            vydejky.WriteXmlSchema(vydejVShlavixkyxmlSchema);
            vydejky.WriteXml(vydejVShlavixkyxml);
        }

        public static void WriteSchemaWithDynamicColumns(VydejService.Vydejky vydejkyFromServer)
        {
            if (vydejkyFromServer.Hlavicky.Columns.Count > vydejky.Hlavicky.Columns.Count)
            {
                vydejkyFromServer.WriteXmlSchema(vydejVShlavixkyxmlSchema);
                LoadHlavicky();
            }
        }

        public static bool HlavickaSelect(Vydejky.HlavickyRow hrow)
        {
            Vydejky.HlavickyRow[] hrows = FindHlavickyRows(hrow.CountEntries); 

            if (hrows.Length > 0)
            {
                return false;
            }
            return true;
        }

        public static Vydejky.HlavickyRow[] HlavickaFind(string countEntries)
        {
            return FindHlavickyRows(countEntries);
        }

        public static bool HlavickaExists(string cisloDavky)
        {
            Vydejky.HlavickyRow[] hrows = FindHlavickyRows(cisloDavky); 

            return hrows.Length > 0;
        }

        public static void HlavickaAdd(Vydejky.HlavickyRow hrow)
        {
            // Overeni dodatecny sloupcu a jeji pripadne vlozeni
            foreach (System.Data.DataColumn dcol in hrow.Table.Columns)
            {
                if (!vydejky.Hlavicky.Columns.Contains(dcol.ColumnName))
                {
                    vydejky.Hlavicky.Columns.Add(dcol.ColumnName, dcol.DataType);
                }

            }
 
            if (hrow.IsSloucenaNull()) hrow.Sloucena = false;

            // Oprava 1csc stahovani X synchronizace...
            if (hrow.Table == vydejky.Hlavicky)
                vydejky.Hlavicky.AddHlavickyRow(hrow);
            else
                vydejky.Hlavicky.ImportRow(hrow);

            //Vydejky.HlavickyRow hrown = vydejky.Hlavicky.NewHlavickyRow();
            //hrown.CountEntries = hrow.CountEntries;
            //hrown.SOPNUMBE = hrow.SOPNUMBE;
            //if (!hrow.IsCntItemsNull()) hrown.CntItems = hrow.CntItems;
            //if (!hrow.IsSumItemsNull()) hrown.SumItems = hrow.SumItems;
            //if (!hrow.IsVNDDOCNMNull()) hrown.VNDDOCNM = hrow.VNDDOCNM;
            //try { if (!hrow.IsInfo1Null()) hrown.Info1 = hrow.Info1; }
            //catch { }
            //vydejky.Hlavicky.AddHlavickyRow(hrown);

            Write();
        }


        public static void HlavickaDelete(Vydejky.HlavickyRow hrow)
        {
            HlavickaDelete(hrow.CountEntries);
        }

        public static void HlavickaDelete(string davka)
        {
            try
            {
                Logging.TracId tid = new Fask.Logging.TracId(MST_Global.UserID, MST_Global.TerminalID, null, "Hlavicky", "HlavickaDelete");
                Logging.Trace2.Write("Start", "HlavickaDelete", tid);

                Vydejky.HlavickyRow[] hrows = FindHlavickyRows(davka); 
                if (hrows.Length > 0)
                {
                    //foreach (Vydejky.HlavickyRow hr in hrows)
                    //{
                    //    hr.Delete();
                    //}
                    for (int i = hrows.Length - 1; i >= 0; i--)
                    {
                        if (hrows[i].Sloucena)
                        {
                            //jedna se o sloucenou a tak ponizit stav v hlavicce
                            VydejService.Vydejky.HlavickyRow s1row = Hlavicky.HlavickaFind("S1")[0];
                            VydejService.Vydejky.HlavickyRow drow = hrows[i];

                            s1row.SOPNUMBE = (int.Parse(s1row.SOPNUMBE) - 1).ToString();
                            s1row.CntItems -= drow.CntItems;
                            s1row.SumItems -= drow.SumItems;
                        }

                        hrows[i].Delete();
                    }
                }

                Write();
                Logging.Trace2.Write("End", "HlavickaDelete", tid);
            }
            catch //(Exception ex)
            {
            }
        }


        /// <summary>
        /// Kvuli sloucenym...JoZ...
        /// </summary>
        /// <param name="davka"></param>
        /// <returns></returns>
        private static Vydejky.HlavickyRow[] FindHlavickyRows(string davka)
        {
            Vydejky.HlavickyRow[] hrows = (Vydejky.HlavickyRow[])vydejky.Hlavicky.Select("CountEntries='" + davka + "'");
            /*
            List<Vydejky.HlavickyRow> hrows = new List<Vydejky.HlavickyRow>(); 

            foreach (Vydejky.HlavickyRow item in vydejky.Hlavicky)
            {
                if (item.CountEntries == davka)
                    hrows.Add(item);
            }
            */

            return hrows;
        }

        public static void SetSloucena(Vydejky.HlavickyRow chosenRow, bool p)
        {
            Vydejky.HlavickyRow[] hrows = FindHlavickyRows(chosenRow.CountEntries);
            Vydejky.HlavickyRow hrow = hrows[0];
            if (!hrow.Sloucena && p) //neni sloucena, ale ma se sloucit ...
            {
                VydejService.Vydejky.HlavickyRow s1row = Hlavicky.HlavickaFind("S1")[0];

                s1row.SOPNUMBE = (int.Parse(s1row.SOPNUMBE) + 1).ToString();
                s1row.CntItems += hrow.CntItems;
                s1row.SumItems += hrow.SumItems;
            }
            else if (hrow.Sloucena && !p) //je sloucena, ale ma se oddelit
            {
                VydejService.Vydejky.HlavickyRow s1row = Hlavicky.HlavickaFind("S1")[0];

                s1row.SOPNUMBE = (int.Parse(s1row.SOPNUMBE) - 1).ToString();
                s1row.CntItems -= hrow.CntItems;
                s1row.SumItems -= hrow.SumItems;
            }
            else 
            {
                // 1) je sloucena a opet se slucuje
                // 2) nebo neni sloucena a oddeluje se
                // => nic se nedeje, zustava jak je ...
            }
            hrow.Sloucena = p;

        }
    }
}
