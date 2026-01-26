/****** Object:  Table [dbo].[OSOBY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OSOBY](
	[OSOBA_ZODP] [int] NOT NULL,
	[TITUL] [nvarchar](6) NULL,
	[PRIJMENI] [nvarchar](20) NULL,
	[JMENO] [nvarchar](12) NULL,
 CONSTRAINT [PK__OSOBY__JKR] PRIMARY KEY CLUSTERED 
(
	[OSOBA_ZODP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/