/****** Object:  Table [dbo].[CZ_UKOL_STATE  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZ_UKOL_STATE](
	[State] [nvarchar](1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[IsStart] [bit] NOT NULL,
	[IsEnd] [bit] NOT NULL,
	[Color] [nvarchar](7) NULL,
 CONSTRAINT [PK_CZ_UKOL_STATE] PRIMARY KEY CLUSTERED 
(
	[State] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/