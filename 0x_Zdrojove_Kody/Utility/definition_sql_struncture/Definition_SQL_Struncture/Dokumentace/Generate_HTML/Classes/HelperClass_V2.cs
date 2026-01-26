using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Definition_SQL_Struncture.Dokumentace.Generate_HTML.Classes
{
    class HelperClass_V2
    {
        #region Hlavné metody HTML Dokumentace soubory

        /// <summary>
        /// Metoda která nakopiruje z Template složky soubory so vystupu Dokumentace
        /// </summary>
        /// <param name="TemplatePath">cesta k Template souborom</param>
        /// <param name="PathToSave">Cesta kam se má uložit vysledna vygenerovana Dokumentace</param>
        public static void SkopirujPotrebneSoubory(string TemplatePath, string PathToSave)
        {
            try
            {
                string GrafikaPath = Path.Combine(TemplatePath, Common.Grafika_Folder);
                string GrafikaPathNew = Path.Combine(PathToSave, Common.Grafika_Folder);

                if (Directory.Exists(GrafikaPathNew))
                {
                    Directory.Delete(GrafikaPathNew);
                }

                Dokumentace.FileHelper.DirectoryCopy(GrafikaPath, GrafikaPathNew, true);

                string MainFilePath = Path.Combine(TemplatePath, Common.MAIN + Common.HTML_Suffix);
                string MainFilePathNew = Path.Combine(PathToSave, Common.MAIN + Common.HTML_Suffix);

                if (File.Exists(MainFilePathNew))
                    File.Delete(MainFilePathNew);


                File.Copy(MainFilePath, MainFilePathNew);

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

                //var TemplateFilePath = Path.Combine(TemplatePath, Common.TemplateHTM);
                var TemplateFilePathNew = Path.Combine(PathToSave, @"html\" + Common.List_Tables_Views);

                if (!Directory.Exists(Path.GetDirectoryName(TemplateFilePathNew)))
                    Directory.CreateDirectory(Path.GetDirectoryName(TemplateFilePathNew));

                //File.Copy(TemplateFilePath, TemplateFilePathNew);

                var htmlDoc = new HtmlDocument();
                //htmlDoc.Load(TemplateFilePathNew);
                htmlDoc.LoadHtml(Common.HTML_TEMPLATE);

                var headElement = htmlDoc.DocumentNode.SelectSingleNode("//head");

                var xx = new XElement(Common.HTML_Tag__TITLE, "MES Tabulky a Pohledy");
                HtmlNode newParaTitle = HtmlNode.CreateNode(xx.ToString());
                headElement.ChildNodes.Add(newParaTitle);

                List<XElement> xElementsCSS = GetXElementCSS();

                foreach (var item in xElementsCSS)
                {
                    HtmlNode newParaScripty = HtmlNode.CreateNode(item.ToString());
                    headElement.ChildNodes.Add(newParaScripty);
                }

                //var tabulka = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='Tabulka']");
                var bodyElement = htmlDoc.DocumentNode.SelectSingleNode("//body");


                XElement tabulka = new XElement(Common.HTML_Tag__TABLE,
                    new XAttribute(Common.HTML_Tag__ID, "tableID"),
                    new XAttribute(Common.HTML_Tag__CLASS, "table table-border")
                    );

                XElement xElementHlavicka = Get_Hlavicka_ToList_Table_View();

                tabulka.Add(xElementHlavicka);

                List<XElement> XelementRowBody = new List<XElement>();

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
                        Value = "zde",
                        IsClickComment = true
                    };


                    row.Table_View_Typ = new Cell()
                    {
                        Value = TypObjektu
                    };

                    XElement X = Get_Row_ToList_Table_View(row);

                    XelementRowBody.Add(X);

                    XElement xElementComment = GetElement_Comment("TODO", "3");
                    XelementRowBody.Add(xElementComment);

                }

                XElement xElementBody = new XElement(Common.HTML_Tag__TBODY, XelementRowBody);

                tabulka.Add(xElementBody);

                XElement root = new XElement(
                    Common.HTML_Tag__DL,
                    new XElement(Common.HTML_Tag__H2, "Tabulky a Pohledy"),
                    new XElement(Common.HTML_Tag__DD, "Popis:"),
                    new XElement(Common.HTML_Tag__DT, "Seznam všech tabulek a pohledů"),
                    new XElement(Common.HTML_Tag__H3, "Seznam: "),
                    tabulka
                    );

                HtmlNode newPara = HtmlNode.CreateNode(root.ToString());
                bodyElement.ChildNodes.Add(newPara);


                var podpis = GetXElementPodpis();

                HtmlNode newParaPodpis = HtmlNode.CreateNode(podpis.ToString());
                bodyElement.ChildNodes.Add(newParaPodpis);


                List<XElement> Scripty = GetXElementScripty();

                foreach (var item in Scripty)
                {
                    HtmlNode newParaScripty = HtmlNode.CreateNode(item.ToString());
                    bodyElement.ChildNodes.Add(newParaScripty);
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

                //var TemplateFilePath = Path.Combine(TemplatePath, Common.TemplateHTM);
                var TemplateFilePathNew = Path.Combine(PathToSave, @"html\" + ClickToFile);

                if (!Directory.Exists(Path.GetDirectoryName(TemplateFilePathNew)))
                    Directory.CreateDirectory(Path.GetDirectoryName(TemplateFilePathNew));

                //File.Copy(TemplateFilePath, TemplateFilePathNew);

                var htmlDoc = new HtmlDocument();
                //htmlDoc.Load(TemplateFilePathNew);
                htmlDoc.LoadHtml(Common.HTML_TEMPLATE);

                var headElement = htmlDoc.DocumentNode.SelectSingleNode("//head");

                var xx = new XElement(Common.HTML_Tag__TITLE, "MES Tabulka " + TableName);
                HtmlNode newParaTitle = HtmlNode.CreateNode(xx.ToString());
                headElement.ChildNodes.Add(newParaTitle);

                List<XElement> xElementsCSS = GetXElementCSS();

                foreach (var elementCSS in xElementsCSS)
                {
                    HtmlNode newParaCSS = HtmlNode.CreateNode(elementCSS.ToString());
                    headElement.ChildNodes.Add(newParaCSS);
                }

                //var tabulka = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='Tabulka']");
                var bodyElement = htmlDoc.DocumentNode.SelectSingleNode("//body");

                XElement tabulka = new XElement(Common.HTML_Tag__TABLE,
                                        new XAttribute(Common.HTML_Tag__ID, "tableID"),
                                        new XAttribute(Common.HTML_Tag__CLASS, "table table-border")
                                        );

                XElement xElementHlavicka = Get_Hlavicka_Table_View();

                tabulka.Add(xElementHlavicka);

                List<XElement> XelementRowBody = new List<XElement>();

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
                        Value = "zde",
                        IsClickComment = true
                    };

                    XElement X = Get_Row_Table_View(radek);

                    XelementRowBody.Add(X);

                    string popis = string.IsNullOrEmpty(row.DESCCOL) ? "TODO potřeba doplnit" : row.DESCCOL.Trim();

                    XElement xElementComment = GetElement_Comment(popis, "10");
                    XelementRowBody.Add(xElementComment);

                    //HtmlNode newPara = HtmlNode.CreateNode(X.ToString());
                    //tabulka.ChildNodes.Add(newPara);
                }

                XElement xElementBody = new XElement(Common.HTML_Tag__TBODY, XelementRowBody);

                tabulka.Add(xElementBody);

                var TypObjektu = item.First().typ.Trim();
                string H2Nadpis = "N/A";

                if (TypObjektu == "BASE TABLE")
                {
                    H2Nadpis = "Tabulka " + TableName;
                }
                else if (TypObjektu == "VIEW")
                {
                    H2Nadpis = "Pohled " + TableName;
                }

                XElement root = new XElement(
                    Common.HTML_Tag__DL,
                    new XElement(Common.HTML_Tag__H2, H2Nadpis),
                    new XElement(Common.HTML_Tag__DD, "Popis:"),
                    new XElement(Common.HTML_Tag__DT, "TODO"),
                    new XElement(Common.HTML_Tag__H3, "Položky: "),
                    tabulka
                    );

                HtmlNode newPara = HtmlNode.CreateNode(root.ToString());
                bodyElement.ChildNodes.Add(newPara);


                var podpis = GetXElementPodpis();

                HtmlNode newParaPodpis = HtmlNode.CreateNode(podpis.ToString());
                bodyElement.ChildNodes.Add(newParaPodpis);


                List<XElement> Scripty = GetXElementScripty();

                foreach (var scr in Scripty)
                {
                    HtmlNode newParaScripty = HtmlNode.CreateNode(scr.ToString());
                    bodyElement.ChildNodes.Add(newParaScripty);
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
        private static XElement GetElement_Row(Cell cell)
        {
            if (cell.IsClick)
            {
                return new XElement(Common.HTML_Tag__TD,
                            new XElement(Common.HTML_Tag__A,
                                new XAttribute(Common.HTML_Tag__HREF, cell.PageToClick), cell.Value));
            }
            if (cell.IsClickComment)
            {
                return new XElement(Common.HTML_Tag__TD,
                    new XAttribute(Common.HTML_Tag__CLASS, "ClickComment ClickCommentCursor"),
                    cell.Value);
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

            Columns.Add(GetElement_Row(Radek.Table_View_Typ));
            Columns.Add(GetElement_Row(Radek.Table_View_Name));
            Columns.Add(GetElement_Row(Radek.Table_View_Desc));

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

            Columns.Add(GetElement_Row(Radek.Value1));
            Columns.Add(GetElement_Row(Radek.Value2));
            Columns.Add(GetElement_Row(Radek.Value3));
            Columns.Add(GetElement_Row(Radek.Value4));
            Columns.Add(GetElement_Row(Radek.Value5));
            Columns.Add(GetElement_Row(Radek.Value6));
            Columns.Add(GetElement_Row(Radek.Value7));
            Columns.Add(GetElement_Row(Radek.Value8));
            Columns.Add(GetElement_Row(Radek.Value9));
            Columns.Add(GetElement_Row(Radek.Value10));

            return new XElement(Common.HTML_Tag__TR, Columns);
        }

        private static XElement GetElement_Header(string txt)
        {
            return new XElement(Common.HTML_Tag__TH,
                new XAttribute(Common.HTML_Tag__CLASS, "font_bold"),
                txt);
        }

        private static XElement Get_Hlavicka_ToList_Table_View()
        {
            List<XElement> Columns = new List<XElement>();

            Columns.Add(GetElement_Header("Typ"));
            Columns.Add(GetElement_Header("Název"));
            Columns.Add(GetElement_Header("Popis"));

            return new XElement(Common.HTML_Tag__THEAD,
                new XElement(Common.HTML_Tag__TR, Columns)
                );
        }

        private static XElement GetXElementPodpis()
        {
            return new XElement(Common.HTML_Tag__P,
                                new XElement(Common.HTML_Tag__EM, "Generated by ©TaD")
                                );
        }

        private static List<XElement> GetXElementScripty()
        {
            List<XElement> xElements = new List<XElement>();

            xElements.Add(GetXElementScript("../Grafika/JS/jquery.js"));
            xElements.Add(GetXElementScript("../Grafika/JS/bootstrap.js"));
            xElements.Add(GetXElementScript("../Grafika/JS/CommentShowHide.js"));

            return xElements;
        }

        private static XElement GetXElementScript(string txt)
        {
            return new XElement(Common.HTML_Tag__SCRIPT,
                new XAttribute(Common.HTML_Tag__SRC, txt),
                new XAttribute(Common.HTML_Tag__TYPE, Common.HTML_TYPE__JS)
                );
        }

        private static List<XElement> GetXElementCSS()
        {
            List<XElement> xElements = new List<XElement>();

            xElements.Add(GetXElementCSS("../Grafika/CSS/bootstrap.css"));
            xElements.Add(GetXElementCSS("../Grafika/CSS/TaD_Sheet.css"));

            return xElements;
        }

        private static XElement GetXElementCSS(string txt)
        {
            return new XElement(Common.HTML_Tag__LINK,
                new XAttribute(Common.HTML_Tag__REL, Common.HTML_REL__CSS),
                new XAttribute(Common.HTML_Tag__HREF, txt),
                new XAttribute(Common.HTML_Tag__TYPE, Common.HTML_TYPE__CSS)
                );
        }

        private static XElement Get_Hlavicka_Table_View()
        {
            List<XElement> Columns = new List<XElement>();

            Columns.Add(GetElement_Header("Schema"));
            Columns.Add(GetElement_Header("Název"));
            Columns.Add(GetElement_Header("Typ"));
            Columns.Add(GetElement_Header("Max Délka"));
            Columns.Add(GetElement_Header("Default"));
            Columns.Add(GetElement_Header("Přesnost číslo"));
            Columns.Add(GetElement_Header("Přesnost datum"));
            Columns.Add(GetElement_Header("Rozsah/škála"));
            Columns.Add(GetElement_Header("Je null?"));
            Columns.Add(GetElement_Header("Popis"));

            return new XElement(Common.HTML_Tag__THEAD,
                new XElement(Common.HTML_Tag__TR, Columns)
                );
        }

        private static XElement GetElement_Comment(string txt, string COLSPAN)
        {
            return new XElement(Common.HTML_Tag__TR,
                    new XElement(Common.HTML_Tag__TD,
                    new XAttribute(Common.HTML_Tag__CLASS, "Comment"),
                    new XAttribute(Common.HTML_Tag__COLSPAN, COLSPAN),
                    txt)
                    );
        }

        #endregion

        #region HTML projekt .chm

        internal static void Generate_Projekt_HTML(string pathToSave, 
            IEnumerable<IGrouping<string, Dok.DT_DocumentationRow>> grupTables,
            Dok ds)
        {
            try
            {
                Create_HHP(pathToSave); // Hlavny projekt
                Create_HHC_V2(pathToSave, grupTables); // Jedná se o menu
                Create_HHK(pathToSave, ds); // Jedná se o Indexy pro vyhledavani
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Hlavny projektovy soubor
        /// </summary>
        /// <param name="pathToSave"></param>
        private static void Create_HHP(string pathToSave)
        {
            try
            {
                //Hlavny projekt

                string FileName = Common.MES_Dokumentace + Common.HHP_Suffix;
                string PathProjectFile = Path.Combine(pathToSave, FileName);

                if (File.Exists(PathProjectFile))
                    File.Delete(PathProjectFile);

                using (var X = File.Create(PathProjectFile))
                {

                }

                using (StreamWriter sw = File.AppendText(PathProjectFile))
                {
                    sw.WriteLine("[OPTIONS]");
                    sw.WriteLine("Compatibility=1.1 or later");
                    sw.WriteLine("Compiled file = " + Common.MES_Dokumentace_ + Common.CHM_Suffix);
                    sw.WriteLine("Contents file = " + Common.MES_Dokumentace_ + Common.HHC_Suffix);
                    sw.WriteLine("Default Window = " + Common.MAIN);
                    sw.WriteLine("Default topic = " + Common.MAIN + Common.HTML_Suffix);
                    sw.WriteLine("Display compile progress=No");
                    sw.WriteLine("Error log file = " + Common.MES_Dokumentace_ + Common.LOG_Suffix);
                    sw.WriteLine("Full-text search = Yes");
                    sw.WriteLine("Index file = " + Common.MES_Dokumentace_ + Common.HHK_Suffix);
                    sw.WriteLine("Language=0x405 Čeština(Česko)");
                    sw.WriteLine("Title=" + Common.MES_Dokumentace);

                    sw.WriteLine(Environment.NewLine);

                    sw.WriteLine("[WINDOWS]");
                    sw.WriteLine("Main=\"" + Common.MES_Dokumentace + "\",\"" + Common.MES_Dokumentace_ + Common.HHC_Suffix + "\",\"" + Common.MES_Dokumentace_ + Common.HHK_Suffix + "\",\"" + Common.MAIN + Common.HTML_Suffix + "\",,,,,,0x42520,200,0x3006,[25,25,575,425],,,,,,,0");



                    sw.WriteLine(Environment.NewLine);
                    sw.WriteLine("[FILES]");


                    //Cyklus, naplnení všech souboru co jsou

                    sw.WriteLine(Common.MAIN + Common.HTML_Suffix);

                    List<string> allhtml = System.IO.Directory.GetFiles(Path.Combine(pathToSave, "html")).ToList();

                    foreach (var htmlItem in allhtml)
                    {
                        var x = @"html\" + System.IO.Path.GetFileName(htmlItem);
                        sw.WriteLine(x);
                    }

                    List<string> allhtmlcss = System.IO.Directory.GetFiles(Path.Combine(pathToSave, @"Grafika/CSS")).ToList();

                    foreach (var htmlItem in allhtmlcss)
                    {
                        var x = @"Grafika\CSS\" + System.IO.Path.GetFileName(htmlItem);
                        sw.WriteLine(x);
                    }

                    List<string> allhtmlImage = System.IO.Directory.GetFiles(Path.Combine(pathToSave, @"Grafika/Image")).ToList();

                    foreach (var htmlItem in allhtmlImage)
                    {
                        var x = @"Grafika\Image\" + System.IO.Path.GetFileName(htmlItem);
                        sw.WriteLine(x);
                    }

                    List<string> allhtmlJS = System.IO.Directory.GetFiles(Path.Combine(pathToSave, @"Grafika/JS")).ToList();

                    foreach (var htmlItem in allhtmlJS)
                    {
                        var x = @"Grafika\JS\" + System.IO.Path.GetFileName(htmlItem);
                        sw.WriteLine(x);
                    }


                    sw.WriteLine(Environment.NewLine);
                    sw.WriteLine("[INFOTYPES]");

                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Index, indexovane vyhledavani
        /// </summary>
        /// <param name="pathToSave"></param>
        private static void Create_HHK(string pathToSave, Dok ds)
        {
            try
            {
                #region Ukazka

                //< !DOCTYPE HTML PUBLIC "-//IETF//DTD HTML//EN" >
                //< HTML >
                //< HEAD >
                //< meta name = "GENERATOR" content = "Microsoft&reg; HTML Help Workshop 4.1" >
                //< !--Sitemap 1.0-- >
                //</ HEAD >< BODY >
                //< OBJECT type = "text/site properties" >
                //< param name = "SaveType" value = "inform. type name K1" >
                //< param name = "SaveTypeDesc" value = "descr.k1" >
                //< param name = "Category" value = "k1" > 
                //< param name = "CategoryDesc" value = "popis k1" >
                //</ OBJECT >
                //< UL >
                //</ UL >
                //</ BODY ></ HTML >

                #endregion

                //Indexovy soubor
                var TemplateFilePathNew = Path.Combine(pathToSave, Common.MES_Dokumentace_ + Common.HHK_Suffix);

                var htmlDoc = new HtmlDocument();

                htmlDoc.LoadHtml(Common.HTML_TEMPLATE_HHK);

                var bodyElement = htmlDoc.DocumentNode.SelectSingleNode("//body");


                string item = Get_HHK_All(ds);

                HtmlNode newParaScripty = HtmlNode.CreateNode(item);
                bodyElement.ChildNodes.Add(newParaScripty);

                htmlDoc.Save(TemplateFilePathNew);

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Content, seznam z ktereho se hleda
        ///// </summary>
        ///// <param name="pathToSave"></param>
        //private static void Create_HHC(string pathToSave, IEnumerable<IGrouping<string, Dok.DT_DocumentationRow>> grupTables)
        //{
        //    try
        //    {
        //        #region Ukazka

        //        //< !DOCTYPE HTML PUBLIC "-//IETF//DTD HTML//EN" >
        //        //< HTML >
        //        //< HEAD >
        //        //< meta name = "GENERATOR" content = "Microsoft&reg; HTML Help Workshop 4.1" >
        //        //< !--Sitemap 1.0-- >
        //        //</ HEAD >< BODY >
        //        //< UL >
        //        //< LI > < OBJECT type = "text/sitemap" >
        //        //< param name = "Name" value = "Struktury a definice projekt&ugrave;" >
        //        //< param name = "Local" value = "html/modules.htm" >
        //        //</ OBJECT >
        //        //< LI > < OBJECT type = "text/sitemap" >
        //        //< param name = "Name" value = "Seznam relac&iacute;" >
        //        //< param name = "Local" value = "html/Relations.htm" >
        //        //</ OBJECT >
        //        //< LI > < OBJECT type = "text/sitemap" >
        //        //< param name = "Name" value = "Seznam QuickReports funkc&iacute;" >
        //        //< param name = "Local" value = "html/QRFunctionGroups.htm" >
        //        //</ OBJECT >
        //        //< LI > < OBJECT type = "text/sitemap" >
        //        //< param name = "Name" value = "Seznam importn&iacute;ch manažer&ugrave;" >
        //        //< param name = "Local" value = "html/ImportMgrList.htm" >
        //        //</ OBJECT >
        //        //</ UL >
        //        //</ BODY ></ HTML >

        //        #endregion

        //        //Soubor ktery vytvoři menu
        //        var TemplateFilePathNew = Path.Combine(pathToSave, Common.MES_Dokumentace_ + Common.HHC_Suffix);

        //        var htmlDoc = new HtmlDocument();

        //        htmlDoc.LoadHtml(Common.Content_Index_HTML_TEMPLATE);

        //        HtmlNode bodyElement = htmlDoc.DocumentNode.SelectSingleNode("//body");

        //        //HtmlNode bodyElementX = new HtmlNode(HtmlNodeType.Element, ) 

        //        string xElement_Main = Get_Li_Object("Main", "Main.html", "Main.html");
        //        string xElement_List = Get_Li_Object("List Tabulek", @"html\List_Tables_Views.html", @"html\List_Tables_Views.html");

        //        List<string> ListxElTabulek = new List<string>();

        //        foreach (var rowGrup in grupTables)
        //        {
        //            string TableName = rowGrup.Key;
        //            string ClickToFile = Common.HTML_Tag__HTML + "\\" + Common.Table_Prefix + TableName + Common.HTML_Suffix;

        //            string TypObjektu = string.Empty;

        //            TypObjektu = rowGrup.First().typ;
        //            string Text = string.Empty;

        //            if (TypObjektu == "BASE TABLE")
        //            {
        //                Text = "Tabulka " + TableName;
        //            }
        //            else if (TypObjektu == "VIEW")
        //            {
        //                Text = "Pohled " + TableName;
        //            }

        //            ListxElTabulek.Add(Get_Li_Object(Text + TableName, ClickToFile, ClickToFile));
        //        }

        //        XElement xElementListTabulekJednotlibo = new XElement(Common.HTML_Tag__UL,
        //            ListxElTabulek
        //            );

        //        XElement xElementListVseho = new XElement(Common.HTML_Tag__UL,
        //            xElement_List,
        //            xElementListTabulekJednotlibo
        //            );



        //        XElement item = new XElement(Common.HTML_Tag__UL,
        //                                    xElement_Main,
        //                                    xElementListVseho
        //                                    );

        //        HtmlNode newParaScripty = HtmlNode.CreateNode(item.ToString());

        //        bodyElement.ChildNodes.Add(newParaScripty);

        //        htmlDoc.Save(TemplateFilePathNew);

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Content, seznam z ktereho se hleda
        /// </summary>
        /// <param name="pathToSave"></param>
        private static void Create_HHC_V2(string pathToSave, IEnumerable<IGrouping<string, Dok.DT_DocumentationRow>> grupTables)
        {
            try
            {
                #region Ukazka

                //< !DOCTYPE HTML PUBLIC "-//IETF//DTD HTML//EN" >
                //< HTML >
                //< HEAD >
                //< meta name = "GENERATOR" content = "Microsoft&reg; HTML Help Workshop 4.1" >
                //< !--Sitemap 1.0-- >
                //</ HEAD >< BODY >
                //< UL >
                //< LI > < OBJECT type = "text/sitemap" >
                //< param name = "Name" value = "Struktury a definice projekt&ugrave;" >
                //< param name = "Local" value = "html/modules.htm" >
                //</ OBJECT >
                //< LI > < OBJECT type = "text/sitemap" >
                //< param name = "Name" value = "Seznam relac&iacute;" >
                //< param name = "Local" value = "html/Relations.htm" >
                //</ OBJECT >
                //< LI > < OBJECT type = "text/sitemap" >
                //< param name = "Name" value = "Seznam QuickReports funkc&iacute;" >
                //< param name = "Local" value = "html/QRFunctionGroups.htm" >
                //</ OBJECT >
                //< LI > < OBJECT type = "text/sitemap" >
                //< param name = "Name" value = "Seznam importn&iacute;ch manažer&ugrave;" >
                //< param name = "Local" value = "html/ImportMgrList.htm" >
                //</ OBJECT >
                //</ UL >
                //</ BODY ></ HTML >

                #endregion

                //Soubor ktery vytvoři menu
                var TemplateFilePathNew = Path.Combine(pathToSave, Common.MES_Dokumentace_ + Common.HHC_Suffix);


                var htmlDoc = new HtmlDocument();

                htmlDoc.LoadHtml(Common.HTML_TEMPLATE_HHC);

                HtmlNode bodyElement = htmlDoc.DocumentNode.SelectSingleNode("//body");

                //TODO myšlenka je takova že to bude Nejak řešene pomoci XElementu a v kombinaci s Htmlnode a XmlTextWriter....

                //Vykašlat sa na XElement, ma problemy s kodovanim a medzerama a je tam toho vic...


                string xElement_Main = Get_Li_Object("Main", "Main.html", "Main.html");
                string xElement_List = Get_Li_Object("List Tabulek a Pohledů", @"html\List_Tables_Views.html", @"html\List_Tables_Views.html");

                List<string> ListxElTabulek = new List<string>();

                foreach (var rowGrup in grupTables)
                {
                    string TableName = rowGrup.Key;
                    string ClickToFile = Common.HTML_Tag__HTML + "\\" + Common.Table_Prefix + TableName + Common.HTML_Suffix;

                    string TypObjektu = string.Empty;

                    TypObjektu = rowGrup.First().typ;
                    string Text = string.Empty;

                    if (TypObjektu == "BASE TABLE")
                    {
                        Text = "Tabulka " + TableName;
                    }
                    else if (TypObjektu == "VIEW")
                    {
                        Text = "Pohled " + TableName;
                    }
                    else
                    {
                        Text = "Err " + TableName;
                    }

                    ListxElTabulek.Add(Get_Li_Object(Text, ClickToFile, ClickToFile));
                }

                //Tenhle zmrd špatně koduje stringy...
                //XElement item = new XElement(Common.HTML_Tag__UL,
                //                            xElement_Main,
                //                            new XElement(Common.HTML_Tag__UL, xElement_List,
                //                                new XElement(Common.HTML_Tag__UL, ListxElTabulek)
                //                                        )       
                //                            );

                string item = Get_Li_All(xElement_Main, xElement_List, ListxElTabulek);

                //HtmlNode newParaScripty = HtmlNode.CreateNode(item.ToString());
                HtmlNode newParaScripty = HtmlNode.CreateNode(item);

                bodyElement.ChildNodes.Add(newParaScripty);

                htmlDoc.Save(TemplateFilePathNew);

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        private static XElement GetParam(string Name, string Value)
        {
            return new XElement(Common.HTML_Tag__PARAM,
                                new XAttribute(Common.HTML_Tag__NAME, Name),
                                new XAttribute(Common.HTML_Tag__VALUE, Value),
                                string.Empty);
        }


        #region Pomocne metody HHC

        private static string Get_Li_All(string xElement_Main, string xElement_List, List<string> ListxElTabulek)
        {

            var stream = new MemoryStream();
            stream.Position = 0;

            XmlTextWriter w = new XmlTextWriter(stream, Encoding.UTF8);
            w.Formatting = Formatting.Indented;

            w.WriteStartElement(Common.HTML_Tag__UL);
            w.WriteRaw(xElement_Main); 

            w.WriteStartElement(Common.HTML_Tag__UL); 
            w.WriteRaw(xElement_List); 

            w.WriteStartElement(Common.HTML_Tag__UL); 

            foreach (string item in ListxElTabulek)
            {    
                w.WriteRaw(item); 
            }

            w.WriteEndElement(); 
            w.WriteEndElement();
            w.WriteEndElement();

            w.Flush();

            stream.Position = 0;
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();

            w.Close();

            return text;
        }


        private static string Get_Li_Object(string Name, string Local, string URL)
        {

            var stream = new MemoryStream();
            stream.Position = 0;

            //StringWriter s = new StringWriter();
            XElement Obj = GetOBJECTToContent(Name, Local, URL);

            XmlTextWriter w = new XmlTextWriter(stream, Encoding.UTF8);
            w.Formatting = Formatting.Indented;

            w.WriteStartElement(Common.HTML_Tag__LI);
            w.WriteRaw(Obj.ToString());
            w.WriteEndElement();
            w.Flush();

            stream.Position = 0;
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();

            w.Close();
            //string li_Object = s.ToString();

            //string li_Object = "<" + Common.HTML_Tag__LI + ">" +
            //    Obj.ToString() +
            //    "</" + Common.HTML_Tag__LI + ">";

            return  text;
        }

        private static XElement GetOBJECTToContent(string Name, string Local, string URL)
        {
            return new XElement(Common.HTML_Tag__OBJECT,
                                new XAttribute(Common.HTML_Tag__TYPE, Common.HTML_TYPE__SITEMAP),
                                GetParam(Common.NAME, Name),
                                GetParam(Common.LOCAL, Local),
                                GetParam(Common.URL, URL)
                                );
        }


        #endregion

        #region Pomocne metody HHK

        private static string Get_HHK_All(Dok ds)
        {
            List<string> all = new List<string>();

            List<string> Tabulky = Get_Tabulky_HHK(ds);
            all.AddRange(Tabulky);

            List<string> Columns = Get_Columns_HHK(ds);
            all.AddRange(Columns);
            
            return Get_UL_FromList(all);
        }

        private static List<string> Get_Columns_HHK(Dok ds)
        {
            List<string> ListxElTabulek = new List<string>();

            var GrupColumn = ds.DT_Documentation.GroupBy(x => x.COLUMN);

            foreach (IGrouping<string, Dok.DT_DocumentationRow> item in GrupColumn)
            {
                List<XElement> xElements = new List<XElement>();

                xElements.Add(GetParam(Common.NAME, item.Key));

                foreach (var row in item)
                {
                    string Text = string.Empty;
                    if (row.typ == "BASE TABLE")
                    {
                        Text = "MES Tabulka " + row.TABLE;
                    }
                    else if (row.typ == "VIEW")
                    {
                        Text = "MES Pohled " + row.TABLE;
                    }
                    else
                    {
                        Text = "Err " + row.TABLE;
                    }

                    xElements.Add(GetParam(Common.NAME, Text));

                    string ClickToFile = Common.HTML_Tag__HTML + "\\" + Common.Table_Prefix + row.TABLE + Common.HTML_Suffix;
                    xElements.Add(GetParam(Common.LOCAL, ClickToFile));
                    xElements.Add(GetParam(Common.URL, ClickToFile));

                }

                XElement x = GetOBJECTToContent(xElements);

                ListxElTabulek.Add(Get_Li_FromXElements(x));

            }

            return ListxElTabulek;
        }

        private static string Get_Li_FromXElements(XElement x)
        {
            var stream = new MemoryStream();
            stream.Position = 0;

            XmlTextWriter w = new XmlTextWriter(stream, Encoding.UTF8);
            w.Formatting = Formatting.Indented;

            w.WriteStartElement(Common.HTML_Tag__LI);
            
            w.WriteRaw(x.ToString());

            w.WriteEndElement();

            w.Flush();

            stream.Position = 0;
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();

            w.Close();

            return text;
        }

        private static XElement GetOBJECTToContent(List<XElement> list )
        {
            return new XElement(Common.HTML_Tag__OBJECT,
                                new XAttribute(Common.HTML_Tag__TYPE, Common.HTML_TYPE__SITEMAP),
                                list
                                );
        }

        private static List<string> Get_Tabulky_HHK(Dok ds)
        {
            List<string> ListxElTabulek = new List<string>();

            var grupTables = ds.DT_Documentation.GroupBy(x => x.TABLE);

            foreach (var rowGrup in grupTables)
            {
                string TableName = rowGrup.Key;
                string ClickToFile = Common.HTML_Tag__HTML + "\\" + Common.Table_Prefix + TableName + Common.HTML_Suffix;

                string TypObjektu = string.Empty;

                TypObjektu = rowGrup.First().typ;
                string Text = string.Empty;

                if (TypObjektu == "BASE TABLE")
                {
                    Text = "Tabulka " + TableName;
                }
                else if (TypObjektu == "VIEW")
                {
                    Text = "Pohled " + TableName;
                }

                ListxElTabulek.Add(Get_Li_Object_HHK(TableName, Text, ClickToFile, ClickToFile));
            }

            return ListxElTabulek;
        }

        private static string Get_UL_FromList(List<string> List)
        {

            var stream = new MemoryStream();
            stream.Position = 0;

            XmlTextWriter w = new XmlTextWriter(stream, Encoding.UTF8);
            w.Formatting = Formatting.Indented;

            w.WriteStartElement(Common.HTML_Tag__UL);

            foreach (string item in List)
            {
                w.WriteRaw(item);
            }

            w.WriteEndElement();

            w.Flush();

            stream.Position = 0;
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();

            w.Close();

            return text;
        }


        private static string Get_Li_Object_HHK(string Name0, string Name1, string Local, string URL)
        {

            var stream = new MemoryStream();
            stream.Position = 0;

            //StringWriter s = new StringWriter();
            XElement Obj = GetOBJECTToContent(Name0, Name1, Local, URL);

            XmlTextWriter w = new XmlTextWriter(stream, Encoding.UTF8);
            w.Formatting = Formatting.Indented;

            w.WriteStartElement(Common.HTML_Tag__LI);
            w.WriteRaw(Obj.ToString());
            w.WriteEndElement();
            w.Flush();

            stream.Position = 0;
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();

            w.Close();

            return text;
        }

        private static XElement GetOBJECTToContent(string Name0, string Name1, string Local, string URL)
        {
            return new XElement(Common.HTML_Tag__OBJECT,
                                new XAttribute(Common.HTML_Tag__TYPE, Common.HTML_TYPE__SITEMAP),
                                GetParam(Common.NAME, Name0),
                                GetParam(Common.NAME, Name1),
                                GetParam(Common.LOCAL, Local),
                                GetParam(Common.URL, URL)
                                );
        }


        #endregion
    }
}
