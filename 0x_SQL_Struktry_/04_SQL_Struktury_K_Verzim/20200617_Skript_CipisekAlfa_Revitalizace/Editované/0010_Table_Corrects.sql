/****** Object:  Table [dbo].[Corrects]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Corrects](
	[id] [int] NOT NULL,
	[desc] [nvarchar](100) NOT NULL,
	[TMFrom] [real] NULL,
	[TMTo] [real] NULL,
	[Production] [tinyint] NOT NULL,
	[ProductionType] [tinyint] NULL,
 CONSTRAINT [PK_correct] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Corrects] ADD  DEFAULT ((0)) FOR [TMFrom]
GO
ALTER TABLE [dbo].[Corrects] ADD  DEFAULT ((1)) FOR [Production]
GO
/**************************************************************************************/