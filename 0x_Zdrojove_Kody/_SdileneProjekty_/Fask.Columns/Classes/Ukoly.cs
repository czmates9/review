using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public class Ukoly
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

		private  string TableName_U = "CZ_UKOL";
		private  string TableName_US = "CZ_UKOL_STATE";
		private  string TableName_UU = "CZ_UKOL_UZIV";

		public  Dictionary<string, ColumnType> ColumnsInfo_CZ_UKOL = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZ_UKOL_STATE = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZ_UKOL_UZIV = new Dictionary<string, ColumnType>();
		#region c'tor

		public Ukoly(DS_Information ds)
		{


            #region z xml souboru

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_Ukol = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_U.Trim());

            foreach (var dr in DT_Ukol)
            {
                ColumnsInfo_CZ_UKOL.AddIfNotExists(dr);
            }

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_Ukol_State = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_US.Trim());

            foreach (var dr in DT_Ukol_State)
            {
                ColumnsInfo_CZ_UKOL_STATE.AddIfNotExists(dr);
            }

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_Ukol_Uziv = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_UU.Trim());

            foreach (var dr in DT_Ukol_Uziv)
            {
                ColumnsInfo_CZ_UKOL_UZIV.AddIfNotExists(dr);
            } 

            #endregion

		}

		#endregion

	}
}

