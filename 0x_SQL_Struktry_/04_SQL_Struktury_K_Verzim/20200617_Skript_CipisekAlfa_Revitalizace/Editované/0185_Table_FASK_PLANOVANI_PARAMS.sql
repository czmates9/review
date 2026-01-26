/****** Object:  Table [dbo].[FASK_PLANOVANI_PARAMS]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_PLANOVANI_PARAMS](
	[GUID_PLANOVANI] [uniqueidentifier] NOT NULL,
	[ColumnName] [nvarchar](100) NOT NULL,
	[Value] [BIT] NOT NULL DEFAULT(0)
) ON [PRIMARY]

GO
/**************************************************************************************/