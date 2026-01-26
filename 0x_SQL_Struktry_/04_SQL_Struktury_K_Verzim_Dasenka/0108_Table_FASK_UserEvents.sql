/****** Object:  Table [dbo].[FASK_UserEvents]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_UserEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NOT NULL,
	[dateeve] [datetime] NOT NULL,
	[statusid] [nvarchar](10) NOT NULL,
	[faskGUID] [uniqueidentifier] NOT NULL,
	[rez_1] [nvarchar](50) NULL,
	[rez_2] [nvarchar](50) NULL,
 CONSTRAINT [PK_FASK_UserEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FASK_UserEvents_faskGUID] UNIQUE NONCLUSTERED 
(
	[faskGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/