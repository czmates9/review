/****** Object:  Table [dbo].[UCSTR]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UCSTR](
	[STREDISKO] [nvarchar](6) NOT NULL,
	[NAZEV] [nvarchar](50) NULL,
	[UCETNI] [nvarchar](3) NULL,
	[STRED2] [nvarchar](3) NULL,
	[CINNOST] [nvarchar](3) NULL,
	[ZAK] [nvarchar](1) NULL,
	[AKTIVNI] [tinyint] NOT NULL,
	[KLIC_STA] [int] NULL,
	[CASZAPSANI] [datetime] NULL,
	[STRUKT] [tinyint] NOT NULL,
	[STRUKTSEZN] [nvarchar](800) NULL,
 CONSTRAINT [PK__UCSTR__JKR] PRIMARY KEY CLUSTERED 
(
	[STREDISKO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/