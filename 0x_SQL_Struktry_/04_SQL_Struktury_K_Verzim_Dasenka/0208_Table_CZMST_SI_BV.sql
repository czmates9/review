/****** Object:  Table [dbo].[CZMST_SI_BV]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SI_BV](
	[CountEntries] [int] NOT NULL,
	[USER_ID] [int] NOT NULL,
	[NMBRPAL] [nvarchar](50) NULL,
	[pal_WEIGHT] [numeric](19, 5) NULL,
	[pal_W] [numeric](19, 5) NULL,
	[pal_H] [numeric](19, 5) NULL,
	[pal_D] [numeric](19, 5) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/