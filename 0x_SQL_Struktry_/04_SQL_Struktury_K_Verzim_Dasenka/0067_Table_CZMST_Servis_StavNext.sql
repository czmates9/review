/****** Object:  Table [dbo].[CZMST_Servis_StavNext]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Servis_StavNext](
	[ID] [nvarchar](20) NOT NULL,
	[IDNext] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_CZMST_Servis_StavNext_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[IDNext] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/