/****** Object:  Table [dbo].[UserEvents]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginid] [nvarchar](20) NOT NULL,
	[machineid] [nvarchar](20) NULL,
	[dateeve] [datetime] NOT NULL,
	[statusid] [nvarchar](10) NOT NULL,
	[UserID] [nvarchar](20) NOT NULL,
	[TermID] [tinyint] NOT NULL,
	[REZ1] [nvarchar](50) NULL,
	[GUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/