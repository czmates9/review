/****** Object:  Table [dbo].[FASK_Vyroba_PVH]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FASK_Vyroba_PVH](
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[SOPTYPE] [nvarchar](11) NOT NULL DEFAULT (''),
	[SOPDESC] [nvarchar](100) NULL,
	[BarcodeH] [nvarchar](31) NOT NULL,
	[Active] [tinyint] NOT NULL DEFAULT ((1)),
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[TypDok] [nvarchar](20) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/