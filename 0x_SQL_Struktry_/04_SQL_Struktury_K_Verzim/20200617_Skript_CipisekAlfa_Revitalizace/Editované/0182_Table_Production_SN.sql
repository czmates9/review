/****** Object:  Table [dbo].[Production_SN]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Production_SN](
	[GUID_Production] [uniqueidentifier] NOT NULL,
	[SERLNMBR] [nvarchar](50) NOT NULL,
	[ITEMNMBR] [nvarchar](40) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[Expirace] [datetime] NULL,
	[REZ_1] [nvarchar](50) NULL,
	[REZ_2] [nvarchar](50) NULL,
	[REZ_3] [nvarchar](50) NULL,
	[REZ_4] [nvarchar](50) NULL,
	[GUID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Production_SN] ADD  DEFAULT ((1)) FOR [QTY]
GO
/**************************************************************************************/