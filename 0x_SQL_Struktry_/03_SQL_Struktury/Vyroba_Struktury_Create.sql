
-- /////////////////////////////////////////////////////////////////////////////////////////////////////
-- /////////////////////////////////////////////////////////////////////////////////////////////////////
-- ///////////////////////////////////////INICIALIZAZACE//////////////////////////////////////////////
/** Pokud tabulky existuji, pak je smaze a provede nove zalozeni, data budou smazana **/
-- /////////////////////////////////////////////////////////////////////////////////////////////////////
-- /////////////////////////////////////////////////////////////////////////////////////////////////////

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[CZPRO_VPH_View]'))
DROP VIEW [dbo].[CZPRO_VPH_View]
GO
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[CZPRO_VPP_View]'))
DROP VIEW [dbo].[CZPRO_VPP_View]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fask_CZPRO_LastUserAction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[fask_CZPRO_LastUserAction]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Corrects]') AND type in (N'U'))
DROP TABLE [dbo].[Corrects]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StatusTypes]') AND type in (N'U'))
DROP TABLE [dbo].[StatusTypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Operations]') AND type in (N'U'))
DROP TABLE [dbo].[Operations]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Machines]') AND type in (N'U'))
DROP TABLE [dbo].[Machines]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VMachinesOperations]') AND type in (N'U'))
DROP TABLE [dbo].[VMachinesOperations]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Logins]') AND type in (N'U'))
DROP TABLE [dbo].[Logins]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserEvents]') AND type in (N'U'))
DROP TABLE [dbo].[UserEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Production]') AND type in (N'U'))
DROP TABLE [dbo].[Production]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Production_Sources]') AND type in (N'U'))
DROP TABLE [dbo].[Production_Sources]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CZPRO_VPP]') AND type in (N'U'))
DROP TABLE [dbo].[CZPRO_VPP]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CZPRO_VPH]') AND type in (N'U'))
DROP TABLE [dbo].[CZPRO_VPH]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FASK_CONS_095]') AND type in (N'U'))
DROP TABLE [dbo].[FASK_CONS_095]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VLoginsGroups]') AND type in (N'U'))
DROP TABLE [dbo].[VLoginsGroups]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Groups]') AND type in (N'U'))
DROP TABLE [dbo].[Groups]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FASK_Vyroba_TP]') AND type in (N'U'))
DROP TABLE [dbo].[FASK_Vyroba_TP]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CZ_UKOL_ServiceMan]') AND type in (N'U'))
DROP TABLE [dbo].[CZ_UKOL_ServiceMan]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FASK_Vyroba_PVH]') AND type in (N'U'))
DROP TABLE [dbo].[FASK_Vyroba_PVH]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FASK_Vyroba_PVP]') AND type in (N'U'))
DROP TABLE [dbo].[FASK_Vyroba_PVP]
GO

-- /////////////////////////////////////////////////////////////////////////////////////////////////////
-- /////////////////////////////////////////////////////////////////////////////////////////////////////
-- ///////////////////////////////////////TABULKY//////////////////////////////////////////////
-- /////////////////////////////////////////////////////////////////////////////////////////////////////
-- /////////////////////////////////////////////////////////////////////////////////////////////////////


