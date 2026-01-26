/****** Object:  Table [dbo].[CZMST_TISKARNA]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_TISKARNA](
	[ID] [int] NOT NULL,
	[NAME] [nvarchar](50) NOT NULL,
	[LOCATION] [nvarchar](50) NULL,
	[IP] [nvarchar](100) NULL,
	[PORT] [nvarchar](10) NULL,
	[COM] [nvarchar](50) NULL,
	[SOUBOR] [nvarchar](max) NULL,
	[TIMEOUT] [int] NULL,
	[BARCODE] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/**************************************************************************************/