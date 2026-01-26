using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public class ColumnType
	{
		//
		//Trida soužicí pro neseni informaci o stloupcu
		public string Name;
		public int? MaxLength;
		string DataType;
		//
		//
		//
		//Typ
		//alow DB null
		//...

		public ColumnType(string name, int? maxlength,string DataType) 
		{
			this.Name = name;
			this.MaxLength = maxlength;
			this.DataType = DataType;
		}
	}

}
