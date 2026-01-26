/****** Object:  Table [dbo].[CZ_UKOL_UZIV]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZ_UKOL_UZIV](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UkolID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[State] [nvarchar](1) NOT NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [int] NULL,
	[Note] [nvarchar](200) NULL,
	[DateNotify] [datetime] NULL,
	[DateFinished] [datetime] NULL,
 CONSTRAINT [PK_CZ_UKOL_UZIV] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CZ_UKOL_UZIV] ADD  DEFAULT (N'N') FOR [State]
GO
/**************************************************************************************/