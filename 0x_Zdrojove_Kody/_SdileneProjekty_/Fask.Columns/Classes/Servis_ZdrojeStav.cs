using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Servis_ZdrojeStav
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

		private  string TableName_Pred = "CZMST_Servis_Predloha";
		private  string TableName_ZS = "CZMST_Servis_ZdrojStav";
		private  string TableName_ZST = "CZMST_Servis_ZdrojStavTmp";
		private  string TableName_Param = "Parametry";

		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Predloha = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_ZdrojStav = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_ZdrojStavTmp = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_Parametry = new Dictionary<string, ColumnType>();

		#region c'tor

		public Servis_ZdrojeStav(DS_Information ds)
		{

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_1 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_Pred.Trim());

            foreach (var dr in DT_1)
            {
                ColumnsInfo_CZMST_Servis_Predloha.AddIfNotExists(dr);
            }

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_2 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_ZS.Trim());

            foreach (var dr in DT_2)
            {
                ColumnsInfo_CZMST_Servis_ZdrojStav.AddIfNotExists(dr);
            }

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_3 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_ZST.Trim());

            foreach (var dr in DT_3)
            {
                ColumnsInfo_CZMST_Servis_ZdrojStavTmp.AddIfNotExists(dr);
            }

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_4 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_Param.Trim());

            foreach (var dr in DT_4)
            {
                ColumnsInfo_Parametry.AddIfNotExists(dr);
            }

		}

		#endregion

	}
}

