/****** Object:  Table [dbo].[CZMST_METAINFO]  ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_METAINFO](
	[DB_Version] [nvarchar](20) NOT NULL,
	[MST_Version] [nvarchar](20) NOT NULL,
	[IS_Provider] [nvarchar](50) NOT NULL,
	[Updated] [datetime] NULL DEFAULT (getdate())
) ON [PRIMARY]

/****************************************************/