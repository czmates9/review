using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Fask.Columns.CreateSQL
{
	public class SQLLoad 
	{
        //public static DS_Information DS_Information = null;

		public static DS_Information SQLLoadData(string _fullFileName) 
		{
			DS_Information ds = new DS_Information();

            //string _fullFileName = Properties.Settings.Default.PathToDS_Information;

			//string PathSQL = Path.Combine(_fullFileName, "DS_Information.xml");

			ds.Clear();
			ds.ReadXml(_fullFileName);
			return ds;
		}
		
	}
}
