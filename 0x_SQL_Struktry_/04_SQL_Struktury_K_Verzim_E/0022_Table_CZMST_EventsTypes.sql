/****** Object:  Table [dbo].[CZMST_EventsTypes]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_EventsTypes](
	[eid] [nvarchar](10) NOT NULL,
	[etype] [nvarchar](10) NOT NULL,
	[edesc] [nvarchar](100) NULL,
	[ebarcode] [nvarchar](21) NULL
) ON [PRIMARY]

GO
/**************************************************************************************/