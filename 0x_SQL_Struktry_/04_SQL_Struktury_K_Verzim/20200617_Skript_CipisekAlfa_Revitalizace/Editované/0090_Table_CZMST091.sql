/****** Object:  Table [dbo].[CZMST091]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST091](
	[str_id] [nvarchar](30) NOT NULL,
	[str_desc] [nvarchar](40) NULL,
	[str_typ] [nvarchar](3) NULL,
	[str_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[skl_id] [nvarchar](20) NULL,
	[odb_id] [nvarchar](12) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/