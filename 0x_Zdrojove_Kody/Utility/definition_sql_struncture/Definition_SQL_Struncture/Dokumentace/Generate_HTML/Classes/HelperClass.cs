using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Definition_SQL_Struncture.Dokumentace.Generate_HTML.Classes
{
    class HelperClass
    {
        #region Hlavné metody

        /// <summary>
        /// Metoda která nakopiruje z Template složky soubory so vystupu Dokumentace
        /// </summary>
        /// <param name="TemplatePath">cesta k Template souborom</param>
        /// <param name="PathToSave">Cesta kam se má uložit vysledna vygenerovana Dokumentace</param>
        public static void SkopirujPotrebneSoubory(string TemplatePath, string PathToSave)
        {
            try
            {
                string PicPath = Path.Combine(TemplatePath, Common.PIC_Folder);
                string PicPathNew = Path.Combine(PathToSave, Common.PIC_Folder);

                Dokumentace.FileHelper.DirectoryCopy(PicPath, PicPathNew, true);

                var cssPath = Path.Combine(TemplatePath, Common.CSS_Folder);
                var cssPathNew = Path.Combine(PathToSave, Common.CSS_Folder);

                Dokumentace.FileHelper.DirectoryCopy(cssPath, cssPathNew, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Metoda která vytvoří hlavní soubor který obsahuje klikabilny seznam všech Tabulek a Pohledu ( zatím)
        /// </summary>
        /// <param name="TemplatePath">cesta k Template souborom</param>
        /// <param name="PathToSave">Cesta kam se má uložit vysledna vygenerovana Dokumentace</param>
        /// <param name="grupTables">Grupnute data z ktery se vytahuji infomrace pro skladani stranky</param>
        public static void GenerujHlavniSeznamTabulek(string TemplatePath, string PathToSave, IEnumerable<IGrouping<string, Dok.DT_DocumentationRow>> grupTables)
        {

            try
            {

                var TemplateFilePath = Path.Combine(TemplatePath, Common.List_Tables_Views_Templates);
                var TemplateFilePathNew = Path.Combine(PathToSave, Common.List_Tables_Views);

                File.Copy(TemplateFilePath, TemplateFilePathNew);

                var htmlDoc = new HtmlDocument();
                htmlDoc.Load(TemplateFilePathNew);

                var tabulka = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='Tabulka']");

                foreach (var item in grupTables)
                {
                    string TableName = item.Key;

                    string TypObjektu = string.Empty;

                    TypObjektu = item.First().typ;

                    string ClickToFile = Common.Table_Prefix + TableName + Common.HTML_Suffix;

                    Row_Table_View_List row = new Row_Table_View_List();

                    row.Table_View_Name = new Cell()
                    {
                        Value = TableName.ToString(),
                        IsClick = true,
                        PageToClick = ClickToFile
                    };

                    row.Table_View_Desc = new Cell()
                    {
                        Value = "TODO"
                    };


                    row.Table_View_Typ = new Cell()
                    {
                        Value = TypObjektu
                    };

                    XElement X = Get_Row_ToList_Table_View(row);

                    HtmlNode newPara = HtmlNode.CreateNode(X.ToString());
                    tabulka.ChildNodes.Add(newPara);
                }

                htmlDoc.Save(TemplateFilePathNew);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metoda pomoci ktere se vytvářejí jednotlívé stranky pro každou tabulku a pohled
        /// </summary>
        /// <param name="TemplatePath">cesta k Template souborom</param>
        /// <param name="PathToSave">Cesta kam se má uložit vysledna vygenerovana Dokumentace</param>
        /// <param name="grupTables">Grupnute data z ktery se vytahuji infomrace pro skladani stranky</param>
        public static void GenerujJednotliveTabulky_a_Pohledy(string TemplatePath, string PathToSave, IEnumerable<IGrouping<string, Dok.DT_DocumentationRow>> grupTables)
        {
            foreach (var item in grupTables)
            {
                string TableName = item.Key;
                string ClickToFile = Common.Table_Prefix + TableName + Common.HTML_Suffix;

                var TemplateFilePath = Path.Combine(TemplatePath, Common.Tables_Views_Templates);
                var TemplateFilePathNew = Path.Combine(PathToSave, ClickToFile);

                File.Copy(TemplateFilePath, TemplateFilePathNew);

                var htmlDoc = new HtmlDocument();
                htmlDoc.Load(TemplateFilePathNew);
                var tabulka = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='Tabulka']");

                foreach (var row in item)
                {
                    Row_Table_View radek = new Row_Table_View();

                    radek.Value1 = new Cell()
                    {
                        Value = row.Schema
                    };

                    radek.Value2 = new Cell()
                    {
                        Value = row.COLUMN
                    };

                    radek.Value3 = new Cell()
                    {
                        Value = row.TYPECOL
                    };

                    radek.Value4 = new Cell()
                    {
                        Value = row.MAXLEN
                    };

                    radek.Value5 = new Cell()
                    {
                        Value = row.DEFVAL
                    };

                    radek.Value6 = new Cell()
                    {
                        Value = row.NUMPREC
                    };

                    radek.Value7 = new Cell()
                    {
                        Value = row.DTPREC
                    };

                    radek.Value8 = new Cell()
                    {
                        Value = row.NUMSCALE
                    };

                    radek.Value9 = new Cell()
                    {
                        Value = row.ISNULL
                    };

                    radek.Value10 = new Cell()
                    {
                        Value = row.DESCCOL
                    };

                    XElement X = Get_Row_Table_View(radek);

                    HtmlNode newPara = HtmlNode.CreateNode(X.ToString());
                    tabulka.ChildNodes.Add(newPara);
                }

                htmlDoc.Save(TemplateFilePathNew);
            }
        }

        #endregion

        #region Pomocne metody

        /// <summary>
        /// Metoda která vratí jednu buňku, a podle typu vratí čí je klikabilna, anebo ne...
        /// </summary>
        /// <param name="cell">Objekt jednej Buňky v tabulke</param>
        /// <returns></returns>
        private static XElement GetElement(Cell cell)
        {
            if (cell.IsClick)
            {
                return new XElement(Common.HTML_Tag__TD,
                            new XElement(Common.HTML_Tag__A, 
                                new XAttribute(Common.HTML_Tag__HREF, cell.PageToClick), cell.Value));
            }
            else
            {
                return new XElement(Common.HTML_Tag__TD, cell.Value);
            }
        }

        /// <summary>
        /// Metoda která vrací XElement, jak jeden rádek
        /// </summary>
        /// <param name="Radek">Objekt Row pro naplneni řadku</param>
        /// <returns></returns>
        private static XElement Get_Row_ToList_Table_View(Row_Table_View_List Radek)
        {
            List<XElement> Columns = new List<XElement>();

            Columns.Add(GetElement(Radek.Table_View_Typ));
            Columns.Add(GetElement(Radek.Table_View_Name));
            Columns.Add(GetElement(Radek.Table_View_Desc));

            return new XElement(Common.HTML_Tag__TR, Columns);
        }

        /// <summary>
        /// Metoda která vrací XElement, jak jeden rádek
        /// </summary>
        /// <param name="Radek">Objekt Row pro naplneni řadku</param>
        /// <returns></returns>
        private static XElement Get_Row_Table_View(Row_Table_View Radek)
        {
            List<XElement> Columns = new List<XElement>();

            Columns.Add(GetElement(Radek.Value1));
            Columns.Add(GetElement(Radek.Value2));
            Columns.Add(GetElement(Radek.Value3));
            Columns.Add(GetElement(Radek.Value4));
            Columns.Add(GetElement(Radek.Value5));
            Columns.Add(GetElement(Radek.Value6));
            Columns.Add(GetElement(Radek.Value7));
            Columns.Add(GetElement(Radek.Value8));
            Columns.Add(GetElement(Radek.Value9));
            Columns.Add(GetElement(Radek.Value10));

            return new XElement(Common.HTML_Tag__TR , Columns);
        }

        #endregion
    }
}
