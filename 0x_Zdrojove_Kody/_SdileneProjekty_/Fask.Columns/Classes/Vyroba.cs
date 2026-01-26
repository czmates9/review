using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Vyroba
	{

		#region SQL script

        #region Production

            //        /****** Object:  Table [dbo].[Production]    Script Date: 21.02.2019 14:32:14 ******/
            //SET ANSI_NULLS ON
            //GO

            //SET QUOTED_IDENTIFIER ON
            //GO

            //SET ANSI_PADDING ON
            //GO

            //CREATE TABLE [dbo].[Production](
            //    [CountEntries] [int] NULL,
            //    [SOPNUMBE] [varchar](17) NULL,
            //    [ITEMNMBR] [varchar](31) NULL,
            //    [ITEMTYPE] [varchar](11) NULL DEFAULT (''),
            //    [ITEMMJ] [varchar](5) NULL,
            //    [ITEMDESC] [varchar](51) NULL,
            //    [ORD] [int] NULL,
            //    [TIMEMODE] [int] NULL DEFAULT ((0)),
            //    [TIMEPREPSTART] [datetime] NULL,
            //    [TIMEPREPSTOP] [datetime] NULL,
            //    [TIMEPREP] [real] NULL,
            //    [TIMEUNIT] [real] NULL,
            //    [TIMESTART] [datetime] NULL,
            //    [TIMESTOP] [datetime] NULL,
            //    [TIMECORSTART] [datetime] NULL,
            //    [TIMECORSTOP] [datetime] NULL,
            //    [TIMECOR] [real] NULL,
            //    [TIMECRID] [int] NULL,
            //    [TIMECRIDTYPE] [tinyint] NULL,
            //    [id] [int] IDENTITY(1,1) NOT NULL,
            //    [loginid] [varchar](10) NOT NULL,
            //    [machineid] [varchar](16) NULL,
            //    [operationid] [varchar](16) NULL,
            //    [dateeve] [datetime] NOT NULL,
            //    [qty] [numeric](19, 5) NOT NULL,
            //    [qtyReal] [numeric](19, 5) NOT NULL,
            //    [QTYPACK] [numeric](19, 5) NULL,
            //    [QTYPACKMJ] [varchar](5) NULL,
            //    [description] [ntext] NULL,
            //    [BarcodeP] [varchar](31) NULL,
            //    [UserID] [varchar](10) NOT NULL,
            //    [TermID] [tinyint] NOT NULL,
            //    [ISOK] [datetime] NULL,
            //    [GUID] [uniqueidentifier] NOT NULL,
            //    [SOUBEHGUID] [uniqueidentifier] NULL,
            //    [CORRGUID] [uniqueidentifier] NULL,
            //    [qtyOld] [numeric](19, 5) NULL,
            //    [idVS] [varchar](10) NULL,
            //    [dateedit] [datetime] NULL,
            //    [SKL_ID] [nvarchar](20) NULL,
            //    [LOCNCODE] [nvarchar](11) NULL,
            // CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
            //(
            //    [id] ASC
            //)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            //) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

            //GO

            //SET ANSI_PADDING OFF
            //GO

        #endregion

		#endregion

        
        private  string TableName_Production = "Production";
        private  string TableName_Groups = "Groups";
        

        
        public  Dictionary<string, ColumnType> ColumnsInfo_Production = new Dictionary<string, ColumnType>();
        public  Dictionary<string, ColumnType> ColumnsInfo_Groups = new Dictionary<string, ColumnType>();



		#region c'tor

		public Vyroba(DS_Information ds)
		{

            #region z XML soboru

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_Production = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_Production.Trim());

            foreach (var dr in DT_Production)
            {
                ColumnsInfo_Production.AddIfNotExists(dr);
            }

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_Groups = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_Groups.Trim());

            foreach (var dr in DT_Groups)
            {
                ColumnsInfo_Groups.AddIfNotExists(dr);
            } 
            #endregion
		}

		#endregion

	}
}

