/****** Object:  Table [dbo].[CZ_UKOL_ServiceMan]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZ_UKOL_ServiceMan](
	[ID] [int] NOT NULL,
	[NAME] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[PHONE] [nvarchar](50) NULL,
	[DESC] [nvarchar](100) NULL,
	[OPERATOR] [nvarchar](50) NULL,
	[OPERATOR_Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_CZ_UKOL_ServiceMan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/**************************************************************************************/