/****** Object:  Table [dbo].[CZMST_I3]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_I3](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[CZ_CarKod] [nvarchar](70) NOT NULL,
	[QTYPACK] [numeric](19, 5) NULL,
	[MJ] [nvarchar](10) NOT NULL,
	[VENDORID] [nvarchar](15) NOT NULL,
	[VNDITNUM] [nvarchar](60) NOT NULL,
	[VENDNAME] [nvarchar](31) NOT NULL,
	[WEIGHT] [numeric](19, 5) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/