using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.ModulePohodaXML.XML
{
    public static class MST_Pohoda
    {

        public const string _unknown = "_unknown";
        public const string _export_faktura_vydana = "_export_fa_v";
        public const string _export_prevodka = "_export_prevodka";
        public const string _export_vydejka = "_export_vydejka";
        public const string _export_prodejka = "_export_prodejka";
        public const string _export_objednavka_prijata = "_export_obj_p";
        public const string _export_objednavka_vydana = "_export_obj_v";
        public const string _export_zasoby = "_export_zasoby";
        public const string _export_adresy = "_export_adresy";
        public const string _export_sklady = "_export_sklady";
        public const string _import_objednavka_prijata = "_import_obj_p";
        public const string _import_objednavka_vydana = "_import_obj_v";
        public const string _import_vydejka = "_import_vydejka";
        public const string _import_faktura = "_import_faktura";
        public const string _import_prijemka = "_import_prijemka";
        public const string _import_prodejka = "_import_prodejka";
        public const string _import_prevodka = "_import_prevodka";
        public const string _export_PrjateObjednavky = "_export_PrjateObjednavky";

        public static string FilenameCompose(string fileidentification)
        {
            return DateTime.Now.ToString("yyMMdd") + "_" + Guid.NewGuid() + fileidentification;
        }

    }
}
