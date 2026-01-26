/****** Object:  Table [dbo].[CZMST_SI_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SI_HISTORY](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ORD] [int] NOT NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[QTYSHPPDMJ] [numeric](19, 5) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL,
	[KOD_SW] [nvarchar](11) NULL,
	[DAT_VYROBY] [nvarchar](11) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[ODBER_ID] [nvarchar](12) NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[USER_ID] [int] NOT NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[PRINTED] [tinyint] NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_SI_HISTORY] ADD  DEFAULT ((0)) FOR [PRINTED]
GO
/**************************************************************************************/