using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
// Importujte knihovnu System.Globalization
using System.Globalization;
using Microsoft.VisualBasic.FileIO; // Nutné pro použití TextFieldParser
using Fask.WEBAPI.API_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq.Dynamic.Core;
using System.Reflection;


//internal class xx {
//    [CsvHelper.Configuration.Attributes.Name("Oznaceni polozky")]
//    public string MyProperty { get; set; }
//}

namespace Konzola.Extensions
{
    public static class DataGridView_Exporty
    {

        private static Fask.Interfaces.IMES providerZbozi = null;

        /// <summary>
        /// Inicializace providera
        /// </summary>
        private static void InitProvider()
        {
            try
            {
                if (string.IsNullOrEmpty(Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola))
                    return;

                if (providerZbozi == null) //inicializace se provede pouze pokud nebyla provedena ... 
                {
                    Assembly providerAssemlby = Assembly.LoadFrom(System.IO.Path.Combine(Konzola.MySystem.MyPath.CurrentDirectory, Konfigurace.Globals_Konfig_Konzola.Konfigurace.Provider[0].ProviderKonzola));
                    Type[] types = providerAssemlby.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                            if (typeof(Fask.Interfaces.Ciselniky.Zbozi.IZbozi2).IsAssignableFrom(t))
                            {
                                providerZbozi = (Fask.Interfaces.Ciselniky.Zbozi.IZbozi2)providerAssemlby.CreateInstance(t.FullName);
                                if (providerZbozi != null)
                                    break;
                            }
                        }
                        catch { }
                    }
                    //return config;
                }

