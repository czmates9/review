using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Ukoly
	{

		#region SQL script

		//create table [CZ_UKOL]
		//(
		//    [ID] int not null,
		//    [Name] nvarchar(50) not null,
		//    [Description] ntext not null,
		//    [Code] nvarchar(50),
		//    [CreatorID] int not null,
		//    [DateCreated] datetime not null,
		//    [DateFrom] datetime,
		//    [DateTo] datetime,
		//    [State] nvarchar(1) not null,
		//    [Kind] nvarchar(1),
		//    [Type] nvarchar(1),
		//    [Priority] int not null,
		//    [PartnerID] nvarchar(20)
		//);
		//alter table [CZ_UKOL] add primary key ([ID]);

		//create table [CZ_UKOL_STATE]
		//(
		//    [State] nvarchar(1) not null,
		//    [Description] nvarchar(50) not null,
		//    [IsStart] bit not null,
		//    [IsEnd] bit not null
		//);
		//alter table [CZ_UKOL_STATE] add primary key ([State]);

		//create table [CZ_UKOL_UZIV]
		//(
		//    [ID] int not null,
		//    [UkolID] int not null,
		//    [UserID] int not null,
		//    [State] nvarchar(1) not null,
		//    [DateChanged] datetime,
		//    [UserIDChanged] int,
		//    [Note] nvarchar(200),
		//    [DateNotify] datetime,
		//    [DateFinished] datetime
		//);
		//alter table [CZ_UKOL_UZIV] add primary key ([ID]);

		#endregion

		private static string TableName_U = "CZ_UKOL";
		private static string TableName_US = "CZ_UKOL_STATE";
		private static string TableName_UU = "CZ_UKOL_UZIV";

		public static Dictionary<string, ColumnType> ColumnsInfo_CZ_UKOL = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZ_UKOL_STATE = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZ_UKOL_UZIV = new Dictionary<string, ColumnType>();

		#region c'tor

		static Ukoly()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Ukoly.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_U.Trim())
            //        {
            //            ColumnsInfo_CZ_UKOL.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_US.Trim())
            //        {
            //            ColumnsInfo_CZ_UKOL_STATE.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_UU.Trim())
            //        {
            //            ColumnsInfo_CZ_UKOL_UZIV.AddIfNotExists(dr);
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

