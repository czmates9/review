/****** Object:  Table [dbo].[FASK_StatusTypes]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_StatusTypes](
	[statusid] [nvarchar](10) NOT NULL,
	[statusdesc] [nvarchar](100) NULL,
 CONSTRAINT [PK_FASK_FASK_StatusTypes] PRIMARY KEY CLUSTERED 
(
	[statusid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/