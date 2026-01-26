/****** Object:  Table [dbo].[CZMST_SSCC_PARAMETERS]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_SSCC_PARAMETERS](
	[ID_SSCC] [int] NOT NULL,
	[DESC_SSCC] [nvarchar](20) NULL,
	[LV] [numeric](1, 0) NULL,
	[GCP] [numeric](9, 0) NULL,
	[GCP_count] [numeric](9, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_SSCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/