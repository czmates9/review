
/****** Object:  Table [dbo].[LOKACE]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LOKACE](
	[LOKACE1] [nvarchar](25) NULL,
	[LOKACE2] [nvarchar](25) NULL,
	[EANL] [nvarchar](20) NULL,
	[NAZEV] [nvarchar](50) NOT NULL,
	[KLIC_LOK] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[KLIC_KAT] [int] NULL,
	[KLIC_K_OLD] [int] NULL,
 CONSTRAINT [PK__LOKACE__JKR] PRIMARY KEY CLUSTERED 
(
	[KLIC_LOK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/