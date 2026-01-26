/****** Object:  Table [dbo].[Production]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Production](
	[CountEntries] [int] NULL,
	[SOPNUMBE] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMTYPE] [nvarchar](11) NULL DEFAULT (''),
	[ITEMMJ] [nvarchar](5) NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ORD] [int] NULL,
	[TIMEMODE] [int] NULL DEFAULT ((0)),
	[TIMEPREPSTART] [datetime] NULL,
	[TIMEPREPSTOP] [datetime] NULL,
	[TIMEPREP] [real] NULL,
	[TIMEUNIT] [real] NULL,
	[TIMESTART] [datetime] NULL,
	[TIMESTOP] [datetime] NULL,
	[TIMECORSTART] [datetime] NULL,
	[TIMECORSTOP] [datetime] NULL,
	[TIMECOR] [real] NULL,
	[TIMECRID] [int] NULL,
	[TIMECRIDTYPE] [tinyint] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NULL,
	[operationid] [nvarchar](16) NULL,
	[dateeve] [datetime] NOT NULL,
	[qty] [numeric](19, 5) NOT NULL,
	[qtyReal] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[QTYPACKMJ] [nvarchar](5) NULL,
	[description] [nvarchar](max) NULL,
	[BarcodeP] [nvarchar](31) NULL,
	[UserID] [nvarchar](20) NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[ISOK] [datetime] NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[SOUBEHGUID] [uniqueidentifier] NULL,
	[CORRGUID] [uniqueidentifier] NULL,
	[qtyOld] [numeric](19, 5) NULL,
	[idVS] [nvarchar](10) NULL,
	[dateedit] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SERLTNUM] [nvarchar](50) NULL,
	[EXPIRATION] [nvarchar](50) NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/