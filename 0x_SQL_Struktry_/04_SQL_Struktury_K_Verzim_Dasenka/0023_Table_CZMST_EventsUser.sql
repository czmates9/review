/****** Object:  Table [dbo].[CZMST_EventsUser]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_EventsUser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[eguid] [uniqueidentifier] NOT NULL,
	[eid] [nvarchar](10) NOT NULL,
	[etype] [nvarchar](10) NOT NULL,
	[etime] [datetime] NOT NULL,
	[termid] [int] NOT NULL,
	[userid] [int] NOT NULL,
	[loginid] [nvarchar](20) NULL,
	[machineid] [nvarchar](20) NULL,
	[modul] [nvarchar](20) NULL,
	[countentries] [int] NULL,
	[docnmbr] [nvarchar](30) NULL,
	[itemnmbr] [nvarchar](40) NULL,
	[REZ1] [nvarchar](50) NULL,
	[REZ2] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[eguid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/