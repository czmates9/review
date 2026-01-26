/****** Object:  Table [dbo].[Production_Sources]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Production_Sources](
	[CountEntries] [int] NULL,
	[SOPNUMBE] [nvarchar](30) NULL,
	[ITEMNAME] [nvarchar](51) NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMTYPE] [nvarchar](11) NULL DEFAULT (''),
	[SKL_ID] [nvarchar](20) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[GUID_Production] [uniqueidentifier] NULL,
	[GUID] [uniqueidentifier] NULL,
	[USER_ID] [nvarchar](10) NULL,
	[TERMINAL_ID] [int] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL DEFAULT ((0)),
	[ISOK] [datetime] NULL,
	[idVS] [nvarchar](10) NULL,
	[dateedit] [datetime] NULL,
UNIQUE NONCLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/