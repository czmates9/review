/****** Object:  Table [dbo].[CZMST_SSCC_SEQUENCE]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_SSCC_SEQUENCE](
	[seq_id] [int] NOT NULL,
	[sequence_count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[seq_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/