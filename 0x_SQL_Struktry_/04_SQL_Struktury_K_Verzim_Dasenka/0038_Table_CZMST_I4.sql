/****** Object:  Table [dbo].[CZMST_I4]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_I4](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[SKL_ID] [nvarchar](20) NOT NULL,
	[VNDITNUM] [nvarchar](60) NOT NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[QUANTITY] [numeric](19, 5) NOT NULL,
	[QUANTITYMJ] [numeric](19, 5) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[DATEDONE] [nvarchar](8) NULL,
	[TIMEDONE] [nvarchar](6) NULL,
	[USERID] [int] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[O_Checked] [bit] NOT NULL DEFAULT ((0)),
	[INPUT_MODE] [tinyint] NOT NULL,
	[ID_TERMINAL] [int] NOT NULL,
	[ITEMCODE] [nvarchar](70) NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/**************************************************************************************/