SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZ_UKOL_STATE](
	[State] [nvarchar](1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[IsStart] [bit] NOT NULL,
	[IsEnd] [bit] NOT NULL,
	[Color] [nchar](7) NULL,
 CONSTRAINT [PK_CZ_UKOL_STATE] PRIMARY KEY CLUSTERED 
(
	[State] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CZ_UKOL_STATE] ([State], [Description], [IsStart], [IsEnd]) VALUES (N'È', N'Èeká se ...', 0, 0)
INSERT [dbo].[CZ_UKOL_STATE] ([State], [Description], [IsStart], [IsEnd]) VALUES (N'K', N'Dokonèeno', 0, 1)
INSERT [dbo].[CZ_UKOL_STATE] ([State], [Description], [IsStart], [IsEnd]) VALUES (N'N', N'Nový', 1, 0)
INSERT [dbo].[CZ_UKOL_STATE] ([State], [Description], [IsStart], [IsEnd]) VALUES (N'P', N'Pøijato', 0, 0)
