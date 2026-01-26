/****** Object:  Table [dbo].[CZMST_PE]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PE](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ORD] [int] NOT NULL,
	[VNDDOCNM] [nvarchar](21) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
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
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[TYPEPAL] [nvarchar](10) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[SERLTNUM] [nvarchar](50) NOT NULL DEFAULT (N''),
	[CZ_REZ1_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[CZ_REZ2_Track] [tinyint] NOT NULL DEFAULT ((0)),
	[Realization_Start] [datetime] NULL,
	[Realization_Stop] [datetime] NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL DEFAULT((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/