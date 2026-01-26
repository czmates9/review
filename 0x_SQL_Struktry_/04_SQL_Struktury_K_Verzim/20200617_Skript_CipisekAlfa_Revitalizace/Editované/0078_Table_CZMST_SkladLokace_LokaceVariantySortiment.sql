/****** Object:  Table [dbo].[CZMST_SkladLokace_LokaceVariantySortiment]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SkladLokace_LokaceVariantySortiment](
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SKL_ID] [nvarchar](20) NOT NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL,
	[TYPE] [nvarchar](2) NOT NULL,
	[UserID] [int] NULL,
	[TermID] [int] NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_SkladLokace_LokaceVariantySortiment] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/**************************************************************************************/