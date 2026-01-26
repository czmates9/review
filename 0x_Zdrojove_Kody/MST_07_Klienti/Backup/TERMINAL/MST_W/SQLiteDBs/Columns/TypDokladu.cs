

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class TypDokladu
	{

		#region SQL script

		//create table [CZMST092]
		//(
		//    [doc_id] nvarchar(12) not null,
		//    [doc_desc] nvarchar(31),
		//    [doc_typ] nvarchar(3),
		//    [doc_carcode] nvarchar(21),
		//    [DEX_ROW_ID] int not null,
		//    [LOCNCODE] nvarchar(11) not null,
		//    [cfg_odb] tinyint not null,
		//    [cfg_str] tinyint not null,
		//    [cfg_disp] tinyint not null,
		//    [cfg_disp_proc] nvarchar(30),
		//    [cfg_disp_param1] nvarchar(10),
		//    [cfg_disp_param2] nvarchar(10),
		//    [cfg_mn2sn] tinyint not null,
		//    [cfg_prac] tinyint not null default 0,
		//    [cfg_palety] tinyint not null default 0,
		//    [cfg_paleta_id] tinyint not null,
		//    [cfg_zakazka_id] tinyint not null,
		//    [cfg_mena_id] tinyint,
		//    [doc_id2] nvarchar(12) not null default '',
		//    [cfg_tisk] tinyint not null,
		//    [cfg_prevod_sklad] tinyint not null,
		//    [cfg_tisk_soupis] tinyint,
		//    [SKL_ID] nvarchar(20),
		//    [cfg_lokace] tinyint,
		//    [cfg_lokace_ciselnik] tinyint,
		//    [cfg_lokace_dest] tinyint,
		//    [cfg_onl_dop_pal] tinyint,
		//    [cfg_onl_over_lokace] tinyint,
		//    [cfg_onl_over_lokace_dest] tinyint,
		//    [cfg_mnozstvi_ze_zbozi] tinyint,
		//    [cfg_predvyplnit_mnozstvi] tinyint,
		//    [cfg_skl_id_dest] tinyint,
		//    [predvyplnit_skl_id_dest] nvarchar(20),
		//    [cfg_lok_mech] tinyint,
		//    [cfg_lok_mech_pohyb_type] nvarchar(1),
		//    [cfg_skl_id_dest_prevzit] tinyint,
		//    [cfg_lokace_dest_ciselnik] tinyint,
		//    [predvyplnit_locncodedest] nvarchar(11),
		//    [cfg_sklady] tinyint,
		//    [cfg_onl_dop_lokace_dest] tinyint,
		//    [cfg_generovat_sn] tinyint,
		//    [cfg_parsovat_ck] tinyint,
		//    [cfg_sn_na_davku] tinyint,
		//    [cfg_lok_mech_online_pohyby] tinyint,
		//    [cfg_onl_palety_generovat] tinyint,
		//    [cfg_tisk_palety] tinyint,
		//    [cfg_sklady_zmena] tinyint,
		//    [cfg_delka_SN] int
		//);

		#endregion

		private static string TableName = "CZMST092";
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST092 = new Dictionary<string, ColumnType>();

		#region c'tor

		static TypDokladu()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "TypDokladu.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName.Trim())
            //        {
            //            ColumnsInfo_CZMST092.AddIfNotExists(dr);
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


