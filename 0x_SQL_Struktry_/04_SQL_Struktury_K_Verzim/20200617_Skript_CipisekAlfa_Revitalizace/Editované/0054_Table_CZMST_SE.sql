/****** Object:  Table [dbo].[CZMST_SE]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SE](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMTYPE] [nvarchar](11) NOT NULL DEFAULT (''),
	[ITEMDESC] [nvarchar](100) NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[ORD] [int] NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QTYSHPPD] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NOT NULL,
	[CZ_DatVyr_Track] [tinyint] NOT NULL,
	[CZ_DatVyr_Delka] [smallint] NOT NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_SW_Track] [tinyint] NOT NULL,
	[CZ_SW_Delka] [smallint] NOT NULL,
	[CZ_Doslo] [tinyint] NOT NULL,
	[Note] [nvarchar](100) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[QTYPAL] [numeric](19, 5) NULL,
	[PRIORITY] [tinyint] NOT NULL DEFAULT ((3)),
	[PRINTED] [tinyint] NULL DEFAULT ((0)),
	[USERID] [int] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[CZ_REZ1_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[CZ_REZ2_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[ITEMCODE] [nvarchar](70) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Realization_Start] [datetime] NULL,
	[Realization_Stop] [datetime] NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL DEFAULT((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/