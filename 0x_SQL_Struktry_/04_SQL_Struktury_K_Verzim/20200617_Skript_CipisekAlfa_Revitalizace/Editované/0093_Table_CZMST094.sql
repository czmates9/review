/****** Object:  Table [dbo].[CZMST094]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST094](
	[SKL_ID] [nvarchar](20) NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[TYPE] [nvarchar](2) NULL,
	[Description] [nvarchar](100) NULL,
	[Barcode] [nvarchar](50) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/