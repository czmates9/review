/****** Object:  Table [dbo].[INVENTUR]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[INVENTUR](
	[ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[KATEGORIE] [nvarchar](2) NULL,
	[I_CISLO] [nvarchar](11) NULL,
	[NAZEV] [nvarchar](50) NULL,
	[STRED] [nvarchar](6) NULL,
	[OSOBA] [int] NULL,
	[LOKACE1] [nvarchar](25) NULL,
	[LOKACE2] [nvarchar](25) NULL,
	[KANCELAR] [nvarchar](6) NULL,
	[EAN] [nvarchar](50) NULL,
	[KUSU] [numeric](12, 0) NULL,
	[KLIC_LOK] [int] NULL,
	[ID_INV] [numeric](20, 0) NULL,
	[OS_ZPR] [nvarchar](20) NULL,
	[ID_TERM] [numeric](20, 0) NULL,
	[CAS_ZPR] [smalldatetime] NULL,
 CONSTRAINT [PK__INVENTUR__JKR] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/