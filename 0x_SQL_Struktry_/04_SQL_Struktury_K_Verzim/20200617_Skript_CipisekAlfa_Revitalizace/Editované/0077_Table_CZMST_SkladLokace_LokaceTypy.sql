/****** Object:  Table [dbo].[CZMST_SkladLokace_LokaceTypy]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_SkladLokace_LokaceTypy](
	[TYPE] [nvarchar](2) NULL,
	[Description] [nvarchar](100) NULL,
	[IS_RECEIVE] [bit] NOT NULL,
	[IS_DEFAULT] [bit] NOT NULL,
	[IS_NORMAL] [bit] NOT NULL
) ON [PRIMARY]

GO
/**************************************************************************************/