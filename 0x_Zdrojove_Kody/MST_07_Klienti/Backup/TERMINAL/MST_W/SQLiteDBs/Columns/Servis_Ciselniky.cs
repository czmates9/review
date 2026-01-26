using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Servis_Ciselniky
	{

		#region SQL script

		//create table [CZMST_Servis_Cinnost]
		//(
		//    [ID] nvarchar(20) not null,
		//    [Oznaceni] nvarchar(50) not null,
		//    [Barcode] nvarchar(50),
		//    [TYPE] nvarchar(2) not null,
		//    [TYPEVALUE] nvarchar(20),
		//    [Mandatory] tinyint not null,
		//    [RequiredLength] int
		//);

		//create table [CZMST_Servis_CinnostNext]
		//(
		//    [ID] nvarchar(20) not null,
		//    [IDNext] nvarchar(20),
		//    [IDValue] nvarchar(20)
		//);

		//create table [CZMST_Servis_Dynamic_Table]
		//(
		//    [ID] nvarchar(20) not null,
		//    [Oznaceni] nvarchar(50) not null,
		//    [Barcode] nvarchar(50)
		//);
		//alter table [CZMST_Servis_Dynamic_Table] add primary key ([ID]);

		//create table [CZMST_Servis_Dynamic_Table_Definition]
		//(
		//    [FullName] nvarchar(50) not null,
		//    [TypeName] nvarchar(20) not null
		//);
		//alter table [CZMST_Servis_Dynamic_Table_Definition] add primary key ([FullName],[TypeName]);

		//create table [CZMST_Servis_Okruh]
		//(
		//    [ID] nvarchar(20) not null,
		//    [Oznaceni] nvarchar(50) not null,
		//    [ODB_ID] nvarchar(12),
		//    [Barcode] nvarchar(50),
		//    [ZdrojSeznamID] nvarchar(20) not null
		//);
		//alter table [CZMST_Servis_Okruh] add primary key ([ID]);

		//create table [CZMST_Servis_Stav]
		//(
		//    [ID] nvarchar(20) not null,
		//    [Oznaceni] nvarchar(50) not null,
		//    [IDCinnost] nvarchar(20),
		//    [Barcode] nvarchar(50)
		//);

		//create table [CZMST_Servis_StavNext]
		//(
		//    [ID] nvarchar(20) not null,
		//    [IDNext] nvarchar(20) not null
		//);

		//create table [CZMST_Servis_Zdroj]
		//(
		//    [ID] nvarchar(20) not null,
		//    [Oznaceni] nvarchar(50) not null,
		//    [Barcode] nvarchar(50),
		//    [Type] nvarchar(50),
		//    [Misto] nvarchar(20)
		//);

		//create table [CZMST_Servis_ZdrojSeznam]
		//(
		//    [ID] nvarchar(20) not null,
		//    [ZdrojID] nvarchar(20) not null,
		//    [Poradi] int,
		//    [IDStav] nvarchar(20),
		//    [IDCinnost] nvarchar(20)
		//);
		//alter table [CZMST_Servis_ZdrojSeznam] add primary key ([ID],[ZdrojID]);

		#endregion

		private static string TableName_C = "CZMST_Servis_Cinnost";
		private static string TableName_CN = "CZMST_Servis_CinnostNext";
		private static string TableName_DT = "CZMST_Servis_Dynamic_Table";
		private static string TableName_DTD = "CZMST_Servis_Dynamic_Table_Definition";
		private static string TableName_SO = "CZMST_Servis_Okruh";
		private static string TableName_SS = "CZMST_Servis_Stav";
		private static string TableName_SSN = "CZMST_Servis_StavNext";
		private static string TableName_SZ = "CZMST_Servis_Zdroj";
		private static string TableName_SZS = "CZMST_Servis_ZdrojSeznam";


		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Cinnost = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_CinnostNext = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Dynamic_Table = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Dynamic_Table_Definition = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Okruh = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Stav = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_StavNext = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Zdroj = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_ZdrojSeznam = new Dictionary<string, ColumnType>();


		#region c'tor

		static Servis_Ciselniky()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Servis_Ciselniky.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_C.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_Cinnost.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_CN.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_CinnostNext.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_DT.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_Dynamic_Table.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_DTD.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_Dynamic_Table_Definition.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_SO.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_Okruh.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_SS.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_Stav.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_SSN.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_StavNext.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_SZ.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_Zdroj.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_SZS.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_ZdrojSeznam.AddIfNotExists(dr);
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

