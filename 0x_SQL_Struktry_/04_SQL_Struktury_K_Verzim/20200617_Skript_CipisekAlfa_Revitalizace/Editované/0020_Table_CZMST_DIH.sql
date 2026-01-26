/****** Object:  Table [dbo].[CZMST_DIH]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_DIH](
	[Zakazka_ID] [nvarchar](50) NULL,
	[Paleta_ID] [nvarchar](50) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/