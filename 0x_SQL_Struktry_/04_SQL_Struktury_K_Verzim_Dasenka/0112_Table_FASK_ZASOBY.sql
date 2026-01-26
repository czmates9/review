/****** Object:  Table [dbo].[FASK_ZASOBY]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_ZASOBY](
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[VNDITNUM] [nvarchar](60) NULL,
	[CZ_CarKod] [nvarchar](70) NULL,
	[LOCNCODE] [nvarchar](11) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[DMJ] [nvarchar](200) NOT NULL CONSTRAINT [DF__FASK_ZASOBY__DMJ__20CCCE1C]  DEFAULT (''),
	[TAXRATE] [numeric](4, 2) NULL,
	[PRICE0] [numeric](18, 2) NULL,
	[PRICE1] [numeric](18, 2) NULL,
	[PRICE2] [numeric](18, 2) NULL,
	[PRICE3] [numeric](18, 2) NULL,
	[PRICE4] [numeric](18, 2) NULL,
	[PRICE5] [numeric](18, 2) NULL,
	[CZ_SerNum_Track] [tinyint] NOT NULL,
	[CZ_SerNum_Delka] [smallint] NOT NULL,
	[CZ_Rez1_Track] [tinyint] NOT NULL CONSTRAINT [DF__FASK_ZASO__CZ_Re__21C0F255]  DEFAULT ((0)),
	[CZ_Rez2_Track] [tinyint] NOT NULL CONSTRAINT [DF__FASK_ZASO__CZ_Re__22B5168E]  DEFAULT ((0)),
	[CZ_Rez3_Track] [tinyint] NOT NULL CONSTRAINT [DF__FASK_ZASO__CZ_Re__23A93AC7]  DEFAULT ((0)),
	[CZ_Rez4_Track] [tinyint] NOT NULL CONSTRAINT [DF__FASK_ZASO__CZ_Re__249D5F00]  DEFAULT ((0)),
	[REZ1] [nvarchar](50) NULL,
	[REZ2] [nvarchar](50) NULL,
	[REZ3] [nvarchar](50) NULL,
	[REZ4] [nvarchar](50) NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[SERLTNUM] [nvarchar](50) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[TIMEFROM] [datetime] NULL,
	[TIMETO] [datetime] NULL,
	[LSTMod] [datetime] NULL,
	[loginid] [nvarchar](20) NULL,
	[CZ_Expirace_Track] [tinyint] NOT NULL Default(0),
	[EXPIRACE] [datetime] NULL
) ON [PRIMARY]

GO
/**************************************************************************************/