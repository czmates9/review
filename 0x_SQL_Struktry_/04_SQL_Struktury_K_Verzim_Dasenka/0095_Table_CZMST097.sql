/****** Object:  Table [dbo].[CZMST097]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST097](
	[mena_ID] [nvarchar](10) NOT NULL,
	[mena_text] [nvarchar](30) NOT NULL,
	[mena_hlavni] [tinyint] NOT NULL DEFAULT ((0)),
	[mena_kurz] [numeric](19, 5) NULL DEFAULT ((0)),
	[mena_kurzDatum] [date] NULL,
 CONSTRAINT [PK_CZMST097] PRIMARY KEY CLUSTERED 
(
	[mena_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/