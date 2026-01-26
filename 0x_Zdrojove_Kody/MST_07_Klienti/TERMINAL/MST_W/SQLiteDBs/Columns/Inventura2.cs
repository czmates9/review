using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Inventura2
	{

		#region SQL script

		//create table [HLAVICKY]
		//(
		//    [ID] int not null,
		//    [GUID] uniqueidentifier not null
		//);

		//create table [INVENTUR]
		//(
		//    [ID] int not null,
		//    [KATEGORIE] nvarchar(2),
		//    [I_CISLO] nvarchar(11),
		//    [NAZEV] nvarchar(35),
		//    [STRED] nvarchar(6),
		//    [OSOBA] int,
		//    [LOKACE1] nvarchar(25),
		//    [LOKACE2] nvarchar(25),
		//    [KANCELAR] nvarchar(6),
		//    [EAN] nvarchar(50),
		//    [KUSU] numeric,
		//    [KLIC_LOK] int,
		//    [ID_INV] numeric,
		//    [OS_ZPR] nvarchar(20),
		//    [ID_TERM] numeric,
		//    [CAS_ZPR] datetime,
		//    [ID_MAJETEK] int not null
		//);
		//alter table [INVENTUR] add primary key ([ID]);
		//create index EAN on [INVENTUR] ([EAN]);
		//create index FILTR_CASZPR on [INVENTUR] ([CAS_ZPR]);
		//create index FILTR_ICISLO on [INVENTUR] ([I_CISLO]);
		//create index FILTR_IDMAJETEK on [INVENTUR] ([ID_MAJETEK]);
		//create index FILTR_KANCELAR on [INVENTUR] ([KANCELAR]);
		//create index FILTR_KATEGORIE on [INVENTUR] ([KATEGORIE]);
		//create index FILTR_LOKACE on [INVENTUR] ([KLIC_LOK]);
		//create index FILTR_NAZEV on [INVENTUR] ([NAZEV]);
		//create index FILTR_OSOBA on [INVENTUR] ([OSOBA]);
		//create index FILTR_STRED on [INVENTUR] ([STRED]);
		//create index FILTR_UZIVATEL on [INVENTUR] ([OS_ZPR]);
		//create index ZAZNAM on [INVENTUR] ([KATEGORIE],[I_CISLO]);

		//create table [KANCL]
		//(
		//    [KANCL] nvarchar(6) not null,
		//    [STRE] nvarchar(6),
		//    [TEXT] nvarchar(50),
		//    [NAZEV] nvarchar(50),
		//    [EAN] nvarchar(20)
		//);
		//alter table [KANCL] add primary key ([KANCL]);
		//create index FILTR_TEXT on [KANCL] ([TEXT]);

		//create table [LOKACE]
		//(
		//    [LOKACE1] nvarchar(25),
		//    [LOKACE2] nvarchar(25),
		//    [EANL] nvarchar(20),
		//    [NAZEV] nvarchar(30) not null,
		//    [KLIC_LOK] int not null,
		//    [KLIC_KAT] int,
		//    [KLIC_K_OLD] int
		//);
		//alter table [LOKACE] add primary key ([KLIC_LOK]);
		//create index EANL on [LOKACE] ([EANL]);
		//create index FILTR_NAZEV on [LOKACE] ([NAZEV]);

		//create table [MAJETEK]
		//(
		//    [ID] int not null,
		//    [KATEGORIE] nvarchar(2),
		//    [I_CISLO] nvarchar(11),
		//    [NAZEV] nvarchar(60),
		//    [STRED] nvarchar(6),
		//    [OSOBA] int,
		//    [LOKACE1] nvarchar(25),
		//    [LOKACE2] nvarchar(25),
		//    [KANCELAR] nvarchar(6),
		//    [EAN] nvarchar(50),
		//    [KUSU] numeric,
		//    [KLIC_LOK] int,
		//    [ID_INV] nvarchar(20),
		//    [ID_TERM] numeric,
		//    [NACTENO] numeric default 0
		//);
		//alter table [MAJETEK] add primary key ([ID]);
		//create index EAN on [MAJETEK] ([EAN]);
		//create index FILTR_KANCELAR on [MAJETEK] ([KANCELAR]);
		//create index FILTR_KLICLOK on [MAJETEK] ([KLIC_LOK]);
		//create index FILTR_NAZEV on [MAJETEK] ([NAZEV]);
		//create index FILTR_OSOBA on [MAJETEK] ([OSOBA]);
		//create index FILTR_STRED on [MAJETEK] ([STRED]);
		//create index ZAZNAM on [MAJETEK] ([KATEGORIE],[I_CISLO]);

		//create table [OSOBY]
		//(
		//    [OSOBA_ZODP] int not null,
		//    [TITUL] nvarchar(6),
		//    [PRIJMENI] nvarchar(20),
		//    [JMENO] nvarchar(12)
		//);
		//alter table [OSOBY] add primary key ([OSOBA_ZODP]);
		//create index FILTR_JMENO on [OSOBY] ([JMENO]);
		//create index FILTR_PRIJMENI on [OSOBY] ([PRIJMENI]);
		//create index FILTR_TITUL on [OSOBY] ([TITUL]);

		//create table [Parametry]
		//(
		//    [CFG_UpozornitNaPrebytek] bit not null,
		//    [CFG_DalsiPolozkuBezDotazu] bit not null,
		//    [CFG_KontrolaUplnostiPolozky] bit not null,
		//    [CFG_PoZadaniSNZpetNaMN] bit not null,
		//    [CFG_PredvyplnitMnozstvi] bit not null,
		//    [CFG_PredvyplnitMnozstviZbyvajici] bit not null,
		//    [CFG_PredvyplnitMnozstviOJedna] bit not null,
		//    [CFG_PovolitDuplicituSN] bit not null,
		//    [CFG_MnozstviScannerem] bit not null,
		//    [CFG_PosunNaDalsiPolozku] bit not null,
		//    [CFG_PovolitZaporneMnozstvi] bit not null,
		//    [CFG_KontrolaUplnosti] bit not null,
		//    [CFG_PovolitZmenuLokace] bit not null
		//);

		//create table [UCSTR]
		//(
		//    [STREDISKO] nvarchar(6) not null,
		//    [NAZEV] nvarchar(50),
		//    [UCETNI] nvarchar(3),
		//    [STRED2] nvarchar(3),
		//    [CINNOST] nvarchar(3),
		//    [ZAK] nvarchar(1),
		//    [AKTIVNI] tinyint not null,
		//    [KLIC_STA] int,
		//    [CASZAPSANI] datetime,
		//    [STRUKT] tinyint not null,
		//    [STRUKTSEZN] nvarchar(800)
		//);
		//alter table [UCSTR] add primary key ([STREDISKO]);
		//create index FILTR_NAZEV on [UCSTR] ([NAZEV]);

		#endregion

		private static string TableName_H = "HLAVICKY";
		private static string TableName_I = "INVENTUR";
		private static string TableName_K = "KANCL";
		private static string TableName_L = "LOKACE";
		private static string TableName_M = "MAJETEK";
		private static string TableName_O = "OSOBY";
		private static string TableName_P = "Parametry";
		private static string TableName_U = "UCSTR";



		public static Dictionary<string, ColumnType> ColumnsInfo_HLAVICKY = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_INVENTUR = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_KANCL = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_LOKACE = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_MAJETEK = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_OSOBY = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_Parametry = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_UCSTR = new Dictionary<string, ColumnType>();

		#region c'tor

		static Inventura2()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Inventura2.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);

            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_H.Trim())
            //        {
            //            ColumnsInfo_HLAVICKY.AddIfNotExists(dr);
            //            continue;
            //        }

            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_I.Trim())
            //        {
            //            ColumnsInfo_INVENTUR.AddIfNotExists(dr);
            //            continue;
            //        }

            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_K.Trim())
            //        {
            //            ColumnsInfo_KANCL.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_L.Trim())
            //        {
            //            ColumnsInfo_LOKACE.AddIfNotExists(dr);
            //            continue;
            //        }

            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_M.Trim())
            //        {
            //            ColumnsInfo_MAJETEK.AddIfNotExists(dr);
            //            continue;
            //        }

            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_O.Trim())
            //        {
            //            ColumnsInfo_OSOBY.AddIfNotExists(dr);
            //            continue;
            //        }

            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_P.Trim())
            //        {
            //            ColumnsInfo_Parametry.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_U.Trim())
            //        {
            //            ColumnsInfo_UCSTR.AddIfNotExists(dr);
            //            continue;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logging.Log.Write(ex);
            //}
		}

		#endregion

	}
}

