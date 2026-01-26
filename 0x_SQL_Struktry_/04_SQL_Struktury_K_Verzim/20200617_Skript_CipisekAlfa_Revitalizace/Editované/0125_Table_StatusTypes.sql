/****** Object:  Table [dbo].[StatusTypes]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StatusTypes](
	[statusid] [nvarchar](10) NOT NULL,
	[statusdesc] [nvarchar](100) NULL,
 CONSTRAINT [PK_StatusTypes] PRIMARY KEY CLUSTERED 
(
	[statusid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/