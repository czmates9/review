using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Servis_ZdrojeStav
	{

		#region SQL script

		//create table [CZMST_Servis_Predloha]
		//(
		//    [CountEntries] int not null,
		//    [DOCUMENT_NUMBER] nvarchar(17),
		//    [Rozpracovano] tinyint not null,
		//    [OkruhID] nvarchar(20) not null,
		//    [UserID] int,
		//    [Barcode] nvarchar(50),
		//    [ODB_ID] nvarchar(12)
		//);

		//create table [CZMST_Servis_ZdrojStav]
		//(
		//    [IDZdroj] nvarchar(20) not null,
		//    [IDStav] nvarchar(20) not null,
		//    [IDCinnost] nvarchar(20),
		//    [Modified] datetime not null,
		//    [IDTerminal] int,
		//    [IDUser] int,
		//    [GUID] uniqueidentifier,
		//    [CinnostValue] nvarchar(50),
		//    [CinnostType] nvarchar(2),
		//    [CountEntries] int,
		//    [ODB_ID] nvarchar(12),
		//    [OkruhID] nvarchar(20),
		//    [CinnostOznaceni] nvarchar(50),
		//    [GPS_X] float,
		//    [GPS_Y] float,
		//    [GPS_Z] int
		//);

		//create table [CZMST_Servis_ZdrojStavTmp]
		//(
		//    [ZdrojID] nvarchar(20) not null,
		//    [Rozpracovano] tinyint not null default 0,
		//    [Dokonceno] datetime
		//);
		//alter table [CZMST_Servis_ZdrojStavTmp] add primary key ([ZdrojID]);

		//create table [Parametry]
		//(
		//    [CONFIG_KONT_DOKONCENOSTI] bit not null
		//);

		#endregion

		private static string TableName_Pred = "CZMST_Servis_Predloha";
		private static string TableName_ZS = "CZMST_Servis_ZdrojStav";
		private static string TableName_ZST = "CZMST_Servis_ZdrojStavTmp";
		private static string TableName_Param = "Parametry";

		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Predloha = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_ZdrojStav = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_ZdrojStavTmp = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_Parametry = new Dictionary<string, ColumnType>();

		#region c'tor

		static Servis_ZdrojeStav()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Servis_ZdrojeStav.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_Pred.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_Predloha.AddIfNotExists(dr);
            //            continue;
            //        }

            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_ZS.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_ZdrojStav.AddIfNotExists(dr);
            //            continue;
            //        }

            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_ZST.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_ZdrojStavTmp.AddIfNotExists(dr);
            //            continue;
            //        }

            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_Param.Trim())
            //        {
            //            ColumnsInfo_Parametry.AddIfNotExists(dr);
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

