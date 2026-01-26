using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;
using System.IO;
using System.Data.Common;
using Fask.Logging;

namespace Fask.Module.ABRA.CarpServise
{
    public partial class Provider : 
		Fask.Server.Interfaces.Tisky.ITisky2,
		Fask.Server.Interfaces.Tisky.ITisky2_MetodaSoupis
    {

		#region ITisky2_MetodaSoupis Members

		public bool TiskMetodaSoupis(
			ref Fask.Server.Interfaces.DataSets.DSValues dataHeader,
			ref List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList,
			ref Fask.Server.Interfaces.DataSets.DSValues dataFooter)
		{

			try
			{
				foreach (Fask.Server.Interfaces.DataSets.DSValues.ValuesRow item in dataHeader.Values)
				{
					if (item.Key == "SOPNUMBE")
					{

						dataHeader.Values.BeginLoadData();

						ABRA_Datasets.Tisk.DataTiskRow Row = Database.ABRA.Tisk_GetData(item.Value);

						if (Row != null)
						{
							dataHeader.Values.AddValuesRow("CITY",Row.IsCITYNull() ? string.Empty : Row.CITY );
							dataHeader.Values.AddValuesRow("COUNTRY", Row.IsCOUNTRYNull() ? string.Empty : Row.COUNTRY);
							dataHeader.Values.AddValuesRow("NAME", Row.IsNAMENull() ? string.Empty : Row.NAME);
							dataHeader.Values.AddValuesRow("POSTCODE", Row.IsPOSTCODENull() ? string.Empty : Row.POSTCODE);
							dataHeader.Values.AddValuesRow("STREET", Row.IsSTREETNull() ? string.Empty : Row.STREET);
							break;
						}


						dataHeader.Values.EndLoadData();
						dataHeader.AcceptChanges();
					}
				}


			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}

			return true;
		}

		#endregion
	}
}
