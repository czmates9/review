/****** Object:  Table [dbo].[CZMST_Servis_Cinnost]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_Servis_Cinnost](
	[ID] [nvarchar](20) NOT NULL,
	[Oznaceni] [nvarchar](50) NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[TYPE] [nvarchar](2) NOT NULL,
	[TYPEVALUE] [nvarchar](20) NULL,
	[Mandatory] [tinyint] NOT NULL,
	[RequiredLength] [int] NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CZMST_Servis_Cinnost] ADD  DEFAULT ((1)) FOR [Mandatory]
GO
/**************************************************************************************/