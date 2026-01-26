/****** Object:  Table [dbo].[CZMST_Expedice_Baleni_Hlavicka_HISTORY]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Expedice_Baleni_Hlavicka_HISTORY](
	[ID] [uniqueidentifier] NOT NULL,
	[Rozpracovano] [tinyint] NOT NULL DEFAULT ((0)),
	[UserID] [int] NULL,
	[TermID] [int] NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateFinished] [datetime] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[DEX_ROW_ID] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/