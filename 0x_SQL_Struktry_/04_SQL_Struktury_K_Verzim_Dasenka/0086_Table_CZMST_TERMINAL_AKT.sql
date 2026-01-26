/****** Object:  Table [dbo].[CZMST_TERMINAL_AKT]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST_TERMINAL_AKT](
	[ID_TERMINAL] [int] NOT NULL,
	[IP] [nvarchar](100) NOT NULL,
	[DATEREQ] [datetime] NOT NULL
) ON [PRIMARY]

GO
/**************************************************************************************/