/****** Object:  Table [dbo].[CZMST_DI_HISTORY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_DI_HISTORY](
	[CountEntries] [int] NOT NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[STR_ID] [nvarchar](30) NULL,
	[DOC_ID] [nvarchar](12) NULL,
	[DOC_ID2] [nvarchar](12) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[PRAC_ID] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[TAXAMPIE] [numeric](19, 5) NULL,
	[AMOUNPIE] [numeric](19, 5) NULL,
	[WITHTAX] [tinyint] NULL,
	[PRICEX] [tinyint] NULL,
	[mena_ID] [nvarchar](10) NULL,
	[TAXAMPIEM] [numeric](19, 5) NULL,
	[AMOUNPIEM] [numeric](19, 5) NULL,
	[mena_IDM] [nvarchar](10) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[REZ_3] [nvarchar](50) NULL,
	[REZ_4] [nvarchar](50) NULL,
	[USER_ID] [int] NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[LOCNCODEDEST] [nvarchar](11) NULL,
	[SKL_ID_DEST] [nvarchar](20) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[PRINTED] [tinyint] NULL,
	[EXPIRACE] [datetime] NULL,
	[AttributeToSN] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_DI_HISTORY] ADD  DEFAULT ((0)) FOR [PRINTED]
GO
/**************************************************************************************/