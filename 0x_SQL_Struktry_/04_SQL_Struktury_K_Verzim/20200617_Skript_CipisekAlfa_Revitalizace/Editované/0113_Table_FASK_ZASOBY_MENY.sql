/****** Object:  Table [dbo].[FASK_ZASOBY_MENY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_ZASOBY_MENY](
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[mena_ID] [nvarchar](10) NOT NULL,
	[PRICE] [numeric](18, 2) NULL,
	[PRICEX] [int] NULL
) ON [PRIMARY]

GO
/**************************************************************************************/