                providerZbozi.InitProvider();


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public static string RemoveDiacritism(string Text)
        {
            string stringFormD = Text.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder retVal = new System.Text.StringBuilder();
            for (int index = 0; index < stringFormD.Length; index++)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stringFormD[index]) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    retVal.Append(stringFormD[index]);
            }
            return retVal.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }





        #region Export to CSV


        private static DialogResult showPathDialog_CSV(out string filepath, bool ulozeni = true)
        {
            filepath = string.Empty;

            //object sfd = null;
            //DialogResult dr = sfd.ShowDialog();
            DialogResult dr = new DialogResult();
            try
            {


                if (ulozeni)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    //sfd = new SaveFileDialog();
                    sfd.Filter = ".csv Files (*.csv)|*.csv";
                    sfd.InitialDirectory = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyPath;
                    sfd.FileName = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyCSVFileName;

                    //DialogResult dr = sfd.ShowDialog();
                    dr = sfd.ShowDialog();
                    if (dr == DialogResult.OK)
                    {

                        Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyPath = Path.GetDirectoryName(sfd.FileName);
                        //Settings.ExportyCSVFileName = Path.GetFileNameWithoutExtension(sfd.FileName);
                        filepath = sfd.FileName;
                    }
                }
                else
                {
                    OpenFileDialog sfd = new OpenFileDialog();
                    //sfd = new OpenFileDialog();
                    sfd.Filter = ".csv Files (*.csv)|*.csv";
                    sfd.InitialDirectory = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyPath;
                    sfd.FileName = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyCSVFileName;

                    // DialogResult dr = sfd.ShowDialog();
                    dr = sfd.ShowDialog();
                    if (dr == DialogResult.OK)
                    {

                        Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyPath = Path.GetDirectoryName(sfd.FileName);
                        //Settings.ExportyCSVFileName = Path.GetFileNameWithoutExtension(sfd.FileName);
                        filepath = sfd.FileName;
                    }
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dr;
        }

        public static bool ExportToCSV(this Zuby.ADGV.AdvancedDataGridView dgv, Fask.Interfaces.Classes.EXPORT_DAT typ_exportu)
        {
            string path = string.Empty;

            // TODO: konfiguracne
            if (showPathDialog_CSV(out path) != DialogResult.OK)
                return false;

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            //System.IO.FileInfo newFile = new System.IO.FileInfo(path);

            // kopie tabulky s naslednym odstranenim skrytych sloupcu
            System.Data.DataTable tCxC = new System.Data.DataTable();

            #region Export VSE
            if (typ_exportu == Fask.Interfaces.Classes.EXPORT_DAT.VSE)
            {
                if (dgv.DataSource is BindingSource)
                {
                    BindingSource bs = (BindingSource)dgv.DataSource;

                    if (bs.List is DataView)
                    {
                        tCxC = ((DataView)bs.List).ToTable().Copy();
                    }
                    else if (bs.DataSource is DataSet)
                    {
                        tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].DefaultView.ToTable().Copy();
                    }
                    else if (bs.DataSource is System.Data.DataTable)
                    {
                        tCxC = ((System.Data.DataTable)bs.DataSource).DefaultView.ToTable().Copy();
                    }
                    else throw new Exception("Export CSV: Neznámý typ pro přetypování");
                }
                else if (dgv.DataSource is System.Data.DataTable)
                {
                    tCxC = ((System.Data.DataTable)dgv.DataSource).DefaultView.ToTable().Copy();
                }
                else throw new Exception("Export CSV: Neznámý typ pro přetypování");
            }
            #endregion

            #region exportovat vybrane  
            else  // exportovat vybrane
            {
                System.Data.DataTable dtClone = new System.Data.DataTable();
                if (dgv.DataSource is BindingSource)
                {
                    BindingSource bs = (BindingSource)dgv.DataSource;

                    if (bs.DataSource is DataSet)
                    {
                        tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
                        dtClone = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
                    }
                    else if (bs.DataSource is System.Data.DataTable)
                    {
                        tCxC = ((System.Data.DataTable)bs.DataSource).Clone();
                        dtClone = ((System.Data.DataTable)bs.DataSource).Clone();
                    }
                    else throw new Exception("Export CSV: Neznámý typ pro přetypování");
                }
                else if (dgv.DataSource is System.Data.DataTable)
                {
                    tCxC = ((System.Data.DataTable)dgv.DataSource).Copy();
                    dtClone = ((System.Data.DataTable)dgv.DataSource).Copy();
                }
                else throw new Exception("Export CSV: Neznámý typ pro přetypování");

                // import vybranych radku
                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    dtClone.ImportRow(((DataRowView)row.DataBoundItem).Row);
                }
                dtClone.AcceptChanges();

                // otocit poradi a naimportovat do tCxC
                for (int i = dtClone.Rows.Count - 1; i >= 0; i--)
                {
                    tCxC.ImportRow(dtClone.Rows[i]);
                }
                tCxC.AcceptChanges();
            }
            #endregion

            // kopie datatable, se kterym se bude pracovat
            System.Data.DataTable dt2 = tCxC.Copy();

            // odstraneni skrytych sloupcu
            var myCol_PK = dt2.PrimaryKey;

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (!dgv.Columns[i].Visible)
                {

                    if (!myCol_PK.Any(x => x.ColumnName.Trim() == dgv.Columns[i].DataPropertyName))
                    {
                        dt2.Columns.Remove(dgv.Columns[i].DataPropertyName);
                    }
                }
            }

            // zmena nazvu hlavicek na ty, ktere se zobrazuji
            foreach (DataGridViewColumn item in dgv.Columns)
            {
                if (dt2.Columns.Contains(item.DataPropertyName) && !dt2.Columns.Contains(item.HeaderText))
                    dt2.Columns[item.DataPropertyName].ColumnName = item.HeaderText;
            }


            dt2.DT_To_CSV(path, ";");

            //// naplneni excelu daty
            //ws.Cells["A1"].LoadFromDataTable(dt2, true);

            //// nastaveni formatu datumu
            //var dateColumns = from DataColumn d in dt2.Columns
            //                  where d.DataType == typeof(DateTime)// || d.ColumnName.Contains("Date")
            //                  select d.Ordinal + 1;
            //foreach (var dc in dateColumns)
            //{
            //    ws.Cells[2, dc, dt2.Rows.Count + 2, dc].Style.Numberformat.Format = Settings.ExportyExcelFormatDatum;
            //}




            //// spusteni v excelu
            //if (Settings.ExportyExcelOtevritPoVygenerovani)
            //    System.Diagnostics.Process.Start(path);

            return true;
        }


        #region import dat z CSV souboru
        #region moje logika
        //public static bool ImportFromCSV(this Zuby.ADGV.AdvancedDataGridView dgv)
        //{
        //    try
        //    {
        //        string path = string.Empty;

        //        if (showPathDialog_CSV(out path, false) != DialogResult.OK)
        //            return false;



        //        //import dat
        //        string import = string.Empty;
        //        //zde vytvor logiku importovani dat ze souboru csv do dgv


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //        //return false;
        //    }

        //    return true;
        //} 
        #endregion

        #region chatGTP
        public static bool ImportFromCSV(this Zuby.ADGV.AdvancedDataGridView dgv, List<Tuple<string, string, bool>> tableInfo)
        {
            try
            {
                string path = string.Empty;

                // inicializace providera
                InitProvider();

                // TODO: Konfigurace
                if (showPathDialog_CSV(out path, false) != DialogResult.OK)
                    return false;

                // Zkontroluje, zda soubor existuje
                if (!System.IO.File.Exists(path))
                {
                    // Řeší případ, kdy soubor neexistuje
                    MessageBox.Show("Vybraný soubor neexistuje.", "Soubor nenalezen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //ziskani dat ze souboru csv..
                //var csvData =  ImportCsvData(path);
                //nosny nastroj.. entity framework?
                //mam cestu k souboru s daty a potrebuji je pretransformovat tak abych je mohl zapsat do tabulky v sql jazyce..jak na to?
               
                FASK_ZASOBY_ALL_row rows = new FASK_ZASOBY_ALL_row();  //--jak na to?

                //nacteni dat z csv souboru a nasledna transformace na zapis do DB
                //filePath = "path_to_your_file.csv";
                var records = new List<FASK_ZASOBY_row>();



                //JiS aplikace TODO MaR---
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(
                           reader,
                           //new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                           //new CsvHelper.Configuration.CsvConfiguration(CultureInfo.CurrentCulture)
                           new CsvHelper.Configuration.CsvConfiguration(CultureInfo.GetCultureInfo("cs-CZ"))
                           {
                               Delimiter = ";",
                               MissingFieldFound = null

                           }))
                {
                    csv.Read();
                    csv.ReadHeader();
                    //var records = csv.GetRecords<FASK_ZASOBY_CSV00_row>();

                    //var listofrecords = records.ToList();

                    //while (csv.Read())
                    //{
                    //    var row = csv.GetRecord<FASK_ZASOBY_CSV00_row>();
                    //    Console.WriteLine(ObjectDumper.Dump(row));
                    //}

                    foreach (var record in csv.GetRecords<FASK_ZASOBY_CSV00_row>())
                    {
                        //  Console.WriteLine(ObjectDumper.Dump(record));//
                        //insert doDB.....sent
                        var coToJe = record;

                        //zde aplikovat zapis do tabulky DB FASK_ZASOBY 22.5.2024

                        //zeptat se poprve zda-li vymazat obsah tabulky..
                        ZapisDoFASK_ZASOBY(record);


                    }
                }



                #region nacteni CSV dat -- zakomentovano
                //using (var reader = new StreamReader(path))
                //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                //{
                //    while (csv.Read())
                //    {
                //        string xx = string.Empty;

                //       //  var x = csv[headeratrringute.username];
                //        records = csv.GetRecords<FASK_ZASOBY_row>().ToList(); //zde mi to vyhazuje chybu typu: IEnumerable<FASK_ZASOBY_row> neobsahuje definici pro ToList..oprav to!
                //                                                              //chyba pretrvava {"Header with name 'RowState'[0] was not found.\r\nHeader with name 'DEX_ROW_ID'[0] was not found.\r\nHeader with name 'ITEMNMBR'[0] was not found.\r\nHeader with name 'ITEMDESC'[0] was not found.\r\nHeader with name 'ITEMCODE'[0] was not found.\r\nHeader with name 'VNDITNUM'[0] was not found.\r\nHeader with name 'CZ_CarKod'[0] was not found.\r\nHeader with name 'LOCNCODE'[0] was not found.\r\nHeader with name 'SKL_ID'[0] was not found.\r\nHeader with name 'QTY'[0] was not found.\r\nHeader with name 'QTYPACK'[0] was not found.\r\nHeader with name 'MJ'[0] was not found.\r\nHeader with name 'DMJ'[0] was not found.\r\nHeader with name 'TAXRATE'[0] was not found.\r\nHeader with name 'PRICE0'[0] was not found.\r\nHeader with name 'PRICE1'[0] was not found.\r\nHeader with name 'PRICE2'[0] was not found.\r\nHeader with name 'PRICE3'[0] was not found.\r\nHeader with name 'PRICE4'[0] was not found.\r\nHeader with name 'PRICE5'[0] was not found.\r\nHeader with name 'CZ_SerNum_Track'[0] was not found.\r\nHeader with name 'CZ_SerNum_Delka'[0] was not found.\r\nHeader with name 'CZ_Rez1_Track'[0] was not found.\r\nHeader with name 'CZ_Rez2_Track'[0] was not found.\r\nHeader with name 'CZ_Rez3_Track'[0] was not found.\r\nHeader with name 'CZ_Rez4_Track'[0] was not found.\r\nHeader with name 'REZ1'[0] was not found.\r\nHeader with name 'REZ2'[0] was not found.\r\nHeader with name 'REZ3'[0] was not found.\r\nHeader with name 'REZ4'[0] was not found.\r\nHeader with name 'ODB_ID'[0] was not found.\r\nHeader with name 'mena_ID'[0] was not found.\r\nHeader with name 'SERLTNUM'[0] was not found.\r\nHeader with name 'WEIGHT'[0] was not found.\r\nHeader with name 'TIMEFROM'[0] was not found.\r\nHeader with name 'TIMETO'[0] was not found.\r\nHeader with name 'LSTMod'[0] was not found.\r\nHeader with name 'loginid'[0] was not found.\r\nHeader with name 'SKL_DESC'[0] was not found.\r\nHeaders: 'DEX_ROW_ID_Zbozi;Cislo polozky;Oznaceni polozky;Kod polozky;Car. kod dodavatele;Car. kod;Lokace;ID skladu;Mnozstvi;Mnozstvi baleni;Merna jednotka;Doporucena merna jednotka;Vyse DPH;Cenova hladina;Cenova hladina 1;Cenova hladina 2;Cenova hladina 3;Cenova hladina 4;Cenova hladina 5;Typ sledovani;Delka serioveho cisla;Doplnovat hodnotu REZ1;Doplnovat hodnotu REZ2;Doplnovat hodnotu REZ3;CZ_Rez4_Track;Doplnkova hodnota 1;Doplnkova hodnota 2;Doplnkova hodnota 3;Doplnkova hodnota 4;ID odberatele;mena_ID;Sarze;Vaha;TIMEFROM;TIMETO;LSTMod;loginid;VPrFVTS;VPrFPTS;VPrFDTS;VPrFITS;VPrFXTS;RefVPrFVTS;RefVPrFPTS;RefVPrFDTS;RefVPrFITS;RefVPrFXTS;VPrTIMEPREP;VPrTIMEUNIT;RefVPrTIMEMODE;DEX_ROW_ID_PARAMETRY;Nazev skladu;Typ polozky;Vetev 1;Vetev 2;Vetev 3;Vetev 4;Vetev 5;Vetev 6;Vetev 7;Typ polozky Text'\r\nIf you are expecting some headers to be missing and want to ignore this validation, set the configuration HeaderValidated to null. You can also change the functionality to do something else, like logging the issue.\r\n\r\nIReader state:\r\n   ColumnCount: 1\r\n   CurrentIndex: -1\r\n   HeaderRecord:\r\n[\"DEX_ROW_ID_Zbozi;Cislo polozky;Oznaceni polozky;Kod polozky;Car. kod dodavatele;Car. kod;Lokace;ID skladu;Mnozstvi;Mnozstvi baleni;Merna jednotka;Doporucena merna jednotka;Vyse DPH;Cenova hladina;Cenova hladina 1;Cenova hladina 2;Cenova hladina 3;Cenova hladina 4;Cenova hladina 5;Typ sledovani;Delka serioveho cisla;Doplnovat hodnotu REZ1;Doplnovat hodnotu REZ2;Doplnovat hodnotu REZ3;CZ_Rez4_Track;Doplnkova hodnota 1;Doplnkova hodnota 2;Doplnkova hodnota 3;Doplnkova hodnota 4;ID odberatele;mena_ID;Sarze;Vaha;TIMEFROM;TIMETO;LSTMod;loginid;VPrFVTS;VPrFPTS;VPrFDTS;VPrFITS;VPrFXTS;RefVPrFVTS;RefVPrFPTS;RefVPrFDTS;RefVPrFITS;RefVPrFXTS;VPrTIMEPREP;VPrTIMEUNIT;RefVPrTIMEMODE;DEX_ROW_ID_PARAMETRY;Nazev skladu;Typ polozky;Vetev 1;Vetev 2;Vetev 3;Vetev 4;Vetev 5;Vetev 6;Vetev 7;Typ polozky Text\"]\r\nIParser state:\r\n   ByteCount: 0\r\n   CharCount: 798\r\n   Row: 1\r\n   RawRow: 1\r\n   Count: 1\r\n   RawRecord:\r\nDEX_ROW_ID_Zbozi;Cislo polozky;Oznaceni polozky;Kod polozky;Car. kod dodavatele;Car. kod;Lokace;ID skladu;Mnozstvi;Mnozstvi baleni;Merna jednotka;Doporucena merna jednotka;Vyse DPH;Cenova hladina;Cenova hladina 1;Cenova hladina 2;Cenova hladina 3;Cenova hladina 4;Cenova hladina 5;Typ sledovani;Delka serioveho cisla;Doplnovat hodnotu REZ1;Doplnovat hodnotu REZ2;Doplnovat hodnotu REZ3;CZ_Rez4_Track;Doplnkova hodnota 1;Doplnkova hodnota 2;Doplnkova hodnota 3;Doplnkova hodnota 4;ID odberatele;mena_ID;Sarze;Vaha;TIMEFROM;TIMETO;LSTMod;loginid;VPrFVTS;VPrFPTS;VPrFDTS;VPrFITS;VPrFXTS;RefVPrFVTS;RefVPrFPTS;RefVPrFDTS;RefVPrFITS;RefVPrFXTS;VPrTIMEPREP;VPrTIMEUNIT;RefVPrTIMEMODE;DEX_ROW_ID_PARAMETRY;Nazev skladu;Typ polozky;Vetev 1;Vetev 2;Vetev 3;Vetev 4;Vetev 5;Vetev 6;Vetev 7;Typ polozky Text\r\n\r\n"}
                //                                                              // records = csv.GetRecords<FASK_ZASOBY_row>();


                //        //csv.Context.RegisterClassMap<FooMap>();
                //        //var records = csv.GetRecords<Foo>();
                //    }


                //}

                //var records22 = new List<FASK_ZASOBY_CSV00_row>();

                //var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                //{
                //    MissingFieldFound = null // Ignore missing fields
                //};


                //using (var reader = new StreamReader(path))
                //using (var csv = new CsvReader(reader, config))
                //{
                //    csv.Read();
                //    csv.ReadHeader();
                //    while (csv.Read())
                //    {
                //        var record = csv.GetRecord<FASK_ZASOBY_CSV00_row>();
                //        records22.Add(record);
                //    }
                //}

                //FASK_ZASOBY_CSV00_row


                //using (var reader = new StreamReader(path))
                //using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", MissingFieldFound = null }))
                //{
                //    csv.Read();
                //    csv.ReadHeader();
                //    var records33 = csv.GetRecords<FASK_ZASOBY_CSV00_row>().ToList();
                //    foreach (var record in records)
                //    {
                //        // Pracujte s načteným záznamem.
                //    }
                //}




                // Console.ReadKey();




                //using (var reader = new StreamReader(path))
                //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                //{
                //    csv.Read();
                //    csv.ReadHeader();
                //    while (csv.Read())
                //    {
                //        var record = csv.GetRecord<FASK_ZASOBY_CSV_row>();
                //        // Do something with the record.
                //    }
                //}
                #endregion


                //FASK_ZASOBY.DB_insert_FASK_ZASOBY(filePath, connectionString);


                //string filePath = "path_to_your_file.csv";
                //var dataList = new List<Zbozi>();

                //var lines = File.ReadAllLines(filePath);
                //foreach (var line in lines)
                //{
                //    var values = line.Split(',');

                //    var data = new MyData
                //    {
                //        // Předpokládejme, že první hodnota je Name a druhá je Value
                //        Name = values[0],
                //        Value = int.Parse(values[1])
                //    };

                //    dataList.Add(data);
                //}

                //using (var context = new MyDbContext())
                //{
                //    context.MyDatas.AddRange(dataList);
                //    context.SaveChanges();
                //}






                //// Přečte všechny řádky ze souboru CSV
                //string[] lines = System.IO.File.ReadAllLines(path);

                // Vyčistí existující řádky v DataGridView

                //dgv.Rows.Clear();// chyba!!!!!!! {"Tento seznam nelze vymazat."}

                //// Přidá každý řádek do DataGridView
                //foreach (string line in lines)
                //{
                //    // Rozdělí řádek podle čárky, abychom získali jednotlivé hodnoty
                //    string[] values = line.Split(',');

                //    // Přidá nový řádek do DataGridView s hodnotami z CSV
                //    dgv.Rows.Add(values);
                //}

                // Přečte všechny řádky ze souboru CSV
                string[] lines = System.IO.File.ReadAllLines(path);

                // Vytvoří nový DataTable pro ukládání dat z CSV
                DataTable dt = new DataTable();



                // Vytvoříme list pro ukládání hlaviček a odpovídajících atributů záznamu z DB
                var headerAttributes = new List<Tuple<string, string, int>>();
                List<string> headerParts = new List<string>();
                int iterace = 0;
                string[] headers = null;

                if (lines.Length < 2)
                    throw new Exception("Not enough lines to work on ... ");

                // Rozdělení řetězce podle středníků a uložení výsledků do List<string>
                headerParts = lines[0].Split(';').ToList();
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    string columnName = column.Name; // Získání názvu sloupce
                    string headerText = column.HeaderText; // Získání hlavičkového textu (popisu sloupce)

                    int i = 0;
                    foreach (string part in headerParts)
                    {
                        //if (headerText.Trim() == part.Trim())
                        //{
                        //    headerAttributes.Add(new Tuple<string, string>(part, columnName)); // Přidání hlavičky a odpovídajícího atributu záznamu do listu

                        //}

                        // Porovnání bez diakritiky
                        if (RemoveDiacritics(headerText.Trim()).Equals(RemoveDiacritics(part.Trim()), StringComparison.CurrentCultureIgnoreCase))
                        {
                            headerAttributes.Add(new Tuple<string, string, int>(part, columnName, i)); // Přidání hlavičky a odpovídajícího atributu záznamu do listu
                            Console.WriteLine(part);
                            break;
                        }
                        i++;
                    }
                }

                // Přidá sloupce do DataTable podle hlavičky CSV (pokud existuje)
                //string[] headers = lines[0].Split(',');
                for (int i = 1; i < lines.Length - 1; i++)
                {

                    //dt.Columns.Add(header);


                    //header - zde mam hlavičky a potrebuji je roztridit do List<string,[prirazeny atribut zaznamu z DB - prozatim vyplneno null, ale prvek je typu string]>
                    //List<string,[hodnota CSV, nazev atribut DB>
                    //.. data jsou oddelenym strednikem --xx;yy;vv
                    //naplnim list --xx;yy;vv

                    //rozpajsuj string header podle stredniku do List<string>

                    var z = new FASK_ZASOBY_ALL_row();
                  //  z.ITEMNMBR = headerAttributes
                }

                //kontrola potrebnych parametru z listu
                var x = headerAttributes;

                //vycet parametru tabulky ZASOBY
                //zde potrebuji nahled do tabulky ZASOBY a ziskat vsechny jeji parametry s nalinkovanymi hodnoty jako informaci, ze nesmi byt null
                //implementuj zde:
                //List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();


                #region kontrola atributu CSV vuci tabulce FASK_ZASOBY
                int pocetPovinnych = 0;
                int pocetShod = 0;


                foreach (var item in tableInfo)
                {
                    if (!item.Item3)
                    {
                        pocetPovinnych++;

                    }
                }


                foreach (var itemTabulky in tableInfo)
                {
                    if (!itemTabulky.Item3)
                    {
                        foreach (var item_CSV in headerAttributes)
                        {


                            if (itemTabulky.Item1 == item_CSV.Item2)
                            {
                                pocetShod++; 
                            }

                        }
                        

                    }
                }


                //zde odecitam 2hodnoty, protoze DEX_ROW_ID se dava automaticky a CZ_Expirace_Track se vyplnuje 0 v procedure
                if ((pocetPovinnych-2) == pocetShod)
                {
                    bool kontrola = true;
                    //uspech - kontrola je v poradku

                    //dotaz na smazani dat v tabulce FASK_ZASOBY!!! dodelat!!

                    //nahrani dat z CSV do tabulky FASK_ZASOBY
                    //csvData;

                }
                else
                {
                    //neuspech kontroly
                    return false;
                }

                #endregion


                

            }
            catch (Exception ex)
            {
                // Zpracuje výjimku
                MessageBox.Show($"Došlo k chybě: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        private static void ZapisDoFASK_ZASOBY(FASK_ZASOBY_CSV00_row record)
        {
            var coToJe = record;

            //validace dat, vse je ok?

            //vzit celej LIST zaznamu !!

            string nychystanoNaZapis = "metoda pro zapis dat do tabulky";

            //LIST zaznamu - udelat to jako transakci, bud vsechno nebo nic TODO MaR .. implementace 23.5.2024

            //smazat data ?? START --->>
            //--pristup do tabulky [FASK_ZASOBY], nasat vsechny zaznamy!!
            //zapnout paralelne proces pro cekani smazani dat z tabulky [FASK_ZASOBY]
            //--START proces cekani smazani dat z tabulky [FASK_ZASOBY]


            //insert nychystanoNaZapis do tabulky [FASK_ZASOBY] --> pokus transakce--> pokud se nezdari nebudou se MAZAT data z tabulky [FASK_ZASOBY]!!!
            StaraSkola(coToJe);

            //--END proces cekani smazani dat z tabulky [FASK_ZASOBY] ?? mohlo a bylo smazano? odpoved true/false
            //smazat dat ?? END <<---

        }

        public static List<Tuple<string, string>> ImportCsvData(string filePath)
        {
            List<Tuple<string, string>> csvData = new List<Tuple<string, string>>();

            try
            {
                // Načtení všech řádků z CSV souboru
                string[] lines = File.ReadAllLines(filePath);

                // Zpracování hlavičky (první řádek)
                string[] headers = lines[0].Split(';').Select(header => header.Trim('"')).ToArray();

                // Iterace přes zbylé řádky (data)
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataValues = lines[i].Split(';').Select(value => value.Trim('"')).ToArray();

                    // Pokud se počet hodnot liší od počtu hlaviček, přeskočit tento řádek
                    if (dataValues.Length != headers.Length)
                    {
                        Console.WriteLine($"Chybný řádek na řádku {i + 1}. Počet hodnot neodpovídá počtu hlaviček.");
                        continue;
                    }

                    // Vytvoření Tuple pro každý pár hlavička-data a přidání do Listu
                    for (int j = 0; j < headers.Length; j++)
                    {
                        csvData.Add(new Tuple<string, string>(headers[j], dataValues[j]));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při importu dat z CSV souboru: {ex.Message}");
            }

            return csvData;
        }

        // Metoda pro odstranění diakritiky ze stringu
        public static string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }



        #region prevzato stary kod, nasel atd.

        private static void StaraSkola(FASK_ZASOBY_CSV00_row record)
        {
            try
            {

                //TODO ulozeni editace/vlozeni...
                #region Insert
                if (record != null)
                {
                    Fask.Interfaces.DataSets.Zbozi ds2 = new Fask.Interfaces.DataSets.Zbozi();


                    #region FASK_ZASOBY

                    #region MaR dilo 23.5.2024

                    Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow newZboziRow = ds2.FASK_ZASOBY_KONZOLA.NewFASK_ZASOBY_KONZOLARow();

                    newZboziRow.ITEMNMBR = /*TextBox_ITEMNMBR.Text.Trim();*/ record.ITEMNMBR;
                    newZboziRow.ITEMDESC = /*string.IsNullOrEmpty(TextBox_ITEMDESC.Text.Trim()) ? string.Empty : TextBox_ITEMDESC.Text.Trim();*/record.ITEMDESC;
                    newZboziRow.ITEMCODE = /*string.IsNullOrEmpty(TextBox_ITEMCODE.Text.Trim()) ? string.Empty : TextBox_ITEMCODE.Text.Trim();*/record.ITEMCODE;

                    newZboziRow.VNDITNUM = /*string.IsNullOrEmpty(TextBox_VNDITNUM.Text.Trim()) ? string.Empty : TextBox_VNDITNUM.Text.Trim();*/ record.VNDITNUM;
                    newZboziRow.CZ_CarKod = /*string.IsNullOrEmpty(TextBox_CZ_CarKod.Text.Trim()) ? string.Empty : TextBox_CZ_CarKod.Text.Trim();*/ record.CZ_CarKod;

                    newZboziRow.LOCNCODE = /*string.IsNullOrEmpty(TextBox_LOCNCODE.Text.Trim()) ? string.Empty : TextBox_LOCNCODE.Text.Trim();*/ record.LOCNCODE;
                    newZboziRow.SKL_ID =/* string.IsNullOrEmpty(TextBox_SKL_ID.Text.Trim()) ? null : TextBox_SKL_ID.Text.Trim();*/ record.SKL_ID;

                    newZboziRow.QTY = /*decimal.Parse(TextBox_QTY.Text.Trim());*/ record.QTY;

                    if (/*string.IsNullOrEmpty(TextBox_QTYPACK.Text.Trim())*/!record.QTYPACK.HasValue)
                        newZboziRow.QTYPACK = 0;
                    else
                        newZboziRow.QTYPACK = decimal.Parse(/*TextBox_QTYPACK.Text.Trim()*/record.QTYPACK.ToString());



                    newZboziRow.MJ = /*TextBox_MJ.Text.Trim();*/ record.MJ;
                    newZboziRow.DMJ = /*TextBox_DMJ.Text.Trim();*/ record.DMJ;

                    if (/*string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim())*/!record.TAXRATE.HasValue)
                        newZboziRow.SetTAXRATENull();
                    else
                        newZboziRow.TAXRATE = decimal.Parse(record.TAXRATE.ToString());


                    if (/*string.IsNullOrEmpty(TextBox_PRICE0.Text.Trim())*/!record.PRICE0.HasValue)
                        newZboziRow.PRICE0 = 0;
                    else
                        newZboziRow.PRICE0 = /*decimal.Parse(TextBox_PRICE0.Text.Trim());*/record.PRICE0.Value;

                    if (/*string.IsNullOrEmpty(TextBox_PRICE1.Text.Trim())*/!record.PRICE1.HasValue)
                        newZboziRow.PRICE1 = 0;
                    else
                        newZboziRow.PRICE1 = /*decimal.Parse(TextBox_PRICE1.Text.Trim());*/record.PRICE1.Value;

                    if (/*string.IsNullOrEmpty(TextBox_PRICE2.Text.Trim())*/!record.PRICE2.HasValue)
                        newZboziRow.PRICE2 = 0;
                    else
                        newZboziRow.PRICE2 = /*decimal.Parse(TextBox_PRICE2.Text.Trim());*/record.PRICE2.Value;

                    if (/*string.IsNullOrEmpty(TextBox_PRICE3.Text.Trim())*/!record.PRICE3.HasValue)
                        newZboziRow.PRICE3 = 0;
                    else
                        newZboziRow.PRICE3 = /*decimal.Parse(TextBox_PRICE3.Text.Trim());*/record.PRICE3.Value;

                    if (/*string.IsNullOrEmpty(TextBox_PRICE4.Text.Trim())*/!record.PRICE4.HasValue)
                        newZboziRow.PRICE4 = 0;
                    else
                        newZboziRow.PRICE4 = /*decimal.Parse(TextBox_PRICE4.Text.Trim());*/record.PRICE4.Value;

                    if (/*string.IsNullOrEmpty(TextBox_PRICE5.Text.Trim())*/!record.PRICE5.HasValue)
                        newZboziRow.PRICE5 = 0;
                    else
                        newZboziRow.PRICE5 = /*decimal.Parse(TextBox_PRICE5.Text.Trim());*/record.PRICE5.Value;


                    newZboziRow.CZ_SerNum_Track = /*byte.Parse(TextBox_CZ_SerNum_Track.Text.Trim());*/record.CZ_SerNum_Track;
                    newZboziRow.CZ_SerNum_Delka = /*short.Parse(TextBox_CZ_SerNum_Delka.Text.Trim());*/record.CZ_SerNum_Delka;

                    newZboziRow.CZ_Rez1_Track = /*byte.Parse(TextBox_CZ_Rez1_Track.Text.Trim());*/record.CZ_Rez1_Track;
                    newZboziRow.CZ_Rez2_Track = /*byte.Parse(TextBox_CZ_Rez2_Track.Text.Trim());*/ record.CZ_Rez2_Track;
                    newZboziRow.CZ_Rez3_Track = /*byte.Parse(TextBox_CZ_Rez3_Track.Text.Trim());*/ record.CZ_Rez3_Track;
                    newZboziRow.CZ_Rez4_Track = /*byte.Parse(TextBox_CZ_Rez4_Track.Text.Trim());*/ record.CZ_Rez4_Track;

                    newZboziRow.REZ1 = string.IsNullOrEmpty(record.REZ1) ? "0" : record.REZ1.Trim();
                    newZboziRow.REZ2 = string.IsNullOrEmpty(record.REZ2) ? "0" : record.REZ2.Trim();
                    newZboziRow.REZ3 = string.IsNullOrEmpty(record.REZ3) ? "0" : record.REZ3.Trim();
                    newZboziRow.REZ4 = string.IsNullOrEmpty(record.REZ4) ? "0" : record.REZ4.Trim();


                    newZboziRow.ODB_ID = string.IsNullOrEmpty(record.ODB_ID) ? null : record.ODB_ID.Trim();
                    //newZboziRow.mena_ID = string.IsNullOrEmpty(TextBox_loginid.Text.Trim()) ? null : TextBox_loginid.Text.Trim();
                    newZboziRow.SERLTNUM = string.IsNullOrEmpty(record.SERLTNUM) ? string.Empty : record.SERLTNUM.Trim();

                    if (!record.WEIGHT.HasValue)
                        newZboziRow.SetWEIGHTNull();
                    else
                        newZboziRow.WEIGHT = /*decimal.Parse(TextBox_WEIGHT.Text.Trim()); */record.WEIGHT.Value;

                    bool neco = false;
                    if (/*DTP_TIMEFROM.Checked*/ neco)
                        newZboziRow.TIMEFROM =/* DTP_TIMEFROM.Value; */record.TIMEFROM.HasValue ? record.TIMEFROM.Value : DateTime.Now;
                    else
                        newZboziRow.SetTIMEFROMNull();


                    if (/*DTP_TIMETO.Checked*/ neco)
                        newZboziRow.TIMETO = /*DTP_TIMETO.Value;*/ record.TIMETO.HasValue ? record.TIMETO.Value : DateTime.Now;
                    else
                        newZboziRow.SetTIMETONull();

                    newZboziRow.LSTMod = DateTime.Now;

                    newZboziRow.loginid = string.IsNullOrEmpty(record.loginid) ? string.Empty : record.loginid.Trim();

                    #region INSERT IMPLEMENTACE - NEDOKONCENO !!
                    //MaR zmeny
                    ds2.FASK_ZASOBY_KONZOLA.AddFASK_ZASOBY_KONZOLARow(newZboziRow);







                    if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZbozi))
                        ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZbozi)providerZbozi).InsertZbozi(newZboziRow);
                    else
                        throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_InsertZbozi");






                    #endregion
                    #endregion



                    #region REIMPLEMENTACE MaR TODO 23.5.2024
                    //Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow newZboziRow = ds2.FASK_ZASOBY_KONZOLA.NewFASK_ZASOBY_KONZOLARow();

                    //newZboziRow.ITEMNMBR = TextBox_ITEMNMBR.Text.Trim();
                    //newZboziRow.ITEMDESC = string.IsNullOrEmpty(TextBox_ITEMDESC.Text.Trim()) ? string.Empty : TextBox_ITEMDESC.Text.Trim();
                    //newZboziRow.ITEMCODE = string.IsNullOrEmpty(TextBox_ITEMCODE.Text.Trim()) ? string.Empty : TextBox_ITEMCODE.Text.Trim();

                    //newZboziRow.VNDITNUM = string.IsNullOrEmpty(TextBox_VNDITNUM.Text.Trim()) ? string.Empty : TextBox_VNDITNUM.Text.Trim();
                    //newZboziRow.CZ_CarKod = string.IsNullOrEmpty(TextBox_CZ_CarKod.Text.Trim()) ? string.Empty : TextBox_CZ_CarKod.Text.Trim();

                    //newZboziRow.LOCNCODE = string.IsNullOrEmpty(TextBox_LOCNCODE.Text.Trim()) ? string.Empty : TextBox_LOCNCODE.Text.Trim();
                    //newZboziRow.SKL_ID = string.IsNullOrEmpty(TextBox_SKL_ID.Text.Trim()) ? null : TextBox_SKL_ID.Text.Trim();

                    //newZboziRow.QTY = decimal.Parse(TextBox_QTY.Text.Trim());

                    //if (string.IsNullOrEmpty(TextBox_QTYPACK.Text.Trim()))
                    //    newZboziRow.QTYPACK = 0;
                    //else
                    //    newZboziRow.QTYPACK = decimal.Parse(TextBox_QTYPACK.Text.Trim());



                    //newZboziRow.MJ = TextBox_MJ.Text.Trim();
                    //newZboziRow.DMJ = TextBox_DMJ.Text.Trim();

                    //if (string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim()))
                    //    newZboziRow.SetTAXRATENull();
                    //else
                    //    newZboziRow.TAXRATE = decimal.Parse(TextBox_TAXRATE.Text.Trim());


                    //if (string.IsNullOrEmpty(TextBox_PRICE0.Text.Trim()))
                    //    newZboziRow.PRICE0 = 0;
                    //else
                    //    newZboziRow.PRICE0 = decimal.Parse(TextBox_PRICE0.Text.Trim());

                    //if (string.IsNullOrEmpty(TextBox_PRICE1.Text.Trim()))
                    //    newZboziRow.PRICE1 = 0;
                    //else
                    //    newZboziRow.PRICE1 = decimal.Parse(TextBox_PRICE1.Text.Trim());

                    //if (string.IsNullOrEmpty(TextBox_PRICE2.Text.Trim()))
                    //    newZboziRow.PRICE2 = 0;
                    //else
                    //    newZboziRow.PRICE2 = decimal.Parse(TextBox_PRICE2.Text.Trim());

                    //if (string.IsNullOrEmpty(TextBox_PRICE3.Text.Trim()))
                    //    newZboziRow.PRICE3 = 0;
                    //else
                    //    newZboziRow.PRICE3 = decimal.Parse(TextBox_PRICE3.Text.Trim());

                    //if (string.IsNullOrEmpty(TextBox_PRICE4.Text.Trim()))
                    //    newZboziRow.PRICE4 = 0;
                    //else
                    //    newZboziRow.PRICE4 = decimal.Parse(TextBox_PRICE4.Text.Trim());

                    //if (string.IsNullOrEmpty(TextBox_PRICE5.Text.Trim()))
                    //    newZboziRow.PRICE5 = 0;
                    //else
                    //    newZboziRow.PRICE5 = decimal.Parse(TextBox_PRICE5.Text.Trim());


                    //newZboziRow.CZ_SerNum_Track = byte.Parse(TextBox_CZ_SerNum_Track.Text.Trim());
                    //newZboziRow.CZ_SerNum_Delka = short.Parse(TextBox_CZ_SerNum_Delka.Text.Trim());

                    //newZboziRow.CZ_Rez1_Track = byte.Parse(TextBox_CZ_Rez1_Track.Text.Trim());
                    //newZboziRow.CZ_Rez2_Track = byte.Parse(TextBox_CZ_Rez2_Track.Text.Trim());
                    //newZboziRow.CZ_Rez3_Track = byte.Parse(TextBox_CZ_Rez3_Track.Text.Trim());
                    //newZboziRow.CZ_Rez4_Track = byte.Parse(TextBox_CZ_Rez4_Track.Text.Trim());

                    //newZboziRow.REZ1 = string.IsNullOrEmpty(TextBox_REZ1.Text.Trim()) ? "0" : TextBox_REZ1.Text.Trim();
                    //newZboziRow.REZ2 = string.IsNullOrEmpty(TextBox_REZ2.Text.Trim()) ? "0" : TextBox_REZ2.Text.Trim();
                    //newZboziRow.REZ3 = string.IsNullOrEmpty(TextBox_REZ3.Text.Trim()) ? "0" : TextBox_REZ3.Text.Trim();
                    //newZboziRow.REZ4 = string.IsNullOrEmpty(TextBox_REZ4.Text.Trim()) ? "0" : TextBox_REZ4.Text.Trim();


                    //newZboziRow.ODB_ID = string.IsNullOrEmpty(TextBox_ODB_ID.Text.Trim()) ? null : TextBox_ODB_ID.Text.Trim();
                    ////newZboziRow.mena_ID = string.IsNullOrEmpty(TextBox_loginid.Text.Trim()) ? null : TextBox_loginid.Text.Trim();
                    //newZboziRow.SERLTNUM = string.IsNullOrEmpty(TextBox_SERLTNUM.Text.Trim()) ? string.Empty : TextBox_SERLTNUM.Text.Trim();

                    //if (string.IsNullOrEmpty(TextBox_WEIGHT.Text.Trim()))
                    //    newZboziRow.SetWEIGHTNull();
                    //else
                    //    newZboziRow.WEIGHT = decimal.Parse(TextBox_WEIGHT.Text.Trim());


                    //if (DTP_TIMEFROM.Checked)
                    //    newZboziRow.TIMEFROM = DTP_TIMEFROM.Value;
                    //else
                    //    newZboziRow.SetTIMEFROMNull();

                    //if (DTP_TIMETO.Checked)
                    //    newZboziRow.TIMETO = DTP_TIMETO.Value;
                    //else
                    //    newZboziRow.SetTIMETONull();

                    //newZboziRow.LSTMod = DateTime.Now;

                    //newZboziRow.loginid = string.IsNullOrEmpty(TextBox_loginid.Text.Trim()) ? string.Empty : TextBox_loginid.Text.Trim();

                    ////MaR zmeny
                    //ds2.FASK_ZASOBY_KONZOLA.AddFASK_ZASOBY_KONZOLARow(newZboziRow);

                    //if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZbozi))
                    //    ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZbozi)providerZbozi).InsertZbozi(newZboziRow);
                    //else
                    //    throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_InsertZbozi"); 
                    #endregion

                    #endregion

                    #region FASK_ZASOBY_PARAMETRY

                    #region Porovnani def a aktual

                    #region NEPOTREBNE ???
                    //bool RefVPrFVTS_default_tmp = false;
                    //bool RefVPrFPTS_default_tmp = false;
                    //bool RefVPrFDTS_default_tmp = false;
                    //bool RefVPrFITS_default_tmp = false;
                    //bool RefVPrFXTS_default_tmp = false;

                    //RefVPrFVTS_default_tmp = (ComboBox_RefVPrFVTS.SelectedItem != (!RefVPrFVTS_default.HasValue ? (object)string.Empty : RefVPrFVTS_default.Value));
                    //RefVPrFPTS_default_tmp = (ComboBox_RefVPrFPTS.SelectedItem != (!RefVPrFPTS_default.HasValue ? (object)string.Empty : RefVPrFPTS_default.Value));
                    //RefVPrFDTS_default_tmp = (ComboBox_RefVPrFDTS.SelectedItem != (!RefVPrFDTS_default.HasValue ? (object)string.Empty : RefVPrFDTS_default.Value));
                    //RefVPrFITS_default_tmp = (ComboBox_RefVPrFITS.SelectedItem != (!RefVPrFITS_default.HasValue ? (object)string.Empty : RefVPrFITS_default.Value));
                    //RefVPrFXTS_default_tmp = (ComboBox_RefVPrFXTS.SelectedItem != (!RefVPrFXTS_default.HasValue ? (object)string.Empty : RefVPrFXTS_default.Value)); 
                    #endregion


                    #endregion


                    #region NEPOTREBNE ???
                    //if (
                    //    CheckBox_VPrFVTS.Checked != VPrFVTS_default ||
                    //    CheckBox_VPrFPTS.Checked != VPrFPTS_default ||
                    //    CheckBox_VPrFDTS.Checked != VPrFDTS_default ||
                    //    CheckBox_VPrFITS.Checked != VPrFITS_default ||
                    //    CheckBox_VPrFXTS.Checked != VPrFXTS_default ||
                    //    RefVPrFVTS_default_tmp ||
                    //    RefVPrFPTS_default_tmp ||
                    //    RefVPrFDTS_default_tmp ||
                    //    RefVPrFITS_default_tmp ||
                    //    RefVPrFXTS_default_tmp
                    //    )
                    //{

                    //    Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_PARAMETRY_KONZOLARow newZboziParamRow = ds2.FASK_ZASOBY_PARAMETRY_KONZOLA.NewFASK_ZASOBY_PARAMETRY_KONZOLARow();

                    //    //newZboziParamRow.DEX_ROW_ID;
                    //    newZboziParamRow.ITEMNMBR = TextBox_ITEMNMBR.Text.Trim();

                    //    newZboziParamRow.VPrFVTS = CheckBox_VPrFVTS.Checked;
                    //    newZboziParamRow.VPrFPTS = CheckBox_VPrFPTS.Checked;
                    //    newZboziParamRow.VPrFDTS = CheckBox_VPrFDTS.Checked;
                    //    newZboziParamRow.VPrFITS = CheckBox_VPrFITS.Checked;
                    //    newZboziParamRow.VPrFXTS = CheckBox_VPrFXTS.Checked;

                    //    if ((ComboBox_RefVPrFVTS.SelectedItem != null) && (ComboBox_RefVPrFVTS.SelectedItem is FXTS))
                    //        newZboziParamRow.RefVPrFVTS = (int)ComboBox_RefVPrFVTS.SelectedItem;
                    //    else
                    //        newZboziParamRow.SetRefVPrFVTSNull();

                    //    if ((ComboBox_RefVPrFPTS.SelectedItem != null) && (ComboBox_RefVPrFPTS.SelectedItem is FXTS))
                    //        newZboziParamRow.RefVPrFPTS = (int)ComboBox_RefVPrFPTS.SelectedItem;
                    //    else
                    //        newZboziParamRow.SetRefVPrFPTSNull();


                    //    if ((ComboBox_RefVPrFDTS.SelectedItem != null) && (ComboBox_RefVPrFDTS.SelectedItem is FXTS))
                    //        newZboziParamRow.RefVPrFDTS = (int)ComboBox_RefVPrFDTS.SelectedItem;
                    //    else
                    //        newZboziParamRow.SetRefVPrFDTSNull();

                    //    if ((ComboBox_RefVPrFITS.SelectedItem != null) && (ComboBox_RefVPrFITS.SelectedItem is FXTS))
                    //        newZboziParamRow.RefVPrFITS = (int)ComboBox_RefVPrFITS.SelectedItem;
                    //    else
                    //        newZboziParamRow.SetRefVPrFITSNull();

                    //    if ((ComboBox_RefVPrFXTS.SelectedItem != null) && (ComboBox_RefVPrFXTS.SelectedItem is FXTS))
                    //        newZboziParamRow.RefVPrFXTS = (int)ComboBox_RefVPrFXTS.SelectedItem;
                    //    else
                    //        newZboziParamRow.SetRefVPrFXTSNull();


                    //    if (string.IsNullOrEmpty(TextBox_VPrTIMEPREP.Text.Trim()))
                    //        newZboziParamRow.SetVPrTIMEPREPNull();
                    //    else
                    //        newZboziParamRow.VPrTIMEPREP = double.Parse(TextBox_VPrTIMEPREP.Text.Trim());

                    //    if (string.IsNullOrEmpty(TextBox_VPrTIMEUNIT.Text.Trim()))
                    //        newZboziParamRow.SetVPrTIMEUNITNull();
                    //    else
                    //        newZboziParamRow.VPrTIMEUNIT = double.Parse(TextBox_VPrTIMEUNIT.Text.Trim());

                    //    if ((ComboBox_RefVPrTIMEMODE.SelectedItem != null) && (ComboBox_RefVPrTIMEMODE.SelectedItem is TypySledovaniVyroba))
                    //        newZboziParamRow.RefVPrTIMEMODE = (int)ComboBox_RefVPrTIMEMODE.SelectedItem;
                    //    else
                    //        newZboziParamRow.SetRefVPrTIMEMODENull();


                    //    if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZboziParams))
                    //        ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZboziParams)providerZbozi).InsertZboziParams(newZboziParamRow);
                    //    else
                    //        throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_InsertZboziParams");


                    //} 
                    #endregion
                    #endregion

                }
                #endregion
                #region Update
                //else
                //{

                //    #region FASK_ZASOBY

                //    rowZboziEdit.ITEMNMBR = TextBox_ITEMNMBR.Text.Trim();
                //    rowZboziEdit.ITEMDESC = string.IsNullOrEmpty(TextBox_ITEMDESC.Text) ? string.Empty : TextBox_ITEMDESC.Text.Trim();
                //    rowZboziEdit.ITEMCODE = string.IsNullOrEmpty(TextBox_ITEMCODE.Text.Trim()) ? string.Empty : TextBox_ITEMCODE.Text.Trim();

                //    rowZboziEdit.VNDITNUM = string.IsNullOrEmpty(TextBox_VNDITNUM.Text) ? string.Empty : TextBox_VNDITNUM.Text.Trim();
                //    rowZboziEdit.CZ_CarKod = string.IsNullOrEmpty(TextBox_CZ_CarKod.Text) ? string.Empty : TextBox_CZ_CarKod.Text.Trim();
                //    rowZboziEdit.LOCNCODE = string.IsNullOrEmpty(TextBox_LOCNCODE.Text) ? string.Empty : TextBox_LOCNCODE.Text.Trim();
                //    rowZboziEdit.SKL_ID = string.IsNullOrEmpty(TextBox_SKL_ID.Text) ? null : TextBox_SKL_ID.Text.Trim();
                //    rowZboziEdit.QTY = decimal.Parse(TextBox_QTY.Text.Trim());

                //    if (string.IsNullOrEmpty(TextBox_QTYPACK.Text))
                //        rowZboziEdit.SetQTYPACKNull();
                //    else
                //        rowZboziEdit.QTYPACK = decimal.Parse(TextBox_QTYPACK.Text.Trim());

                //    rowZboziEdit.MJ = TextBox_MJ.Text.Trim();
                //    rowZboziEdit.DMJ = TextBox_DMJ.Text.Trim();

                //    if (string.IsNullOrEmpty(TextBox_TAXRATE.Text.Trim()))
                //        rowZboziEdit.SetTAXRATENull();
                //    else
                //        rowZboziEdit.TAXRATE = decimal.Parse(TextBox_TAXRATE.Text.Trim());


                //    if (string.IsNullOrEmpty(TextBox_PRICE0.Text.Trim()))
                //        rowZboziEdit.SetPRICE0Null();
                //    else
                //        rowZboziEdit.PRICE0 = decimal.Parse(TextBox_PRICE0.Text.Trim());

                //    if (string.IsNullOrEmpty(TextBox_PRICE1.Text.Trim()))
                //        rowZboziEdit.SetPRICE1Null();
                //    else
                //        rowZboziEdit.PRICE1 = decimal.Parse(TextBox_PRICE1.Text.Trim());

                //    if (string.IsNullOrEmpty(TextBox_PRICE2.Text.Trim()))
                //        rowZboziEdit.SetPRICE2Null();
                //    else
                //        rowZboziEdit.PRICE2 = decimal.Parse(TextBox_PRICE2.Text.Trim());

                //    if (string.IsNullOrEmpty(TextBox_PRICE3.Text.Trim()))
                //        rowZboziEdit.SetPRICE3Null();
                //    else
                //        rowZboziEdit.PRICE3 = decimal.Parse(TextBox_PRICE3.Text.Trim());

                //    if (string.IsNullOrEmpty(TextBox_PRICE4.Text.Trim()))
                //        rowZboziEdit.SetPRICE4Null();
                //    else
                //        rowZboziEdit.PRICE4 = decimal.Parse(TextBox_PRICE4.Text.Trim());

                //    if (string.IsNullOrEmpty(TextBox_PRICE5.Text.Trim()))
                //        rowZboziEdit.SetPRICE5Null();
                //    else
                //        rowZboziEdit.PRICE5 = decimal.Parse(TextBox_PRICE5.Text.Trim());


                //    rowZboziEdit.CZ_SerNum_Track = byte.Parse(TextBox_CZ_SerNum_Track.Text.Trim());
                //    rowZboziEdit.CZ_SerNum_Delka = short.Parse(TextBox_CZ_SerNum_Delka.Text.Trim());
                //    rowZboziEdit.CZ_Rez1_Track = byte.Parse(TextBox_CZ_Rez1_Track.Text.Trim());
                //    rowZboziEdit.CZ_Rez2_Track = byte.Parse(TextBox_CZ_Rez2_Track.Text.Trim());
                //    rowZboziEdit.CZ_Rez3_Track = byte.Parse(TextBox_CZ_Rez3_Track.Text.Trim());
                //    rowZboziEdit.CZ_Rez4_Track = byte.Parse(TextBox_CZ_Rez4_Track.Text.Trim());
                //    rowZboziEdit.REZ1 = string.IsNullOrEmpty(TextBox_REZ1.Text.Trim()) ? null : TextBox_REZ1.Text.Trim();
                //    rowZboziEdit.REZ2 = string.IsNullOrEmpty(TextBox_REZ2.Text.Trim()) ? null : TextBox_REZ2.Text.Trim();
                //    rowZboziEdit.REZ3 = string.IsNullOrEmpty(TextBox_REZ3.Text.Trim()) ? null : TextBox_REZ3.Text.Trim();
                //    rowZboziEdit.REZ4 = string.IsNullOrEmpty(TextBox_REZ4.Text.Trim()) ? null : TextBox_REZ4.Text.Trim();

                //    rowZboziEdit.ODB_ID = string.IsNullOrEmpty(TextBox_ODB_ID.Text.Trim()) ? null : TextBox_ODB_ID.Text.Trim();
                //    //rowZboziEdit.mena_ID = string.IsNullOrEmpty(TextBox_loginid.Text.Trim()) ? null : TextBox_loginid.Text.Trim();
                //    rowZboziEdit.SERLTNUM = string.IsNullOrEmpty(TextBox_SERLTNUM.Text.Trim()) ? null : TextBox_SERLTNUM.Text.Trim();


                //    if (string.IsNullOrEmpty(TextBox_WEIGHT.Text.Trim()))
                //        rowZboziEdit.SetWEIGHTNull();
                //    else
                //        rowZboziEdit.WEIGHT = decimal.Parse(TextBox_WEIGHT.Text.Trim());

                //    if (DTP_TIMEFROM.Checked)
                //        rowZboziEdit.TIMEFROM = DTP_TIMEFROM.Value;
                //    else
                //        rowZboziEdit.SetTIMEFROMNull();

                //    if (DTP_TIMETO.Checked)
                //        rowZboziEdit.TIMETO = DTP_TIMETO.Value;
                //    else
                //        rowZboziEdit.SetTIMETONull();

                //    rowZboziEdit.LSTMod = DateTime.Now;

                //    rowZboziEdit.loginid = string.IsNullOrEmpty(TextBox_loginid.Text.Trim()) ? string.Empty : TextBox_loginid.Text.Trim();


                //    if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZbozi))
                //        ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZbozi)providerZbozi).UpdateZbozi(rowZboziEdit);
                //    else
                //        throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_UpdateZbozi");

                //    #endregion

                //    #region FASK_ZASOBY_PARAMETRY

                //    #region Porovnani def a aktual

                //    bool RefVPrFVTS_default_tmp = false;
                //    bool RefVPrFPTS_default_tmp = false;
                //    bool RefVPrFDTS_default_tmp = false;
                //    bool RefVPrFITS_default_tmp = false;
                //    bool RefVPrFXTS_default_tmp = false;

                //    RefVPrFVTS_default_tmp = (ComboBox_RefVPrFVTS.SelectedItem != (!RefVPrFVTS_default.HasValue ? (object)string.Empty : RefVPrFVTS_default.Value));
                //    RefVPrFPTS_default_tmp = (ComboBox_RefVPrFPTS.SelectedItem != (!RefVPrFPTS_default.HasValue ? (object)string.Empty : RefVPrFPTS_default.Value));
                //    RefVPrFDTS_default_tmp = (ComboBox_RefVPrFDTS.SelectedItem != (!RefVPrFDTS_default.HasValue ? (object)string.Empty : RefVPrFDTS_default.Value));
                //    RefVPrFITS_default_tmp = (ComboBox_RefVPrFITS.SelectedItem != (!RefVPrFITS_default.HasValue ? (object)string.Empty : RefVPrFITS_default.Value));
                //    RefVPrFXTS_default_tmp = (ComboBox_RefVPrFXTS.SelectedItem != (!RefVPrFXTS_default.HasValue ? (object)string.Empty : RefVPrFXTS_default.Value));


                //    #endregion


                //    if (
                //        CheckBox_VPrFVTS.Checked != VPrFVTS_default ||
                //        CheckBox_VPrFPTS.Checked != VPrFPTS_default ||
                //        CheckBox_VPrFDTS.Checked != VPrFDTS_default ||
                //        CheckBox_VPrFITS.Checked != VPrFITS_default ||
                //        CheckBox_VPrFXTS.Checked != VPrFXTS_default ||
                //        RefVPrFVTS_default_tmp ||
                //        RefVPrFPTS_default_tmp ||
                //        RefVPrFDTS_default_tmp ||
                //        RefVPrFITS_default_tmp ||
                //        RefVPrFXTS_default_tmp
                //        )
                //    {


                //        //newZboziParamRow.DEX_ROW_ID;
                //        //rowZboziEdit.ITEMNMBR = TextBox_ITEMNMBR.Text.Trim();

                //        rowZboziEdit.VPrFVTS = CheckBox_VPrFVTS.Checked;
                //        rowZboziEdit.VPrFPTS = CheckBox_VPrFPTS.Checked;
                //        rowZboziEdit.VPrFDTS = CheckBox_VPrFDTS.Checked;
                //        rowZboziEdit.VPrFITS = CheckBox_VPrFITS.Checked;
                //        rowZboziEdit.VPrFXTS = CheckBox_VPrFXTS.Checked;

                //        if ((ComboBox_RefVPrFVTS.SelectedItem != null) && (ComboBox_RefVPrFVTS.SelectedItem is FXTS))
                //            rowZboziEdit.RefVPrFVTS = (int)ComboBox_RefVPrFVTS.SelectedItem;
                //        else
                //            rowZboziEdit.SetRefVPrFVTSNull();


                //        if ((ComboBox_RefVPrFPTS.SelectedItem != null) && (ComboBox_RefVPrFPTS.SelectedItem is FXTS))
                //            rowZboziEdit.RefVPrFPTS = (int)ComboBox_RefVPrFPTS.SelectedItem;
                //        else
                //            rowZboziEdit.SetRefVPrFPTSNull();


                //        if ((ComboBox_RefVPrFDTS.SelectedItem != null) && (ComboBox_RefVPrFDTS.SelectedItem is FXTS))
                //            rowZboziEdit.RefVPrFDTS = (int)ComboBox_RefVPrFDTS.SelectedItem;
                //        else
                //            rowZboziEdit.SetRefVPrFDTSNull();


                //        if ((ComboBox_RefVPrFITS.SelectedItem != null) && (ComboBox_RefVPrFITS.SelectedItem is FXTS))
                //            rowZboziEdit.RefVPrFITS = (int)ComboBox_RefVPrFITS.SelectedItem;
                //        else
                //            rowZboziEdit.SetRefVPrFITSNull();


                //        if ((ComboBox_RefVPrFXTS.SelectedItem != null) && (ComboBox_RefVPrFXTS.SelectedItem is FXTS))
                //            rowZboziEdit.RefVPrFXTS = (int)ComboBox_RefVPrFXTS.SelectedItem;
                //        else
                //            rowZboziEdit.SetRefVPrFXTSNull();


                //        if (string.IsNullOrEmpty(TextBox_VPrTIMEPREP.Text.Trim()))
                //            rowZboziEdit.SetVPrTIMEPREPNull();
                //        else
                //            rowZboziEdit.VPrTIMEPREP = double.Parse(TextBox_VPrTIMEPREP.Text.Trim());

                //        if (string.IsNullOrEmpty(TextBox_VPrTIMEUNIT.Text.Trim()))
                //            rowZboziEdit.SetVPrTIMEUNITNull();
                //        else
                //            rowZboziEdit.VPrTIMEUNIT = double.Parse(TextBox_VPrTIMEUNIT.Text.Trim());


                //        if ((ComboBox_RefVPrTIMEMODE.SelectedItem != null) && (ComboBox_RefVPrTIMEMODE.SelectedItem is TypySledovaniVyroba))
                //            rowZboziEdit.RefVPrTIMEMODE = (int)ComboBox_RefVPrTIMEMODE.SelectedItem;
                //        else
                //            rowZboziEdit.SetRefVPrTIMEMODENull();


                //        if ((providerZbozi != null) && (providerZbozi is Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZboziParams))
                //            ((Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZboziParams)providerZbozi).UpdateZboziParams(rowZboziEdit);
                //        else
                //            throw new NotImplementedException("Provider neobsahuje implemetaci IZbozi2_UpdateZboziParams");
                //    }
                //    #endregion


                //}
                #endregion



            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                  // Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion



        #endregion

        #endregion

        private static  void DT_To_CSV(this DataTable dt ,string Path, string separator) 
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName);

            List<string> columnNamesBezDiakritiky = new List<string>();
            foreach (var item in columnNames)
            {
                columnNamesBezDiakritiky.Add(RemoveDiacritism(item));
            }

            sb.AppendLine(string.Join(separator, columnNamesBezDiakritiky));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field =>
                  string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                sb.AppendLine(string.Join(separator, fields));
            }

            File.WriteAllText(Path, sb.ToString());
        }

        #endregion

        #region Export to Excel
        private static DialogResult showPathDialog_Excel(out string filepath)
        {
            filepath = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = ".xlsx Files (*.xlsx)|*.xlsx";
            sfd.InitialDirectory = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyPath;
            sfd.FileName = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyExcelFileName;

            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyPath = Path.GetDirectoryName(sfd.FileName);
                //Settings.ExportyExcelFileName = Path.GetFileNameWithoutExtension(sfd.FileName);
                filepath = sfd.FileName;
            }

            return dr;
        }

        public static bool ExportToExcel(this Zuby.ADGV.AdvancedDataGridView dgv, Fask.Interfaces.Classes.EXPORT_DAT typ_exportu)
        {
            string path = string.Empty;

            // TODO: konfiguracne
            if (showPathDialog_Excel(out path) != DialogResult.OK)
                return false;

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            System.IO.FileInfo newFile = new System.IO.FileInfo(path);
            using (OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(newFile))
            {
                OfficeOpenXml.ExcelWorksheet ws = pck.Workbook.Worksheets.Add("List1");

                // kopie tabulky s naslednym odstranenim skrytych sloupcu
                System.Data.DataTable tCxC = new System.Data.DataTable();

                // exportovat vse
                if (typ_exportu == Fask.Interfaces.Classes.EXPORT_DAT.VSE)
                {
                    if (dgv.DataSource is BindingSource)
                    {
                        BindingSource bs = (BindingSource)dgv.DataSource;
                        //tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].Copy();
                        if (bs.List is DataView)
                        {
                            tCxC = ((DataView)bs.List).ToTable().Copy();
                        }
                        else if (bs.DataSource is DataSet)
                        {
                            tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].DefaultView.ToTable().Copy();
                        }
                        else if (bs.DataSource is System.Data.DataTable)
                        {
                            tCxC = ((System.Data.DataTable)bs.DataSource).DefaultView.ToTable().Copy();
                        }
                        else throw new Exception("Export Excel: Neznámý typ pro přetypování");
                    }
                    else if (dgv.DataSource is System.Data.DataTable)
                    {
                        tCxC = ((System.Data.DataTable)dgv.DataSource).DefaultView.ToTable().Copy();
                    }
                    else throw new Exception("Export Excel: Neznámý typ pro přetypování");
                }
                else  // exportovat vybrane
                {
                    System.Data.DataTable dtClone = new System.Data.DataTable();
                    if (dgv.DataSource is BindingSource)
                    {
                        BindingSource bs = (BindingSource)dgv.DataSource;
                        //tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
                        //dtClone = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
                        if (bs.DataSource is DataSet)
                        {
                            tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
                            dtClone = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
                        }
                        else if (bs.DataSource is System.Data.DataTable)
                        {
                            tCxC = ((System.Data.DataTable)bs.DataSource).Clone();
                            dtClone = ((System.Data.DataTable)bs.DataSource).Clone();
                        }
                        else throw new Exception("Export Excel: Neznámý typ pro přetypování");
                    }
                    else if (dgv.DataSource is System.Data.DataTable)
                    {
                        tCxC = ((System.Data.DataTable)dgv.DataSource).Copy();
                        dtClone = ((System.Data.DataTable)dgv.DataSource).Copy();
                    }
                    else throw new Exception("Export Excel: Neznámý typ pro přetypování");

                    // import vybranych radku
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        dtClone.ImportRow(((DataRowView)row.DataBoundItem).Row);
                    }
                    dtClone.AcceptChanges();

                   // dtClone.Select(,)

                    // otocit poradi a naimportovat do tCxC
                    for (int i = dtClone.Rows.Count - 1; i >= 0; i--)
                    {
                        tCxC.ImportRow(dtClone.Rows[i]);
                    }
                    tCxC.AcceptChanges();
                }

                // kopie datatable, se kterym se bude pracovat
                System.Data.DataTable dt2 = tCxC.Copy();


                // odstraneni skrytych sloupcu

                var myCol_PK = dt2.PrimaryKey;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (!dgv.Columns[i].Visible)
                    {
                        
                        if (!myCol_PK.Any(x => x.ColumnName.Trim() == dgv.Columns[i].DataPropertyName))
                        {
                            dt2.Columns.Remove(dgv.Columns[i].DataPropertyName);
                        }
                    }
                }

                // zmena nazvu hlavicek na ty, ktere se zobrazuji
                foreach (DataGridViewColumn item in dgv.Columns)
                {
                    if (dt2.Columns.Contains(item.DataPropertyName) && !dt2.Columns.Contains(item.HeaderText))
                        dt2.Columns[item.DataPropertyName].ColumnName = item.HeaderText;
                }

                // naplneni excelu daty
                ws.Cells["A1"].LoadFromDataTable(dt2, true);

                // nastaveni formatu datumu
                var dateColumns = from DataColumn d in dt2.Columns
                                  where d.DataType == typeof(DateTime)// || d.ColumnName.Contains("Date")
                                  select d.Ordinal + 1;
                foreach (var dc in dateColumns)
                {
                    ws.Cells[2, dc, dt2.Rows.Count + 2, dc].Style.Numberformat.Format = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExcelFormatDatum;
                }

                // nastaveni automaticke velikosti sloupcu
                ws.Cells.AutoFitColumns();

                // ulozeni
                pck.Save();
            }

            // spusteni v excelu
            if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExcelOtevritPoVygenerovani)
                System.Diagnostics.Process.Start(path);

            return true;
        }

        /// <summary>
        /// Pouziva se pouze pro export nasnimane inventury v pripade, ze je povoleno zobrazeni checkboxu pro vyber
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="typ_exportu"></param>
        public static void ExportToExcelInventura(this Zuby.ADGV.AdvancedDataGridView dgv, Fask.Interfaces.Classes.EXPORT_DAT typ_exportu)
        {
            string path = string.Empty;

            // TODO: konfiguracne
            if (showPathDialog_Excel(out path) != DialogResult.OK)
                return;

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            System.IO.FileInfo newFile = new System.IO.FileInfo(path);
            using (OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(newFile))
            {
                OfficeOpenXml.ExcelWorksheet ws = pck.Workbook.Worksheets.Add("List1");

                // kopie tabulky s naslednym odstranenim skrytych sloupcu
                System.Data.DataTable tCxC = new System.Data.DataTable();
                if (typ_exportu == Fask.Interfaces.Classes.EXPORT_DAT.VSE)
                {
                    if (dgv.DataSource is BindingSource)
                    {
                        BindingSource bs = (BindingSource)dgv.DataSource;
                        if (bs.DataSource is DataSet)
                        {
                            tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].Copy();
                        }
                        else if (bs.DataSource is System.Data.DataTable)
                        {
                            tCxC = ((System.Data.DataTable)bs.DataSource).Copy();
                        }
                        else throw new Exception("Export Excel: Neznámý typ pro přetypování");
                    }
                    else if (dgv.DataSource is System.Data.DataTable)
                    {
                        tCxC = (System.Data.DataTable)dgv.DataSource;
                    }
                    else throw new Exception("Export Excel: Neznámý typ pro přetypování");
                }
                else
                {
                    System.Data.DataTable dtClone = new System.Data.DataTable();
                    if (dgv.DataSource is BindingSource)
                    {
                        BindingSource bs = (BindingSource)dgv.DataSource;

                        if (bs.DataSource is DataSet)
                        {
                            tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
                            dtClone = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
                        }
                        else if (bs.DataSource is System.Data.DataTable)
                        {
                            tCxC = ((System.Data.DataTable)bs.DataSource).Clone();
                            dtClone = ((System.Data.DataTable)bs.DataSource).Clone();
                        }
                        else throw new Exception("Export Excel: Neznámý typ pro přetypování");
                    }
                    else if (dgv.DataSource is System.Data.DataTable)
                    {
                        tCxC = (System.Data.DataTable)dgv.DataSource;
                        dtClone = (System.Data.DataTable)dgv.DataSource;
                    }
                    else throw new Exception("Export Excel: Neznámý typ pro přetypování");

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (Convert.ToBoolean(dgv.Rows[row.Index].Cells["Vybrano"].Value))
                            tCxC.ImportRow(((DataRowView)row.DataBoundItem).Row);
                    }
                    tCxC.AcceptChanges();
                }


                // kopie datatable, se kterym se bude pracovat
                System.Data.DataTable dt2 = tCxC.Copy();

                // odstraneni skrytych sloupcu
                var myCol_PK = dt2.PrimaryKey;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (!dgv.Columns[i].Visible)
                    {

                        if (!myCol_PK.Any(x => x.ColumnName.Trim() == dgv.Columns[i].DataPropertyName))
                        {
                            dt2.Columns.Remove(dgv.Columns[i].DataPropertyName);
                        }
                    }
                }

                // zmena nazvu hlavicek na ty, ktere se zobrazuji
                foreach (DataGridViewColumn item in dgv.Columns)
                {
                    if (dt2.Columns.Contains(item.DataPropertyName))
                        dt2.Columns[item.DataPropertyName].ColumnName = item.HeaderText;
                }

                // naplneni excelu daty
                ws.Cells["A1"].LoadFromDataTable(dt2, true);

                // nastaveni formatu datumu
                var dateColumns = from DataColumn d in dt2.Columns
                                  where d.DataType == typeof(DateTime)// || d.ColumnName.Contains("Date")
                                  select d.Ordinal + 1;
                foreach (var dc in dateColumns)
                {
                    ws.Cells[2, dc, dt2.Rows.Count + 2, dc].Style.Numberformat.Format = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExcelFormatDatum;
                }

                // nastaveni automaticke velikosti sloupcu
                ws.Cells.AutoFitColumns();

                // ulozeni
                pck.Save();
            }

            // spusteni v excelu
            if (Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExcelOtevritPoVygenerovani)
                System.Diagnostics.Process.Start(path);
        }

        #endregion

        #region Export to XML

        private static DialogResult showPathDialog_XML(out string filepath)
        {
            filepath = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = ".xml Files (*.xml)|*.xml";
            sfd.InitialDirectory = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyPath;
            sfd.FileName = Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyXMLFileName;

            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {

                Konfigurace.Globals_Konfig_Konzola.Konfigurace.Export[0].ExportyPath = Path.GetDirectoryName(sfd.FileName);
                //Settings.ExportyCSVFileName = Path.GetFileNameWithoutExtension(sfd.FileName);
                filepath = sfd.FileName;
            }

            return dr;
        }

        public static bool ExportToXML(this Zuby.ADGV.AdvancedDataGridView dgv, Fask.Interfaces.Classes.EXPORT_DAT typ_exportu)
        {
            string path = string.Empty;

            // TODO: konfiguracne
            if (showPathDialog_XML(out path) != DialogResult.OK)
                return false;

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            //System.IO.FileInfo newFile = new System.IO.FileInfo(path);

            // kopie tabulky s naslednym odstranenim skrytych sloupcu
            System.Data.DataTable tCxC = new System.Data.DataTable();

            #region Export VSE
            if (typ_exportu == Fask.Interfaces.Classes.EXPORT_DAT.VSE)
            {
                if (dgv.DataSource is BindingSource)
                {
                    BindingSource bs = (BindingSource)dgv.DataSource;

                    if (bs.List is DataView)
                    {
                        tCxC = ((DataView)bs.List).ToTable().Copy();
                    }
                    else if (bs.DataSource is DataSet)
                    {
                        tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].DefaultView.ToTable().Copy();
                    }
                    else if (bs.DataSource is System.Data.DataTable)
                    {
                        tCxC = ((System.Data.DataTable)bs.DataSource).DefaultView.ToTable().Copy();
                    }
                    else throw new Exception("Export CSV: Neznámý typ pro přetypování");
                }
                else if (dgv.DataSource is System.Data.DataTable)
                {
                    tCxC = ((System.Data.DataTable)dgv.DataSource).DefaultView.ToTable().Copy();
                }
                else throw new Exception("Export CSV: Neznámý typ pro přetypování");
            }
            #endregion

            #region exportovat vybrane
            else  // exportovat vybrane
            {
                System.Data.DataTable dtClone = new System.Data.DataTable();
                if (dgv.DataSource is BindingSource)
                {
                    BindingSource bs = (BindingSource)dgv.DataSource;

                    if (bs.DataSource is DataSet)
                    {
                        tCxC = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
                        dtClone = ((DataSet)bs.DataSource).Tables[bs.DataMember].Clone();
                    }
                    else if (bs.DataSource is System.Data.DataTable)
                    {
                        tCxC = ((System.Data.DataTable)bs.DataSource).Clone();
                        dtClone = ((System.Data.DataTable)bs.DataSource).Clone();
                    }
                    else throw new Exception("Export CSV: Neznámý typ pro přetypování");
                }
                else if (dgv.DataSource is System.Data.DataTable)
                {
                    tCxC = ((System.Data.DataTable)dgv.DataSource).Copy();
                    dtClone = ((System.Data.DataTable)dgv.DataSource).Copy();
                }
                else throw new Exception("Export CSV: Neznámý typ pro přetypování");

                // import vybranych radku
                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    dtClone.ImportRow(((DataRowView)row.DataBoundItem).Row);
                }
                dtClone.AcceptChanges();

                // otocit poradi a naimportovat do tCxC
                for (int i = dtClone.Rows.Count - 1; i >= 0; i--)
                {
                    tCxC.ImportRow(dtClone.Rows[i]);
                }
                tCxC.AcceptChanges();
            }
            #endregion

            // kopie datatable, se kterym se bude pracovat
            System.Data.DataTable dt2 = tCxC.Copy();

            // odstraneni skrytych sloupcu
            var myCol_PK = dt2.PrimaryKey;

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (!dgv.Columns[i].Visible)
                {

                    if (!myCol_PK.Any(x => x.ColumnName.Trim() == dgv.Columns[i].DataPropertyName))
                    {
                        dt2.Columns.Remove(dgv.Columns[i].DataPropertyName);
                    }
                }
            }

            // zmena nazvu hlavicek na ty, ktere se zobrazuji
            //foreach (DataGridViewColumn item in dgv.Columns)
            //{
            //    if (dt2.Columns.Contains(item.DataPropertyName) && !dt2.Columns.Contains(item.HeaderText))
            //        dt2.Columns[item.DataPropertyName].ColumnName = item.HeaderText;
            //}


            dt2.DT_To_XML(path);



            return true;
        }


        private static void DT_To_XML(this DataTable dt, string Path)
        {
            dt.WriteXml(Path);
        }



        #endregion

    }


}
