using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.WEBAPI.API_BusinessObjects
{
	public class MachinesDefinition_objekt
	{
		public string IP;
		public string Description;
		public string MType;
		public int? PORT;
		public int ID_group;
	}


	public class MachinesDefinitionMeasurement_objekt
	{
		public string IP;
		public string IP_M;
		public string Description_M;
		public string MType_M;
		public int? PORT_M;
		public int? ID_group_M;
	}


}