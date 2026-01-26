/****** Object:  Table [dbo].[CZMST_SE_SN_HISTORY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST_SE_SN_HISTORY](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[ORD] [int] NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[DEX_ROW_ID] [int] NOT NULL,
	[Expirace] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CZMST_SE_SN_HISTORY] ADD  CONSTRAINT [DF__CZMST_SE_SN__QTY__40058253]  DEFAULT ((1)) FOR [QTY]
GO
/**************************************************************************************/