/****** Object:  Table [dbo].[CZMST_PE_SN_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_PE_SN_HISTORY](
	[CountEntries] [int] NOT NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/