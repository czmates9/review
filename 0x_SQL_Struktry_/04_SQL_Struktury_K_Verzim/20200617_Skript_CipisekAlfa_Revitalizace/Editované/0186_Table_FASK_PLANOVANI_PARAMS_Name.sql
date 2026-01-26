/****** Object:  Table [dbo].[FASK_PLANOVANI_PARAMS_Name]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_PLANOVANI_PARAMS_Name](
	[Column_Name] [nvarchar](100) NOT NULL,
	[DESC] [nvarchar](200) NOT NULL,
	[dateedit] [datetime] NOT NULL DEFAULT(getdate())
) ON [PRIMARY]

GO
/**************************************************************************************/