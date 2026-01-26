/****** Object:  Table [dbo].[CZMST096]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CZMST096](
	[prac_id] [nvarchar](30) NOT NULL,
	[prac_desc] [nvarchar](40) NULL,
	[prac_typ] [nvarchar](3) NULL,
	[prac_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/**************************************************************************************/