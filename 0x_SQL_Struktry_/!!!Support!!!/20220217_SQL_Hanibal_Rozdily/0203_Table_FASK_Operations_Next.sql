/****** Object:  Table [dbo].[FASK_Operations_Next]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FASK_Operations_Next](
	[machinetype] [nvarchar](10) NOT NULL,
	[IDO] [nvarchar](10) NOT NULL,
	[IDO_NEXT] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_FASK_Operations_Next] PRIMARY KEY CLUSTERED 
(
	[machinetype] ASC,
	[IDO] ASC,
	[IDO_NEXT] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/**************************************************************************************/