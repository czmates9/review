/****** Object:  Table [dbo].[CZMST_PEH]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PEH](
	[CountEntries] [int] NOT NULL,
	[PONUMBER] [nvarchar](30) NOT NULL,
	[DOKLTYPE] [nvarchar](10) NOT NULL,
	[GUID] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_PEH] ADD  DEFAULT ('') FOR [DOKLTYPE]
GO
/**************************************************************************************/