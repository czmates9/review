/****** Object:  Table [dbo].[FASK_ZASOBY_STAV]  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FASK_ZASOBY_STAV](
	[KOD] [nvarchar](12) NOT NULL DEFAULT (''),
	[NAZEV] [nvarchar](50) NOT NULL DEFAULT (''),
	[KOD_LOK] [nvarchar](3) NOT NULL DEFAULT (''),
	[STAV] [decimal](12, 3) NOT NULL DEFAULT ((0)),
	[CENA] [decimal](15, 3) NOT NULL DEFAULT ((0)),
	[CENA_ZUST] [decimal](10, 3) NOT NULL DEFAULT ((0)),
	[REZERVACE] [decimal](12, 3) NOT NULL DEFAULT ((0)),
	[TS] [datetime2](7) NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_FASK_ZASOBY_STAV] PRIMARY KEY CLUSTERED 
(
	[KOD] ASC,
	[KOD_LOK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/**************************************************************************************/