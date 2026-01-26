/****** Object:  Table [dbo].[CZMST_CountEntries]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_CountEntries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TBL] [nvarchar](20) NOT NULL,
	[CountEntries] [int] NULL,
	[BLOCKED] [bit] NOT NULL,
UNIQUE NONCLUSTERED 
(
	[TBL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CZMST_CountEntries] ADD  DEFAULT ((0)) FOR [BLOCKED]
GO
/**************************************************************************************/