/****** Object:  Table [dbo].[CZPRO_VPH] ******/
CREATE TABLE [dbo].[CZPRO_VPH] (
	[CountEntries] [int] NOT NULL DEFAULT ('1'), --èíslo dávky, 
	[SOPNUMBE] [varchar] (17) NOT NULL, --èíslo výr.zakázky,
	[SOPTYPE] [varchar] (11) NOT NULL DEFAULT (''), --typ zakázky,
	[SOPDESC] [varchar] (51) NULL, -- popis zakázky,
	[VNDDOCNMH] [varchar] (21) NULL, -- èíslo objednatele,
	[BarcodeH] [varchar] (31) NOT NULL, -- èár. kód zakázky,
	[LOCNCODE] [varchar] (11) NULL, --lokace,
	[DateProd] [smallint] NOT NULL, --oèek. Datum výroby,
	[Rez1] [varchar](11) NOT NULL, --rezerva 1,
	[Rez2] [varchar](11) NOT NULL, --rezerva 2,
	[TermID] [tinyint] NOT NULL, --ID term. – posl. naètení dat,
	[LSTMod] [datetime] NOT NULL, --èas.razítko posl. modifi.,
	[DEX_ROW_ID] [int] IDENTITY (1, 1) NOT NULL , -- Poradi zaznamu
	[Active] [tinyint] NOT NULL Default(1), --priznak zda je prikaz aktivni/neaktivni (1-aktivni a prenasi se do terminalu, 0-neaktivni a neprenasi se do terminalu)
CONSTRAINT [PK_CZPRO_VPH] PRIMARY KEY
(
 [SOPNUMBE] ASC, CountEntries
)
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[CZPRO_VPP] ******/
CREATE TABLE [dbo].[CZPRO_VPP] (
	[CountEntries] [int] NOT NULL DEFAULT ('1'), -- èíslo dávky,
	[SOPNUMBE] [varchar] (17) NOT NULL, -- èíslo výr.zakázky,
	[ITEMNMBR] [varchar] (31) NOT NULL, -- ID položky,
	[ITEMTYPE] [varchar] (11) NOT NULL DEFAULT (''),-- typ položky,
	[ITEMDESC] [varchar] (51) NULL, -- popis položky,
	[ITEMMJ] [varchar] (5) NULL, -- mìrná jednotka položky
	[VNDDOCNMP] [varchar] (21) NULL, -- èíslo dok. externí,
	[VNDITNUM] [varchar] (31) NULL, --èíslo položky externí,
	[ORD] [int] NOT NULL DEFAULT (0), --poøadí položky (operace),
	[BarcodeP] [varchar] (31) NOT NULL, --èárový kód položky,
	[LOCNCODE] [varchar] (11) NULL, -- lokace,
	[QTYSHPPD] [numeric](19, 5) NOT NULL, -- množství,
	[QTYDOKON] [numeric](19, 5) NOT NULL DEFAULT (0), -- množství dokoncene rucnim odvodem,
	[QTYPACK] [numeric](19, 5) NOT NULL, -- množství v balení(pøepoètový koeficent)
	[QTYPACKMJ] [varchar] (5) NULL, --mìrná jednotka balení,
	[TIMEMODE] [int] NOT NULL Default(0), --typ sledovani casu, 0=stop vyroby, 1=start/stop vyroby, 2=start/stop priprava, start/stop vyroba
	[TIMEPREP] [real] NOT NULL, -- pøípravný èas v minutach,
	[TIMEUNIT] [real] NOT NULL, -- jednotkový èas v minutach,
	[DtProdT] [tinyint] NOT NULL, -- sledovat dat.vyr.,
	[DtProdL] [smallint] NOT NULL, -- rozsah p. dat.výr.,
	[SerNumT] [tinyint] NOT NULL, -- sledovat SN,
	[SerNumL] [smallint] NOT NULL, -- rozsah p. SN,
	[VerT] [tinyint] NOT NULL, -- sledovat verzi,
	[VerL] [smallint] NOT NULL, -- rozsah p. verze,
	[TermID] [tinyint] NOT NULL, -- pøíznak pøevzetí,
	[LSTMod] [datetime] NOT NULL, -- èas.razítko posl. modifi.,
	[DEX_ROW_ID] [int] IDENTITY (1, 1) NOT NULL, -- poradi zaznamu
CONSTRAINT [PK_CZPRO_VPP] PRIMARY KEY
(
 [SOPNUMBE] ASC,
 [ITEMNMBR] ASC,
 [CountEntries]
)
) ON [PRIMARY]
GO
 
--Vystupni data
/****** Object:  Table [dbo].[Production] ******/
CREATE TABLE [dbo].[Production](
	[CountEntries] [int] NULL, -- èíslo dávky,
	[SOPNUMBE] [varchar](17) NULL, -- zakázka,
	[ITEMNMBR] [varchar] (31) NULL, -- ID položky,
	[ITEMTYPE] [varchar] (11) NULL DEFAULT (''),-- typ položky,
	[ITEMMJ] [varchar] (5) NULL, -- mìrná jednotka položky
	[ITEMDESC] [varchar] (51) NULL, -- nazev položky,
	[ORD] [int] NULL, --poøadí položky (operace),
	[TIMEMODE] [int] NULL Default(0), --typ sledovani casu, 0=stop vyroby, 1=start/stop vyroby, 2=start/stop priprava, start/stop vyroba
	[TIMEPREPSTART] [datetime] NULL, --start cas pripravy
	[TIMEPREPSTOP] [datetime] NULL, --stop cas pripravy
	[TIMEPREP] [real] NULL, -- pøípravný èas v minutach,
	[TIMEUNIT] [real] NULL, -- jednotkový èas v minutach,	
	[TIMESTART] [datetime] NULL, -- start èas zahajeni operace,
	[TIMESTOP] [datetime] NULL, -- stop èas ukonceni operace,
	[TIMECORSTART] [datetime] NULL, --start cas korekce
	[TIMECORSTOP] [datetime] NULL, --stop cas korekce
	[TIMECOR] [real] NULL, -- korekce èasu v minutach,
	[TIMECRID] [int] NULL, -- ID korekce èasu,
	[TIMECRIDTYPE] [tinyint] NULL, -- Typ korekce èasu ([NULL,0]-zpoždìní, [1]-úspora, ...),
	[id] [int] IDENTITY(1,1) NOT NULL, -- ID události,
	[loginid] [varchar](10) NOT NULL, -- id uživatele (smeny). uzivatel, ktery prihlasil smenu,
	[machineid] [varchar](16) NULL, -- id stroje,
	[operationid] [varchar](16) NULL, -- id operace,
	[dateeve] [datetime] NOT NULL, -- datum a èas,
	[qty] [numeric](19, 5) NOT NULL, -- poèet kusù zadaný uživ.,
	[qtyReal] [numeric](19, 5) NOT NULL, -- poèet kusù sejmuto,
	[QTYPACK] [numeric](19, 5) NULL, -- množství v balení(pøepoètový koeficent)
	[QTYPACKMJ] [varchar] (5) NULL, --mìrná jednotka balení,
	[description] [ntext] NULL, -- popis,
	[BarcodeP] [varchar] (31) NULL, -- èárový kód položky,
	[UserID]  [varchar] (10) NOT NULL, -- ID prac. který provedl operaci,
	[TermID] [tinyint] NOT NULL, --ID term, ktery zaznamenal provedeni operace,
	[ISOK]   [datetime] NULL, -- èas. pøevzetí do IS,
	[GUID] [uniqueidentifier] NOT NULL, --jedinecna idetifikace zaznamu, slouzi pro update do db, v pripade vicenasobneho prenosu
	[SOUBEHGUID] [uniqueidentifier] NULL, --jedinecna idetifikace, slouzi pro identifikaci zakazek (zakazky v soubehu, pripadne spojeni start/stop)
	[CORRGUID] [uniqueidentifier] NULL, --jedinecna idetifikace, slouzi pro identifikaci korekci (spojeni start/stop)
	[qtyOld] [numeric](19, 5) NULL, -- poèet kusù zadaný uživ. (pokud vedoucí smìny provedl úpravu množství),
	[idVS] [varchar](10) NULL, -- ID vedoucího smìny, který upravil množství
	[dateedit] [datetime] NULL, -- datum a èas editace množství vedoucím smìny
	[SKL_ID] [nvarchar](20) NULL, -- cislo skladu polozky 	
	[LOCNCODE][nvarchar](11)NULL, -- Lokace
	[SERLTNUM] [nvarchar](50) NULL, -- STEINEX, seriove èislo/šarže
	[EXPIRATION] [nvarchar](50) NULL, -- expirace, datum trvanlivosti
CONSTRAINT [PK_Production] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

--Vystupni data (Rozpad materialu vyrobku - opreno o ciselnik zbozi czmst095 z MTZ)
/****** Object:  Table [dbo].[Production_Sources] ******/
CREATE TABLE [dbo].[Production_Sources](
	[CountEntries] [int] NULL, -- èíslo dávky,
	[SOPNUMBE] [nvarchar](17) NULL, -- zakázka,
	[ITEMNAME] [nvarchar](51) NULL, 
	[ITEMNMBR] [nvarchar] (31) NULL, -- ID položky,
	[ITEMTYPE] [nvarchar] (11) NULL DEFAULT (''),-- typ položky,
	[SKL_ID] [nvarchar](20) NULL, -- cislo skladu polozky 	
	[ITEMCODE] [nvarchar](50) NULL,
	[LOCNCODE] [nvarchar] (11) NULL , -- lokace skladu polozky
	[MJ] [nvarchar] (5) NOT NULL ,
	[QTYSHPPD] [numeric](19, 5) NOT NULL ,
	[QTYSHPPDMJ] [numeric](19, 5) NOT NULL ,
	[QTYPACK] [numeric](19, 5) NULL ,
	[SERLTNUM] [char] (21) NOT NULL ,
	[GUID_Production] [uniqueidentifier] NULL,
	[GUID] [uniqueidentifier] NULL UNIQUE,
	[USER_ID] [varchar] (10) NULL ,
	[TERMINAL_ID] [int] NOT NULL,
	[DEX_ROW_ID] [int] IDENTITY (1, 1) NOT NULL,  --Identifikator
	[WEIGHT] [numeric](19, 5) NULL,               --váha
	[NMBRPAL] [nvarchar] (50) NULL,		      -- ID palety, na ktere je polozka umistena
	[TYPEPAL] [nvarchar] (10) NULL,		      -- typ palety
	[PRINTED] tinyint NULL Default(0),	      -- priznak tisku (zatim se nevyuziva)		
	[ISOK]   [datetime] NULL, 		      -- èas. pøevzetí do IS
	[idVS]   [varchar] (10) NULL, 		      --ID vedouciho smeny
	[dateedit] [datetime] NULL		      -- datum a cas editace
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UserEvents] ******/
CREATE TABLE [dbo].[UserEvents](
	[id] [int] IDENTITY(1,1) NOT NULL, -- ID uživatele
	[loginid] [varchar](10) NOT NULL, -- id smeny (uzivatel, ktery prihlasil smenu)
	[machineid] [varchar](16) NULL, -- machineid
	[dateeve] [datetime] NOT NULL, -- datum a èas události
	[statusid] [varchar](10) NOT NULL, -- status id
	[UserID]  [varchar] (10) NOT NULL, -- ID pracovnika, ke kteremu se udalost vaze,
	[TermID] [tinyint] NOT NULL, --ID term, ktery zaznamenal vlozeni udalosti,
	[REZ1] [varchar] (20) NULL, --Dopòující údaj události obsluhy (napø. è.výrobního pøíkazu)
	[GUID] [uniqueidentifier] not null, --jedinecna idetifikace zaznamu, slouzi pro update do db, v pripade vicenasobneho prenosu
 CONSTRAINT [PK_UserEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]

--Èíselníky:
/****** Object:  Table [dbo].[Logins] ******/
CREATE TABLE [dbo].[Logins](
	[id] [varchar](10) NOT NULL, -- ID osoby (ÈK)
	[firstname] [varchar](20) NOT NULL, -- køestní jméno
	[surname] [varchar](50) NOT NULL, -- pøíjmení
	[psswd] [varchar](10) NOT NULL, -- heslo
	[VS] [tinyint] NOT NULL DEFAULT (0) -- vedouci smeny
 CONSTRAINT [PK_Logins] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]

/****** Object:  Table [dbo].[VLoginsGroups] ******/
CREATE TABLE [dbo].[VLoginsGroups](
	[loginid] [varchar](10) NOT NULL, -- ID uzivatele
	[groupid] [varchar](10) NOT NULL -- ID skupiny
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Groups] ******/
CREATE TABLE [dbo].[Groups](
	[id] [varchar](10) NOT NULL, -- ID skupiny
	[name] [varchar](20) NOT NULL, -- jméno skupiny
	[description] [varchar](50) NOT NULL, -- popis skupiny
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]
GO
GO
/****** Object:  Table [dbo].[Machines] ******/
CREATE TABLE [dbo].[Machines](
	[id] [varchar](16) NOT NULL, -- ID stroje
	[name] [varchar](20) NOT NULL, -- jméno stroje
	[description] [varchar](50) NOT NULL, -- popis
 CONSTRAINT [PK_Machines] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Operations] ******/
CREATE TABLE [dbo].[Operations](
	[id] [varchar](16) NOT NULL, -- ID operace
	[name] [varchar](20) NOT NULL, -- jméno operace
	[description] [varchar](50) NOT NULL, -- popis operace
 CONSTRAINT [PK_Operations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VMachinesOperations] ******/
CREATE TABLE [dbo].[VMachinesOperations](
	[machineid] [varchar](16) NOT NULL, -- ID stroje
	[operationid] [varchar](16) NOT NULL -- ID operace
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[StatusTypes] ******/
CREATE TABLE [dbo].[StatusTypes](
	[statusid] [varchar](10) NOT NULL, -- ID
	[statusdesc] [varchar](100) NULL, -- popis
	--[statustype] [varchar] (1) NULL DEFAULT (''), -- type statusu udalosti ()
 CONSTRAINT [PK_StatusTypes] PRIMARY KEY CLUSTERED 
(
	[statusid] ASC
)
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Corrects] ******/
CREATE TABLE [dbo].[Corrects](
	[id] [int] NOT NULL, -- ID korekce
	[desc] [varchar](10) NOT NULL, -- popis korekce
	[TMFrom] [real] NULL DEFAULT (0), -- minimalni doba trvani korekce operace(je-li NULL, pak min=0),
	[TMTo] [real] NULL, -- maximalni doba trvani korekce operace(je-li NULL, pak neni omezeno),
	[Production] [tinyint] NOT NULL Default(1), --priznak, zda jde o korekci vazanou k vyrobe (0=nesouvisejici, 1=souvisecjici se zakazkou)
	[ProductionType] [tinyint] NULL, --priznak, zda jde o usporu casu (0-zpozdeni, 1-uspora)
CONSTRAINT [PK_correct] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[FASK_CONS_095]    Script Date: 7.11.2007 16:42:44 ******/
CREATE TABLE [dbo].[FASK_CONS_095] (
	[ITEMNMBR] [char] (31) NOT NULL ,
	[ITEMDESC] [char] (51) NULL ,
	[VNDITNUM] [char] (31) NULL ,
	[CZ_CarKod] [char] (31) NULL ,
	[LOCNCODE] [char] (11) NULL ,
	[SKL_ID] [char] (20) NULL ,
	[QTY] [numeric](19, 5) NOT NULL ,
	[QTYPACK] [numeric](19, 5) NULL ,
	[MJ] [varchar] (5) NOT NULL ,
	[DMJ] [varchar] (200) NULL ,
	[TAXRATE] [numeric](4, 2) NULL ,
	[PRICE0] [numeric](18, 2) NULL ,
	[PRICE1] [numeric](18, 2) NULL ,
	[PRICE2] [numeric](18, 2) NULL ,
	[PRICE3] [numeric](18, 2) NULL ,
	[PRICE4] [numeric](18, 2) NULL ,
	[PRICE5] [numeric](18, 2) NULL ,
	[CZ_SerNum_Track] [tinyint] NOT NULL ,
	[CZ_SerNum_Delka] [smallint] NOT NULL ,
	[CZ_Rez1_Track] [tinyint] NOT NULL Default 0,
	[CZ_Rez2_Track] [tinyint] NOT NULL Default 0,
	[CZ_Rez3_Track] [tinyint] NOT NULL Default 0,
	[CZ_Rez4_Track] [tinyint] NOT NULL Default 0,
	[REZ1] [varchar] (10) NULL,
	[DEX_ROW_ID] [int] IDENTITY (1, 1) NOT NULL,
	[ITEMCODE] [varchar](50) NULL,
	[ODB_ID] [varchar](12) NULL,
	[TIMEMODE] [int] NOT NULL Default(0), --typ sledovani casu, 0=stop vyroby, 1=start/stop vyroby, 2=start/stop priprava, start/stop vyroba
	[TIMEPREP] [real] NOT NULL, -- pøípravný èas v minutach
	[TIMEUNIT] [real] NOT NULL, -- jednotkový èas v minutach,
	[TIMEFROM] [datetime] NULL, --start cas pripravy (platnost od)
	[TIMETO] [datetime] NULL, --stop cas pripravy (platnost do)
	[LSTMod] [datetime] NOT NULL, -- èas.razítko posl. modifi., (kdy bylo poslednì editováno )
	[loginid] [varchar](10) NOT NULL -- id uživatele (smeny). uzivatel, ktery provedl modifikaci (kdo poslednì editoval)
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[FASK_Vyroba_TP]    Script Date: 11/29/2017 17:20:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FASK_Vyroba_TP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_H] [nvarchar](50) NULL,
	[ID_L] [nvarchar](50) NULL,
	[ITEMNMBR_Def] [nvarchar](31) NULL,
	[DESC_Def] [nvarchar](51) NULL,
	[MJ_Def] [nvarchar](50) NULL,
	[ITEMNMBR_fol] [nvarchar](31) NULL,
	[DESC_Fol] [nvarchar](51) NULL,
	[MJ_Fol] [nvarchar](50) NULL,
	[koef] [nvarchar](50) NULL,
	[ID_USER] [nvarchar](50) NULL,
	[dateedit] [datetime] NULL,
	[alter] [nvarchar](1) NULL,
	[PUO] [nvarchar](1) NULL,
	CONSTRAINT [PK_FASK_Vyroba_TP] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY] 
GO


/****** Object:  Table [dbo].[FASK_Vyroba_PVH]    Script Date: 11.06.2019 13:10:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[FASK_Vyroba_PVH](
	[SOPNUMBE] [varchar](17) NOT NULL,
	[SOPTYPE] [varchar](11) NOT NULL DEFAULT (''),
	[SOPDESC] [varchar](51) NULL,
	[BarcodeH] [varchar](31) NOT NULL,
	[Active] [tinyint] NOT NULL DEFAULT ((1)),
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/****** Object:  Table [dbo].[FASK_Vyroba_PVP]    Script Date: 11.06.2019 13:10:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FASK_Vyroba_PVP](
	[OBJ_NMBR] [nvarchar](17) NULL,
	[OBJ_DESC] [nvarchar](100) NULL,
	[OBJ_TYPE] [nvarchar](11) NULL,
	[OBJ_COMPANY] [nvarchar](255) NULL,
	[OBJ_DATE_FROM] [datetime] NULL,
	[OBJ_DATE_TO] [datetime] NULL,
	[OBJ_ORD] [int] NULL,
	[OBJ_ITEM_ORD] [int] NULL,
	[ITEMNMBR] [nvarchar](40) NOT NULL,
	[ITEMDESC] [nvarchar](100) NULL,
	[ITEMCODE] [nvarchar](64) NULL,
	[QTY] [numeric](19, 5) NOT NULL,
	[DATE_ZAPLANOVANI] [datetime] NULL,
	[VP_PRPS] [bit] NOT NULL DEFAULT ((0)),
	[VP_PRPS_QTY] [numeric](19, 5) NULL,
	[VP_PRDCT_QTY] [numeric](19, 5) NULL,
	[USERID] [int] NULL,
	[Ref_PVH] [int] NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO





--Struktura pro Servisny zasah Labara
/****** Object:  Table [dbo].[CZ_UKOL_ServiceMan]    Script Date: 06/26/2018 16:56:01 ******/
CREATE TABLE [dbo].[CZ_UKOL_ServiceMan](
	[ID] [int] NOT NULL,
	[NAME] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[PHONE] [nvarchar](50) NULL,
	[DESC] [nvarchar](50) NULL,
	[OPERATOR] [nvarchar](50) NULL,
	[OPERATOR_Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_CZ_UKOL_ServiceMan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


-- /////////////////////////////////////////////////////////////////////////////////////////////////////
-- /////////////////////////////////////////////////////////////////////////////////////////////////////
-- ///////////////////////////////////////VIEW//////////////////////////////////////////////////////////
-- /////////////////////////////////////////////////////////////////////////////////////////////////////
-- /////////////////////////////////////////////////////////////////////////////////////////////////////


/****** Object:  View [dbo].[CZPRO_VPH_View]    Script Date: 06/24/2009 09:38:05 ******/
CREATE VIEW [dbo].[CZPRO_VPH_View]
AS
SELECT
	   vph.[CountEntries]
      ,vph.[SOPNUMBE]
      ,vph.[SOPTYPE]
      ,vph.[SOPDESC]
      ,vph.[VNDDOCNMH]
      ,vph.[BarcodeH]
      ,vph.[LOCNCODE]
      ,vph.[DateProd]
      ,vph.[Rez1]
      ,vph.[Rez2]
      ,vph.[TermID]
      ,vph.[LSTMod]
      ,vph.[DEX_ROW_ID]
FROM         
	CZPRO_VPH vph
WHERE ISNULL(vph.Active,1)=1

GO

/****** Object:  View [dbo].[CZPRO_VPP_View]    Script Date: 06/24/2009 09:38:05 ******/
CREATE VIEW [dbo].[CZPRO_VPP_View]
AS
SELECT    
	vpp.CountEntries, 
	vpp.SOPNUMBE, 
	vpp.ITEMNMBR, 
	vpp.ITEMTYPE, 
	vpp.ITEMDESC, 
	vpp.ITEMMJ,
	vpp.VNDDOCNMP, 
	vpp.VNDITNUM, 
	vpp.ORD, 
	vpp.BarcodeP, 
	vpp.LOCNCODE, 
	vpp.QTYSHPPD, 
	vpp.QTYPACK, 
	vpp.QTYPACKMJ,
	vpp.TIMEMODE,
	vpp.TIMEPREP, 
	vpp.TIMEUNIT, 
	vpp.DtProdT, 
	vpp.DtProdL, 
	vpp.SerNumT, 
	vpp.SerNumL, 
	vpp.VerT, 
	vpp.VerL, 
	vpp.TermID, 
	vpp.LSTMod, --psum.LSTMod, 
	vpp.DEX_ROW_ID, 
	vpp.QTYDOKON + ISNULL(psum.QTYODVEDENO, 0) AS QTYODVEDENO, --Pocet kusu odvedenych automaticky terminaly a pripadnym rucnim odvodem ze systemu
	ISNULL(psum.CNTODVEDENO, 0) AS CNTODVEDENO	--Slouzi k informaci, zda zapocitat pripravny cas do dalsiho vystupu(>0 => nezapocitat pripravny cas)
FROM         
	dbo.CZPRO_VPP 
AS vpp 
LEFT OUTER JOIN 
(
	SELECT CountEntries, SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, Count(*) AS CNTODVEDENO --, MAX(dateeve) as LSTMod
	FROM   dbo.Production
	GROUP BY CountEntries, SOPNUMBE, ITEMNMBR
) AS psum ON psum.CountEntries = vpp.CountEntries and psum.SOPNUMBE = vpp.SOPNUMBE AND psum.ITEMNMBR = vpp.ITEMNMBR
LEFT JOIN CZPRO_VPH AS vph ON vph.CountEntries=vpp.CountEntries and vph.SOPNUMBE=vpp.SOPNUMBE
WHERE ISNULL(vph.Active,1)=1

GO



-- /////////////////////////////////////////////////////////////////////////////////////////////////////
-- /////////////////////////////////////////////////////////////////////////////////////////////////////
-- ///////////////////////////////////////Procedura/////////////////////////////////////////////////////
-- /////////////////////////////////////////////////////////////////////////////////////////////////////
-- /////////////////////////////////////////////////////////////////////////////////////////////////////


-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 7.7.2014
-- Description:	Last user production
-- =============================================
CREATE PROCEDURE fask_CZPRO_LastUserAction 
	-- Add the parameters for the stored procedure here
	@loginid varchar(20), 
	@machineid varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 * from Production
	where loginid=@loginid and machineid=@machineid
	order by dateeve desc
END
GO





