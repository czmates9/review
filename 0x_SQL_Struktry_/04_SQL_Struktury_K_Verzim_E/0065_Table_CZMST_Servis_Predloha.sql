/****** Object:  Table [dbo].[CZMST_Servis_Predloha]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_Servis_Predloha](
	[CountEntries] [int] NOT NULL,
	[DOCUMENT_NUMBER] [nvarchar](30) NULL,
	[Rozpracovano] [tinyint] NOT NULL,
	[ODB_ID] [nvarchar](12) NULL,
	[OkruhID] [nvarchar](20) NOT NULL,
	[UserID] [int] NULL,
	[Barcode] [nvarchar](50) NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_Servis_Predloha] ADD  DEFAULT ((0)) FOR [Rozpracovano]
GO
ALTER TABLE [dbo].[CZMST_Servis_Predloha] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/**************************************************************************************/