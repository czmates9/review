using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Fask.Columns
{
	public static class DictionaryExtension
	{
		public static void AddIfNotExists(this Dictionary<string, ColumnType> dictionary, DataRow dr)
		{

			CheckDictionaryIsNull(dictionary);

			string Key = (string)dr["COLUMN_NAME"];
			int? MaxLen = null;
			object tmp = dr["character_maximum_length"];

			if(tmp != null)
			{
				if(tmp is int)
					MaxLen = Convert.ToInt32(tmp);
			}

			string DataType = (string)dr["DATA_TYPE"];

			if (!dictionary.ContainsKey(Key))
			{
				dictionary.Add(Key, new ColumnType(Key, MaxLen, DataType));
			}
		
		}

		private static void CheckDictionaryIsNull(this Dictionary<string, ColumnType> dictionary)
		{
			if (dictionary == null) throw new ArgumentNullException();
		}

	}
}
