using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public class Servis_Ciselniky
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

		private  string TableName_C = "CZMST_Servis_Cinnost";
		private  string TableName_CN = "CZMST_Servis_CinnostNext";
		private  string TableName_DT = "CZMST_Servis_Dynamic_Table";
		private  string TableName_DTD = "CZMST_Servis_Dynamic_Table_Definition";
		private  string TableName_SO = "CZMST_Servis_Okruh";
		private  string TableName_SS = "CZMST_Servis_Stav";
		private  string TableName_SSN = "CZMST_Servis_StavNext";
		private  string TableName_SZ = "CZMST_Servis_Zdroj";
		private  string TableName_SZS = "CZMST_Servis_ZdrojSeznam";


		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Cinnost = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_CinnostNext = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Dynamic_Table = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Dynamic_Table_Definition = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Okruh = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Stav = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_StavNext = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_Zdroj = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_ZdrojSeznam = new Dictionary<string, ColumnType>();


		#region c'tor

		public Servis_Ciselniky(DS_Information ds)
		{
            #region z XML souboru

            #region C

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_1 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_C.Trim());

            foreach (var dr in DT_1)
            {
                ColumnsInfo_CZMST_Servis_Cinnost.AddIfNotExists(dr);
            }

            #endregion

            #region CN
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_2 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_CN.Trim());

            foreach (var dr in DT_2)
            {
                ColumnsInfo_CZMST_Servis_CinnostNext.AddIfNotExists(dr);
            }
            #endregion

            #region DT
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_3 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_DT.Trim());

            foreach (var dr in DT_3)
            {
                ColumnsInfo_CZMST_Servis_Dynamic_Table.AddIfNotExists(dr);
            }
            #endregion

            #region DTD
           
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_4 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_DTD.Trim());

            foreach (var dr in DT_4)
            {
                ColumnsInfo_CZMST_Servis_Dynamic_Table_Definition.AddIfNotExists(dr);
            }
            #endregion

            #region SO
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_5 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_SO.Trim());

            foreach (var dr in DT_5)
            {
                ColumnsInfo_CZMST_Servis_Okruh.AddIfNotExists(dr);
            }
            #endregion

            #region SS
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_6 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_SS.Trim());

            foreach (var dr in DT_6)
            {
                ColumnsInfo_CZMST_Servis_Stav.AddIfNotExists(dr);
            }
            #endregion

            #region SSN
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_7 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_SSN.Trim());

            foreach (var dr in DT_7)
            {
                ColumnsInfo_CZMST_Servis_StavNext.AddIfNotExists(dr);
            }
            #endregion

            #region SZ
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_8 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_SZ.Trim());

            foreach (var dr in DT_8)
            {
                ColumnsInfo_CZMST_Servis_Zdroj.AddIfNotExists(dr);
            }
            #endregion

            #region SZS
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_9 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_SZS.Trim());

            foreach (var dr in DT_9)
            {
                ColumnsInfo_CZMST_Servis_ZdrojSeznam.AddIfNotExists(dr);
            }
            #endregion


            #endregion

		}

		#endregion

	}
}

