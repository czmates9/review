
/****** Object:  Table [dbo].[CZMST_DIH]    Script Date: 19.05.2025 9:52:57 ******/
/**

Rozsirujici prvky:
ISOK - datum a cas prevzeti do IS
status - stav davky z pohledu IS
  null - stav nespravovan 
  1 - novy
  2 - 
  3 -
  4 -
  5 - spracovano


**/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CZMST_DIH](
	[Zakazka_ID] [nvarchar](50) NULL,
	[Paleta_ID] [nvarchar](50) NULL,
	[mena_ID] [nvarchar](10) NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[CountEntries] [int] NOT NULL,
	[ISOK] [datetime] NULL,
	[status] [int] NULL
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[CZMST_SEH](
	[CountEntries] [int] NOT NULL,
	[SOPNUMBE] [nvarchar](30) NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[ISOK] [datetime] NULL,
	[status] [int] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CZMST_SIH](
	[CountEntries] [int] NOT NULL,
	[TISKARNA_NAME] [nvarchar](30) NULL,
	[PRAC_ID] [nvarchar](30) NULL,
	[ISOK] [datetime] NULL,
	[status] [int] NULL
) ON [PRIMARY]
GO


