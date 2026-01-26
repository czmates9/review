/****** Object:  Table [dbo].[CZMST_PI_F]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PI_F](
	[IMG_NAME] [nvarchar](30) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/