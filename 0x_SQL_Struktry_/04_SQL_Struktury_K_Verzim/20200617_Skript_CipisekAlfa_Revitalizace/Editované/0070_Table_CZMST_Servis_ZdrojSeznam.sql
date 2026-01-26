/****** Object:  Table [dbo].[CZMST_Servis_ZdrojSeznam]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Servis_ZdrojSeznam](
	[ID] [nvarchar](20) NOT NULL,
	[ZdrojID] [nvarchar](20) NOT NULL,
	[Poradi] [int] NULL,
	[IDStav] [nvarchar](20) NULL,
	[IDCinnost] [nvarchar](20) NULL,
 CONSTRAINT [PK_CZMST_Servis_ZdrojSeznam] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[ZdrojID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/