/****** Object:  Table [dbo].[CZMST093]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST093](
	[skl_id] [nvarchar](20) NOT NULL,
	[skl_desc] [nvarchar](40) NULL,
	[skl_typ] [nvarchar](3) NULL DEFAULT (''),
	[skl_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/