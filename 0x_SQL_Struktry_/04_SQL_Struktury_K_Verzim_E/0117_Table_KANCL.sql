/****** Object:  Table [dbo].[KANCL]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KANCL](
	[KANCL] [nvarchar](6) NOT NULL,
	[STRE] [nvarchar](6) NULL,
	[TEXT] [nvarchar](50) NULL,
	[NAZEV] [nvarchar](50) NULL,
	[EAN] [nvarchar](50) NULL,
 CONSTRAINT [PK__KANCL__JKR] PRIMARY KEY CLUSTERED 
(
	[KANCL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/