/****** Object:  Table [dbo].[FASK_AGENDA]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_AGENDA](
	[AGENDAID] [nvarchar](50) NULL,
	[NAME] [nvarchar](50) NULL,
	[DESCIPTION] [nvarchar](max) NULL,
	[AUTH] [tinyint] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/**************************************************************************************/