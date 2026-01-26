/****** Object:  Table [dbo].[FASK_RADY]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FASK_RADY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Default] [bit] NULL,
	[PlatnostOd] [datetime] NULL,
	[PlatnostDo] [datetime] NULL,
	[Modul] [nvarchar](20) NOT NULL,
	[Modul_ID] [nvarchar](20) NULL,
	[Modul_ID2] [nvarchar](20) NULL,
	[Modul_Funkce] [nvarchar](20) NULL,
	[Rada_ID] [int] NULL,
	[Rada_Nazev] [nvarchar](100) NULL,
	[Rada_Prefix] [nvarchar](10) NULL,
	[Rada_Count] [int] NULL,
	[Filtr_SkladID] [nvarchar](20) NULL,
	[Filtr_UserID] [nvarchar](10) NULL,
	[Vloz_Stredisko0] [nvarchar](20) NULL,
	[Vloz_Stredisko1] [nvarchar](20) NULL,
	[Vloz_Cinnost] [nvarchar](20) NULL,
	[Vloz_Zakazka] [nvarchar](20) NULL,
	[Kontrola_Disponability] [bit] NULL,
	[Vyber_Typ_Prevodka] [tinyint] NULL,
	[Prodej_Prijemka_Tisk_Tiskarna] [nvarchar](50) NULL,
	[Prodej_Vydejka_Tisk_Tiskarna] [nvarchar](50) NULL,
	[Prodej_Prevodka_Tisk_Tiskarna] [nvarchar](50) NULL,
	[Prodej_Prijemka_Tisk_ID_sablona] [int] NULL,
	[Prodej_Vydejka_Tisk_ID_sablona] [int] NULL,
	[Prodej_Prevodka_Tisk_ID_sablona] [int] NULL,
	[Import_Doklad_IS] [bit] NOT NULL DEFAULT(1)
) ON [PRIMARY]

GO
/**************************************************************************************/