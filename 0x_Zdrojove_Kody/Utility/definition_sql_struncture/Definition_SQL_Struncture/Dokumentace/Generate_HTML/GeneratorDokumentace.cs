using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using HtmlAgilityPack;
using System.Xml.Linq;



namespace Definition_SQL_Struncture.Dokumentace.Generate_HTML
{
    public class GeneratorDokumentace : IDisposable
    {
        /// <summary>
        /// Hlavná cesta
        /// </summary>
        private string _dirlog = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Cesta k Template souborom
        /// </summary>
        private string _templatePath
        {
            get
            {
                return Path.Combine(_dirlog, Common.Template_Folder);
            }
        }
            
        /// <summary>
        /// Cesta kam se uloží vygenerovana dokumentace
        /// </summary>
        private string _pathToSave
        {
            get
            {
                return System.IO.Path.Combine(_dirlog, _folderName);
            }
        }

        private string _folderName { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public GeneratorDokumentace()
        {
            _folderName = Common.Dokumentace_Prefix  + DateTime.Now.ToString(Common.Dokumentace_DatumFormat);
        }

        /// <summary>
        /// Dispose
        /// Zatím netuším k čemu, ale neco se najde :D 
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// Medtoda která spustí automatickou tvorbu dokumentace
        /// </summary>
        /// <param name="ds">Naplnení dataset s datatable která nase všechny informace pro dokumentaci</param>
        public void Generuj(Dok ds)
        {
            //Grupnuti tabulky podle kliče, nazev Tabulky
            var GrupTables = ds.DT_Documentation.GroupBy(x => x.TABLE);

            Classes.HelperClass_V2.SkopirujPotrebneSoubory(_templatePath, _pathToSave);

            Classes.HelperClass_V2.GenerujHlavniSeznamTabulek(_templatePath, _pathToSave, GrupTables);

            Classes.HelperClass_V2.GenerujJednotliveTabulky_a_Pohledy(_templatePath, _pathToSave, GrupTables);

            Classes.HelperClass_V2.Generate_Projekt_HTML(_pathToSave, GrupTables, ds);

        }


    }
}
