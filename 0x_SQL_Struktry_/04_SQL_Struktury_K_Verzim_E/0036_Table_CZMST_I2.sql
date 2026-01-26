/****** Object:  Table [dbo].[CZMST_I2]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_I2](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[QTY] [numeric](19, 5) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/