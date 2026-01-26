using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Uzivatele
	{

		#region SQL script

		//create table [Users]
		//(
		//    [ID] int not null,
		//    [Login] nvarchar(100) not null,
		//    [Pwd] nvarchar(128),
		//    [Hash] nvarchar(128),
		//    [EAN] nvarchar(50),
		//    [FIRSTNAME] nvarchar(15),
		//    [SECONDNAME] nvarchar(40)
		//);
		//alter table [Users] add primary key ([ID]);
		//create unique index UQ__Users__0000000000000010 on [Users] ([Login]);

		#endregion

		private  string TableName = "Users";
		public  Dictionary<string, ColumnType> ColumnsInfo_Users = new Dictionary<string, ColumnType>();

		#region c'tor

		public Uzivatele(DS_Information ds)
		{

            #region z XML souboru
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_user = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName.Trim());

            foreach (var dr in DT_user)
            {
                ColumnsInfo_Users.AddIfNotExists(dr);
            } 
            #endregion

		}

		#endregion

	}
}

