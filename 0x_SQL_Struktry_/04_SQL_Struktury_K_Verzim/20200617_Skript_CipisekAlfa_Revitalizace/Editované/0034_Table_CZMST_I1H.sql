/****** Object:  Table [dbo].[CZMST_I1H]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_I1H](
	[CountEntries] [int] NOT NULL,
	[CE_Orig] [int] NULL,
	[Description] [nvarchar](100) NULL,
	[State] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[CountEntries] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/